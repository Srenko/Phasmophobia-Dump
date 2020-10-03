using System;
using Photon;
using UnityEngine;

// Token: 0x02000069 RID: 105
[RequireComponent(typeof(PhotonView))]
public class CubeInter : Photon.MonoBehaviour, IPunObservable
{
	// Token: 0x0600024E RID: 590 RVA: 0x0000F84C File Offset: 0x0000DA4C
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			Vector3 localPosition = base.transform.localPosition;
			Quaternion localRotation = base.transform.localRotation;
			stream.Serialize(ref localPosition);
			stream.Serialize(ref localRotation);
			stream.SendNext(Environment.TickCount);
			return;
		}
		Vector3 zero = Vector3.zero;
		Quaternion identity = Quaternion.identity;
		stream.Serialize(ref zero);
		stream.Serialize(ref identity);
		for (int i = this.m_BufferedState.Length - 1; i >= 1; i--)
		{
			this.m_BufferedState[i] = this.m_BufferedState[i - 1];
		}
		CubeInter.State state;
		state.timestamp = info.timestamp;
		state.pos = zero;
		state.rot = identity;
		this.m_BufferedState[0] = state;
		this.m_TimestampCount = Mathf.Min(this.m_TimestampCount + 1, this.m_BufferedState.Length);
		for (int j = 0; j < this.m_TimestampCount - 1; j++)
		{
			if (this.m_BufferedState[j].timestamp < this.m_BufferedState[j + 1].timestamp)
			{
				Debug.Log("State inconsistent");
			}
		}
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000F97C File Offset: 0x0000DB7C
	public void Update()
	{
		if (base.photonView.isMine || !PhotonNetwork.inRoom)
		{
			return;
		}
		double num = PhotonNetwork.time - this.InterpolationDelay;
		if (this.m_BufferedState[0].timestamp > num)
		{
			for (int i = 0; i < this.m_TimestampCount; i++)
			{
				if (this.m_BufferedState[i].timestamp <= num || i == this.m_TimestampCount - 1)
				{
					CubeInter.State state = this.m_BufferedState[Mathf.Max(i - 1, 0)];
					CubeInter.State state2 = this.m_BufferedState[i];
					double num2 = state.timestamp - state2.timestamp;
					float t = 0f;
					if (num2 > 0.0001)
					{
						t = (float)((num - state2.timestamp) / num2);
					}
					base.transform.localPosition = Vector3.Lerp(state2.pos, state.pos, t);
					base.transform.localRotation = Quaternion.Slerp(state2.rot, state.rot, t);
					return;
				}
			}
			return;
		}
		CubeInter.State state3 = this.m_BufferedState[0];
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, state3.pos, Time.deltaTime * 20f);
		base.transform.localRotation = state3.rot;
	}

	// Token: 0x04000286 RID: 646
	private CubeInter.State[] m_BufferedState = new CubeInter.State[20];

	// Token: 0x04000287 RID: 647
	private int m_TimestampCount;

	// Token: 0x04000288 RID: 648
	public double InterpolationDelay = 0.15;

	// Token: 0x020004E7 RID: 1255
	internal struct State
	{
		// Token: 0x040023B8 RID: 9144
		internal double timestamp;

		// Token: 0x040023B9 RID: 9145
		internal Vector3 pos;

		// Token: 0x040023BA RID: 9146
		internal Quaternion rot;
	}
}
