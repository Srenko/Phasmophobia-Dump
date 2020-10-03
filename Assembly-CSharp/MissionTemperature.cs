using System;
using UnityEngine;

// Token: 0x020001A4 RID: 420
public class MissionTemperature : Mission
{
	// Token: 0x06000B5B RID: 2907 RVA: 0x00045DB6 File Offset: 0x00043FB6
	private void Awake()
	{
		MissionTemperature.instance = this;
		this.SetMissionType();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x00045DD0 File Offset: 0x00043FD0
	public void CompleteMission()
	{
		this.view.RPC("CompletedTemperatureMissionSync", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x00045540 File Offset: 0x00043740
	[PunRPC]
	private void CompletedTemperatureMissionSync()
	{
		base.Completed();
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00045DE8 File Offset: 0x00043FE8
	private void SetMissionType()
	{
		this.type = Mission.MissionType.side;
		this.completed = false;
		this.missionName = ((PlayerPrefs.GetInt("degreesValue") == 0) ? LocalisationSystem.GetLocalisedValue("Mission_Temperature") : LocalisationSystem.GetLocalisedValue("Mission_TemperatureFarenheit"));
	}

	// Token: 0x04000B90 RID: 2960
	public static MissionTemperature instance;
}
