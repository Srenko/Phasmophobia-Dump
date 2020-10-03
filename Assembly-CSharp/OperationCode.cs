using System;

// Token: 0x02000090 RID: 144
public class OperationCode
{
	// Token: 0x040003E6 RID: 998
	[Obsolete("Exchanging encrpytion keys is done internally in the lib now. Don't expect this operation-result.")]
	public const byte ExchangeKeysForEncryption = 250;

	// Token: 0x040003E7 RID: 999
	[Obsolete]
	public const byte Join = 255;

	// Token: 0x040003E8 RID: 1000
	public const byte AuthenticateOnce = 231;

	// Token: 0x040003E9 RID: 1001
	public const byte Authenticate = 230;

	// Token: 0x040003EA RID: 1002
	public const byte JoinLobby = 229;

	// Token: 0x040003EB RID: 1003
	public const byte LeaveLobby = 228;

	// Token: 0x040003EC RID: 1004
	public const byte CreateGame = 227;

	// Token: 0x040003ED RID: 1005
	public const byte JoinGame = 226;

	// Token: 0x040003EE RID: 1006
	public const byte JoinRandomGame = 225;

	// Token: 0x040003EF RID: 1007
	public const byte Leave = 254;

	// Token: 0x040003F0 RID: 1008
	public const byte RaiseEvent = 253;

	// Token: 0x040003F1 RID: 1009
	public const byte SetProperties = 252;

	// Token: 0x040003F2 RID: 1010
	public const byte GetProperties = 251;

	// Token: 0x040003F3 RID: 1011
	public const byte ChangeGroups = 248;

	// Token: 0x040003F4 RID: 1012
	public const byte FindFriends = 222;

	// Token: 0x040003F5 RID: 1013
	public const byte GetLobbyStats = 221;

	// Token: 0x040003F6 RID: 1014
	public const byte GetRegions = 220;

	// Token: 0x040003F7 RID: 1015
	public const byte WebRpc = 219;

	// Token: 0x040003F8 RID: 1016
	public const byte ServerSettings = 218;

	// Token: 0x040003F9 RID: 1017
	public const byte GetGameList = 217;
}
