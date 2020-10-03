using System;

namespace Valve.VR
{
	// Token: 0x020003CF RID: 975
	public enum EVRSettingsError
	{
		// Token: 0x04001BC3 RID: 7107
		None,
		// Token: 0x04001BC4 RID: 7108
		IPCFailed,
		// Token: 0x04001BC5 RID: 7109
		WriteFailed,
		// Token: 0x04001BC6 RID: 7110
		ReadFailed,
		// Token: 0x04001BC7 RID: 7111
		JsonParseFailed,
		// Token: 0x04001BC8 RID: 7112
		UnsetSettingHasNoDefault
	}
}
