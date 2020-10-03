using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VRTK
{
	// Token: 0x02000306 RID: 774
	[AddComponentMenu("VRTK/Scripts/UI/VRTK_UIPointer")]
	public class VRTK_UIPointer : MonoBehaviour
	{
		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06001AB9 RID: 6841 RVA: 0x0008CCBC File Offset: 0x0008AEBC
		// (remove) Token: 0x06001ABA RID: 6842 RVA: 0x0008CCF4 File Offset: 0x0008AEF4
		public event ControllerInteractionEventHandler ActivationButtonPressed;

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06001ABB RID: 6843 RVA: 0x0008CD2C File Offset: 0x0008AF2C
		// (remove) Token: 0x06001ABC RID: 6844 RVA: 0x0008CD64 File Offset: 0x0008AF64
		public event ControllerInteractionEventHandler ActivationButtonReleased;

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06001ABD RID: 6845 RVA: 0x0008CD9C File Offset: 0x0008AF9C
		// (remove) Token: 0x06001ABE RID: 6846 RVA: 0x0008CDD4 File Offset: 0x0008AFD4
		public event ControllerInteractionEventHandler SelectionButtonPressed;

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x06001ABF RID: 6847 RVA: 0x0008CE0C File Offset: 0x0008B00C
		// (remove) Token: 0x06001AC0 RID: 6848 RVA: 0x0008CE44 File Offset: 0x0008B044
		public event ControllerInteractionEventHandler SelectionButtonReleased;

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x06001AC1 RID: 6849 RVA: 0x0008CE7C File Offset: 0x0008B07C
		// (remove) Token: 0x06001AC2 RID: 6850 RVA: 0x0008CEB4 File Offset: 0x0008B0B4
		public event UIPointerEventHandler UIPointerElementEnter;

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x06001AC3 RID: 6851 RVA: 0x0008CEEC File Offset: 0x0008B0EC
		// (remove) Token: 0x06001AC4 RID: 6852 RVA: 0x0008CF24 File Offset: 0x0008B124
		public event UIPointerEventHandler UIPointerElementExit;

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x06001AC5 RID: 6853 RVA: 0x0008CF5C File Offset: 0x0008B15C
		// (remove) Token: 0x06001AC6 RID: 6854 RVA: 0x0008CF94 File Offset: 0x0008B194
		public event UIPointerEventHandler UIPointerElementClick;

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x06001AC7 RID: 6855 RVA: 0x0008CFCC File Offset: 0x0008B1CC
		// (remove) Token: 0x06001AC8 RID: 6856 RVA: 0x0008D004 File Offset: 0x0008B204
		public event UIPointerEventHandler UIPointerElementDragStart;

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x06001AC9 RID: 6857 RVA: 0x0008D03C File Offset: 0x0008B23C
		// (remove) Token: 0x06001ACA RID: 6858 RVA: 0x0008D074 File Offset: 0x0008B274
		public event UIPointerEventHandler UIPointerElementDragEnd;

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x0008D0A9 File Offset: 0x0008B2A9
		protected VRTK_ControllerReference controllerReference
		{
			get
			{
				return VRTK_ControllerReference.GetControllerReference((this.controller != null) ? this.controller.gameObject : null);
			}
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0008D0CC File Offset: 0x0008B2CC
		public virtual void OnUIPointerElementEnter(UIPointerEventArgs e)
		{
			if (e.currentTarget != this.currentTarget)
			{
				this.ResetHoverTimer();
			}
			if (this.clickAfterHoverDuration > 0f && this.hoverDurationTimer <= 0f)
			{
				this.canClickOnHover = true;
				this.hoverDurationTimer = this.clickAfterHoverDuration;
			}
			this.currentTarget = e.currentTarget;
			if (this.UIPointerElementEnter != null)
			{
				this.UIPointerElementEnter(this, e);
			}
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0008D140 File Offset: 0x0008B340
		public virtual void OnUIPointerElementExit(UIPointerEventArgs e)
		{
			if (e.previousTarget == this.currentTarget)
			{
				this.ResetHoverTimer();
			}
			if (this.UIPointerElementExit != null)
			{
				this.UIPointerElementExit(this, e);
				if (this.attemptClickOnDeactivate && !e.isActive && e.previousTarget)
				{
					this.pointerEventData.pointerPress = e.previousTarget;
				}
			}
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0008D1A9 File Offset: 0x0008B3A9
		public virtual void OnUIPointerElementClick(UIPointerEventArgs e)
		{
			if (e.currentTarget == this.currentTarget)
			{
				this.ResetHoverTimer();
			}
			if (this.UIPointerElementClick != null)
			{
				this.UIPointerElementClick(this, e);
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0008D1D9 File Offset: 0x0008B3D9
		public virtual void OnUIPointerElementDragStart(UIPointerEventArgs e)
		{
			if (this.UIPointerElementDragStart != null)
			{
				this.UIPointerElementDragStart(this, e);
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0008D1F0 File Offset: 0x0008B3F0
		public virtual void OnUIPointerElementDragEnd(UIPointerEventArgs e)
		{
			if (this.UIPointerElementDragEnd != null)
			{
				this.UIPointerElementDragEnd(this, e);
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0008D207 File Offset: 0x0008B407
		public virtual void OnActivationButtonPressed(ControllerInteractionEventArgs e)
		{
			if (this.ActivationButtonPressed != null)
			{
				this.ActivationButtonPressed(this, e);
			}
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0008D21E File Offset: 0x0008B41E
		public virtual void OnActivationButtonReleased(ControllerInteractionEventArgs e)
		{
			if (this.ActivationButtonReleased != null)
			{
				this.ActivationButtonReleased(this, e);
			}
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0008D235 File Offset: 0x0008B435
		public virtual void OnSelectionButtonPressed(ControllerInteractionEventArgs e)
		{
			if (this.SelectionButtonPressed != null)
			{
				this.SelectionButtonPressed(this, e);
			}
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0008D24C File Offset: 0x0008B44C
		public virtual void OnSelectionButtonReleased(ControllerInteractionEventArgs e)
		{
			if (this.SelectionButtonReleased != null)
			{
				this.SelectionButtonReleased(this, e);
			}
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0008D264 File Offset: 0x0008B464
		public virtual UIPointerEventArgs SetUIPointerEvent(RaycastResult currentRaycastResult, GameObject currentTarget, GameObject lastTarget = null)
		{
			UIPointerEventArgs result;
			result.controllerIndex = VRTK_ControllerReference.GetRealIndex(this.controllerReference);
			result.controllerReference = this.controllerReference;
			result.isActive = this.PointerActive();
			result.currentTarget = currentTarget;
			result.previousTarget = lastTarget;
			result.raycastResult = currentRaycastResult;
			return result;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0008D2B8 File Offset: 0x0008B4B8
		public virtual VRTK_VRInputModule SetEventSystem(EventSystem eventSystem)
		{
			if (!eventSystem)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_SCENE, new object[]
				{
					"VRTK_UIPointer",
					"EventSystem"
				}));
				return null;
			}
			if (!(eventSystem is VRTK_EventSystem))
			{
				eventSystem = eventSystem.gameObject.AddComponent<VRTK_EventSystem>();
			}
			return eventSystem.GetComponent<VRTK_VRInputModule>();
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0008D30C File Offset: 0x0008B50C
		public virtual void RemoveEventSystem()
		{
			VRTK_EventSystem vrtk_EventSystem = Object.FindObjectOfType<VRTK_EventSystem>();
			if (!vrtk_EventSystem)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_SCENE, new object[]
				{
					"VRTK_UIPointer",
					"EventSystem"
				}));
				return;
			}
			Object.Destroy(vrtk_EventSystem);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0008D350 File Offset: 0x0008B550
		public virtual bool PointerActive()
		{
			if (this.activationMode == VRTK_UIPointer.ActivationMethods.AlwaysOn || this.autoActivatingCanvas != null)
			{
				return true;
			}
			if (this.activationMode == VRTK_UIPointer.ActivationMethods.HoldButton)
			{
				return this.IsActivationButtonPressed();
			}
			this.pointerClicked = false;
			if (this.IsActivationButtonPressed() && !this.lastPointerPressState)
			{
				this.pointerClicked = true;
			}
			this.lastPointerPressState = (this.controller != null && this.controller.IsButtonPressed(this.activationButton));
			if (this.pointerClicked)
			{
				this.beamEnabledState = !this.beamEnabledState;
			}
			return this.beamEnabledState;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0008D3E8 File Offset: 0x0008B5E8
		public virtual bool IsActivationButtonPressed()
		{
			return this.controller != null && this.controller.IsButtonPressed(this.activationButton);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0008D40B File Offset: 0x0008B60B
		public virtual bool IsSelectionButtonPressed()
		{
			return this.controller != null && this.controller.IsButtonPressed(this.selectionButton);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0008D430 File Offset: 0x0008B630
		public virtual bool ValidClick(bool checkLastClick, bool lastClickState = false)
		{
			bool flag = this.collisionClick ? this.collisionClick : this.IsSelectionButtonPressed();
			bool result = checkLastClick ? (flag && this.lastPointerClickState == lastClickState) : flag;
			this.lastPointerClickState = flag;
			return result;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0008D470 File Offset: 0x0008B670
		public virtual Vector3 GetOriginPosition()
		{
			if (!this.pointerOriginTransform)
			{
				return base.transform.position;
			}
			return this.pointerOriginTransform.position;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0008D496 File Offset: 0x0008B696
		public virtual Vector3 GetOriginForward()
		{
			if (!this.pointerOriginTransform)
			{
				return base.transform.forward;
			}
			return this.pointerOriginTransform.forward;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0008D4BC File Offset: 0x0008B6BC
		protected virtual void Awake()
		{
			this.originalPointerOriginTransform = this.pointerOriginTransform;
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0008D4D8 File Offset: 0x0008B6D8
		protected virtual void OnEnable()
		{
			this.pointerOriginTransform = ((this.originalPointerOriginTransform == null) ? VRTK_SDK_Bridge.GenerateControllerPointerOrigin(base.gameObject) : this.originalPointerOriginTransform);
			this.controller = ((this.controller != null) ? this.controller : base.GetComponent<VRTK_ControllerEvents>());
			this.ConfigureEventSystem();
			this.pointerClicked = false;
			this.lastPointerPressState = false;
			this.lastPointerClickState = false;
			this.beamEnabledState = false;
			if (this.controller != null)
			{
				this.controller.SubscribeToButtonAliasEvent(this.activationButton, true, new ControllerInteractionEventHandler(this.DoActivationButtonPressed));
				this.controller.SubscribeToButtonAliasEvent(this.activationButton, false, new ControllerInteractionEventHandler(this.DoActivationButtonReleased));
				this.controller.SubscribeToButtonAliasEvent(this.selectionButton, true, new ControllerInteractionEventHandler(this.DoSelectionButtonPressed));
				this.controller.SubscribeToButtonAliasEvent(this.selectionButton, false, new ControllerInteractionEventHandler(this.DoSelectionButtonReleased));
			}
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0008D5DC File Offset: 0x0008B7DC
		protected virtual void OnDisable()
		{
			if (this.cachedVRInputModule && this.cachedVRInputModule.pointers.Contains(this))
			{
				this.cachedVRInputModule.pointers.Remove(this);
			}
			if (this.controller != null)
			{
				this.controller.UnsubscribeToButtonAliasEvent(this.activationButton, true, new ControllerInteractionEventHandler(this.DoActivationButtonPressed));
				this.controller.UnsubscribeToButtonAliasEvent(this.activationButton, false, new ControllerInteractionEventHandler(this.DoActivationButtonReleased));
				this.controller.UnsubscribeToButtonAliasEvent(this.selectionButton, true, new ControllerInteractionEventHandler(this.DoSelectionButtonPressed));
				this.controller.UnsubscribeToButtonAliasEvent(this.selectionButton, false, new ControllerInteractionEventHandler(this.DoSelectionButtonReleased));
			}
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0008D6A8 File Offset: 0x0008B8A8
		protected virtual void LateUpdate()
		{
			if (this.controller != null)
			{
				this.pointerEventData.pointerId = (int)VRTK_ControllerReference.GetRealIndex(this.controllerReference);
			}
			if (this.controllerRenderModel == null && VRTK_ControllerReference.IsValid(this.controllerReference))
			{
				this.controllerRenderModel = VRTK_SDK_Bridge.GetControllerRenderModel(this.controllerReference);
			}
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0008D705 File Offset: 0x0008B905
		protected virtual void DoActivationButtonPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.OnActivationButtonPressed(this.controller.SetControllerEvent());
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0008D718 File Offset: 0x0008B918
		protected virtual void DoActivationButtonReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.OnActivationButtonReleased(this.controller.SetControllerEvent());
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0008D72B File Offset: 0x0008B92B
		protected virtual void DoSelectionButtonPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.OnSelectionButtonPressed(this.controller.SetControllerEvent());
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0008D73E File Offset: 0x0008B93E
		protected virtual void DoSelectionButtonReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.OnSelectionButtonReleased(this.controller.SetControllerEvent());
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0008D751 File Offset: 0x0008B951
		protected virtual void ResetHoverTimer()
		{
			this.hoverDurationTimer = 0f;
			this.canClickOnHover = false;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0008D768 File Offset: 0x0008B968
		protected virtual void ConfigureEventSystem()
		{
			if (!this.cachedEventSystem)
			{
				this.cachedEventSystem = Object.FindObjectOfType<EventSystem>();
			}
			if (!this.cachedVRInputModule)
			{
				this.cachedVRInputModule = this.SetEventSystem(this.cachedEventSystem);
			}
			if (this.cachedEventSystem && this.cachedVRInputModule)
			{
				if (this.pointerEventData == null)
				{
					this.pointerEventData = new PointerEventData(this.cachedEventSystem);
				}
				if (!this.cachedVRInputModule.pointers.Contains(this))
				{
					this.cachedVRInputModule.pointers.Add(this);
				}
			}
		}

		// Token: 0x040015A0 RID: 5536
		[Header("Activation Settings")]
		[Tooltip("The button used to activate/deactivate the UI raycast for the pointer.")]
		public VRTK_ControllerEvents.ButtonAlias activationButton = VRTK_ControllerEvents.ButtonAlias.TouchpadPress;

		// Token: 0x040015A1 RID: 5537
		[Tooltip("Determines when the UI pointer should be active.")]
		public VRTK_UIPointer.ActivationMethods activationMode;

		// Token: 0x040015A2 RID: 5538
		[Header("Selection Settings")]
		[Tooltip("The button used to execute the select action at the pointer's target position.")]
		public VRTK_ControllerEvents.ButtonAlias selectionButton = VRTK_ControllerEvents.ButtonAlias.TriggerPress;

		// Token: 0x040015A3 RID: 5539
		[Tooltip("Determines when the UI Click event action should happen.")]
		public VRTK_UIPointer.ClickMethods clickMethod;

		// Token: 0x040015A4 RID: 5540
		[Tooltip("Determines whether the UI click action should be triggered when the pointer is deactivated. If the pointer is hovering over a clickable element then it will invoke the click action on that element. Note: Only works with `Click Method =  Click_On_Button_Up`")]
		public bool attemptClickOnDeactivate;

		// Token: 0x040015A5 RID: 5541
		[Tooltip("The amount of time the pointer can be over the same UI element before it automatically attempts to click it. 0f means no click attempt will be made.")]
		public float clickAfterHoverDuration;

		// Token: 0x040015A6 RID: 5542
		[Header("Customisation Settings")]
		[Tooltip("The controller that will be used to toggle the pointer. If the script is being applied onto a controller then this parameter can be left blank as it will be auto populated by the controller the script is on at runtime.")]
		public VRTK_ControllerEvents controller;

		// Token: 0x040015A7 RID: 5543
		[Tooltip("A custom transform to use as the origin of the pointer. If no pointer origin transform is provided then the transform the script is attached to is used.")]
		public Transform pointerOriginTransform;

		// Token: 0x040015A8 RID: 5544
		[HideInInspector]
		public PointerEventData pointerEventData;

		// Token: 0x040015A9 RID: 5545
		[HideInInspector]
		public GameObject hoveringElement;

		// Token: 0x040015AA RID: 5546
		[HideInInspector]
		public GameObject controllerRenderModel;

		// Token: 0x040015AB RID: 5547
		[HideInInspector]
		public float hoverDurationTimer;

		// Token: 0x040015AC RID: 5548
		[HideInInspector]
		public bool canClickOnHover;

		// Token: 0x040015AD RID: 5549
		[HideInInspector]
		public GameObject autoActivatingCanvas;

		// Token: 0x040015AE RID: 5550
		[HideInInspector]
		public bool collisionClick;

		// Token: 0x040015B8 RID: 5560
		protected bool pointerClicked;

		// Token: 0x040015B9 RID: 5561
		protected bool beamEnabledState;

		// Token: 0x040015BA RID: 5562
		protected bool lastPointerPressState;

		// Token: 0x040015BB RID: 5563
		protected bool lastPointerClickState;

		// Token: 0x040015BC RID: 5564
		protected GameObject currentTarget;

		// Token: 0x040015BD RID: 5565
		protected EventSystem cachedEventSystem;

		// Token: 0x040015BE RID: 5566
		protected VRTK_VRInputModule cachedVRInputModule;

		// Token: 0x040015BF RID: 5567
		protected Transform originalPointerOriginTransform;

		// Token: 0x020005F9 RID: 1529
		public enum ActivationMethods
		{
			// Token: 0x0400283E RID: 10302
			HoldButton,
			// Token: 0x0400283F RID: 10303
			ToggleButton,
			// Token: 0x04002840 RID: 10304
			AlwaysOn
		}

		// Token: 0x020005FA RID: 1530
		public enum ClickMethods
		{
			// Token: 0x04002842 RID: 10306
			ClickOnButtonUp,
			// Token: 0x04002843 RID: 10307
			ClickOnButtonDown
		}
	}
}
