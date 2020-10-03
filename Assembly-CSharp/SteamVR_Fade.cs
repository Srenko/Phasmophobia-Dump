using System;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

// Token: 0x020001E7 RID: 487
public class SteamVR_Fade : MonoBehaviour
{
	// Token: 0x06000D81 RID: 3457 RVA: 0x00054D51 File Offset: 0x00052F51
	public static void Start(Color newColor, float duration, bool fadeOverlay = false)
	{
		SteamVR_Events.Fade.Send(newColor, duration, fadeOverlay);
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x00054D60 File Offset: 0x00052F60
	public static void View(Color newColor, float duration)
	{
		CVRCompositor compositor = OpenVR.Compositor;
		if (compositor != null)
		{
			compositor.FadeToColor(duration, newColor.r, newColor.g, newColor.b, newColor.a, false);
		}
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x00054D96 File Offset: 0x00052F96
	public void OnStartFade(Color newColor, float duration, bool fadeOverlay)
	{
		if (duration > 0f)
		{
			this.targetColor = newColor;
			this.deltaColor = (this.targetColor - this.currentColor) / duration;
			return;
		}
		this.currentColor = newColor;
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x00054DCC File Offset: 0x00052FCC
	private void OnEnable()
	{
		if (SteamVR_Fade.fadeMaterial == null)
		{
			SteamVR_Fade.fadeMaterial = new Material(Shader.Find("Custom/SteamVR_Fade"));
			SteamVR_Fade.fadeMaterialColorID = Shader.PropertyToID("fadeColor");
		}
		SteamVR_Events.Fade.Listen(new UnityAction<Color, float, bool>(this.OnStartFade));
		SteamVR_Events.FadeReady.Send();
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x00054E29 File Offset: 0x00053029
	private void OnDisable()
	{
		SteamVR_Events.Fade.Remove(new UnityAction<Color, float, bool>(this.OnStartFade));
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x00054E44 File Offset: 0x00053044
	private void OnPostRender()
	{
		if (this.currentColor != this.targetColor)
		{
			if (Mathf.Abs(this.currentColor.a - this.targetColor.a) < Mathf.Abs(this.deltaColor.a) * Time.deltaTime)
			{
				this.currentColor = this.targetColor;
				this.deltaColor = new Color(0f, 0f, 0f, 0f);
			}
			else
			{
				this.currentColor += this.deltaColor * Time.deltaTime;
			}
			if (this.fadeOverlay)
			{
				SteamVR_Overlay instance = SteamVR_Overlay.instance;
				if (instance != null)
				{
					instance.alpha = 1f - this.currentColor.a;
				}
			}
		}
		if (this.currentColor.a > 0f && SteamVR_Fade.fadeMaterial)
		{
			SteamVR_Fade.fadeMaterial.SetColor(SteamVR_Fade.fadeMaterialColorID, this.currentColor);
			SteamVR_Fade.fadeMaterial.SetPass(0);
			GL.Begin(7);
			GL.Vertex3(-1f, -1f, 0f);
			GL.Vertex3(1f, -1f, 0f);
			GL.Vertex3(1f, 1f, 0f);
			GL.Vertex3(-1f, 1f, 0f);
			GL.End();
		}
	}

	// Token: 0x04000DE4 RID: 3556
	private Color currentColor = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04000DE5 RID: 3557
	private Color targetColor = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04000DE6 RID: 3558
	private Color deltaColor = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04000DE7 RID: 3559
	private bool fadeOverlay;

	// Token: 0x04000DE8 RID: 3560
	private static Material fadeMaterial = null;

	// Token: 0x04000DE9 RID: 3561
	private static int fadeMaterialColorID = -1;
}
