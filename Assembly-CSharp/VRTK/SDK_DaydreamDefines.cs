using System;

namespace VRTK
{
	// Token: 0x0200026B RID: 619
	public static class SDK_DaydreamDefines
	{
		// Token: 0x06001281 RID: 4737 RVA: 0x0006947C File Offset: 0x0006767C
		[SDK_ScriptingDefineSymbolPredicate("VRTK_DEFINE_SDK_DAYDREAM", "Android")]
		private static bool IsDaydreamAvailable()
		{
			return VRTK_SharedMethods.GetTypeUnknownAssembly("GvrController") != null;
		}

		// Token: 0x040010AD RID: 4269
		public const string ScriptingDefineSymbol = "VRTK_DEFINE_SDK_DAYDREAM";
	}
}
