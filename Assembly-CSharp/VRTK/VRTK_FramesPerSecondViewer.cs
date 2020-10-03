using System;
using UnityEngine;
using UnityEngine.UI;

namespace VRTK
{
	// Token: 0x0200024E RID: 590
	public class VRTK_FramesPerSecondViewer : MonoBehaviour
	{
		// Token: 0x06001139 RID: 4409 RVA: 0x00064D85 File Offset: 0x00062F85
		protected virtual void OnEnable()
		{
			this.sdkManager = VRTK_SDKManager.instance;
			if (this.sdkManager != null)
			{
				this.sdkManager.LoadedSetupChanged += this.LoadedSetupChanged;
			}
			this.InitCanvas();
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00064DBE File Offset: 0x00062FBE
		protected virtual void OnDisable()
		{
			if (this.sdkManager != null && !base.gameObject.activeSelf)
			{
				this.sdkManager.LoadedSetupChanged -= this.LoadedSetupChanged;
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00064DF4 File Offset: 0x00062FF4
		protected virtual void Update()
		{
			this.framesCount++;
			this.framesTime += Time.unscaledDeltaTime;
			if (this.framesTime > 0.5f)
			{
				if (this.text != null)
				{
					if (this.displayFPS)
					{
						float num = (float)this.framesCount / this.framesTime;
						this.text.text = string.Format("{0:F2} FPS", num);
						this.text.color = ((num > (float)(this.targetFPS - 5)) ? this.goodColor : ((num > (float)(this.targetFPS - 30)) ? this.warnColor : this.badColor));
					}
					else
					{
						this.text.text = "";
					}
				}
				this.framesCount = 0;
				this.framesTime = 0f;
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00064ECF File Offset: 0x000630CF
		protected virtual void LoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
		{
			this.SetCanvasCamera();
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00064ED8 File Offset: 0x000630D8
		protected virtual void InitCanvas()
		{
			this.canvas = base.transform.GetComponentInParent<Canvas>();
			this.text = base.GetComponent<Text>();
			if (this.canvas != null)
			{
				this.canvas.planeDistance = 0.5f;
			}
			if (this.text != null)
			{
				this.text.fontSize = this.fontSize;
				this.text.transform.localPosition = this.position;
			}
			this.SetCanvasCamera();
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00064F5C File Offset: 0x0006315C
		protected virtual void SetCanvasCamera()
		{
			Transform transform = VRTK_DeviceFinder.HeadsetCamera();
			if (transform != null)
			{
				this.canvas.worldCamera = transform.GetComponent<Camera>();
			}
		}

		// Token: 0x0400100C RID: 4108
		[Tooltip("Toggles whether the FPS text is visible.")]
		public bool displayFPS = true;

		// Token: 0x0400100D RID: 4109
		[Tooltip("The frames per second deemed acceptable that is used as the benchmark to change the FPS text colour.")]
		public int targetFPS = 90;

		// Token: 0x0400100E RID: 4110
		[Tooltip("The size of the font the FPS is displayed in.")]
		public int fontSize = 32;

		// Token: 0x0400100F RID: 4111
		[Tooltip("The position of the FPS text within the headset view.")]
		public Vector3 position = Vector3.zero;

		// Token: 0x04001010 RID: 4112
		[Tooltip("The colour of the FPS text when the frames per second are within reasonable limits of the Target FPS.")]
		public Color goodColor = Color.green;

		// Token: 0x04001011 RID: 4113
		[Tooltip("The colour of the FPS text when the frames per second are falling short of reasonable limits of the Target FPS.")]
		public Color warnColor = Color.yellow;

		// Token: 0x04001012 RID: 4114
		[Tooltip("The colour of the FPS text when the frames per second are at an unreasonable level of the Target FPS.")]
		public Color badColor = Color.red;

		// Token: 0x04001013 RID: 4115
		protected const float updateInterval = 0.5f;

		// Token: 0x04001014 RID: 4116
		protected int framesCount;

		// Token: 0x04001015 RID: 4117
		protected float framesTime;

		// Token: 0x04001016 RID: 4118
		protected Canvas canvas;

		// Token: 0x04001017 RID: 4119
		protected Text text;

		// Token: 0x04001018 RID: 4120
		protected VRTK_SDKManager sdkManager;
	}
}
