using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200010B RID: 267
public class WanderState : IState
{
	// Token: 0x0600071D RID: 1821 RVA: 0x00029D38 File Offset: 0x00027F38
	public WanderState(GhostAI ghostAI, NavMeshAgent agent)
	{
		this.ghostAI = ghostAI;
		this.agent = agent;
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x00029D5C File Offset: 0x00027F5C
	public void Enter()
	{
		if (!this.ghostAI.canWander)
		{
			this.ghostAI.ChangeState(GhostAI.States.favouriteRoom, null, null);
			return;
		}
		Vector3 zero = Vector3.zero;
		Vector3 vector = Vector3.zero;
		if (!this.RandomNavSphere(out zero))
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		vector = zero;
		if (!LevelController.instance.currentGhostRoom.isBasementOrAttic && SoundController.instance.GetFloorTypeFromPosition(vector.y) != LevelController.instance.currentGhostRoom.floorType)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		this.agent.destination = vector;
		this.ghostAI.anim.SetBool("isIdle", false);
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00029E14 File Offset: 0x00028014
	public void Execute()
	{
		this.stuckTimer -= Time.deltaTime;
		if (this.stuckTimer < 0f)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
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

	// Token: 0x06000720 RID: 1824 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x00029EB0 File Offset: 0x000280B0
	private bool RandomNavSphere(out Vector3 hitPos)
	{
		NavMeshHit navMeshHit;
		if (NavMesh.SamplePosition(Random.insideUnitSphere * 3f + this.ghostAI.transform.position, out navMeshHit, 3f, 1))
		{
			hitPos = navMeshHit.position;
			return true;
		}
		hitPos = Vector3.zero;
		return false;
	}

	// Token: 0x040006D0 RID: 1744
	private GhostAI ghostAI;

	// Token: 0x040006D1 RID: 1745
	private NavMeshAgent agent;

	// Token: 0x040006D2 RID: 1746
	private float stuckTimer = 30f;
}
