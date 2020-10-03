using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class JumpAndRunMovement : MonoBehaviour
{
	// Token: 0x0600014B RID: 331 RVA: 0x00009EA5 File Offset: 0x000080A5
	private void Awake()
	{
		this.m_Animator = base.GetComponent<Animator>();
		this.m_Body = base.GetComponent<Rigidbody2D>();
		this.m_PhotonView = base.GetComponent<PhotonView>();
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00009ECB File Offset: 0x000080CB
	private void Update()
	{
		this.UpdateIsGrounded();
		this.UpdateIsRunning();
		this.UpdateFacingDirection();
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00009EDF File Offset: 0x000080DF
	private void FixedUpdate()
	{
		if (!this.m_PhotonView.isMine)
		{
			return;
		}
		this.UpdateMovement();
		this.UpdateJumping();
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00009EFC File Offset: 0x000080FC
	private void UpdateFacingDirection()
	{
		if (this.m_Body.velocity.x > 0.2f)
		{
			base.transform.localScale = new Vector3(1f, 1f, 1f);
			return;
		}
		if (this.m_Body.velocity.x < -0.2f)
		{
			base.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00009F78 File Offset: 0x00008178
	private void UpdateJumping()
	{
		if (Input.GetButton("Jump") && this.m_IsGrounded)
		{
			this.m_Animator.SetTrigger("IsJumping");
			this.m_Body.AddForce(Vector2.up * this.JumpForce);
			this.m_PhotonView.RPC("DoJump", PhotonTargets.Others, Array.Empty<object>());
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00009FDA File Offset: 0x000081DA
	[PunRPC]
	private void DoJump()
	{
		this.m_Animator.SetTrigger("IsJumping");
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00009FEC File Offset: 0x000081EC
	private void UpdateMovement()
	{
		Vector2 velocity = this.m_Body.velocity;
		if (Input.GetAxisRaw("Horizontal") > 0.5f)
		{
			velocity.x = this.Speed;
		}
		else if (Input.GetAxisRaw("Horizontal") < -0.5f)
		{
			velocity.x = -this.Speed;
		}
		else
		{
			velocity.x = 0f;
		}
		this.m_Body.velocity = velocity;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x0000A05E File Offset: 0x0000825E
	private void UpdateIsRunning()
	{
		this.m_Animator.SetBool("IsRunning", Mathf.Abs(this.m_Body.velocity.x) > 0.1f);
	}

	// Token: 0x06000153 RID: 339 RVA: 0x0000A08C File Offset: 0x0000828C
	private void UpdateIsGrounded()
	{
		this.m_IsGrounded = (Physics2D.Raycast(new Vector2(base.transform.position.x, base.transform.position.y), -Vector2.up, 0.1f).collider != null);
		this.m_Animator.SetBool("IsGrounded", this.m_IsGrounded);
	}

	// Token: 0x04000192 RID: 402
	public float Speed;

	// Token: 0x04000193 RID: 403
	public float JumpForce;

	// Token: 0x04000194 RID: 404
	private Animator m_Animator;

	// Token: 0x04000195 RID: 405
	private Rigidbody2D m_Body;

	// Token: 0x04000196 RID: 406
	private PhotonView m_PhotonView;

	// Token: 0x04000197 RID: 407
	private bool m_IsGrounded;
}
