using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000361 RID: 865
	public class RealGun : VRTK_InteractableObject
	{
		// Token: 0x06001DDD RID: 7645 RVA: 0x00097DD4 File Offset: 0x00095FD4
		private void ToggleCollision(Rigidbody objRB, Collider objCol, bool state)
		{
			objRB.isKinematic = state;
			objCol.isTrigger = state;
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x00097DE4 File Offset: 0x00095FE4
		private void ToggleSlide(bool state)
		{
			if (!state)
			{
				this.slide.ForceStopInteracting();
			}
			this.slide.enabled = state;
			this.slide.isGrabbable = state;
			this.ToggleCollision(this.slideRigidbody, this.slideCollider, state);
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x00097E1F File Offset: 0x0009601F
		private void ToggleSafetySwitch(bool state)
		{
			if (!state)
			{
				this.safetySwitch.ForceStopInteracting();
			}
			this.ToggleCollision(this.safetySwitchRigidbody, this.safetySwitchCollider, state);
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00097E44 File Offset: 0x00096044
		public override void Grabbed(VRTK_InteractGrab currentGrabbingObject)
		{
			base.Grabbed(currentGrabbingObject);
			this.controllerEvents = currentGrabbingObject.GetComponent<VRTK_ControllerEvents>();
			this.ToggleSlide(true);
			this.ToggleSafetySwitch(true);
			if (VRTK_DeviceFinder.GetControllerHand(currentGrabbingObject.controllerEvents.gameObject) == SDK_BaseController.ControllerHand.Left)
			{
				this.allowedTouchControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
				this.allowedUseControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
				this.slide.allowedGrabControllers = VRTK_InteractableObject.AllowedController.RightOnly;
				this.safetySwitch.allowedGrabControllers = VRTK_InteractableObject.AllowedController.RightOnly;
				return;
			}
			if (VRTK_DeviceFinder.GetControllerHand(currentGrabbingObject.controllerEvents.gameObject) == SDK_BaseController.ControllerHand.Right)
			{
				this.allowedTouchControllers = VRTK_InteractableObject.AllowedController.RightOnly;
				this.allowedUseControllers = VRTK_InteractableObject.AllowedController.RightOnly;
				this.slide.allowedGrabControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
				this.safetySwitch.allowedGrabControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
			}
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00097EE8 File Offset: 0x000960E8
		public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
		{
			base.Ungrabbed(previousGrabbingObject);
			this.ToggleSlide(false);
			this.ToggleSafetySwitch(false);
			this.allowedTouchControllers = VRTK_InteractableObject.AllowedController.Both;
			this.allowedUseControllers = VRTK_InteractableObject.AllowedController.Both;
			this.slide.allowedGrabControllers = VRTK_InteractableObject.AllowedController.Both;
			this.safetySwitch.allowedGrabControllers = VRTK_InteractableObject.AllowedController.Both;
			this.controllerEvents = null;
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x00097F38 File Offset: 0x00096138
		public override void StartUsing(VRTK_InteractUse currentUsingObject)
		{
			base.StartUsing(currentUsingObject);
			if (this.safetySwitch.safetyOff)
			{
				this.slide.Fire();
				this.FireBullet();
				VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(this.controllerEvents.gameObject), 0.63f, 0.2f, 0.01f);
				return;
			}
			VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(this.controllerEvents.gameObject), 0.08f, 0.1f, 0.01f);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x00097FB4 File Offset: 0x000961B4
		protected override void Awake()
		{
			base.Awake();
			this.bullet = base.transform.Find("Bullet").gameObject;
			this.bullet.SetActive(false);
			this.trigger = base.transform.Find("TriggerHolder").gameObject;
			this.slide = base.transform.Find("Slide").GetComponent<RealGun_Slide>();
			this.slideRigidbody = this.slide.GetComponent<Rigidbody>();
			this.slideCollider = this.slide.GetComponent<Collider>();
			this.safetySwitch = base.transform.Find("SafetySwitch").GetComponent<RealGun_SafetySwitch>();
			this.safetySwitchRigidbody = this.safetySwitch.GetComponent<Rigidbody>();
			this.safetySwitchCollider = this.safetySwitch.GetComponent<Collider>();
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x00098084 File Offset: 0x00096284
		protected override void Update()
		{
			base.Update();
			if (this.controllerEvents)
			{
				float y = this.maxTriggerRotation * this.controllerEvents.GetTriggerAxis() - this.minTriggerRotation;
				this.trigger.transform.localEulerAngles = new Vector3(0f, y, 0f);
				return;
			}
			this.trigger.transform.localEulerAngles = new Vector3(0f, this.minTriggerRotation, 0f);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00098104 File Offset: 0x00096304
		private void FireBullet()
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.bullet, this.bullet.transform.position, this.bullet.transform.rotation);
			gameObject.SetActive(true);
			gameObject.GetComponent<Rigidbody>().AddForce(this.bullet.transform.forward * this.bulletSpeed);
			Object.Destroy(gameObject, this.bulletLife);
		}

		// Token: 0x0400178C RID: 6028
		public float bulletSpeed = 200f;

		// Token: 0x0400178D RID: 6029
		public float bulletLife = 5f;

		// Token: 0x0400178E RID: 6030
		private GameObject bullet;

		// Token: 0x0400178F RID: 6031
		private GameObject trigger;

		// Token: 0x04001790 RID: 6032
		private RealGun_Slide slide;

		// Token: 0x04001791 RID: 6033
		private RealGun_SafetySwitch safetySwitch;

		// Token: 0x04001792 RID: 6034
		private Rigidbody slideRigidbody;

		// Token: 0x04001793 RID: 6035
		private Collider slideCollider;

		// Token: 0x04001794 RID: 6036
		private Rigidbody safetySwitchRigidbody;

		// Token: 0x04001795 RID: 6037
		private Collider safetySwitchCollider;

		// Token: 0x04001796 RID: 6038
		private VRTK_ControllerEvents controllerEvents;

		// Token: 0x04001797 RID: 6039
		private float minTriggerRotation = -10f;

		// Token: 0x04001798 RID: 6040
		private float maxTriggerRotation = 45f;
	}
}
