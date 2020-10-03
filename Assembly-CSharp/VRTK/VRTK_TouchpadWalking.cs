using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002DD RID: 733
	[Obsolete("`VRTK_TouchpadWalking` has been replaced with `VRTK_TouchpadControl`. This script will be removed in a future version of VRTK.")]
	public class VRTK_TouchpadWalking : MonoBehaviour
	{
		// Token: 0x0600186B RID: 6251 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00081F84 File Offset: 0x00080184
		protected virtual void OnEnable()
		{
			this.touchpadAxisChanged = new ControllerInteractionEventHandler(this.DoTouchpadAxisChanged);
			this.touchpadUntouched = new ControllerInteractionEventHandler(this.DoTouchpadTouchEnd);
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			this.controllerLeftHand = VRTK_DeviceFinder.GetControllerLeftHand(false);
			this.controllerRightHand = VRTK_DeviceFinder.GetControllerRightHand(false);
			if (!this.playArea)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.SDK_OBJECT_NOT_FOUND, new object[]
				{
					"PlayArea",
					"Boundaries SDK"
				}));
			}
			VRTK_PlayerObject.SetPlayerObject(base.gameObject, VRTK_PlayerObject.ObjectTypes.CameraRig);
			this.SetControllerListeners(this.controllerLeftHand, this.leftController, ref this.leftSubscribed, false);
			this.SetControllerListeners(this.controllerRightHand, this.rightController, ref this.rightSubscribed, false);
			this.bodyPhysics = base.GetComponent<VRTK_BodyPhysics>();
			this.movementSpeed = 0f;
			this.strafeSpeed = 0f;
			this.multiplySpeed = false;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0008206D File Offset: 0x0008026D
		protected virtual void OnDisable()
		{
			this.SetControllerListeners(this.controllerLeftHand, this.leftController, ref this.leftSubscribed, true);
			this.SetControllerListeners(this.controllerRightHand, this.rightController, ref this.rightSubscribed, true);
			this.bodyPhysics = null;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x000820A8 File Offset: 0x000802A8
		protected virtual void Update()
		{
			this.multiplySpeed = (this.controllerEvents && this.speedMultiplierButton != VRTK_ControllerEvents.ButtonAlias.Undefined && this.controllerEvents.IsButtonPressed(this.speedMultiplierButton));
			this.CheckControllerState(this.controllerLeftHand, this.leftController, ref this.leftSubscribed, ref this.previousLeftControllerState);
			this.CheckControllerState(this.controllerRightHand, this.rightController, ref this.rightSubscribed, ref this.previousRightControllerState);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x00082120 File Offset: 0x00080320
		protected virtual void FixedUpdate()
		{
			this.HandleFalling();
			this.CalculateSpeed(ref this.movementSpeed, this.touchAxis.y);
			this.CalculateSpeed(ref this.strafeSpeed, this.touchAxis.x);
			this.Move();
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0008215C File Offset: 0x0008035C
		protected virtual void HandleFalling()
		{
			if (this.bodyPhysics && this.bodyPhysics.IsFalling())
			{
				this.touchAxis = Vector2.zero;
				this.wasFalling = true;
			}
			if (this.bodyPhysics && !this.bodyPhysics.IsFalling() && this.wasFalling)
			{
				this.touchAxis = Vector2.zero;
				this.wasFalling = false;
				this.strafeSpeed = 0f;
				this.movementSpeed = 0f;
			}
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000821DF File Offset: 0x000803DF
		protected virtual void CheckControllerState(GameObject controller, bool controllerState, ref bool subscribedState, ref bool previousState)
		{
			if (controllerState != previousState)
			{
				this.SetControllerListeners(controller, controllerState, ref subscribedState, false);
			}
			previousState = controllerState;
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000821F8 File Offset: 0x000803F8
		private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
		{
			this.controllerEvents = (VRTK_ControllerEvents)sender;
			if (this.moveOnButtonPress != VRTK_ControllerEvents.ButtonAlias.Undefined && !this.controllerEvents.IsButtonPressed(this.moveOnButtonPress))
			{
				this.touchAxis = Vector2.zero;
				this.controllerEvents = null;
				return;
			}
			this.touchAxis = e.touchpadAxis;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0008224B File Offset: 0x0008044B
		private void DoTouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
		{
			this.touchAxis = Vector2.zero;
			this.controllerEvents = null;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0008225F File Offset: 0x0008045F
		private void CalculateSpeed(ref float speed, float inputValue)
		{
			if (inputValue != 0f)
			{
				speed = this.maxWalkSpeed * inputValue;
				speed = (this.multiplySpeed ? (speed * this.speedMultiplier) : speed);
				return;
			}
			this.Decelerate(ref speed);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00082294 File Offset: 0x00080494
		private void Decelerate(ref float speed)
		{
			if (speed > 0f)
			{
				speed -= Mathf.Lerp(this.deceleration, this.maxWalkSpeed, 0f);
			}
			else if (speed < 0f)
			{
				speed += Mathf.Lerp(this.deceleration, -this.maxWalkSpeed, 0f);
			}
			else
			{
				speed = 0f;
			}
			float num = 0.1f;
			if (speed < num && speed > -num)
			{
				speed = 0f;
			}
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00082310 File Offset: 0x00080510
		private void Move()
		{
			Transform transform = VRTK_DeviceFinder.DeviceTransform(this.deviceForDirection);
			if (transform)
			{
				Vector3 a = transform.forward * this.movementSpeed * Time.deltaTime;
				Vector3 b = transform.right * this.strafeSpeed * Time.deltaTime;
				float y = this.playArea.position.y;
				this.playArea.position += a + b;
				this.playArea.position = new Vector3(this.playArea.position.x, y, this.playArea.position.z);
			}
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x000823CC File Offset: 0x000805CC
		private void SetControllerListeners(GameObject controller, bool controllerState, ref bool subscribedState, bool forceDisabled = false)
		{
			if (controller)
			{
				bool toggle = !forceDisabled && controllerState;
				this.ToggleControllerListeners(controller, toggle, ref subscribedState);
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x000823F4 File Offset: 0x000805F4
		private void ToggleControllerListeners(GameObject controller, bool toggle, ref bool subscribed)
		{
			VRTK_ControllerEvents component = controller.GetComponent<VRTK_ControllerEvents>();
			if (component && toggle && !subscribed)
			{
				component.TouchpadAxisChanged += this.touchpadAxisChanged;
				component.TouchpadTouchEnd += this.touchpadUntouched;
				subscribed = true;
				return;
			}
			if ((component && !toggle) & subscribed)
			{
				component.TouchpadAxisChanged -= this.touchpadAxisChanged;
				component.TouchpadTouchEnd -= this.touchpadUntouched;
				this.touchAxis = Vector2.zero;
				subscribed = false;
			}
		}

		// Token: 0x040013F7 RID: 5111
		[Tooltip("If this is checked then the left controller touchpad will be enabled to move the play area.")]
		public bool leftController = true;

		// Token: 0x040013F8 RID: 5112
		[Tooltip("If this is checked then the right controller touchpad will be enabled to move the play area.")]
		public bool rightController = true;

		// Token: 0x040013F9 RID: 5113
		[Tooltip("The maximum speed the play area will be moved when the touchpad is being touched at the extremes of the axis. If a lower part of the touchpad axis is touched (nearer the centre) then the walk speed is slower.")]
		public float maxWalkSpeed = 3f;

		// Token: 0x040013FA RID: 5114
		[Tooltip("The speed in which the play area slows down to a complete stop when the user is no longer touching the touchpad. This deceleration effect can ease any motion sickness that may be suffered.")]
		public float deceleration = 0.1f;

		// Token: 0x040013FB RID: 5115
		[Tooltip("If a button is defined then movement will only occur when the specified button is being held down and the touchpad axis changes.")]
		public VRTK_ControllerEvents.ButtonAlias moveOnButtonPress;

		// Token: 0x040013FC RID: 5116
		[Tooltip("The direction that will be moved in is the direction of this device.")]
		public VRTK_DeviceFinder.Devices deviceForDirection;

		// Token: 0x040013FD RID: 5117
		[Tooltip("If the defined speed multiplier button is pressed then the current movement speed will be multiplied by the `Speed Multiplier` value.")]
		public VRTK_ControllerEvents.ButtonAlias speedMultiplierButton;

		// Token: 0x040013FE RID: 5118
		[Tooltip("The amount to mmultiply the movement speed by if the `Speed Multiplier Button` is pressed.")]
		public float speedMultiplier = 1f;

		// Token: 0x040013FF RID: 5119
		private GameObject controllerLeftHand;

		// Token: 0x04001400 RID: 5120
		private GameObject controllerRightHand;

		// Token: 0x04001401 RID: 5121
		private Transform playArea;

		// Token: 0x04001402 RID: 5122
		private Vector2 touchAxis;

		// Token: 0x04001403 RID: 5123
		private float movementSpeed;

		// Token: 0x04001404 RID: 5124
		private float strafeSpeed;

		// Token: 0x04001405 RID: 5125
		private bool leftSubscribed;

		// Token: 0x04001406 RID: 5126
		private bool rightSubscribed;

		// Token: 0x04001407 RID: 5127
		private ControllerInteractionEventHandler touchpadAxisChanged;

		// Token: 0x04001408 RID: 5128
		private ControllerInteractionEventHandler touchpadUntouched;

		// Token: 0x04001409 RID: 5129
		private bool multiplySpeed;

		// Token: 0x0400140A RID: 5130
		private VRTK_ControllerEvents controllerEvents;

		// Token: 0x0400140B RID: 5131
		private VRTK_BodyPhysics bodyPhysics;

		// Token: 0x0400140C RID: 5132
		private bool wasFalling;

		// Token: 0x0400140D RID: 5133
		private bool previousLeftControllerState;

		// Token: 0x0400140E RID: 5134
		private bool previousRightControllerState;
	}
}
