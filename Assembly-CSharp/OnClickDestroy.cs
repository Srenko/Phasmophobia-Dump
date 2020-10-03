using System;
using System.Collections;
using Photon;
using UnityEngine;

// Token: 0x020000D1 RID: 209
[RequireComponent(typeof(PhotonView))]
public class OnClickDestroy : Photon.MonoBehaviour
{
	// Token: 0x060005D9 RID: 1497 RVA: 0x000215F8 File Offset: 0x0001F7F8
	public void OnClick()
	{
		if (!this.DestroyByRpc)
		{
			PhotonNetwork.Destroy(base.gameObject);
			return;
		}
		base.photonView.RPC("DestroyRpc", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x00021624 File Offset: 0x0001F824
	[PunRPC]
	public IEnumerator DestroyRpc()
	{
		Object.Destroy(base.gameObject);
		yield return 0;
		PhotonNetwork.UnAllocateViewID(base.photonView.viewID);
		yield break;
	}

	// Token: 0x040005F5 RID: 1525
	public bool DestroyByRpc;
}
