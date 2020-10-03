using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000452 RID: 1106
	public class TeleportArc : MonoBehaviour
	{
		// Token: 0x06002213 RID: 8723 RVA: 0x000A9548 File Offset: 0x000A7748
		private void Start()
		{
			this.arcTimeOffset = Time.time;
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000A9555 File Offset: 0x000A7755
		private void Update()
		{
			if (this.thickness != this.prevThickness || this.segmentCount != this.prevSegmentCount)
			{
				this.CreateLineRendererObjects();
				this.prevThickness = this.thickness;
				this.prevSegmentCount = this.segmentCount;
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000A9594 File Offset: 0x000A7794
		private void CreateLineRendererObjects()
		{
			if (this.arcObjectsTransfrom != null)
			{
				Object.Destroy(this.arcObjectsTransfrom.gameObject);
			}
			GameObject gameObject = new GameObject("ArcObjects");
			this.arcObjectsTransfrom = gameObject.transform;
			this.arcObjectsTransfrom.SetParent(base.transform);
			this.lineRenderers = new LineRenderer[this.segmentCount];
			for (int i = 0; i < this.segmentCount; i++)
			{
				GameObject gameObject2 = new GameObject("LineRenderer_" + i);
				gameObject2.transform.SetParent(this.arcObjectsTransfrom);
				this.lineRenderers[i] = gameObject2.AddComponent<LineRenderer>();
				this.lineRenderers[i].receiveShadows = false;
				this.lineRenderers[i].reflectionProbeUsage = ReflectionProbeUsage.Off;
				this.lineRenderers[i].lightProbeUsage = LightProbeUsage.Off;
				this.lineRenderers[i].shadowCastingMode = ShadowCastingMode.Off;
				this.lineRenderers[i].material = this.material;
				this.lineRenderers[i].startWidth = this.thickness;
				this.lineRenderers[i].endWidth = this.thickness;
				this.lineRenderers[i].enabled = false;
			}
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000A96C3 File Offset: 0x000A78C3
		public void SetArcData(Vector3 position, Vector3 velocity, bool gravity, bool pointerAtBadAngle)
		{
			this.startPos = position;
			this.projectileVelocity = velocity;
			this.useGravity = gravity;
			if (this.arcInvalid && !pointerAtBadAngle)
			{
				this.arcTimeOffset = Time.time;
			}
			this.arcInvalid = pointerAtBadAngle;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000A96F9 File Offset: 0x000A78F9
		public void Show()
		{
			this.showArc = true;
			if (this.lineRenderers == null)
			{
				this.CreateLineRendererObjects();
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000A9710 File Offset: 0x000A7910
		public void Hide()
		{
			if (this.showArc)
			{
				this.HideLineSegments(0, this.segmentCount);
			}
			this.showArc = false;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000A9730 File Offset: 0x000A7930
		public bool DrawArc(out RaycastHit hitInfo)
		{
			float num = this.arcDuration / (float)this.segmentCount;
			float num2 = (Time.time - this.arcTimeOffset) * this.arcSpeed;
			if (num2 > num + this.segmentBreak)
			{
				this.arcTimeOffset = Time.time;
				num2 = 0f;
			}
			float num3 = num2;
			float num4 = this.FindProjectileCollision(out hitInfo);
			if (this.arcInvalid)
			{
				this.lineRenderers[0].enabled = true;
				this.lineRenderers[0].SetPosition(0, this.GetArcPositionAtTime(0f));
				this.lineRenderers[0].SetPosition(1, this.GetArcPositionAtTime((num4 < num) ? num4 : num));
				this.HideLineSegments(1, this.segmentCount);
			}
			else
			{
				int num5 = 0;
				if (num3 > this.segmentBreak)
				{
					float num6 = num2 - this.segmentBreak;
					if (num4 < num6)
					{
						num6 = num4;
					}
					this.DrawArcSegment(0, 0f, num6);
					num5 = 1;
				}
				bool flag = false;
				int i = 0;
				if (num3 < num4)
				{
					for (i = num5; i < this.segmentCount; i++)
					{
						float num7 = num3 + num;
						if (num7 >= this.arcDuration)
						{
							num7 = this.arcDuration;
							flag = true;
						}
						if (num7 >= num4)
						{
							num7 = num4;
							flag = true;
						}
						this.DrawArcSegment(i, num3, num7);
						num3 += num + this.segmentBreak;
						if (flag || num3 >= this.arcDuration || num3 >= num4)
						{
							break;
						}
					}
				}
				else
				{
					i--;
				}
				this.HideLineSegments(i + 1, this.segmentCount);
			}
			return num4 != float.MaxValue;
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000A98A1 File Offset: 0x000A7AA1
		private void DrawArcSegment(int index, float startTime, float endTime)
		{
			this.lineRenderers[index].enabled = true;
			this.lineRenderers[index].SetPosition(0, this.GetArcPositionAtTime(startTime));
			this.lineRenderers[index].SetPosition(1, this.GetArcPositionAtTime(endTime));
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000A98DC File Offset: 0x000A7ADC
		public void SetColor(Color color)
		{
			for (int i = 0; i < this.segmentCount; i++)
			{
				this.lineRenderers[i].startColor = color;
				this.lineRenderers[i].endColor = color;
			}
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000A9918 File Offset: 0x000A7B18
		private float FindProjectileCollision(out RaycastHit hitInfo)
		{
			float num = this.arcDuration / (float)this.segmentCount;
			float num2 = 0f;
			hitInfo = default(RaycastHit);
			Vector3 vector = this.GetArcPositionAtTime(num2);
			for (int i = 0; i < this.segmentCount; i++)
			{
				float num3 = num2 + num;
				Vector3 arcPositionAtTime = this.GetArcPositionAtTime(num3);
				if (Physics.Linecast(vector, arcPositionAtTime, out hitInfo, this.traceLayerMask) && hitInfo.collider.GetComponent<IgnoreTeleportTrace>() == null)
				{
					Util.DrawCross(hitInfo.point, Color.red, 0.5f);
					float num4 = Vector3.Distance(vector, arcPositionAtTime);
					return num2 + num * (hitInfo.distance / num4);
				}
				num2 = num3;
				vector = arcPositionAtTime;
			}
			return float.MaxValue;
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000A99C8 File Offset: 0x000A7BC8
		public Vector3 GetArcPositionAtTime(float time)
		{
			Vector3 a = this.useGravity ? Physics.gravity : Vector3.zero;
			return this.startPos + (this.projectileVelocity * time + 0.5f * time * time * a);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000A9A18 File Offset: 0x000A7C18
		private void HideLineSegments(int startSegment, int endSegment)
		{
			if (this.lineRenderers != null)
			{
				for (int i = startSegment; i < endSegment; i++)
				{
					this.lineRenderers[i].enabled = false;
				}
			}
		}

		// Token: 0x04001FB8 RID: 8120
		public int segmentCount = 60;

		// Token: 0x04001FB9 RID: 8121
		public float thickness = 0.01f;

		// Token: 0x04001FBA RID: 8122
		[Tooltip("The amount of time in seconds to predict the motion of the projectile.")]
		public float arcDuration = 3f;

		// Token: 0x04001FBB RID: 8123
		[Tooltip("The amount of time in seconds between each segment of the projectile.")]
		public float segmentBreak = 0.025f;

		// Token: 0x04001FBC RID: 8124
		[Tooltip("The speed at which the line segments of the arc move.")]
		public float arcSpeed = 0.2f;

		// Token: 0x04001FBD RID: 8125
		public Material material;

		// Token: 0x04001FBE RID: 8126
		[HideInInspector]
		public int traceLayerMask;

		// Token: 0x04001FBF RID: 8127
		private LineRenderer[] lineRenderers;

		// Token: 0x04001FC0 RID: 8128
		private float arcTimeOffset;

		// Token: 0x04001FC1 RID: 8129
		private float prevThickness;

		// Token: 0x04001FC2 RID: 8130
		private int prevSegmentCount;

		// Token: 0x04001FC3 RID: 8131
		private bool showArc = true;

		// Token: 0x04001FC4 RID: 8132
		private Vector3 startPos;

		// Token: 0x04001FC5 RID: 8133
		private Vector3 projectileVelocity;

		// Token: 0x04001FC6 RID: 8134
		private bool useGravity = true;

		// Token: 0x04001FC7 RID: 8135
		private Transform arcObjectsTransfrom;

		// Token: 0x04001FC8 RID: 8136
		private bool arcInvalid;
	}
}
