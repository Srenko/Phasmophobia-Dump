using System;
using System.Collections;
using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x02000186 RID: 390
public class LobbyManager : Photon.MonoBehaviour
{
	// Token: 0x06000A5D RID: 2653 RVA: 0x0004056C File Offset: 0x0003E76C
	private void Start()
	{
		this.regionDropdown.value = PlayerPrefs.GetInt("currentRegionID");
		if (this.regionDropdown.value == 0)
		{
			this.hasSetInitialRegion = true;
		}
		this.RefreshList();
		MenuAudio.instance.LobbyScreen(PhotonNetwork.GetRoomList().Length);
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x000405B9 File Offset: 0x0003E7B9
	private void Update()
	{
		if (this.rooms.Length == 0)
		{
			this.refreshTimer -= Time.deltaTime;
			if (this.refreshTimer < 0f)
			{
				this.RefreshList();
				this.refreshTimer = 5f;
			}
		}
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x000405F4 File Offset: 0x0003E7F4
	public void RefreshList()
	{
		if (PhotonNetwork.offlineMode)
		{
			this.populationText.text = LocalisationSystem.GetLocalisedValue("Lobby_Population") + ": " + LocalisationSystem.GetLocalisedValue("Menu_Offline");
			this.roomsText.text = LocalisationSystem.GetLocalisedValue("Lobby_Rooms") + ": " + LocalisationSystem.GetLocalisedValue("Menu_Offline");
		}
		else
		{
			this.populationText.text = LocalisationSystem.GetLocalisedValue("Lobby_Population") + ": " + PhotonNetwork.countOfPlayers.ToString();
			this.roomsText.text = LocalisationSystem.GetLocalisedValue("Lobby_Rooms") + ": " + PhotonNetwork.countOfRooms.ToString();
		}
		this.rooms = PhotonNetwork.GetRoomList();
		this.pageText.text = this.pageIndex / 4 + " / " + (this.rooms.Length / 4 + 1);
		this.leftButton.SetActive(false);
		this.rightButton.SetActive(false);
		if (this.rooms.Length > 4)
		{
			this.rightButton.SetActive(true);
		}
		this.pageIndex = 4;
		this.SetRooms();
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x00040730 File Offset: 0x0003E930
	public void PreviousPageButton()
	{
		this.pageIndex -= 4;
		this.pageText.text = this.pageIndex / 4 + " / " + (this.rooms.Length / 4 + 1);
		if (this.pageIndex == 4)
		{
			this.leftButton.SetActive(false);
		}
		if (this.rooms.Length > this.pageIndex)
		{
			this.rightButton.SetActive(true);
		}
		this.SetRooms();
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x000407B8 File Offset: 0x0003E9B8
	public void NextPageButton()
	{
		this.pageIndex += 4;
		if (this.pageIndex / 4 > this.rooms.Length / 4)
		{
			this.pageText.text = this.pageIndex / 4 + " / " + this.pageIndex / 4;
		}
		else
		{
			this.pageText.text = this.pageIndex / 4 + " / " + (this.rooms.Length / 4 + 1);
		}
		if (this.pageIndex > 4)
		{
			this.leftButton.SetActive(true);
		}
		if (this.rooms.Length < this.pageIndex)
		{
			this.rightButton.SetActive(false);
		}
		this.SetRooms();
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00040884 File Offset: 0x0003EA84
	public void SetRooms()
	{
		this.serverItem1.gameObject.SetActive(false);
		this.serverItem2.gameObject.SetActive(false);
		this.serverItem3.gameObject.SetActive(false);
		this.serverItem4.gameObject.SetActive(false);
		if (this.rooms.Length == 0)
		{
			return;
		}
		if (this.rooms.Length >= this.pageIndex - 3)
		{
			this.serverItem1.gameObject.SetActive(true);
			this.serverItem1.SetUI(this.rooms[this.pageIndex - 4].Name, this.rooms[this.pageIndex - 4].PlayerCount.ToString(), this.rooms[this.pageIndex - 4]);
		}
		if (this.rooms.Length >= this.pageIndex - 2)
		{
			this.serverItem2.gameObject.SetActive(true);
			this.serverItem2.SetUI(this.rooms[this.pageIndex - 3].Name, this.rooms[this.pageIndex - 3].PlayerCount.ToString(), this.rooms[this.pageIndex - 3]);
		}
		if (this.rooms.Length >= this.pageIndex - 1)
		{
			this.serverItem3.gameObject.SetActive(true);
			this.serverItem3.SetUI(this.rooms[this.pageIndex - 2].Name, this.rooms[this.pageIndex - 2].PlayerCount.ToString(), this.rooms[this.pageIndex - 2]);
		}
		if (this.rooms.Length >= this.pageIndex)
		{
			this.serverItem4.gameObject.SetActive(true);
			this.serverItem4.SetUI(this.rooms[this.pageIndex - 1].Name, this.rooms[this.pageIndex - 1].PlayerCount.ToString(), this.rooms[this.pageIndex - 1]);
		}
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x00040A94 File Offset: 0x0003EC94
	public void JoinServer(RoomInfo info)
	{
		if (!info.IsOpen || info.PlayerCount == 4 || !info.IsVisible)
		{
			this.RefreshList();
			return;
		}
		if (XRDevice.isPresent)
		{
			this.SaveVRPlayerPositions();
		}
		else
		{
			this.SavePCPlayerPositions();
		}
		PhotonNetwork.JoinRoom(info.Name);
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x00040AE2 File Offset: 0x0003ECE2
	public void JoinServerByName(string serverName)
	{
		if (XRDevice.isPresent)
		{
			this.SaveVRPlayerPositions();
		}
		else
		{
			this.SavePCPlayerPositions();
		}
		PhotonNetwork.JoinRoom(serverName);
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x00040B00 File Offset: 0x0003ED00
	public void OnPhotonJoinRoomFailed(object[] codeAndMsg)
	{
		this.RefreshList();
		Debug.LogError("Failed to join room: " + codeAndMsg[1].ToString());
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x00040B20 File Offset: 0x0003ED20
	public void CreateServer(bool isPrivate)
	{
		if (XRDevice.isPresent)
		{
			this.SaveVRPlayerPositions();
		}
		else
		{
			this.SavePCPlayerPositions();
		}
		PlayerPrefs.SetInt("isPublicServer", isPrivate ? 0 : 1);
		RoomOptions roomOptions = new RoomOptions
		{
			IsOpen = true,
			IsVisible = !isPrivate,
			MaxPlayers = 4,
			PlayerTtl = 2000
		};
		if (!isPrivate)
		{
			PhotonNetwork.CreateRoom(PhotonNetwork.player.NickName + "#" + Random.Range(0, 999999).ToString("000000"), roomOptions, TypedLobby.Default);
			return;
		}
		PhotonNetwork.CreateRoom(Random.Range(0, 999999).ToString("000000"), roomOptions, TypedLobby.Default);
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x00040BDC File Offset: 0x0003EDDC
	private void OnJoinedLobby()
	{
		this.RefreshList();
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x00040BE4 File Offset: 0x0003EDE4
	private void OnJoinedRoom()
	{
		if (this.mainManager.localPlayer)
		{
			Object.Destroy(this.mainManager.localPlayer.gameObject);
		}
		base.StartCoroutine(this.OnJoinedRoomDelay());
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x00040C1A File Offset: 0x0003EE1A
	private IEnumerator OnJoinedRoomDelay()
	{
		this.ServerManager.SetActive(true);
		for (int i = 0; i < this.lobbyObjects.Length; i++)
		{
			this.lobbyObjects[i].SetActive(false);
		}
		yield return new WaitForEndOfFrame();
		base.gameObject.SetActive(false);
		if (XRDevice.isPresent)
		{
			this.mainManager.localPlayer = PhotonNetwork.Instantiate("VRPlayer", this.mainManager.spawns[Random.Range(0, this.mainManager.spawns.Count)].position, Quaternion.identity, 0).GetComponent<Player>();
		}
		else
		{
			this.mainManager.localPlayer = PhotonNetwork.Instantiate("PCPlayer", new Vector3(this.pcPlayerXPos, this.pcPlayerYPos, this.pcPlayerZPos), this.pcPlayerRotation, 0).GetComponent<Player>();
		}
		yield break;
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x00040C2C File Offset: 0x0003EE2C
	private void SaveVRPlayerPositions()
	{
		this.steamVRXPos = this.mainManager.localPlayer.steamVRObj.position.x;
		this.steamVRYPos = this.mainManager.localPlayer.steamVRObj.position.y;
		this.steamVRZPos = this.mainManager.localPlayer.steamVRObj.position.z;
		this.steamVRRotation = this.mainManager.localPlayer.steamVRObj.rotation;
		this.vrikXPos = this.mainManager.localPlayer.VRIKObj.position.x;
		this.vrikYPos = this.mainManager.localPlayer.VRIKObj.position.y;
		this.vrikZPos = this.mainManager.localPlayer.VRIKObj.position.z;
		this.vrikRotation = this.mainManager.localPlayer.VRIKObj.rotation;
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x00040D30 File Offset: 0x0003EF30
	private void SavePCPlayerPositions()
	{
		if (this.mainManager.localPlayer == null && Object.FindObjectOfType<Player>() != null)
		{
			this.mainManager.localPlayer = Object.FindObjectOfType<Player>();
		}
		if (this.mainManager.localPlayer)
		{
			this.pcPlayerXPos = this.mainManager.localPlayer.transform.position.x;
			this.pcPlayerYPos = this.mainManager.localPlayer.transform.position.y;
			this.pcPlayerZPos = this.mainManager.localPlayer.transform.position.z;
			this.pcPlayerRotation = this.mainManager.localPlayer.transform.rotation;
		}
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x00040DFC File Offset: 0x0003EFFC
	public void ChangeRegion()
	{
		if (PlayerPrefs.HasKey("isCrackedVersion") && PlayerPrefs.GetInt("isCrackedVersion") == 1)
		{
			return;
		}
		if (!this.hasSetInitialRegion)
		{
			this.hasSetInitialRegion = true;
			return;
		}
		if (PhotonNetwork.offlineMode)
		{
			return;
		}
		switch (this.regionDropdown.value)
		{
		case 0:
			PhotonNetwork.OverrideBestCloudServer(CloudRegionCode.eu);
			break;
		case 1:
			PhotonNetwork.OverrideBestCloudServer(CloudRegionCode.us);
			break;
		case 2:
			PhotonNetwork.OverrideBestCloudServer(CloudRegionCode.sa);
			break;
		case 3:
			PhotonNetwork.OverrideBestCloudServer(CloudRegionCode.asia);
			break;
		case 4:
			PhotonNetwork.OverrideBestCloudServer(CloudRegionCode.au);
			break;
		}
		PlayerPrefs.SetInt("currentRegionID", this.regionDropdown.value);
		this.isChangingRegion = true;
		PhotonNetwork.Disconnect();
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x00040EAC File Offset: 0x0003F0AC
	private void OnDisconnectedFromPhoton()
	{
		if (!this.isChangingRegion)
		{
			if (XRDevice.isPresent)
			{
				SteamVR_LoadLevel.Begin(SceneManager.GetActiveScene().name, false, 0.5f, 0f, 0f, 0f, 1f);
			}
			else
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
		else
		{
			PhotonNetwork.ConnectUsingSettings(this.storeSDKManager.serverVersion + this.storeSDKManager.storeBranchType.ToString());
		}
		this.isChangingRegion = false;
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0002DBFD File Offset: 0x0002BDFD
	private void OnConnectedToMaster()
	{
		PhotonNetwork.JoinLobby();
	}

	// Token: 0x04000A82 RID: 2690
	[SerializeField]
	private Text populationText;

	// Token: 0x04000A83 RID: 2691
	[SerializeField]
	private Text roomsText;

	// Token: 0x04000A84 RID: 2692
	[SerializeField]
	private ServerListItem serverItem1;

	// Token: 0x04000A85 RID: 2693
	[SerializeField]
	private ServerListItem serverItem2;

	// Token: 0x04000A86 RID: 2694
	[SerializeField]
	private ServerListItem serverItem3;

	// Token: 0x04000A87 RID: 2695
	[SerializeField]
	private ServerListItem serverItem4;

	// Token: 0x04000A88 RID: 2696
	public GameObject ServerManager;

	// Token: 0x04000A89 RID: 2697
	[SerializeField]
	private MainManager mainManager;

	// Token: 0x04000A8A RID: 2698
	[SerializeField]
	private StoreSDKManager storeSDKManager;

	// Token: 0x04000A8B RID: 2699
	[SerializeField]
	private Dropdown regionDropdown;

	// Token: 0x04000A8C RID: 2700
	private bool hasSetInitialRegion;

	// Token: 0x04000A8D RID: 2701
	private bool isChangingRegion;

	// Token: 0x04000A8E RID: 2702
	[SerializeField]
	private PCManager pcManager;

	// Token: 0x04000A8F RID: 2703
	private float steamVRXPos;

	// Token: 0x04000A90 RID: 2704
	private float steamVRYPos;

	// Token: 0x04000A91 RID: 2705
	private float steamVRZPos;

	// Token: 0x04000A92 RID: 2706
	private Quaternion steamVRRotation;

	// Token: 0x04000A93 RID: 2707
	private float vrikXPos;

	// Token: 0x04000A94 RID: 2708
	private float vrikYPos;

	// Token: 0x04000A95 RID: 2709
	private float vrikZPos;

	// Token: 0x04000A96 RID: 2710
	private Quaternion vrikRotation;

	// Token: 0x04000A97 RID: 2711
	private float pcPlayerXPos;

	// Token: 0x04000A98 RID: 2712
	private float pcPlayerYPos;

	// Token: 0x04000A99 RID: 2713
	private float pcPlayerZPos;

	// Token: 0x04000A9A RID: 2714
	private Quaternion pcPlayerRotation;

	// Token: 0x04000A9B RID: 2715
	[SerializeField]
	private SteamVR_LoadLevel loadLevel;

	// Token: 0x04000A9C RID: 2716
	[HideInInspector]
	public RoomInfo[] rooms;

	// Token: 0x04000A9D RID: 2717
	[SerializeField]
	private GameObject leftButton;

	// Token: 0x04000A9E RID: 2718
	[SerializeField]
	private GameObject rightButton;

	// Token: 0x04000A9F RID: 2719
	[SerializeField]
	private Text pageText;

	// Token: 0x04000AA0 RID: 2720
	private int pageIndex = 4;

	// Token: 0x04000AA1 RID: 2721
	private float refreshTimer = 5f;

	// Token: 0x04000AA2 RID: 2722
	[SerializeField]
	private GameObject[] lobbyObjects;
}
