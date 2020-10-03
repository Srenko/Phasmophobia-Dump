using System;
using Photon;
using UnityEngine;
using VRTK;

// Token: 0x0200013B RID: 315
public class PhotonInstantiate : Photon.MonoBehaviour
{
	// Token: 0x0600083B RID: 2107 RVA: 0x000313B6 File Offset: 0x0002F5B6
	private void OnEnable()
	{
		if (this.spawnOnEnable && PhotonNetwork.inRoom)
		{
			base.Invoke("Spawn", 1f);
		}
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x000313D7 File Offset: 0x0002F5D7
	private void OnJoinedRoom()
	{
		if (this.spawnOnEnable)
		{
			base.Invoke("Spawn", 1f);
		}
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x000313F4 File Offset: 0x0002F5F4
	public void Spawn()
	{
		if (PhotonNetwork.isMasterClient)
		{
			GameObject gameObject = PhotonNetwork.InstantiateSceneObject(this.prefab.name, base.transform.position, base.transform.localRotation, 0, null);
			gameObject.transform.SetParent(base.transform);
			gameObject.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
			gameObject.transform.SetParent(null);
			if (this.parent != null)
			{
				this.view.RPC("SyncParentAndDropZone", PhotonTargets.AllBuffered, new object[]
				{
					gameObject.GetComponent<PhotonView>().viewID
				});
			}
		}
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x000314B0 File Offset: 0x0002F6B0
	[PunRPC]
	private void SyncParentAndDropZone(int ID)
	{
		PhotonObjectInteract component = PhotonView.Find(ID).GetComponent<PhotonObjectInteract>();
		component.transform.SetParent(this.parent);
		this.snapDropZone.ForceSnap(component.gameObject);
	}

	// Token: 0x04000859 RID: 2137
	[Header("Prefab must be in resources folder")]
	[SerializeField]
	private GameObject prefab;

	// Token: 0x0400085A RID: 2138
	[SerializeField]
	private Transform parent;

	// Token: 0x0400085B RID: 2139
	[SerializeField]
	private VRTK_SnapDropZone snapDropZone;

	// Token: 0x0400085C RID: 2140
	[SerializeField]
	private PhotonView view;

	// Token: 0x0400085D RID: 2141
	[SerializeField]
	private bool spawnOnEnable = true;
}
