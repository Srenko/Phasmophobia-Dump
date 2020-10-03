using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class GameLogic : MonoBehaviour
{
	// Token: 0x0600029D RID: 669 RVA: 0x000118B1 File Offset: 0x0000FAB1
	public void Start()
	{
		GameLogic.ScenePhotonView = base.GetComponent<PhotonView>();
	}

	// Token: 0x0600029E RID: 670 RVA: 0x000118BE File Offset: 0x0000FABE
	public void OnJoinedRoom()
	{
		if (PhotonNetwork.playerList.Length == 1)
		{
			GameLogic.playerWhoIsIt = PhotonNetwork.player.ID;
		}
		Debug.Log("playerWhoIsIt: " + GameLogic.playerWhoIsIt);
	}

	// Token: 0x0600029F RID: 671 RVA: 0x000118F2 File Offset: 0x0000FAF2
	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected: " + player);
		if (PhotonNetwork.isMasterClient)
		{
			GameLogic.TagPlayer(GameLogic.playerWhoIsIt);
		}
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x00011915 File Offset: 0x0000FB15
	public static void TagPlayer(int playerID)
	{
		Debug.Log("TagPlayer: " + playerID);
		GameLogic.ScenePhotonView.RPC("TaggedPlayer", PhotonTargets.All, new object[]
		{
			playerID
		});
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0001194B File Offset: 0x0000FB4B
	[PunRPC]
	public void TaggedPlayer(int playerID)
	{
		GameLogic.playerWhoIsIt = playerID;
		Debug.Log("TaggedPlayer: " + playerID);
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x00011968 File Offset: 0x0000FB68
	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerDisconnected: " + player);
		if (PhotonNetwork.isMasterClient && player.ID == GameLogic.playerWhoIsIt)
		{
			GameLogic.TagPlayer(PhotonNetwork.player.ID);
		}
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0001199D File Offset: 0x0000FB9D
	public void OnMasterClientSwitched()
	{
		Debug.Log("OnMasterClientSwitched");
	}

	// Token: 0x040002E8 RID: 744
	public static int playerWhoIsIt;

	// Token: 0x040002E9 RID: 745
	private static PhotonView ScenePhotonView;
}
