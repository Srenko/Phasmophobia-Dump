using System;
using UnityEngine;

// Token: 0x020001B4 RID: 436
public class Breath : MonoBehaviour
{
	// Token: 0x06000BE4 RID: 3044 RVA: 0x0004A558 File Offset: 0x00048758
	private void Awake()
	{
		this.particles = base.GetComponent<ParticleSystemRenderer>();
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x0004A566 File Offset: 0x00048766
	private void Start()
	{
		this.particles.maxParticleSize = 0f;
		this.strength = 0f;
		if (MainManager.instance)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x0004A598 File Offset: 0x00048798
	private void Update()
	{
		if (LevelController.instance)
		{
			if (this.player.currentRoom.temperature < 0f)
			{
				this.strength = this.player.currentRoom.temperature * -1f * 10f;
				this.particles.maxParticleSize = this.strength / 3f;
				return;
			}
			this.strength = 0f;
			this.particles.maxParticleSize = 0f;
		}
	}

	// Token: 0x04000C27 RID: 3111
	[HideInInspector]
	public ParticleSystemRenderer particles;

	// Token: 0x04000C28 RID: 3112
	private float strength;

	// Token: 0x04000C29 RID: 3113
	[SerializeField]
	private Player player;
}
