using System;

// Token: 0x02000107 RID: 263
public class SinkState : IState
{
	// Token: 0x0600070D RID: 1805 RVA: 0x00029A28 File Offset: 0x00027C28
	public SinkState(GhostAI ghostAI, GhostInteraction ghostInteraction, PhotonObjectInteract obj)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.sink = obj.GetComponent<Sink>();
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x00029A4C File Offset: 0x00027C4C
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		this.sink.Use();
		this.sink.SpawnDirtyWater();
		this.ghostInteraction.CreateInteractionEMF(this.sink.transform.position);
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x040006C4 RID: 1732
	private GhostAI ghostAI;

	// Token: 0x040006C5 RID: 1733
	private GhostInteraction ghostInteraction;

	// Token: 0x040006C6 RID: 1734
	private Sink sink;
}
