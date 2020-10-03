using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000F3 RID: 243
public class AppearState : IState
{
	// Token: 0x060006B6 RID: 1718 RVA: 0x000256A8 File Offset: 0x000238A8
	public AppearState(GhostAI ghostAI, NavMeshAgent agent)
	{
		this.ghostAI = ghostAI;
		this.agent = agent;
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x000256CC File Offset: 0x000238CC
	public void Enter()
	{
		this.ghostAI.Appear(false);
		this.ghostAI.ghostInteraction.CreateAppearedEMF(this.ghostAI.transform.position);
		this.ghostAI.anim.SetBool("isIdle", false);
		if (LevelController.instance.currentGhostRoom == null)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		for (int i = 0; i < LevelController.instance.currentGhostRoom.playersInRoom.Count; i++)
		{
			if (LevelController.instance.currentGhostRoom.playersInRoom[i] == null)
			{
				LevelController.instance.currentGhostRoom.playersInRoom.RemoveAt(i);
				this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
				return;
			}
		}
		if (LevelController.instance.currentGhostRoom.playersInRoom.Count == 0)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		for (int j = 0; j < LevelController.instance.currentGhostRoom.playersInRoom.Count; j++)
		{
			if (LevelController.instance.currentGhostRoom.playersInRoom[j] != null)
			{
				this.target = LevelController.instance.currentGhostRoom.playersInRoom[0].transform.root.GetComponent<Player>().headObject.transform;
				break;
			}
		}
		if (this.target == null)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (Vector3.Distance(this.target.position, this.ghostAI.raycastPoint.position) < 2.5f)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		for (int k = 0; k < LevelController.instance.currentGhostRoom.lightSwitches.Count; k++)
		{
			LevelController.instance.currentGhostRoom.lightSwitches[k].TurnOff();
			if (LevelController.instance.currentGhostRoom.lightSwitches[k].lever != null)
			{
				this.mainLight = LevelController.instance.currentGhostRoom.lightSwitches[k];
			}
		}
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x000258FC File Offset: 0x00023AFC
	public void Execute()
	{
		this.appearTimer -= Time.deltaTime;
		if (this.appearTimer < 0f)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.mainLight == null || LevelController.instance.currentGhostRoom == null || this.target == null)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.mainLight.isOn || LevelController.instance.currentGhostRoom.playersInRoom.Count == 0)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		this.agent.SetDestination(this.target.position);
		if (Vector3.Distance(this.ghostAI.transform.position, this.agent.destination) < 1.5f)
		{
			this.target.root.gameObject.GetComponent<Player>().ChangeSanity(-25);
			this.ghostAI.ghostAudio.PlaySound(1, false, false);
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		}
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x00025A21 File Offset: 0x00023C21
	public void Exit()
	{
		this.ghostAI.UnAppear(false);
	}

	// Token: 0x04000677 RID: 1655
	private GhostAI ghostAI;

	// Token: 0x04000678 RID: 1656
	private NavMeshAgent agent;

	// Token: 0x04000679 RID: 1657
	private LightSwitch mainLight;

	// Token: 0x0400067A RID: 1658
	private Transform target;

	// Token: 0x0400067B RID: 1659
	private float appearTimer = 2.5f;
}
