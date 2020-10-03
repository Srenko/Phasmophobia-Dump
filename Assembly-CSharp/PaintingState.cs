using System;

// Token: 0x02000105 RID: 261
public class PaintingState : IState
{
	// Token: 0x06000705 RID: 1797 RVA: 0x000298F4 File Offset: 0x00027AF4
	public PaintingState(GhostAI ghostAI, GhostInteraction ghostInteraction, PhotonObjectInteract obj)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.painting = obj.GetComponent<Painting>();
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x00029916 File Offset: 0x00027B16
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		this.painting.KnockOver();
		this.ghostInteraction.CreateInteractionEMF(this.painting.transform.position);
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x040006BF RID: 1727
	private GhostAI ghostAI;

	// Token: 0x040006C0 RID: 1728
	private GhostInteraction ghostInteraction;

	// Token: 0x040006C1 RID: 1729
	private Painting painting;
}
