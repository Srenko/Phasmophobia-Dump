using System;
using System.Reflection;

namespace VRTK
{
	// Token: 0x0200027F RID: 639
	public static class SDK_SteamVRDefines
	{
		// Token: 0x06001346 RID: 4934 RVA: 0x0006BD34 File Offset: 0x00069F34
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_SDK_STEAMVR", "Standalone")]
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_STEAMVR_PLUGIN_1_2_2_OR_NEWER", "Standalone")]
		private static bool IsPluginVersion122OrNewer()
		{
			Type typeUnknownAssembly = VRTK_SharedMethods.GetTypeUnknownAssembly("SteamVR_ControllerManager");
			return !(typeUnknownAssembly == null) && typeUnknownAssembly.GetMethod("SetUniqueObject", BindingFlags.Instance | BindingFlags.NonPublic) != null;
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0006BD6C File Offset: 0x00069F6C
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_SDK_STEAMVR", "Standalone")]
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_STEAMVR_PLUGIN_1_2_1_OR_NEWER", "Standalone")]
		private static bool IsPluginVersion121OrNewer()
		{
			Type typeUnknownAssembly = VRTK_SharedMethods.GetTypeUnknownAssembly("SteamVR_Events");
			if (typeUnknownAssembly == null)
			{
				return false;
			}
			MethodInfo method = typeUnknownAssembly.GetMethod("System", BindingFlags.Static | BindingFlags.Public);
			if (method == null)
			{
				return false;
			}
			ParameterInfo[] parameters = method.GetParameters();
			return parameters.Length == 1 && parameters[0].ParameterType == VRTK_SharedMethods.GetTypeUnknownAssembly("Valve.VR.EVREventType");
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0006BDD0 File Offset: 0x00069FD0
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_SDK_STEAMVR", "Standalone")]
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_STEAMVR_PLUGIN_1_2_0", "Standalone")]
		private static bool IsPluginVersion120()
		{
			Type typeUnknownAssembly = VRTK_SharedMethods.GetTypeUnknownAssembly("SteamVR_Events");
			if (typeUnknownAssembly == null)
			{
				return false;
			}
			MethodInfo method = typeUnknownAssembly.GetMethod("System", BindingFlags.Static | BindingFlags.Public);
			if (method == null)
			{
				return false;
			}
			ParameterInfo[] parameters = method.GetParameters();
			return parameters.Length == 1 && parameters[0].ParameterType == typeof(string);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0006BE34 File Offset: 0x0006A034
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_SDK_STEAMVR", "Standalone")]
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_STEAMVR_PLUGIN_1_1_1_OR_OLDER", "Standalone")]
		private static bool IsPluginVersion111OrOlder()
		{
			Type typeUnknownAssembly = VRTK_SharedMethods.GetTypeUnknownAssembly("SteamVR_Utils");
			if (typeUnknownAssembly == null)
			{
				return false;
			}
			Type nestedType = typeUnknownAssembly.GetNestedType("Event");
			return !(nestedType == null) && nestedType.GetMethod("Listen", BindingFlags.Static | BindingFlags.Public) != null;
		}

		// Token: 0x040010F9 RID: 4345
		public const string ScriptingDefineSymbol = "VRTK_DEFINE_SDK_STEAMVR";

		// Token: 0x040010FA RID: 4346
		private const string BuildTargetGroupName = "Standalone";
	}
}
