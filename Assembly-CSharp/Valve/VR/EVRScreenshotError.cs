using System;

namespace Valve.VR
{
	// Token: 0x020003D0 RID: 976
	public enum EVRScreenshotError
	{
		// Token: 0x04001BCA RID: 7114
		None,
		// Token: 0x04001BCB RID: 7115
		RequestFailed,
		// Token: 0x04001BCC RID: 7116
		IncompatibleVersion = 100,
		// Token: 0x04001BCD RID: 7117
		NotFound,
		// Token: 0x04001BCE RID: 7118
		BufferTooSmall,
		// Token: 0x04001BCF RID: 7119
		ScreenshotAlreadyInProgress = 108
	}
}
