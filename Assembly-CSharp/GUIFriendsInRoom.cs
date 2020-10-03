using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class GUIFriendsInRoom : MonoBehaviour
{
	// Token: 0x060001C9 RID: 457 RVA: 0x0000C248 File Offset: 0x0000A448
	private void Start()
	{
		this.GuiRect = new Rect((float)(Screen.width / 4), 80f, (float)(Screen.width / 2), (float)(Screen.height - 100));
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0000C274 File Offset: 0x0000A474
	public void OnGUI()
	{
		if (!PhotonNetwork.inRoom)
		{
			return;
		}
		GUILayout.BeginArea(this.GuiRect);
		GUILayout.Label("In-Game", Array.Empty<GUILayoutOption>());
		GUILayout.Label("For simplicity, this demo just shows the players in this room. The list will expand when more join.", Array.Empty<GUILayoutOption>());
		GUILayout.Label("Your (random) name: " + PhotonNetwork.playerName, Array.Empty<GUILayoutOption>());
		GUILayout.Label(PhotonNetwork.playerList.Length + " players in this room.", Array.Empty<GUILayoutOption>());
		GUILayout.Label("The others are:", Array.Empty<GUILayoutOption>());
		PhotonPlayer[] otherPlayers = PhotonNetwork.otherPlayers;
		for (int i = 0; i < otherPlayers.Length; i++)
		{
			GUILayout.Label(otherPlayers[i].ToString(), Array.Empty<GUILayoutOption>());
		}
		if (GUILayout.Button("Leave", Array.Empty<GUILayoutOption>()))
		{
			PhotonNetwork.LeaveRoom(true);
		}
		GUILayout.EndArea();
	}

	// Token: 0x040001E9 RID: 489
	public Rect GuiRect;
}
