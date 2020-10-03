using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000FD RID: 253
public class FavouriteRoomState : IState
{
	// Token: 0x060006E2 RID: 1762 RVA: 0x00027C78 File Offset: 0x00025E78
	public FavouriteRoomState(GhostAI ghostAI, NavMeshAgent agent)
	{
		this.ghostAI = ghostAI;
		this.agent = agent;
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x00027C9C File Offset: 0x00025E9C
	public void Enter()
	{
		Vector3 zero = Vector3.zero;
		if (this.GetRandomPositionInRoom(out zero))
		{
			this.agent.destination = zero;
			return;
		}
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00027CD4 File Offset: 0x00025ED4
	public void Execute()
	{
		this.stuckTimer -= Time.deltaTime;
		if (this.stuckTimer < 0f)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.agent.pathStatus == NavMeshPathStatus.PathPartial || this.agent.pathStatus == NavMeshPathStatus.PathInvalid || !this.agent.hasPath)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (Vector3.Distance(this.ghostAI.transform.position, this.agent.destination) < 1f)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		}
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00027D7C File Offset: 0x00025F7C
	private bool GetRandomPositionInRoom(out Vector3 hitPos)
	{
		float maxDistance = Random.Range(0f, 0.5f);
		BoxCollider boxCollider = this.ghostAI.ghostInfo.favouriteRoom.colliders[Random.Range(0, this.ghostAI.ghostInfo.favouriteRoom.colliders.Count)];
		NavMeshHit navMeshHit;
		if (NavMesh.SamplePosition(new Vector3(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x), boxCollider.bounds.min.y, Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z)), out navMeshHit, maxDistance, 1))
		{
			hitPos = navMeshHit.position;
			return true;
		}
		hitPos = Vector3.zero;
		return false;
	}

	// Token: 0x0400069F RID: 1695
	private GhostAI ghostAI;

	// Token: 0x040006A0 RID: 1696
	private NavMeshAgent agent;

	// Token: 0x040006A1 RID: 1697
	private float stuckTimer = 30f;
}
