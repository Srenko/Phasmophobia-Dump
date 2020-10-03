using System;

namespace VRTK
{
	// Token: 0x020002B6 RID: 694
	public static class VRTK_Defines
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0007BEC8 File Offset: 0x0007A0C8
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x0007BECF File Offset: 0x0007A0CF
		public static string CurrentExactVersionScriptingDefineSymbol { get; private set; } = VRTK_Defines.ExactVersionSymbol(VRTK_Defines.CurrentVersion);

		// Token: 0x06001718 RID: 5912 RVA: 0x0007BF16 File Offset: 0x0007A116
		private static string ExactVersionSymbol(Version version)
		{
			return string.Format("{0}{1}", "VRTK_VERSION_", version.ToString().Replace(".", "_"));
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0007BF3C File Offset: 0x0007A13C
		private static string AtLeastVersionSymbol(Version version)
		{
			return string.Format("{0}{1}", VRTK_Defines.ExactVersionSymbol(version), "_OR_NEWER");
		}

		// Token: 0x040012EB RID: 4843
		public static readonly Version CurrentVersion = new Version(3, 2, 1);

		// Token: 0x040012EC RID: 4844
		public static readonly Version[] PreviousVersions = new Version[]
		{
			new Version(3, 1, 0),
			new Version(3, 2, 0)
		};

		// Token: 0x040012EE RID: 4846
		public const string VersionScriptingDefineSymbolPrefix = "VRTK_VERSION_";

		// Token: 0x040012EF RID: 4847
		public const string VersionScriptingDefineSymbolSuffix = "_OR_NEWER";
	}
}
