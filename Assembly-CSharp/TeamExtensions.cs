using System;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x020000DC RID: 220
public static class TeamExtensions
{
	// Token: 0x0600060B RID: 1547 RVA: 0x00022278 File Offset: 0x00020478
	public static PunTeams.Team GetTeam(this PhotonPlayer player)
	{
		object obj;
		if (player.CustomProperties.TryGetValue("team", out obj))
		{
			return (PunTeams.Team)obj;
		}
		return PunTeams.Team.none;
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x000222A4 File Offset: 0x000204A4
	public static void SetTeam(this PhotonPlayer player, PunTeams.Team team)
	{
		if (!PhotonNetwork.connectedAndReady)
		{
			Debug.LogWarning("JoinTeam was called in state: " + PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady.");
			return;
		}
		if (player.GetTeam() != team)
		{
			player.SetCustomProperties(new Hashtable
			{
				{
					"team",
					(byte)team
				}
			}, null, false);
		}
	}
}
