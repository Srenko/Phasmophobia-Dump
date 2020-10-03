using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000439 RID: 1081
	[RequireComponent(typeof(Interactable))]
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(VelocityEstimator))]
	public class Throwable : MonoBehaviour
	{
		// Token: 0x0600210F RID: 8463 RVA: 0x000A30E1 File Offset: 0x000A12E1
		private void Awake()
		{
			this.velocityEstimator = base.GetComponent<VelocityEstimator>();
			if (this.attachEaseIn)
			{
				this.attachmentFlags &= ~Hand.AttachmentFlags.SnapOnAttach;
			}
			base.GetComponent<Rigidbody>().maxAngularVelocity = 50f;
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000A3118 File Offset: 0x000A1318
		private void OnHandHoverBegin(Hand hand)
		{
			bool flag = false;
			if (!this.attached && hand.GetStandardInteractionButton() && base.GetComponent<Rigidbody>().velocity.magnitude >= this.catchSpeedThreshold)
			{
				hand.AttachObject(base.gameObject, this.attachmentFlags, this.attachmentPoint);
				flag = false;
			}
			if (flag)
			{
				ControllerButtonHints.ShowButtonHint(hand, new EVRButtonId[]
				{
					EVRButtonId.k_EButton_Axis1
				});
			}
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x000A3180 File Offset: 0x000A1380
		private void OnHandHoverEnd(Hand hand)
		{
			ControllerButtonHints.HideButtonHint(hand, new EVRButtonId[]
			{
				EVRButtonId.k_EButton_Axis1
			});
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x000A3193 File Offset: 0x000A1393
		private void HandHoverUpdate(Hand hand)
		{
			if (hand.GetStandardInteractionButtonDown())
			{
				hand.AttachObject(base.gameObject, this.attachmentFlags, this.attachmentPoint);
				ControllerButtonHints.HideButtonHint(hand, new EVRButtonId[]
				{
					EVRButtonId.k_EButton_Axis1
				});
			}
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x000A31C8 File Offset: 0x000A13C8
		private void OnAttachedToHand(Hand hand)
		{
			this.attached = true;
			this.onPickUp.Invoke();
			hand.HoverLock(null);
			Rigidbody component = base.GetComponent<Rigidbody>();
			component.isKinematic = true;
			component.interpolation = RigidbodyInterpolation.None;
			if (hand.controller == null)
			{
				this.velocityEstimator.BeginEstimatingVelocity();
			}
			this.attachTime = Time.time;
			this.attachPosition = base.transform.position;
			this.attachRotation = base.transform.rotation;
			if (this.attachEaseIn)
			{
				this.attachEaseInTransform = hand.transform;
				if (!Util.IsNullOrEmpty<string>(this.attachEaseInAttachmentNames))
				{
					float num = float.MaxValue;
					for (int i = 0; i < this.attachEaseInAttachmentNames.Length; i++)
					{
						Transform attachmentTransform = hand.GetAttachmentTransform(this.attachEaseInAttachmentNames[i]);
						float num2 = Quaternion.Angle(attachmentTransform.rotation, this.attachRotation);
						if (num2 < num)
						{
							this.attachEaseInTransform = attachmentTransform;
							num = num2;
						}
					}
				}
			}
			this.snapAttachEaseInCompleted = false;
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x000A32B0 File Offset: 0x000A14B0
		private void OnDetachedFromHand(Hand hand)
		{
			this.attached = false;
			this.onDetachFromHand.Invoke();
			hand.HoverUnlock(null);
			Rigidbody component = base.GetComponent<Rigidbody>();
			component.isKinematic = false;
			component.interpolation = RigidbodyInterpolation.Interpolate;
			Vector3 b = Vector3.zero;
			Vector3 a = Vector3.zero;
			Vector3 vector = Vector3.zero;
			if (hand.controller == null)
			{
				this.velocityEstimator.FinishEstimatingVelocity();
				a = this.velocityEstimator.GetVelocityEstimate();
				vector = this.velocityEstimator.GetAngularVelocityEstimate();
				b = this.velocityEstimator.transform.position;
			}
			else
			{
				a = Player.instance.trackingOriginTransform.TransformVector(hand.controller.velocity);
				vector = Player.instance.trackingOriginTransform.TransformVector(hand.controller.angularVelocity);
				b = hand.transform.position;
			}
			Vector3 rhs = base.transform.TransformPoint(component.centerOfMass) - b;
			component.velocity = a + Vector3.Cross(vector, rhs);
			component.angularVelocity = vector;
			float num = Time.fixedDeltaTime + Time.fixedTime - Time.time;
			base.transform.position += num * a;
			float num2 = 57.29578f * vector.magnitude;
			Vector3 normalized = vector.normalized;
			base.transform.rotation *= Quaternion.AngleAxis(num2 * num, normalized);
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000A341C File Offset: 0x000A161C
		private void HandAttachedUpdate(Hand hand)
		{
			if (!hand.GetStandardInteractionButton())
			{
				base.StartCoroutine(this.LateDetach(hand));
			}
			if (this.attachEaseIn)
			{
				float num = Util.RemapNumberClamped(Time.time, this.attachTime, this.attachTime + this.snapAttachEaseInTime, 0f, 1f);
				if (num < 1f)
				{
					num = this.snapAttachEaseInCurve.Evaluate(num);
					base.transform.position = Vector3.Lerp(this.attachPosition, this.attachEaseInTransform.position, num);
					base.transform.rotation = Quaternion.Lerp(this.attachRotation, this.attachEaseInTransform.rotation, num);
					return;
				}
				if (!this.snapAttachEaseInCompleted)
				{
					base.gameObject.SendMessage("OnThrowableAttachEaseInCompleted", hand, SendMessageOptions.DontRequireReceiver);
					this.snapAttachEaseInCompleted = true;
				}
			}
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000A34ED File Offset: 0x000A16ED
		private IEnumerator LateDetach(Hand hand)
		{
			yield return new WaitForEndOfFrame();
			hand.DetachObject(base.gameObject, this.restoreOriginalParent);
			yield break;
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x000A3503 File Offset: 0x000A1703
		private void OnHandFocusAcquired(Hand hand)
		{
			base.gameObject.SetActive(true);
			this.velocityEstimator.BeginEstimatingVelocity();
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000A351C File Offset: 0x000A171C
		private void OnHandFocusLost(Hand hand)
		{
			base.gameObject.SetActive(false);
			this.velocityEstimator.FinishEstimatingVelocity();
		}

		// Token: 0x04001E95 RID: 7829
		[EnumFlags]
		[Tooltip("The flags used to attach this object to the hand.")]
		public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.ParentToHand;

		// Token: 0x04001E96 RID: 7830
		[Tooltip("Name of the attachment transform under in the hand's hierarchy which the object should should snap to.")]
		public string attachmentPoint;

		// Token: 0x04001E97 RID: 7831
		[Tooltip("How fast must this object be moving to attach due to a trigger hold instead of a trigger press?")]
		public float catchSpeedThreshold;

		// Token: 0x04001E98 RID: 7832
		[Tooltip("When detaching the object, should it return to its original parent?")]
		public bool restoreOriginalParent;

		// Token: 0x04001E99 RID: 7833
		public bool attachEaseIn;

		// Token: 0x04001E9A RID: 7834
		public AnimationCurve snapAttachEaseInCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x04001E9B RID: 7835
		public float snapAttachEaseInTime = 0.15f;

		// Token: 0x04001E9C RID: 7836
		public string[] attachEaseInAttachmentNames;

		// Token: 0x04001E9D RID: 7837
		private VelocityEstimator velocityEstimator;

		// Token: 0x04001E9E RID: 7838
		private bool attached;

		// Token: 0x04001E9F RID: 7839
		private float attachTime;

		// Token: 0x04001EA0 RID: 7840
		private Vector3 attachPosition;

		// Token: 0x04001EA1 RID: 7841
		private Quaternion attachRotation;

		// Token: 0x04001EA2 RID: 7842
		private Transform attachEaseInTransform;

		// Token: 0x04001EA3 RID: 7843
		public UnityEvent onPickUp;

		// Token: 0x04001EA4 RID: 7844
		public UnityEvent onDetachFromHand;

		// Token: 0x04001EA5 RID: 7845
		public bool snapAttachEaseInCompleted;
	}
}
