using System;

// Token: 0x0200009A RID: 154
public class TypedLobbyInfo : TypedLobby
{
	// Token: 0x06000327 RID: 807 RVA: 0x00013998 File Offset: 0x00011B98
	public override string ToString()
	{
		return string.Format("TypedLobbyInfo '{0}'[{1}] rooms: {2} players: {3}", new object[]
		{
			this.Name,
			this.Type,
			this.RoomCount,
			this.PlayerCount
		});
	}

	// Token: 0x04000434 RID: 1076
	public int PlayerCount;

	// Token: 0x04000435 RID: 1077
	public int RoomCount;
}
