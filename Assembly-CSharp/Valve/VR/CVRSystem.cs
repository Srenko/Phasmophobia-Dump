using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x02000391 RID: 913
	public class CVRSystem
	{
		// Token: 0x06001ED5 RID: 7893 RVA: 0x0009C831 File Offset: 0x0009AA31
		internal CVRSystem(IntPtr pInterface)
		{
			this.FnTable = (IVRSystem)Marshal.PtrToStructure(pInterface, typeof(IVRSystem));
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0009C854 File Offset: 0x0009AA54
		public void GetRecommendedRenderTargetSize(ref uint pnWidth, ref uint pnHeight)
		{
			pnWidth = 0U;
			pnHeight = 0U;
			this.FnTable.GetRecommendedRenderTargetSize(ref pnWidth, ref pnHeight);
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0009C86E File Offset: 0x0009AA6E
		public HmdMatrix44_t GetProjectionMatrix(EVREye eEye, float fNearZ, float fFarZ)
		{
			return this.FnTable.GetProjectionMatrix(eEye, fNearZ, fFarZ);
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0009C883 File Offset: 0x0009AA83
		public void GetProjectionRaw(EVREye eEye, ref float pfLeft, ref float pfRight, ref float pfTop, ref float pfBottom)
		{
			pfLeft = 0f;
			pfRight = 0f;
			pfTop = 0f;
			pfBottom = 0f;
			this.FnTable.GetProjectionRaw(eEye, ref pfLeft, ref pfRight, ref pfTop, ref pfBottom);
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x0009C8BA File Offset: 0x0009AABA
		public bool ComputeDistortion(EVREye eEye, float fU, float fV, ref DistortionCoordinates_t pDistortionCoordinates)
		{
			return this.FnTable.ComputeDistortion(eEye, fU, fV, ref pDistortionCoordinates);
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0009C8D1 File Offset: 0x0009AAD1
		public HmdMatrix34_t GetEyeToHeadTransform(EVREye eEye)
		{
			return this.FnTable.GetEyeToHeadTransform(eEye);
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x0009C8E4 File Offset: 0x0009AAE4
		public bool GetTimeSinceLastVsync(ref float pfSecondsSinceLastVsync, ref ulong pulFrameCounter)
		{
			pfSecondsSinceLastVsync = 0f;
			pulFrameCounter = 0UL;
			return this.FnTable.GetTimeSinceLastVsync(ref pfSecondsSinceLastVsync, ref pulFrameCounter);
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x0009C903 File Offset: 0x0009AB03
		public int GetD3D9AdapterIndex()
		{
			return this.FnTable.GetD3D9AdapterIndex();
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x0009C915 File Offset: 0x0009AB15
		public void GetDXGIOutputInfo(ref int pnAdapterIndex)
		{
			pnAdapterIndex = 0;
			this.FnTable.GetDXGIOutputInfo(ref pnAdapterIndex);
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x0009C92B File Offset: 0x0009AB2B
		public void GetOutputDevice(ref ulong pnDevice, ETextureType textureType)
		{
			pnDevice = 0UL;
			this.FnTable.GetOutputDevice(ref pnDevice, textureType);
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x0009C943 File Offset: 0x0009AB43
		public bool IsDisplayOnDesktop()
		{
			return this.FnTable.IsDisplayOnDesktop();
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x0009C955 File Offset: 0x0009AB55
		public bool SetDisplayVisibility(bool bIsVisibleOnDesktop)
		{
			return this.FnTable.SetDisplayVisibility(bIsVisibleOnDesktop);
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x0009C968 File Offset: 0x0009AB68
		public void GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin eOrigin, float fPredictedSecondsToPhotonsFromNow, TrackedDevicePose_t[] pTrackedDevicePoseArray)
		{
			this.FnTable.GetDeviceToAbsoluteTrackingPose(eOrigin, fPredictedSecondsToPhotonsFromNow, pTrackedDevicePoseArray, (uint)pTrackedDevicePoseArray.Length);
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0009C980 File Offset: 0x0009AB80
		public void ResetSeatedZeroPose()
		{
			this.FnTable.ResetSeatedZeroPose();
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0009C992 File Offset: 0x0009AB92
		public HmdMatrix34_t GetSeatedZeroPoseToStandingAbsoluteTrackingPose()
		{
			return this.FnTable.GetSeatedZeroPoseToStandingAbsoluteTrackingPose();
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0009C9A4 File Offset: 0x0009ABA4
		public HmdMatrix34_t GetRawZeroPoseToStandingAbsoluteTrackingPose()
		{
			return this.FnTable.GetRawZeroPoseToStandingAbsoluteTrackingPose();
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0009C9B6 File Offset: 0x0009ABB6
		public uint GetSortedTrackedDeviceIndicesOfClass(ETrackedDeviceClass eTrackedDeviceClass, uint[] punTrackedDeviceIndexArray, uint unRelativeToTrackedDeviceIndex)
		{
			return this.FnTable.GetSortedTrackedDeviceIndicesOfClass(eTrackedDeviceClass, punTrackedDeviceIndexArray, (uint)punTrackedDeviceIndexArray.Length, unRelativeToTrackedDeviceIndex);
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x0009C9CE File Offset: 0x0009ABCE
		public EDeviceActivityLevel GetTrackedDeviceActivityLevel(uint unDeviceId)
		{
			return this.FnTable.GetTrackedDeviceActivityLevel(unDeviceId);
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x0009C9E1 File Offset: 0x0009ABE1
		public void ApplyTransform(ref TrackedDevicePose_t pOutputPose, ref TrackedDevicePose_t pTrackedDevicePose, ref HmdMatrix34_t pTransform)
		{
			this.FnTable.ApplyTransform(ref pOutputPose, ref pTrackedDevicePose, ref pTransform);
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x0009C9F6 File Offset: 0x0009ABF6
		public uint GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole unDeviceType)
		{
			return this.FnTable.GetTrackedDeviceIndexForControllerRole(unDeviceType);
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0009CA09 File Offset: 0x0009AC09
		public ETrackedControllerRole GetControllerRoleForTrackedDeviceIndex(uint unDeviceIndex)
		{
			return this.FnTable.GetControllerRoleForTrackedDeviceIndex(unDeviceIndex);
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x0009CA1C File Offset: 0x0009AC1C
		public ETrackedDeviceClass GetTrackedDeviceClass(uint unDeviceIndex)
		{
			return this.FnTable.GetTrackedDeviceClass(unDeviceIndex);
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0009CA2F File Offset: 0x0009AC2F
		public bool IsTrackedDeviceConnected(uint unDeviceIndex)
		{
			return this.FnTable.IsTrackedDeviceConnected(unDeviceIndex);
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0009CA42 File Offset: 0x0009AC42
		public bool GetBoolTrackedDeviceProperty(uint unDeviceIndex, ETrackedDeviceProperty prop, ref ETrackedPropertyError pError)
		{
			return this.FnTable.GetBoolTrackedDeviceProperty(unDeviceIndex, prop, ref pError);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x0009CA57 File Offset: 0x0009AC57
		public float GetFloatTrackedDeviceProperty(uint unDeviceIndex, ETrackedDeviceProperty prop, ref ETrackedPropertyError pError)
		{
			return this.FnTable.GetFloatTrackedDeviceProperty(unDeviceIndex, prop, ref pError);
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0009CA6C File Offset: 0x0009AC6C
		public int GetInt32TrackedDeviceProperty(uint unDeviceIndex, ETrackedDeviceProperty prop, ref ETrackedPropertyError pError)
		{
			return this.FnTable.GetInt32TrackedDeviceProperty(unDeviceIndex, prop, ref pError);
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x0009CA81 File Offset: 0x0009AC81
		public ulong GetUint64TrackedDeviceProperty(uint unDeviceIndex, ETrackedDeviceProperty prop, ref ETrackedPropertyError pError)
		{
			return this.FnTable.GetUint64TrackedDeviceProperty(unDeviceIndex, prop, ref pError);
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x0009CA96 File Offset: 0x0009AC96
		public HmdMatrix34_t GetMatrix34TrackedDeviceProperty(uint unDeviceIndex, ETrackedDeviceProperty prop, ref ETrackedPropertyError pError)
		{
			return this.FnTable.GetMatrix34TrackedDeviceProperty(unDeviceIndex, prop, ref pError);
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0009CAAB File Offset: 0x0009ACAB
		public uint GetStringTrackedDeviceProperty(uint unDeviceIndex, ETrackedDeviceProperty prop, StringBuilder pchValue, uint unBufferSize, ref ETrackedPropertyError pError)
		{
			return this.FnTable.GetStringTrackedDeviceProperty(unDeviceIndex, prop, pchValue, unBufferSize, ref pError);
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0009CAC4 File Offset: 0x0009ACC4
		public string GetPropErrorNameFromEnum(ETrackedPropertyError error)
		{
			return Marshal.PtrToStringAnsi(this.FnTable.GetPropErrorNameFromEnum(error));
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0009CADC File Offset: 0x0009ACDC
		public bool PollNextEvent(ref VREvent_t pEvent, uint uncbVREvent)
		{
			if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
			{
				VREvent_t_Packed vrevent_t_Packed = default(VREvent_t_Packed);
				CVRSystem.PollNextEventUnion pollNextEventUnion;
				pollNextEventUnion.pPollNextEventPacked = null;
				pollNextEventUnion.pPollNextEvent = this.FnTable.PollNextEvent;
				bool result = pollNextEventUnion.pPollNextEventPacked(ref vrevent_t_Packed, (uint)Marshal.SizeOf(typeof(VREvent_t_Packed)));
				vrevent_t_Packed.Unpack(ref pEvent);
				return result;
			}
			return this.FnTable.PollNextEvent(ref pEvent, uncbVREvent);
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x0009CB5C File Offset: 0x0009AD5C
		public bool PollNextEventWithPose(ETrackingUniverseOrigin eOrigin, ref VREvent_t pEvent, uint uncbVREvent, ref TrackedDevicePose_t pTrackedDevicePose)
		{
			return this.FnTable.PollNextEventWithPose(eOrigin, ref pEvent, uncbVREvent, ref pTrackedDevicePose);
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0009CB73 File Offset: 0x0009AD73
		public string GetEventTypeNameFromEnum(EVREventType eType)
		{
			return Marshal.PtrToStringAnsi(this.FnTable.GetEventTypeNameFromEnum(eType));
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0009CB8B File Offset: 0x0009AD8B
		public HiddenAreaMesh_t GetHiddenAreaMesh(EVREye eEye, EHiddenAreaMeshType type)
		{
			return this.FnTable.GetHiddenAreaMesh(eEye, type);
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0009CBA0 File Offset: 0x0009ADA0
		public bool GetControllerState(uint unControllerDeviceIndex, ref VRControllerState_t pControllerState, uint unControllerStateSize)
		{
			if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
			{
				VRControllerState_t_Packed vrcontrollerState_t_Packed = new VRControllerState_t_Packed(pControllerState);
				CVRSystem.GetControllerStateUnion getControllerStateUnion;
				getControllerStateUnion.pGetControllerStatePacked = null;
				getControllerStateUnion.pGetControllerState = this.FnTable.GetControllerState;
				bool result = getControllerStateUnion.pGetControllerStatePacked(unControllerDeviceIndex, ref vrcontrollerState_t_Packed, (uint)Marshal.SizeOf(typeof(VRControllerState_t_Packed)));
				vrcontrollerState_t_Packed.Unpack(ref pControllerState);
				return result;
			}
			return this.FnTable.GetControllerState(unControllerDeviceIndex, ref pControllerState, unControllerStateSize);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0009CC28 File Offset: 0x0009AE28
		public bool GetControllerStateWithPose(ETrackingUniverseOrigin eOrigin, uint unControllerDeviceIndex, ref VRControllerState_t pControllerState, uint unControllerStateSize, ref TrackedDevicePose_t pTrackedDevicePose)
		{
			if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
			{
				VRControllerState_t_Packed vrcontrollerState_t_Packed = new VRControllerState_t_Packed(pControllerState);
				CVRSystem.GetControllerStateWithPoseUnion getControllerStateWithPoseUnion;
				getControllerStateWithPoseUnion.pGetControllerStateWithPosePacked = null;
				getControllerStateWithPoseUnion.pGetControllerStateWithPose = this.FnTable.GetControllerStateWithPose;
				bool result = getControllerStateWithPoseUnion.pGetControllerStateWithPosePacked(eOrigin, unControllerDeviceIndex, ref vrcontrollerState_t_Packed, (uint)Marshal.SizeOf(typeof(VRControllerState_t_Packed)), ref pTrackedDevicePose);
				vrcontrollerState_t_Packed.Unpack(ref pControllerState);
				return result;
			}
			return this.FnTable.GetControllerStateWithPose(eOrigin, unControllerDeviceIndex, ref pControllerState, unControllerStateSize, ref pTrackedDevicePose);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0009CCB6 File Offset: 0x0009AEB6
		public void TriggerHapticPulse(uint unControllerDeviceIndex, uint unAxisId, char usDurationMicroSec)
		{
			this.FnTable.TriggerHapticPulse(unControllerDeviceIndex, unAxisId, usDurationMicroSec);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x0009CCCB File Offset: 0x0009AECB
		public string GetButtonIdNameFromEnum(EVRButtonId eButtonId)
		{
			return Marshal.PtrToStringAnsi(this.FnTable.GetButtonIdNameFromEnum(eButtonId));
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x0009CCE3 File Offset: 0x0009AEE3
		public string GetControllerAxisTypeNameFromEnum(EVRControllerAxisType eAxisType)
		{
			return Marshal.PtrToStringAnsi(this.FnTable.GetControllerAxisTypeNameFromEnum(eAxisType));
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x0009CCFB File Offset: 0x0009AEFB
		public bool CaptureInputFocus()
		{
			return this.FnTable.CaptureInputFocus();
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x0009CD0D File Offset: 0x0009AF0D
		public void ReleaseInputFocus()
		{
			this.FnTable.ReleaseInputFocus();
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x0009CD1F File Offset: 0x0009AF1F
		public bool IsInputFocusCapturedByAnotherProcess()
		{
			return this.FnTable.IsInputFocusCapturedByAnotherProcess();
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0009CD31 File Offset: 0x0009AF31
		public uint DriverDebugRequest(uint unDeviceIndex, string pchRequest, string pchResponseBuffer, uint unResponseBufferSize)
		{
			return this.FnTable.DriverDebugRequest(unDeviceIndex, pchRequest, pchResponseBuffer, unResponseBufferSize);
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x0009CD48 File Offset: 0x0009AF48
		public EVRFirmwareError PerformFirmwareUpdate(uint unDeviceIndex)
		{
			return this.FnTable.PerformFirmwareUpdate(unDeviceIndex);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x0009CD5B File Offset: 0x0009AF5B
		public void AcknowledgeQuit_Exiting()
		{
			this.FnTable.AcknowledgeQuit_Exiting();
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x0009CD6D File Offset: 0x0009AF6D
		public void AcknowledgeQuit_UserPrompt()
		{
			this.FnTable.AcknowledgeQuit_UserPrompt();
		}

		// Token: 0x0400192A RID: 6442
		private IVRSystem FnTable;

		// Token: 0x02000761 RID: 1889
		// (Invoke) Token: 0x06002F8B RID: 12171
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _PollNextEventPacked(ref VREvent_t_Packed pEvent, uint uncbVREvent);

		// Token: 0x02000762 RID: 1890
		[StructLayout(LayoutKind.Explicit)]
		private struct PollNextEventUnion
		{
			// Token: 0x040028E6 RID: 10470
			[FieldOffset(0)]
			public IVRSystem._PollNextEvent pPollNextEvent;

			// Token: 0x040028E7 RID: 10471
			[FieldOffset(0)]
			public CVRSystem._PollNextEventPacked pPollNextEventPacked;
		}

		// Token: 0x02000763 RID: 1891
		// (Invoke) Token: 0x06002F8F RID: 12175
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetControllerStatePacked(uint unControllerDeviceIndex, ref VRControllerState_t_Packed pControllerState, uint unControllerStateSize);

		// Token: 0x02000764 RID: 1892
		[StructLayout(LayoutKind.Explicit)]
		private struct GetControllerStateUnion
		{
			// Token: 0x040028E8 RID: 10472
			[FieldOffset(0)]
			public IVRSystem._GetControllerState pGetControllerState;

			// Token: 0x040028E9 RID: 10473
			[FieldOffset(0)]
			public CVRSystem._GetControllerStatePacked pGetControllerStatePacked;
		}

		// Token: 0x02000765 RID: 1893
		// (Invoke) Token: 0x06002F93 RID: 12179
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetControllerStateWithPosePacked(ETrackingUniverseOrigin eOrigin, uint unControllerDeviceIndex, ref VRControllerState_t_Packed pControllerState, uint unControllerStateSize, ref TrackedDevicePose_t pTrackedDevicePose);

		// Token: 0x02000766 RID: 1894
		[StructLayout(LayoutKind.Explicit)]
		private struct GetControllerStateWithPoseUnion
		{
			// Token: 0x040028EA RID: 10474
			[FieldOffset(0)]
			public IVRSystem._GetControllerStateWithPose pGetControllerStateWithPose;

			// Token: 0x040028EB RID: 10475
			[FieldOffset(0)]
			public CVRSystem._GetControllerStateWithPosePacked pGetControllerStateWithPosePacked;
		}
	}
}
