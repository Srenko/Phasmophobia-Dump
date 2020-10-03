using System;

namespace Valve.VR
{
	// Token: 0x020003A8 RID: 936
	public enum ETrackedPropertyError
	{
		// Token: 0x040019D2 RID: 6610
		TrackedProp_Success,
		// Token: 0x040019D3 RID: 6611
		TrackedProp_WrongDataType,
		// Token: 0x040019D4 RID: 6612
		TrackedProp_WrongDeviceClass,
		// Token: 0x040019D5 RID: 6613
		TrackedProp_BufferTooSmall,
		// Token: 0x040019D6 RID: 6614
		TrackedProp_UnknownProperty,
		// Token: 0x040019D7 RID: 6615
		TrackedProp_InvalidDevice,
		// Token: 0x040019D8 RID: 6616
		TrackedProp_CouldNotContactServer,
		// Token: 0x040019D9 RID: 6617
		TrackedProp_ValueNotProvidedByDevice,
		// Token: 0x040019DA RID: 6618
		TrackedProp_StringExceedsMaximumLength,
		// Token: 0x040019DB RID: 6619
		TrackedProp_NotYetAvailable,
		// Token: 0x040019DC RID: 6620
		TrackedProp_PermissionDenied,
		// Token: 0x040019DD RID: 6621
		TrackedProp_InvalidOperation
	}
}
