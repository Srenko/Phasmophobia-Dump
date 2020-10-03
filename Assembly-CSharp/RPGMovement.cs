using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
[RequireComponent(typeof(CharacterController))]
public class RPGMovement : MonoBehaviour
{
	// Token: 0x06000214 RID: 532 RVA: 0x0000E90B File Offset: 0x0000CB0B
	private void Start()
	{
		this.m_CharacterController = base.GetComponent<CharacterController>();
		this.m_Animator = base.GetComponent<Animator>();
		this.m_PhotonView = base.GetComponent<PhotonView>();
		this.m_TransformView = base.GetComponent<PhotonTransformView>();
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0000E940 File Offset: 0x0000CB40
	private void Update()
	{
		if (this.m_PhotonView.isMine)
		{
			this.ResetSpeedValues();
			this.UpdateRotateMovement();
			this.UpdateForwardMovement();
			this.UpdateBackwardMovement();
			this.UpdateStrafeMovement();
			this.MoveCharacterController();
			this.ApplyGravityToCharacterController();
			this.ApplySynchronizedValues();
		}
		this.UpdateAnimation();
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000E990 File Offset: 0x0000CB90
	private void UpdateAnimation()
	{
		Vector3 vector = base.transform.position - this.m_LastPosition;
		float num = Vector3.Dot(vector.normalized, base.transform.forward);
		float num2 = Vector3.Dot(vector.normalized, base.transform.right);
		if (Mathf.Abs(num) < 0.2f)
		{
			num = 0f;
		}
		if (num > 0.6f)
		{
			num = 1f;
			num2 = 0f;
		}
		if (num >= 0f && Mathf.Abs(num2) > 0.7f)
		{
			num = 1f;
		}
		this.m_AnimatorSpeed = Mathf.MoveTowards(this.m_AnimatorSpeed, num, Time.deltaTime * 5f);
		this.m_Animator.SetFloat("Speed", this.m_AnimatorSpeed);
		this.m_Animator.SetFloat("Direction", num2);
		this.m_LastPosition = base.transform.position;
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000EA7B File Offset: 0x0000CC7B
	private void ResetSpeedValues()
	{
		this.m_CurrentMovement = Vector3.zero;
		this.m_CurrentTurnSpeed = 0f;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000EA93 File Offset: 0x0000CC93
	private void ApplySynchronizedValues()
	{
		this.m_TransformView.SetSynchronizedValues(this.m_CurrentMovement, this.m_CurrentTurnSpeed);
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000EAAC File Offset: 0x0000CCAC
	private void ApplyGravityToCharacterController()
	{
		this.m_CharacterController.Move(base.transform.up * Time.deltaTime * -9.81f);
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000EAD9 File Offset: 0x0000CCD9
	private void MoveCharacterController()
	{
		this.m_CharacterController.Move(this.m_CurrentMovement * Time.deltaTime);
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000EAF7 File Offset: 0x0000CCF7
	private void UpdateForwardMovement()
	{
		if (Input.GetKey(KeyCode.W) || Input.GetAxisRaw("Vertical") > 0.1f)
		{
			this.m_CurrentMovement = base.transform.forward * this.ForwardSpeed;
		}
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000EB2F File Offset: 0x0000CD2F
	private void UpdateBackwardMovement()
	{
		if (Input.GetKey(KeyCode.S) || Input.GetAxisRaw("Vertical") < -0.1f)
		{
			this.m_CurrentMovement = -base.transform.forward * this.BackwardSpeed;
		}
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000EB6C File Offset: 0x0000CD6C
	private void UpdateStrafeMovement()
	{
		if (Input.GetKey(KeyCode.Q))
		{
			this.m_CurrentMovement = -base.transform.right * this.StrafeSpeed;
		}
		if (Input.GetKey(KeyCode.E))
		{
			this.m_CurrentMovement = base.transform.right * this.StrafeSpeed;
		}
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0000EBC8 File Offset: 0x0000CDC8
	private void UpdateRotateMovement()
	{
		if (Input.GetKey(KeyCode.A) || Input.GetAxisRaw("Horizontal") < -0.1f)
		{
			this.m_CurrentTurnSpeed = -this.RotateSpeed;
			base.transform.Rotate(0f, -this.RotateSpeed * Time.deltaTime, 0f);
		}
		if (Input.GetKey(KeyCode.D) || Input.GetAxisRaw("Horizontal") > 0.1f)
		{
			this.m_CurrentTurnSpeed = this.RotateSpeed;
			base.transform.Rotate(0f, this.RotateSpeed * Time.deltaTime, 0f);
		}
	}

	// Token: 0x04000255 RID: 597
	public float ForwardSpeed;

	// Token: 0x04000256 RID: 598
	public float BackwardSpeed;

	// Token: 0x04000257 RID: 599
	public float StrafeSpeed;

	// Token: 0x04000258 RID: 600
	public float RotateSpeed;

	// Token: 0x04000259 RID: 601
	private CharacterController m_CharacterController;

	// Token: 0x0400025A RID: 602
	private Vector3 m_LastPosition;

	// Token: 0x0400025B RID: 603
	private Animator m_Animator;

	// Token: 0x0400025C RID: 604
	private PhotonView m_PhotonView;

	// Token: 0x0400025D RID: 605
	private PhotonTransformView m_TransformView;

	// Token: 0x0400025E RID: 606
	private float m_AnimatorSpeed;

	// Token: 0x0400025F RID: 607
	private Vector3 m_CurrentMovement;

	// Token: 0x04000260 RID: 608
	private float m_CurrentTurnSpeed;
}
