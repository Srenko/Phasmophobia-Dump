using System;

namespace Valve.VR
{
	// Token: 0x020003A7 RID: 935
	public enum ETrackedDeviceProperty
	{
		// Token: 0x0400195B RID: 6491
		Prop_Invalid,
		// Token: 0x0400195C RID: 6492
		Prop_TrackingSystemName_String = 1000,
		// Token: 0x0400195D RID: 6493
		Prop_ModelNumber_String,
		// Token: 0x0400195E RID: 6494
		Prop_SerialNumber_String,
		// Token: 0x0400195F RID: 6495
		Prop_RenderModelName_String,
		// Token: 0x04001960 RID: 6496
		Prop_WillDriftInYaw_Bool,
		// Token: 0x04001961 RID: 6497
		Prop_ManufacturerName_String,
		// Token: 0x04001962 RID: 6498
		Prop_TrackingFirmwareVersion_String,
		// Token: 0x04001963 RID: 6499
		Prop_HardwareRevision_String,
		// Token: 0x04001964 RID: 6500
		Prop_AllWirelessDongleDescriptions_String,
		// Token: 0x04001965 RID: 6501
		Prop_ConnectedWirelessDongle_String,
		// Token: 0x04001966 RID: 6502
		Prop_DeviceIsWireless_Bool,
		// Token: 0x04001967 RID: 6503
		Prop_DeviceIsCharging_Bool,
		// Token: 0x04001968 RID: 6504
		Prop_DeviceBatteryPercentage_Float,
		// Token: 0x04001969 RID: 6505
		Prop_StatusDisplayTransform_Matrix34,
		// Token: 0x0400196A RID: 6506
		Prop_Firmware_UpdateAvailable_Bool,
		// Token: 0x0400196B RID: 6507
		Prop_Firmware_ManualUpdate_Bool,
		// Token: 0x0400196C RID: 6508
		Prop_Firmware_ManualUpdateURL_String,
		// Token: 0x0400196D RID: 6509
		Prop_HardwareRevision_Uint64,
		// Token: 0x0400196E RID: 6510
		Prop_FirmwareVersion_Uint64,
		// Token: 0x0400196F RID: 6511
		Prop_FPGAVersion_Uint64,
		// Token: 0x04001970 RID: 6512
		Prop_VRCVersion_Uint64,
		// Token: 0x04001971 RID: 6513
		Prop_RadioVersion_Uint64,
		// Token: 0x04001972 RID: 6514
		Prop_DongleVersion_Uint64,
		// Token: 0x04001973 RID: 6515
		Prop_BlockServerShutdown_Bool,
		// Token: 0x04001974 RID: 6516
		Prop_CanUnifyCoordinateSystemWithHmd_Bool,
		// Token: 0x04001975 RID: 6517
		Prop_ContainsProximitySensor_Bool,
		// Token: 0x04001976 RID: 6518
		Prop_DeviceProvidesBatteryStatus_Bool,
		// Token: 0x04001977 RID: 6519
		Prop_DeviceCanPowerOff_Bool,
		// Token: 0x04001978 RID: 6520
		Prop_Firmware_ProgrammingTarget_String,
		// Token: 0x04001979 RID: 6521
		Prop_DeviceClass_Int32,
		// Token: 0x0400197A RID: 6522
		Prop_HasCamera_Bool,
		// Token: 0x0400197B RID: 6523
		Prop_DriverVersion_String,
		// Token: 0x0400197C RID: 6524
		Prop_Firmware_ForceUpdateRequired_Bool,
		// Token: 0x0400197D RID: 6525
		Prop_ViveSystemButtonFixRequired_Bool,
		// Token: 0x0400197E RID: 6526
		Prop_ParentDriver_Uint64,
		// Token: 0x0400197F RID: 6527
		Prop_ResourceRoot_String,
		// Token: 0x04001980 RID: 6528
		Prop_ReportsTimeSinceVSync_Bool = 2000,
		// Token: 0x04001981 RID: 6529
		Prop_SecondsFromVsyncToPhotons_Float,
		// Token: 0x04001982 RID: 6530
		Prop_DisplayFrequency_Float,
		// Token: 0x04001983 RID: 6531
		Prop_UserIpdMeters_Float,
		// Token: 0x04001984 RID: 6532
		Prop_CurrentUniverseId_Uint64,
		// Token: 0x04001985 RID: 6533
		Prop_PreviousUniverseId_Uint64,
		// Token: 0x04001986 RID: 6534
		Prop_DisplayFirmwareVersion_Uint64,
		// Token: 0x04001987 RID: 6535
		Prop_IsOnDesktop_Bool,
		// Token: 0x04001988 RID: 6536
		Prop_DisplayMCType_Int32,
		// Token: 0x04001989 RID: 6537
		Prop_DisplayMCOffset_Float,
		// Token: 0x0400198A RID: 6538
		Prop_DisplayMCScale_Float,
		// Token: 0x0400198B RID: 6539
		Prop_EdidVendorID_Int32,
		// Token: 0x0400198C RID: 6540
		Prop_DisplayMCImageLeft_String,
		// Token: 0x0400198D RID: 6541
		Prop_DisplayMCImageRight_String,
		// Token: 0x0400198E RID: 6542
		Prop_DisplayGCBlackClamp_Float,
		// Token: 0x0400198F RID: 6543
		Prop_EdidProductID_Int32,
		// Token: 0x04001990 RID: 6544
		Prop_CameraToHeadTransform_Matrix34,
		// Token: 0x04001991 RID: 6545
		Prop_DisplayGCType_Int32,
		// Token: 0x04001992 RID: 6546
		Prop_DisplayGCOffset_Float,
		// Token: 0x04001993 RID: 6547
		Prop_DisplayGCScale_Float,
		// Token: 0x04001994 RID: 6548
		Prop_DisplayGCPrescale_Float,
		// Token: 0x04001995 RID: 6549
		Prop_DisplayGCImage_String,
		// Token: 0x04001996 RID: 6550
		Prop_LensCenterLeftU_Float,
		// Token: 0x04001997 RID: 6551
		Prop_LensCenterLeftV_Float,
		// Token: 0x04001998 RID: 6552
		Prop_LensCenterRightU_Float,
		// Token: 0x04001999 RID: 6553
		Prop_LensCenterRightV_Float,
		// Token: 0x0400199A RID: 6554
		Prop_UserHeadToEyeDepthMeters_Float,
		// Token: 0x0400199B RID: 6555
		Prop_CameraFirmwareVersion_Uint64,
		// Token: 0x0400199C RID: 6556
		Prop_CameraFirmwareDescription_String,
		// Token: 0x0400199D RID: 6557
		Prop_DisplayFPGAVersion_Uint64,
		// Token: 0x0400199E RID: 6558
		Prop_DisplayBootloaderVersion_Uint64,
		// Token: 0x0400199F RID: 6559
		Prop_DisplayHardwareVersion_Uint64,
		// Token: 0x040019A0 RID: 6560
		Prop_AudioFirmwareVersion_Uint64,
		// Token: 0x040019A1 RID: 6561
		Prop_CameraCompatibilityMode_Int32,
		// Token: 0x040019A2 RID: 6562
		Prop_ScreenshotHorizontalFieldOfViewDegrees_Float,
		// Token: 0x040019A3 RID: 6563
		Prop_ScreenshotVerticalFieldOfViewDegrees_Float,
		// Token: 0x040019A4 RID: 6564
		Prop_DisplaySuppressed_Bool,
		// Token: 0x040019A5 RID: 6565
		Prop_DisplayAllowNightMode_Bool,
		// Token: 0x040019A6 RID: 6566
		Prop_DisplayMCImageWidth_Int32,
		// Token: 0x040019A7 RID: 6567
		Prop_DisplayMCImageHeight_Int32,
		// Token: 0x040019A8 RID: 6568
		Prop_DisplayMCImageNumChannels_Int32,
		// Token: 0x040019A9 RID: 6569
		Prop_DisplayMCImageData_Binary,
		// Token: 0x040019AA RID: 6570
		Prop_SecondsFromPhotonsToVblank_Float,
		// Token: 0x040019AB RID: 6571
		Prop_DriverDirectModeSendsVsyncEvents_Bool,
		// Token: 0x040019AC RID: 6572
		Prop_DisplayDebugMode_Bool,
		// Token: 0x040019AD RID: 6573
		Prop_GraphicsAdapterLuid_Uint64,
		// Token: 0x040019AE RID: 6574
		Prop_AttachedDeviceId_String = 3000,
		// Token: 0x040019AF RID: 6575
		Prop_SupportedButtons_Uint64,
		// Token: 0x040019B0 RID: 6576
		Prop_Axis0Type_Int32,
		// Token: 0x040019B1 RID: 6577
		Prop_Axis1Type_Int32,
		// Token: 0x040019B2 RID: 6578
		Prop_Axis2Type_Int32,
		// Token: 0x040019B3 RID: 6579
		Prop_Axis3Type_Int32,
		// Token: 0x040019B4 RID: 6580
		Prop_Axis4Type_Int32,
		// Token: 0x040019B5 RID: 6581
		Prop_ControllerRoleHint_Int32,
		// Token: 0x040019B6 RID: 6582
		Prop_FieldOfViewLeftDegrees_Float = 4000,
		// Token: 0x040019B7 RID: 6583
		Prop_FieldOfViewRightDegrees_Float,
		// Token: 0x040019B8 RID: 6584
		Prop_FieldOfViewTopDegrees_Float,
		// Token: 0x040019B9 RID: 6585
		Prop_FieldOfViewBottomDegrees_Float,
		// Token: 0x040019BA RID: 6586
		Prop_TrackingRangeMinimumMeters_Float,
		// Token: 0x040019BB RID: 6587
		Prop_TrackingRangeMaximumMeters_Float,
		// Token: 0x040019BC RID: 6588
		Prop_ModeLabel_String,
		// Token: 0x040019BD RID: 6589
		Prop_IconPathName_String = 5000,
		// Token: 0x040019BE RID: 6590
		Prop_NamedIconPathDeviceOff_String,
		// Token: 0x040019BF RID: 6591
		Prop_NamedIconPathDeviceSearching_String,
		// Token: 0x040019C0 RID: 6592
		Prop_NamedIconPathDeviceSearchingAlert_String,
		// Token: 0x040019C1 RID: 6593
		Prop_NamedIconPathDeviceReady_String,
		// Token: 0x040019C2 RID: 6594
		Prop_NamedIconPathDeviceReadyAlert_String,
		// Token: 0x040019C3 RID: 6595
		Prop_NamedIconPathDeviceNotReady_String,
		// Token: 0x040019C4 RID: 6596
		Prop_NamedIconPathDeviceStandby_String,
		// Token: 0x040019C5 RID: 6597
		Prop_NamedIconPathDeviceAlertLow_String,
		// Token: 0x040019C6 RID: 6598
		Prop_DisplayHiddenArea_Binary_Start = 5100,
		// Token: 0x040019C7 RID: 6599
		Prop_DisplayHiddenArea_Binary_End = 5150,
		// Token: 0x040019C8 RID: 6600
		Prop_UserConfigPath_String = 6000,
		// Token: 0x040019C9 RID: 6601
		Prop_InstallPath_String,
		// Token: 0x040019CA RID: 6602
		Prop_HasDisplayComponent_Bool,
		// Token: 0x040019CB RID: 6603
		Prop_HasControllerComponent_Bool,
		// Token: 0x040019CC RID: 6604
		Prop_HasCameraComponent_Bool,
		// Token: 0x040019CD RID: 6605
		Prop_HasDriverDirectModeComponent_Bool,
		// Token: 0x040019CE RID: 6606
		Prop_HasVirtualDisplayComponent_Bool,
		// Token: 0x040019CF RID: 6607
		Prop_VendorSpecific_Reserved_Start = 10000,
		// Token: 0x040019D0 RID: 6608
		Prop_VendorSpecific_Reserved_End = 10999
	}
}
