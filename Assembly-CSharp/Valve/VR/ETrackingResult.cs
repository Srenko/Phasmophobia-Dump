using System;

namespace Valve.VR
{
	// Token: 0x020003A3 RID: 931
	public enum ETrackingResult
	{
		// Token: 0x04001946 RID: 6470
		Uninitialized = 1,
		// Token: 0x04001947 RID: 6471
		Calibrating_InProgress = 100,
		// Token: 0x04001948 RID: 6472
		Calibrating_OutOfRange,
		// Token: 0x04001949 RID: 6473
		Running_OK = 200,
		// Token: 0x0400194A RID: 6474
		Running_OutOfRange
	}
}
