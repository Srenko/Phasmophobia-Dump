using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x0200035F RID: 863
	public class RC_Car : MonoBehaviour
	{
		// Token: 0x06001DC9 RID: 7625 RVA: 0x00097A3C File Offset: 0x00095C3C
		public void SetTouchAxis(Vector2 data)
		{
			this.touchAxis = data;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00097A45 File Offset: 0x00095C45
		public void SetTriggerAxis(float data)
		{
			this.triggerAxis = data;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00097A4E File Offset: 0x00095C4E
		public void ResetCar()
		{
			base.transform.position = this.defaultPosition;
			base.transform.rotation = this.defaultRotation;
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00097A72 File Offset: 0x00095C72
		private void Awake()
		{
			this.rb = base.GetComponent<Rigidbody>();
			this.defaultPosition = base.transform.position;
			this.defaultRotation = base.transform.rotation;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00097AA2 File Offset: 0x00095CA2
		private void FixedUpdate()
		{
			if (this.isJumping)
			{
				this.touchAxis.x = 0f;
			}
			this.CalculateSpeed();
			this.Move();
			this.Turn();
			this.Jump();
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00097AD4 File Offset: 0x00095CD4
		private void CalculateSpeed()
		{
			if (this.touchAxis.y != 0f)
			{
				this.movementSpeed += this.acceleration * this.touchAxis.y;
				this.movementSpeed = Mathf.Clamp(this.movementSpeed, -this.maxAcceleration, this.maxAcceleration);
				return;
			}
			this.Decelerate();
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00097B38 File Offset: 0x00095D38
		private void Decelerate()
		{
			if (this.movementSpeed > 0f)
			{
				this.movementSpeed -= Mathf.Lerp(this.acceleration, this.maxAcceleration, 0f);
				return;
			}
			if (this.movementSpeed < 0f)
			{
				this.movementSpeed += Mathf.Lerp(this.acceleration, -this.maxAcceleration, 0f);
				return;
			}
			this.movementSpeed = 0f;
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00097BB4 File Offset: 0x00095DB4
		private void Move()
		{
			Vector3 b = base.transform.forward * this.movementSpeed * Time.deltaTime;
			this.rb.MovePosition(this.rb.position + b);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00097C00 File Offset: 0x00095E00
		private void Turn()
		{
			float y = this.touchAxis.x * this.rotationSpeed * Time.deltaTime;
			Quaternion rhs = Quaternion.Euler(0f, y, 0f);
			this.rb.MoveRotation(this.rb.rotation * rhs);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00097C54 File Offset: 0x00095E54
		private void Jump()
		{
			if (!this.isJumping && this.triggerAxis > 0f)
			{
				float d = this.triggerAxis * this.jumpPower;
				this.rb.AddRelativeForce(Vector3.up * d);
				this.triggerAxis = 0f;
			}
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00097CA5 File Offset: 0x00095EA5
		private void OnTriggerStay(Collider collider)
		{
			this.isJumping = false;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00097CAE File Offset: 0x00095EAE
		private void OnTriggerExit(Collider collider)
		{
			this.isJumping = true;
		}

		// Token: 0x0400177F RID: 6015
		public float maxAcceleration = 3f;

		// Token: 0x04001780 RID: 6016
		public float jumpPower = 10f;

		// Token: 0x04001781 RID: 6017
		private float acceleration = 0.05f;

		// Token: 0x04001782 RID: 6018
		private float movementSpeed;

		// Token: 0x04001783 RID: 6019
		private float rotationSpeed = 180f;

		// Token: 0x04001784 RID: 6020
		private bool isJumping;

		// Token: 0x04001785 RID: 6021
		private Vector2 touchAxis;

		// Token: 0x04001786 RID: 6022
		private float triggerAxis;

		// Token: 0x04001787 RID: 6023
		private Rigidbody rb;

		// Token: 0x04001788 RID: 6024
		private Vector3 defaultPosition;

		// Token: 0x04001789 RID: 6025
		private Quaternion defaultRotation;
	}
}
