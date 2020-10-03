using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x02000410 RID: 1040
	public class OpenVR
	{
		// Token: 0x0600200F RID: 8207 RVA: 0x0009E721 File Offset: 0x0009C921
		public static uint InitInternal(ref EVRInitError peError, EVRApplicationType eApplicationType)
		{
			return OpenVRInterop.InitInternal(ref peError, eApplicationType);
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x0009E72A File Offset: 0x0009C92A
		public static void ShutdownInternal()
		{
			OpenVRInterop.ShutdownInternal();
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0009E731 File Offset: 0x0009C931
		public static bool IsHmdPresent()
		{
			return OpenVRInterop.IsHmdPresent();
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0009E738 File Offset: 0x0009C938
		public static bool IsRuntimeInstalled()
		{
			return OpenVRInterop.IsRuntimeInstalled();
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x0009E73F File Offset: 0x0009C93F
		public static string GetStringForHmdError(EVRInitError error)
		{
			return Marshal.PtrToStringAnsi(OpenVRInterop.GetStringForHmdError(error));
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x0009E74C File Offset: 0x0009C94C
		public static IntPtr GetGenericInterface(string pchInterfaceVersion, ref EVRInitError peError)
		{
			return OpenVRInterop.GetGenericInterface(pchInterfaceVersion, ref peError);
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x0009E755 File Offset: 0x0009C955
		public static bool IsInterfaceVersionValid(string pchInterfaceVersion)
		{
			return OpenVRInterop.IsInterfaceVersionValid(pchInterfaceVersion);
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x0009E75D File Offset: 0x0009C95D
		public static uint GetInitToken()
		{
			return OpenVRInterop.GetInitToken();
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x0009E764 File Offset: 0x0009C964
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x0009E76B File Offset: 0x0009C96B
		private static uint VRToken { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x0009E773 File Offset: 0x0009C973
		private static OpenVR.COpenVRContext OpenVRInternal_ModuleContext
		{
			get
			{
				if (OpenVR._OpenVRInternal_ModuleContext == null)
				{
					OpenVR._OpenVRInternal_ModuleContext = new OpenVR.COpenVRContext();
				}
				return OpenVR._OpenVRInternal_ModuleContext;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x0009E78B File Offset: 0x0009C98B
		public static CVRSystem System
		{
			get
			{
				return OpenVR.OpenVRInternal_ModuleContext.VRSystem();
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x0009E797 File Offset: 0x0009C997
		public static CVRChaperone Chaperone
		{
			get
			{
				return OpenVR.OpenVRInternal_ModuleContext.VRChaperone();
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x0009E7A3 File Offset: 0x0009C9A3
		public static CVRChaperoneSetup ChaperoneSetup
		{
			get
			{
				return OpenVR.OpenVRInternal_ModuleContext.VRChaperoneSetup();
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x0009E7AF File Offset: 0x0009C9AF
		public static CVRCompositor Compositor
		{
			get
			{
				return OpenVR.OpenVRInternal_ModuleContext.VRCompositor();
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x0009E7BB File Offset: 0x0009C9BB
		public static CVROverlay Overlay
		{
			get
			{
				return OpenVR.OpenVRInternal_ModuleContext.VROverlay();
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x0009E7C7 File Offset: 0x0009C9C7
		public static CVRRenderModels RenderModels
		{
			get
			{
				return OpenVR.OpenVRInternal_ModuleContext.VRRenderModels();
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x0009E7D3 File Offset: 0x0009C9D3
		public static CVRExtendedDisplay ExtendedDisplay
		{
			get
			{
				return OpenVR.OpenVRInternal_ModuleContext.VRExtendedDisplay();
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x0009E7DF File Offset: 0x0009C9DF
		public static CVRSettings Settings
		{
			get
			{
				return OpenVR.OpenVRInternal_ModuleContext.VRSettings();
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x0009E7EB File Offset: 0x0009C9EB
		public static CVRApplications Applications
		{
			get
			{
				return OpenVR.OpenVRInternal_ModuleContext.VRApplications();
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x0009E7F7 File Offset: 0x0009C9F7
		public static CVRScreenshots Screenshots
		{
			get
			{
				return OpenVR.OpenVRInternal_ModuleContext.VRScreenshots();
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x0009E803 File Offset: 0x0009CA03
		public static CVRTrackedCamera TrackedCamera
		{
			get
			{
				return OpenVR.OpenVRInternal_ModuleContext.VRTrackedCamera();
			}
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0009E80F File Offset: 0x0009CA0F
		public static CVRSystem Init(ref EVRInitError peError, EVRApplicationType eApplicationType = EVRApplicationType.VRApplication_Scene)
		{
			OpenVR.VRToken = OpenVR.InitInternal(ref peError, eApplicationType);
			OpenVR.OpenVRInternal_ModuleContext.Clear();
			if (peError != EVRInitError.None)
			{
				return null;
			}
			if (!OpenVR.IsInterfaceVersionValid("IVRSystem_016"))
			{
				OpenVR.ShutdownInternal();
				peError = EVRInitError.Init_InterfaceNotFound;
				return null;
			}
			return OpenVR.System;
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x0009E849 File Offset: 0x0009CA49
		public static void Shutdown()
		{
			OpenVR.ShutdownInternal();
		}

		// Token: 0x04001CFB RID: 7419
		public const uint k_nDriverNone = 4294967295U;

		// Token: 0x04001CFC RID: 7420
		public const uint k_unMaxDriverDebugResponseSize = 32768U;

		// Token: 0x04001CFD RID: 7421
		public const uint k_unTrackedDeviceIndex_Hmd = 0U;

		// Token: 0x04001CFE RID: 7422
		public const uint k_unMaxTrackedDeviceCount = 16U;

		// Token: 0x04001CFF RID: 7423
		public const uint k_unTrackedDeviceIndexOther = 4294967294U;

		// Token: 0x04001D00 RID: 7424
		public const uint k_unTrackedDeviceIndexInvalid = 4294967295U;

		// Token: 0x04001D01 RID: 7425
		public const ulong k_ulInvalidPropertyContainer = 0UL;

		// Token: 0x04001D02 RID: 7426
		public const uint k_unInvalidPropertyTag = 0U;

		// Token: 0x04001D03 RID: 7427
		public const uint k_unFloatPropertyTag = 1U;

		// Token: 0x04001D04 RID: 7428
		public const uint k_unInt32PropertyTag = 2U;

		// Token: 0x04001D05 RID: 7429
		public const uint k_unUint64PropertyTag = 3U;

		// Token: 0x04001D06 RID: 7430
		public const uint k_unBoolPropertyTag = 4U;

		// Token: 0x04001D07 RID: 7431
		public const uint k_unStringPropertyTag = 5U;

		// Token: 0x04001D08 RID: 7432
		public const uint k_unHmdMatrix34PropertyTag = 20U;

		// Token: 0x04001D09 RID: 7433
		public const uint k_unHmdMatrix44PropertyTag = 21U;

		// Token: 0x04001D0A RID: 7434
		public const uint k_unHmdVector3PropertyTag = 22U;

		// Token: 0x04001D0B RID: 7435
		public const uint k_unHmdVector4PropertyTag = 23U;

		// Token: 0x04001D0C RID: 7436
		public const uint k_unHiddenAreaPropertyTag = 30U;

		// Token: 0x04001D0D RID: 7437
		public const uint k_unOpenVRInternalReserved_Start = 1000U;

		// Token: 0x04001D0E RID: 7438
		public const uint k_unOpenVRInternalReserved_End = 10000U;

		// Token: 0x04001D0F RID: 7439
		public const uint k_unMaxPropertyStringSize = 32768U;

		// Token: 0x04001D10 RID: 7440
		public const uint k_unControllerStateAxisCount = 5U;

		// Token: 0x04001D11 RID: 7441
		public const ulong k_ulOverlayHandleInvalid = 0UL;

		// Token: 0x04001D12 RID: 7442
		public const uint k_unScreenshotHandleInvalid = 0U;

		// Token: 0x04001D13 RID: 7443
		public const string IVRSystem_Version = "IVRSystem_016";

		// Token: 0x04001D14 RID: 7444
		public const string IVRExtendedDisplay_Version = "IVRExtendedDisplay_001";

		// Token: 0x04001D15 RID: 7445
		public const string IVRTrackedCamera_Version = "IVRTrackedCamera_003";

		// Token: 0x04001D16 RID: 7446
		public const uint k_unMaxApplicationKeyLength = 128U;

		// Token: 0x04001D17 RID: 7447
		public const string k_pch_MimeType_HomeApp = "vr/home";

		// Token: 0x04001D18 RID: 7448
		public const string k_pch_MimeType_GameTheater = "vr/game_theater";

		// Token: 0x04001D19 RID: 7449
		public const string IVRApplications_Version = "IVRApplications_006";

		// Token: 0x04001D1A RID: 7450
		public const string IVRChaperone_Version = "IVRChaperone_003";

		// Token: 0x04001D1B RID: 7451
		public const string IVRChaperoneSetup_Version = "IVRChaperoneSetup_005";

		// Token: 0x04001D1C RID: 7452
		public const string IVRCompositor_Version = "IVRCompositor_020";

		// Token: 0x04001D1D RID: 7453
		public const uint k_unVROverlayMaxKeyLength = 128U;

		// Token: 0x04001D1E RID: 7454
		public const uint k_unVROverlayMaxNameLength = 128U;

		// Token: 0x04001D1F RID: 7455
		public const uint k_unMaxOverlayCount = 64U;

		// Token: 0x04001D20 RID: 7456
		public const uint k_unMaxOverlayIntersectionMaskPrimitivesCount = 32U;

		// Token: 0x04001D21 RID: 7457
		public const string IVROverlay_Version = "IVROverlay_016";

		// Token: 0x04001D22 RID: 7458
		public const string k_pch_Controller_Component_GDC2015 = "gdc2015";

		// Token: 0x04001D23 RID: 7459
		public const string k_pch_Controller_Component_Base = "base";

		// Token: 0x04001D24 RID: 7460
		public const string k_pch_Controller_Component_Tip = "tip";

		// Token: 0x04001D25 RID: 7461
		public const string k_pch_Controller_Component_HandGrip = "handgrip";

		// Token: 0x04001D26 RID: 7462
		public const string k_pch_Controller_Component_Status = "status";

		// Token: 0x04001D27 RID: 7463
		public const string IVRRenderModels_Version = "IVRRenderModels_005";

		// Token: 0x04001D28 RID: 7464
		public const uint k_unNotificationTextMaxSize = 256U;

		// Token: 0x04001D29 RID: 7465
		public const string IVRNotifications_Version = "IVRNotifications_002";

		// Token: 0x04001D2A RID: 7466
		public const uint k_unMaxSettingsKeyLength = 128U;

		// Token: 0x04001D2B RID: 7467
		public const string IVRSettings_Version = "IVRSettings_002";

		// Token: 0x04001D2C RID: 7468
		public const string k_pch_SteamVR_Section = "steamvr";

		// Token: 0x04001D2D RID: 7469
		public const string k_pch_SteamVR_RequireHmd_String = "requireHmd";

		// Token: 0x04001D2E RID: 7470
		public const string k_pch_SteamVR_ForcedDriverKey_String = "forcedDriver";

		// Token: 0x04001D2F RID: 7471
		public const string k_pch_SteamVR_ForcedHmdKey_String = "forcedHmd";

		// Token: 0x04001D30 RID: 7472
		public const string k_pch_SteamVR_DisplayDebug_Bool = "displayDebug";

		// Token: 0x04001D31 RID: 7473
		public const string k_pch_SteamVR_DebugProcessPipe_String = "debugProcessPipe";

		// Token: 0x04001D32 RID: 7474
		public const string k_pch_SteamVR_DisplayDebugX_Int32 = "displayDebugX";

		// Token: 0x04001D33 RID: 7475
		public const string k_pch_SteamVR_DisplayDebugY_Int32 = "displayDebugY";

		// Token: 0x04001D34 RID: 7476
		public const string k_pch_SteamVR_SendSystemButtonToAllApps_Bool = "sendSystemButtonToAllApps";

		// Token: 0x04001D35 RID: 7477
		public const string k_pch_SteamVR_LogLevel_Int32 = "loglevel";

		// Token: 0x04001D36 RID: 7478
		public const string k_pch_SteamVR_IPD_Float = "ipd";

		// Token: 0x04001D37 RID: 7479
		public const string k_pch_SteamVR_Background_String = "background";

		// Token: 0x04001D38 RID: 7480
		public const string k_pch_SteamVR_BackgroundUseDomeProjection_Bool = "backgroundUseDomeProjection";

		// Token: 0x04001D39 RID: 7481
		public const string k_pch_SteamVR_BackgroundCameraHeight_Float = "backgroundCameraHeight";

		// Token: 0x04001D3A RID: 7482
		public const string k_pch_SteamVR_BackgroundDomeRadius_Float = "backgroundDomeRadius";

		// Token: 0x04001D3B RID: 7483
		public const string k_pch_SteamVR_GridColor_String = "gridColor";

		// Token: 0x04001D3C RID: 7484
		public const string k_pch_SteamVR_PlayAreaColor_String = "playAreaColor";

		// Token: 0x04001D3D RID: 7485
		public const string k_pch_SteamVR_ShowStage_Bool = "showStage";

		// Token: 0x04001D3E RID: 7486
		public const string k_pch_SteamVR_ActivateMultipleDrivers_Bool = "activateMultipleDrivers";

		// Token: 0x04001D3F RID: 7487
		public const string k_pch_SteamVR_DirectMode_Bool = "directMode";

		// Token: 0x04001D40 RID: 7488
		public const string k_pch_SteamVR_DirectModeEdidVid_Int32 = "directModeEdidVid";

		// Token: 0x04001D41 RID: 7489
		public const string k_pch_SteamVR_DirectModeEdidPid_Int32 = "directModeEdidPid";

		// Token: 0x04001D42 RID: 7490
		public const string k_pch_SteamVR_UsingSpeakers_Bool = "usingSpeakers";

		// Token: 0x04001D43 RID: 7491
		public const string k_pch_SteamVR_SpeakersForwardYawOffsetDegrees_Float = "speakersForwardYawOffsetDegrees";

		// Token: 0x04001D44 RID: 7492
		public const string k_pch_SteamVR_BaseStationPowerManagement_Bool = "basestationPowerManagement";

		// Token: 0x04001D45 RID: 7493
		public const string k_pch_SteamVR_NeverKillProcesses_Bool = "neverKillProcesses";

		// Token: 0x04001D46 RID: 7494
		public const string k_pch_SteamVR_SupersampleScale_Float = "supersampleScale";

		// Token: 0x04001D47 RID: 7495
		public const string k_pch_SteamVR_AllowAsyncReprojection_Bool = "allowAsyncReprojection";

		// Token: 0x04001D48 RID: 7496
		public const string k_pch_SteamVR_AllowReprojection_Bool = "allowInterleavedReprojection";

		// Token: 0x04001D49 RID: 7497
		public const string k_pch_SteamVR_ForceReprojection_Bool = "forceReprojection";

		// Token: 0x04001D4A RID: 7498
		public const string k_pch_SteamVR_ForceFadeOnBadTracking_Bool = "forceFadeOnBadTracking";

		// Token: 0x04001D4B RID: 7499
		public const string k_pch_SteamVR_DefaultMirrorView_Int32 = "defaultMirrorView";

		// Token: 0x04001D4C RID: 7500
		public const string k_pch_SteamVR_ShowMirrorView_Bool = "showMirrorView";

		// Token: 0x04001D4D RID: 7501
		public const string k_pch_SteamVR_MirrorViewGeometry_String = "mirrorViewGeometry";

		// Token: 0x04001D4E RID: 7502
		public const string k_pch_SteamVR_StartMonitorFromAppLaunch = "startMonitorFromAppLaunch";

		// Token: 0x04001D4F RID: 7503
		public const string k_pch_SteamVR_StartCompositorFromAppLaunch_Bool = "startCompositorFromAppLaunch";

		// Token: 0x04001D50 RID: 7504
		public const string k_pch_SteamVR_StartDashboardFromAppLaunch_Bool = "startDashboardFromAppLaunch";

		// Token: 0x04001D51 RID: 7505
		public const string k_pch_SteamVR_StartOverlayAppsFromDashboard_Bool = "startOverlayAppsFromDashboard";

		// Token: 0x04001D52 RID: 7506
		public const string k_pch_SteamVR_EnableHomeApp = "enableHomeApp";

		// Token: 0x04001D53 RID: 7507
		public const string k_pch_SteamVR_CycleBackgroundImageTimeSec_Int32 = "CycleBackgroundImageTimeSec";

		// Token: 0x04001D54 RID: 7508
		public const string k_pch_SteamVR_RetailDemo_Bool = "retailDemo";

		// Token: 0x04001D55 RID: 7509
		public const string k_pch_SteamVR_IpdOffset_Float = "ipdOffset";

		// Token: 0x04001D56 RID: 7510
		public const string k_pch_SteamVR_AllowSupersampleFiltering_Bool = "allowSupersampleFiltering";

		// Token: 0x04001D57 RID: 7511
		public const string k_pch_Lighthouse_Section = "driver_lighthouse";

		// Token: 0x04001D58 RID: 7512
		public const string k_pch_Lighthouse_DisableIMU_Bool = "disableimu";

		// Token: 0x04001D59 RID: 7513
		public const string k_pch_Lighthouse_UseDisambiguation_String = "usedisambiguation";

		// Token: 0x04001D5A RID: 7514
		public const string k_pch_Lighthouse_DisambiguationDebug_Int32 = "disambiguationdebug";

		// Token: 0x04001D5B RID: 7515
		public const string k_pch_Lighthouse_PrimaryBasestation_Int32 = "primarybasestation";

		// Token: 0x04001D5C RID: 7516
		public const string k_pch_Lighthouse_DBHistory_Bool = "dbhistory";

		// Token: 0x04001D5D RID: 7517
		public const string k_pch_Null_Section = "driver_null";

		// Token: 0x04001D5E RID: 7518
		public const string k_pch_Null_SerialNumber_String = "serialNumber";

		// Token: 0x04001D5F RID: 7519
		public const string k_pch_Null_ModelNumber_String = "modelNumber";

		// Token: 0x04001D60 RID: 7520
		public const string k_pch_Null_WindowX_Int32 = "windowX";

		// Token: 0x04001D61 RID: 7521
		public const string k_pch_Null_WindowY_Int32 = "windowY";

		// Token: 0x04001D62 RID: 7522
		public const string k_pch_Null_WindowWidth_Int32 = "windowWidth";

		// Token: 0x04001D63 RID: 7523
		public const string k_pch_Null_WindowHeight_Int32 = "windowHeight";

		// Token: 0x04001D64 RID: 7524
		public const string k_pch_Null_RenderWidth_Int32 = "renderWidth";

		// Token: 0x04001D65 RID: 7525
		public const string k_pch_Null_RenderHeight_Int32 = "renderHeight";

		// Token: 0x04001D66 RID: 7526
		public const string k_pch_Null_SecondsFromVsyncToPhotons_Float = "secondsFromVsyncToPhotons";

		// Token: 0x04001D67 RID: 7527
		public const string k_pch_Null_DisplayFrequency_Float = "displayFrequency";

		// Token: 0x04001D68 RID: 7528
		public const string k_pch_UserInterface_Section = "userinterface";

		// Token: 0x04001D69 RID: 7529
		public const string k_pch_UserInterface_StatusAlwaysOnTop_Bool = "StatusAlwaysOnTop";

		// Token: 0x04001D6A RID: 7530
		public const string k_pch_UserInterface_MinimizeToTray_Bool = "MinimizeToTray";

		// Token: 0x04001D6B RID: 7531
		public const string k_pch_UserInterface_Screenshots_Bool = "screenshots";

		// Token: 0x04001D6C RID: 7532
		public const string k_pch_UserInterface_ScreenshotType_Int = "screenshotType";

		// Token: 0x04001D6D RID: 7533
		public const string k_pch_Notifications_Section = "notifications";

		// Token: 0x04001D6E RID: 7534
		public const string k_pch_Notifications_DoNotDisturb_Bool = "DoNotDisturb";

		// Token: 0x04001D6F RID: 7535
		public const string k_pch_Keyboard_Section = "keyboard";

		// Token: 0x04001D70 RID: 7536
		public const string k_pch_Keyboard_TutorialCompletions = "TutorialCompletions";

		// Token: 0x04001D71 RID: 7537
		public const string k_pch_Keyboard_ScaleX = "ScaleX";

		// Token: 0x04001D72 RID: 7538
		public const string k_pch_Keyboard_ScaleY = "ScaleY";

		// Token: 0x04001D73 RID: 7539
		public const string k_pch_Keyboard_OffsetLeftX = "OffsetLeftX";

		// Token: 0x04001D74 RID: 7540
		public const string k_pch_Keyboard_OffsetRightX = "OffsetRightX";

		// Token: 0x04001D75 RID: 7541
		public const string k_pch_Keyboard_OffsetY = "OffsetY";

		// Token: 0x04001D76 RID: 7542
		public const string k_pch_Keyboard_Smoothing = "Smoothing";

		// Token: 0x04001D77 RID: 7543
		public const string k_pch_Perf_Section = "perfcheck";

		// Token: 0x04001D78 RID: 7544
		public const string k_pch_Perf_HeuristicActive_Bool = "heuristicActive";

		// Token: 0x04001D79 RID: 7545
		public const string k_pch_Perf_NotifyInHMD_Bool = "warnInHMD";

		// Token: 0x04001D7A RID: 7546
		public const string k_pch_Perf_NotifyOnlyOnce_Bool = "warnOnlyOnce";

		// Token: 0x04001D7B RID: 7547
		public const string k_pch_Perf_AllowTimingStore_Bool = "allowTimingStore";

		// Token: 0x04001D7C RID: 7548
		public const string k_pch_Perf_SaveTimingsOnExit_Bool = "saveTimingsOnExit";

		// Token: 0x04001D7D RID: 7549
		public const string k_pch_Perf_TestData_Float = "perfTestData";

		// Token: 0x04001D7E RID: 7550
		public const string k_pch_Perf_LinuxGPUProfiling_Bool = "linuxGPUProfiling";

		// Token: 0x04001D7F RID: 7551
		public const string k_pch_CollisionBounds_Section = "collisionBounds";

		// Token: 0x04001D80 RID: 7552
		public const string k_pch_CollisionBounds_Style_Int32 = "CollisionBoundsStyle";

		// Token: 0x04001D81 RID: 7553
		public const string k_pch_CollisionBounds_GroundPerimeterOn_Bool = "CollisionBoundsGroundPerimeterOn";

		// Token: 0x04001D82 RID: 7554
		public const string k_pch_CollisionBounds_CenterMarkerOn_Bool = "CollisionBoundsCenterMarkerOn";

		// Token: 0x04001D83 RID: 7555
		public const string k_pch_CollisionBounds_PlaySpaceOn_Bool = "CollisionBoundsPlaySpaceOn";

		// Token: 0x04001D84 RID: 7556
		public const string k_pch_CollisionBounds_FadeDistance_Float = "CollisionBoundsFadeDistance";

		// Token: 0x04001D85 RID: 7557
		public const string k_pch_CollisionBounds_ColorGammaR_Int32 = "CollisionBoundsColorGammaR";

		// Token: 0x04001D86 RID: 7558
		public const string k_pch_CollisionBounds_ColorGammaG_Int32 = "CollisionBoundsColorGammaG";

		// Token: 0x04001D87 RID: 7559
		public const string k_pch_CollisionBounds_ColorGammaB_Int32 = "CollisionBoundsColorGammaB";

		// Token: 0x04001D88 RID: 7560
		public const string k_pch_CollisionBounds_ColorGammaA_Int32 = "CollisionBoundsColorGammaA";

		// Token: 0x04001D89 RID: 7561
		public const string k_pch_Camera_Section = "camera";

		// Token: 0x04001D8A RID: 7562
		public const string k_pch_Camera_EnableCamera_Bool = "enableCamera";

		// Token: 0x04001D8B RID: 7563
		public const string k_pch_Camera_EnableCameraInDashboard_Bool = "enableCameraInDashboard";

		// Token: 0x04001D8C RID: 7564
		public const string k_pch_Camera_EnableCameraForCollisionBounds_Bool = "enableCameraForCollisionBounds";

		// Token: 0x04001D8D RID: 7565
		public const string k_pch_Camera_EnableCameraForRoomView_Bool = "enableCameraForRoomView";

		// Token: 0x04001D8E RID: 7566
		public const string k_pch_Camera_BoundsColorGammaR_Int32 = "cameraBoundsColorGammaR";

		// Token: 0x04001D8F RID: 7567
		public const string k_pch_Camera_BoundsColorGammaG_Int32 = "cameraBoundsColorGammaG";

		// Token: 0x04001D90 RID: 7568
		public const string k_pch_Camera_BoundsColorGammaB_Int32 = "cameraBoundsColorGammaB";

		// Token: 0x04001D91 RID: 7569
		public const string k_pch_Camera_BoundsColorGammaA_Int32 = "cameraBoundsColorGammaA";

		// Token: 0x04001D92 RID: 7570
		public const string k_pch_Camera_BoundsStrength_Int32 = "cameraBoundsStrength";

		// Token: 0x04001D93 RID: 7571
		public const string k_pch_audio_Section = "audio";

		// Token: 0x04001D94 RID: 7572
		public const string k_pch_audio_OnPlaybackDevice_String = "onPlaybackDevice";

		// Token: 0x04001D95 RID: 7573
		public const string k_pch_audio_OnRecordDevice_String = "onRecordDevice";

		// Token: 0x04001D96 RID: 7574
		public const string k_pch_audio_OnPlaybackMirrorDevice_String = "onPlaybackMirrorDevice";

		// Token: 0x04001D97 RID: 7575
		public const string k_pch_audio_OffPlaybackDevice_String = "offPlaybackDevice";

		// Token: 0x04001D98 RID: 7576
		public const string k_pch_audio_OffRecordDevice_String = "offRecordDevice";

		// Token: 0x04001D99 RID: 7577
		public const string k_pch_audio_VIVEHDMIGain = "viveHDMIGain";

		// Token: 0x04001D9A RID: 7578
		public const string k_pch_Power_Section = "power";

		// Token: 0x04001D9B RID: 7579
		public const string k_pch_Power_PowerOffOnExit_Bool = "powerOffOnExit";

		// Token: 0x04001D9C RID: 7580
		public const string k_pch_Power_TurnOffScreensTimeout_Float = "turnOffScreensTimeout";

		// Token: 0x04001D9D RID: 7581
		public const string k_pch_Power_TurnOffControllersTimeout_Float = "turnOffControllersTimeout";

		// Token: 0x04001D9E RID: 7582
		public const string k_pch_Power_ReturnToWatchdogTimeout_Float = "returnToWatchdogTimeout";

		// Token: 0x04001D9F RID: 7583
		public const string k_pch_Power_AutoLaunchSteamVROnButtonPress = "autoLaunchSteamVROnButtonPress";

		// Token: 0x04001DA0 RID: 7584
		public const string k_pch_Dashboard_Section = "dashboard";

		// Token: 0x04001DA1 RID: 7585
		public const string k_pch_Dashboard_EnableDashboard_Bool = "enableDashboard";

		// Token: 0x04001DA2 RID: 7586
		public const string k_pch_Dashboard_ArcadeMode_Bool = "arcadeMode";

		// Token: 0x04001DA3 RID: 7587
		public const string k_pch_modelskin_Section = "modelskins";

		// Token: 0x04001DA4 RID: 7588
		public const string k_pch_Driver_Enable_Bool = "enable";

		// Token: 0x04001DA5 RID: 7589
		public const string IVRScreenshots_Version = "IVRScreenshots_001";

		// Token: 0x04001DA6 RID: 7590
		public const string IVRResources_Version = "IVRResources_001";

		// Token: 0x04001DA7 RID: 7591
		public const string IVRDriverManager_Version = "IVRDriverManager_001";

		// Token: 0x04001DA9 RID: 7593
		private const string FnTable_Prefix = "FnTable:";

		// Token: 0x04001DAA RID: 7594
		private static OpenVR.COpenVRContext _OpenVRInternal_ModuleContext;

		// Token: 0x0200076B RID: 1899
		private class COpenVRContext
		{
			// Token: 0x06002F9E RID: 12190 RVA: 0x000C9EB8 File Offset: 0x000C80B8
			public COpenVRContext()
			{
				this.Clear();
			}

			// Token: 0x06002F9F RID: 12191 RVA: 0x000C9EC8 File Offset: 0x000C80C8
			public void Clear()
			{
				this.m_pVRSystem = null;
				this.m_pVRChaperone = null;
				this.m_pVRChaperoneSetup = null;
				this.m_pVRCompositor = null;
				this.m_pVROverlay = null;
				this.m_pVRRenderModels = null;
				this.m_pVRExtendedDisplay = null;
				this.m_pVRSettings = null;
				this.m_pVRApplications = null;
				this.m_pVRScreenshots = null;
				this.m_pVRTrackedCamera = null;
			}

			// Token: 0x06002FA0 RID: 12192 RVA: 0x000C9F22 File Offset: 0x000C8122
			private void CheckClear()
			{
				if (OpenVR.VRToken != OpenVR.GetInitToken())
				{
					this.Clear();
					OpenVR.VRToken = OpenVR.GetInitToken();
				}
			}

			// Token: 0x06002FA1 RID: 12193 RVA: 0x000C9F40 File Offset: 0x000C8140
			public CVRSystem VRSystem()
			{
				this.CheckClear();
				if (this.m_pVRSystem == null)
				{
					EVRInitError evrinitError = EVRInitError.None;
					IntPtr genericInterface = OpenVRInterop.GetGenericInterface("FnTable:IVRSystem_016", ref evrinitError);
					if (genericInterface != IntPtr.Zero && evrinitError == EVRInitError.None)
					{
						this.m_pVRSystem = new CVRSystem(genericInterface);
					}
				}
				return this.m_pVRSystem;
			}

			// Token: 0x06002FA2 RID: 12194 RVA: 0x000C9F8C File Offset: 0x000C818C
			public CVRChaperone VRChaperone()
			{
				this.CheckClear();
				if (this.m_pVRChaperone == null)
				{
					EVRInitError evrinitError = EVRInitError.None;
					IntPtr genericInterface = OpenVRInterop.GetGenericInterface("FnTable:IVRChaperone_003", ref evrinitError);
					if (genericInterface != IntPtr.Zero && evrinitError == EVRInitError.None)
					{
						this.m_pVRChaperone = new CVRChaperone(genericInterface);
					}
				}
				return this.m_pVRChaperone;
			}

			// Token: 0x06002FA3 RID: 12195 RVA: 0x000C9FD8 File Offset: 0x000C81D8
			public CVRChaperoneSetup VRChaperoneSetup()
			{
				this.CheckClear();
				if (this.m_pVRChaperoneSetup == null)
				{
					EVRInitError evrinitError = EVRInitError.None;
					IntPtr genericInterface = OpenVRInterop.GetGenericInterface("FnTable:IVRChaperoneSetup_005", ref evrinitError);
					if (genericInterface != IntPtr.Zero && evrinitError == EVRInitError.None)
					{
						this.m_pVRChaperoneSetup = new CVRChaperoneSetup(genericInterface);
					}
				}
				return this.m_pVRChaperoneSetup;
			}

			// Token: 0x06002FA4 RID: 12196 RVA: 0x000CA024 File Offset: 0x000C8224
			public CVRCompositor VRCompositor()
			{
				this.CheckClear();
				if (this.m_pVRCompositor == null)
				{
					EVRInitError evrinitError = EVRInitError.None;
					IntPtr genericInterface = OpenVRInterop.GetGenericInterface("FnTable:IVRCompositor_020", ref evrinitError);
					if (genericInterface != IntPtr.Zero && evrinitError == EVRInitError.None)
					{
						this.m_pVRCompositor = new CVRCompositor(genericInterface);
					}
				}
				return this.m_pVRCompositor;
			}

			// Token: 0x06002FA5 RID: 12197 RVA: 0x000CA070 File Offset: 0x000C8270
			public CVROverlay VROverlay()
			{
				this.CheckClear();
				if (this.m_pVROverlay == null)
				{
					EVRInitError evrinitError = EVRInitError.None;
					IntPtr genericInterface = OpenVRInterop.GetGenericInterface("FnTable:IVROverlay_016", ref evrinitError);
					if (genericInterface != IntPtr.Zero && evrinitError == EVRInitError.None)
					{
						this.m_pVROverlay = new CVROverlay(genericInterface);
					}
				}
				return this.m_pVROverlay;
			}

			// Token: 0x06002FA6 RID: 12198 RVA: 0x000CA0BC File Offset: 0x000C82BC
			public CVRRenderModels VRRenderModels()
			{
				this.CheckClear();
				if (this.m_pVRRenderModels == null)
				{
					EVRInitError evrinitError = EVRInitError.None;
					IntPtr genericInterface = OpenVRInterop.GetGenericInterface("FnTable:IVRRenderModels_005", ref evrinitError);
					if (genericInterface != IntPtr.Zero && evrinitError == EVRInitError.None)
					{
						this.m_pVRRenderModels = new CVRRenderModels(genericInterface);
					}
				}
				return this.m_pVRRenderModels;
			}

			// Token: 0x06002FA7 RID: 12199 RVA: 0x000CA108 File Offset: 0x000C8308
			public CVRExtendedDisplay VRExtendedDisplay()
			{
				this.CheckClear();
				if (this.m_pVRExtendedDisplay == null)
				{
					EVRInitError evrinitError = EVRInitError.None;
					IntPtr genericInterface = OpenVRInterop.GetGenericInterface("FnTable:IVRExtendedDisplay_001", ref evrinitError);
					if (genericInterface != IntPtr.Zero && evrinitError == EVRInitError.None)
					{
						this.m_pVRExtendedDisplay = new CVRExtendedDisplay(genericInterface);
					}
				}
				return this.m_pVRExtendedDisplay;
			}

			// Token: 0x06002FA8 RID: 12200 RVA: 0x000CA154 File Offset: 0x000C8354
			public CVRSettings VRSettings()
			{
				this.CheckClear();
				if (this.m_pVRSettings == null)
				{
					EVRInitError evrinitError = EVRInitError.None;
					IntPtr genericInterface = OpenVRInterop.GetGenericInterface("FnTable:IVRSettings_002", ref evrinitError);
					if (genericInterface != IntPtr.Zero && evrinitError == EVRInitError.None)
					{
						this.m_pVRSettings = new CVRSettings(genericInterface);
					}
				}
				return this.m_pVRSettings;
			}

			// Token: 0x06002FA9 RID: 12201 RVA: 0x000CA1A0 File Offset: 0x000C83A0
			public CVRApplications VRApplications()
			{
				this.CheckClear();
				if (this.m_pVRApplications == null)
				{
					EVRInitError evrinitError = EVRInitError.None;
					IntPtr genericInterface = OpenVRInterop.GetGenericInterface("FnTable:IVRApplications_006", ref evrinitError);
					if (genericInterface != IntPtr.Zero && evrinitError == EVRInitError.None)
					{
						this.m_pVRApplications = new CVRApplications(genericInterface);
					}
				}
				return this.m_pVRApplications;
			}

			// Token: 0x06002FAA RID: 12202 RVA: 0x000CA1EC File Offset: 0x000C83EC
			public CVRScreenshots VRScreenshots()
			{
				this.CheckClear();
				if (this.m_pVRScreenshots == null)
				{
					EVRInitError evrinitError = EVRInitError.None;
					IntPtr genericInterface = OpenVRInterop.GetGenericInterface("FnTable:IVRScreenshots_001", ref evrinitError);
					if (genericInterface != IntPtr.Zero && evrinitError == EVRInitError.None)
					{
						this.m_pVRScreenshots = new CVRScreenshots(genericInterface);
					}
				}
				return this.m_pVRScreenshots;
			}

			// Token: 0x06002FAB RID: 12203 RVA: 0x000CA238 File Offset: 0x000C8438
			public CVRTrackedCamera VRTrackedCamera()
			{
				this.CheckClear();
				if (this.m_pVRTrackedCamera == null)
				{
					EVRInitError evrinitError = EVRInitError.None;
					IntPtr genericInterface = OpenVRInterop.GetGenericInterface("FnTable:IVRTrackedCamera_003", ref evrinitError);
					if (genericInterface != IntPtr.Zero && evrinitError == EVRInitError.None)
					{
						this.m_pVRTrackedCamera = new CVRTrackedCamera(genericInterface);
					}
				}
				return this.m_pVRTrackedCamera;
			}

			// Token: 0x040028F0 RID: 10480
			private CVRSystem m_pVRSystem;

			// Token: 0x040028F1 RID: 10481
			private CVRChaperone m_pVRChaperone;

			// Token: 0x040028F2 RID: 10482
			private CVRChaperoneSetup m_pVRChaperoneSetup;

			// Token: 0x040028F3 RID: 10483
			private CVRCompositor m_pVRCompositor;

			// Token: 0x040028F4 RID: 10484
			private CVROverlay m_pVROverlay;

			// Token: 0x040028F5 RID: 10485
			private CVRRenderModels m_pVRRenderModels;

			// Token: 0x040028F6 RID: 10486
			private CVRExtendedDisplay m_pVRExtendedDisplay;

			// Token: 0x040028F7 RID: 10487
			private CVRSettings m_pVRSettings;

			// Token: 0x040028F8 RID: 10488
			private CVRApplications m_pVRApplications;

			// Token: 0x040028F9 RID: 10489
			private CVRScreenshots m_pVRScreenshots;

			// Token: 0x040028FA RID: 10490
			private CVRTrackedCamera m_pVRTrackedCamera;
		}
	}
}
