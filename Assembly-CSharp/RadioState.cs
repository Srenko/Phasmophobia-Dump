using System;
using UnityEngine;

// Token: 0x02000106 RID: 262
public class RadioState : IState
{
	// Token: 0x06000709 RID: 1801 RVA: 0x0002994C File Offset: 0x00027B4C
	public RadioState(GhostAI ghostAI, GhostInteraction ghostInteraction)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x00029964 File Offset: 0x00027B64
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		if (Random.Range(0, 5) > 1)
		{
			return;
		}
		if (LevelController.instance.radiosInLevel.Length != 0)
		{
			for (int i = 0; i < LevelController.instance.radiosInLevel.Length; i++)
			{
				if (Vector3.Distance(this.ghostAI.transform.position, LevelController.instance.radiosInLevel[i].transform.position) < 2f)
				{
					LevelController.instance.radiosInLevel[i].view.RPC("TurnOn", PhotonTargets.All, Array.Empty<object>());
					this.ghostInteraction.CreateInteractionEMF(LevelController.instance.radiosInLevel[i].transform.position);
				}
			}
		}
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x040006C2 RID: 1730
	private GhostAI ghostAI;

	// Token: 0x040006C3 RID: 1731
	private GhostInteraction ghostInteraction;
}
