using System;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class DoorLockState : IState
{
	// Token: 0x060006C6 RID: 1734 RVA: 0x00025C6C File Offset: 0x00023E6C
	public DoorLockState(GhostAI ghostAI, GhostInteraction ghostInteraction, PhotonObjectInteract obj)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.door = obj.GetComponent<Door>();
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x00025C90 File Offset: 0x00023E90
	public void Enter()
	{
		if (this.door.GetComponent<Door>().type == Key.KeyType.none || this.door.type == Key.KeyType.Car)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		if (this.door.GetComponent<PhotonObjectInteract>().isGrabbed)
		{
			return;
		}
		if (!this.door.GetComponent<PhotonView>().isMine)
		{
			this.door.GetComponent<PhotonView>().RequestOwnership();
		}
		if (!this.door.closed)
		{
			Rigidbody component = this.door.GetComponent<Rigidbody>();
			component.mass = 1f;
			component.isKinematic = false;
			component.useGravity = true;
			component.AddTorque(new Vector3(0f, (component.GetComponent<HingeJoint>().limits.min == 0f) ? -1.25f : 1.25f, 0f), ForceMode.VelocityChange);
			this.ghostAI.StartCoroutine(this.ghostAI.ResetRigidbody(component, this.door));
			this.ghostInteraction.CreateDoorNoise(this.door.transform.position);
		}
		this.door.LockDoor();
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x04000683 RID: 1667
	private GhostAI ghostAI;

	// Token: 0x04000684 RID: 1668
	private GhostInteraction ghostInteraction;

	// Token: 0x04000685 RID: 1669
	private Door door;
}
