using System;

// Token: 0x020001A2 RID: 418
public class MissionGhostEvent : Mission
{
	// Token: 0x06000B51 RID: 2897 RVA: 0x00045D12 File Offset: 0x00043F12
	private void Awake()
	{
		MissionGhostEvent.instance = this;
		this.SetMissionType();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x00045D2C File Offset: 0x00043F2C
	public void CompleteMission()
	{
		this.view.RPC("CompletedGhostEventMissionSync", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00045540 File Offset: 0x00043740
	[PunRPC]
	private void CompletedGhostEventMissionSync()
	{
		base.Completed();
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00045D44 File Offset: 0x00043F44
	private void SetMissionType()
	{
		this.type = Mission.MissionType.side;
		this.completed = false;
		this.missionName = LocalisationSystem.GetLocalisedValue("Mission_GhostEventName");
	}

	// Token: 0x04000B8E RID: 2958
	public static MissionGhostEvent instance;
}
