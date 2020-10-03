using System;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class JinnPower : IState
{
	// Token: 0x0600064A RID: 1610 RVA: 0x00022F1C File Offset: 0x0002111C
	public JinnPower(GhostAI ghostAI, GhostInteraction ghostInteraction)
	{
		this.ghostInteraction = ghostInteraction;
		this.ghostAI = ghostAI;
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x00022F3D File Offset: 0x0002113D
	public void Enter()
	{
		if (!LevelController.instance.fuseBox.isOn)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		}
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x00022F60 File Offset: 0x00021160
	public void Execute()
	{
		this.timer -= Time.deltaTime;
		if (this.timer < 0f)
		{
			this.timer = 100f;
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			this.ghostAI.JinnPowerDistanceCheck();
			this.ghostInteraction.CreateInteractionEMF(this.ghostAI.raycastPoint.position);
		}
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x04000627 RID: 1575
	private GhostInteraction ghostInteraction;

	// Token: 0x04000628 RID: 1576
	private GhostAI ghostAI;

	// Token: 0x04000629 RID: 1577
	private float timer = 5f;
}
