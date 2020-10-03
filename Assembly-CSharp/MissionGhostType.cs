using System;

// Token: 0x02000199 RID: 409
public class MissionGhostType : Mission
{
	// Token: 0x06000B1D RID: 2845 RVA: 0x00045526 File Offset: 0x00043726
	private void Awake()
	{
		MissionGhostType.instance = this;
		this.view = base.GetComponent<PhotonView>();
		this.SetMissionType();
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x00045540 File Offset: 0x00043740
	[PunRPC]
	private void CompletedGhostMissionSync()
	{
		base.Completed();
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x00045548 File Offset: 0x00043748
	public void CheckMissionComplete()
	{
		if (LevelController.instance && LevelController.instance.journalController.GetGhostType() == LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType)
		{
			this.view.RPC("CompletedGhostMissionSync", PhotonTargets.AllBuffered, Array.Empty<object>());
		}
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x000455A1 File Offset: 0x000437A1
	private void SetMissionType()
	{
		this.type = Mission.MissionType.main;
		this.completed = false;
		this.missionName = LocalisationSystem.GetLocalisedValue("Mission_GhostType");
	}

	// Token: 0x04000B73 RID: 2931
	public static MissionGhostType instance;
}
