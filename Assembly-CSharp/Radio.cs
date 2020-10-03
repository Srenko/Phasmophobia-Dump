using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200014C RID: 332
[RequireComponent(typeof(PhotonView))]
public class Radio : MonoBehaviour
{
	// Token: 0x060008BB RID: 2235 RVA: 0x00034B28 File Offset: 0x00032D28
	private void Awake()
	{
		if (!this.source)
		{
			this.source = base.GetComponent<AudioSource>();
		}
		if (!this.photonInteract)
		{
			this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		}
		if (!this.view)
		{
			this.view = base.GetComponent<PhotonView>();
		}
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x00034B94 File Offset: 0x00032D94
	private void Start()
	{
		this.isOn = false;
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
		this.source.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00034BE4 File Offset: 0x00032DE4
	private void Use()
	{
		this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00034BFC File Offset: 0x00032DFC
	[PunRPC]
	private void NetworkedUse()
	{
		this.isOn = !this.isOn;
		this.noise.gameObject.SetActive(this.isOn);
		if (this.isOn)
		{
			this.source.Play();
			return;
		}
		this.source.Stop();
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00034C4D File Offset: 0x00032E4D
	[PunRPC]
	private void TurnOn()
	{
		this.isOn = true;
		this.source.Play();
		this.noise.gameObject.SetActive(this.isOn);
	}

	// Token: 0x040008D1 RID: 2257
	public PhotonView view;

	// Token: 0x040008D2 RID: 2258
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x040008D3 RID: 2259
	[SerializeField]
	private Noise noise;

	// Token: 0x040008D4 RID: 2260
	private bool isOn;

	// Token: 0x040008D5 RID: 2261
	private AudioSource source;
}
