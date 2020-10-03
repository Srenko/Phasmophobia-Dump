using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;

namespace VRTK
{
	// Token: 0x0200030D RID: 781
	public sealed class VRTK_SDKManager : MonoBehaviour
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0008E291 File Offset: 0x0008C491
		// (set) Token: 0x06001B2B RID: 6955 RVA: 0x0008E298 File Offset: 0x0008C498
		public static ReadOnlyCollection<VRTK_SDKManager.ScriptingDefineSymbolPredicateInfo> AvailableScriptingDefineSymbolPredicateInfos { get; private set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0008E2A0 File Offset: 0x0008C4A0
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x0008E2A7 File Offset: 0x0008C4A7
		public static ReadOnlyCollection<VRTK_SDKInfo> AvailableSystemSDKInfos { get; private set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0008E2AF File Offset: 0x0008C4AF
		// (set) Token: 0x06001B2F RID: 6959 RVA: 0x0008E2B6 File Offset: 0x0008C4B6
		public static ReadOnlyCollection<VRTK_SDKInfo> AvailableBoundariesSDKInfos { get; private set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x0008E2BE File Offset: 0x0008C4BE
		// (set) Token: 0x06001B31 RID: 6961 RVA: 0x0008E2C5 File Offset: 0x0008C4C5
		public static ReadOnlyCollection<VRTK_SDKInfo> AvailableHeadsetSDKInfos { get; private set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0008E2CD File Offset: 0x0008C4CD
		// (set) Token: 0x06001B33 RID: 6963 RVA: 0x0008E2D4 File Offset: 0x0008C4D4
		public static ReadOnlyCollection<VRTK_SDKInfo> AvailableControllerSDKInfos { get; private set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0008E2DC File Offset: 0x0008C4DC
		// (set) Token: 0x06001B35 RID: 6965 RVA: 0x0008E2E3 File Offset: 0x0008C4E3
		public static ReadOnlyCollection<VRTK_SDKInfo> InstalledSystemSDKInfos { get; private set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0008E2EB File Offset: 0x0008C4EB
		// (set) Token: 0x06001B37 RID: 6967 RVA: 0x0008E2F2 File Offset: 0x0008C4F2
		public static ReadOnlyCollection<VRTK_SDKInfo> InstalledBoundariesSDKInfos { get; private set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0008E2FA File Offset: 0x0008C4FA
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x0008E301 File Offset: 0x0008C501
		public static ReadOnlyCollection<VRTK_SDKInfo> InstalledHeadsetSDKInfos { get; private set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0008E309 File Offset: 0x0008C509
		// (set) Token: 0x06001B3B RID: 6971 RVA: 0x0008E310 File Offset: 0x0008C510
		public static ReadOnlyCollection<VRTK_SDKInfo> InstalledControllerSDKInfos { get; private set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0008E318 File Offset: 0x0008C518
		public static VRTK_SDKManager instance
		{
			get
			{
				if (VRTK_SDKManager._instance == null)
				{
					VRTK_SDKManager vrtk_SDKManager = VRTK_SharedMethods.FindEvenInactiveComponent<VRTK_SDKManager>();
					if (vrtk_SDKManager != null)
					{
						vrtk_SDKManager.CreateInstance();
					}
				}
				return VRTK_SDKManager._instance;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0008E34C File Offset: 0x0008C54C
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x0008E354 File Offset: 0x0008C554
		public VRTK_SDKSetup loadedSetup { get; private set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0008E35D File Offset: 0x0008C55D
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x0008E365 File Offset: 0x0008C565
		public ReadOnlyCollection<Behaviour> behavioursToToggleOnLoadedSetupChange { get; private set; }

		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06001B41 RID: 6977 RVA: 0x0008E370 File Offset: 0x0008C570
		// (remove) Token: 0x06001B42 RID: 6978 RVA: 0x0008E3A8 File Offset: 0x0008C5A8
		public event VRTK_SDKManager.LoadedSetupChangeEventHandler LoadedSetupChanged;

		// Token: 0x06001B43 RID: 6979 RVA: 0x0008E3E0 File Offset: 0x0008C5E0
		public void AddBehaviourToToggleOnLoadedSetupChange(Behaviour behaviour)
		{
			if (!this._behavioursToToggleOnLoadedSetupChange.Contains(behaviour))
			{
				this._behavioursToToggleOnLoadedSetupChange.Add(behaviour);
				this._behavioursInitialState.Add(behaviour, behaviour.enabled);
			}
			if (this.loadedSetup == null && behaviour.enabled)
			{
				behaviour.enabled = false;
			}
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0008E436 File Offset: 0x0008C636
		public void RemoveBehaviourToToggleOnLoadedSetupChange(Behaviour behaviour)
		{
			this._behavioursToToggleOnLoadedSetupChange.Remove(behaviour);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0008E448 File Offset: 0x0008C648
		public void TryLoadSDKSetupFromList(bool tryUseLastLoadedSetup = true)
		{
			int num = 0;
			if (tryUseLastLoadedSetup && VRTK_SDKManager._previouslyUsedSetupInfos.Count > 0)
			{
				num = Array.FindIndex<VRTK_SDKSetup>(this.setups, (VRTK_SDKSetup setup) => VRTK_SDKManager._previouslyUsedSetupInfos.SetEquals(new VRTK_SDKInfo[]
				{
					setup.systemSDKInfo,
					setup.boundariesSDKInfo,
					setup.headsetSDKInfo,
					setup.controllerSDKInfo
				}));
			}
			else if (XRSettings.enabled)
			{
				num = Array.FindIndex<VRTK_SDKSetup>(this.setups, (VRTK_SDKSetup setup) => setup.usedVRDeviceNames.Contains(XRSettings.loadedDeviceName));
			}
			else
			{
				string[] commandLineArgs = Environment.GetCommandLineArgs();
				int num2 = Array.IndexOf<string>(commandLineArgs, "-vrmode", 1);
				if (XRSettings.loadedDeviceName == "None" || (num2 != -1 && num2 + 1 < commandLineArgs.Length && commandLineArgs[num2 + 1].ToLowerInvariant() == "none"))
				{
					num = Array.FindIndex<VRTK_SDKSetup>(this.setups, (VRTK_SDKSetup setup) => setup.usedVRDeviceNames.All((string vrDeviceName) => vrDeviceName == "None"));
				}
			}
			num = ((num == -1) ? 0 : num);
			this.TryLoadSDKSetup(num, false, this.setups.ToArray<VRTK_SDKSetup>());
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0008E558 File Offset: 0x0008C758
		public void TryLoadSDKSetup(int startIndex, bool tryToReinitialize, params VRTK_SDKSetup[] sdkSetups)
		{
			if (sdkSetups.Length == 0)
			{
				return;
			}
			if (startIndex < 0 || startIndex >= sdkSetups.Length)
			{
				VRTK_Logger.Fatal(new ArgumentOutOfRangeException("startIndex"));
				return;
			}
			sdkSetups = sdkSetups.ToList<VRTK_SDKSetup>().GetRange(startIndex, sdkSetups.Length - startIndex).ToArray();
			foreach (VRTK_SDKSetup vrtk_SDKSetup in from setup in sdkSetups
			where !setup.isValid
			select setup)
			{
				string text = string.Join("\n- ", vrtk_SDKSetup.GetSimplifiedErrorDescriptions());
				if (!string.IsNullOrEmpty(text))
				{
					text = "- " + text;
					VRTK_Logger.Warn(string.Format("Ignoring SDK Setup '{0}' because there are some errors with it:\n{1}", vrtk_SDKSetup.name, text));
				}
			}
			sdkSetups = (from setup in sdkSetups
			where setup.isValid
			select setup).ToArray<VRTK_SDKSetup>();
			VRTK_SDKSetup loadedSetup = this.loadedSetup;
			this.ToggleBehaviours(false);
			this.loadedSetup = null;
			if (loadedSetup != null)
			{
				loadedSetup.OnUnloaded(this);
			}
			if (!XRSettings.enabled || !sdkSetups[0].usedVRDeviceNames.Contains(XRSettings.loadedDeviceName))
			{
				if (!tryToReinitialize && !XRSettings.enabled && !string.IsNullOrEmpty(XRSettings.loadedDeviceName))
				{
					sdkSetups = (from setup in sdkSetups
					where !setup.usedVRDeviceNames.Contains(XRSettings.loadedDeviceName)
					select setup).ToArray<VRTK_SDKSetup>();
				}
				VRTK_SDKSetup[] array = (from setup in sdkSetups
				where setup.usedVRDeviceNames.Except(XRSettings.supportedDevices).Any<string>()
				select setup).ToArray<VRTK_SDKSetup>();
				foreach (VRTK_SDKSetup vrtk_SDKSetup2 in array)
				{
					string arg = string.Join(", ", vrtk_SDKSetup2.usedVRDeviceNames.Except(XRSettings.supportedDevices).ToArray<string>());
					VRTK_Logger.Warn(string.Format("Ignoring SDK Setup '{0}' because the following VR device names are missing from the PlayerSettings:\n{1}", vrtk_SDKSetup2.name, arg));
				}
				sdkSetups = sdkSetups.Except(array).ToArray<VRTK_SDKSetup>();
				XRSettings.LoadDeviceByName(sdkSetups.SelectMany((VRTK_SDKSetup setup) => setup.usedVRDeviceNames).Distinct<string>().Concat(new string[]
				{
					"None"
				}).ToArray<string>());
			}
			base.StartCoroutine(this.FinishSDKSetupLoading(sdkSetups, loadedSetup));
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0008E7CC File Offset: 0x0008C9CC
		public void UnloadSDKSetup(bool disableVR = false)
		{
			if (this.loadedSetup != null)
			{
				this.ToggleBehaviours(false);
			}
			VRTK_SDKSetup loadedSetup = this.loadedSetup;
			this.loadedSetup = null;
			if (loadedSetup != null)
			{
				loadedSetup.OnUnloaded(this);
			}
			if (disableVR)
			{
				XRSettings.LoadDeviceByName("None");
				XRSettings.enabled = false;
			}
			if (loadedSetup != null)
			{
				this.OnLoadedSetupChanged(new VRTK_SDKManager.LoadedSetupChangeEventArgs(loadedSetup, null, null));
			}
			VRTK_SDKManager._previouslyUsedSetupInfos.Clear();
			if (loadedSetup != null)
			{
				VRTK_SDKManager._previouslyUsedSetupInfos.UnionWith(new VRTK_SDKInfo[]
				{
					loadedSetup.systemSDKInfo,
					loadedSetup.boundariesSDKInfo,
					loadedSetup.headsetSDKInfo,
					loadedSetup.controllerSDKInfo
				});
			}
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0008E880 File Offset: 0x0008CA80
		static VRTK_SDKManager()
		{
			VRTK_SDKManager.PopulateAvailableScriptingDefineSymbolPredicateInfos();
			VRTK_SDKManager.PopulateAvailableAndInstalledSDKInfos();
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0008E913 File Offset: 0x0008CB13
		private void OnEnable()
		{
			this.behavioursToToggleOnLoadedSetupChange = this._behavioursToToggleOnLoadedSetupChange.AsReadOnly();
			this.CreateInstance();
			if (this.autoLoadSetup)
			{
				this.TryLoadSDKSetupFromList(true);
			}
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0008E93B File Offset: 0x0008CB3B
		private void OnDisable()
		{
			if (VRTK_SDKManager._instance == this && !this.persistOnLoad)
			{
				this.UnloadSDKSetup(false);
			}
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0008E95C File Offset: 0x0008CB5C
		private void CreateInstance()
		{
			if (VRTK_SDKManager._instance == null)
			{
				VRTK_SDKManager._instance = this;
				VRTK_SDK_Bridge.InvalidateCaches();
				if (this.persistOnLoad && Application.isPlaying)
				{
					Object.DontDestroyOnLoad(base.gameObject);
					return;
				}
			}
			else if (VRTK_SDKManager._instance != this)
			{
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0008E9B4 File Offset: 0x0008CBB4
		private void OnLoadedSetupChanged(VRTK_SDKManager.LoadedSetupChangeEventArgs e)
		{
			VRTK_SDKManager.LoadedSetupChangeEventHandler loadedSetupChanged = this.LoadedSetupChanged;
			if (loadedSetupChanged != null)
			{
				loadedSetupChanged(this, e);
			}
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0008E9D3 File Offset: 0x0008CBD3
		private IEnumerator FinishSDKSetupLoading(VRTK_SDKSetup[] sdkSetups, VRTK_SDKSetup previousLoadedSetup)
		{
			yield return null;
			string loadedDeviceName = string.IsNullOrEmpty(XRSettings.loadedDeviceName) ? "None" : XRSettings.loadedDeviceName;
			this.loadedSetup = sdkSetups.FirstOrDefault((VRTK_SDKSetup setup) => setup.usedVRDeviceNames.Contains(loadedDeviceName));
			if (this.loadedSetup == null)
			{
				this.UnloadSDKSetup(false);
				VRTK_Logger.Error("No SDK Setup from the provided list could be loaded.");
				this.OnLoadedSetupChanged(new VRTK_SDKManager.LoadedSetupChangeEventArgs(previousLoadedSetup, null, "No SDK Setup from the provided list could be loaded."));
				yield break;
			}
			if (this.loadedSetup.usedVRDeviceNames.Except(new string[]
			{
				"None"
			}).Any<string>())
			{
				XRSettings.enabled = true;
				if (!XRDevice.isPresent)
				{
					int num = Array.IndexOf<VRTK_SDKSetup>(sdkSetups, this.loadedSetup) + 1;
					string text = "An SDK Setup from the provided list could be loaded, but the device is not in working order.";
					this.ToggleBehaviours(false);
					this.loadedSetup = null;
					if (num < sdkSetups.Length && sdkSetups.Length - num > 0)
					{
						text += " Now retrying with the remaining SDK Setups from the provided list...";
						VRTK_Logger.Warn(text);
						this.OnLoadedSetupChanged(new VRTK_SDKManager.LoadedSetupChangeEventArgs(previousLoadedSetup, null, text));
						this.TryLoadSDKSetup(num, false, sdkSetups);
						yield break;
					}
					this.UnloadSDKSetup(false);
					text += " There are no other Setups in the provided list to try.";
					VRTK_Logger.Error(text);
					this.OnLoadedSetupChanged(new VRTK_SDKManager.LoadedSetupChangeEventArgs(previousLoadedSetup, null, text));
					yield break;
				}
			}
			this.loadedSetup.OnLoaded(this);
			this.ToggleBehaviours(true);
			this.OnLoadedSetupChanged(new VRTK_SDKManager.LoadedSetupChangeEventArgs(previousLoadedSetup, this.loadedSetup, null));
			yield break;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0008E9F0 File Offset: 0x0008CBF0
		private void ToggleBehaviours(bool state)
		{
			List<Behaviour> list = this._behavioursToToggleOnLoadedSetupChange.ToList<Behaviour>();
			if (!state)
			{
				list.Reverse();
			}
			for (int i = 0; i < list.Count; i++)
			{
				Behaviour behaviour = list[i];
				if (behaviour == null)
				{
					VRTK_Logger.Error(string.Format("A behaviour to toggle has been destroyed. Have you forgot the corresponding call `VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this)` in the `OnDestroy` method of `{0}`?", behaviour.GetType()));
					this._behavioursToToggleOnLoadedSetupChange.RemoveAt(state ? i : (this._behavioursToToggleOnLoadedSetupChange.Count - 1 - i));
				}
				else
				{
					behaviour.enabled = ((state && this._behavioursInitialState.ContainsKey(behaviour)) ? this._behavioursInitialState[behaviour] : state);
				}
			}
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0008EA94 File Offset: 0x0008CC94
		private static void PopulateAvailableScriptingDefineSymbolPredicateInfos()
		{
			List<VRTK_SDKManager.ScriptingDefineSymbolPredicateInfo> list = new List<VRTK_SDKManager.ScriptingDefineSymbolPredicateInfo>();
			foreach (Type type in typeof(VRTK_SDKManager).Assembly.GetTypes())
			{
				for (int j = 0; j < type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Length; j++)
				{
					MethodInfo methodInfo = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)[j];
					SDK_ScriptingDefineSymbolPredicateAttribute[] array = (SDK_ScriptingDefineSymbolPredicateAttribute[])methodInfo.GetCustomAttributes(typeof(SDK_ScriptingDefineSymbolPredicateAttribute), false);
					if (array.Length != 0)
					{
						if (methodInfo.ReturnType != typeof(bool) || methodInfo.GetParameters().Length != 0)
						{
							VRTK_Logger.Fatal(new InvalidOperationException(string.Format("The method '{0}' on '{1}' has '{2}' specified but its signature is wrong. The method must take no arguments and return bool.", methodInfo.Name, type, typeof(SDK_ScriptingDefineSymbolPredicateAttribute))));
							return;
						}
						list.AddRange(from predicateAttribute in array
						select new VRTK_SDKManager.ScriptingDefineSymbolPredicateInfo(predicateAttribute, methodInfo));
					}
				}
			}
			list.Sort((VRTK_SDKManager.ScriptingDefineSymbolPredicateInfo x, VRTK_SDKManager.ScriptingDefineSymbolPredicateInfo y) => string.Compare(x.attribute.symbol, y.attribute.symbol, StringComparison.Ordinal));
			VRTK_SDKManager.AvailableScriptingDefineSymbolPredicateInfos = list.AsReadOnly();
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0008EBD4 File Offset: 0x0008CDD4
		private static void PopulateAvailableAndInstalledSDKInfos()
		{
			List<string> symbolsOfInstalledSDKs = (from predicateInfo in VRTK_SDKManager.AvailableScriptingDefineSymbolPredicateInfos
			where (bool)predicateInfo.methodInfo.Invoke(null, null)
			select predicateInfo.attribute.symbol).ToList<string>();
			List<VRTK_SDKInfo> list = new List<VRTK_SDKInfo>();
			List<VRTK_SDKInfo> list2 = new List<VRTK_SDKInfo>();
			List<VRTK_SDKInfo> list3 = new List<VRTK_SDKInfo>();
			List<VRTK_SDKInfo> list4 = new List<VRTK_SDKInfo>();
			List<VRTK_SDKInfo> list5 = new List<VRTK_SDKInfo>();
			List<VRTK_SDKInfo> list6 = new List<VRTK_SDKInfo>();
			List<VRTK_SDKInfo> list7 = new List<VRTK_SDKInfo>();
			List<VRTK_SDKInfo> list8 = new List<VRTK_SDKInfo>();
			VRTK_SDKManager.PopulateAvailableAndInstalledSDKInfos<SDK_BaseSystem, SDK_FallbackSystem>(list, list5, symbolsOfInstalledSDKs);
			VRTK_SDKManager.PopulateAvailableAndInstalledSDKInfos<SDK_BaseBoundaries, SDK_FallbackBoundaries>(list2, list6, symbolsOfInstalledSDKs);
			VRTK_SDKManager.PopulateAvailableAndInstalledSDKInfos<SDK_BaseHeadset, SDK_FallbackHeadset>(list3, list7, symbolsOfInstalledSDKs);
			VRTK_SDKManager.PopulateAvailableAndInstalledSDKInfos<SDK_BaseController, SDK_FallbackController>(list4, list8, symbolsOfInstalledSDKs);
			VRTK_SDKManager.AvailableSystemSDKInfos = list.AsReadOnly();
			VRTK_SDKManager.AvailableBoundariesSDKInfos = list2.AsReadOnly();
			VRTK_SDKManager.AvailableHeadsetSDKInfos = list3.AsReadOnly();
			VRTK_SDKManager.AvailableControllerSDKInfos = list4.AsReadOnly();
			VRTK_SDKManager.InstalledSystemSDKInfos = list5.AsReadOnly();
			VRTK_SDKManager.InstalledBoundariesSDKInfos = list6.AsReadOnly();
			VRTK_SDKManager.InstalledHeadsetSDKInfos = list7.AsReadOnly();
			VRTK_SDKManager.InstalledControllerSDKInfos = list8.AsReadOnly();
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0008ECE8 File Offset: 0x0008CEE8
		private static void PopulateAvailableAndInstalledSDKInfos<BaseType, FallbackType>(List<VRTK_SDKInfo> availableSDKInfos, List<VRTK_SDKInfo> installedSDKInfos, ICollection<string> symbolsOfInstalledSDKs) where BaseType : SDK_Base where FallbackType : BaseType
		{
			Type baseType = typeof(BaseType);
			Type fallbackType = VRTK_SDKManager.SDKFallbackTypesByBaseType[baseType];
			availableSDKInfos.AddRange(VRTK_SDKInfo.Create<BaseType, FallbackType, FallbackType>());
			availableSDKInfos.AddRange((from type in baseType.Assembly.GetExportedTypes()
			where type.IsSubclassOf(baseType) && type != fallbackType && !type.IsAbstract
			select type).SelectMany(new Func<Type, IEnumerable<VRTK_SDKInfo>>(VRTK_SDKInfo.Create<BaseType, FallbackType>)));
			availableSDKInfos.Sort(delegate(VRTK_SDKInfo x, VRTK_SDKInfo y)
			{
				if (!x.description.describesFallbackSDK)
				{
					return string.Compare(x.description.prettyName, y.description.prettyName, StringComparison.Ordinal);
				}
				return -1;
			});
			installedSDKInfos.AddRange(availableSDKInfos.Where(delegate(VRTK_SDKInfo info)
			{
				string symbol = info.description.symbol;
				return string.IsNullOrEmpty(symbol) || symbolsOfInstalledSDKs.Contains(symbol);
			}));
		}

		// Token: 0x040015EE RID: 5614
		public static readonly Dictionary<Type, Type> SDKFallbackTypesByBaseType = new Dictionary<Type, Type>
		{
			{
				typeof(SDK_BaseSystem),
				typeof(SDK_FallbackSystem)
			},
			{
				typeof(SDK_BaseBoundaries),
				typeof(SDK_FallbackBoundaries)
			},
			{
				typeof(SDK_BaseHeadset),
				typeof(SDK_FallbackHeadset)
			},
			{
				typeof(SDK_BaseController),
				typeof(SDK_FallbackController)
			}
		};

		// Token: 0x040015F7 RID: 5623
		private static VRTK_SDKManager _instance;

		// Token: 0x040015F8 RID: 5624
		[Tooltip("**OBSOLETE. STOP USING THIS ASAP!** If this is true then the instance of the SDK Manager won't be destroyed on every scene load.")]
		[Obsolete("`VRTK_SDKManager.persistOnLoad` has been deprecated and will be removed in a future version of VRTK. See https://github.com/thestonefox/VRTK/issues/1316 for details.")]
		public bool persistOnLoad;

		// Token: 0x040015F9 RID: 5625
		[Tooltip("Determines whether the scripting define symbols required by the installed SDKs are automatically added to and removed from the player settings.")]
		public bool autoManageScriptDefines = true;

		// Token: 0x040015FA RID: 5626
		public List<SDK_ScriptingDefineSymbolPredicateAttribute> activeScriptingDefineSymbolsWithoutSDKClasses = new List<SDK_ScriptingDefineSymbolPredicateAttribute>();

		// Token: 0x040015FB RID: 5627
		[Tooltip("A reference to the GameObject that contains any scripts that apply to the Left Hand Controller.")]
		public GameObject scriptAliasLeftController;

		// Token: 0x040015FC RID: 5628
		[Tooltip("A reference to the GameObject that contains any scripts that apply to the Right Hand Controller.")]
		public GameObject scriptAliasRightController;

		// Token: 0x040015FD RID: 5629
		[Tooltip("Determines whether the VR settings of the Player Settings are automatically adjusted to allow for all the used SDKs in the SDK Setups list below.")]
		public bool autoManageVRSettings = true;

		// Token: 0x040015FE RID: 5630
		[Tooltip("Determines whether the SDK Setups list below is used whenever the SDK Manager is enabled. The first loadable Setup is then loaded.")]
		public bool autoLoadSetup = true;

		// Token: 0x040015FF RID: 5631
		[Tooltip("The list of SDK Setups to choose from.")]
		public VRTK_SDKSetup[] setups = new VRTK_SDKSetup[0];

		// Token: 0x04001601 RID: 5633
		private static HashSet<VRTK_SDKInfo> _previouslyUsedSetupInfos = new HashSet<VRTK_SDKInfo>();

		// Token: 0x04001603 RID: 5635
		private List<Behaviour> _behavioursToToggleOnLoadedSetupChange = new List<Behaviour>();

		// Token: 0x04001604 RID: 5636
		private Dictionary<Behaviour, bool> _behavioursInitialState = new Dictionary<Behaviour, bool>();

		// Token: 0x020005FD RID: 1533
		public sealed class ScriptingDefineSymbolPredicateInfo
		{
			// Token: 0x06002A86 RID: 10886 RVA: 0x000C8F28 File Offset: 0x000C7128
			public ScriptingDefineSymbolPredicateInfo(SDK_ScriptingDefineSymbolPredicateAttribute attribute, MethodInfo methodInfo)
			{
				this.attribute = attribute;
				this.methodInfo = methodInfo;
			}

			// Token: 0x0400284D RID: 10317
			public readonly SDK_ScriptingDefineSymbolPredicateAttribute attribute;

			// Token: 0x0400284E RID: 10318
			public readonly MethodInfo methodInfo;
		}

		// Token: 0x020005FE RID: 1534
		public struct LoadedSetupChangeEventArgs
		{
			// Token: 0x06002A87 RID: 10887 RVA: 0x000C8F3E File Offset: 0x000C713E
			public LoadedSetupChangeEventArgs(VRTK_SDKSetup previousSetup, VRTK_SDKSetup currentSetup, string errorMessage)
			{
				this.previousSetup = previousSetup;
				this.currentSetup = currentSetup;
				this.errorMessage = errorMessage;
			}

			// Token: 0x0400284F RID: 10319
			public readonly VRTK_SDKSetup previousSetup;

			// Token: 0x04002850 RID: 10320
			public readonly VRTK_SDKSetup currentSetup;

			// Token: 0x04002851 RID: 10321
			public readonly string errorMessage;
		}

		// Token: 0x020005FF RID: 1535
		// (Invoke) Token: 0x06002A89 RID: 10889
		public delegate void LoadedSetupChangeEventHandler(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e);
	}
}
