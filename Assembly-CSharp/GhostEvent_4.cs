using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000FC RID: 252
public class GhostEvent_4 : IState
{
	// Token: 0x060006DD RID: 1757 RVA: 0x0002750C File Offset: 0x0002570C
	public GhostEvent_4(GhostAI ghost, GhostInteraction ghostInteraction)
	{
		this.ghost = ghost;
		this.ghostInteraction = ghostInteraction;
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x00027538 File Offset: 0x00025738
	public void Enter()
	{
		if (GhostController.instance.ghostEventPlayer == null)
		{
			GhostController.instance.ghostEventPlayer = Object.FindObjectOfType<GhostEventPlayer>();
		}
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
		this.ghost.anim.SetBool("isIdle", true);
		Vector3 zero = Vector3.zero;
		if (this.GetPositionOnNavMesh(vector, out zero))
		{
			GhostController.instance.ghostEventPlayer.SpawnPlayer(this.player, zero);
			this.ghostInteraction.CreateAppearedEMF(this.ghost.transform.position);
			return;
		}
		GhostController.instance.ghostEventPlayer.Stop();
		this.ghost.ChangeState(GhostAI.States.idle, null, null);
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00027990 File Offset: 0x00025B90
	public void Execute()
	{
		this.timer -= Time.deltaTime;
		if (this.player.currentRoom == LevelController.instance.outsideRoom)
		{
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		RaycastHit raycastHit;
		if (Physics.Raycast(this.player.cam.transform.position, this.player.cam.transform.forward, out raycastHit, 2f, this.player.ghostRaycastMask) && raycastHit.collider.CompareTag("Ghost"))
		{
			GhostController.instance.ghostEventPlayer.Stop();
			GhostController.instance.ghostEventPlayer.screamSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(GhostController.instance.ghostEventPlayer.transform.position.y);
			GhostController.instance.ghostEventPlayer.screamSource.Play();
			this.player.ChangeSanity(-20);
			this.ghost.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (Vector3.Distance(GhostController.instance.ghostEventPlayer.transform.position, this.player.headObject.transform.position) < 2f)
		{
			GhostController.instance.ghostEventPlayer.Stop();
			GhostController.instance.ghostEventPlayer.screamSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(GhostController.instance.ghostEventPlayer.transform.position.y);
			GhostController.instance.ghostEventPlayer.screamSource.Play();
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

	// Token: 0x060006E0 RID: 1760 RVA: 0x00027BE4 File Offset: 0x00025DE4
	public void Exit()
	{
		this.ghost.ghostAudio.StopSound();
		GhostController.instance.ghostEventPlayer.Stop();
		this.ghost.UnAppear(true);
		this.ghost.ghostAudio.TurnOnOrOffAppearSource(false);
		this.ghost.ghostAudio.PlayOrStopAppearSource(false);
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00027C40 File Offset: 0x00025E40
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

	// Token: 0x0400069A RID: 1690
	private GhostAI ghost;

	// Token: 0x0400069B RID: 1691
	private GhostInteraction ghostInteraction;

	// Token: 0x0400069C RID: 1692
	private LevelRoom room;

	// Token: 0x0400069D RID: 1693
	private Player player;

	// Token: 0x0400069E RID: 1694
	private float timer = Random.Range(5f, 15f);
}
