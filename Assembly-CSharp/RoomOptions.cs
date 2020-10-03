using System;
using ExitGames.Client.Photon;

// Token: 0x02000096 RID: 150
public class RoomOptions
{
	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000302 RID: 770 RVA: 0x000137CB File Offset: 0x000119CB
	// (set) Token: 0x06000303 RID: 771 RVA: 0x000137D3 File Offset: 0x000119D3
	public bool IsVisible
	{
		get
		{
			return this.isVisibleField;
		}
		set
		{
			this.isVisibleField = value;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06000304 RID: 772 RVA: 0x000137DC File Offset: 0x000119DC
	// (set) Token: 0x06000305 RID: 773 RVA: 0x000137E4 File Offset: 0x000119E4
	public bool IsOpen
	{
		get
		{
			return this.isOpenField;
		}
		set
		{
			this.isOpenField = value;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000306 RID: 774 RVA: 0x000137ED File Offset: 0x000119ED
	// (set) Token: 0x06000307 RID: 775 RVA: 0x000137F5 File Offset: 0x000119F5
	public bool CleanupCacheOnLeave
	{
		get
		{
			return this.cleanupCacheOnLeaveField;
		}
		set
		{
			this.cleanupCacheOnLeaveField = value;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x06000308 RID: 776 RVA: 0x000137FE File Offset: 0x000119FE
	public bool SuppressRoomEvents
	{
		get
		{
			return this.suppressRoomEventsField;
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000309 RID: 777 RVA: 0x00013806 File Offset: 0x00011A06
	// (set) Token: 0x0600030A RID: 778 RVA: 0x0001380E File Offset: 0x00011A0E
	public bool PublishUserId
	{
		get
		{
			return this.publishUserIdField;
		}
		set
		{
			this.publishUserIdField = value;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x0600030B RID: 779 RVA: 0x00013817 File Offset: 0x00011A17
	// (set) Token: 0x0600030C RID: 780 RVA: 0x0001381F File Offset: 0x00011A1F
	public bool DeleteNullProperties
	{
		get
		{
			return this.deleteNullPropertiesField;
		}
		set
		{
			this.deleteNullPropertiesField = value;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x0600030D RID: 781 RVA: 0x000137CB File Offset: 0x000119CB
	// (set) Token: 0x0600030E RID: 782 RVA: 0x000137D3 File Offset: 0x000119D3
	[Obsolete("Use property with uppercase naming instead.")]
	public bool isVisible
	{
		get
		{
			return this.isVisibleField;
		}
		set
		{
			this.isVisibleField = value;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x0600030F RID: 783 RVA: 0x000137DC File Offset: 0x000119DC
	// (set) Token: 0x06000310 RID: 784 RVA: 0x000137E4 File Offset: 0x000119E4
	[Obsolete("Use property with uppercase naming instead.")]
	public bool isOpen
	{
		get
		{
			return this.isOpenField;
		}
		set
		{
			this.isOpenField = value;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000311 RID: 785 RVA: 0x00013828 File Offset: 0x00011A28
	// (set) Token: 0x06000312 RID: 786 RVA: 0x00013830 File Offset: 0x00011A30
	[Obsolete("Use property with uppercase naming instead.")]
	public byte maxPlayers
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

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000313 RID: 787 RVA: 0x000137ED File Offset: 0x000119ED
	// (set) Token: 0x06000314 RID: 788 RVA: 0x000137F5 File Offset: 0x000119F5
	[Obsolete("Use property with uppercase naming instead.")]
	public bool cleanupCacheOnLeave
	{
		get
		{
			return this.cleanupCacheOnLeaveField;
		}
		set
		{
			this.cleanupCacheOnLeaveField = value;
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000315 RID: 789 RVA: 0x00013839 File Offset: 0x00011A39
	// (set) Token: 0x06000316 RID: 790 RVA: 0x00013841 File Offset: 0x00011A41
	[Obsolete("Use property with uppercase naming instead.")]
	public Hashtable customRoomProperties
	{
		get
		{
			return this.CustomRoomProperties;
		}
		set
		{
			this.CustomRoomProperties = value;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000317 RID: 791 RVA: 0x0001384A File Offset: 0x00011A4A
	// (set) Token: 0x06000318 RID: 792 RVA: 0x00013852 File Offset: 0x00011A52
	[Obsolete("Use property with uppercase naming instead.")]
	public string[] customRoomPropertiesForLobby
	{
		get
		{
			return this.CustomRoomPropertiesForLobby;
		}
		set
		{
			this.CustomRoomPropertiesForLobby = value;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000319 RID: 793 RVA: 0x0001385B File Offset: 0x00011A5B
	// (set) Token: 0x0600031A RID: 794 RVA: 0x00013863 File Offset: 0x00011A63
	[Obsolete("Use property with uppercase naming instead.")]
	public string[] plugins
	{
		get
		{
			return this.Plugins;
		}
		set
		{
			this.Plugins = value;
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x0600031B RID: 795 RVA: 0x000137FE File Offset: 0x000119FE
	[Obsolete("Use property with uppercase naming instead.")]
	public bool suppressRoomEvents
	{
		get
		{
			return this.suppressRoomEventsField;
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x0600031C RID: 796 RVA: 0x00013806 File Offset: 0x00011A06
	// (set) Token: 0x0600031D RID: 797 RVA: 0x0001380E File Offset: 0x00011A0E
	[Obsolete("Use property with uppercase naming instead.")]
	public bool publishUserId
	{
		get
		{
			return this.publishUserIdField;
		}
		set
		{
			this.publishUserIdField = value;
		}
	}

	// Token: 0x04000419 RID: 1049
	private bool isVisibleField = true;

	// Token: 0x0400041A RID: 1050
	private bool isOpenField = true;

	// Token: 0x0400041B RID: 1051
	public byte MaxPlayers;

	// Token: 0x0400041C RID: 1052
	public int PlayerTtl;

	// Token: 0x0400041D RID: 1053
	public int EmptyRoomTtl;

	// Token: 0x0400041E RID: 1054
	private bool cleanupCacheOnLeaveField = PhotonNetwork.autoCleanUpPlayerObjects;

	// Token: 0x0400041F RID: 1055
	public Hashtable CustomRoomProperties;

	// Token: 0x04000420 RID: 1056
	public string[] CustomRoomPropertiesForLobby = new string[0];

	// Token: 0x04000421 RID: 1057
	public string[] Plugins;

	// Token: 0x04000422 RID: 1058
	private bool suppressRoomEventsField;

	// Token: 0x04000423 RID: 1059
	private bool publishUserIdField;

	// Token: 0x04000424 RID: 1060
	private bool deleteNullPropertiesField;
}
