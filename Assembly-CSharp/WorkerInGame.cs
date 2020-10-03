using System;
using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000071 RID: 113
public class WorkerInGame : Photon.MonoBehaviour
{
	// Token: 0x0600027D RID: 637 RVA: 0x0001103C File Offset: 0x0000F23C
	public void Awake()
	{
		if (!PhotonNetwork.connected)
		{
			SceneManager.LoadScene(WorkerMenu.SceneNameMenu);
			return;
		}
		PhotonNetwork.Instantiate(this.playerPrefab.name, base.transform.position, Quaternion.identity, 0);
	}

	// Token: 0x0600027E RID: 638 RVA: 0x00011072 File Offset: 0x0000F272
	public void OnGUI()
	{
		if (GUILayout.Button("Return to Lobby", Array.Empty<GUILayoutOption>()))
		{
			PhotonNetwork.LeaveRoom(true);
		}
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0001108C File Offset: 0x0000F28C
	public void OnMasterClientSwitched(PhotonPlayer player)
	{
		Debug.Log("OnMasterClientSwitched: " + player);
		InRoomChat component = base.GetComponent<InRoomChat>();
		if (component != null)
		{
			string newLine;
			if (player.IsLocal)
			{
				newLine = "You are Master Client now.";
			}
			else
			{
				newLine = player.NickName + " is Master Client now.";
			}
			component.AddLine(newLine);
		}
	}

	// Token: 0x06000280 RID: 640 RVA: 0x000110E1 File Offset: 0x0000F2E1
	public void OnLeftRoom()
	{
		Debug.Log("OnLeftRoom (local)");
		SceneManager.LoadScene(WorkerMenu.SceneNameMenu);
	}

	// Token: 0x06000281 RID: 641 RVA: 0x000110F7 File Offset: 0x0000F2F7
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("OnDisconnectedFromPhoton");
		SceneManager.LoadScene(WorkerMenu.SceneNameMenu);
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0001110D File Offset: 0x0000F30D
	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate " + info.sender);
	}

	// Token: 0x06000283 RID: 643 RVA: 0x00011124 File Offset: 0x0000F324
	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected: " + player);
	}

	// Token: 0x06000284 RID: 644 RVA: 0x00011136 File Offset: 0x0000F336
	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPlayerDisconneced: " + player);
	}

	// Token: 0x06000285 RID: 645 RVA: 0x00011148 File Offset: 0x0000F348
	public void OnFailedToConnectToPhoton()
	{
		Debug.Log("OnFailedToConnectToPhoton");
		SceneManager.LoadScene(WorkerMenu.SceneNameMenu);
	}

	// Token: 0x040002DB RID: 731
	public Transform playerPrefab;
}
