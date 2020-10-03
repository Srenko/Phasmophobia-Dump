using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000449 RID: 1097
	public class FireSource : MonoBehaviour
	{
		// Token: 0x060021B3 RID: 8627 RVA: 0x000A6D85 File Offset: 0x000A4F85
		private void Start()
		{
			if (this.startActive)
			{
				this.StartBurning();
			}
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000A6D98 File Offset: 0x000A4F98
		private void Update()
		{
			if (this.burnTime != 0f && Time.time > this.ignitionTime + this.burnTime && this.isBurning)
			{
				this.isBurning = false;
				if (this.customParticles != null)
				{
					this.customParticles.Stop();
					return;
				}
				Object.Destroy(this.fireObject);
			}
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000A6DFA File Offset: 0x000A4FFA
		private void OnTriggerEnter(Collider other)
		{
			if (this.isBurning && this.canSpreadFromThisSource)
			{
				other.SendMessageUpwards("FireExposure", SendMessageOptions.DontRequireReceiver);
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000A6E18 File Offset: 0x000A5018
		private void FireExposure()
		{
			if (this.fireObject == null)
			{
				base.Invoke("StartBurning", this.ignitionDelay);
			}
			if (this.hand = base.GetComponentInParent<Hand>())
			{
				this.hand.controller.TriggerHapticPulse(1000, EVRButtonId.k_EButton_Axis0);
			}
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000A6E74 File Offset: 0x000A5074
		private void StartBurning()
		{
			this.isBurning = true;
			this.ignitionTime = Time.time;
			if (this.ignitionSound != null)
			{
				this.ignitionSound.Play();
			}
			if (this.customParticles != null)
			{
				this.customParticles.Play();
				return;
			}
			if (this.fireParticlePrefab != null)
			{
				this.fireObject = Object.Instantiate<GameObject>(this.fireParticlePrefab, base.transform.position, base.transform.rotation);
				this.fireObject.transform.parent = base.transform;
			}
		}

		// Token: 0x04001F1C RID: 7964
		public GameObject fireParticlePrefab;

		// Token: 0x04001F1D RID: 7965
		public bool startActive;

		// Token: 0x04001F1E RID: 7966
		private GameObject fireObject;

		// Token: 0x04001F1F RID: 7967
		public ParticleSystem customParticles;

		// Token: 0x04001F20 RID: 7968
		public bool isBurning;

		// Token: 0x04001F21 RID: 7969
		public float burnTime;

		// Token: 0x04001F22 RID: 7970
		public float ignitionDelay;

		// Token: 0x04001F23 RID: 7971
		private float ignitionTime;

		// Token: 0x04001F24 RID: 7972
		private Hand hand;

		// Token: 0x04001F25 RID: 7973
		public AudioSource ignitionSound;

		// Token: 0x04001F26 RID: 7974
		public bool canSpreadFromThisSource = true;
	}
}
