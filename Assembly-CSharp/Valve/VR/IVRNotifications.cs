using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x0200038C RID: 908
	public struct IVRNotifications
	{
		// Token: 0x04001911 RID: 6417
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRNotifications._CreateNotification CreateNotification;

		// Token: 0x04001912 RID: 6418
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRNotifications._RemoveNotification RemoveNotification;

		// Token: 0x02000748 RID: 1864
		// (Invoke) Token: 0x06002F27 RID: 12071
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRNotificationError _CreateNotification(ulong ulOverlayHandle, ulong ulUserValue, EVRNotificationType type, string pchText, EVRNotificationStyle style, ref NotificationBitmap_t pImage, ref uint pNotificationId);

		// Token: 0x02000749 RID: 1865
		// (Invoke) Token: 0x06002F2B RID: 12075
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRNotificationError _RemoveNotification(uint notificationId);
	}
}
