using System;
using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExitGames.Demos.DemoAnimator
{
	// Token: 0x02000485 RID: 1157
	public class GameManager : PunBehaviour
	{
		// Token: 0x06002412 RID: 9234 RVA: 0x000B0B84 File Offset: 0x000AED84
		private void Start()
		{
			GameManager.Instance = this;
			if (!PhotonNetwork.connected)
			{
				SceneManager.LoadScene("PunBasics-Launcher");
				return;
			}
			if (this.playerPrefab == null)
			{
				Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
				return;
			}
			if (PlayerManager.LocalPlayerInstance == null)
			{
				Debug.Log("We are Instantiating LocalPlayer from " + SceneManagerHelper.ActiveSceneName);
				PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
				return;
			}
			Debug.Log("Ignoring scene load for " + SceneManagerHelper.ActiveSceneName);
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000B0C24 File Offset: 0x000AEE24
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				this.QuitApplication();
			}
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000B0C38 File Offset: 0x000AEE38
		public override void OnPhotonPlayerConnected(PhotonPlayer other)
		{
			Debug.Log("OnPhotonPlayerConnected() " + other.NickName);
			if (PhotonNetwork.isMasterClient)
			{
				Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient.ToString());
				this.LoadArena();
			}
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000B0C84 File Offset: 0x000AEE84
		public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
		{
			Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName);
			if (PhotonNetwork.isMasterClient)
			{
				Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient.ToString());
				this.LoadArena();
			}
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000B0CCF File Offset: 0x000AEECF
		public override void OnLeftRoom()
		{
			SceneManager.LoadScene("PunBasics-Launcher");
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000B0CDB File Offset: 0x000AEEDB
		public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom(true);
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x0004170B File Offset: 0x0003F90B
		public void QuitApplication()
		{
			Application.Quit();
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000B0CE4 File Offset: 0x000AEEE4
		private void LoadArena()
		{
			if (!PhotonNetwork.isMasterClient)
			{
				Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
			}
			Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.room.PlayerCount);
			PhotonNetwork.LoadLevel("PunBasics-Room for " + PhotonNetwork.room.PlayerCount);
		}

		// Token: 0x04002165 RID: 8549
		public static GameManager Instance;

		// Token: 0x04002166 RID: 8550
		[Tooltip("The prefab to use for representing the player")]
		public GameObject playerPrefab;

		// Token: 0x04002167 RID: 8551
		private GameObject instance;
	}
}
