using System;

// Token: 0x02000108 RID: 264
public class SoundState : IState
{
	// Token: 0x06000711 RID: 1809 RVA: 0x00029A98 File Offset: 0x00027C98
	public SoundState(GhostAI ghostAI, GhostInteraction ghostInteraction, PhotonObjectInteract obj)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.sound = obj.GetComponent<Sound>();
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00029ABA File Offset: 0x00027CBA
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		this.sound.Use();
		this.ghostInteraction.CreateInteractionEMF(this.sound.transform.position);
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x040006C7 RID: 1735
	private GhostAI ghostAI;

	// Token: 0x040006C8 RID: 1736
	private GhostInteraction ghostInteraction;

	// Token: 0x040006C9 RID: 1737
	private Sound sound;
}
