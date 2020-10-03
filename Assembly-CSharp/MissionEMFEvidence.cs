using System;

// Token: 0x020001A1 RID: 417
public class MissionEMFEvidence : Mission
{
	// Token: 0x06000B4C RID: 2892 RVA: 0x00045CC0 File Offset: 0x00043EC0
	private void Awake()
	{
		MissionEMFEvidence.instance = this;
		this.SetMissionType();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00045CDA File Offset: 0x00043EDA
	public void CompleteMission()
	{
		this.view.RPC("CompletedEMFMissionSync", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00045540 File Offset: 0x00043740
	[PunRPC]
	private void CompletedEMFMissionSync()
	{
		base.Completed();
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x00045CF2 File Offset: 0x00043EF2
	private void SetMissionType()
	{
		this.type = Mission.MissionType.side;
		this.completed = false;
		this.missionName = LocalisationSystem.GetLocalisedValue("Mission_EMFEvidence");
	}

	// Token: 0x04000B8D RID: 2957
	public static MissionEMFEvidence instance;
}
