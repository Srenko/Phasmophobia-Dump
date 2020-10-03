using System;
using ExitGames.Client.Photon;

// Token: 0x020000DF RID: 223
public static class TurnExtensions
{
	// Token: 0x06000621 RID: 1569 RVA: 0x000225A0 File Offset: 0x000207A0
	public static void SetTurn(this Room room, int turn, bool setStartTime = false)
	{
		if (room == null || room.CustomProperties == null)
		{
			return;
		}
		Hashtable hashtable = new Hashtable();
		hashtable[TurnExtensions.TurnPropKey] = turn;
		if (setStartTime)
		{
			hashtable[TurnExtensions.TurnStartPropKey] = PhotonNetwork.ServerTimestamp;
		}
		room.SetCustomProperties(hashtable, null, false);
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x000225F1 File Offset: 0x000207F1
	public static int GetTurn(this RoomInfo room)
	{
		if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnExtensions.TurnPropKey))
		{
			return 0;
		}
		return (int)room.CustomProperties[TurnExtensions.TurnPropKey];
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x00022627 File Offset: 0x00020827
	public static int GetTurnStart(this RoomInfo room)
	{
		if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnExtensions.TurnStartPropKey))
		{
			return 0;
		}
		return (int)room.CustomProperties[TurnExtensions.TurnStartPropKey];
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x00022660 File Offset: 0x00020860
	public static int GetFinishedTurn(this PhotonPlayer player)
	{
		Room room = PhotonNetwork.room;
		if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnExtensions.TurnPropKey))
		{
			return 0;
		}
		string key = TurnExtensions.FinishedTurnPropKey + player.ID;
		return (int)room.CustomProperties[key];
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x000226BC File Offset: 0x000208BC
	public static void SetFinishedTurn(this PhotonPlayer player, int turn)
	{
		Room room = PhotonNetwork.room;
		if (room == null || room.CustomProperties == null)
		{
			return;
		}
		string key = TurnExtensions.FinishedTurnPropKey + player.ID;
		Hashtable hashtable = new Hashtable();
		hashtable[key] = turn;
		room.SetCustomProperties(hashtable, null, false);
	}

	// Token: 0x04000613 RID: 1555
	public static readonly string TurnPropKey = "Turn";

	// Token: 0x04000614 RID: 1556
	public static readonly string TurnStartPropKey = "TStart";

	// Token: 0x04000615 RID: 1557
	public static readonly string FinishedTurnPropKey = "FToA";
}
