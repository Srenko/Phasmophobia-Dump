using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

namespace VRTK
{
	// Token: 0x02000310 RID: 784
	[AddComponentMenu("VRTK/Scripts/Utilities/VRTK_AdaptiveQuality")]
	public sealed class VRTK_AdaptiveQuality : MonoBehaviour
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x0008FB84 File Offset: 0x0008DD84
		public static float CurrentRenderScale
		{
			get
			{
				return XRSettings.eyeTextureResolutionScale * XRSettings.renderViewportScale;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x0008FB91 File Offset: 0x0008DD91
		public Vector2 defaultRenderTargetResolution
		{
			get
			{
				return VRTK_AdaptiveQuality.RenderTargetResolutionForRenderScale(this.allRenderScales[this.defaultRenderViewportScaleLevel]);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0008FBA9 File Offset: 0x0008DDA9
		public Vector2 currentRenderTargetResolution
		{
			get
			{
				return VRTK_AdaptiveQuality.RenderTargetResolutionForRenderScale(VRTK_AdaptiveQuality.CurrentRenderScale);
			}
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0008FBB8 File Offset: 0x0008DDB8
		public VRTK_AdaptiveQuality()
		{
			this.renderScales = this.allRenderScales.AsReadOnly();
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0008FC5A File Offset: 0x0008DE5A
		public static Vector2 RenderTargetResolutionForRenderScale(float renderScale)
		{
			return new Vector2((float)((int)((float)XRSettings.eyeTextureWidth / XRSettings.eyeTextureResolutionScale * renderScale)), (float)((int)((float)XRSettings.eyeTextureHeight / XRSettings.eyeTextureResolutionScale * renderScale)));
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0008FC84 File Offset: 0x0008DE84
		public float BiggestAllowedMaximumRenderScale()
		{
			if (XRSettings.eyeTextureWidth == 0 || XRSettings.eyeTextureHeight == 0)
			{
				return this.maximumRenderScale;
			}
			float a = (float)this.maximumRenderTargetDimension * XRSettings.eyeTextureResolutionScale / (float)XRSettings.eyeTextureWidth;
			float b = (float)this.maximumRenderTargetDimension * XRSettings.eyeTextureResolutionScale / (float)XRSettings.eyeTextureHeight;
			return Mathf.Min(a, b);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0008FCD8 File Offset: 0x0008DED8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Adaptive Quality\n");
			stringBuilder.AppendLine("Render Scale:");
			stringBuilder.AppendLine("Level - Resolution - Multiplier");
			for (int i = 0; i < this.allRenderScales.Count; i++)
			{
				float num = this.allRenderScales[i];
				Vector2 vector = VRTK_AdaptiveQuality.RenderTargetResolutionForRenderScale(num);
				stringBuilder.AppendFormat("{0, 3} {1, 5}x{2, -5} {3, -8}", new object[]
				{
					i,
					(int)vector.x,
					(int)vector.y,
					num
				});
				if (i == 0)
				{
					stringBuilder.Append(" (Interleaved reprojection hint)");
				}
				else if (i == this.defaultRenderViewportScaleLevel)
				{
					stringBuilder.Append(" (Default)");
				}
				if (i == this.renderViewportScaleSetting.currentValue)
				{
					stringBuilder.Append(" (Current Viewport)");
				}
				if (i == this.renderScaleSetting.currentValue)
				{
					stringBuilder.Append(" (Current Target Resolution)");
				}
				if (i != this.allRenderScales.Count - 1)
				{
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00064607 File Offset: 0x00062807
		private void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x0008FDF4 File Offset: 0x0008DFF4
		private void OnEnable()
		{
			Camera.onPreCull = (Camera.CameraCallback)Delegate.Combine(Camera.onPreCull, new Camera.CameraCallback(this.OnCameraPreCull));
			this.hmdDisplayIsOnDesktop = VRTK_SDK_Bridge.IsDisplayOnDesktop();
			this.singleFrameDurationInMilliseconds = ((XRDevice.refreshRate > 0f) ? (1000f / XRDevice.refreshRate) : 11.1111107f);
			this.HandleCommandLineArguments();
			if (!Application.isEditor)
			{
				this.OnValidate();
			}
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x0008FE63 File Offset: 0x0008E063
		private void OnDisable()
		{
			Camera.onPreCull = (Camera.CameraCallback)Delegate.Remove(Camera.onPreCull, new Camera.CameraCallback(this.OnCameraPreCull));
			VRTK_AdaptiveQuality.SetRenderScale(1f, 1f);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00063DD5 File Offset: 0x00061FD5
		private void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0008FE94 File Offset: 0x0008E094
		private void OnValidate()
		{
			this.minimumRenderScale = Mathf.Max(0.01f, this.minimumRenderScale);
			this.maximumRenderScale = Mathf.Max(this.minimumRenderScale, this.maximumRenderScale);
			this.maximumRenderTargetDimension = Mathf.Max(2, this.maximumRenderTargetDimension);
			this.renderScaleFillRateStepSizeInPercent = Mathf.Max(1, this.renderScaleFillRateStepSizeInPercent);
			this.msaaLevel = ((this.msaaLevel == 1) ? 0 : Mathf.Clamp(Mathf.ClosestPowerOfTwo(this.msaaLevel), 0, 8));
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0008FF16 File Offset: 0x0008E116
		private void Update()
		{
			this.HandleKeyPresses();
			this.UpdateRenderScaleLevels();
			this.CreateOrDestroyDebugVisualization();
			this.UpdateDebugVisualization();
			this.timing.SaveCurrentFrameTiming();
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0008FF3B File Offset: 0x0008E13B
		private void LateUpdate()
		{
			this.UpdateRenderScale();
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x0008FF43 File Offset: 0x0008E143
		private void OnCameraPreCull(Camera camera)
		{
			if (camera.transform != VRTK_SDK_Bridge.GetHeadsetCamera())
			{
				return;
			}
			this.UpdateMSAALevel();
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0008FF60 File Offset: 0x0008E160
		private void HandleCommandLineArguments()
		{
			if (!this.allowCommandLineArguments)
			{
				return;
			}
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 0; i < commandLineArgs.Length; i++)
			{
				string text = commandLineArgs[i];
				string s = (i + 1 < commandLineArgs.Length) ? commandLineArgs[i + 1] : "";
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1942250862U)
				{
					if (num <= 127231785U)
					{
						if (num != 14630367U)
						{
							if (num == 127231785U)
							{
								if (text == "-noaq")
								{
									this.scaleRenderViewport = false;
								}
							}
						}
						else if (text == "-vrdebug")
						{
							this.drawDebugVisualization = true;
						}
					}
					else if (num != 1760075434U)
					{
						if (num == 1942250862U)
						{
							if (text == "-aqmaxres")
							{
								this.maximumRenderTargetDimension = int.Parse(s);
							}
						}
					}
					else if (text == "-msaa")
					{
						this.msaaLevel = int.Parse(s);
					}
				}
				else if (num <= 3221740608U)
				{
					if (num != 2176489346U)
					{
						if (num == 3221740608U)
						{
							if (text == "-aqminscale")
							{
								this.minimumRenderScale = float.Parse(s);
							}
						}
					}
					else if (text == "-aqmaxscale")
					{
						this.maximumRenderScale = float.Parse(s);
					}
				}
				else if (num != 3699776195U)
				{
					if (num == 4205078942U)
					{
						if (text == "-aqoverride")
						{
							this.overrideRenderViewportScale = true;
							this.overrideRenderViewportScaleLevel = int.Parse(s);
						}
					}
				}
				else if (text == "-aqfillratestep")
				{
					this.renderScaleFillRateStepSizeInPercent = int.Parse(s);
				}
			}
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00090124 File Offset: 0x0008E324
		private void HandleKeyPresses()
		{
			if (!this.allowKeyboardShortcuts || !VRTK_AdaptiveQuality.KeyboardShortcuts.Modifiers.Any(new Func<KeyCode, bool>(Input.GetKey)))
			{
				return;
			}
			if (Input.GetKeyDown(KeyCode.F1))
			{
				this.drawDebugVisualization = !this.drawDebugVisualization;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F2))
			{
				this.overrideRenderViewportScale = !this.overrideRenderViewportScale;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F3))
			{
				this.overrideRenderViewportScaleLevel = this.ClampRenderScaleLevel(this.overrideRenderViewportScaleLevel - 1);
				return;
			}
			if (Input.GetKeyDown(KeyCode.F4))
			{
				this.overrideRenderViewportScaleLevel = this.ClampRenderScaleLevel(this.overrideRenderViewportScaleLevel + 1);
			}
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x000901CB File Offset: 0x0008E3CB
		private void UpdateMSAALevel()
		{
			if (QualitySettings.antiAliasing != this.msaaLevel)
			{
				QualitySettings.antiAliasing = this.msaaLevel;
			}
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x000901E8 File Offset: 0x0008E3E8
		private void UpdateRenderScaleLevels()
		{
			if (Mathf.Abs(this.previousMinimumRenderScale - this.minimumRenderScale) <= 1.401298E-45f && Mathf.Abs(this.previousMaximumRenderScale - this.maximumRenderScale) <= 1.401298E-45f && Mathf.Abs(this.previousRenderScaleFillRateStepSizeInPercent - (float)this.renderScaleFillRateStepSizeInPercent) <= 1.401298E-45f)
			{
				return;
			}
			this.previousMinimumRenderScale = this.minimumRenderScale;
			this.previousMaximumRenderScale = this.maximumRenderScale;
			this.previousRenderScaleFillRateStepSizeInPercent = (float)this.renderScaleFillRateStepSizeInPercent;
			this.allRenderScales.Clear();
			float num = this.BiggestAllowedMaximumRenderScale();
			float num2 = Mathf.Min(this.minimumRenderScale, num);
			this.allRenderScales.Add(num2);
			while (num2 <= this.maximumRenderScale)
			{
				this.allRenderScales.Add(num2);
				num2 = Mathf.Sqrt((float)(this.renderScaleFillRateStepSizeInPercent + 100) / 100f * num2 * num2);
				if (num2 > num)
				{
					break;
				}
			}
			this.defaultRenderViewportScaleLevel = Mathf.Clamp(this.allRenderScales.FindIndex((float renderScale) => renderScale >= 1f), 1, this.allRenderScales.Count - 1);
			this.renderViewportScaleSetting.currentValue = this.defaultRenderViewportScaleLevel;
			this.renderScaleSetting.currentValue = this.defaultRenderViewportScaleLevel;
			this.overrideRenderViewportScaleLevel = this.ClampRenderScaleLevel(this.overrideRenderViewportScaleLevel);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00090340 File Offset: 0x0008E540
		private void UpdateRenderScale()
		{
			if (this.allRenderScales.Count == 0)
			{
				return;
			}
			if (!this.scaleRenderViewport)
			{
				this.renderViewportScaleSetting.currentValue = this.defaultRenderViewportScaleLevel;
				this.renderScaleSetting.currentValue = this.defaultRenderViewportScaleLevel;
				VRTK_AdaptiveQuality.SetRenderScale(1f, 1f);
				return;
			}
			float num = VRTK_SDK_Bridge.ShouldAppRenderWithLowResources() ? (this.singleFrameDurationInMilliseconds * 0.75f) : this.singleFrameDurationInMilliseconds;
			float thresholdInMilliseconds = 0.7f * num;
			float extrapolationThresholdInMilliseconds = 0.85f * num;
			float thresholdInMilliseconds2 = 0.9f * num;
			int num2 = this.renderViewportScaleSetting.currentValue;
			if (this.timing.WasFrameTimingBad(1, thresholdInMilliseconds2, this.renderViewportScaleSetting.lastChangeFrameCount, this.renderViewportScaleSetting.decreaseFrameCost) || this.timing.WasFrameTimingBad(3, thresholdInMilliseconds2, this.renderViewportScaleSetting.lastChangeFrameCount, this.renderViewportScaleSetting.decreaseFrameCost) || this.timing.WillFrameTimingBeBad(extrapolationThresholdInMilliseconds, thresholdInMilliseconds2, this.renderViewportScaleSetting.lastChangeFrameCount, this.renderViewportScaleSetting.decreaseFrameCost))
			{
				num2 = this.ClampRenderScaleLevel((this.renderViewportScaleSetting.currentValue == 2) ? 1 : (this.renderViewportScaleSetting.currentValue - 2));
			}
			else if (this.timing.WasFrameTimingGood(12, thresholdInMilliseconds, this.renderViewportScaleSetting.lastChangeFrameCount - this.renderViewportScaleSetting.increaseFrameCost, this.renderViewportScaleSetting.increaseFrameCost))
			{
				num2 = this.ClampRenderScaleLevel(this.renderViewportScaleSetting.currentValue + 2);
			}
			else if (this.timing.WasFrameTimingGood(6, thresholdInMilliseconds, this.renderViewportScaleSetting.lastChangeFrameCount, this.renderViewportScaleSetting.increaseFrameCost))
			{
				num2 = this.ClampRenderScaleLevel(this.renderViewportScaleSetting.currentValue + 1);
			}
			if (num2 != this.renderViewportScaleSetting.currentValue)
			{
				if (this.renderViewportScaleSetting.currentValue >= this.renderScaleSetting.currentValue && num2 < this.renderScaleSetting.currentValue)
				{
					this.lastRenderViewportScaleLevelBelowRenderScaleLevelFrameCount = Time.frameCount;
				}
				this.renderViewportScaleSetting.currentValue = num2;
			}
			if (this.overrideRenderViewportScale)
			{
				this.renderViewportScaleSetting.currentValue = this.overrideRenderViewportScaleLevel;
			}
			float num3 = 1f;
			if (!this.hmdDisplayIsOnDesktop)
			{
				if (this.renderViewportScaleSetting.currentValue == 0)
				{
					if (this.interleavedReprojectionEnabled && this.timing.GetFrameTiming(0) < this.singleFrameDurationInMilliseconds * 0.85f)
					{
						this.interleavedReprojectionEnabled = false;
					}
					else if (this.timing.GetFrameTiming(0) > this.singleFrameDurationInMilliseconds * 0.925f)
					{
						this.interleavedReprojectionEnabled = true;
					}
				}
				else
				{
					this.interleavedReprojectionEnabled = false;
				}
				VRTK_SDK_Bridge.ForceInterleavedReprojectionOn(this.interleavedReprojectionEnabled);
			}
			else if (this.renderViewportScaleSetting.currentValue == 0)
			{
				num3 = 0.8f;
			}
			int currentValue = this.renderScaleSetting.currentValue;
			int b = (this.renderViewportScaleSetting.currentValue - this.renderScaleSetting.currentValue) / 2;
			if (this.renderScaleSetting.currentValue < this.renderViewportScaleSetting.currentValue && Time.frameCount >= this.renderScaleSetting.lastChangeFrameCount + this.renderScaleSetting.increaseFrameCost)
			{
				currentValue = this.ClampRenderScaleLevel(this.renderScaleSetting.currentValue + Mathf.Max(1, b));
			}
			else if (this.renderScaleSetting.currentValue > this.renderViewportScaleSetting.currentValue && Time.frameCount >= this.renderScaleSetting.lastChangeFrameCount + this.renderScaleSetting.decreaseFrameCost && Time.frameCount >= this.lastRenderViewportScaleLevelBelowRenderScaleLevelFrameCount + this.renderViewportScaleSetting.increaseFrameCost)
			{
				currentValue = (this.timing.WasFrameTimingGood(6, thresholdInMilliseconds, 0, 0) ? this.ClampRenderScaleLevel(this.renderScaleSetting.currentValue + Mathf.Min(-1, b)) : this.renderViewportScaleSetting.currentValue);
			}
			this.renderScaleSetting.currentValue = currentValue;
			if (!this.scaleRenderTargetResolution)
			{
				this.renderScaleSetting.currentValue = this.allRenderScales.Count - 1;
			}
			float num4 = this.allRenderScales[this.renderScaleSetting.currentValue];
			float renderViewportScale = this.allRenderScales[Mathf.Min(this.renderViewportScaleSetting.currentValue, this.renderScaleSetting.currentValue)] / num4 * num3;
			VRTK_AdaptiveQuality.SetRenderScale(num4, renderViewportScale);
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00090770 File Offset: 0x0008E970
		private static void SetRenderScale(float renderScale, float renderViewportScale)
		{
			if (Mathf.Abs(XRSettings.eyeTextureResolutionScale - renderScale) > 1.401298E-45f)
			{
				XRSettings.eyeTextureResolutionScale = renderScale;
			}
			if (Mathf.Abs(XRSettings.renderViewportScale - renderViewportScale) > 1.401298E-45f)
			{
				XRSettings.renderViewportScale = renderViewportScale;
			}
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x000907A4 File Offset: 0x0008E9A4
		private int ClampRenderScaleLevel(int renderScaleLevel)
		{
			return Mathf.Clamp(renderScaleLevel, 0, this.allRenderScales.Count - 1);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000907BC File Offset: 0x0008E9BC
		private void CreateOrDestroyDebugVisualization()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (base.enabled && this.drawDebugVisualization && this.debugVisualizationQuad == null)
			{
				Mesh mesh = new Mesh
				{
					vertices = new Vector3[]
					{
						new Vector3(-0.5f, 0.9f, 1f),
						new Vector3(-0.5f, 1f, 1f),
						new Vector3(0.5f, 1f, 1f),
						new Vector3(0.5f, 0.9f, 1f)
					},
					uv = new Vector2[]
					{
						new Vector2(0f, 0f),
						new Vector2(0f, 1f),
						new Vector2(1f, 1f),
						new Vector2(1f, 0f)
					},
					triangles = new int[]
					{
						0,
						1,
						2,
						0,
						2,
						3
					}
				};
				mesh.UploadMeshData(true);
				this.debugVisualizationQuad = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
				{
					"AdaptiveQualityDebugVisualizationQuad"
				}));
				this.debugVisualizationQuad.transform.parent = VRTK_DeviceFinder.HeadsetTransform();
				this.debugVisualizationQuad.transform.localPosition = Vector3.forward;
				this.debugVisualizationQuad.transform.localRotation = Quaternion.identity;
				this.debugVisualizationQuad.AddComponent<MeshFilter>().mesh = mesh;
				this.debugVisualizationQuadMaterial = Resources.Load<Material>("AdaptiveQualityDebugVisualization");
				this.debugVisualizationQuad.AddComponent<MeshRenderer>().material = this.debugVisualizationQuadMaterial;
				return;
			}
			if ((!base.enabled || !this.drawDebugVisualization) && this.debugVisualizationQuad != null)
			{
				Object.Destroy(this.debugVisualizationQuad);
				this.debugVisualizationQuad = null;
				this.debugVisualizationQuadMaterial = null;
			}
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x000909C8 File Offset: 0x0008EBC8
		private void UpdateDebugVisualization()
		{
			if (!this.drawDebugVisualization || this.debugVisualizationQuadMaterial == null)
			{
				return;
			}
			int value = (this.interleavedReprojectionEnabled || VRTK_SharedMethods.GetGPUTimeLastFrame() > this.singleFrameDurationInMilliseconds) ? 0 : 1;
			this.debugVisualizationQuadMaterial.SetInt(VRTK_AdaptiveQuality.ShaderPropertyIDs.RenderScaleLevelsCount, this.allRenderScales.Count);
			this.debugVisualizationQuadMaterial.SetInt(VRTK_AdaptiveQuality.ShaderPropertyIDs.DefaultRenderViewportScaleLevel, this.defaultRenderViewportScaleLevel);
			this.debugVisualizationQuadMaterial.SetInt(VRTK_AdaptiveQuality.ShaderPropertyIDs.CurrentRenderViewportScaleLevel, this.renderViewportScaleSetting.currentValue);
			this.debugVisualizationQuadMaterial.SetInt(VRTK_AdaptiveQuality.ShaderPropertyIDs.CurrentRenderScaleLevel, this.renderScaleSetting.currentValue);
			this.debugVisualizationQuadMaterial.SetInt(VRTK_AdaptiveQuality.ShaderPropertyIDs.LastFrameIsInBudget, value);
		}

		// Token: 0x04001621 RID: 5665
		[Tooltip("Toggles whether to show the debug overlay.\n\nEach square represents a different level on the quality scale. Levels increase from left to right, the first green box that is lit above represents the recommended render target resolution provided by the current `VRDevice`, the box that is lit below in cyan represents the current resolution and the filled box represents the current viewport scale. The yellow boxes represent resolutions below the recommended render target resolution.\nThe currently lit box becomes red whenever the user is likely seeing reprojection in the HMD since the application isn't maintaining VR framerate. If lit, the box all the way on the left is almost always lit red because it represents the lowest render scale with reprojection on.")]
		public bool drawDebugVisualization;

		// Token: 0x04001622 RID: 5666
		[Tooltip("Toggles whether to allow keyboard shortcuts to control this script.\n\n* The supported shortcuts are:\n * `Shift+F1`: Toggle debug visualization on/off\n * `Shift+F2`: Toggle usage of override render scale on/off\n * `Shift+F3`: Decrease override render scale level\n * `Shift+F4`: Increase override render scale level")]
		public bool allowKeyboardShortcuts = true;

		// Token: 0x04001623 RID: 5667
		[Tooltip("Toggles whether to allow command line arguments to control this script at startup of the standalone build.\n\n* The supported command line arguments all begin with '-' and are:\n * `-noaq`: Disable adaptive quality\n * `-aqminscale X`: Set minimum render scale to X\n * `-aqmaxscale X`: Set maximum render scale to X\n * `-aqmaxres X`: Set maximum render target dimension to X\n * `-aqfillratestep X`: Set render scale fill rate step size in percent to X (X from 1 to 100)\n * `-aqoverride X`: Set override render scale level to X\n * `-vrdebug`: Enable debug visualization\n * `-msaa X`: Set MSAA level to X")]
		public bool allowCommandLineArguments = true;

		// Token: 0x04001624 RID: 5668
		[Tooltip("The MSAA level to use.")]
		[Header("Quality")]
		[Range(0f, 8f)]
		public int msaaLevel = 4;

		// Token: 0x04001625 RID: 5669
		[Tooltip("Toggles whether the render viewport scale is dynamically adjusted to maintain VR framerate.\n\nIf unchecked, the renderer will render at the recommended resolution provided by the current `VRDevice`.")]
		public bool scaleRenderViewport = true;

		// Token: 0x04001626 RID: 5670
		[Tooltip("The minimum allowed render scale.")]
		[Range(0.01f, 5f)]
		public float minimumRenderScale = 0.8f;

		// Token: 0x04001627 RID: 5671
		[Tooltip("The maximum allowed render scale.")]
		public float maximumRenderScale = 1.4f;

		// Token: 0x04001628 RID: 5672
		[Tooltip("The maximum allowed render target dimension.\n\nThis puts an upper limit on the size of the render target regardless of the maximum render scale.")]
		public int maximumRenderTargetDimension = 4096;

		// Token: 0x04001629 RID: 5673
		[Tooltip("The fill rate step size in percent by which the render scale levels will be calculated.")]
		[Range(1f, 100f)]
		public int renderScaleFillRateStepSizeInPercent = 15;

		// Token: 0x0400162A RID: 5674
		[Tooltip("Toggles whether the render target resolution is dynamically adjusted to maintain VR framerate.\n\nIf unchecked, the renderer will use the maximum target resolution specified by `maximumRenderScale`.")]
		public bool scaleRenderTargetResolution;

		// Token: 0x0400162B RID: 5675
		[Tooltip("Toggles whether to override the used render viewport scale level.")]
		[Header("Override")]
		[NonSerialized]
		public bool overrideRenderViewportScale;

		// Token: 0x0400162C RID: 5676
		[Tooltip("The render viewport scale level to override the current one with.")]
		[NonSerialized]
		public int overrideRenderViewportScaleLevel;

		// Token: 0x0400162D RID: 5677
		public readonly ReadOnlyCollection<float> renderScales;

		// Token: 0x0400162E RID: 5678
		private const float DefaultFrameDurationInMilliseconds = 11.1111107f;

		// Token: 0x0400162F RID: 5679
		private readonly VRTK_AdaptiveQuality.AdaptiveSetting<int> renderViewportScaleSetting = new VRTK_AdaptiveQuality.AdaptiveSetting<int>(0, 30, 10);

		// Token: 0x04001630 RID: 5680
		private readonly VRTK_AdaptiveQuality.AdaptiveSetting<int> renderScaleSetting = new VRTK_AdaptiveQuality.AdaptiveSetting<int>(0, 180, 90);

		// Token: 0x04001631 RID: 5681
		private readonly List<float> allRenderScales = new List<float>();

		// Token: 0x04001632 RID: 5682
		private int defaultRenderViewportScaleLevel;

		// Token: 0x04001633 RID: 5683
		private float previousMinimumRenderScale;

		// Token: 0x04001634 RID: 5684
		private float previousMaximumRenderScale;

		// Token: 0x04001635 RID: 5685
		private float previousRenderScaleFillRateStepSizeInPercent;

		// Token: 0x04001636 RID: 5686
		private readonly VRTK_AdaptiveQuality.Timing timing = new VRTK_AdaptiveQuality.Timing();

		// Token: 0x04001637 RID: 5687
		private int lastRenderViewportScaleLevelBelowRenderScaleLevelFrameCount;

		// Token: 0x04001638 RID: 5688
		private bool interleavedReprojectionEnabled;

		// Token: 0x04001639 RID: 5689
		private bool hmdDisplayIsOnDesktop;

		// Token: 0x0400163A RID: 5690
		private float singleFrameDurationInMilliseconds;

		// Token: 0x0400163B RID: 5691
		private GameObject debugVisualizationQuad;

		// Token: 0x0400163C RID: 5692
		private Material debugVisualizationQuadMaterial;

		// Token: 0x0200060C RID: 1548
		private sealed class AdaptiveSetting<T>
		{
			// Token: 0x17000367 RID: 871
			// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x000C93CE File Offset: 0x000C75CE
			// (set) Token: 0x06002ABA RID: 10938 RVA: 0x000C93D6 File Offset: 0x000C75D6
			public T currentValue
			{
				get
				{
					return this._currentValue;
				}
				set
				{
					if (!EqualityComparer<T>.Default.Equals(value, this._currentValue))
					{
						this.lastChangeFrameCount = Time.frameCount;
					}
					this.previousValue = this._currentValue;
					this._currentValue = value;
				}
			}

			// Token: 0x17000368 RID: 872
			// (get) Token: 0x06002ABB RID: 10939 RVA: 0x000C9409 File Offset: 0x000C7609
			// (set) Token: 0x06002ABC RID: 10940 RVA: 0x000C9411 File Offset: 0x000C7611
			public T previousValue { get; private set; }

			// Token: 0x17000369 RID: 873
			// (get) Token: 0x06002ABD RID: 10941 RVA: 0x000C941A File Offset: 0x000C761A
			// (set) Token: 0x06002ABE RID: 10942 RVA: 0x000C9422 File Offset: 0x000C7622
			public int lastChangeFrameCount { get; private set; }

			// Token: 0x06002ABF RID: 10943 RVA: 0x000C942B File Offset: 0x000C762B
			public AdaptiveSetting(T currentValue, int increaseFrameCost, int decreaseFrameCost)
			{
				this.previousValue = currentValue;
				this.currentValue = currentValue;
				this.increaseFrameCost = increaseFrameCost;
				this.decreaseFrameCost = decreaseFrameCost;
			}

			// Token: 0x0400287A RID: 10362
			public readonly int increaseFrameCost;

			// Token: 0x0400287B RID: 10363
			public readonly int decreaseFrameCost;

			// Token: 0x0400287C RID: 10364
			private T _currentValue;
		}

		// Token: 0x0200060D RID: 1549
		private static class CommandLineArguments
		{
			// Token: 0x0400287D RID: 10365
			public const string Disable = "-noaq";

			// Token: 0x0400287E RID: 10366
			public const string MinimumRenderScale = "-aqminscale";

			// Token: 0x0400287F RID: 10367
			public const string MaximumRenderScale = "-aqmaxscale";

			// Token: 0x04002880 RID: 10368
			public const string MaximumRenderTargetDimension = "-aqmaxres";

			// Token: 0x04002881 RID: 10369
			public const string RenderScaleFillRateStepSizeInPercent = "-aqfillratestep";

			// Token: 0x04002882 RID: 10370
			public const string OverrideRenderScaleLevel = "-aqoverride";

			// Token: 0x04002883 RID: 10371
			public const string DrawDebugVisualization = "-vrdebug";

			// Token: 0x04002884 RID: 10372
			public const string MSAALevel = "-msaa";
		}

		// Token: 0x0200060E RID: 1550
		private static class KeyboardShortcuts
		{
			// Token: 0x04002885 RID: 10373
			public static readonly KeyCode[] Modifiers = new KeyCode[]
			{
				KeyCode.LeftShift,
				KeyCode.RightShift
			};

			// Token: 0x04002886 RID: 10374
			public const KeyCode ToggleDrawDebugVisualization = KeyCode.F1;

			// Token: 0x04002887 RID: 10375
			public const KeyCode ToggleOverrideRenderScale = KeyCode.F2;

			// Token: 0x04002888 RID: 10376
			public const KeyCode DecreaseOverrideRenderScaleLevel = KeyCode.F3;

			// Token: 0x04002889 RID: 10377
			public const KeyCode IncreaseOverrideRenderScaleLevel = KeyCode.F4;
		}

		// Token: 0x0200060F RID: 1551
		private static class ShaderPropertyIDs
		{
			// Token: 0x0400288A RID: 10378
			public static readonly int RenderScaleLevelsCount = Shader.PropertyToID("_RenderScaleLevelsCount");

			// Token: 0x0400288B RID: 10379
			public static readonly int DefaultRenderViewportScaleLevel = Shader.PropertyToID("_DefaultRenderViewportScaleLevel");

			// Token: 0x0400288C RID: 10380
			public static readonly int CurrentRenderViewportScaleLevel = Shader.PropertyToID("_CurrentRenderViewportScaleLevel");

			// Token: 0x0400288D RID: 10381
			public static readonly int CurrentRenderScaleLevel = Shader.PropertyToID("_CurrentRenderScaleLevel");

			// Token: 0x0400288E RID: 10382
			public static readonly int LastFrameIsInBudget = Shader.PropertyToID("_LastFrameIsInBudget");
		}

		// Token: 0x02000610 RID: 1552
		private sealed class Timing
		{
			// Token: 0x06002AC2 RID: 10946 RVA: 0x000C94C4 File Offset: 0x000C76C4
			public void SaveCurrentFrameTiming()
			{
				this.bufferIndex = (this.bufferIndex + 1) % this.buffer.Length;
				this.buffer[this.bufferIndex] = VRTK_SharedMethods.GetGPUTimeLastFrame();
			}

			// Token: 0x06002AC3 RID: 10947 RVA: 0x000C94EF File Offset: 0x000C76EF
			public float GetFrameTiming(int framesAgo)
			{
				return this.buffer[(this.bufferIndex - framesAgo + this.buffer.Length) % this.buffer.Length];
			}

			// Token: 0x06002AC4 RID: 10948 RVA: 0x000C9514 File Offset: 0x000C7714
			public bool WasFrameTimingBad(int framesAgo, float thresholdInMilliseconds, int lastChangeFrameCount, int changeFrameCost)
			{
				if (!VRTK_AdaptiveQuality.Timing.AreFramesAvailable(framesAgo, lastChangeFrameCount, changeFrameCost))
				{
					return false;
				}
				for (int i = 0; i < framesAgo; i++)
				{
					if (this.GetFrameTiming(i) <= thresholdInMilliseconds)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06002AC5 RID: 10949 RVA: 0x000C9548 File Offset: 0x000C7748
			public bool WasFrameTimingGood(int framesAgo, float thresholdInMilliseconds, int lastChangeFrameCount, int changeFrameCost)
			{
				if (!VRTK_AdaptiveQuality.Timing.AreFramesAvailable(framesAgo, lastChangeFrameCount, changeFrameCost))
				{
					return false;
				}
				for (int i = 0; i < framesAgo; i++)
				{
					if (this.GetFrameTiming(i) > thresholdInMilliseconds)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06002AC6 RID: 10950 RVA: 0x000C957C File Offset: 0x000C777C
			public bool WillFrameTimingBeBad(float extrapolationThresholdInMilliseconds, float thresholdInMilliseconds, int lastChangeFrameCount, int changeFrameCost)
			{
				if (!VRTK_AdaptiveQuality.Timing.AreFramesAvailable(2, lastChangeFrameCount, changeFrameCost))
				{
					return false;
				}
				float frameTiming = this.GetFrameTiming(0);
				if (frameTiming <= extrapolationThresholdInMilliseconds)
				{
					return false;
				}
				float num = frameTiming - this.GetFrameTiming(1);
				if (!VRTK_AdaptiveQuality.Timing.AreFramesAvailable(3, lastChangeFrameCount, changeFrameCost))
				{
					num = Mathf.Max(num, (frameTiming - this.GetFrameTiming(2)) / 2f);
				}
				return frameTiming + num > thresholdInMilliseconds;
			}

			// Token: 0x06002AC7 RID: 10951 RVA: 0x000C95D5 File Offset: 0x000C77D5
			private static bool AreFramesAvailable(int framesAgo, int lastChangeFrameCount, int changeFrameCost)
			{
				return Time.frameCount >= framesAgo + lastChangeFrameCount + changeFrameCost;
			}

			// Token: 0x0400288F RID: 10383
			private readonly float[] buffer = new float[12];

			// Token: 0x04002890 RID: 10384
			private int bufferIndex;
		}
	}
}
