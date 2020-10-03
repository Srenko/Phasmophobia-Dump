using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002EA RID: 746
	[AddComponentMenu("VRTK/Scripts/Pointers/VRTK_Pointer")]
	public class VRTK_Pointer : VRTK_DestinationMarker
	{
		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06001945 RID: 6469 RVA: 0x00086E30 File Offset: 0x00085030
		// (remove) Token: 0x06001946 RID: 6470 RVA: 0x00086E68 File Offset: 0x00085068
		public event ControllerInteractionEventHandler ActivationButtonPressed;

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06001947 RID: 6471 RVA: 0x00086EA0 File Offset: 0x000850A0
		// (remove) Token: 0x06001948 RID: 6472 RVA: 0x00086ED8 File Offset: 0x000850D8
		public event ControllerInteractionEventHandler ActivationButtonReleased;

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x06001949 RID: 6473 RVA: 0x00086F10 File Offset: 0x00085110
		// (remove) Token: 0x0600194A RID: 6474 RVA: 0x00086F48 File Offset: 0x00085148
		public event ControllerInteractionEventHandler SelectionButtonPressed;

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x0600194B RID: 6475 RVA: 0x00086F80 File Offset: 0x00085180
		// (remove) Token: 0x0600194C RID: 6476 RVA: 0x00086FB8 File Offset: 0x000851B8
		public event ControllerInteractionEventHandler SelectionButtonReleased;

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x0600194D RID: 6477 RVA: 0x00086FF0 File Offset: 0x000851F0
		// (remove) Token: 0x0600194E RID: 6478 RVA: 0x00087028 File Offset: 0x00085228
		public event DestinationMarkerEventHandler PointerStateValid;

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x0600194F RID: 6479 RVA: 0x00087060 File Offset: 0x00085260
		// (remove) Token: 0x06001950 RID: 6480 RVA: 0x00087098 File Offset: 0x00085298
		public event DestinationMarkerEventHandler PointerStateInvalid;

		// Token: 0x06001951 RID: 6481 RVA: 0x000870CD File Offset: 0x000852CD
		public virtual void OnActivationButtonPressed(ControllerInteractionEventArgs e)
		{
			if (this.ActivationButtonPressed != null)
			{
				this.ActivationButtonPressed(this, e);
			}
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x000870E4 File Offset: 0x000852E4
		public virtual void OnActivationButtonReleased(ControllerInteractionEventArgs e)
		{
			if (this.ActivationButtonReleased != null)
			{
				this.ActivationButtonReleased(this, e);
			}
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x000870FB File Offset: 0x000852FB
		public virtual void OnSelectionButtonPressed(ControllerInteractionEventArgs e)
		{
			if (this.SelectionButtonPressed != null)
			{
				this.SelectionButtonPressed(this, e);
			}
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00087112 File Offset: 0x00085312
		public virtual void OnSelectionButtonReleased(ControllerInteractionEventArgs e)
		{
			if (this.SelectionButtonReleased != null)
			{
				this.SelectionButtonReleased(this, e);
			}
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00087129 File Offset: 0x00085329
		public virtual void OnPointerStateValid()
		{
			if (this.PointerStateValid != null)
			{
				this.PointerStateValid(this, this.GetStateEventPayload());
			}
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00087145 File Offset: 0x00085345
		public virtual void OnPointerStateInvalid()
		{
			if (this.PointerStateInvalid != null)
			{
				this.PointerStateInvalid(this, this.GetStateEventPayload());
			}
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00087161 File Offset: 0x00085361
		public virtual bool IsActivationButtonPressed()
		{
			return this.activationButtonPressed;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00087169 File Offset: 0x00085369
		public virtual bool IsSelectionButtonPressed()
		{
			return this.selectionButtonPressed;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00087174 File Offset: 0x00085374
		public virtual void PointerEnter(RaycastHit givenHit)
		{
			if (base.enabled && givenHit.transform != null && (!this.ControllerRequired() || VRTK_ControllerReference.IsValid(this.controllerReference)))
			{
				this.SetHoverSelectionTimer(givenHit.collider);
				DestinationMarkerEventArgs e = this.SetDestinationMarkerEvent(givenHit.distance, givenHit.transform, givenHit, givenHit.point, this.controllerReference, false, this.GetCursorRotation());
				if (this.pointerRenderer != null && givenHit.collider != this.pointerRenderer.GetDestinationHit().collider)
				{
					this.OnDestinationMarkerEnter(e);
				}
				else
				{
					this.OnDestinationMarkerHover(e);
				}
				this.StartUseAction(givenHit.transform);
			}
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0008723C File Offset: 0x0008543C
		public virtual void PointerExit(RaycastHit givenHit)
		{
			this.ResetHoverSelectionTimer(givenHit.collider);
			if (givenHit.transform != null && (!this.ControllerRequired() || VRTK_ControllerReference.IsValid(this.controllerReference)))
			{
				this.OnDestinationMarkerExit(this.SetDestinationMarkerEvent(givenHit.distance, givenHit.transform, givenHit, givenHit.point, this.controllerReference, false, this.GetCursorRotation()));
				this.StopUseAction();
			}
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x000872AF File Offset: 0x000854AF
		public virtual bool CanActivate()
		{
			return Time.time >= this.activateDelayTimer;
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000872C1 File Offset: 0x000854C1
		public virtual bool CanSelect()
		{
			return Time.time >= this.selectDelayTimer;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x000872D3 File Offset: 0x000854D3
		public virtual bool IsPointerActive()
		{
			return this.currentActivationState != 0;
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000872DE File Offset: 0x000854DE
		public virtual void ResetActivationTimer(bool forceZero = false)
		{
			this.activateDelayTimer = (forceZero ? 0f : (Time.time + this.activationDelay));
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x000872FC File Offset: 0x000854FC
		public virtual void ResetSelectionTimer(bool forceZero = false)
		{
			this.selectDelayTimer = (forceZero ? 0f : (Time.time + this.selectionDelay));
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0008731C File Offset: 0x0008551C
		public virtual void Toggle(bool state)
		{
			if (!this.CanActivate() || this.NoPointerRenderer() || this.CanActivateOnToggleButton(state) || (state && this.IsPointerActive()) || (!state && !this.IsPointerActive()))
			{
				return;
			}
			this.ManageActivationState(this.willDeactivate || state);
			this.pointerRenderer.Toggle(this.IsPointerActive(), state);
			this.willDeactivate = false;
			if (!state)
			{
				this.StopUseAction();
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0008738D File Offset: 0x0008558D
		public virtual bool IsStateValid()
		{
			return this.EnabledPointerRenderer() && this.pointerRenderer.IsValidCollision();
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x000873A4 File Offset: 0x000855A4
		protected virtual void Awake()
		{
			this.originalCustomOrigin = this.customOrigin;
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000873C0 File Offset: 0x000855C0
		protected override void OnEnable()
		{
			base.OnEnable();
			VRTK_PlayerObject.SetPlayerObject(base.gameObject, VRTK_PlayerObject.ObjectTypes.Pointer);
			this.SetDefaultValues();
			if (this.NoPointerRenderer())
			{
				VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_PARAMETER, new object[]
				{
					"VRTK_Pointer",
					"VRTK_BasePointerRenderer",
					"Pointer Renderer"
				}));
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00087416 File Offset: 0x00085616
		protected override void OnDisable()
		{
			base.OnDisable();
			this.UnsubscribeActivationButton();
			this.UnsubscribeSelectionButton();
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0008742A File Offset: 0x0008562A
		protected virtual void Update()
		{
			this.AttemptControllerSetup();
			this.CheckButtonSubscriptions();
			this.HandleEnabledPointer();
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00087440 File Offset: 0x00085640
		protected virtual void SetDefaultValues()
		{
			this.customOrigin = ((this.originalCustomOrigin == null) ? VRTK_SDK_Bridge.GenerateControllerPointerOrigin(base.gameObject) : this.originalCustomOrigin);
			this.SetupRenderer();
			this.activateDelayTimer = 0f;
			this.selectDelayTimer = 0f;
			this.hoverDurationTimer = 0f;
			this.currentActivationState = 0;
			this.wasActivated = false;
			this.willDeactivate = false;
			this.canClickOnHover = false;
			this.attemptControllerSetup = true;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x000874BE File Offset: 0x000856BE
		protected virtual void AttemptControllerSetup()
		{
			if (this.attemptControllerSetup && this.FindController())
			{
				this.attemptControllerSetup = false;
				this.SetupController();
				this.SetupRenderer();
				if (this.activateOnEnable)
				{
					this.Toggle(true);
				}
			}
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x000874F4 File Offset: 0x000856F4
		protected virtual void HandleEnabledPointer()
		{
			if (this.EnabledPointerRenderer())
			{
				this.pointerRenderer.InitalizePointer(this, this.targetListPolicy, this.navMeshCheckDistance, this.headsetPositionCompensation);
				this.pointerRenderer.UpdateRenderer();
				if (!this.IsPointerActive())
				{
					bool state = this.pointerRenderer.IsVisible();
					this.pointerRenderer.ToggleInteraction(state);
				}
				this.CheckHoverSelect();
			}
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00087558 File Offset: 0x00085758
		protected virtual Quaternion? GetCursorRotation()
		{
			if (this.EnabledPointerRenderer() && this.pointerRenderer.directionIndicator != null)
			{
				return new Quaternion?(this.pointerRenderer.directionIndicator.GetRotation());
			}
			return null;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0008759F File Offset: 0x0008579F
		protected virtual bool EnabledPointerRenderer()
		{
			return this.pointerRenderer != null && this.pointerRenderer.enabled;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000875BC File Offset: 0x000857BC
		protected virtual bool NoPointerRenderer()
		{
			return this.pointerRenderer == null || !this.pointerRenderer.enabled;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x000875DC File Offset: 0x000857DC
		protected virtual bool CanActivateOnToggleButton(bool state)
		{
			bool flag = state && !this.holdButtonToActivate && this.IsPointerActive();
			if (flag)
			{
				this.willDeactivate = true;
			}
			return flag;
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x000875FC File Offset: 0x000857FC
		protected virtual bool ControllerRequired()
		{
			return this.activationButton != VRTK_ControllerEvents.ButtonAlias.Undefined || this.selectionButton > VRTK_ControllerEvents.ButtonAlias.Undefined;
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00087614 File Offset: 0x00085814
		protected virtual bool FindController()
		{
			if (this.controller == null)
			{
				this.controller = base.GetComponentInParent<VRTK_ControllerEvents>();
			}
			if (this.controller == null && this.ControllerRequired())
			{
				VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_Pointer",
					"VRTK_ControllerEvents",
					"the Controller Alias",
					". To omit this warning, set the `Activation Button` and `Selection Button` to `Undefined`"
				}));
				return false;
			}
			this.GetInteractUse();
			return true;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0008768C File Offset: 0x0008588C
		protected virtual void GetInteractUse()
		{
			this.interactUse = ((this.interactUse != null) ? this.interactUse : base.GetComponentInChildren<VRTK_InteractUse>());
			this.interactUse = ((this.interactUse == null && this.controller != null) ? this.controller.GetComponentInChildren<VRTK_InteractUse>() : this.interactUse);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000876F0 File Offset: 0x000858F0
		protected virtual void SetupController()
		{
			if (this.controller != null)
			{
				this.CheckButtonMappingConflict();
				this.SubscribeSelectionButton();
				this.SubscribeActivationButton();
			}
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00087712 File Offset: 0x00085912
		protected virtual void SetupRenderer()
		{
			if (this.EnabledPointerRenderer())
			{
				this.pointerRenderer.InitalizePointer(this, this.targetListPolicy, this.navMeshCheckDistance, this.headsetPositionCompensation);
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0008773A File Offset: 0x0008593A
		protected virtual bool ButtonMappingIsUndefined(VRTK_ControllerEvents.ButtonAlias givenButton, VRTK_ControllerEvents.ButtonAlias givenSubscribedButton)
		{
			return givenSubscribedButton != VRTK_ControllerEvents.ButtonAlias.Undefined && givenButton == VRTK_ControllerEvents.ButtonAlias.Undefined;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00087748 File Offset: 0x00085948
		protected virtual void CheckButtonMappingConflict()
		{
			if (this.activationButton == this.selectionButton)
			{
				if (this.selectOnPress && this.holdButtonToActivate)
				{
					VRTK_Logger.Warn("`Hold Button To Activate` and `Select On Press` cannot both be checked when using the same button for Activation and Selection. Fixing by setting `Select On Press` to `false`.");
				}
				if (!this.selectOnPress && !this.holdButtonToActivate)
				{
					VRTK_Logger.Warn("`Hold Button To Activate` and `Select On Press` cannot both be unchecked when using the same button for Activation and Selection. Fixing by setting `Select On Press` to `true`.");
				}
				this.selectOnPress = !this.holdButtonToActivate;
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x000877A8 File Offset: 0x000859A8
		protected virtual void CheckButtonSubscriptions()
		{
			this.CheckButtonMappingConflict();
			if (this.ButtonMappingIsUndefined(this.selectionButton, this.subscribedSelectionButton) || this.selectOnPress != this.currentSelectOnPress)
			{
				this.UnsubscribeSelectionButton();
			}
			if (this.selectionButton != this.subscribedSelectionButton)
			{
				this.SubscribeSelectionButton();
				this.UnsubscribeActivationButton();
			}
			if (this.ButtonMappingIsUndefined(this.activationButton, this.subscribedActivationButton))
			{
				this.UnsubscribeActivationButton();
			}
			if (this.activationButton != this.subscribedActivationButton)
			{
				this.SubscribeActivationButton();
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0008782C File Offset: 0x00085A2C
		protected virtual void SubscribeActivationButton()
		{
			if (this.subscribedActivationButton != VRTK_ControllerEvents.ButtonAlias.Undefined)
			{
				this.UnsubscribeActivationButton();
			}
			if (this.controller != null)
			{
				this.controller.SubscribeToButtonAliasEvent(this.activationButton, true, new ControllerInteractionEventHandler(this.DoActivationButtonPressed));
				this.controller.SubscribeToButtonAliasEvent(this.activationButton, false, new ControllerInteractionEventHandler(this.DoActivationButtonReleased));
				this.subscribedActivationButton = this.activationButton;
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x000878A0 File Offset: 0x00085AA0
		protected virtual void UnsubscribeActivationButton()
		{
			if (this.controller != null && this.subscribedActivationButton != VRTK_ControllerEvents.ButtonAlias.Undefined)
			{
				this.controller.UnsubscribeToButtonAliasEvent(this.subscribedActivationButton, true, new ControllerInteractionEventHandler(this.DoActivationButtonPressed));
				this.controller.UnsubscribeToButtonAliasEvent(this.subscribedActivationButton, false, new ControllerInteractionEventHandler(this.DoActivationButtonReleased));
				this.subscribedActivationButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00087908 File Offset: 0x00085B08
		protected virtual void DoActivationButtonPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.OnActivationButtonPressed(this.controller.SetControllerEvent(ref this.activationButtonPressed, true, 0f));
			if (this.EnabledPointerRenderer())
			{
				this.controllerReference = e.controllerReference;
				this.Toggle(true);
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00087944 File Offset: 0x00085B44
		protected virtual void DoActivationButtonReleased(object sender, ControllerInteractionEventArgs e)
		{
			if (this.EnabledPointerRenderer())
			{
				this.controllerReference = e.controllerReference;
				if (this.IsPointerActive())
				{
					this.Toggle(false);
				}
			}
			this.OnActivationButtonReleased(this.controller.SetControllerEvent(ref this.activationButtonPressed, false, 0f));
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00087994 File Offset: 0x00085B94
		protected virtual void SubscribeSelectionButton()
		{
			if (this.subscribedSelectionButton != VRTK_ControllerEvents.ButtonAlias.Undefined)
			{
				this.UnsubscribeSelectionButton();
			}
			if (this.controller != null)
			{
				this.controller.SubscribeToButtonAliasEvent(this.selectionButton, true, new ControllerInteractionEventHandler(this.DoSelectionButtonPressed));
				this.controller.SubscribeToButtonAliasEvent(this.selectionButton, false, new ControllerInteractionEventHandler(this.DoSelectionButtonReleased));
				this.controller.SubscribeToButtonAliasEvent(this.selectionButton, this.selectOnPress, new ControllerInteractionEventHandler(this.SelectionButtonAction));
				this.subscribedSelectionButton = this.selectionButton;
				this.currentSelectOnPress = this.selectOnPress;
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00087A38 File Offset: 0x00085C38
		protected virtual void UnsubscribeSelectionButton()
		{
			if (this.controller != null && this.subscribedSelectionButton != VRTK_ControllerEvents.ButtonAlias.Undefined)
			{
				this.controller.UnsubscribeToButtonAliasEvent(this.selectionButton, true, new ControllerInteractionEventHandler(this.DoSelectionButtonPressed));
				this.controller.UnsubscribeToButtonAliasEvent(this.selectionButton, false, new ControllerInteractionEventHandler(this.DoSelectionButtonReleased));
				this.controller.UnsubscribeToButtonAliasEvent(this.subscribedSelectionButton, this.currentSelectOnPress, new ControllerInteractionEventHandler(this.SelectionButtonAction));
				this.subscribedSelectionButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
			}
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00087AC4 File Offset: 0x00085CC4
		protected virtual void DoSelectionButtonPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.OnSelectionButtonPressed(this.controller.SetControllerEvent(ref this.selectionButtonPressed, true, 0f));
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00087AE3 File Offset: 0x00085CE3
		protected virtual void DoSelectionButtonReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.OnSelectionButtonReleased(this.controller.SetControllerEvent(ref this.selectionButtonPressed, false, 0f));
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00087B02 File Offset: 0x00085D02
		protected virtual void SelectionButtonAction(object sender, ControllerInteractionEventArgs e)
		{
			this.controllerReference = e.controllerReference;
			this.ExecuteSelectionButtonAction();
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00087B18 File Offset: 0x00085D18
		protected virtual void ExecuteSelectionButtonAction()
		{
			if (this.EnabledPointerRenderer() && this.CanSelect() && (this.IsPointerActive() || this.wasActivated))
			{
				this.wasActivated = false;
				RaycastHit destinationHit = this.pointerRenderer.GetDestinationHit();
				this.AttemptUseOnSet(destinationHit.transform);
				if (destinationHit.transform && this.IsPointerActive() && this.pointerRenderer.ValidPlayArea() && !this.PointerActivatesUseAction(this.pointerInteractableObject) && this.pointerRenderer.IsValidCollision())
				{
					this.ResetHoverSelectionTimer(destinationHit.collider);
					this.ResetSelectionTimer(false);
					this.OnDestinationMarkerSet(this.SetDestinationMarkerEvent(destinationHit.distance, destinationHit.transform, destinationHit, destinationHit.point, this.controllerReference, false, this.GetCursorRotation()));
				}
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00087BEF File Offset: 0x00085DEF
		protected virtual bool CanResetActivationState(bool givenState)
		{
			return (!givenState && this.holdButtonToActivate) || (givenState && !this.holdButtonToActivate && this.currentActivationState >= 2);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00087C17 File Offset: 0x00085E17
		protected virtual void ManageActivationState(bool state)
		{
			if (state)
			{
				this.currentActivationState++;
			}
			this.wasActivated = (this.currentActivationState == 2);
			if (this.CanResetActivationState(state))
			{
				this.currentActivationState = 0;
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00087C49 File Offset: 0x00085E49
		protected virtual bool PointerActivatesUseAction(VRTK_InteractableObject givenInteractableObject)
		{
			return givenInteractableObject != null && givenInteractableObject.pointerActivatesUseAction && (!this.ControllerRequired() || givenInteractableObject.IsValidInteractableController(this.controller.gameObject, givenInteractableObject.allowedUseControllers));
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00087C80 File Offset: 0x00085E80
		protected virtual void StartUseAction(Transform target)
		{
			this.pointerInteractableObject = target.GetComponent<VRTK_InteractableObject>();
			bool flag = this.pointerInteractableObject && this.pointerInteractableObject.useOnlyIfGrabbed && !this.pointerInteractableObject.IsGrabbed(null);
			if (this.interactUse != null && this.PointerActivatesUseAction(this.pointerInteractableObject) && this.pointerInteractableObject.holdButtonToUse && !flag && this.pointerInteractableObject.usingState == 0)
			{
				this.pointerInteractableObject.StartUsing(this.interactUse);
				this.pointerInteractableObject.usingState++;
			}
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00087D24 File Offset: 0x00085F24
		protected virtual void StopUseAction()
		{
			if (this.interactUse != null && this.PointerActivatesUseAction(this.pointerInteractableObject) && this.pointerInteractableObject.holdButtonToUse && this.pointerInteractableObject.IsUsing(null))
			{
				this.pointerInteractableObject.StopUsing(this.interactUse);
				this.pointerInteractableObject.usingState = 0;
			}
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00087D88 File Offset: 0x00085F88
		protected virtual void AttemptUseOnSet(Transform target)
		{
			if (this.pointerInteractableObject != null && target != null && this.interactUse != null && this.PointerActivatesUseAction(this.pointerInteractableObject))
			{
				if (this.pointerInteractableObject.IsUsing(null))
				{
					this.pointerInteractableObject.StopUsing(this.interactUse);
					this.pointerInteractableObject.usingState = 0;
					return;
				}
				if (!this.pointerInteractableObject.holdButtonToUse)
				{
					this.pointerInteractableObject.StartUsing(this.interactUse);
					this.pointerInteractableObject.usingState++;
				}
			}
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00087E28 File Offset: 0x00086028
		protected virtual void SetHoverSelectionTimer(Collider collider)
		{
			if (collider != this.currentCollider)
			{
				this.hoverDurationTimer = 0f;
			}
			if (this.selectAfterHoverDuration > 0f && this.hoverDurationTimer <= 0f)
			{
				this.canClickOnHover = true;
				this.hoverDurationTimer = this.selectAfterHoverDuration;
			}
			this.currentCollider = collider;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00087E82 File Offset: 0x00086082
		protected virtual void ResetHoverSelectionTimer(Collider collider)
		{
			this.canClickOnHover = false;
			this.hoverDurationTimer = ((collider == this.currentCollider) ? 0f : this.hoverDurationTimer);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00087EAC File Offset: 0x000860AC
		protected virtual void CheckHoverSelect()
		{
			if (this.hoverDurationTimer > 0f)
			{
				this.hoverDurationTimer -= Time.deltaTime;
			}
			if (this.canClickOnHover && this.hoverDurationTimer <= 0f)
			{
				this.canClickOnHover = false;
				this.ExecuteSelectionButtonAction();
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00087EFC File Offset: 0x000860FC
		protected virtual DestinationMarkerEventArgs GetStateEventPayload()
		{
			DestinationMarkerEventArgs result = default(DestinationMarkerEventArgs);
			if (this.EnabledPointerRenderer())
			{
				RaycastHit destinationHit = this.pointerRenderer.GetDestinationHit();
				result = this.SetDestinationMarkerEvent(destinationHit.distance, destinationHit.transform, destinationHit, destinationHit.point, this.controllerReference, false, this.GetCursorRotation());
			}
			return result;
		}

		// Token: 0x040014B2 RID: 5298
		[Header("Pointer Activation Settings")]
		[Tooltip("The specific renderer to use when the pointer is activated. The renderer also determines how the pointer reaches it's destination (e.g. straight line, bezier curve).")]
		public VRTK_BasePointerRenderer pointerRenderer;

		// Token: 0x040014B3 RID: 5299
		[Tooltip("The button used to activate/deactivate the pointer.")]
		public VRTK_ControllerEvents.ButtonAlias activationButton = VRTK_ControllerEvents.ButtonAlias.TouchpadPress;

		// Token: 0x040014B4 RID: 5300
		[Tooltip("If this is checked then the Activation Button needs to be continuously held down to keep the pointer active. If this is unchecked then the Activation Button works as a toggle, the first press/release enables the pointer and the second press/release disables the pointer.")]
		public bool holdButtonToActivate = true;

		// Token: 0x040014B5 RID: 5301
		[Tooltip("If this is checked then the pointer will be toggled on when the script is enabled.")]
		public bool activateOnEnable;

		// Token: 0x040014B6 RID: 5302
		[Tooltip("The time in seconds to delay the pointer being able to be active again.")]
		public float activationDelay;

		// Token: 0x040014B7 RID: 5303
		[Header("Pointer Selection Settings")]
		[Tooltip("The button used to execute the select action at the pointer's target position.")]
		public VRTK_ControllerEvents.ButtonAlias selectionButton = VRTK_ControllerEvents.ButtonAlias.TouchpadPress;

		// Token: 0x040014B8 RID: 5304
		[Tooltip("If this is checked then the pointer selection action is executed when the Selection Button is pressed down. If this is unchecked then the selection action is executed when the Selection Button is released.")]
		public bool selectOnPress;

		// Token: 0x040014B9 RID: 5305
		[Tooltip("The time in seconds to delay the pointer being able to execute the select action again.")]
		public float selectionDelay;

		// Token: 0x040014BA RID: 5306
		[Tooltip("The amount of time the pointer can be over the same collider before it automatically attempts to select it. 0f means no selection attempt will be made.")]
		public float selectAfterHoverDuration;

		// Token: 0x040014BB RID: 5307
		[Header("Pointer Interaction Settings")]
		[Tooltip("If this is checked then the pointer will be an extension of the controller and able to interact with Interactable Objects.")]
		public bool interactWithObjects;

		// Token: 0x040014BC RID: 5308
		[Tooltip("If `Interact With Objects` is checked and this is checked then when an object is grabbed with the pointer touching it, the object will attach to the pointer tip and not snap to the controller.")]
		public bool grabToPointerTip;

		// Token: 0x040014BD RID: 5309
		[Header("Pointer Customisation Settings")]
		[Tooltip("An optional controller that will be used to toggle the pointer. If the script is being applied onto a controller then this parameter can be left blank as it will be auto populated by the controller the script is on at runtime.")]
		public VRTK_ControllerEvents controller;

		// Token: 0x040014BE RID: 5310
		[Tooltip("An optional InteractUse script that will be used when using interactable objects with pointer. If this is left blank then it will attempt to get the InteractUse script from the same GameObject and if it cannot find one then it will attempt to get it from the attached controller.")]
		public VRTK_InteractUse interactUse;

		// Token: 0x040014BF RID: 5311
		[Tooltip("A custom transform to use as the origin of the pointer. If no pointer origin transform is provided then the transform the script is attached to is used.")]
		public Transform customOrigin;

		// Token: 0x040014C6 RID: 5318
		protected VRTK_ControllerEvents.ButtonAlias subscribedActivationButton;

		// Token: 0x040014C7 RID: 5319
		protected VRTK_ControllerEvents.ButtonAlias subscribedSelectionButton;

		// Token: 0x040014C8 RID: 5320
		protected bool currentSelectOnPress;

		// Token: 0x040014C9 RID: 5321
		protected float activateDelayTimer;

		// Token: 0x040014CA RID: 5322
		protected float selectDelayTimer;

		// Token: 0x040014CB RID: 5323
		protected float hoverDurationTimer;

		// Token: 0x040014CC RID: 5324
		protected int currentActivationState;

		// Token: 0x040014CD RID: 5325
		protected bool willDeactivate;

		// Token: 0x040014CE RID: 5326
		protected bool wasActivated;

		// Token: 0x040014CF RID: 5327
		protected VRTK_ControllerReference controllerReference;

		// Token: 0x040014D0 RID: 5328
		protected VRTK_InteractableObject pointerInteractableObject;

		// Token: 0x040014D1 RID: 5329
		protected Collider currentCollider;

		// Token: 0x040014D2 RID: 5330
		protected bool canClickOnHover;

		// Token: 0x040014D3 RID: 5331
		protected bool activationButtonPressed;

		// Token: 0x040014D4 RID: 5332
		protected bool selectionButtonPressed;

		// Token: 0x040014D5 RID: 5333
		protected bool attemptControllerSetup;

		// Token: 0x040014D6 RID: 5334
		protected Transform originalCustomOrigin;
	}
}
