using System;
using ExitGames.Client.Photon;

// Token: 0x020000BB RID: 187
public class RoomInfo
{
	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001E2F0 File Offset: 0x0001C4F0
	// (set) Token: 0x0600053A RID: 1338 RVA: 0x0001E2F8 File Offset: 0x0001C4F8
	public bool removedFromList { get; internal set; }

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x0600053B RID: 1339 RVA: 0x0001E301 File Offset: 0x0001C501
	// (set) Token: 0x0600053C RID: 1340 RVA: 0x0001E309 File Offset: 0x0001C509
	protected internal bool serverSideMasterClient { get; private set; }

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001E312 File Offset: 0x0001C512
	public Hashtable CustomProperties
	{
		get
		{
			return this.customPropertiesField;
		}
	}

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x0600053E RID: 1342 RVA: 0x0001DD44 File Offset: 0x0001BF44
	public string Name
	{
		get
		{
			return this.nameField;
		}
	}

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x0600053F RID: 1343 RVA: 0x0001E31A File Offset: 0x0001C51A
	// (set) Token: 0x06000540 RID: 1344 RVA: 0x0001E322 File Offset: 0x0001C522
	public int PlayerCount { get; private set; }

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001E32B File Offset: 0x0001C52B
	// (set) Token: 0x06000542 RID: 1346 RVA: 0x0001E333 File Offset: 0x0001C533
	public bool IsLocalClientInside { get; set; }

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x06000543 RID: 1347 RVA: 0x0001DE48 File Offset: 0x0001C048
	public byte MaxPlayers
	{
		get
		{
			return this.maxPlayersField;
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001DD55 File Offset: 0x0001BF55
	public bool IsOpen
	{
		get
		{
			return this.openField;
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001DDC3 File Offset: 0x0001BFC3
	public bool IsVisible
	{
		get
		{
			return this.visibleField;
		}
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x0001E33C File Offset: 0x0001C53C
	protected internal RoomInfo(string roomName, Hashtable properties)
	{
		this.InternalCacheProperties(properties);
		this.nameField = roomName;
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x0001E378 File Offset: 0x0001C578
	public override bool Equals(object other)
	{
		RoomInfo roomInfo = other as RoomInfo;
		return roomInfo != null && this.Name.Equals(roomInfo.nameField);
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x0001E3A2 File Offset: 0x0001C5A2
	public override int GetHashCode()
	{
		return this.nameField.GetHashCode();
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x0001E3B0 File Offset: 0x0001C5B0
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

	// Token: 0x0600054A RID: 1354 RVA: 0x0001E420 File Offset: 0x0001C620
	public string ToStringFull()
	{
		return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", new object[]
		{
			this.nameField,
			this.visibleField ? "visible" : "hidden",
			this.openField ? "open" : "closed",
			this.maxPlayersField,
			this.PlayerCount,
			this.customPropertiesField.ToStringFull()
		});
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x0001E4A0 File Offset: 0x0001C6A0
	protected internal void InternalCacheProperties(Hashtable propertiesToCache)
	{
		if (propertiesToCache == null || propertiesToCache.Count == 0 || this.customPropertiesField.Equals(propertiesToCache))
		{
			return;
		}
		if (propertiesToCache.ContainsKey(251))
		{
			this.removedFromList = (bool)propertiesToCache[251];
			if (this.removedFromList)
			{
				return;
			}
		}
		if (propertiesToCache.ContainsKey(255))
		{
			this.maxPlayersField = (byte)propertiesToCache[byte.MaxValue];
		}
		if (propertiesToCache.ContainsKey(253))
		{
			this.openField = (bool)propertiesToCache[253];
		}
		if (propertiesToCache.ContainsKey(254))
		{
			this.visibleField = (bool)propertiesToCache[254];
		}
		if (propertiesToCache.ContainsKey(252))
		{
			this.PlayerCount = (int)((byte)propertiesToCache[252]);
		}
		if (propertiesToCache.ContainsKey(249))
		{
			this.autoCleanUpField = (bool)propertiesToCache[249];
		}
		if (propertiesToCache.ContainsKey(248))
		{
			this.serverSideMasterClient = true;
			bool flag = this.masterClientIdField != 0;
			this.masterClientIdField = (int)propertiesToCache[248];
			if (flag)
			{
				PhotonNetwork.networkingPeer.UpdateMasterClient();
			}
		}
		if (propertiesToCache.ContainsKey(247))
		{
			this.expectedUsersField = (string[])propertiesToCache[247];
		}
		if (propertiesToCache.ContainsKey(245))
		{
			this.emptyRoomTtlField = (int)propertiesToCache[245];
		}
		if (propertiesToCache.ContainsKey(246))
		{
			this.playerTtlField = (int)propertiesToCache[246];
		}
		this.customPropertiesField.MergeStringKeys(propertiesToCache);
		this.customPropertiesField.StripKeysWithNullValues();
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001E6C5 File Offset: 0x0001C8C5
	[Obsolete("Please use CustomProperties (updated case for naming).")]
	public Hashtable customProperties
	{
		get
		{
			return this.CustomProperties;
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x0600054D RID: 1357 RVA: 0x0001E6CD File Offset: 0x0001C8CD
	[Obsolete("Please use Name (updated case for naming).")]
	public string name
	{
		get
		{
			return this.Name;
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001E6D5 File Offset: 0x0001C8D5
	// (set) Token: 0x0600054F RID: 1359 RVA: 0x0001E6DD File Offset: 0x0001C8DD
	[Obsolete("Please use PlayerCount (updated case for naming).")]
	public int playerCount
	{
		get
		{
			return this.PlayerCount;
		}
		set
		{
			this.PlayerCount = value;
		}
	}

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001E6E6 File Offset: 0x0001C8E6
	// (set) Token: 0x06000551 RID: 1361 RVA: 0x0001E6EE File Offset: 0x0001C8EE
	[Obsolete("Please use IsLocalClientInside (updated case for naming).")]
	public bool isLocalClientInside
	{
		get
		{
			return this.IsLocalClientInside;
		}
		set
		{
			this.IsLocalClientInside = value;
		}
	}

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001E6F7 File Offset: 0x0001C8F7
	[Obsolete("Please use MaxPlayers (updated case for naming).")]
	public byte maxPlayers
	{
		get
		{
			return this.MaxPlayers;
		}
	}

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001E6FF File Offset: 0x0001C8FF
	[Obsolete("Please use IsOpen (updated case for naming).")]
	public bool open
	{
		get
		{
			return this.IsOpen;
		}
	}

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001E707 File Offset: 0x0001C907
	[Obsolete("Please use IsVisible (updated case for naming).")]
	public bool visible
	{
		get
		{
			return this.IsVisible;
		}
	}

	// Token: 0x0400055C RID: 1372
	private Hashtable customPropertiesField = new Hashtable();

	// Token: 0x0400055D RID: 1373
	protected byte maxPlayersField;

	// Token: 0x0400055E RID: 1374
	protected int emptyRoomTtlField;

	// Token: 0x0400055F RID: 1375
	protected int playerTtlField;

	// Token: 0x04000560 RID: 1376
	protected string[] expectedUsersField;

	// Token: 0x04000561 RID: 1377
	protected bool openField = true;

	// Token: 0x04000562 RID: 1378
	protected bool visibleField = true;

	// Token: 0x04000563 RID: 1379
	protected bool autoCleanUpField = PhotonNetwork.autoCleanUpPlayerObjects;

	// Token: 0x04000564 RID: 1380
	protected string nameField;

	// Token: 0x04000565 RID: 1381
	protected internal int masterClientIdField;
}
