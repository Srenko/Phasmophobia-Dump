using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK
{
	// Token: 0x020002A1 RID: 673
	[AddComponentMenu("VRTK/Scripts/Interactions/VRTK_InteractGrab")]
	public class VRTK_InteractGrab : MonoBehaviour
	{
		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06001574 RID: 5492 RVA: 0x00076100 File Offset: 0x00074300
		// (remove) Token: 0x06001575 RID: 5493 RVA: 0x00076138 File Offset: 0x00074338
		public event ControllerInteractionEventHandler GrabButtonPressed;

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06001576 RID: 5494 RVA: 0x00076170 File Offset: 0x00074370
		// (remove) Token: 0x06001577 RID: 5495 RVA: 0x000761A8 File Offset: 0x000743A8
		public event ControllerInteractionEventHandler GrabButtonReleased;

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06001578 RID: 5496 RVA: 0x000761E0 File Offset: 0x000743E0
		// (remove) Token: 0x06001579 RID: 5497 RVA: 0x00076218 File Offset: 0x00074418
		public event ObjectInteractEventHandler ControllerStartGrabInteractableObject;

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x0600157A RID: 5498 RVA: 0x00076250 File Offset: 0x00074450
		// (remove) Token: 0x0600157B RID: 5499 RVA: 0x00076288 File Offset: 0x00074488
		public event ObjectInteractEventHandler ControllerGrabInteractableObject;

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x0600157C RID: 5500 RVA: 0x000762C0 File Offset: 0x000744C0
		// (remove) Token: 0x0600157D RID: 5501 RVA: 0x000762F8 File Offset: 0x000744F8
		public event ObjectInteractEventHandler ControllerStartUngrabInteractableObject;

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x0600157E RID: 5502 RVA: 0x00076330 File Offset: 0x00074530
		// (remove) Token: 0x0600157F RID: 5503 RVA: 0x00076368 File Offset: 0x00074568
		public event ObjectInteractEventHandler ControllerUngrabInteractableObject;

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x0007639D File Offset: 0x0007459D
		protected VRTK_ControllerReference controllerReference
		{
			get
			{
				return VRTK_ControllerReference.GetControllerReference((this.interactTouch != null) ? this.interactTouch.gameObject : null);
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x000763C0 File Offset: 0x000745C0
		public virtual void OnControllerStartGrabInteractableObject(ObjectInteractEventArgs e)
		{
			if (this.ControllerStartGrabInteractableObject != null)
			{
				this.ControllerStartGrabInteractableObject(this, e);
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x000763D7 File Offset: 0x000745D7
		public virtual void OnControllerGrabInteractableObject(ObjectInteractEventArgs e)
		{
			if (this.ControllerGrabInteractableObject != null)
			{
				this.ControllerGrabInteractableObject(this, e);
			}
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x000763EE File Offset: 0x000745EE
		public virtual void OnControllerStartUngrabInteractableObject(ObjectInteractEventArgs e)
		{
			if (this.ControllerStartUngrabInteractableObject != null)
			{
				this.ControllerStartUngrabInteractableObject(this, e);
			}
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00076405 File Offset: 0x00074605
		public virtual void OnControllerUngrabInteractableObject(ObjectInteractEventArgs e)
		{
			if (this.ControllerUngrabInteractableObject != null)
			{
				this.ControllerUngrabInteractableObject(this, e);
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0007641C File Offset: 0x0007461C
		public virtual void OnGrabButtonPressed(ControllerInteractionEventArgs e)
		{
			this.OnGrabPressed.Invoke();
			if (this.isToggleGrab)
			{
				if (this.GetGrabbedObject() == null)
				{
					if (this.GrabButtonPressed != null)
					{
						this.GrabButtonPressed(this, e);
						return;
					}
				}
				else if (this.GrabButtonReleased != null)
				{
					this.GrabButtonReleased(this, e);
					return;
				}
			}
			else if (this.GrabButtonPressed != null)
			{
				this.GrabButtonPressed(this, e);
			}
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0007648B File Offset: 0x0007468B
		public virtual void OnGrabButtonReleased(ControllerInteractionEventArgs e)
		{
			if (this.isToggleGrab)
			{
				return;
			}
			if (this.GrabButtonReleased != null)
			{
				this.GrabButtonReleased(this, e);
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x000764AB File Offset: 0x000746AB
		public virtual bool IsGrabButtonPressed()
		{
			return this.grabPressed;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000764B3 File Offset: 0x000746B3
		public virtual void ForceRelease(bool applyGrabbingObjectVelocity = false)
		{
			this.InitUngrabbedObject(applyGrabbingObjectVelocity);
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000764BC File Offset: 0x000746BC
		public virtual void AttemptGrab()
		{
			this.AttemptGrabObject();
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x000764C4 File Offset: 0x000746C4
		public virtual GameObject GetGrabbedObject()
		{
			return this.grabbedObject;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x000764CC File Offset: 0x000746CC
		protected virtual void Awake()
		{
			this.originalControllerAttachPoint = this.controllerAttachPoint;
			this.controllerEvents = ((this.controllerEvents != null) ? this.controllerEvents : base.GetComponentInParent<VRTK_ControllerEvents>());
			this.interactTouch = ((this.interactTouch != null) ? this.interactTouch : base.GetComponentInParent<VRTK_InteractTouch>());
			if (this.interactTouch == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_NOT_INJECTED, new object[]
				{
					"VRTK_InteractGrab",
					"VRTK_InteractTouch",
					"interactTouch",
					"the same or parent"
				}));
			}
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00076574 File Offset: 0x00074774
		protected virtual void OnEnable()
		{
			this.RegrabUndroppableObject();
			this.ManageGrabListener(true);
			this.ManageInteractTouchListener(true);
			if (this.controllerEvents != null)
			{
				this.controllerEvents.ControllerIndexChanged += this.ControllerIndexChanged;
			}
			this.SetControllerAttachPoint();
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x000765C4 File Offset: 0x000747C4
		protected virtual void OnDisable()
		{
			if (this.attemptSetCurrentControllerAttachPoint != null)
			{
				base.StopCoroutine(this.attemptSetCurrentControllerAttachPoint);
				this.attemptSetCurrentControllerAttachPoint = null;
			}
			this.SetUndroppableObject();
			this.ForceRelease(false);
			this.ManageGrabListener(false);
			this.ManageInteractTouchListener(false);
			if (this.controllerEvents != null)
			{
				this.controllerEvents.ControllerIndexChanged -= this.ControllerIndexChanged;
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0007662D File Offset: 0x0007482D
		protected virtual void Update()
		{
			this.ManageGrabListener(true);
			this.CheckControllerAttachPointSet();
			this.CreateNonTouchingRigidbody();
			this.CheckPrecognitionGrab();
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00076648 File Offset: 0x00074848
		protected virtual void ControllerIndexChanged(object sender, ControllerInteractionEventArgs e)
		{
			this.SetControllerAttachPoint();
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00076650 File Offset: 0x00074850
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

		// Token: 0x06001592 RID: 5522 RVA: 0x000766E0 File Offset: 0x000748E0
		protected virtual void ControllerTouchInteractableObject(object sender, ObjectInteractEventArgs e)
		{
			if (e.target != null)
			{
				PhotonObjectInteract component = e.target.GetComponent<PhotonObjectInteract>();
				if (component != null && component.grabOverrideButton != VRTK_ControllerEvents.ButtonAlias.Undefined)
				{
					this.savedGrabButton = this.subscribedGrabButton;
					this.grabButton = component.grabOverrideButton;
				}
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00076730 File Offset: 0x00074930
		protected virtual void ControllerUntouchInteractableObject(object sender, ObjectInteractEventArgs e)
		{
			if (e.target != null && !e.target.GetComponent<PhotonObjectInteract>().isGrabbed && this.savedGrabButton != VRTK_ControllerEvents.ButtonAlias.Undefined)
			{
				this.grabButton = this.savedGrabButton;
				this.savedGrabButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00076770 File Offset: 0x00074970
		protected virtual void ManageGrabListener(bool state)
		{
			if (this.controllerEvents != null && this.subscribedGrabButton != VRTK_ControllerEvents.ButtonAlias.Undefined && (!state || !this.grabButton.Equals(this.subscribedGrabButton)))
			{
				this.controllerEvents.UnsubscribeToButtonAliasEvent(this.subscribedGrabButton, true, new ControllerInteractionEventHandler(this.DoGrabObject));
				this.controllerEvents.UnsubscribeToButtonAliasEvent(this.subscribedGrabButton, false, new ControllerInteractionEventHandler(this.DoReleaseObject));
				this.subscribedGrabButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
			}
			if (this.controllerEvents != null && state && this.grabButton != VRTK_ControllerEvents.ButtonAlias.Undefined && !this.grabButton.Equals(this.subscribedGrabButton))
			{
				this.controllerEvents.SubscribeToButtonAliasEvent(this.grabButton, true, new ControllerInteractionEventHandler(this.DoGrabObject));
				this.controllerEvents.SubscribeToButtonAliasEvent(this.grabButton, false, new ControllerInteractionEventHandler(this.DoReleaseObject));
				this.subscribedGrabButton = this.grabButton;
			}
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0007687C File Offset: 0x00074A7C
		protected virtual void RegrabUndroppableObject()
		{
			if (this.undroppableGrabbedObject != null)
			{
				PhotonObjectInteract component = this.undroppableGrabbedObject.GetComponent<PhotonObjectInteract>();
				if (this.interactTouch != null && component != null && !component.isGrabbed)
				{
					this.undroppableGrabbedObject.SetActive(true);
					this.interactTouch.ForceTouch(this.undroppableGrabbedObject);
					this.AttemptGrab();
					return;
				}
			}
			else
			{
				this.undroppableGrabbedObject = null;
			}
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x000768F0 File Offset: 0x00074AF0
		protected virtual void SetUndroppableObject()
		{
			if (this.undroppableGrabbedObject != null)
			{
				PhotonObjectInteract component = this.undroppableGrabbedObject.GetComponent<PhotonObjectInteract>();
				if (component != null && component.IsDroppable())
				{
					this.undroppableGrabbedObject = null;
					return;
				}
				this.undroppableGrabbedObject.SetActive(false);
			}
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0007693C File Offset: 0x00074B3C
		protected virtual void SetControllerAttachPoint()
		{
			if (this.controllerReference.model != null && this.originalControllerAttachPoint == null)
			{
				SDK_BaseController.ControllerHand controllerHand = VRTK_DeviceFinder.GetControllerHand(this.interactTouch.gameObject);
				string controllerElementPath = VRTK_SDK_Bridge.GetControllerElementPath(SDK_BaseController.ControllerElements.AttachPoint, controllerHand, false);
				this.attemptSetCurrentControllerAttachPoint = base.StartCoroutine(this.SetCurrentControllerAttachPoint(controllerElementPath, 10, 0.1f));
			}
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0007699E File Offset: 0x00074B9E
		protected virtual IEnumerator SetCurrentControllerAttachPoint(string searchPath, int attempts, float delay)
		{
			WaitForSeconds delayInstruction = new WaitForSeconds(delay);
			Transform defaultAttachPoint = this.controllerReference.model.transform.Find(searchPath);
			while (defaultAttachPoint == null && attempts > 0)
			{
				defaultAttachPoint = this.controllerReference.model.transform.Find(searchPath);
				int num = attempts;
				attempts = num - 1;
				yield return delayInstruction;
			}
			if (defaultAttachPoint != null)
			{
				this.controllerAttachPoint = defaultAttachPoint.GetComponent<Rigidbody>();
				if (this.controllerAttachPoint == null)
				{
					Rigidbody rigidbody = defaultAttachPoint.gameObject.AddComponent<Rigidbody>();
					rigidbody.isKinematic = true;
					this.controllerAttachPoint = rigidbody;
				}
			}
			yield break;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x000769C4 File Offset: 0x00074BC4
		protected virtual bool IsObjectGrabbable(GameObject obj)
		{
			VRTK_InteractableObject component = obj.GetComponent<VRTK_InteractableObject>();
			return this.interactTouch != null && this.interactTouch.IsObjectInteractable(obj) && component != null && (component.isGrabbable || component.PerformSecondaryAction());
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00076A10 File Offset: 0x00074C10
		protected virtual bool IsObjectHoldOnGrab(GameObject obj)
		{
			if (obj != null)
			{
				VRTK_InteractableObject component = obj.GetComponent<VRTK_InteractableObject>();
				return component != null && component.holdButtonToGrab;
			}
			return false;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00076A40 File Offset: 0x00074C40
		protected virtual void ChooseGrabSequence(PhotonObjectInteract grabbedObjectScript)
		{
			if (!grabbedObjectScript.isGrabbed || grabbedObjectScript.IsSwappable())
			{
				this.InitPrimaryGrab(grabbedObjectScript);
				return;
			}
			this.InitSecondaryGrab(grabbedObjectScript);
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00076A64 File Offset: 0x00074C64
		protected virtual void ToggleControllerVisibility(bool visible)
		{
			if (this.grabbedObject != null)
			{
				VRTK_InteractControllerAppearance[] componentsInParent = this.grabbedObject.GetComponentsInParent<VRTK_InteractControllerAppearance>(true);
				if (componentsInParent.Length != 0)
				{
					componentsInParent[0].ToggleControllerOnGrab(visible, this.controllerReference.model, this.grabbedObject);
					return;
				}
			}
			else if (visible)
			{
				VRTK_ObjectAppearance.SetRendererVisible(this.controllerReference.model, this.grabbedObject);
			}
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00076AC4 File Offset: 0x00074CC4
		protected virtual void InitGrabbedObject()
		{
			this.grabbedObject = ((this.interactTouch != null) ? this.interactTouch.GetTouchedObject() : null);
			if (this.grabbedObject != null)
			{
				this.OnControllerStartGrabInteractableObject(this.interactTouch.SetControllerInteractEvent(this.grabbedObject));
				PhotonObjectInteract component = this.grabbedObject.GetComponent<PhotonObjectInteract>();
				this.ChooseGrabSequence(component);
				this.ToggleControllerVisibility(false);
				this.OnControllerGrabInteractableObject(this.interactTouch.SetControllerInteractEvent(this.grabbedObject));
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00076B4C File Offset: 0x00074D4C
		protected virtual void InitPrimaryGrab(VRTK_InteractableObject currentGrabbedObject)
		{
			if (currentGrabbedObject.GetComponent<PhotonObjectInteract>().isGrabbed)
			{
				return;
			}
			if (!currentGrabbedObject.IsValidInteractableController(base.gameObject, currentGrabbedObject.allowedGrabControllers))
			{
				this.grabbedObject = null;
				if (this.interactTouch != null && currentGrabbedObject.IsGrabbed(base.gameObject))
				{
					this.interactTouch.ForceStopTouching();
				}
				return;
			}
			this.influencingGrabbedObject = false;
			currentGrabbedObject.SaveCurrentState();
			currentGrabbedObject.Grabbed(this);
			currentGrabbedObject.ZeroVelocity();
			currentGrabbedObject.ToggleHighlight(false);
			currentGrabbedObject.isKinematic = false;
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00076BD4 File Offset: 0x00074DD4
		protected virtual void InitSecondaryGrab(VRTK_InteractableObject currentGrabbedObject)
		{
			if (currentGrabbedObject.GetComponent<PhotonObjectInteract>().isGrabbed)
			{
				return;
			}
			if (!currentGrabbedObject.IsValidInteractableController(base.gameObject, currentGrabbedObject.allowedGrabControllers))
			{
				this.grabbedObject = null;
				this.influencingGrabbedObject = false;
				currentGrabbedObject.Ungrabbed(this);
				return;
			}
			this.influencingGrabbedObject = true;
			currentGrabbedObject.Grabbed(this);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00076C27 File Offset: 0x00074E27
		protected virtual void CheckInfluencingObjectOnRelease()
		{
			if (!this.influencingGrabbedObject && this.interactTouch != null)
			{
				this.interactTouch.ForceStopTouching();
				this.ToggleControllerVisibility(true);
			}
			this.influencingGrabbedObject = false;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00076C58 File Offset: 0x00074E58
		protected virtual void InitUngrabbedObject(bool applyGrabbingObjectVelocity)
		{
			if (this.grabbedObject != null && this.interactTouch != null)
			{
				this.OnControllerStartUngrabInteractableObject(this.interactTouch.SetControllerInteractEvent(this.grabbedObject));
				VRTK_InteractableObject component = this.grabbedObject.GetComponent<VRTK_InteractableObject>();
				if (component != null)
				{
					if (!this.influencingGrabbedObject)
					{
						component.grabAttachMechanicScript.StopGrab(applyGrabbingObjectVelocity);
					}
					component.Ungrabbed(this);
					component.ToggleHighlight(false);
					this.ToggleControllerVisibility(true);
					this.OnControllerUngrabInteractableObject(this.interactTouch.SetControllerInteractEvent(this.grabbedObject));
				}
			}
			this.CheckInfluencingObjectOnRelease();
			this.grabEnabledState = 0;
			this.grabbedObject = null;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00076D04 File Offset: 0x00074F04
		protected virtual GameObject GetGrabbableObject()
		{
			GameObject gameObject = (this.interactTouch != null) ? this.interactTouch.GetTouchedObject() : null;
			if (gameObject != null && this.interactTouch.IsObjectInteractable(gameObject))
			{
				return gameObject;
			}
			return this.grabbedObject;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00076D4D File Offset: 0x00074F4D
		protected virtual void IncrementGrabState()
		{
			if (this.interactTouch != null && !this.IsObjectHoldOnGrab(this.interactTouch.GetTouchedObject()))
			{
				this.grabEnabledState++;
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00076D80 File Offset: 0x00074F80
		protected virtual GameObject GetUndroppableObject()
		{
			if (!(this.grabbedObject != null))
			{
				return null;
			}
			VRTK_InteractableObject component = this.grabbedObject.GetComponent<VRTK_InteractableObject>();
			if (!(component != null) || component.IsDroppable())
			{
				return null;
			}
			return this.grabbedObject;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00076DC4 File Offset: 0x00074FC4
		protected virtual void AttemptHaptics(bool initialGrabAttempt)
		{
			if (this.grabbedObject != null && initialGrabAttempt)
			{
				VRTK_InteractHaptics componentInParent = this.grabbedObject.GetComponentInParent<VRTK_InteractHaptics>();
				if (componentInParent != null)
				{
					componentInParent.HapticsOnGrab(this.controllerReference);
				}
			}
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00076E04 File Offset: 0x00075004
		protected virtual void AttemptGrabObject()
		{
			GameObject grabbableObject = this.GetGrabbableObject();
			if (!(grabbableObject != null))
			{
				this.grabPrecognitionTimer = Time.time + this.grabPrecognition;
				return;
			}
			if (grabbableObject.GetComponent<PhotonObjectInteract>().isGrabbed)
			{
				return;
			}
			this.PerformGrabAttempt(grabbableObject);
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00076E4C File Offset: 0x0007504C
		protected virtual void PerformGrabAttempt(GameObject objectToGrab)
		{
			this.IncrementGrabState();
			bool initialGrabAttempt = this.IsValidGrabAttempt(objectToGrab);
			this.undroppableGrabbedObject = this.GetUndroppableObject();
			this.AttemptHaptics(initialGrabAttempt);
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00076E7A File Offset: 0x0007507A
		protected virtual bool ScriptValidGrab(VRTK_InteractableObject objectToGrabScript)
		{
			return objectToGrabScript != null && objectToGrabScript.grabAttachMechanicScript != null && objectToGrabScript.grabAttachMechanicScript.ValidGrab(this.controllerAttachPoint);
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00076EA8 File Offset: 0x000750A8
		protected virtual bool IsValidGrabAttempt(GameObject objectToGrab)
		{
			bool result = false;
			VRTK_InteractableObject vrtk_InteractableObject = (objectToGrab != null) ? objectToGrab.GetComponent<VRTK_InteractableObject>() : null;
			if (this.grabbedObject == null && this.interactTouch != null && this.IsObjectGrabbable(this.interactTouch.GetTouchedObject()) && this.ScriptValidGrab(vrtk_InteractableObject))
			{
				this.InitGrabbedObject();
				if (!this.influencingGrabbedObject)
				{
					result = vrtk_InteractableObject.grabAttachMechanicScript.StartGrab(base.gameObject, this.grabbedObject, this.controllerAttachPoint);
				}
			}
			return result;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00076F30 File Offset: 0x00075130
		protected virtual bool CanRelease()
		{
			if (this.grabbedObject != null)
			{
				VRTK_InteractableObject component = this.grabbedObject.GetComponent<VRTK_InteractableObject>();
				return component != null && component.IsDroppable();
			}
			return false;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00076F6A File Offset: 0x0007516A
		protected virtual void AttemptReleaseObject()
		{
			if (this.CanRelease() && (this.IsObjectHoldOnGrab(this.grabbedObject) || this.grabEnabledState >= 2))
			{
				this.InitUngrabbedObject(true);
			}
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00076F94 File Offset: 0x00075194
		protected virtual void DoGrabObject(object sender, ControllerInteractionEventArgs e)
		{
			if (!this.isToggleGrab)
			{
				this.OnGrabButtonPressed(this.controllerEvents.SetControllerEvent(ref this.grabPressed, true, 0f));
				this.AttemptGrabObject();
				return;
			}
			if (this.GetGrabbedObject() == null)
			{
				this.OnGrabButtonPressed(this.controllerEvents.SetControllerEvent(ref this.grabPressed, true, 0f));
				this.AttemptGrabObject();
				return;
			}
			this.AttemptReleaseObject();
			this.OnGrabButtonReleased(this.controllerEvents.SetControllerEvent(ref this.grabPressed, false, 0f));
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00077024 File Offset: 0x00075224
		protected virtual void DoReleaseObject(object sender, ControllerInteractionEventArgs e)
		{
			if (!this.isToggleGrab)
			{
				this.AttemptReleaseObject();
				this.OnGrabButtonReleased(this.controllerEvents.SetControllerEvent(ref this.grabPressed, false, 0f));
				return;
			}
			if (this.GetGrabbedObject() != null && this.GetGrabbedObject().CompareTag("Door"))
			{
				this.AttemptReleaseObject();
				this.OnGrabButtonReleased(this.controllerEvents.SetControllerEvent(ref this.grabPressed, false, 0f));
			}
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x000770A0 File Offset: 0x000752A0
		protected virtual void CheckControllerAttachPointSet()
		{
			if (this.controllerAttachPoint == null)
			{
				this.SetControllerAttachPoint();
			}
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x000770B8 File Offset: 0x000752B8
		protected virtual void CreateNonTouchingRigidbody()
		{
			if (this.createRigidBodyWhenNotTouching && this.grabbedObject == null && this.interactTouch != null && !this.interactTouch.IsRigidBodyForcedActive() && this.interactTouch.IsRigidBodyActive() != this.grabPressed)
			{
				this.interactTouch.ToggleControllerRigidBody(this.grabPressed, false);
			}
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0007711B File Offset: 0x0007531B
		protected virtual void CheckPrecognitionGrab()
		{
			if (this.grabPrecognitionTimer >= Time.time && this.GetGrabbableObject() != null)
			{
				this.AttemptGrabObject();
				if (this.GetGrabbedObject() != null)
				{
					this.grabPrecognitionTimer = 0f;
				}
			}
		}

		// Token: 0x04001240 RID: 4672
		[Header("Grab Settings")]
		[Tooltip("The button used to grab/release a touched object.")]
		public VRTK_ControllerEvents.ButtonAlias grabButton = VRTK_ControllerEvents.ButtonAlias.GripPress;

		// Token: 0x04001241 RID: 4673
		[Tooltip("An amount of time between when the grab button is pressed to when the controller is touching something to grab it. For example, if an object is falling at a fast rate, then it is very hard to press the grab button in time to catch the object due to human reaction times. A higher number here will mean the grab button can be pressed before the controller touches the object and when the collision takes place, if the grab button is still being held down then the grab action will be successful.")]
		public float grabPrecognition;

		// Token: 0x04001242 RID: 4674
		[Tooltip("An amount to multiply the velocity of any objects being thrown. This can be useful when scaling up the play area to simulate being able to throw items further.")]
		public float throwMultiplier = 1f;

		// Token: 0x04001243 RID: 4675
		[Tooltip("If this is checked and the controller is not touching an Interactable Object when the grab button is pressed then a rigid body is added to the controller to allow the controller to push other rigid body objects around.")]
		public bool createRigidBodyWhenNotTouching;

		// Token: 0x04001244 RID: 4676
		[Header("Custom Settings")]
		[Tooltip("The rigidbody point on the controller model to snap the grabbed object to. If blank it will be set to the SDK default.")]
		public Rigidbody controllerAttachPoint;

		// Token: 0x04001245 RID: 4677
		[Tooltip("The controller to listen for the events on. If the script is being applied onto a controller then this parameter can be left blank as it will be auto populated by the controller the script is on at runtime.")]
		public VRTK_ControllerEvents controllerEvents;

		// Token: 0x04001246 RID: 4678
		[Tooltip("The Interact Touch to listen for touches on. If the script is being applied onto a controller then this parameter can be left blank as it will be auto populated by the controller the script is on at runtime.")]
		public VRTK_InteractTouch interactTouch;

		// Token: 0x04001247 RID: 4679
		public bool isRightHand;

		// Token: 0x04001248 RID: 4680
		public bool isToggleGrab;

		// Token: 0x0400124F RID: 4687
		public UnityEvent OnGrabPressed;

		// Token: 0x04001250 RID: 4688
		protected VRTK_ControllerEvents.ButtonAlias subscribedGrabButton;

		// Token: 0x04001251 RID: 4689
		protected VRTK_ControllerEvents.ButtonAlias savedGrabButton;

		// Token: 0x04001252 RID: 4690
		protected bool grabPressed;

		// Token: 0x04001253 RID: 4691
		protected GameObject grabbedObject;

		// Token: 0x04001254 RID: 4692
		protected bool influencingGrabbedObject;

		// Token: 0x04001255 RID: 4693
		protected int grabEnabledState;

		// Token: 0x04001256 RID: 4694
		protected float grabPrecognitionTimer;

		// Token: 0x04001257 RID: 4695
		protected GameObject undroppableGrabbedObject;

		// Token: 0x04001258 RID: 4696
		protected Rigidbody originalControllerAttachPoint;

		// Token: 0x04001259 RID: 4697
		protected Coroutine attemptSetCurrentControllerAttachPoint;
	}
}
