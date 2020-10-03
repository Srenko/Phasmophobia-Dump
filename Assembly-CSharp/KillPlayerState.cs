using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000101 RID: 257
public class KillPlayerState : IState
{
	// Token: 0x060006F5 RID: 1781 RVA: 0x000295C7 File Offset: 0x000277C7
	public KillPlayerState(GhostAI ghostAI, NavMeshAgent agent, PhotonView view)
	{
		this.ghostAI = ghostAI;
		this.agent = agent;
		this.view = view;
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x000295F0 File Offset: 0x000277F0
	public void Enter()
	{
		this.ghostAI.ghostAudio.PlayOrStopAppearSource(true);
		this.agent.isStopped = true;
		this.ghostAI.ghostAudio.PlaySound(1, false, false);
		this.ghostAI.UnAppear(false);
		this.ghostAI.anim.SetBool("isIdle", true);
		this.ghostAI.anim.SetTrigger("Attack");
		this.ghostAI.playerToKill.StartKillingPlayer(this.ghostAI.playerToKill.view.owner);
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0002968C File Offset: 0x0002788C
	public void Execute()
	{
		this.killTimer -= Time.deltaTime;
		if (this.killTimer < 0f)
		{
			this.ghostAI.Appear(false);
			this.agent.isStopped = false;
			DeadZoneController.instance.SpawnDeathRoom();
			this.killTimer = 100f;
		}
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x000296E5 File Offset: 0x000278E5
	public void Exit()
	{
		this.ghostAI.ghostAudio.PlayOrStopAppearSource(false);
		this.ghostAI.playerToKill = null;
	}

	// Token: 0x040006B3 RID: 1715
	private GhostAI ghostAI;

	// Token: 0x040006B4 RID: 1716
	private NavMeshAgent agent;

	// Token: 0x040006B5 RID: 1717
	private PhotonView view;

	// Token: 0x040006B6 RID: 1718
	private float killTimer = 4.55f;
}
