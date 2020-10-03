using System;

// Token: 0x0200009F RID: 159
public enum ClientState
{
	// Token: 0x0400044D RID: 1101
	Uninitialized,
	// Token: 0x0400044E RID: 1102
	PeerCreated,
	// Token: 0x0400044F RID: 1103
	Queued,
	// Token: 0x04000450 RID: 1104
	Authenticated,
	// Token: 0x04000451 RID: 1105
	JoinedLobby,
	// Token: 0x04000452 RID: 1106
	DisconnectingFromMasterserver,
	// Token: 0x04000453 RID: 1107
	ConnectingToGameserver,
	// Token: 0x04000454 RID: 1108
	ConnectedToGameserver,
	// Token: 0x04000455 RID: 1109
	Joining,
	// Token: 0x04000456 RID: 1110
	Joined,
	// Token: 0x04000457 RID: 1111
	Leaving,
	// Token: 0x04000458 RID: 1112
	DisconnectingFromGameserver,
	// Token: 0x04000459 RID: 1113
	ConnectingToMasterserver,
	// Token: 0x0400045A RID: 1114
	QueuedComingFromGameserver,
	// Token: 0x0400045B RID: 1115
	Disconnecting,
	// Token: 0x0400045C RID: 1116
	Disconnected,
	// Token: 0x0400045D RID: 1117
	ConnectedToMaster,
	// Token: 0x0400045E RID: 1118
	ConnectingToNameServer,
	// Token: 0x0400045F RID: 1119
	ConnectedToNameServer,
	// Token: 0x04000460 RID: 1120
	DisconnectingFromNameServer,
	// Token: 0x04000461 RID: 1121
	Authenticating
}
