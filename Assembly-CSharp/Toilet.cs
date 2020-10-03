using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000150 RID: 336
public class Toilet : MonoBehaviour
{
	// Token: 0x060008D3 RID: 2259 RVA: 0x0003506E File Offset: 0x0003326E
	private void Awake()
	{
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.source = base.GetComponent<AudioSource>();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00035094 File Offset: 0x00033294
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x000350AD File Offset: 0x000332AD
	private void Use()
	{
		this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x000350C5 File Offset: 0x000332C5
	[PunRPC]
	private void NetworkedUse()
	{
		if (this.source.isPlaying)
		{
			return;
		}
		this.source.Play();
	}

	// Token: 0x040008E9 RID: 2281
	private PhotonObjectInteract photonInteract;

	// Token: 0x040008EA RID: 2282
	private AudioSource source;

	// Token: 0x040008EB RID: 2283
	private PhotonView view;
}
