using System;
using System.Collections;
using System.Collections.Generic;
using Photon;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200019B RID: 411
[RequireComponent(typeof(PhotonView))]
public class MissionManager : Photon.MonoBehaviour
{
	// Token: 0x06000B25 RID: 2853 RVA: 0x00045694 File Offset: 0x00043894
	private void Awake()
	{
		MissionManager.instance = this;
		this.view = base.GetComponent<PhotonView>();
		this.CreateMissionsList();
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x000456AE File Offset: 0x000438AE
	private void Start()
	{
		this.SetGiveQuests();
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x000456AE File Offset: 0x000438AE
	private void OnJoinedRoom()
	{
		this.SetGiveQuests();
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x000456AE File Offset: 0x000438AE
	private void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		this.SetGiveQuests();
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x000456B8 File Offset: 0x000438B8
	private void SetGiveQuests()
	{
		if (PhotonNetwork.isMasterClient)
		{
			if (this.hasSet)
			{
				return;
			}
			this.hasSet = true;
			if (!this.hasSetupMissions)
			{
				if (LevelController.instance.currentGhost == null)
				{
					GameController.instance.OnGhostSpawned.AddListener(new UnityAction(this.SetMissionDescription));
				}
				else
				{
					this.SetMissionDescription();
				}
			}
			if (GameController.instance.playersData.Count != PhotonNetwork.playerList.Length)
			{
				GameController.instance.OnAllPlayersConnected.AddListener(new UnityAction(this.StartQuests));
				return;
			}
			this.StartQuests();
		}
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x00045756 File Offset: 0x00043956
	private void StartQuests()
	{
		if (!this.hasSetupMissions)
		{
			base.StartCoroutine(this.GiveQuests());
		}
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x0004576D File Offset: 0x0004396D
	private IEnumerator GiveQuests()
	{
		this.view.RPC("SetMission", PhotonTargets.AllBuffered, new object[]
		{
			1,
			Random.Range(0, this.mainMissions.Count),
			1
		});
		yield return new WaitForSeconds(0.5f);
		this.view.RPC("SetMission", PhotonTargets.AllBuffered, new object[]
		{
			2,
			Random.Range(0, this.sideMissions.Count),
			2
		});
		yield return new WaitForSeconds(0.5f);
		this.view.RPC("SetMission", PhotonTargets.AllBuffered, new object[]
		{
			2,
			Random.Range(0, this.sideMissions.Count),
			3
		});
		yield return new WaitForSeconds(0.5f);
		this.view.RPC("SetMission", PhotonTargets.AllBuffered, new object[]
		{
			2,
			Random.Range(0, this.sideMissions.Count),
			4
		});
		yield break;
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x0004577C File Offset: 0x0004397C
	[PunRPC]
	private void SetMission(int typeID, int missionID, int textUIID)
	{
		this.hasSetupMissions = true;
		this.CreateMissionsList();
		Text text;
		if (textUIID == 1)
		{
			text = this.mainMissionText;
		}
		else if (textUIID == 2)
		{
			text = this.sideMissionText;
		}
		else if (textUIID == 3)
		{
			text = this.side2MissionText;
		}
		else
		{
			text = this.hiddenMissionText;
		}
		if (typeID == 1)
		{
			this.AddMission(this.mainMissions[missionID], Mission.MissionType.main, text);
			return;
		}
		if (typeID == 2)
		{
			this.AddMission(this.sideMissions[missionID], Mission.MissionType.side, text);
		}
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x000457F8 File Offset: 0x000439F8
	private void CreateMissionsList()
	{
		if (this.hasCreatedMissions)
		{
			return;
		}
		this.hasCreatedMissions = true;
		int @int = PlayerPrefs.GetInt("highestLevel");
		this.sideMissions.Add(typeof(MissionCapturePhoto));
		this.sideMissions.Add(typeof(MissionEMFEvidence));
		this.sideMissions.Add(typeof(MissionBurnSage));
		if (@int >= 6)
		{
			this.sideMissions.Add(typeof(MissionMotionSensor));
		}
		this.sideMissions.Add(typeof(MissionTemperature));
		this.sideMissions.Add(typeof(MissionDirtyWater));
		this.sideMissions.Add(typeof(MissionCrucifix));
		this.sideMissions.Add(typeof(MissionGhostEvent));
		this.mainMissions.Add(typeof(MissionGhostType));
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x000458DF File Offset: 0x00043ADF
	private void AddMission(Type mission, Mission.MissionType type, Text text)
	{
		this.AddMissionComponent(mission, type, text);
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x000458EC File Offset: 0x00043AEC
	private void AddMissionComponent(Type mission, Mission.MissionType type, Text text)
	{
		if (type == Mission.MissionType.main)
		{
			for (int i = 0; i < this.mainMissions.Count; i++)
			{
				if (this.mainMissions[i] == mission)
				{
					Mission mission2 = base.gameObject.AddComponent(this.mainMissions[i]) as Mission;
					this.currentMissions.Add(mission2);
					mission2.myText = text;
					mission2.sideMissionID = 0;
					mission2.SetUIText();
					this.mainMissions.RemoveAt(i);
				}
			}
			return;
		}
		if (type == Mission.MissionType.side)
		{
			this.missionIndex++;
			for (int j = 0; j < this.sideMissions.Count; j++)
			{
				if (this.sideMissions[j] == mission)
				{
					Mission mission3 = base.gameObject.AddComponent(this.sideMissions[j]) as Mission;
					this.currentMissions.Add(mission3);
					mission3.myText = text;
					mission3.sideMissionID = this.missionIndex;
					mission3.SetUIText();
					this.sideMissions.RemoveAt(j);
				}
			}
		}
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x00045A00 File Offset: 0x00043C00
	private void SetMissionDescription()
	{
		if (PhotonNetwork.isMasterClient)
		{
			this.view.RPC("SetMissionDescriptionSynced", PhotonTargets.AllBuffered, new object[]
			{
				LevelController.instance.currentGhost.ghostInfo.ghostTraits.isShy,
				LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostName
			});
		}
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x00045A68 File Offset: 0x00043C68
	[PunRPC]
	private void SetMissionDescriptionSynced(bool isShy, string ghostName)
	{
		string text = LocalisationSystem.GetLocalisedValue("WhiteBoard_FirstSentence") + " " + ghostName + ".";
		text = text + " " + (isShy ? LocalisationSystem.GetLocalisedValue("WhiteBoard_GhostResponse2") : LocalisationSystem.GetLocalisedValue("WhiteBoard_GhostResponse1"));
		string localisedValue = LocalisationSystem.GetLocalisedValue("WhiteBoard_SecondSentence");
		string localisedValue2 = LocalisationSystem.GetLocalisedValue("WhiteBoard_ThirdSentence");
		this.missionDescription.text = string.Concat(new string[]
		{
			text,
			" ",
			localisedValue,
			" ",
			localisedValue2
		});
	}

	// Token: 0x04000B7A RID: 2938
	public static MissionManager instance;

	// Token: 0x04000B7B RID: 2939
	private List<Type> mainMissions = new List<Type>();

	// Token: 0x04000B7C RID: 2940
	private List<Type> sideMissions = new List<Type>();

	// Token: 0x04000B7D RID: 2941
	[SerializeField]
	private Text mainMissionText;

	// Token: 0x04000B7E RID: 2942
	[SerializeField]
	private Text sideMissionText;

	// Token: 0x04000B7F RID: 2943
	[SerializeField]
	private Text side2MissionText;

	// Token: 0x04000B80 RID: 2944
	[SerializeField]
	private Text hiddenMissionText;

	// Token: 0x04000B81 RID: 2945
	[SerializeField]
	private Text missionDescription;

	// Token: 0x04000B82 RID: 2946
	public List<Mission> currentMissions = new List<Mission>();

	// Token: 0x04000B83 RID: 2947
	private PhotonView view;

	// Token: 0x04000B84 RID: 2948
	private bool hasCreatedMissions;

	// Token: 0x04000B85 RID: 2949
	private int missionIndex;

	// Token: 0x04000B86 RID: 2950
	private bool hasSet;

	// Token: 0x04000B87 RID: 2951
	private bool hasSetupMissions;
}
