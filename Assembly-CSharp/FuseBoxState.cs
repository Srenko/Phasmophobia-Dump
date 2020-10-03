using System;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class FuseBoxState : IState
{
	// Token: 0x060006E7 RID: 1767 RVA: 0x00027E68 File Offset: 0x00026068
	public FuseBoxState(GhostAI ghostAI, GhostInteraction ghostInteraction)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00027E80 File Offset: 0x00026080
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		if (GameController.instance.isTutorial)
		{
			return;
		}
		if (LevelController.instance.fuseBox.isOn)
		{
			if (this.ghostAI.ghostInfo.ghostTraits.ghostType != GhostTraits.Type.Jinn)
			{
				this.ghostInteraction.CreateInteractionEMF(LevelController.instance.fuseBox.transform.position);
				LevelController.instance.fuseBox.TurnOff();
				return;
			}
		}
		else if (Random.Range(0, 5) == 1)
		{
			this.ghostInteraction.CreateInteractionEMF(LevelController.instance.fuseBox.transform.position);
			LevelController.instance.fuseBox.Use();
		}
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x040006A2 RID: 1698
	private GhostAI ghostAI;

	// Token: 0x040006A3 RID: 1699
	private GhostInteraction ghostInteraction;
}
