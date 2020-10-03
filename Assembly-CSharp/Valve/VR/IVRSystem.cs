using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x02000383 RID: 899
	public struct IVRSystem
	{
		// Token: 0x0400180F RID: 6159
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetRecommendedRenderTargetSize GetRecommendedRenderTargetSize;

		// Token: 0x04001810 RID: 6160
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetProjectionMatrix GetProjectionMatrix;

		// Token: 0x04001811 RID: 6161
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetProjectionRaw GetProjectionRaw;

		// Token: 0x04001812 RID: 6162
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._ComputeDistortion ComputeDistortion;

		// Token: 0x04001813 RID: 6163
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetEyeToHeadTransform GetEyeToHeadTransform;

		// Token: 0x04001814 RID: 6164
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetTimeSinceLastVsync GetTimeSinceLastVsync;

		// Token: 0x04001815 RID: 6165
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetD3D9AdapterIndex GetD3D9AdapterIndex;

		// Token: 0x04001816 RID: 6166
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetDXGIOutputInfo GetDXGIOutputInfo;

		// Token: 0x04001817 RID: 6167
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetOutputDevice GetOutputDevice;

		// Token: 0x04001818 RID: 6168
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._IsDisplayOnDesktop IsDisplayOnDesktop;

		// Token: 0x04001819 RID: 6169
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._SetDisplayVisibility SetDisplayVisibility;

		// Token: 0x0400181A RID: 6170
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetDeviceToAbsoluteTrackingPose GetDeviceToAbsoluteTrackingPose;

		// Token: 0x0400181B RID: 6171
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._ResetSeatedZeroPose ResetSeatedZeroPose;

		// Token: 0x0400181C RID: 6172
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetSeatedZeroPoseToStandingAbsoluteTrackingPose GetSeatedZeroPoseToStandingAbsoluteTrackingPose;

		// Token: 0x0400181D RID: 6173
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetRawZeroPoseToStandingAbsoluteTrackingPose GetRawZeroPoseToStandingAbsoluteTrackingPose;

		// Token: 0x0400181E RID: 6174
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetSortedTrackedDeviceIndicesOfClass GetSortedTrackedDeviceIndicesOfClass;

		// Token: 0x0400181F RID: 6175
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetTrackedDeviceActivityLevel GetTrackedDeviceActivityLevel;

		// Token: 0x04001820 RID: 6176
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._ApplyTransform ApplyTransform;

		// Token: 0x04001821 RID: 6177
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetTrackedDeviceIndexForControllerRole GetTrackedDeviceIndexForControllerRole;

		// Token: 0x04001822 RID: 6178
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetControllerRoleForTrackedDeviceIndex GetControllerRoleForTrackedDeviceIndex;

		// Token: 0x04001823 RID: 6179
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetTrackedDeviceClass GetTrackedDeviceClass;

		// Token: 0x04001824 RID: 6180
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._IsTrackedDeviceConnected IsTrackedDeviceConnected;

		// Token: 0x04001825 RID: 6181
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetBoolTrackedDeviceProperty GetBoolTrackedDeviceProperty;

		// Token: 0x04001826 RID: 6182
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetFloatTrackedDeviceProperty GetFloatTrackedDeviceProperty;

		// Token: 0x04001827 RID: 6183
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetInt32TrackedDeviceProperty GetInt32TrackedDeviceProperty;

		// Token: 0x04001828 RID: 6184
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetUint64TrackedDeviceProperty GetUint64TrackedDeviceProperty;

		// Token: 0x04001829 RID: 6185
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetMatrix34TrackedDeviceProperty GetMatrix34TrackedDeviceProperty;

		// Token: 0x0400182A RID: 6186
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetStringTrackedDeviceProperty GetStringTrackedDeviceProperty;

		// Token: 0x0400182B RID: 6187
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetPropErrorNameFromEnum GetPropErrorNameFromEnum;

		// Token: 0x0400182C RID: 6188
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._PollNextEvent PollNextEvent;

		// Token: 0x0400182D RID: 6189
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._PollNextEventWithPose PollNextEventWithPose;

		// Token: 0x0400182E RID: 6190
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetEventTypeNameFromEnum GetEventTypeNameFromEnum;

		// Token: 0x0400182F RID: 6191
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetHiddenAreaMesh GetHiddenAreaMesh;

		// Token: 0x04001830 RID: 6192
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetControllerState GetControllerState;

		// Token: 0x04001831 RID: 6193
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetControllerStateWithPose GetControllerStateWithPose;

		// Token: 0x04001832 RID: 6194
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._TriggerHapticPulse TriggerHapticPulse;

		// Token: 0x04001833 RID: 6195
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetButtonIdNameFromEnum GetButtonIdNameFromEnum;

		// Token: 0x04001834 RID: 6196
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._GetControllerAxisTypeNameFromEnum GetControllerAxisTypeNameFromEnum;

		// Token: 0x04001835 RID: 6197
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._CaptureInputFocus CaptureInputFocus;

		// Token: 0x04001836 RID: 6198
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._ReleaseInputFocus ReleaseInputFocus;

		// Token: 0x04001837 RID: 6199
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._IsInputFocusCapturedByAnotherProcess IsInputFocusCapturedByAnotherProcess;

		// Token: 0x04001838 RID: 6200
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._DriverDebugRequest DriverDebugRequest;

		// Token: 0x04001839 RID: 6201
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._PerformFirmwareUpdate PerformFirmwareUpdate;

		// Token: 0x0400183A RID: 6202
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._AcknowledgeQuit_Exiting AcknowledgeQuit_Exiting;

		// Token: 0x0400183B RID: 6203
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSystem._AcknowledgeQuit_UserPrompt AcknowledgeQuit_UserPrompt;

		// Token: 0x02000646 RID: 1606
		// (Invoke) Token: 0x06002B1F RID: 11039
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _GetRecommendedRenderTargetSize(ref uint pnWidth, ref uint pnHeight);

		// Token: 0x02000647 RID: 1607
		// (Invoke) Token: 0x06002B23 RID: 11043
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate HmdMatrix44_t _GetProjectionMatrix(EVREye eEye, float fNearZ, float fFarZ);

		// Token: 0x02000648 RID: 1608
		// (Invoke) Token: 0x06002B27 RID: 11047
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _GetProjectionRaw(EVREye eEye, ref float pfLeft, ref float pfRight, ref float pfTop, ref float pfBottom);

		// Token: 0x02000649 RID: 1609
		// (Invoke) Token: 0x06002B2B RID: 11051
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _ComputeDistortion(EVREye eEye, float fU, float fV, ref DistortionCoordinates_t pDistortionCoordinates);

		// Token: 0x0200064A RID: 1610
		// (Invoke) Token: 0x06002B2F RID: 11055
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate HmdMatrix34_t _GetEyeToHeadTransform(EVREye eEye);

		// Token: 0x0200064B RID: 1611
		// (Invoke) Token: 0x06002B33 RID: 11059
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetTimeSinceLastVsync(ref float pfSecondsSinceLastVsync, ref ulong pulFrameCounter);

		// Token: 0x0200064C RID: 1612
		// (Invoke) Token: 0x06002B37 RID: 11063
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate int _GetD3D9AdapterIndex();

		// Token: 0x0200064D RID: 1613
		// (Invoke) Token: 0x06002B3B RID: 11067
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _GetDXGIOutputInfo(ref int pnAdapterIndex);

		// Token: 0x0200064E RID: 1614
		// (Invoke) Token: 0x06002B3F RID: 11071
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _GetOutputDevice(ref ulong pnDevice, ETextureType textureType);

		// Token: 0x0200064F RID: 1615
		// (Invoke) Token: 0x06002B43 RID: 11075
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _IsDisplayOnDesktop();

		// Token: 0x02000650 RID: 1616
		// (Invoke) Token: 0x06002B47 RID: 11079
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _SetDisplayVisibility(bool bIsVisibleOnDesktop);

		// Token: 0x02000651 RID: 1617
		// (Invoke) Token: 0x06002B4B RID: 11083
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin eOrigin, float fPredictedSecondsToPhotonsFromNow, [In] [Out] TrackedDevicePose_t[] pTrackedDevicePoseArray, uint unTrackedDevicePoseArrayCount);

		// Token: 0x02000652 RID: 1618
		// (Invoke) Token: 0x06002B4F RID: 11087
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ResetSeatedZeroPose();

		// Token: 0x02000653 RID: 1619
		// (Invoke) Token: 0x06002B53 RID: 11091
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate HmdMatrix34_t _GetSeatedZeroPoseToStandingAbsoluteTrackingPose();

		// Token: 0x02000654 RID: 1620
		// (Invoke) Token: 0x06002B57 RID: 11095
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate HmdMatrix34_t _GetRawZeroPoseToStandingAbsoluteTrackingPose();

		// Token: 0x02000655 RID: 1621
		// (Invoke) Token: 0x06002B5B RID: 11099
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetSortedTrackedDeviceIndicesOfClass(ETrackedDeviceClass eTrackedDeviceClass, [In] [Out] uint[] punTrackedDeviceIndexArray, uint unTrackedDeviceIndexArrayCount, uint unRelativeToTrackedDeviceIndex);

		// Token: 0x02000656 RID: 1622
		// (Invoke) Token: 0x06002B5F RID: 11103
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EDeviceActivityLevel _GetTrackedDeviceActivityLevel(uint unDeviceId);

		// Token: 0x02000657 RID: 1623
		// (Invoke) Token: 0x06002B63 RID: 11107
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ApplyTransform(ref TrackedDevicePose_t pOutputPose, ref TrackedDevicePose_t pTrackedDevicePose, ref HmdMatrix34_t pTransform);

		// Token: 0x02000658 RID: 1624
		// (Invoke) Token: 0x06002B67 RID: 11111
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole unDeviceType);

		// Token: 0x02000659 RID: 1625
		// (Invoke) Token: 0x06002B6B RID: 11115
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate ETrackedControllerRole _GetControllerRoleForTrackedDeviceIndex(uint unDeviceIndex);

		// Token: 0x0200065A RID: 1626
		// (Invoke) Token: 0x06002B6F RID: 11119
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate ETrackedDeviceClass _GetTrackedDeviceClass(uint unDeviceIndex);

		// Token: 0x0200065B RID: 1627
		// (Invoke) Token: 0x06002B73 RID: 11123
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _IsTrackedDeviceConnected(uint unDeviceIndex);

		// Token: 0x0200065C RID: 1628
		// (Invoke) Token: 0x06002B77 RID: 11127
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetBoolTrackedDeviceProperty(uint unDeviceIndex, ETrackedDeviceProperty prop, ref ETrackedPropertyError pError);

		// Token: 0x0200065D RID: 1629
		// (Invoke) Token: 0x06002B7B RID: 11131
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate float _GetFloatTrackedDeviceProperty(uint unDeviceIndex, ETrackedDeviceProperty prop, ref ETrackedPropertyError pError);

		// Token: 0x0200065E RID: 1630
		// (Invoke) Token: 0x06002B7F RID: 11135
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate int _GetInt32TrackedDeviceProperty(uint unDeviceIndex, ETrackedDeviceProperty prop, ref ETrackedPropertyError pError);

		// Token: 0x0200065F RID: 1631
		// (Invoke) Token: 0x06002B83 RID: 11139
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate ulong _GetUint64TrackedDeviceProperty(uint unDeviceIndex, ETrackedDeviceProperty prop, ref ETrackedPropertyError pError);

		// Token: 0x02000660 RID: 1632
		// (Invoke) Token: 0x06002B87 RID: 11143
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate HmdMatrix34_t _GetMatrix34TrackedDeviceProperty(uint unDeviceIndex, ETrackedDeviceProperty prop, ref ETrackedPropertyError pError);

		// Token: 0x02000661 RID: 1633
		// (Invoke) Token: 0x06002B8B RID: 11147
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetStringTrackedDeviceProperty(uint unDeviceIndex, ETrackedDeviceProperty prop, StringBuilder pchValue, uint unBufferSize, ref ETrackedPropertyError pError);

		// Token: 0x02000662 RID: 1634
		// (Invoke) Token: 0x06002B8F RID: 11151
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate IntPtr _GetPropErrorNameFromEnum(ETrackedPropertyError error);

		// Token: 0x02000663 RID: 1635
		// (Invoke) Token: 0x06002B93 RID: 11155
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _PollNextEvent(ref VREvent_t pEvent, uint uncbVREvent);

		// Token: 0x02000664 RID: 1636
		// (Invoke) Token: 0x06002B97 RID: 11159
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _PollNextEventWithPose(ETrackingUniverseOrigin eOrigin, ref VREvent_t pEvent, uint uncbVREvent, ref TrackedDevicePose_t pTrackedDevicePose);

		// Token: 0x02000665 RID: 1637
		// (Invoke) Token: 0x06002B9B RID: 11163
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate IntPtr _GetEventTypeNameFromEnum(EVREventType eType);

		// Token: 0x02000666 RID: 1638
		// (Invoke) Token: 0x06002B9F RID: 11167
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate HiddenAreaMesh_t _GetHiddenAreaMesh(EVREye eEye, EHiddenAreaMeshType type);

		// Token: 0x02000667 RID: 1639
		// (Invoke) Token: 0x06002BA3 RID: 11171
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetControllerState(uint unControllerDeviceIndex, ref VRControllerState_t pControllerState, uint unControllerStateSize);

		// Token: 0x02000668 RID: 1640
		// (Invoke) Token: 0x06002BA7 RID: 11175
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetControllerStateWithPose(ETrackingUniverseOrigin eOrigin, uint unControllerDeviceIndex, ref VRControllerState_t pControllerState, uint unControllerStateSize, ref TrackedDevicePose_t pTrackedDevicePose);

		// Token: 0x02000669 RID: 1641
		// (Invoke) Token: 0x06002BAB RID: 11179
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _TriggerHapticPulse(uint unControllerDeviceIndex, uint unAxisId, char usDurationMicroSec);

		// Token: 0x0200066A RID: 1642
		// (Invoke) Token: 0x06002BAF RID: 11183
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate IntPtr _GetButtonIdNameFromEnum(EVRButtonId eButtonId);

		// Token: 0x0200066B RID: 1643
		// (Invoke) Token: 0x06002BB3 RID: 11187
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate IntPtr _GetControllerAxisTypeNameFromEnum(EVRControllerAxisType eAxisType);

		// Token: 0x0200066C RID: 1644
		// (Invoke) Token: 0x06002BB7 RID: 11191
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _CaptureInputFocus();

		// Token: 0x0200066D RID: 1645
		// (Invoke) Token: 0x06002BBB RID: 11195
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _ReleaseInputFocus();

		// Token: 0x0200066E RID: 1646
		// (Invoke) Token: 0x06002BBF RID: 11199
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _IsInputFocusCapturedByAnotherProcess();

		// Token: 0x0200066F RID: 1647
		// (Invoke) Token: 0x06002BC3 RID: 11203
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _DriverDebugRequest(uint unDeviceIndex, string pchRequest, string pchResponseBuffer, uint unResponseBufferSize);

		// Token: 0x02000670 RID: 1648
		// (Invoke) Token: 0x06002BC7 RID: 11207
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRFirmwareError _PerformFirmwareUpdate(uint unDeviceIndex);

		// Token: 0x02000671 RID: 1649
		// (Invoke) Token: 0x06002BCB RID: 11211
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _AcknowledgeQuit_Exiting();

		// Token: 0x02000672 RID: 1650
		// (Invoke) Token: 0x06002BCF RID: 11215
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _AcknowledgeQuit_UserPrompt();
	}
}
