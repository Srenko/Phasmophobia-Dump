using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class PoltergeistPower : IState
{
	// Token: 0x06000653 RID: 1619 RVA: 0x0002314A File Offset: 0x0002134A
	public PoltergeistPower(GhostAI ghostAI, GhostInteraction ghostInteraction, LayerMask mask, PhotonObjectInteract[] props)
	{
		this.ghostInteraction = ghostInteraction;
		this.mask = mask;
		this.ghostAI = ghostAI;
		this.props = props;
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x00023170 File Offset: 0x00021370
	public void Enter()
	{
		if (this.props.Length == 0)
		{
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		for (int i = 0; i < this.props.Length; i++)
		{
			if (this.props[i] != null)
			{
				if (!this.props[i].GetComponent<PhotonView>().isMine)
				{
					this.props[i].GetComponent<PhotonView>().RequestOwnership();
				}
				this.props[i].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-4f, 4f), Random.Range(-2f, 2f), Random.Range(-4f, 4f)), ForceMode.Impulse);
				this.ghostInteraction.CreateThrowingEMF(this.props[i].transform.position);
			}
		}
		Vector3 vector = GameController.instance.myPlayer.player.cam.WorldToViewportPoint(this.ghostInteraction.transform.position);
		if (vector.x > 0f && vector.x < 1f && vector.y > 0f && vector.y < 1f && !Physics.Linecast(this.ghostInteraction.transform.position, GameController.instance.myPlayer.player.cam.transform.position, this.mask))
		{
			GameController.instance.myPlayer.player.insanity += (float)this.props.Length * 2f;
		}
		this.ghostInteraction.CreateInteractionEMF(this.ghostAI.raycastPoint.position);
		this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x00003F60 File Offset: 0x00002160
	public void Execute()
	{
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x0400062D RID: 1581
	private GhostInteraction ghostInteraction;

	// Token: 0x0400062E RID: 1582
	private LayerMask mask;

	// Token: 0x0400062F RID: 1583
	private GhostAI ghostAI;

	// Token: 0x04000630 RID: 1584
	private PhotonObjectInteract[] props;
}
