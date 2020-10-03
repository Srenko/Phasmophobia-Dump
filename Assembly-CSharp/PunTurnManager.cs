using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class PunTurnManager : PunBehaviour
{
	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x0600060D RID: 1549 RVA: 0x000222FE File Offset: 0x000204FE
	// (set) Token: 0x0600060E RID: 1550 RVA: 0x0002230A File Offset: 0x0002050A
	public int Turn
	{
		get
		{
			return PhotonNetwork.room.GetTurn();
		}
		private set
		{
			this._isOverCallProcessed = false;
			PhotonNetwork.room.SetTurn(value, true);
		}
	}

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x0600060F RID: 1551 RVA: 0x0002231F File Offset: 0x0002051F
	public float ElapsedTimeInTurn
	{
		get
		{
			return (float)(PhotonNetwork.ServerTimestamp - PhotonNetwork.room.GetTurnStart()) / 1000f;
		}
	}

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06000610 RID: 1552 RVA: 0x00022338 File Offset: 0x00020538
	public float RemainingSecondsInTurn
	{
		get
		{
			return Mathf.Max(0f, this.TurnDuration - this.ElapsedTimeInTurn);
		}
	}

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x06000611 RID: 1553 RVA: 0x00022351 File Offset: 0x00020551
	public bool IsCompletedByAll
	{
		get
		{
			return PhotonNetwork.room != null && this.Turn > 0 && this.finishedPlayers.Count == PhotonNetwork.room.PlayerCount;
		}
	}

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06000612 RID: 1554 RVA: 0x0002237C File Offset: 0x0002057C
	public bool IsFinishedByMe
	{
		get
		{
			return this.finishedPlayers.Contains(PhotonNetwork.player);
		}
	}

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06000613 RID: 1555 RVA: 0x0002238E File Offset: 0x0002058E
	public bool IsOver
	{
		get
		{
			return this.RemainingSecondsInTurn <= 0f;
		}
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x000223A0 File Offset: 0x000205A0
	private void Start()
	{
		PhotonNetwork.OnEventCall += this.OnEvent;
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x000223B3 File Offset: 0x000205B3
	private void Update()
	{
		if (this.Turn > 0 && this.IsOver && !this._isOverCallProcessed)
		{
			this._isOverCallProcessed = true;
			this.TurnManagerListener.OnTurnTimeEnds(this.Turn);
		}
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x000223E6 File Offset: 0x000205E6
	public void BeginTurn()
	{
		this.Turn++;
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x000223F8 File Offset: 0x000205F8
	public void SendMove(object move, bool finished)
	{
		if (this.IsFinishedByMe)
		{
			Debug.LogWarning("Can't SendMove. Turn is finished by this player.");
			return;
		}
		Hashtable hashtable = new Hashtable();
		hashtable.Add("turn", this.Turn);
		hashtable.Add("move", move);
		byte eventCode = finished ? 2 : 1;
		PhotonNetwork.RaiseEvent(eventCode, hashtable, true, new RaiseEventOptions
		{
			CachingOption = EventCaching.AddToRoomCache
		});
		if (finished)
		{
			PhotonNetwork.player.SetFinishedTurn(this.Turn);
		}
		this.OnEvent(eventCode, hashtable, PhotonNetwork.player.ID);
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x00022482 File Offset: 0x00020682
	public bool GetPlayerFinishedTurn(PhotonPlayer player)
	{
		return player != null && this.finishedPlayers != null && this.finishedPlayers.Contains(player);
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x000224A0 File Offset: 0x000206A0
	public void OnEvent(byte eventCode, object content, int senderId)
	{
		PhotonPlayer photonPlayer = PhotonPlayer.Find(senderId);
		if (eventCode == 1)
		{
			Hashtable hashtable = content as Hashtable;
			int turn = (int)hashtable["turn"];
			object move = hashtable["move"];
			this.TurnManagerListener.OnPlayerMove(photonPlayer, turn, move);
			return;
		}
		if (eventCode != 2)
		{
			return;
		}
		Hashtable hashtable2 = content as Hashtable;
		int num = (int)hashtable2["turn"];
		object move2 = hashtable2["move"];
		if (num == this.Turn)
		{
			this.finishedPlayers.Add(photonPlayer);
			this.TurnManagerListener.OnPlayerFinished(photonPlayer, num, move2);
		}
		if (this.IsCompletedByAll)
		{
			this.TurnManagerListener.OnTurnCompleted(this.Turn);
		}
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0002254F File Offset: 0x0002074F
	public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
	{
		if (propertiesThatChanged.ContainsKey("Turn"))
		{
			this._isOverCallProcessed = false;
			this.finishedPlayers.Clear();
			this.TurnManagerListener.OnTurnBegins(this.Turn);
		}
	}

	// Token: 0x0400060C RID: 1548
	public float TurnDuration = 20f;

	// Token: 0x0400060D RID: 1549
	public IPunTurnManagerCallbacks TurnManagerListener;

	// Token: 0x0400060E RID: 1550
	private readonly HashSet<PhotonPlayer> finishedPlayers = new HashSet<PhotonPlayer>();

	// Token: 0x0400060F RID: 1551
	public const byte TurnManagerEventOffset = 0;

	// Token: 0x04000610 RID: 1552
	public const byte EvMove = 1;

	// Token: 0x04000611 RID: 1553
	public const byte EvFinalMove = 2;

	// Token: 0x04000612 RID: 1554
	private bool _isOverCallProcessed;
}
