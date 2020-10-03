using System;
using System.Collections;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002A0 RID: 672
	[AddComponentMenu("VRTK/Scripts/Interactions/VRTK_InteractControllerAppearance")]
	public class VRTK_InteractControllerAppearance : MonoBehaviour
	{
		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06001552 RID: 5458 RVA: 0x00075A88 File Offset: 0x00073C88
		// (remove) Token: 0x06001553 RID: 5459 RVA: 0x00075AC0 File Offset: 0x00073CC0
		public event InteractControllerAppearanceEventHandler ControllerHidden;

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06001554 RID: 5460 RVA: 0x00075AF8 File Offset: 0x00073CF8
		// (remove) Token: 0x06001555 RID: 5461 RVA: 0x00075B30 File Offset: 0x00073D30
		public event InteractControllerAppearanceEventHandler ControllerVisible;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06001556 RID: 5462 RVA: 0x00075B68 File Offset: 0x00073D68
		// (remove) Token: 0x06001557 RID: 5463 RVA: 0x00075BA0 File Offset: 0x00073DA0
		public event InteractControllerAppearanceEventHandler HiddenOnTouch;

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06001558 RID: 5464 RVA: 0x00075BD8 File Offset: 0x00073DD8
		// (remove) Token: 0x06001559 RID: 5465 RVA: 0x00075C10 File Offset: 0x00073E10
		public event InteractControllerAppearanceEventHandler VisibleOnTouch;

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x0600155A RID: 5466 RVA: 0x00075C48 File Offset: 0x00073E48
		// (remove) Token: 0x0600155B RID: 5467 RVA: 0x00075C80 File Offset: 0x00073E80
		public event InteractControllerAppearanceEventHandler HiddenOnGrab;

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x0600155C RID: 5468 RVA: 0x00075CB8 File Offset: 0x00073EB8
		// (remove) Token: 0x0600155D RID: 5469 RVA: 0x00075CF0 File Offset: 0x00073EF0
		public event InteractControllerAppearanceEventHandler VisibleOnGrab;

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x0600155E RID: 5470 RVA: 0x00075D28 File Offset: 0x00073F28
		// (remove) Token: 0x0600155F RID: 5471 RVA: 0x00075D60 File Offset: 0x00073F60
		public event InteractControllerAppearanceEventHandler HiddenOnUse;

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06001560 RID: 5472 RVA: 0x00075D98 File Offset: 0x00073F98
		// (remove) Token: 0x06001561 RID: 5473 RVA: 0x00075DD0 File Offset: 0x00073FD0
		public event InteractControllerAppearanceEventHandler VisibleOnUse;

		// Token: 0x06001562 RID: 5474 RVA: 0x00075E05 File Offset: 0x00074005
		public virtual void OnControllerHidden(InteractControllerAppearanceEventArgs e)
		{
			if (this.ControllerHidden != null)
			{
				this.ControllerHidden(this, e);
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00075E1C File Offset: 0x0007401C
		public virtual void OnControllerVisible(InteractControllerAppearanceEventArgs e)
		{
			if (this.ControllerVisible != null)
			{
				this.ControllerVisible(this, e);
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00075E33 File Offset: 0x00074033
		public virtual void OnHiddenOnTouch(InteractControllerAppearanceEventArgs e)
		{
			if (this.HiddenOnTouch != null)
			{
				this.HiddenOnTouch(this, e);
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00075E4A File Offset: 0x0007404A
		public virtual void OnVisibleOnTouch(InteractControllerAppearanceEventArgs e)
		{
			if (this.VisibleOnTouch != null)
			{
				this.VisibleOnTouch(this, e);
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00075E61 File Offset: 0x00074061
		public virtual void OnHiddenOnGrab(InteractControllerAppearanceEventArgs e)
		{
			if (this.HiddenOnGrab != null)
			{
				this.HiddenOnGrab(this, e);
			}
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00075E78 File Offset: 0x00074078
		public virtual void OnVisibleOnGrab(InteractControllerAppearanceEventArgs e)
		{
			if (this.VisibleOnGrab != null)
			{
				this.VisibleOnGrab(this, e);
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00075E8F File Offset: 0x0007408F
		public virtual void OnHiddenOnUse(InteractControllerAppearanceEventArgs e)
		{
			if (this.HiddenOnUse != null)
			{
				this.HiddenOnUse(this, e);
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00075EA6 File Offset: 0x000740A6
		public virtual void OnVisibleOnUse(InteractControllerAppearanceEventArgs e)
		{
			if (this.VisibleOnUse != null)
			{
				this.VisibleOnUse(this, e);
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00075EBD File Offset: 0x000740BD
		public virtual void ToggleControllerOnTouch(bool showController, GameObject touchingObject, GameObject ignoredObject)
		{
			if (this.hideControllerOnTouch)
			{
				this.touchControllerShow = showController;
				this.ToggleController(showController, touchingObject, ignoredObject, this.hideDelayOnTouch);
				if (showController)
				{
					this.OnVisibleOnTouch(this.SetEventPayload(touchingObject, ignoredObject));
					return;
				}
				this.OnHiddenOnTouch(this.SetEventPayload(touchingObject, ignoredObject));
			}
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00075F00 File Offset: 0x00074100
		public virtual void ToggleControllerOnGrab(bool showController, GameObject grabbingObject, GameObject ignoredObject)
		{
			if (this.hideControllerOnGrab)
			{
				VRTK_InteractableObject vrtk_InteractableObject = (ignoredObject != null) ? ignoredObject.GetComponentInParent<VRTK_InteractableObject>() : null;
				if (showController && !this.touchControllerShow && vrtk_InteractableObject && vrtk_InteractableObject.IsTouched())
				{
					return;
				}
				this.grabControllerShow = showController;
				this.ToggleController(showController, grabbingObject, ignoredObject, this.hideDelayOnGrab);
				if (showController)
				{
					this.OnVisibleOnGrab(this.SetEventPayload(grabbingObject, ignoredObject));
					return;
				}
				this.OnHiddenOnGrab(this.SetEventPayload(grabbingObject, ignoredObject));
			}
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00075F7C File Offset: 0x0007417C
		public virtual void ToggleControllerOnUse(bool showController, GameObject usingObject, GameObject ignoredObject)
		{
			if (this.hideControllerOnUse)
			{
				VRTK_InteractableObject vrtk_InteractableObject = (ignoredObject != null) ? ignoredObject.GetComponentInParent<VRTK_InteractableObject>() : null;
				if (showController && ((!this.grabControllerShow && vrtk_InteractableObject && vrtk_InteractableObject.IsGrabbed(null)) || (!this.touchControllerShow && vrtk_InteractableObject && vrtk_InteractableObject.IsTouched())))
				{
					return;
				}
				this.ToggleController(showController, usingObject, ignoredObject, this.hideDelayOnUse);
				if (showController)
				{
					this.OnVisibleOnUse(this.SetEventPayload(usingObject, ignoredObject));
					return;
				}
				this.OnHiddenOnUse(this.SetEventPayload(usingObject, ignoredObject));
			}
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00076008 File Offset: 0x00074208
		protected virtual void OnEnable()
		{
			if (!base.GetComponent<VRTK_InteractableObject>())
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_InteractControllerAppearance",
					"VRTK_InteractableObject",
					"the same"
				}));
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00076040 File Offset: 0x00074240
		protected virtual void OnDisable()
		{
			if (this.hideControllerRoutine != null)
			{
				base.StopCoroutine(this.hideControllerRoutine);
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00076056 File Offset: 0x00074256
		protected virtual void ToggleController(bool showController, GameObject interactingObject, GameObject ignoredObject, float delayTime)
		{
			if (showController)
			{
				this.ShowController(interactingObject, ignoredObject);
				return;
			}
			this.hideControllerRoutine = base.StartCoroutine(this.HideController(interactingObject, ignoredObject, delayTime));
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0007607A File Offset: 0x0007427A
		protected virtual void ShowController(GameObject interactingObject, GameObject ignoredObject)
		{
			if (this.hideControllerRoutine != null)
			{
				base.StopCoroutine(this.hideControllerRoutine);
			}
			VRTK_ObjectAppearance.SetRendererVisible(interactingObject, ignoredObject);
			this.OnControllerVisible(this.SetEventPayload(interactingObject, ignoredObject));
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x000760A5 File Offset: 0x000742A5
		protected virtual IEnumerator HideController(GameObject interactingObject, GameObject ignoredObject, float delayTime)
		{
			yield return new WaitForSeconds(delayTime);
			VRTK_ObjectAppearance.SetRendererHidden(interactingObject, ignoredObject);
			this.OnControllerHidden(this.SetEventPayload(interactingObject, ignoredObject));
			yield break;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x000760CC File Offset: 0x000742CC
		protected virtual InteractControllerAppearanceEventArgs SetEventPayload(GameObject interactingObject, GameObject ignroedObject)
		{
			InteractControllerAppearanceEventArgs result;
			result.interactingObject = interactingObject;
			result.ignoredObject = ignroedObject;
			return result;
		}

		// Token: 0x0400122F RID: 4655
		[Header("Touch Visibility")]
		[Tooltip("Hides the controller model when a valid touch occurs.")]
		public bool hideControllerOnTouch;

		// Token: 0x04001230 RID: 4656
		[Tooltip("The amount of seconds to wait before hiding the controller on touch.")]
		public float hideDelayOnTouch;

		// Token: 0x04001231 RID: 4657
		[Header("Grab Visibility")]
		[Tooltip("Hides the controller model when a valid grab occurs.")]
		public bool hideControllerOnGrab;

		// Token: 0x04001232 RID: 4658
		[Tooltip("The amount of seconds to wait before hiding the controller on grab.")]
		public float hideDelayOnGrab;

		// Token: 0x04001233 RID: 4659
		[Header("Use Visibility")]
		[Tooltip("Hides the controller model when a valid use occurs.")]
		public bool hideControllerOnUse;

		// Token: 0x04001234 RID: 4660
		[Tooltip("The amount of seconds to wait before hiding the controller on use.")]
		public float hideDelayOnUse;

		// Token: 0x0400123D RID: 4669
		protected bool touchControllerShow = true;

		// Token: 0x0400123E RID: 4670
		protected bool grabControllerShow = true;

		// Token: 0x0400123F RID: 4671
		protected Coroutine hideControllerRoutine;
	}
}
