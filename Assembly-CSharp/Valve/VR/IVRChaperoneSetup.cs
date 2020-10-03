using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x02000388 RID: 904
	public struct IVRChaperoneSetup
	{
		// Token: 0x04001872 RID: 6258
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._CommitWorkingCopy CommitWorkingCopy;

		// Token: 0x04001873 RID: 6259
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._RevertWorkingCopy RevertWorkingCopy;

		// Token: 0x04001874 RID: 6260
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._GetWorkingPlayAreaSize GetWorkingPlayAreaSize;

		// Token: 0x04001875 RID: 6261
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._GetWorkingPlayAreaRect GetWorkingPlayAreaRect;

		// Token: 0x04001876 RID: 6262
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._GetWorkingCollisionBoundsInfo GetWorkingCollisionBoundsInfo;

		// Token: 0x04001877 RID: 6263
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._GetLiveCollisionBoundsInfo GetLiveCollisionBoundsInfo;

		// Token: 0x04001878 RID: 6264
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._GetWorkingSeatedZeroPoseToRawTrackingPose GetWorkingSeatedZeroPoseToRawTrackingPose;

		// Token: 0x04001879 RID: 6265
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._GetWorkingStandingZeroPoseToRawTrackingPose GetWorkingStandingZeroPoseToRawTrackingPose;

		// Token: 0x0400187A RID: 6266
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._SetWorkingPlayAreaSize SetWorkingPlayAreaSize;

		// Token: 0x0400187B RID: 6267
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._SetWorkingCollisionBoundsInfo SetWorkingCollisionBoundsInfo;

		// Token: 0x0400187C RID: 6268
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._SetWorkingSeatedZeroPoseToRawTrackingPose SetWorkingSeatedZeroPoseToRawTrackingPose;

		// Token: 0x0400187D RID: 6269
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._SetWorkingStandingZeroPoseToRawTrackingPose SetWorkingStandingZeroPoseToRawTrackingPose;

		// Token: 0x0400187E RID: 6270
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._ReloadFromDisk ReloadFromDisk;

		// Token: 0x0400187F RID: 6271
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._GetLiveSeatedZeroPoseToRawTrackingPose GetLiveSeatedZeroPoseToRawTrackingPose;

		// Token: 0x04001880 RID: 6272
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._SetWorkingCollisionBoundsTagsInfo SetWorkingCollisionBoundsTagsInfo;

		// Token: 0x04001881 RID: 6273
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._GetLiveCollisionBoundsTagsInfo GetLiveCollisionBoundsTagsInfo;

		// Token: 0x04001882 RID: 6274
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._SetWorkingPhysicalBoundsInfo SetWorkingPhysicalBoundsInfo;

		// Token: 0x04001883 RID: 6275
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._GetLivePhysicalBoundsInfo GetLivePhysicalBoundsInfo;

		// Token: 0x04001884 RID: 6276
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._ExportLiveToBuffer ExportLiveToBuffer;

		// Token: 0x04001885 RID: 6277
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRChaperoneSetup._ImportFromBufferToWorking ImportFromBufferToWorking;

		// Token: 0x020006A9 RID: 1705
		// (Invoke) Token: 0x06002CAB RID: 11435
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _CommitWorkingCopy(EChaperoneConfigFile configFile);

		// Token: 0x020006AA RID: 1706
		// (Invoke) Token: 0x06002CAF RID: 11439
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _RevertWorkingCopy();

		// Token: 0x020006AB RID: 1707
		// (Invoke) Token: 0x06002CB3 RID: 11443
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetWorkingPlayAreaSize(ref float pSizeX, ref float pSizeZ);

		// Token: 0x020006AC RID: 1708
		// (Invoke) Token: 0x06002CB7 RID: 11447
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetWorkingPlayAreaRect(ref HmdQuad_t rect);

		// Token: 0x020006AD RID: 1709
		// (Invoke) Token: 0x06002CBB RID: 11451
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetWorkingCollisionBoundsInfo([In] [Out] HmdQuad_t[] pQuadsBuffer, ref uint punQuadsCount);

		// Token: 0x020006AE RID: 1710
		// (Invoke) Token: 0x06002CBF RID: 11455
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetLiveCollisionBoundsInfo([In] [Out] HmdQuad_t[] pQuadsBuffer, ref uint punQuadsCount);

		// Token: 0x020006AF RID: 1711
		// (Invoke) Token: 0x06002CC3 RID: 11459
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetWorkingSeatedZeroPoseToRawTrackingPose(ref HmdMatrix34_t pmatSeatedZeroPoseToRawTrackingPose);

		// Token: 0x020006B0 RID: 1712
		// (Invoke) Token: 0x06002CC7 RID: 11463
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetWorkingStandingZeroPoseToRawTrackingPose(ref HmdMatrix34_t pmatStandingZeroPoseToRawTrackingPose);

		// Token: 0x020006B1 RID: 1713
		// (Invoke) Token: 0x06002CCB RID: 11467
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetWorkingPlayAreaSize(float sizeX, float sizeZ);

		// Token: 0x020006B2 RID: 1714
		// (Invoke) Token: 0x06002CCF RID: 11471
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetWorkingCollisionBoundsInfo([In] [Out] HmdQuad_t[] pQuadsBuffer, uint unQuadsCount);

		// Token: 0x020006B3 RID: 1715
		// (Invoke) Token: 0x06002CD3 RID: 11475
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetWorkingSeatedZeroPoseToRawTrackingPose(ref HmdMatrix34_t pMatSeatedZeroPoseToRawTrackingPose);

		// Token: 0x020006B4 RID: 1716
		// (Invoke) Token: 0x06002CD7 RID: 11479
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetWorkingStandingZeroPoseToRawTrackingPose(ref HmdMatrix34_t pMatStandingZeroPoseToRawTrackingPose);

		// Token: 0x020006B5 RID: 1717
		// (Invoke) Token: 0x06002CDB RID: 11483
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ReloadFromDisk(EChaperoneConfigFile configFile);

		// Token: 0x020006B6 RID: 1718
		// (Invoke) Token: 0x06002CDF RID: 11487
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetLiveSeatedZeroPoseToRawTrackingPose(ref HmdMatrix34_t pmatSeatedZeroPoseToRawTrackingPose);

		// Token: 0x020006B7 RID: 1719
		// (Invoke) Token: 0x06002CE3 RID: 11491
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetWorkingCollisionBoundsTagsInfo([In] [Out] byte[] pTagsBuffer, uint unTagCount);

		// Token: 0x020006B8 RID: 1720
		// (Invoke) Token: 0x06002CE7 RID: 11495
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetLiveCollisionBoundsTagsInfo([In] [Out] byte[] pTagsBuffer, ref uint punTagCount);

		// Token: 0x020006B9 RID: 1721
		// (Invoke) Token: 0x06002CEB RID: 11499
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _SetWorkingPhysicalBoundsInfo([In] [Out] HmdQuad_t[] pQuadsBuffer, uint unQuadsCount);

		// Token: 0x020006BA RID: 1722
		// (Invoke) Token: 0x06002CEF RID: 11503
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetLivePhysicalBoundsInfo([In] [Out] HmdQuad_t[] pQuadsBuffer, ref uint punQuadsCount);

		// Token: 0x020006BB RID: 1723
		// (Invoke) Token: 0x06002CF3 RID: 11507
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _ExportLiveToBuffer(StringBuilder pBuffer, ref uint pnBufferLength);

		// Token: 0x020006BC RID: 1724
		// (Invoke) Token: 0x06002CF7 RID: 11511
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _ImportFromBufferToWorking(string pBuffer, uint nImportFlags);
	}
}
