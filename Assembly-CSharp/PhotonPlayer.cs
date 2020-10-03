using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class PhotonPlayer : IComparable<PhotonPlayer>, IComparable<int>, IEquatable<PhotonPlayer>, IEquatable<int>
{
	// Token: 0x1700009B RID: 155
	// (get) Token: 0x060004AF RID: 1199 RVA: 0x0001C4F6 File Offset: 0x0001A6F6
	public int ID
	{
		get
		{
			return this.actorID;
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001C4FE File Offset: 0x0001A6FE
	// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0001C506 File Offset: 0x0001A706
	public string NickName
	{
		get
		{
			return this.nameField;
		}
		set
		{
			if (!this.IsLocal)
			{
				Debug.LogError("Error: Cannot change the name of a remote player!");
				return;
			}
			if (string.IsNullOrEmpty(value) || value.Equals(this.nameField))
			{
				return;
			}
			this.nameField = value;
			PhotonNetwork.playerName = value;
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0001C53F File Offset: 0x0001A73F
	// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0001C547 File Offset: 0x0001A747
	public string UserId { get; internal set; }

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0001C550 File Offset: 0x0001A750
	public bool IsMasterClient
	{
		get
		{
			return PhotonNetwork.networkingPeer.mMasterClientId == this.ID;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0001C564 File Offset: 0x0001A764
	// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0001C56C File Offset: 0x0001A76C
	public bool IsInactive { get; set; }

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0001C575 File Offset: 0x0001A775
	// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0001C57D File Offset: 0x0001A77D
	public Hashtable CustomProperties { get; internal set; }

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0001C586 File Offset: 0x0001A786
	public Hashtable AllProperties
	{
		get
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Merge(this.CustomProperties);
			hashtable[byte.MaxValue] = this.NickName;
			return hashtable;
		}
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0001C5AF File Offset: 0x0001A7AF
	public PhotonPlayer(bool isLocal, int actorID, string name)
	{
		this.CustomProperties = new Hashtable();
		this.IsLocal = isLocal;
		this.actorID = actorID;
		this.nameField = name;
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0001C5E9 File Offset: 0x0001A7E9
	protected internal PhotonPlayer(bool isLocal, int actorID, Hashtable properties)
	{
		this.CustomProperties = new Hashtable();
		this.IsLocal = isLocal;
		this.actorID = actorID;
		this.InternalCacheProperties(properties);
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0001C624 File Offset: 0x0001A824
	public override bool Equals(object p)
	{
		PhotonPlayer photonPlayer = p as PhotonPlayer;
		return photonPlayer != null && this.GetHashCode() == photonPlayer.GetHashCode();
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x0001C64B File Offset: 0x0001A84B
	public override int GetHashCode()
	{
		return this.ID;
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x0001C653 File Offset: 0x0001A853
	internal void InternalChangeLocalID(int newID)
	{
		if (!this.IsLocal)
		{
			Debug.LogError("ERROR You should never change PhotonPlayer IDs!");
			return;
		}
		this.actorID = newID;
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0001C670 File Offset: 0x0001A870
	internal void InternalCacheProperties(Hashtable properties)
	{
		if (properties == null || properties.Count == 0 || this.CustomProperties.Equals(properties))
		{
			return;
		}
		if (properties.ContainsKey(255))
		{
			this.nameField = (string)properties[byte.MaxValue];
		}
		if (properties.ContainsKey(253))
		{
			this.UserId = (string)properties[253];
		}
		if (properties.ContainsKey(254))
		{
			this.IsInactive = (bool)properties[254];
		}
		this.CustomProperties.MergeStringKeys(properties);
		this.CustomProperties.StripKeysWithNullValues();
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0001C738 File Offset: 0x0001A938
	public void SetCustomProperties(Hashtable propertiesToSet, Hashtable expectedValues = null, bool webForward = false)
	{
		if (propertiesToSet == null)
		{
			return;
		}
		Hashtable hashtable = propertiesToSet.StripToStringKeys();
		Hashtable hashtable2 = expectedValues.StripToStringKeys();
		bool flag = hashtable2 == null || hashtable2.Count == 0;
		object obj = this.actorID > 0 && !PhotonNetwork.offlineMode;
		if (flag)
		{
			this.CustomProperties.Merge(hashtable);
			this.CustomProperties.StripKeysWithNullValues();
		}
		object obj2 = obj;
		if (obj2 != null)
		{
			PhotonNetwork.networkingPeer.OpSetPropertiesOfActor(this.actorID, hashtable, hashtable2, webForward);
		}
		if (obj2 == 0 || flag)
		{
			this.InternalCacheProperties(hashtable);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, new object[]
			{
				this,
				hashtable
			});
		}
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0001C7CF File Offset: 0x0001A9CF
	public static PhotonPlayer Find(int ID)
	{
		if (PhotonNetwork.networkingPeer != null)
		{
			return PhotonNetwork.networkingPeer.GetPlayerWithId(ID);
		}
		return null;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0001C7E5 File Offset: 0x0001A9E5
	public PhotonPlayer Get(int id)
	{
		return PhotonPlayer.Find(id);
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x0001C7ED File Offset: 0x0001A9ED
	public PhotonPlayer GetNext()
	{
		return this.GetNextFor(this.ID);
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x0001C7FB File Offset: 0x0001A9FB
	public PhotonPlayer GetNextFor(PhotonPlayer currentPlayer)
	{
		if (currentPlayer == null)
		{
			return null;
		}
		return this.GetNextFor(currentPlayer.ID);
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x0001C810 File Offset: 0x0001AA10
	public PhotonPlayer GetNextFor(int currentPlayerId)
	{
		if (PhotonNetwork.networkingPeer == null || PhotonNetwork.networkingPeer.mActors == null || PhotonNetwork.networkingPeer.mActors.Count < 2)
		{
			return null;
		}
		Dictionary<int, PhotonPlayer> mActors = PhotonNetwork.networkingPeer.mActors;
		int num = int.MaxValue;
		int num2 = currentPlayerId;
		foreach (int num3 in mActors.Keys)
		{
			if (num3 < num2)
			{
				num2 = num3;
			}
			else if (num3 > currentPlayerId && num3 < num)
			{
				num = num3;
			}
		}
		if (num == 2147483647)
		{
			return mActors[num2];
		}
		return mActors[num];
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0001C8C8 File Offset: 0x0001AAC8
	public int CompareTo(PhotonPlayer other)
	{
		if (other == null)
		{
			return 0;
		}
		return this.GetHashCode().CompareTo(other.GetHashCode());
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0001C8F0 File Offset: 0x0001AAF0
	public int CompareTo(int other)
	{
		return this.GetHashCode().CompareTo(other);
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0001C90C File Offset: 0x0001AB0C
	public bool Equals(PhotonPlayer other)
	{
		return other != null && this.GetHashCode().Equals(other.GetHashCode());
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0001C934 File Offset: 0x0001AB34
	public bool Equals(int other)
	{
		return this.GetHashCode().Equals(other);
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0001C950 File Offset: 0x0001AB50
	public override string ToString()
	{
		if (string.IsNullOrEmpty(this.NickName))
		{
			return string.Format("#{0:00}{1}{2}", this.ID, this.IsInactive ? " (inactive)" : " ", this.IsMasterClient ? "(master)" : "");
		}
		return string.Format("'{0}'{1}{2}", this.NickName, this.IsInactive ? " (inactive)" : " ", this.IsMasterClient ? "(master)" : "");
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0001C9E0 File Offset: 0x0001ABE0
	public string ToStringFull()
	{
		return string.Format("#{0:00} '{1}'{2} {3}", new object[]
		{
			this.ID,
			this.NickName,
			this.IsInactive ? " (inactive)" : "",
			this.CustomProperties.ToStringFull()
		});
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060004CC RID: 1228 RVA: 0x0001CA39 File Offset: 0x0001AC39
	// (set) Token: 0x060004CD RID: 1229 RVA: 0x0001CA41 File Offset: 0x0001AC41
	[Obsolete("Please use NickName (updated case for naming).")]
	public string name
	{
		get
		{
			return this.NickName;
		}
		set
		{
			this.NickName = value;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001CA4A File Offset: 0x0001AC4A
	// (set) Token: 0x060004CF RID: 1231 RVA: 0x0001CA52 File Offset: 0x0001AC52
	[Obsolete("Please use UserId (updated case for naming).")]
	public string userId
	{
		get
		{
			return this.UserId;
		}
		internal set
		{
			this.UserId = value;
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001CA5B File Offset: 0x0001AC5B
	[Obsolete("Please use IsLocal (updated case for naming).")]
	public bool isLocal
	{
		get
		{
			return this.IsLocal;
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001CA63 File Offset: 0x0001AC63
	[Obsolete("Please use IsMasterClient (updated case for naming).")]
	public bool isMasterClient
	{
		get
		{
			return this.IsMasterClient;
		}
	}

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0001CA6B File Offset: 0x0001AC6B
	// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0001CA73 File Offset: 0x0001AC73
	[Obsolete("Please use IsInactive (updated case for naming).")]
	public bool isInactive
	{
		get
		{
			return this.IsInactive;
		}
		set
		{
			this.IsInactive = value;
		}
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0001CA7C File Offset: 0x0001AC7C
	// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0001CA84 File Offset: 0x0001AC84
	[Obsolete("Please use CustomProperties (updated case for naming).")]
	public Hashtable customProperties
	{
		get
		{
			return this.CustomProperties;
		}
		internal set
		{
			this.CustomProperties = value;
		}
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0001CA8D File Offset: 0x0001AC8D
	[Obsolete("Please use AllProperties (updated case for naming).")]
	public Hashtable allProperties
	{
		get
		{
			return this.AllProperties;
		}
	}

	// Token: 0x04000513 RID: 1299
	private int actorID = -1;

	// Token: 0x04000514 RID: 1300
	private string nameField = "";

	// Token: 0x04000516 RID: 1302
	public readonly bool IsLocal;

	// Token: 0x04000519 RID: 1305
	public object TagObject;
}
