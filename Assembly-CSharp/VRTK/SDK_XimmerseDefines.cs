using System;

namespace VRTK
{
	// Token: 0x02000285 RID: 645
	public static class SDK_XimmerseDefines
	{
		// Token: 0x060013C2 RID: 5058 RVA: 0x0006C6BD File Offset: 0x0006A8BD
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_SDK_XIMMERSE", "Standalone")]
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_SDK_XIMMERSE", "Android")]
		private static bool IsXimmerseAvailable()
		{
			return VRTK_SharedMethods.GetTypeUnknownAssembly("Ximmerse.InputSystem.XDevicePlugin") != null;
		}

		// Token: 0x040010FF RID: 4351
		public const string ScriptingDefineSymbol = "VRTK_DEFINE_SDK_XIMMERSE";
	}
}
