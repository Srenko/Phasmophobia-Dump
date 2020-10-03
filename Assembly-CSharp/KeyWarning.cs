using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class KeyWarning : MonoBehaviour
{
	// Token: 0x0600072D RID: 1837 RVA: 0x00029FFC File Offset: 0x000281FC
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0002A00C File Offset: 0x0002820C
	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger)
		{
			return;
		}
		if (PhotonNetwork.inRoom && other.transform.root.CompareTag("Player"))
		{
			this.view.RPC("PlayAudio", PhotonNetwork.masterClient, Array.Empty<object>());
		}
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x0002A05A File Offset: 0x0002825A
	[PunRPC]
	private void PlayAudio()
	{
		if (!TruckRadioController.instance.playedKeyAudio)
		{
			TruckRadioController.instance.PlayKeyWarningAudio();
		}
	}

	// Token: 0x040006D8 RID: 1752
	private PhotonView view;
}
