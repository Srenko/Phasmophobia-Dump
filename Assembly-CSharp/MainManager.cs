using System;
using System.Collections;
using System.Collections.Generic;
using Photon;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Viveport;
using VRTK;

// Token: 0x02000187 RID: 391
public class MainManager : Photon.MonoBehaviour
{
	// Token: 0x06000A70 RID: 2672 RVA: 0x00040F57 File Offset: 0x0003F157
	private void Awake()
	{
		MainManager.instance = this;
		this.vrtkCanvas.enabled = XRDevice.isPresent;
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x00040F6F File Offset: 0x0003F16F
	public IEnumerator Start()
	{
		if (!PhotonNetwork.connected && !PhotonNetwork.offlineMode)
		{
			FileBasedPrefs.SetInt("StayInServerRoom", 0);
		}
		this.serverVersionText.text = LocalisationSystem.GetLocalisedValue("Menu_ServerVersion") + ": " + this.storeSDKManager.serverVersion;
		yield return new WaitForSeconds(0.5f);
		if (!XRDevice.isPresent)
		{
			for (int i = 0; i < this.spawns.Count; i++)
			{
				this.spawns[i].Translate(Vector3.up);
			}
		}
		if (!PhotonNetwork.connected || PhotonNetwork.offlineMode)
		{
			if (PhotonNetwork.offlineMode)
			{
				PhotonNetwork.offlineMode = false;
			}
			if (this.storeSDKManager.storeSDKType == StoreSDKManager.StoreSDKType.steam)
			{
				this.steamAuth.ConnectViaSteamAuthenticator();
			}
			if (XRDevice.isPresent)
			{
				this.localPlayer = Object.Instantiate<GameObject>(this.vrPlayerModel, this.spawns[Random.Range(0, this.spawns.Count)].position, Quaternion.identity).GetComponent<Player>();
			}
			else
			{
				this.localPlayer = Object.Instantiate<GameObject>(this.pcPlayerModel, this.spawns[Random.Range(0, this.spawns.Count)].position, Quaternion.identity).GetComponent<Player>();
				this.pcManager.SetValues();
			}
			FileBasedPrefs.SetInt("StayInServerRoom", 0);
			this.LoadRewardScreens();
		}
		else if (FileBasedPrefs.GetInt("StayInServerRoom", 0) == 1)
		{
			PhotonNetwork.LeaveRoom(true);
		}
		else
		{
			FileBasedPrefs.SetInt("StayInServerRoom", 0);
			PhotonNetwork.LeaveRoom(true);
		}
		if (FileBasedPrefs.GetInt("completedTraining", 0) == 0 && !Application.isEditor)
		{
			this.serverLobbyButton.interactable = false;
			this.serverLobbyText.color = new Color32(50, 50, 50, 119);
		}
		FileBasedPrefs.SetInt("isTutorial", 0);
		this.trainingGhostTypeText.text = LocalisationSystem.GetLocalisedValue("Reward_Ghost") + " " + FileBasedPrefs.GetString("GhostType", "");
		this.SetScreenResolution();
		if (FileBasedPrefs.GetInt("myTotalExp", 0) < 100)
		{
			FileBasedPrefs.SetInt("myTotalExp", 100);
		}
		if (FileBasedPrefs.GetInt("myTotalExp", 0) < 100)
		{
			FileBasedPrefs.SetInt("myTotalExp", 100);
		}
		yield break;
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x00040F7E File Offset: 0x0003F17E
	private void OnDisconnectedFromPhoton()
	{
		FileBasedPrefs.SetInt("StayInServerRoom", 0);
		PhotonNetwork.offlineMode = true;
		Debug.Log("Photon is now in Offline Mode: Disconnected");
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x00040F9C File Offset: 0x0003F19C
	private void OnConnectionFail(DisconnectCause cause)
	{
		FileBasedPrefs.SetInt("StayInServerRoom", 0);
		PhotonNetwork.offlineMode = true;
		Debug.Log("Photon is now in Offline Mode: " + cause);
		PlayerPrefs.SetString("ErrorMessage", "Connection Failed: " + cause);
		this.ErrorScreen.SetActive(true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x00041004 File Offset: 0x0003F204
	private void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		FileBasedPrefs.SetInt("StayInServerRoom", 0);
		PhotonNetwork.offlineMode = true;
		Debug.Log("Photon is now in Offline Mode: " + cause);
		PlayerPrefs.SetString("ErrorMessage", "Failed To Connect: " + cause);
		this.ErrorScreen.SetActive(true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0004106C File Offset: 0x0003F26C
	private void OnPhotonMaxCccuReached()
	{
		FileBasedPrefs.SetInt("StayInServerRoom", 0);
		PhotonNetwork.offlineMode = true;
		Debug.Log("Photon is now in Offline Mode due to too many players on the server!");
		PlayerPrefs.SetString("ErrorMessage", "Disconnected: Server player limit reached. Please let the developer know as soon as possible.");
		this.ResetSettings(true);
		this.ErrorScreen.SetActive(true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x000410C2 File Offset: 0x0003F2C2
	private void OnConnectedToMaster()
	{
		if (!PhotonNetwork.offlineMode)
		{
			PhotonNetwork.JoinLobby();
			return;
		}
		this.OnJoinedLobby();
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x000410D8 File Offset: 0x0003F2D8
	private void OnLeftRoom()
	{
		if (FileBasedPrefs.GetInt("StayInServerRoom", 0) == 0)
		{
			if (XRDevice.isPresent)
			{
				this.localPlayer = Object.Instantiate<GameObject>(this.vrPlayerModel, this.spawns[Random.Range(0, this.spawns.Count)].position, Quaternion.identity).GetComponent<Player>();
			}
			else
			{
				this.localPlayer = Object.Instantiate<GameObject>(this.pcPlayerModel, this.spawns[Random.Range(0, this.spawns.Count)].position, Quaternion.identity).GetComponent<Player>();
				this.pcManager.SetValues();
			}
		}
		if (FileBasedPrefs.GetInt("MissionStatus", 0) == 3)
		{
			this.LoadRewardScreens();
		}
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x00041198 File Offset: 0x0003F398
	private void OnJoinedRoom()
	{
		if (XRDevice.isPresent)
		{
			this.localPlayer = PhotonNetwork.Instantiate("VRPlayer", this.spawns[Random.Range(0, this.spawns.Count)].position, Quaternion.identity, 0).GetComponent<Player>();
		}
		else
		{
			this.localPlayer = PhotonNetwork.Instantiate("PCPlayer", this.spawns[Random.Range(0, this.spawns.Count)].position, Quaternion.identity, 0).GetComponent<Player>();
			this.pcManager.SetValues();
		}
		this.LoadRewardScreens();
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x00041238 File Offset: 0x0003F438
	public void SetPlayerName()
	{
		string text = "Unkwown";
		if (this.storeSDKManager.storeSDKType == StoreSDKManager.StoreSDKType.steam)
		{
			text = (SteamManager.Initialized ? SteamFriends.GetPersonaName() : "Unkwown");
		}
		else if (this.storeSDKManager.storeSDKType == StoreSDKManager.StoreSDKType.viveport)
		{
			text = (ViveportInitialiser.Initialized ? User.GetUserName() : "Unkwown");
		}
		if (text == "Goldberg")
		{
			FileBasedPrefs.SetInt("StayInServerRoom", 0);
			PhotonNetwork.offlineMode = true;
			text = "I pirated the game";
			Debug.Log("I pirated the game");
		}
		PhotonNetwork.playerName = text;
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x000412C4 File Offset: 0x0003F4C4
	private void OnJoinedLobby()
	{
		if (FileBasedPrefs.GetInt("StayInServerRoom", 0) == 1)
		{
			FileBasedPrefs.SetInt("StayInServerRoom", 0);
			RoomOptions roomOptions = new RoomOptions
			{
				IsOpen = true,
				IsVisible = (PlayerPrefs.GetInt("isPublicServer") == 1),
				MaxPlayers = 4,
				PlayerTtl = 2000
			};
			PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("ServerName"), roomOptions, TypedLobby.Default);
			return;
		}
		this.LoadRewardScreens();
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x0004133C File Offset: 0x0003F53C
	private void OnPhotonCreateRoomFailed()
	{
		RoomOptions roomOptions = new RoomOptions
		{
			IsOpen = true,
			IsVisible = (PlayerPrefs.GetInt("isPublicServer") == 1),
			MaxPlayers = 4,
			PlayerTtl = 2000
		};
		PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("ServerName"), roomOptions, TypedLobby.Default);
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x00041394 File Offset: 0x0003F594
	private void OnPhotonJoinRoomFailed()
	{
		if (this.connectionAttempts == 6)
		{
			if (XRDevice.isPresent)
			{
				this.localPlayer = Object.Instantiate<GameObject>(this.vrPlayerModel, this.spawns[Random.Range(0, this.spawns.Count)].position, Quaternion.identity).GetComponent<Player>();
			}
			else
			{
				this.localPlayer = Object.Instantiate<GameObject>(this.pcPlayerModel, this.spawns[Random.Range(0, this.spawns.Count)].position, Quaternion.identity).GetComponent<Player>();
				this.pcManager.SetValues();
			}
			this.LoadRewardScreens();
			FileBasedPrefs.SetInt("StayInServerRoom", 0);
			return;
		}
		this.connectionAttempts++;
		base.StartCoroutine(this.AttemptToJoinRoomAfterDelay());
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x00041466 File Offset: 0x0003F666
	private IEnumerator AttemptToJoinRoomAfterDelay()
	{
		yield return new WaitForSeconds(2f);
		RoomOptions roomOptions = new RoomOptions
		{
			IsOpen = true,
			IsVisible = (PlayerPrefs.GetInt("isPublicServer") == 1),
			MaxPlayers = 4,
			PlayerTtl = 2000
		};
		PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("ServerName"), roomOptions, TypedLobby.Default);
		yield break;
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x0004146E File Offset: 0x0003F66E
	public void AcceptPhotoWarning()
	{
		PlayerPrefs.SetInt("PhotoSensitivityWarning", 3);
		this.PhotoWarningScreen.SetActive(false);
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x00041494 File Offset: 0x0003F694
	private void LoadRewardScreens()
	{
		if (this.ranOnce)
		{
			return;
		}
		this.ranOnce = true;
		if (PlayerPrefs.GetString("ErrorMessage") != string.Empty)
		{
			Debug.LogError("Disconnect Error: " + PlayerPrefs.GetString("ErrorMessage"));
			this.ResetSettings(true);
			this.ErrorScreen.SetActive(true);
			base.gameObject.SetActive(false);
		}
		else if (PlayerPrefs.GetInt("PhotoSensitivityWarning") != 3)
		{
			this.ResetSettings(true);
			this.PhotoWarningScreen.SetActive(true);
			base.gameObject.SetActive(false);
		}
		else if (FileBasedPrefs.GetInt("MissionStatus", 0) == 1)
		{
			this.ResetSettings(true);
			this.RewardScreen.SetActive(true);
			base.gameObject.SetActive(false);
		}
		else if (FileBasedPrefs.GetInt("MissionStatus", 0) == 3)
		{
			this.ResetSettings(true);
			this.TrainingScreen.SetActive(true);
			base.gameObject.SetActive(false);
		}
		else if (FileBasedPrefs.GetInt("MissionStatus", 0) == 2)
		{
			this.ResetSettings(false);
			this.FailureScreen.SetActive(true);
			base.gameObject.SetActive(false);
		}
		if (PhotonNetwork.inRoom)
		{
			this.serverScreen.SetActive(true);
			this.serverManager.EnableMasks(false);
		}
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x000415DF File Offset: 0x0003F7DF
	private void ResetSettings(bool resetSetup)
	{
		PlayerPrefs.SetInt("isInGame", 0);
		FileBasedPrefs.SetInt("isTutorial", 0);
		FileBasedPrefs.SetInt("MissionStatus", 0);
		if (resetSetup)
		{
			FileBasedPrefs.SetInt("setupPhase", 0);
		}
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x00041610 File Offset: 0x0003F810
	private void SetScreenResolution()
	{
		if (!XRDevice.isPresent)
		{
			if (PlayerPrefs.GetInt("resolutionValue") > Screen.resolutions.Length - 1)
			{
				PlayerPrefs.SetInt("resolutionValue", Screen.resolutions.Length - 1);
			}
			else if (PlayerPrefs.GetInt("resolutionValue") < 0)
			{
				PlayerPrefs.SetInt("resolutionValue", Screen.resolutions.Length - 1);
			}
			if (PlayerPrefs.GetInt("resolutionValue") == 0)
			{
				PlayerPrefs.SetInt("resolutionValue", Screen.resolutions.Length - 1);
				Screen.SetResolution(Screen.resolutions[Screen.resolutions.Length - 1].width, Screen.resolutions[Screen.resolutions.Length - 1].height, true);
				return;
			}
			Screen.SetResolution(Screen.resolutions[PlayerPrefs.GetInt("resolutionValue")].width, Screen.resolutions[PlayerPrefs.GetInt("resolutionValue")].height, PlayerPrefs.GetInt("fullscreenType") == 1);
		}
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0004170B File Offset: 0x0003F90B
	public void QuitGame()
	{
		Application.Quit();
	}

	// Token: 0x04000AA3 RID: 2723
	public static MainManager instance;

	// Token: 0x04000AA4 RID: 2724
	public Camera sceneCamera;

	// Token: 0x04000AA5 RID: 2725
	public List<Transform> spawns = new List<Transform>();

	// Token: 0x04000AA6 RID: 2726
	public GameObject vrPlayerModel;

	// Token: 0x04000AA7 RID: 2727
	public GameObject pcPlayerModel;

	// Token: 0x04000AA8 RID: 2728
	private bool ranOnce;

	// Token: 0x04000AA9 RID: 2729
	[SerializeField]
	private GameObject serverObject;

	// Token: 0x04000AAA RID: 2730
	[SerializeField]
	private VRTK_UICanvas vrtkCanvas;

	// Token: 0x04000AAB RID: 2731
	public PCManager pcManager;

	// Token: 0x04000AAC RID: 2732
	[SerializeField]
	private MyAudioManager audioManager;

	// Token: 0x04000AAD RID: 2733
	[SerializeField]
	private StoreSDKManager storeSDKManager;

	// Token: 0x04000AAE RID: 2734
	public ControlsManager controlsManager;

	// Token: 0x04000AAF RID: 2735
	public ServerManager serverManager;

	// Token: 0x04000AB0 RID: 2736
	[SerializeField]
	private GameObject RewardScreen;

	// Token: 0x04000AB1 RID: 2737
	[SerializeField]
	private GameObject FailureScreen;

	// Token: 0x04000AB2 RID: 2738
	[SerializeField]
	private GameObject TrainingScreen;

	// Token: 0x04000AB3 RID: 2739
	[SerializeField]
	private GameObject serverScreen;

	// Token: 0x04000AB4 RID: 2740
	[SerializeField]
	private GameObject ErrorScreen;

	// Token: 0x04000AB5 RID: 2741
	[SerializeField]
	private GameObject PhotoWarningScreen;

	// Token: 0x04000AB6 RID: 2742
	[SerializeField]
	private Text trainingGhostTypeText;

	// Token: 0x04000AB7 RID: 2743
	[SerializeField]
	private Button serverLobbyButton;

	// Token: 0x04000AB8 RID: 2744
	[SerializeField]
	private Text serverLobbyText;

	// Token: 0x04000AB9 RID: 2745
	[SerializeField]
	private Text serverVersionText;

	// Token: 0x04000ABA RID: 2746
	[HideInInspector]
	public Player localPlayer;

	// Token: 0x04000ABB RID: 2747
	private int connectionAttempts;

	// Token: 0x04000ABC RID: 2748
	[SerializeField]
	private SteamAuth steamAuth;
}
