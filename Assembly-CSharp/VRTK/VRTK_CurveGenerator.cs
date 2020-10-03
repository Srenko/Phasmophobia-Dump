using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace VRTK
{
	// Token: 0x020002B5 RID: 693
	public class VRTK_CurveGenerator : MonoBehaviour
	{
		// Token: 0x06001701 RID: 5889 RVA: 0x0007B62C File Offset: 0x0007982C
		public virtual void Create(int setFrequency, float radius, GameObject tracer, bool rescaleTracer = false)
		{
			float num = radius / 8f;
			this.frequency = setFrequency;
			this.customLineRenderer = ((tracer != null) ? tracer.GetComponent<LineRenderer>() : null);
			this.lineRendererAndItem = (this.customLineRenderer != null && tracer.GetComponentInChildren<MeshFilter>());
			if (this.customLineRenderer != null)
			{
				this.tracerLineRenderer = Object.Instantiate<GameObject>(tracer);
				this.tracerLineRenderer.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
				{
					base.name,
					"LineRenderer"
				});
				for (int i = 0; i < this.tracerLineRenderer.transform.childCount; i++)
				{
					Object.Destroy(this.tracerLineRenderer.transform.GetChild(i).gameObject);
				}
				this.customLineRenderer = this.tracerLineRenderer.GetComponent<LineRenderer>();
				this.customLineRenderer.positionCount = this.frequency;
			}
			if (this.customLineRenderer == null || this.lineRendererAndItem)
			{
				this.items = new GameObject[this.frequency];
				for (int j = 0; j < this.items.Length; j++)
				{
					this.customTracer = true;
					this.items[j] = ((tracer != null) ? Object.Instantiate<GameObject>(tracer) : this.CreateSphere());
					this.items[j].transform.SetParent(base.transform);
					this.items[j].layer = LayerMask.NameToLayer("Ignore Raycast");
					this.items[j].transform.localScale = new Vector3(num, num, num);
					if (this.customLineRenderer != null)
					{
						Object.Destroy(this.items[j].GetComponent<LineRenderer>());
					}
				}
			}
			this.rescalePointerTracer = rescaleTracer;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0007B7F9 File Offset: 0x000799F9
		public virtual void SetPoints(Vector3[] controlPoints, Material material, Color color)
		{
			this.PointsInit(controlPoints);
			this.SetObjects(material, color);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0007B80C File Offset: 0x00079A0C
		public virtual Vector3[] GetPoints(Vector3[] controlPoints)
		{
			this.PointsInit(controlPoints);
			Vector3[] array = new Vector3[this.frequency];
			float num = (float)this.frequency;
			if (this.Loop || num == 1f)
			{
				num = 1f / num;
			}
			else
			{
				num = 1f / (num - 1f);
			}
			for (int i = 0; i < this.frequency; i++)
			{
				array[i] = this.GetPoint((float)i * num);
			}
			return array;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0007B87F File Offset: 0x00079A7F
		public virtual void TogglePoints(bool state)
		{
			base.gameObject.SetActive(state);
			if (this.tracerLineRenderer != null)
			{
				this.tracerLineRenderer.SetActive(state);
			}
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0007B8A7 File Offset: 0x00079AA7
		protected virtual void PointsInit(Vector3[] controlPoints)
		{
			this.points = controlPoints;
			this.modes = new VRTK_CurveGenerator.BezierControlPointMode[2];
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0007B8BC File Offset: 0x00079ABC
		protected virtual GameObject CreateSphere()
		{
			this.customTracer = false;
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gameObject.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				"Sphere"
			});
			Object.Destroy(gameObject.GetComponent<SphereCollider>());
			gameObject.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
			gameObject.GetComponent<MeshRenderer>().receiveShadows = false;
			return gameObject;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0007B915 File Offset: 0x00079B15
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x0007B91D File Offset: 0x00079B1D
		protected virtual bool Loop
		{
			get
			{
				return this.loop;
			}
			set
			{
				this.loop = value;
				if (value)
				{
					this.modes[this.modes.Length - 1] = this.modes[0];
					this.SetControlPoint(0, this.points[0]);
				}
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x0007B955 File Offset: 0x00079B55
		protected virtual int ControlPointCount
		{
			get
			{
				return this.points.Length;
			}
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0007B95F File Offset: 0x00079B5F
		protected virtual Vector3 GetControlPoint(int index)
		{
			return this.points[index];
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0007B970 File Offset: 0x00079B70
		protected virtual void SetControlPoint(int index, Vector3 point)
		{
			if (index % 3 == 0)
			{
				Vector3 b = point - this.points[index];
				if (this.loop)
				{
					if (index == 0)
					{
						this.points[1] += b;
						this.points[this.points.Length - 2] += b;
						this.points[this.points.Length - 1] = point;
					}
					else if (index == this.points.Length - 1)
					{
						this.points[0] = point;
						this.points[1] += b;
						this.points[index - 1] += b;
					}
					else
					{
						this.points[index - 1] += b;
						this.points[index + 1] += b;
					}
				}
				else
				{
					if (index > 0)
					{
						this.points[index - 1] += b;
					}
					if (index + 1 < this.points.Length)
					{
						this.points[index + 1] += b;
					}
				}
			}
			this.points[index] = point;
			this.EnforceMode(index);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0007BB04 File Offset: 0x00079D04
		protected virtual void EnforceMode(int index)
		{
			int num = (index + 1) / 3;
			VRTK_CurveGenerator.BezierControlPointMode bezierControlPointMode = this.modes[num];
			if (bezierControlPointMode == VRTK_CurveGenerator.BezierControlPointMode.Free || (!this.loop && (num == 0 || num == this.modes.Length - 1)))
			{
				return;
			}
			int num2 = num * 3;
			int num3;
			int num4;
			if (index <= num2)
			{
				num3 = num2 - 1;
				if (num3 < 0)
				{
					num3 = this.points.Length - 2;
				}
				num4 = num2 + 1;
				if (num4 >= this.points.Length)
				{
					num4 = 1;
				}
			}
			else
			{
				num3 = num2 + 1;
				if (num3 >= this.points.Length)
				{
					num3 = 1;
				}
				num4 = num2 - 1;
				if (num4 < 0)
				{
					num4 = this.points.Length - 2;
				}
			}
			Vector3 a = this.points[num2];
			Vector3 b = a - this.points[num3];
			if (bezierControlPointMode == VRTK_CurveGenerator.BezierControlPointMode.Aligned)
			{
				b = b.normalized * Vector3.Distance(a, this.points[num4]);
			}
			this.points[num4] = a + b;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0007BBF3 File Offset: 0x00079DF3
		protected virtual int CurveCount
		{
			get
			{
				return (this.points.Length - 1) / 3;
			}
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0007BC04 File Offset: 0x00079E04
		protected virtual Vector3 GetPoint(float t)
		{
			int num;
			if (t >= 1f)
			{
				t = 1f;
				num = this.points.Length - 4;
			}
			else
			{
				t = Mathf.Clamp01(t) * (float)this.CurveCount;
				num = (int)t;
				t -= (float)num;
				num *= 3;
			}
			return base.transform.TransformPoint(Bezier.GetPoint(this.points[num], this.points[num + 1], this.points[num + 2], this.points[num + 3], t));
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0007BC94 File Offset: 0x00079E94
		protected virtual void SetObjects(Material material, Color color)
		{
			float num = (float)this.frequency;
			if (this.Loop || num == 1f)
			{
				num = 1f / num;
			}
			else
			{
				num = 1f / (num - 1f);
			}
			this.SetPointData(material, color, num);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0007BCDC File Offset: 0x00079EDC
		protected virtual void SetPointData(Material material, Color color, float stepSize)
		{
			for (int i = 0; i < this.frequency; i++)
			{
				Vector3 point = this.GetPoint((float)i * stepSize);
				if (this.customLineRenderer != null)
				{
					this.customLineRenderer.SetPosition(i, point);
					this.SetMaterial(this.customLineRenderer.sharedMaterial, color);
				}
				if (this.customLineRenderer == null || this.lineRendererAndItem)
				{
					this.SetItemPosition(i, point, material, color, stepSize);
				}
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0007BD54 File Offset: 0x00079F54
		protected virtual void SetItemPosition(int currentIndex, Vector3 setPosition, Material material, Color color, float stepSize)
		{
			if (this.customTracer && currentIndex == this.frequency - 1)
			{
				this.items[currentIndex].SetActive(false);
				return;
			}
			this.SetItemMaterial(this.items[currentIndex], material, color);
			this.items[currentIndex].transform.position = setPosition;
			Vector3 vector = this.GetPoint((float)(currentIndex + 1) * stepSize) - setPosition;
			Vector3 normalized = vector.normalized;
			if (normalized != Vector3.zero)
			{
				this.items[currentIndex].transform.rotation = Quaternion.LookRotation(normalized);
				if (this.rescalePointerTracer)
				{
					Vector3 localScale = this.items[currentIndex].transform.localScale;
					localScale.z = vector.magnitude / 2f;
					this.items[currentIndex].transform.localScale = localScale;
				}
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0007BE2C File Offset: 0x0007A02C
		protected virtual void SetItemMaterial(GameObject item, Material material, Color color)
		{
			foreach (Renderer renderer in item.GetComponentsInChildren<Renderer>())
			{
				if (material != null)
				{
					renderer.material = material;
				}
				this.SetMaterial(renderer.material, color);
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0007BE70 File Offset: 0x0007A070
		protected virtual void SetMaterial(Material material, Color color)
		{
			if (material != null)
			{
				material.EnableKeyword("_EMISSION");
				if (material.HasProperty("_Color"))
				{
					material.color = color;
				}
				if (material.HasProperty("_EmissionColor"))
				{
					material.SetColor("_EmissionColor", VRTK_SharedMethods.ColorDarken(color, 50f));
				}
			}
		}

		// Token: 0x040012E1 RID: 4833
		protected Vector3[] points;

		// Token: 0x040012E2 RID: 4834
		protected GameObject[] items;

		// Token: 0x040012E3 RID: 4835
		protected VRTK_CurveGenerator.BezierControlPointMode[] modes;

		// Token: 0x040012E4 RID: 4836
		protected bool loop;

		// Token: 0x040012E5 RID: 4837
		protected int frequency;

		// Token: 0x040012E6 RID: 4838
		protected bool customTracer;

		// Token: 0x040012E7 RID: 4839
		protected bool rescalePointerTracer;

		// Token: 0x040012E8 RID: 4840
		protected GameObject tracerLineRenderer;

		// Token: 0x040012E9 RID: 4841
		protected LineRenderer customLineRenderer;

		// Token: 0x040012EA RID: 4842
		protected bool lineRendererAndItem;

		// Token: 0x020005DC RID: 1500
		public enum BezierControlPointMode
		{
			// Token: 0x040027AF RID: 10159
			Free,
			// Token: 0x040027B0 RID: 10160
			Aligned,
			// Token: 0x040027B1 RID: 10161
			Mirrored
		}
	}
}
