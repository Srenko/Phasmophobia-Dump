using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000E6 RID: 230
public class BansheePower : IState
{
	// Token: 0x06000645 RID: 1605 RVA: 0x00022D3F File Offset: 0x00020F3F
	public BansheePower(GhostAI ghostAI, GhostInteraction ghostInteraction, GhostAudio ghostAudio, NavMeshAgent agent, LayerMask mask)
	{
		this.ghostInteraction = ghostInteraction;
		this.ghostAudio = ghostAudio;
		this.ghostAI = ghostAI;
		this.agent = agent;
		this.mask = mask;
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x00022D78 File Offset: 0x00020F78
	public void Enter()
	{
		if (!(this.ghostAI.bansheeTarget.currentRoom != LevelController.instance.outsideRoom))
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		Vector3 zero = Vector3.zero;
		if (this.MoveToTargetPlayerPosition(out zero))
		{
			this.agent.destination = zero;
			return;
		}
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x00022DE0 File Offset: 0x00020FE0
	public void Execute()
	{
		this.timer -= Time.deltaTime;
		if (this.timer < 0f)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (!Physics.Linecast(this.ghostAI.transform.position, this.ghostAI.bansheeTarget.transform.position, this.mask))
		{
			this.ghostAI.ChangeState(GameController.instance.isTutorial ? GhostAI.States.idle : GhostAI.States.hunting, null, null);
		}
		if (Vector3.Distance(this.ghostAI.transform.position, this.agent.destination) < 1f)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		}
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x00022EA4 File Offset: 0x000210A4
	public void Exit()
	{
		this.ghostAudio.StopSound();
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x00022EB4 File Offset: 0x000210B4
	private bool MoveToTargetPlayerPosition(out Vector3 pos)
	{
		float num = Random.Range(1f, 2f);
		NavMeshHit navMeshHit;
		if (NavMesh.SamplePosition(Random.insideUnitSphere * num + this.ghostAI.bansheeTarget.transform.position, out navMeshHit, num, 1))
		{
			pos = navMeshHit.position;
			return true;
		}
		pos = Vector3.zero;
		return false;
	}

	// Token: 0x04000621 RID: 1569
	private GhostInteraction ghostInteraction;

	// Token: 0x04000622 RID: 1570
	private GhostAudio ghostAudio;

	// Token: 0x04000623 RID: 1571
	private GhostAI ghostAI;

	// Token: 0x04000624 RID: 1572
	private NavMeshAgent agent;

	// Token: 0x04000625 RID: 1573
	private LayerMask mask;

	// Token: 0x04000626 RID: 1574
	private float timer = 20f;
}
