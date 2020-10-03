using System;
using System.Collections;
using System.Collections.Generic;
using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x02000192 RID: 402
public class ServerManager : Photon.MonoBehaviour
{
	// Token: 0x06000AC8 RID: 2760 RVA: 0x00043383 File Offset: 0x00041583
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.inventoryManager = base.GetComponent<InventoryManager>();
		this.myCharacterIndex = PlayerPrefs.GetInt("CharacterIndex");
		this.UpdateUI();
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x000433B4 File Offset: 0x000415B4
	private void Start()
	{
		if (PhotonNetwork.isMasterClient)
		{
			this.MakeServerVisibleDelay();
		}
		this.jobFinderButton.interactable = PhotonNetwork.isMasterClient;
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
		{
			this.AssignNewPlayerSpot(PhotonNetwork.playerList[i]);
		}
		this.UpdateUI();
		base.Invoke("AssignAllPlayerInfoDelay", 3f);
		this.mainSelector.SetSelection();
		PlayerPrefs.SetString("ServerName", PhotonNetwork.room.Name);
		if (!PhotonNetwork.room.IsVisible && !PhotonNetwork.room.Name.Contains("#"))
		{
			this.inviteCodeText.text = ((PlayerPrefs.GetInt("inviteCodeHidden") == 1) ? "??????" : PhotonNetwork.room.Name);
			PlayerPrefs.SetInt("isPublicServer", 0);
			this.inviteCodeText.enabled = true;
			return;
		}
		PlayerPrefs.SetInt("isPublicServer", 1);
		this.inviteCodeText.enabled = false;
		this.inviteText.enabled = false;
		this.hideButton.gameObject.SetActive(false);
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x000434CA File Offset: 0x000416CA
	private IEnumerator MakeServerVisibleDelay()
	{
		if (PlayerPrefs.GetInt("isPublicServer") == 0)
		{
			yield return null;
		}
		yield return new WaitForSeconds(10f);
		if (!PhotonNetwork.offlineMode)
		{
			PhotonNetwork.room.IsVisible = true;
		}
		yield break;
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x000434D4 File Offset: 0x000416D4
	private void AssignAllPlayerInfoDelay()
	{
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
		{
			this.AssignNewPlayerSpot(PhotonNetwork.playerList[i]);
		}
		this.UpdateUI();
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x00043508 File Offset: 0x00041708
	public void EnableOrDisablePlayerModels(bool active)
	{
		if (XRDevice.isPresent)
		{
			return;
		}
		if (active)
		{
			for (int i = 0; i < this.players.Count; i++)
			{
				if (this.players[i].myPlayer != null)
				{
					this.players[i].myPlayer.characterModels[this.players[i].myPlayer.modelID].SetActive(true);
				}
			}
			return;
		}
		for (int j = 0; j < this.players.Count; j++)
		{
			if (this.players[j].myPlayer != null)
			{
				for (int k = 0; k < this.players[j].myPlayer.characterModels.Length; k++)
				{
					this.players[j].myPlayer.characterModels[k].SetActive(false);
				}
			}
		}
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x000435F3 File Offset: 0x000417F3
	private IEnumerator AssignPlayerInfoDelay(PhotonPlayer player)
	{
		yield return new WaitForSeconds(1f);
		this.AssignNewPlayerSpot(player);
		this.UpdateUI();
		yield break;
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x00043609 File Offset: 0x00041809
	private void OnEnable()
	{
		this.UpdateUI();
		this.EnableMasks(true);
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x00043618 File Offset: 0x00041818
	public void EnableMasks(bool active)
	{
		this.mainMask.sizeDelta = new Vector2((float)(active ? 1200 : 0), (float)(active ? 1200 : 0));
		this.serverMask.sizeDelta = new Vector2((float)(active ? 1200 : 0), (float)(active ? 1200 : 0));
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x00043675 File Offset: 0x00041875
	public void KickPlayer(int id)
	{
		if (PhotonNetwork.isMasterClient)
		{
			this.view.RPC("LeaveServer", this.players[id].player, new object[]
			{
				true
			});
		}
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x000436B0 File Offset: 0x000418B0
	public void UpdateUI()
	{
		for (int i = 0; i < this.players.Count; i++)
		{
			this.players[i].playerReady = false;
		}
		for (int j = 0; j < this.serverItems.Count; j++)
		{
			this.serverItems[j].gameObject.SetActive(false);
		}
		for (int k = 0; k < this.kickPlayerButtons.Length; k++)
		{
			this.kickPlayerButtons[k].interactable = false;
		}
		for (int l = 0; l < this.players.Count; l++)
		{
			this.serverItems[l].gameObject.SetActive(true);
			this.serverItems[l].playerName.text = this.players[l].player.NickName;
			if (this.players[l].level != 0)
			{
				this.serverItems[l].playerLevel.text = LocalisationSystem.GetLocalisedValue("Experience_Level") + ": " + this.players[l].level;
			}
			this.serverItems[l].playerReadyText.text = LocalisationSystem.GetLocalisedValue("Server_Unready");
			this.serverItems[l].playerIcon.sprite = this.characterIcons[this.players[l].playerCharacterIndex];
			if (this.players[l].player != PhotonNetwork.player && PhotonNetwork.isMasterClient)
			{
				this.kickPlayerButtons[l].interactable = true;
			}
		}
		this.startGameButton.interactable = false;
		this.startGameText.color = this.disabledColour;
		if (PhotonNetwork.isMasterClient)
		{
			this.selectJobText.color = new Color32(50, 50, 50, byte.MaxValue);
		}
		else
		{
			this.selectJobText.color = this.disabledColour;
		}
		if (this.levelSelectionManager.selectedLevelName != string.Empty)
		{
			this.readyButton.interactable = true;
			this.readyText.color = new Color32(50, 50, 50, byte.MaxValue);
			this.levelSelectionText.text = LocalisationSystem.GetLocalisedValue("Map_Contract") + ": " + this.levelSelectionManager.contractLevelName;
			if (this.levelSelectionManager.contractLevelDifficulty == Contract.LevelDifficulty.Amateur)
			{
				this.difficultyText.text = LocalisationSystem.GetLocalisedValue("Contract_Amateur");
				return;
			}
			if (this.levelSelectionManager.contractLevelDifficulty == Contract.LevelDifficulty.Intermediate)
			{
				this.difficultyText.text = LocalisationSystem.GetLocalisedValue("Contract_Intermediate");
				return;
			}
			this.difficultyText.text = LocalisationSystem.GetLocalisedValue("Contract_Professional");
		}
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x00043984 File Offset: 0x00041B84
	private void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		if (PhotonNetwork.isMasterClient && this.levelSelectionManager.selectedContract != null)
		{
			this.levelSelectionManager.SyncContract();
		}
		this.AssignNewPlayerSpot(player);
		this.jobFinderButton.interactable = PhotonNetwork.isMasterClient;
		this.UpdateUI();
		base.StartCoroutine(this.AssignPlayerInfoDelay(player));
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x000439E4 File Offset: 0x00041BE4
	private void OnDisable()
	{
		this.players.Clear();
		for (int i = 0; i < this.serverItems.Count; i++)
		{
			this.serverItems[i].gameObject.SetActive(false);
		}
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x00043A29 File Offset: 0x00041C29
	private void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		this.RemovePlayerSpot(player);
		this.jobFinderButton.interactable = PhotonNetwork.isMasterClient;
		this.UpdateUI();
		if (PhotonNetwork.isMasterClient && !PhotonNetwork.room.IsVisible)
		{
			this.MakeServerVisibleDelay();
		}
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x00043A64 File Offset: 0x00041C64
	private void AssignNewPlayerSpot(PhotonPlayer photonPlayer)
	{
		if (PhotonNetwork.player == photonPlayer && MainManager.instance != null && MainManager.instance.localPlayer != null)
		{
			this.view.RPC("SetPlayerInformation", PhotonTargets.AllBufferedViaServer, new object[]
			{
				photonPlayer,
				this.myCharacterIndex,
				Mathf.FloorToInt((float)(FileBasedPrefs.GetInt("myTotalExp", 0) / 100)),
				MainManager.instance.localPlayer.view.viewID
			});
		}
		this.UpdateUI();
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x00043B04 File Offset: 0x00041D04
	[PunRPC]
	public void SetPlayerInformation(PhotonPlayer photonPlayer, int index, int level, int playerViewID)
	{
		bool flag = false;
		for (int i = 0; i < this.players.Count; i++)
		{
			if (this.players[i].player == photonPlayer)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			PlayerServerSpot playerServerSpot = new PlayerServerSpot
			{
				playerReady = false,
				player = photonPlayer
			};
			if (PhotonView.Find(playerViewID) != null && PhotonView.Find(playerViewID).GetComponent<Player>() != null)
			{
				playerServerSpot.myPlayer = PhotonView.Find(playerViewID).GetComponent<Player>();
			}
			this.players.Add(playerServerSpot);
		}
		else
		{
			for (int j = 0; j < this.players.Count; j++)
			{
				if (this.players[j].player == photonPlayer)
				{
					PlayerServerSpot playerServerSpot2 = new PlayerServerSpot
					{
						player = photonPlayer
					};
					if (playerServerSpot2.myPlayer == null)
					{
						playerServerSpot2.myPlayer = PhotonView.Find(playerViewID).GetComponent<Player>();
					}
					this.players[j] = playerServerSpot2;
				}
			}
		}
		for (int k = 0; k < this.players.Count; k++)
		{
			if (this.players[k].player == photonPlayer)
			{
				this.players[k].playerCharacterIndex = index;
				this.serverItems[k].playerIcon.sprite = this.characterIcons[index];
				this.players[k].level = level;
				break;
			}
		}
		this.UpdateUI();
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x00043C80 File Offset: 0x00041E80
	private void RemovePlayerSpot(PhotonPlayer player)
	{
		for (int i = 0; i < this.players.Count; i++)
		{
			if (this.players[i].player == player)
			{
				this.serverItems[i].playerName.text = "";
				this.serverItems[i].playerReadyText.text = LocalisationSystem.GetLocalisedValue("Server_Unready");
				this.serverItems[i].playerIcon.sprite = this.characterIcons[0];
				this.serverItems[i].gameObject.SetActive(false);
				this.players.RemoveAt(i);
			}
		}
		this.UpdateUI();
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x00043D3F File Offset: 0x00041F3F
	public void Ready()
	{
		this.view.RPC("NetworkedReady", PhotonTargets.AllBufferedViaServer, new object[]
		{
			PhotonNetwork.player
		});
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x00043D60 File Offset: 0x00041F60
	public void StartGame()
	{
		if (this.levelSelectionManager.selectedLevelName != string.Empty)
		{
			if (PhotonNetwork.playerList.Length == 1)
			{
				if (PhotonNetwork.offlineMode)
				{
					this.view.RPC("LoadScene", PhotonTargets.AllBufferedViaServer, new object[]
					{
						this.levelSelectionManager.selectedLevelName
					});
				}
				else
				{
					PhotonNetwork.Disconnect();
				}
			}
			else
			{
				this.view.RPC("LoadScene", PhotonTargets.AllBufferedViaServer, new object[]
				{
					this.levelSelectionManager.selectedLevelName
				});
			}
		}
		if (PhotonNetwork.connected && !PhotonNetwork.offlineMode)
		{
			PhotonNetwork.room.IsOpen = false;
			PhotonNetwork.room.IsVisible = false;
			PhotonNetwork.room.MaxPlayers = PhotonNetwork.room.PlayerCount;
		}
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x00043E20 File Offset: 0x00042020
	private void OnDisconnectedFromPhoton()
	{
		FileBasedPrefs.SetInt("StayInServerRoom", 0);
		PhotonNetwork.offlineMode = true;
		Debug.Log("Photon is now in Offline Mode: Disconnected");
		if (!PhotonNetwork.offlineMode)
		{
			if (XRDevice.isPresent)
			{
				SteamVR_LoadLevel.Begin(SceneManager.GetActiveScene().name, false, 0.5f, 0f, 0f, 0f, 1f);
				return;
			}
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x00043E98 File Offset: 0x00042098
	private void OnConnectedToMaster()
	{
		if (PhotonNetwork.offlineMode)
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
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x00043F41 File Offset: 0x00042141
	private void OnCreatedRoom()
	{
		if (this.levelSelectionManager.selectedLevelName != string.Empty)
		{
			this.LoadScene(this.levelSelectionManager.selectedLevelName);
		}
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x00043F6C File Offset: 0x0004216C
	public void ChangeCharacterButton()
	{
		this.myCharacterIndex++;
		if (this.myCharacterIndex >= this.characterIcons.Length)
		{
			this.myCharacterIndex = 0;
		}
		this.view.RPC("UpdateCharacter", PhotonTargets.AllBufferedViaServer, new object[]
		{
			PhotonNetwork.player,
			this.myCharacterIndex
		});
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x00043FCC File Offset: 0x000421CC
	public void OpenStore(bool active)
	{
		this.storeObject.SetActive(active);
		this.serverMask.sizeDelta = new Vector2((float)(active ? 0 : 1200), (float)(active ? 0 : 1200));
		if (!active)
		{
			for (int i = 0; i < this.inventoryManager.items.Count; i++)
			{
				this.inventoryManager.items[i].UpdateTotalText();
			}
		}
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x00044044 File Offset: 0x00042244
	public void HideCodeButton()
	{
		if (this.inviteCodeText.text.Contains("?"))
		{
			this.inviteCodeText.text = PhotonNetwork.room.Name;
			PlayerPrefs.SetInt("inviteCodeHidden", 0);
			return;
		}
		this.inviteCodeText.text = "??????";
		PlayerPrefs.SetInt("inviteCodeHidden", 1);
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x000440A4 File Offset: 0x000422A4
	public void SelectJob(bool active)
	{
		this.contractSelectionObject.SetActive(active);
		this.mainMask.sizeDelta = new Vector2((float)(active ? 0 : 1200), (float)(active ? 0 : 1200));
		if (active)
		{
			this.contractSelector.SetSelection();
			return;
		}
		this.mainSelector.SetSelection();
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x00044100 File Offset: 0x00042300
	[PunRPC]
	private void UpdateCharacter(PhotonPlayer player, int characterIndex)
	{
		for (int i = 0; i < this.players.Count; i++)
		{
			if (this.players[i].player == player)
			{
				this.players[i].playerCharacterIndex = characterIndex;
				if (this.players[i].playerCharacterIndex >= this.characterIcons.Length)
				{
					this.players[i].playerCharacterIndex = 0;
				}
				this.serverItems[i].playerIcon.sprite = this.characterIcons[characterIndex];
			}
		}
		this.UpdateUI();
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x000441A0 File Offset: 0x000423A0
	[PunRPC]
	private void NetworkedReady(PhotonPlayer player)
	{
		for (int i = 0; i < this.players.Count; i++)
		{
			if (this.players[i].player == player)
			{
				this.players[i].playerReady = !this.players[i].playerReady;
				this.serverItems[i].playerReadyText.text = (this.players[i].playerReady ? LocalisationSystem.GetLocalisedValue("Server_Ready") : LocalisationSystem.GetLocalisedValue("Server_Unready"));
			}
		}
		if (PhotonNetwork.isMasterClient)
		{
			this.startGameButton.interactable = true;
			this.startGameText.color = this.enabledColour;
			for (int j = 0; j < this.players.Count; j++)
			{
				if (!this.players[j].playerReady)
				{
					this.startGameButton.interactable = false;
					this.startGameText.color = this.disabledColour;
				}
			}
		}
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x000442A8 File Offset: 0x000424A8
	[PunRPC]
	private void LoadScene(string levelToLoad)
	{
		PhotonNetwork.isMessageQueueRunning = false;
		this.inventoryManager.SaveInventory();
		PlayerPrefs.SetInt("CharacterIndex", this.myCharacterIndex);
		PlayerPrefs.SetInt("isInGame", 1);
		PlayerPrefs.SetInt("playerCount", PhotonNetwork.playerList.Length);
		int num = 1;
		for (int i = 0; i < this.players.Count; i++)
		{
			if (this.players[i].level > num)
			{
				num = this.players[i].level;
			}
		}
		PlayerPrefs.SetInt("highestLevel", num);
		if (PhotonNetwork.isMasterClient)
		{
			PhotonNetwork.room.IsOpen = false;
			PhotonNetwork.room.IsVisible = false;
			PhotonNetwork.room.MaxPlayers = PhotonNetwork.room.PlayerCount;
		}
		this.loadingScreen.SetActive(true);
		base.gameObject.SetActive(false);
		this.storeObject.SetActive(false);
		if (XRDevice.isPresent)
		{
			MainManager.instance.localPlayer.cam.cullingMask = MainManager.instance.localPlayer.noLayersMask;
			this.loadLevel.levelName = levelToLoad;
			this.loadLevel.Trigger();
			return;
		}
		this.loadingAsyncManager.LoadScene(levelToLoad);
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x000443E4 File Offset: 0x000425E4
	[PunRPC]
	public void LeaveServer(bool isKicked)
	{
		this.inventoryManager.LeftRoom();
		if (isKicked)
		{
			PlayerPrefs.SetString("ErrorMessage", "You were kicked from the server.");
		}
		if (XRDevice.isPresent)
		{
			SteamVR_LoadLevel.Begin(SceneManager.GetActiveScene().name, false, 0.5f, 0f, 0f, 0f, 1f);
			return;
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	// Token: 0x04000B14 RID: 2836
	[SerializeField]
	private Sprite[] characterIcons = new Sprite[0];

	// Token: 0x04000B15 RID: 2837
	[SerializeField]
	private GameObject loadingScreen;

	// Token: 0x04000B16 RID: 2838
	[SerializeField]
	private Button startGameButton;

	// Token: 0x04000B17 RID: 2839
	[SerializeField]
	private Button readyButton;

	// Token: 0x04000B18 RID: 2840
	[SerializeField]
	private Text readyText;

	// Token: 0x04000B19 RID: 2841
	[SerializeField]
	private Text selectJobText;

	// Token: 0x04000B1A RID: 2842
	[SerializeField]
	private Text startGameText;

	// Token: 0x04000B1B RID: 2843
	[SerializeField]
	private Color enabledColour;

	// Token: 0x04000B1C RID: 2844
	[SerializeField]
	private Color disabledColour;

	// Token: 0x04000B1D RID: 2845
	[HideInInspector]
	public PhotonView view;

	// Token: 0x04000B1E RID: 2846
	[HideInInspector]
	public InventoryManager inventoryManager;

	// Token: 0x04000B1F RID: 2847
	[SerializeField]
	private LevelSelectionManager levelSelectionManager;

	// Token: 0x04000B20 RID: 2848
	[SerializeField]
	private LoadingAsyncManager loadingAsyncManager;

	// Token: 0x04000B21 RID: 2849
	[SerializeField]
	private SteamVR_LoadLevel loadLevel;

	// Token: 0x04000B22 RID: 2850
	[SerializeField]
	private Button jobFinderButton;

	// Token: 0x04000B23 RID: 2851
	[SerializeField]
	private GameObject contractSelectionObject;

	// Token: 0x04000B24 RID: 2852
	[SerializeField]
	private Text difficultyText;

	// Token: 0x04000B25 RID: 2853
	[SerializeField]
	private Text levelSelectionText;

	// Token: 0x04000B26 RID: 2854
	public List<PlayerServerSpot> players = new List<PlayerServerSpot>();

	// Token: 0x04000B27 RID: 2855
	public List<ServerItem> serverItems = new List<ServerItem>();

	// Token: 0x04000B28 RID: 2856
	private int myCharacterIndex;

	// Token: 0x04000B29 RID: 2857
	[SerializeField]
	private RectTransform serverMask;

	// Token: 0x04000B2A RID: 2858
	[SerializeField]
	private RectTransform mainMask;

	// Token: 0x04000B2B RID: 2859
	[SerializeField]
	private GameObject storeObject;

	// Token: 0x04000B2C RID: 2860
	[SerializeField]
	private GamepadUISelector mainSelector;

	// Token: 0x04000B2D RID: 2861
	[SerializeField]
	private GamepadUISelector contractSelector;

	// Token: 0x04000B2E RID: 2862
	[SerializeField]
	private Button[] kickPlayerButtons;

	// Token: 0x04000B2F RID: 2863
	[SerializeField]
	private Text inviteCodeText;

	// Token: 0x04000B30 RID: 2864
	[SerializeField]
	private Text inviteText;

	// Token: 0x04000B31 RID: 2865
	[SerializeField]
	private Button hideButton;
}
