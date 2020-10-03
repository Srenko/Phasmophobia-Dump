using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000FB RID: 251
public class GhostEvent_3 : IState
{
	// Token: 0x060006D8 RID: 1752 RVA: 0x00026EC8 File Offset: 0x000250C8
	public GhostEvent_3(GhostAI ghost, GhostInteraction ghostInteraction)
	{
		this.ghost = ghost;
		this.ghostInteraction = ghostInteraction;
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x00026EF4 File Offset: 0x000250F4
	public void Enter()
	{
		this.player = GameController.instance.playersData[Random.Range(0, GameController.instance.playersData.Count)].player;
		float num = Vector3.Distance(this.ghost.raycastPoint.transform.position, this.player.headObject.transform.position);
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i] != null && !GameController.instance.playersData[i].player.isDead && Vector3.Distance(this.ghost.raycastPoint.transform.position, GameController.instance.playersData[i].player.headObject.transform.position) < num)
			{
				num = Vector3.Distance(this.ghost.raycastPoint.transform.position, GameController.instance.playersData[i].player.headObject.transform.position);
				this.player = GameController.instance.playersData[i].player;
			}
		}
		if (this.player == null)
		{
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.player.isDead)
		{
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.player.currentRoom == LevelController.instance.outsideRoom)
		{
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		this.room = this.player.currentRoom;
		Collider collider = this.room.colliders[Random.Range(0, this.room.colliders.Count)];
		Vector3 vector = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x), collider.bounds.min.y, Random.Range(collider.bounds.min.z, collider.bounds.max.z));
		if (this.room == LevelController.instance.outsideRoom)
		{
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.room.doors.Length == 0 && !this.room.isBasementOrAttic)
		{
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.room.floorType != LevelController.instance.currentGhostRoom.floorType)
		{
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (Vector3.Distance(this.player.headObject.transform.position, this.ghost.raycastPoint.transform.position) > 15f)
		{
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (Vector3.Distance(this.player.headObject.transform.position, vector) < 2f)
		{
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (MissionGhostEvent.instance != null && !MissionGhostEvent.instance.completed)
		{
			MissionGhostEvent.instance.CompleteMission();
		}
		for (int j = 0; j < this.room.lightSwitches.Count; j++)
		{
			this.room.lightSwitches[j].TurnOff();
		}
		Vector3 zero = Vector3.zero;
		if (this.GetPositionOnNavMesh(vector, out zero))
		{
			this.ghost.agent.Warp(zero);
			this.ghost.anim.SetBool("isIdle", true);
			this.ghost.ghostAudio.PlaySound(0, true, false);
			this.ghost.Appear(true);
			this.ghost.ghostAudio.TurnOnOrOffAppearSource(true);
			this.ghost.ghostAudio.PlayOrStopAppearSource(true);
			this.ghost.ghostInteraction.CreateAppearedEMF(this.ghost.transform.position);
			return;
		}
		this.ghost.ChangeState(GhostAI.States.idle, null, null);
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00027360 File Offset: 0x00025560
	public void Execute()
	{
		this.timer -= Time.deltaTime;
		RaycastHit raycastHit;
		if (Physics.Raycast(this.player.cam.transform.position, this.player.cam.transform.forward, out raycastHit, 2f, this.player.ghostRaycastMask) && raycastHit.collider.CompareTag("Ghost"))
		{
			this.ghost.ghostAudio.PlaySound(1, false, false);
			this.player.ChangeSanity(-20);
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (Vector3.Distance(this.player.headObject.transform.position, this.ghost.raycastPoint.transform.position) > 5f)
		{
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.player.currentRoom != this.room)
		{
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.timer < 0f)
		{
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00027491 File Offset: 0x00025691
	public void Exit()
	{
		this.ghost.ghostAudio.StopSound();
		this.ghost.UnAppear(true);
		this.ghost.ghostAudio.TurnOnOrOffAppearSource(false);
		this.ghost.ghostAudio.PlayOrStopAppearSource(false);
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x000274D4 File Offset: 0x000256D4
	private bool GetPositionOnNavMesh(Vector3 pos, out Vector3 hitPos)
	{
		NavMeshHit navMeshHit;
		if (NavMesh.SamplePosition(pos, out navMeshHit, 2f, 1))
		{
			hitPos = navMeshHit.position;
			return true;
		}
		hitPos = Vector3.zero;
		return false;
	}

	// Token: 0x04000695 RID: 1685
	private GhostAI ghost;

	// Token: 0x04000696 RID: 1686
	private GhostInteraction ghostInteraction;

	// Token: 0x04000697 RID: 1687
	private LevelRoom room;

	// Token: 0x04000698 RID: 1688
	private Player player;

	// Token: 0x04000699 RID: 1689
	private float timer = Random.Range(5f, 15f);
}
