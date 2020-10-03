using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002BC RID: 700
	public class VRTK_ScreenFade : MonoBehaviour
	{
		// Token: 0x0600173B RID: 5947 RVA: 0x0007C76D File Offset: 0x0007A96D
		public static void Start(Color newColor, float duration)
		{
			if (VRTK_ScreenFade.instance)
			{
				VRTK_ScreenFade.instance.StartFade(newColor, duration);
			}
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0007C787 File Offset: 0x0007A987
		public virtual void StartFade(Color newColor, float duration)
		{
			if (duration > 0f)
			{
				this.targetColor = newColor;
				this.deltaColor = (this.targetColor - this.currentColor) / duration;
				return;
			}
			this.currentColor = newColor;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0007C7BD File Offset: 0x0007A9BD
		protected virtual void Awake()
		{
			this.fadeMaterial = new Material(Shader.Find("Unlit/TransparentColor"));
			VRTK_ScreenFade.instance = this;
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0007C7DC File Offset: 0x0007A9DC
		protected virtual void OnPostRender()
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
			}
			if (this.currentColor.a > 0f && this.fadeMaterial)
			{
				this.currentColor.a = ((this.targetColor.a > this.currentColor.a && this.currentColor.a > 0.98f) ? 1f : this.currentColor.a);
				this.fadeMaterial.color = this.currentColor;
				this.fadeMaterial.SetPass(0);
				GL.PushMatrix();
				GL.LoadOrtho();
				GL.Color(this.fadeMaterial.color);
				GL.Begin(7);
				GL.Vertex3(0f, 0f, 0.9999f);
				GL.Vertex3(0f, 1f, 0.9999f);
				GL.Vertex3(1f, 1f, 0.9999f);
				GL.Vertex3(1f, 0f, 0.9999f);
				GL.End();
				GL.PopMatrix();
			}
		}

		// Token: 0x04001304 RID: 4868
		public static VRTK_ScreenFade instance;

		// Token: 0x04001305 RID: 4869
		protected Material fadeMaterial;

		// Token: 0x04001306 RID: 4870
		protected Color currentColor = new Color(0f, 0f, 0f, 0f);

		// Token: 0x04001307 RID: 4871
		protected Color targetColor = new Color(0f, 0f, 0f, 0f);

		// Token: 0x04001308 RID: 4872
		protected Color deltaColor = new Color(0f, 0f, 0f, 0f);
	}
}
