using System;
using ExitGames.Client.Photon;

// Token: 0x0200008A RID: 138
internal class EnterRoomParams
{
	// Token: 0x04000360 RID: 864
	public string RoomName;

	// Token: 0x04000361 RID: 865
	public RoomOptions RoomOptions;

	// Token: 0x04000362 RID: 866
	public TypedLobby Lobby;

	// Token: 0x04000363 RID: 867
	public Hashtable PlayerProperties;

	// Token: 0x04000364 RID: 868
	public bool OnGameServer = true;

	// Token: 0x04000365 RID: 869
	public bool CreateIfNotExists;

	// Token: 0x04000366 RID: 870
	public bool RejoinOnly;

	// Token: 0x04000367 RID: 871
	public string[] ExpectedUsers;
}
