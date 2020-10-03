using System;
using System.Collections.Generic;
using Photon;
using UnityEngine;

// Token: 0x020000D5 RID: 213
[RequireComponent(typeof(PhotonView))]
public class PickupItem : Photon.MonoBehaviour, IPunObservable
{
	// Token: 0x170000DF RID: 223
	// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00021815 File Offset: 0x0001FA15
	public int ViewID
	{
		get
		{
			return base.photonView.viewID;
		}
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x00021824 File Offset: 0x0001FA24
	public void OnTriggerEnter(Collider other)
	{
		PhotonView component = other.GetComponent<PhotonView>();
		if (this.PickupOnTrigger && component != null && component.isMine)
		{
			this.Pickup();
		}
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x00021858 File Offset: 0x0001FA58
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting && this.SecondsBeforeRespawn <= 0f)
		{
			stream.SendNext(base.gameObject.transform.position);
			return;
		}
		Vector3 position = (Vector3)stream.ReceiveNext();
		base.gameObject.transform.position = position;
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x000218B3 File Offset: 0x0001FAB3
	public void Pickup()
	{
		if (this.SentPickup)
		{
			return;
		}
		this.SentPickup = true;
		base.photonView.RPC("PunPickup", PhotonTargets.AllViaServer, Array.Empty<object>());
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x000218DB File Offset: 0x0001FADB
	public void Drop()
	{
		if (this.PickupIsMine)
		{
			base.photonView.RPC("PunRespawn", PhotonTargets.AllViaServer, Array.Empty<object>());
		}
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x000218FB File Offset: 0x0001FAFB
	public void Drop(Vector3 newPosition)
	{
		if (this.PickupIsMine)
		{
			base.photonView.RPC("PunRespawn", PhotonTargets.AllViaServer, new object[]
			{
				newPosition
			});
		}
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x00021928 File Offset: 0x0001FB28
	[PunRPC]
	public void PunPickup(PhotonMessageInfo msgInfo)
	{
		if (msgInfo.sender.IsLocal)
		{
			this.SentPickup = false;
		}
		if (!base.gameObject.GetActive())
		{
			Debug.Log(string.Concat(new object[]
			{
				"Ignored PU RPC, cause item is inactive. ",
				base.gameObject,
				" SecondsBeforeRespawn: ",
				this.SecondsBeforeRespawn,
				" TimeOfRespawn: ",
				this.TimeOfRespawn,
				" respawn in future: ",
				(this.TimeOfRespawn > PhotonNetwork.time).ToString()
			}));
			return;
		}
		this.PickupIsMine = msgInfo.sender.IsLocal;
		if (this.OnPickedUpCall != null)
		{
			this.OnPickedUpCall.SendMessage("OnPickedUp", this);
		}
		if (this.SecondsBeforeRespawn <= 0f)
		{
			this.PickedUp(0f);
			return;
		}
		double num = PhotonNetwork.time - msgInfo.timestamp;
		double num2 = (double)this.SecondsBeforeRespawn - num;
		if (num2 > 0.0)
		{
			this.PickedUp((float)num2);
		}
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x00021A3C File Offset: 0x0001FC3C
	internal void PickedUp(float timeUntilRespawn)
	{
		base.gameObject.SetActive(false);
		PickupItem.DisabledPickupItems.Add(this);
		this.TimeOfRespawn = 0.0;
		if (timeUntilRespawn > 0f)
		{
			this.TimeOfRespawn = PhotonNetwork.time + (double)timeUntilRespawn;
			base.Invoke("PunRespawn", timeUntilRespawn);
		}
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x00021A92 File Offset: 0x0001FC92
	[PunRPC]
	internal void PunRespawn(Vector3 pos)
	{
		Debug.Log("PunRespawn with Position.");
		this.PunRespawn();
		base.gameObject.transform.position = pos;
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x00021AB5 File Offset: 0x0001FCB5
	[PunRPC]
	internal void PunRespawn()
	{
		PickupItem.DisabledPickupItems.Remove(this);
		this.TimeOfRespawn = 0.0;
		this.PickupIsMine = false;
		if (base.gameObject != null)
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x040005FD RID: 1533
	public float SecondsBeforeRespawn = 2f;

	// Token: 0x040005FE RID: 1534
	public bool PickupOnTrigger;

	// Token: 0x040005FF RID: 1535
	public bool PickupIsMine;

	// Token: 0x04000600 RID: 1536
	public UnityEngine.MonoBehaviour OnPickedUpCall;

	// Token: 0x04000601 RID: 1537
	public bool SentPickup;

	// Token: 0x04000602 RID: 1538
	public double TimeOfRespawn;

	// Token: 0x04000603 RID: 1539
	public static HashSet<PickupItem> DisabledPickupItems = new HashSet<PickupItem>();
}
