using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000444 RID: 1092
	public class Balloon : MonoBehaviour
	{
		// Token: 0x0600219B RID: 8603 RVA: 0x000A6324 File Offset: 0x000A4524
		private void Start()
		{
			this.destructTime = Time.time + this.lifetime + Random.value;
			this.hand = base.GetComponentInParent<Hand>();
			this.balloonRigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000A6358 File Offset: 0x000A4558
		private void Update()
		{
			if (this.destructTime != 0f && Time.time > this.destructTime)
			{
				if (this.burstOnLifetimeEnd)
				{
					this.SpawnParticles(this.lifetimeEndParticlePrefab, this.lifetimeEndSound);
				}
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000A63A4 File Offset: 0x000A45A4
		private void SpawnParticles(GameObject particlePrefab, SoundPlayOneshot sound)
		{
			if (this.bParticlesSpawned)
			{
				return;
			}
			this.bParticlesSpawned = true;
			if (particlePrefab != null)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(particlePrefab, base.transform.position, base.transform.rotation);
				gameObject.GetComponent<ParticleSystem>().Play();
				Object.Destroy(gameObject, 2f);
			}
			if (sound != null)
			{
				if (Time.time - Balloon.s_flLastDeathSound < 0.1f)
				{
					sound.volMax *= 0.25f;
					sound.volMin *= 0.25f;
				}
				sound.Play();
				Balloon.s_flLastDeathSound = Time.time;
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000A644C File Offset: 0x000A464C
		private void FixedUpdate()
		{
			if (this.balloonRigidbody.velocity.sqrMagnitude > this.maxVelocity)
			{
				this.balloonRigidbody.velocity *= 0.97f;
			}
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000A648F File Offset: 0x000A468F
		private void ApplyDamage()
		{
			this.SpawnParticles(this.popPrefab, null);
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000A64AC File Offset: 0x000A46AC
		private void OnCollisionEnter(Collision collision)
		{
			if (this.bParticlesSpawned)
			{
				return;
			}
			Hand x = null;
			BalloonHapticBump component = collision.gameObject.GetComponent<BalloonHapticBump>();
			if (component != null && component.physParent != null)
			{
				x = component.physParent.GetComponentInParent<Hand>();
			}
			if (Time.time > this.lastSoundTime + this.soundDelay)
			{
				if (x != null)
				{
					if (Time.time > this.releaseTime + this.soundDelay)
					{
						this.collisionSound.Play();
						this.lastSoundTime = Time.time;
					}
				}
				else
				{
					this.collisionSound.Play();
					this.lastSoundTime = Time.time;
				}
			}
			if (this.destructTime > 0f)
			{
				return;
			}
			if (this.balloonRigidbody.velocity.magnitude > this.maxVelocity * 10f)
			{
				this.balloonRigidbody.velocity = this.balloonRigidbody.velocity.normalized * this.maxVelocity;
			}
			if (this.hand != null)
			{
				ushort durationMicroSec = (ushort)Mathf.Clamp(Util.RemapNumber(collision.relativeVelocity.magnitude, 0f, 3f, 500f, 800f), 500f, 800f);
				this.hand.controller.TriggerHapticPulse(durationMicroSec, EVRButtonId.k_EButton_Axis0);
			}
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000A6603 File Offset: 0x000A4803
		public void SetColor(Balloon.BalloonColor color)
		{
			base.GetComponentInChildren<MeshRenderer>().material.color = this.BalloonColorToRGB(color);
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000A661C File Offset: 0x000A481C
		private Color BalloonColorToRGB(Balloon.BalloonColor balloonColorVar)
		{
			Color result = new Color(255f, 0f, 0f);
			switch (balloonColorVar)
			{
			case Balloon.BalloonColor.Red:
				return new Color(237f, 29f, 37f, 255f) / 255f;
			case Balloon.BalloonColor.OrangeRed:
				return new Color(241f, 91f, 35f, 255f) / 255f;
			case Balloon.BalloonColor.Orange:
				return new Color(245f, 140f, 31f, 255f) / 255f;
			case Balloon.BalloonColor.YellowOrange:
				return new Color(253f, 185f, 19f, 255f) / 255f;
			case Balloon.BalloonColor.Yellow:
				return new Color(254f, 243f, 0f, 255f) / 255f;
			case Balloon.BalloonColor.GreenYellow:
				return new Color(172f, 209f, 54f, 255f) / 255f;
			case Balloon.BalloonColor.Green:
				return new Color(0f, 167f, 79f, 255f) / 255f;
			case Balloon.BalloonColor.BlueGreen:
				return new Color(108f, 202f, 189f, 255f) / 255f;
			case Balloon.BalloonColor.Blue:
				return new Color(0f, 119f, 178f, 255f) / 255f;
			case Balloon.BalloonColor.VioletBlue:
				return new Color(82f, 80f, 162f, 255f) / 255f;
			case Balloon.BalloonColor.Violet:
				return new Color(102f, 46f, 143f, 255f) / 255f;
			case Balloon.BalloonColor.RedViolet:
				return new Color(182f, 36f, 102f, 255f) / 255f;
			case Balloon.BalloonColor.LightGray:
				return new Color(192f, 192f, 192f, 255f) / 255f;
			case Balloon.BalloonColor.DarkGray:
				return new Color(128f, 128f, 128f, 255f) / 255f;
			case Balloon.BalloonColor.Random:
			{
				int balloonColorVar2 = Random.Range(0, 12);
				return this.BalloonColorToRGB((Balloon.BalloonColor)balloonColorVar2);
			}
			default:
				return result;
			}
		}

		// Token: 0x04001EF9 RID: 7929
		private Hand hand;

		// Token: 0x04001EFA RID: 7930
		public GameObject popPrefab;

		// Token: 0x04001EFB RID: 7931
		public float maxVelocity = 5f;

		// Token: 0x04001EFC RID: 7932
		public float lifetime = 15f;

		// Token: 0x04001EFD RID: 7933
		public bool burstOnLifetimeEnd;

		// Token: 0x04001EFE RID: 7934
		public GameObject lifetimeEndParticlePrefab;

		// Token: 0x04001EFF RID: 7935
		public SoundPlayOneshot lifetimeEndSound;

		// Token: 0x04001F00 RID: 7936
		private float destructTime;

		// Token: 0x04001F01 RID: 7937
		private float releaseTime = 99999f;

		// Token: 0x04001F02 RID: 7938
		public SoundPlayOneshot collisionSound;

		// Token: 0x04001F03 RID: 7939
		private float lastSoundTime;

		// Token: 0x04001F04 RID: 7940
		private float soundDelay = 0.2f;

		// Token: 0x04001F05 RID: 7941
		private Rigidbody balloonRigidbody;

		// Token: 0x04001F06 RID: 7942
		private bool bParticlesSpawned;

		// Token: 0x04001F07 RID: 7943
		private static float s_flLastDeathSound;

		// Token: 0x02000787 RID: 1927
		public enum BalloonColor
		{
			// Token: 0x04002958 RID: 10584
			Red,
			// Token: 0x04002959 RID: 10585
			OrangeRed,
			// Token: 0x0400295A RID: 10586
			Orange,
			// Token: 0x0400295B RID: 10587
			YellowOrange,
			// Token: 0x0400295C RID: 10588
			Yellow,
			// Token: 0x0400295D RID: 10589
			GreenYellow,
			// Token: 0x0400295E RID: 10590
			Green,
			// Token: 0x0400295F RID: 10591
			BlueGreen,
			// Token: 0x04002960 RID: 10592
			Blue,
			// Token: 0x04002961 RID: 10593
			VioletBlue,
			// Token: 0x04002962 RID: 10594
			Violet,
			// Token: 0x04002963 RID: 10595
			RedViolet,
			// Token: 0x04002964 RID: 10596
			LightGray,
			// Token: 0x04002965 RID: 10597
			DarkGray,
			// Token: 0x04002966 RID: 10598
			Random
		}
	}
}
