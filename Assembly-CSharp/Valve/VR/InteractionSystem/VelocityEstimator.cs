using System;
using System.Collections;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200043E RID: 1086
	public class VelocityEstimator : MonoBehaviour
	{
		// Token: 0x06002156 RID: 8534 RVA: 0x000A40BA File Offset: 0x000A22BA
		public void BeginEstimatingVelocity()
		{
			this.FinishEstimatingVelocity();
			this.routine = base.StartCoroutine(this.EstimateVelocityCoroutine());
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000A40D4 File Offset: 0x000A22D4
		public void FinishEstimatingVelocity()
		{
			if (this.routine != null)
			{
				base.StopCoroutine(this.routine);
				this.routine = null;
			}
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x000A40F4 File Offset: 0x000A22F4
		public Vector3 GetVelocityEstimate()
		{
			Vector3 vector = Vector3.zero;
			int num = Mathf.Min(this.sampleCount, this.velocitySamples.Length);
			if (num != 0)
			{
				for (int i = 0; i < num; i++)
				{
					vector += this.velocitySamples[i];
				}
				vector *= 1f / (float)num;
			}
			return vector;
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x000A4150 File Offset: 0x000A2350
		public Vector3 GetAngularVelocityEstimate()
		{
			Vector3 vector = Vector3.zero;
			int num = Mathf.Min(this.sampleCount, this.angularVelocitySamples.Length);
			if (num != 0)
			{
				for (int i = 0; i < num; i++)
				{
					vector += this.angularVelocitySamples[i];
				}
				vector *= 1f / (float)num;
			}
			return vector;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000A41AC File Offset: 0x000A23AC
		public Vector3 GetAccelerationEstimate()
		{
			Vector3 a = Vector3.zero;
			for (int i = 2 + this.sampleCount - this.velocitySamples.Length; i < this.sampleCount; i++)
			{
				if (i >= 2)
				{
					int num = i - 2;
					int num2 = i - 1;
					Vector3 b = this.velocitySamples[num % this.velocitySamples.Length];
					Vector3 a2 = this.velocitySamples[num2 % this.velocitySamples.Length];
					a += a2 - b;
				}
			}
			return a * (1f / Time.deltaTime);
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000A423D File Offset: 0x000A243D
		private void Awake()
		{
			this.velocitySamples = new Vector3[this.velocityAverageFrames];
			this.angularVelocitySamples = new Vector3[this.angularVelocityAverageFrames];
			if (this.estimateOnAwake)
			{
				this.BeginEstimatingVelocity();
			}
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000A426F File Offset: 0x000A246F
		private IEnumerator EstimateVelocityCoroutine()
		{
			this.sampleCount = 0;
			Vector3 previousPosition = base.transform.position;
			Quaternion previousRotation = base.transform.rotation;
			for (;;)
			{
				yield return new WaitForEndOfFrame();
				float num = 1f / Time.deltaTime;
				int num2 = this.sampleCount % this.velocitySamples.Length;
				int num3 = this.sampleCount % this.angularVelocitySamples.Length;
				this.sampleCount++;
				this.velocitySamples[num2] = num * (base.transform.position - previousPosition);
				Quaternion quaternion = base.transform.rotation * Quaternion.Inverse(previousRotation);
				float num4 = 2f * Mathf.Acos(Mathf.Clamp(quaternion.w, -1f, 1f));
				if (num4 > 3.14159274f)
				{
					num4 -= 6.28318548f;
				}
				Vector3 vector = new Vector3(quaternion.x, quaternion.y, quaternion.z);
				if (vector.sqrMagnitude > 0f)
				{
					vector = num4 * num * vector.normalized;
				}
				this.angularVelocitySamples[num3] = vector;
				previousPosition = base.transform.position;
				previousRotation = base.transform.rotation;
			}
			yield break;
		}

		// Token: 0x04001EB7 RID: 7863
		[Tooltip("How many frames to average over for computing velocity")]
		public int velocityAverageFrames = 5;

		// Token: 0x04001EB8 RID: 7864
		[Tooltip("How many frames to average over for computing angular velocity")]
		public int angularVelocityAverageFrames = 11;

		// Token: 0x04001EB9 RID: 7865
		public bool estimateOnAwake;

		// Token: 0x04001EBA RID: 7866
		private Coroutine routine;

		// Token: 0x04001EBB RID: 7867
		private int sampleCount;

		// Token: 0x04001EBC RID: 7868
		private Vector3[] velocitySamples;

		// Token: 0x04001EBD RID: 7869
		private Vector3[] angularVelocitySamples;
	}
}
