using System;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class DoorState : IState
{
	// Token: 0x060006CA RID: 1738 RVA: 0x00025DC5 File Offset: 0x00023FC5
	public DoorState(GhostAI ghostAI, GhostInteraction ghostInteraction, GhostInfo ghostInfo, PhotonObjectInteract obj)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.ghostInfo = ghostInfo;
		this.door = obj.GetComponent<Door>();
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x00025DF0 File Offset: 0x00023FF0
	public void Enter()
	{
		if (EvidenceController.instance.IsFingerPrintEvidence())
		{
			this.door.SpawnHandPrintEvidence();
		}
		if (!this.door.GetComponent<PhotonView>().isMine)
		{
			this.door.GetComponent<PhotonView>().RequestOwnership();
		}
		Rigidbody component = this.door.GetComponent<Rigidbody>();
		component.mass = 1f;
		component.isKinematic = false;
		component.useGravity = true;
		if (this.door.closed)
		{
			component.AddTorque(new Vector3(0f, (component.GetComponent<HingeJoint>().limits.min == 0f) ? 1f : -1f, 0f), ForceMode.VelocityChange);
		}
		else
		{
			component.AddTorque(new Vector3(0f, Random.Range(-1f, 1f), 0f), ForceMode.VelocityChange);
		}
		this.ghostAI.StartCoroutine(this.ghostAI.ResetRigidbody(component, this.door));
		this.ghostInteraction.CreateDoorNoise(this.door.transform.position);
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x04000686 RID: 1670
	private GhostAI ghostAI;

	// Token: 0x04000687 RID: 1671
	private GhostInteraction ghostInteraction;

	// Token: 0x04000688 RID: 1672
	private GhostInfo ghostInfo;

	// Token: 0x04000689 RID: 1673
	private Door door;
}
