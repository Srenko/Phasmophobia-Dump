using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public class PhotonTransformViewPositionControl
{
	// Token: 0x06000587 RID: 1415 RVA: 0x0001F889 File Offset: 0x0001DA89
	public PhotonTransformViewPositionControl(PhotonTransformViewPositionModel model)
	{
		this.m_Model = model;
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x0001F8B8 File Offset: 0x0001DAB8
	private Vector3 GetOldestStoredNetworkPosition()
	{
		Vector3 result = this.m_NetworkPosition;
		if (this.m_OldNetworkPositions.Count > 0)
		{
			result = this.m_OldNetworkPositions.Peek();
		}
		return result;
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0001F8E7 File Offset: 0x0001DAE7
	public void SetSynchronizedValues(Vector3 speed, float turnSpeed)
	{
		this.m_SynchronizedSpeed = speed;
		this.m_SynchronizedTurnSpeed = turnSpeed;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0001F8F8 File Offset: 0x0001DAF8
	public Vector3 UpdatePosition(Vector3 currentPosition)
	{
		Vector3 vector = this.GetNetworkPosition() + this.GetExtrapolatedPositionOffset();
		switch (this.m_Model.InterpolateOption)
		{
		case PhotonTransformViewPositionModel.InterpolateOptions.Disabled:
			if (!this.m_UpdatedPositionAfterOnSerialize)
			{
				currentPosition = vector;
				this.m_UpdatedPositionAfterOnSerialize = true;
			}
			break;
		case PhotonTransformViewPositionModel.InterpolateOptions.FixedSpeed:
			currentPosition = Vector3.MoveTowards(currentPosition, vector, Time.deltaTime * this.m_Model.InterpolateMoveTowardsSpeed);
			break;
		case PhotonTransformViewPositionModel.InterpolateOptions.EstimatedSpeed:
			if (this.m_OldNetworkPositions.Count != 0)
			{
				float num = Vector3.Distance(this.m_NetworkPosition, this.GetOldestStoredNetworkPosition()) / (float)this.m_OldNetworkPositions.Count * (float)PhotonNetwork.sendRateOnSerialize;
				currentPosition = Vector3.MoveTowards(currentPosition, vector, Time.deltaTime * num);
			}
			break;
		case PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues:
			if (this.m_SynchronizedSpeed.magnitude == 0f)
			{
				currentPosition = vector;
			}
			else
			{
				currentPosition = Vector3.MoveTowards(currentPosition, vector, Time.deltaTime * this.m_SynchronizedSpeed.magnitude);
			}
			break;
		case PhotonTransformViewPositionModel.InterpolateOptions.Lerp:
			currentPosition = Vector3.Lerp(currentPosition, vector, Time.deltaTime * this.m_Model.InterpolateLerpSpeed);
			break;
		}
		if (this.m_Model.TeleportEnabled && Vector3.Distance(currentPosition, this.GetNetworkPosition()) > this.m_Model.TeleportIfDistanceGreaterThan)
		{
			currentPosition = this.GetNetworkPosition();
		}
		return currentPosition;
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0001FA3F File Offset: 0x0001DC3F
	public Vector3 GetNetworkPosition()
	{
		return this.m_NetworkPosition;
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x0001FA48 File Offset: 0x0001DC48
	public Vector3 GetExtrapolatedPositionOffset()
	{
		float num = (float)(PhotonNetwork.time - this.m_LastSerializeTime);
		if (this.m_Model.ExtrapolateIncludingRoundTripTime)
		{
			num += (float)PhotonNetwork.GetPing() / 1000f;
		}
		Vector3 result = Vector3.zero;
		switch (this.m_Model.ExtrapolateOption)
		{
		case PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues:
			result = Quaternion.Euler(0f, this.m_SynchronizedTurnSpeed * num, 0f) * (this.m_SynchronizedSpeed * num);
			break;
		case PhotonTransformViewPositionModel.ExtrapolateOptions.EstimateSpeedAndTurn:
			result = (this.m_NetworkPosition - this.GetOldestStoredNetworkPosition()) * (float)PhotonNetwork.sendRateOnSerialize * num;
			break;
		case PhotonTransformViewPositionModel.ExtrapolateOptions.FixedSpeed:
			result = (this.m_NetworkPosition - this.GetOldestStoredNetworkPosition()).normalized * this.m_Model.ExtrapolateSpeed * num;
			break;
		}
		return result;
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x0001FB2B File Offset: 0x0001DD2B
	public void OnPhotonSerializeView(Vector3 currentPosition, PhotonStream stream, PhotonMessageInfo info)
	{
		if (!this.m_Model.SynchronizeEnabled)
		{
			return;
		}
		if (stream.isWriting)
		{
			this.SerializeData(currentPosition, stream, info);
		}
		else
		{
			this.DeserializeData(stream, info);
		}
		this.m_LastSerializeTime = PhotonNetwork.time;
		this.m_UpdatedPositionAfterOnSerialize = false;
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x0001FB68 File Offset: 0x0001DD68
	private void SerializeData(Vector3 currentPosition, PhotonStream stream, PhotonMessageInfo info)
	{
		stream.SendNext(currentPosition);
		this.m_NetworkPosition = currentPosition;
		if (this.m_Model.ExtrapolateOption == PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues || this.m_Model.InterpolateOption == PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues)
		{
			stream.SendNext(this.m_SynchronizedSpeed);
			stream.SendNext(this.m_SynchronizedTurnSpeed);
		}
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
	private void DeserializeData(PhotonStream stream, PhotonMessageInfo info)
	{
		Vector3 networkPosition = (Vector3)stream.ReceiveNext();
		if (this.m_Model.ExtrapolateOption == PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues || this.m_Model.InterpolateOption == PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues)
		{
			this.m_SynchronizedSpeed = (Vector3)stream.ReceiveNext();
			this.m_SynchronizedTurnSpeed = (float)stream.ReceiveNext();
		}
		if (this.m_OldNetworkPositions.Count == 0)
		{
			this.m_NetworkPosition = networkPosition;
		}
		this.m_OldNetworkPositions.Enqueue(this.m_NetworkPosition);
		this.m_NetworkPosition = networkPosition;
		while (this.m_OldNetworkPositions.Count > this.m_Model.ExtrapolateNumberOfStoredPositions)
		{
			this.m_OldNetworkPositions.Dequeue();
		}
	}

	// Token: 0x04000598 RID: 1432
	private PhotonTransformViewPositionModel m_Model;

	// Token: 0x04000599 RID: 1433
	private float m_CurrentSpeed;

	// Token: 0x0400059A RID: 1434
	private double m_LastSerializeTime;

	// Token: 0x0400059B RID: 1435
	private Vector3 m_SynchronizedSpeed = Vector3.zero;

	// Token: 0x0400059C RID: 1436
	private float m_SynchronizedTurnSpeed;

	// Token: 0x0400059D RID: 1437
	private Vector3 m_NetworkPosition;

	// Token: 0x0400059E RID: 1438
	private Queue<Vector3> m_OldNetworkPositions = new Queue<Vector3>();

	// Token: 0x0400059F RID: 1439
	private bool m_UpdatedPositionAfterOnSerialize = true;
}
