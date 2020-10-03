using System;
using Photon;
using UnityEngine;

// Token: 0x020000D6 RID: 214
[RequireComponent(typeof(PhotonView))]
public class PickupItemSimple : Photon.MonoBehaviour
{
	// Token: 0x060005EF RID: 1519 RVA: 0x00021B14 File Offset: 0x0001FD14
	public void OnTriggerEnter(Collider other)
	{
		PhotonView component = other.GetComponent<PhotonView>();
		if (this.PickupOnCollide && component != null && component.isMine)
		{
			this.Pickup();
		}
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00021B47 File Offset: 0x0001FD47
	public void Pickup()
	{
		if (this.SentPickup)
		{
			return;
		}
		this.SentPickup = true;
		base.photonView.RPC("PunPickupSimple", PhotonTargets.AllViaServer, Array.Empty<object>());
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x00021B70 File Offset: 0x0001FD70
	[PunRPC]
	public void PunPickupSimple(PhotonMessageInfo msgInfo)
	{
		if (this.SentPickup && msgInfo.sender.IsLocal)
		{
			base.gameObject.GetActive();
		}
		this.SentPickup = false;
		if (!base.gameObject.GetActive())
		{
			Debug.Log("Ignored PU RPC, cause item is inactive. " + base.gameObject);
			return;
		}
		double num = PhotonNetwork.time - msgInfo.timestamp;
		float num2 = this.SecondsBeforeRespawn - (float)num;
		if (num2 > 0f)
		{
			base.gameObject.SetActive(false);
			base.Invoke("RespawnAfter", num2);
		}
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x00021C00 File Offset: 0x0001FE00
	public void RespawnAfter()
	{
		if (base.gameObject != null)
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x04000604 RID: 1540
	public float SecondsBeforeRespawn = 2f;

	// Token: 0x04000605 RID: 1541
	public bool PickupOnCollide;

	// Token: 0x04000606 RID: 1542
	public bool SentPickup;
}
