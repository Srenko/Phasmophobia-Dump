using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002A8 RID: 680
	[AddComponentMenu("VRTK/Scripts/Interactions/VRTK_InteractUse")]
	public class VRTK_InteractUse : MonoBehaviour
	{
		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06001603 RID: 5635 RVA: 0x000780DC File Offset: 0x000762DC
		// (remove) Token: 0x06001604 RID: 5636 RVA: 0x00078114 File Offset: 0x00076314
		public event ControllerInteractionEventHandler UseButtonPressed;

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06001605 RID: 5637 RVA: 0x0007814C File Offset: 0x0007634C
		// (remove) Token: 0x06001606 RID: 5638 RVA: 0x00078184 File Offset: 0x00076384
		public event ControllerInteractionEventHandler UseButtonReleased;

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06001607 RID: 5639 RVA: 0x000781BC File Offset: 0x000763BC
		// (remove) Token: 0x06001608 RID: 5640 RVA: 0x000781F4 File Offset: 0x000763F4
		public event ObjectInteractEventHandler ControllerStartUseInteractableObject;

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06001609 RID: 5641 RVA: 0x0007822C File Offset: 0x0007642C
		// (remove) Token: 0x0600160A RID: 5642 RVA: 0x00078264 File Offset: 0x00076464
		public event ObjectInteractEventHandler ControllerUseInteractableObject;

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x0600160B RID: 5643 RVA: 0x0007829C File Offset: 0x0007649C
		// (remove) Token: 0x0600160C RID: 5644 RVA: 0x000782D4 File Offset: 0x000764D4
		public event ObjectInteractEventHandler ControllerStartUnuseInteractableObject;

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x0600160D RID: 5645 RVA: 0x0007830C File Offset: 0x0007650C
		// (remove) Token: 0x0600160E RID: 5646 RVA: 0x00078344 File Offset: 0x00076544
		public event ObjectInteractEventHandler ControllerUnuseInteractableObject;

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00078379 File Offset: 0x00076579
		protected VRTK_ControllerReference controllerReference
		{
			get
			{
				return VRTK_ControllerReference.GetControllerReference((this.interactTouch != null) ? this.interactTouch.gameObject : null);
			}
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0007839C File Offset: 0x0007659C
		public virtual void OnControllerStartUseInteractableObject(ObjectInteractEventArgs e)
		{
			if (this.ControllerStartUseInteractableObject != null)
			{
				this.ControllerStartUseInteractableObject(this, e);
			}
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000783B3 File Offset: 0x000765B3
		public virtual void OnControllerUseInteractableObject(ObjectInteractEventArgs e)
		{
			if (this.ControllerUseInteractableObject != null)
			{
				this.ControllerUseInteractableObject(this, e);
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000783CA File Offset: 0x000765CA
		public virtual void OnControllerStartUnuseInteractableObject(ObjectInteractEventArgs e)
		{
			if (this.ControllerStartUnuseInteractableObject != null)
			{
				this.ControllerStartUnuseInteractableObject(this, e);
			}
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x000783E1 File Offset: 0x000765E1
		public virtual void OnControllerUnuseInteractableObject(ObjectInteractEventArgs e)
		{
			if (this.ControllerUnuseInteractableObject != null)
			{
				this.ControllerUnuseInteractableObject(this, e);
			}
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x000783F8 File Offset: 0x000765F8
		public virtual void OnUseButtonPressed(ControllerInteractionEventArgs e)
		{
			if (this.UseButtonPressed != null)
			{
				this.UseButtonPressed(this, e);
			}
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0007840F File Offset: 0x0007660F
		public virtual void OnUseButtonReleased(ControllerInteractionEventArgs e)
		{
			if (this.UseButtonReleased != null)
			{
				this.UseButtonReleased(this, e);
			}
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00078426 File Offset: 0x00076626
		public virtual bool IsUseButtonPressed()
		{
			return this.usePressed;
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0007842E File Offset: 0x0007662E
		public virtual GameObject GetUsingObject()
		{
			return this.usingObject;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00078436 File Offset: 0x00076636
		public virtual void ForceStopUsing()
		{
			if (this.usingObject != null)
			{
				this.StopUsing();
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0007844C File Offset: 0x0007664C
		public virtual void ForceResetUsing()
		{
			if (this.usingObject != null)
			{
				this.UnuseInteractedObject(false);
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00078463 File Offset: 0x00076663
		public virtual void AttemptUse()
		{
			this.AttemptUseObject();
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0007846C File Offset: 0x0007666C
		protected virtual void OnEnable()
		{
			this.controllerEvents = ((this.controllerEvents != null) ? this.controllerEvents : base.GetComponentInParent<VRTK_ControllerEvents>());
			this.interactTouch = ((this.interactTouch != null) ? this.interactTouch : base.GetComponentInParent<VRTK_InteractTouch>());
			this.interactGrab = ((this.interactGrab != null) ? this.interactGrab : base.GetComponentInParent<VRTK_InteractGrab>());
			if (this.interactTouch == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_NOT_INJECTED, new object[]
				{
					"VRTK_InteractUse",
					"VRTK_InteractTouch",
					"interactTouch",
					"the same or parent"
				}));
			}
			this.ManageUseListener(true);
			this.ManageInteractTouchListener(true);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0007852C File Offset: 0x0007672C
		protected virtual void OnDisable()
		{
			this.ForceResetUsing();
			this.ManageUseListener(false);
			this.ManageInteractTouchListener(false);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00078542 File Offset: 0x00076742
		protected virtual void Update()
		{
			this.ManageUseListener(true);
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0007854C File Offset: 0x0007674C
		protected virtual void ManageInteractTouchListener(bool state)
		{
			if (this.interactTouch != null && !state)
			{
				this.interactTouch.ControllerTouchInteractableObject -= this.ControllerTouchInteractableObject;
				this.interactTouch.ControllerUntouchInteractableObject -= this.ControllerUntouchInteractableObject;
			}
			if (this.interactTouch != null && state)
			{
				this.interactTouch.ControllerTouchInteractableObject += this.ControllerTouchInteractableObject;
				this.interactTouch.ControllerUntouchInteractableObject += this.ControllerUntouchInteractableObject;
			}
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000785DC File Offset: 0x000767DC
		protected virtual void ControllerTouchInteractableObject(object sender, ObjectInteractEventArgs e)
		{
			if (e.target != null)
			{
				VRTK_InteractableObject component = e.target.GetComponent<VRTK_InteractableObject>();
				if (component != null && component.useOverrideButton != VRTK_ControllerEvents.ButtonAlias.Undefined)
				{
					this.savedUseButton = this.subscribedUseButton;
					this.useButton = component.useOverrideButton;
				}
			}
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0007862C File Offset: 0x0007682C
		protected virtual void ControllerUntouchInteractableObject(object sender, ObjectInteractEventArgs e)
		{
			if (e.target != null)
			{
				VRTK_InteractableObject component = e.target.GetComponent<VRTK_InteractableObject>();
				if (component != null && !component.IsUsing(null) && this.savedUseButton != VRTK_ControllerEvents.ButtonAlias.Undefined)
				{
					this.useButton = this.savedUseButton;
					this.savedUseButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
				}
			}
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00078680 File Offset: 0x00076880
		protected virtual void ManageUseListener(bool state)
		{
			if (this.controllerEvents != null && this.subscribedUseButton != VRTK_ControllerEvents.ButtonAlias.Undefined && (!state || !this.useButton.Equals(this.subscribedUseButton)))
			{
				this.controllerEvents.UnsubscribeToButtonAliasEvent(this.subscribedUseButton, true, new ControllerInteractionEventHandler(this.DoStartUseObject));
				this.controllerEvents.UnsubscribeToButtonAliasEvent(this.subscribedUseButton, false, new ControllerInteractionEventHandler(this.DoStopUseObject));
				this.subscribedUseButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
			}
			if (this.controllerEvents != null && state && this.useButton != VRTK_ControllerEvents.ButtonAlias.Undefined && !this.useButton.Equals(this.subscribedUseButton))
			{
				this.controllerEvents.SubscribeToButtonAliasEvent(this.useButton, true, new ControllerInteractionEventHandler(this.DoStartUseObject));
				this.controllerEvents.SubscribeToButtonAliasEvent(this.useButton, false, new ControllerInteractionEventHandler(this.DoStopUseObject));
				this.subscribedUseButton = this.useButton;
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0007878C File Offset: 0x0007698C
		protected virtual bool IsObjectUsable(GameObject obj)
		{
			VRTK_InteractableObject vrtk_InteractableObject = (obj != null) ? obj.GetComponent<VRTK_InteractableObject>() : null;
			return obj != null && this.interactTouch != null && this.interactTouch.IsObjectInteractable(obj) && vrtk_InteractableObject != null && vrtk_InteractableObject.isUsable;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x000787E4 File Offset: 0x000769E4
		protected virtual bool IsObjectHoldOnUse(GameObject obj)
		{
			if (obj != null)
			{
				VRTK_InteractableObject component = obj.GetComponent<VRTK_InteractableObject>();
				return component != null && component.holdButtonToUse;
			}
			return false;
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x00078814 File Offset: 0x00076A14
		protected virtual int GetObjectUsingState(GameObject obj)
		{
			if (obj != null)
			{
				VRTK_InteractableObject component = obj.GetComponent<VRTK_InteractableObject>();
				if (component != null)
				{
					return component.usingState;
				}
			}
			return 0;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00078844 File Offset: 0x00076A44
		protected virtual void SetObjectUsingState(GameObject obj, int value)
		{
			if (obj != null)
			{
				VRTK_InteractableObject component = obj.GetComponent<VRTK_InteractableObject>();
				if (component != null)
				{
					component.usingState = value;
				}
			}
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00078874 File Offset: 0x00076A74
		protected virtual void AttemptHaptics()
		{
			if (this.usingObject != null)
			{
				VRTK_InteractHaptics componentInParent = this.usingObject.GetComponentInParent<VRTK_InteractHaptics>();
				if (componentInParent != null)
				{
					componentInParent.HapticsOnUse(this.controllerReference);
				}
			}
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x000788B0 File Offset: 0x00076AB0
		protected virtual void ToggleControllerVisibility(bool visible)
		{
			if (this.usingObject != null)
			{
				VRTK_InteractControllerAppearance[] componentsInParent = this.usingObject.GetComponentsInParent<VRTK_InteractControllerAppearance>(true);
				if (componentsInParent.Length != 0)
				{
					componentsInParent[0].ToggleControllerOnUse(visible, this.controllerReference.model, this.usingObject);
				}
			}
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x000788F8 File Offset: 0x00076AF8
		protected virtual void UseInteractedObject(GameObject touchedObject)
		{
			if ((this.usingObject == null || this.usingObject != touchedObject) && this.IsObjectUsable(touchedObject) && this.interactTouch != null)
			{
				this.usingObject = touchedObject;
				this.OnControllerStartUseInteractableObject(this.interactTouch.SetControllerInteractEvent(this.usingObject));
				VRTK_InteractableObject vrtk_InteractableObject = (this.usingObject != null) ? this.usingObject.GetComponent<VRTK_InteractableObject>() : null;
				if (vrtk_InteractableObject != null)
				{
					if (!vrtk_InteractableObject.IsValidInteractableController(base.gameObject, vrtk_InteractableObject.allowedUseControllers))
					{
						this.usingObject = null;
						return;
					}
					vrtk_InteractableObject.StartUsing(this);
					this.ToggleControllerVisibility(false);
					this.AttemptHaptics();
					this.OnControllerUseInteractableObject(this.interactTouch.SetControllerInteractEvent(this.usingObject));
				}
			}
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000789CC File Offset: 0x00076BCC
		protected virtual void UnuseInteractedObject(bool completeStop)
		{
			if (this.usingObject != null && this.interactTouch != null)
			{
				this.OnControllerStartUnuseInteractableObject(this.interactTouch.SetControllerInteractEvent(this.usingObject));
				VRTK_InteractableObject component = this.usingObject.GetComponent<VRTK_InteractableObject>();
				if (component != null && completeStop)
				{
					component.StopUsing(this);
				}
				this.ToggleControllerVisibility(true);
				this.OnControllerUnuseInteractableObject(this.interactTouch.SetControllerInteractEvent(this.usingObject));
				this.usingObject = null;
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00078A4F File Offset: 0x00076C4F
		protected virtual GameObject GetFromGrab()
		{
			if (this.interactGrab != null)
			{
				return this.interactGrab.GetGrabbedObject();
			}
			return null;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00078A6C File Offset: 0x00076C6C
		protected virtual void StopUsing()
		{
			this.SetObjectUsingState(this.usingObject, 0);
			this.UnuseInteractedObject(true);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00078A84 File Offset: 0x00076C84
		protected virtual void AttemptUseObject()
		{
			GameObject gameObject = (this.interactTouch != null) ? this.interactTouch.GetTouchedObject() : null;
			if (gameObject == null)
			{
				gameObject = this.GetFromGrab();
			}
			if (gameObject != null && this.interactTouch != null && this.interactTouch.IsObjectInteractable(gameObject))
			{
				VRTK_InteractableObject component = gameObject.GetComponent<VRTK_InteractableObject>();
				if (component != null && component.useOnlyIfGrabbed && !component.IsGrabbed(null))
				{
					return;
				}
				this.UseInteractedObject(gameObject);
				if (this.usingObject != null && !this.IsObjectHoldOnUse(this.usingObject))
				{
					this.SetObjectUsingState(this.usingObject, this.GetObjectUsingState(this.usingObject) + 1);
				}
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00078B42 File Offset: 0x00076D42
		protected virtual void DoStartUseObject(object sender, ControllerInteractionEventArgs e)
		{
			this.OnUseButtonPressed(this.controllerEvents.SetControllerEvent(ref this.usePressed, true, 0f));
			this.AttemptUseObject();
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00078B68 File Offset: 0x00076D68
		protected virtual void DoStopUseObject(object sender, ControllerInteractionEventArgs e)
		{
			if (this.IsObjectHoldOnUse(this.usingObject) || this.GetObjectUsingState(this.usingObject) >= 2)
			{
				this.StopUsing();
			}
			this.OnUseButtonReleased(this.controllerEvents.SetControllerEvent(ref this.usePressed, false, 0f));
		}

		// Token: 0x04001280 RID: 4736
		[Header("Use Settings")]
		[Tooltip("The button used to use/unuse a touched object.")]
		public VRTK_ControllerEvents.ButtonAlias useButton = VRTK_ControllerEvents.ButtonAlias.TriggerPress;

		// Token: 0x04001281 RID: 4737
		[Header("Custom Settings")]
		[Tooltip("The controller to listen for the events on. If the script is being applied onto a controller then this parameter can be left blank as it will be auto populated by the controller the script is on at runtime.")]
		public VRTK_ControllerEvents controllerEvents;

		// Token: 0x04001282 RID: 4738
		[Tooltip("The Interact Touch to listen for touches on. If the script is being applied onto a controller then this parameter can be left blank as it will be auto populated by the controller the script is on at runtime.")]
		public VRTK_InteractTouch interactTouch;

		// Token: 0x04001283 RID: 4739
		[Tooltip("The Interact Grab to listen for grab actions on. If the script is being applied onto a controller then this parameter can be left blank as it will be auto populated by the controller the script is on at runtime.")]
		public VRTK_InteractGrab interactGrab;

		// Token: 0x0400128A RID: 4746
		protected VRTK_ControllerEvents.ButtonAlias subscribedUseButton;

		// Token: 0x0400128B RID: 4747
		protected VRTK_ControllerEvents.ButtonAlias savedUseButton;

		// Token: 0x0400128C RID: 4748
		protected bool usePressed;

		// Token: 0x0400128D RID: 4749
		protected GameObject usingObject;
	}
}
