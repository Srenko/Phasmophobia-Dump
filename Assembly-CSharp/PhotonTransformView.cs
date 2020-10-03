using System;
using UnityEngine;

// Token: 0x020000C1 RID: 193
[RequireComponent(typeof(PhotonView))]
[AddComponentMenu("Photon Networking/Photon Transform View")]
public class PhotonTransformView : MonoBehaviour, IPunObservable
{
	// Token: 0x0600057D RID: 1405 RVA: 0x0001F574 File Offset: 0x0001D774
	private void Awake()
	{
		this.m_PhotonView = base.GetComponent<PhotonView>();
		this.m_PositionControl = new PhotonTransformViewPositionControl(this.m_PositionModel);
		this.m_RotationControl = new PhotonTransformViewRotationControl(this.m_RotationModel);
		this.m_ScaleControl = new PhotonTransformViewScaleControl(this.m_ScaleModel);
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x0001F5C0 File Offset: 0x0001D7C0
	private void OnEnable()
	{
		this.m_firstTake = true;
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x0001F5C9 File Offset: 0x0001D7C9
	private void Update()
	{
		if (this.m_PhotonView == null || this.m_PhotonView.isMine || !PhotonNetwork.connected)
		{
			return;
		}
		this.UpdatePosition();
		this.UpdateRotation();
		this.UpdateScale();
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x0001F600 File Offset: 0x0001D800
	private void UpdatePosition()
	{
		if (!this.m_PositionModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
		{
			return;
		}
		base.transform.localPosition = this.m_PositionControl.UpdatePosition(base.transform.localPosition);
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x0001F639 File Offset: 0x0001D839
	private void UpdateRotation()
	{
		if (!this.m_RotationModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
		{
			return;
		}
		base.transform.localRotation = this.m_RotationControl.GetRotation(base.transform.localRotation);
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x0001F672 File Offset: 0x0001D872
	private void UpdateScale()
	{
		if (!this.m_ScaleModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
		{
			return;
		}
		base.transform.localScale = this.m_ScaleControl.GetScale(base.transform.localScale);
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x0001F6AB File Offset: 0x0001D8AB
	public void SetSynchronizedValues(Vector3 speed, float turnSpeed)
	{
		this.m_PositionControl.SetSynchronizedValues(speed, turnSpeed);
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x0001F6BC File Offset: 0x0001D8BC
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		this.m_PositionControl.OnPhotonSerializeView(base.transform.localPosition, stream, info);
		this.m_RotationControl.OnPhotonSerializeView(base.transform.localRotation, stream, info);
		this.m_ScaleControl.OnPhotonSerializeView(base.transform.localScale, stream, info);
		if (!this.m_PhotonView.isMine && this.m_PositionModel.DrawErrorGizmo)
		{
			this.DoDrawEstimatedPositionError();
		}
		if (stream.isReading)
		{
			this.m_ReceivedNetworkUpdate = true;
			if (this.m_firstTake)
			{
				this.m_firstTake = false;
				if (this.m_PositionModel.SynchronizeEnabled)
				{
					base.transform.localPosition = this.m_PositionControl.GetNetworkPosition();
				}
				if (this.m_RotationModel.SynchronizeEnabled)
				{
					base.transform.localRotation = this.m_RotationControl.GetNetworkRotation();
				}
				if (this.m_ScaleModel.SynchronizeEnabled)
				{
					base.transform.localScale = this.m_ScaleControl.GetNetworkScale();
				}
			}
		}
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
	private void DoDrawEstimatedPositionError()
	{
		Vector3 vector = this.m_PositionControl.GetNetworkPosition();
		if (base.transform.parent != null)
		{
			vector = base.transform.parent.position + vector;
		}
		Debug.DrawLine(vector, base.transform.position, Color.red, 2f);
		Debug.DrawLine(base.transform.position, base.transform.position + Vector3.up, Color.green, 2f);
		Debug.DrawLine(vector, vector + Vector3.up, Color.red, 2f);
	}

	// Token: 0x0400058F RID: 1423
	[SerializeField]
	public PhotonTransformViewPositionModel m_PositionModel = new PhotonTransformViewPositionModel();

	// Token: 0x04000590 RID: 1424
	[SerializeField]
	public PhotonTransformViewRotationModel m_RotationModel = new PhotonTransformViewRotationModel();

	// Token: 0x04000591 RID: 1425
	[SerializeField]
	public PhotonTransformViewScaleModel m_ScaleModel = new PhotonTransformViewScaleModel();

	// Token: 0x04000592 RID: 1426
	private PhotonTransformViewPositionControl m_PositionControl;

	// Token: 0x04000593 RID: 1427
	private PhotonTransformViewRotationControl m_RotationControl;

	// Token: 0x04000594 RID: 1428
	private PhotonTransformViewScaleControl m_ScaleControl;

	// Token: 0x04000595 RID: 1429
	private PhotonView m_PhotonView;

	// Token: 0x04000596 RID: 1430
	private bool m_ReceivedNetworkUpdate;

	// Token: 0x04000597 RID: 1431
	private bool m_firstTake;
}
