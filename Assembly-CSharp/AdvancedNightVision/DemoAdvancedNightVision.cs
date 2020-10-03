using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdvancedNightVision
{
	// Token: 0x02000475 RID: 1141
	public sealed class DemoAdvancedNightVision : MonoBehaviour
	{
		// Token: 0x0600238A RID: 9098 RVA: 0x000AE330 File Offset: 0x000AC530
		private void OnEnable()
		{
			Camera camera = null;
			Camera[] array = Object.FindObjectsOfType<Camera>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].enabled)
				{
					camera = array[i];
					break;
				}
			}
			if (camera != null)
			{
				this.advancedNightVision = camera.gameObject.GetComponent<AdvancedNightVision>();
				if (this.advancedNightVision == null)
				{
					this.advancedNightVision = camera.gameObject.AddComponent<AdvancedNightVision>();
				}
				this.toolbarNightVisionStatus = (this.advancedNightVision.enabled ? 0 : 1);
				this.toolbarLightsStatus = 0;
				this.lights.AddRange(Object.FindObjectsOfType<Light>());
				if (this.musicClip != null)
				{
					AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
					audioSource.clip = this.musicClip;
					audioSource.loop = true;
					audioSource.Play();
					return;
				}
			}
			else
			{
				Debug.LogWarning("No camera found.");
			}
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000AE40C File Offset: 0x000AC60C
		private void Update()
		{
			this.timeleft -= Time.deltaTime;
			this.accum += Time.timeScale / Time.deltaTime;
			this.frames++;
			if (this.timeleft <= 0f)
			{
				this.fps = this.accum / (float)this.frames;
				this.timeleft = this.updateInterval;
				this.accum = 0f;
				this.frames = 0;
			}
			if (Input.GetKeyUp(KeyCode.Tab))
			{
				this.guiShow = !this.guiShow;
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000AE4B8 File Offset: 0x000AC6B8
		private void OnGUI()
		{
			if (this.advancedNightVision == null)
			{
				return;
			}
			if (this.effectNameStyle == null)
			{
				this.effectNameStyle = new GUIStyle(GUI.skin.textArea);
				this.effectNameStyle.alignment = TextAnchor.MiddleCenter;
				this.effectNameStyle.fontSize = 22;
			}
			if (this.menuStyle == null)
			{
				this.menuStyle = new GUIStyle(GUI.skin.textArea);
				this.menuStyle.alignment = TextAnchor.MiddleCenter;
				this.menuStyle.fontSize = 14;
			}
			if (!this.guiShow)
			{
				return;
			}
			GUILayout.BeginHorizontal("box", new GUILayoutOption[]
			{
				GUILayout.Width((float)Screen.width)
			});
			GUILayout.Space(10f);
			if (GUILayout.Button("OPTIONS", this.menuStyle, new GUILayoutOption[]
			{
				GUILayout.Width(80f)
			}))
			{
				this.menuOpen = !this.menuOpen;
			}
			GUILayout.FlexibleSpace();
			GUILayout.Label("ADVANCED NIGHT VISION", this.menuStyle, new GUILayoutOption[]
			{
				GUILayout.Width(200f)
			});
			GUILayout.FlexibleSpace();
			if (this.fps < 30f)
			{
				GUI.contentColor = Color.yellow;
			}
			else if (this.fps < 15f)
			{
				GUI.contentColor = Color.red;
			}
			else
			{
				GUI.contentColor = Color.green;
			}
			GUILayout.Label(this.fps.ToString("000"), this.menuStyle, new GUILayoutOption[]
			{
				GUILayout.Width(40f)
			});
			GUI.contentColor = Color.white;
			GUILayout.Space(10f);
			GUILayout.EndHorizontal();
			if (this.menuOpen)
			{
				GUILayout.BeginVertical("box", new GUILayoutOption[]
				{
					GUILayout.Width(425f)
				});
				this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, Array.Empty<GUILayoutOption>());
				GUILayout.Space(10f);
				GUILayout.BeginVertical("box", Array.Empty<GUILayoutOption>());
				string[] texts = new string[]
				{
					"ON",
					"OFF"
				};
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Label(" Lights", Array.Empty<GUILayoutOption>());
				this.toolbarLightsStatus = GUILayout.Toolbar(this.toolbarLightsStatus, texts, Array.Empty<GUILayoutOption>());
				GUILayout.EndHorizontal();
				this.ChangeLights(this.toolbarLightsStatus == 0);
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Label(" Night Vision", Array.Empty<GUILayoutOption>());
				this.toolbarNightVisionStatus = GUILayout.Toolbar(this.toolbarNightVisionStatus, texts, Array.Empty<GUILayoutOption>());
				GUILayout.EndHorizontal();
				this.ChangeNightVision(this.toolbarNightVisionStatus == 0);
				GUILayout.EndHorizontal();
				GUILayout.BeginVertical("box", Array.Empty<GUILayoutOption>());
				GUILayout.Label("Quality", Array.Empty<GUILayoutOption>());
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Label(" Shader", Array.Empty<GUILayoutOption>());
				string[] texts2 = new string[]
				{
					"Multi-pass",
					"One pass"
				};
				this.toolbarMultipass = GUILayout.Toolbar(this.toolbarMultipass, texts2, Array.Empty<GUILayoutOption>());
				this.advancedNightVision.ShaderPass = ((this.toolbarMultipass == 0) ? AdvancedNightVision.ShaderPasses.MultiPass : AdvancedNightVision.ShaderPasses.OnePass);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Label(" Render", Array.Empty<GUILayoutOption>());
				string[] texts3 = new string[]
				{
					"Normal",
					"Half",
					"Quarter"
				};
				this.toolbarRenderSize = GUILayout.Toolbar(this.toolbarRenderSize, texts3, Array.Empty<GUILayoutOption>());
				switch (this.toolbarRenderSize)
				{
				case 0:
					this.advancedNightVision.RenderTextureResolution = AdvancedNightVision.RenderTextureResolutions.Normal;
					break;
				case 1:
					this.advancedNightVision.RenderTextureResolution = AdvancedNightVision.RenderTextureResolutions.Half;
					break;
				case 2:
					this.advancedNightVision.RenderTextureResolution = AdvancedNightVision.RenderTextureResolutions.Quarter;
					break;
				}
				GUILayout.EndHorizontal();
				if (this.advancedNightVision.ShaderPass == AdvancedNightVision.ShaderPasses.MultiPass)
				{
					this.advancedNightVision.BlurPasses = (float)((int)this.HorizontalSlider(" Blur", this.advancedNightVision.BlurPasses, 0f, 10f));
					this.advancedNightVision.Glow = (float)((int)this.HorizontalSlider(" Glow", this.advancedNightVision.Glow, 0f, 25f));
				}
				GUILayout.EndVertical();
				GUILayout.Space(10f);
				GUILayout.BeginVertical("box", Array.Empty<GUILayoutOption>());
				GUILayout.Label("Color", Array.Empty<GUILayoutOption>());
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Label(" Mode", Array.Empty<GUILayoutOption>());
				string[] texts4 = new string[]
				{
					"Simple",
					"Advanced"
				};
				this.toolbarColorControl = GUILayout.Toolbar(this.toolbarColorControl, texts4, Array.Empty<GUILayoutOption>());
				this.advancedNightVision.ColorControl = (AdvancedNightVision.ColorControls)this.toolbarColorControl;
				GUILayout.EndHorizontal();
				this.advancedNightVision.LuminanceAmount = this.HorizontalSlider(" Amount", this.advancedNightVision.LuminanceAmount, 0f, 1f);
				this.advancedNightVision.Exposure = this.HorizontalSlider(" Exposure", this.advancedNightVision.Exposure, 0f, 100f);
				if (this.advancedNightVision.ColorControl == AdvancedNightVision.ColorControls.Advanced)
				{
					this.advancedNightVision.Brightness = this.HorizontalSlider(" Brightness", this.advancedNightVision.Brightness, -1f, 1f);
					this.advancedNightVision.Contrast = this.HorizontalSlider(" Contrast", this.advancedNightVision.Contrast, -1f, 1f);
					this.advancedNightVision.Saturation = this.HorizontalSlider(" Saturation", this.advancedNightVision.Saturation, 0f, 1f);
					GUILayout.Label(" Offset", new GUILayoutOption[]
					{
						GUILayout.Width(70f)
					});
					Vector3 rgboffset = this.advancedNightVision.RGBOffset;
					rgboffset.x = this.HorizontalSlider("    R", rgboffset.x, -100f, 100f);
					rgboffset.y = this.HorizontalSlider("    G", rgboffset.y, -100f, 100f);
					rgboffset.z = this.HorizontalSlider("    B", rgboffset.z, -100f, 100f);
					this.advancedNightVision.RGBOffset = rgboffset;
				}
				GUILayout.EndVertical();
				GUILayout.Space(10f);
				GUILayout.BeginVertical("box", Array.Empty<GUILayoutOption>());
				GUILayout.Label("Glitches", Array.Empty<GUILayoutOption>());
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Label(" Vignette", Array.Empty<GUILayoutOption>());
				string[] texts5 = new string[]
				{
					"None",
					"Screen",
					"Monocular",
					"Binocular"
				};
				this.toolbarVignette = GUILayout.Toolbar(this.toolbarVignette, texts5, Array.Empty<GUILayoutOption>());
				this.advancedNightVision.VignetteType = (AdvancedNightVision.VignetteTypes)this.toolbarVignette;
				GUILayout.EndHorizontal();
				if (this.advancedNightVision.VignetteType != AdvancedNightVision.VignetteTypes.None)
				{
					this.advancedNightVision.VignetteScale = this.HorizontalSlider("    Scale", this.advancedNightVision.VignetteScale, 0f, 100f);
					this.advancedNightVision.VignetteSoftness = this.HorizontalSlider("    Softness", this.advancedNightVision.VignetteSoftness, 0f, 10f);
				}
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Label(" Distortion", Array.Empty<GUILayoutOption>());
				string[] texts6 = new string[]
				{
					"None",
					"Simple",
					"Advanced"
				};
				this.toolbarDistortion = GUILayout.Toolbar(this.toolbarDistortion, texts6, Array.Empty<GUILayoutOption>());
				this.advancedNightVision.ChromaticAberrationType = (AdvancedNightVision.ChromaticAberrationTypes)this.toolbarDistortion;
				GUILayout.EndHorizontal();
				if (this.advancedNightVision.ShaderPass != AdvancedNightVision.ShaderPasses.OnePass && this.advancedNightVision.ChromaticAberrationType != AdvancedNightVision.ChromaticAberrationTypes.None)
				{
					this.advancedNightVision.ChromaticAberration = this.HorizontalSlider("    Aberration", this.advancedNightVision.ChromaticAberration, -100f, 100f);
					if (this.advancedNightVision.ChromaticAberrationType == AdvancedNightVision.ChromaticAberrationTypes.Advanced)
					{
						this.advancedNightVision.DistortionsPasses = (int)this.HorizontalSlider("    Passes", (float)this.advancedNightVision.DistortionsPasses, 3f, 24f);
						this.advancedNightVision.BarrelDistortion = (float)((int)this.HorizontalSlider("    Barrel", this.advancedNightVision.BarrelDistortion, -100f, 100f));
					}
				}
				this.advancedNightVision.Noise = this.HorizontalSlider(" Noise", this.advancedNightVision.Noise, 0f, 10f);
				this.advancedNightVision.Scanline = this.HorizontalSlider(" Scanlines", this.advancedNightVision.Scanline, 0f, 2f);
				GUILayout.EndVertical();
				GUILayout.Space(10f);
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				if (GUILayout.Button("Reset values", Array.Empty<GUILayoutOption>()))
				{
					this.advancedNightVision.ResetDefaultValues();
				}
				if (GUILayout.Button("Open Web", Array.Empty<GUILayoutOption>()))
				{
					Application.OpenURL("http://www.ibuprogames.com/2016/04/27/advanced-night-vision/");
				}
				if (GUILayout.Button("Quit", Array.Empty<GUILayoutOption>()))
				{
					Application.Quit();
				}
				GUILayout.EndHorizontal();
				GUILayout.BeginVertical("box", Array.Empty<GUILayoutOption>());
				GUILayout.Label("TAB - Hide/Show gui.", Array.Empty<GUILayoutOption>());
				GUILayout.FlexibleSpace();
				GUILayout.EndVertical();
				GUILayout.Space(10f);
				GUILayout.EndScrollView();
				GUILayout.EndVertical();
			}
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000AEE24 File Offset: 0x000AD024
		private float HorizontalSlider(string label, float val, float min, float max)
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(label, new GUILayoutOption[]
			{
				GUILayout.Width(90f)
			});
			val = GUILayout.HorizontalSlider(val, min, max, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			return val;
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000AEE60 File Offset: 0x000AD060
		private void ChangeLights(bool on)
		{
			for (int i = 0; i < this.lights.Count; i++)
			{
				this.lights[i].intensity = (on ? this.lightsIntensity : 0f);
			}
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000AEEA4 File Offset: 0x000AD0A4
		private void ChangeNightVision(bool on)
		{
			this.advancedNightVision.enabled = on;
			if (this.nightVisionLight != null)
			{
				this.nightVisionLight.enabled = on;
				this.nightVisionLight.intensity = this.lightsIntensity;
			}
		}

		// Token: 0x040020DA RID: 8410
		public bool guiShow = true;

		// Token: 0x040020DB RID: 8411
		public AudioClip musicClip;

		// Token: 0x040020DC RID: 8412
		private AdvancedNightVision advancedNightVision;

		// Token: 0x040020DD RID: 8413
		private bool menuOpen;

		// Token: 0x040020DE RID: 8414
		private const float guiMargen = 10f;

		// Token: 0x040020DF RID: 8415
		private const float guiWidth = 425f;

		// Token: 0x040020E0 RID: 8416
		private float updateInterval = 0.5f;

		// Token: 0x040020E1 RID: 8417
		private float accum;

		// Token: 0x040020E2 RID: 8418
		private int frames;

		// Token: 0x040020E3 RID: 8419
		private float timeleft;

		// Token: 0x040020E4 RID: 8420
		private float fps;

		// Token: 0x040020E5 RID: 8421
		private GUIStyle effectNameStyle;

		// Token: 0x040020E6 RID: 8422
		private GUIStyle menuStyle;

		// Token: 0x040020E7 RID: 8423
		private int toolbarMultipass;

		// Token: 0x040020E8 RID: 8424
		private int toolbarRenderSize;

		// Token: 0x040020E9 RID: 8425
		private int toolbarColorControl = 1;

		// Token: 0x040020EA RID: 8426
		private int toolbarVignette = 1;

		// Token: 0x040020EB RID: 8427
		private int toolbarDistortion = 2;

		// Token: 0x040020EC RID: 8428
		private Vector2 scrollPosition = Vector2.zero;

		// Token: 0x040020ED RID: 8429
		public Light nightVisionLight;

		// Token: 0x040020EE RID: 8430
		public float lightsIntensity = 1f;

		// Token: 0x040020EF RID: 8431
		private int toolbarLightsStatus;

		// Token: 0x040020F0 RID: 8432
		private int toolbarNightVisionStatus;

		// Token: 0x040020F1 RID: 8433
		private List<Light> lights = new List<Light>();
	}
}
