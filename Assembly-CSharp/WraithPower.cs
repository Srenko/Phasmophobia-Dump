using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000EA RID: 234
public class WraithPower : IState
{
	// Token: 0x06000657 RID: 1623 RVA: 0x00023340 File Offset: 0x00021540
	public WraithPower(GhostAI ghostAI, GhostInteraction ghostInteraction, NavMeshAgent agent)
	{
		this.ghostInteraction = ghostInteraction;
		this.ghostAI = ghostAI;
		this.agent = agent;
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x00023360 File Offset: 0x00021560
	public void Enter()
	{
		Player player = GameController.instance.playersData[Random.Range(0, GameController.instance.playersData.Count)].player;
		if (player.currentRoom == LevelController.instance.outsideRoom)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (player.isDead)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		Vector3 vector;
		if (this.GetPositionOnNavMesh(player.transform.position, out vector))
		{
			this.agent.Warp(vector);
			this.ghostInteraction.CreateInteractionEMF(vector);
		}
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x00023410 File Offset: 0x00021610
	private bool GetPositionOnNavMesh(Vector3 pos, out Vector3 result)
	{
		NavMeshHit navMeshHit;
		if (NavMesh.SamplePosition(pos, out navMeshHit, 3f, 1))
		{
			result = navMeshHit.position;
			return true;
		}
		result = Vector3.zero;
		return false;
	}

	// Token: 0x04000631 RID: 1585
	private GhostInteraction ghostInteraction;

	// Token: 0x04000632 RID: 1586
	private GhostAI ghostAI;

	// Token: 0x04000633 RID: 1587
	private NavMeshAgent agent;
}
