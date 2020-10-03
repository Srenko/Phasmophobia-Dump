using System;
using System.Collections.Generic;
using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

// Token: 0x0200011F RID: 287
public class MultiplayerController : Photon.MonoBehaviour
{
	// Token: 0x060007B5 RID: 1973 RVA: 0x0002DB90 File Offset: 0x0002BD90
	private void Awake()
	{
		MultiplayerController.instance = this;
		if (!XRDevice.isPresent)
		{
			for (int i = 0; i < this.spawns.Count; i++)
			{
				this.spawns[i].Translate(Vector3.up);
			}
		}
		if (!PhotonNetwork.inRoom && Application.isEditor && !PhotonNetwork.offlineMode)
		{
			PhotonNetwork.ConnectUsingSettings("Editor");
			return;
		}
		this.SpawnPlayer();
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0002DBFD File Offset: 0x0002BDFD
	private void OnConnectedToMaster()
	{
		PhotonNetwork.JoinLobby();
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0002DC08 File Offset: 0x0002BE08
	private void OnJoinedLobby()
	{
		RoomOptions roomOptions = new RoomOptions
		{
			IsVisible = false,
			IsOpen = false,
			MaxPlayers = 4
		};
		PhotonNetwork.JoinOrCreateRoom(Random.Range(1000, 100000).ToString(), roomOptions, TypedLobby.Default);
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0002DC53 File Offset: 0x0002BE53
	private void OnJoinedRoom()
	{
		this.SpawnPlayer();
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0002DC5C File Offset: 0x0002BE5C
	private void SpawnPlayer()
	{
		PhotonNetwork.Instantiate(XRDevice.isPresent ? "VRPlayer" : "PCPlayer", this.spawns[Random.Range(0, this.spawns.Count)].position, Quaternion.identity, 0);
		this.sceneCamera.gameObject.SetActive(false);
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x0002DCBA File Offset: 0x0002BEBA
	private void OnDisconnectedFromPhoton()
	{
		if (XRDevice.isPresent)
		{
			GameController.instance.loadLevel.levelName = "Menu_New";
			GameController.instance.loadLevel.Trigger();
			return;
		}
		SceneManager.LoadScene("Menu_New", LoadSceneMode.Single);
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x0002DCF4 File Offset: 0x0002BEF4
	private void OnConnectionFail(DisconnectCause cause)
	{
		PlayerPrefs.SetString("ErrorMessage", LocalisationSystem.GetLocalisedValue("Error_Disconnected") + cause);
		FileBasedPrefs.SetInt("MissionStatus", 1);
		FileBasedPrefs.SetInt("StayInServerRoom", 0);
		FileBasedPrefs.SetInt("setupPhase", 1);
	}

	// Token: 0x04000784 RID: 1924
	public static MultiplayerController instance;

	// Token: 0x04000785 RID: 1925
	public Camera sceneCamera;

	// Token: 0x04000786 RID: 1926
	public List<Transform> spawns = new List<Transform>();
}
