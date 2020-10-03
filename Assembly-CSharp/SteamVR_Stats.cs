using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

// Token: 0x020001F3 RID: 499
public class SteamVR_Stats : MonoBehaviour
{
	// Token: 0x06000DF8 RID: 3576 RVA: 0x00058E30 File Offset: 0x00057030
	private void Awake()
	{
		if (this.text == null)
		{
			this.text = base.GetComponent<Text>();
			this.text.enabled = false;
		}
		if (this.fadeDuration > 0f)
		{
			SteamVR_Fade.Start(this.fadeColor, 0f, false);
			SteamVR_Fade.Start(Color.clear, this.fadeDuration, false);
		}
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x00058E94 File Offset: 0x00057094
	private void Update()
	{
		if (this.text != null)
		{
			if (Input.GetKeyDown(KeyCode.I))
			{
				this.text.enabled = !this.text.enabled;
			}
			if (this.text.enabled)
			{
				CVRCompositor compositor = OpenVR.Compositor;
				if (compositor != null)
				{
					Compositor_FrameTiming compositor_FrameTiming = default(Compositor_FrameTiming);
					compositor_FrameTiming.m_nSize = (uint)Marshal.SizeOf(typeof(Compositor_FrameTiming));
					compositor.GetFrameTiming(ref compositor_FrameTiming, 0U);
					double flSystemTimeInSeconds = compositor_FrameTiming.m_flSystemTimeInSeconds;
					if (flSystemTimeInSeconds > this.lastUpdate)
					{
						double num = (this.lastUpdate > 0.0) ? (1.0 / (flSystemTimeInSeconds - this.lastUpdate)) : 0.0;
						this.lastUpdate = flSystemTimeInSeconds;
						this.text.text = string.Format("framerate: {0:N0}\ndropped frames: {1}", num, (int)compositor_FrameTiming.m_nNumDroppedFrames);
						return;
					}
					this.lastUpdate = flSystemTimeInSeconds;
				}
			}
		}
	}

	// Token: 0x04000E6A RID: 3690
	public Text text;

	// Token: 0x04000E6B RID: 3691
	public Color fadeColor = Color.black;

	// Token: 0x04000E6C RID: 3692
	public float fadeDuration = 1f;

	// Token: 0x04000E6D RID: 3693
	private double lastUpdate;
}
