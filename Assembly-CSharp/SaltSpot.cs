using System;
using UnityEngine;

// Token: 0x02000167 RID: 359
public class SaltSpot : MonoBehaviour
{
	// Token: 0x06000997 RID: 2455 RVA: 0x0003B084 File Offset: 0x00039284
	[PunRPC]
	private void SyncSalt()
	{
		this.evidence.enabled = true;
		this.normalSalt.SetActive(false);
		this.flatSalt.SetActive(true);
		LevelController.instance.currentGhost.ghostInteraction.hasWalkedInSalt = true;
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x0003B0C0 File Offset: 0x000392C0
	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger)
		{
			return;
		}
		if (this.used)
		{
			return;
		}
		if (!PhotonNetwork.isMasterClient)
		{
			return;
		}
		if (other.CompareTag("Ghost"))
		{
			if (LevelController.instance.currentGhost.ghostInteraction.hasWalkedInSalt)
			{
				return;
			}
			if (LevelController.instance.currentGhost.isHunting)
			{
				return;
			}
			this.view.RPC("SyncSalt", PhotonTargets.All, Array.Empty<object>());
			this.used = true;
		}
	}

	// Token: 0x040009A8 RID: 2472
	[SerializeField]
	private GameObject normalSalt;

	// Token: 0x040009A9 RID: 2473
	[SerializeField]
	private GameObject flatSalt;

	// Token: 0x040009AA RID: 2474
	[SerializeField]
	private Evidence evidence;

	// Token: 0x040009AB RID: 2475
	[SerializeField]
	private PhotonView view;

	// Token: 0x040009AC RID: 2476
	private bool used;
}
