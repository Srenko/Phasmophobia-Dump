using System;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class PhotonTransformViewRotationControl
{
	// Token: 0x06000591 RID: 1425 RVA: 0x0001FD7D File Offset: 0x0001DF7D
	public PhotonTransformViewRotationControl(PhotonTransformViewRotationModel model)
	{
		this.m_Model = model;
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0001FD8C File Offset: 0x0001DF8C
	public Quaternion GetNetworkRotation()
	{
		return this.m_NetworkRotation;
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x0001FD94 File Offset: 0x0001DF94
	public Quaternion GetRotation(Quaternion currentRotation)
	{
		switch (this.m_Model.InterpolateOption)
		{
		default:
			return this.m_NetworkRotation;
		case PhotonTransformViewRotationModel.InterpolateOptions.RotateTowards:
			return Quaternion.RotateTowards(currentRotation, this.m_NetworkRotation, this.m_Model.InterpolateRotateTowardsSpeed * Time.deltaTime);
		case PhotonTransformViewRotationModel.InterpolateOptions.Lerp:
			return Quaternion.Lerp(currentRotation, this.m_NetworkRotation, this.m_Model.InterpolateLerpSpeed * Time.deltaTime);
		}
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x0001FE01 File Offset: 0x0001E001
	public void OnPhotonSerializeView(Quaternion currentRotation, PhotonStream stream, PhotonMessageInfo info)
	{
		if (!this.m_Model.SynchronizeEnabled)
		{
			return;
		}
		if (stream.isWriting)
		{
			stream.SendNext(currentRotation);
			this.m_NetworkRotation = currentRotation;
			return;
		}
		this.m_NetworkRotation = (Quaternion)stream.ReceiveNext();
	}

	// Token: 0x040005AE RID: 1454
	private PhotonTransformViewRotationModel m_Model;

	// Token: 0x040005AF RID: 1455
	private Quaternion m_NetworkRotation;
}
