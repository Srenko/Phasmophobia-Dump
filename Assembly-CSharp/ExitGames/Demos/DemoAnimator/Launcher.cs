using System;
using Photon;
using UnityEngine;
using UnityEngine.UI;

namespace ExitGames.Demos.DemoAnimator
{
	// Token: 0x02000486 RID: 1158
	public class Launcher : PunBehaviour
	{
		// Token: 0x0600241B RID: 9243 RVA: 0x000B0D3E File Offset: 0x000AEF3E
		private void Awake()
		{
			if (this.loaderAnime == null)
			{
				Debug.LogError("<Color=Red><b>Missing</b></Color> loaderAnime Reference.", this);
			}
			PhotonNetwork.autoJoinLobby = false;
			PhotonNetwork.automaticallySyncScene = true;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000B0D68 File Offset: 0x000AEF68
		public void Connect()
		{
			this.feedbackText.text = "";
			this.isConnecting = true;
			this.controlPanel.SetActive(false);
			if (this.loaderAnime != null)
			{
				this.loaderAnime.StartLoaderAnimation();
			}
			if (PhotonNetwork.connected)
			{
				this.LogFeedback("Joining Room...");
				PhotonNetwork.JoinRandomRoom();
				return;
			}
			this.LogFeedback("Connecting...");
			PhotonNetwork.ConnectUsingSettings(this._gameVersion);
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000B0DE1 File Offset: 0x000AEFE1
		private void LogFeedback(string message)
		{
			if (this.feedbackText == null)
			{
				return;
			}
			Text text = this.feedbackText;
			text.text = text.text + Environment.NewLine + message;
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000B0E10 File Offset: 0x000AF010
		public override void OnConnectedToMaster()
		{
			Debug.Log("Region:" + PhotonNetwork.networkingPeer.CloudRegion);
			if (this.isConnecting)
			{
				this.LogFeedback("OnConnectedToMaster: Next -> try to Join Random Room");
				Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");
				PhotonNetwork.JoinRandomRoom();
			}
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000B0E5E File Offset: 0x000AF05E
		public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
		{
			this.LogFeedback("<Color=Red>OnPhotonRandomJoinFailed</Color>: Next -> Create a new Room");
			Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
			PhotonNetwork.CreateRoom(null, new RoomOptions
			{
				MaxPlayers = this.maxPlayersPerRoom
			}, null);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000B0E8E File Offset: 0x000AF08E
		public override void OnDisconnectedFromPhoton()
		{
			this.LogFeedback("<Color=Red>OnDisconnectedFromPhoton</Color>");
			Debug.LogError("DemoAnimator/Launcher:Disconnected");
			this.loaderAnime.StopLoaderAnimation();
			this.isConnecting = false;
			this.controlPanel.SetActive(true);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000B0EC4 File Offset: 0x000AF0C4
		public override void OnJoinedRoom()
		{
			this.LogFeedback("<Color=Green>OnJoinedRoom</Color> with " + PhotonNetwork.room.PlayerCount + " Player(s)");
			Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
			if (PhotonNetwork.room.PlayerCount == 1)
			{
				Debug.Log("We load the 'Room for 1' ");
				PhotonNetwork.LoadLevel("PunBasics-Room for 1");
			}
		}

		// Token: 0x04002168 RID: 8552
		[Tooltip("The Ui Panel to let the user enter name, connect and play")]
		public GameObject controlPanel;

		// Token: 0x04002169 RID: 8553
		[Tooltip("The Ui Text to inform the user about the connection progress")]
		public Text feedbackText;

		// Token: 0x0400216A RID: 8554
		[Tooltip("The maximum number of players per room")]
		public byte maxPlayersPerRoom = 4;

		// Token: 0x0400216B RID: 8555
		[Tooltip("The UI Loader Anime")]
		public LoaderAnime loaderAnime;

		// Token: 0x0400216C RID: 8556
		private bool isConnecting;

		// Token: 0x0400216D RID: 8557
		private string _gameVersion = "1";
	}
}
