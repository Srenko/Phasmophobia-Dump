using System;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.XR;
using Valve.VR;

// Token: 0x020001DE RID: 478
public class SteamVR : IDisposable
{
	// Token: 0x170000EA RID: 234
	// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0005280D File Offset: 0x00050A0D
	public static bool active
	{
		get
		{
			return SteamVR._instance != null;
		}
	}

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00052817 File Offset: 0x00050A17
	// (set) Token: 0x06000D09 RID: 3337 RVA: 0x0005282B File Offset: 0x00050A2B
	public static bool enabled
	{
		get
		{
			if (!XRSettings.enabled)
			{
				SteamVR.enabled = false;
			}
			return SteamVR._enabled;
		}
		set
		{
			SteamVR._enabled = value;
			if (!SteamVR._enabled)
			{
				SteamVR.SafeDispose();
			}
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0005283F File Offset: 0x00050A3F
	public static SteamVR instance
	{
		get
		{
			if (!SteamVR.enabled)
			{
				return null;
			}
			if (SteamVR._instance == null)
			{
				SteamVR._instance = SteamVR.CreateInstance();
				if (SteamVR._instance == null)
				{
					SteamVR._enabled = false;
				}
			}
			return SteamVR._instance;
		}
	}

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0005286D File Offset: 0x00050A6D
	public static bool usingNativeSupport
	{
		get
		{
			return XRDevice.GetNativePtr() != IntPtr.Zero;
		}
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x00052880 File Offset: 0x00050A80
	private static SteamVR CreateInstance()
	{
		try
		{
			EVRInitError evrinitError = EVRInitError.None;
			if (!SteamVR.usingNativeSupport)
			{
				Debug.Log("OpenVR initialization failed.  Ensure 'Virtual Reality Supported' is checked in Player Settings, and OpenVR is added to the list of Virtual Reality SDKs.");
				return null;
			}
			OpenVR.GetGenericInterface("IVRCompositor_020", ref evrinitError);
			if (evrinitError != EVRInitError.None)
			{
				SteamVR.ReportError(evrinitError);
				return null;
			}
			OpenVR.GetGenericInterface("IVROverlay_016", ref evrinitError);
			if (evrinitError != EVRInitError.None)
			{
				SteamVR.ReportError(evrinitError);
				return null;
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			return null;
		}
		return new SteamVR();
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x000528FC File Offset: 0x00050AFC
	private static void ReportError(EVRInitError error)
	{
		if (error <= EVRInitError.Init_VRClientDLLNotFound)
		{
			if (error == EVRInitError.None)
			{
				return;
			}
			if (error == EVRInitError.Init_VRClientDLLNotFound)
			{
				Debug.Log("SteamVR drivers not found!  They can be installed via Steam under Library > Tools.  Visit http://steampowered.com to install Steam.");
				return;
			}
		}
		else
		{
			if (error == EVRInitError.Driver_RuntimeOutOfDate)
			{
				Debug.Log("SteamVR Initialization Failed!  Make sure device's runtime is up to date.");
				return;
			}
			if (error == EVRInitError.VendorSpecific_UnableToConnectToOculusRuntime)
			{
				Debug.Log("SteamVR Initialization Failed!  Make sure device is on, Oculus runtime is installed, and OVRService_*.exe is running.");
				return;
			}
		}
		Debug.Log(OpenVR.GetStringForHmdError(error));
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00052954 File Offset: 0x00050B54
	// (set) Token: 0x06000D0F RID: 3343 RVA: 0x0005295C File Offset: 0x00050B5C
	public CVRSystem hmd { get; private set; }

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00052965 File Offset: 0x00050B65
	// (set) Token: 0x06000D11 RID: 3345 RVA: 0x0005296D File Offset: 0x00050B6D
	public CVRCompositor compositor { get; private set; }

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00052976 File Offset: 0x00050B76
	// (set) Token: 0x06000D13 RID: 3347 RVA: 0x0005297E File Offset: 0x00050B7E
	public CVROverlay overlay { get; private set; }

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00052987 File Offset: 0x00050B87
	// (set) Token: 0x06000D15 RID: 3349 RVA: 0x0005298E File Offset: 0x00050B8E
	public static bool initializing { get; private set; }

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00052996 File Offset: 0x00050B96
	// (set) Token: 0x06000D17 RID: 3351 RVA: 0x0005299D File Offset: 0x00050B9D
	public static bool calibrating { get; private set; }

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x06000D18 RID: 3352 RVA: 0x000529A5 File Offset: 0x00050BA5
	// (set) Token: 0x06000D19 RID: 3353 RVA: 0x000529AC File Offset: 0x00050BAC
	public static bool outOfRange { get; private set; }

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x06000D1A RID: 3354 RVA: 0x000529B4 File Offset: 0x00050BB4
	// (set) Token: 0x06000D1B RID: 3355 RVA: 0x000529BC File Offset: 0x00050BBC
	public float sceneWidth { get; private set; }

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x06000D1C RID: 3356 RVA: 0x000529C5 File Offset: 0x00050BC5
	// (set) Token: 0x06000D1D RID: 3357 RVA: 0x000529CD File Offset: 0x00050BCD
	public float sceneHeight { get; private set; }

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x06000D1E RID: 3358 RVA: 0x000529D6 File Offset: 0x00050BD6
	// (set) Token: 0x06000D1F RID: 3359 RVA: 0x000529DE File Offset: 0x00050BDE
	public float aspect { get; private set; }

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x06000D20 RID: 3360 RVA: 0x000529E7 File Offset: 0x00050BE7
	// (set) Token: 0x06000D21 RID: 3361 RVA: 0x000529EF File Offset: 0x00050BEF
	public float fieldOfView { get; private set; }

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x06000D22 RID: 3362 RVA: 0x000529F8 File Offset: 0x00050BF8
	// (set) Token: 0x06000D23 RID: 3363 RVA: 0x00052A00 File Offset: 0x00050C00
	public Vector2 tanHalfFov { get; private set; }

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00052A09 File Offset: 0x00050C09
	// (set) Token: 0x06000D25 RID: 3365 RVA: 0x00052A11 File Offset: 0x00050C11
	public VRTextureBounds_t[] textureBounds { get; private set; }

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00052A1A File Offset: 0x00050C1A
	// (set) Token: 0x06000D27 RID: 3367 RVA: 0x00052A22 File Offset: 0x00050C22
	public SteamVR_Utils.RigidTransform[] eyes { get; private set; }

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00052A2B File Offset: 0x00050C2B
	public string hmd_TrackingSystemName
	{
		get
		{
			return this.GetStringProperty(ETrackedDeviceProperty.Prop_TrackingSystemName_String, 0U);
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00052A39 File Offset: 0x00050C39
	public string hmd_ModelNumber
	{
		get
		{
			return this.GetStringProperty(ETrackedDeviceProperty.Prop_ModelNumber_String, 0U);
		}
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00052A47 File Offset: 0x00050C47
	public string hmd_SerialNumber
	{
		get
		{
			return this.GetStringProperty(ETrackedDeviceProperty.Prop_SerialNumber_String, 0U);
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00052A55 File Offset: 0x00050C55
	public float hmd_SecondsFromVsyncToPhotons
	{
		get
		{
			return this.GetFloatProperty(ETrackedDeviceProperty.Prop_SecondsFromVsyncToPhotons_Float, 0U);
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00052A63 File Offset: 0x00050C63
	public float hmd_DisplayFrequency
	{
		get
		{
			return this.GetFloatProperty(ETrackedDeviceProperty.Prop_DisplayFrequency_Float, 0U);
		}
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x00052A74 File Offset: 0x00050C74
	public string GetTrackedDeviceString(uint deviceId)
	{
		ETrackedPropertyError etrackedPropertyError = ETrackedPropertyError.TrackedProp_Success;
		uint stringTrackedDeviceProperty = this.hmd.GetStringTrackedDeviceProperty(deviceId, ETrackedDeviceProperty.Prop_AttachedDeviceId_String, null, 0U, ref etrackedPropertyError);
		if (stringTrackedDeviceProperty > 1U)
		{
			StringBuilder stringBuilder = new StringBuilder((int)stringTrackedDeviceProperty);
			this.hmd.GetStringTrackedDeviceProperty(deviceId, ETrackedDeviceProperty.Prop_AttachedDeviceId_String, stringBuilder, stringTrackedDeviceProperty, ref etrackedPropertyError);
			return stringBuilder.ToString();
		}
		return null;
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x00052AC4 File Offset: 0x00050CC4
	public string GetStringProperty(ETrackedDeviceProperty prop, uint deviceId = 0U)
	{
		ETrackedPropertyError etrackedPropertyError = ETrackedPropertyError.TrackedProp_Success;
		uint stringTrackedDeviceProperty = this.hmd.GetStringTrackedDeviceProperty(deviceId, prop, null, 0U, ref etrackedPropertyError);
		if (stringTrackedDeviceProperty > 1U)
		{
			StringBuilder stringBuilder = new StringBuilder((int)stringTrackedDeviceProperty);
			this.hmd.GetStringTrackedDeviceProperty(deviceId, prop, stringBuilder, stringTrackedDeviceProperty, ref etrackedPropertyError);
			return stringBuilder.ToString();
		}
		if (etrackedPropertyError == ETrackedPropertyError.TrackedProp_Success)
		{
			return "<unknown>";
		}
		return etrackedPropertyError.ToString();
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x00052B20 File Offset: 0x00050D20
	public float GetFloatProperty(ETrackedDeviceProperty prop, uint deviceId = 0U)
	{
		ETrackedPropertyError etrackedPropertyError = ETrackedPropertyError.TrackedProp_Success;
		return this.hmd.GetFloatTrackedDeviceProperty(deviceId, prop, ref etrackedPropertyError);
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x00052B3E File Offset: 0x00050D3E
	private void OnInitializing(bool initializing)
	{
		SteamVR.initializing = initializing;
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x00052B46 File Offset: 0x00050D46
	private void OnCalibrating(bool calibrating)
	{
		SteamVR.calibrating = calibrating;
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x00052B4E File Offset: 0x00050D4E
	private void OnOutOfRange(bool outOfRange)
	{
		SteamVR.outOfRange = outOfRange;
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x00052B56 File Offset: 0x00050D56
	private void OnDeviceConnected(int i, bool connected)
	{
		SteamVR.connected[i] = connected;
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x00052B60 File Offset: 0x00050D60
	private void OnNewPoses(TrackedDevicePose_t[] poses)
	{
		this.eyes[0] = new SteamVR_Utils.RigidTransform(this.hmd.GetEyeToHeadTransform(EVREye.Eye_Left));
		this.eyes[1] = new SteamVR_Utils.RigidTransform(this.hmd.GetEyeToHeadTransform(EVREye.Eye_Right));
		for (int i = 0; i < poses.Length; i++)
		{
			bool bDeviceIsConnected = poses[i].bDeviceIsConnected;
			if (bDeviceIsConnected != SteamVR.connected[i])
			{
				SteamVR_Events.DeviceConnected.Send(i, bDeviceIsConnected);
			}
		}
		if ((long)poses.Length > 0L)
		{
			ETrackingResult eTrackingResult = poses[0].eTrackingResult;
			bool flag = eTrackingResult == ETrackingResult.Uninitialized;
			if (flag != SteamVR.initializing)
			{
				SteamVR_Events.Initializing.Send(flag);
			}
			bool flag2 = eTrackingResult == ETrackingResult.Calibrating_InProgress || eTrackingResult == ETrackingResult.Calibrating_OutOfRange;
			if (flag2 != SteamVR.calibrating)
			{
				SteamVR_Events.Calibrating.Send(flag2);
			}
			bool flag3 = eTrackingResult == ETrackingResult.Running_OutOfRange || eTrackingResult == ETrackingResult.Calibrating_OutOfRange;
			if (flag3 != SteamVR.outOfRange)
			{
				SteamVR_Events.OutOfRange.Send(flag3);
			}
		}
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x00052C50 File Offset: 0x00050E50
	private SteamVR()
	{
		this.hmd = OpenVR.System;
		Debug.Log("Connected to " + this.hmd_TrackingSystemName + ":" + this.hmd_SerialNumber);
		this.compositor = OpenVR.Compositor;
		this.overlay = OpenVR.Overlay;
		uint num = 0U;
		uint num2 = 0U;
		this.hmd.GetRecommendedRenderTargetSize(ref num, ref num2);
		this.sceneWidth = num;
		this.sceneHeight = num2;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		this.hmd.GetProjectionRaw(EVREye.Eye_Left, ref num3, ref num4, ref num5, ref num6);
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		this.hmd.GetProjectionRaw(EVREye.Eye_Right, ref num7, ref num8, ref num9, ref num10);
		this.tanHalfFov = new Vector2(Mathf.Max(new float[]
		{
			-num3,
			num4,
			-num7,
			num8
		}), Mathf.Max(new float[]
		{
			-num5,
			num6,
			-num9,
			num10
		}));
		this.textureBounds = new VRTextureBounds_t[2];
		this.textureBounds[0].uMin = 0.5f + 0.5f * num3 / this.tanHalfFov.x;
		this.textureBounds[0].uMax = 0.5f + 0.5f * num4 / this.tanHalfFov.x;
		this.textureBounds[0].vMin = 0.5f - 0.5f * num6 / this.tanHalfFov.y;
		this.textureBounds[0].vMax = 0.5f - 0.5f * num5 / this.tanHalfFov.y;
		this.textureBounds[1].uMin = 0.5f + 0.5f * num7 / this.tanHalfFov.x;
		this.textureBounds[1].uMax = 0.5f + 0.5f * num8 / this.tanHalfFov.x;
		this.textureBounds[1].vMin = 0.5f - 0.5f * num10 / this.tanHalfFov.y;
		this.textureBounds[1].vMax = 0.5f - 0.5f * num9 / this.tanHalfFov.y;
		this.sceneWidth /= Mathf.Max(this.textureBounds[0].uMax - this.textureBounds[0].uMin, this.textureBounds[1].uMax - this.textureBounds[1].uMin);
		this.sceneHeight /= Mathf.Max(this.textureBounds[0].vMax - this.textureBounds[0].vMin, this.textureBounds[1].vMax - this.textureBounds[1].vMin);
		this.aspect = this.tanHalfFov.x / this.tanHalfFov.y;
		this.fieldOfView = 2f * Mathf.Atan(this.tanHalfFov.y) * 57.29578f;
		this.eyes = new SteamVR_Utils.RigidTransform[]
		{
			new SteamVR_Utils.RigidTransform(this.hmd.GetEyeToHeadTransform(EVREye.Eye_Left)),
			new SteamVR_Utils.RigidTransform(this.hmd.GetEyeToHeadTransform(EVREye.Eye_Right))
		};
		GraphicsDeviceType graphicsDeviceType = SystemInfo.graphicsDeviceType;
		if (graphicsDeviceType <= GraphicsDeviceType.OpenGLES3)
		{
			if (graphicsDeviceType != GraphicsDeviceType.OpenGLES2 && graphicsDeviceType != GraphicsDeviceType.OpenGLES3)
			{
				goto IL_3DC;
			}
		}
		else if (graphicsDeviceType != GraphicsDeviceType.OpenGLCore)
		{
			if (graphicsDeviceType != GraphicsDeviceType.Vulkan)
			{
				goto IL_3DC;
			}
			this.textureType = ETextureType.Vulkan;
			goto IL_3E3;
		}
		this.textureType = ETextureType.OpenGL;
		goto IL_3E3;
		IL_3DC:
		this.textureType = ETextureType.DirectX;
		IL_3E3:
		SteamVR_Events.Initializing.Listen(new UnityAction<bool>(this.OnInitializing));
		SteamVR_Events.Calibrating.Listen(new UnityAction<bool>(this.OnCalibrating));
		SteamVR_Events.OutOfRange.Listen(new UnityAction<bool>(this.OnOutOfRange));
		SteamVR_Events.DeviceConnected.Listen(new UnityAction<int, bool>(this.OnDeviceConnected));
		SteamVR_Events.NewPoses.Listen(new UnityAction<TrackedDevicePose_t[]>(this.OnNewPoses));
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x000530B0 File Offset: 0x000512B0
	~SteamVR()
	{
		this.Dispose(false);
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x000530E0 File Offset: 0x000512E0
	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x000530F0 File Offset: 0x000512F0
	private void Dispose(bool disposing)
	{
		SteamVR_Events.Initializing.Remove(new UnityAction<bool>(this.OnInitializing));
		SteamVR_Events.Calibrating.Remove(new UnityAction<bool>(this.OnCalibrating));
		SteamVR_Events.OutOfRange.Remove(new UnityAction<bool>(this.OnOutOfRange));
		SteamVR_Events.DeviceConnected.Remove(new UnityAction<int, bool>(this.OnDeviceConnected));
		SteamVR_Events.NewPoses.Remove(new UnityAction<TrackedDevicePose_t[]>(this.OnNewPoses));
		SteamVR._instance = null;
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x00053171 File Offset: 0x00051371
	public static void SafeDispose()
	{
		if (SteamVR._instance != null)
		{
			SteamVR._instance.Dispose();
		}
	}

	// Token: 0x04000D9D RID: 3485
	private static bool _enabled = true;

	// Token: 0x04000D9E RID: 3486
	private static SteamVR _instance;

	// Token: 0x04000DA5 RID: 3493
	public static bool[] connected = new bool[16];

	// Token: 0x04000DAD RID: 3501
	public ETextureType textureType;
}
