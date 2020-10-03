using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000E8 RID: 232
public class PhantomPower : IState
{
	// Token: 0x0600064E RID: 1614 RVA: 0x00022FCB File Offset: 0x000211CB
	public PhantomPower(GhostAI ghostAI, GhostInteraction ghostInteraction, NavMeshAgent agent)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.agent = agent;
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x00022FE8 File Offset: 0x000211E8
	public void Enter()
	{
		this.ghostAI.anim.SetBool("isIdle", false);
		Vector3 zero = Vector3.zero;
		if (this.MoveToPlayerPosition(GameController.instance.playersData[Random.Range(0, GameController.instance.playersData.Count)].player, out zero))
		{
			this.agent.destination = zero;
			this.ghostInteraction.CreateInteractionEMF(this.ghostAI.raycastPoint.position);
			return;
		}
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0002307C File Offset: 0x0002127C
	public void Execute()
	{
		if (this.agent.pathStatus == NavMeshPathStatus.PathPartial || this.agent.pathStatus == NavMeshPathStatus.PathInvalid)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (Vector3.Distance(this.ghostAI.transform.position, this.agent.destination) < 1f)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		}
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x000230EC File Offset: 0x000212EC
	private bool MoveToPlayerPosition(Player player, out Vector3 pos)
	{
		float num = Random.Range(1f, 5f);
		NavMeshHit navMeshHit;
		if (NavMesh.SamplePosition(Random.insideUnitSphere * num + player.transform.position, out navMeshHit, num, 1))
		{
			pos = navMeshHit.position;
			return true;
		}
		pos = Vector3.zero;
		return false;
	}

	// Token: 0x0400062A RID: 1578
	private GhostInteraction ghostInteraction;

	// Token: 0x0400062B RID: 1579
	private GhostAI ghostAI;

	// Token: 0x0400062C RID: 1580
	private NavMeshAgent agent;
}
