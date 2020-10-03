using System;
using System.Collections;
using UnityEngine;

namespace VRTK.SecondaryControllerGrabActions
{
	// Token: 0x0200033D RID: 829
	[AddComponentMenu("VRTK/Scripts/Interactions/Secondary Controller Grab Actions/VRTK_ControlDirectionGrabAction")]
	public class VRTK_ControlDirectionGrabAction : VRTK_BaseGrabAction
	{
		// Token: 0x06001D0D RID: 7437 RVA: 0x00094D20 File Offset: 0x00092F20
		public override void Initialise(VRTK_InteractableObject currentGrabbdObject, VRTK_InteractGrab currentPrimaryGrabbingObject, VRTK_InteractGrab currentSecondaryGrabbingObject, Transform primaryGrabPoint, Transform secondaryGrabPoint)
		{
			base.Initialise(currentGrabbdObject, currentPrimaryGrabbingObject, currentSecondaryGrabbingObject, primaryGrabPoint, secondaryGrabPoint);
			this.initialPosition = currentGrabbdObject.transform.localPosition;
			this.initialRotation = currentGrabbdObject.transform.localRotation;
			this.StopRealignOnRelease();
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x00094D58 File Offset: 0x00092F58
		public override void ResetAction()
		{
			this.releaseRotation = base.transform.localRotation;
			if (!this.grabbedObject.grabAttachMechanicScript.precisionGrab)
			{
				if (this.releaseSnapSpeed < 3.40282347E+38f && this.releaseSnapSpeed > 0f)
				{
					this.snappingOnRelease = base.StartCoroutine(this.RealignOnRelease());
				}
				else if (this.releaseSnapSpeed == 0f)
				{
					base.transform.localRotation = this.initialRotation;
				}
			}
			base.ResetAction();
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x00094DDA File Offset: 0x00092FDA
		public override void OnDropAction()
		{
			base.OnDropAction();
			this.StopRealignOnRelease();
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x00094DE8 File Offset: 0x00092FE8
		public override void ProcessUpdate()
		{
			base.ProcessUpdate();
			this.CheckForceStopDistance(this.ungrabDistance);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00094DFC File Offset: 0x00092FFC
		public override void ProcessFixedUpdate()
		{
			base.ProcessFixedUpdate();
			if (this.initialised)
			{
				this.AimObject();
			}
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x00094E12 File Offset: 0x00093012
		protected virtual void StopRealignOnRelease()
		{
			if (this.snappingOnRelease != null)
			{
				base.StopCoroutine(this.snappingOnRelease);
			}
			this.snappingOnRelease = null;
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x00094E2F File Offset: 0x0009302F
		protected virtual IEnumerator RealignOnRelease()
		{
			float elapsedTime = 0f;
			while (elapsedTime < this.releaseSnapSpeed)
			{
				base.transform.localRotation = Quaternion.Lerp(this.releaseRotation, this.initialRotation, elapsedTime / this.releaseSnapSpeed);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			base.transform.localRotation = this.initialRotation;
			base.transform.localPosition = this.initialPosition;
			yield break;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x00094E40 File Offset: 0x00093040
		protected virtual void AimObject()
		{
			if (this.lockZRotation)
			{
				this.ZLockedAim();
			}
			else
			{
				base.transform.rotation = Quaternion.LookRotation(this.secondaryGrabbingObject.transform.position - this.primaryGrabbingObject.transform.position, this.secondaryGrabbingObject.transform.TransformDirection(Vector3.forward));
			}
			if (this.grabbedObject.grabAttachMechanicScript.precisionGrab)
			{
				base.transform.Translate(this.primaryGrabbingObject.controllerAttachPoint.transform.position - this.primaryInitialGrabPoint.position, Space.World);
			}
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x00094EEC File Offset: 0x000930EC
		protected virtual void ZLockedAim()
		{
			Vector3 normalized = (this.secondaryGrabbingObject.transform.position - this.primaryGrabbingObject.transform.position).normalized;
			Quaternion quaternion = Quaternion.LookRotation(normalized, Vector3.Cross(-this.primaryGrabbingObject.transform.right, normalized).normalized);
			float num;
			Vector3 vector;
			(Quaternion.Inverse(this.grabbedObject.transform.rotation) * quaternion).ToAngleAxis(out num, out vector);
			if (num > 180f)
			{
				num -= 360f;
			}
			num = Mathf.Abs(num);
			Quaternion quaternion2 = Quaternion.LookRotation(normalized, this.primaryGrabbingObject.transform.forward);
			float num2;
			Vector3 vector2;
			(Quaternion.Inverse(this.grabbedObject.transform.rotation) * quaternion2).ToAngleAxis(out num2, out vector2);
			if (num2 > 180f)
			{
				num2 -= 360f;
			}
			num2 = Mathf.Abs(num2);
			this.grabbedObject.transform.rotation = ((num2 < num) ? quaternion2 : quaternion);
		}

		// Token: 0x0400170B RID: 5899
		[Tooltip("The distance the secondary controller must move away from the original grab position before the secondary controller auto ungrabs the object.")]
		public float ungrabDistance = 1f;

		// Token: 0x0400170C RID: 5900
		[Tooltip("The speed in which the object will snap back to it's original rotation when the secondary controller stops grabbing it. `0` for instant snap, `infinity` for no snap back.")]
		public float releaseSnapSpeed = 0.1f;

		// Token: 0x0400170D RID: 5901
		[Tooltip("Prevent the secondary controller rotating the grabbed object through it's z-axis.")]
		public bool lockZRotation = true;

		// Token: 0x0400170E RID: 5902
		protected Vector3 initialPosition;

		// Token: 0x0400170F RID: 5903
		protected Quaternion initialRotation;

		// Token: 0x04001710 RID: 5904
		protected Quaternion releaseRotation;

		// Token: 0x04001711 RID: 5905
		protected Coroutine snappingOnRelease;
	}
}
