using System;

namespace Valve.VR
{
	// Token: 0x020003BF RID: 959
	public enum ChaperoneCalibrationState
	{
		// Token: 0x04001B57 RID: 6999
		OK = 1,
		// Token: 0x04001B58 RID: 7000
		Warning = 100,
		// Token: 0x04001B59 RID: 7001
		Warning_BaseStationMayHaveMoved,
		// Token: 0x04001B5A RID: 7002
		Warning_BaseStationRemoved,
		// Token: 0x04001B5B RID: 7003
		Warning_SeatedBoundsInvalid,
		// Token: 0x04001B5C RID: 7004
		Error = 200,
		// Token: 0x04001B5D RID: 7005
		Error_BaseStationUninitialized,
		// Token: 0x04001B5E RID: 7006
		Error_BaseStationConflict,
		// Token: 0x04001B5F RID: 7007
		Error_PlayAreaInvalid,
		// Token: 0x04001B60 RID: 7008
		Error_CollisionBoundsInvalid
	}
}
