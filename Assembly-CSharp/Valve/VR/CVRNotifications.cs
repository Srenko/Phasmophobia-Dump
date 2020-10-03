using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x0200039A RID: 922
	public class CVRNotifications
	{
		// Token: 0x06001FE0 RID: 8160 RVA: 0x0009E237 File Offset: 0x0009C437
		internal CVRNotifications(IntPtr pInterface)
		{
			this.FnTable = (IVRNotifications)Marshal.PtrToStructure(pInterface, typeof(IVRNotifications));
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x0009E25A File Offset: 0x0009C45A
		public EVRNotificationError CreateNotification(ulong ulOverlayHandle, ulong ulUserValue, EVRNotificationType type, string pchText, EVRNotificationStyle style, ref NotificationBitmap_t pImage, ref uint pNotificationId)
		{
			pNotificationId = 0U;
			return this.FnTable.CreateNotification(ulOverlayHandle, ulUserValue, type, pchText, style, ref pImage, ref pNotificationId);
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x0009E27B File Offset: 0x0009C47B
		public EVRNotificationError RemoveNotification(uint notificationId)
		{
			return this.FnTable.RemoveNotification(notificationId);
		}

		// Token: 0x04001933 RID: 6451
		private IVRNotifications FnTable;
	}
}
