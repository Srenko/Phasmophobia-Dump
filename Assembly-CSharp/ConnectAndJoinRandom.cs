using System;
using Photon;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class ConnectAndJoinRandom : Photon.MonoBehaviour
{
	// Token: 0x0600059B RID: 1435 RVA: 0x0001FF45 File Offset: 0x0001E145
	public virtual void Start()
	{
		PhotonNetwork.autoJoinLobby = false;
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0001FF50 File Offset: 0x0001E150
	public virtual void Update()
	{
		if (this.ConnectInUpdate && this.AutoConnect && !PhotonNetwork.connected)
		{
			Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");
			this.ConnectInUpdate = false;
			PhotonNetwork.ConnectUsingSettings(this.Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
		}
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0001FFAA File Offset: 0x0001E1AA
	public virtual void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
		PhotonNetwork.JoinRandomRoom();
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0001FFBC File Offset: 0x0001E1BC
	public virtual void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
		PhotonNetwork.JoinRandomRoom();
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0001FFCE File Offset: 0x0001E1CE
	public virtual void OnPhotonRandomJoinFailed()
	{
		Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
		PhotonNetwork.CreateRoom(null, new RoomOptions
		{
			MaxPlayers = 4
		}, null);
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x0001FFEE File Offset: 0x0001E1EE
	public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		Debug.LogError("Cause: " + cause);
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00020005 File Offset: 0x0001E205
	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
	}

	// Token: 0x040005BA RID: 1466
	public bool AutoConnect = true;

	// Token: 0x040005BB RID: 1467
	public byte Version = 1;

	// Token: 0x040005BC RID: 1468
	private bool ConnectInUpdate = true;
}
