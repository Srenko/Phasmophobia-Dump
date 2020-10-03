using System;

namespace Valve.VR
{
	// Token: 0x020003AB RID: 939
	public enum EVREventType
	{
		// Token: 0x040019EE RID: 6638
		VREvent_None,
		// Token: 0x040019EF RID: 6639
		VREvent_TrackedDeviceActivated = 100,
		// Token: 0x040019F0 RID: 6640
		VREvent_TrackedDeviceDeactivated,
		// Token: 0x040019F1 RID: 6641
		VREvent_TrackedDeviceUpdated,
		// Token: 0x040019F2 RID: 6642
		VREvent_TrackedDeviceUserInteractionStarted,
		// Token: 0x040019F3 RID: 6643
		VREvent_TrackedDeviceUserInteractionEnded,
		// Token: 0x040019F4 RID: 6644
		VREvent_IpdChanged,
		// Token: 0x040019F5 RID: 6645
		VREvent_EnterStandbyMode,
		// Token: 0x040019F6 RID: 6646
		VREvent_LeaveStandbyMode,
		// Token: 0x040019F7 RID: 6647
		VREvent_TrackedDeviceRoleChanged,
		// Token: 0x040019F8 RID: 6648
		VREvent_WatchdogWakeUpRequested,
		// Token: 0x040019F9 RID: 6649
		VREvent_LensDistortionChanged,
		// Token: 0x040019FA RID: 6650
		VREvent_PropertyChanged,
		// Token: 0x040019FB RID: 6651
		VREvent_ButtonPress = 200,
		// Token: 0x040019FC RID: 6652
		VREvent_ButtonUnpress,
		// Token: 0x040019FD RID: 6653
		VREvent_ButtonTouch,
		// Token: 0x040019FE RID: 6654
		VREvent_ButtonUntouch,
		// Token: 0x040019FF RID: 6655
		VREvent_MouseMove = 300,
		// Token: 0x04001A00 RID: 6656
		VREvent_MouseButtonDown,
		// Token: 0x04001A01 RID: 6657
		VREvent_MouseButtonUp,
		// Token: 0x04001A02 RID: 6658
		VREvent_FocusEnter,
		// Token: 0x04001A03 RID: 6659
		VREvent_FocusLeave,
		// Token: 0x04001A04 RID: 6660
		VREvent_Scroll,
		// Token: 0x04001A05 RID: 6661
		VREvent_TouchPadMove,
		// Token: 0x04001A06 RID: 6662
		VREvent_OverlayFocusChanged,
		// Token: 0x04001A07 RID: 6663
		VREvent_InputFocusCaptured = 400,
		// Token: 0x04001A08 RID: 6664
		VREvent_InputFocusReleased,
		// Token: 0x04001A09 RID: 6665
		VREvent_SceneFocusLost,
		// Token: 0x04001A0A RID: 6666
		VREvent_SceneFocusGained,
		// Token: 0x04001A0B RID: 6667
		VREvent_SceneApplicationChanged,
		// Token: 0x04001A0C RID: 6668
		VREvent_SceneFocusChanged,
		// Token: 0x04001A0D RID: 6669
		VREvent_InputFocusChanged,
		// Token: 0x04001A0E RID: 6670
		VREvent_SceneApplicationSecondaryRenderingStarted,
		// Token: 0x04001A0F RID: 6671
		VREvent_HideRenderModels = 410,
		// Token: 0x04001A10 RID: 6672
		VREvent_ShowRenderModels,
		// Token: 0x04001A11 RID: 6673
		VREvent_OverlayShown = 500,
		// Token: 0x04001A12 RID: 6674
		VREvent_OverlayHidden,
		// Token: 0x04001A13 RID: 6675
		VREvent_DashboardActivated,
		// Token: 0x04001A14 RID: 6676
		VREvent_DashboardDeactivated,
		// Token: 0x04001A15 RID: 6677
		VREvent_DashboardThumbSelected,
		// Token: 0x04001A16 RID: 6678
		VREvent_DashboardRequested,
		// Token: 0x04001A17 RID: 6679
		VREvent_ResetDashboard,
		// Token: 0x04001A18 RID: 6680
		VREvent_RenderToast,
		// Token: 0x04001A19 RID: 6681
		VREvent_ImageLoaded,
		// Token: 0x04001A1A RID: 6682
		VREvent_ShowKeyboard,
		// Token: 0x04001A1B RID: 6683
		VREvent_HideKeyboard,
		// Token: 0x04001A1C RID: 6684
		VREvent_OverlayGamepadFocusGained,
		// Token: 0x04001A1D RID: 6685
		VREvent_OverlayGamepadFocusLost,
		// Token: 0x04001A1E RID: 6686
		VREvent_OverlaySharedTextureChanged,
		// Token: 0x04001A1F RID: 6687
		VREvent_DashboardGuideButtonDown,
		// Token: 0x04001A20 RID: 6688
		VREvent_DashboardGuideButtonUp,
		// Token: 0x04001A21 RID: 6689
		VREvent_ScreenshotTriggered,
		// Token: 0x04001A22 RID: 6690
		VREvent_ImageFailed,
		// Token: 0x04001A23 RID: 6691
		VREvent_DashboardOverlayCreated,
		// Token: 0x04001A24 RID: 6692
		VREvent_RequestScreenshot = 520,
		// Token: 0x04001A25 RID: 6693
		VREvent_ScreenshotTaken,
		// Token: 0x04001A26 RID: 6694
		VREvent_ScreenshotFailed,
		// Token: 0x04001A27 RID: 6695
		VREvent_SubmitScreenshotToDashboard,
		// Token: 0x04001A28 RID: 6696
		VREvent_ScreenshotProgressToDashboard,
		// Token: 0x04001A29 RID: 6697
		VREvent_PrimaryDashboardDeviceChanged,
		// Token: 0x04001A2A RID: 6698
		VREvent_Notification_Shown = 600,
		// Token: 0x04001A2B RID: 6699
		VREvent_Notification_Hidden,
		// Token: 0x04001A2C RID: 6700
		VREvent_Notification_BeginInteraction,
		// Token: 0x04001A2D RID: 6701
		VREvent_Notification_Destroyed,
		// Token: 0x04001A2E RID: 6702
		VREvent_Quit = 700,
		// Token: 0x04001A2F RID: 6703
		VREvent_ProcessQuit,
		// Token: 0x04001A30 RID: 6704
		VREvent_QuitAborted_UserPrompt,
		// Token: 0x04001A31 RID: 6705
		VREvent_QuitAcknowledged,
		// Token: 0x04001A32 RID: 6706
		VREvent_DriverRequestedQuit,
		// Token: 0x04001A33 RID: 6707
		VREvent_ChaperoneDataHasChanged = 800,
		// Token: 0x04001A34 RID: 6708
		VREvent_ChaperoneUniverseHasChanged,
		// Token: 0x04001A35 RID: 6709
		VREvent_ChaperoneTempDataHasChanged,
		// Token: 0x04001A36 RID: 6710
		VREvent_ChaperoneSettingsHaveChanged,
		// Token: 0x04001A37 RID: 6711
		VREvent_SeatedZeroPoseReset,
		// Token: 0x04001A38 RID: 6712
		VREvent_AudioSettingsHaveChanged = 820,
		// Token: 0x04001A39 RID: 6713
		VREvent_BackgroundSettingHasChanged = 850,
		// Token: 0x04001A3A RID: 6714
		VREvent_CameraSettingsHaveChanged,
		// Token: 0x04001A3B RID: 6715
		VREvent_ReprojectionSettingHasChanged,
		// Token: 0x04001A3C RID: 6716
		VREvent_ModelSkinSettingsHaveChanged,
		// Token: 0x04001A3D RID: 6717
		VREvent_EnvironmentSettingsHaveChanged,
		// Token: 0x04001A3E RID: 6718
		VREvent_PowerSettingsHaveChanged,
		// Token: 0x04001A3F RID: 6719
		VREvent_EnableHomeAppSettingsHaveChanged,
		// Token: 0x04001A40 RID: 6720
		VREvent_StatusUpdate = 900,
		// Token: 0x04001A41 RID: 6721
		VREvent_MCImageUpdated = 1000,
		// Token: 0x04001A42 RID: 6722
		VREvent_FirmwareUpdateStarted = 1100,
		// Token: 0x04001A43 RID: 6723
		VREvent_FirmwareUpdateFinished,
		// Token: 0x04001A44 RID: 6724
		VREvent_KeyboardClosed = 1200,
		// Token: 0x04001A45 RID: 6725
		VREvent_KeyboardCharInput,
		// Token: 0x04001A46 RID: 6726
		VREvent_KeyboardDone,
		// Token: 0x04001A47 RID: 6727
		VREvent_ApplicationTransitionStarted = 1300,
		// Token: 0x04001A48 RID: 6728
		VREvent_ApplicationTransitionAborted,
		// Token: 0x04001A49 RID: 6729
		VREvent_ApplicationTransitionNewAppStarted,
		// Token: 0x04001A4A RID: 6730
		VREvent_ApplicationListUpdated,
		// Token: 0x04001A4B RID: 6731
		VREvent_ApplicationMimeTypeLoad,
		// Token: 0x04001A4C RID: 6732
		VREvent_ApplicationTransitionNewAppLaunchComplete,
		// Token: 0x04001A4D RID: 6733
		VREvent_ProcessConnected,
		// Token: 0x04001A4E RID: 6734
		VREvent_ProcessDisconnected,
		// Token: 0x04001A4F RID: 6735
		VREvent_Compositor_MirrorWindowShown = 1400,
		// Token: 0x04001A50 RID: 6736
		VREvent_Compositor_MirrorWindowHidden,
		// Token: 0x04001A51 RID: 6737
		VREvent_Compositor_ChaperoneBoundsShown = 1410,
		// Token: 0x04001A52 RID: 6738
		VREvent_Compositor_ChaperoneBoundsHidden,
		// Token: 0x04001A53 RID: 6739
		VREvent_TrackedCamera_StartVideoStream = 1500,
		// Token: 0x04001A54 RID: 6740
		VREvent_TrackedCamera_StopVideoStream,
		// Token: 0x04001A55 RID: 6741
		VREvent_TrackedCamera_PauseVideoStream,
		// Token: 0x04001A56 RID: 6742
		VREvent_TrackedCamera_ResumeVideoStream,
		// Token: 0x04001A57 RID: 6743
		VREvent_TrackedCamera_EditingSurface = 1550,
		// Token: 0x04001A58 RID: 6744
		VREvent_PerformanceTest_EnableCapture = 1600,
		// Token: 0x04001A59 RID: 6745
		VREvent_PerformanceTest_DisableCapture,
		// Token: 0x04001A5A RID: 6746
		VREvent_PerformanceTest_FidelityLevel,
		// Token: 0x04001A5B RID: 6747
		VREvent_MessageOverlay_Closed = 1650,
		// Token: 0x04001A5C RID: 6748
		VREvent_VendorSpecific_Reserved_Start = 10000,
		// Token: 0x04001A5D RID: 6749
		VREvent_VendorSpecific_Reserved_End = 19999
	}
}
