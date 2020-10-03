using System;
using System.Collections.Generic;
using Photon;
using UnityEngine;

// Token: 0x020000E2 RID: 226
[RequireComponent(typeof(PhotonView))]
public class SmoothSyncMovement : Photon.MonoBehaviour, IPunObservable
{
	// Token: 0x0600062C RID: 1580 RVA: 0x00022840 File Offset: 0x00020A40
	public void Awake()
	{
		bool flag = false;
		using (List<Component>.Enumerator enumerator = base.photonView.ObservedComponents.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == this)
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			Debug.LogWarning(this + " is not observed by this object's photonView! OnPhotonSerializeView() in this class won't be used.");
		}
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x000228B4 File Offset: 0x00020AB4
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(base.transform.position);
			stream.SendNext(base.transform.rotation);
			return;
		}
		this.correctPlayerPos = (Vector3)stream.ReceiveNext();
		this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x00022918 File Offset: 0x00020B18
	public void Update()
	{
		if (!base.photonView.isMine)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
		}
	}

	// Token: 0x04000617 RID: 1559
	public float SmoothingDelay = 5f;

	// Token: 0x04000618 RID: 1560
	private Vector3 correctPlayerPos = Vector3.zero;

	// Token: 0x04000619 RID: 1561
	private Quaternion correctPlayerRot = Quaternion.identity;
}
