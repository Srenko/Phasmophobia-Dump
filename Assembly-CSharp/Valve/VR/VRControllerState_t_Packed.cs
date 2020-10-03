using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x020003FC RID: 1020
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct VRControllerState_t_Packed
	{
		// Token: 0x06002009 RID: 8201 RVA: 0x0009E57C File Offset: 0x0009C77C
		public VRControllerState_t_Packed(VRControllerState_t unpacked)
		{
			this.unPacketNum = unpacked.unPacketNum;
			this.ulButtonPressed = unpacked.ulButtonPressed;
			this.ulButtonTouched = unpacked.ulButtonTouched;
			this.rAxis0 = unpacked.rAxis0;
			this.rAxis1 = unpacked.rAxis1;
			this.rAxis2 = unpacked.rAxis2;
			this.rAxis3 = unpacked.rAxis3;
			this.rAxis4 = unpacked.rAxis4;
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0009E5EC File Offset: 0x0009C7EC
		public void Unpack(ref VRControllerState_t unpacked)
		{
			unpacked.unPacketNum = this.unPacketNum;
			unpacked.ulButtonPressed = this.ulButtonPressed;
			unpacked.ulButtonTouched = this.ulButtonTouched;
			unpacked.rAxis0 = this.rAxis0;
			unpacked.rAxis1 = this.rAxis1;
			unpacked.rAxis2 = this.rAxis2;
			unpacked.rAxis3 = this.rAxis3;
			unpacked.rAxis4 = this.rAxis4;
		}

		// Token: 0x04001C7D RID: 7293
		public uint unPacketNum;

		// Token: 0x04001C7E RID: 7294
		public ulong ulButtonPressed;

		// Token: 0x04001C7F RID: 7295
		public ulong ulButtonTouched;

		// Token: 0x04001C80 RID: 7296
		public VRControllerAxis_t rAxis0;

		// Token: 0x04001C81 RID: 7297
		public VRControllerAxis_t rAxis1;

		// Token: 0x04001C82 RID: 7298
		public VRControllerAxis_t rAxis2;

		// Token: 0x04001C83 RID: 7299
		public VRControllerAxis_t rAxis3;

		// Token: 0x04001C84 RID: 7300
		public VRControllerAxis_t rAxis4;
	}
}
