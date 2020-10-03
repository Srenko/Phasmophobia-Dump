using System;

// Token: 0x020001A0 RID: 416
public class MissionDirtyWater : Mission
{
	// Token: 0x06000B47 RID: 2887 RVA: 0x00045C6E File Offset: 0x00043E6E
	private void Awake()
	{
		MissionDirtyWater.instance = this;
		this.SetMissionType();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x00045C88 File Offset: 0x00043E88
	public void CompleteMission()
	{
		this.view.RPC("CompletedDirtyWaterMissionSync", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x00045540 File Offset: 0x00043740
	[PunRPC]
	private void CompletedDirtyWaterMissionSync()
	{
		base.Completed();
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x00045CA0 File Offset: 0x00043EA0
	private void SetMissionType()
	{
		this.type = Mission.MissionType.side;
		this.completed = false;
		this.missionName = LocalisationSystem.GetLocalisedValue("Mission_DirtyWater");
	}

	// Token: 0x04000B8C RID: 2956
	public static MissionDirtyWater instance;
}
