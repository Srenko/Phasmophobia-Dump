using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class Lightning : MonoBehaviour
{
	// Token: 0x06000119 RID: 281 RVA: 0x00008F45 File Offset: 0x00007145
	private void Start()
	{
		base.StartCoroutine("Storm");
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00008F53 File Offset: 0x00007153
	private IEnumerator Storm()
	{
		for (;;)
		{
			yield return new WaitForSeconds(Random.Range(this.offMin, this.offMax));
			this.LightningBolt.SetActive(true);
			this.LightningBolt.transform.Rotate(0f, (float)Random.Range(1, 360), 0f);
			base.StartCoroutine("Soundfx");
			yield return new WaitForSeconds(Random.Range(this.onMin, this.onMax));
			this.LightningBolt.SetActive(false);
		}
		yield break;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00008F62 File Offset: 0x00007162
	private IEnumerator Soundfx()
	{
		this.ThunderRND = Random.Range(1, 5);
		this.ThunderVol = Random.Range(0.2f, 1f);
		this.ThunderWait = 9f - this.ThunderVol * 3f * 3f - 2f;
		while (this.ThunderRND == 1)
		{
			yield return new WaitForSeconds(this.ThunderWait);
			this.ThunderAudioA.volume = this.ThunderVol;
			this.ThunderAudioA.Play();
			this.ThunderRND = 0;
		}
		while (this.ThunderRND == 2)
		{
			yield return new WaitForSeconds(this.ThunderWait);
			this.ThunderAudioB.volume = this.ThunderVol;
			this.ThunderAudioB.Play();
			this.ThunderRND = 0;
		}
		while (this.ThunderRND == 3)
		{
			yield return new WaitForSeconds(this.ThunderWait);
			this.ThunderAudioC.volume = this.ThunderVol;
			this.ThunderAudioC.Play();
			this.ThunderRND = 0;
		}
		while (this.ThunderRND == 4)
		{
			yield return new WaitForSeconds(this.ThunderWait);
			this.ThunderAudioD.volume = this.ThunderVol;
			this.ThunderAudioD.Play();
			this.ThunderRND = 0;
		}
		yield break;
	}

	// Token: 0x04000154 RID: 340
	public float offMin = 10f;

	// Token: 0x04000155 RID: 341
	public float offMax = 60f;

	// Token: 0x04000156 RID: 342
	public AudioSource ThunderAudioA;

	// Token: 0x04000157 RID: 343
	public AudioSource ThunderAudioB;

	// Token: 0x04000158 RID: 344
	public AudioSource ThunderAudioC;

	// Token: 0x04000159 RID: 345
	public AudioSource ThunderAudioD;

	// Token: 0x0400015A RID: 346
	public GameObject LightningBolt;

	// Token: 0x0400015B RID: 347
	private float onMin = 0.25f;

	// Token: 0x0400015C RID: 348
	private float onMax = 2f;

	// Token: 0x0400015D RID: 349
	private int ThunderRND = 1;

	// Token: 0x0400015E RID: 350
	private float ThunderVol;

	// Token: 0x0400015F RID: 351
	private float ThunderWait;
}
