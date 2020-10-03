using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000418 RID: 1048
	[RequireComponent(typeof(ParticleSystem))]
	public class DestroyOnParticleSystemDeath : MonoBehaviour
	{
		// Token: 0x06002058 RID: 8280 RVA: 0x0009FB64 File Offset: 0x0009DD64
		private void Awake()
		{
			this.particles = base.GetComponent<ParticleSystem>();
			base.InvokeRepeating("CheckParticleSystem", 0.1f, 0.1f);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x0009FB87 File Offset: 0x0009DD87
		private void CheckParticleSystem()
		{
			if (!this.particles.IsAlive())
			{
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x04001DE5 RID: 7653
		private ParticleSystem particles;
	}
}
