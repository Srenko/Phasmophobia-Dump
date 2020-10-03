using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000447 RID: 1095
	public class BalloonSpawner : MonoBehaviour
	{
		// Token: 0x060021AC RID: 8620 RVA: 0x000A6B24 File Offset: 0x000A4D24
		private void Start()
		{
			if (this.balloonPrefab == null)
			{
				return;
			}
			if (this.autoSpawn && this.spawnAtStartup)
			{
				this.SpawnBalloon(this.color);
				this.nextSpawnTime = Random.Range(this.minSpawnTime, this.maxSpawnTime) + Time.time;
			}
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000A6B7C File Offset: 0x000A4D7C
		private void Update()
		{
			if (this.balloonPrefab == null)
			{
				return;
			}
			if (Time.time > this.nextSpawnTime && this.autoSpawn)
			{
				this.SpawnBalloon(this.color);
				this.nextSpawnTime = Random.Range(this.minSpawnTime, this.maxSpawnTime) + Time.time;
			}
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x000A6BD8 File Offset: 0x000A4DD8
		public GameObject SpawnBalloon(Balloon.BalloonColor color = Balloon.BalloonColor.Red)
		{
			if (this.balloonPrefab == null)
			{
				return null;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.balloonPrefab, base.transform.position, base.transform.rotation);
			gameObject.transform.localScale = new Vector3(this.scale, this.scale, this.scale);
			if (this.attachBalloon)
			{
				gameObject.transform.parent = base.transform;
			}
			if (this.sendSpawnMessageToParent && base.transform.parent != null)
			{
				base.transform.parent.SendMessage("OnBalloonSpawned", gameObject, SendMessageOptions.DontRequireReceiver);
			}
			if (this.playSounds)
			{
				if (this.inflateSound != null)
				{
					this.inflateSound.Play();
				}
				if (this.stretchSound != null)
				{
					this.stretchSound.Play();
				}
			}
			gameObject.GetComponentInChildren<Balloon>().SetColor(color);
			if (this.spawnDirectionTransform != null)
			{
				gameObject.GetComponentInChildren<Rigidbody>().AddForce(this.spawnDirectionTransform.forward * this.spawnForce);
			}
			return gameObject;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000A6CFA File Offset: 0x000A4EFA
		public void SpawnBalloonFromEvent(int color)
		{
			this.SpawnBalloon((Balloon.BalloonColor)color);
		}

		// Token: 0x04001F0D RID: 7949
		public float minSpawnTime = 5f;

		// Token: 0x04001F0E RID: 7950
		public float maxSpawnTime = 15f;

		// Token: 0x04001F0F RID: 7951
		private float nextSpawnTime;

		// Token: 0x04001F10 RID: 7952
		public GameObject balloonPrefab;

		// Token: 0x04001F11 RID: 7953
		public bool autoSpawn = true;

		// Token: 0x04001F12 RID: 7954
		public bool spawnAtStartup = true;

		// Token: 0x04001F13 RID: 7955
		public bool playSounds = true;

		// Token: 0x04001F14 RID: 7956
		public SoundPlayOneshot inflateSound;

		// Token: 0x04001F15 RID: 7957
		public SoundPlayOneshot stretchSound;

		// Token: 0x04001F16 RID: 7958
		public bool sendSpawnMessageToParent;

		// Token: 0x04001F17 RID: 7959
		public float scale = 1f;

		// Token: 0x04001F18 RID: 7960
		public Transform spawnDirectionTransform;

		// Token: 0x04001F19 RID: 7961
		public float spawnForce;

		// Token: 0x04001F1A RID: 7962
		public bool attachBalloon;

		// Token: 0x04001F1B RID: 7963
		public Balloon.BalloonColor color = Balloon.BalloonColor.Random;
	}
}
