using System;
using Photon;
using UnityEngine;

namespace ExitGames.Demos.DemoAnimator
{
	// Token: 0x02000488 RID: 1160
	public class PlayerAnimatorManager : Photon.MonoBehaviour
	{
		// Token: 0x06002428 RID: 9256 RVA: 0x000B101A File Offset: 0x000AF21A
		private void Start()
		{
			this.animator = base.GetComponent<Animator>();
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000B1028 File Offset: 0x000AF228
		private void Update()
		{
			if (!base.photonView.isMine && PhotonNetwork.connected)
			{
				return;
			}
			if (!this.animator)
			{
				return;
			}
			if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Run") && Input.GetButtonDown("Fire2"))
			{
				this.animator.SetTrigger("Jump");
			}
			float axis = Input.GetAxis("Horizontal");
			float num = Input.GetAxis("Vertical");
			if (num < 0f)
			{
				num = 0f;
			}
			this.animator.SetFloat("Speed", axis * axis + num * num);
			this.animator.SetFloat("Direction", axis, this.DirectionDampTime, Time.deltaTime);
		}

		// Token: 0x04002175 RID: 8565
		public float DirectionDampTime = 0.25f;

		// Token: 0x04002176 RID: 8566
		private Animator animator;
	}
}
