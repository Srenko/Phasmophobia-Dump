using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x0200038A RID: 906
	public struct IVROverlay
	{
		// Token: 0x040018AF RID: 6319
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._FindOverlay FindOverlay;

		// Token: 0x040018B0 RID: 6320
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._CreateOverlay CreateOverlay;

		// Token: 0x040018B1 RID: 6321
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._DestroyOverlay DestroyOverlay;

		// Token: 0x040018B2 RID: 6322
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetHighQualityOverlay SetHighQualityOverlay;

		// Token: 0x040018B3 RID: 6323
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetHighQualityOverlay GetHighQualityOverlay;

		// Token: 0x040018B4 RID: 6324
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayKey GetOverlayKey;

		// Token: 0x040018B5 RID: 6325
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayName GetOverlayName;

		// Token: 0x040018B6 RID: 6326
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayName SetOverlayName;

		// Token: 0x040018B7 RID: 6327
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayImageData GetOverlayImageData;

		// Token: 0x040018B8 RID: 6328
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayErrorNameFromEnum GetOverlayErrorNameFromEnum;

		// Token: 0x040018B9 RID: 6329
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayRenderingPid SetOverlayRenderingPid;

		// Token: 0x040018BA RID: 6330
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayRenderingPid GetOverlayRenderingPid;

		// Token: 0x040018BB RID: 6331
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayFlag SetOverlayFlag;

		// Token: 0x040018BC RID: 6332
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayFlag GetOverlayFlag;

		// Token: 0x040018BD RID: 6333
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayColor SetOverlayColor;

		// Token: 0x040018BE RID: 6334
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayColor GetOverlayColor;

		// Token: 0x040018BF RID: 6335
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayAlpha SetOverlayAlpha;

		// Token: 0x040018C0 RID: 6336
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayAlpha GetOverlayAlpha;

		// Token: 0x040018C1 RID: 6337
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayTexelAspect SetOverlayTexelAspect;

		// Token: 0x040018C2 RID: 6338
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayTexelAspect GetOverlayTexelAspect;

		// Token: 0x040018C3 RID: 6339
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlaySortOrder SetOverlaySortOrder;

		// Token: 0x040018C4 RID: 6340
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlaySortOrder GetOverlaySortOrder;

		// Token: 0x040018C5 RID: 6341
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayWidthInMeters SetOverlayWidthInMeters;

		// Token: 0x040018C6 RID: 6342
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayWidthInMeters GetOverlayWidthInMeters;

		// Token: 0x040018C7 RID: 6343
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayAutoCurveDistanceRangeInMeters SetOverlayAutoCurveDistanceRangeInMeters;

		// Token: 0x040018C8 RID: 6344
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayAutoCurveDistanceRangeInMeters GetOverlayAutoCurveDistanceRangeInMeters;

		// Token: 0x040018C9 RID: 6345
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayTextureColorSpace SetOverlayTextureColorSpace;

		// Token: 0x040018CA RID: 6346
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayTextureColorSpace GetOverlayTextureColorSpace;

		// Token: 0x040018CB RID: 6347
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayTextureBounds SetOverlayTextureBounds;

		// Token: 0x040018CC RID: 6348
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayTextureBounds GetOverlayTextureBounds;

		// Token: 0x040018CD RID: 6349
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayRenderModel GetOverlayRenderModel;

		// Token: 0x040018CE RID: 6350
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayRenderModel SetOverlayRenderModel;

		// Token: 0x040018CF RID: 6351
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayTransformType GetOverlayTransformType;

		// Token: 0x040018D0 RID: 6352
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayTransformAbsolute SetOverlayTransformAbsolute;

		// Token: 0x040018D1 RID: 6353
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayTransformAbsolute GetOverlayTransformAbsolute;

		// Token: 0x040018D2 RID: 6354
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayTransformTrackedDeviceRelative SetOverlayTransformTrackedDeviceRelative;

		// Token: 0x040018D3 RID: 6355
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayTransformTrackedDeviceRelative GetOverlayTransformTrackedDeviceRelative;

		// Token: 0x040018D4 RID: 6356
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayTransformTrackedDeviceComponent SetOverlayTransformTrackedDeviceComponent;

		// Token: 0x040018D5 RID: 6357
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayTransformTrackedDeviceComponent GetOverlayTransformTrackedDeviceComponent;

		// Token: 0x040018D6 RID: 6358
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayTransformOverlayRelative GetOverlayTransformOverlayRelative;

		// Token: 0x040018D7 RID: 6359
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayTransformOverlayRelative SetOverlayTransformOverlayRelative;

		// Token: 0x040018D8 RID: 6360
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._ShowOverlay ShowOverlay;

		// Token: 0x040018D9 RID: 6361
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._HideOverlay HideOverlay;

		// Token: 0x040018DA RID: 6362
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._IsOverlayVisible IsOverlayVisible;

		// Token: 0x040018DB RID: 6363
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetTransformForOverlayCoordinates GetTransformForOverlayCoordinates;

		// Token: 0x040018DC RID: 6364
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._PollNextOverlayEvent PollNextOverlayEvent;

		// Token: 0x040018DD RID: 6365
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayInputMethod GetOverlayInputMethod;

		// Token: 0x040018DE RID: 6366
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayInputMethod SetOverlayInputMethod;

		// Token: 0x040018DF RID: 6367
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayMouseScale GetOverlayMouseScale;

		// Token: 0x040018E0 RID: 6368
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayMouseScale SetOverlayMouseScale;

		// Token: 0x040018E1 RID: 6369
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._ComputeOverlayIntersection ComputeOverlayIntersection;

		// Token: 0x040018E2 RID: 6370
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._HandleControllerOverlayInteractionAsMouse HandleControllerOverlayInteractionAsMouse;

		// Token: 0x040018E3 RID: 6371
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._IsHoverTargetOverlay IsHoverTargetOverlay;

		// Token: 0x040018E4 RID: 6372
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetGamepadFocusOverlay GetGamepadFocusOverlay;

		// Token: 0x040018E5 RID: 6373
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetGamepadFocusOverlay SetGamepadFocusOverlay;

		// Token: 0x040018E6 RID: 6374
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayNeighbor SetOverlayNeighbor;

		// Token: 0x040018E7 RID: 6375
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._MoveGamepadFocusToNeighbor MoveGamepadFocusToNeighbor;

		// Token: 0x040018E8 RID: 6376
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayTexture SetOverlayTexture;

		// Token: 0x040018E9 RID: 6377
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._ClearOverlayTexture ClearOverlayTexture;

		// Token: 0x040018EA RID: 6378
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayRaw SetOverlayRaw;

		// Token: 0x040018EB RID: 6379
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayFromFile SetOverlayFromFile;

		// Token: 0x040018EC RID: 6380
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayTexture GetOverlayTexture;

		// Token: 0x040018ED RID: 6381
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._ReleaseNativeOverlayHandle ReleaseNativeOverlayHandle;

		// Token: 0x040018EE RID: 6382
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayTextureSize GetOverlayTextureSize;

		// Token: 0x040018EF RID: 6383
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._CreateDashboardOverlay CreateDashboardOverlay;

		// Token: 0x040018F0 RID: 6384
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._IsDashboardVisible IsDashboardVisible;

		// Token: 0x040018F1 RID: 6385
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._IsActiveDashboardOverlay IsActiveDashboardOverlay;

		// Token: 0x040018F2 RID: 6386
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetDashboardOverlaySceneProcess SetDashboardOverlaySceneProcess;

		// Token: 0x040018F3 RID: 6387
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetDashboardOverlaySceneProcess GetDashboardOverlaySceneProcess;

		// Token: 0x040018F4 RID: 6388
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._ShowDashboard ShowDashboard;

		// Token: 0x040018F5 RID: 6389
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetPrimaryDashboardDevice GetPrimaryDashboardDevice;

		// Token: 0x040018F6 RID: 6390
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._ShowKeyboard ShowKeyboard;

		// Token: 0x040018F7 RID: 6391
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._ShowKeyboardForOverlay ShowKeyboardForOverlay;

		// Token: 0x040018F8 RID: 6392
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetKeyboardText GetKeyboardText;

		// Token: 0x040018F9 RID: 6393
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._HideKeyboard HideKeyboard;

		// Token: 0x040018FA RID: 6394
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetKeyboardTransformAbsolute SetKeyboardTransformAbsolute;

		// Token: 0x040018FB RID: 6395
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetKeyboardPositionForOverlay SetKeyboardPositionForOverlay;

		// Token: 0x040018FC RID: 6396
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._SetOverlayIntersectionMask SetOverlayIntersectionMask;

		// Token: 0x040018FD RID: 6397
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._GetOverlayFlags GetOverlayFlags;

		// Token: 0x040018FE RID: 6398
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVROverlay._ShowMessageOverlay ShowMessageOverlay;

		// Token: 0x020006E6 RID: 1766
		// (Invoke) Token: 0x06002D9F RID: 11679
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _FindOverlay(string pchOverlayKey, ref ulong pOverlayHandle);

		// Token: 0x020006E7 RID: 1767
		// (Invoke) Token: 0x06002DA3 RID: 11683
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _CreateOverlay(string pchOverlayKey, string pchOverlayName, ref ulong pOverlayHandle);

		// Token: 0x020006E8 RID: 1768
		// (Invoke) Token: 0x06002DA7 RID: 11687
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _DestroyOverlay(ulong ulOverlayHandle);

		// Token: 0x020006E9 RID: 1769
		// (Invoke) Token: 0x06002DAB RID: 11691
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetHighQualityOverlay(ulong ulOverlayHandle);

		// Token: 0x020006EA RID: 1770
		// (Invoke) Token: 0x06002DAF RID: 11695
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate ulong _GetHighQualityOverlay();

		// Token: 0x020006EB RID: 1771
		// (Invoke) Token: 0x06002DB3 RID: 11699
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetOverlayKey(ulong ulOverlayHandle, StringBuilder pchValue, uint unBufferSize, ref EVROverlayError pError);

		// Token: 0x020006EC RID: 1772
		// (Invoke) Token: 0x06002DB7 RID: 11703
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetOverlayName(ulong ulOverlayHandle, StringBuilder pchValue, uint unBufferSize, ref EVROverlayError pError);

		// Token: 0x020006ED RID: 1773
		// (Invoke) Token: 0x06002DBB RID: 11707
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayName(ulong ulOverlayHandle, string pchName);

		// Token: 0x020006EE RID: 1774
		// (Invoke) Token: 0x06002DBF RID: 11711
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayImageData(ulong ulOverlayHandle, IntPtr pvBuffer, uint unBufferSize, ref uint punWidth, ref uint punHeight);

		// Token: 0x020006EF RID: 1775
		// (Invoke) Token: 0x06002DC3 RID: 11715
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate IntPtr _GetOverlayErrorNameFromEnum(EVROverlayError error);

		// Token: 0x020006F0 RID: 1776
		// (Invoke) Token: 0x06002DC7 RID: 11719
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayRenderingPid(ulong ulOverlayHandle, uint unPID);

		// Token: 0x020006F1 RID: 1777
		// (Invoke) Token: 0x06002DCB RID: 11723
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetOverlayRenderingPid(ulong ulOverlayHandle);

		// Token: 0x020006F2 RID: 1778
		// (Invoke) Token: 0x06002DCF RID: 11727
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayFlag(ulong ulOverlayHandle, VROverlayFlags eOverlayFlag, bool bEnabled);

		// Token: 0x020006F3 RID: 1779
		// (Invoke) Token: 0x06002DD3 RID: 11731
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayFlag(ulong ulOverlayHandle, VROverlayFlags eOverlayFlag, ref bool pbEnabled);

		// Token: 0x020006F4 RID: 1780
		// (Invoke) Token: 0x06002DD7 RID: 11735
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayColor(ulong ulOverlayHandle, float fRed, float fGreen, float fBlue);

		// Token: 0x020006F5 RID: 1781
		// (Invoke) Token: 0x06002DDB RID: 11739
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayColor(ulong ulOverlayHandle, ref float pfRed, ref float pfGreen, ref float pfBlue);

		// Token: 0x020006F6 RID: 1782
		// (Invoke) Token: 0x06002DDF RID: 11743
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayAlpha(ulong ulOverlayHandle, float fAlpha);

		// Token: 0x020006F7 RID: 1783
		// (Invoke) Token: 0x06002DE3 RID: 11747
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayAlpha(ulong ulOverlayHandle, ref float pfAlpha);

		// Token: 0x020006F8 RID: 1784
		// (Invoke) Token: 0x06002DE7 RID: 11751
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayTexelAspect(ulong ulOverlayHandle, float fTexelAspect);

		// Token: 0x020006F9 RID: 1785
		// (Invoke) Token: 0x06002DEB RID: 11755
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayTexelAspect(ulong ulOverlayHandle, ref float pfTexelAspect);

		// Token: 0x020006FA RID: 1786
		// (Invoke) Token: 0x06002DEF RID: 11759
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlaySortOrder(ulong ulOverlayHandle, uint unSortOrder);

		// Token: 0x020006FB RID: 1787
		// (Invoke) Token: 0x06002DF3 RID: 11763
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlaySortOrder(ulong ulOverlayHandle, ref uint punSortOrder);

		// Token: 0x020006FC RID: 1788
		// (Invoke) Token: 0x06002DF7 RID: 11767
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayWidthInMeters(ulong ulOverlayHandle, float fWidthInMeters);

		// Token: 0x020006FD RID: 1789
		// (Invoke) Token: 0x06002DFB RID: 11771
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayWidthInMeters(ulong ulOverlayHandle, ref float pfWidthInMeters);

		// Token: 0x020006FE RID: 1790
		// (Invoke) Token: 0x06002DFF RID: 11775
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayAutoCurveDistanceRangeInMeters(ulong ulOverlayHandle, float fMinDistanceInMeters, float fMaxDistanceInMeters);

		// Token: 0x020006FF RID: 1791
		// (Invoke) Token: 0x06002E03 RID: 11779
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayAutoCurveDistanceRangeInMeters(ulong ulOverlayHandle, ref float pfMinDistanceInMeters, ref float pfMaxDistanceInMeters);

		// Token: 0x02000700 RID: 1792
		// (Invoke) Token: 0x06002E07 RID: 11783
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayTextureColorSpace(ulong ulOverlayHandle, EColorSpace eTextureColorSpace);

		// Token: 0x02000701 RID: 1793
		// (Invoke) Token: 0x06002E0B RID: 11787
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayTextureColorSpace(ulong ulOverlayHandle, ref EColorSpace peTextureColorSpace);

		// Token: 0x02000702 RID: 1794
		// (Invoke) Token: 0x06002E0F RID: 11791
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayTextureBounds(ulong ulOverlayHandle, ref VRTextureBounds_t pOverlayTextureBounds);

		// Token: 0x02000703 RID: 1795
		// (Invoke) Token: 0x06002E13 RID: 11795
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayTextureBounds(ulong ulOverlayHandle, ref VRTextureBounds_t pOverlayTextureBounds);

		// Token: 0x02000704 RID: 1796
		// (Invoke) Token: 0x06002E17 RID: 11799
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetOverlayRenderModel(ulong ulOverlayHandle, string pchValue, uint unBufferSize, ref HmdColor_t pColor, ref EVROverlayError pError);

		// Token: 0x02000705 RID: 1797
		// (Invoke) Token: 0x06002E1B RID: 11803
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayRenderModel(ulong ulOverlayHandle, string pchRenderModel, ref HmdColor_t pColor);

		// Token: 0x02000706 RID: 1798
		// (Invoke) Token: 0x06002E1F RID: 11807
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayTransformType(ulong ulOverlayHandle, ref VROverlayTransformType peTransformType);

		// Token: 0x02000707 RID: 1799
		// (Invoke) Token: 0x06002E23 RID: 11811
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayTransformAbsolute(ulong ulOverlayHandle, ETrackingUniverseOrigin eTrackingOrigin, ref HmdMatrix34_t pmatTrackingOriginToOverlayTransform);

		// Token: 0x02000708 RID: 1800
		// (Invoke) Token: 0x06002E27 RID: 11815
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayTransformAbsolute(ulong ulOverlayHandle, ref ETrackingUniverseOrigin peTrackingOrigin, ref HmdMatrix34_t pmatTrackingOriginToOverlayTransform);

		// Token: 0x02000709 RID: 1801
		// (Invoke) Token: 0x06002E2B RID: 11819
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayTransformTrackedDeviceRelative(ulong ulOverlayHandle, uint unTrackedDevice, ref HmdMatrix34_t pmatTrackedDeviceToOverlayTransform);

		// Token: 0x0200070A RID: 1802
		// (Invoke) Token: 0x06002E2F RID: 11823
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayTransformTrackedDeviceRelative(ulong ulOverlayHandle, ref uint punTrackedDevice, ref HmdMatrix34_t pmatTrackedDeviceToOverlayTransform);

		// Token: 0x0200070B RID: 1803
		// (Invoke) Token: 0x06002E33 RID: 11827
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayTransformTrackedDeviceComponent(ulong ulOverlayHandle, uint unDeviceIndex, string pchComponentName);

		// Token: 0x0200070C RID: 1804
		// (Invoke) Token: 0x06002E37 RID: 11831
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayTransformTrackedDeviceComponent(ulong ulOverlayHandle, ref uint punDeviceIndex, string pchComponentName, uint unComponentNameSize);

		// Token: 0x0200070D RID: 1805
		// (Invoke) Token: 0x06002E3B RID: 11835
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayTransformOverlayRelative(ulong ulOverlayHandle, ref ulong ulOverlayHandleParent, ref HmdMatrix34_t pmatParentOverlayToOverlayTransform);

		// Token: 0x0200070E RID: 1806
		// (Invoke) Token: 0x06002E3F RID: 11839
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayTransformOverlayRelative(ulong ulOverlayHandle, ulong ulOverlayHandleParent, ref HmdMatrix34_t pmatParentOverlayToOverlayTransform);

		// Token: 0x0200070F RID: 1807
		// (Invoke) Token: 0x06002E43 RID: 11843
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _ShowOverlay(ulong ulOverlayHandle);

		// Token: 0x02000710 RID: 1808
		// (Invoke) Token: 0x06002E47 RID: 11847
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _HideOverlay(ulong ulOverlayHandle);

		// Token: 0x02000711 RID: 1809
		// (Invoke) Token: 0x06002E4B RID: 11851
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _IsOverlayVisible(ulong ulOverlayHandle);

		// Token: 0x02000712 RID: 1810
		// (Invoke) Token: 0x06002E4F RID: 11855
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetTransformForOverlayCoordinates(ulong ulOverlayHandle, ETrackingUniverseOrigin eTrackingOrigin, HmdVector2_t coordinatesInOverlay, ref HmdMatrix34_t pmatTransform);

		// Token: 0x02000713 RID: 1811
		// (Invoke) Token: 0x06002E53 RID: 11859
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _PollNextOverlayEvent(ulong ulOverlayHandle, ref VREvent_t pEvent, uint uncbVREvent);

		// Token: 0x02000714 RID: 1812
		// (Invoke) Token: 0x06002E57 RID: 11863
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayInputMethod(ulong ulOverlayHandle, ref VROverlayInputMethod peInputMethod);

		// Token: 0x02000715 RID: 1813
		// (Invoke) Token: 0x06002E5B RID: 11867
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayInputMethod(ulong ulOverlayHandle, VROverlayInputMethod eInputMethod);

		// Token: 0x02000716 RID: 1814
		// (Invoke) Token: 0x06002E5F RID: 11871
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayMouseScale(ulong ulOverlayHandle, ref HmdVector2_t pvecMouseScale);

		// Token: 0x02000717 RID: 1815
		// (Invoke) Token: 0x06002E63 RID: 11875
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayMouseScale(ulong ulOverlayHandle, ref HmdVector2_t pvecMouseScale);

		// Token: 0x02000718 RID: 1816
		// (Invoke) Token: 0x06002E67 RID: 11879
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _ComputeOverlayIntersection(ulong ulOverlayHandle, ref VROverlayIntersectionParams_t pParams, ref VROverlayIntersectionResults_t pResults);

		// Token: 0x02000719 RID: 1817
		// (Invoke) Token: 0x06002E6B RID: 11883
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _HandleControllerOverlayInteractionAsMouse(ulong ulOverlayHandle, uint unControllerDeviceIndex);

		// Token: 0x0200071A RID: 1818
		// (Invoke) Token: 0x06002E6F RID: 11887
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _IsHoverTargetOverlay(ulong ulOverlayHandle);

		// Token: 0x0200071B RID: 1819
		// (Invoke) Token: 0x06002E73 RID: 11891
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate ulong _GetGamepadFocusOverlay();

		// Token: 0x0200071C RID: 1820
		// (Invoke) Token: 0x06002E77 RID: 11895
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetGamepadFocusOverlay(ulong ulNewFocusOverlay);

		// Token: 0x0200071D RID: 1821
		// (Invoke) Token: 0x06002E7B RID: 11899
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayNeighbor(EOverlayDirection eDirection, ulong ulFrom, ulong ulTo);

		// Token: 0x0200071E RID: 1822
		// (Invoke) Token: 0x06002E7F RID: 11903
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _MoveGamepadFocusToNeighbor(EOverlayDirection eDirection, ulong ulFrom);

		// Token: 0x0200071F RID: 1823
		// (Invoke) Token: 0x06002E83 RID: 11907
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayTexture(ulong ulOverlayHandle, ref Texture_t pTexture);

		// Token: 0x02000720 RID: 1824
		// (Invoke) Token: 0x06002E87 RID: 11911
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _ClearOverlayTexture(ulong ulOverlayHandle);

		// Token: 0x02000721 RID: 1825
		// (Invoke) Token: 0x06002E8B RID: 11915
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayRaw(ulong ulOverlayHandle, IntPtr pvBuffer, uint unWidth, uint unHeight, uint unDepth);

		// Token: 0x02000722 RID: 1826
		// (Invoke) Token: 0x06002E8F RID: 11919
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayFromFile(ulong ulOverlayHandle, string pchFilePath);

		// Token: 0x02000723 RID: 1827
		// (Invoke) Token: 0x06002E93 RID: 11923
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayTexture(ulong ulOverlayHandle, ref IntPtr pNativeTextureHandle, IntPtr pNativeTextureRef, ref uint pWidth, ref uint pHeight, ref uint pNativeFormat, ref ETextureType pAPIType, ref EColorSpace pColorSpace, ref VRTextureBounds_t pTextureBounds);

		// Token: 0x02000724 RID: 1828
		// (Invoke) Token: 0x06002E97 RID: 11927
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _ReleaseNativeOverlayHandle(ulong ulOverlayHandle, IntPtr pNativeTextureHandle);

		// Token: 0x02000725 RID: 1829
		// (Invoke) Token: 0x06002E9B RID: 11931
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayTextureSize(ulong ulOverlayHandle, ref uint pWidth, ref uint pHeight);

		// Token: 0x02000726 RID: 1830
		// (Invoke) Token: 0x06002E9F RID: 11935
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _CreateDashboardOverlay(string pchOverlayKey, string pchOverlayFriendlyName, ref ulong pMainHandle, ref ulong pThumbnailHandle);

		// Token: 0x02000727 RID: 1831
		// (Invoke) Token: 0x06002EA3 RID: 11939
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _IsDashboardVisible();

		// Token: 0x02000728 RID: 1832
		// (Invoke) Token: 0x06002EA7 RID: 11943
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _IsActiveDashboardOverlay(ulong ulOverlayHandle);

		// Token: 0x02000729 RID: 1833
		// (Invoke) Token: 0x06002EAB RID: 11947
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetDashboardOverlaySceneProcess(ulong ulOverlayHandle, uint unProcessId);

		// Token: 0x0200072A RID: 1834
		// (Invoke) Token: 0x06002EAF RID: 11951
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetDashboardOverlaySceneProcess(ulong ulOverlayHandle, ref uint punProcessId);

		// Token: 0x0200072B RID: 1835
		// (Invoke) Token: 0x06002EB3 RID: 11955
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ShowDashboard(string pchOverlayToShow);

		// Token: 0x0200072C RID: 1836
		// (Invoke) Token: 0x06002EB7 RID: 11959
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetPrimaryDashboardDevice();

		// Token: 0x0200072D RID: 1837
		// (Invoke) Token: 0x06002EBB RID: 11963
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _ShowKeyboard(int eInputMode, int eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText, bool bUseMinimalMode, ulong uUserValue);

		// Token: 0x0200072E RID: 1838
		// (Invoke) Token: 0x06002EBF RID: 11967
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _ShowKeyboardForOverlay(ulong ulOverlayHandle, int eInputMode, int eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText, bool bUseMinimalMode, ulong uUserValue);

		// Token: 0x0200072F RID: 1839
		// (Invoke) Token: 0x06002EC3 RID: 11971
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetKeyboardText(StringBuilder pchText, uint cchText);

		// Token: 0x02000730 RID: 1840
		// (Invoke) Token: 0x06002EC7 RID: 11975
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _HideKeyboard();

		// Token: 0x02000731 RID: 1841
		// (Invoke) Token: 0x06002ECB RID: 11979
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetKeyboardTransformAbsolute(ETrackingUniverseOrigin eTrackingOrigin, ref HmdMatrix34_t pmatTrackingOriginToKeyboardTransform);

		// Token: 0x02000732 RID: 1842
		// (Invoke) Token: 0x06002ECF RID: 11983
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetKeyboardPositionForOverlay(ulong ulOverlayHandle, HmdRect2_t avoidRect);

		// Token: 0x02000733 RID: 1843
		// (Invoke) Token: 0x06002ED3 RID: 11987
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _SetOverlayIntersectionMask(ulong ulOverlayHandle, ref VROverlayIntersectionMaskPrimitive_t pMaskPrimitives, uint unNumMaskPrimitives, uint unPrimitiveSize);

		// Token: 0x02000734 RID: 1844
		// (Invoke) Token: 0x06002ED7 RID: 11991
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVROverlayError _GetOverlayFlags(ulong ulOverlayHandle, ref uint pFlags);

		// Token: 0x02000735 RID: 1845
		// (Invoke) Token: 0x06002EDB RID: 11995
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate VRMessageOverlayResponse _ShowMessageOverlay(string pchText, string pchCaption, string pchButton0Text, string pchButton1Text, string pchButton2Text, string pchButton3Text);
	}
}
