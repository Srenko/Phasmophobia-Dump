using System;

// Token: 0x020001A5 RID: 421
public class MissionVictimName : Mission
{
	// Token: 0x06000B60 RID: 2912 RVA: 0x00045E20 File Offset: 0x00044020
	private void Awake()
	{
		MissionVictimName.instance = this;
		this.SetMissionType();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00045E3A File Offset: 0x0004403A
	public void CompleteMission()
	{
		this.view.RPC("CompletedVictimMissionSync", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x00045540 File Offset: 0x00043740
	[PunRPC]
	private void CompletedVictimMissionSync()
	{
		base.Completed();
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x00045E52 File Offset: 0x00044052
	private void SetMissionType()
	{
		this.type = Mission.MissionType.side;
		this.completed = false;
		this.missionName = LocalisationSystem.GetLocalisedValue("Mission_VictimName");
	}

	// Token: 0x04000B91 RID: 2961
	public static MissionVictimName instance;
}
