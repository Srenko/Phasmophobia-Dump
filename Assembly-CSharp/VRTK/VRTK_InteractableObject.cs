using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.GrabAttachMechanics;
using VRTK.Highlighters;
using VRTK.SecondaryControllerGrabActions;

namespace VRTK
{
	// Token: 0x020002AB RID: 683
	[AddComponentMenu("VRTK/Scripts/Interactions/VRTK_InteractableObject")]
	public class VRTK_InteractableObject : MonoBehaviour
	{
		// Token: 0x1400007C RID: 124
		// (add) Token: 0x06001634 RID: 5684 RVA: 0x00078BC4 File Offset: 0x00076DC4
		// (remove) Token: 0x06001635 RID: 5685 RVA: 0x00078BFC File Offset: 0x00076DFC
		public event InteractableObjectEventHandler InteractableObjectTouched;

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x06001636 RID: 5686 RVA: 0x00078C34 File Offset: 0x00076E34
		// (remove) Token: 0x06001637 RID: 5687 RVA: 0x00078C6C File Offset: 0x00076E6C
		public event InteractableObjectEventHandler InteractableObjectUntouched;

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x06001638 RID: 5688 RVA: 0x00078CA4 File Offset: 0x00076EA4
		// (remove) Token: 0x06001639 RID: 5689 RVA: 0x00078CDC File Offset: 0x00076EDC
		public event InteractableObjectEventHandler InteractableObjectGrabbed;

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x0600163A RID: 5690 RVA: 0x00078D14 File Offset: 0x00076F14
		// (remove) Token: 0x0600163B RID: 5691 RVA: 0x00078D4C File Offset: 0x00076F4C
		public event InteractableObjectEventHandler InteractableObjectUngrabbed;

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x0600163C RID: 5692 RVA: 0x00078D84 File Offset: 0x00076F84
		// (remove) Token: 0x0600163D RID: 5693 RVA: 0x00078DBC File Offset: 0x00076FBC
		public event InteractableObjectEventHandler InteractableObjectUsed;

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x0600163E RID: 5694 RVA: 0x00078DF4 File Offset: 0x00076FF4
		// (remove) Token: 0x0600163F RID: 5695 RVA: 0x00078E2C File Offset: 0x0007702C
		public event InteractableObjectEventHandler InteractableObjectUnused;

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06001640 RID: 5696 RVA: 0x00078E64 File Offset: 0x00077064
		// (remove) Token: 0x06001641 RID: 5697 RVA: 0x00078E9C File Offset: 0x0007709C
		public event InteractableObjectEventHandler InteractableObjectEnteredSnapDropZone;

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06001642 RID: 5698 RVA: 0x00078ED4 File Offset: 0x000770D4
		// (remove) Token: 0x06001643 RID: 5699 RVA: 0x00078F0C File Offset: 0x0007710C
		public event InteractableObjectEventHandler InteractableObjectExitedSnapDropZone;

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x06001644 RID: 5700 RVA: 0x00078F44 File Offset: 0x00077144
		// (remove) Token: 0x06001645 RID: 5701 RVA: 0x00078F7C File Offset: 0x0007717C
		public event InteractableObjectEventHandler InteractableObjectSnappedToDropZone;

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x06001646 RID: 5702 RVA: 0x00078FB4 File Offset: 0x000771B4
		// (remove) Token: 0x06001647 RID: 5703 RVA: 0x00078FEC File Offset: 0x000771EC
		public event InteractableObjectEventHandler InteractableObjectUnsnappedFromDropZone;

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x00079021 File Offset: 0x00077221
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x0007903D File Offset: 0x0007723D
		public bool isKinematic
		{
			get
			{
				return !this.interactableRigidbody || this.interactableRigidbody.isKinematic;
			}
			set
			{
				if (this.interactableRigidbody)
				{
					this.interactableRigidbody.isKinematic = value;
				}
			}
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00079058 File Offset: 0x00077258
		public virtual void OnInteractableObjectTouched(InteractableObjectEventArgs e)
		{
			if (this.InteractableObjectTouched != null)
			{
				this.InteractableObjectTouched(this, e);
			}
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0007906F File Offset: 0x0007726F
		public virtual void OnInteractableObjectUntouched(InteractableObjectEventArgs e)
		{
			if (this.InteractableObjectUntouched != null)
			{
				this.InteractableObjectUntouched(this, e);
			}
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00079086 File Offset: 0x00077286
		public virtual void OnInteractableObjectGrabbed(InteractableObjectEventArgs e)
		{
			if (this.InteractableObjectGrabbed != null)
			{
				this.InteractableObjectGrabbed(this, e);
			}
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0007909D File Offset: 0x0007729D
		public virtual void OnInteractableObjectUngrabbed(InteractableObjectEventArgs e)
		{
			if (this.InteractableObjectUngrabbed != null)
			{
				this.InteractableObjectUngrabbed(this, e);
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x000790B4 File Offset: 0x000772B4
		public virtual void OnInteractableObjectUsed(InteractableObjectEventArgs e)
		{
			if (this.InteractableObjectUsed != null)
			{
				this.InteractableObjectUsed(this, e);
			}
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x000790CB File Offset: 0x000772CB
		public virtual void OnInteractableObjectUnused(InteractableObjectEventArgs e)
		{
			if (this.InteractableObjectUnused != null)
			{
				this.InteractableObjectUnused(this, e);
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x000790E2 File Offset: 0x000772E2
		public virtual void OnInteractableObjectEnteredSnapDropZone(InteractableObjectEventArgs e)
		{
			if (this.InteractableObjectEnteredSnapDropZone != null)
			{
				this.InteractableObjectEnteredSnapDropZone(this, e);
			}
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x000790F9 File Offset: 0x000772F9
		public virtual void OnInteractableObjectExitedSnapDropZone(InteractableObjectEventArgs e)
		{
			if (this.InteractableObjectExitedSnapDropZone != null)
			{
				this.InteractableObjectExitedSnapDropZone(this, e);
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00079110 File Offset: 0x00077310
		public virtual void OnInteractableObjectSnappedToDropZone(InteractableObjectEventArgs e)
		{
			if (this.InteractableObjectSnappedToDropZone != null)
			{
				this.InteractableObjectSnappedToDropZone(this, e);
			}
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00079127 File Offset: 0x00077327
		public virtual void OnInteractableObjectUnsnappedFromDropZone(InteractableObjectEventArgs e)
		{
			if (this.InteractableObjectUnsnappedFromDropZone != null)
			{
				this.InteractableObjectUnsnappedFromDropZone(this, e);
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00079140 File Offset: 0x00077340
		public InteractableObjectEventArgs SetInteractableObjectEvent(GameObject interactingObject)
		{
			InteractableObjectEventArgs result;
			result.interactingObject = interactingObject;
			return result;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00079156 File Offset: 0x00077356
		public virtual bool IsTouched()
		{
			return this.touchingObjects.Count > 0;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00079166 File Offset: 0x00077366
		public virtual bool IsGrabbed(GameObject grabbedBy = null)
		{
			if (this.grabbingObjects.Count > 0 && grabbedBy != null)
			{
				return this.grabbingObjects.Contains(grabbedBy);
			}
			return this.grabbingObjects.Count > 0;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0007919A File Offset: 0x0007739A
		public virtual bool IsUsing(GameObject usedBy = null)
		{
			if (this.usingObject != null && usedBy != null)
			{
				return this.usingObject.gameObject == usedBy;
			}
			return this.usingObject != null;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x000791D1 File Offset: 0x000773D1
		[Obsolete("`VRTK_InteractableObject.StartTouching(GameObject currentTouchingObject)` has been replaced with `VRTK_InteractableObject.StartTouching(VRTK_InteractTouch currentTouchingObject)`. This method will be removed in a future version of VRTK.")]
		public virtual void StartTouching(GameObject currentTouchingObject)
		{
			this.StartTouching((currentTouchingObject != null) ? currentTouchingObject.GetComponent<VRTK_InteractTouch>() : null);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x000791EC File Offset: 0x000773EC
		public virtual void StartTouching(VRTK_InteractTouch currentTouchingObject = null)
		{
			GameObject gameObject = (currentTouchingObject != null) ? currentTouchingObject.gameObject : null;
			if (gameObject != null)
			{
				this.IgnoreColliders(gameObject);
				if (!this.touchingObjects.Contains(gameObject))
				{
					this.ToggleEnableState(true);
					this.touchingObjects.Add(gameObject);
					this.OnInteractableObjectTouched(this.SetInteractableObjectEvent(gameObject));
				}
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0007924A File Offset: 0x0007744A
		[Obsolete("`VRTK_InteractableObject.StopTouching(GameObject previousTouchingObject)` has been replaced with `VRTK_InteractableObject.StopTouching(VRTK_InteractTouch previousTouchingObject)`. This method will be removed in a future version of VRTK.")]
		public virtual void StopTouching(GameObject previousTouchingObject)
		{
			this.StopTouching((previousTouchingObject != null) ? previousTouchingObject.GetComponent<VRTK_InteractTouch>() : null);
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00079264 File Offset: 0x00077464
		public virtual void StopTouching(VRTK_InteractTouch previousTouchingObject = null)
		{
			GameObject gameObject = (previousTouchingObject != null) ? previousTouchingObject.gameObject : null;
			if (gameObject != null && this.touchingObjects.Contains(gameObject))
			{
				this.ResetUseState(gameObject);
				this.OnInteractableObjectUntouched(this.SetInteractableObjectEvent(gameObject));
				this.touchingObjects.Remove(gameObject);
			}
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000792BC File Offset: 0x000774BC
		[Obsolete("`VRTK_InteractableObject.Grabbed(GameObject currentGrabbingObject)` has been replaced with `VRTK_InteractableObject.Grabbed(VRTK_InteractGrab currentGrabbingObject)`. This method will be removed in a future version of VRTK.")]
		public virtual void Grabbed(GameObject currentGrabbingObject)
		{
			this.Grabbed((currentGrabbingObject != null) ? currentGrabbingObject.GetComponent<VRTK_InteractGrab>() : null);
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000792D8 File Offset: 0x000774D8
		public virtual void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
		{
			GameObject gameObject = (currentGrabbingObject != null) ? currentGrabbingObject.gameObject : null;
			this.ToggleEnableState(true);
			if (!this.IsGrabbed(null) || this.IsSwappable())
			{
				this.PrimaryControllerGrab(gameObject);
			}
			else
			{
				this.SecondaryControllerGrab(gameObject);
			}
			this.OnInteractableObjectGrabbed(this.SetInteractableObjectEvent(gameObject));
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0007932D File Offset: 0x0007752D
		[Obsolete("`VRTK_InteractableObject.Ungrabbed(GameObject previousGrabbingObject)` has been replaced with `VRTK_InteractableObject.Ungrabbed(VRTK_InteractGrab previousGrabbingObject)`. This method will be removed in a future version of VRTK.")]
		public virtual void Ungrabbed(GameObject previousGrabbingObject)
		{
			this.Ungrabbed((previousGrabbingObject != null) ? previousGrabbingObject.GetComponent<VRTK_InteractGrab>() : null);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00079348 File Offset: 0x00077548
		public virtual void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
		{
			GameObject gameObject = (previousGrabbingObject != null) ? previousGrabbingObject.gameObject : null;
			GameObject secondaryGrabbingObject = this.GetSecondaryGrabbingObject();
			if (!secondaryGrabbingObject || secondaryGrabbingObject != gameObject)
			{
				this.SecondaryControllerUngrab(secondaryGrabbingObject);
				this.PrimaryControllerUngrab(gameObject, secondaryGrabbingObject);
			}
			else
			{
				this.SecondaryControllerUngrab(gameObject);
			}
			this.OnInteractableObjectUngrabbed(this.SetInteractableObjectEvent(gameObject));
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000793A5 File Offset: 0x000775A5
		[Obsolete("`VRTK_InteractableObject.StartUsing(GameObject currentUsingObject)` has been replaced with `VRTK_InteractableObject.StartUsing(VRTK_InteractUse currentUsingObject)`. This method will be removed in a future version of VRTK.")]
		public virtual void StartUsing(GameObject currentUsingObject)
		{
			this.StartUsing((currentUsingObject != null) ? currentUsingObject.GetComponent<VRTK_InteractUse>() : null);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000793C0 File Offset: 0x000775C0
		public virtual void StartUsing(VRTK_InteractUse currentUsingObject = null)
		{
			GameObject gameObject = (currentUsingObject != null) ? currentUsingObject.gameObject : null;
			this.ToggleEnableState(true);
			if (this.IsUsing(null) && !this.IsUsing(gameObject))
			{
				this.ResetUsingObject();
			}
			this.OnInteractableObjectUsed(this.SetInteractableObjectEvent(gameObject));
			this.usingObject = currentUsingObject;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00079413 File Offset: 0x00077613
		[Obsolete("`VRTK_InteractableObject.StopUsing(GameObject previousUsingObject)` has been replaced with `VRTK_InteractableObject.StopUsing(VRTK_InteractUse previousUsingObject)`. This method will be removed in a future version of VRTK.")]
		public virtual void StopUsing(GameObject previousUsingObject)
		{
			this.StopUsing((previousUsingObject != null) ? previousUsingObject.GetComponent<VRTK_InteractUse>() : null);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00079430 File Offset: 0x00077630
		public virtual void StopUsing(VRTK_InteractUse previousUsingObject = null)
		{
			GameObject interactableObjectEvent = (previousUsingObject != null) ? previousUsingObject.gameObject : null;
			this.OnInteractableObjectUnused(this.SetInteractableObjectEvent(interactableObjectEvent));
			this.ResetUsingObject();
			this.usingState = 0;
			this.usingObject = null;
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00079474 File Offset: 0x00077674
		public virtual void ToggleHighlight(bool toggle)
		{
			this.InitialiseHighlighter();
			if (this.touchHighlightColor != Color.clear && this.objectHighlighter)
			{
				if (toggle && !this.IsGrabbed(null))
				{
					this.objectHighlighter.Highlight(new Color?(this.touchHighlightColor), 0f);
					return;
				}
				this.objectHighlighter.Unhighlight(null, 0f);
			}
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000794E7 File Offset: 0x000776E7
		public virtual void ResetHighlighter()
		{
			if (this.objectHighlighter)
			{
				this.objectHighlighter.ResetHighlighter();
			}
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00079504 File Offset: 0x00077704
		public virtual void PauseCollisions(float delay)
		{
			if (delay > 0f)
			{
				Rigidbody[] componentsInChildren = base.GetComponentsInChildren<Rigidbody>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].detectCollisions = false;
				}
				base.Invoke("UnpauseCollisions", delay);
			}
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00079543 File Offset: 0x00077743
		public virtual void ZeroVelocity()
		{
			if (this.interactableRigidbody)
			{
				this.interactableRigidbody.velocity = Vector3.zero;
				this.interactableRigidbody.angularVelocity = Vector3.zero;
			}
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00079574 File Offset: 0x00077774
		public virtual void SaveCurrentState()
		{
			if (!this.IsGrabbed(null) && !this.snappedInSnapDropZone)
			{
				this.previousParent = base.transform.parent;
				if (!this.IsSwappable())
				{
					this.previousIsGrabbable = this.isGrabbable;
				}
				if (this.interactableRigidbody)
				{
					this.previousKinematicState = this.interactableRigidbody.isKinematic;
				}
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x000795D5 File Offset: 0x000777D5
		public virtual List<GameObject> GetTouchingObjects()
		{
			return this.touchingObjects;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x000795DD File Offset: 0x000777DD
		public virtual GameObject GetGrabbingObject()
		{
			if (!this.IsGrabbed(null))
			{
				return null;
			}
			return this.grabbingObjects[0];
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x000795F6 File Offset: 0x000777F6
		public virtual GameObject GetSecondaryGrabbingObject()
		{
			if (this.grabbingObjects.Count <= 1)
			{
				return null;
			}
			return this.grabbingObjects[1];
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00079614 File Offset: 0x00077814
		public virtual GameObject GetUsingObject()
		{
			return this.usingObject.gameObject;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00079621 File Offset: 0x00077821
		public virtual VRTK_InteractUse GetUsingScript()
		{
			return this.usingObject;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0007962C File Offset: 0x0007782C
		public virtual bool IsValidInteractableController(GameObject actualController, VRTK_InteractableObject.AllowedController controllerCheck)
		{
			if (controllerCheck == VRTK_InteractableObject.AllowedController.Both)
			{
				return true;
			}
			SDK_BaseController.ControllerHand controllerHandType = VRTK_DeviceFinder.GetControllerHandType(controllerCheck.ToString().Replace("Only", ""));
			return VRTK_DeviceFinder.IsControllerOfHand(actualController, controllerHandType);
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00079668 File Offset: 0x00077868
		public virtual void ForceStopInteracting()
		{
			if (base.gameObject.activeInHierarchy)
			{
				this.forceDisabled = false;
				base.StartCoroutine(this.ForceStopInteractingAtEndOfFrame());
			}
			if (!base.gameObject.activeInHierarchy && this.forceDisabled)
			{
				this.ForceStopAllInteractions();
				this.forceDisabled = false;
			}
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x000796B8 File Offset: 0x000778B8
		public virtual void ForceStopSecondaryGrabInteraction()
		{
			GameObject secondaryGrabbingObject = this.GetSecondaryGrabbingObject();
			if (secondaryGrabbingObject != null)
			{
				secondaryGrabbingObject.GetComponent<VRTK_InteractGrab>().ForceRelease(false);
			}
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x000796E1 File Offset: 0x000778E1
		public virtual void RegisterTeleporters()
		{
			base.StartCoroutine(this.RegisterTeleportersAtEndOfFrame());
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000796F0 File Offset: 0x000778F0
		public virtual void UnregisterTeleporters()
		{
			foreach (VRTK_BasicTeleport vrtk_BasicTeleport in VRTK_ObjectCache.registeredTeleporters)
			{
				vrtk_BasicTeleport.Teleporting -= this.OnTeleporting;
				vrtk_BasicTeleport.Teleported -= this.OnTeleported;
			}
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00079760 File Offset: 0x00077960
		public virtual void StoreLocalScale()
		{
			this.previousLocalScale = base.transform.localScale;
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00079774 File Offset: 0x00077974
		public virtual void ToggleSnapDropZone(VRTK_SnapDropZone snapDropZone, bool state)
		{
			this.snappedInSnapDropZone = state;
			if (state)
			{
				this.storedSnapDropZone = snapDropZone;
				this.OnInteractableObjectSnappedToDropZone(this.SetInteractableObjectEvent(snapDropZone.gameObject));
				return;
			}
			this.interactableRigidbody.WakeUp();
			this.ResetDropSnapType();
			this.OnInteractableObjectUnsnappedFromDropZone(this.SetInteractableObjectEvent(snapDropZone.gameObject));
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x000797C8 File Offset: 0x000779C8
		public virtual bool IsInSnapDropZone()
		{
			return this.snappedInSnapDropZone;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x000797D0 File Offset: 0x000779D0
		public virtual void SetSnapDropZoneHover(VRTK_SnapDropZone snapDropZone, bool state)
		{
			if (state)
			{
				if (!this.hoveredSnapObjects.Contains(snapDropZone.gameObject))
				{
					this.hoveredSnapObjects.Add(snapDropZone.gameObject);
					this.OnInteractableObjectEnteredSnapDropZone(this.SetInteractableObjectEvent(snapDropZone.gameObject));
				}
			}
			else if (this.hoveredSnapObjects.Contains(snapDropZone.gameObject))
			{
				this.hoveredSnapObjects.Remove(snapDropZone.gameObject);
				this.OnInteractableObjectExitedSnapDropZone(this.SetInteractableObjectEvent(snapDropZone.gameObject));
			}
			this.hoveredOverSnapDropZone = (this.hoveredSnapObjects.Count > 0);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00079863 File Offset: 0x00077A63
		public virtual VRTK_SnapDropZone GetStoredSnapDropZone()
		{
			return this.storedSnapDropZone;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0007986C File Offset: 0x00077A6C
		public virtual bool IsDroppable()
		{
			switch (this.validDrop)
			{
			case VRTK_InteractableObject.ValidDropTypes.NoDrop:
				return false;
			case VRTK_InteractableObject.ValidDropTypes.DropAnywhere:
				return true;
			case VRTK_InteractableObject.ValidDropTypes.DropValidSnapDropZone:
				return this.hoveredOverSnapDropZone;
			default:
				return false;
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000798A0 File Offset: 0x00077AA0
		public virtual bool IsSwappable()
		{
			return this.secondaryGrabActionScript != null && this.secondaryGrabActionScript.IsSwappable();
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x000798BD File Offset: 0x00077ABD
		public virtual bool PerformSecondaryAction()
		{
			return this.GetGrabbingObject() != null && this.GetSecondaryGrabbingObject() == null && this.secondaryGrabActionScript != null && this.secondaryGrabActionScript.IsActionable();
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x000798F8 File Offset: 0x00077AF8
		public virtual void ResetIgnoredColliders()
		{
			for (int i = 0; i < this.currentIgnoredColliders.Count; i++)
			{
				if (this.currentIgnoredColliders[i] != null)
				{
					Collider[] componentsInChildren = this.currentIgnoredColliders[i].GetComponentsInChildren<Collider>();
					if (this.ignoredColliders != null)
					{
						for (int j = 0; j < this.ignoredColliders.Count; j++)
						{
							for (int k = 0; k < componentsInChildren.Length; k++)
							{
								Physics.IgnoreCollision(componentsInChildren[k], this.ignoredColliders[j], false);
							}
						}
					}
				}
			}
			this.currentIgnoredColliders.Clear();
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00079990 File Offset: 0x00077B90
		protected virtual void Awake()
		{
			this.interactableRigidbody = base.GetComponent<Rigidbody>();
			if (this.interactableRigidbody != null)
			{
				this.interactableRigidbody.maxAngularVelocity = float.MaxValue;
			}
			if (this.disableWhenIdle && base.enabled && this.IsIdle())
			{
				this.startDisabled = true;
				base.enabled = false;
			}
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000799ED File Offset: 0x00077BED
		public virtual void OnEnable()
		{
			this.InitialiseHighlighter();
			this.RegisterTeleporters();
			this.forceDisabled = false;
			if (this.forcedDropped)
			{
				this.LoadPreviousState();
			}
			this.forcedDropped = false;
			this.startDisabled = false;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00079A1E File Offset: 0x00077C1E
		public virtual void OnDisable()
		{
			this.UnregisterTeleporters();
			if (this.autoHighlighter)
			{
				Object.Destroy(this.objectHighlighter);
				this.objectHighlighter = null;
			}
			if (!this.startDisabled)
			{
				this.forceDisabled = true;
				this.ForceStopInteracting();
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00079A58 File Offset: 0x00077C58
		protected virtual void FixedUpdate()
		{
			if (this.trackPoint != null && this.grabAttachMechanicScript != null)
			{
				this.grabAttachMechanicScript.ProcessFixedUpdate();
			}
			if (this.secondaryGrabActionScript != null)
			{
				this.secondaryGrabActionScript.ProcessFixedUpdate();
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00079AA8 File Offset: 0x00077CA8
		protected virtual void Update()
		{
			this.AttemptSetGrabMechanic();
			this.AttemptSetSecondaryGrabAction();
			if (this.trackPoint != null && this.grabAttachMechanicScript != null)
			{
				this.grabAttachMechanicScript.ProcessUpdate();
			}
			if (this.secondaryGrabActionScript != null)
			{
				this.secondaryGrabActionScript.ProcessUpdate();
			}
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00079B01 File Offset: 0x00077D01
		protected virtual bool IsIdle()
		{
			return !this.IsTouched() && !this.IsGrabbed(null) && !this.IsUsing(null);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00079B20 File Offset: 0x00077D20
		protected virtual void LateUpdate()
		{
			if (this.disableWhenIdle && this.IsIdle())
			{
				this.ToggleEnableState(false);
			}
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00079B3C File Offset: 0x00077D3C
		protected virtual void LoadPreviousState()
		{
			if (base.gameObject.activeInHierarchy)
			{
				base.transform.SetParent(this.previousParent);
				this.forcedDropped = false;
			}
			if (this.interactableRigidbody != null)
			{
				this.interactableRigidbody.isKinematic = this.previousKinematicState;
			}
			if (!this.IsSwappable())
			{
				this.isGrabbable = this.previousIsGrabbable;
			}
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00079BA4 File Offset: 0x00077DA4
		protected virtual void InitialiseHighlighter()
		{
			if (this.touchHighlightColor != Color.clear && this.objectHighlighter == null)
			{
				this.autoHighlighter = false;
				this.objectHighlighter = VRTK_BaseHighlighter.GetActiveHighlighter(base.gameObject);
				if (this.objectHighlighter == null)
				{
					this.autoHighlighter = true;
					this.objectHighlighter = base.gameObject.AddComponent<VRTK_MaterialColorSwapHighlighter>();
				}
				this.objectHighlighter.Initialise(new Color?(this.touchHighlightColor), null);
			}
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00079C28 File Offset: 0x00077E28
		protected virtual void IgnoreColliders(GameObject touchingObject)
		{
			if (this.ignoredColliders != null && !this.currentIgnoredColliders.Contains(touchingObject))
			{
				bool flag = false;
				Collider[] componentsInChildren = touchingObject.GetComponentsInChildren<Collider>();
				for (int i = 0; i < this.ignoredColliders.Count; i++)
				{
					for (int j = 0; j < componentsInChildren.Length; j++)
					{
						Physics.IgnoreCollision(componentsInChildren[j], this.ignoredColliders[i]);
						flag = true;
					}
				}
				if (flag)
				{
					this.currentIgnoredColliders.Add(touchingObject);
				}
			}
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00079C9D File Offset: 0x00077E9D
		protected virtual void ToggleEnableState(bool state)
		{
			if (this.disableWhenIdle)
			{
				base.enabled = state;
			}
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00079CB0 File Offset: 0x00077EB0
		protected virtual void AttemptSetGrabMechanic()
		{
			if (this.isGrabbable && this.grabAttachMechanicScript == null)
			{
				VRTK_BaseGrabAttach exists = base.GetComponent<VRTK_BaseGrabAttach>();
				if (!exists)
				{
					exists = base.gameObject.AddComponent<VRTK_FixedJointGrabAttach>();
				}
				this.grabAttachMechanicScript = exists;
			}
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00079CF5 File Offset: 0x00077EF5
		protected virtual void AttemptSetSecondaryGrabAction()
		{
			if (this.isGrabbable && this.secondaryGrabActionScript == null)
			{
				this.secondaryGrabActionScript = base.GetComponent<VRTK_BaseGrabAction>();
			}
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00079D1C File Offset: 0x00077F1C
		protected virtual void ForceReleaseGrab()
		{
			GameObject grabbingObject = this.GetGrabbingObject();
			if (grabbingObject != null)
			{
				grabbingObject.GetComponent<VRTK_InteractGrab>().ForceRelease(false);
			}
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00079D48 File Offset: 0x00077F48
		protected virtual void PrimaryControllerGrab(GameObject currentGrabbingObject)
		{
			if (this.snappedInSnapDropZone)
			{
				this.ToggleSnapDropZone(this.storedSnapDropZone, false);
			}
			this.ForceReleaseGrab();
			this.RemoveTrackPoint();
			this.grabbingObjects.Add(currentGrabbingObject);
			this.SetTrackPoint(currentGrabbingObject);
			if (!this.IsSwappable())
			{
				this.previousIsGrabbable = this.isGrabbable;
				this.isGrabbable = false;
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00079DA4 File Offset: 0x00077FA4
		protected virtual void SecondaryControllerGrab(GameObject currentGrabbingObject)
		{
			if (!this.grabbingObjects.Contains(currentGrabbingObject))
			{
				this.grabbingObjects.Add(currentGrabbingObject);
				this.secondaryControllerAttachPoint = this.CreateAttachPoint(currentGrabbingObject.name, "Secondary", currentGrabbingObject.transform);
				if (this.secondaryGrabActionScript != null)
				{
					this.secondaryGrabActionScript.Initialise(this, this.GetGrabbingObject().GetComponent<VRTK_InteractGrab>(), this.GetSecondaryGrabbingObject().GetComponent<VRTK_InteractGrab>(), this.primaryControllerAttachPoint, this.secondaryControllerAttachPoint);
				}
			}
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00079E24 File Offset: 0x00078024
		protected virtual void PrimaryControllerUngrab(GameObject previousGrabbingObject, GameObject previousSecondaryGrabbingObject)
		{
			this.UnpauseCollisions();
			this.RemoveTrackPoint();
			this.ResetUseState(previousGrabbingObject);
			this.grabbingObjects.Clear();
			if (this.secondaryGrabActionScript != null && previousSecondaryGrabbingObject != null)
			{
				this.secondaryGrabActionScript.OnDropAction();
				previousSecondaryGrabbingObject.GetComponent<VRTK_InteractGrab>().ForceRelease(false);
			}
			this.LoadPreviousState();
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00079E84 File Offset: 0x00078084
		protected virtual void SecondaryControllerUngrab(GameObject previousGrabbingObject)
		{
			if (this.grabbingObjects.Contains(previousGrabbingObject))
			{
				this.grabbingObjects.Remove(previousGrabbingObject);
				Object.Destroy(this.secondaryControllerAttachPoint.gameObject);
				this.secondaryControllerAttachPoint = null;
				if (this.secondaryGrabActionScript != null)
				{
					this.secondaryGrabActionScript.ResetAction();
				}
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00079EDC File Offset: 0x000780DC
		protected virtual void UnpauseCollisions()
		{
			Rigidbody[] componentsInChildren = base.GetComponentsInChildren<Rigidbody>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].detectCollisions = true;
			}
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00079F08 File Offset: 0x00078108
		protected virtual void SetTrackPoint(GameObject currentGrabbingObject)
		{
			this.AddTrackPoint(currentGrabbingObject);
			this.primaryControllerAttachPoint = this.CreateAttachPoint(this.GetGrabbingObject().name, "Original", this.trackPoint);
			if (this.grabAttachMechanicScript != null)
			{
				this.grabAttachMechanicScript.SetTrackPoint(this.trackPoint);
				this.grabAttachMechanicScript.SetInitialAttachPoint(this.primaryControllerAttachPoint);
			}
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00079F70 File Offset: 0x00078170
		protected virtual Transform CreateAttachPoint(string namePrefix, string nameSuffix, Transform origin)
		{
			Transform transform = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				namePrefix,
				nameSuffix,
				"Controller",
				"AttachPoint"
			})).transform;
			transform.parent = base.transform;
			transform.position = origin.position;
			transform.rotation = origin.rotation;
			return transform;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00079FD0 File Offset: 0x000781D0
		protected virtual void AddTrackPoint(GameObject currentGrabbingObject)
		{
			VRTK_InteractGrab component = currentGrabbingObject.GetComponent<VRTK_InteractGrab>();
			Transform controllerPoint = (component && component.controllerAttachPoint) ? component.controllerAttachPoint.transform : currentGrabbingObject.transform;
			if (this.grabAttachMechanicScript != null)
			{
				this.trackPoint = this.grabAttachMechanicScript.CreateTrackPoint(controllerPoint, base.gameObject, currentGrabbingObject, ref this.customTrackPoint);
			}
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0007A03C File Offset: 0x0007823C
		protected virtual void RemoveTrackPoint()
		{
			if (this.customTrackPoint && this.trackPoint != null)
			{
				Object.Destroy(this.trackPoint.gameObject);
			}
			else
			{
				this.trackPoint = null;
			}
			if (this.primaryControllerAttachPoint != null)
			{
				Object.Destroy(this.primaryControllerAttachPoint.gameObject);
			}
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0007A096 File Offset: 0x00078296
		protected virtual void OnTeleporting(object sender, DestinationMarkerEventArgs e)
		{
			if (!this.stayGrabbedOnTeleport)
			{
				this.ZeroVelocity();
				this.ForceStopAllInteractions();
			}
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0007A0AC File Offset: 0x000782AC
		protected virtual void OnTeleported(object sender, DestinationMarkerEventArgs e)
		{
			if (this.grabAttachMechanicScript != null && this.grabAttachMechanicScript.IsTracked() && this.stayGrabbedOnTeleport && this.trackPoint != null)
			{
				GameObject actualController = VRTK_DeviceFinder.GetActualController(this.GetGrabbingObject());
				base.transform.position = (actualController ? actualController.transform.position : base.transform.position);
			}
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0007A121 File Offset: 0x00078321
		protected virtual IEnumerator RegisterTeleportersAtEndOfFrame()
		{
			yield return new WaitForEndOfFrame();
			using (List<VRTK_BasicTeleport>.Enumerator enumerator = VRTK_ObjectCache.registeredTeleporters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					VRTK_BasicTeleport vrtk_BasicTeleport = enumerator.Current;
					vrtk_BasicTeleport.Teleporting += this.OnTeleporting;
					vrtk_BasicTeleport.Teleported += this.OnTeleported;
				}
				yield break;
			}
			yield break;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0007A130 File Offset: 0x00078330
		protected virtual void ResetUseState(GameObject checkObject)
		{
			if (checkObject != null)
			{
				VRTK_InteractUse component = checkObject.GetComponent<VRTK_InteractUse>();
				if (component != null && this.holdButtonToUse)
				{
					component.ForceStopUsing();
				}
			}
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0007A164 File Offset: 0x00078364
		protected virtual IEnumerator ForceStopInteractingAtEndOfFrame()
		{
			yield return new WaitForEndOfFrame();
			this.ForceStopAllInteractions();
			yield break;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0007A173 File Offset: 0x00078373
		protected virtual void ForceStopAllInteractions()
		{
			if (this.touchingObjects == null)
			{
				return;
			}
			this.StopTouchingInteractions();
			this.StopGrabbingInteractions();
			this.StopUsingInteractions();
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0007A190 File Offset: 0x00078390
		protected virtual void StopTouchingInteractions()
		{
			for (int i = 0; i < this.touchingObjects.Count; i++)
			{
				GameObject gameObject = this.touchingObjects[i];
				if (gameObject.activeInHierarchy || this.forceDisabled)
				{
					gameObject.GetComponent<VRTK_InteractTouch>().ForceStopTouching();
				}
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0007A1DC File Offset: 0x000783DC
		protected virtual void StopGrabbingInteractions()
		{
			GameObject grabbingObject = this.GetGrabbingObject();
			if (grabbingObject != null && (grabbingObject.activeInHierarchy || this.forceDisabled))
			{
				grabbingObject.GetComponent<VRTK_InteractTouch>().ForceStopTouching();
				grabbingObject.GetComponent<VRTK_InteractGrab>().ForceRelease(false);
				this.forcedDropped = true;
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0007A228 File Offset: 0x00078428
		protected virtual void StopUsingInteractions()
		{
			if (this.usingObject != null && (this.usingObject.gameObject.activeInHierarchy || this.forceDisabled))
			{
				if (this.usingObject.GetComponent<VRTK_InteractTouch>() != null)
				{
					this.usingObject.GetComponent<VRTK_InteractTouch>().ForceStopTouching();
				}
				if (this.usingObject.GetComponent<VRTK_InteractUse>() != null)
				{
					this.usingObject.GetComponent<VRTK_InteractUse>().ForceStopUsing();
				}
			}
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0007A2A4 File Offset: 0x000784A4
		protected virtual void ResetDropSnapType()
		{
			switch (this.storedSnapDropZone.snapType)
			{
			case VRTK_SnapDropZone.SnapTypes.UseKinematic:
			case VRTK_SnapDropZone.SnapTypes.UseParenting:
				this.LoadPreviousState();
				break;
			case VRTK_SnapDropZone.SnapTypes.UseJoint:
			{
				Joint component = this.storedSnapDropZone.GetComponent<Joint>();
				if (component)
				{
					component.connectedBody = null;
				}
				break;
			}
			}
			if (!this.previousLocalScale.Equals(Vector3.zero))
			{
				base.transform.localScale = this.previousLocalScale;
			}
			this.storedSnapDropZone.OnObjectUnsnappedFromDropZone(this.storedSnapDropZone.SetSnapDropZoneEvent(base.gameObject));
			this.storedSnapDropZone = null;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0007A33C File Offset: 0x0007853C
		protected virtual void ResetUsingObject()
		{
			if (this.usingObject != null)
			{
				VRTK_InteractUse component = this.usingObject.GetComponent<VRTK_InteractUse>();
				if (component != null)
				{
					component.ForceResetUsing();
				}
			}
		}

		// Token: 0x0400128F RID: 4751
		[Tooltip("If this is checked then the interactable object script will be disabled when the object is not being interacted with. This will eliminate the potential number of calls the interactable objects make each frame.")]
		public bool disableWhenIdle = true;

		// Token: 0x04001290 RID: 4752
		[Header("Touch Options", order = 1)]
		[Tooltip("The colour to highlight the object when it is touched. This colour will override any globally set colour (for instance on the `VRTK_InteractTouch` script).")]
		public Color touchHighlightColor = Color.clear;

		// Token: 0x04001291 RID: 4753
		[Tooltip("Determines which controller can initiate a touch action.")]
		public VRTK_InteractableObject.AllowedController allowedTouchControllers;

		// Token: 0x04001292 RID: 4754
		[Tooltip("An array of colliders on the object to ignore when being touched.")]
		public List<Collider> ignoredColliders = new List<Collider>();

		// Token: 0x04001293 RID: 4755
		[Header("Grab Options", order = 2)]
		[Tooltip("Determines if the object can be grabbed.")]
		public bool isGrabbable;

		// Token: 0x04001294 RID: 4756
		[Tooltip("If this is checked then the grab button on the controller needs to be continually held down to keep grabbing. If this is unchecked the grab button toggles the grab action with one button press to grab and another to release.")]
		[HideInInspector]
		public bool holdButtonToGrab = true;

		// Token: 0x04001295 RID: 4757
		[Tooltip("If this is checked then the object will stay grabbed to the controller when a teleport occurs. If it is unchecked then the object will be released when a teleport occurs.")]
		[HideInInspector]
		public bool stayGrabbedOnTeleport = true;

		// Token: 0x04001296 RID: 4758
		[Tooltip("Determines in what situation the object can be dropped by the controller grab button.")]
		public VRTK_InteractableObject.ValidDropTypes validDrop = VRTK_InteractableObject.ValidDropTypes.DropAnywhere;

		// Token: 0x04001297 RID: 4759
		[Tooltip("If this is set to `Undefined` then the global grab alias button will grab the object, setting it to any other button will ensure the override button is used to grab this specific interactable object.")]
		public VRTK_ControllerEvents.ButtonAlias grabOverrideButton;

		// Token: 0x04001298 RID: 4760
		[Tooltip("Determines which controller can initiate a grab action.")]
		public VRTK_InteractableObject.AllowedController allowedGrabControllers;

		// Token: 0x04001299 RID: 4761
		[Tooltip("This determines how the grabbed item will be attached to the controller when it is grabbed. If one isn't provided then the first Grab Attach script on the GameObject will be used, if one is not found and the object is grabbable then a Fixed Joint Grab Attach script will be created at runtime.")]
		public VRTK_BaseGrabAttach grabAttachMechanicScript;

		// Token: 0x0400129A RID: 4762
		[Tooltip("The script to utilise when processing the secondary controller action on a secondary grab attempt. If one isn't provided then the first Secondary Controller Grab Action script on the GameObject will be used, if one is not found then no action will be taken on secondary grab.")]
		public VRTK_BaseGrabAction secondaryGrabActionScript;

		// Token: 0x0400129B RID: 4763
		[Header("Use Options", order = 3)]
		[Tooltip("Determines if the object can be used.")]
		public bool isUsable;

		// Token: 0x0400129C RID: 4764
		[Tooltip("If this is checked then the use button on the controller needs to be continually held down to keep using. If this is unchecked the the use button toggles the use action with one button press to start using and another to stop using.")]
		public bool holdButtonToUse = true;

		// Token: 0x0400129D RID: 4765
		[Tooltip("If this is checked the object can be used only if it is currently being grabbed.")]
		public bool useOnlyIfGrabbed;

		// Token: 0x0400129E RID: 4766
		[Tooltip("If this is checked then when a Base Pointer beam (projected from the controller) hits the interactable object, if the object has `Hold Button To Use` unchecked then whilst the pointer is over the object it will run it's `Using` method. If `Hold Button To Use` is unchecked then the `Using` method will be run when the pointer is deactivated. The world pointer will not throw the `Destination Set` event if it is affecting an interactable object with this setting checked as this prevents unwanted teleporting from happening when using an object with a pointer.")]
		public bool pointerActivatesUseAction;

		// Token: 0x0400129F RID: 4767
		[Tooltip("If this is set to `Undefined` then the global use alias button will use the object, setting it to any other button will ensure the override button is used to use this specific interactable object.")]
		public VRTK_ControllerEvents.ButtonAlias useOverrideButton;

		// Token: 0x040012A0 RID: 4768
		[Tooltip("Determines which controller can initiate a use action.")]
		public VRTK_InteractableObject.AllowedController allowedUseControllers;

		// Token: 0x040012AB RID: 4779
		[HideInInspector]
		public int usingState;

		// Token: 0x040012AC RID: 4780
		protected Rigidbody interactableRigidbody;

		// Token: 0x040012AD RID: 4781
		protected List<GameObject> touchingObjects = new List<GameObject>();

		// Token: 0x040012AE RID: 4782
		protected List<GameObject> grabbingObjects = new List<GameObject>();

		// Token: 0x040012AF RID: 4783
		protected List<GameObject> hoveredSnapObjects = new List<GameObject>();

		// Token: 0x040012B0 RID: 4784
		protected VRTK_InteractUse usingObject;

		// Token: 0x040012B1 RID: 4785
		protected Transform trackPoint;

		// Token: 0x040012B2 RID: 4786
		protected bool customTrackPoint;

		// Token: 0x040012B3 RID: 4787
		protected Transform primaryControllerAttachPoint;

		// Token: 0x040012B4 RID: 4788
		protected Transform secondaryControllerAttachPoint;

		// Token: 0x040012B5 RID: 4789
		protected Transform previousParent;

		// Token: 0x040012B6 RID: 4790
		protected bool previousKinematicState;

		// Token: 0x040012B7 RID: 4791
		protected bool previousIsGrabbable;

		// Token: 0x040012B8 RID: 4792
		protected bool forcedDropped;

		// Token: 0x040012B9 RID: 4793
		protected bool forceDisabled;

		// Token: 0x040012BA RID: 4794
		protected VRTK_BaseHighlighter objectHighlighter;

		// Token: 0x040012BB RID: 4795
		protected bool autoHighlighter;

		// Token: 0x040012BC RID: 4796
		protected bool hoveredOverSnapDropZone;

		// Token: 0x040012BD RID: 4797
		protected bool snappedInSnapDropZone;

		// Token: 0x040012BE RID: 4798
		protected VRTK_SnapDropZone storedSnapDropZone;

		// Token: 0x040012BF RID: 4799
		protected Vector3 previousLocalScale = Vector3.zero;

		// Token: 0x040012C0 RID: 4800
		protected List<GameObject> currentIgnoredColliders = new List<GameObject>();

		// Token: 0x040012C1 RID: 4801
		protected bool startDisabled;

		// Token: 0x020005D5 RID: 1493
		public enum AllowedController
		{
			// Token: 0x04002792 RID: 10130
			Both,
			// Token: 0x04002793 RID: 10131
			LeftOnly,
			// Token: 0x04002794 RID: 10132
			RightOnly
		}

		// Token: 0x020005D6 RID: 1494
		public enum ValidDropTypes
		{
			// Token: 0x04002796 RID: 10134
			NoDrop,
			// Token: 0x04002797 RID: 10135
			DropAnywhere,
			// Token: 0x04002798 RID: 10136
			DropValidSnapDropZone
		}
	}
}
