using System;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class CarAlarmState : IState
{
	// Token: 0x060006BE RID: 1726 RVA: 0x00025B04 File Offset: 0x00023D04
	public CarAlarmState(GhostAI ghostAI, GhostInteraction ghostInteraction)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00025B1C File Offset: 0x00023D1C
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		if (LevelController.instance.car != null && Random.Range(0, 2) == 1)
		{
			if (Vector3.Distance(LevelController.instance.currentGhost.transform.position, LevelController.instance.car.transform.position) > 2f)
			{
				return;
			}
			LevelController.instance.car.view.RPC("TurnAlarmOn", PhotonTargets.All, Array.Empty<object>());
			this.ghostInteraction.CreateInteractionEMF(LevelController.instance.car.raycastSpot.position);
		}
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x0400067F RID: 1663
	private GhostAI ghostAI;

	// Token: 0x04000680 RID: 1664
	private GhostInteraction ghostInteraction;
}
