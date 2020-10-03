using System;
using Photon;
using UnityEngine;

// Token: 0x02000068 RID: 104
[RequireComponent(typeof(PhotonView))]
public class CubeExtra : Photon.MonoBehaviour, IPunObservable
{
	// Token: 0x0600024A RID: 586 RVA: 0x0000F718 File Offset: 0x0000D918
	public void Awake()
	{
		this.latestCorrectPos = base.transform.position;
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0000F72C File Offset: 0x0000D92C
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			Vector3 localPosition = base.transform.localPosition;
			stream.Serialize(ref localPosition);
			return;
		}
		Vector3 zero = Vector3.zero;
		stream.Serialize(ref zero);
		double num = info.timestamp - this.lastTime;
		this.lastTime = info.timestamp;
		this.movementVector = (zero - this.latestCorrectPos) / (float)num;
		this.errorVector = (zero - base.transform.localPosition) / (float)num;
		this.latestCorrectPos = zero;
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
	public void Update()
	{
		if (base.photonView.isMine)
		{
			return;
		}
		base.transform.localPosition += (this.movementVector + this.errorVector) * this.Factor * Time.deltaTime;
	}

	// Token: 0x04000281 RID: 641
	[Range(0.9f, 1.1f)]
	public float Factor = 0.98f;

	// Token: 0x04000282 RID: 642
	private Vector3 latestCorrectPos = Vector3.zero;

	// Token: 0x04000283 RID: 643
	private Vector3 movementVector = Vector3.zero;

	// Token: 0x04000284 RID: 644
	private Vector3 errorVector = Vector3.zero;

	// Token: 0x04000285 RID: 645
	private double lastTime;
}
