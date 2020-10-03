using System;

// Token: 0x02000087 RID: 135
public class FriendInfo
{
	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060002D8 RID: 728 RVA: 0x000127DD File Offset: 0x000109DD
	[Obsolete("Use UserId.")]
	public string Name
	{
		get
		{
			return this.UserId;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060002D9 RID: 729 RVA: 0x000127E5 File Offset: 0x000109E5
	// (set) Token: 0x060002DA RID: 730 RVA: 0x000127ED File Offset: 0x000109ED
	public string UserId { get; protected internal set; }

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060002DB RID: 731 RVA: 0x000127F6 File Offset: 0x000109F6
	// (set) Token: 0x060002DC RID: 732 RVA: 0x000127FE File Offset: 0x000109FE
	public bool IsOnline { get; protected internal set; }

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060002DD RID: 733 RVA: 0x00012807 File Offset: 0x00010A07
	// (set) Token: 0x060002DE RID: 734 RVA: 0x0001280F File Offset: 0x00010A0F
	public string Room { get; protected internal set; }

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060002DF RID: 735 RVA: 0x00012818 File Offset: 0x00010A18
	public bool IsInRoom
	{
		get
		{
			return this.IsOnline && !string.IsNullOrEmpty(this.Room);
		}
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x00012832 File Offset: 0x00010A32
	public override string ToString()
	{
		return string.Format("{0}\t is: {1}", this.UserId, (!this.IsOnline) ? "offline" : (this.IsInRoom ? "playing" : "on master"));
	}
}
