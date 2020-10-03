using System;

// Token: 0x0200010C RID: 268
public class WindowKnockState : IState
{
	// Token: 0x06000722 RID: 1826 RVA: 0x00029F0B File Offset: 0x0002810B
	public WindowKnockState(GhostAI ghostAI, GhostInteraction ghostInteraction, PhotonObjectInteract obj)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.window = obj.GetComponent<Window>();
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00029F30 File Offset: 0x00028130
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		this.window.PlayKnockingSound();
		if (EvidenceController.instance.IsFingerPrintEvidence())
		{
			this.window.SpawnHandPrintEvidence();
		}
		this.ghostInteraction.CreateInteractionEMF(this.window.transform.position);
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x040006D3 RID: 1747
	private GhostAI ghostAI;

	// Token: 0x040006D4 RID: 1748
	private GhostInteraction ghostInteraction;

	// Token: 0x040006D5 RID: 1749
	private Window window;
}
