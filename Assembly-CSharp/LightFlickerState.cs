using System;
using UnityEngine;

// Token: 0x02000102 RID: 258
public class LightFlickerState : IState
{
	// Token: 0x060006F9 RID: 1785 RVA: 0x00029704 File Offset: 0x00027904
	public LightFlickerState(GhostAI ghostAI, GhostInteraction ghostInteraction)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0002971C File Offset: 0x0002791C
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		if (LevelController.instance.currentGhostRoom.lightSwitches.Count == 0)
		{
			return;
		}
		int index = Random.Range(0, LevelController.instance.currentGhostRoom.lightSwitches.Count);
		if (LevelController.instance.currentGhostRoom.lightSwitches[index].isOn)
		{
			LevelController.instance.currentGhostRoom.lightSwitches[index].view.RPC("FlickerNetworked", PhotonTargets.All, Array.Empty<object>());
		}
		this.ghostInteraction.CreateInteractionEMF(LevelController.instance.currentGhostRoom.lightSwitches[index].transform.position);
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x040006B7 RID: 1719
	private GhostAI ghostAI;

	// Token: 0x040006B8 RID: 1720
	private GhostInteraction ghostInteraction;
}
