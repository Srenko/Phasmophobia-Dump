using System;

namespace Valve.VR
{
	// Token: 0x020003B3 RID: 947
	public enum EVROverlayError
	{
		// Token: 0x04001A90 RID: 6800
		None,
		// Token: 0x04001A91 RID: 6801
		UnknownOverlay = 10,
		// Token: 0x04001A92 RID: 6802
		InvalidHandle,
		// Token: 0x04001A93 RID: 6803
		PermissionDenied,
		// Token: 0x04001A94 RID: 6804
		OverlayLimitExceeded,
		// Token: 0x04001A95 RID: 6805
		WrongVisibilityType,
		// Token: 0x04001A96 RID: 6806
		KeyTooLong,
		// Token: 0x04001A97 RID: 6807
		NameTooLong,
		// Token: 0x04001A98 RID: 6808
		KeyInUse,
		// Token: 0x04001A99 RID: 6809
		WrongTransformType,
		// Token: 0x04001A9A RID: 6810
		InvalidTrackedDevice,
		// Token: 0x04001A9B RID: 6811
		InvalidParameter,
		// Token: 0x04001A9C RID: 6812
		ThumbnailCantBeDestroyed,
		// Token: 0x04001A9D RID: 6813
		ArrayTooSmall,
		// Token: 0x04001A9E RID: 6814
		RequestFailed,
		// Token: 0x04001A9F RID: 6815
		InvalidTexture,
		// Token: 0x04001AA0 RID: 6816
		UnableToLoadFile,
		// Token: 0x04001AA1 RID: 6817
		KeyboardAlreadyInUse,
		// Token: 0x04001AA2 RID: 6818
		NoNeighbor,
		// Token: 0x04001AA3 RID: 6819
		TooManyMaskPrimitives = 29,
		// Token: 0x04001AA4 RID: 6820
		BadMaskPrimitive
	}
}
