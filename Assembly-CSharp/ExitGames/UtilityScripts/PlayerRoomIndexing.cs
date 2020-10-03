using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon;
using UnityEngine;

namespace ExitGames.UtilityScripts
{
	// Token: 0x0200047A RID: 1146
	public class PlayerRoomIndexing : PunBehaviour
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x000AFC3D File Offset: 0x000ADE3D
		public int[] PlayerIds
		{
			get
			{
				return this._playerIds;
			}
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000AFC45 File Offset: 0x000ADE45
		public void Awake()
		{
			if (PlayerRoomIndexing.instance != null)
			{
				Debug.LogError("Existing instance of PlayerRoomIndexing found. Only One instance is required at the most. Please correct and have only one at any time.");
			}
			PlayerRoomIndexing.instance = this;
			if (PhotonNetwork.room != null)
			{
				this.SanitizeIndexing(true);
			}
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000AFC72 File Offset: 0x000ADE72
		public override void OnJoinedRoom()
		{
			if (PhotonNetwork.isMasterClient)
			{
				this.AssignIndex(PhotonNetwork.player);
				return;
			}
			this.RefreshData();
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000AFC8D File Offset: 0x000ADE8D
		public override void OnLeftRoom()
		{
			this.RefreshData();
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000AFC95 File Offset: 0x000ADE95
		public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
		{
			if (PhotonNetwork.isMasterClient)
			{
				this.AssignIndex(newPlayer);
			}
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000AFCA5 File Offset: 0x000ADEA5
		public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
		{
			if (PhotonNetwork.isMasterClient)
			{
				this.UnAssignIndex(otherPlayer);
			}
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000AFCB5 File Offset: 0x000ADEB5
		public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
		{
			if (propertiesThatChanged.ContainsKey("PlayerIndexes"))
			{
				this.RefreshData();
			}
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000AFCCA File Offset: 0x000ADECA
		public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
		{
			if (PhotonNetwork.isMasterClient)
			{
				this.SanitizeIndexing(false);
			}
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000AFCDA File Offset: 0x000ADEDA
		public int GetRoomIndex(PhotonPlayer player)
		{
			if (this._indexesLUT != null && this._indexesLUT.ContainsKey(player.ID))
			{
				return this._indexesLUT[player.ID];
			}
			return -1;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000AFD0C File Offset: 0x000ADF0C
		private void SanitizeIndexing(bool forceIndexing = false)
		{
			if (!forceIndexing && !PhotonNetwork.isMasterClient)
			{
				return;
			}
			if (PhotonNetwork.room == null)
			{
				return;
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			if (PhotonNetwork.room.CustomProperties.TryGetValue("PlayerIndexes", out this._indexes))
			{
				dictionary = (this._indexes as Dictionary<int, int>);
			}
			if (dictionary.Count != PhotonNetwork.room.PlayerCount)
			{
				foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList)
				{
					if (!dictionary.ContainsKey(photonPlayer.ID))
					{
						this.AssignIndex(photonPlayer);
					}
				}
			}
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000AFD9C File Offset: 0x000ADF9C
		private void RefreshData()
		{
			if (PhotonNetwork.room != null)
			{
				if (!PhotonNetwork.room.CustomProperties.TryGetValue("PlayerIndexes", out this._indexes))
				{
					goto IL_BD;
				}
				this._indexesLUT = (this._indexes as Dictionary<int, int>);
				this._playerIds = new int[this._indexesLUT.Keys.Max() + 1];
				using (Dictionary<int, int>.Enumerator enumerator = this._indexesLUT.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, int> keyValuePair = enumerator.Current;
						this._p = PhotonPlayer.Find(keyValuePair.Key);
						this._playerIds[keyValuePair.Value] = this._p.ID;
					}
					goto IL_BD;
				}
			}
			this._playerIds = new int[0];
			IL_BD:
			if (this.OnRoomIndexingChanged != null)
			{
				this.OnRoomIndexingChanged();
			}
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000AFE8C File Offset: 0x000AE08C
		private void AssignIndex(PhotonPlayer player)
		{
			List<bool> list = new List<bool>();
			if (PhotonNetwork.room.CustomProperties.TryGetValue("PlayerIndexes", out this._indexes))
			{
				this._indexesLUT = (this._indexes as Dictionary<int, int>);
				list = new List<bool>(new bool[this._indexesLUT.Keys.Max() + 1]);
				using (Dictionary<int, int>.Enumerator enumerator = this._indexesLUT.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, int> keyValuePair = enumerator.Current;
						list[keyValuePair.Value] = true;
					}
					goto IL_98;
				}
			}
			this._indexesLUT = new Dictionary<int, int>();
			IL_98:
			this._indexesLUT[player.ID] = Mathf.Max(0, list.IndexOf(false));
			PhotonNetwork.room.SetCustomProperties(new Hashtable
			{
				{
					"PlayerIndexes",
					this._indexesLUT
				}
			}, null, false);
			this.RefreshData();
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000AFF88 File Offset: 0x000AE188
		private void UnAssignIndex(PhotonPlayer player)
		{
			if (PhotonNetwork.room.CustomProperties.TryGetValue("PlayerIndexes", out this._indexes))
			{
				this._indexesLUT = (this._indexes as Dictionary<int, int>);
				this._indexesLUT.Remove(player.ID);
				PhotonNetwork.room.SetCustomProperties(new Hashtable
				{
					{
						"PlayerIndexes",
						this._indexesLUT
					}
				}, null, false);
			}
			this.RefreshData();
		}

		// Token: 0x04002135 RID: 8501
		public static PlayerRoomIndexing instance;

		// Token: 0x04002136 RID: 8502
		public PlayerRoomIndexing.RoomIndexingChanged OnRoomIndexingChanged;

		// Token: 0x04002137 RID: 8503
		public const string RoomPlayerIndexedProp = "PlayerIndexes";

		// Token: 0x04002138 RID: 8504
		private int[] _playerIds;

		// Token: 0x04002139 RID: 8505
		private object _indexes;

		// Token: 0x0400213A RID: 8506
		private Dictionary<int, int> _indexesLUT;

		// Token: 0x0400213B RID: 8507
		private List<bool> _indexesPool;

		// Token: 0x0400213C RID: 8508
		private PhotonPlayer _p;

		// Token: 0x02000796 RID: 1942
		// (Invoke) Token: 0x06003028 RID: 12328
		public delegate void RoomIndexingChanged();
	}
}
