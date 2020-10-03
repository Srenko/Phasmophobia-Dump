using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001CC RID: 460
public class TruckMapSwitch : MonoBehaviour
{
	// Token: 0x06000C96 RID: 3222 RVA: 0x00050581 File Offset: 0x0004E781
	private void Awake()
	{
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x0005059B File Offset: 0x0004E79B
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x000505B4 File Offset: 0x0004E7B4
	private void Use()
	{
		if (GameController.instance.allPlayersAreConnected)
		{
			this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
		}
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x000505D8 File Offset: 0x0004E7D8
	[PunRPC]
	private void NetworkedUse()
	{
		if (this.source)
		{
			this.source.Play();
		}
		if (MapController.instance)
		{
			MapController.instance.ChangeFloor();
		}
	}

	// Token: 0x04000D3F RID: 3391
	private PhotonView view;

	// Token: 0x04000D40 RID: 3392
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000D41 RID: 3393
	[SerializeField]
	private AudioSource source;
}
