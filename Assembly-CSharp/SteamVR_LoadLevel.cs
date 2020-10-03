using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

// Token: 0x020001EB RID: 491
public class SteamVR_LoadLevel : MonoBehaviour
{
	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00055B37 File Offset: 0x00053D37
	public static bool loading
	{
		get
		{
			return SteamVR_LoadLevel._active != null;
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00055B44 File Offset: 0x00053D44
	public static float progress
	{
		get
		{
			if (!(SteamVR_LoadLevel._active != null) || SteamVR_LoadLevel._active.async == null)
			{
				return 0f;
			}
			return SteamVR_LoadLevel._active.async.progress;
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00055B74 File Offset: 0x00053D74
	public static Texture progressTexture
	{
		get
		{
			if (!(SteamVR_LoadLevel._active != null))
			{
				return null;
			}
			return SteamVR_LoadLevel._active.renderTexture;
		}
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x00055B8F File Offset: 0x00053D8F
	private void OnEnable()
	{
		if (this.autoTriggerOnEnable)
		{
			this.Trigger();
		}
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x00055B9F File Offset: 0x00053D9F
	public void Trigger()
	{
		if (!SteamVR_LoadLevel.loading && !string.IsNullOrEmpty(this.levelName))
		{
			base.StartCoroutine(this.LoadLevel());
		}
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x00055BC2 File Offset: 0x00053DC2
	public static void Begin(string levelName, bool showGrid = false, float fadeOutTime = 0.5f, float r = 0f, float g = 0f, float b = 0f, float a = 1f)
	{
		SteamVR_LoadLevel steamVR_LoadLevel = new GameObject("loader").AddComponent<SteamVR_LoadLevel>();
		steamVR_LoadLevel.levelName = levelName;
		steamVR_LoadLevel.showGrid = showGrid;
		steamVR_LoadLevel.fadeOutTime = fadeOutTime;
		steamVR_LoadLevel.backgroundColor = new Color(r, g, b, a);
		steamVR_LoadLevel.Trigger();
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x00055C00 File Offset: 0x00053E00
	private void OnGUI()
	{
		if (SteamVR_LoadLevel._active != this)
		{
			return;
		}
		if (this.progressBarEmpty != null && this.progressBarFull != null)
		{
			if (this.progressBarOverlayHandle == 0UL)
			{
				this.progressBarOverlayHandle = this.GetOverlayHandle("progressBar", (this.progressBarTransform != null) ? this.progressBarTransform : base.transform, this.progressBarWidthInMeters);
			}
			if (this.progressBarOverlayHandle != 0UL)
			{
				float num = (this.async != null) ? this.async.progress : 0f;
				int width = this.progressBarFull.width;
				int height = this.progressBarFull.height;
				if (this.renderTexture == null)
				{
					this.renderTexture = new RenderTexture(width, height, 0);
					this.renderTexture.Create();
				}
				RenderTexture active = RenderTexture.active;
				RenderTexture.active = this.renderTexture;
				if (Event.current.type == EventType.Repaint)
				{
					GL.Clear(false, true, Color.clear);
				}
				GUILayout.BeginArea(new Rect(0f, 0f, (float)width, (float)height));
				GUI.DrawTexture(new Rect(0f, 0f, (float)width, (float)height), this.progressBarEmpty);
				GUI.DrawTextureWithTexCoords(new Rect(0f, 0f, num * (float)width, (float)height), this.progressBarFull, new Rect(0f, 0f, num, 1f));
				GUILayout.EndArea();
				RenderTexture.active = active;
				CVROverlay overlay = OpenVR.Overlay;
				if (overlay != null)
				{
					Texture_t texture_t = default(Texture_t);
					texture_t.handle = this.renderTexture.GetNativeTexturePtr();
					texture_t.eType = SteamVR.instance.textureType;
					texture_t.eColorSpace = EColorSpace.Auto;
					overlay.SetOverlayTexture(this.progressBarOverlayHandle, ref texture_t);
				}
			}
		}
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x00055DC8 File Offset: 0x00053FC8
	private void Update()
	{
		if (SteamVR_LoadLevel._active != this)
		{
			return;
		}
		this.alpha = Mathf.Clamp01(this.alpha + this.fadeRate * Time.deltaTime);
		CVROverlay overlay = OpenVR.Overlay;
		if (overlay != null)
		{
			if (this.loadingScreenOverlayHandle != 0UL)
			{
				overlay.SetOverlayAlpha(this.loadingScreenOverlayHandle, this.alpha);
			}
			if (this.progressBarOverlayHandle != 0UL)
			{
				overlay.SetOverlayAlpha(this.progressBarOverlayHandle, this.alpha);
			}
		}
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x00055E40 File Offset: 0x00054040
	private IEnumerator LoadLevel()
	{
		if (this.loadingScreen != null && this.loadingScreenDistance > 0f)
		{
			SteamVR_Controller.Device hmd = SteamVR_Controller.Input(0);
			while (!hmd.hasTracking)
			{
				yield return null;
			}
			SteamVR_Utils.RigidTransform transform = hmd.transform;
			transform.rot = Quaternion.Euler(0f, transform.rot.eulerAngles.y, 0f);
			transform.pos += transform.rot * new Vector3(0f, 0f, this.loadingScreenDistance);
			Transform transform2 = (this.loadingScreenTransform != null) ? this.loadingScreenTransform : base.transform;
			transform2.position = transform.pos;
			transform2.rotation = transform.rot;
			hmd = null;
		}
		SteamVR_LoadLevel._active = this;
		SteamVR_Events.Loading.Send(true);
		if (this.loadingScreenFadeInTime > 0f)
		{
			this.fadeRate = 1f / this.loadingScreenFadeInTime;
		}
		else
		{
			this.alpha = 1f;
		}
		CVROverlay overlay = OpenVR.Overlay;
		if (this.loadingScreen != null && overlay != null)
		{
			this.loadingScreenOverlayHandle = this.GetOverlayHandle("loadingScreen", (this.loadingScreenTransform != null) ? this.loadingScreenTransform : base.transform, this.loadingScreenWidthInMeters);
			if (this.loadingScreenOverlayHandle != 0UL)
			{
				Texture_t texture_t = default(Texture_t);
				texture_t.handle = this.loadingScreen.GetNativeTexturePtr();
				texture_t.eType = SteamVR.instance.textureType;
				texture_t.eColorSpace = EColorSpace.Auto;
				overlay.SetOverlayTexture(this.loadingScreenOverlayHandle, ref texture_t);
			}
		}
		bool fadedForeground = false;
		SteamVR_Events.LoadingFadeOut.Send(this.fadeOutTime);
		CVRCompositor compositor = OpenVR.Compositor;
		if (compositor != null)
		{
			if (this.front != null)
			{
				SteamVR_Skybox.SetOverride(this.front, this.back, this.left, this.right, this.top, this.bottom);
				compositor.FadeGrid(this.fadeOutTime, true);
				yield return new WaitForSeconds(this.fadeOutTime);
			}
			else if (this.backgroundColor != Color.clear)
			{
				if (this.showGrid)
				{
					compositor.FadeToColor(0f, this.backgroundColor.r, this.backgroundColor.g, this.backgroundColor.b, this.backgroundColor.a, true);
					compositor.FadeGrid(this.fadeOutTime, true);
					yield return new WaitForSeconds(this.fadeOutTime);
				}
				else
				{
					compositor.FadeToColor(this.fadeOutTime, this.backgroundColor.r, this.backgroundColor.g, this.backgroundColor.b, this.backgroundColor.a, false);
					yield return new WaitForSeconds(this.fadeOutTime + 0.1f);
					compositor.FadeGrid(0f, true);
					fadedForeground = true;
				}
			}
		}
		SteamVR_Render.pauseRendering = true;
		while (this.alpha < 1f)
		{
			yield return null;
		}
		base.transform.parent = null;
		Object.DontDestroyOnLoad(base.gameObject);
		if (!string.IsNullOrEmpty(this.internalProcessPath))
		{
			Debug.Log("Launching external application...");
			CVRApplications applications = OpenVR.Applications;
			if (applications == null)
			{
				Debug.Log("Failed to get OpenVR.Applications interface!");
			}
			else
			{
				string currentDirectory = Directory.GetCurrentDirectory();
				string text = Path.Combine(currentDirectory, this.internalProcessPath);
				Debug.Log("LaunchingInternalProcess");
				Debug.Log("ExternalAppPath = " + this.internalProcessPath);
				Debug.Log("FullPath = " + text);
				Debug.Log("ExternalAppArgs = " + this.internalProcessArgs);
				Debug.Log("WorkingDirectory = " + currentDirectory);
				EVRApplicationError evrapplicationError = applications.LaunchInternalProcess(text, this.internalProcessArgs, currentDirectory);
				Debug.Log("LaunchInternalProcessError: " + evrapplicationError);
				Process.GetCurrentProcess().Kill();
			}
		}
		else
		{
			LoadSceneMode mode = this.loadAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
			if (this.loadAsync)
			{
				Application.backgroundLoadingPriority = ThreadPriority.Low;
				this.async = SceneManager.LoadSceneAsync(this.levelName, mode);
				if (MainManager.instance)
				{
					if (MainManager.instance.localPlayer)
					{
						MainManager.instance.localPlayer.cam.cullingMask = MainManager.instance.localPlayer.noLayersMask;
					}
				}
				else if (GameController.instance && GameController.instance.myPlayer != null && GameController.instance.myPlayer.player != null)
				{
					GameController.instance.myPlayer.player.cam.cullingMask = GameController.instance.myPlayer.player.noLayersMask;
				}
				yield return new WaitUntil(() => this.async.isDone);
			}
			else
			{
				SceneManager.LoadScene(this.levelName, mode);
			}
		}
		yield return null;
		yield return null;
		yield return new WaitForSeconds(this.postLoadSettleTime);
		SteamVR_Render.pauseRendering = false;
		if (this.loadingScreenFadeOutTime > 0f)
		{
			this.fadeRate = -1f / this.loadingScreenFadeOutTime;
		}
		else
		{
			this.alpha = 0f;
		}
		SteamVR_Events.LoadingFadeIn.Send(this.fadeInTime);
		if (compositor != null)
		{
			if (fadedForeground)
			{
				compositor.FadeGrid(0f, false);
				compositor.FadeToColor(this.fadeInTime, 0f, 0f, 0f, 0f, false);
				yield return new WaitForSeconds(this.fadeInTime);
			}
			else
			{
				compositor.FadeGrid(this.fadeInTime, false);
				yield return new WaitForSeconds(this.fadeInTime);
				if (this.front != null)
				{
					SteamVR_Skybox.ClearOverride();
				}
			}
		}
		while (this.alpha > 0f)
		{
			yield return null;
		}
		if (overlay != null)
		{
			if (this.progressBarOverlayHandle != 0UL)
			{
				overlay.HideOverlay(this.progressBarOverlayHandle);
			}
			if (this.loadingScreenOverlayHandle != 0UL)
			{
				overlay.HideOverlay(this.loadingScreenOverlayHandle);
			}
		}
		Object.Destroy(base.gameObject);
		SteamVR_LoadLevel._active = null;
		SteamVR_Events.Loading.Send(false);
		yield break;
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x00055E50 File Offset: 0x00054050
	private ulong GetOverlayHandle(string overlayName, Transform transform, float widthInMeters = 1f)
	{
		ulong num = 0UL;
		CVROverlay overlay = OpenVR.Overlay;
		if (overlay == null)
		{
			return num;
		}
		string pchOverlayKey = SteamVR_Overlay.key + "." + overlayName;
		EVROverlayError evroverlayError = overlay.FindOverlay(pchOverlayKey, ref num);
		if (evroverlayError != EVROverlayError.None)
		{
			evroverlayError = overlay.CreateOverlay(pchOverlayKey, overlayName, ref num);
		}
		if (evroverlayError == EVROverlayError.None)
		{
			overlay.ShowOverlay(num);
			overlay.SetOverlayAlpha(num, this.alpha);
			overlay.SetOverlayWidthInMeters(num, widthInMeters);
			if (SteamVR.instance.textureType == ETextureType.DirectX)
			{
				VRTextureBounds_t vrtextureBounds_t = default(VRTextureBounds_t);
				vrtextureBounds_t.uMin = 0f;
				vrtextureBounds_t.vMin = 1f;
				vrtextureBounds_t.uMax = 1f;
				vrtextureBounds_t.vMax = 0f;
				overlay.SetOverlayTextureBounds(num, ref vrtextureBounds_t);
			}
			SteamVR_Camera steamVR_Camera = (this.loadingScreenDistance == 0f) ? SteamVR_Render.Top() : null;
			if (steamVR_Camera != null && steamVR_Camera.origin != null)
			{
				SteamVR_Utils.RigidTransform rigidTransform = new SteamVR_Utils.RigidTransform(steamVR_Camera.origin, transform);
				rigidTransform.pos.x = rigidTransform.pos.x / steamVR_Camera.origin.localScale.x;
				rigidTransform.pos.y = rigidTransform.pos.y / steamVR_Camera.origin.localScale.y;
				rigidTransform.pos.z = rigidTransform.pos.z / steamVR_Camera.origin.localScale.z;
				HmdMatrix34_t hmdMatrix34_t = rigidTransform.ToHmdMatrix34();
				overlay.SetOverlayTransformAbsolute(num, SteamVR_Render.instance.trackingSpace, ref hmdMatrix34_t);
			}
			else
			{
				HmdMatrix34_t hmdMatrix34_t2 = new SteamVR_Utils.RigidTransform(transform).ToHmdMatrix34();
				overlay.SetOverlayTransformAbsolute(num, SteamVR_Render.instance.trackingSpace, ref hmdMatrix34_t2);
			}
		}
		return num;
	}

	// Token: 0x04000DFB RID: 3579
	private static SteamVR_LoadLevel _active;

	// Token: 0x04000DFC RID: 3580
	public string levelName;

	// Token: 0x04000DFD RID: 3581
	public string internalProcessPath;

	// Token: 0x04000DFE RID: 3582
	public string internalProcessArgs;

	// Token: 0x04000DFF RID: 3583
	public bool loadAdditive;

	// Token: 0x04000E00 RID: 3584
	public bool loadAsync = true;

	// Token: 0x04000E01 RID: 3585
	public Texture loadingScreen;

	// Token: 0x04000E02 RID: 3586
	public Texture progressBarEmpty;

	// Token: 0x04000E03 RID: 3587
	public Texture progressBarFull;

	// Token: 0x04000E04 RID: 3588
	public float loadingScreenWidthInMeters = 6f;

	// Token: 0x04000E05 RID: 3589
	public float progressBarWidthInMeters = 3f;

	// Token: 0x04000E06 RID: 3590
	public float loadingScreenDistance;

	// Token: 0x04000E07 RID: 3591
	public Transform loadingScreenTransform;

	// Token: 0x04000E08 RID: 3592
	public Transform progressBarTransform;

	// Token: 0x04000E09 RID: 3593
	public Texture front;

	// Token: 0x04000E0A RID: 3594
	public Texture back;

	// Token: 0x04000E0B RID: 3595
	public Texture left;

	// Token: 0x04000E0C RID: 3596
	public Texture right;

	// Token: 0x04000E0D RID: 3597
	public Texture top;

	// Token: 0x04000E0E RID: 3598
	public Texture bottom;

	// Token: 0x04000E0F RID: 3599
	public Color backgroundColor = Color.black;

	// Token: 0x04000E10 RID: 3600
	public bool showGrid;

	// Token: 0x04000E11 RID: 3601
	public float fadeOutTime = 0.5f;

	// Token: 0x04000E12 RID: 3602
	public float fadeInTime = 0.5f;

	// Token: 0x04000E13 RID: 3603
	public float postLoadSettleTime;

	// Token: 0x04000E14 RID: 3604
	public float loadingScreenFadeInTime = 1f;

	// Token: 0x04000E15 RID: 3605
	public float loadingScreenFadeOutTime = 0.25f;

	// Token: 0x04000E16 RID: 3606
	private float fadeRate = 1f;

	// Token: 0x04000E17 RID: 3607
	private float alpha;

	// Token: 0x04000E18 RID: 3608
	private AsyncOperation async;

	// Token: 0x04000E19 RID: 3609
	private RenderTexture renderTexture;

	// Token: 0x04000E1A RID: 3610
	private ulong loadingScreenOverlayHandle;

	// Token: 0x04000E1B RID: 3611
	private ulong progressBarOverlayHandle;

	// Token: 0x04000E1C RID: 3612
	public bool autoTriggerOnEnable;
}
