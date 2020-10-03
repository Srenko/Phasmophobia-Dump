using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200016C RID: 364
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(AudioSource))]
public class Torch : MonoBehaviour
{
	// Token: 0x060009B3 RID: 2483 RVA: 0x0003BB7D File Offset: 0x00039D7D
	private void Awake()
	{
		this.startIntensity = this.myLight.intensity;
		this.noise.gameObject.SetActive(false);
		this.blinkTimer = Random.Range(0f, 0.5f);
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x0003BBB8 File Offset: 0x00039DB8
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
		this.myLight.enabled = false;
		if (this.isBlacklight)
		{
			this.glass.material.DisableKeyword("_EMISSION");
			this.bulb.material.DisableKeyword("_EMISSION");
		}
		else
		{
			for (int i = 0; i < this.rend.materials.Length; i++)
			{
				this.rend.materials[i].DisableKeyword("_EMISSION");
			}
		}
		if (LevelController.instance)
		{
			LevelController.instance.torches.Add(this);
		}
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x0003BC68 File Offset: 0x00039E68
	public void Use()
	{
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("NetworkedUse", PhotonTargets.AllBuffered, new object[]
			{
				PhotonNetwork.player.ID
			});
			return;
		}
		this.NetworkedUse(PhotonNetwork.player.ID);
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x0003BCB8 File Offset: 0x00039EB8
	[PunRPC]
	private void NetworkedUse(int actorID)
	{
		this.myLight.enabled = !this.myLight.enabled;
		this.source.outputAudioMixerGroup = SoundController.instance.GetPlayersAudioGroup(actorID);
		this.source.Play();
		if (this.myLight.enabled)
		{
			if (this.isBlacklight)
			{
				this.glass.material.EnableKeyword("_EMISSION");
				this.bulb.material.EnableKeyword("_EMISSION");
			}
			else
			{
				for (int i = 0; i < this.rend.materials.Length; i++)
				{
					this.rend.materials[i].EnableKeyword("_EMISSION");
				}
			}
		}
		else if (this.isBlacklight)
		{
			this.glass.material.DisableKeyword("_EMISSION");
			this.bulb.material.DisableKeyword("_EMISSION");
		}
		else
		{
			for (int j = 0; j < this.rend.materials.Length; j++)
			{
				this.rend.materials[j].DisableKeyword("_EMISSION");
			}
		}
		base.StartCoroutine(this.PlayNoiseObject());
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0003BDE4 File Offset: 0x00039FE4
	public void StartTrailerFlicker()
	{
		base.StartCoroutine(this.TrailerFlicker());
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0003BDF3 File Offset: 0x00039FF3
	private IEnumerator TrailerFlicker()
	{
		yield return new WaitForSeconds(1f);
		this.myLight.intensity = 1.7f;
		yield return new WaitForSeconds(0.15f);
		this.myLight.intensity = 0.6f;
		yield return new WaitForSeconds(0.2f);
		this.myLight.intensity = 1.3f;
		yield return new WaitForSeconds(0.1f);
		this.myLight.intensity = 0.9f;
		yield return new WaitForSeconds(0.15f);
		this.myLight.intensity = 1.2f;
		yield return new WaitForSeconds(0.25f);
		this.myLight.intensity = 0f;
		for (int i = 0; i < this.rend.materials.Length; i++)
		{
			this.rend.materials[i].DisableKeyword("_EMISSION");
		}
		yield break;
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0003BE04 File Offset: 0x0003A004
	public void TurnBlinkOff()
	{
		this.myLight.intensity = this.startIntensity;
		if (this.myLight.enabled)
		{
			if (this.isBlacklight)
			{
				this.glass.material.EnableKeyword("_EMISSION");
				this.bulb.material.EnableKeyword("_EMISSION");
				return;
			}
			for (int i = 0; i < this.rend.materials.Length; i++)
			{
				if (this.rend.materials[i] != null)
				{
					this.rend.materials[i].EnableKeyword("_EMISSION");
				}
			}
			return;
		}
		else
		{
			if (this.isBlacklight)
			{
				this.glass.material.DisableKeyword("_EMISSION");
				this.bulb.material.DisableKeyword("_EMISSION");
				return;
			}
			for (int j = 0; j < this.rend.materials.Length; j++)
			{
				if (this.rend.materials[j] != null)
				{
					this.rend.materials[j].DisableKeyword("_EMISSION");
				}
			}
			return;
		}
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x0003BF20 File Offset: 0x0003A120
	private void Update()
	{
		if (LevelController.instance == null)
		{
			return;
		}
		if (LevelController.instance.currentGhost == null)
		{
			return;
		}
		if (LevelController.instance.currentGhost.isHunting)
		{
			this.blinkTimer -= Time.deltaTime;
			if (this.blinkTimer < 0f)
			{
				this.Blink();
				this.blinkTimer = Random.Range(0.1f, 0.5f);
			}
		}
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0003BF9C File Offset: 0x0003A19C
	private void Blink()
	{
		if (this.myLight.intensity == 0f)
		{
			this.myLight.intensity = this.startIntensity;
			if (this.isBlacklight)
			{
				this.glass.material.EnableKeyword("_EMISSION");
				this.bulb.material.EnableKeyword("_EMISSION");
				return;
			}
			for (int i = 0; i < this.rend.materials.Length; i++)
			{
				this.rend.materials[i].EnableKeyword("_EMISSION");
			}
			return;
		}
		else
		{
			this.myLight.intensity = 0f;
			if (this.isBlacklight)
			{
				this.glass.material.DisableKeyword("_EMISSION");
				this.bulb.material.DisableKeyword("_EMISSION");
				return;
			}
			for (int j = 0; j < this.rend.materials.Length; j++)
			{
				this.rend.materials[j].DisableKeyword("_EMISSION");
			}
			return;
		}
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0003C0A1 File Offset: 0x0003A2A1
	private IEnumerator PlayNoiseObject()
	{
		this.noise.gameObject.SetActive(true);
		yield return 0;
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x040009CF RID: 2511
	public Light myLight;

	// Token: 0x040009D0 RID: 2512
	public AudioSource source;

	// Token: 0x040009D1 RID: 2513
	[SerializeField]
	private Renderer rend;

	// Token: 0x040009D2 RID: 2514
	[SerializeField]
	private PhotonView view;

	// Token: 0x040009D3 RID: 2515
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x040009D4 RID: 2516
	public bool isBlacklight;

	// Token: 0x040009D5 RID: 2517
	[SerializeField]
	private Renderer glass;

	// Token: 0x040009D6 RID: 2518
	[SerializeField]
	private Renderer bulb;

	// Token: 0x040009D7 RID: 2519
	[SerializeField]
	private Noise noise;

	// Token: 0x040009D8 RID: 2520
	private float startIntensity;

	// Token: 0x040009D9 RID: 2521
	private float blinkTimer;
}
