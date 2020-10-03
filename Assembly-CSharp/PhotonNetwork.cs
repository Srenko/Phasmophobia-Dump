using System;
using System.Collections.Generic;
using System.Diagnostics;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000AE RID: 174
public static class PhotonNetwork
{
	// Token: 0x1700006C RID: 108
	// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001A5D4 File Offset: 0x000187D4
	// (set) Token: 0x0600041E RID: 1054 RVA: 0x0001A5DB File Offset: 0x000187DB
	public static string gameVersion { get; set; }

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x0600041F RID: 1055 RVA: 0x0001A5E3 File Offset: 0x000187E3
	public static string ServerAddress
	{
		get
		{
			if (PhotonNetwork.networkingPeer == null)
			{
				return "<not connected>";
			}
			return PhotonNetwork.networkingPeer.ServerAddress;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06000420 RID: 1056 RVA: 0x0001A5FC File Offset: 0x000187FC
	public static CloudRegionCode CloudRegion
	{
		get
		{
			if (PhotonNetwork.networkingPeer == null || !PhotonNetwork.connected || PhotonNetwork.Server == ServerConnection.NameServer)
			{
				return CloudRegionCode.none;
			}
			return PhotonNetwork.networkingPeer.CloudRegion;
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06000421 RID: 1057 RVA: 0x0001A620 File Offset: 0x00018820
	public static string CurrentCluster
	{
		get
		{
			if (PhotonNetwork.networkingPeer == null)
			{
				return null;
			}
			return PhotonNetwork.networkingPeer.CurrentCluster;
		}
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001A638 File Offset: 0x00018838
	public static bool connected
	{
		get
		{
			return PhotonNetwork.offlineMode || (PhotonNetwork.networkingPeer != null && (!PhotonNetwork.networkingPeer.IsInitialConnect && PhotonNetwork.networkingPeer.State != ClientState.PeerCreated && PhotonNetwork.networkingPeer.State != ClientState.Disconnected && PhotonNetwork.networkingPeer.State != ClientState.Disconnecting) && PhotonNetwork.networkingPeer.State != ClientState.ConnectingToNameServer);
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001A69F File Offset: 0x0001889F
	public static bool connecting
	{
		get
		{
			return PhotonNetwork.networkingPeer.IsInitialConnect && !PhotonNetwork.offlineMode;
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001A6B8 File Offset: 0x000188B8
	public static bool connectedAndReady
	{
		get
		{
			if (!PhotonNetwork.connected)
			{
				return false;
			}
			if (PhotonNetwork.offlineMode)
			{
				return true;
			}
			ClientState connectionStateDetailed = PhotonNetwork.connectionStateDetailed;
			if (connectionStateDetailed <= ClientState.ConnectingToGameserver)
			{
				if (connectionStateDetailed != ClientState.PeerCreated && connectionStateDetailed != ClientState.ConnectingToGameserver)
				{
					return true;
				}
			}
			else if (connectionStateDetailed != ClientState.Joining)
			{
				switch (connectionStateDetailed)
				{
				case ClientState.ConnectingToMasterserver:
				case ClientState.Disconnecting:
				case ClientState.Disconnected:
				case ClientState.ConnectingToNameServer:
				case ClientState.Authenticating:
					break;
				case ClientState.QueuedComingFromGameserver:
				case ClientState.ConnectedToMaster:
				case ClientState.ConnectedToNameServer:
				case ClientState.DisconnectingFromNameServer:
					return true;
				default:
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001A724 File Offset: 0x00018924
	public static ConnectionState connectionState
	{
		get
		{
			if (PhotonNetwork.offlineMode)
			{
				return ConnectionState.Connected;
			}
			if (PhotonNetwork.networkingPeer == null)
			{
				return ConnectionState.Disconnected;
			}
			PeerStateValue peerState = PhotonNetwork.networkingPeer.PeerState;
			switch (peerState)
			{
			case PeerStateValue.Disconnected:
				return ConnectionState.Disconnected;
			case PeerStateValue.Connecting:
				return ConnectionState.Connecting;
			case (PeerStateValue)2:
				break;
			case PeerStateValue.Connected:
				return ConnectionState.Connected;
			case PeerStateValue.Disconnecting:
				return ConnectionState.Disconnecting;
			default:
				if (peerState == PeerStateValue.InitializingApplication)
				{
					return ConnectionState.InitializingApplication;
				}
				break;
			}
			return ConnectionState.Disconnected;
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06000426 RID: 1062 RVA: 0x0001A77A File Offset: 0x0001897A
	public static ClientState connectionStateDetailed
	{
		get
		{
			if (PhotonNetwork.offlineMode)
			{
				if (PhotonNetwork.offlineModeRoom == null)
				{
					return ClientState.ConnectedToMaster;
				}
				return ClientState.Joined;
			}
			else
			{
				if (PhotonNetwork.networkingPeer == null)
				{
					return ClientState.Disconnected;
				}
				return PhotonNetwork.networkingPeer.State;
			}
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001A7A4 File Offset: 0x000189A4
	public static ServerConnection Server
	{
		get
		{
			if (PhotonNetwork.networkingPeer == null)
			{
				return ServerConnection.NameServer;
			}
			return PhotonNetwork.networkingPeer.Server;
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000428 RID: 1064 RVA: 0x0001A7B9 File Offset: 0x000189B9
	// (set) Token: 0x06000429 RID: 1065 RVA: 0x0001A7CE File Offset: 0x000189CE
	public static AuthenticationValues AuthValues
	{
		get
		{
			if (PhotonNetwork.networkingPeer == null)
			{
				return null;
			}
			return PhotonNetwork.networkingPeer.AuthValues;
		}
		set
		{
			if (PhotonNetwork.networkingPeer != null)
			{
				PhotonNetwork.networkingPeer.AuthValues = value;
			}
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x0600042A RID: 1066 RVA: 0x0001A7E2 File Offset: 0x000189E2
	public static Room room
	{
		get
		{
			if (PhotonNetwork.isOfflineMode)
			{
				return PhotonNetwork.offlineModeRoom;
			}
			return PhotonNetwork.networkingPeer.CurrentRoom;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x0600042B RID: 1067 RVA: 0x0001A7FB File Offset: 0x000189FB
	public static PhotonPlayer player
	{
		get
		{
			if (PhotonNetwork.networkingPeer == null)
			{
				return null;
			}
			return PhotonNetwork.networkingPeer.LocalPlayer;
		}
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001A810 File Offset: 0x00018A10
	public static PhotonPlayer masterClient
	{
		get
		{
			if (PhotonNetwork.offlineMode)
			{
				return PhotonNetwork.player;
			}
			if (PhotonNetwork.networkingPeer == null)
			{
				return null;
			}
			return PhotonNetwork.networkingPeer.GetPlayerWithId(PhotonNetwork.networkingPeer.mMasterClientId);
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001A83C File Offset: 0x00018A3C
	// (set) Token: 0x0600042E RID: 1070 RVA: 0x0001A848 File Offset: 0x00018A48
	public static string playerName
	{
		get
		{
			return PhotonNetwork.networkingPeer.PlayerName;
		}
		set
		{
			PhotonNetwork.networkingPeer.PlayerName = value;
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001A855 File Offset: 0x00018A55
	public static PhotonPlayer[] playerList
	{
		get
		{
			if (PhotonNetwork.networkingPeer == null)
			{
				return new PhotonPlayer[0];
			}
			return PhotonNetwork.networkingPeer.mPlayerListCopy;
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001A86F File Offset: 0x00018A6F
	public static PhotonPlayer[] otherPlayers
	{
		get
		{
			if (PhotonNetwork.networkingPeer == null)
			{
				return new PhotonPlayer[0];
			}
			return PhotonNetwork.networkingPeer.mOtherPlayerListCopy;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001A889 File Offset: 0x00018A89
	// (set) Token: 0x06000432 RID: 1074 RVA: 0x0001A890 File Offset: 0x00018A90
	public static List<FriendInfo> Friends { get; internal set; }

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000433 RID: 1075 RVA: 0x0001A898 File Offset: 0x00018A98
	public static int FriendsListAge
	{
		get
		{
			if (PhotonNetwork.networkingPeer == null)
			{
				return 0;
			}
			return PhotonNetwork.networkingPeer.FriendListAge;
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000434 RID: 1076 RVA: 0x0001A8AD File Offset: 0x00018AAD
	// (set) Token: 0x06000435 RID: 1077 RVA: 0x0001A8B9 File Offset: 0x00018AB9
	public static IPunPrefabPool PrefabPool
	{
		get
		{
			return PhotonNetwork.networkingPeer.ObjectPool;
		}
		set
		{
			PhotonNetwork.networkingPeer.ObjectPool = value;
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001A8C6 File Offset: 0x00018AC6
	// (set) Token: 0x06000437 RID: 1079 RVA: 0x0001A8D0 File Offset: 0x00018AD0
	public static bool offlineMode
	{
		get
		{
			return PhotonNetwork.isOfflineMode;
		}
		set
		{
			if (value == PhotonNetwork.isOfflineMode)
			{
				return;
			}
			if (value && PhotonNetwork.connected)
			{
				Debug.LogError("Can't start OFFLINE mode while connected!");
				return;
			}
			if (PhotonNetwork.networkingPeer.PeerState != PeerStateValue.Disconnected)
			{
				PhotonNetwork.networkingPeer.Disconnect();
			}
			PhotonNetwork.isOfflineMode = value;
			if (PhotonNetwork.isOfflineMode)
			{
				PhotonNetwork.networkingPeer.ChangeLocalID(-1);
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectedToMaster, Array.Empty<object>());
				return;
			}
			PhotonNetwork.offlineModeRoom = null;
			PhotonNetwork.networkingPeer.ChangeLocalID(-1);
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001A947 File Offset: 0x00018B47
	// (set) Token: 0x06000439 RID: 1081 RVA: 0x0001A94E File Offset: 0x00018B4E
	public static bool automaticallySyncScene
	{
		get
		{
			return PhotonNetwork._mAutomaticallySyncScene;
		}
		set
		{
			PhotonNetwork._mAutomaticallySyncScene = value;
			if (PhotonNetwork._mAutomaticallySyncScene && PhotonNetwork.room != null)
			{
				PhotonNetwork.networkingPeer.LoadLevelIfSynced();
			}
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x0600043A RID: 1082 RVA: 0x0001A96E File Offset: 0x00018B6E
	// (set) Token: 0x0600043B RID: 1083 RVA: 0x0001A975 File Offset: 0x00018B75
	public static bool autoCleanUpPlayerObjects
	{
		get
		{
			return PhotonNetwork.m_autoCleanUpPlayerObjects;
		}
		set
		{
			if (PhotonNetwork.room != null)
			{
				Debug.LogError("Setting autoCleanUpPlayerObjects while in a room is not supported.");
				return;
			}
			PhotonNetwork.m_autoCleanUpPlayerObjects = value;
		}
	}

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001A98F File Offset: 0x00018B8F
	// (set) Token: 0x0600043D RID: 1085 RVA: 0x0001A99B File Offset: 0x00018B9B
	public static bool autoJoinLobby
	{
		get
		{
			return PhotonNetwork.PhotonServerSettings.JoinLobby;
		}
		set
		{
			PhotonNetwork.PhotonServerSettings.JoinLobby = value;
		}
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001A9A8 File Offset: 0x00018BA8
	// (set) Token: 0x0600043F RID: 1087 RVA: 0x0001A9B4 File Offset: 0x00018BB4
	public static bool EnableLobbyStatistics
	{
		get
		{
			return PhotonNetwork.PhotonServerSettings.EnableLobbyStatistics;
		}
		set
		{
			PhotonNetwork.PhotonServerSettings.EnableLobbyStatistics = value;
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001A9C1 File Offset: 0x00018BC1
	// (set) Token: 0x06000441 RID: 1089 RVA: 0x0001A9CD File Offset: 0x00018BCD
	public static List<TypedLobbyInfo> LobbyStatistics
	{
		get
		{
			return PhotonNetwork.networkingPeer.LobbyStatistics;
		}
		private set
		{
			PhotonNetwork.networkingPeer.LobbyStatistics = value;
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x06000442 RID: 1090 RVA: 0x0001A9DA File Offset: 0x00018BDA
	public static bool insideLobby
	{
		get
		{
			return PhotonNetwork.networkingPeer.insideLobby;
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x06000443 RID: 1091 RVA: 0x0001A9E6 File Offset: 0x00018BE6
	// (set) Token: 0x06000444 RID: 1092 RVA: 0x0001A9F2 File Offset: 0x00018BF2
	public static TypedLobby lobby
	{
		get
		{
			return PhotonNetwork.networkingPeer.lobby;
		}
		set
		{
			PhotonNetwork.networkingPeer.lobby = value;
		}
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000445 RID: 1093 RVA: 0x0001A9FF File Offset: 0x00018BFF
	// (set) Token: 0x06000446 RID: 1094 RVA: 0x0001AA0C File Offset: 0x00018C0C
	public static int sendRate
	{
		get
		{
			return 1000 / PhotonNetwork.sendInterval;
		}
		set
		{
			PhotonNetwork.sendInterval = 1000 / value;
			if (PhotonNetwork.photonMono != null)
			{
				PhotonNetwork.photonMono.updateInterval = PhotonNetwork.sendInterval;
			}
			if (value < PhotonNetwork.sendRateOnSerialize)
			{
				PhotonNetwork.sendRateOnSerialize = value;
			}
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06000447 RID: 1095 RVA: 0x0001AA44 File Offset: 0x00018C44
	// (set) Token: 0x06000448 RID: 1096 RVA: 0x0001AA54 File Offset: 0x00018C54
	public static int sendRateOnSerialize
	{
		get
		{
			return 1000 / PhotonNetwork.sendIntervalOnSerialize;
		}
		set
		{
			if (value > PhotonNetwork.sendRate)
			{
				Debug.LogError("Error: Can not set the OnSerialize rate higher than the overall SendRate.");
				value = PhotonNetwork.sendRate;
			}
			PhotonNetwork.sendIntervalOnSerialize = 1000 / value;
			if (PhotonNetwork.photonMono != null)
			{
				PhotonNetwork.photonMono.updateIntervalOnSerialize = PhotonNetwork.sendIntervalOnSerialize;
			}
		}
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001AAA2 File Offset: 0x00018CA2
	// (set) Token: 0x0600044A RID: 1098 RVA: 0x0001AAA9 File Offset: 0x00018CA9
	public static bool isMessageQueueRunning
	{
		get
		{
			return PhotonNetwork.m_isMessageQueueRunning;
		}
		set
		{
			if (value)
			{
				PhotonHandler.StartFallbackSendAckThread();
			}
			PhotonNetwork.networkingPeer.IsSendingOnlyAcks = !value;
			PhotonNetwork.m_isMessageQueueRunning = value;
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001AAC7 File Offset: 0x00018CC7
	public static double time
	{
		get
		{
			return PhotonNetwork.ServerTimestamp / 1000.0;
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x0600044C RID: 1100 RVA: 0x0001AADA File Offset: 0x00018CDA
	public static int ServerTimestamp
	{
		get
		{
			if (PhotonNetwork.offlineMode)
			{
				return (int)PhotonNetwork.startupStopwatch.ElapsedMilliseconds;
			}
			return PhotonNetwork.networkingPeer.ServerTimeInMilliSeconds;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x0600044D RID: 1101 RVA: 0x0001AAF9 File Offset: 0x00018CF9
	public static bool isMasterClient
	{
		get
		{
			return PhotonNetwork.offlineMode || PhotonNetwork.networkingPeer.mMasterClientId == PhotonNetwork.player.ID;
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x0600044E RID: 1102 RVA: 0x0001AB1A File Offset: 0x00018D1A
	public static bool inRoom
	{
		get
		{
			return PhotonNetwork.connectionStateDetailed == ClientState.Joined;
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x0600044F RID: 1103 RVA: 0x0001AB25 File Offset: 0x00018D25
	public static bool isNonMasterClientInRoom
	{
		get
		{
			return !PhotonNetwork.isMasterClient && PhotonNetwork.room != null;
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001AB38 File Offset: 0x00018D38
	public static int countOfPlayersOnMaster
	{
		get
		{
			return PhotonNetwork.networkingPeer.PlayersOnMasterCount;
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001AB44 File Offset: 0x00018D44
	public static int countOfPlayersInRooms
	{
		get
		{
			return PhotonNetwork.networkingPeer.PlayersInRoomsCount;
		}
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x06000452 RID: 1106 RVA: 0x0001AB50 File Offset: 0x00018D50
	public static int countOfPlayers
	{
		get
		{
			return PhotonNetwork.networkingPeer.PlayersInRoomsCount + PhotonNetwork.networkingPeer.PlayersOnMasterCount;
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x06000453 RID: 1107 RVA: 0x0001AB67 File Offset: 0x00018D67
	public static int countOfRooms
	{
		get
		{
			return PhotonNetwork.networkingPeer.RoomsCount;
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x06000454 RID: 1108 RVA: 0x0001AB73 File Offset: 0x00018D73
	// (set) Token: 0x06000455 RID: 1109 RVA: 0x0001AB7F File Offset: 0x00018D7F
	public static bool NetworkStatisticsEnabled
	{
		get
		{
			return PhotonNetwork.networkingPeer.TrafficStatsEnabled;
		}
		set
		{
			PhotonNetwork.networkingPeer.TrafficStatsEnabled = value;
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x06000456 RID: 1110 RVA: 0x0001AB8C File Offset: 0x00018D8C
	public static int ResentReliableCommands
	{
		get
		{
			return PhotonNetwork.networkingPeer.ResentReliableCommands;
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x06000457 RID: 1111 RVA: 0x0001AB98 File Offset: 0x00018D98
	// (set) Token: 0x06000458 RID: 1112 RVA: 0x0001ABA4 File Offset: 0x00018DA4
	public static bool CrcCheckEnabled
	{
		get
		{
			return PhotonNetwork.networkingPeer.CrcEnabled;
		}
		set
		{
			if (!PhotonNetwork.connected && !PhotonNetwork.connecting)
			{
				PhotonNetwork.networkingPeer.CrcEnabled = value;
				return;
			}
			Debug.Log("Can't change CrcCheckEnabled while being connected. CrcCheckEnabled stays " + PhotonNetwork.networkingPeer.CrcEnabled.ToString());
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000459 RID: 1113 RVA: 0x0001ABEC File Offset: 0x00018DEC
	public static int PacketLossByCrcCheck
	{
		get
		{
			return PhotonNetwork.networkingPeer.PacketLossByCrc;
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x0600045A RID: 1114 RVA: 0x0001ABF8 File Offset: 0x00018DF8
	// (set) Token: 0x0600045B RID: 1115 RVA: 0x0001AC04 File Offset: 0x00018E04
	public static int MaxResendsBeforeDisconnect
	{
		get
		{
			return PhotonNetwork.networkingPeer.SentCountAllowance;
		}
		set
		{
			if (value < 3)
			{
				value = 3;
			}
			if (value > 10)
			{
				value = 10;
			}
			PhotonNetwork.networkingPeer.SentCountAllowance = value;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x0600045C RID: 1116 RVA: 0x0001AC21 File Offset: 0x00018E21
	// (set) Token: 0x0600045D RID: 1117 RVA: 0x0001AC2D File Offset: 0x00018E2D
	public static int QuickResends
	{
		get
		{
			return (int)PhotonNetwork.networkingPeer.QuickResendAttempts;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (value > 3)
			{
				value = 3;
			}
			PhotonNetwork.networkingPeer.QuickResendAttempts = (byte)value;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x0600045E RID: 1118 RVA: 0x0001AC49 File Offset: 0x00018E49
	// (set) Token: 0x0600045F RID: 1119 RVA: 0x0001AC50 File Offset: 0x00018E50
	public static bool UseAlternativeUdpPorts { get; set; }

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000460 RID: 1120 RVA: 0x0001AC58 File Offset: 0x00018E58
	// (remove) Token: 0x06000461 RID: 1121 RVA: 0x0001AC8C File Offset: 0x00018E8C
	public static event PhotonNetwork.EventCallback OnEventCall;

	// Token: 0x06000462 RID: 1122 RVA: 0x0001ACC0 File Offset: 0x00018EC0
	static PhotonNetwork()
	{
		if (PhotonNetwork.PhotonServerSettings != null)
		{
			Application.runInBackground = PhotonNetwork.PhotonServerSettings.RunInBackground;
		}
		GameObject gameObject = new GameObject();
		PhotonNetwork.photonMono = gameObject.AddComponent<PhotonHandler>();
		gameObject.name = "PhotonMono";
		gameObject.hideFlags = HideFlags.HideInHierarchy;
		ConnectionProtocol protocol = PhotonNetwork.PhotonServerSettings.Protocol;
		PhotonNetwork.networkingPeer = new NetworkingPeer(string.Empty, protocol);
		PhotonNetwork.networkingPeer.QuickResendAttempts = 2;
		PhotonNetwork.networkingPeer.SentCountAllowance = 7;
		PhotonNetwork.startupStopwatch = new Stopwatch();
		PhotonNetwork.startupStopwatch.Start();
		PhotonNetwork.networkingPeer.LocalMsTimestampDelegate = (() => (int)PhotonNetwork.startupStopwatch.ElapsedMilliseconds);
		CustomTypes.Register();
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x0001AE31 File Offset: 0x00019031
	public static void SwitchToProtocol(ConnectionProtocol cp)
	{
		PhotonNetwork.networkingPeer.TransportProtocol = cp;
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x0001AE40 File Offset: 0x00019040
	public static bool ConnectUsingSettings(string gameVersion)
	{
		if (PhotonNetwork.networkingPeer.PeerState != PeerStateValue.Disconnected)
		{
			Debug.LogWarning("ConnectUsingSettings() failed. Can only connect while in state 'Disconnected'. Current state: " + PhotonNetwork.networkingPeer.PeerState);
			return false;
		}
		if (PhotonNetwork.PhotonServerSettings == null)
		{
			Debug.LogError("Can't connect: Loading settings failed. ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
			return false;
		}
		if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.NotSet)
		{
			Debug.LogError("You did not select a Hosting Type in your PhotonServerSettings. Please set it up or don't use ConnectUsingSettings().");
			return false;
		}
		if (PhotonNetwork.logLevel == PhotonLogLevel.ErrorsOnly)
		{
			PhotonNetwork.logLevel = PhotonNetwork.PhotonServerSettings.PunLogging;
		}
		if (PhotonNetwork.networkingPeer.DebugOut == DebugLevel.ERROR)
		{
			PhotonNetwork.networkingPeer.DebugOut = PhotonNetwork.PhotonServerSettings.NetworkLogging;
		}
		PhotonNetwork.SwitchToProtocol(PhotonNetwork.PhotonServerSettings.Protocol);
		PhotonNetwork.networkingPeer.SetApp(PhotonNetwork.PhotonServerSettings.AppID, gameVersion);
		if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.OfflineMode)
		{
			PhotonNetwork.offlineMode = true;
			return true;
		}
		if (PhotonNetwork.offlineMode)
		{
			Debug.LogWarning("ConnectUsingSettings() disabled the offline mode. No longer offline.");
		}
		PhotonNetwork.offlineMode = false;
		PhotonNetwork.isMessageQueueRunning = true;
		PhotonNetwork.networkingPeer.IsInitialConnect = true;
		if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.SelfHosted)
		{
			PhotonNetwork.networkingPeer.IsUsingNameServer = false;
			PhotonNetwork.networkingPeer.MasterServerAddress = ((PhotonNetwork.PhotonServerSettings.ServerPort == 0) ? PhotonNetwork.PhotonServerSettings.ServerAddress : (PhotonNetwork.PhotonServerSettings.ServerAddress + ":" + PhotonNetwork.PhotonServerSettings.ServerPort));
			return PhotonNetwork.networkingPeer.Connect(PhotonNetwork.networkingPeer.MasterServerAddress, ServerConnection.MasterServer);
		}
		if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.BestRegion)
		{
			return PhotonNetwork.ConnectToBestCloudServer(gameVersion);
		}
		return PhotonNetwork.networkingPeer.ConnectToRegionMaster(PhotonNetwork.PhotonServerSettings.PreferredRegion, null);
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x0001AFE0 File Offset: 0x000191E0
	public static bool ConnectToMaster(string masterServerAddress, int port, string appID, string gameVersion)
	{
		if (PhotonNetwork.networkingPeer.PeerState != PeerStateValue.Disconnected)
		{
			Debug.LogWarning("ConnectToMaster() failed. Can only connect while in state 'Disconnected'. Current state: " + PhotonNetwork.networkingPeer.PeerState);
			return false;
		}
		if (PhotonNetwork.offlineMode)
		{
			PhotonNetwork.offlineMode = false;
			Debug.LogWarning("ConnectToMaster() disabled the offline mode. No longer offline.");
		}
		if (!PhotonNetwork.isMessageQueueRunning)
		{
			PhotonNetwork.isMessageQueueRunning = true;
			Debug.LogWarning("ConnectToMaster() enabled isMessageQueueRunning. Needs to be able to dispatch incoming messages.");
		}
		PhotonNetwork.networkingPeer.SetApp(appID, gameVersion);
		PhotonNetwork.networkingPeer.IsUsingNameServer = false;
		PhotonNetwork.networkingPeer.IsInitialConnect = true;
		PhotonNetwork.networkingPeer.MasterServerAddress = ((port == 0) ? masterServerAddress : (masterServerAddress + ":" + port));
		return PhotonNetwork.networkingPeer.Connect(PhotonNetwork.networkingPeer.MasterServerAddress, ServerConnection.MasterServer);
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x0001B0A0 File Offset: 0x000192A0
	public static bool Reconnect()
	{
		if (string.IsNullOrEmpty(PhotonNetwork.networkingPeer.MasterServerAddress))
		{
			Debug.LogWarning("Reconnect() failed. It seems the client wasn't connected before?! Current state: " + PhotonNetwork.networkingPeer.PeerState);
			return false;
		}
		if (PhotonNetwork.networkingPeer.PeerState != PeerStateValue.Disconnected)
		{
			Debug.LogWarning("Reconnect() failed. Can only connect while in state 'Disconnected'. Current state: " + PhotonNetwork.networkingPeer.PeerState);
			return false;
		}
		if (PhotonNetwork.offlineMode)
		{
			PhotonNetwork.offlineMode = false;
			Debug.LogWarning("Reconnect() disabled the offline mode. No longer offline.");
		}
		if (!PhotonNetwork.isMessageQueueRunning)
		{
			PhotonNetwork.isMessageQueueRunning = true;
			Debug.LogWarning("Reconnect() enabled isMessageQueueRunning. Needs to be able to dispatch incoming messages.");
		}
		PhotonNetwork.networkingPeer.IsUsingNameServer = false;
		PhotonNetwork.networkingPeer.IsInitialConnect = false;
		return PhotonNetwork.networkingPeer.ReconnectToMaster();
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x0001B158 File Offset: 0x00019358
	public static bool ReconnectAndRejoin()
	{
		if (PhotonNetwork.networkingPeer.PeerState != PeerStateValue.Disconnected)
		{
			Debug.LogWarning("ReconnectAndRejoin() failed. Can only connect while in state 'Disconnected'. Current state: " + PhotonNetwork.networkingPeer.PeerState);
			return false;
		}
		if (PhotonNetwork.offlineMode)
		{
			PhotonNetwork.offlineMode = false;
			Debug.LogWarning("ReconnectAndRejoin() disabled the offline mode. No longer offline.");
		}
		if (string.IsNullOrEmpty(PhotonNetwork.networkingPeer.GameServerAddress))
		{
			Debug.LogWarning("ReconnectAndRejoin() failed. It seems the client wasn't connected to a game server before (no address).");
			return false;
		}
		if (PhotonNetwork.networkingPeer.enterRoomParamsCache == null)
		{
			Debug.LogWarning("ReconnectAndRejoin() failed. It seems the client doesn't have any previous room to re-join.");
			return false;
		}
		if (!PhotonNetwork.isMessageQueueRunning)
		{
			PhotonNetwork.isMessageQueueRunning = true;
			Debug.LogWarning("ReconnectAndRejoin() enabled isMessageQueueRunning. Needs to be able to dispatch incoming messages.");
		}
		PhotonNetwork.networkingPeer.IsUsingNameServer = false;
		PhotonNetwork.networkingPeer.IsInitialConnect = false;
		return PhotonNetwork.networkingPeer.ReconnectAndRejoin();
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x0001B214 File Offset: 0x00019414
	public static bool ConnectToBestCloudServer(string gameVersion)
	{
		if (PhotonNetwork.networkingPeer.PeerState != PeerStateValue.Disconnected)
		{
			Debug.LogWarning("ConnectToBestCloudServer() failed. Can only connect while in state 'Disconnected'. Current state: " + PhotonNetwork.networkingPeer.PeerState);
			return false;
		}
		if (PhotonNetwork.PhotonServerSettings == null)
		{
			Debug.LogError("Can't connect: Loading settings failed. ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
			return false;
		}
		if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.OfflineMode)
		{
			return PhotonNetwork.ConnectUsingSettings(gameVersion);
		}
		PhotonNetwork.networkingPeer.IsInitialConnect = true;
		PhotonNetwork.networkingPeer.SetApp(PhotonNetwork.PhotonServerSettings.AppID, gameVersion);
		return PhotonNetwork.networkingPeer.ConnectToNameServer();
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x0001B2A4 File Offset: 0x000194A4
	public static bool ConnectToRegion(CloudRegionCode region, string gameVersion, string cluster = null)
	{
		if (PhotonNetwork.networkingPeer.PeerState != PeerStateValue.Disconnected)
		{
			Debug.LogWarning("ConnectToRegion() failed. Can only connect while in state 'Disconnected'. Current state: " + PhotonNetwork.networkingPeer.PeerState);
			return false;
		}
		if (PhotonNetwork.PhotonServerSettings == null)
		{
			Debug.LogError("Can't connect: ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
			return false;
		}
		if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.OfflineMode)
		{
			return PhotonNetwork.ConnectUsingSettings(gameVersion);
		}
		PhotonNetwork.networkingPeer.IsInitialConnect = true;
		PhotonNetwork.networkingPeer.SetApp(PhotonNetwork.PhotonServerSettings.AppID, gameVersion);
		if (region != CloudRegionCode.none)
		{
			Debug.Log("ConnectToRegion: " + region);
			return PhotonNetwork.networkingPeer.ConnectToRegionMaster(region, cluster);
		}
		return false;
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x0001B351 File Offset: 0x00019551
	public static void OverrideBestCloudServer(CloudRegionCode region)
	{
		PhotonHandler.BestRegionCodeInPreferences = region;
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x0001B359 File Offset: 0x00019559
	public static void RefreshCloudServerRating()
	{
		throw new NotImplementedException("not available at the moment");
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x0001B365 File Offset: 0x00019565
	public static void NetworkStatisticsReset()
	{
		PhotonNetwork.networkingPeer.TrafficStatsReset();
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0001B371 File Offset: 0x00019571
	public static string NetworkStatisticsToString()
	{
		if (PhotonNetwork.networkingPeer == null || PhotonNetwork.offlineMode)
		{
			return "Offline or in OfflineMode. No VitalStats available.";
		}
		return PhotonNetwork.networkingPeer.VitalStatsToString(false);
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00003F60 File Offset: 0x00002160
	[Obsolete("Used for compatibility with Unity networking only. Encryption is automatically initialized while connecting.")]
	public static void InitializeSecurity()
	{
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0001B392 File Offset: 0x00019592
	private static bool VerifyCanUseNetwork()
	{
		if (PhotonNetwork.connected)
		{
			return true;
		}
		Debug.LogError("Cannot send messages when not connected. Either connect to Photon OR use offline mode!");
		return false;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0001B3A8 File Offset: 0x000195A8
	public static void Disconnect()
	{
		if (PhotonNetwork.offlineMode)
		{
			PhotonNetwork.offlineMode = false;
			PhotonNetwork.offlineModeRoom = null;
			PhotonNetwork.networkingPeer.State = ClientState.Disconnecting;
			PhotonNetwork.networkingPeer.OnStatusChanged(StatusCode.Disconnect);
			return;
		}
		if (PhotonNetwork.networkingPeer == null)
		{
			return;
		}
		PhotonNetwork.networkingPeer.Disconnect();
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x0001B3F6 File Offset: 0x000195F6
	public static bool FindFriends(string[] friendsToFind, FindFriendsOptions options = null)
	{
		return PhotonNetwork.networkingPeer != null && !PhotonNetwork.isOfflineMode && PhotonNetwork.networkingPeer.OpFindFriends(friendsToFind, options);
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0001B414 File Offset: 0x00019614
	public static bool CreateRoom(string roomName)
	{
		return PhotonNetwork.CreateRoom(roomName, null, null, null);
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0001B41F File Offset: 0x0001961F
	public static bool CreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby)
	{
		return PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby, null);
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0001B42C File Offset: 0x0001962C
	public static bool CreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby, string[] expectedUsers)
	{
		if (PhotonNetwork.offlineMode)
		{
			if (PhotonNetwork.offlineModeRoom != null)
			{
				Debug.LogError("CreateRoom failed. In offline mode you still have to leave a room to enter another.");
				return false;
			}
			PhotonNetwork.EnterOfflineRoom(roomName, roomOptions, true);
			return true;
		}
		else
		{
			if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
			{
				Debug.LogError("CreateRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
				return false;
			}
			typedLobby = (typedLobby ?? (PhotonNetwork.networkingPeer.insideLobby ? PhotonNetwork.networkingPeer.lobby : null));
			EnterRoomParams enterRoomParams = new EnterRoomParams();
			enterRoomParams.RoomName = roomName;
			enterRoomParams.RoomOptions = roomOptions;
			enterRoomParams.Lobby = typedLobby;
			enterRoomParams.ExpectedUsers = expectedUsers;
			return PhotonNetwork.networkingPeer.OpCreateGame(enterRoomParams);
		}
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0001B4C9 File Offset: 0x000196C9
	public static bool JoinRoom(string roomName)
	{
		return PhotonNetwork.JoinRoom(roomName, null);
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x0001B4D4 File Offset: 0x000196D4
	public static bool JoinRoom(string roomName, string[] expectedUsers)
	{
		if (PhotonNetwork.offlineMode)
		{
			if (PhotonNetwork.offlineModeRoom != null)
			{
				Debug.LogError("JoinRoom failed. In offline mode you still have to leave a room to enter another.");
				return false;
			}
			PhotonNetwork.EnterOfflineRoom(roomName, null, true);
			return true;
		}
		else
		{
			if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
			{
				Debug.LogError("JoinRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
				return false;
			}
			if (string.IsNullOrEmpty(roomName))
			{
				Debug.LogError("JoinRoom failed. A roomname is required. If you don't know one, how will you join?");
				return false;
			}
			EnterRoomParams enterRoomParams = new EnterRoomParams();
			enterRoomParams.RoomName = roomName;
			enterRoomParams.ExpectedUsers = expectedUsers;
			return PhotonNetwork.networkingPeer.OpJoinRoom(enterRoomParams);
		}
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x0001B557 File Offset: 0x00019757
	public static bool JoinOrCreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby)
	{
		return PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby, null);
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x0001B564 File Offset: 0x00019764
	public static bool JoinOrCreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby, string[] expectedUsers)
	{
		if (PhotonNetwork.offlineMode)
		{
			if (PhotonNetwork.offlineModeRoom != null)
			{
				Debug.LogError("JoinOrCreateRoom failed. In offline mode you still have to leave a room to enter another.");
				return false;
			}
			PhotonNetwork.EnterOfflineRoom(roomName, roomOptions, true);
			return true;
		}
		else
		{
			if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
			{
				Debug.LogError("JoinOrCreateRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
				return false;
			}
			if (string.IsNullOrEmpty(roomName))
			{
				Debug.LogError("JoinOrCreateRoom failed. A roomname is required. If you don't know one, how will you join?");
				return false;
			}
			typedLobby = (typedLobby ?? (PhotonNetwork.networkingPeer.insideLobby ? PhotonNetwork.networkingPeer.lobby : null));
			EnterRoomParams enterRoomParams = new EnterRoomParams();
			enterRoomParams.RoomName = roomName;
			enterRoomParams.RoomOptions = roomOptions;
			enterRoomParams.Lobby = typedLobby;
			enterRoomParams.CreateIfNotExists = true;
			enterRoomParams.PlayerProperties = PhotonNetwork.player.CustomProperties;
			enterRoomParams.ExpectedUsers = expectedUsers;
			return PhotonNetwork.networkingPeer.OpJoinRoom(enterRoomParams);
		}
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x0001B62C File Offset: 0x0001982C
	public static bool JoinRandomRoom()
	{
		return PhotonNetwork.JoinRandomRoom(null, 0, MatchmakingMode.FillRoom, null, null, null);
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x0001B639 File Offset: 0x00019839
	public static bool JoinRandomRoom(Hashtable expectedCustomRoomProperties, byte expectedMaxPlayers)
	{
		return PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, expectedMaxPlayers, MatchmakingMode.FillRoom, null, null, null);
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x0001B648 File Offset: 0x00019848
	public static bool JoinRandomRoom(Hashtable expectedCustomRoomProperties, byte expectedMaxPlayers, MatchmakingMode matchingType, TypedLobby typedLobby, string sqlLobbyFilter, string[] expectedUsers = null)
	{
		if (PhotonNetwork.offlineMode)
		{
			if (PhotonNetwork.offlineModeRoom != null)
			{
				Debug.LogError("JoinRandomRoom failed. In offline mode you still have to leave a room to enter another.");
				return false;
			}
			PhotonNetwork.EnterOfflineRoom("offline room", null, true);
			return true;
		}
		else
		{
			if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
			{
				Debug.LogError("JoinRandomRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
				return false;
			}
			typedLobby = (typedLobby ?? (PhotonNetwork.networkingPeer.insideLobby ? PhotonNetwork.networkingPeer.lobby : null));
			OpJoinRandomRoomParams opJoinRandomRoomParams = new OpJoinRandomRoomParams();
			opJoinRandomRoomParams.ExpectedCustomRoomProperties = expectedCustomRoomProperties;
			opJoinRandomRoomParams.ExpectedMaxPlayers = expectedMaxPlayers;
			opJoinRandomRoomParams.MatchingType = matchingType;
			opJoinRandomRoomParams.TypedLobby = typedLobby;
			opJoinRandomRoomParams.SqlLobbyFilter = sqlLobbyFilter;
			opJoinRandomRoomParams.ExpectedUsers = expectedUsers;
			return PhotonNetwork.networkingPeer.OpJoinRandomRoom(opJoinRandomRoomParams);
		}
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x0001B6FC File Offset: 0x000198FC
	public static bool ReJoinRoom(string roomName)
	{
		if (PhotonNetwork.offlineMode)
		{
			Debug.LogError("ReJoinRoom failed due to offline mode.");
			return false;
		}
		if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
		{
			Debug.LogError("ReJoinRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
			return false;
		}
		if (string.IsNullOrEmpty(roomName))
		{
			Debug.LogError("ReJoinRoom failed. A roomname is required. If you don't know one, how will you join?");
			return false;
		}
		EnterRoomParams enterRoomParams = new EnterRoomParams();
		enterRoomParams.RoomName = roomName;
		enterRoomParams.RejoinOnly = true;
		enterRoomParams.PlayerProperties = PhotonNetwork.player.CustomProperties;
		return PhotonNetwork.networkingPeer.OpJoinRoom(enterRoomParams);
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x0001B780 File Offset: 0x00019980
	private static void EnterOfflineRoom(string roomName, RoomOptions roomOptions, bool createdRoom)
	{
		PhotonNetwork.offlineModeRoom = new Room(roomName, roomOptions);
		PhotonNetwork.networkingPeer.ChangeLocalID(1);
		PhotonNetwork.networkingPeer.State = ClientState.ConnectingToGameserver;
		PhotonNetwork.networkingPeer.OnStatusChanged(StatusCode.Connect);
		PhotonNetwork.offlineModeRoom.MasterClientId = 1;
		if (createdRoom)
		{
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom, Array.Empty<object>());
		}
		NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom, Array.Empty<object>());
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x0001B7E3 File Offset: 0x000199E3
	public static bool JoinLobby()
	{
		return PhotonNetwork.JoinLobby(null);
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x0001B7EB File Offset: 0x000199EB
	public static bool JoinLobby(TypedLobby typedLobby)
	{
		if (PhotonNetwork.connected && PhotonNetwork.Server == ServerConnection.MasterServer)
		{
			if (typedLobby == null)
			{
				typedLobby = TypedLobby.Default;
			}
			bool flag = PhotonNetwork.networkingPeer.OpJoinLobby(typedLobby);
			if (flag)
			{
				PhotonNetwork.networkingPeer.lobby = typedLobby;
			}
			return flag;
		}
		return false;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0001B820 File Offset: 0x00019A20
	public static bool LeaveLobby()
	{
		return PhotonNetwork.connected && PhotonNetwork.Server == ServerConnection.MasterServer && PhotonNetwork.networkingPeer.OpLeaveLobby();
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x0001B83C File Offset: 0x00019A3C
	public static bool LeaveRoom(bool becomeInactive = true)
	{
		if (PhotonNetwork.offlineMode)
		{
			PhotonNetwork.offlineModeRoom = null;
			PhotonNetwork.networkingPeer.State = ClientState.PeerCreated;
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLeftRoom, Array.Empty<object>());
			return true;
		}
		if (PhotonNetwork.room == null)
		{
			Debug.LogWarning("PhotonNetwork.room is null. You don't have to call LeaveRoom() when you're not in one. State: " + PhotonNetwork.connectionStateDetailed);
		}
		else
		{
			becomeInactive = (becomeInactive && PhotonNetwork.room.PlayerTtl != 0);
		}
		return PhotonNetwork.networkingPeer.OpLeaveRoom(becomeInactive);
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x0001B8B2 File Offset: 0x00019AB2
	public static bool GetCustomRoomList(TypedLobby typedLobby, string sqlLobbyFilter)
	{
		return PhotonNetwork.networkingPeer.OpGetGameList(typedLobby, sqlLobbyFilter);
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x0001B8C0 File Offset: 0x00019AC0
	public static RoomInfo[] GetRoomList()
	{
		if (PhotonNetwork.offlineMode || PhotonNetwork.networkingPeer == null)
		{
			return new RoomInfo[0];
		}
		return PhotonNetwork.networkingPeer.mGameListCopy;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0001B8E4 File Offset: 0x00019AE4
	public static void SetPlayerCustomProperties(Hashtable customProperties)
	{
		if (customProperties == null)
		{
			customProperties = new Hashtable();
			foreach (object obj in PhotonNetwork.player.CustomProperties.Keys)
			{
				customProperties[(string)obj] = null;
			}
		}
		if (PhotonNetwork.room != null && PhotonNetwork.room.IsLocalClientInside)
		{
			PhotonNetwork.player.SetCustomProperties(customProperties, null, false);
			return;
		}
		PhotonNetwork.player.InternalCacheProperties(customProperties);
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x0001B97C File Offset: 0x00019B7C
	public static void RemovePlayerCustomProperties(string[] customPropertiesToDelete)
	{
		if (customPropertiesToDelete == null || customPropertiesToDelete.Length == 0 || PhotonNetwork.player.CustomProperties == null)
		{
			PhotonNetwork.player.CustomProperties = new Hashtable();
			return;
		}
		foreach (string key in customPropertiesToDelete)
		{
			if (PhotonNetwork.player.CustomProperties.ContainsKey(key))
			{
				PhotonNetwork.player.CustomProperties.Remove(key);
			}
		}
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x0001B9E1 File Offset: 0x00019BE1
	public static bool RaiseEvent(byte eventCode, object eventContent, bool sendReliable, RaiseEventOptions options)
	{
		if (!PhotonNetwork.inRoom || eventCode >= 200)
		{
			Debug.LogWarning("RaiseEvent() failed. Your event is not being sent! Check if your are in a Room and the eventCode must be less than 200 (0..199).");
			return false;
		}
		return PhotonNetwork.networkingPeer.OpRaiseEvent(eventCode, eventContent, sendReliable, options);
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x0001BA0C File Offset: 0x00019C0C
	public static int AllocateViewID()
	{
		int num = PhotonNetwork.AllocateViewID(PhotonNetwork.player.ID);
		PhotonNetwork.manuallyAllocatedViewIds.Add(num);
		return num;
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0001BA38 File Offset: 0x00019C38
	public static int AllocateSceneViewID()
	{
		if (!PhotonNetwork.isMasterClient)
		{
			Debug.LogError("Only the Master Client can AllocateSceneViewID(). Check PhotonNetwork.isMasterClient!");
			return -1;
		}
		int num = PhotonNetwork.AllocateViewID(0);
		PhotonNetwork.manuallyAllocatedViewIds.Add(num);
		return num;
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x0001BA6C File Offset: 0x00019C6C
	private static int AllocateViewID(int ownerId)
	{
		if (ownerId == 0)
		{
			int num = PhotonNetwork.lastUsedViewSubIdStatic;
			int num2 = ownerId * PhotonNetwork.MAX_VIEW_IDS;
			for (int i = 1; i < PhotonNetwork.MAX_VIEW_IDS; i++)
			{
				num = (num + 1) % PhotonNetwork.MAX_VIEW_IDS;
				if (num != 0)
				{
					int num3 = num + num2;
					if (!PhotonNetwork.networkingPeer.photonViewList.ContainsKey(num3))
					{
						PhotonNetwork.lastUsedViewSubIdStatic = num;
						return num3;
					}
				}
			}
			throw new Exception(string.Format("AllocateViewID() failed. Room (user {0}) is out of 'scene' viewIDs. It seems all available are in use.", ownerId));
		}
		int num4 = PhotonNetwork.lastUsedViewSubId;
		int num5 = ownerId * PhotonNetwork.MAX_VIEW_IDS;
		for (int j = 1; j < PhotonNetwork.MAX_VIEW_IDS; j++)
		{
			num4 = (num4 + 1) % PhotonNetwork.MAX_VIEW_IDS;
			if (num4 != 0)
			{
				int num6 = num4 + num5;
				if (!PhotonNetwork.networkingPeer.photonViewList.ContainsKey(num6) && !PhotonNetwork.manuallyAllocatedViewIds.Contains(num6))
				{
					PhotonNetwork.lastUsedViewSubId = num4;
					return num6;
				}
			}
		}
		throw new Exception(string.Format("AllocateViewID() failed. User {0} is out of subIds, as all viewIDs are used.", ownerId));
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x0001BB58 File Offset: 0x00019D58
	private static int[] AllocateSceneViewIDs(int countOfNewViews)
	{
		int[] array = new int[countOfNewViews];
		for (int i = 0; i < countOfNewViews; i++)
		{
			array[i] = PhotonNetwork.AllocateViewID(0);
		}
		return array;
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x0001BB84 File Offset: 0x00019D84
	public static void UnAllocateViewID(int viewID)
	{
		PhotonNetwork.manuallyAllocatedViewIds.Remove(viewID);
		if (PhotonNetwork.networkingPeer.photonViewList.ContainsKey(viewID))
		{
			Debug.LogWarning(string.Format("UnAllocateViewID() should be called after the PhotonView was destroyed (GameObject.Destroy()). ViewID: {0} still found in: {1}", viewID, PhotonNetwork.networkingPeer.photonViewList[viewID]));
		}
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x0001BBD4 File Offset: 0x00019DD4
	public static GameObject Instantiate(string prefabName, Vector3 position, Quaternion rotation, byte group)
	{
		return PhotonNetwork.Instantiate(prefabName, position, rotation, group, null);
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0001BBE0 File Offset: 0x00019DE0
	public static GameObject Instantiate(string prefabName, Vector3 position, Quaternion rotation, byte group, object[] data)
	{
		if (!PhotonNetwork.connected || (PhotonNetwork.InstantiateInRoomOnly && !PhotonNetwork.inRoom))
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Failed to Instantiate prefab: ",
				prefabName,
				". Client should be in a room. Current connectionStateDetailed: ",
				PhotonNetwork.connectionStateDetailed
			}));
			return null;
		}
		GameObject gameObject;
		if (!PhotonNetwork.UsePrefabCache || !PhotonNetwork.PrefabCache.TryGetValue(prefabName, out gameObject))
		{
			gameObject = (GameObject)Resources.Load(prefabName, typeof(GameObject));
			if (PhotonNetwork.UsePrefabCache)
			{
				PhotonNetwork.PrefabCache.Add(prefabName, gameObject);
			}
		}
		if (gameObject == null)
		{
			Debug.LogError("Failed to Instantiate prefab: " + prefabName + ". Verify the Prefab is in a Resources folder (and not in a subfolder)");
			return null;
		}
		if (gameObject.GetComponent<PhotonView>() == null)
		{
			Debug.LogError("Failed to Instantiate prefab:" + prefabName + ". Prefab must have a PhotonView component.");
			return null;
		}
		Component[] photonViewsInChildren = gameObject.GetPhotonViewsInChildren();
		int[] array = new int[photonViewsInChildren.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = PhotonNetwork.AllocateViewID(PhotonNetwork.player.ID);
		}
		Hashtable evData = PhotonNetwork.networkingPeer.SendInstantiate(prefabName, position, rotation, group, array, data, false);
		return PhotonNetwork.networkingPeer.DoInstantiate(evData, PhotonNetwork.networkingPeer.LocalPlayer, gameObject);
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0001BD18 File Offset: 0x00019F18
	public static GameObject InstantiateSceneObject(string prefabName, Vector3 position, Quaternion rotation, byte group, object[] data)
	{
		if (!PhotonNetwork.connected || (PhotonNetwork.InstantiateInRoomOnly && !PhotonNetwork.inRoom))
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Failed to InstantiateSceneObject prefab: ",
				prefabName,
				". Client should be in a room. Current connectionStateDetailed: ",
				PhotonNetwork.connectionStateDetailed
			}));
			return null;
		}
		if (!PhotonNetwork.isMasterClient)
		{
			Debug.LogError("Failed to InstantiateSceneObject prefab: " + prefabName + ". Client is not the MasterClient in this room.");
			return null;
		}
		GameObject gameObject;
		if (!PhotonNetwork.UsePrefabCache || !PhotonNetwork.PrefabCache.TryGetValue(prefabName, out gameObject))
		{
			gameObject = (GameObject)Resources.Load(prefabName, typeof(GameObject));
			if (PhotonNetwork.UsePrefabCache)
			{
				PhotonNetwork.PrefabCache.Add(prefabName, gameObject);
			}
		}
		if (gameObject == null)
		{
			Debug.LogError("Failed to InstantiateSceneObject prefab: " + prefabName + ". Verify the Prefab is in a Resources folder (and not in a subfolder)");
			return null;
		}
		if (gameObject.GetComponent<PhotonView>() == null)
		{
			Debug.LogError("Failed to InstantiateSceneObject prefab:" + prefabName + ". Prefab must have a PhotonView component.");
			return null;
		}
		Component[] photonViewsInChildren = gameObject.GetPhotonViewsInChildren();
		int[] array = PhotonNetwork.AllocateSceneViewIDs(photonViewsInChildren.Length);
		if (array == null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Failed to InstantiateSceneObject prefab: ",
				prefabName,
				". No ViewIDs are free to use. Max is: ",
				PhotonNetwork.MAX_VIEW_IDS
			}));
			return null;
		}
		Hashtable evData = PhotonNetwork.networkingPeer.SendInstantiate(prefabName, position, rotation, group, array, data, true);
		return PhotonNetwork.networkingPeer.DoInstantiate(evData, PhotonNetwork.networkingPeer.LocalPlayer, gameObject);
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x0001BE7E File Offset: 0x0001A07E
	public static int GetPing()
	{
		return PhotonNetwork.networkingPeer.RoundTripTime;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0001BE8A File Offset: 0x0001A08A
	public static void FetchServerTimestamp()
	{
		if (PhotonNetwork.networkingPeer != null)
		{
			PhotonNetwork.networkingPeer.FetchServerTimestamp();
		}
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0001BE9D File Offset: 0x0001A09D
	public static void SendOutgoingCommands()
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		while (PhotonNetwork.networkingPeer.SendOutgoingCommands())
		{
		}
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0001BEB4 File Offset: 0x0001A0B4
	public static bool CloseConnection(PhotonPlayer kickPlayer)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return false;
		}
		if (!PhotonNetwork.player.IsMasterClient)
		{
			Debug.LogError("CloseConnection: Only the masterclient can kick another player.");
			return false;
		}
		if (kickPlayer == null)
		{
			Debug.LogError("CloseConnection: No such player connected!");
			return false;
		}
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			TargetActors = new int[]
			{
				kickPlayer.ID
			}
		};
		return PhotonNetwork.networkingPeer.OpRaiseEvent(203, null, true, raiseEventOptions);
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0001BF20 File Offset: 0x0001A120
	public static bool SetMasterClient(PhotonPlayer masterClientPlayer)
	{
		if (!PhotonNetwork.inRoom || !PhotonNetwork.VerifyCanUseNetwork() || PhotonNetwork.offlineMode)
		{
			if (PhotonNetwork.logLevel == PhotonLogLevel.Informational)
			{
				Debug.Log("Can not SetMasterClient(). Not in room or in offlineMode.");
			}
			return false;
		}
		if (PhotonNetwork.room.serverSideMasterClient)
		{
			Hashtable gameProperties = new Hashtable
			{
				{
					248,
					masterClientPlayer.ID
				}
			};
			Hashtable expectedProperties = new Hashtable
			{
				{
					248,
					PhotonNetwork.networkingPeer.mMasterClientId
				}
			};
			return PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(gameProperties, expectedProperties, false);
		}
		return PhotonNetwork.isMasterClient && PhotonNetwork.networkingPeer.SetMasterClient(masterClientPlayer.ID, true);
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0001BFD0 File Offset: 0x0001A1D0
	public static void Destroy(PhotonView targetView)
	{
		if (targetView != null)
		{
			PhotonNetwork.networkingPeer.RemoveInstantiatedGO(targetView.gameObject, !PhotonNetwork.inRoom);
			return;
		}
		Debug.LogError("Destroy(targetPhotonView) failed, cause targetPhotonView is null.");
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x0001BFFE File Offset: 0x0001A1FE
	public static void Destroy(GameObject targetGo)
	{
		PhotonNetwork.networkingPeer.RemoveInstantiatedGO(targetGo, !PhotonNetwork.inRoom);
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x0001C013 File Offset: 0x0001A213
	public static void DestroyPlayerObjects(PhotonPlayer targetPlayer)
	{
		if (PhotonNetwork.player == null)
		{
			Debug.LogError("DestroyPlayerObjects() failed, cause parameter 'targetPlayer' was null.");
		}
		PhotonNetwork.DestroyPlayerObjects(targetPlayer.ID);
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x0001C034 File Offset: 0x0001A234
	public static void DestroyPlayerObjects(int targetPlayerId)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		if (PhotonNetwork.player.IsMasterClient || targetPlayerId == PhotonNetwork.player.ID)
		{
			PhotonNetwork.networkingPeer.DestroyPlayerObjects(targetPlayerId, false);
			return;
		}
		Debug.LogError("DestroyPlayerObjects() failed, cause players can only destroy their own GameObjects. A Master Client can destroy anyone's. This is master: " + PhotonNetwork.isMasterClient.ToString());
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0001C08B File Offset: 0x0001A28B
	public static void DestroyAll()
	{
		if (PhotonNetwork.isMasterClient)
		{
			PhotonNetwork.networkingPeer.DestroyAll(false);
			return;
		}
		Debug.LogError("Couldn't call DestroyAll() as only the master client is allowed to call this.");
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0001C0AA File Offset: 0x0001A2AA
	public static void RemoveRPCs(PhotonPlayer targetPlayer)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		if (!targetPlayer.IsLocal && !PhotonNetwork.isMasterClient)
		{
			Debug.LogError("Error; Only the MasterClient can call RemoveRPCs for other players.");
			return;
		}
		PhotonNetwork.networkingPeer.OpCleanRpcBuffer(targetPlayer.ID);
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0001C0DE File Offset: 0x0001A2DE
	public static void RemoveRPCs(PhotonView targetPhotonView)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		PhotonNetwork.networkingPeer.CleanRpcBufferIfMine(targetPhotonView);
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0001C0F3 File Offset: 0x0001A2F3
	public static void RemoveRPCsInGroup(int targetGroup)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		PhotonNetwork.networkingPeer.RemoveRPCsInGroup(targetGroup);
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0001C108 File Offset: 0x0001A308
	internal static void RPC(PhotonView view, string methodName, PhotonTargets target, bool encrypt, params object[] parameters)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		if (PhotonNetwork.room == null)
		{
			Debug.LogWarning("RPCs can only be sent in rooms. Call of \"" + methodName + "\" gets executed locally only, if at all.");
			return;
		}
		if (PhotonNetwork.networkingPeer == null)
		{
			Debug.LogWarning("Could not execute RPC " + methodName + ". Possible scene loading in progress?");
			return;
		}
		if (PhotonNetwork.room.serverSideMasterClient)
		{
			PhotonNetwork.networkingPeer.RPC(view, methodName, target, null, encrypt, parameters);
			return;
		}
		if (PhotonNetwork.networkingPeer.hasSwitchedMC && target == PhotonTargets.MasterClient)
		{
			PhotonNetwork.networkingPeer.RPC(view, methodName, PhotonTargets.Others, PhotonNetwork.masterClient, encrypt, parameters);
			return;
		}
		PhotonNetwork.networkingPeer.RPC(view, methodName, target, null, encrypt, parameters);
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0001C1AC File Offset: 0x0001A3AC
	internal static void RPC(PhotonView view, string methodName, PhotonPlayer targetPlayer, bool encrpyt, params object[] parameters)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		if (PhotonNetwork.room == null)
		{
			Debug.LogWarning("RPCs can only be sent in rooms. Call of \"" + methodName + "\" gets executed locally only, if at all.");
			return;
		}
		if (PhotonNetwork.player == null)
		{
			Debug.LogError("RPC can't be sent to target PhotonPlayer being null! Did not send \"" + methodName + "\" call.");
		}
		if (PhotonNetwork.networkingPeer != null)
		{
			PhotonNetwork.networkingPeer.RPC(view, methodName, PhotonTargets.Others, targetPlayer, encrpyt, parameters);
			return;
		}
		Debug.LogWarning("Could not execute RPC " + methodName + ". Possible scene loading in progress?");
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x0001C228 File Offset: 0x0001A428
	public static void CacheSendMonoMessageTargets(Type type)
	{
		if (type == null)
		{
			type = PhotonNetwork.SendMonoMessageTargetType;
		}
		PhotonNetwork.SendMonoMessageTargets = PhotonNetwork.FindGameObjectsWithComponent(type);
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x0001C248 File Offset: 0x0001A448
	public static HashSet<GameObject> FindGameObjectsWithComponent(Type type)
	{
		HashSet<GameObject> hashSet = new HashSet<GameObject>();
		Component[] array = (Component[])Object.FindObjectsOfType(type);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				hashSet.Add(array[i].gameObject);
			}
		}
		return hashSet;
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0001C290 File Offset: 0x0001A490
	[Obsolete("Use SetInterestGroups(byte group, bool enabled) instead.")]
	public static void SetReceivingEnabled(int group, bool enabled)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		PhotonNetwork.SetInterestGroups((byte)group, enabled);
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x0001C2A4 File Offset: 0x0001A4A4
	public static void SetInterestGroups(byte group, bool enabled)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		if (enabled)
		{
			byte[] enableGroups = new byte[]
			{
				group
			};
			PhotonNetwork.networkingPeer.SetInterestGroups(null, enableGroups);
			return;
		}
		byte[] disableGroups = new byte[]
		{
			group
		};
		PhotonNetwork.networkingPeer.SetInterestGroups(disableGroups, null);
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x0001C2EC File Offset: 0x0001A4EC
	[Obsolete("Use SetInterestGroups(byte[] disableGroups, byte[] enableGroups) instead. Mind the parameter order!")]
	public static void SetReceivingEnabled(int[] enableGroups, int[] disableGroups)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		byte[] array = null;
		byte[] array2 = null;
		if (enableGroups != null)
		{
			array2 = new byte[enableGroups.Length];
			Array.Copy(enableGroups, array2, enableGroups.Length);
		}
		if (disableGroups != null)
		{
			array = new byte[disableGroups.Length];
			Array.Copy(disableGroups, array, disableGroups.Length);
		}
		PhotonNetwork.networkingPeer.SetInterestGroups(array, array2);
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x0001C33D File Offset: 0x0001A53D
	public static void SetInterestGroups(byte[] disableGroups, byte[] enableGroups)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		PhotonNetwork.networkingPeer.SetInterestGroups(disableGroups, enableGroups);
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0001C353 File Offset: 0x0001A553
	[Obsolete("Use SetSendingEnabled(byte group, bool enabled). Interest Groups have a byte-typed ID. Mind the parameter order.")]
	public static void SetSendingEnabled(int group, bool enabled)
	{
		PhotonNetwork.SetSendingEnabled((byte)group, enabled);
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x0001C35D File Offset: 0x0001A55D
	public static void SetSendingEnabled(byte group, bool enabled)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		PhotonNetwork.networkingPeer.SetSendingEnabled(group, enabled);
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x0001C374 File Offset: 0x0001A574
	[Obsolete("Use SetSendingEnabled(byte group, bool enabled). Interest Groups have a byte-typed ID. Mind the parameter order.")]
	public static void SetSendingEnabled(int[] enableGroups, int[] disableGroups)
	{
		byte[] array = null;
		byte[] array2 = null;
		if (enableGroups != null)
		{
			array2 = new byte[enableGroups.Length];
			Array.Copy(enableGroups, array2, enableGroups.Length);
		}
		if (disableGroups != null)
		{
			array = new byte[disableGroups.Length];
			Array.Copy(disableGroups, array, disableGroups.Length);
		}
		PhotonNetwork.SetSendingEnabled(array, array2);
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
	public static void SetSendingEnabled(byte[] disableGroups, byte[] enableGroups)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		PhotonNetwork.networkingPeer.SetSendingEnabled(disableGroups, enableGroups);
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0001C3CE File Offset: 0x0001A5CE
	public static void SetLevelPrefix(short prefix)
	{
		if (!PhotonNetwork.VerifyCanUseNetwork())
		{
			return;
		}
		PhotonNetwork.networkingPeer.SetLevelPrefix(prefix);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0001C3E3 File Offset: 0x0001A5E3
	public static void LoadLevel(int levelNumber)
	{
		PhotonNetwork.networkingPeer.AsynchLevelLoadCall = false;
		if (PhotonNetwork.automaticallySyncScene)
		{
			PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced(levelNumber, true, false);
		}
		PhotonNetwork.isMessageQueueRunning = false;
		PhotonNetwork.networkingPeer.loadingLevelAndPausedNetwork = true;
		SceneManager.LoadScene(levelNumber);
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0001C420 File Offset: 0x0001A620
	public static AsyncOperation LoadLevelAsync(int levelNumber)
	{
		PhotonNetwork.networkingPeer.AsynchLevelLoadCall = true;
		if (PhotonNetwork.automaticallySyncScene)
		{
			PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced(levelNumber, true, false);
		}
		PhotonNetwork.isMessageQueueRunning = false;
		PhotonNetwork.networkingPeer.loadingLevelAndPausedNetwork = true;
		return SceneManager.LoadSceneAsync(levelNumber, LoadSceneMode.Single);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0001C45E File Offset: 0x0001A65E
	public static void LoadLevel(string levelName)
	{
		PhotonNetwork.networkingPeer.AsynchLevelLoadCall = false;
		if (PhotonNetwork.automaticallySyncScene)
		{
			PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced(levelName, true, false);
		}
		PhotonNetwork.isMessageQueueRunning = false;
		PhotonNetwork.networkingPeer.loadingLevelAndPausedNetwork = true;
		SceneManager.LoadScene(levelName);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0001C496 File Offset: 0x0001A696
	public static AsyncOperation LoadLevelAsync(string levelName)
	{
		PhotonNetwork.networkingPeer.AsynchLevelLoadCall = true;
		if (PhotonNetwork.automaticallySyncScene)
		{
			PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced(levelName, true, false);
		}
		PhotonNetwork.isMessageQueueRunning = false;
		PhotonNetwork.networkingPeer.loadingLevelAndPausedNetwork = true;
		return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0001C4CF File Offset: 0x0001A6CF
	public static bool WebRpc(string name, object parameters)
	{
		return PhotonNetwork.networkingPeer.WebRpc(name, parameters);
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0001C4DD File Offset: 0x0001A6DD
	public static bool CallEvent(byte eventCode, object content, int senderId)
	{
		if (PhotonNetwork.OnEventCall != null)
		{
			PhotonNetwork.OnEventCall(eventCode, content, senderId);
			return true;
		}
		return false;
	}

	// Token: 0x040004F1 RID: 1265
	public const string versionPUN = "1.103.1";

	// Token: 0x040004F3 RID: 1267
	internal static readonly PhotonHandler photonMono;

	// Token: 0x040004F4 RID: 1268
	internal static NetworkingPeer networkingPeer;

	// Token: 0x040004F5 RID: 1269
	public static readonly int MAX_VIEW_IDS = 10000;

	// Token: 0x040004F6 RID: 1270
	internal const string serverSettingsAssetFile = "PhotonServerSettings";

	// Token: 0x040004F7 RID: 1271
	public static ServerSettings PhotonServerSettings = (ServerSettings)Resources.Load("PhotonServerSettings", typeof(ServerSettings));

	// Token: 0x040004F8 RID: 1272
	public static bool InstantiateInRoomOnly = true;

	// Token: 0x040004F9 RID: 1273
	public static PhotonLogLevel logLevel = PhotonLogLevel.ErrorsOnly;

	// Token: 0x040004FB RID: 1275
	public static float precisionForVectorSynchronization = 9.9E-05f;

	// Token: 0x040004FC RID: 1276
	public static float precisionForQuaternionSynchronization = 1f;

	// Token: 0x040004FD RID: 1277
	public static float precisionForFloatSynchronization = 0.01f;

	// Token: 0x040004FE RID: 1278
	public static bool UseRpcMonoBehaviourCache;

	// Token: 0x040004FF RID: 1279
	public static bool UsePrefabCache = true;

	// Token: 0x04000500 RID: 1280
	public static Dictionary<string, GameObject> PrefabCache = new Dictionary<string, GameObject>();

	// Token: 0x04000501 RID: 1281
	public static HashSet<GameObject> SendMonoMessageTargets;

	// Token: 0x04000502 RID: 1282
	public static Type SendMonoMessageTargetType = typeof(MonoBehaviour);

	// Token: 0x04000503 RID: 1283
	public static bool StartRpcsAsCoroutine = true;

	// Token: 0x04000504 RID: 1284
	private static bool isOfflineMode = false;

	// Token: 0x04000505 RID: 1285
	private static Room offlineModeRoom = null;

	// Token: 0x04000506 RID: 1286
	[Obsolete("Used for compatibility with Unity networking only.")]
	public static int maxConnections;

	// Token: 0x04000507 RID: 1287
	private static bool _mAutomaticallySyncScene = false;

	// Token: 0x04000508 RID: 1288
	private static bool m_autoCleanUpPlayerObjects = true;

	// Token: 0x04000509 RID: 1289
	private static int sendInterval = 50;

	// Token: 0x0400050A RID: 1290
	private static int sendIntervalOnSerialize = 100;

	// Token: 0x0400050B RID: 1291
	private static bool m_isMessageQueueRunning = true;

	// Token: 0x0400050C RID: 1292
	private static Stopwatch startupStopwatch;

	// Token: 0x0400050D RID: 1293
	public static float BackgroundTimeout = 60f;

	// Token: 0x04000510 RID: 1296
	internal static int lastUsedViewSubId = 0;

	// Token: 0x04000511 RID: 1297
	internal static int lastUsedViewSubIdStatic = 0;

	// Token: 0x04000512 RID: 1298
	internal static List<int> manuallyAllocatedViewIds = new List<int>();

	// Token: 0x020004EE RID: 1262
	// (Invoke) Token: 0x06002665 RID: 9829
	public delegate void EventCallback(byte eventCode, object content, int senderId);
}
