using System;

namespace Valve.VR
{
	// Token: 0x020003B7 RID: 951
	public enum EVRInitError
	{
		// Token: 0x04001ABA RID: 6842
		None,
		// Token: 0x04001ABB RID: 6843
		Unknown,
		// Token: 0x04001ABC RID: 6844
		Init_InstallationNotFound = 100,
		// Token: 0x04001ABD RID: 6845
		Init_InstallationCorrupt,
		// Token: 0x04001ABE RID: 6846
		Init_VRClientDLLNotFound,
		// Token: 0x04001ABF RID: 6847
		Init_FileNotFound,
		// Token: 0x04001AC0 RID: 6848
		Init_FactoryNotFound,
		// Token: 0x04001AC1 RID: 6849
		Init_InterfaceNotFound,
		// Token: 0x04001AC2 RID: 6850
		Init_InvalidInterface,
		// Token: 0x04001AC3 RID: 6851
		Init_UserConfigDirectoryInvalid,
		// Token: 0x04001AC4 RID: 6852
		Init_HmdNotFound,
		// Token: 0x04001AC5 RID: 6853
		Init_NotInitialized,
		// Token: 0x04001AC6 RID: 6854
		Init_PathRegistryNotFound,
		// Token: 0x04001AC7 RID: 6855
		Init_NoConfigPath,
		// Token: 0x04001AC8 RID: 6856
		Init_NoLogPath,
		// Token: 0x04001AC9 RID: 6857
		Init_PathRegistryNotWritable,
		// Token: 0x04001ACA RID: 6858
		Init_AppInfoInitFailed,
		// Token: 0x04001ACB RID: 6859
		Init_Retry,
		// Token: 0x04001ACC RID: 6860
		Init_InitCanceledByUser,
		// Token: 0x04001ACD RID: 6861
		Init_AnotherAppLaunching,
		// Token: 0x04001ACE RID: 6862
		Init_SettingsInitFailed,
		// Token: 0x04001ACF RID: 6863
		Init_ShuttingDown,
		// Token: 0x04001AD0 RID: 6864
		Init_TooManyObjects,
		// Token: 0x04001AD1 RID: 6865
		Init_NoServerForBackgroundApp,
		// Token: 0x04001AD2 RID: 6866
		Init_NotSupportedWithCompositor,
		// Token: 0x04001AD3 RID: 6867
		Init_NotAvailableToUtilityApps,
		// Token: 0x04001AD4 RID: 6868
		Init_Internal,
		// Token: 0x04001AD5 RID: 6869
		Init_HmdDriverIdIsNone,
		// Token: 0x04001AD6 RID: 6870
		Init_HmdNotFoundPresenceFailed,
		// Token: 0x04001AD7 RID: 6871
		Init_VRMonitorNotFound,
		// Token: 0x04001AD8 RID: 6872
		Init_VRMonitorStartupFailed,
		// Token: 0x04001AD9 RID: 6873
		Init_LowPowerWatchdogNotSupported,
		// Token: 0x04001ADA RID: 6874
		Init_InvalidApplicationType,
		// Token: 0x04001ADB RID: 6875
		Init_NotAvailableToWatchdogApps,
		// Token: 0x04001ADC RID: 6876
		Init_WatchdogDisabledInSettings,
		// Token: 0x04001ADD RID: 6877
		Init_VRDashboardNotFound,
		// Token: 0x04001ADE RID: 6878
		Init_VRDashboardStartupFailed,
		// Token: 0x04001ADF RID: 6879
		Init_VRHomeNotFound,
		// Token: 0x04001AE0 RID: 6880
		Init_VRHomeStartupFailed,
		// Token: 0x04001AE1 RID: 6881
		Driver_Failed = 200,
		// Token: 0x04001AE2 RID: 6882
		Driver_Unknown,
		// Token: 0x04001AE3 RID: 6883
		Driver_HmdUnknown,
		// Token: 0x04001AE4 RID: 6884
		Driver_NotLoaded,
		// Token: 0x04001AE5 RID: 6885
		Driver_RuntimeOutOfDate,
		// Token: 0x04001AE6 RID: 6886
		Driver_HmdInUse,
		// Token: 0x04001AE7 RID: 6887
		Driver_NotCalibrated,
		// Token: 0x04001AE8 RID: 6888
		Driver_CalibrationInvalid,
		// Token: 0x04001AE9 RID: 6889
		Driver_HmdDisplayNotFound,
		// Token: 0x04001AEA RID: 6890
		Driver_TrackedDeviceInterfaceUnknown,
		// Token: 0x04001AEB RID: 6891
		Driver_HmdDriverIdOutOfBounds = 211,
		// Token: 0x04001AEC RID: 6892
		Driver_HmdDisplayMirrored,
		// Token: 0x04001AED RID: 6893
		IPC_ServerInitFailed = 300,
		// Token: 0x04001AEE RID: 6894
		IPC_ConnectFailed,
		// Token: 0x04001AEF RID: 6895
		IPC_SharedStateInitFailed,
		// Token: 0x04001AF0 RID: 6896
		IPC_CompositorInitFailed,
		// Token: 0x04001AF1 RID: 6897
		IPC_MutexInitFailed,
		// Token: 0x04001AF2 RID: 6898
		IPC_Failed,
		// Token: 0x04001AF3 RID: 6899
		IPC_CompositorConnectFailed,
		// Token: 0x04001AF4 RID: 6900
		IPC_CompositorInvalidConnectResponse,
		// Token: 0x04001AF5 RID: 6901
		IPC_ConnectFailedAfterMultipleAttempts,
		// Token: 0x04001AF6 RID: 6902
		Compositor_Failed = 400,
		// Token: 0x04001AF7 RID: 6903
		Compositor_D3D11HardwareRequired,
		// Token: 0x04001AF8 RID: 6904
		Compositor_FirmwareRequiresUpdate,
		// Token: 0x04001AF9 RID: 6905
		Compositor_OverlayInitFailed,
		// Token: 0x04001AFA RID: 6906
		Compositor_ScreenshotsInitFailed,
		// Token: 0x04001AFB RID: 6907
		Compositor_UnableToCreateDevice,
		// Token: 0x04001AFC RID: 6908
		VendorSpecific_UnableToConnectToOculusRuntime = 1000,
		// Token: 0x04001AFD RID: 6909
		VendorSpecific_HmdFound_CantOpenDevice = 1101,
		// Token: 0x04001AFE RID: 6910
		VendorSpecific_HmdFound_UnableToRequestConfigStart,
		// Token: 0x04001AFF RID: 6911
		VendorSpecific_HmdFound_NoStoredConfig,
		// Token: 0x04001B00 RID: 6912
		VendorSpecific_HmdFound_ConfigTooBig,
		// Token: 0x04001B01 RID: 6913
		VendorSpecific_HmdFound_ConfigTooSmall,
		// Token: 0x04001B02 RID: 6914
		VendorSpecific_HmdFound_UnableToInitZLib,
		// Token: 0x04001B03 RID: 6915
		VendorSpecific_HmdFound_CantReadFirmwareVersion,
		// Token: 0x04001B04 RID: 6916
		VendorSpecific_HmdFound_UnableToSendUserDataStart,
		// Token: 0x04001B05 RID: 6917
		VendorSpecific_HmdFound_UnableToGetUserDataStart,
		// Token: 0x04001B06 RID: 6918
		VendorSpecific_HmdFound_UnableToGetUserDataNext,
		// Token: 0x04001B07 RID: 6919
		VendorSpecific_HmdFound_UserDataAddressRange,
		// Token: 0x04001B08 RID: 6920
		VendorSpecific_HmdFound_UserDataError,
		// Token: 0x04001B09 RID: 6921
		VendorSpecific_HmdFound_ConfigFailedSanityCheck,
		// Token: 0x04001B0A RID: 6922
		Steam_SteamInstallationNotFound = 2000
	}
}
