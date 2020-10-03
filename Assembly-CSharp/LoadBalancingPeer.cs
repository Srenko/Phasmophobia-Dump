using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x02000088 RID: 136
internal class LoadBalancingPeer : PhotonPeer
{
	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060002E2 RID: 738 RVA: 0x00012867 File Offset: 0x00010A67
	internal bool IsProtocolSecure
	{
		get
		{
			return base.UsedProtocol == ConnectionProtocol.WebSocketSecure;
		}
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x00012872 File Offset: 0x00010A72
	public LoadBalancingPeer(ConnectionProtocol protocolType) : base(protocolType)
	{
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00012886 File Offset: 0x00010A86
	public LoadBalancingPeer(IPhotonPeerListener listener, ConnectionProtocol protocolType) : this(protocolType)
	{
		base.Listener = listener;
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00012898 File Offset: 0x00010A98
	public virtual bool OpGetRegions(string appId)
	{
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		dictionary[224] = appId;
		SendOptions sendOptions = new SendOptions
		{
			Reliability = true,
			Channel = 0,
			Encrypt = true
		};
		return this.SendOperation(220, dictionary, sendOptions);
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x000128E8 File Offset: 0x00010AE8
	public virtual bool OpJoinLobby(TypedLobby lobby = null)
	{
		if (this.DebugOut >= DebugLevel.INFO)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpJoinLobby()");
		}
		Dictionary<byte, object> dictionary = null;
		if (lobby != null && !lobby.IsDefault)
		{
			dictionary = new Dictionary<byte, object>();
			dictionary[213] = lobby.Name;
			dictionary[212] = (byte)lobby.Type;
		}
		return this.SendOperation(229, dictionary, SendOptions.SendReliable);
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x0001295A File Offset: 0x00010B5A
	public virtual bool OpLeaveLobby()
	{
		if (this.DebugOut >= DebugLevel.INFO)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpLeaveLobby()");
		}
		return this.SendOperation(228, null, SendOptions.SendReliable);
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00012988 File Offset: 0x00010B88
	private void RoomOptionsToOpParameters(Dictionary<byte, object> op, RoomOptions roomOptions)
	{
		if (roomOptions == null)
		{
			roomOptions = new RoomOptions();
		}
		Hashtable hashtable = new Hashtable();
		hashtable[253] = roomOptions.IsOpen;
		hashtable[254] = roomOptions.IsVisible;
		hashtable[250] = ((roomOptions.CustomRoomPropertiesForLobby == null) ? new string[0] : roomOptions.CustomRoomPropertiesForLobby);
		hashtable.MergeStringKeys(roomOptions.CustomRoomProperties);
		if (roomOptions.MaxPlayers > 0)
		{
			hashtable[byte.MaxValue] = roomOptions.MaxPlayers;
		}
		op[248] = hashtable;
		int num = 0;
		op[241] = roomOptions.CleanupCacheOnLeave;
		if (roomOptions.CleanupCacheOnLeave)
		{
			num |= 2;
			hashtable[249] = true;
		}
		num |= 1;
		op[232] = true;
		if (roomOptions.PlayerTtl > 0 || roomOptions.PlayerTtl == -1)
		{
			op[235] = roomOptions.PlayerTtl;
		}
		if (roomOptions.EmptyRoomTtl > 0)
		{
			op[236] = roomOptions.EmptyRoomTtl;
		}
		if (roomOptions.SuppressRoomEvents)
		{
			num |= 4;
			op[237] = true;
		}
		if (roomOptions.Plugins != null)
		{
			op[204] = roomOptions.Plugins;
		}
		if (roomOptions.PublishUserId)
		{
			num |= 8;
			op[239] = true;
		}
		if (roomOptions.DeleteNullProperties)
		{
			num |= 16;
		}
		op[191] = num;
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x00012B44 File Offset: 0x00010D44
	public virtual bool OpCreateRoom(EnterRoomParams opParams)
	{
		if (this.DebugOut >= DebugLevel.INFO)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpCreateRoom()");
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		if (!string.IsNullOrEmpty(opParams.RoomName))
		{
			dictionary[byte.MaxValue] = opParams.RoomName;
		}
		if (opParams.Lobby != null && !opParams.Lobby.IsDefault)
		{
			dictionary[213] = opParams.Lobby.Name;
			dictionary[212] = (byte)opParams.Lobby.Type;
		}
		if (opParams.ExpectedUsers != null && opParams.ExpectedUsers.Length != 0)
		{
			dictionary[238] = opParams.ExpectedUsers;
		}
		if (opParams.OnGameServer)
		{
			if (opParams.PlayerProperties != null && opParams.PlayerProperties.Count > 0)
			{
				dictionary[249] = opParams.PlayerProperties;
				dictionary[250] = true;
			}
			this.RoomOptionsToOpParameters(dictionary, opParams.RoomOptions);
		}
		return this.SendOperation(227, dictionary, SendOptions.SendReliable);
	}

	// Token: 0x060002EA RID: 746 RVA: 0x00012C58 File Offset: 0x00010E58
	public virtual bool OpJoinRoom(EnterRoomParams opParams)
	{
		if (this.DebugOut >= DebugLevel.INFO)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpJoinRoom()");
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		if (!string.IsNullOrEmpty(opParams.RoomName))
		{
			dictionary[byte.MaxValue] = opParams.RoomName;
		}
		if (opParams.CreateIfNotExists)
		{
			dictionary[215] = 1;
			if (opParams.Lobby != null && !opParams.Lobby.IsDefault)
			{
				dictionary[213] = opParams.Lobby.Name;
				dictionary[212] = (byte)opParams.Lobby.Type;
			}
		}
		if (opParams.RejoinOnly)
		{
			dictionary[215] = 3;
		}
		if (opParams.ExpectedUsers != null && opParams.ExpectedUsers.Length != 0)
		{
			dictionary[238] = opParams.ExpectedUsers;
		}
		if (opParams.OnGameServer)
		{
			if (opParams.PlayerProperties != null && opParams.PlayerProperties.Count > 0)
			{
				dictionary[249] = opParams.PlayerProperties;
				dictionary[250] = true;
			}
			if (opParams.CreateIfNotExists)
			{
				this.RoomOptionsToOpParameters(dictionary, opParams.RoomOptions);
			}
		}
		return this.SendOperation(226, dictionary, SendOptions.SendReliable);
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00012DA4 File Offset: 0x00010FA4
	public virtual bool OpJoinRandomRoom(OpJoinRandomRoomParams opJoinRandomRoomParams)
	{
		if (this.DebugOut >= DebugLevel.INFO)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpJoinRandomRoom()");
		}
		Hashtable hashtable = new Hashtable();
		hashtable.MergeStringKeys(opJoinRandomRoomParams.ExpectedCustomRoomProperties);
		if (opJoinRandomRoomParams.ExpectedMaxPlayers > 0)
		{
			hashtable[byte.MaxValue] = opJoinRandomRoomParams.ExpectedMaxPlayers;
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		if (hashtable.Count > 0)
		{
			dictionary[248] = hashtable;
		}
		if (opJoinRandomRoomParams.MatchingType != MatchmakingMode.FillRoom)
		{
			dictionary[223] = (byte)opJoinRandomRoomParams.MatchingType;
		}
		if (opJoinRandomRoomParams.TypedLobby != null && !opJoinRandomRoomParams.TypedLobby.IsDefault)
		{
			dictionary[213] = opJoinRandomRoomParams.TypedLobby.Name;
			dictionary[212] = (byte)opJoinRandomRoomParams.TypedLobby.Type;
		}
		if (!string.IsNullOrEmpty(opJoinRandomRoomParams.SqlLobbyFilter))
		{
			dictionary[245] = opJoinRandomRoomParams.SqlLobbyFilter;
		}
		if (opJoinRandomRoomParams.ExpectedUsers != null && opJoinRandomRoomParams.ExpectedUsers.Length != 0)
		{
			dictionary[238] = opJoinRandomRoomParams.ExpectedUsers;
		}
		return this.SendOperation(225, dictionary, SendOptions.SendReliable);
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00012ED4 File Offset: 0x000110D4
	public virtual bool OpLeaveRoom(bool becomeInactive)
	{
		Dictionary<byte, object> dictionary = null;
		if (becomeInactive)
		{
			dictionary = new Dictionary<byte, object>();
			dictionary[233] = becomeInactive;
		}
		return this.SendOperation(254, dictionary, SendOptions.SendReliable);
	}

	// Token: 0x060002ED RID: 749 RVA: 0x00012F10 File Offset: 0x00011110
	public virtual bool OpGetGameList(TypedLobby lobby, string queryData)
	{
		if (this.DebugOut >= DebugLevel.INFO)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpGetGameList()");
		}
		if (lobby == null)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpGetGameList not sent. Lobby cannot be null.");
			}
			return false;
		}
		if (lobby.Type != LobbyType.SqlLobby)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpGetGameList not sent. LobbyType must be SqlLobby.");
			}
			return false;
		}
		if (lobby.IsDefault)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpGetGameList not sent. LobbyName must be not null and not empty.");
			}
			return false;
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		dictionary[213] = lobby.Name;
		dictionary[212] = (byte)lobby.Type;
		dictionary[245] = queryData;
		return this.SendOperation(217, dictionary, SendOptions.SendReliable);
	}

	// Token: 0x060002EE RID: 750 RVA: 0x00012FEC File Offset: 0x000111EC
	public virtual bool OpFindFriends(string[] friendsToFind, FindFriendsOptions options = null)
	{
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		if (friendsToFind != null && friendsToFind.Length != 0)
		{
			dictionary[1] = friendsToFind;
		}
		if (options != null)
		{
			dictionary[2] = options.ToIntFlags();
		}
		return this.SendOperation(222, dictionary, SendOptions.SendReliable);
	}

	// Token: 0x060002EF RID: 751 RVA: 0x00013034 File Offset: 0x00011234
	public bool OpSetCustomPropertiesOfActor(int actorNr, Hashtable actorProperties)
	{
		return this.OpSetPropertiesOfActor(actorNr, actorProperties.StripToStringKeys(), null, false);
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x00013048 File Offset: 0x00011248
	protected internal bool OpSetPropertiesOfActor(int actorNr, Hashtable actorProperties, Hashtable expectedProperties = null, bool webForward = false)
	{
		if (this.DebugOut >= DebugLevel.INFO)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfActor()");
		}
		if (actorNr <= 0 || actorProperties == null)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfActor not sent. ActorNr must be > 0 and actorProperties != null.");
			}
			return false;
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		dictionary.Add(251, actorProperties);
		dictionary.Add(254, actorNr);
		dictionary.Add(250, true);
		if (expectedProperties != null && expectedProperties.Count != 0)
		{
			dictionary.Add(231, expectedProperties);
		}
		if (webForward)
		{
			dictionary[234] = true;
		}
		SendOptions sendOptions = new SendOptions
		{
			Reliability = true,
			Channel = 0,
			Encrypt = false
		};
		return this.SendOperation(252, dictionary, sendOptions);
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x00013124 File Offset: 0x00011324
	protected internal void OpSetPropertyOfRoom(byte propCode, object value)
	{
		Hashtable hashtable = new Hashtable();
		hashtable[propCode] = value;
		this.OpSetPropertiesOfRoom(hashtable, null, false);
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0001314E File Offset: 0x0001134E
	[Obsolete("Use the other overload method")]
	public bool OpSetCustomPropertiesOfRoom(Hashtable gameProperties, bool broadcast, byte channelId)
	{
		return this.OpSetPropertiesOfRoom(gameProperties.StripToStringKeys(), null, false);
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0001315E File Offset: 0x0001135E
	public bool OpSetCustomPropertiesOfRoom(Hashtable gameProperties, Hashtable expectedProperties = null, bool webForward = false)
	{
		return this.OpSetPropertiesOfRoom(gameProperties.StripToStringKeys(), expectedProperties.StripToStringKeys(), webForward);
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x00013174 File Offset: 0x00011374
	protected internal bool OpSetPropertiesOfRoom(Hashtable gameProperties, Hashtable expectedProperties = null, bool webForward = false)
	{
		if (this.DebugOut >= DebugLevel.INFO)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfRoom()");
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		dictionary.Add(251, gameProperties);
		dictionary.Add(250, true);
		if (expectedProperties != null && expectedProperties.Count != 0)
		{
			dictionary.Add(231, expectedProperties);
		}
		if (webForward)
		{
			dictionary[234] = true;
		}
		SendOptions sendOptions = new SendOptions
		{
			Reliability = true,
			Channel = 0,
			Encrypt = false
		};
		return this.SendOperation(252, dictionary, sendOptions);
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00013218 File Offset: 0x00011418
	public virtual bool OpAuthenticate(string appId, string appVersion, AuthenticationValues authValues, string regionCode, bool getLobbyStatistics)
	{
		if (this.DebugOut >= DebugLevel.INFO)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpAuthenticate()");
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		if (getLobbyStatistics)
		{
			dictionary[211] = true;
		}
		SendOptions sendOptions;
		if (authValues != null && authValues.Token != null)
		{
			dictionary[221] = authValues.Token;
			sendOptions = new SendOptions
			{
				Reliability = true,
				Channel = 0,
				Encrypt = false
			};
			return this.SendOperation(230, dictionary, sendOptions);
		}
		dictionary[220] = appVersion;
		dictionary[224] = appId;
		if (!string.IsNullOrEmpty(regionCode))
		{
			dictionary[210] = regionCode;
		}
		if (authValues != null)
		{
			if (!string.IsNullOrEmpty(authValues.UserId))
			{
				dictionary[225] = authValues.UserId;
			}
			if (authValues.AuthType != CustomAuthenticationType.None)
			{
				if (!this.IsProtocolSecure && !base.IsEncryptionAvailable)
				{
					base.Listener.DebugReturn(DebugLevel.ERROR, "OpAuthenticate() failed. When you want Custom Authentication encryption is mandatory.");
					return false;
				}
				dictionary[217] = (byte)authValues.AuthType;
				if (!string.IsNullOrEmpty(authValues.Token))
				{
					dictionary[221] = authValues.Token;
				}
				else
				{
					if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
					{
						dictionary[216] = authValues.AuthGetParameters;
					}
					if (authValues.AuthPostData != null)
					{
						dictionary[214] = authValues.AuthPostData;
					}
				}
			}
		}
		sendOptions = new SendOptions
		{
			Reliability = true,
			Channel = 0,
			Encrypt = base.IsEncryptionAvailable
		};
		bool flag = this.SendOperation(230, dictionary, sendOptions);
		if (!flag)
		{
			base.Listener.DebugReturn(DebugLevel.ERROR, "Error calling OpAuthenticate! Did not work. Check log output, AuthValues and if you're connected.");
		}
		return flag;
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x000133E0 File Offset: 0x000115E0
	public virtual bool OpAuthenticateOnce(string appId, string appVersion, AuthenticationValues authValues, string regionCode, EncryptionMode encryptionMode, ConnectionProtocol expectedProtocol)
	{
		if (this.DebugOut >= DebugLevel.INFO)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpAuthenticate()");
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		SendOptions sendOptions;
		if (authValues != null && authValues.Token != null)
		{
			dictionary[221] = authValues.Token;
			sendOptions = new SendOptions
			{
				Reliability = true,
				Channel = 0,
				Encrypt = false
			};
			return this.SendOperation(231, dictionary, sendOptions);
		}
		if (encryptionMode == EncryptionMode.DatagramEncryption && expectedProtocol != ConnectionProtocol.Udp)
		{
			Debug.LogWarning("Expected protocol set to UDP, due to encryption mode DatagramEncryption. Changing protocol in PhotonServerSettings from: " + PhotonNetwork.PhotonServerSettings.Protocol);
			PhotonNetwork.PhotonServerSettings.Protocol = ConnectionProtocol.Udp;
			expectedProtocol = ConnectionProtocol.Udp;
		}
		dictionary[195] = (byte)expectedProtocol;
		dictionary[193] = (byte)encryptionMode;
		dictionary[220] = appVersion;
		dictionary[224] = appId;
		if (!string.IsNullOrEmpty(regionCode))
		{
			dictionary[210] = regionCode;
		}
		if (authValues != null)
		{
			if (!string.IsNullOrEmpty(authValues.UserId))
			{
				dictionary[225] = authValues.UserId;
			}
			if (authValues.AuthType != CustomAuthenticationType.None)
			{
				dictionary[217] = (byte)authValues.AuthType;
				if (!string.IsNullOrEmpty(authValues.Token))
				{
					dictionary[221] = authValues.Token;
				}
				else
				{
					if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
					{
						dictionary[216] = authValues.AuthGetParameters;
					}
					if (authValues.AuthPostData != null)
					{
						dictionary[214] = authValues.AuthPostData;
					}
				}
			}
		}
		sendOptions = new SendOptions
		{
			Reliability = true,
			Channel = 0,
			Encrypt = base.IsEncryptionAvailable
		};
		return this.SendOperation(231, dictionary, sendOptions);
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x000135B4 File Offset: 0x000117B4
	public virtual bool OpChangeGroups(byte[] groupsToRemove, byte[] groupsToAdd)
	{
		if (this.DebugOut >= DebugLevel.ALL)
		{
			base.Listener.DebugReturn(DebugLevel.ALL, "OpChangeGroups()");
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		if (groupsToRemove != null)
		{
			dictionary[239] = groupsToRemove;
		}
		if (groupsToAdd != null)
		{
			dictionary[238] = groupsToAdd;
		}
		return this.SendOperation(248, dictionary, SendOptions.SendReliable);
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00013610 File Offset: 0x00011810
	public virtual bool OpRaiseEvent(byte eventCode, object customEventContent, bool sendReliable, RaiseEventOptions raiseEventOptions)
	{
		this.opParameters.Clear();
		this.opParameters[244] = eventCode;
		if (customEventContent != null)
		{
			this.opParameters[245] = customEventContent;
		}
		if (raiseEventOptions == null)
		{
			raiseEventOptions = RaiseEventOptions.Default;
		}
		else
		{
			if (raiseEventOptions.CachingOption != EventCaching.DoNotCache)
			{
				this.opParameters[247] = (byte)raiseEventOptions.CachingOption;
			}
			if (raiseEventOptions.Receivers != ReceiverGroup.Others)
			{
				this.opParameters[246] = (byte)raiseEventOptions.Receivers;
			}
			if (raiseEventOptions.InterestGroup != 0)
			{
				this.opParameters[240] = raiseEventOptions.InterestGroup;
			}
			if (raiseEventOptions.TargetActors != null)
			{
				this.opParameters[252] = raiseEventOptions.TargetActors;
			}
			if (raiseEventOptions.ForwardToWebhook)
			{
				this.opParameters[234] = true;
			}
		}
		SendOptions sendOptions = new SendOptions
		{
			Reliability = sendReliable,
			Channel = raiseEventOptions.SequenceChannel,
			Encrypt = raiseEventOptions.Encrypt
		};
		return this.SendOperation(253, this.opParameters, sendOptions);
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00013750 File Offset: 0x00011950
	public virtual bool OpSettings(bool receiveLobbyStats)
	{
		if (this.DebugOut >= DebugLevel.ALL)
		{
			base.Listener.DebugReturn(DebugLevel.ALL, "OpSettings()");
		}
		this.opParameters.Clear();
		if (receiveLobbyStats)
		{
			this.opParameters[0] = receiveLobbyStats;
		}
		return this.opParameters.Count == 0 || this.SendOperation(218, this.opParameters, SendOptions.SendReliable);
	}

	// Token: 0x04000359 RID: 857
	private readonly Dictionary<byte, object> opParameters = new Dictionary<byte, object>();

	// Token: 0x020004EA RID: 1258
	private enum RoomOptionBit
	{
		// Token: 0x040023C2 RID: 9154
		CheckUserOnJoin = 1,
		// Token: 0x040023C3 RID: 9155
		DeleteCacheOnLeave,
		// Token: 0x040023C4 RID: 9156
		SuppressRoomEvents = 4,
		// Token: 0x040023C5 RID: 9157
		PublishUserId = 8,
		// Token: 0x040023C6 RID: 9158
		DeleteNullProps = 16,
		// Token: 0x040023C7 RID: 9159
		BroadcastPropsChangeToAll = 32
	}
}
