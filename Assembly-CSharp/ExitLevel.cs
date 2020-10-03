using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

// Token: 0x020001C5 RID: 453
public class ExitLevel : MonoBehaviour
{
	// Token: 0x06000C62 RID: 3170 RVA: 0x0004E6C2 File Offset: 0x0004C8C2
	public void StartAttemptExitLevel()
	{
		base.StopAllCoroutines();
		if (this.isExiting)
		{
			base.StartCoroutine(this.AttemptExitLevel());
		}
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x0004E6E0 File Offset: 0x0004C8E0
	public bool ThereAreAlivePlayersOutsideTheTruck()
	{
		bool result = false;
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i] != null && GameController.instance.playersData[i].player != null && !GameController.instance.playersData[i].player.isDead && !this.trigger.playersInTruck.Contains(GameController.instance.playersData[i].player))
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x0004E781 File Offset: 0x0004C981
	[PunRPC]
	private void PlayTruckStartUpSound()
	{
		this.source.clip = this.startExitSound;
		this.source.Play();
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x0004E79F File Offset: 0x0004C99F
	[PunRPC]
	private void PlayTruckStopSound()
	{
		this.source.clip = this.stopExitSound;
		this.source.Play();
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x0004E7BD File Offset: 0x0004C9BD
	private IEnumerator AttemptExitLevel()
	{
		this.view.RPC("PlayTruckStartUpSound", PhotonTargets.All, Array.Empty<object>());
		yield return new WaitForSeconds(9f);
		if (!this.isExiting)
		{
			this.view.RPC("PlayTruckStopSound", PhotonTargets.All, Array.Empty<object>());
			yield return null;
		}
		if (this.trigger.playersInTruck.Count == 0)
		{
			this.isExiting = false;
			this.view.RPC("PlayTruckStopSound", PhotonTargets.All, Array.Empty<object>());
			yield return null;
		}
		if (this.ThereAreAlivePlayersOutsideTheTruck())
		{
			this.isExiting = false;
			this.view.RPC("PlayTruckStopSound", PhotonTargets.All, Array.Empty<object>());
			yield return null;
		}
		if (this.isExiting)
		{
			this.view.RPC("Exit", PhotonTargets.AllBufferedViaServer, Array.Empty<object>());
		}
		yield break;
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x0004E7CC File Offset: 0x0004C9CC
	[PunRPC]
	private void Exit()
	{
		if (GameController.instance)
		{
			GameController.instance.isLoadingBackToMenu = true;
		}
		if (PhotonNetwork.isMasterClient)
		{
			this.view.RPC("SyncPhotoValue", PhotonTargets.AllBuffered, new object[]
			{
				EvidenceController.instance.totalEvidenceFoundInPhotos
			});
		}
		if (GameController.instance.isTutorial)
		{
			FileBasedPrefs.SetInt("MissionStatus", 3);
			FileBasedPrefs.SetInt("setupPhase", 0);
			FileBasedPrefs.SetInt("completedTraining", 1);
			FileBasedPrefs.SetInt("StayInServerRoom", 0);
			if (MissionGhostType.instance)
			{
				MissionGhostType.instance.CheckMissionComplete();
			}
		}
		else if (!GameController.instance.myPlayer.player.isDead)
		{
			FileBasedPrefs.SetInt("MissionStatus", 1);
			FileBasedPrefs.SetInt("StayInServerRoom", 1);
			ExitLevel.CheckMissions();
			ExitLevel.CheckExp(false);
			ExitLevel.CheckChallenges(false);
			if (!this.itemSpawner.hasSpawnedOtherItems)
			{
				DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.completeWithDefaultItems, 1);
			}
		}
		else
		{
			FileBasedPrefs.SetInt("PlayerDied", 1);
			FileBasedPrefs.SetInt("MissionStatus", 1);
			FileBasedPrefs.SetInt("StayInServerRoom", 1);
			ExitLevel.CheckMissions();
			ExitLevel.CheckExp(true);
			ExitLevel.CheckChallenges(true);
			InventoryManager.RemoveItemsFromInventory();
		}
		base.StartCoroutine(this.LoadLevelAfterDelay());
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x0004E912 File Offset: 0x0004CB12
	private IEnumerator LoadLevelAfterDelay()
	{
		yield return new WaitForSeconds(2f);
		if (XRDevice.isPresent)
		{
			GameController.instance.loadLevel.levelName = "Menu_New";
			GameController.instance.loadLevel.Trigger();
		}
		else
		{
			AsyncOperation async = SceneManager.LoadSceneAsync("Menu_New");
			while (!async.isDone)
			{
				if (async.progress == 0.9f)
				{
					GameController.instance.myPlayer.player.pcCanvas.LoadingGame();
				}
				yield return null;
			}
			GameController.instance.myPlayer.player.gameObject.SetActive(false);
			async = null;
		}
		yield break;
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x0004E91C File Offset: 0x0004CB1C
	public static void CheckMissions()
	{
		if (MissionGhostType.instance)
		{
			MissionGhostType.instance.CheckMissionComplete();
		}
		PlayerPrefs.SetInt("MissionType", (int)LevelController.instance.type);
		foreach (Mission mission in MissionManager.instance.currentMissions)
		{
			if (mission.type == Mission.MissionType.main)
			{
				PlayerPrefs.SetInt("MainMission", mission.completed ? 1 : 0);
			}
			else if (mission.type == Mission.MissionType.side)
			{
				if (mission.sideMissionID == 1)
				{
					PlayerPrefs.SetInt("SideMission1", mission.completed ? 1 : 0);
				}
				else if (mission.sideMissionID == 2)
				{
					PlayerPrefs.SetInt("SideMission2", mission.completed ? 1 : 0);
				}
				else if (mission.sideMissionID == 3)
				{
					PlayerPrefs.SetInt("SideMission3", mission.completed ? 1 : 0);
				}
			}
		}
		PlayerPrefs.SetInt("DNAMission", EvidenceController.instance.foundGhostDNA ? 1 : 0);
		int num = 0;
		num += PlayerPrefs.GetInt("MainMission");
		num += PlayerPrefs.GetInt("SideMission1");
		num += PlayerPrefs.GetInt("SideMission2");
		num += PlayerPrefs.GetInt("SideMission3");
		if (num > 0)
		{
			DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.CompleteObjectives, num);
		}
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x0004EA88 File Offset: 0x0004CC88
	public static void CheckExp(bool isDead)
	{
		int num = 0;
		int @int = FileBasedPrefs.GetInt("myTotalExp", 0);
		if (LevelController.instance.type == LevelController.levelType.small)
		{
			num += PlayerPrefs.GetInt("MainMission") * 25;
		}
		else if (LevelController.instance.type == LevelController.levelType.medium)
		{
			num += PlayerPrefs.GetInt("MainMission") * 35;
		}
		else if (LevelController.instance.type == LevelController.levelType.large)
		{
			num += PlayerPrefs.GetInt("MainMission") * 45;
		}
		num += PlayerPrefs.GetInt("SideMission1") * 20;
		num += PlayerPrefs.GetInt("SideMission2") * 20;
		num += PlayerPrefs.GetInt("SideMission3") * 20;
		num += PlayerPrefs.GetInt("DNAMission") * 15 * (int)(LevelController.instance.type + 1);
		if (isDead)
		{
			num /= 2;
		}
		else if (GameController.instance.levelDifficulty == Contract.LevelDifficulty.Intermediate)
		{
			num = (int)((double)num * 1.5);
		}
		else if (GameController.instance.levelDifficulty == Contract.LevelDifficulty.Professional)
		{
			num *= 2;
		}
		FileBasedPrefs.SetInt("totalExp", num);
		FileBasedPrefs.SetInt("myTotalExp", @int + num);
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x0004EB9C File Offset: 0x0004CD9C
	public static void CheckChallenges(bool isDead)
	{
		DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.playContracts, 1);
		if (PhotonNetwork.playerList.Length > 1)
		{
			DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.playTogether, 1);
		}
		if (LevelController.instance.type == LevelController.levelType.medium)
		{
			DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.PlayAMediumMap, 1);
		}
		foreach (Mission mission in MissionManager.instance.currentMissions)
		{
			if (mission.type == Mission.MissionType.main && mission.completed)
			{
				DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.DiscoverGhostType, 1);
			}
		}
		if (EvidenceController.instance.foundGhostDNA)
		{
			DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.FindDNAEvidence, 1);
		}
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x0004EC5C File Offset: 0x0004CE5C
	[PunRPC]
	private void SyncPhotoValue(int value)
	{
		PlayerPrefs.SetInt("PhotosMission", value);
		if (value >= 50)
		{
			DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.PhotoReward, 1);
		}
	}

	// Token: 0x04000CEA RID: 3306
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000CEB RID: 3307
	[HideInInspector]
	public bool isExiting;

	// Token: 0x04000CEC RID: 3308
	[SerializeField]
	private ExitLevelTrigger trigger;

	// Token: 0x04000CED RID: 3309
	[SerializeField]
	private AudioSource source;

	// Token: 0x04000CEE RID: 3310
	[SerializeField]
	private AudioClip startExitSound;

	// Token: 0x04000CEF RID: 3311
	[SerializeField]
	private AudioClip stopExitSound;

	// Token: 0x04000CF0 RID: 3312
	[SerializeField]
	private ItemSpawner itemSpawner;
}
