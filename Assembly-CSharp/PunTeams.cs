using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class PunTeams : MonoBehaviour
{
	// Token: 0x06000602 RID: 1538 RVA: 0x00022140 File Offset: 0x00020340
	public void Start()
	{
		PunTeams.PlayersPerTeam = new Dictionary<PunTeams.Team, List<PhotonPlayer>>();
		foreach (object obj in Enum.GetValues(typeof(PunTeams.Team)))
		{
			PunTeams.PlayersPerTeam[(PunTeams.Team)obj] = new List<PhotonPlayer>();
		}
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x000221B8 File Offset: 0x000203B8
	public void OnDisable()
	{
		PunTeams.PlayersPerTeam = new Dictionary<PunTeams.Team, List<PhotonPlayer>>();
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x000221C4 File Offset: 0x000203C4
	public void OnJoinedRoom()
	{
		this.UpdateTeams();
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x000221CC File Offset: 0x000203CC
	public void OnLeftRoom()
	{
		this.Start();
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x000221C4 File Offset: 0x000203C4
	public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		this.UpdateTeams();
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x000221C4 File Offset: 0x000203C4
	public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{
		this.UpdateTeams();
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x000221C4 File Offset: 0x000203C4
	public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		this.UpdateTeams();
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x000221D4 File Offset: 0x000203D4
	public void UpdateTeams()
	{
		foreach (object obj in Enum.GetValues(typeof(PunTeams.Team)))
		{
			PunTeams.PlayersPerTeam[(PunTeams.Team)obj].Clear();
		}
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
		{
			PhotonPlayer photonPlayer = PhotonNetwork.playerList[i];
			PunTeams.Team team = photonPlayer.GetTeam();
			PunTeams.PlayersPerTeam[team].Add(photonPlayer);
		}
	}

	// Token: 0x0400060A RID: 1546
	public static Dictionary<PunTeams.Team, List<PhotonPlayer>> PlayersPerTeam;

	// Token: 0x0400060B RID: 1547
	public const string TeamPlayerProp = "team";

	// Token: 0x02000502 RID: 1282
	public enum Team : byte
	{
		// Token: 0x04002410 RID: 9232
		none,
		// Token: 0x04002411 RID: 9233
		red,
		// Token: 0x04002412 RID: 9234
		blue
	}
}
