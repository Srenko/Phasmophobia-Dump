using System;
using System.Collections;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000253 RID: 595
	public class VRTK_PanelMenuController : MonoBehaviour
	{
		// Token: 0x0600116A RID: 4458 RVA: 0x00065D04 File Offset: 0x00063F04
		public virtual void ToggleMenu()
		{
			if (this.isShown)
			{
				this.HideMenu(true);
				return;
			}
			this.ShowMenu();
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00065D1C File Offset: 0x00063F1C
		public virtual void ShowMenu()
		{
			if (!this.isShown)
			{
				this.isShown = true;
				base.StopCoroutine("TweenMenuScale");
				if (base.enabled)
				{
					base.StartCoroutine("TweenMenuScale", this.isShown);
				}
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00065D57 File Offset: 0x00063F57
		public virtual void HideMenu(bool force)
		{
			if (this.isShown && force)
			{
				this.isShown = false;
				base.StopCoroutine("TweenMenuScale");
				if (base.enabled)
				{
					base.StartCoroutine("TweenMenuScale", this.isShown);
				}
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00065D94 File Offset: 0x00063F94
		public virtual void HideMenuImmediate()
		{
			if (this.currentPanelMenuItemController != null && this.isShown)
			{
				this.HandlePanelMenuItemControllerVisibility(this.currentPanelMenuItemController);
			}
			base.transform.localScale = Vector3.zero;
			this.canvasObject.transform.localScale = Vector3.zero;
			this.isShown = false;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00065DEF File Offset: 0x00063FEF
		protected virtual void Awake()
		{
			this.Initialize();
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00065E04 File Offset: 0x00064004
		protected virtual void Start()
		{
			this.interactableObject = base.gameObject.transform.parent.gameObject;
			if (this.interactableObject == null || this.interactableObject.GetComponent<VRTK_InteractableObject>() == null)
			{
				VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"PanelMenuController",
					"VRTK_InteractableObject",
					"a parent"
				}));
				return;
			}
			this.interactableObject.GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += this.DoInteractableObjectIsGrabbed;
			this.interactableObject.GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += this.DoInteractableObjectIsUngrabbed;
			this.canvasObject = base.gameObject.transform.GetChild(0).gameObject;
			if (this.canvasObject == null || this.canvasObject.GetComponent<Canvas>() == null)
			{
				VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"PanelMenuController",
					"Canvas",
					"a child"
				}));
			}
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00065F18 File Offset: 0x00064118
		protected virtual void Update()
		{
			if (this.interactableObject != null)
			{
				if (this.rotateTowards == null)
				{
					this.rotateTowards = VRTK_DeviceFinder.HeadsetTransform().gameObject;
					if (this.rotateTowards == null)
					{
						VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.COULD_NOT_FIND_OBJECT_FOR_ACTION, new object[]
						{
							"PanelMenuController",
							"an object",
							"rotate towards"
						}));
					}
				}
				if (this.isShown && this.rotateTowards != null)
				{
					base.transform.rotation = Quaternion.LookRotation((this.rotateTowards.transform.position - base.transform.position) * -1f, Vector3.up);
				}
				if (this.isPendingSwipeCheck)
				{
					this.CalculateSwipeAction();
				}
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00065FF0 File Offset: 0x000641F0
		protected virtual void Initialize()
		{
			if (Application.isPlaying && !this.isShown)
			{
				base.transform.localScale = Vector3.zero;
			}
			if (this.controllerEvents == null)
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, base.transform.localPosition.z);
				this.controllerEvents = base.GetComponentInParent<VRTK_ControllerEvents>();
			}
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00066078 File Offset: 0x00064278
		protected virtual void BindControllerEvents()
		{
			this.controllerEvents.TouchpadPressed += this.DoTouchpadPress;
			this.controllerEvents.TouchpadTouchStart += this.DoTouchpadTouched;
			this.controllerEvents.TouchpadTouchEnd += this.DoTouchpadUntouched;
			this.controllerEvents.TouchpadAxisChanged += this.DoTouchpadAxisChanged;
			this.controllerEvents.TriggerPressed += this.DoTriggerPressed;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00066100 File Offset: 0x00064300
		protected virtual void UnbindControllerEvents()
		{
			this.controllerEvents.TouchpadPressed -= this.DoTouchpadPress;
			this.controllerEvents.TouchpadTouchStart -= this.DoTouchpadTouched;
			this.controllerEvents.TouchpadTouchEnd -= this.DoTouchpadUntouched;
			this.controllerEvents.TouchpadAxisChanged -= this.DoTouchpadAxisChanged;
			this.controllerEvents.TriggerPressed -= this.DoTriggerPressed;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00066188 File Offset: 0x00064388
		protected virtual void HandlePanelMenuItemControllerVisibility(VRTK_PanelMenuItemController targetPanelItemController)
		{
			if (this.isShown)
			{
				if (this.currentPanelMenuItemController == targetPanelItemController)
				{
					targetPanelItemController.Hide(this.interactableObject);
					this.currentPanelMenuItemController = null;
					this.HideMenu(true);
				}
				else
				{
					this.currentPanelMenuItemController.Hide(this.interactableObject);
					this.currentPanelMenuItemController = targetPanelItemController;
				}
			}
			else
			{
				this.currentPanelMenuItemController = targetPanelItemController;
			}
			if (this.currentPanelMenuItemController != null)
			{
				this.currentPanelMenuItemController.Show(this.interactableObject);
				this.ShowMenu();
			}
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0006620D File Offset: 0x0006440D
		protected virtual IEnumerator TweenMenuScale(bool show)
		{
			float targetScale = 0f;
			Vector3 direction = -1f * Vector3.one;
			if (show)
			{
				this.canvasObject.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
				targetScale = this.zoomScaleMultiplier;
				direction = Vector3.one;
			}
			int i = 0;
			while (i < 250 && ((show && base.transform.localScale.x < targetScale) || (!show && base.transform.localScale.x > targetScale)))
			{
				base.transform.localScale += direction * Time.deltaTime * 4f * this.zoomScaleMultiplier;
				yield return true;
				int num = i;
				i = num + 1;
			}
			base.transform.localScale = direction * targetScale;
			base.StopCoroutine("TweenMenuScale");
			if (!show)
			{
				this.canvasObject.transform.localScale = Vector3.zero;
			}
			yield break;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00066223 File Offset: 0x00064423
		protected virtual void DoInteractableObjectIsGrabbed(object sender, InteractableObjectEventArgs e)
		{
			this.controllerEvents = e.interactingObject.GetComponentInParent<VRTK_ControllerEvents>();
			if (this.controllerEvents != null)
			{
				this.BindControllerEvents();
			}
			this.isGrabbed = true;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00066251 File Offset: 0x00064451
		protected virtual void DoInteractableObjectIsUngrabbed(object sender, InteractableObjectEventArgs e)
		{
			this.isGrabbed = false;
			if (this.isShown)
			{
				this.HideMenuImmediate();
			}
			if (this.controllerEvents != null)
			{
				this.UnbindControllerEvents();
				this.controllerEvents = null;
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00066284 File Offset: 0x00064484
		protected virtual void DoTouchpadPress(object sender, ControllerInteractionEventArgs e)
		{
			if (this.isGrabbed)
			{
				switch (this.CalculateTouchpadPressPosition())
				{
				case VRTK_PanelMenuController.TouchpadPressPosition.Top:
					if (this.topPanelMenuItemController != null)
					{
						this.HandlePanelMenuItemControllerVisibility(this.topPanelMenuItemController);
						return;
					}
					break;
				case VRTK_PanelMenuController.TouchpadPressPosition.Bottom:
					if (this.bottomPanelMenuItemController != null)
					{
						this.HandlePanelMenuItemControllerVisibility(this.bottomPanelMenuItemController);
						return;
					}
					break;
				case VRTK_PanelMenuController.TouchpadPressPosition.Left:
					if (this.leftPanelMenuItemController != null)
					{
						this.HandlePanelMenuItemControllerVisibility(this.leftPanelMenuItemController);
						return;
					}
					break;
				case VRTK_PanelMenuController.TouchpadPressPosition.Right:
					if (this.rightPanelMenuItemController != null)
					{
						this.HandlePanelMenuItemControllerVisibility(this.rightPanelMenuItemController);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00066327 File Offset: 0x00064527
		protected virtual void DoTouchpadTouched(object sender, ControllerInteractionEventArgs e)
		{
			this.touchStartPosition = new Vector2(e.touchpadAxis.x, e.touchpadAxis.y);
			this.touchStartTime = Time.time;
			this.isTrackingSwipe = true;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0006635C File Offset: 0x0006455C
		protected virtual void DoTouchpadUntouched(object sender, ControllerInteractionEventArgs e)
		{
			this.isTrackingSwipe = false;
			this.isPendingSwipeCheck = true;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0006636C File Offset: 0x0006456C
		protected virtual void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
		{
			this.ChangeAngle(this.CalculateAngle(e), null);
			if (this.isTrackingSwipe)
			{
				this.touchEndPosition = new Vector2(e.touchpadAxis.x, e.touchpadAxis.y);
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000663A5 File Offset: 0x000645A5
		protected virtual void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
		{
			if (this.isGrabbed)
			{
				this.OnTriggerPressed();
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000663B5 File Offset: 0x000645B5
		protected virtual void ChangeAngle(float angle, object sender = null)
		{
			this.currentAngle = angle;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000663C0 File Offset: 0x000645C0
		protected virtual void CalculateSwipeAction()
		{
			this.isPendingSwipeCheck = false;
			float num = Time.time - this.touchStartTime;
			Vector2 lhs = this.touchEndPosition - this.touchStartPosition;
			if (lhs.magnitude / num > 4f && lhs.magnitude > 0.2f)
			{
				lhs.Normalize();
				float num2 = Vector2.Dot(lhs, this.xAxis);
				num2 = Mathf.Acos(num2) * 57.29578f;
				if (num2 < 30f)
				{
					this.OnSwipeRight();
					return;
				}
				if (180f - num2 < 30f)
				{
					this.OnSwipeLeft();
					return;
				}
				num2 = Vector2.Dot(lhs, this.yAxis);
				num2 = Mathf.Acos(num2) * 57.29578f;
				if (num2 < 30f)
				{
					this.OnSwipeTop();
					return;
				}
				if (180f - num2 < 30f)
				{
					this.OnSwipeBottom();
				}
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0006649C File Offset: 0x0006469C
		protected virtual VRTK_PanelMenuController.TouchpadPressPosition CalculateTouchpadPressPosition()
		{
			if (this.CheckAnglePosition(this.currentAngle, 30f, 0f))
			{
				return VRTK_PanelMenuController.TouchpadPressPosition.Top;
			}
			if (this.CheckAnglePosition(this.currentAngle, 30f, 180f))
			{
				return VRTK_PanelMenuController.TouchpadPressPosition.Bottom;
			}
			if (this.CheckAnglePosition(this.currentAngle, 30f, 270f))
			{
				return VRTK_PanelMenuController.TouchpadPressPosition.Left;
			}
			if (this.CheckAnglePosition(this.currentAngle, 30f, 90f))
			{
				return VRTK_PanelMenuController.TouchpadPressPosition.Right;
			}
			return VRTK_PanelMenuController.TouchpadPressPosition.None;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00066512 File Offset: 0x00064712
		protected virtual void OnSwipeLeft()
		{
			if (this.currentPanelMenuItemController != null)
			{
				this.currentPanelMenuItemController.SwipeLeft(this.interactableObject);
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00066533 File Offset: 0x00064733
		protected virtual void OnSwipeRight()
		{
			if (this.currentPanelMenuItemController != null)
			{
				this.currentPanelMenuItemController.SwipeRight(this.interactableObject);
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00066554 File Offset: 0x00064754
		protected virtual void OnSwipeTop()
		{
			if (this.currentPanelMenuItemController != null)
			{
				this.currentPanelMenuItemController.SwipeTop(this.interactableObject);
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00066575 File Offset: 0x00064775
		protected virtual void OnSwipeBottom()
		{
			if (this.currentPanelMenuItemController != null)
			{
				this.currentPanelMenuItemController.SwipeBottom(this.interactableObject);
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00066596 File Offset: 0x00064796
		protected virtual void OnTriggerPressed()
		{
			if (this.currentPanelMenuItemController != null)
			{
				this.currentPanelMenuItemController.TriggerPressed(this.interactableObject);
			}
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000665B7 File Offset: 0x000647B7
		protected virtual float CalculateAngle(ControllerInteractionEventArgs e)
		{
			return e.touchpadAngle;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000665BF File Offset: 0x000647BF
		protected virtual float NormAngle(float currentDegree, float maxAngle = 360f)
		{
			if (currentDegree < 0f)
			{
				currentDegree += maxAngle;
			}
			return currentDegree % maxAngle;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000665D4 File Offset: 0x000647D4
		protected virtual bool CheckAnglePosition(float currentDegree, float tolerance, float targetDegree)
		{
			float num = this.NormAngle(currentDegree - tolerance, 360f);
			float num2 = this.NormAngle(currentDegree + tolerance, 360f);
			if (num > num2)
			{
				return targetDegree >= num || targetDegree <= num2;
			}
			return targetDegree >= num && targetDegree <= num2;
		}

		// Token: 0x04001037 RID: 4151
		[Tooltip("The GameObject the panel should rotate towards, which is the Camera (eye) by default.")]
		public GameObject rotateTowards;

		// Token: 0x04001038 RID: 4152
		[Tooltip("The scale multiplier, which relates to the scale of parent interactable object.")]
		public float zoomScaleMultiplier = 1f;

		// Token: 0x04001039 RID: 4153
		[Tooltip("The top PanelMenuItemController, which is triggered by pressing up on the controller touchpad.")]
		public VRTK_PanelMenuItemController topPanelMenuItemController;

		// Token: 0x0400103A RID: 4154
		[Tooltip("The bottom PanelMenuItemController, which is triggered by pressing down on the controller touchpad.")]
		public VRTK_PanelMenuItemController bottomPanelMenuItemController;

		// Token: 0x0400103B RID: 4155
		[Tooltip("The left PanelMenuItemController, which is triggered by pressing left on the controller touchpad.")]
		public VRTK_PanelMenuItemController leftPanelMenuItemController;

		// Token: 0x0400103C RID: 4156
		[Tooltip("The right PanelMenuItemController, which is triggered by pressing right on the controller touchpad.")]
		public VRTK_PanelMenuItemController rightPanelMenuItemController;

		// Token: 0x0400103D RID: 4157
		protected const float CanvasScaleSize = 0.001f;

		// Token: 0x0400103E RID: 4158
		protected const float AngleTolerance = 30f;

		// Token: 0x0400103F RID: 4159
		protected const float SwipeMinDist = 0.2f;

		// Token: 0x04001040 RID: 4160
		protected const float SwipeMinVelocity = 4f;

		// Token: 0x04001041 RID: 4161
		protected VRTK_ControllerEvents controllerEvents;

		// Token: 0x04001042 RID: 4162
		protected VRTK_PanelMenuItemController currentPanelMenuItemController;

		// Token: 0x04001043 RID: 4163
		protected GameObject interactableObject;

		// Token: 0x04001044 RID: 4164
		protected GameObject canvasObject;

		// Token: 0x04001045 RID: 4165
		protected readonly Vector2 xAxis = new Vector2(1f, 0f);

		// Token: 0x04001046 RID: 4166
		protected readonly Vector2 yAxis = new Vector2(0f, 1f);

		// Token: 0x04001047 RID: 4167
		protected Vector2 touchStartPosition;

		// Token: 0x04001048 RID: 4168
		protected Vector2 touchEndPosition;

		// Token: 0x04001049 RID: 4169
		protected float touchStartTime;

		// Token: 0x0400104A RID: 4170
		protected float currentAngle;

		// Token: 0x0400104B RID: 4171
		protected bool isTrackingSwipe;

		// Token: 0x0400104C RID: 4172
		protected bool isPendingSwipeCheck;

		// Token: 0x0400104D RID: 4173
		protected bool isGrabbed;

		// Token: 0x0400104E RID: 4174
		protected bool isShown;

		// Token: 0x020005B6 RID: 1462
		public enum TouchpadPressPosition
		{
			// Token: 0x040026E7 RID: 9959
			None,
			// Token: 0x040026E8 RID: 9960
			Top,
			// Token: 0x040026E9 RID: 9961
			Bottom,
			// Token: 0x040026EA RID: 9962
			Left,
			// Token: 0x040026EB RID: 9963
			Right
		}
	}
}
