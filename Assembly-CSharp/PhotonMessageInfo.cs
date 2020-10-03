using System;

// Token: 0x020000A7 RID: 167
public struct PhotonMessageInfo
{
	// Token: 0x060003E0 RID: 992 RVA: 0x00019987 File Offset: 0x00017B87
	public PhotonMessageInfo(PhotonPlayer player, int timestamp, PhotonView view)
	{
		this.sender = player;
		this.timeInt = timestamp;
		this.photonView = view;
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001999E File Offset: 0x00017B9E
	public double timestamp
	{
		get
		{
			return this.timeInt / 1000.0;
		}
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x000199B2 File Offset: 0x00017BB2
	public override string ToString()
	{
		return string.Format("[PhotonMessageInfo: Sender='{1}' Senttime={0}]", this.timestamp, this.sender);
	}

	// Token: 0x040004CB RID: 1227
	private readonly int timeInt;

	// Token: 0x040004CC RID: 1228
	public readonly PhotonPlayer sender;

	// Token: 0x040004CD RID: 1229
	public readonly PhotonView photonView;
}
