using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002B8 RID: 696
	public class VRTK_Logger : MonoBehaviour
	{
		// Token: 0x06001723 RID: 5923 RVA: 0x0007C15C File Offset: 0x0007A35C
		public static void CreateIfNotExists()
		{
			if (VRTK_Logger.instance == null)
			{
				VRTK_Logger.instance = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
				{
					"Logger"
				}))
				{
					hideFlags = (HideFlags.HideInHierarchy | HideFlags.DontSaveInEditor)
				}.AddComponent<VRTK_Logger>();
			}
			if (VRTK_Logger.commonMessageParts.Count != VRTK_Logger.commonMessages.Count)
			{
				VRTK_Logger.commonMessageParts.Clear();
				foreach (KeyValuePair<VRTK_Logger.CommonMessageKeys, string> keyValuePair in VRTK_Logger.commonMessages)
				{
					int value = Regex.Matches(keyValuePair.Value, "(?<!\\{)\\{([0-9]+).*?\\}(?!})").Cast<Match>().DefaultIfEmpty<Match>().Max(delegate(Match m)
					{
						if (m != null)
						{
							return int.Parse(m.Groups[1].Value);
						}
						return -1;
					}) + 1;
					VRTK_Logger.commonMessageParts.Add(keyValuePair.Key, value);
				}
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0007C258 File Offset: 0x0007A458
		public static string GetCommonMessage(VRTK_Logger.CommonMessageKeys messageKey, params object[] parameters)
		{
			VRTK_Logger.CreateIfNotExists();
			string result = "";
			if (VRTK_Logger.commonMessages.ContainsKey(messageKey))
			{
				if (parameters.Length != VRTK_Logger.commonMessageParts[messageKey])
				{
					Array.Resize<object>(ref parameters, VRTK_Logger.commonMessageParts[messageKey]);
				}
				result = string.Format(VRTK_Logger.commonMessages[messageKey], parameters);
			}
			return result;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0007C2B2 File Offset: 0x0007A4B2
		public static void Trace(string message)
		{
			VRTK_Logger.Log(VRTK_Logger.LogLevels.Trace, message);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0007C2BB File Offset: 0x0007A4BB
		public static void Debug(string message)
		{
			VRTK_Logger.Log(VRTK_Logger.LogLevels.Debug, message);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0007C2C4 File Offset: 0x0007A4C4
		public static void Info(string message)
		{
			VRTK_Logger.Log(VRTK_Logger.LogLevels.Info, message);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0007C2CD File Offset: 0x0007A4CD
		public static void Warn(string message)
		{
			VRTK_Logger.Log(VRTK_Logger.LogLevels.Warn, message);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0007C2D6 File Offset: 0x0007A4D6
		public static void Error(string message)
		{
			VRTK_Logger.Log(VRTK_Logger.LogLevels.Error, message);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0007C2DF File Offset: 0x0007A4DF
		public static void Fatal(string message)
		{
			VRTK_Logger.Log(VRTK_Logger.LogLevels.Fatal, message);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0007C2E8 File Offset: 0x0007A4E8
		public static void Fatal(Exception exception)
		{
			VRTK_Logger.Log(VRTK_Logger.LogLevels.Fatal, exception.Message);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0007C2F8 File Offset: 0x0007A4F8
		public static void Log(VRTK_Logger.LogLevels level, string message)
		{
			VRTK_Logger.CreateIfNotExists();
			if (VRTK_Logger.instance.minLevel > level)
			{
				return;
			}
			switch (level)
			{
			case VRTK_Logger.LogLevels.Trace:
			case VRTK_Logger.LogLevels.Debug:
			case VRTK_Logger.LogLevels.Info:
				UnityEngine.Debug.Log(message);
				return;
			case VRTK_Logger.LogLevels.Warn:
				UnityEngine.Debug.LogWarning(message);
				return;
			case VRTK_Logger.LogLevels.Error:
			case VRTK_Logger.LogLevels.Fatal:
				if (VRTK_Logger.instance.throwExceptions)
				{
					throw new Exception(message);
				}
				UnityEngine.Debug.LogError(message);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0007C35E File Offset: 0x0007A55E
		protected virtual void Awake()
		{
			VRTK_Logger.instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x040012F5 RID: 4853
		public static VRTK_Logger instance = null;

		// Token: 0x040012F6 RID: 4854
		public static Dictionary<VRTK_Logger.CommonMessageKeys, string> commonMessages = new Dictionary<VRTK_Logger.CommonMessageKeys, string>
		{
			{
				VRTK_Logger.CommonMessageKeys.NOT_DEFINED,
				"`{0}` not defined{1}."
			},
			{
				VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_SCENE,
				"`{0}` requires the `{1}` component to be available in the scene{2}."
			},
			{
				VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT,
				"`{0}` requires the `{1}` component to be attached to {2} GameObject{3}."
			},
			{
				VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_PARAMETER,
				"`{0}` requires a `{1}` component to be specified as the `{2}` parameter{3}."
			},
			{
				VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_NOT_INJECTED,
				"`{0}` requires the `{1}` component. Either the `{2}` parameter is not set or no `{1}` component is attached to {3} GameObject{4}."
			},
			{
				VRTK_Logger.CommonMessageKeys.COULD_NOT_FIND_OBJECT_FOR_ACTION,
				"The `{0}` could not automatically find {1} to {2}."
			},
			{
				VRTK_Logger.CommonMessageKeys.SDK_OBJECT_NOT_FOUND,
				"No {0} could be found. Have you selected a valid {1} in the SDK Manager? If you are unsure, then click the GameObject with the `VRTK_SDKManager` script attached to it in Edit Mode and select a {1} from the dropdown."
			},
			{
				VRTK_Logger.CommonMessageKeys.SDK_NOT_FOUND,
				"The SDK '{0}' doesn't exist anymore. The fallback SDK '{1}' will be used instead."
			},
			{
				VRTK_Logger.CommonMessageKeys.SDK_MANAGER_ERRORS,
				"The current SDK Manager setup is causing the following errors:\n\n{0}"
			},
			{
				VRTK_Logger.CommonMessageKeys.SCRIPTING_DEFINE_SYMBOLS_ADDED,
				"Scripting Define Symbols added to [Project Settings->Player] for {0}: {1}"
			},
			{
				VRTK_Logger.CommonMessageKeys.SCRIPTING_DEFINE_SYMBOLS_REMOVED,
				"Scripting Define Symbols removed from [Project Settings->Player] for {0}: {1}"
			}
		};

		// Token: 0x040012F7 RID: 4855
		public static Dictionary<VRTK_Logger.CommonMessageKeys, int> commonMessageParts = new Dictionary<VRTK_Logger.CommonMessageKeys, int>();

		// Token: 0x040012F8 RID: 4856
		public VRTK_Logger.LogLevels minLevel;

		// Token: 0x040012F9 RID: 4857
		public bool throwExceptions = true;

		// Token: 0x020005DE RID: 1502
		public enum LogLevels
		{
			// Token: 0x040027B6 RID: 10166
			Trace,
			// Token: 0x040027B7 RID: 10167
			Debug,
			// Token: 0x040027B8 RID: 10168
			Info,
			// Token: 0x040027B9 RID: 10169
			Warn,
			// Token: 0x040027BA RID: 10170
			Error,
			// Token: 0x040027BB RID: 10171
			Fatal,
			// Token: 0x040027BC RID: 10172
			None
		}

		// Token: 0x020005DF RID: 1503
		public enum CommonMessageKeys
		{
			// Token: 0x040027BE RID: 10174
			NOT_DEFINED,
			// Token: 0x040027BF RID: 10175
			REQUIRED_COMPONENT_MISSING_FROM_SCENE,
			// Token: 0x040027C0 RID: 10176
			REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT,
			// Token: 0x040027C1 RID: 10177
			REQUIRED_COMPONENT_MISSING_FROM_PARAMETER,
			// Token: 0x040027C2 RID: 10178
			REQUIRED_COMPONENT_MISSING_NOT_INJECTED,
			// Token: 0x040027C3 RID: 10179
			COULD_NOT_FIND_OBJECT_FOR_ACTION,
			// Token: 0x040027C4 RID: 10180
			SDK_OBJECT_NOT_FOUND,
			// Token: 0x040027C5 RID: 10181
			SDK_NOT_FOUND,
			// Token: 0x040027C6 RID: 10182
			SDK_MANAGER_ERRORS,
			// Token: 0x040027C7 RID: 10183
			SCRIPTING_DEFINE_SYMBOLS_ADDED,
			// Token: 0x040027C8 RID: 10184
			SCRIPTING_DEFINE_SYMBOLS_REMOVED
		}
	}
}
