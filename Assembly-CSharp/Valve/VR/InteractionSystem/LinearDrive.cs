using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200042E RID: 1070
	[RequireComponent(typeof(Interactable))]
	public class LinearDrive : MonoBehaviour
	{
		// Token: 0x060020CA RID: 8394 RVA: 0x000A1B61 File Offset: 0x0009FD61
		private void Awake()
		{
			this.mappingChangeSamples = new float[this.numMappingChangeSamples];
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000A1B74 File Offset: 0x0009FD74
		private void Start()
		{
			if (this.linearMapping == null)
			{
				this.linearMapping = base.GetComponent<LinearMapping>();
			}
			if (this.linearMapping == null)
			{
				this.linearMapping = base.gameObject.AddComponent<LinearMapping>();
			}
			this.initialMappingOffset = this.linearMapping.value;
			if (this.repositionGameObject)
			{
				this.UpdateLinearMapping(base.transform);
			}
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000A1BE0 File Offset: 0x0009FDE0
		private void HandHoverUpdate(Hand hand)
		{
			if (hand.GetStandardInteractionButtonDown())
			{
				hand.HoverLock(base.GetComponent<Interactable>());
				this.initialMappingOffset = this.linearMapping.value - this.CalculateLinearMapping(hand.transform);
				this.sampleCount = 0;
				this.mappingChangeRate = 0f;
			}
			if (hand.GetStandardInteractionButtonUp())
			{
				hand.HoverUnlock(base.GetComponent<Interactable>());
				this.CalculateMappingChangeRate();
			}
			if (hand.GetStandardInteractionButton())
			{
				this.UpdateLinearMapping(hand.transform);
			}
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000A1C60 File Offset: 0x0009FE60
		private void CalculateMappingChangeRate()
		{
			this.mappingChangeRate = 0f;
			int num = Mathf.Min(this.sampleCount, this.mappingChangeSamples.Length);
			if (num != 0)
			{
				for (int i = 0; i < num; i++)
				{
					this.mappingChangeRate += this.mappingChangeSamples[i];
				}
				this.mappingChangeRate /= (float)num;
			}
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000A1CC0 File Offset: 0x0009FEC0
		private void UpdateLinearMapping(Transform tr)
		{
			this.prevMapping = this.linearMapping.value;
			this.linearMapping.value = Mathf.Clamp01(this.initialMappingOffset + this.CalculateLinearMapping(tr));
			this.mappingChangeSamples[this.sampleCount % this.mappingChangeSamples.Length] = 1f / Time.deltaTime * (this.linearMapping.value - this.prevMapping);
			this.sampleCount++;
			if (this.repositionGameObject)
			{
				base.transform.position = Vector3.Lerp(this.startPosition.position, this.endPosition.position, this.linearMapping.value);
			}
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000A1D78 File Offset: 0x0009FF78
		private float CalculateLinearMapping(Transform tr)
		{
			Vector3 rhs = this.endPosition.position - this.startPosition.position;
			float magnitude = rhs.magnitude;
			rhs.Normalize();
			return Vector3.Dot(tr.position - this.startPosition.position, rhs) / magnitude;
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000A1DD0 File Offset: 0x0009FFD0
		private void Update()
		{
			if (this.maintainMomemntum && this.mappingChangeRate != 0f)
			{
				this.mappingChangeRate = Mathf.Lerp(this.mappingChangeRate, 0f, this.momemtumDampenRate * Time.deltaTime);
				this.linearMapping.value = Mathf.Clamp01(this.linearMapping.value + this.mappingChangeRate * Time.deltaTime);
				if (this.repositionGameObject)
				{
					base.transform.position = Vector3.Lerp(this.startPosition.position, this.endPosition.position, this.linearMapping.value);
				}
			}
		}

		// Token: 0x04001E50 RID: 7760
		public Transform startPosition;

		// Token: 0x04001E51 RID: 7761
		public Transform endPosition;

		// Token: 0x04001E52 RID: 7762
		public LinearMapping linearMapping;

		// Token: 0x04001E53 RID: 7763
		public bool repositionGameObject = true;

		// Token: 0x04001E54 RID: 7764
		public bool maintainMomemntum = true;

		// Token: 0x04001E55 RID: 7765
		public float momemtumDampenRate = 5f;

		// Token: 0x04001E56 RID: 7766
		private float initialMappingOffset;

		// Token: 0x04001E57 RID: 7767
		private int numMappingChangeSamples = 5;

		// Token: 0x04001E58 RID: 7768
		private float[] mappingChangeSamples;

		// Token: 0x04001E59 RID: 7769
		private float prevMapping;

		// Token: 0x04001E5A RID: 7770
		private float mappingChangeRate;

		// Token: 0x04001E5B RID: 7771
		private int sampleCount;
	}
}
