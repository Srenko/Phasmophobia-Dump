using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class WorkerMenu : MonoBehaviour
{
	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000287 RID: 647 RVA: 0x0001115E File Offset: 0x0000F35E
	// (set) Token: 0x06000288 RID: 648 RVA: 0x00011166 File Offset: 0x0000F366
	public string ErrorDialog
	{
		get
		{
			return this.errorDialog;
		}
		private set
		{
			this.errorDialog = value;
			if (!string.IsNullOrEmpty(value))
			{
				this.timeToClearDialog = (double)(Time.time + 4f);
			}
		}
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0001118C File Offset: 0x0000F38C
	public void Awake()
	{
		PhotonNetwork.automaticallySyncScene = true;
		if (PhotonNetwork.connectionStateDetailed == ClientState.PeerCreated)
		{
			PhotonNetwork.ConnectUsingSettings("0.9");
		}
		if (string.IsNullOrEmpty(PhotonNetwork.playerName))
		{
			PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
		}
	}

	// Token: 0x0600028A RID: 650 RVA: 0x000111E0 File Offset: 0x0000F3E0
	public void OnGUI()
	{
		if (this.Skin != null)
		{
			GUI.skin = this.Skin;
		}
		if (!PhotonNetwork.connected)
		{
			if (PhotonNetwork.connecting)
			{
				GUILayout.Label("Connecting to: " + PhotonNetwork.ServerAddress, Array.Empty<GUILayoutOption>());
			}
			else
			{
				GUILayout.Label(string.Concat(new object[]
				{
					"Not connected. Check console output. Detailed connection state: ",
					PhotonNetwork.connectionStateDetailed,
					" Server: ",
					PhotonNetwork.ServerAddress
				}), Array.Empty<GUILayoutOption>());
			}
			if (this.connectFailed)
			{
				GUILayout.Label("Connection failed. Check setup and use Setup Wizard to fix configuration.", Array.Empty<GUILayoutOption>());
				GUILayout.Label(string.Format("Server: {0}", new object[]
				{
					PhotonNetwork.ServerAddress
				}), Array.Empty<GUILayoutOption>());
				GUILayout.Label("AppId: " + PhotonNetwork.PhotonServerSettings.AppID.Substring(0, 8) + "****", Array.Empty<GUILayoutOption>());
				if (GUILayout.Button("Try Again", new GUILayoutOption[]
				{
					GUILayout.Width(100f)
				}))
				{
					this.connectFailed = false;
					PhotonNetwork.ConnectUsingSettings("0.9");
				}
			}
			return;
		}
		Rect rect = new Rect(((float)Screen.width - this.WidthAndHeight.x) / 2f, ((float)Screen.height - this.WidthAndHeight.y) / 2f, this.WidthAndHeight.x, this.WidthAndHeight.y);
		GUI.Box(rect, "Join or Create Room");
		GUILayout.BeginArea(rect);
		GUILayout.Space(40f);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label("Player name:", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		});
		PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName, Array.Empty<GUILayoutOption>());
		GUILayout.Space(158f);
		if (GUI.changed)
		{
			PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(15f);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label("Roomname:", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		});
		this.roomName = GUILayout.TextField(this.roomName, Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Create Room", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		}))
		{
			PhotonNetwork.CreateRoom(this.roomName, new RoomOptions
			{
				MaxPlayers = 10
			}, null);
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Join Room", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		}))
		{
			PhotonNetwork.JoinRoom(this.roomName);
		}
		GUILayout.EndHorizontal();
		if (!string.IsNullOrEmpty(this.ErrorDialog))
		{
			GUILayout.Label(this.ErrorDialog, Array.Empty<GUILayoutOption>());
			if (this.timeToClearDialog < (double)Time.time)
			{
				this.timeToClearDialog = 0.0;
				this.ErrorDialog = "";
			}
		}
		GUILayout.Space(15f);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label(string.Concat(new object[]
		{
			PhotonNetwork.countOfPlayers,
			" users are online in ",
			PhotonNetwork.countOfRooms,
			" rooms."
		}), Array.Empty<GUILayoutOption>());
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Join Random", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		}))
		{
			PhotonNetwork.JoinRandomRoom();
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(15f);
		if (PhotonNetwork.GetRoomList().Length == 0)
		{
			GUILayout.Label("Currently no games are available.", Array.Empty<GUILayoutOption>());
			GUILayout.Label("Rooms will be listed here, when they become available.", Array.Empty<GUILayoutOption>());
		}
		else
		{
			GUILayout.Label(PhotonNetwork.GetRoomList().Length + " rooms available:", Array.Empty<GUILayoutOption>());
			this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, Array.Empty<GUILayoutOption>());
			foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
			{
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Label(string.Concat(new object[]
				{
					roomInfo.Name,
					" ",
					roomInfo.PlayerCount,
					"/",
					roomInfo.MaxPlayers
				}), Array.Empty<GUILayoutOption>());
				if (GUILayout.Button("Join", new GUILayoutOption[]
				{
					GUILayout.Width(150f)
				}))
				{
					PhotonNetwork.JoinRoom(roomInfo.Name);
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndScrollView();
		}
		GUILayout.EndArea();
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0001166B File Offset: 0x0000F86B
	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00011677 File Offset: 0x0000F877
	public void OnPhotonCreateRoomFailed()
	{
		this.ErrorDialog = "Error: Can't create room (room name maybe already used).";
		Debug.Log("OnPhotonCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0001168E File Offset: 0x0000F88E
	public void OnPhotonJoinRoomFailed(object[] cause)
	{
		this.ErrorDialog = "Error: Can't join room (full or unknown room name). " + cause[1];
		Debug.Log("OnPhotonJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
	}

	// Token: 0x0600028E RID: 654 RVA: 0x000116AD File Offset: 0x0000F8AD
	public void OnPhotonRandomJoinFailed()
	{
		this.ErrorDialog = "Error: Can't join random room (none found).";
		Debug.Log("OnPhotonRandomJoinFailed got called. Happens if no room is available (or all full or invisible or closed). JoinrRandom filter-options can limit available rooms.");
	}

	// Token: 0x0600028F RID: 655 RVA: 0x000116C4 File Offset: 0x0000F8C4
	public void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
		PhotonNetwork.LoadLevel(WorkerMenu.SceneNameGame);
	}

	// Token: 0x06000290 RID: 656 RVA: 0x000116DA File Offset: 0x0000F8DA
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("Disconnected from Photon.");
	}

	// Token: 0x06000291 RID: 657 RVA: 0x000116E6 File Offset: 0x0000F8E6
	public void OnFailedToConnectToPhoton(object parameters)
	{
		this.connectFailed = true;
		Debug.Log(string.Concat(new object[]
		{
			"OnFailedToConnectToPhoton. StatusCode: ",
			parameters,
			" ServerAddress: ",
			PhotonNetwork.ServerAddress
		}));
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0001171B File Offset: 0x0000F91B
	public void OnConnectedToMaster()
	{
		Debug.Log("As OnConnectedToMaster() got called, the PhotonServerSetting.AutoJoinLobby must be off. Joining lobby by calling PhotonNetwork.JoinLobby().");
		PhotonNetwork.JoinLobby();
	}

	// Token: 0x040002DC RID: 732
	public GUISkin Skin;

	// Token: 0x040002DD RID: 733
	public Vector2 WidthAndHeight = new Vector2(600f, 400f);

	// Token: 0x040002DE RID: 734
	private string roomName = "myRoom";

	// Token: 0x040002DF RID: 735
	private Vector2 scrollPos = Vector2.zero;

	// Token: 0x040002E0 RID: 736
	private bool connectFailed;

	// Token: 0x040002E1 RID: 737
	public static readonly string SceneNameMenu = "DemoWorker-Scene";

	// Token: 0x040002E2 RID: 738
	public static readonly string SceneNameGame = "DemoWorkerGame-Scene";

	// Token: 0x040002E3 RID: 739
	private string errorDialog;

	// Token: 0x040002E4 RID: 740
	private double timeToClearDialog;
}
