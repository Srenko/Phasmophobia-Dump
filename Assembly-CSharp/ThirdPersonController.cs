using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class ThirdPersonController : MonoBehaviour
{
	// Token: 0x06000264 RID: 612 RVA: 0x000104AB File Offset: 0x0000E6AB
	public void Start()
	{
		Application.targetFrameRate = 60;
	}

	// Token: 0x06000265 RID: 613 RVA: 0x000104B4 File Offset: 0x0000E6B4
	private void Awake()
	{
		this.moveDirection = base.transform.TransformDirection(Vector3.forward);
		this._animation = base.GetComponent<Animation>();
		if (!this._animation)
		{
			Debug.Log("The character you would like to control doesn't have animations. Moving her might look weird.");
		}
		if (!this.idleAnimation)
		{
			this._animation = null;
			Debug.Log("No idle animation found. Turning off animations.");
		}
		if (!this.walkAnimation)
		{
			this._animation = null;
			Debug.Log("No walk animation found. Turning off animations.");
		}
		if (!this.runAnimation)
		{
			this._animation = null;
			Debug.Log("No run animation found. Turning off animations.");
		}
		if (!this.jumpPoseAnimation && this.canJump)
		{
			this._animation = null;
			Debug.Log("No jump animation found and the character has canJump enabled. Turning off animations.");
		}
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0001057C File Offset: 0x0000E77C
	private void UpdateSmoothedMovementDirection()
	{
		Transform transform = Camera.main.transform;
		bool flag = this.IsGrounded();
		Vector3 vector = transform.TransformDirection(Vector3.forward);
		vector.y = 0f;
		vector = vector.normalized;
		Vector3 a = new Vector3(vector.z, 0f, -vector.x);
		float axisRaw = Input.GetAxisRaw("Vertical");
		float axisRaw2 = Input.GetAxisRaw("Horizontal");
		if (axisRaw < -0.2f)
		{
			this.movingBack = true;
		}
		else
		{
			this.movingBack = false;
		}
		bool flag2 = this.isMoving;
		this.isMoving = (Mathf.Abs(axisRaw2) > 0.1f || Mathf.Abs(axisRaw) > 0.1f);
		Vector3 vector2 = axisRaw2 * a + axisRaw * vector;
		if (flag)
		{
			this.lockCameraTimer += Time.deltaTime;
			if (this.isMoving != flag2)
			{
				this.lockCameraTimer = 0f;
			}
			if (vector2 != Vector3.zero)
			{
				if (this.moveSpeed < this.walkSpeed * 0.9f && flag)
				{
					this.moveDirection = vector2.normalized;
				}
				else
				{
					this.moveDirection = Vector3.RotateTowards(this.moveDirection, vector2, this.rotateSpeed * 0.0174532924f * Time.deltaTime, 1000f);
					this.moveDirection = this.moveDirection.normalized;
				}
			}
			float t = this.speedSmoothing * Time.deltaTime;
			float num = Mathf.Min(vector2.magnitude, 1f);
			this._characterState = CharacterState.Idle;
			if (Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift))
			{
				num *= this.runSpeed;
				this._characterState = CharacterState.Running;
			}
			else if (Time.time - this.trotAfterSeconds > this.walkTimeStart)
			{
				num *= this.trotSpeed;
				this._characterState = CharacterState.Trotting;
			}
			else
			{
				num *= this.walkSpeed;
				this._characterState = CharacterState.Walking;
			}
			this.moveSpeed = Mathf.Lerp(this.moveSpeed, num, t);
			if (this.moveSpeed < this.walkSpeed * 0.3f)
			{
				this.walkTimeStart = Time.time;
				return;
			}
		}
		else
		{
			if (this.jumping)
			{
				this.lockCameraTimer = 0f;
			}
			if (this.isMoving)
			{
				this.inAirVelocity += vector2.normalized * Time.deltaTime * this.inAirControlAcceleration;
			}
		}
	}

	// Token: 0x06000267 RID: 615 RVA: 0x000107EC File Offset: 0x0000E9EC
	private void ApplyJumping()
	{
		if (this.lastJumpTime + this.jumpRepeatTime > Time.time)
		{
			return;
		}
		if (this.IsGrounded() && this.canJump && Time.time < this.lastJumpButtonTime + this.jumpTimeout)
		{
			this.verticalSpeed = this.CalculateJumpVerticalSpeed(this.jumpHeight);
			base.SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x00010850 File Offset: 0x0000EA50
	private void ApplyGravity()
	{
		if (this.isControllable)
		{
			if (this.jumping && !this.jumpingReachedApex && this.verticalSpeed <= 0f)
			{
				this.jumpingReachedApex = true;
				base.SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
			}
			if (this.IsGrounded())
			{
				this.verticalSpeed = 0f;
				return;
			}
			this.verticalSpeed -= this.gravity * Time.deltaTime;
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x000108C2 File Offset: 0x0000EAC2
	private float CalculateJumpVerticalSpeed(float targetJumpHeight)
	{
		return Mathf.Sqrt(2f * targetJumpHeight * this.gravity);
	}

	// Token: 0x0600026A RID: 618 RVA: 0x000108D7 File Offset: 0x0000EAD7
	private void DidJump()
	{
		this.jumping = true;
		this.jumpingReachedApex = false;
		this.lastJumpTime = Time.time;
		this.lastJumpButtonTime = -10f;
		this._characterState = CharacterState.Jumping;
	}

	// Token: 0x0600026B RID: 619 RVA: 0x00010904 File Offset: 0x0000EB04
	private void Update()
	{
		if (this.isControllable)
		{
			if (Input.GetButtonDown("Jump"))
			{
				this.lastJumpButtonTime = Time.time;
			}
			this.UpdateSmoothedMovementDirection();
			this.ApplyGravity();
			this.ApplyJumping();
			Vector3 vector = this.moveDirection * this.moveSpeed + new Vector3(0f, this.verticalSpeed, 0f) + this.inAirVelocity;
			vector *= Time.deltaTime;
			CharacterController component = base.GetComponent<CharacterController>();
			this.collisionFlags = component.Move(vector);
		}
		this.velocity = (base.transform.position - this.lastPos) * 25f;
		if (this._animation)
		{
			if (this._characterState == CharacterState.Jumping)
			{
				if (!this.jumpingReachedApex)
				{
					this._animation[this.jumpPoseAnimation.name].speed = this.jumpAnimationSpeed;
					this._animation[this.jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
					this._animation.CrossFade(this.jumpPoseAnimation.name);
				}
				else
				{
					this._animation[this.jumpPoseAnimation.name].speed = -this.landAnimationSpeed;
					this._animation[this.jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
					this._animation.CrossFade(this.jumpPoseAnimation.name);
				}
			}
			else if (this.isControllable && this.velocity.sqrMagnitude < 0.001f)
			{
				this._characterState = CharacterState.Idle;
				this._animation.CrossFade(this.idleAnimation.name);
			}
			else if (this._characterState == CharacterState.Idle)
			{
				this._animation.CrossFade(this.idleAnimation.name);
			}
			else if (this._characterState == CharacterState.Running)
			{
				this._animation[this.runAnimation.name].speed = this.runMaxAnimationSpeed;
				if (this.isControllable)
				{
					this._animation[this.runAnimation.name].speed = Mathf.Clamp(this.velocity.magnitude, 0f, this.runMaxAnimationSpeed);
				}
				this._animation.CrossFade(this.runAnimation.name);
			}
			else if (this._characterState == CharacterState.Trotting)
			{
				this._animation[this.walkAnimation.name].speed = this.trotMaxAnimationSpeed;
				if (this.isControllable)
				{
					this._animation[this.walkAnimation.name].speed = Mathf.Clamp(this.velocity.magnitude, 0f, this.trotMaxAnimationSpeed);
				}
				this._animation.CrossFade(this.walkAnimation.name);
			}
			else if (this._characterState == CharacterState.Walking)
			{
				this._animation[this.walkAnimation.name].speed = this.walkMaxAnimationSpeed;
				if (this.isControllable)
				{
					this._animation[this.walkAnimation.name].speed = Mathf.Clamp(this.velocity.magnitude, 0f, this.walkMaxAnimationSpeed);
				}
				this._animation.CrossFade(this.walkAnimation.name);
			}
		}
		if (this.IsGrounded())
		{
			base.transform.rotation = Quaternion.LookRotation(this.moveDirection);
		}
		if (this.IsGrounded())
		{
			this.lastGroundedTime = Time.time;
			this.inAirVelocity = Vector3.zero;
			if (this.jumping)
			{
				this.jumping = false;
				base.SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
			}
		}
		this.lastPos = base.transform.position;
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0000E258 File Offset: 0x0000C458
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		float y = hit.moveDirection.y;
	}

	// Token: 0x0600026D RID: 621 RVA: 0x00010CDF File Offset: 0x0000EEDF
	public float GetSpeed()
	{
		return this.moveSpeed;
	}

	// Token: 0x0600026E RID: 622 RVA: 0x00010CE7 File Offset: 0x0000EEE7
	public bool IsJumping()
	{
		return this.jumping;
	}

	// Token: 0x0600026F RID: 623 RVA: 0x00010CEF File Offset: 0x0000EEEF
	public bool IsGrounded()
	{
		return (this.collisionFlags & CollisionFlags.Below) > CollisionFlags.None;
	}

	// Token: 0x06000270 RID: 624 RVA: 0x00010CFC File Offset: 0x0000EEFC
	public Vector3 GetDirection()
	{
		return this.moveDirection;
	}

	// Token: 0x06000271 RID: 625 RVA: 0x00010D04 File Offset: 0x0000EF04
	public bool IsMovingBackwards()
	{
		return this.movingBack;
	}

	// Token: 0x06000272 RID: 626 RVA: 0x00010D0C File Offset: 0x0000EF0C
	public float GetLockCameraTimer()
	{
		return this.lockCameraTimer;
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0000E2A1 File Offset: 0x0000C4A1
	public bool IsMoving()
	{
		return Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f;
	}

	// Token: 0x06000274 RID: 628 RVA: 0x00010D14 File Offset: 0x0000EF14
	public bool HasJumpReachedApex()
	{
		return this.jumpingReachedApex;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x00010D1C File Offset: 0x0000EF1C
	public bool IsGroundedWithTimeout()
	{
		return this.lastGroundedTime + this.groundedTimeout > Time.time;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0000E2E7 File Offset: 0x0000C4E7
	public void Reset()
	{
		base.gameObject.tag = "Player";
	}

	// Token: 0x040002AD RID: 685
	public AnimationClip idleAnimation;

	// Token: 0x040002AE RID: 686
	public AnimationClip walkAnimation;

	// Token: 0x040002AF RID: 687
	public AnimationClip runAnimation;

	// Token: 0x040002B0 RID: 688
	public AnimationClip jumpPoseAnimation;

	// Token: 0x040002B1 RID: 689
	public float walkMaxAnimationSpeed = 0.75f;

	// Token: 0x040002B2 RID: 690
	public float trotMaxAnimationSpeed = 1f;

	// Token: 0x040002B3 RID: 691
	public float runMaxAnimationSpeed = 1f;

	// Token: 0x040002B4 RID: 692
	public float jumpAnimationSpeed = 1.15f;

	// Token: 0x040002B5 RID: 693
	public float landAnimationSpeed = 1f;

	// Token: 0x040002B6 RID: 694
	private Animation _animation;

	// Token: 0x040002B7 RID: 695
	public CharacterState _characterState;

	// Token: 0x040002B8 RID: 696
	public float walkSpeed = 2f;

	// Token: 0x040002B9 RID: 697
	public float trotSpeed = 4f;

	// Token: 0x040002BA RID: 698
	public float runSpeed = 6f;

	// Token: 0x040002BB RID: 699
	public float inAirControlAcceleration = 3f;

	// Token: 0x040002BC RID: 700
	public float jumpHeight = 0.5f;

	// Token: 0x040002BD RID: 701
	public float gravity = 20f;

	// Token: 0x040002BE RID: 702
	public float speedSmoothing = 10f;

	// Token: 0x040002BF RID: 703
	public float rotateSpeed = 500f;

	// Token: 0x040002C0 RID: 704
	public float trotAfterSeconds = 3f;

	// Token: 0x040002C1 RID: 705
	public bool canJump;

	// Token: 0x040002C2 RID: 706
	private float jumpRepeatTime = 0.05f;

	// Token: 0x040002C3 RID: 707
	private float jumpTimeout = 0.15f;

	// Token: 0x040002C4 RID: 708
	private float groundedTimeout = 0.25f;

	// Token: 0x040002C5 RID: 709
	private float lockCameraTimer;

	// Token: 0x040002C6 RID: 710
	private Vector3 moveDirection = Vector3.zero;

	// Token: 0x040002C7 RID: 711
	private float verticalSpeed;

	// Token: 0x040002C8 RID: 712
	private float moveSpeed;

	// Token: 0x040002C9 RID: 713
	private CollisionFlags collisionFlags;

	// Token: 0x040002CA RID: 714
	private bool jumping;

	// Token: 0x040002CB RID: 715
	private bool jumpingReachedApex;

	// Token: 0x040002CC RID: 716
	private bool movingBack;

	// Token: 0x040002CD RID: 717
	private bool isMoving;

	// Token: 0x040002CE RID: 718
	private float walkTimeStart;

	// Token: 0x040002CF RID: 719
	private float lastJumpButtonTime = -10f;

	// Token: 0x040002D0 RID: 720
	private float lastJumpTime = -1f;

	// Token: 0x040002D1 RID: 721
	private Vector3 inAirVelocity = Vector3.zero;

	// Token: 0x040002D2 RID: 722
	private float lastGroundedTime;

	// Token: 0x040002D3 RID: 723
	public bool isControllable = true;

	// Token: 0x040002D4 RID: 724
	private Vector3 lastPos;

	// Token: 0x040002D5 RID: 725
	private Vector3 velocity = Vector3.zero;
}
