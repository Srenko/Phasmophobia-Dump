using System;
using System.Collections;
using System.Collections.Generic;
using Photon;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

// Token: 0x02000114 RID: 276
[RequireComponent(typeof(PhotonView))]
public class GameController : Photon.MonoBehaviour
{
	// Token: 0x0600075C RID: 1884 RVA: 0x0002AFFC File Offset: 0x000291FC
	private void Awake()
	{
		GameController.instance = this;
		this.view = base.GetComponent<PhotonView>();
		if (FileBasedPrefs.GetInt("isTutorial", 0) == 0)
		{
			this.levelDifficulty = (Contract.LevelDifficulty)FileBasedPrefs.GetInt("LevelDifficulty", 0);
		}
		else
		{
			this.levelDifficulty = Contract.LevelDifficulty.Amateur;
		}
		FileBasedPrefs.SetInt("PlayerDied", 0);
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0002B04D File Offset: 0x0002924D
	private void Start()
	{
		base.StartCoroutine(this.CheckIfAllPlayersAreConnected());
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x0002B05C File Offset: 0x0002925C
	public float GetAveragePlayerInsanity()
	{
		float num = 0f;
		for (int i = 0; i < this.playersData.Count; i++)
		{
			num += this.playersData[i].player.insanity;
		}
		this.currentAverageSanity = num / (float)this.playersData.Count;
		return this.currentAverageSanity;
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x0002B0B8 File Offset: 0x000292B8
	public void PlayerDied()
	{
		if (PhotonNetwork.isMasterClient && this.AllPlayersAreDead())
		{
			base.StartCoroutine(this.ExitLevelAfterDelay());
		}
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x0002B0D8 File Offset: 0x000292D8
	private bool AllPlayersAreDead()
	{
		foreach (PlayerData playerData in this.playersData)
		{
			if (playerData != null && !playerData.player.isDead)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x0002B13C File Offset: 0x0002933C
	private IEnumerator CheckIfAllPlayersAreConnected()
	{
		if (this.allPlayersAreConnected)
		{
			yield return null;
		}
		yield return new WaitUntil(() => this.playersData.Count == PhotonNetwork.playerList.Length);
		if (this.allPlayersAreConnected)
		{
			yield return null;
		}
		this.allPlayersAreConnected = true;
		this.OnAllPlayersConnected.Invoke();
		yield break;
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x0002B14C File Offset: 0x0002934C
	private void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		for (int i = 0; i < this.playersData.Count; i++)
		{
			if (this.playersData[i].actorID == player.ID)
			{
				this.playersData.RemoveAt(i);
			}
		}
		if (this.AllPlayersAreDead())
		{
			base.StartCoroutine(this.ExitLevelAfterDelay());
		}
		this.OnPlayerDisconnected.Invoke();
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x0002B1B4 File Offset: 0x000293B4
	private void OnPhotonPlayerActivityChanged(PhotonPlayer player)
	{
		if (PhotonNetwork.isMasterClient && player.IsInactive)
		{
			for (int i = 0; i < this.playersData.Count; i++)
			{
				if (this.playersData[i].actorID == player.ID)
				{
					this.playersData[i].player.ForceDropPropsSync();
				}
			}
		}
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x0002B218 File Offset: 0x00029418
	public void RemovePlayersWhenLoading()
	{
		for (int i = 0; i < this.playersData.Count; i++)
		{
			if (this.playersData[i].photonPlayer != PhotonNetwork.player)
			{
				this.playersData[i].player.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x0002B270 File Offset: 0x00029470
	public Player GetPlayerFromActorID(int id)
	{
		for (int i = 0; i < this.playersData.Count; i++)
		{
			if (this.playersData[i].actorID == id)
			{
				return this.playersData[i].player;
			}
		}
		return this.myPlayer.player;
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x0002B2C4 File Offset: 0x000294C4
	private IEnumerator ExitLevelAfterDelay()
	{
		yield return new WaitForSeconds((PhotonNetwork.playerList.Length == 1) ? 4f : 2f);
		this.view.RPC("Exit", PhotonTargets.AllBufferedViaServer, Array.Empty<object>());
		yield break;
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x0002B2D4 File Offset: 0x000294D4
	[PunRPC]
	private void Exit()
	{
		this.isLoadingBackToMenu = true;
		FileBasedPrefs.SetInt("StayInServerRoom", 1);
		if (PhotonNetwork.playerList.Length == 1)
		{
			FileBasedPrefs.SetInt("PlayerDied", 1);
			FileBasedPrefs.SetInt("MissionStatus", 1);
			FileBasedPrefs.SetInt("StayInServerRoom", 1);
			ExitLevel.CheckMissions();
			ExitLevel.CheckExp(true);
			ExitLevel.CheckChallenges(true);
			InventoryManager.RemoveItemsFromInventory();
		}
		else
		{
			FileBasedPrefs.SetInt("MissionStatus", 2);
		}
		if (XRDevice.isPresent)
		{
			this.loadLevel.levelName = "Menu_New";
			this.loadLevel.Trigger();
			return;
		}
		PhotonNetwork.LoadLevel("Menu_New");
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x0002B370 File Offset: 0x00029570
	private void OnApplicationQuit()
	{
		if (Application.isEditor && this.isTutorial)
		{
			FileBasedPrefs.SetInt("isTutorial", 0);
			return;
		}
		if (this.isTutorial)
		{
			FileBasedPrefs.SetInt("MissionStatus", 3);
			FileBasedPrefs.SetInt("setupPhase", 0);
			FileBasedPrefs.SetInt("completedTraining", 1);
			FileBasedPrefs.SetInt("StayInServerRoom", 0);
			return;
		}
		FileBasedPrefs.SetInt("StayInServerRoom", 0);
		FileBasedPrefs.SetInt("MissionStatus", 1);
		if (this.myPlayer.player.isDead)
		{
			FileBasedPrefs.SetInt("PlayerDied", 1);
			InventoryManager.RemoveItemsFromInventory();
			return;
		}
		FileBasedPrefs.SetInt("PlayerDied", 0);
	}

	// Token: 0x04000706 RID: 1798
	public static GameController instance;

	// Token: 0x04000707 RID: 1799
	[HideInInspector]
	public PlayerData myPlayer;

	// Token: 0x04000708 RID: 1800
	[HideInInspector]
	public UnityEvent OnAllPlayersConnected = new UnityEvent();

	// Token: 0x04000709 RID: 1801
	[HideInInspector]
	public UnityEvent OnPlayerSpawned = new UnityEvent();

	// Token: 0x0400070A RID: 1802
	[HideInInspector]
	public UnityEvent OnLocalPlayerSpawned = new UnityEvent();

	// Token: 0x0400070B RID: 1803
	[HideInInspector]
	public UnityEvent OnPlayerDisconnected = new UnityEvent();

	// Token: 0x0400070C RID: 1804
	[HideInInspector]
	public UnityEvent OnGhostSpawned = new UnityEvent();

	// Token: 0x0400070D RID: 1805
	[HideInInspector]
	public List<PlayerData> playersData = new List<PlayerData>();

	// Token: 0x0400070E RID: 1806
	[HideInInspector]
	public float currentAverageSanity;

	// Token: 0x0400070F RID: 1807
	public SteamVR_LoadLevel loadLevel;

	// Token: 0x04000710 RID: 1808
	private PhotonView view;

	// Token: 0x04000711 RID: 1809
	[HideInInspector]
	public bool isTutorial;

	// Token: 0x04000712 RID: 1810
	[HideInInspector]
	public Contract.LevelDifficulty levelDifficulty;

	// Token: 0x04000713 RID: 1811
	[HideInInspector]
	public bool allPlayersAreConnected;

	// Token: 0x04000714 RID: 1812
	[HideInInspector]
	public bool isLoadingBackToMenu;
}
