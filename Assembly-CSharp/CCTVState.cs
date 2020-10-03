using System;

// Token: 0x020000F4 RID: 244
public class CCTVState : IState
{
	// Token: 0x060006BA RID: 1722 RVA: 0x00025A2F File Offset: 0x00023C2F
	public CCTVState(GhostAI ghostAI, GhostInteraction ghostInteraction, PhotonObjectInteract obj)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.cam = obj.GetComponent<CCTV>();
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x00025A54 File Offset: 0x00023C54
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		if (LevelController.instance.fuseBox.isOn)
		{
			for (int i = 0; i < LevelController.instance.currentGhostRoom.lightSwitches.Count; i++)
			{
				if (LevelController.instance.currentGhostRoom.lightSwitches[i].isOn)
				{
					return;
				}
			}
		}
		if (!this.cam.GetComponent<PhotonView>().isMine)
		{
			this.cam.GetComponent<PhotonView>().RequestOwnership();
		}
		this.ghostInteraction.CreateInteractionEMF(this.cam.transform.position);
		this.cam.TurnOff();
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x0400067C RID: 1660
	private GhostAI ghostAI;

	// Token: 0x0400067D RID: 1661
	private GhostInteraction ghostInteraction;

	// Token: 0x0400067E RID: 1662
	private CCTV cam;
}
