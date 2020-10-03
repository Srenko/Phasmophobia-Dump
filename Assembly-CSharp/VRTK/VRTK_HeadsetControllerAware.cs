using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002F6 RID: 758
	[AddComponentMenu("VRTK/Scripts/Presence/VRTK_HeadsetControllerAware")]
	public class VRTK_HeadsetControllerAware : MonoBehaviour
	{
		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x06001A37 RID: 6711 RVA: 0x0008B204 File Offset: 0x00089404
		// (remove) Token: 0x06001A38 RID: 6712 RVA: 0x0008B23C File Offset: 0x0008943C
		public event HeadsetControllerAwareEventHandler ControllerObscured;

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06001A39 RID: 6713 RVA: 0x0008B274 File Offset: 0x00089474
		// (remove) Token: 0x06001A3A RID: 6714 RVA: 0x0008B2AC File Offset: 0x000894AC
		public event HeadsetControllerAwareEventHandler ControllerUnobscured;

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06001A3B RID: 6715 RVA: 0x0008B2E4 File Offset: 0x000894E4
		// (remove) Token: 0x06001A3C RID: 6716 RVA: 0x0008B31C File Offset: 0x0008951C
		public event HeadsetControllerAwareEventHandler ControllerGlanceEnter;

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x06001A3D RID: 6717 RVA: 0x0008B354 File Offset: 0x00089554
		// (remove) Token: 0x06001A3E RID: 6718 RVA: 0x0008B38C File Offset: 0x0008958C
		public event HeadsetControllerAwareEventHandler ControllerGlanceExit;

		// Token: 0x06001A3F RID: 6719 RVA: 0x0008B3C1 File Offset: 0x000895C1
		public virtual void OnControllerObscured(HeadsetControllerAwareEventArgs e)
		{
			if (this.ControllerObscured != null)
			{
				this.ControllerObscured(this, e);
			}
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0008B3D8 File Offset: 0x000895D8
		public virtual void OnControllerUnobscured(HeadsetControllerAwareEventArgs e)
		{
			if (this.ControllerUnobscured != null)
			{
				this.ControllerUnobscured(this, e);
			}
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0008B3EF File Offset: 0x000895EF
		public virtual void OnControllerGlanceEnter(HeadsetControllerAwareEventArgs e)
		{
			if (this.ControllerGlanceEnter != null)
			{
				this.ControllerGlanceEnter(this, e);
			}
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0008B406 File Offset: 0x00089606
		public virtual void OnControllerGlanceExit(HeadsetControllerAwareEventArgs e)
		{
			if (this.ControllerGlanceExit != null)
			{
				this.ControllerGlanceExit(this, e);
			}
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0008B41D File Offset: 0x0008961D
		public virtual bool LeftControllerObscured()
		{
			return this.leftControllerObscured;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0008B425 File Offset: 0x00089625
		public virtual bool RightControllerObscured()
		{
			return this.rightControllerObscured;
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0008B42D File Offset: 0x0008962D
		public virtual bool LeftControllerGlanced()
		{
			return this.leftControllerGlance;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0008B435 File Offset: 0x00089635
		public virtual bool RightControllerGlanced()
		{
			return this.rightControllerGlance;
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0008B43D File Offset: 0x0008963D
		protected virtual void OnEnable()
		{
			VRTK_ObjectCache.registeredHeadsetControllerAwareness = this;
			this.headset = VRTK_DeviceFinder.HeadsetTransform();
			this.leftController = VRTK_DeviceFinder.GetControllerLeftHand(false);
			this.rightController = VRTK_DeviceFinder.GetControllerRightHand(false);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0008B468 File Offset: 0x00089668
		protected virtual void OnDisable()
		{
			VRTK_ObjectCache.registeredHeadsetControllerAwareness = null;
			this.leftController = null;
			this.rightController = null;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0008B480 File Offset: 0x00089680
		protected virtual void Update()
		{
			if (this.trackLeftController)
			{
				this.RayCastToController(this.leftController, this.customLeftControllerOrigin, ref this.leftControllerObscured, ref this.leftControllerLastState);
			}
			if (this.trackRightController)
			{
				this.RayCastToController(this.rightController, this.customRightControllerOrigin, ref this.rightControllerObscured, ref this.rightControllerLastState);
			}
			this.CheckHeadsetView(this.leftController, this.customLeftControllerOrigin, ref this.leftControllerGlance, ref this.leftControllerGlanceLastState);
			this.CheckHeadsetView(this.rightController, this.customRightControllerOrigin, ref this.rightControllerGlance, ref this.rightControllerGlanceLastState);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0008B518 File Offset: 0x00089718
		protected virtual HeadsetControllerAwareEventArgs SetHeadsetControllerAwareEvent(RaycastHit raycastHit, VRTK_ControllerReference controllerReference)
		{
			HeadsetControllerAwareEventArgs result;
			result.raycastHit = raycastHit;
			result.controllerIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			result.controllerReference = controllerReference;
			return result;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0008B544 File Offset: 0x00089744
		protected virtual void RayCastToController(GameObject controller, Transform customDestination, ref bool obscured, ref bool lastState)
		{
			obscured = false;
			if (controller && controller.gameObject.activeInHierarchy)
			{
				Vector3 endPosition = customDestination ? customDestination.position : controller.transform.position;
				RaycastHit hitInfo;
				if (VRTK_CustomRaycast.Linecast(this.customRaycast, this.headset.position, endPosition, out hitInfo, default(LayerMask), QueryTriggerInteraction.Ignore))
				{
					obscured = true;
				}
				if (lastState != obscured)
				{
					this.ObscuredStateChanged(controller.gameObject, obscured, hitInfo);
				}
				lastState = obscured;
			}
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0008B5C8 File Offset: 0x000897C8
		protected virtual void ObscuredStateChanged(GameObject controller, bool obscured, RaycastHit hitInfo)
		{
			VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(controller);
			if (obscured)
			{
				this.OnControllerObscured(this.SetHeadsetControllerAwareEvent(hitInfo, controllerReference));
				return;
			}
			this.OnControllerUnobscured(this.SetHeadsetControllerAwareEvent(hitInfo, controllerReference));
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0008B5FC File Offset: 0x000897FC
		protected virtual void CheckHeadsetView(GameObject controller, Transform customDestination, ref bool controllerGlance, ref bool controllerGlanceLastState)
		{
			controllerGlance = false;
			if (controller && controller.gameObject.activeInHierarchy)
			{
				Vector3 vector = customDestination ? customDestination.position : controller.transform.position;
				float d = Vector3.Distance(this.headset.position, vector);
				Vector3 b = this.headset.position + this.headset.forward * d;
				if (Vector3.Distance(vector, b) <= this.controllerGlanceRadius)
				{
					controllerGlance = true;
				}
				if (controllerGlanceLastState != controllerGlance)
				{
					this.GlanceStateChanged(controller.gameObject, controllerGlance);
				}
				controllerGlanceLastState = controllerGlance;
			}
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0008B6A0 File Offset: 0x000898A0
		protected virtual void GlanceStateChanged(GameObject controller, bool glance)
		{
			RaycastHit raycastHit = default(RaycastHit);
			VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(controller);
			if (glance)
			{
				this.OnControllerGlanceEnter(this.SetHeadsetControllerAwareEvent(raycastHit, controllerReference));
				return;
			}
			this.OnControllerGlanceExit(this.SetHeadsetControllerAwareEvent(raycastHit, controllerReference));
		}

		// Token: 0x0400154B RID: 5451
		[Tooltip("If this is checked then the left controller will be checked if items obscure it's path from the headset.")]
		public bool trackLeftController = true;

		// Token: 0x0400154C RID: 5452
		[Tooltip("If this is checked then the right controller will be checked if items obscure it's path from the headset.")]
		public bool trackRightController = true;

		// Token: 0x0400154D RID: 5453
		[Tooltip("The radius of the accepted distance from the controller origin point to determine if the controller is being looked at.")]
		public float controllerGlanceRadius = 0.15f;

		// Token: 0x0400154E RID: 5454
		[Tooltip("A custom transform to provide the world space position of the right controller.")]
		public Transform customRightControllerOrigin;

		// Token: 0x0400154F RID: 5455
		[Tooltip("A custom transform to provide the world space position of the left controller.")]
		public Transform customLeftControllerOrigin;

		// Token: 0x04001550 RID: 5456
		[Tooltip("A custom raycaster to use when raycasting to find controllers.")]
		public VRTK_CustomRaycast customRaycast;

		// Token: 0x04001555 RID: 5461
		protected GameObject leftController;

		// Token: 0x04001556 RID: 5462
		protected GameObject rightController;

		// Token: 0x04001557 RID: 5463
		protected Transform headset;

		// Token: 0x04001558 RID: 5464
		protected bool leftControllerObscured;

		// Token: 0x04001559 RID: 5465
		protected bool rightControllerObscured;

		// Token: 0x0400155A RID: 5466
		protected bool leftControllerLastState;

		// Token: 0x0400155B RID: 5467
		protected bool rightControllerLastState;

		// Token: 0x0400155C RID: 5468
		protected bool leftControllerGlance;

		// Token: 0x0400155D RID: 5469
		protected bool rightControllerGlance;

		// Token: 0x0400155E RID: 5470
		protected bool leftControllerGlanceLastState;

		// Token: 0x0400155F RID: 5471
		protected bool rightControllerGlanceLastState;
	}
}
