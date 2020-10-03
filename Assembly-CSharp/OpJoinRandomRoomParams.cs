using System;
using ExitGames.Client.Photon;

// Token: 0x02000089 RID: 137
internal class OpJoinRandomRoomParams
{
	// Token: 0x0400035A RID: 858
	public Hashtable ExpectedCustomRoomProperties;

	// Token: 0x0400035B RID: 859
	public byte ExpectedMaxPlayers;

	// Token: 0x0400035C RID: 860
	public MatchmakingMode MatchingType;

	// Token: 0x0400035D RID: 861
	public TypedLobby TypedLobby;

	// Token: 0x0400035E RID: 862
	public string SqlLobbyFilter;

	// Token: 0x0400035F RID: 863
	public string[] ExpectedUsers;
}
