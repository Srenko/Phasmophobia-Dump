using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class IdleRunJump : MonoBehaviour
{
	// Token: 0x060002B3 RID: 691 RVA: 0x00011C10 File Offset: 0x0000FE10
	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
		this.m_PhotonView = base.GetComponent<PhotonView>();
		this.m_TransformView = base.GetComponent<PhotonTransformView>();
		if (this.animator.layerCount >= 2)
		{
			this.animator.SetLayerWeight(1, 1f);
		}
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x00011C60 File Offset: 0x0000FE60
	private void Update()
	{
		if (!this.m_PhotonView.isMine && PhotonNetwork.connected)
		{
			return;
		}
		if (this.animator)
		{
			if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Run"))
			{
				if (Input.GetButton("Fire1"))
				{
					this.animator.SetBool("Jump", true);
				}
			}
			else
			{
				this.animator.SetBool("Jump", false);
			}
			if (Input.GetButtonDown("Fire2") && this.animator.layerCount >= 2)
			{
				this.animator.SetBool("Hi", !this.animator.GetBool("Hi"));
			}
			float axis = Input.GetAxis("Horizontal");
			float num = Input.GetAxis("Vertical");
			if (num < 0f)
			{
				num = 0f;
			}
			this.animator.SetFloat("Speed", axis * axis + num * num);
			this.animator.SetFloat("Direction", axis, this.DirectionDampTime, Time.deltaTime);
			float @float = this.animator.GetFloat("Direction");
			float target = Mathf.Abs(num);
			if (Mathf.Abs(@float) > 0.2f)
			{
				target = this.TurnSpeedModifier;
			}
			this.m_SpeedModifier = Mathf.MoveTowards(this.m_SpeedModifier, target, Time.deltaTime * 25f);
			Vector3 speed = base.transform.forward * this.SynchronizedMaxSpeed * this.m_SpeedModifier;
			float turnSpeed = @float * this.SynchronizedTurnSpeed;
			this.m_TransformView.SetSynchronizedValues(speed, turnSpeed);
		}
	}

	// Token: 0x040002EF RID: 751
	protected Animator animator;

	// Token: 0x040002F0 RID: 752
	public float DirectionDampTime = 0.25f;

	// Token: 0x040002F1 RID: 753
	public bool ApplyGravity = true;

	// Token: 0x040002F2 RID: 754
	public float SynchronizedMaxSpeed;

	// Token: 0x040002F3 RID: 755
	public float TurnSpeedModifier;

	// Token: 0x040002F4 RID: 756
	public float SynchronizedTurnSpeed;

	// Token: 0x040002F5 RID: 757
	public float SynchronizedSpeedAcceleration;

	// Token: 0x040002F6 RID: 758
	protected PhotonView m_PhotonView;

	// Token: 0x040002F7 RID: 759
	private PhotonTransformView m_TransformView;

	// Token: 0x040002F8 RID: 760
	private float m_SpeedModifier;
}
