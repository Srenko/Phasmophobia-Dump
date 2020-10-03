using System;

// Token: 0x020000F2 RID: 242
public class AnimationObjectState : IState
{
	// Token: 0x060006B2 RID: 1714 RVA: 0x00025650 File Offset: 0x00023850
	public AnimationObjectState(GhostAI ghostAI, GhostInteraction ghostInteraction, PhotonObjectInteract obj)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.animation = obj.GetComponent<AnimationObject>();
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x00025672 File Offset: 0x00023872
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		this.animation.Use();
		this.ghostInteraction.CreateInteractionEMF(this.animation.transform.position);
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x04000674 RID: 1652
	private GhostAI ghostAI;

	// Token: 0x04000675 RID: 1653
	private GhostInteraction ghostInteraction;

	// Token: 0x04000676 RID: 1654
	private AnimationObject animation;
}
