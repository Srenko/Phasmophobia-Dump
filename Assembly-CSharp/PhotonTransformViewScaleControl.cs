using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class PhotonTransformViewScaleControl
{
	// Token: 0x06000596 RID: 1430 RVA: 0x0001FE63 File Offset: 0x0001E063
	public PhotonTransformViewScaleControl(PhotonTransformViewScaleModel model)
	{
		this.m_Model = model;
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0001FE7D File Offset: 0x0001E07D
	public Vector3 GetNetworkScale()
	{
		return this.m_NetworkScale;
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0001FE88 File Offset: 0x0001E088
	public Vector3 GetScale(Vector3 currentScale)
	{
		switch (this.m_Model.InterpolateOption)
		{
		default:
			return this.m_NetworkScale;
		case PhotonTransformViewScaleModel.InterpolateOptions.MoveTowards:
			return Vector3.MoveTowards(currentScale, this.m_NetworkScale, this.m_Model.InterpolateMoveTowardsSpeed * Time.deltaTime);
		case PhotonTransformViewScaleModel.InterpolateOptions.Lerp:
			return Vector3.Lerp(currentScale, this.m_NetworkScale, this.m_Model.InterpolateLerpSpeed * Time.deltaTime);
		}
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0001FEF5 File Offset: 0x0001E0F5
	public void OnPhotonSerializeView(Vector3 currentScale, PhotonStream stream, PhotonMessageInfo info)
	{
		if (!this.m_Model.SynchronizeEnabled)
		{
			return;
		}
		if (stream.isWriting)
		{
			stream.SendNext(currentScale);
			this.m_NetworkScale = currentScale;
			return;
		}
		this.m_NetworkScale = (Vector3)stream.ReceiveNext();
	}

	// Token: 0x040005B4 RID: 1460
	private PhotonTransformViewScaleModel m_Model;

	// Token: 0x040005B5 RID: 1461
	private Vector3 m_NetworkScale = Vector3.one;
}
