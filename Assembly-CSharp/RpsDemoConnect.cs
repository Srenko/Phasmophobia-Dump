using System;
using Photon;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000067 RID: 103
public class RpsDemoConnect : PunBehaviour
{
	// Token: 0x06000240 RID: 576 RVA: 0x0000F4BE File Offset: 0x0000D6BE
	private void Start()
	{
		this.InputField.text = (PlayerPrefs.HasKey("NickName") ? PlayerPrefs.GetString("NickName") : "");
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
	public void ApplyUserIdAndConnect()
	{
		string text = "DemoNick";
		if (this.InputField != null && !string.IsNullOrEmpty(this.InputField.text))
		{
			text = this.InputField.text;
			PlayerPrefs.SetString("NickName", text);
		}
		if (PhotonNetwork.AuthValues == null)
		{
			PhotonNetwork.AuthValues = new AuthenticationValues();
		}
		PhotonNetwork.AuthValues.UserId = text;
		Debug.Log("Nickname: " + text + " userID: " + this.UserId, this);
		PhotonNetwork.playerName = text;
		PhotonNetwork.ConnectUsingSettings("0.5");
		PhotonHandler.StopFallbackSendAckThread();
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0000F580 File Offset: 0x0000D780
	public override void OnConnectedToMaster()
	{
		this.UserId = PhotonNetwork.player.UserId;
		if (PlayerPrefs.HasKey(this.previousRoomPlayerPrefKey))
		{
			Debug.Log("getting previous room from prefs: ");
			this.previousRoom = PlayerPrefs.GetString(this.previousRoomPlayerPrefKey);
			PlayerPrefs.DeleteKey(this.previousRoomPlayerPrefKey);
		}
		if (!string.IsNullOrEmpty(this.previousRoom))
		{
			Debug.Log("ReJoining previous room: " + this.previousRoom);
			PhotonNetwork.ReJoinRoom(this.previousRoom);
			this.previousRoom = null;
			return;
		}
		PhotonNetwork.JoinRandomRoom();
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0000F60C File Offset: 0x0000D80C
	public override void OnJoinedLobby()
	{
		this.OnConnectedToMaster();
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0000F614 File Offset: 0x0000D814
	public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
	{
		Debug.Log("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom(null, new RoomOptions
		{
			MaxPlayers = 2,
			PlayerTtl = 20000
		}, null);
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000F63F File Offset: 0x0000D83F
	public override void OnJoinedRoom()
	{
		Debug.Log("Joined room: " + PhotonNetwork.room.Name);
		this.previousRoom = PhotonNetwork.room.Name;
		PlayerPrefs.SetString(this.previousRoomPlayerPrefKey, this.previousRoom);
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0000F67B File Offset: 0x0000D87B
	public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
	{
		Debug.Log("OnPhotonJoinRoomFailed");
		this.previousRoom = null;
		PlayerPrefs.DeleteKey(this.previousRoomPlayerPrefKey);
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0000F699 File Offset: 0x0000D899
	public override void OnConnectionFail(DisconnectCause cause)
	{
		Debug.Log(string.Concat(new object[]
		{
			"Disconnected due to: ",
			cause,
			". this.previousRoom: ",
			this.previousRoom
		}));
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
	public override void OnPhotonPlayerActivityChanged(PhotonPlayer otherPlayer)
	{
		Debug.Log("OnPhotonPlayerActivityChanged() for " + otherPlayer.NickName + " IsInactive: " + otherPlayer.IsInactive.ToString());
	}

	// Token: 0x0400027B RID: 635
	public InputField InputField;

	// Token: 0x0400027C RID: 636
	public string UserId;

	// Token: 0x0400027D RID: 637
	private string previousRoomPlayerPrefKey = "PUN:Demo:RPS:PreviousRoom";

	// Token: 0x0400027E RID: 638
	public string previousRoom;

	// Token: 0x0400027F RID: 639
	private const string MainSceneName = "DemoRPS-Scene";

	// Token: 0x04000280 RID: 640
	private const string NickNamePlayerPrefsKey = "NickName";
}
