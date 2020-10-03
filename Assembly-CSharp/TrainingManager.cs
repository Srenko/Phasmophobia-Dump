using System;
using Photon;
using UnityEngine;
using UnityEngine.XR;

// Token: 0x02000196 RID: 406
public class TrainingManager : Photon.MonoBehaviour
{
	// Token: 0x06000AF4 RID: 2804 RVA: 0x00044851 File Offset: 0x00042A51
	private void Start()
	{
		if (PhotonNetwork.connected)
		{
			PhotonNetwork.Disconnect();
			return;
		}
		this.OnDisconnectedFromPhoton();
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x00044866 File Offset: 0x00042A66
	private void OnDisconnectedFromPhoton()
	{
		PhotonNetwork.offlineMode = true;
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x00044870 File Offset: 0x00042A70
	private void OnConnectedToMaster()
	{
		RoomOptions roomOptions = new RoomOptions
		{
			IsOpen = false,
			IsVisible = false,
			MaxPlayers = 1,
			PlayerTtl = 2000
		};
		if (SteamManager.Initialized)
		{
			PhotonNetwork.CreateRoom(PhotonNetwork.player.NickName + "Training#" + Random.Range(0f, 10000f).ToString(), roomOptions, TypedLobby.Default);
			return;
		}
		PhotonNetwork.CreateRoom("UnknownTraining#" + Random.Range(0f, 100000f).ToString(), roomOptions, TypedLobby.Default);
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x00044910 File Offset: 0x00042B10
	private void OnJoinedRoom()
	{
		FileBasedPrefs.SetInt("isTutorial", 1);
		PlayerPrefs.SetInt("isInGame", 1);
		this.loadingScreen.SetActive(true);
		this.mainScreen.SetActive(false);
		PhotonNetwork.isMessageQueueRunning = false;
		if (XRDevice.isPresent)
		{
			MainManager.instance.localPlayer.cam.cullingMask = MainManager.instance.localPlayer.noLayersMask;
			this.loadLevel.levelName = "Tanglewood_Street_House";
			this.loadLevel.Trigger();
			return;
		}
		this.loadingAsyncManager.LoadScene("Tanglewood_Street_House");
	}

	// Token: 0x04000B4B RID: 2891
	[SerializeField]
	private GameObject loadingScreen;

	// Token: 0x04000B4C RID: 2892
	[SerializeField]
	private GameObject mainScreen;

	// Token: 0x04000B4D RID: 2893
	[SerializeField]
	private SteamVR_LoadLevel loadLevel;

	// Token: 0x04000B4E RID: 2894
	[SerializeField]
	private LoadingAsyncManager loadingAsyncManager;
}
