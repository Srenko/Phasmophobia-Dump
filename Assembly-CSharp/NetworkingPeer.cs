using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;

// Token: 0x020000A3 RID: 163
internal class NetworkingPeer : LoadBalancingPeer, IPhotonPeerListener
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x0600033C RID: 828 RVA: 0x00013B48 File Offset: 0x00011D48
	protected internal string AppVersion
	{
		get
		{
			return string.Format("{0}_{1}", PhotonNetwork.gameVersion, "1.103.1");
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x0600033D RID: 829 RVA: 0x00013B5E File Offset: 0x00011D5E
	// (set) Token: 0x0600033E RID: 830 RVA: 0x00013B66 File Offset: 0x00011D66
	public AuthenticationValues AuthValues { get; set; }

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x0600033F RID: 831 RVA: 0x00013B6F File Offset: 0x00011D6F
	private string TokenForInit
	{
		get
		{
			if (this.AuthMode == AuthModeOption.Auth)
			{
				return null;
			}
			if (this.AuthValues == null)
			{
				return null;
			}
			return this.AuthValues.Token;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06000340 RID: 832 RVA: 0x00013B90 File Offset: 0x00011D90
	// (set) Token: 0x06000341 RID: 833 RVA: 0x00013B98 File Offset: 0x00011D98
	public bool IsUsingNameServer { get; protected internal set; }

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000342 RID: 834 RVA: 0x00013BA1 File Offset: 0x00011DA1
	public string NameServerAddress
	{
		get
		{
			return this.GetNameServerAddress();
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000343 RID: 835 RVA: 0x00013BA9 File Offset: 0x00011DA9
	// (set) Token: 0x06000344 RID: 836 RVA: 0x00013BB1 File Offset: 0x00011DB1
	public string MasterServerAddress { get; protected internal set; }

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x06000345 RID: 837 RVA: 0x00013BBA File Offset: 0x00011DBA
	// (set) Token: 0x06000346 RID: 838 RVA: 0x00013BC2 File Offset: 0x00011DC2
	public string GameServerAddress { get; protected internal set; }

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x06000347 RID: 839 RVA: 0x00013BCB File Offset: 0x00011DCB
	// (set) Token: 0x06000348 RID: 840 RVA: 0x00013BD3 File Offset: 0x00011DD3
	protected internal ServerConnection Server { get; private set; }

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000349 RID: 841 RVA: 0x00013BDC File Offset: 0x00011DDC
	// (set) Token: 0x0600034A RID: 842 RVA: 0x00013BE4 File Offset: 0x00011DE4
	public ClientState State { get; internal set; }

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x0600034B RID: 843 RVA: 0x00013BED File Offset: 0x00011DED
	// (set) Token: 0x0600034C RID: 844 RVA: 0x00013BF5 File Offset: 0x00011DF5
	public TypedLobby lobby { get; set; }

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x0600034D RID: 845 RVA: 0x00013BFE File Offset: 0x00011DFE
	private bool requestLobbyStatistics
	{
		get
		{
			return PhotonNetwork.EnableLobbyStatistics && this.Server == ServerConnection.MasterServer;
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x0600034E RID: 846 RVA: 0x00013C12 File Offset: 0x00011E12
	// (set) Token: 0x0600034F RID: 847 RVA: 0x00013C1C File Offset: 0x00011E1C
	public string PlayerName
	{
		get
		{
			return this.playername;
		}
		set
		{
			if (string.IsNullOrEmpty(value) || value.Equals(this.playername))
			{
				return;
			}
			if (this.LocalPlayer != null)
			{
				this.LocalPlayer.NickName = value;
			}
			this.playername = value;
			if (this.CurrentRoom != null)
			{
				this.SendPlayerName();
			}
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000350 RID: 848 RVA: 0x00013C69 File Offset: 0x00011E69
	// (set) Token: 0x06000351 RID: 849 RVA: 0x00013C88 File Offset: 0x00011E88
	public Room CurrentRoom
	{
		get
		{
			if (this.currentRoom != null && this.currentRoom.IsLocalClientInside)
			{
				return this.currentRoom;
			}
			return null;
		}
		private set
		{
			this.currentRoom = value;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000352 RID: 850 RVA: 0x00013C91 File Offset: 0x00011E91
	// (set) Token: 0x06000353 RID: 851 RVA: 0x00013C99 File Offset: 0x00011E99
	public PhotonPlayer LocalPlayer { get; internal set; }

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06000354 RID: 852 RVA: 0x00013CA2 File Offset: 0x00011EA2
	// (set) Token: 0x06000355 RID: 853 RVA: 0x00013CAA File Offset: 0x00011EAA
	public int PlayersOnMasterCount { get; internal set; }

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000356 RID: 854 RVA: 0x00013CB3 File Offset: 0x00011EB3
	// (set) Token: 0x06000357 RID: 855 RVA: 0x00013CBB File Offset: 0x00011EBB
	public int PlayersInRoomsCount { get; internal set; }

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000358 RID: 856 RVA: 0x00013CC4 File Offset: 0x00011EC4
	// (set) Token: 0x06000359 RID: 857 RVA: 0x00013CCC File Offset: 0x00011ECC
	public int RoomsCount { get; internal set; }

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x0600035A RID: 858 RVA: 0x00013CD5 File Offset: 0x00011ED5
	protected internal int FriendListAge
	{
		get
		{
			if (!this.isFetchingFriendList && this.friendListTimestamp != 0)
			{
				return Environment.TickCount - this.friendListTimestamp;
			}
			return 0;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x0600035B RID: 859 RVA: 0x00013CF5 File Offset: 0x00011EF5
	public bool IsAuthorizeSecretAvailable
	{
		get
		{
			return this.AuthValues != null && !string.IsNullOrEmpty(this.AuthValues.Token);
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x0600035C RID: 860 RVA: 0x00013D14 File Offset: 0x00011F14
	// (set) Token: 0x0600035D RID: 861 RVA: 0x00013D1C File Offset: 0x00011F1C
	public List<Region> AvailableRegions { get; protected internal set; }

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x0600035E RID: 862 RVA: 0x00013D25 File Offset: 0x00011F25
	// (set) Token: 0x0600035F RID: 863 RVA: 0x00013D2D File Offset: 0x00011F2D
	public CloudRegionCode CloudRegion { get; protected internal set; }

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000360 RID: 864 RVA: 0x00013D36 File Offset: 0x00011F36
	// (set) Token: 0x06000361 RID: 865 RVA: 0x00013D60 File Offset: 0x00011F60
	public int mMasterClientId
	{
		get
		{
			if (PhotonNetwork.offlineMode)
			{
				return this.LocalPlayer.ID;
			}
			if (this.CurrentRoom != null)
			{
				return this.CurrentRoom.MasterClientId;
			}
			return 0;
		}
		private set
		{
			if (this.CurrentRoom != null)
			{
				this.CurrentRoom.MasterClientId = value;
			}
		}
	}

	// Token: 0x06000362 RID: 866 RVA: 0x00013D78 File Offset: 0x00011F78
	public NetworkingPeer(string playername, ConnectionProtocol connectionProtocol) : base(connectionProtocol)
	{
		base.Listener = this;
		this.lobby = TypedLobby.Default;
		this.PlayerName = playername;
		this.LocalPlayer = new PhotonPlayer(true, -1, this.playername);
		this.AddNewPlayer(this.LocalPlayer.ID, this.LocalPlayer);
		this.rpcShortcuts = new Dictionary<string, int>(PhotonNetwork.PhotonServerSettings.RpcList.Count);
		for (int i = 0; i < PhotonNetwork.PhotonServerSettings.RpcList.Count; i++)
		{
			string key = PhotonNetwork.PhotonServerSettings.RpcList[i];
			this.rpcShortcuts[key] = i;
		}
		this.State = ClientState.PeerCreated;
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00013F88 File Offset: 0x00012188
	private string GetNameServerAddress()
	{
		ConnectionProtocol transportProtocol = base.TransportProtocol;
		int num = 0;
		NetworkingPeer.ProtocolToNameServerPort.TryGetValue(transportProtocol, out num);
		string arg = string.Empty;
		if (transportProtocol == ConnectionProtocol.WebSocket)
		{
			arg = "ws://";
		}
		else if (transportProtocol == ConnectionProtocol.WebSocketSecure)
		{
			arg = "wss://";
		}
		if (PhotonNetwork.UseAlternativeUdpPorts && base.TransportProtocol == ConnectionProtocol.Udp)
		{
			num = 27000;
		}
		return string.Format("{0}{1}:{2}", arg, "ns.exitgames.com", num);
	}

	// Token: 0x06000364 RID: 868 RVA: 0x00013FF3 File Offset: 0x000121F3
	public override bool Connect(string serverAddress, string applicationName)
	{
		Debug.LogError("Avoid using this directly. Thanks.");
		return false;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00014000 File Offset: 0x00012200
	public bool ReconnectToMaster()
	{
		if (this.AuthValues == null)
		{
			Debug.LogWarning("ReconnectToMaster() with AuthValues == null is not correct!");
			this.AuthValues = new AuthenticationValues();
		}
		this.AuthValues.Token = this.tokenCache;
		return this.Connect(this.MasterServerAddress, ServerConnection.MasterServer);
	}

	// Token: 0x06000366 RID: 870 RVA: 0x00014040 File Offset: 0x00012240
	public bool ReconnectAndRejoin()
	{
		if (this.AuthValues == null)
		{
			Debug.LogWarning("ReconnectAndRejoin() with AuthValues == null is not correct!");
			this.AuthValues = new AuthenticationValues();
		}
		this.AuthValues.Token = this.tokenCache;
		if (!string.IsNullOrEmpty(this.GameServerAddress) && this.enterRoomParamsCache != null)
		{
			this.lastJoinType = JoinType.JoinRoom;
			this.enterRoomParamsCache.RejoinOnly = true;
			return this.Connect(this.GameServerAddress, ServerConnection.GameServer);
		}
		return false;
	}

	// Token: 0x06000367 RID: 871 RVA: 0x000140B4 File Offset: 0x000122B4
	public bool Connect(string serverAddress, ServerConnection type)
	{
		if (PhotonHandler.AppQuits)
		{
			Debug.LogWarning("Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
			return false;
		}
		if (this.State == ClientState.Disconnecting)
		{
			Debug.LogError("Connect() failed. Can't connect while disconnecting (still). Current state: " + PhotonNetwork.connectionStateDetailed);
			return false;
		}
		this.cachedServerType = type;
		this.cachedServerAddress = serverAddress;
		this.cachedApplicationName = string.Empty;
		this.SetupProtocol(type);
		bool flag = base.Connect(serverAddress, "", this.TokenForInit);
		if (flag)
		{
			switch (type)
			{
			case ServerConnection.MasterServer:
				this.State = ClientState.ConnectingToMasterserver;
				break;
			case ServerConnection.GameServer:
				this.State = ClientState.ConnectingToGameserver;
				break;
			case ServerConnection.NameServer:
				this.State = ClientState.ConnectingToNameServer;
				break;
			}
		}
		return flag;
	}

	// Token: 0x06000368 RID: 872 RVA: 0x00014160 File Offset: 0x00012360
	private bool Reconnect()
	{
		this._isReconnecting = true;
		PhotonNetwork.SwitchToProtocol(PhotonNetwork.PhotonServerSettings.Protocol);
		this.SetupProtocol(this.cachedServerType);
		bool flag = base.Connect(this.cachedServerAddress, this.cachedApplicationName, this.TokenForInit);
		if (flag)
		{
			switch (this.cachedServerType)
			{
			case ServerConnection.MasterServer:
				this.State = ClientState.ConnectingToMasterserver;
				break;
			case ServerConnection.GameServer:
				this.State = ClientState.ConnectingToGameserver;
				break;
			case ServerConnection.NameServer:
				this.State = ClientState.ConnectingToNameServer;
				break;
			}
		}
		return flag;
	}

	// Token: 0x06000369 RID: 873 RVA: 0x000141E4 File Offset: 0x000123E4
	public bool ConnectToNameServer()
	{
		if (PhotonHandler.AppQuits)
		{
			Debug.LogWarning("Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
			return false;
		}
		this.IsUsingNameServer = true;
		this.CloudRegion = CloudRegionCode.none;
		this.cloudCluster = null;
		if (this.State == ClientState.ConnectedToNameServer)
		{
			return true;
		}
		this.SetupProtocol(ServerConnection.NameServer);
		this.cachedServerType = ServerConnection.NameServer;
		this.cachedServerAddress = this.NameServerAddress;
		this.cachedApplicationName = "ns";
		if (!base.Connect(this.NameServerAddress, "ns", this.TokenForInit))
		{
			return false;
		}
		this.State = ClientState.ConnectingToNameServer;
		return true;
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00014270 File Offset: 0x00012470
	public bool ConnectToRegionMaster(CloudRegionCode region, string specificCluster = null)
	{
		if (PhotonHandler.AppQuits)
		{
			Debug.LogWarning("Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
			return false;
		}
		this.IsUsingNameServer = true;
		this.CloudRegion = region;
		this.cloudCluster = specificCluster;
		if (this.State == ClientState.ConnectedToNameServer)
		{
			return this.CallAuthenticate();
		}
		this.cachedServerType = ServerConnection.NameServer;
		this.cachedServerAddress = this.NameServerAddress;
		this.cachedApplicationName = "ns";
		this.SetupProtocol(ServerConnection.NameServer);
		if (!base.Connect(this.NameServerAddress, "ns", this.TokenForInit))
		{
			return false;
		}
		this.State = ClientState.ConnectingToNameServer;
		return true;
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00014300 File Offset: 0x00012500
	protected internal void SetupProtocol(ServerConnection serverType)
	{
		ConnectionProtocol connectionProtocol = base.TransportProtocol;
		if (this.AuthMode == AuthModeOption.AuthOnceWss)
		{
			if (serverType != ServerConnection.NameServer)
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.ErrorsOnly)
				{
					Debug.LogWarning("Using PhotonServerSettings.Protocol when leaving the NameServer (AuthMode is AuthOnceWss): " + PhotonNetwork.PhotonServerSettings.Protocol);
				}
				connectionProtocol = PhotonNetwork.PhotonServerSettings.Protocol;
			}
			else
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.ErrorsOnly)
				{
					Debug.LogWarning("Using WebSocket to connect NameServer (AuthMode is AuthOnceWss).");
				}
				connectionProtocol = ConnectionProtocol.WebSocketSecure;
			}
		}
		Type type = null;
		bool flag = this.SocketImplementationConfig == null || !this.SocketImplementationConfig.ContainsKey(ConnectionProtocol.WebSocket) || this.SocketImplementationConfig[ConnectionProtocol.WebSocket] == null;
		bool flag2 = this.SocketImplementationConfig == null || !this.SocketImplementationConfig.ContainsKey(ConnectionProtocol.WebSocketSecure) || this.SocketImplementationConfig[ConnectionProtocol.WebSocketSecure] == null;
		if (flag || flag2)
		{
			type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp", false);
			if (type == null)
			{
				type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp-firstpass", false);
			}
		}
		if (type != null)
		{
			this.SocketImplementationConfig[ConnectionProtocol.WebSocket] = type;
			this.SocketImplementationConfig[ConnectionProtocol.WebSocketSecure] = type;
		}
		if (PhotonHandler.PingImplementation == null)
		{
			PhotonHandler.PingImplementation = typeof(PingMono);
		}
		if (base.TransportProtocol == connectionProtocol)
		{
			return;
		}
		if (PhotonNetwork.logLevel >= PhotonLogLevel.ErrorsOnly)
		{
			Debug.LogWarning(string.Concat(new object[]
			{
				"Protocol switch from: ",
				base.TransportProtocol,
				" to: ",
				connectionProtocol,
				"."
			}));
		}
		base.TransportProtocol = connectionProtocol;
	}

	// Token: 0x0600036C RID: 876 RVA: 0x0001447D File Offset: 0x0001267D
	public override void Disconnect()
	{
		if (base.PeerState == PeerStateValue.Disconnected)
		{
			if (!PhotonHandler.AppQuits)
			{
				Debug.LogWarning(string.Format("Can't execute Disconnect() while not connected. Nothing changed. State: {0}", this.State));
			}
			return;
		}
		this.State = ClientState.Disconnecting;
		base.Disconnect();
	}

	// Token: 0x0600036D RID: 877 RVA: 0x000144B8 File Offset: 0x000126B8
	private bool CallAuthenticate()
	{
		AuthenticationValues authenticationValues;
		if ((authenticationValues = this.AuthValues) == null)
		{
			(authenticationValues = new AuthenticationValues()).UserId = this.PlayerName;
		}
		AuthenticationValues authenticationValues2 = authenticationValues;
		if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.SelfHosted && string.IsNullOrEmpty(authenticationValues2.UserId))
		{
			authenticationValues2.UserId = Guid.NewGuid().ToString();
		}
		if (string.IsNullOrEmpty(this.cloudCluster))
		{
			this.cloudCluster = "*";
		}
		string regionCode = this.CloudRegion + "/" + this.cloudCluster;
		if (this.AuthMode == AuthModeOption.Auth)
		{
			return this.OpAuthenticate(this.AppId, this.AppVersion, authenticationValues2, regionCode, this.requestLobbyStatistics);
		}
		return this.OpAuthenticateOnce(this.AppId, this.AppVersion, authenticationValues2, regionCode, this.EncryptionMode, PhotonNetwork.PhotonServerSettings.Protocol);
	}

	// Token: 0x0600036E RID: 878 RVA: 0x00014590 File Offset: 0x00012790
	private void DisconnectToReconnect()
	{
		switch (this.Server)
		{
		case ServerConnection.MasterServer:
			this.State = ClientState.DisconnectingFromMasterserver;
			base.Disconnect();
			return;
		case ServerConnection.GameServer:
			this.State = ClientState.DisconnectingFromGameserver;
			base.Disconnect();
			return;
		case ServerConnection.NameServer:
			this.State = ClientState.DisconnectingFromNameServer;
			base.Disconnect();
			return;
		default:
			return;
		}
	}

	// Token: 0x0600036F RID: 879 RVA: 0x000145E2 File Offset: 0x000127E2
	public bool GetRegions()
	{
		if (this.Server != ServerConnection.NameServer)
		{
			return false;
		}
		bool flag = this.OpGetRegions(this.AppId);
		if (flag)
		{
			this.AvailableRegions = null;
		}
		return flag;
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00014605 File Offset: 0x00012805
	public override bool OpFindFriends(string[] friendsToFind, FindFriendsOptions findFriendsOptions = null)
	{
		if (this.isFetchingFriendList)
		{
			return false;
		}
		this.friendListRequested = friendsToFind;
		this.isFetchingFriendList = true;
		return base.OpFindFriends(friendsToFind, findFriendsOptions);
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00014628 File Offset: 0x00012828
	public bool OpCreateGame(EnterRoomParams enterRoomParams)
	{
		bool flag = this.Server == ServerConnection.GameServer;
		enterRoomParams.OnGameServer = flag;
		enterRoomParams.PlayerProperties = this.GetLocalActorProperties();
		if (!flag)
		{
			this.enterRoomParamsCache = enterRoomParams;
		}
		this.lastJoinType = JoinType.CreateRoom;
		return base.OpCreateRoom(enterRoomParams);
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0001466C File Offset: 0x0001286C
	public override bool OpJoinRoom(EnterRoomParams opParams)
	{
		bool flag = this.Server == ServerConnection.GameServer;
		opParams.OnGameServer = flag;
		if (!flag)
		{
			this.enterRoomParamsCache = opParams;
		}
		this.lastJoinType = (opParams.CreateIfNotExists ? JoinType.JoinOrCreateRoom : JoinType.JoinRoom);
		return base.OpJoinRoom(opParams);
	}

	// Token: 0x06000373 RID: 883 RVA: 0x000146AD File Offset: 0x000128AD
	public override bool OpJoinRandomRoom(OpJoinRandomRoomParams opJoinRandomRoomParams)
	{
		this.enterRoomParamsCache = new EnterRoomParams();
		this.enterRoomParamsCache.Lobby = opJoinRandomRoomParams.TypedLobby;
		this.enterRoomParamsCache.ExpectedUsers = opJoinRandomRoomParams.ExpectedUsers;
		this.lastJoinType = JoinType.JoinRandomRoom;
		return base.OpJoinRandomRoom(opJoinRandomRoomParams);
	}

	// Token: 0x06000374 RID: 884 RVA: 0x000146EA File Offset: 0x000128EA
	public override bool OpRaiseEvent(byte eventCode, object customEventContent, bool sendReliable, RaiseEventOptions raiseEventOptions)
	{
		return !PhotonNetwork.offlineMode && base.OpRaiseEvent(eventCode, customEventContent, sendReliable, raiseEventOptions);
	}

	// Token: 0x06000375 RID: 885 RVA: 0x00014700 File Offset: 0x00012900
	private void ReadoutProperties(Hashtable gameProperties, Hashtable pActorProperties, int targetActorNr)
	{
		if (pActorProperties != null && pActorProperties.Count > 0)
		{
			if (targetActorNr > 0)
			{
				PhotonPlayer playerWithId = this.GetPlayerWithId(targetActorNr);
				if (playerWithId != null)
				{
					Hashtable hashtable = this.ReadoutPropertiesForActorNr(pActorProperties, targetActorNr);
					playerWithId.InternalCacheProperties(hashtable);
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, new object[]
					{
						playerWithId,
						hashtable
					});
				}
			}
			else
			{
				foreach (object obj in pActorProperties.Keys)
				{
					int num = (int)obj;
					Hashtable hashtable2 = (Hashtable)pActorProperties[obj];
					string name = (string)hashtable2[byte.MaxValue];
					PhotonPlayer photonPlayer = this.GetPlayerWithId(num);
					if (photonPlayer == null)
					{
						photonPlayer = new PhotonPlayer(false, num, name);
						this.AddNewPlayer(num, photonPlayer);
					}
					photonPlayer.InternalCacheProperties(hashtable2);
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, new object[]
					{
						photonPlayer,
						hashtable2
					});
				}
			}
		}
		if (this.CurrentRoom != null && gameProperties != null)
		{
			this.CurrentRoom.InternalCacheProperties(gameProperties);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCustomRoomPropertiesChanged, new object[]
			{
				gameProperties
			});
			if (PhotonNetwork.automaticallySyncScene)
			{
				this.LoadLevelIfSynced();
			}
		}
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00014840 File Offset: 0x00012A40
	private Hashtable ReadoutPropertiesForActorNr(Hashtable actorProperties, int actorNr)
	{
		if (actorProperties.ContainsKey(actorNr))
		{
			return (Hashtable)actorProperties[actorNr];
		}
		return actorProperties;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00014864 File Offset: 0x00012A64
	public void ChangeLocalID(int newID)
	{
		if (this.LocalPlayer == null)
		{
			Debug.LogWarning(string.Format("LocalPlayer is null or not in mActors! LocalPlayer: {0} mActors==null: {1} newID: {2}", this.LocalPlayer, this.mActors == null, newID));
		}
		if (this.mActors.ContainsKey(this.LocalPlayer.ID))
		{
			this.mActors.Remove(this.LocalPlayer.ID);
		}
		this.LocalPlayer.InternalChangeLocalID(newID);
		this.mActors[this.LocalPlayer.ID] = this.LocalPlayer;
		this.RebuildPlayerListCopies();
	}

	// Token: 0x06000378 RID: 888 RVA: 0x000148FF File Offset: 0x00012AFF
	private void LeftLobbyCleanup()
	{
		this.mGameList = new Dictionary<string, RoomInfo>();
		this.mGameListCopy = new RoomInfo[0];
		if (this.insideLobby)
		{
			this.insideLobby = false;
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLeftLobby, Array.Empty<object>());
		}
	}

	// Token: 0x06000379 RID: 889 RVA: 0x00014934 File Offset: 0x00012B34
	private void LeftRoomCleanup()
	{
		bool flag = this.CurrentRoom != null;
		bool flag2 = (this.CurrentRoom != null) ? this.CurrentRoom.AutoCleanUp : PhotonNetwork.autoCleanUpPlayerObjects;
		this.hasSwitchedMC = false;
		this.CurrentRoom = null;
		this.mActors = new Dictionary<int, PhotonPlayer>();
		this.mPlayerListCopy = new PhotonPlayer[0];
		this.mOtherPlayerListCopy = new PhotonPlayer[0];
		this.allowedReceivingGroups = new HashSet<byte>();
		this.blockSendingGroups = new HashSet<byte>();
		this.mGameList = new Dictionary<string, RoomInfo>();
		this.mGameListCopy = new RoomInfo[0];
		this.isFetchingFriendList = false;
		this.ChangeLocalID(-1);
		if (flag2)
		{
			this.LocalCleanupAnythingInstantiated(true);
			PhotonNetwork.manuallyAllocatedViewIds = new List<int>();
		}
		if (flag)
		{
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLeftRoom, Array.Empty<object>());
		}
	}

	// Token: 0x0600037A RID: 890 RVA: 0x000149F0 File Offset: 0x00012BF0
	protected internal void LocalCleanupAnythingInstantiated(bool destroyInstantiatedGameObjects)
	{
		if (this.tempInstantiationData.Count > 0)
		{
			Debug.LogWarning("It seems some instantiation is not completed, as instantiation data is used. You should make sure instantiations are paused when calling this method. Cleaning now, despite this.");
		}
		if (destroyInstantiatedGameObjects)
		{
			HashSet<GameObject> hashSet = new HashSet<GameObject>();
			foreach (PhotonView photonView in this.photonViewList.Values)
			{
				if (photonView.isRuntimeInstantiated)
				{
					hashSet.Add(photonView.gameObject);
				}
			}
			foreach (GameObject go in hashSet)
			{
				this.RemoveInstantiatedGO(go, true);
			}
		}
		this.tempInstantiationData.Clear();
		PhotonNetwork.lastUsedViewSubId = 0;
		PhotonNetwork.lastUsedViewSubIdStatic = 0;
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00014AD4 File Offset: 0x00012CD4
	private void GameEnteredOnGameServer(OperationResponse operationResponse)
	{
		if (operationResponse.ReturnCode != 0)
		{
			switch (operationResponse.OperationCode)
			{
			case 225:
				if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
				{
					Debug.Log("Join failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse.DebugMessage);
					if (operationResponse.ReturnCode == 32758)
					{
						Debug.Log("Most likely the game became empty during the switch to GameServer.");
					}
				}
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonRandomJoinFailed, new object[]
				{
					operationResponse.ReturnCode,
					operationResponse.DebugMessage
				});
				break;
			case 226:
				if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
				{
					Debug.Log("Join failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse.DebugMessage);
					if (operationResponse.ReturnCode == 32758)
					{
						Debug.Log("Most likely the game became empty during the switch to GameServer.");
					}
				}
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonJoinRoomFailed, new object[]
				{
					operationResponse.ReturnCode,
					operationResponse.DebugMessage
				});
				break;
			case 227:
				if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
				{
					Debug.Log("Create failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse.DebugMessage);
				}
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCreateRoomFailed, new object[]
				{
					operationResponse.ReturnCode,
					operationResponse.DebugMessage
				});
				break;
			}
			this.DisconnectToReconnect();
			return;
		}
		this.CurrentRoom = new Room(this.enterRoomParamsCache.RoomName, this.enterRoomParamsCache.RoomOptions)
		{
			IsLocalClientInside = true
		};
		this.State = ClientState.Joined;
		if (operationResponse.Parameters.ContainsKey(252))
		{
			int[] actorsInRoom = (int[])operationResponse.Parameters[252];
			this.UpdatedActorList(actorsInRoom);
		}
		int newID = (int)operationResponse[254];
		this.ChangeLocalID(newID);
		Hashtable pActorProperties = (Hashtable)operationResponse[249];
		Hashtable gameProperties = (Hashtable)operationResponse[248];
		this.ReadoutProperties(gameProperties, pActorProperties, 0);
		if (!this.CurrentRoom.serverSideMasterClient)
		{
			this.CheckMasterClient(-1);
		}
		if (this.mPlayernameHasToBeUpdated)
		{
			this.SendPlayerName();
		}
		byte operationCode = operationResponse.OperationCode;
		if (operationCode - 225 > 1 && operationCode == 227)
		{
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom, Array.Empty<object>());
		}
	}

	// Token: 0x0600037C RID: 892 RVA: 0x00014CFE File Offset: 0x00012EFE
	private void AddNewPlayer(int ID, PhotonPlayer player)
	{
		if (!this.mActors.ContainsKey(ID))
		{
			this.mActors[ID] = player;
			this.RebuildPlayerListCopies();
			return;
		}
		Debug.LogError("Adding player twice: " + ID);
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00014D37 File Offset: 0x00012F37
	private void RemovePlayer(int ID, PhotonPlayer player)
	{
		this.mActors.Remove(ID);
		if (!player.IsLocal)
		{
			this.RebuildPlayerListCopies();
		}
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00014D54 File Offset: 0x00012F54
	private void RebuildPlayerListCopies()
	{
		this.mPlayerListCopy = new PhotonPlayer[this.mActors.Count];
		this.mActors.Values.CopyTo(this.mPlayerListCopy, 0);
		List<PhotonPlayer> list = new List<PhotonPlayer>();
		for (int i = 0; i < this.mPlayerListCopy.Length; i++)
		{
			PhotonPlayer photonPlayer = this.mPlayerListCopy[i];
			if (!photonPlayer.IsLocal)
			{
				list.Add(photonPlayer);
			}
		}
		this.mOtherPlayerListCopy = list.ToArray();
	}

	// Token: 0x0600037F RID: 895 RVA: 0x00014DCC File Offset: 0x00012FCC
	private void ResetPhotonViewsOnSerialize()
	{
		foreach (PhotonView photonView in this.photonViewList.Values)
		{
			photonView.lastOnSerializeDataSent = null;
		}
	}

	// Token: 0x06000380 RID: 896 RVA: 0x00014E24 File Offset: 0x00013024
	private void HandleEventLeave(int actorID, EventData evLeave)
	{
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
		{
			Debug.Log(string.Concat(new object[]
			{
				"HandleEventLeave for player ID: ",
				actorID,
				" evLeave: ",
				evLeave.ToStringFull()
			}));
		}
		PhotonPlayer playerWithId = this.GetPlayerWithId(actorID);
		if (playerWithId == null)
		{
			Debug.LogError(string.Format("Received event Leave for unknown player ID: {0}", actorID));
			return;
		}
		bool isInactive = playerWithId.IsInactive;
		if (evLeave.Parameters.ContainsKey(233))
		{
			playerWithId.IsInactive = (bool)evLeave.Parameters[233];
			if (playerWithId.IsInactive != isInactive)
			{
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerActivityChanged, new object[]
				{
					playerWithId
				});
			}
			if (playerWithId.IsInactive && isInactive)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"HandleEventLeave for player ID: ",
					actorID,
					" isInactive: ",
					playerWithId.IsInactive.ToString(),
					". Stopping handling if inactive."
				}));
				return;
			}
		}
		if (evLeave.Parameters.ContainsKey(203))
		{
			if ((int)evLeave[203] != 0)
			{
				this.mMasterClientId = (int)evLeave[203];
				this.UpdateMasterClient();
			}
		}
		else if (!this.CurrentRoom.serverSideMasterClient)
		{
			this.CheckMasterClient(actorID);
		}
		if (playerWithId.IsInactive && !isInactive)
		{
			return;
		}
		if (this.CurrentRoom != null && this.CurrentRoom.AutoCleanUp)
		{
			this.DestroyPlayerObjects(actorID, true);
		}
		this.RemovePlayer(actorID, playerWithId);
		NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerDisconnected, new object[]
		{
			playerWithId
		});
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00014FC4 File Offset: 0x000131C4
	private void CheckMasterClient(int leavingPlayerId)
	{
		bool flag = this.mMasterClientId == leavingPlayerId;
		bool flag2 = leavingPlayerId > 0;
		if (flag2 && !flag)
		{
			return;
		}
		int num;
		if (this.mActors.Count <= 1)
		{
			num = this.LocalPlayer.ID;
		}
		else
		{
			num = int.MaxValue;
			foreach (int num2 in this.mActors.Keys)
			{
				if (num2 < num && num2 != leavingPlayerId)
				{
					num = num2;
				}
			}
		}
		this.mMasterClientId = num;
		if (flag2)
		{
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, new object[]
			{
				this.GetPlayerWithId(num)
			});
		}
	}

	// Token: 0x06000382 RID: 898 RVA: 0x0001507C File Offset: 0x0001327C
	protected internal void UpdateMasterClient()
	{
		NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, new object[]
		{
			PhotonNetwork.masterClient
		});
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00015094 File Offset: 0x00013294
	private static int ReturnLowestPlayerId(PhotonPlayer[] players, int playerIdToIgnore)
	{
		if (players == null || players.Length == 0)
		{
			return -1;
		}
		int num = int.MaxValue;
		foreach (PhotonPlayer photonPlayer in players)
		{
			if (photonPlayer.ID != playerIdToIgnore && photonPlayer.ID < num)
			{
				num = photonPlayer.ID;
			}
		}
		return num;
	}

	// Token: 0x06000384 RID: 900 RVA: 0x000150DC File Offset: 0x000132DC
	protected internal bool SetMasterClient(int playerId, bool sync)
	{
		if (this.mMasterClientId == playerId || !this.mActors.ContainsKey(playerId))
		{
			return false;
		}
		if (sync && !this.OpRaiseEvent(208, new Hashtable
		{
			{
				1,
				playerId
			}
		}, true, null))
		{
			return false;
		}
		this.hasSwitchedMC = true;
		this.CurrentRoom.MasterClientId = playerId;
		NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, new object[]
		{
			this.GetPlayerWithId(playerId)
		});
		return true;
	}

	// Token: 0x06000385 RID: 901 RVA: 0x0001515C File Offset: 0x0001335C
	public bool SetMasterClient(int nextMasterId)
	{
		Hashtable gameProperties = new Hashtable
		{
			{
				248,
				nextMasterId
			}
		};
		Hashtable expectedProperties = new Hashtable
		{
			{
				248,
				this.mMasterClientId
			}
		};
		return base.OpSetPropertiesOfRoom(gameProperties, expectedProperties, false);
	}

	// Token: 0x06000386 RID: 902 RVA: 0x000151B0 File Offset: 0x000133B0
	protected internal PhotonPlayer GetPlayerWithId(int number)
	{
		if (this.mActors == null)
		{
			return null;
		}
		PhotonPlayer result = null;
		this.mActors.TryGetValue(number, out result);
		return result;
	}

	// Token: 0x06000387 RID: 903 RVA: 0x000151DC File Offset: 0x000133DC
	private void SendPlayerName()
	{
		if (this.State == ClientState.Joining)
		{
			this.mPlayernameHasToBeUpdated = true;
			return;
		}
		if (this.LocalPlayer != null)
		{
			this.LocalPlayer.NickName = this.PlayerName;
			Hashtable hashtable = new Hashtable();
			hashtable[byte.MaxValue] = this.PlayerName;
			if (this.LocalPlayer.ID > 0)
			{
				base.OpSetPropertiesOfActor(this.LocalPlayer.ID, hashtable, null, false);
				this.mPlayernameHasToBeUpdated = false;
			}
		}
	}

	// Token: 0x06000388 RID: 904 RVA: 0x00015259 File Offset: 0x00013459
	private Hashtable GetLocalActorProperties()
	{
		if (PhotonNetwork.player != null)
		{
			return PhotonNetwork.player.AllProperties;
		}
		Hashtable hashtable = new Hashtable();
		hashtable[byte.MaxValue] = this.PlayerName;
		return hashtable;
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00015288 File Offset: 0x00013488
	public void DebugReturn(DebugLevel level, string message)
	{
		if (level == DebugLevel.ERROR)
		{
			Debug.LogError(message);
			return;
		}
		if (level == DebugLevel.WARNING)
		{
			Debug.LogWarning(message);
			return;
		}
		if (level == DebugLevel.INFO && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
		{
			Debug.Log(message);
			return;
		}
		if (level == DebugLevel.ALL && PhotonNetwork.logLevel == PhotonLogLevel.Full)
		{
			Debug.Log(message);
		}
	}

	// Token: 0x0600038A RID: 906 RVA: 0x000152C8 File Offset: 0x000134C8
	public void OnOperationResponse(OperationResponse operationResponse)
	{
		if (PhotonNetwork.networkingPeer.State == ClientState.Disconnecting)
		{
			if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
			{
				Debug.Log("OperationResponse ignored while disconnecting. Code: " + operationResponse.OperationCode);
			}
			return;
		}
		if (operationResponse.ReturnCode == 0)
		{
			if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
			{
				Debug.Log(operationResponse.ToString());
			}
		}
		else if (operationResponse.ReturnCode == -3)
		{
			Debug.LogError("Operation " + operationResponse.OperationCode + " could not be executed (yet). Wait for state JoinedLobby or ConnectedToMaster and their callbacks before calling operations. WebRPCs need a server-side configuration. Enum OperationCode helps identify the operation.");
		}
		else if (operationResponse.ReturnCode == 32752)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Operation ",
				operationResponse.OperationCode,
				" failed in a server-side plugin. Check the configuration in the Dashboard. Message from server-plugin: ",
				operationResponse.DebugMessage
			}));
		}
		else if (operationResponse.ReturnCode == 32760)
		{
			Debug.LogWarning("Operation failed: " + operationResponse.ToStringFull());
		}
		else
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Operation failed: ",
				operationResponse.ToStringFull(),
				" Server: ",
				this.Server
			}));
		}
		if (operationResponse.Parameters.ContainsKey(221))
		{
			if (this.AuthValues == null)
			{
				this.AuthValues = new AuthenticationValues();
			}
			this.AuthValues.Token = (operationResponse[221] as string);
			this.tokenCache = this.AuthValues.Token;
		}
		byte operationCode = operationResponse.OperationCode;
		switch (operationCode)
		{
		case 217:
		{
			if (operationResponse.ReturnCode != 0)
			{
				this.DebugReturn(DebugLevel.ERROR, "GetGameList failed: " + operationResponse.ToStringFull());
				return;
			}
			this.mGameList = new Dictionary<string, RoomInfo>();
			Hashtable hashtable = (Hashtable)operationResponse[222];
			foreach (object obj in hashtable.Keys)
			{
				string text = (string)obj;
				this.mGameList[text] = new RoomInfo(text, (Hashtable)hashtable[obj]);
			}
			this.mGameListCopy = new RoomInfo[this.mGameList.Count];
			this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate, Array.Empty<object>());
			return;
		}
		case 218:
		case 221:
		case 223:
		case 224:
			break;
		case 219:
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnWebRpcResponse, new object[]
			{
				operationResponse
			});
			return;
		case 220:
		{
			if (operationResponse.ReturnCode == 32767)
			{
				Debug.LogError(string.Format("The appId this client sent is unknown on the server (Cloud). Check settings. If using the Cloud, check account.", Array.Empty<object>()));
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, new object[]
				{
					DisconnectCause.InvalidAuthentication
				});
				this.State = ClientState.Disconnecting;
				this.Disconnect();
				return;
			}
			if (operationResponse.ReturnCode != 0)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"GetRegions failed. Can't provide regions list. Error: ",
					operationResponse.ReturnCode,
					": ",
					operationResponse.DebugMessage
				}));
				return;
			}
			string[] array = operationResponse[210] as string[];
			string[] array2 = operationResponse[230] as string[];
			if (array == null || array2 == null || array.Length != array2.Length)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"The region arrays from Name Server are not ok. Must be non-null and same length. ",
					(array == null).ToString(),
					" ",
					(array2 == null).ToString(),
					"\n",
					operationResponse.ToStringFull()
				}));
				return;
			}
			this.AvailableRegions = new List<Region>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				if (!string.IsNullOrEmpty(text2))
				{
					text2 = text2.ToLower();
					CloudRegionCode cloudRegionCode = Region.Parse(text2);
					bool flag = true;
					if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.BestRegion && PhotonNetwork.PhotonServerSettings.EnabledRegions != (CloudRegionFlag)0)
					{
						CloudRegionFlag cloudRegionFlag = Region.ParseFlag(cloudRegionCode);
						flag = ((PhotonNetwork.PhotonServerSettings.EnabledRegions & cloudRegionFlag) > (CloudRegionFlag)0);
						if (!flag && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
						{
							Debug.Log("Skipping region because it's not in PhotonServerSettings.EnabledRegions: " + cloudRegionCode);
						}
					}
					if (flag)
					{
						this.AvailableRegions.Add(new Region(cloudRegionCode, text2, array2[i]));
					}
				}
			}
			if (PhotonNetwork.PhotonServerSettings.HostType != ServerSettings.HostingOption.BestRegion)
			{
				return;
			}
			CloudRegionCode bestFromPrefs = PhotonHandler.BestRegionCodeInPreferences;
			if (bestFromPrefs == CloudRegionCode.none || !this.AvailableRegions.Exists((Region x) => x.Code == bestFromPrefs))
			{
				PhotonHandler.PingAvailableRegionsAndConnectToBest();
				return;
			}
			Debug.Log("Best region found in PlayerPrefs. Connecting to: " + bestFromPrefs);
			if (!this.ConnectToRegionMaster(bestFromPrefs, null))
			{
				PhotonHandler.PingAvailableRegionsAndConnectToBest();
				return;
			}
			return;
		}
		case 222:
		{
			bool[] array3 = operationResponse[1] as bool[];
			string[] array4 = operationResponse[2] as string[];
			if (array3 != null && array4 != null && this.friendListRequested != null && array3.Length == this.friendListRequested.Length)
			{
				List<FriendInfo> list = new List<FriendInfo>(this.friendListRequested.Length);
				for (int j = 0; j < this.friendListRequested.Length; j++)
				{
					list.Insert(j, new FriendInfo
					{
						UserId = this.friendListRequested[j],
						Room = array4[j],
						IsOnline = array3[j]
					});
				}
				PhotonNetwork.Friends = list;
			}
			else
			{
				Debug.LogError("FindFriends failed to apply the result, as a required value wasn't provided or the friend list length differed from result.");
			}
			this.friendListRequested = null;
			this.isFetchingFriendList = false;
			this.friendListTimestamp = Environment.TickCount;
			if (this.friendListTimestamp == 0)
			{
				this.friendListTimestamp = 1;
			}
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnUpdatedFriendList, Array.Empty<object>());
			return;
		}
		case 225:
		{
			if (operationResponse.ReturnCode != 0)
			{
				if (operationResponse.ReturnCode == 32760)
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
					{
						Debug.Log("JoinRandom failed: No open game. Calling: OnPhotonRandomJoinFailed() and staying on master server.");
					}
				}
				else if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
				{
					Debug.LogWarning(string.Format("JoinRandom failed: {0}.", operationResponse.ToStringFull()));
				}
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonRandomJoinFailed, new object[]
				{
					operationResponse.ReturnCode,
					operationResponse.DebugMessage
				});
				return;
			}
			string roomName = (string)operationResponse[byte.MaxValue];
			this.enterRoomParamsCache.RoomName = roomName;
			this.GameServerAddress = (string)operationResponse[230];
			if (PhotonNetwork.UseAlternativeUdpPorts && base.TransportProtocol == ConnectionProtocol.Udp)
			{
				this.GameServerAddress = this.GameServerAddress.Replace("5058", "27000").Replace("5055", "27001").Replace("5056", "27002");
			}
			this.DisconnectToReconnect();
			return;
		}
		case 226:
			if (this.Server == ServerConnection.GameServer)
			{
				this.GameEnteredOnGameServer(operationResponse);
				return;
			}
			if (operationResponse.ReturnCode != 0)
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
				{
					Debug.Log(string.Format("JoinRoom failed (room maybe closed by now). Client stays on masterserver: {0}. State: {1}", operationResponse.ToStringFull(), this.State));
				}
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonJoinRoomFailed, new object[]
				{
					operationResponse.ReturnCode,
					operationResponse.DebugMessage
				});
				return;
			}
			this.GameServerAddress = (string)operationResponse[230];
			if (PhotonNetwork.UseAlternativeUdpPorts && base.TransportProtocol == ConnectionProtocol.Udp)
			{
				this.GameServerAddress = this.GameServerAddress.Replace("5058", "27000").Replace("5055", "27001").Replace("5056", "27002");
			}
			this.DisconnectToReconnect();
			return;
		case 227:
		{
			if (this.Server == ServerConnection.GameServer)
			{
				this.GameEnteredOnGameServer(operationResponse);
				return;
			}
			if (operationResponse.ReturnCode != 0)
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
				{
					Debug.LogWarning(string.Format("CreateRoom failed, client stays on masterserver: {0}.", operationResponse.ToStringFull()));
				}
				this.State = (this.insideLobby ? ClientState.JoinedLobby : ClientState.ConnectedToMaster);
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCreateRoomFailed, new object[]
				{
					operationResponse.ReturnCode,
					operationResponse.DebugMessage
				});
				return;
			}
			string text3 = (string)operationResponse[byte.MaxValue];
			if (!string.IsNullOrEmpty(text3))
			{
				this.enterRoomParamsCache.RoomName = text3;
			}
			this.GameServerAddress = (string)operationResponse[230];
			if (PhotonNetwork.UseAlternativeUdpPorts && base.TransportProtocol == ConnectionProtocol.Udp)
			{
				this.GameServerAddress = this.GameServerAddress.Replace("5058", "27000").Replace("5055", "27001").Replace("5056", "27002");
			}
			this.DisconnectToReconnect();
			return;
		}
		case 228:
			this.State = ClientState.Authenticated;
			this.LeftLobbyCleanup();
			return;
		case 229:
			this.State = ClientState.JoinedLobby;
			this.insideLobby = true;
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedLobby, Array.Empty<object>());
			return;
		case 230:
		case 231:
			if (operationResponse.ReturnCode != 0)
			{
				if (operationResponse.ReturnCode == -2)
				{
					Debug.LogError(string.Format("If you host Photon yourself, make sure to start the 'Instance LoadBalancing' " + base.ServerAddress, Array.Empty<object>()));
				}
				else if (operationResponse.ReturnCode == 32767)
				{
					Debug.LogError(string.Format("The appId this client sent is unknown on the server (Cloud). Check settings. If using the Cloud, check account.", Array.Empty<object>()));
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, new object[]
					{
						DisconnectCause.InvalidAuthentication
					});
				}
				else if (operationResponse.ReturnCode == 32755)
				{
					Debug.LogError(string.Format("Custom Authentication failed (either due to user-input or configuration or AuthParameter string format). Calling: OnCustomAuthenticationFailed()", Array.Empty<object>()));
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCustomAuthenticationFailed, new object[]
					{
						operationResponse.DebugMessage
					});
				}
				else
				{
					Debug.LogError(string.Format("Authentication failed: '{0}' Code: {1}", operationResponse.DebugMessage, operationResponse.ReturnCode));
				}
				this.State = ClientState.Disconnecting;
				this.Disconnect();
				if (operationResponse.ReturnCode == 32757)
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
					{
						Debug.LogWarning(string.Format("Currently, the limit of users is reached for this title. Try again later. Disconnecting", Array.Empty<object>()));
					}
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonMaxCccuReached, Array.Empty<object>());
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, new object[]
					{
						DisconnectCause.MaxCcuReached
					});
					return;
				}
				if (operationResponse.ReturnCode == 32756)
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
					{
						Debug.LogError(string.Format("The used master server address is not available with the subscription currently used. Got to Photon Cloud Dashboard or change URL. Disconnecting.", Array.Empty<object>()));
					}
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, new object[]
					{
						DisconnectCause.InvalidRegion
					});
					return;
				}
				if (operationResponse.ReturnCode == 32753)
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
					{
						Debug.LogError(string.Format("The authentication ticket expired. You need to connect (and authenticate) again. Disconnecting.", Array.Empty<object>()));
					}
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, new object[]
					{
						DisconnectCause.AuthenticationTicketExpired
					});
					return;
				}
				return;
			}
			else
			{
				if (this.Server == ServerConnection.NameServer || this.Server == ServerConnection.MasterServer)
				{
					if (operationResponse.Parameters.ContainsKey(225))
					{
						string text4 = (string)operationResponse.Parameters[225];
						if (!string.IsNullOrEmpty(text4))
						{
							if (this.AuthValues == null)
							{
								this.AuthValues = new AuthenticationValues();
							}
							this.AuthValues.UserId = text4;
							PhotonNetwork.player.UserId = text4;
							if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
							{
								this.DebugReturn(DebugLevel.INFO, string.Format("Received your UserID from server. Updating local value to: {0}", text4));
							}
						}
					}
					if (operationResponse.Parameters.ContainsKey(202))
					{
						this.PlayerName = (string)operationResponse.Parameters[202];
						if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
						{
							this.DebugReturn(DebugLevel.INFO, string.Format("Received your NickName from server. Updating local value to: {0}", this.playername));
						}
					}
					if (operationResponse.Parameters.ContainsKey(192))
					{
						this.SetupEncryption((Dictionary<byte, object>)operationResponse.Parameters[192]);
					}
				}
				if (this.Server == ServerConnection.NameServer)
				{
					this.MasterServerAddress = (operationResponse[230] as string);
					if (PhotonNetwork.UseAlternativeUdpPorts && base.TransportProtocol == ConnectionProtocol.Udp)
					{
						this.MasterServerAddress = this.MasterServerAddress.Replace("5058", "27000").Replace("5055", "27001").Replace("5056", "27002");
					}
					string text5 = operationResponse[196] as string;
					if (!string.IsNullOrEmpty(text5))
					{
						this.CurrentCluster = text5;
					}
					this.DisconnectToReconnect();
				}
				else if (this.Server == ServerConnection.MasterServer)
				{
					if (this.AuthMode != AuthModeOption.Auth)
					{
						this.OpSettings(this.requestLobbyStatistics);
					}
					if (PhotonNetwork.autoJoinLobby)
					{
						this.State = ClientState.Authenticated;
						this.OpJoinLobby(this.lobby);
					}
					else
					{
						this.State = ClientState.ConnectedToMaster;
						NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectedToMaster, Array.Empty<object>());
					}
				}
				else if (this.Server == ServerConnection.GameServer)
				{
					this.State = ClientState.Joining;
					this.enterRoomParamsCache.PlayerProperties = this.GetLocalActorProperties();
					this.enterRoomParamsCache.OnGameServer = true;
					if (this.lastJoinType == JoinType.JoinRoom || this.lastJoinType == JoinType.JoinRandomRoom || this.lastJoinType == JoinType.JoinOrCreateRoom)
					{
						this.OpJoinRoom(this.enterRoomParamsCache);
					}
					else if (this.lastJoinType == JoinType.CreateRoom)
					{
						this.OpCreateGame(this.enterRoomParamsCache);
					}
				}
				if (!operationResponse.Parameters.ContainsKey(245))
				{
					return;
				}
				Dictionary<string, object> dictionary = (Dictionary<string, object>)operationResponse.Parameters[245];
				if (dictionary != null)
				{
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCustomAuthenticationResponse, new object[]
					{
						dictionary
					});
					return;
				}
				return;
			}
			break;
		default:
			switch (operationCode)
			{
			case 251:
			{
				Hashtable pActorProperties = (Hashtable)operationResponse[249];
				Hashtable gameProperties = (Hashtable)operationResponse[248];
				this.ReadoutProperties(gameProperties, pActorProperties, 0);
				return;
			}
			case 252:
			case 253:
				return;
			case 254:
				this.DisconnectToReconnect();
				return;
			}
			break;
		}
		Debug.LogWarning(string.Format("OperationResponse unhandled: {0}", operationResponse.ToString()));
	}

	// Token: 0x0600038B RID: 907 RVA: 0x00016064 File Offset: 0x00014264
	public void OnStatusChanged(StatusCode statusCode)
	{
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
		{
			Debug.Log(string.Format("OnStatusChanged: {0} current State: {1}", statusCode.ToString(), this.State));
		}
		switch (statusCode)
		{
		case StatusCode.SecurityExceptionOnConnect:
		case StatusCode.ExceptionOnConnect:
		{
			this.IsInitialConnect = false;
			this.State = ClientState.PeerCreated;
			if (this.AuthValues != null)
			{
				this.AuthValues.Token = null;
			}
			DisconnectCause disconnectCause = (DisconnectCause)statusCode;
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, new object[]
			{
				disconnectCause
			});
			return;
		}
		case StatusCode.Connect:
			if (this.State == ClientState.ConnectingToNameServer)
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
				{
					Debug.Log("Connected to NameServer.");
				}
				this.Server = ServerConnection.NameServer;
				if (this.AuthValues != null)
				{
					this.AuthValues.Token = null;
				}
			}
			if (this.State == ClientState.ConnectingToGameserver)
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
				{
					Debug.Log("Connected to gameserver.");
				}
				this.Server = ServerConnection.GameServer;
				this.State = ClientState.ConnectedToGameserver;
			}
			if (this.State == ClientState.ConnectingToMasterserver)
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
				{
					Debug.Log("Connected to masterserver.");
				}
				this.Server = ServerConnection.MasterServer;
				this.State = ClientState.Authenticating;
				if (this.IsInitialConnect)
				{
					this.IsInitialConnect = false;
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectedToPhoton, Array.Empty<object>());
				}
			}
			if (PhotonNetwork.offlineMode)
			{
				return;
			}
			if (base.TransportProtocol != ConnectionProtocol.WebSocketSecure)
			{
				if (this.Server == ServerConnection.NameServer || this.AuthMode == AuthModeOption.Auth)
				{
					base.EstablishEncryption();
					return;
				}
				return;
			}
			else if (this.DebugOut == DebugLevel.INFO)
			{
				Debug.Log("Skipping EstablishEncryption. Protocol is secure.");
			}
			break;
		case StatusCode.Disconnect:
			this.didAuthenticate = false;
			this.isFetchingFriendList = false;
			if (this.Server == ServerConnection.GameServer)
			{
				this.LeftRoomCleanup();
			}
			if (this.Server == ServerConnection.MasterServer)
			{
				this.LeftLobbyCleanup();
			}
			if (this.State == ClientState.DisconnectingFromMasterserver)
			{
				if (this.Connect(this.GameServerAddress, ServerConnection.GameServer))
				{
					this.State = ClientState.ConnectingToGameserver;
					return;
				}
				return;
			}
			else if (this.State == ClientState.DisconnectingFromGameserver || this.State == ClientState.DisconnectingFromNameServer)
			{
				this.SetupProtocol(ServerConnection.MasterServer);
				if (this.Connect(this.MasterServerAddress, ServerConnection.MasterServer))
				{
					this.State = ClientState.ConnectingToMasterserver;
					return;
				}
				return;
			}
			else
			{
				if (this._isReconnecting)
				{
					return;
				}
				if (this.AuthValues != null)
				{
					this.AuthValues.Token = null;
				}
				this.IsInitialConnect = false;
				this.State = ClientState.PeerCreated;
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnDisconnectedFromPhoton, Array.Empty<object>());
				return;
			}
			break;
		case StatusCode.Exception:
			if (this.IsInitialConnect)
			{
				Debug.LogError("Exception while connecting to: " + base.ServerAddress + ". Check if the server is available.");
				if (base.ServerAddress == null || base.ServerAddress.StartsWith("127.0.0.1"))
				{
					Debug.LogWarning("The server address is 127.0.0.1 (localhost): Make sure the server is running on this machine. Android and iOS emulators have their own localhost.");
					if (base.ServerAddress == this.GameServerAddress)
					{
						Debug.LogWarning("This might be a misconfiguration in the game server config. You need to edit it to a (public) address.");
					}
				}
				this.State = ClientState.PeerCreated;
				DisconnectCause disconnectCause = (DisconnectCause)statusCode;
				this.IsInitialConnect = false;
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, new object[]
				{
					disconnectCause
				});
			}
			else
			{
				this.State = ClientState.PeerCreated;
				DisconnectCause disconnectCause = (DisconnectCause)statusCode;
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, new object[]
				{
					disconnectCause
				});
			}
			this.Disconnect();
			return;
		case (StatusCode)1027:
		case (StatusCode)1028:
		case (StatusCode)1029:
			goto IL_583;
		case StatusCode.SendError:
			goto IL_4F5;
		default:
			switch (statusCode)
			{
			case StatusCode.ExceptionOnReceive:
			case StatusCode.DisconnectByServerTimeout:
			case StatusCode.DisconnectByServerUserLimit:
			case StatusCode.DisconnectByServerLogic:
				goto IL_4F5;
			case StatusCode.TimeoutDisconnect:
				if (this.IsInitialConnect)
				{
					if (!this._isReconnecting)
					{
						Debug.LogWarning(string.Concat(new object[]
						{
							statusCode,
							" while connecting to: ",
							base.ServerAddress,
							". Check if the server is available."
						}));
						this.IsInitialConnect = false;
						DisconnectCause disconnectCause = (DisconnectCause)statusCode;
						NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, new object[]
						{
							disconnectCause
						});
					}
				}
				else
				{
					DisconnectCause disconnectCause = (DisconnectCause)statusCode;
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, new object[]
					{
						disconnectCause
					});
				}
				if (this.AuthValues != null)
				{
					this.AuthValues.Token = null;
				}
				this.Disconnect();
				return;
			case StatusCode.DisconnectByServerReasonUnknown:
			case (StatusCode)1045:
			case (StatusCode)1046:
			case (StatusCode)1047:
				goto IL_583;
			case StatusCode.EncryptionEstablished:
				break;
			case StatusCode.EncryptionFailedToEstablish:
			{
				Debug.LogError("Encryption wasn't established: " + statusCode + ". Going to authenticate anyways.");
				AuthenticationValues authenticationValues;
				if ((authenticationValues = this.AuthValues) == null)
				{
					(authenticationValues = new AuthenticationValues()).UserId = this.PlayerName;
				}
				AuthenticationValues authValues = authenticationValues;
				this.OpAuthenticate(this.AppId, this.AppVersion, authValues, this.CloudRegion.ToString(), this.requestLobbyStatistics);
				return;
			}
			default:
				goto IL_583;
			}
			break;
		}
		this._isReconnecting = false;
		if (this.Server == ServerConnection.NameServer)
		{
			this.State = ClientState.ConnectedToNameServer;
			if (!this.didAuthenticate && this.CloudRegion == CloudRegionCode.none)
			{
				this.OpGetRegions(this.AppId);
			}
		}
		if (this.Server != ServerConnection.NameServer && (this.AuthMode == AuthModeOption.AuthOnce || this.AuthMode == AuthModeOption.AuthOnceWss))
		{
			Debug.Log(string.Concat(new object[]
			{
				"didAuthenticate ",
				this.didAuthenticate.ToString(),
				" AuthMode ",
				this.AuthMode
			}));
			return;
		}
		if (this.didAuthenticate || (this.IsUsingNameServer && this.CloudRegion == CloudRegionCode.none))
		{
			return;
		}
		this.didAuthenticate = this.CallAuthenticate();
		if (this.didAuthenticate)
		{
			this.State = ClientState.Authenticating;
			return;
		}
		return;
		IL_4F5:
		if (this.IsInitialConnect)
		{
			Debug.LogWarning(string.Concat(new object[]
			{
				statusCode,
				" while connecting to: ",
				base.ServerAddress,
				". Check if the server is available."
			}));
			this.IsInitialConnect = false;
			DisconnectCause disconnectCause = (DisconnectCause)statusCode;
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, new object[]
			{
				disconnectCause
			});
		}
		else
		{
			DisconnectCause disconnectCause = (DisconnectCause)statusCode;
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, new object[]
			{
				disconnectCause
			});
		}
		if (this.AuthValues != null)
		{
			this.AuthValues.Token = null;
		}
		this.Disconnect();
		return;
		IL_583:
		Debug.LogError("Received unknown status code: " + statusCode);
	}

	// Token: 0x0600038C RID: 908 RVA: 0x0001660C File Offset: 0x0001480C
	public void OnEvent(EventData photonEvent)
	{
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
		{
			Debug.Log(string.Format("OnEvent: {0}", photonEvent.ToString()));
		}
		int sender = photonEvent.Sender;
		PhotonPlayer playerWithId = this.GetPlayerWithId(sender);
		byte code = photonEvent.Code;
		switch (code)
		{
		case 200:
			this.ExecuteRpc(photonEvent[245] as Hashtable, sender);
			return;
		case 201:
		case 206:
		{
			Hashtable hashtable = (Hashtable)photonEvent[245];
			int networkTime = (int)hashtable[0];
			short correctPrefix = -1;
			byte b = 10;
			int num = 1;
			if (hashtable.ContainsKey(1))
			{
				correctPrefix = (short)hashtable[1];
				num = 2;
			}
			byte b2 = b;
			while ((int)(b2 - b) < hashtable.Count - num)
			{
				this.OnSerializeRead(hashtable[b2] as object[], playerWithId, networkTime, correctPrefix);
				b2 += 1;
			}
			return;
		}
		case 202:
			this.DoInstantiate((Hashtable)photonEvent[245], playerWithId, null);
			return;
		case 203:
			if (playerWithId == null || !playerWithId.IsMasterClient)
			{
				Debug.LogError("Error: Someone else(" + playerWithId + ") then the masterserver requests a disconnect!");
				return;
			}
			if (this._AsyncLevelLoadingOperation != null)
			{
				this._AsyncLevelLoadingOperation = null;
			}
			PhotonNetwork.LeaveRoom(false);
			return;
		case 204:
		{
			int num2 = (int)((Hashtable)photonEvent[245])[0];
			PhotonView photonView = null;
			if (this.photonViewList.TryGetValue(num2, out photonView))
			{
				this.RemoveInstantiatedGO(photonView.gameObject, true);
				return;
			}
			if (this.DebugOut >= DebugLevel.ERROR)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Ev Destroy Failed. Could not find PhotonView with instantiationId ",
					num2,
					". Sent by actorNr: ",
					sender
				}));
				return;
			}
			return;
		}
		case 205:
		case 211:
		case 213:
		case 214:
		case 215:
		case 216:
		case 217:
		case 218:
		case 219:
		case 220:
		case 221:
		case 222:
		case 225:
		case 227:
		case 228:
			break;
		case 207:
		{
			int num3 = (int)((Hashtable)photonEvent[245])[0];
			if (num3 >= 0)
			{
				this.DestroyPlayerObjects(num3, true);
				return;
			}
			if (this.DebugOut >= DebugLevel.INFO)
			{
				Debug.Log("Ev DestroyAll! By PlayerId: " + sender);
			}
			this.DestroyAll(true);
			return;
		}
		case 208:
		{
			int playerId = (int)((Hashtable)photonEvent[245])[1];
			this.SetMasterClient(playerId, false);
			return;
		}
		case 209:
		{
			int[] array = (int[])photonEvent.Parameters[245];
			int num4 = array[0];
			int num5 = array[1];
			PhotonView photonView2 = PhotonView.Find(num4);
			if (photonView2 == null)
			{
				Debug.LogWarning("Can't find PhotonView of incoming OwnershipRequest. ViewId not found: " + num4);
				return;
			}
			if (PhotonNetwork.logLevel == PhotonLogLevel.Informational)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Ev OwnershipRequest ",
					photonView2.ownershipTransfer,
					". ActorNr: ",
					sender,
					" takes from: ",
					num5,
					". local RequestedView.ownerId: ",
					photonView2.ownerId,
					" isOwnerActive: ",
					photonView2.isOwnerActive.ToString(),
					". MasterClient: ",
					this.mMasterClientId,
					". This client's player: ",
					PhotonNetwork.player.ToStringFull()
				}));
			}
			switch (photonView2.ownershipTransfer)
			{
			case OwnershipOption.Fixed:
				Debug.LogWarning("Ownership mode == fixed. Ignoring request.");
				return;
			case OwnershipOption.Takeover:
				if (num5 == photonView2.ownerId || (num5 == 0 && photonView2.ownerId == this.mMasterClientId) || photonView2.ownerId == 0)
				{
					photonView2.OwnerShipWasTransfered = true;
					int ownerId = photonView2.ownerId;
					PhotonPlayer playerWithId2 = this.GetPlayerWithId(ownerId);
					photonView2.ownerId = sender;
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
					{
						Debug.LogWarning(photonView2 + " ownership transfered to: " + sender);
					}
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnOwnershipTransfered, new object[]
					{
						photonView2,
						playerWithId,
						playerWithId2
					});
					return;
				}
				return;
			case OwnershipOption.Request:
				if ((num5 == PhotonNetwork.player.ID || PhotonNetwork.player.IsMasterClient) && (photonView2.ownerId == PhotonNetwork.player.ID || (PhotonNetwork.player.IsMasterClient && !photonView2.isOwnerActive)))
				{
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnOwnershipRequest, new object[]
					{
						photonView2,
						playerWithId
					});
					return;
				}
				return;
			default:
				return;
			}
			break;
		}
		case 210:
		{
			int[] array2 = (int[])photonEvent.Parameters[245];
			if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Ev OwnershipTransfer. ViewID ",
					array2[0],
					" to: ",
					array2[1],
					" Time: ",
					Environment.TickCount % 1000
				}));
			}
			int viewID = array2[0];
			int num6 = array2[1];
			PhotonView photonView3 = PhotonView.Find(viewID);
			if (photonView3 != null)
			{
				int ownerId2 = photonView3.ownerId;
				photonView3.OwnerShipWasTransfered = true;
				photonView3.ownerId = num6;
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnOwnershipTransfered, new object[]
				{
					photonView3,
					PhotonPlayer.Find(num6),
					PhotonPlayer.Find(ownerId2)
				});
				return;
			}
			return;
		}
		case 212:
			if ((bool)photonEvent.Parameters[245])
			{
				PhotonNetwork.LoadLevelAsync(SceneManagerHelper.ActiveSceneName);
				return;
			}
			PhotonNetwork.LoadLevel(SceneManagerHelper.ActiveSceneName);
			return;
		case 223:
			if (this.AuthValues == null)
			{
				this.AuthValues = new AuthenticationValues();
			}
			this.AuthValues.Token = (photonEvent[221] as string);
			this.tokenCache = this.AuthValues.Token;
			return;
		case 224:
		{
			string[] array3 = photonEvent[213] as string[];
			byte[] array4 = photonEvent[212] as byte[];
			int[] array5 = photonEvent[229] as int[];
			int[] array6 = photonEvent[228] as int[];
			this.LobbyStatistics.Clear();
			for (int i = 0; i < array3.Length; i++)
			{
				TypedLobbyInfo typedLobbyInfo = new TypedLobbyInfo();
				typedLobbyInfo.Name = array3[i];
				typedLobbyInfo.Type = (LobbyType)array4[i];
				typedLobbyInfo.PlayerCount = array5[i];
				typedLobbyInfo.RoomCount = array6[i];
				this.LobbyStatistics.Add(typedLobbyInfo);
			}
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLobbyStatisticsUpdate, Array.Empty<object>());
			return;
		}
		case 226:
			this.PlayersInRoomsCount = (int)photonEvent[229];
			this.PlayersOnMasterCount = (int)photonEvent[227];
			this.RoomsCount = (int)photonEvent[228];
			return;
		case 229:
		{
			Hashtable hashtable2 = (Hashtable)photonEvent[222];
			foreach (object obj in hashtable2.Keys)
			{
				string text = (string)obj;
				RoomInfo roomInfo = new RoomInfo(text, (Hashtable)hashtable2[obj]);
				if (roomInfo.removedFromList)
				{
					this.mGameList.Remove(text);
				}
				else
				{
					this.mGameList[text] = roomInfo;
				}
			}
			this.mGameListCopy = new RoomInfo[this.mGameList.Count];
			this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate, Array.Empty<object>());
			return;
		}
		case 230:
		{
			this.mGameList = new Dictionary<string, RoomInfo>();
			Hashtable hashtable3 = (Hashtable)photonEvent[222];
			foreach (object obj2 in hashtable3.Keys)
			{
				string text2 = (string)obj2;
				this.mGameList[text2] = new RoomInfo(text2, (Hashtable)hashtable3[obj2]);
			}
			this.mGameListCopy = new RoomInfo[this.mGameList.Count];
			this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate, Array.Empty<object>());
			return;
		}
		default:
			switch (code)
			{
			case 251:
				if (!PhotonNetwork.CallEvent(photonEvent.Code, photonEvent[218], sender))
				{
					Debug.LogWarning("Warning: Unhandled Event ErrorInfo (251). Set PhotonNetwork.OnEventCall to the method PUN should call for this event.");
					return;
				}
				return;
			case 253:
			{
				int num7 = (int)photonEvent[253];
				Hashtable gameProperties = null;
				Hashtable pActorProperties = null;
				if (num7 == 0)
				{
					gameProperties = (Hashtable)photonEvent[251];
				}
				else
				{
					pActorProperties = (Hashtable)photonEvent[251];
				}
				this.ReadoutProperties(gameProperties, pActorProperties, num7);
				return;
			}
			case 254:
				if (this._AsyncLevelLoadingOperation != null)
				{
					this._AsyncLevelLoadingOperation = null;
				}
				this.HandleEventLeave(sender, photonEvent);
				return;
			case 255:
			{
				bool flag = false;
				Hashtable properties = (Hashtable)photonEvent[249];
				if (playerWithId == null)
				{
					bool isLocal = this.LocalPlayer.ID == sender;
					this.AddNewPlayer(sender, new PhotonPlayer(isLocal, sender, properties));
					this.ResetPhotonViewsOnSerialize();
				}
				else
				{
					flag = playerWithId.IsInactive;
					playerWithId.InternalCacheProperties(properties);
					playerWithId.IsInactive = false;
				}
				if (sender == this.LocalPlayer.ID)
				{
					int[] actorsInRoom = (int[])photonEvent[252];
					this.UpdatedActorList(actorsInRoom);
					if (this.lastJoinType == JoinType.JoinOrCreateRoom && this.LocalPlayer.ID == 1)
					{
						NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom, Array.Empty<object>());
					}
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom, Array.Empty<object>());
					return;
				}
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerConnected, new object[]
				{
					this.mActors[sender]
				});
				if (flag)
				{
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerActivityChanged, new object[]
					{
						this.mActors[sender]
					});
					return;
				}
				return;
			}
			}
			break;
		}
		if (photonEvent.Code < 200 && !PhotonNetwork.CallEvent(photonEvent.Code, photonEvent[245], sender))
		{
			Debug.LogWarning("Warning: Unhandled event " + photonEvent + ". Set PhotonNetwork.OnEventCall.");
		}
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00003F60 File Offset: 0x00002160
	public void OnMessage(object messages)
	{
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00017098 File Offset: 0x00015298
	private void SetupEncryption(Dictionary<byte, object> encryptionData)
	{
		if (this.AuthMode == AuthModeOption.Auth && this.DebugOut == DebugLevel.ERROR)
		{
			Debug.LogWarning("SetupEncryption() called but ignored. Not XB1 compiled. EncryptionData: " + encryptionData.ToStringFull());
			return;
		}
		if (this.DebugOut == DebugLevel.INFO)
		{
			Debug.Log("SetupEncryption() got called. " + encryptionData.ToStringFull());
		}
		EncryptionMode encryptionMode = (EncryptionMode)((byte)encryptionData[0]);
		if (encryptionMode == EncryptionMode.PayloadEncryption)
		{
			byte[] secret = (byte[])encryptionData[1];
			base.InitPayloadEncryption(secret);
			return;
		}
		if (encryptionMode != EncryptionMode.DatagramEncryption)
		{
			throw new ArgumentOutOfRangeException();
		}
		byte[] encryptionSecret = (byte[])encryptionData[1];
		byte[] hmacSecret = (byte[])encryptionData[2];
		base.InitDatagramEncryption(encryptionSecret, hmacSecret, false, false);
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00017144 File Offset: 0x00015344
	protected internal void UpdatedActorList(int[] actorsInRoom)
	{
		foreach (int num in actorsInRoom)
		{
			if (this.LocalPlayer.ID != num && !this.mActors.ContainsKey(num))
			{
				this.AddNewPlayer(num, new PhotonPlayer(false, num, string.Empty));
			}
		}
	}

	// Token: 0x06000390 RID: 912 RVA: 0x00017194 File Offset: 0x00015394
	private void SendVacantViewIds()
	{
		Debug.Log("SendVacantViewIds()");
		List<int> list = new List<int>();
		foreach (PhotonView photonView in this.photonViewList.Values)
		{
			if (!photonView.isOwnerActive)
			{
				list.Add(photonView.viewID);
			}
		}
		Debug.Log("Sending vacant view IDs. Length: " + list.Count);
		this.OpRaiseEvent(211, list.ToArray(), true, null);
	}

	// Token: 0x06000391 RID: 913 RVA: 0x00017238 File Offset: 0x00015438
	public static void SendMonoMessage(PhotonNetworkingMessage methodString, params object[] parameters)
	{
		HashSet<GameObject> hashSet;
		if (PhotonNetwork.SendMonoMessageTargets != null)
		{
			hashSet = PhotonNetwork.SendMonoMessageTargets;
		}
		else
		{
			hashSet = PhotonNetwork.FindGameObjectsWithComponent(PhotonNetwork.SendMonoMessageTargetType);
		}
		string methodName = methodString.ToString();
		object value = (parameters != null && parameters.Length == 1) ? parameters[0] : parameters;
		foreach (GameObject gameObject in hashSet)
		{
			if (gameObject != null)
			{
				gameObject.SendMessage(methodName, value, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x000172CC File Offset: 0x000154CC
	protected internal void ExecuteRpc(Hashtable rpcData, int senderID = 0)
	{
		if (rpcData == null || !rpcData.ContainsKey(this.keyByteZero))
		{
			Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(rpcData));
			return;
		}
		int num = (int)rpcData[this.keyByteZero];
		int num2 = 0;
		if (rpcData.ContainsKey(this.keyByteOne))
		{
			num2 = (int)((short)rpcData[this.keyByteOne]);
		}
		string text;
		if (rpcData.ContainsKey(this.keyByteFive))
		{
			int num3 = (int)((byte)rpcData[this.keyByteFive]);
			if (num3 > PhotonNetwork.PhotonServerSettings.RpcList.Count - 1)
			{
				Debug.LogError("Could not find RPC with index: " + num3 + ". Going to ignore! Check PhotonServerSettings.RpcList");
				return;
			}
			text = PhotonNetwork.PhotonServerSettings.RpcList[num3];
		}
		else
		{
			text = (string)rpcData[this.keyByteThree];
		}
		object[] array = this.emptyObjectArray;
		if (rpcData.ContainsKey(this.keyByteFour))
		{
			array = (object[])rpcData[this.keyByteFour];
		}
		PhotonView photonView = this.GetPhotonView(num);
		if (photonView == null)
		{
			int num4 = num / PhotonNetwork.MAX_VIEW_IDS;
			bool flag = num4 == this.LocalPlayer.ID;
			bool flag2 = num4 == senderID;
			if (flag)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Received RPC \"",
					text,
					"\" for viewID ",
					num,
					" but this PhotonView does not exist! View was/is ours.",
					flag2 ? " Owner called." : " Remote called.",
					" By: ",
					senderID
				}));
				return;
			}
			Debug.LogWarning(string.Concat(new object[]
			{
				"Received RPC \"",
				text,
				"\" for viewID ",
				num,
				" but this PhotonView does not exist! Was remote PV.",
				flag2 ? " Owner called." : " Remote called.",
				" By: ",
				senderID,
				" Maybe GO was destroyed but RPC not cleaned up."
			}));
			return;
		}
		else
		{
			if (photonView.prefix != num2)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Received RPC \"",
					text,
					"\" on viewID ",
					num,
					" with a prefix of ",
					num2,
					", our prefix is ",
					photonView.prefix,
					". The RPC has been ignored."
				}));
				return;
			}
			if (string.IsNullOrEmpty(text))
			{
				Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(rpcData));
				return;
			}
			if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
			{
				Debug.Log("Received RPC: " + text);
			}
			if (photonView.group != 0 && !this.allowedReceivingGroups.Contains(photonView.group))
			{
				return;
			}
			Type[] array2 = this.emptyTypeArray;
			if (array.Length != 0)
			{
				array2 = new Type[array.Length];
				int num5 = 0;
				foreach (object obj in array)
				{
					if (obj == null)
					{
						array2[num5] = null;
					}
					else
					{
						array2[num5] = obj.GetType();
					}
					num5++;
				}
			}
			int num6 = 0;
			int num7 = 0;
			if (!PhotonNetwork.UseRpcMonoBehaviourCache || photonView.RpcMonoBehaviours == null || photonView.RpcMonoBehaviours.Length == 0)
			{
				photonView.RefreshRpcMonoBehaviourCache();
			}
			for (int j = 0; j < photonView.RpcMonoBehaviours.Length; j++)
			{
				MonoBehaviour monoBehaviour = photonView.RpcMonoBehaviours[j];
				if (monoBehaviour == null)
				{
					Debug.LogError("ERROR You have missing MonoBehaviours on your gameobjects!");
				}
				else
				{
					Type type = monoBehaviour.GetType();
					List<MethodInfo> list = null;
					if (!this.monoRPCMethodsCache.TryGetValue(type, out list))
					{
						List<MethodInfo> methods = SupportClass.GetMethods(type, this.typePunRPC);
						this.monoRPCMethodsCache[type] = methods;
						list = methods;
					}
					if (list != null)
					{
						for (int k = 0; k < list.Count; k++)
						{
							MethodInfo methodInfo = list[k];
							if (methodInfo.Name.Equals(text))
							{
								num7++;
								ParameterInfo[] cachedParemeters = methodInfo.GetCachedParemeters();
								if (cachedParemeters.Length == array2.Length)
								{
									if (this.CheckTypeMatch(cachedParemeters, array2))
									{
										num6++;
										IEnumerator enumerator = methodInfo.Invoke(monoBehaviour, array) as IEnumerator;
										if (PhotonNetwork.StartRpcsAsCoroutine && enumerator != null)
										{
											monoBehaviour.StartCoroutine(enumerator);
										}
									}
								}
								else if (cachedParemeters.Length == array2.Length + 1)
								{
									if (this.CheckTypeMatch(cachedParemeters, array2) && cachedParemeters[cachedParemeters.Length - 1].ParameterType == this.typePhotonMessageInfo)
									{
										int timestamp = (int)rpcData[this.keyByteTwo];
										PhotonMessageInfo photonMessageInfo = new PhotonMessageInfo(this.GetPlayerWithId(senderID), timestamp, photonView);
										object[] array3 = new object[array.Length + 1];
										array.CopyTo(array3, 0);
										array3[array3.Length - 1] = photonMessageInfo;
										num6++;
										IEnumerator enumerator2 = methodInfo.Invoke(monoBehaviour, array3) as IEnumerator;
										if (PhotonNetwork.StartRpcsAsCoroutine && enumerator2 != null)
										{
											monoBehaviour.StartCoroutine(enumerator2);
										}
									}
								}
								else if (cachedParemeters.Length == 1 && cachedParemeters[0].ParameterType.IsArray)
								{
									num6++;
									IEnumerator enumerator3 = methodInfo.Invoke(monoBehaviour, new object[]
									{
										array
									}) as IEnumerator;
									if (PhotonNetwork.StartRpcsAsCoroutine && enumerator3 != null)
									{
										monoBehaviour.StartCoroutine(enumerator3);
									}
								}
							}
						}
					}
				}
			}
			if (num6 != 1)
			{
				string text2 = string.Empty;
				foreach (Type type2 in array2)
				{
					if (text2 != string.Empty)
					{
						text2 += ", ";
					}
					if (type2 == null)
					{
						text2 += "null";
					}
					else
					{
						text2 += type2.Name;
					}
				}
				if (num6 == 0)
				{
					if (num7 == 0)
					{
						Debug.LogError(string.Concat(new object[]
						{
							"PhotonView with ID ",
							num,
							" has no method \"",
							text,
							"\" marked with the [PunRPC](C#) or @PunRPC(JS) property! Args: ",
							text2
						}));
						return;
					}
					Debug.LogError(string.Concat(new object[]
					{
						"PhotonView with ID ",
						num,
						" has no method \"",
						text,
						"\" that takes ",
						array2.Length,
						" argument(s): ",
						text2
					}));
					return;
				}
				else
				{
					Debug.LogError(string.Concat(new object[]
					{
						"PhotonView with ID ",
						num,
						" has ",
						num6,
						" methods \"",
						text,
						"\" that takes ",
						array2.Length,
						" argument(s): ",
						text2,
						". Should be just one?"
					}));
				}
			}
			return;
		}
	}

	// Token: 0x06000393 RID: 915 RVA: 0x0001798C File Offset: 0x00015B8C
	private bool CheckTypeMatch(ParameterInfo[] methodParameters, Type[] callParameterTypes)
	{
		if (methodParameters.Length < callParameterTypes.Length)
		{
			return false;
		}
		for (int i = 0; i < callParameterTypes.Length; i++)
		{
			Type parameterType = methodParameters[i].ParameterType;
			if (callParameterTypes[i] != null && !parameterType.IsAssignableFrom(callParameterTypes[i]) && (!parameterType.IsEnum || !Enum.GetUnderlyingType(parameterType).IsAssignableFrom(callParameterTypes[i])))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000394 RID: 916 RVA: 0x000179EC File Offset: 0x00015BEC
	internal Hashtable SendInstantiate(string prefabName, Vector3 position, Quaternion rotation, byte group, int[] viewIDs, object[] data, bool isGlobalObject)
	{
		int num = viewIDs[0];
		Hashtable hashtable = new Hashtable();
		hashtable[0] = prefabName;
		if (position != Vector3.zero)
		{
			hashtable[1] = position;
		}
		if (rotation != Quaternion.identity)
		{
			hashtable[2] = rotation;
		}
		if (group != 0)
		{
			hashtable[3] = group;
		}
		if (viewIDs.Length > 1)
		{
			hashtable[4] = viewIDs;
		}
		if (data != null)
		{
			hashtable[5] = data;
		}
		if (this.currentLevelPrefix > 0)
		{
			hashtable[8] = this.currentLevelPrefix;
		}
		hashtable[6] = PhotonNetwork.ServerTimestamp;
		hashtable[7] = num;
		this.OpRaiseEvent(202, hashtable, true, new RaiseEventOptions
		{
			CachingOption = (isGlobalObject ? EventCaching.AddToRoomCacheGlobal : EventCaching.AddToRoomCache)
		});
		return hashtable;
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00017AFC File Offset: 0x00015CFC
	internal GameObject DoInstantiate(Hashtable evData, PhotonPlayer photonPlayer, GameObject resourceGameObject)
	{
		string text = (string)evData[0];
		int timestamp = (int)evData[6];
		int num = (int)evData[7];
		Vector3 position;
		if (evData.ContainsKey(1))
		{
			position = (Vector3)evData[1];
		}
		else
		{
			position = Vector3.zero;
		}
		Quaternion rotation = Quaternion.identity;
		if (evData.ContainsKey(2))
		{
			rotation = (Quaternion)evData[2];
		}
		byte b = 0;
		if (evData.ContainsKey(3))
		{
			b = (byte)evData[3];
		}
		short prefix = 0;
		if (evData.ContainsKey(8))
		{
			prefix = (short)evData[8];
		}
		int[] array;
		if (evData.ContainsKey(4))
		{
			array = (int[])evData[4];
		}
		else
		{
			array = new int[]
			{
				num
			};
		}
		object[] array2;
		if (evData.ContainsKey(5))
		{
			array2 = (object[])evData[5];
		}
		else
		{
			array2 = null;
		}
		if (b != 0 && !this.allowedReceivingGroups.Contains(b))
		{
			return null;
		}
		if (this.ObjectPool != null)
		{
			GameObject gameObject = this.ObjectPool.Instantiate(text, position, rotation);
			PhotonView[] photonViewsInChildren = gameObject.GetPhotonViewsInChildren();
			if (photonViewsInChildren.Length != array.Length)
			{
				throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
			}
			for (int i = 0; i < photonViewsInChildren.Length; i++)
			{
				photonViewsInChildren[i].didAwake = false;
				photonViewsInChildren[i].viewID = 0;
				photonViewsInChildren[i].prefix = (int)prefix;
				photonViewsInChildren[i].instantiationId = num;
				photonViewsInChildren[i].isRuntimeInstantiated = true;
				photonViewsInChildren[i].instantiationDataField = array2;
				photonViewsInChildren[i].didAwake = true;
				photonViewsInChildren[i].viewID = array[i];
			}
			gameObject.SendMessage(NetworkingPeer.OnPhotonInstantiateString, new PhotonMessageInfo(photonPlayer, timestamp, null), SendMessageOptions.DontRequireReceiver);
			return gameObject;
		}
		else
		{
			if (resourceGameObject == null)
			{
				if (!NetworkingPeer.UsePrefabCache || !NetworkingPeer.PrefabCache.TryGetValue(text, out resourceGameObject))
				{
					resourceGameObject = (GameObject)Resources.Load(text, typeof(GameObject));
					if (NetworkingPeer.UsePrefabCache)
					{
						NetworkingPeer.PrefabCache.Add(text, resourceGameObject);
					}
				}
				if (resourceGameObject == null)
				{
					Debug.LogError("PhotonNetwork error: Could not Instantiate the prefab [" + text + "]. Please verify you have this gameobject in a Resources folder.");
					return null;
				}
			}
			PhotonView[] photonViewsInChildren2 = resourceGameObject.GetPhotonViewsInChildren();
			if (photonViewsInChildren2.Length != array.Length)
			{
				throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
			}
			for (int j = 0; j < array.Length; j++)
			{
				photonViewsInChildren2[j].viewID = array[j];
				photonViewsInChildren2[j].prefix = (int)prefix;
				photonViewsInChildren2[j].instantiationId = num;
				photonViewsInChildren2[j].isRuntimeInstantiated = true;
			}
			this.StoreInstantiationData(num, array2);
			GameObject gameObject2 = Object.Instantiate<GameObject>(resourceGameObject, position, rotation);
			for (int k = 0; k < array.Length; k++)
			{
				photonViewsInChildren2[k].viewID = 0;
				photonViewsInChildren2[k].prefix = -1;
				photonViewsInChildren2[k].prefixBackup = -1;
				photonViewsInChildren2[k].instantiationId = -1;
				photonViewsInChildren2[k].isRuntimeInstantiated = false;
			}
			this.RemoveInstantiationData(num);
			gameObject2.SendMessage(NetworkingPeer.OnPhotonInstantiateString, new PhotonMessageInfo(photonPlayer, timestamp, null), SendMessageOptions.DontRequireReceiver);
			return gameObject2;
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00017E54 File Offset: 0x00016054
	private void StoreInstantiationData(int instantiationId, object[] instantiationData)
	{
		this.tempInstantiationData[instantiationId] = instantiationData;
	}

	// Token: 0x06000397 RID: 919 RVA: 0x00017E64 File Offset: 0x00016064
	public object[] FetchInstantiationData(int instantiationId)
	{
		object[] result = null;
		if (instantiationId == 0)
		{
			return null;
		}
		this.tempInstantiationData.TryGetValue(instantiationId, out result);
		return result;
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00017E88 File Offset: 0x00016088
	private void RemoveInstantiationData(int instantiationId)
	{
		this.tempInstantiationData.Remove(instantiationId);
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00017E98 File Offset: 0x00016098
	public void DestroyPlayerObjects(int playerId, bool localOnly)
	{
		if (playerId <= 0)
		{
			Debug.LogError("Failed to Destroy objects of playerId: " + playerId);
			return;
		}
		if (!localOnly)
		{
			this.OpRemoveFromServerInstantiationsOfPlayer(playerId);
			this.OpCleanRpcBuffer(playerId);
			this.SendDestroyOfPlayer(playerId);
		}
		HashSet<GameObject> hashSet = new HashSet<GameObject>();
		foreach (PhotonView photonView in this.photonViewList.Values)
		{
			if (photonView != null && photonView.CreatorActorNr == playerId)
			{
				hashSet.Add(photonView.gameObject);
			}
		}
		foreach (GameObject go in hashSet)
		{
			this.RemoveInstantiatedGO(go, true);
		}
		foreach (PhotonView photonView2 in this.photonViewList.Values)
		{
			if (photonView2.ownerId == playerId)
			{
				photonView2.ownerId = photonView2.CreatorActorNr;
			}
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00017FDC File Offset: 0x000161DC
	public void DestroyAll(bool localOnly)
	{
		if (!localOnly)
		{
			this.OpRemoveCompleteCache();
			this.SendDestroyOfAll();
		}
		this.LocalCleanupAnythingInstantiated(true);
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00017FF4 File Offset: 0x000161F4
	protected internal void RemoveInstantiatedGO(GameObject go, bool localOnly)
	{
		if (go == null)
		{
			Debug.LogError("Failed to 'network-remove' GameObject because it's null.");
			return;
		}
		PhotonView[] componentsInChildren = go.GetComponentsInChildren<PhotonView>(true);
		if (componentsInChildren == null || componentsInChildren.Length == 0)
		{
			Debug.LogError("Failed to 'network-remove' GameObject because has no PhotonView components: " + go);
			return;
		}
		PhotonView photonView = componentsInChildren[0];
		int creatorActorNr = photonView.CreatorActorNr;
		int instantiationId = photonView.instantiationId;
		if (!localOnly)
		{
			if (!photonView.isMine)
			{
				Debug.LogError("Failed to 'network-remove' GameObject. Client is neither owner nor masterClient taking over for owner who left: " + photonView);
				return;
			}
			if (instantiationId < 1)
			{
				Debug.LogError("Failed to 'network-remove' GameObject because it is missing a valid InstantiationId on view: " + photonView + ". Not Destroying GameObject or PhotonViews!");
				return;
			}
		}
		if (!localOnly)
		{
			this.ServerCleanInstantiateAndDestroy(instantiationId, creatorActorNr, photonView.isRuntimeInstantiated);
		}
		for (int i = componentsInChildren.Length - 1; i >= 0; i--)
		{
			PhotonView photonView2 = componentsInChildren[i];
			if (!(photonView2 == null))
			{
				if (photonView2.instantiationId >= 1)
				{
					this.LocalCleanPhotonView(photonView2);
				}
				if (!localOnly)
				{
					this.OpCleanRpcBuffer(photonView2);
				}
			}
		}
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
		{
			Debug.Log("Network destroy Instantiated GO: " + go.name);
		}
		if (this.ObjectPool != null)
		{
			PhotonView[] photonViewsInChildren = go.GetPhotonViewsInChildren();
			for (int j = 0; j < photonViewsInChildren.Length; j++)
			{
				photonViewsInChildren[j].viewID = 0;
			}
			this.ObjectPool.Destroy(go);
			return;
		}
		Object.Destroy(go);
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00018130 File Offset: 0x00016330
	private void ServerCleanInstantiateAndDestroy(int instantiateId, int creatorId, bool isRuntimeInstantiated)
	{
		Hashtable hashtable = new Hashtable();
		hashtable[7] = instantiateId;
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache,
			TargetActors = new int[]
			{
				creatorId
			}
		};
		this.OpRaiseEvent(202, hashtable, true, raiseEventOptions);
		Hashtable hashtable2 = new Hashtable();
		hashtable2[0] = instantiateId;
		raiseEventOptions = null;
		if (!isRuntimeInstantiated)
		{
			raiseEventOptions = new RaiseEventOptions();
			raiseEventOptions.CachingOption = EventCaching.AddToRoomCacheGlobal;
			Debug.Log("Destroying GO as global. ID: " + instantiateId);
		}
		this.OpRaiseEvent(204, hashtable2, true, raiseEventOptions);
	}

	// Token: 0x0600039D RID: 925 RVA: 0x000181D4 File Offset: 0x000163D4
	private void SendDestroyOfPlayer(int actorNr)
	{
		Hashtable hashtable = new Hashtable();
		hashtable[0] = actorNr;
		this.OpRaiseEvent(207, hashtable, true, null);
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00018208 File Offset: 0x00016408
	private void SendDestroyOfAll()
	{
		Hashtable hashtable = new Hashtable();
		hashtable[0] = -1;
		this.OpRaiseEvent(207, hashtable, true, null);
	}

	// Token: 0x0600039F RID: 927 RVA: 0x0001823C File Offset: 0x0001643C
	private void OpRemoveFromServerInstantiationsOfPlayer(int actorNr)
	{
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache,
			TargetActors = new int[]
			{
				actorNr
			}
		};
		this.OpRaiseEvent(202, null, true, raiseEventOptions);
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00018277 File Offset: 0x00016477
	protected internal void RequestOwnership(int viewID, int fromOwner)
	{
		this.OpRaiseEvent(209, new int[]
		{
			viewID,
			fromOwner
		}, true, new RaiseEventOptions
		{
			Receivers = ReceiverGroup.All
		});
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x000182A0 File Offset: 0x000164A0
	protected internal void TransferOwnership(int viewID, int playerID)
	{
		Debug.Log(string.Concat(new object[]
		{
			"TransferOwnership() view ",
			viewID,
			" to: ",
			playerID,
			" Time: ",
			Environment.TickCount % 1000
		}));
		this.OpRaiseEvent(210, new int[]
		{
			viewID,
			playerID
		}, true, new RaiseEventOptions
		{
			Receivers = ReceiverGroup.All
		});
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00018321 File Offset: 0x00016521
	public bool LocalCleanPhotonView(PhotonView view)
	{
		view.removedFromLocalViewList = true;
		return this.photonViewList.Remove(view.viewID);
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x0001833C File Offset: 0x0001653C
	public PhotonView GetPhotonView(int viewID)
	{
		PhotonView photonView = null;
		this.photonViewList.TryGetValue(viewID, out photonView);
		if (photonView == null)
		{
			foreach (PhotonView photonView2 in Object.FindObjectsOfType(typeof(PhotonView)) as PhotonView[])
			{
				if (photonView2.viewID == viewID)
				{
					if (photonView2.didAwake)
					{
						Debug.LogWarning("Had to lookup view that wasn't in photonViewList: " + photonView2);
					}
					return photonView2;
				}
			}
		}
		return photonView;
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x000183B0 File Offset: 0x000165B0
	public void RegisterPhotonView(PhotonView netView)
	{
		if (!Application.isPlaying)
		{
			this.photonViewList = new Dictionary<int, PhotonView>();
			return;
		}
		if (netView.viewID == 0)
		{
			Debug.Log("PhotonView register is ignored, because viewID is 0. No id assigned yet to: " + netView);
			return;
		}
		PhotonView photonView = null;
		if (this.photonViewList.TryGetValue(netView.viewID, out photonView))
		{
			if (!(netView != photonView))
			{
				return;
			}
			Debug.LogError(string.Format("PhotonView ID duplicate found: {0}. New: {1} old: {2}. Maybe one wasn't destroyed on scene load?! Check for 'DontDestroyOnLoad'. Destroying old entry, adding new.", netView.viewID, netView, photonView));
			this.RemoveInstantiatedGO(photonView.gameObject, true);
		}
		this.photonViewList.Add(netView.viewID, netView);
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
		{
			Debug.Log("Registered PhotonView: " + netView.viewID);
		}
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x0001846C File Offset: 0x0001666C
	public void OpCleanRpcBuffer(int actorNumber)
	{
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache,
			TargetActors = new int[]
			{
				actorNumber
			}
		};
		this.OpRaiseEvent(200, null, true, raiseEventOptions);
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x000184A8 File Offset: 0x000166A8
	public void OpRemoveCompleteCacheOfPlayer(int actorNumber)
	{
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache,
			TargetActors = new int[]
			{
				actorNumber
			}
		};
		this.OpRaiseEvent(0, null, true, raiseEventOptions);
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x000184E0 File Offset: 0x000166E0
	public void OpRemoveCompleteCache()
	{
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache,
			Receivers = ReceiverGroup.MasterClient
		};
		this.OpRaiseEvent(0, null, true, raiseEventOptions);
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x0001850C File Offset: 0x0001670C
	private void RemoveCacheOfLeftPlayers()
	{
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		dictionary[244] = 0;
		dictionary[247] = 7;
		this.SendOperation(253, dictionary, SendOptions.SendReliable);
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x00018554 File Offset: 0x00016754
	public void CleanRpcBufferIfMine(PhotonView view)
	{
		if (view.ownerId != this.LocalPlayer.ID && !this.LocalPlayer.IsMasterClient)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Cannot remove cached RPCs on a PhotonView thats not ours! ",
				view.owner,
				" scene: ",
				view.isSceneView.ToString()
			}));
			return;
		}
		this.OpCleanRpcBuffer(view);
	}

	// Token: 0x060003AA RID: 938 RVA: 0x000185C4 File Offset: 0x000167C4
	public void OpCleanRpcBuffer(PhotonView view)
	{
		Hashtable hashtable = new Hashtable();
		hashtable[0] = view.viewID;
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache
		};
		this.OpRaiseEvent(200, hashtable, true, raiseEventOptions);
	}

	// Token: 0x060003AB RID: 939 RVA: 0x0001860C File Offset: 0x0001680C
	public void RemoveRPCsInGroup(int group)
	{
		foreach (PhotonView photonView in this.photonViewList.Values)
		{
			if ((int)photonView.group == group)
			{
				this.CleanRpcBufferIfMine(photonView);
			}
		}
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00018670 File Offset: 0x00016870
	public void SetLevelPrefix(short prefix)
	{
		this.currentLevelPrefix = prefix;
	}

	// Token: 0x060003AD RID: 941 RVA: 0x0001867C File Offset: 0x0001687C
	internal void RPC(PhotonView view, string methodName, PhotonTargets target, PhotonPlayer player, bool encrypt, params object[] parameters)
	{
		if (this.blockSendingGroups.Contains(view.group))
		{
			return;
		}
		if (view.viewID < 1)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Illegal view ID:",
				view.viewID,
				" method: ",
				methodName,
				" GO:",
				view.gameObject.name
			}));
		}
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Sending RPC \"",
				methodName,
				"\" to target: ",
				target,
				" or player:",
				player,
				"."
			}));
		}
		this.reusedRpcEvent.Clear();
		Hashtable hashtable = this.reusedRpcEvent;
		hashtable[this.keyByteZero] = view.viewID;
		if (view.prefix > 0)
		{
			hashtable[this.keyByteOne] = (short)view.prefix;
		}
		hashtable[this.keyByteTwo] = PhotonNetwork.ServerTimestamp;
		int num = 0;
		if (this.rpcShortcuts.TryGetValue(methodName, out num))
		{
			hashtable[this.keyByteFive] = (byte)num;
		}
		else
		{
			hashtable[this.keyByteThree] = methodName;
		}
		if (parameters != null && parameters.Length != 0)
		{
			hashtable[this.keyByteFour] = parameters;
		}
		if (player != null)
		{
			if (this.LocalPlayer.ID == player.ID)
			{
				this.ExecuteRpc(hashtable, player.ID);
				return;
			}
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions
			{
				TargetActors = new int[]
				{
					player.ID
				},
				Encrypt = encrypt
			};
			this.OpRaiseEvent(200, hashtable, true, raiseEventOptions);
			return;
		}
		else
		{
			if (target == PhotonTargets.All)
			{
				RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions
				{
					InterestGroup = view.group,
					Encrypt = encrypt
				};
				this.OpRaiseEvent(200, hashtable, true, raiseEventOptions2);
				this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
				return;
			}
			if (target == PhotonTargets.Others)
			{
				RaiseEventOptions raiseEventOptions3 = new RaiseEventOptions
				{
					InterestGroup = view.group,
					Encrypt = encrypt
				};
				this.OpRaiseEvent(200, hashtable, true, raiseEventOptions3);
				return;
			}
			if (target == PhotonTargets.AllBuffered)
			{
				RaiseEventOptions raiseEventOptions4 = new RaiseEventOptions
				{
					CachingOption = EventCaching.AddToRoomCache,
					Encrypt = encrypt
				};
				this.OpRaiseEvent(200, hashtable, true, raiseEventOptions4);
				this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
				return;
			}
			if (target == PhotonTargets.OthersBuffered)
			{
				RaiseEventOptions raiseEventOptions5 = new RaiseEventOptions
				{
					CachingOption = EventCaching.AddToRoomCache,
					Encrypt = encrypt
				};
				this.OpRaiseEvent(200, hashtable, true, raiseEventOptions5);
				return;
			}
			if (target != PhotonTargets.MasterClient)
			{
				if (target == PhotonTargets.AllViaServer)
				{
					RaiseEventOptions raiseEventOptions6 = new RaiseEventOptions
					{
						InterestGroup = view.group,
						Receivers = ReceiverGroup.All,
						Encrypt = encrypt
					};
					this.OpRaiseEvent(200, hashtable, true, raiseEventOptions6);
					if (PhotonNetwork.offlineMode)
					{
						this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
						return;
					}
				}
				else if (target == PhotonTargets.AllBufferedViaServer)
				{
					RaiseEventOptions raiseEventOptions7 = new RaiseEventOptions
					{
						InterestGroup = view.group,
						Receivers = ReceiverGroup.All,
						CachingOption = EventCaching.AddToRoomCache,
						Encrypt = encrypt
					};
					this.OpRaiseEvent(200, hashtable, true, raiseEventOptions7);
					if (PhotonNetwork.offlineMode)
					{
						this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
						return;
					}
				}
				else
				{
					Debug.LogError("Unsupported target enum: " + target);
				}
				return;
			}
			if (this.mMasterClientId == this.LocalPlayer.ID)
			{
				this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
				return;
			}
			RaiseEventOptions raiseEventOptions8 = new RaiseEventOptions
			{
				Receivers = ReceiverGroup.MasterClient,
				Encrypt = encrypt
			};
			this.OpRaiseEvent(200, hashtable, true, raiseEventOptions8);
			return;
		}
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00018A28 File Offset: 0x00016C28
	public void SetInterestGroups(byte[] disableGroups, byte[] enableGroups)
	{
		if (disableGroups != null)
		{
			if (disableGroups.Length == 0)
			{
				this.allowedReceivingGroups.Clear();
			}
			else
			{
				foreach (byte b in disableGroups)
				{
					if (b <= 0)
					{
						Debug.LogError("Error: PhotonNetwork.SetInterestGroups was called with an illegal group number: " + b + ". The group number should be at least 1.");
					}
					else if (this.allowedReceivingGroups.Contains(b))
					{
						this.allowedReceivingGroups.Remove(b);
					}
				}
			}
		}
		if (enableGroups != null)
		{
			if (enableGroups.Length == 0)
			{
				for (byte b2 = 0; b2 < 255; b2 += 1)
				{
					this.allowedReceivingGroups.Add(b2);
				}
				this.allowedReceivingGroups.Add(byte.MaxValue);
			}
			else
			{
				foreach (byte b3 in enableGroups)
				{
					if (b3 <= 0)
					{
						Debug.LogError("Error: PhotonNetwork.SetInterestGroups was called with an illegal group number: " + b3 + ". The group number should be at least 1.");
					}
					else
					{
						this.allowedReceivingGroups.Add(b3);
					}
				}
			}
		}
		if (!PhotonNetwork.offlineMode)
		{
			this.OpChangeGroups(disableGroups, enableGroups);
		}
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00018B21 File Offset: 0x00016D21
	public void SetSendingEnabled(byte group, bool enabled)
	{
		if (!enabled)
		{
			this.blockSendingGroups.Add(group);
			return;
		}
		this.blockSendingGroups.Remove(group);
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00018B44 File Offset: 0x00016D44
	public void SetSendingEnabled(byte[] disableGroups, byte[] enableGroups)
	{
		if (disableGroups != null)
		{
			foreach (byte item in disableGroups)
			{
				this.blockSendingGroups.Add(item);
			}
		}
		if (enableGroups != null)
		{
			foreach (byte item2 in enableGroups)
			{
				this.blockSendingGroups.Remove(item2);
			}
		}
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00018B98 File Offset: 0x00016D98
	public void NewSceneLoaded()
	{
		if (this.loadingLevelAndPausedNetwork)
		{
			this.loadingLevelAndPausedNetwork = false;
			PhotonNetwork.isMessageQueueRunning = true;
		}
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, PhotonView> keyValuePair in this.photonViewList)
		{
			if (keyValuePair.Value == null)
			{
				list.Add(keyValuePair.Key);
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			int key = list[i];
			this.photonViewList.Remove(key);
		}
		if (list.Count > 0 && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
		{
			Debug.Log("New level loaded. Removed " + list.Count + " scene view IDs from last level.");
		}
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00018C74 File Offset: 0x00016E74
	public void RunViewUpdate()
	{
		if (!PhotonNetwork.connected || PhotonNetwork.offlineMode || this.mActors == null)
		{
			return;
		}
		if (PhotonNetwork.inRoom && this._AsyncLevelLoadingOperation != null && this._AsyncLevelLoadingOperation.isDone)
		{
			this._AsyncLevelLoadingOperation = null;
			this.LoadLevelIfSynced();
		}
		if (this.mActors.Count <= 1)
		{
			return;
		}
		int num = 0;
		this.options.Reset();
		List<int> list = null;
		foreach (KeyValuePair<int, PhotonView> keyValuePair in this.photonViewList)
		{
			PhotonView value = keyValuePair.Value;
			if (value == null)
			{
				string format = "PhotonView with ID {0} wasn't properly unregistered! Please report this case to developer@photonengine.com";
				Dictionary<int, PhotonView>.Enumerator enumerator;
				keyValuePair = enumerator.Current;
				Debug.LogError(string.Format(format, keyValuePair.Key));
				if (list == null)
				{
					list = new List<int>(4);
				}
				List<int> list2 = list;
				keyValuePair = enumerator.Current;
				list2.Add(keyValuePair.Key);
			}
			else if (value.synchronization != ViewSynchronization.Off && value.isMine && value.gameObject.activeInHierarchy && !this.blockSendingGroups.Contains(value.group))
			{
				object[] array = this.OnSerializeWrite(value);
				if (array != null)
				{
					if (value.synchronization == ViewSynchronization.ReliableDeltaCompressed || value.mixedModeIsReliable)
					{
						Hashtable hashtable = null;
						if (!this.dataPerGroupReliable.TryGetValue((int)value.group, out hashtable))
						{
							hashtable = new Hashtable(NetworkingPeer.ObjectsInOneUpdate);
							this.dataPerGroupReliable[(int)value.group] = hashtable;
						}
						hashtable.Add((byte)(hashtable.Count + 10), array);
						num++;
						if (hashtable.Count >= NetworkingPeer.ObjectsInOneUpdate)
						{
							num -= hashtable.Count;
							this.options.InterestGroup = value.group;
							hashtable[0] = PhotonNetwork.ServerTimestamp;
							if (this.currentLevelPrefix >= 0)
							{
								hashtable[1] = this.currentLevelPrefix;
							}
							this.OpRaiseEvent(206, hashtable, true, this.options);
							hashtable.Clear();
						}
					}
					else
					{
						Hashtable hashtable2 = null;
						if (!this.dataPerGroupUnreliable.TryGetValue((int)value.group, out hashtable2))
						{
							hashtable2 = new Hashtable(NetworkingPeer.ObjectsInOneUpdate);
							this.dataPerGroupUnreliable[(int)value.group] = hashtable2;
						}
						hashtable2.Add((byte)(hashtable2.Count + 10), array);
						num++;
						if (hashtable2.Count >= NetworkingPeer.ObjectsInOneUpdate)
						{
							num -= hashtable2.Count;
							this.options.InterestGroup = value.group;
							hashtable2[0] = PhotonNetwork.ServerTimestamp;
							if (this.currentLevelPrefix >= 0)
							{
								hashtable2[1] = this.currentLevelPrefix;
							}
							this.OpRaiseEvent(201, hashtable2, false, this.options);
							hashtable2.Clear();
						}
					}
				}
			}
		}
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				this.photonViewList.Remove(list[i]);
				i++;
			}
		}
		if (num == 0)
		{
			return;
		}
		foreach (int num2 in this.dataPerGroupReliable.Keys)
		{
			this.options.InterestGroup = (byte)num2;
			Hashtable hashtable3 = this.dataPerGroupReliable[num2];
			if (hashtable3.Count != 0)
			{
				hashtable3[0] = PhotonNetwork.ServerTimestamp;
				if (this.currentLevelPrefix >= 0)
				{
					hashtable3[1] = this.currentLevelPrefix;
				}
				this.OpRaiseEvent(206, hashtable3, true, this.options);
				hashtable3.Clear();
			}
		}
		foreach (int num3 in this.dataPerGroupUnreliable.Keys)
		{
			this.options.InterestGroup = (byte)num3;
			Hashtable hashtable4 = this.dataPerGroupUnreliable[num3];
			if (hashtable4.Count != 0)
			{
				hashtable4[0] = PhotonNetwork.ServerTimestamp;
				if (this.currentLevelPrefix >= 0)
				{
					hashtable4[1] = this.currentLevelPrefix;
				}
				this.OpRaiseEvent(201, hashtable4, false, this.options);
				hashtable4.Clear();
			}
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00019128 File Offset: 0x00017328
	private object[] OnSerializeWrite(PhotonView view)
	{
		if (view.synchronization == ViewSynchronization.Off)
		{
			return null;
		}
		PhotonMessageInfo info = new PhotonMessageInfo(this.LocalPlayer, PhotonNetwork.ServerTimestamp, view);
		this.pStream.ResetWriteStream();
		this.pStream.SendNext(null);
		this.pStream.SendNext(null);
		this.pStream.SendNext(null);
		view.SerializeView(this.pStream, info);
		if (this.pStream.Count <= 3)
		{
			return null;
		}
		object[] array = this.pStream.ToArray();
		array[0] = view.viewID;
		array[1] = false;
		array[2] = null;
		if (view.synchronization == ViewSynchronization.Unreliable)
		{
			return array;
		}
		if (view.synchronization == ViewSynchronization.UnreliableOnChange)
		{
			if (this.AlmostEquals(array, view.lastOnSerializeDataSent))
			{
				if (view.mixedModeIsReliable)
				{
					return null;
				}
				view.mixedModeIsReliable = true;
				view.lastOnSerializeDataSent = array;
			}
			else
			{
				view.mixedModeIsReliable = false;
				view.lastOnSerializeDataSent = array;
			}
			return array;
		}
		if (view.synchronization == ViewSynchronization.ReliableDeltaCompressed)
		{
			object[] result = this.DeltaCompressionWrite(view.lastOnSerializeDataSent, array);
			view.lastOnSerializeDataSent = array;
			return result;
		}
		return null;
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00019234 File Offset: 0x00017434
	private void OnSerializeRead(object[] data, PhotonPlayer sender, int networkTime, short correctPrefix)
	{
		int num = (int)data[0];
		PhotonView photonView = this.GetPhotonView(num);
		if (photonView == null)
		{
			Debug.LogWarning(string.Concat(new object[]
			{
				"Received OnSerialization for view ID ",
				num,
				". We have no such PhotonView! Ignored this if you're leaving a room. State: ",
				this.State
			}));
			return;
		}
		if (photonView.prefix > 0 && (int)correctPrefix != photonView.prefix)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Received OnSerialization for view ID ",
				num,
				" with prefix ",
				correctPrefix,
				". Our prefix is ",
				photonView.prefix
			}));
			return;
		}
		if (photonView.group != 0 && !this.allowedReceivingGroups.Contains(photonView.group))
		{
			return;
		}
		if (photonView.synchronization == ViewSynchronization.ReliableDeltaCompressed)
		{
			object[] array = this.DeltaCompressionRead(photonView.lastOnSerializeDataReceived, data);
			if (array == null)
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
				{
					Debug.Log(string.Concat(new object[]
					{
						"Skipping packet for ",
						photonView.name,
						" [",
						photonView.viewID,
						"] as we haven't received a full packet for delta compression yet. This is OK if it happens for the first few frames after joining a game."
					}));
				}
				return;
			}
			photonView.lastOnSerializeDataReceived = array;
			data = array;
		}
		if (sender.ID != photonView.ownerId && (!photonView.OwnerShipWasTransfered || photonView.ownerId == 0) && photonView.currentMasterID == -1)
		{
			photonView.ownerId = sender.ID;
		}
		this.readStream.SetReadStream(data, 3);
		PhotonMessageInfo info = new PhotonMessageInfo(sender, networkTime, photonView);
		photonView.DeserializeView(this.readStream, info);
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x000193D0 File Offset: 0x000175D0
	private object[] DeltaCompressionWrite(object[] previousContent, object[] currentContent)
	{
		if (currentContent == null || previousContent == null || previousContent.Length != currentContent.Length)
		{
			return currentContent;
		}
		if (currentContent.Length <= 3)
		{
			return null;
		}
		previousContent[1] = false;
		int num = 0;
		Queue<int> queue = null;
		for (int i = 3; i < currentContent.Length; i++)
		{
			object obj = currentContent[i];
			object two = previousContent[i];
			if (this.AlmostEquals(obj, two))
			{
				num++;
				previousContent[i] = null;
			}
			else
			{
				previousContent[i] = obj;
				if (obj == null)
				{
					if (queue == null)
					{
						queue = new Queue<int>(currentContent.Length);
					}
					queue.Enqueue(i);
				}
			}
		}
		if (num > 0)
		{
			if (num == currentContent.Length - 3)
			{
				return null;
			}
			previousContent[1] = true;
			if (queue != null)
			{
				previousContent[2] = queue.ToArray();
			}
		}
		previousContent[0] = currentContent[0];
		return previousContent;
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00019478 File Offset: 0x00017678
	private object[] DeltaCompressionRead(object[] lastOnSerializeDataReceived, object[] incomingData)
	{
		if (!(bool)incomingData[1])
		{
			return incomingData;
		}
		if (lastOnSerializeDataReceived == null)
		{
			return null;
		}
		int[] array = incomingData[2] as int[];
		for (int i = 3; i < incomingData.Length; i++)
		{
			if ((array == null || !array.Contains(i)) && incomingData[i] == null)
			{
				object obj = lastOnSerializeDataReceived[i];
				incomingData[i] = obj;
			}
		}
		return incomingData;
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x000194C8 File Offset: 0x000176C8
	private bool AlmostEquals(object[] lastData, object[] currentContent)
	{
		if (lastData == null && currentContent == null)
		{
			return true;
		}
		if (lastData == null || currentContent == null || lastData.Length != currentContent.Length)
		{
			return false;
		}
		for (int i = 0; i < currentContent.Length; i++)
		{
			object one = currentContent[i];
			object two = lastData[i];
			if (!this.AlmostEquals(one, two))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00019510 File Offset: 0x00017710
	private bool AlmostEquals(object one, object two)
	{
		if (one == null || two == null)
		{
			return one == null && two == null;
		}
		if (!one.Equals(two))
		{
			if (one is Vector3)
			{
				Vector3 target = (Vector3)one;
				Vector3 second = (Vector3)two;
				if (target.AlmostEquals(second, PhotonNetwork.precisionForVectorSynchronization))
				{
					return true;
				}
			}
			else if (one is Vector2)
			{
				Vector2 target2 = (Vector2)one;
				Vector2 second2 = (Vector2)two;
				if (target2.AlmostEquals(second2, PhotonNetwork.precisionForVectorSynchronization))
				{
					return true;
				}
			}
			else if (one is Quaternion)
			{
				Quaternion target3 = (Quaternion)one;
				Quaternion second3 = (Quaternion)two;
				if (target3.AlmostEquals(second3, PhotonNetwork.precisionForQuaternionSynchronization))
				{
					return true;
				}
			}
			else if (one is float)
			{
				float target4 = (float)one;
				float second4 = (float)two;
				if (target4.AlmostEquals(second4, PhotonNetwork.precisionForFloatSynchronization))
				{
					return true;
				}
			}
			return false;
		}
		return true;
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x000195CC File Offset: 0x000177CC
	protected internal static bool GetMethod(MonoBehaviour monob, string methodType, out MethodInfo mi)
	{
		mi = null;
		if (monob == null || string.IsNullOrEmpty(methodType))
		{
			return false;
		}
		List<MethodInfo> methods = SupportClass.GetMethods(monob.GetType(), null);
		for (int i = 0; i < methods.Count; i++)
		{
			MethodInfo methodInfo = methods[i];
			if (methodInfo.Name.Equals(methodType))
			{
				mi = methodInfo;
				return true;
			}
		}
		return false;
	}

	// Token: 0x060003BA RID: 954 RVA: 0x0001962C File Offset: 0x0001782C
	protected internal void LoadLevelIfSynced()
	{
		if (!PhotonNetwork.automaticallySyncScene || PhotonNetwork.isMasterClient || PhotonNetwork.room == null)
		{
			return;
		}
		if (this._AsyncLevelLoadingOperation != null)
		{
			if (!this._AsyncLevelLoadingOperation.isDone)
			{
				return;
			}
			this._AsyncLevelLoadingOperation = null;
		}
		if (!PhotonNetwork.room.CustomProperties.ContainsKey("curScn"))
		{
			return;
		}
		bool flag = PhotonNetwork.room.CustomProperties.ContainsKey("curScnLa");
		object obj = PhotonNetwork.room.CustomProperties["curScn"];
		if (obj is int)
		{
			if (SceneManagerHelper.ActiveSceneBuildIndex != (int)obj)
			{
				if (flag)
				{
					this._AsyncLevelLoadingOperation = PhotonNetwork.LoadLevelAsync((int)obj);
					return;
				}
				PhotonNetwork.LoadLevel((int)obj);
				return;
			}
		}
		else if (obj is string && SceneManagerHelper.ActiveSceneName != (string)obj)
		{
			if (flag)
			{
				this._AsyncLevelLoadingOperation = PhotonNetwork.LoadLevelAsync((string)obj);
				return;
			}
			PhotonNetwork.LoadLevel((string)obj);
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00019720 File Offset: 0x00017920
	protected internal void SetLevelInPropsIfSynced(object levelId, bool initiatingCall, bool asyncLoading = false)
	{
		if (!PhotonNetwork.automaticallySyncScene || !PhotonNetwork.isMasterClient || PhotonNetwork.room == null)
		{
			return;
		}
		if (levelId == null)
		{
			Debug.LogError("Parameter levelId can't be null!");
			return;
		}
		if (!asyncLoading && PhotonNetwork.room.CustomProperties.ContainsKey("curScn"))
		{
			object obj = PhotonNetwork.room.CustomProperties["curScn"];
			if (obj is int && SceneManagerHelper.ActiveSceneBuildIndex == (int)obj)
			{
				this.SendLevelReloadEvent();
				return;
			}
			if (obj is string && SceneManagerHelper.ActiveSceneName != null && SceneManagerHelper.ActiveSceneName.Equals((string)obj))
			{
				bool flag = false;
				if (!this.IsReloadingLevel)
				{
					if (levelId is int)
					{
						flag = ((int)levelId == SceneManagerHelper.ActiveSceneBuildIndex);
					}
					else if (levelId is string)
					{
						flag = SceneManagerHelper.ActiveSceneName.Equals((string)levelId);
					}
				}
				if (initiatingCall && this.IsReloadingLevel)
				{
					flag = false;
				}
				if (flag)
				{
					this.SendLevelReloadEvent();
				}
				return;
			}
		}
		Hashtable hashtable = new Hashtable();
		if (levelId is int)
		{
			hashtable["curScn"] = (int)levelId;
		}
		else if (levelId is string)
		{
			hashtable["curScn"] = (string)levelId;
		}
		else
		{
			Debug.LogError("Parameter levelId must be int or string!");
		}
		if (asyncLoading)
		{
			hashtable["curScnLa"] = true;
		}
		PhotonNetwork.room.SetCustomProperties(hashtable, null, false);
		this.SendOutgoingCommands();
	}

	// Token: 0x060003BC RID: 956 RVA: 0x00019887 File Offset: 0x00017A87
	private void SendLevelReloadEvent()
	{
		this.IsReloadingLevel = true;
		if (PhotonNetwork.inRoom)
		{
			this.OpRaiseEvent(212, this.AsynchLevelLoadCall, true, this._levelReloadEventOptions);
		}
	}

	// Token: 0x060003BD RID: 957 RVA: 0x000198B5 File Offset: 0x00017AB5
	public void SetApp(string appId, string gameVersion)
	{
		this.AppId = appId.Trim();
		if (!string.IsNullOrEmpty(gameVersion))
		{
			PhotonNetwork.gameVersion = gameVersion.Trim();
		}
	}

	// Token: 0x060003BE RID: 958 RVA: 0x000198D8 File Offset: 0x00017AD8
	public bool WebRpc(string uriPath, object parameters)
	{
		return this.SendOperation(219, new Dictionary<byte, object>
		{
			{
				209,
				uriPath
			},
			{
				208,
				parameters
			}
		}, SendOptions.SendReliable);
	}

	// Token: 0x04000478 RID: 1144
	protected internal string AppId;

	// Token: 0x0400047A RID: 1146
	private string tokenCache;

	// Token: 0x0400047B RID: 1147
	public AuthModeOption AuthMode;

	// Token: 0x0400047C RID: 1148
	public EncryptionMode EncryptionMode;

	// Token: 0x0400047E RID: 1150
	public const string NameServerHost = "ns.exitgames.com";

	// Token: 0x0400047F RID: 1151
	public const string NameServerHttp = "http://ns.exitgamescloud.com:80/photon/n";

	// Token: 0x04000480 RID: 1152
	private static readonly Dictionary<ConnectionProtocol, int> ProtocolToNameServerPort = new Dictionary<ConnectionProtocol, int>
	{
		{
			ConnectionProtocol.Udp,
			5058
		},
		{
			ConnectionProtocol.Tcp,
			4533
		},
		{
			ConnectionProtocol.WebSocket,
			9093
		},
		{
			ConnectionProtocol.WebSocketSecure,
			19093
		}
	};

	// Token: 0x04000485 RID: 1157
	public bool IsInitialConnect;

	// Token: 0x04000486 RID: 1158
	public bool insideLobby;

	// Token: 0x04000488 RID: 1160
	protected internal List<TypedLobbyInfo> LobbyStatistics = new List<TypedLobbyInfo>();

	// Token: 0x04000489 RID: 1161
	public Dictionary<string, RoomInfo> mGameList = new Dictionary<string, RoomInfo>();

	// Token: 0x0400048A RID: 1162
	public RoomInfo[] mGameListCopy = new RoomInfo[0];

	// Token: 0x0400048B RID: 1163
	private string playername = "";

	// Token: 0x0400048C RID: 1164
	private bool mPlayernameHasToBeUpdated;

	// Token: 0x0400048D RID: 1165
	private Room currentRoom;

	// Token: 0x04000492 RID: 1170
	private JoinType lastJoinType;

	// Token: 0x04000493 RID: 1171
	protected internal EnterRoomParams enterRoomParamsCache;

	// Token: 0x04000494 RID: 1172
	private bool didAuthenticate;

	// Token: 0x04000495 RID: 1173
	private string[] friendListRequested;

	// Token: 0x04000496 RID: 1174
	private int friendListTimestamp;

	// Token: 0x04000497 RID: 1175
	private bool isFetchingFriendList;

	// Token: 0x0400049A RID: 1178
	private string cloudCluster;

	// Token: 0x0400049B RID: 1179
	public string CurrentCluster;

	// Token: 0x0400049C RID: 1180
	public Dictionary<int, PhotonPlayer> mActors = new Dictionary<int, PhotonPlayer>();

	// Token: 0x0400049D RID: 1181
	public PhotonPlayer[] mOtherPlayerListCopy = new PhotonPlayer[0];

	// Token: 0x0400049E RID: 1182
	public PhotonPlayer[] mPlayerListCopy = new PhotonPlayer[0];

	// Token: 0x0400049F RID: 1183
	public bool hasSwitchedMC;

	// Token: 0x040004A0 RID: 1184
	private HashSet<byte> allowedReceivingGroups = new HashSet<byte>();

	// Token: 0x040004A1 RID: 1185
	private HashSet<byte> blockSendingGroups = new HashSet<byte>();

	// Token: 0x040004A2 RID: 1186
	protected internal Dictionary<int, PhotonView> photonViewList = new Dictionary<int, PhotonView>();

	// Token: 0x040004A3 RID: 1187
	private readonly PhotonStream readStream = new PhotonStream(false, null);

	// Token: 0x040004A4 RID: 1188
	private readonly PhotonStream pStream = new PhotonStream(true, null);

	// Token: 0x040004A5 RID: 1189
	private readonly Dictionary<int, Hashtable> dataPerGroupReliable = new Dictionary<int, Hashtable>();

	// Token: 0x040004A6 RID: 1190
	private readonly Dictionary<int, Hashtable> dataPerGroupUnreliable = new Dictionary<int, Hashtable>();

	// Token: 0x040004A7 RID: 1191
	protected internal short currentLevelPrefix;

	// Token: 0x040004A8 RID: 1192
	protected internal bool loadingLevelAndPausedNetwork;

	// Token: 0x040004A9 RID: 1193
	protected internal const string CurrentSceneProperty = "curScn";

	// Token: 0x040004AA RID: 1194
	protected internal const string CurrentScenePropertyLoadAsync = "curScnLa";

	// Token: 0x040004AB RID: 1195
	public static bool UsePrefabCache = true;

	// Token: 0x040004AC RID: 1196
	internal IPunPrefabPool ObjectPool;

	// Token: 0x040004AD RID: 1197
	public static Dictionary<string, GameObject> PrefabCache = new Dictionary<string, GameObject>();

	// Token: 0x040004AE RID: 1198
	private Dictionary<Type, List<MethodInfo>> monoRPCMethodsCache = new Dictionary<Type, List<MethodInfo>>();

	// Token: 0x040004AF RID: 1199
	private readonly Dictionary<string, int> rpcShortcuts;

	// Token: 0x040004B0 RID: 1200
	private static readonly string OnPhotonInstantiateString = PhotonNetworkingMessage.OnPhotonInstantiate.ToString();

	// Token: 0x040004B1 RID: 1201
	private string cachedServerAddress;

	// Token: 0x040004B2 RID: 1202
	private string cachedApplicationName;

	// Token: 0x040004B3 RID: 1203
	private ServerConnection cachedServerType;

	// Token: 0x040004B4 RID: 1204
	private AsyncOperation _AsyncLevelLoadingOperation;

	// Token: 0x040004B5 RID: 1205
	private RaiseEventOptions _levelReloadEventOptions = new RaiseEventOptions
	{
		Receivers = ReceiverGroup.Others
	};

	// Token: 0x040004B6 RID: 1206
	private bool _isReconnecting;

	// Token: 0x040004B7 RID: 1207
	private readonly Type typePunRPC = typeof(PunRPC);

	// Token: 0x040004B8 RID: 1208
	private readonly Type typePhotonMessageInfo = typeof(PhotonMessageInfo);

	// Token: 0x040004B9 RID: 1209
	private readonly object keyByteZero = 0;

	// Token: 0x040004BA RID: 1210
	private readonly object keyByteOne = 1;

	// Token: 0x040004BB RID: 1211
	private readonly object keyByteTwo = 2;

	// Token: 0x040004BC RID: 1212
	private readonly object keyByteThree = 3;

	// Token: 0x040004BD RID: 1213
	private readonly object keyByteFour = 4;

	// Token: 0x040004BE RID: 1214
	private readonly object keyByteFive = 5;

	// Token: 0x040004BF RID: 1215
	private readonly object[] emptyObjectArray = new object[0];

	// Token: 0x040004C0 RID: 1216
	private readonly Type[] emptyTypeArray = new Type[0];

	// Token: 0x040004C1 RID: 1217
	private Dictionary<int, object[]> tempInstantiationData = new Dictionary<int, object[]>();

	// Token: 0x040004C2 RID: 1218
	private readonly Hashtable reusedRpcEvent = new Hashtable();

	// Token: 0x040004C3 RID: 1219
	public static int ObjectsInOneUpdate = 10;

	// Token: 0x040004C4 RID: 1220
	private RaiseEventOptions options = new RaiseEventOptions();

	// Token: 0x040004C5 RID: 1221
	public const int SyncViewId = 0;

	// Token: 0x040004C6 RID: 1222
	public const int SyncCompressed = 1;

	// Token: 0x040004C7 RID: 1223
	public const int SyncNullValues = 2;

	// Token: 0x040004C8 RID: 1224
	public const int SyncFirstValue = 3;

	// Token: 0x040004C9 RID: 1225
	public bool IsReloadingLevel;

	// Token: 0x040004CA RID: 1226
	public bool AsynchLevelLoadCall;
}
