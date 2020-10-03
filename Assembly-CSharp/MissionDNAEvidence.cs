using System;

// Token: 0x0200019F RID: 415
public class MissionDNAEvidence : Mission
{
	// Token: 0x06000B42 RID: 2882 RVA: 0x00045C1C File Offset: 0x00043E1C
	private void Awake()
	{
		MissionDNAEvidence.instance = this;
		this.SetMissionType();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x00045C36 File Offset: 0x00043E36
	public void CompleteMission()
	{
		this.view.RPC("CompletedDNAMissionSync", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x00045540 File Offset: 0x00043740
	[PunRPC]
	private void CompletedDNAMissionSync()
	{
		base.Completed();
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x00045C4E File Offset: 0x00043E4E
	private void SetMissionType()
	{
		this.type = Mission.MissionType.side;
		this.completed = false;
		this.missionName = LocalisationSystem.GetLocalisedValue("Mission_DNAEvidence");
	}

	// Token: 0x04000B8B RID: 2955
	public static MissionDNAEvidence instance;
}
