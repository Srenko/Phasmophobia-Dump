using System;

// Token: 0x0200008F RID: 143
public class ParameterCode
{
	// Token: 0x040003A1 RID: 929
	public const byte SuppressRoomEvents = 237;

	// Token: 0x040003A2 RID: 930
	public const byte EmptyRoomTTL = 236;

	// Token: 0x040003A3 RID: 931
	public const byte PlayerTTL = 235;

	// Token: 0x040003A4 RID: 932
	public const byte EventForward = 234;

	// Token: 0x040003A5 RID: 933
	[Obsolete("Use: IsInactive")]
	public const byte IsComingBack = 233;

	// Token: 0x040003A6 RID: 934
	public const byte IsInactive = 233;

	// Token: 0x040003A7 RID: 935
	public const byte CheckUserOnJoin = 232;

	// Token: 0x040003A8 RID: 936
	public const byte ExpectedValues = 231;

	// Token: 0x040003A9 RID: 937
	public const byte Address = 230;

	// Token: 0x040003AA RID: 938
	public const byte PeerCount = 229;

	// Token: 0x040003AB RID: 939
	public const byte GameCount = 228;

	// Token: 0x040003AC RID: 940
	public const byte MasterPeerCount = 227;

	// Token: 0x040003AD RID: 941
	public const byte UserId = 225;

	// Token: 0x040003AE RID: 942
	public const byte ApplicationId = 224;

	// Token: 0x040003AF RID: 943
	public const byte Position = 223;

	// Token: 0x040003B0 RID: 944
	public const byte MatchMakingType = 223;

	// Token: 0x040003B1 RID: 945
	public const byte GameList = 222;

	// Token: 0x040003B2 RID: 946
	public const byte Secret = 221;

	// Token: 0x040003B3 RID: 947
	public const byte AppVersion = 220;

	// Token: 0x040003B4 RID: 948
	[Obsolete("TCP routing was removed after becoming obsolete.")]
	public const byte AzureNodeInfo = 210;

	// Token: 0x040003B5 RID: 949
	[Obsolete("TCP routing was removed after becoming obsolete.")]
	public const byte AzureLocalNodeId = 209;

	// Token: 0x040003B6 RID: 950
	[Obsolete("TCP routing was removed after becoming obsolete.")]
	public const byte AzureMasterNodeId = 208;

	// Token: 0x040003B7 RID: 951
	public const byte RoomName = 255;

	// Token: 0x040003B8 RID: 952
	public const byte Broadcast = 250;

	// Token: 0x040003B9 RID: 953
	public const byte ActorList = 252;

	// Token: 0x040003BA RID: 954
	public const byte ActorNr = 254;

	// Token: 0x040003BB RID: 955
	public const byte PlayerProperties = 249;

	// Token: 0x040003BC RID: 956
	public const byte CustomEventContent = 245;

	// Token: 0x040003BD RID: 957
	public const byte Data = 245;

	// Token: 0x040003BE RID: 958
	public const byte Code = 244;

	// Token: 0x040003BF RID: 959
	public const byte GameProperties = 248;

	// Token: 0x040003C0 RID: 960
	public const byte Properties = 251;

	// Token: 0x040003C1 RID: 961
	public const byte TargetActorNr = 253;

	// Token: 0x040003C2 RID: 962
	public const byte ReceiverGroup = 246;

	// Token: 0x040003C3 RID: 963
	public const byte Cache = 247;

	// Token: 0x040003C4 RID: 964
	public const byte CleanupCacheOnLeave = 241;

	// Token: 0x040003C5 RID: 965
	public const byte Group = 240;

	// Token: 0x040003C6 RID: 966
	public const byte Remove = 239;

	// Token: 0x040003C7 RID: 967
	public const byte PublishUserId = 239;

	// Token: 0x040003C8 RID: 968
	public const byte Add = 238;

	// Token: 0x040003C9 RID: 969
	public const byte Info = 218;

	// Token: 0x040003CA RID: 970
	public const byte ClientAuthenticationType = 217;

	// Token: 0x040003CB RID: 971
	public const byte ClientAuthenticationParams = 216;

	// Token: 0x040003CC RID: 972
	public const byte JoinMode = 215;

	// Token: 0x040003CD RID: 973
	public const byte ClientAuthenticationData = 214;

	// Token: 0x040003CE RID: 974
	public const byte MasterClientId = 203;

	// Token: 0x040003CF RID: 975
	public const byte FindFriendsRequestList = 1;

	// Token: 0x040003D0 RID: 976
	public const byte FindFriendsOptions = 2;

	// Token: 0x040003D1 RID: 977
	public const byte FindFriendsResponseOnlineList = 1;

	// Token: 0x040003D2 RID: 978
	public const byte FindFriendsResponseRoomIdList = 2;

	// Token: 0x040003D3 RID: 979
	public const byte LobbyName = 213;

	// Token: 0x040003D4 RID: 980
	public const byte LobbyType = 212;

	// Token: 0x040003D5 RID: 981
	public const byte LobbyStats = 211;

	// Token: 0x040003D6 RID: 982
	public const byte Region = 210;

	// Token: 0x040003D7 RID: 983
	public const byte UriPath = 209;

	// Token: 0x040003D8 RID: 984
	public const byte WebRpcParameters = 208;

	// Token: 0x040003D9 RID: 985
	public const byte WebRpcReturnCode = 207;

	// Token: 0x040003DA RID: 986
	public const byte WebRpcReturnMessage = 206;

	// Token: 0x040003DB RID: 987
	public const byte CacheSliceIndex = 205;

	// Token: 0x040003DC RID: 988
	public const byte Plugins = 204;

	// Token: 0x040003DD RID: 989
	public const byte NickName = 202;

	// Token: 0x040003DE RID: 990
	public const byte PluginName = 201;

	// Token: 0x040003DF RID: 991
	public const byte PluginVersion = 200;

	// Token: 0x040003E0 RID: 992
	public const byte Cluster = 196;

	// Token: 0x040003E1 RID: 993
	public const byte ExpectedProtocol = 195;

	// Token: 0x040003E2 RID: 994
	public const byte CustomInitData = 194;

	// Token: 0x040003E3 RID: 995
	public const byte EncryptionMode = 193;

	// Token: 0x040003E4 RID: 996
	public const byte EncryptionData = 192;

	// Token: 0x040003E5 RID: 997
	public const byte RoomOptionFlags = 191;
}
