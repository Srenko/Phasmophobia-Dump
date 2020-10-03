using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002D2 RID: 722
	public abstract class VRTK_ObjectControl : MonoBehaviour
	{
		// Token: 0x14000094 RID: 148
		// (add) Token: 0x060017F9 RID: 6137 RVA: 0x0007FF04 File Offset: 0x0007E104
		// (remove) Token: 0x060017FA RID: 6138 RVA: 0x0007FF3C File Offset: 0x0007E13C
		public event ObjectControlEventHandler XAxisChanged;

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x060017FB RID: 6139 RVA: 0x0007FF74 File Offset: 0x0007E174
		// (remove) Token: 0x060017FC RID: 6140 RVA: 0x0007FFAC File Offset: 0x0007E1AC
		public event ObjectControlEventHandler YAxisChanged;

		// Token: 0x060017FD RID: 6141 RVA: 0x0007FFE1 File Offset: 0x0007E1E1
		public virtual void OnXAxisChanged(ObjectControlEventArgs e)
		{
			if (this.XAxisChanged != null)
			{
				this.XAxisChanged(this, e);
			}
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0007FFF8 File Offset: 0x0007E1F8
		public virtual void OnYAxisChanged(ObjectControlEventArgs e)
		{
			if (this.YAxisChanged != null)
			{
				this.YAxisChanged(this, e);
			}
		}

		// Token: 0x060017FF RID: 6143
		protected abstract void ControlFixedUpdate();

		// Token: 0x06001800 RID: 6144
		protected abstract VRTK_ObjectControl GetOtherControl();

		// Token: 0x06001801 RID: 6145
		protected abstract bool IsInAction();

		// Token: 0x06001802 RID: 6146
		protected abstract void SetListeners(bool state);

		// Token: 0x06001803 RID: 6147 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00080010 File Offset: 0x0007E210
		protected virtual void OnEnable()
		{
			this.currentAxis = Vector2.zero;
			this.storedAxis = Vector2.zero;
			this.controllerEvents = ((this.controller != null) ? this.controller : base.GetComponent<VRTK_ControllerEvents>());
			if (!this.controllerEvents)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_NOT_INJECTED, new object[]
				{
					"VRTK_ObjectControl",
					"VRTK_ControllerEvents",
					"controller",
					"the same"
				}));
				return;
			}
			this.SetControlledObject();
			this.bodyPhysics = ((!this.controlOverrideObject) ? Object.FindObjectOfType<VRTK_BodyPhysics>() : null);
			this.directionDevice = this.GetDirectionDevice();
			this.SetListeners(true);
			this.otherObjectControl = this.GetOtherControl();
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x000800D4 File Offset: 0x0007E2D4
		protected virtual void OnDisable()
		{
			this.SetListeners(false);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000800DD File Offset: 0x0007E2DD
		protected virtual void Update()
		{
			if (this.controlOverrideObject != this.setControlOverrideObject)
			{
				this.SetControlledObject();
			}
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x000800F8 File Offset: 0x0007E2F8
		protected virtual void FixedUpdate()
		{
			this.CheckDirectionDevice();
			this.CheckFalling();
			this.ControlFixedUpdate();
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0008010C File Offset: 0x0007E30C
		protected virtual ObjectControlEventArgs SetEventArguements(Vector3 axisDirection, float axis, float axisDeadzone)
		{
			ObjectControlEventArgs result;
			result.controlledGameObject = this.controlledGameObject;
			result.directionDevice = this.directionDevice;
			result.axisDirection = axisDirection;
			result.axis = axis;
			result.deadzone = axisDeadzone;
			result.currentlyFalling = this.currentlyFalling;
			result.modifierActive = this.modifierActive;
			return result;
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00080168 File Offset: 0x0007E368
		protected virtual void SetControlledObject()
		{
			this.setControlOverrideObject = this.controlOverrideObject;
			this.controlledGameObject = (this.controlOverrideObject ? this.controlOverrideObject : VRTK_DeviceFinder.PlayAreaTransform().gameObject);
			this.controlledGameObjectPreviousY = this.controlledGameObject.transform.position.y;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x000801C4 File Offset: 0x0007E3C4
		protected virtual void CheckFalling()
		{
			if (this.bodyPhysics && this.bodyPhysics.IsFalling() && this.ObjectHeightChange())
			{
				if (!this.affectOnFalling)
				{
					if (this.storedAxis == Vector2.zero)
					{
						this.storedAxis = new Vector2(this.currentAxis.x, this.currentAxis.y);
					}
					this.currentAxis = Vector2.zero;
				}
				this.currentlyFalling = true;
			}
			if (this.bodyPhysics && !this.bodyPhysics.IsFalling() && this.currentlyFalling)
			{
				this.currentAxis = (this.IsInAction() ? this.storedAxis : Vector2.zero);
				this.storedAxis = Vector2.zero;
				this.currentlyFalling = false;
			}
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00080290 File Offset: 0x0007E490
		protected virtual bool ObjectHeightChange()
		{
			bool result = this.controlledGameObjectPreviousY - this.controlledGameObjectPreviousYOffset > this.controlledGameObject.transform.position.y;
			this.controlledGameObjectPreviousY = this.controlledGameObject.transform.position.y;
			return result;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x000802DC File Offset: 0x0007E4DC
		protected virtual Transform GetDirectionDevice()
		{
			switch (this.deviceForDirection)
			{
			case VRTK_ObjectControl.DirectionDevices.Headset:
				return VRTK_DeviceFinder.HeadsetTransform();
			case VRTK_ObjectControl.DirectionDevices.LeftController:
				return VRTK_DeviceFinder.GetControllerLeftHand(true).transform;
			case VRTK_ObjectControl.DirectionDevices.RightController:
				return VRTK_DeviceFinder.GetControllerRightHand(true).transform;
			case VRTK_ObjectControl.DirectionDevices.ControlledObject:
				return this.controlledGameObject.transform;
			default:
				return null;
			}
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00080333 File Offset: 0x0007E533
		protected virtual void CheckDirectionDevice()
		{
			if (this.previousDeviceForDirection != this.deviceForDirection)
			{
				this.directionDevice = this.GetDirectionDevice();
			}
			this.previousDeviceForDirection = this.deviceForDirection;
		}

		// Token: 0x0400138A RID: 5002
		[Header("Control Settings")]
		[Tooltip("The controller to read the controller events from. If this is blank then it will attempt to get a controller events script from the same GameObject.")]
		public VRTK_ControllerEvents controller;

		// Token: 0x0400138B RID: 5003
		[Tooltip("The direction that will be moved in is the direction of this device.")]
		public VRTK_ObjectControl.DirectionDevices deviceForDirection;

		// Token: 0x0400138C RID: 5004
		[Tooltip("If this is checked then whenever the axis on the attached controller is being changed, all other object control scripts of the same type on other controllers will be disabled.")]
		public bool disableOtherControlsOnActive = true;

		// Token: 0x0400138D RID: 5005
		[Tooltip("If a `VRTK_BodyPhysics` script is present and this is checked, then the object control will affect the play area whilst it is falling.")]
		public bool affectOnFalling;

		// Token: 0x0400138E RID: 5006
		[Tooltip("An optional game object to apply the object control to. If this is blank then the PlayArea will be controlled.")]
		public GameObject controlOverrideObject;

		// Token: 0x04001391 RID: 5009
		protected VRTK_ControllerEvents controllerEvents;

		// Token: 0x04001392 RID: 5010
		protected VRTK_BodyPhysics bodyPhysics;

		// Token: 0x04001393 RID: 5011
		protected VRTK_ObjectControl otherObjectControl;

		// Token: 0x04001394 RID: 5012
		protected GameObject controlledGameObject;

		// Token: 0x04001395 RID: 5013
		protected GameObject setControlOverrideObject;

		// Token: 0x04001396 RID: 5014
		protected Transform directionDevice;

		// Token: 0x04001397 RID: 5015
		protected VRTK_ObjectControl.DirectionDevices previousDeviceForDirection;

		// Token: 0x04001398 RID: 5016
		protected Vector2 currentAxis;

		// Token: 0x04001399 RID: 5017
		protected Vector2 storedAxis;

		// Token: 0x0400139A RID: 5018
		protected bool currentlyFalling;

		// Token: 0x0400139B RID: 5019
		protected bool modifierActive;

		// Token: 0x0400139C RID: 5020
		protected float controlledGameObjectPreviousY;

		// Token: 0x0400139D RID: 5021
		protected float controlledGameObjectPreviousYOffset = 0.01f;

		// Token: 0x020005E9 RID: 1513
		public enum DirectionDevices
		{
			// Token: 0x040027F7 RID: 10231
			Headset,
			// Token: 0x040027F8 RID: 10232
			LeftController,
			// Token: 0x040027F9 RID: 10233
			RightController,
			// Token: 0x040027FA RID: 10234
			ControlledObject
		}
	}
}
