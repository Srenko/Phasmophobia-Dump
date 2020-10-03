using System;

// Token: 0x02000109 RID: 265
public class TeleportObjectState : IState
{
	// Token: 0x06000715 RID: 1813 RVA: 0x00029AF0 File Offset: 0x00027CF0
	public TeleportObjectState(GhostAI ghostAI, GhostInteraction ghostInteraction, PhotonObjectInteract obj)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.teleportObj = obj.GetComponent<TeleportableObject>();
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x00029B12 File Offset: 0x00027D12
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		this.teleportObj.Use();
		this.ghostInteraction.CreateInteractionEMF(this.teleportObj.transform.position);
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x040006CA RID: 1738
	private GhostAI ghostAI;

	// Token: 0x040006CB RID: 1739
	private GhostInteraction ghostInteraction;

	// Token: 0x040006CC RID: 1740
	private TeleportableObject teleportObj;
}
