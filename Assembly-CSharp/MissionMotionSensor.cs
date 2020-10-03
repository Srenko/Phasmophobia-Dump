using System;

// Token: 0x020001A3 RID: 419
public class MissionMotionSensor : Mission
{
	// Token: 0x06000B56 RID: 2902 RVA: 0x00045D64 File Offset: 0x00043F64
	private void Awake()
	{
		MissionMotionSensor.instance = this;
		this.SetMissionType();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x00045D7E File Offset: 0x00043F7E
	public void CompleteMission()
	{
		this.view.RPC("CompletedMotionMissionSync", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00045540 File Offset: 0x00043740
	[PunRPC]
	private void CompletedMotionMissionSync()
	{
		base.Completed();
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00045D96 File Offset: 0x00043F96
	private void SetMissionType()
	{
		this.type = Mission.MissionType.side;
		this.completed = false;
		this.missionName = LocalisationSystem.GetLocalisedValue("Mission_MotionSensor");
	}

	// Token: 0x04000B8F RID: 2959
	public static MissionMotionSensor instance;
}
