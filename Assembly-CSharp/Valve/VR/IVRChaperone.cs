using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x02000387 RID: 903
	public struct IVRChaperone
	{
		// Token: 0x0400186A RID: 6250
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperone._GetCalibrationState GetCalibrationState;

		// Token: 0x0400186B RID: 6251
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperone._GetPlayAreaSize GetPlayAreaSize;

		// Token: 0x0400186C RID: 6252
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperone._GetPlayAreaRect GetPlayAreaRect;

		// Token: 0x0400186D RID: 6253
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperone._ReloadInfo ReloadInfo;

		// Token: 0x0400186E RID: 6254
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperone._SetSceneColor SetSceneColor;

		// Token: 0x0400186F RID: 6255
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperone._GetBoundsColor GetBoundsColor;

		// Token: 0x04001870 RID: 6256
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperone._AreBoundsVisible AreBoundsVisible;

		// Token: 0x04001871 RID: 6257
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperone._ForceBoundsVisible ForceBoundsVisible;

		// Token: 0x020006A1 RID: 1697
		// (Invoke) Token: 0x06002C8B RID: 11403
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate ChaperoneCalibrationState _GetCalibrationState();

		// Token: 0x020006A2 RID: 1698
		// (Invoke) Token: 0x06002C8F RID: 11407
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetPlayAreaSize(ref float pSizeX, ref float pSizeZ);

		// Token: 0x020006A3 RID: 1699
		// (Invoke) Token: 0x06002C93 RID: 11411
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetPlayAreaRect(ref HmdQuad_t rect);

		// Token: 0x020006A4 RID: 1700
		// (Invoke) Token: 0x06002C97 RID: 11415
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ReloadInfo();

		// Token: 0x020006A5 RID: 1701
		// (Invoke) Token: 0x06002C9B RID: 11419
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetSceneColor(HmdColor_t color);

		// Token: 0x020006A6 RID: 1702
		// (Invoke) Token: 0x06002C9F RID: 11423
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _GetBoundsColor(ref HmdColor_t pOutputColorArray, int nNumOutputColors, float flCollisionBoundsFadeDistance, ref HmdColor_t pOutputCameraColor);

		// Token: 0x020006A7 RID: 1703
		// (Invoke) Token: 0x06002CA3 RID: 11427
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _AreBoundsVisible();

		// Token: 0x020006A8 RID: 1704
		// (Invoke) Token: 0x06002CA7 RID: 11431
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ForceBoundsVisible(bool bForce);
	}
}
