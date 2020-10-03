using System;

// Token: 0x0200019E RID: 414
public class MissionCrucifix : Mission
{
	// Token: 0x06000B3D RID: 2877 RVA: 0x00045BCA File Offset: 0x00043DCA
	private void Awake()
	{
		MissionCrucifix.instance = this;
		this.SetMissionType();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x00045BE4 File Offset: 0x00043DE4
	public void CompleteMission()
	{
		this.view.RPC("CompletedCrucifixMissionSync", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x00045540 File Offset: 0x00043740
	[PunRPC]
	private void CompletedCrucifixMissionSync()
	{
		base.Completed();
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x00045BFC File Offset: 0x00043DFC
	private void SetMissionType()
	{
		this.type = Mission.MissionType.side;
		this.completed = false;
		this.missionName = LocalisationSystem.GetLocalisedValue("Mission_CrucifixName");
	}

	// Token: 0x04000B8A RID: 2954
	public static MissionCrucifix instance;
}
