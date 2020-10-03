using System;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000413 RID: 1043
	[RequireComponent(typeof(Interactable))]
	public class ComplexThrowable : MonoBehaviour
	{
		// Token: 0x0600203C RID: 8252 RVA: 0x0009F427 File Offset: 0x0009D627
		private void Awake()
		{
			base.GetComponentsInChildren<Rigidbody>(this.rigidBodies);
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x0009F438 File Offset: 0x0009D638
		private void Update()
		{
			for (int i = 0; i < this.holdingHands.Count; i++)
			{
				if (!this.holdingHands[i].GetStandardInteractionButton())
				{
					this.PhysicsDetach(this.holdingHands[i]);
				}
			}
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x0009F481 File Offset: 0x0009D681
		private void OnHandHoverBegin(Hand hand)
		{
			if (this.holdingHands.IndexOf(hand) == -1 && hand.controller != null)
			{
				hand.controller.TriggerHapticPulse(800, EVRButtonId.k_EButton_Axis0);
			}
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x0009F4AC File Offset: 0x0009D6AC
		private void OnHandHoverEnd(Hand hand)
		{
			if (this.holdingHands.IndexOf(hand) == -1 && hand.controller != null)
			{
				hand.controller.TriggerHapticPulse(500, EVRButtonId.k_EButton_Axis0);
			}
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x0009F4D7 File Offset: 0x0009D6D7
		private void HandHoverUpdate(Hand hand)
		{
			if (hand.GetStandardInteractionButtonDown())
			{
				this.PhysicsAttach(hand);
			}
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x0009F4E8 File Offset: 0x0009D6E8
		private void PhysicsAttach(Hand hand)
		{
			this.PhysicsDetach(hand);
			Rigidbody rigidbody = null;
			Vector3 item = Vector3.zero;
			float num = float.MaxValue;
			for (int i = 0; i < this.rigidBodies.Count; i++)
			{
				float num2 = Vector3.Distance(this.rigidBodies[i].worldCenterOfMass, hand.transform.position);
				if (num2 < num)
				{
					rigidbody = this.rigidBodies[i];
					num = num2;
				}
			}
			if (rigidbody == null)
			{
				return;
			}
			if (this.attachMode == ComplexThrowable.AttachMode.FixedJoint)
			{
				Util.FindOrAddComponent<Rigidbody>(hand.gameObject).isKinematic = true;
				hand.gameObject.AddComponent<FixedJoint>().connectedBody = rigidbody;
			}
			hand.HoverLock(null);
			Vector3 b = hand.transform.position - rigidbody.worldCenterOfMass;
			b = Mathf.Min(b.magnitude, 1f) * b.normalized;
			item = rigidbody.transform.InverseTransformPoint(rigidbody.worldCenterOfMass + b);
			hand.AttachObject(base.gameObject, this.attachmentFlags, "");
			this.holdingHands.Add(hand);
			this.holdingBodies.Add(rigidbody);
			this.holdingPoints.Add(item);
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x0009F624 File Offset: 0x0009D824
		private bool PhysicsDetach(Hand hand)
		{
			int num = this.holdingHands.IndexOf(hand);
			if (num != -1)
			{
				this.holdingHands[num].DetachObject(base.gameObject, false);
				this.holdingHands[num].HoverUnlock(null);
				if (this.attachMode == ComplexThrowable.AttachMode.FixedJoint)
				{
					Object.Destroy(this.holdingHands[num].GetComponent<FixedJoint>());
				}
				Util.FastRemove<Hand>(this.holdingHands, num);
				Util.FastRemove<Rigidbody>(this.holdingBodies, num);
				Util.FastRemove<Vector3>(this.holdingPoints, num);
				return true;
			}
			return false;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x0009F6B4 File Offset: 0x0009D8B4
		private void FixedUpdate()
		{
			if (this.attachMode == ComplexThrowable.AttachMode.Force)
			{
				for (int i = 0; i < this.holdingHands.Count; i++)
				{
					Vector3 vector = this.holdingBodies[i].transform.TransformPoint(this.holdingPoints[i]);
					Vector3 a = this.holdingHands[i].transform.position - vector;
					this.holdingBodies[i].AddForceAtPosition(this.attachForce * a, vector, ForceMode.Acceleration);
					this.holdingBodies[i].AddForceAtPosition(-this.attachForceDamper * this.holdingBodies[i].GetPointVelocity(vector), vector, ForceMode.Acceleration);
				}
			}
		}

		// Token: 0x04001DD3 RID: 7635
		public float attachForce = 800f;

		// Token: 0x04001DD4 RID: 7636
		public float attachForceDamper = 25f;

		// Token: 0x04001DD5 RID: 7637
		public ComplexThrowable.AttachMode attachMode;

		// Token: 0x04001DD6 RID: 7638
		[EnumFlags]
		public Hand.AttachmentFlags attachmentFlags;

		// Token: 0x04001DD7 RID: 7639
		private List<Hand> holdingHands = new List<Hand>();

		// Token: 0x04001DD8 RID: 7640
		private List<Rigidbody> holdingBodies = new List<Rigidbody>();

		// Token: 0x04001DD9 RID: 7641
		private List<Vector3> holdingPoints = new List<Vector3>();

		// Token: 0x04001DDA RID: 7642
		private List<Rigidbody> rigidBodies = new List<Rigidbody>();

		// Token: 0x0200076E RID: 1902
		public enum AttachMode
		{
			// Token: 0x04002907 RID: 10503
			FixedJoint,
			// Token: 0x04002908 RID: 10504
			Force
		}
	}
}
