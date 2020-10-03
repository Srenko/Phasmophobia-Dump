using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class PickupDemoGui : MonoBehaviour
{
	// Token: 0x06000206 RID: 518 RVA: 0x0000E41C File Offset: 0x0000C61C
	public void OnGUI()
	{
		if (!PhotonNetwork.inRoom)
		{
			return;
		}
		if (this.ShowScores)
		{
			GUILayout.Label("Your Score: " + PhotonNetwork.player.GetScore(), Array.Empty<GUILayoutOption>());
		}
		if (this.ShowDropButton)
		{
			foreach (PickupItem pickupItem in PickupItem.DisabledPickupItems)
			{
				if (pickupItem.PickupIsMine && pickupItem.SecondsBeforeRespawn <= 0f)
				{
					if (GUILayout.Button("Drop " + pickupItem.name, Array.Empty<GUILayoutOption>()))
					{
						pickupItem.Drop();
					}
					GameObject gameObject = PhotonNetwork.player.TagObject as GameObject;
					if (gameObject != null && GUILayout.Button("Drop here " + pickupItem.name, Array.Empty<GUILayoutOption>()))
					{
						Vector3 a = Random.insideUnitSphere;
						a.y = 0f;
						a = a.normalized;
						Vector3 newPosition = gameObject.transform.position + this.DropOffset * a;
						pickupItem.Drop(newPosition);
					}
				}
			}
		}
		if (this.ShowTeams)
		{
			foreach (PunTeams.Team key in PunTeams.PlayersPerTeam.Keys)
			{
				GUILayout.Label("Team: " + key.ToString(), Array.Empty<GUILayoutOption>());
				foreach (PhotonPlayer photonPlayer in PunTeams.PlayersPerTeam[key])
				{
					GUILayout.Label(string.Concat(new object[]
					{
						"  ",
						photonPlayer.ToStringFull(),
						" Score: ",
						photonPlayer.GetScore()
					}), Array.Empty<GUILayoutOption>());
				}
			}
			if (GUILayout.Button("to red", Array.Empty<GUILayoutOption>()))
			{
				PhotonNetwork.player.SetTeam(PunTeams.Team.red);
			}
			if (GUILayout.Button("to blue", Array.Empty<GUILayoutOption>()))
			{
				PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
			}
		}
	}

	// Token: 0x04000247 RID: 583
	public bool ShowScores;

	// Token: 0x04000248 RID: 584
	public bool ShowDropButton;

	// Token: 0x04000249 RID: 585
	public bool ShowTeams;

	// Token: 0x0400024A RID: 586
	public float DropOffset = 0.5f;
}
