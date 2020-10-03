using System;
using UnityEngine;

// Token: 0x02000103 RID: 259
public class LightState : IState
{
	// Token: 0x060006FD RID: 1789 RVA: 0x000297D9 File Offset: 0x000279D9
	public LightState(GhostAI ghostAI, GhostInteraction ghostInteraction, PhotonObjectInteract obj)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.lightSwitch = obj.GetComponent<LightSwitch>();
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x000297FC File Offset: 0x000279FC
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		if (this.lightSwitch.isOn)
		{
			this.lightSwitch.UseLight();
		}
		else if (Random.Range(0, 2) == 1)
		{
			this.lightSwitch.UseLight();
		}
		if (EvidenceController.instance.IsFingerPrintEvidence())
		{
			this.lightSwitch.SpawnHandPrintEvidence();
		}
		this.ghostInteraction.CreateInteractionEMF(this.lightSwitch.transform.position);
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x040006B9 RID: 1721
	private GhostAI ghostAI;

	// Token: 0x040006BA RID: 1722
	private GhostInteraction ghostInteraction;

	// Token: 0x040006BB RID: 1723
	private LightSwitch lightSwitch;
}
