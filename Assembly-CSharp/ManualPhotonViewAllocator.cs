using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
[RequireComponent(typeof(PhotonView))]
public class ManualPhotonViewAllocator : MonoBehaviour
{
	// Token: 0x060005CB RID: 1483 RVA: 0x00020E3C File Offset: 0x0001F03C
	public void AllocateManualPhotonView()
	{
		PhotonView photonView = base.gameObject.GetPhotonView();
		if (photonView == null)
		{
			Debug.LogError("Can't do manual instantiation without PhotonView component.");
			return;
		}
		int num = PhotonNetwork.AllocateViewID();
		photonView.RPC("InstantiateRpc", PhotonTargets.AllBuffered, new object[]
		{
			num
		});
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x00020E8C File Offset: 0x0001F08C
	[PunRPC]
	public void InstantiateRpc(int viewID)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.Prefab, InputToEvent.inputHitPos + new Vector3(0f, 5f, 0f), Quaternion.identity);
		gameObject.GetPhotonView().viewID = viewID;
		gameObject.GetComponent<OnClickDestroy>().DestroyByRpc = true;
	}

	// Token: 0x040005E6 RID: 1510
	public GameObject Prefab;
}
