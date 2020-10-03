using System;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public class DoorKnockState : IState
{
	// Token: 0x060006C2 RID: 1730 RVA: 0x00025BC6 File Offset: 0x00023DC6
	public DoorKnockState(GhostAI ghostAI, GhostInteraction ghostInteraction)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00025BDC File Offset: 0x00023DDC
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		if (Random.Range(0, 5) > 3)
		{
			return;
		}
		if (Vector3.Distance(this.ghostAI.transform.position, SoundController.instance.doorAudioSource.transform.position) > 3f)
		{
			return;
		}
		SoundController.instance.view.RPC("PlayDoorKnockingSound", PhotonTargets.All, Array.Empty<object>());
		this.ghostInteraction.CreateInteractionEMF(SoundController.instance.doorAudioSource.transform.position);
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x04000681 RID: 1665
	private GhostAI ghostAI;

	// Token: 0x04000682 RID: 1666
	private GhostInteraction ghostInteraction;
}
