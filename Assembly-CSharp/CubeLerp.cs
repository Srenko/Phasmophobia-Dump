using System;
using Photon;
using UnityEngine;

// Token: 0x0200006A RID: 106
[RequireComponent(typeof(PhotonView))]
public class CubeLerp : Photon.MonoBehaviour, IPunObservable
{
	// Token: 0x06000251 RID: 593 RVA: 0x0000FAFD File Offset: 0x0000DCFD
	public void Start()
	{
		this.latestCorrectPos = base.transform.position;
		this.onUpdatePos = base.transform.position;
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0000FB24 File Offset: 0x0000DD24
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			Vector3 localPosition = base.transform.localPosition;
			Quaternion localRotation = base.transform.localRotation;
			stream.Serialize(ref localPosition);
			stream.Serialize(ref localRotation);
			return;
		}
		Vector3 zero = Vector3.zero;
		Quaternion identity = Quaternion.identity;
		stream.Serialize(ref zero);
		stream.Serialize(ref identity);
		this.latestCorrectPos = zero;
		this.onUpdatePos = base.transform.localPosition;
		this.fraction = 0f;
		base.transform.localRotation = identity;
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
	public void Update()
	{
		if (base.photonView.isMine)
		{
			return;
		}
		this.fraction += Time.deltaTime * 9f;
		base.transform.localPosition = Vector3.Lerp(this.onUpdatePos, this.latestCorrectPos, this.fraction);
	}

	// Token: 0x04000289 RID: 649
	private Vector3 latestCorrectPos;

	// Token: 0x0400028A RID: 650
	private Vector3 onUpdatePos;

	// Token: 0x0400028B RID: 651
	private float fraction;
}
