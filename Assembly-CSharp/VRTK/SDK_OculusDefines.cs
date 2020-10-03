using System;
using System.Reflection;

namespace VRTK
{
	// Token: 0x02000274 RID: 628
	public static class SDK_OculusDefines
	{
		// Token: 0x060012B9 RID: 4793 RVA: 0x000694F8 File Offset: 0x000676F8
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_SDK_OCULUS", "Standalone")]
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_OCULUS_UTILITIES_1_12_0_OR_NEWER", "Standalone")]
		private static bool IsUtilitiesVersion1120OrNewer()
		{
			Version oculusWrapperVersion = SDK_OculusDefines.GetOculusWrapperVersion();
			return oculusWrapperVersion != null && oculusWrapperVersion >= new Version(1, 12, 0);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00069528 File Offset: 0x00067728
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_SDK_OCULUS", "Standalone")]
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_OCULUS_UTILITIES_1_11_0_OR_OLDER", "Standalone")]
		private static bool IsUtilitiesVersion1110OrOlder()
		{
			Version oculusWrapperVersion = SDK_OculusDefines.GetOculusWrapperVersion();
			return oculusWrapperVersion != null && oculusWrapperVersion < new Version(1, 12, 0);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00069555 File Offset: 0x00067755
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_SDK_OCULUS_AVATAR", "Standalone")]
		private static bool IsAvatarAvailable()
		{
			return (SDK_OculusDefines.IsUtilitiesVersion1120OrNewer() || SDK_OculusDefines.IsUtilitiesVersion1110OrOlder()) && VRTK_SharedMethods.GetTypeUnknownAssembly("OvrAvatar") != null;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00069578 File Offset: 0x00067778
		private static Version GetOculusWrapperVersion()
		{
			Type typeUnknownAssembly = VRTK_SharedMethods.GetTypeUnknownAssembly("OVRPlugin");
			if (typeUnknownAssembly == null)
			{
				return null;
			}
			FieldInfo field = typeUnknownAssembly.GetField("wrapperVersion", BindingFlags.Static | BindingFlags.Public);
			if (field == null)
			{
				return null;
			}
			return (Version)field.GetValue(null);
		}

		// Token: 0x040010AE RID: 4270
		public const string ScriptingDefineSymbol = "VRTK_DEFINE_SDK_OCULUS";

		// Token: 0x040010AF RID: 4271
		public const string AvatarScriptingDefineSymbol = "VRTK_DEFINE_SDK_OCULUS_AVATAR";

		// Token: 0x040010B0 RID: 4272
		private const string BuildTargetGroupName = "Standalone";
	}
}
