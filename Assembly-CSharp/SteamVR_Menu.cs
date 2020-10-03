using System;
using UnityEngine;
using Valve.VR;

// Token: 0x020001EC RID: 492
public class SteamVR_Menu : MonoBehaviour
{
	// Token: 0x1700010A RID: 266
	// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00056073 File Offset: 0x00054273
	public RenderTexture texture
	{
		get
		{
			if (!this.overlay)
			{
				return null;
			}
			return this.overlay.texture as RenderTexture;
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00056094 File Offset: 0x00054294
	// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x0005609C File Offset: 0x0005429C
	public float scale { get; private set; }

	// Token: 0x06000DA3 RID: 3491 RVA: 0x000560A8 File Offset: 0x000542A8
	private void Awake()
	{
		this.scaleLimitX = string.Format("{0:N1}", this.scaleLimits.x);
		this.scaleLimitY = string.Format("{0:N1}", this.scaleLimits.y);
		this.scaleRateText = string.Format("{0:N1}", this.scaleRate);
		SteamVR_Overlay instance = SteamVR_Overlay.instance;
		if (instance != null)
		{
			this.uvOffset = instance.uvOffset;
			this.distance = instance.distance;
		}
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x00056138 File Offset: 0x00054338
	private void OnGUI()
	{
		if (this.overlay == null)
		{
			return;
		}
		RenderTexture renderTexture = this.overlay.texture as RenderTexture;
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = renderTexture;
		if (Event.current.type == EventType.Repaint)
		{
			GL.Clear(false, true, Color.clear);
		}
		Rect screenRect = new Rect(0f, 0f, (float)renderTexture.width, (float)renderTexture.height);
		if (Screen.width < renderTexture.width)
		{
			screenRect.width = (float)Screen.width;
			this.overlay.uvOffset.x = -(float)(renderTexture.width - Screen.width) / (float)(2 * renderTexture.width);
		}
		if (Screen.height < renderTexture.height)
		{
			screenRect.height = (float)Screen.height;
			this.overlay.uvOffset.y = (float)(renderTexture.height - Screen.height) / (float)(2 * renderTexture.height);
		}
		GUILayout.BeginArea(screenRect);
		if (this.background != null)
		{
			GUI.DrawTexture(new Rect((screenRect.width - (float)this.background.width) / 2f, (screenRect.height - (float)this.background.height) / 2f, (float)this.background.width, (float)this.background.height), this.background);
		}
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		if (this.logo != null)
		{
			GUILayout.Space(screenRect.height / 2f - this.logoHeight);
			GUILayout.Box(this.logo, Array.Empty<GUILayoutOption>());
		}
		GUILayout.Space(this.menuOffset);
		bool flag = GUILayout.Button("[Esc] - Close menu", Array.Empty<GUILayoutOption>());
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label(string.Format("Scale: {0:N4}", this.scale), Array.Empty<GUILayoutOption>());
		float num = GUILayout.HorizontalSlider(this.scale, this.scaleLimits.x, this.scaleLimits.y, Array.Empty<GUILayoutOption>());
		if (num != this.scale)
		{
			this.SetScale(num);
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label(string.Format("Scale limits:", Array.Empty<object>()), Array.Empty<GUILayoutOption>());
		string text = GUILayout.TextField(this.scaleLimitX, Array.Empty<GUILayoutOption>());
		if (text != this.scaleLimitX && float.TryParse(text, out this.scaleLimits.x))
		{
			this.scaleLimitX = text;
		}
		string text2 = GUILayout.TextField(this.scaleLimitY, Array.Empty<GUILayoutOption>());
		if (text2 != this.scaleLimitY && float.TryParse(text2, out this.scaleLimits.y))
		{
			this.scaleLimitY = text2;
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label(string.Format("Scale rate:", Array.Empty<object>()), Array.Empty<GUILayoutOption>());
		string text3 = GUILayout.TextField(this.scaleRateText, Array.Empty<GUILayoutOption>());
		if (text3 != this.scaleRateText && float.TryParse(text3, out this.scaleRate))
		{
			this.scaleRateText = text3;
		}
		GUILayout.EndHorizontal();
		if (SteamVR.active)
		{
			SteamVR instance = SteamVR.instance;
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			float sceneResolutionScale = SteamVR_Camera.sceneResolutionScale;
			int num2 = (int)(instance.sceneWidth * sceneResolutionScale);
			int num3 = (int)(instance.sceneHeight * sceneResolutionScale);
			int num4 = (int)(100f * sceneResolutionScale);
			GUILayout.Label(string.Format("Scene quality: {0}x{1} ({2}%)", num2, num3, num4), Array.Empty<GUILayoutOption>());
			int num5 = Mathf.RoundToInt(GUILayout.HorizontalSlider((float)num4, 50f, 200f, Array.Empty<GUILayoutOption>()));
			if (num5 != num4)
			{
				SteamVR_Camera.sceneResolutionScale = (float)num5 / 100f;
			}
			GUILayout.EndHorizontal();
		}
		this.overlay.highquality = GUILayout.Toggle(this.overlay.highquality, "High quality", Array.Empty<GUILayoutOption>());
		if (this.overlay.highquality)
		{
			this.overlay.curved = GUILayout.Toggle(this.overlay.curved, "Curved overlay", Array.Empty<GUILayoutOption>());
			this.overlay.antialias = GUILayout.Toggle(this.overlay.antialias, "Overlay RGSS(2x2)", Array.Empty<GUILayoutOption>());
		}
		else
		{
			this.overlay.curved = false;
			this.overlay.antialias = false;
		}
		SteamVR_Camera steamVR_Camera = SteamVR_Render.Top();
		if (steamVR_Camera != null)
		{
			steamVR_Camera.wireframe = GUILayout.Toggle(steamVR_Camera.wireframe, "Wireframe", Array.Empty<GUILayoutOption>());
			SteamVR_Render instance2 = SteamVR_Render.instance;
			if (instance2.trackingSpace == ETrackingUniverseOrigin.TrackingUniverseSeated)
			{
				if (GUILayout.Button("Switch to Standing", Array.Empty<GUILayoutOption>()))
				{
					instance2.trackingSpace = ETrackingUniverseOrigin.TrackingUniverseStanding;
				}
				if (GUILayout.Button("Center View", Array.Empty<GUILayoutOption>()))
				{
					CVRSystem system = OpenVR.System;
					if (system != null)
					{
						system.ResetSeatedZeroPose();
					}
				}
			}
			else if (GUILayout.Button("Switch to Seated", Array.Empty<GUILayoutOption>()))
			{
				instance2.trackingSpace = ETrackingUniverseOrigin.TrackingUniverseSeated;
			}
		}
		if (GUILayout.Button("Exit", Array.Empty<GUILayoutOption>()))
		{
			Application.Quit();
		}
		GUILayout.Space(this.menuOffset);
		string environmentVariable = Environment.GetEnvironmentVariable("VR_OVERRIDE");
		if (environmentVariable != null)
		{
			GUILayout.Label("VR_OVERRIDE=" + environmentVariable, Array.Empty<GUILayoutOption>());
		}
		GUILayout.Label("Graphics device: " + SystemInfo.graphicsDeviceVersion, Array.Empty<GUILayoutOption>());
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		if (this.cursor != null)
		{
			float x = Input.mousePosition.x;
			float y = (float)Screen.height - Input.mousePosition.y;
			float width = (float)this.cursor.width;
			float height = (float)this.cursor.height;
			GUI.DrawTexture(new Rect(x, y, width, height), this.cursor);
		}
		RenderTexture.active = active;
		if (flag)
		{
			this.HideMenu();
		}
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x0005671C File Offset: 0x0005491C
	public void ShowMenu()
	{
		SteamVR_Overlay instance = SteamVR_Overlay.instance;
		if (instance == null)
		{
			return;
		}
		RenderTexture renderTexture = instance.texture as RenderTexture;
		if (renderTexture == null)
		{
			Debug.LogError("Menu requires overlay texture to be a render texture.");
			return;
		}
		this.SaveCursorState();
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		this.overlay = instance;
		this.uvOffset = instance.uvOffset;
		this.distance = instance.distance;
		foreach (Camera camera in Object.FindObjectsOfType(typeof(Camera)) as Camera[])
		{
			if (camera.enabled && camera.targetTexture == renderTexture)
			{
				this.overlayCam = camera;
				this.overlayCam.enabled = false;
				break;
			}
		}
		SteamVR_Camera steamVR_Camera = SteamVR_Render.Top();
		if (steamVR_Camera != null)
		{
			this.scale = steamVR_Camera.origin.localScale.x;
		}
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x0005680C File Offset: 0x00054A0C
	public void HideMenu()
	{
		this.RestoreCursorState();
		if (this.overlayCam != null)
		{
			this.overlayCam.enabled = true;
		}
		if (this.overlay != null)
		{
			this.overlay.uvOffset = this.uvOffset;
			this.overlay.distance = this.distance;
			this.overlay = null;
		}
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x00056870 File Offset: 0x00054A70
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
		{
			if (this.overlay == null)
			{
				this.ShowMenu();
				return;
			}
			this.HideMenu();
			return;
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Home))
			{
				this.SetScale(1f);
				return;
			}
			if (Input.GetKey(KeyCode.PageUp))
			{
				this.SetScale(Mathf.Clamp(this.scale + this.scaleRate * Time.deltaTime, this.scaleLimits.x, this.scaleLimits.y));
				return;
			}
			if (Input.GetKey(KeyCode.PageDown))
			{
				this.SetScale(Mathf.Clamp(this.scale - this.scaleRate * Time.deltaTime, this.scaleLimits.x, this.scaleLimits.y));
			}
			return;
		}
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x00056948 File Offset: 0x00054B48
	private void SetScale(float scale)
	{
		this.scale = scale;
		SteamVR_Camera steamVR_Camera = SteamVR_Render.Top();
		if (steamVR_Camera != null)
		{
			steamVR_Camera.origin.localScale = new Vector3(scale, scale, scale);
		}
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x0005697E File Offset: 0x00054B7E
	private void SaveCursorState()
	{
		this.savedCursorVisible = Cursor.visible;
		this.savedCursorLockState = Cursor.lockState;
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x00056996 File Offset: 0x00054B96
	private void RestoreCursorState()
	{
		Cursor.visible = this.savedCursorVisible;
		Cursor.lockState = this.savedCursorLockState;
	}

	// Token: 0x04000E1D RID: 3613
	public Texture cursor;

	// Token: 0x04000E1E RID: 3614
	public Texture background;

	// Token: 0x04000E1F RID: 3615
	public Texture logo;

	// Token: 0x04000E20 RID: 3616
	public float logoHeight;

	// Token: 0x04000E21 RID: 3617
	public float menuOffset;

	// Token: 0x04000E22 RID: 3618
	public Vector2 scaleLimits = new Vector2(0.1f, 5f);

	// Token: 0x04000E23 RID: 3619
	public float scaleRate = 0.5f;

	// Token: 0x04000E24 RID: 3620
	private SteamVR_Overlay overlay;

	// Token: 0x04000E25 RID: 3621
	private Camera overlayCam;

	// Token: 0x04000E26 RID: 3622
	private Vector4 uvOffset;

	// Token: 0x04000E27 RID: 3623
	private float distance;

	// Token: 0x04000E29 RID: 3625
	private string scaleLimitX;

	// Token: 0x04000E2A RID: 3626
	private string scaleLimitY;

	// Token: 0x04000E2B RID: 3627
	private string scaleRateText;

	// Token: 0x04000E2C RID: 3628
	private CursorLockMode savedCursorLockState;

	// Token: 0x04000E2D RID: 3629
	private bool savedCursorVisible;
}
