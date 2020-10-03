using System;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x020000BA RID: 186
public class Room : RoomInfo
{
	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x06000510 RID: 1296 RVA: 0x0001DD44 File Offset: 0x0001BF44
	// (set) Token: 0x06000511 RID: 1297 RVA: 0x0001DD4C File Offset: 0x0001BF4C
	public new string Name
	{
		get
		{
			return this.nameField;
		}
		internal set
		{
			this.nameField = value;
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000512 RID: 1298 RVA: 0x0001DD55 File Offset: 0x0001BF55
	// (set) Token: 0x06000513 RID: 1299 RVA: 0x0001DD60 File Offset: 0x0001BF60
	public new bool IsOpen
	{
		get
		{
			return this.openField;
		}
		set
		{
			if (!this.Equals(PhotonNetwork.room))
			{
				Debug.LogWarning("Can't set open when not in that room.");
			}
			if (value != this.openField && !PhotonNetwork.offlineMode)
			{
				PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(new Hashtable
				{
					{
						253,
						value
					}
				}, null, false);
			}
			this.openField = value;
		}
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001DDC3 File Offset: 0x0001BFC3
	// (set) Token: 0x06000515 RID: 1301 RVA: 0x0001DDCC File Offset: 0x0001BFCC
	public new bool IsVisible
	{
		get
		{
			return this.visibleField;
		}
		set
		{
			if (!this.Equals(PhotonNetwork.room))
			{
				Debug.LogWarning("Can't set visible when not in that room.");
			}
			if (value != this.visibleField && !PhotonNetwork.offlineMode)
			{
				PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(new Hashtable
				{
					{
						254,
						value
					}
				}, null, false);
			}
			this.visibleField = value;
		}
	}

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001DE2F File Offset: 0x0001C02F
	// (set) Token: 0x06000517 RID: 1303 RVA: 0x0001DE37 File Offset: 0x0001C037
	public string[] PropertiesListedInLobby { get; private set; }

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001DE40 File Offset: 0x0001C040
	public bool AutoCleanUp
	{
		get
		{
			return this.autoCleanUpField;
		}
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000519 RID: 1305 RVA: 0x0001DE48 File Offset: 0x0001C048
	// (set) Token: 0x0600051A RID: 1306 RVA: 0x0001DE50 File Offset: 0x0001C050
	public new int MaxPlayers
	{
		get
		{
			return (int)this.maxPlayersField;
		}
		set
		{
			if (!this.Equals(PhotonNetwork.room))
			{
				Debug.LogWarning("Can't set MaxPlayers when not in that room.");
			}
			if (value > 255)
			{
				Debug.LogWarning("Can't set Room.MaxPlayers to: " + value + ". Using max value: 255.");
				value = 255;
			}
			if (value != (int)this.maxPlayersField && !PhotonNetwork.offlineMode)
			{
				PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(new Hashtable
				{
					{
						byte.MaxValue,
						(byte)value
					}
				}, null, false);
			}
			this.maxPlayersField = (byte)value;
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x0600051B RID: 1307 RVA: 0x0001DEDE File Offset: 0x0001C0DE
	public new int PlayerCount
	{
		get
		{
			if (PhotonNetwork.playerList != null)
			{
				return PhotonNetwork.playerList.Length;
			}
			return 0;
		}
	}

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001DEF0 File Offset: 0x0001C0F0
	public string[] ExpectedUsers
	{
		get
		{
			return this.expectedUsersField;
		}
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x0600051D RID: 1309 RVA: 0x0001DEF8 File Offset: 0x0001C0F8
	// (set) Token: 0x0600051E RID: 1310 RVA: 0x0001DF00 File Offset: 0x0001C100
	public int PlayerTtl
	{
		get
		{
			return this.playerTtlField;
		}
		set
		{
			if (!this.Equals(PhotonNetwork.room))
			{
				Debug.LogWarning("Can't set PlayerTtl when not in a room.");
			}
			if (value != this.playerTtlField && !PhotonNetwork.offlineMode)
			{
				PhotonNetwork.networkingPeer.OpSetPropertyOfRoom(246, value);
			}
			this.playerTtlField = value;
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x0600051F RID: 1311 RVA: 0x0001DF50 File Offset: 0x0001C150
	// (set) Token: 0x06000520 RID: 1312 RVA: 0x0001DF58 File Offset: 0x0001C158
	public int EmptyRoomTtl
	{
		get
		{
			return this.emptyRoomTtlField;
		}
		set
		{
			if (!this.Equals(PhotonNetwork.room))
			{
				Debug.LogWarning("Can't set EmptyRoomTtl when not in a room.");
			}
			if (value != this.emptyRoomTtlField && !PhotonNetwork.offlineMode)
			{
				PhotonNetwork.networkingPeer.OpSetPropertyOfRoom(245, value);
			}
			this.emptyRoomTtlField = value;
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000521 RID: 1313 RVA: 0x0001DFA8 File Offset: 0x0001C1A8
	// (set) Token: 0x06000522 RID: 1314 RVA: 0x0001DFB0 File Offset: 0x0001C1B0
	protected internal int MasterClientId
	{
		get
		{
			return this.masterClientIdField;
		}
		set
		{
			this.masterClientIdField = value;
		}
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x0001DFBC File Offset: 0x0001C1BC
	internal Room(string roomName, RoomOptions options) : base(roomName, null)
	{
		if (options == null)
		{
			options = new RoomOptions();
		}
		this.visibleField = options.IsVisible;
		this.openField = options.IsOpen;
		this.maxPlayersField = options.MaxPlayers;
		this.autoCleanUpField = options.CleanupCacheOnLeave;
		base.InternalCacheProperties(options.CustomRoomProperties);
		this.PropertiesListedInLobby = options.CustomRoomPropertiesForLobby;
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x0001E024 File Offset: 0x0001C224
	public void SetCustomProperties(Hashtable propertiesToSet, Hashtable expectedValues = null, bool webForward = false)
	{
		if (propertiesToSet == null)
		{
			return;
		}
		Hashtable hashtable = propertiesToSet.StripToStringKeys();
		Hashtable hashtable2 = expectedValues.StripToStringKeys();
		bool flag = hashtable2 == null || hashtable2.Count == 0;
		if (PhotonNetwork.offlineMode || flag)
		{
			base.CustomProperties.Merge(hashtable);
			base.CustomProperties.StripKeysWithNullValues();
		}
		if (!PhotonNetwork.offlineMode)
		{
			PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(hashtable, hashtable2, webForward);
		}
		if (PhotonNetwork.offlineMode || flag)
		{
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCustomRoomPropertiesChanged, new object[]
			{
				hashtable
			});
		}
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x0001E0A4 File Offset: 0x0001C2A4
	public void SetPropertiesListedInLobby(string[] propsListedInLobby)
	{
		Hashtable hashtable = new Hashtable();
		hashtable[250] = propsListedInLobby;
		PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(hashtable, null, false);
		this.PropertiesListedInLobby = propsListedInLobby;
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x0001E0E0 File Offset: 0x0001C2E0
	public void ClearExpectedUsers()
	{
		Hashtable hashtable = new Hashtable();
		hashtable[247] = new string[0];
		Hashtable hashtable2 = new Hashtable();
		hashtable2[247] = this.ExpectedUsers;
		PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(hashtable, hashtable2, false);
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x0001E134 File Offset: 0x0001C334
	public void SetExpectedUsers(string[] expectedUsers)
	{
		Hashtable hashtable = new Hashtable();
		hashtable[247] = expectedUsers;
		Hashtable hashtable2 = new Hashtable();
		hashtable2[247] = this.ExpectedUsers;
		PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(hashtable, hashtable2, false);
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x0001E184 File Offset: 0x0001C384
	public override string ToString()
	{
		return string.Format("Room: '{0}' {1},{2} {4}/{3} players.", new object[]
		{
			this.nameField,
			this.visibleField ? "visible" : "hidden",
			this.openField ? "open" : "closed",
			this.maxPlayersField,
			this.PlayerCount
		});
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x0001E1F4 File Offset: 0x0001C3F4
	public new string ToStringFull()
	{
		return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", new object[]
		{
			this.nameField,
			this.visibleField ? "visible" : "hidden",
			this.openField ? "open" : "closed",
			this.maxPlayersField,
			this.PlayerCount,
			base.CustomProperties.ToStringFull()
		});
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001E272 File Offset: 0x0001C472
	// (set) Token: 0x0600052B RID: 1323 RVA: 0x0001E27A File Offset: 0x0001C47A
	[Obsolete("Please use Name (updated case for naming).")]
	public new string name
	{
		get
		{
			return this.Name;
		}
		internal set
		{
			this.Name = value;
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x0600052C RID: 1324 RVA: 0x0001E283 File Offset: 0x0001C483
	// (set) Token: 0x0600052D RID: 1325 RVA: 0x0001E28B File Offset: 0x0001C48B
	[Obsolete("Please use IsOpen (updated case for naming).")]
	public new bool open
	{
		get
		{
			return this.IsOpen;
		}
		set
		{
			this.IsOpen = value;
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001E294 File Offset: 0x0001C494
	// (set) Token: 0x0600052F RID: 1327 RVA: 0x0001E29C File Offset: 0x0001C49C
	[Obsolete("Please use IsVisible (updated case for naming).")]
	public new bool visible
	{
		get
		{
			return this.IsVisible;
		}
		set
		{
			this.IsVisible = value;
		}
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001E2A5 File Offset: 0x0001C4A5
	// (set) Token: 0x06000531 RID: 1329 RVA: 0x0001E2AD File Offset: 0x0001C4AD
	[Obsolete("Please use PropertiesListedInLobby (updated case for naming).")]
	public string[] propertiesListedInLobby
	{
		get
		{
			return this.PropertiesListedInLobby;
		}
		private set
		{
			this.PropertiesListedInLobby = value;
		}
	}

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001E2B6 File Offset: 0x0001C4B6
	[Obsolete("Please use AutoCleanUp (updated case for naming).")]
	public bool autoCleanUp
	{
		get
		{
			return this.AutoCleanUp;
		}
	}

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001E2BE File Offset: 0x0001C4BE
	// (set) Token: 0x06000534 RID: 1332 RVA: 0x0001E2C6 File Offset: 0x0001C4C6
	[Obsolete("Please use MaxPlayers (updated case for naming).")]
	public new int maxPlayers
	{
		get
		{
			return this.MaxPlayers;
		}
		set
		{
			this.MaxPlayers = value;
		}
	}

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001E2CF File Offset: 0x0001C4CF
	[Obsolete("Please use PlayerCount (updated case for naming).")]
	public new int playerCount
	{
		get
		{
			return this.PlayerCount;
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001E2D7 File Offset: 0x0001C4D7
	[Obsolete("Please use ExpectedUsers (updated case for naming).")]
	public string[] expectedUsers
	{
		get
		{
			return this.ExpectedUsers;
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001E2DF File Offset: 0x0001C4DF
	// (set) Token: 0x06000538 RID: 1336 RVA: 0x0001E2E7 File Offset: 0x0001C4E7
	[Obsolete("Please use MasterClientId (updated case for naming).")]
	protected internal int masterClientId
	{
		get
		{
			return this.MasterClientId;
		}
		set
		{
			this.MasterClientId = value;
		}
	}
}
