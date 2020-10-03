using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000355 RID: 853
	public class FireExtinguisher_Sprayer : VRTK_InteractableObject
	{
		// Token: 0x06001D9F RID: 7583 RVA: 0x00097064 File Offset: 0x00095264
		public void Spray(float power)
		{
			if (power <= 0f)
			{
				this.particles.Stop();
			}
			if (power > 0f)
			{
				if (this.particles.isPaused || this.particles.isStopped)
				{
					this.particles.Play();
				}
				this.particles.main.startSpeedMultiplier = this.maxSprayPower * power;
			}
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x000970CC File Offset: 0x000952CC
		protected override void Awake()
		{
			base.Awake();
			this.waterSpray = base.transform.Find("WaterSpray").gameObject;
			this.particles = this.waterSpray.GetComponent<ParticleSystem>();
			this.particles.Stop();
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0009710B File Offset: 0x0009530B
		protected override void Update()
		{
			base.Update();
			if (Vector3.Distance(base.transform.position, this.baseCan.transform.position) > this.breakDistance)
			{
				this.ForceStopInteracting();
			}
		}

		// Token: 0x04001759 RID: 5977
		public FireExtinguisher_Base baseCan;

		// Token: 0x0400175A RID: 5978
		public float breakDistance = 0.12f;

		// Token: 0x0400175B RID: 5979
		public float maxSprayPower = 5f;

		// Token: 0x0400175C RID: 5980
		private GameObject waterSpray;

		// Token: 0x0400175D RID: 5981
		private ParticleSystem particles;
	}
}
