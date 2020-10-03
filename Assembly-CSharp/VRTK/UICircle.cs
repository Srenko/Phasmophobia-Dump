using System;
using UnityEngine;
using UnityEngine.UI;

namespace VRTK
{
	// Token: 0x02000260 RID: 608
	[ExecuteInEditMode]
	public class UICircle : Graphic
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00068BA2 File Offset: 0x00066DA2
		public override Texture mainTexture
		{
			get
			{
				if (!(this.setTexture == null))
				{
					return this.setTexture;
				}
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00068BBE File Offset: 0x00066DBE
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x00068BC6 File Offset: 0x00066DC6
		public Texture texture
		{
			get
			{
				return this.setTexture;
			}
			set
			{
				if (this.setTexture == value)
				{
					return;
				}
				this.setTexture = value;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00068BEC File Offset: 0x00066DEC
		[Obsolete("Use OnPopulateMesh(VertexHelper vh) instead.")]
		protected override void OnPopulateMesh(Mesh toFill)
		{
			float num = -base.rectTransform.pivot.x * base.rectTransform.rect.width;
			float num2 = -base.rectTransform.pivot.x * base.rectTransform.rect.width + (float)this.thickness;
			toFill.Clear();
			VertexHelper vertexHelper = new VertexHelper(toFill);
			Vector2 vector = Vector2.zero;
			Vector2 vector2 = Vector2.zero;
			Vector2 vector3 = new Vector2(0f, 0f);
			Vector2 vector4 = new Vector2(0f, 1f);
			Vector2 vector5 = new Vector2(1f, 1f);
			Vector2 vector6 = new Vector2(1f, 0f);
			float num3 = (float)this.fillPercent / 100f;
			float num4 = 360f / (float)this.segments;
			int num5 = (int)((float)(this.segments + 1) * num3);
			for (int i = -1 - num5 / 2; i < num5 / 2 + 1; i++)
			{
				float f = 0.0174532924f * ((float)i * num4);
				float num6 = Mathf.Cos(f);
				float num7 = Mathf.Sin(f);
				vector3 = new Vector2(0f, 1f);
				vector4 = new Vector2(1f, 1f);
				vector5 = new Vector2(1f, 0f);
				vector6 = new Vector2(0f, 0f);
				Vector2 vector7 = vector;
				Vector2 vector8 = new Vector2(num * num6, num * num7);
				Vector2 zero;
				Vector2 vector9;
				if (this.fill)
				{
					zero = Vector2.zero;
					vector9 = Vector2.zero;
				}
				else
				{
					zero = new Vector2(num2 * num6, num2 * num7);
					vector9 = vector2;
				}
				vector = vector8;
				vector2 = zero;
				vertexHelper.AddUIVertexQuad(this.SetVbo(new Vector2[]
				{
					vector7,
					vector8,
					zero,
					vector9
				}, new Vector2[]
				{
					vector3,
					vector4,
					vector5,
					vector6
				}));
			}
			if (vertexHelper.currentVertCount > 3)
			{
				vertexHelper.FillMesh(toFill);
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00068E10 File Offset: 0x00067010
		protected virtual void Update()
		{
			this.thickness = (int)Mathf.Clamp((float)this.thickness, 0f, base.rectTransform.rect.width / 2f);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00068E50 File Offset: 0x00067050
		protected virtual UIVertex[] SetVbo(Vector2[] vertices, Vector2[] uvs)
		{
			UIVertex[] array = new UIVertex[4];
			for (int i = 0; i < vertices.Length; i++)
			{
				UIVertex simpleVert = UIVertex.simpleVert;
				simpleVert.color = this.color;
				simpleVert.position = vertices[i];
				simpleVert.uv0 = uvs[i];
				array[i] = simpleVert;
			}
			return array;
		}

		// Token: 0x0400109A RID: 4250
		[Range(0f, 100f)]
		public int fillPercent;

		// Token: 0x0400109B RID: 4251
		public bool fill = true;

		// Token: 0x0400109C RID: 4252
		public int thickness = 5;

		// Token: 0x0400109D RID: 4253
		[Range(0f, 360f)]
		public int segments = 360;

		// Token: 0x0400109E RID: 4254
		[SerializeField]
		protected Texture setTexture;
	}
}
