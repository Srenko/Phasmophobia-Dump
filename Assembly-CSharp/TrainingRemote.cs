using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200016D RID: 365
public class TrainingRemote : MonoBehaviour
{
	// Token: 0x060009BE RID: 2494 RVA: 0x0003C0B0 File Offset: 0x0003A2B0
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.source = base.GetComponent<AudioSource>();
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0003C0D6 File Offset: 0x0003A2D6
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0003C0EF File Offset: 0x0003A2EF
	private void Use()
	{
		TrainingController.instance.NextSlide();
		this.source.Play();
	}

	// Token: 0x040009DA RID: 2522
	private PhotonView view;

	// Token: 0x040009DB RID: 2523
	private PhotonObjectInteract photonInteract;

	// Token: 0x040009DC RID: 2524
	private AudioSource source;
}
