using System;

// Token: 0x0200019D RID: 413
public class MissionCapturePhoto : Mission
{
	// Token: 0x06000B38 RID: 2872 RVA: 0x00045B78 File Offset: 0x00043D78
	private void Awake()
	{
		MissionCapturePhoto.instance = this;
		this.SetMissionType();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x00045B92 File Offset: 0x00043D92
	public void CompleteMission()
	{
		this.view.RPC("CompletedCapturePhotoMissionSync", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x00045540 File Offset: 0x00043740
	[PunRPC]
	private void CompletedCapturePhotoMissionSync()
	{
		base.Completed();
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x00045BAA File Offset: 0x00043DAA
	private void SetMissionType()
	{
		this.type = Mission.MissionType.side;
		this.completed = false;
		this.missionName = LocalisationSystem.GetLocalisedValue("Mission_CapturePhoto");
	}

	// Token: 0x04000B89 RID: 2953
	public static MissionCapturePhoto instance;
}
