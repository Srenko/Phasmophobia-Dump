using System;
using UnityEngine;

namespace ExitGames.UtilityScripts
{
	// Token: 0x0200047B RID: 1147
	public static class PlayerRoomIndexingExtensions
	{
		// Token: 0x060023E2 RID: 9186 RVA: 0x000AFFFC File Offset: 0x000AE1FC
		public static int GetRoomIndex(this PhotonPlayer player)
		{
			if (PlayerRoomIndexing.instance == null)
			{
				Debug.LogError("Missing PlayerRoomIndexing Component in Scene");
				return -1;
			}
			return PlayerRoomIndexing.instance.GetRoomIndex(player);
		}
	}
}
