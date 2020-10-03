using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace ExitGames.Demos.DemoPunVoice
{
	// Token: 0x0200048C RID: 1164
	[RequireComponent(typeof(PhotonView))]
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(Animator))]
	public abstract class BaseController : MonoBehaviour
	{
		// Token: 0x0600243F RID: 9279 RVA: 0x000B1610 File Offset: 0x000AF810
		protected virtual void OnEnable()
		{
			ChangePOV.CameraChanged += this.ChangePOV_CameraChanged;
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000B1624 File Offset: 0x000AF824
		protected virtual void OnDisable()
		{
			ChangePOV.CameraChanged -= this.ChangePOV_CameraChanged;
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000B1638 File Offset: 0x000AF838
		protected virtual void ChangePOV_CameraChanged(Camera camera)
		{
			if (camera != this.ControllerCamera)
			{
				base.enabled = false;
				this.HideCamera(this.ControllerCamera);
				return;
			}
			this.ShowCamera(this.ControllerCamera);
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000B1668 File Offset: 0x000AF868
		protected virtual void Start()
		{
			if (base.GetComponent<PhotonView>().isMine)
			{
				this.Init();
				this.SetCamera();
				return;
			}
			base.enabled = false;
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000B168B File Offset: 0x000AF88B
		protected virtual void Init()
		{
			this.rigidBody = base.GetComponent<Rigidbody>();
			this.animator = base.GetComponent<Animator>();
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000B16A5 File Offset: 0x000AF8A5
		protected virtual void SetCamera()
		{
			this.camTrans = this.ControllerCamera.transform;
			this.camTrans.position += this.cameraDistance * base.transform.forward;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000B16E4 File Offset: 0x000AF8E4
		protected virtual void UpdateAnimator(float h, float v)
		{
			bool value = h != 0f || v != 0f;
			this.animator.SetBool("IsWalking", value);
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000B171C File Offset: 0x000AF91C
		protected virtual void FixedUpdate()
		{
			this.h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
			this.v = CrossPlatformInputManager.GetAxisRaw("Vertical");
			this.UpdateAnimator(this.h, this.v);
			this.Move(this.h, this.v);
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000B176D File Offset: 0x000AF96D
		protected virtual void ShowCamera(Camera camera)
		{
			if (camera != null)
			{
				camera.gameObject.SetActive(true);
			}
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000B1784 File Offset: 0x000AF984
		protected virtual void HideCamera(Camera camera)
		{
			if (camera != null)
			{
				camera.gameObject.SetActive(false);
			}
		}

		// Token: 0x06002449 RID: 9289
		protected abstract void Move(float h, float v);

		// Token: 0x04002185 RID: 8581
		public Camera ControllerCamera;

		// Token: 0x04002186 RID: 8582
		protected Rigidbody rigidBody;

		// Token: 0x04002187 RID: 8583
		protected Animator animator;

		// Token: 0x04002188 RID: 8584
		protected Transform camTrans;

		// Token: 0x04002189 RID: 8585
		private float h;

		// Token: 0x0400218A RID: 8586
		private float v;

		// Token: 0x0400218B RID: 8587
		[SerializeField]
		protected float speed = 5f;

		// Token: 0x0400218C RID: 8588
		[SerializeField]
		private float cameraDistance;
	}
}
