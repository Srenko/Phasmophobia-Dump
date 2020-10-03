using System;
using ExitGames.UtilityScripts;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class DemoOwnershipGui : MonoBehaviour
{
	// Token: 0x06000176 RID: 374 RVA: 0x0000A77C File Offset: 0x0000897C
	public void OnOwnershipRequest(object[] viewAndPlayer)
	{
		PhotonView photonView = viewAndPlayer[0] as PhotonView;
		PhotonPlayer photonPlayer = viewAndPlayer[1] as PhotonPlayer;
		Debug.Log(string.Concat(new object[]
		{
			"OnOwnershipRequest(): Player ",
			photonPlayer,
			" requests ownership of: ",
			photonView,
			"."
		}));
		if (this.TransferOwnershipOnRequest)
		{
			photonView.TransferOwnership(photonPlayer.ID);
		}
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000A7E0 File Offset: 0x000089E0
	public void OnOwnershipTransfered(object[] viewAndPlayers)
	{
		PhotonView photonView = viewAndPlayers[0] as PhotonView;
		PhotonPlayer photonPlayer = viewAndPlayers[1] as PhotonPlayer;
		PhotonPlayer photonPlayer2 = viewAndPlayers[2] as PhotonPlayer;
		Debug.Log(string.Concat(new object[]
		{
			"OnOwnershipTransfered for PhotonView",
			photonView.ToString(),
			" from ",
			photonPlayer2,
			" to ",
			photonPlayer
		}));
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0000A844 File Offset: 0x00008A44
	public void OnGUI()
	{
		GUI.skin = this.Skin;
		GUILayout.BeginArea(new Rect((float)(Screen.width - 200), 0f, 200f, (float)Screen.height));
		if (GUILayout.Button(this.TransferOwnershipOnRequest ? "passing objects" : "rejecting to pass", Array.Empty<GUILayoutOption>()))
		{
			this.TransferOwnershipOnRequest = !this.TransferOwnershipOnRequest;
		}
		GUILayout.EndArea();
		if (PhotonNetwork.inRoom)
		{
			int id = PhotonNetwork.player.ID;
			string arg = PhotonNetwork.player.IsMasterClient ? "(master) " : "";
			string colorName = this.GetColorName(PhotonNetwork.player.ID);
			GUILayout.Label(string.Format("player {0}, {1} {2}(you)", id, colorName, arg), Array.Empty<GUILayoutOption>());
			foreach (PhotonPlayer photonPlayer in PhotonNetwork.otherPlayers)
			{
				id = photonPlayer.ID;
				arg = (photonPlayer.IsMasterClient ? "(master)" : "");
				colorName = this.GetColorName(photonPlayer.ID);
				GUILayout.Label(string.Format("player {0}, {1} {2}", id, colorName, arg), Array.Empty<GUILayoutOption>());
			}
			if (PhotonNetwork.inRoom && PhotonNetwork.otherPlayers.Length == 0)
			{
				GUILayout.Label("Join more clients to switch object-control.", Array.Empty<GUILayoutOption>());
				return;
			}
		}
		else
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString(), Array.Empty<GUILayoutOption>());
		}
	}

	// Token: 0x06000179 RID: 377 RVA: 0x0000A9B4 File Offset: 0x00008BB4
	private string GetColorName(int playerId)
	{
		switch (Array.IndexOf<int>(PlayerRoomIndexing.instance.PlayerIds, playerId))
		{
		case 0:
			return "red";
		case 1:
			return "blue";
		case 2:
			return "yellow";
		case 3:
			return "green";
		default:
			return string.Empty;
		}
	}

	// Token: 0x040001B2 RID: 434
	public GUISkin Skin;

	// Token: 0x040001B3 RID: 435
	public bool TransferOwnershipOnRequest = true;
}
