using System;
using Photon;
using UnityEngine;

// Token: 0x0200014F RID: 335
[RequireComponent(typeof(PhotonView))]
public class TeleportableObject : Photon.MonoBehaviour
{
	// Token: 0x060008CE RID: 2254 RVA: 0x00034EFC File Offset: 0x000330FC
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.body = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00034F24 File Offset: 0x00033124
	public void Use()
	{
		if (this.photonInteract.isGrabbed)
		{
			return;
		}
		this.body.useGravity = true;
		this.body.isKinematic = false;
		if (this.view.isMine)
		{
			this.TeleportObject();
			return;
		}
		this.view.RequestOwnership();
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x00034F76 File Offset: 0x00033176
	public void OnOwnershipRequest()
	{
		this.TeleportObject();
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00034F80 File Offset: 0x00033180
	private void TeleportObject()
	{
		int num = Random.Range(0, EvidenceController.instance.roomsToSpawnDNAEvidenceInside.Length);
		int index = Random.Range(0, EvidenceController.instance.roomsToSpawnDNAEvidenceInside[num].colliders.Count);
		Bounds bounds = EvidenceController.instance.roomsToSpawnDNAEvidenceInside[num].colliders[index].bounds;
		Vector3 position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), Random.Range(bounds.min.z, bounds.max.z));
		if (this.specificLocation != null)
		{
			position = this.specificLocation.position;
		}
		this.body.velocity = Vector3.zero;
		base.transform.position = position;
	}

	// Token: 0x040008E5 RID: 2277
	[HideInInspector]
	public PhotonView view;

	// Token: 0x040008E6 RID: 2278
	private PhotonObjectInteract photonInteract;

	// Token: 0x040008E7 RID: 2279
	private Rigidbody body;

	// Token: 0x040008E8 RID: 2280
	[SerializeField]
	private Transform specificLocation;
}
