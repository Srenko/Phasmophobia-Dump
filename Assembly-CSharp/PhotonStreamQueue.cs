using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class PhotonStreamQueue
{
	// Token: 0x060004DC RID: 1244 RVA: 0x0001CEB8 File Offset: 0x0001B0B8
	public PhotonStreamQueue(int sampleRate)
	{
		this.m_SampleRate = sampleRate;
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
	private void BeginWritePackage()
	{
		if (Time.realtimeSinceStartup < this.m_LastSampleTime + 1f / (float)this.m_SampleRate)
		{
			this.m_IsWriting = false;
			return;
		}
		if (this.m_SampleCount == 1)
		{
			this.m_ObjectsPerSample = this.m_Objects.Count;
		}
		else if (this.m_SampleCount > 1 && this.m_Objects.Count / this.m_SampleCount != this.m_ObjectsPerSample)
		{
			Debug.LogWarning("The number of objects sent via a PhotonStreamQueue has to be the same each frame");
			Debug.LogWarning(string.Concat(new object[]
			{
				"Objects in List: ",
				this.m_Objects.Count,
				" / Sample Count: ",
				this.m_SampleCount,
				" = ",
				this.m_Objects.Count / this.m_SampleCount,
				" != ",
				this.m_ObjectsPerSample
			}));
		}
		this.m_IsWriting = true;
		this.m_SampleCount++;
		this.m_LastSampleTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0001D00F File Offset: 0x0001B20F
	public void Reset()
	{
		this.m_SampleCount = 0;
		this.m_ObjectsPerSample = -1;
		this.m_LastSampleTime = float.NegativeInfinity;
		this.m_LastFrameCount = -1;
		this.m_Objects.Clear();
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x0001D03C File Offset: 0x0001B23C
	public void SendNext(object obj)
	{
		if (Time.frameCount != this.m_LastFrameCount)
		{
			this.BeginWritePackage();
		}
		this.m_LastFrameCount = Time.frameCount;
		if (!this.m_IsWriting)
		{
			return;
		}
		this.m_Objects.Add(obj);
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x0001D071 File Offset: 0x0001B271
	public bool HasQueuedObjects()
	{
		return this.m_NextObjectIndex != -1;
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x0001D080 File Offset: 0x0001B280
	public object ReceiveNext()
	{
		if (this.m_NextObjectIndex == -1)
		{
			return null;
		}
		if (this.m_NextObjectIndex >= this.m_Objects.Count)
		{
			this.m_NextObjectIndex -= this.m_ObjectsPerSample;
		}
		List<object> objects = this.m_Objects;
		int nextObjectIndex = this.m_NextObjectIndex;
		this.m_NextObjectIndex = nextObjectIndex + 1;
		return objects[nextObjectIndex];
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x0001D0DC File Offset: 0x0001B2DC
	public void Serialize(PhotonStream stream)
	{
		if (this.m_Objects.Count > 0 && this.m_ObjectsPerSample < 0)
		{
			this.m_ObjectsPerSample = this.m_Objects.Count;
		}
		stream.SendNext(this.m_SampleCount);
		stream.SendNext(this.m_ObjectsPerSample);
		for (int i = 0; i < this.m_Objects.Count; i++)
		{
			stream.SendNext(this.m_Objects[i]);
		}
		this.m_Objects.Clear();
		this.m_SampleCount = 0;
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0001D170 File Offset: 0x0001B370
	public void Deserialize(PhotonStream stream)
	{
		this.m_Objects.Clear();
		this.m_SampleCount = (int)stream.ReceiveNext();
		this.m_ObjectsPerSample = (int)stream.ReceiveNext();
		for (int i = 0; i < this.m_SampleCount * this.m_ObjectsPerSample; i++)
		{
			this.m_Objects.Add(stream.ReceiveNext());
		}
		if (this.m_Objects.Count > 0)
		{
			this.m_NextObjectIndex = 0;
			return;
		}
		this.m_NextObjectIndex = -1;
	}

	// Token: 0x04000521 RID: 1313
	private int m_SampleRate;

	// Token: 0x04000522 RID: 1314
	private int m_SampleCount;

	// Token: 0x04000523 RID: 1315
	private int m_ObjectsPerSample = -1;

	// Token: 0x04000524 RID: 1316
	private float m_LastSampleTime = float.NegativeInfinity;

	// Token: 0x04000525 RID: 1317
	private int m_LastFrameCount = -1;

	// Token: 0x04000526 RID: 1318
	private int m_NextObjectIndex = -1;

	// Token: 0x04000527 RID: 1319
	private List<object> m_Objects = new List<object>();

	// Token: 0x04000528 RID: 1320
	private bool m_IsWriting;
}
