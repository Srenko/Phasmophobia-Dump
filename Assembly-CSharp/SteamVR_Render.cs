using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

// Token: 0x020001EF RID: 495
public class SteamVR_Render : MonoBehaviour
{
	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00057656 File Offset: 0x00055856
	// (set) Token: 0x06000DBE RID: 3518 RVA: 0x0005765D File Offset: 0x0005585D
	public static EVREye eye { get; private set; }

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00057665 File Offset: 0x00055865
	public static SteamVR_Render instance
	{
		get
		{
			if (SteamVR_Render._instance == null)
			{
				SteamVR_Render._instance = Object.FindObjectOfType<SteamVR_Render>();
				if (SteamVR_Render._instance == null)
				{
					SteamVR_Render._instance = new GameObject("[SteamVR]").AddComponent<SteamVR_Render>();
				}
			}
			return SteamVR_Render._instance;
		}
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x000576A4 File Offset: 0x000558A4
	private void OnDestroy()
	{
		SteamVR_Render._instance = null;
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x000576AC File Offset: 0x000558AC
	private void OnApplicationQuit()
	{
		SteamVR_Render.isQuitting = true;
		SteamVR.SafeDispose();
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x000576B9 File Offset: 0x000558B9
	public static void Add(SteamVR_Camera vrcam)
	{
		if (!SteamVR_Render.isQuitting)
		{
			SteamVR_Render.instance.AddInternal(vrcam);
		}
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x000576CD File Offset: 0x000558CD
	public static void Remove(SteamVR_Camera vrcam)
	{
		if (!SteamVR_Render.isQuitting && SteamVR_Render._instance != null)
		{
			SteamVR_Render.instance.RemoveInternal(vrcam);
		}
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x000576EE File Offset: 0x000558EE
	public static SteamVR_Camera Top()
	{
		if (!SteamVR_Render.isQuitting)
		{
			return SteamVR_Render.instance.TopInternal();
		}
		return null;
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x00057704 File Offset: 0x00055904
	private void AddInternal(SteamVR_Camera vrcam)
	{
		Camera component = vrcam.GetComponent<Camera>();
		int num = this.cameras.Length;
		SteamVR_Camera[] array = new SteamVR_Camera[num + 1];
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			Camera component2 = this.cameras[i].GetComponent<Camera>();
			if (i == num2 && component2.depth > component.depth)
			{
				array[num2++] = vrcam;
			}
			array[num2++] = this.cameras[i];
		}
		if (num2 == num)
		{
			array[num2] = vrcam;
		}
		this.cameras = array;
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x00057788 File Offset: 0x00055988
	private void RemoveInternal(SteamVR_Camera vrcam)
	{
		int num = this.cameras.Length;
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			if (this.cameras[i] == vrcam)
			{
				num2++;
			}
		}
		if (num2 == 0)
		{
			return;
		}
		SteamVR_Camera[] array = new SteamVR_Camera[num - num2];
		int num3 = 0;
		for (int j = 0; j < num; j++)
		{
			SteamVR_Camera steamVR_Camera = this.cameras[j];
			if (steamVR_Camera != vrcam)
			{
				array[num3++] = steamVR_Camera;
			}
		}
		this.cameras = array;
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x00057809 File Offset: 0x00055A09
	private SteamVR_Camera TopInternal()
	{
		if (this.cameras.Length != 0)
		{
			return this.cameras[this.cameras.Length - 1];
		}
		return null;
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00057827 File Offset: 0x00055A27
	// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x00057830 File Offset: 0x00055A30
	public static bool pauseRendering
	{
		get
		{
			return SteamVR_Render._pauseRendering;
		}
		set
		{
			SteamVR_Render._pauseRendering = value;
			CVRCompositor compositor = OpenVR.Compositor;
			if (compositor != null)
			{
				compositor.SuspendRendering(value);
			}
		}
	}

	// Token: 0x06000DCA RID: 3530 RVA: 0x00057853 File Offset: 0x00055A53
	private IEnumerator RenderLoop()
	{
		while (Application.isPlaying)
		{
			yield return this.waitForEndOfFrame;
			if (!SteamVR_Render.pauseRendering)
			{
				CVRCompositor compositor = OpenVR.Compositor;
				if (compositor != null)
				{
					if (!compositor.CanRenderScene())
					{
						continue;
					}
					compositor.SetTrackingSpace(this.trackingSpace);
				}
				SteamVR_Overlay instance = SteamVR_Overlay.instance;
				if (instance != null)
				{
					instance.UpdateOverlay();
				}
				this.RenderExternalCamera();
			}
		}
		yield break;
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x00057864 File Offset: 0x00055A64
	private void RenderExternalCamera()
	{
		if (this.externalCamera == null)
		{
			return;
		}
		if (!this.externalCamera.gameObject.activeInHierarchy)
		{
			return;
		}
		int num = (int)Mathf.Max(this.externalCamera.config.frameSkip, 0f);
		if (Time.frameCount % (num + 1) != 0)
		{
			return;
		}
		this.externalCamera.AttachToCamera(this.TopInternal());
		this.externalCamera.RenderNear();
		this.externalCamera.RenderFar();
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x000578E4 File Offset: 0x00055AE4
	private void OnInputFocus(bool hasFocus)
	{
		if (hasFocus)
		{
			if (this.pauseGameWhenDashboardIsVisible)
			{
				Time.timeScale = this.timeScale;
			}
			SteamVR_Camera.sceneResolutionScale = this.sceneResolutionScale;
			return;
		}
		if (this.pauseGameWhenDashboardIsVisible)
		{
			this.timeScale = Time.timeScale;
			Time.timeScale = 0f;
		}
		this.sceneResolutionScale = SteamVR_Camera.sceneResolutionScale;
		SteamVR_Camera.sceneResolutionScale = 0.5f;
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x0004170B File Offset: 0x0003F90B
	private void OnQuit(VREvent_t vrEvent)
	{
		Application.Quit();
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x00057948 File Offset: 0x00055B48
	private string GetScreenshotFilename(uint screenshotHandle, EVRScreenshotPropertyFilenames screenshotPropertyFilename)
	{
		EVRScreenshotError evrscreenshotError = EVRScreenshotError.None;
		uint screenshotPropertyFilename2 = OpenVR.Screenshots.GetScreenshotPropertyFilename(screenshotHandle, screenshotPropertyFilename, null, 0U, ref evrscreenshotError);
		if (evrscreenshotError != EVRScreenshotError.None && evrscreenshotError != EVRScreenshotError.BufferTooSmall)
		{
			return null;
		}
		if (screenshotPropertyFilename2 <= 1U)
		{
			return null;
		}
		StringBuilder stringBuilder = new StringBuilder((int)screenshotPropertyFilename2);
		OpenVR.Screenshots.GetScreenshotPropertyFilename(screenshotHandle, screenshotPropertyFilename, stringBuilder, screenshotPropertyFilename2, ref evrscreenshotError);
		if (evrscreenshotError != EVRScreenshotError.None)
		{
			return null;
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x0005799C File Offset: 0x00055B9C
	private void OnRequestScreenshot(VREvent_t vrEvent)
	{
		uint handle = vrEvent.data.screenshot.handle;
		EVRScreenshotType type = (EVRScreenshotType)vrEvent.data.screenshot.type;
		if (type == EVRScreenshotType.StereoPanorama)
		{
			string screenshotFilename = this.GetScreenshotFilename(handle, EVRScreenshotPropertyFilenames.Preview);
			string screenshotFilename2 = this.GetScreenshotFilename(handle, EVRScreenshotPropertyFilenames.VR);
			if (screenshotFilename == null || screenshotFilename2 == null)
			{
				return;
			}
			SteamVR_Utils.TakeStereoScreenshot(handle, new GameObject("screenshotPosition")
			{
				transform = 
				{
					position = SteamVR_Render.Top().transform.position,
					rotation = SteamVR_Render.Top().transform.rotation,
					localScale = SteamVR_Render.Top().transform.lossyScale
				}
			}, 32, 0.064f, ref screenshotFilename, ref screenshotFilename2);
			OpenVR.Screenshots.SubmitScreenshot(handle, type, screenshotFilename, screenshotFilename2);
		}
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x00057A6C File Offset: 0x00055C6C
	private void OnEnable()
	{
		base.StartCoroutine(this.RenderLoop());
		SteamVR_Events.InputFocus.Listen(new UnityAction<bool>(this.OnInputFocus));
		SteamVR_Events.System(EVREventType.VREvent_Quit).Listen(new UnityAction<VREvent_t>(this.OnQuit));
		SteamVR_Events.System(EVREventType.VREvent_RequestScreenshot).Listen(new UnityAction<VREvent_t>(this.OnRequestScreenshot));
		Application.onBeforeRender += this.OnBeforeRender;
		if (SteamVR.instance == null)
		{
			base.enabled = false;
			return;
		}
		EVRScreenshotType[] pSupportedTypes = new EVRScreenshotType[]
		{
			EVRScreenshotType.StereoPanorama
		};
		OpenVR.Screenshots.HookScreenshot(pSupportedTypes);
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x00057B0C File Offset: 0x00055D0C
	private void OnDisable()
	{
		base.StopAllCoroutines();
		SteamVR_Events.InputFocus.Remove(new UnityAction<bool>(this.OnInputFocus));
		SteamVR_Events.System(EVREventType.VREvent_Quit).Remove(new UnityAction<VREvent_t>(this.OnQuit));
		SteamVR_Events.System(EVREventType.VREvent_RequestScreenshot).Remove(new UnityAction<VREvent_t>(this.OnRequestScreenshot));
		Application.onBeforeRender -= this.OnBeforeRender;
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x00057B7C File Offset: 0x00055D7C
	private void Awake()
	{
		if (this.externalCamera == null && File.Exists(this.externalCameraConfigPath))
		{
			GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("SteamVR_ExternalCamera"));
			gameObject.gameObject.name = "External Camera";
			this.externalCamera = gameObject.transform.GetChild(0).GetComponent<SteamVR_ExternalCamera>();
			this.externalCamera.configPath = this.externalCameraConfigPath;
			this.externalCamera.ReadConfig();
		}
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x00057BF8 File Offset: 0x00055DF8
	public void UpdatePoses()
	{
		CVRCompositor compositor = OpenVR.Compositor;
		if (compositor != null)
		{
			compositor.GetLastPoses(this.poses, this.gamePoses);
			SteamVR_Events.NewPoses.Send(this.poses);
			SteamVR_Events.NewPosesApplied.Send();
		}
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x00057C3B File Offset: 0x00055E3B
	private void OnBeforeRender()
	{
		this.UpdatePoses();
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x00057C44 File Offset: 0x00055E44
	private void Update()
	{
		SteamVR_Controller.Update();
		CVRSystem system = OpenVR.System;
		if (system != null)
		{
			VREvent_t vrevent_t = default(VREvent_t);
			uint uncbVREvent = (uint)Marshal.SizeOf(typeof(VREvent_t));
			int num = 0;
			while (num < 64 && system.PollNextEvent(ref vrevent_t, uncbVREvent))
			{
				EVREventType eventType = (EVREventType)vrevent_t.eventType;
				if (eventType <= EVREventType.VREvent_InputFocusReleased)
				{
					if (eventType != EVREventType.VREvent_InputFocusCaptured)
					{
						if (eventType != EVREventType.VREvent_InputFocusReleased)
						{
							goto IL_CF;
						}
						if (vrevent_t.data.process.pid == 0U)
						{
							SteamVR_Events.InputFocus.Send(true);
						}
					}
					else if (vrevent_t.data.process.oldPid == 0U)
					{
						SteamVR_Events.InputFocus.Send(false);
					}
				}
				else if (eventType != EVREventType.VREvent_HideRenderModels)
				{
					if (eventType != EVREventType.VREvent_ShowRenderModels)
					{
						goto IL_CF;
					}
					SteamVR_Events.HideRenderModels.Send(false);
				}
				else
				{
					SteamVR_Events.HideRenderModels.Send(true);
				}
				IL_E0:
				num++;
				continue;
				IL_CF:
				SteamVR_Events.System((EVREventType)vrevent_t.eventType).Send(vrevent_t);
				goto IL_E0;
			}
		}
		Application.targetFrameRate = -1;
		Application.runInBackground = true;
		QualitySettings.maxQueuedFrames = -1;
		QualitySettings.vSyncCount = 0;
		if (this.lockPhysicsUpdateRateToRenderFrequency && Time.timeScale > 0f)
		{
			SteamVR instance = SteamVR.instance;
			if (instance != null)
			{
				Compositor_FrameTiming compositor_FrameTiming = default(Compositor_FrameTiming);
				compositor_FrameTiming.m_nSize = (uint)Marshal.SizeOf(typeof(Compositor_FrameTiming));
				instance.compositor.GetFrameTiming(ref compositor_FrameTiming, 0U);
				Time.fixedDeltaTime = Time.timeScale / instance.hmd_DisplayFrequency;
			}
		}
	}

	// Token: 0x04000E42 RID: 3650
	public bool pauseGameWhenDashboardIsVisible;

	// Token: 0x04000E43 RID: 3651
	public bool lockPhysicsUpdateRateToRenderFrequency = true;

	// Token: 0x04000E44 RID: 3652
	public SteamVR_ExternalCamera externalCamera;

	// Token: 0x04000E45 RID: 3653
	public string externalCameraConfigPath = "externalcamera.cfg";

	// Token: 0x04000E46 RID: 3654
	public ETrackingUniverseOrigin trackingSpace = ETrackingUniverseOrigin.TrackingUniverseStanding;

	// Token: 0x04000E48 RID: 3656
	private static SteamVR_Render _instance;

	// Token: 0x04000E49 RID: 3657
	private static bool isQuitting;

	// Token: 0x04000E4A RID: 3658
	private SteamVR_Camera[] cameras = new SteamVR_Camera[0];

	// Token: 0x04000E4B RID: 3659
	public TrackedDevicePose_t[] poses = new TrackedDevicePose_t[16];

	// Token: 0x04000E4C RID: 3660
	public TrackedDevicePose_t[] gamePoses = new TrackedDevicePose_t[0];

	// Token: 0x04000E4D RID: 3661
	private static bool _pauseRendering;

	// Token: 0x04000E4E RID: 3662
	private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

	// Token: 0x04000E4F RID: 3663
	private float sceneResolutionScale = 1f;

	// Token: 0x04000E50 RID: 3664
	private float timeScale = 1f;
}
