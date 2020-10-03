using System;
using UnityEngine;

// Token: 0x0200010A RID: 266
public class ThrowingState : IState
{
	// Token: 0x06000719 RID: 1817 RVA: 0x00029B48 File Offset: 0x00027D48
	public ThrowingState(GhostAI ghostAI, GhostInteraction ghostInteraction, PhotonObjectInteract prop)
	{
		this.ghostAI = ghostAI;
		this.ghostInteraction = ghostInteraction;
		this.prop = prop;
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x00029B68 File Offset: 0x00027D68
	public void Enter()
	{
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
		if (this.prop == null)
		{
			return;
		}
		if (LevelController.instance.currentGhostRoom == LevelController.instance.outsideRoom)
		{
			return;
		}
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
		if (!this.prop.GetComponent<PhotonView>().isMine)
		{
			this.prop.GetComponent<PhotonView>().RequestOwnership();
		}
		if (this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Poltergeist)
		{
			this.prop.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-4f, 4f), Random.Range(-3f, 3f), Random.Range(-2.5f, 2.5f)), ForceMode.Impulse);
		}
		else if (this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Poltergeist)
		{
			this.prop.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-5f, 5f), Random.Range(-2.5f, 2.5f), Random.Range(-3f, 3f)), ForceMode.Impulse);
		}
		else
		{
			this.prop.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2f, 2f), Random.Range(-2.5f, 2.5f)), ForceMode.Impulse);
		}
		this.ghostInteraction.CreateThrowingEMF(this.prop.transform.position);
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x040006CD RID: 1741
	private GhostAI ghostAI;

	// Token: 0x040006CE RID: 1742
	private GhostInteraction ghostInteraction;

	// Token: 0x040006CF RID: 1743
	private PhotonObjectInteract prop;
}
