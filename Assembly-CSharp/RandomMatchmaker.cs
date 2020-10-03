using System;
using Photon;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class RandomMatchmaker : PunBehaviour
{
	// Token: 0x060002AC RID: 684 RVA: 0x00011B0B File Offset: 0x0000FD0B
	public void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	// Token: 0x060002AD RID: 685 RVA: 0x00011B18 File Offset: 0x0000FD18
	public override void OnJoinedLobby()
	{
		Debug.Log("JoinRandom");
		PhotonNetwork.JoinRandomRoom();
	}

	// Token: 0x060002AE RID: 686 RVA: 0x00011B2A File Offset: 0x0000FD2A
	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinRandomRoom();
	}

	// Token: 0x060002AF RID: 687 RVA: 0x00011B32 File Offset: 0x0000FD32
	public void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom(null);
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x00011B3C File Offset: 0x0000FD3C
	public override void OnJoinedRoom()
	{
		GameObject gameObject = PhotonNetwork.Instantiate("monsterprefab", Vector3.zero, Quaternion.identity, 0);
		gameObject.GetComponent<myThirdPersonController>().isControllable = true;
		this.myPhotonView = gameObject.GetComponent<PhotonView>();
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x00011B78 File Offset: 0x0000FD78
	public void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString(), Array.Empty<GUILayoutOption>());
		if (PhotonNetwork.inRoom)
		{
			bool flag = GameLogic.playerWhoIsIt == PhotonNetwork.player.ID;
			if (flag && GUILayout.Button("Marco!", Array.Empty<GUILayoutOption>()))
			{
				this.myPhotonView.RPC("Marco", PhotonTargets.All, Array.Empty<object>());
			}
			if (!flag && GUILayout.Button("Polo!", Array.Empty<GUILayoutOption>()))
			{
				this.myPhotonView.RPC("Polo", PhotonTargets.All, Array.Empty<object>());
			}
		}
	}

	// Token: 0x040002EE RID: 750
	private PhotonView myPhotonView;
}
