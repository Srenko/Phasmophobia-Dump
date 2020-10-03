using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001C9 RID: 457
[RequireComponent(typeof(ExitLevel))]
public class LiftButton : MonoBehaviour
{
	// Token: 0x06000C82 RID: 3202 RVA: 0x0004FF26 File Offset: 0x0004E126
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x0004FF3F File Offset: 0x0004E13F
	private void Update()
	{
		if (this.isAnimating)
		{
			this.timer -= Time.deltaTime;
			if (this.timer < 0f)
			{
				this.isAnimating = false;
				this.timer = 4f;
			}
		}
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x0004FF7A File Offset: 0x0004E17A
	private void Use()
	{
		if (GameController.instance)
		{
			this.view.RPC("AttemptUse", PhotonTargets.MasterClient, Array.Empty<object>());
		}
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x0004FFA0 File Offset: 0x0004E1A0
	[PunRPC]
	private void AttemptUse()
	{
		if (GameController.instance.playersData.Count != PhotonNetwork.playerList.Length)
		{
			return;
		}
		if (this.isAnimating)
		{
			return;
		}
		if (!this.isClosed && this.exitLevel.ThereAreAlivePlayersOutsideTheTruck())
		{
			return;
		}
		this.isAnimating = true;
		this.exitLevel.isExiting = !this.isClosed;
		this.isClosed = !this.isClosed;
		this.view.RPC("NetworkedUse", PhotonTargets.AllBufferedViaServer, new object[]
		{
			this.isClosed
		});
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x00050034 File Offset: 0x0004E234
	[PunRPC]
	private void NetworkedUse(bool _isClosed)
	{
		this.isClosed = _isClosed;
		this.wallCollider.enabled = _isClosed;
		this.source.Play();
		this.anim.SetTrigger("Switch");
		if (PhotonNetwork.isMasterClient && this.isClosed)
		{
			this.exitLevel.StartAttemptExitLevel();
		}
	}

	// Token: 0x04000D1C RID: 3356
	[SerializeField]
	private Animator anim;

	// Token: 0x04000D1D RID: 3357
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000D1E RID: 3358
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000D1F RID: 3359
	[SerializeField]
	private AudioSource source;

	// Token: 0x04000D20 RID: 3360
	[SerializeField]
	private ExitLevel exitLevel;

	// Token: 0x04000D21 RID: 3361
	private float timer = 5f;

	// Token: 0x04000D22 RID: 3362
	private bool isAnimating;

	// Token: 0x04000D23 RID: 3363
	private bool isClosed = true;

	// Token: 0x04000D24 RID: 3364
	[SerializeField]
	private Collider wallCollider;
}
