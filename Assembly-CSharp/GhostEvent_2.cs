using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000FA RID: 250
public class GhostEvent_2 : IState
{
	// Token: 0x060006D3 RID: 1747 RVA: 0x00026788 File Offset: 0x00024988
	public GhostEvent_2(GhostAI ghost, GhostInteraction ghostInteraction)
	{
		this.ghost = ghost;
		this.ghostInteraction = ghostInteraction;
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x000267B4 File Offset: 0x000249B4
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
		if (Vector3.Distance(this.player.headObject.transform.position, this.ghost.raycastPoint.position) > 15f)
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
			for (int k = 0; k < this.room.doors.Length; k++)
			{
				if (!this.room.doors[k].photonInteract.isGrabbed)
				{
					Rigidbody component = this.room.doors[k].GetComponent<Rigidbody>();
					component.mass = 1f;
					component.isKinematic = false;
					component.AddTorque(new Vector3(0f, (component.GetComponent<HingeJoint>().limits.min == 0f) ? -6f : 6f, 0f), ForceMode.VelocityChange);
					this.ghost.StartCoroutine(this.ghost.ResetRigidbody(component, this.room.doors[k]));
					this.ghostInteraction.CreateDoorNoise(this.room.doors[k].transform.position);
				}
			}
			this.ghost.Appear(true);
			this.ghost.ghostAudio.TurnOnOrOffAppearSource(true);
			this.ghost.ghostAudio.PlayOrStopAppearSource(true);
			this.ghost.ghostInteraction.CreateAppearedEMF(this.ghost.raycastPoint.position);
			return;
		}
		this.ghost.ChangeState(GhostAI.States.idle, null, null);
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00026D0C File Offset: 0x00024F0C
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
		if (Vector3.Distance(this.player.headObject.transform.position, this.ghost.raycastPoint.position) < 2f)
		{
			this.ghost.ghostAudio.PlaySound(1, false, false);
			this.player.ChangeSanity(-20);
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.playerDistance > 5f)
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

	// Token: 0x060006D6 RID: 1750 RVA: 0x00026E4D File Offset: 0x0002504D
	public void Exit()
	{
		this.ghost.ghostAudio.StopSound();
		this.ghost.UnAppear(true);
		this.ghost.ghostAudio.TurnOnOrOffAppearSource(false);
		this.ghost.ghostAudio.PlayOrStopAppearSource(false);
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00026E90 File Offset: 0x00025090
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

	// Token: 0x0400068F RID: 1679
	private GhostAI ghost;

	// Token: 0x04000690 RID: 1680
	private GhostInteraction ghostInteraction;

	// Token: 0x04000691 RID: 1681
	private Player player;

	// Token: 0x04000692 RID: 1682
	private LevelRoom room;

	// Token: 0x04000693 RID: 1683
	private float playerDistance;

	// Token: 0x04000694 RID: 1684
	private float timer = Random.Range(5f, 15f);
}
