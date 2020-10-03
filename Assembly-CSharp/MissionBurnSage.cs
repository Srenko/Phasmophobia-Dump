using System;

// Token: 0x0200019C RID: 412
public class MissionBurnSage : Mission
{
	// Token: 0x06000B33 RID: 2867 RVA: 0x00045B26 File Offset: 0x00043D26
	private void Awake()
	{
		MissionBurnSage.instance = this;
		this.SetMissionType();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00045B40 File Offset: 0x00043D40
	public void CompleteMission()
	{
		this.view.RPC("CompletedBurnSageMissionSync", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x00045540 File Offset: 0x00043740
	[PunRPC]
	private void CompletedBurnSageMissionSync()
	{
		base.Completed();
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x00045B58 File Offset: 0x00043D58
	private void SetMissionType()
	{
		this.type = Mission.MissionType.side;
		this.completed = false;
		this.missionName = LocalisationSystem.GetLocalisedValue("Mission_BurnSage");
	}

	// Token: 0x04000B88 RID: 2952
	public static MissionBurnSage instance;
}
