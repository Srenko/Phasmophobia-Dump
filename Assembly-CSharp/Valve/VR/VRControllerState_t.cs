using System;

namespace Valve.VR
{
	// Token: 0x020003FB RID: 1019
	public struct VRControllerState_t
	{
		// Token: 0x04001C75 RID: 7285
		public uint unPacketNum;

		// Token: 0x04001C76 RID: 7286
		public ulong ulButtonPressed;

		// Token: 0x04001C77 RID: 7287
		public ulong ulButtonTouched;

		// Token: 0x04001C78 RID: 7288
		public VRControllerAxis_t rAxis0;

		// Token: 0x04001C79 RID: 7289
		public VRControllerAxis_t rAxis1;

		// Token: 0x04001C7A RID: 7290
		public VRControllerAxis_t rAxis2;

		// Token: 0x04001C7B RID: 7291
		public VRControllerAxis_t rAxis3;

		// Token: 0x04001C7C RID: 7292
		public VRControllerAxis_t rAxis4;
	}
}
