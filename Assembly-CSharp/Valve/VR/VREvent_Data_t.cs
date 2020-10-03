using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x020003D1 RID: 977
	[StructLayout(LayoutKind.Explicit)]
	public struct VREvent_Data_t
	{
		// Token: 0x04001BD0 RID: 7120
		[FieldOffset(0)]
		public VREvent_Reserved_t reserved;

		// Token: 0x04001BD1 RID: 7121
		[FieldOffset(0)]
		public VREvent_Controller_t controller;

		// Token: 0x04001BD2 RID: 7122
		[FieldOffset(0)]
		public VREvent_Mouse_t mouse;

		// Token: 0x04001BD3 RID: 7123
		[FieldOffset(0)]
		public VREvent_Scroll_t scroll;

		// Token: 0x04001BD4 RID: 7124
		[FieldOffset(0)]
		public VREvent_Process_t process;

		// Token: 0x04001BD5 RID: 7125
		[FieldOffset(0)]
		public VREvent_Notification_t notification;

		// Token: 0x04001BD6 RID: 7126
		[FieldOffset(0)]
		public VREvent_Overlay_t overlay;

		// Token: 0x04001BD7 RID: 7127
		[FieldOffset(0)]
		public VREvent_Status_t status;

		// Token: 0x04001BD8 RID: 7128
		[FieldOffset(0)]
		public VREvent_Ipd_t ipd;

		// Token: 0x04001BD9 RID: 7129
		[FieldOffset(0)]
		public VREvent_Chaperone_t chaperone;

		// Token: 0x04001BDA RID: 7130
		[FieldOffset(0)]
		public VREvent_PerformanceTest_t performanceTest;

		// Token: 0x04001BDB RID: 7131
		[FieldOffset(0)]
		public VREvent_TouchPadMove_t touchPadMove;

		// Token: 0x04001BDC RID: 7132
		[FieldOffset(0)]
		public VREvent_SeatedZeroPoseReset_t seatedZeroPoseReset;

		// Token: 0x04001BDD RID: 7133
		[FieldOffset(0)]
		public VREvent_Screenshot_t screenshot;

		// Token: 0x04001BDE RID: 7134
		[FieldOffset(0)]
		public VREvent_ScreenshotProgress_t screenshotProgress;

		// Token: 0x04001BDF RID: 7135
		[FieldOffset(0)]
		public VREvent_ApplicationLaunch_t applicationLaunch;

		// Token: 0x04001BE0 RID: 7136
		[FieldOffset(0)]
		public VREvent_EditingCameraSurface_t cameraSurface;

		// Token: 0x04001BE1 RID: 7137
		[FieldOffset(0)]
		public VREvent_MessageOverlay_t messageOverlay;

		// Token: 0x04001BE2 RID: 7138
		[FieldOffset(0)]
		public VREvent_Keyboard_t keyboard;
	}
}
