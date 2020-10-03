using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class LightningController : MonoBehaviour
{
	// Token: 0x0600079C RID: 1948 RVA: 0x0002D4BA File Offset: 0x0002B6BA
	private void Awake()
	{
		this.myLight = base.GetComponent<Light>();
		this.source = base.GetComponent<AudioSource>();
		this.view = base.GetComponent<PhotonView>();
		LightningController.instance = this;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0002D4E8 File Offset: 0x0002B6E8
	private void Start()
	{
		this.defaultExposure = RenderSettings.skybox.GetFloat("_Exposure");
		this.timer = Random.Range(30f, 100f);
		this.myLight.intensity = 0f;
		this.isUsing = false;
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0002D538 File Offset: 0x0002B738
	private void Update()
	{
		if (!this.view.isMine)
		{
			return;
		}
		if (!this.isUsing)
		{
			this.timer -= Time.deltaTime;
			if (this.timer < 0f)
			{
				this.PlayLightning();
				this.isUsing = true;
			}
		}
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x0002D587 File Offset: 0x0002B787
	public void PlayLightning()
	{
		this.view.RPC("PlayLightningNetworked", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0002D5A0 File Offset: 0x0002B7A0
	[PunRPC]
	private void PlayLightningNetworked()
	{
		if (!GameController.instance.myPlayer.player.isDead)
		{
			base.StartCoroutine(this.Lightning(Random.Range(0f, 2f), Random.Range(0f, 1f)));
		}
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0002D5EE File Offset: 0x0002B7EE
	private IEnumerator Lightning(float rand1, float rand2)
	{
		base.Invoke("PlaySound", rand1);
		while ((double)this.myLight.intensity < 0.95)
		{
			this.myLight.intensity = Mathf.Lerp(this.myLight.intensity, 1f, Time.deltaTime * 1f);
			RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(RenderSettings.skybox.GetFloat("_Exposure"), 2f, Time.deltaTime * 4f));
		}
		yield return new WaitForSeconds(rand2);
		this.myLight.intensity = 0f;
		RenderSettings.skybox.SetFloat("_Exposure", this.defaultExposure);
		this.timer = Random.Range(30f, 100f);
		this.isUsing = false;
		yield break;
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0002D60C File Offset: 0x0002B80C
	private void PlaySound()
	{
		this.source.volume = Random.Range(0.005f, 0.01f);
		this.source.clip = this.clips[Random.Range(0, this.clips.Count)];
		this.source.Play();
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0002D665 File Offset: 0x0002B865
	private void OnDisable()
	{
		RenderSettings.skybox.SetFloat("_Exposure", this.defaultExposure);
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0002D665 File Offset: 0x0002B865
	private void OnDestroy()
	{
		RenderSettings.skybox.SetFloat("_Exposure", this.defaultExposure);
	}

	// Token: 0x04000770 RID: 1904
	private AudioSource source;

	// Token: 0x04000771 RID: 1905
	private Light myLight;

	// Token: 0x04000772 RID: 1906
	public List<AudioClip> clips = new List<AudioClip>();

	// Token: 0x04000773 RID: 1907
	private PhotonView view;

	// Token: 0x04000774 RID: 1908
	private float timer = 5f;

	// Token: 0x04000775 RID: 1909
	private bool isUsing;

	// Token: 0x04000776 RID: 1910
	private float defaultExposure;

	// Token: 0x04000777 RID: 1911
	public static LightningController instance;
}
