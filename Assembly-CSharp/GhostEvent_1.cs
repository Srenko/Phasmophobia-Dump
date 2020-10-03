using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000F9 RID: 249
public class GhostEvent_1 : IState
{
	// Token: 0x060006CE RID: 1742 RVA: 0x00025F12 File Offset: 0x00024112
	public GhostEvent_1(GhostAI ghostAI, GhostInteraction ghostInteraction)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x00025F40 File Offset: 0x00024140
	public void Enter()
	{
		this.player = GameController.instance.playersData[Random.Range(0, GameController.instance.playersData.Count)].player;
		float num = Vector3.Distance(this.ghostAI.raycastPoint.transform.position, this.player.headObject.transform.position);
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i] != null && !GameController.instance.playersData[i].player.isDead && Vector3.Distance(this.ghostAI.raycastPoint.transform.position, GameController.instance.playersData[i].player.headObject.transform.position) < num)
			{
				num = Vector3.Distance(this.ghostAI.raycastPoint.transform.position, GameController.instance.playersData[i].player.headObject.transform.position);
				this.player = GameController.instance.playersData[i].player;
			}
		}
		if (this.player == null)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.player.isDead)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.player.currentRoom == LevelController.instance.outsideRoom)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		this.room = this.player.currentRoom;
		Collider collider = this.room.colliders[Random.Range(0, this.room.colliders.Count)];
		Vector3 vector = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x), collider.bounds.min.y, Random.Range(collider.bounds.min.z, collider.bounds.max.z));
		this.ghostInteraction.CreateAppearedEMF(this.ghostAI.transform.position);
		if (this.player == null)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.room.doors.Length == 0 && !this.room.isBasementOrAttic)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.room.floorType != LevelController.instance.currentGhostRoom.floorType)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (Vector3.Distance(this.player.headObject.transform.position, this.ghostAI.raycastPoint.position) > 15f)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (Vector3.Distance(this.player.headObject.transform.position, vector) < 2f)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (MissionGhostEvent.instance != null && !MissionGhostEvent.instance.completed)
		{
			MissionGhostEvent.instance.CompleteMission();
		}
		for (int j = 0; j < this.room.doors.Length; j++)
		{
			this.room.doors[j].view.RequestOwnership();
		}
		for (int k = 0; k < this.room.doors.Length; k++)
		{
			if (!this.room.doors[k].photonInteract.isGrabbed)
			{
				Rigidbody component = this.room.doors[k].GetComponent<Rigidbody>();
				component.mass = 1f;
				component.isKinematic = false;
				component.AddTorque(new Vector3(0f, (component.GetComponent<HingeJoint>().limits.min == 0f) ? -6f : 6f, 0f), ForceMode.VelocityChange);
				this.ghostAI.StartCoroutine(this.ghostAI.ResetRigidbody(component, this.room.doors[k]));
				this.ghostInteraction.CreateDoorNoise(this.room.doors[k].transform.position);
			}
		}
		for (int l = 0; l < this.room.lightSwitches.Count; l++)
		{
			this.room.lightSwitches[l].TurnOff();
		}
		Vector3 zero = Vector3.zero;
		if (this.GetPositionOnNavMesh(vector, out zero))
		{
			this.ghostInteraction.CreateAppearedEMF(this.ghostAI.transform.position);
			this.ghostAI.Appear(true);
			this.ghostAI.ghostAudio.TurnOnOrOffAppearSource(true);
			this.ghostAI.ghostAudio.PlayOrStopAppearSource(true);
			for (int m = 0; m < this.room.doors.Length; m++)
			{
				if (!this.room.doors[m].photonInteract.isGrabbed)
				{
					Rigidbody component2 = this.room.doors[m].GetComponent<Rigidbody>();
					component2.isKinematic = false;
					component2.AddTorque(new Vector3(0f, -3f, 0f), ForceMode.Impulse);
					this.ghostAI.StartCoroutine(this.ghostAI.ResetRigidbody(component2, this.room.doors[m]));
					this.ghostInteraction.CreateDoorNoise(this.room.doors[m].transform.position);
				}
			}
			this.ghostAI.agent.speed = this.ghostAI.agent.speed / 2f;
			return;
		}
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x00026584 File Offset: 0x00024784
	public void Execute()
	{
		this.timer -= Time.deltaTime;
		this.ghostAI.agent.SetDestination(this.player.headObject.transform.position);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.player.cam.transform.position, this.player.cam.transform.forward, out raycastHit, 2f, this.player.ghostRaycastMask) && raycastHit.collider.CompareTag("Ghost"))
		{
			this.ghostAI.ghostAudio.PlaySound(1, false, false);
			this.player.ChangeSanity(-20);
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (Vector3.Distance(this.player.headObject.transform.position, this.ghostAI.raycastPoint.position) < 1.5f)
		{
			this.player.ChangeSanity(-20);
			this.ghostAI.ghostAudio.PlaySound(1, false, false);
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.player.currentRoom != this.room)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.timer < 0f)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x000266F8 File Offset: 0x000248F8
	public void Exit()
	{
		this.ghostAI.ghostAudio.TurnOnOrOffAppearSource(false);
		this.ghostAI.ghostAudio.PlayOrStopAppearSource(false);
		this.ghostAI.UnAppear(true);
		this.ghostAI.agent.speed = this.ghostAI.defaultSpeed;
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x00026750 File Offset: 0x00024950
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

	// Token: 0x0400068A RID: 1674
	private GhostAI ghostAI;

	// Token: 0x0400068B RID: 1675
	private GhostInteraction ghostInteraction;

	// Token: 0x0400068C RID: 1676
	private Player player;

	// Token: 0x0400068D RID: 1677
	private LevelRoom room;

	// Token: 0x0400068E RID: 1678
	private float timer = Random.Range(5f, 15f);
}
