using System;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class MannequinState : IState
{
	// Token: 0x06000701 RID: 1793 RVA: 0x00029878 File Offset: 0x00027A78
	public MannequinState(GhostAI ghostAI, GhostInteraction ghostInteraction, PhotonObjectInteract obj)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.mannequin = obj.GetComponent<Mannequin>();
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x0002989C File Offset: 0x00027A9C
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		if (Random.Range(0, 3) < 2)
		{
			this.mannequin.Teleport();
		}
		else
		{
			this.mannequin.Rotate();
		}
		this.ghostInteraction.CreateInteractionEMF(this.mannequin.transform.position);
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x040006BC RID: 1724
	private GhostAI ghostAI;

	// Token: 0x040006BD RID: 1725
	private GhostInteraction ghostInteraction;

	// Token: 0x040006BE RID: 1726
	private Mannequin mannequin;
}
