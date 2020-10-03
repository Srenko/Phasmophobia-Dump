using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002DC RID: 732
	[RequireComponent(typeof(VRTK_BodyPhysics))]
	[Obsolete("`VRTK_TouchpadMovement` has been replaced with `VRTK_TouchpadControl`. This script will be removed in a future version of VRTK.")]
	public class VRTK_TouchpadMovement : MonoBehaviour
	{
		// Token: 0x14000098 RID: 152
		// (add) Token: 0x06001853 RID: 6227 RVA: 0x0008123C File Offset: 0x0007F43C
		// (remove) Token: 0x06001854 RID: 6228 RVA: 0x00081274 File Offset: 0x0007F474
		public event TouchpadMovementAxisEventHandler AxisMovement;

		// Token: 0x06001855 RID: 6229 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x000812AC File Offset: 0x0007F4AC
		protected virtual void OnEnable()
		{
			this.touchpadAxisChanged = new ControllerInteractionEventHandler(this.DoTouchpadAxisChanged);
			this.touchpadUntouched = new ControllerInteractionEventHandler(this.DoTouchpadTouchEnd);
			this.controllerLeftHand = VRTK_DeviceFinder.GetControllerLeftHand(false);
			this.controllerRightHand = VRTK_DeviceFinder.GetControllerRightHand(false);
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			if (!this.playArea)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.SDK_OBJECT_NOT_FOUND, new object[]
				{
					"PlayArea",
					"Boundaries SDK"
				}));
			}
			this.headset = VRTK_DeviceFinder.HeadsetTransform();
			if (!this.headset)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.SDK_OBJECT_NOT_FOUND, new object[]
				{
					"HeadsetTransform",
					"Headset SDK"
				}));
			}
			VRTK_PlayerObject.SetPlayerObject(base.gameObject, VRTK_PlayerObject.ObjectTypes.CameraRig);
			this.SetControllerListeners(this.controllerLeftHand, this.leftController, ref this.leftSubscribed, false);
			this.SetControllerListeners(this.controllerRightHand, this.rightController, ref this.rightSubscribed, false);
			this.bodyPhysics = base.GetComponent<VRTK_BodyPhysics>();
			this.movementSpeed = 0f;
			this.strafeSpeed = 0f;
			this.blinkFadeInTime = 0f;
			this.lastWarp = 0f;
			this.lastFlip = 0f;
			this.lastSnapRotate = 0f;
			this.multiplyMovement = false;
			this.bodyCollider = this.playArea.GetComponentInChildren<CapsuleCollider>();
			if (!this.bodyCollider)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_TouchpadMovement",
					"CapsuleCollider",
					"the PlayArea"
				}));
			}
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00081441 File Offset: 0x0007F641
		protected virtual void OnDisable()
		{
			this.SetControllerListeners(this.controllerLeftHand, this.leftController, ref this.leftSubscribed, true);
			this.SetControllerListeners(this.controllerRightHand, this.rightController, ref this.rightSubscribed, true);
			this.bodyPhysics = null;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0008147C File Offset: 0x0007F67C
		protected virtual void Update()
		{
			this.multiplyMovement = (this.controllerEvents && this.movementMultiplierButton != VRTK_ControllerEvents.ButtonAlias.Undefined && this.controllerEvents.IsButtonPressed(this.movementMultiplierButton));
			this.CheckControllerState(this.controllerLeftHand, this.leftController, ref this.leftSubscribed, ref this.previousLeftControllerState);
			this.CheckControllerState(this.controllerRightHand, this.rightController, ref this.rightSubscribed, ref this.previousRightControllerState);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000814F4 File Offset: 0x0007F6F4
		protected virtual void FixedUpdate()
		{
			bool flag = false;
			this.HandleFalling();
			if (this.horizontalAxisMovement == VRTK_TouchpadMovement.HorizontalAxisMovement.Slide)
			{
				this.CalculateSpeed(true, ref this.strafeSpeed, this.touchAxis.x);
				flag = true;
			}
			else if (this.horizontalAxisMovement == VRTK_TouchpadMovement.HorizontalAxisMovement.Rotate)
			{
				this.Rotate();
			}
			else if ((this.horizontalAxisMovement == VRTK_TouchpadMovement.HorizontalAxisMovement.SnapRotate || this.horizontalAxisMovement == VRTK_TouchpadMovement.HorizontalAxisMovement.SnapRotateWithBlink) && Mathf.Abs(this.touchAxis.x) > this.horizontalDeadzone && this.lastSnapRotate < Time.timeSinceLevelLoad)
			{
				this.SnapRotate(this.horizontalAxisMovement == VRTK_TouchpadMovement.HorizontalAxisMovement.SnapRotateWithBlink, false);
			}
			else if ((this.horizontalAxisMovement == VRTK_TouchpadMovement.HorizontalAxisMovement.Warp || this.horizontalAxisMovement == VRTK_TouchpadMovement.HorizontalAxisMovement.WarpWithBlink) && Mathf.Abs(this.touchAxis.x) > this.horizontalDeadzone && this.lastWarp < Time.timeSinceLevelLoad)
			{
				this.Warp(this.horizontalAxisMovement == VRTK_TouchpadMovement.HorizontalAxisMovement.WarpWithBlink, true);
			}
			if (this.flipDirectionEnabled && this.touchAxis.y < 0f)
			{
				if (this.touchAxis.y < -this.flipDeadzone && this.lastFlip < Time.timeSinceLevelLoad)
				{
					this.lastFlip = Time.timeSinceLevelLoad + this.flipDelay;
					this.SnapRotate(this.flipBlink, true);
				}
			}
			else if (this.verticalAxisMovement == VRTK_TouchpadMovement.VerticalAxisMovement.Slide)
			{
				this.CalculateSpeed(false, ref this.movementSpeed, this.touchAxis.y);
				flag = true;
			}
			else if ((this.verticalAxisMovement == VRTK_TouchpadMovement.VerticalAxisMovement.Warp || this.verticalAxisMovement == VRTK_TouchpadMovement.VerticalAxisMovement.WarpWithBlink) && Mathf.Abs(this.touchAxis.y) > this.verticalDeadzone && this.lastWarp < Time.timeSinceLevelLoad)
			{
				this.Warp(this.verticalAxisMovement == VRTK_TouchpadMovement.VerticalAxisMovement.WarpWithBlink, false);
			}
			if (flag)
			{
				this.Move();
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x000816AC File Offset: 0x0007F8AC
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

		// Token: 0x0600185C RID: 6236 RVA: 0x0008172F File Offset: 0x0007F92F
		protected virtual void CheckControllerState(GameObject controller, bool controllerState, ref bool subscribedState, ref bool previousState)
		{
			if (controllerState != previousState)
			{
				this.SetControllerListeners(controller, controllerState, ref subscribedState, false);
			}
			previousState = controllerState;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00081748 File Offset: 0x0007F948
		private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
		{
			this.controllerEvents = (VRTK_ControllerEvents)sender;
			if (this.moveOnButtonPress != VRTK_ControllerEvents.ButtonAlias.Undefined && !this.controllerEvents.IsButtonPressed(this.moveOnButtonPress))
			{
				this.touchAxis = Vector2.zero;
				this.controllerEvents = null;
				return;
			}
			Vector2 touchpadAxis = e.touchpadAxis;
			Vector2 normalized = touchpadAxis.normalized;
			float magnitude = touchpadAxis.magnitude;
			if (touchpadAxis.y < this.verticalDeadzone && touchpadAxis.y > -this.verticalDeadzone)
			{
				touchpadAxis.y = 0f;
			}
			else
			{
				touchpadAxis.y = (normalized * ((magnitude - this.verticalDeadzone) / (1f - this.verticalDeadzone))).y;
			}
			if (touchpadAxis.x < this.horizontalDeadzone && touchpadAxis.x > -this.horizontalDeadzone)
			{
				touchpadAxis.x = 0f;
			}
			else
			{
				touchpadAxis.x = (normalized * ((magnitude - this.horizontalDeadzone) / (1f - this.horizontalDeadzone))).x;
			}
			this.touchAxis = touchpadAxis;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00081851 File Offset: 0x0007FA51
		private void DoTouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
		{
			this.touchAxis = Vector2.zero;
			this.controllerEvents = null;
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00081868 File Offset: 0x0007FA68
		private void OnAxisMovement(VRTK_TouchpadMovement.AxisMovementType givenMovementType, VRTK_TouchpadMovement.AxisMovementDirection givenDirection)
		{
			if (this.AxisMovement != null)
			{
				TouchpadMovementAxisEventArgs e = default(TouchpadMovementAxisEventArgs);
				e.movementType = givenMovementType;
				e.direction = givenDirection;
				this.AxisMovement(this, e);
			}
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x000818A2 File Offset: 0x0007FAA2
		private void CalculateSpeed(bool horizontal, ref float speed, float inputValue)
		{
			if (inputValue != 0f)
			{
				speed = this.slideMaxSpeed * inputValue;
				speed = (this.multiplyMovement ? (speed * (horizontal ? this.horizontalMultiplier : this.verticalMultiplier)) : speed);
				return;
			}
			this.Decelerate(ref speed);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x000818E0 File Offset: 0x0007FAE0
		private void Decelerate(ref float speed)
		{
			if (speed > 0f)
			{
				speed -= Mathf.Lerp(this.slideDeceleration, this.slideMaxSpeed, 0f);
			}
			else if (speed < 0f)
			{
				speed += Mathf.Lerp(this.slideDeceleration, -this.slideMaxSpeed, 0f);
			}
			else
			{
				speed = 0f;
			}
			if (speed < this.verticalDeadzone && speed > -this.verticalDeadzone)
			{
				speed = 0f;
			}
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00081960 File Offset: 0x0007FB60
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

		// Token: 0x06001863 RID: 6243 RVA: 0x00081A1C File Offset: 0x0007FC1C
		private void Warp(bool blink = false, bool horizontal = false)
		{
			float num = this.warpRange * (this.multiplyMovement ? (horizontal ? this.horizontalMultiplier : this.verticalMultiplier) : 1f);
			Transform transform = VRTK_DeviceFinder.DeviceTransform(this.deviceForDirection);
			Vector3 vector = this.playArea.TransformPoint(this.bodyCollider.center);
			Vector3 a = vector + (horizontal ? transform.right : transform.forward) * num * (float)(((horizontal ? this.touchAxis.x : this.touchAxis.y) < 0f) ? -1 : 1);
			float num2 = 0.2f;
			Vector3 vector2 = horizontal ? ((this.touchAxis.x < 0f) ? this.headset.right : (this.headset.right * -1f)) : ((this.touchAxis.y > 0f) ? this.headset.forward : (this.headset.forward * -1f));
			RaycastHit raycastHit;
			if (Physics.Raycast(this.headset.position + Vector3.up * num2, vector2, out raycastHit, num))
			{
				a = raycastHit.point - vector2 * this.bodyCollider.radius;
			}
			if (Physics.Raycast(a + Vector3.up * (this.warpMaxAltitudeChange + num2), Vector3.down, out raycastHit, (this.warpMaxAltitudeChange + num2) * 2f))
			{
				a.y = raycastHit.point.y + this.bodyCollider.height / 2f;
				this.lastWarp = Time.timeSinceLevelLoad + this.warpDelay;
				this.playArea.position = a - vector + this.playArea.position;
				if (blink)
				{
					this.blinkFadeInTime = this.warpDelay * this.blinkDurationMultiplier;
					VRTK_SDK_Bridge.HeadsetFade(Color.black, 0f, false);
					base.Invoke("ReleaseBlink", 0.01f);
				}
				this.OnAxisMovement(VRTK_TouchpadMovement.AxisMovementType.Warp, horizontal ? ((this.touchAxis.x < 0f) ? VRTK_TouchpadMovement.AxisMovementDirection.Left : VRTK_TouchpadMovement.AxisMovementDirection.Right) : ((this.touchAxis.y < 0f) ? VRTK_TouchpadMovement.AxisMovementDirection.Backward : VRTK_TouchpadMovement.AxisMovementDirection.Forward));
			}
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00081C7C File Offset: 0x0007FE7C
		private void RotateAroundPlayer(float angle)
		{
			Vector3 vector = this.playArea.TransformPoint(this.bodyCollider.center);
			this.playArea.Rotate(Vector3.up, angle);
			vector -= this.playArea.TransformPoint(this.bodyCollider.center);
			this.playArea.position += vector;
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00081CE8 File Offset: 0x0007FEE8
		private void Rotate()
		{
			float angle = this.touchAxis.x * this.rotateMaxSpeed * Time.deltaTime * (this.multiplyMovement ? this.horizontalMultiplier : 1f) * 10f;
			this.RotateAroundPlayer(angle);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00081D34 File Offset: 0x0007FF34
		private void SnapRotate(bool blink = false, bool flipDirection = false)
		{
			this.lastSnapRotate = Time.timeSinceLevelLoad + this.snapRotateDelay;
			float angle = flipDirection ? 180f : (this.snapRotateAngle * (this.multiplyMovement ? this.horizontalMultiplier : 1f) * (float)((this.touchAxis.x < 0f) ? -1 : 1));
			this.RotateAroundPlayer(angle);
			if (blink)
			{
				this.blinkFadeInTime = this.snapRotateDelay * this.blinkDurationMultiplier;
				VRTK_SDK_Bridge.HeadsetFade(Color.black, 0f, false);
				base.Invoke("ReleaseBlink", 0.01f);
			}
			this.OnAxisMovement(flipDirection ? VRTK_TouchpadMovement.AxisMovementType.FlipDirection : VRTK_TouchpadMovement.AxisMovementType.SnapRotate, (this.touchAxis.x < 0f) ? VRTK_TouchpadMovement.AxisMovementDirection.Left : VRTK_TouchpadMovement.AxisMovementDirection.Right);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00081DF2 File Offset: 0x0007FFF2
		private void ReleaseBlink()
		{
			VRTK_SDK_Bridge.HeadsetFade(Color.clear, this.blinkFadeInTime, false);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00081E08 File Offset: 0x00080008
		private void SetControllerListeners(GameObject controller, bool controllerState, ref bool subscribedState, bool forceDisabled = false)
		{
			if (controller)
			{
				bool toggle = !forceDisabled && controllerState;
				this.ToggleControllerListeners(controller, toggle, ref subscribedState);
			}
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00081E30 File Offset: 0x00080030
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

		// Token: 0x040013C9 RID: 5065
		[Header("General settings")]
		[Tooltip("If this is checked then the left controller touchpad will be enabled for the selected movement types.")]
		public bool leftController = true;

		// Token: 0x040013CA RID: 5066
		[Tooltip("If this is checked then the right controller touchpad will be enabled for the selected movement types.")]
		public bool rightController = true;

		// Token: 0x040013CB RID: 5067
		[Tooltip("If a button is defined then the selected movement will only be performed when the specified button is being held down and the touchpad axis changes.")]
		public VRTK_ControllerEvents.ButtonAlias moveOnButtonPress;

		// Token: 0x040013CC RID: 5068
		[Tooltip("If the defined movement multiplier button is pressed then the movement will be affected by the axis multiplier value.")]
		public VRTK_ControllerEvents.ButtonAlias movementMultiplierButton;

		// Token: 0x040013CD RID: 5069
		[Header("Vertical Axis")]
		[Tooltip("Selects the main movement type to be performed when the vertical axis changes occur.")]
		public VRTK_TouchpadMovement.VerticalAxisMovement verticalAxisMovement = VRTK_TouchpadMovement.VerticalAxisMovement.Slide;

		// Token: 0x040013CE RID: 5070
		[Tooltip("Dead zone for the vertical axis. High value recommended for warp movement.")]
		[Range(0f, 1f)]
		public float verticalDeadzone = 0.2f;

		// Token: 0x040013CF RID: 5071
		[Tooltip("Multiplier for the vertical axis movement when the multiplier button is pressed.")]
		public float verticalMultiplier = 1.5f;

		// Token: 0x040013D0 RID: 5072
		[Tooltip("The direction that will be moved in is the direction of this device.")]
		public VRTK_DeviceFinder.Devices deviceForDirection;

		// Token: 0x040013D1 RID: 5073
		[Header("Direction flip")]
		[Tooltip("Enables a secondary action of a direction flip of 180 degrees when the touchpad is pulled downwards.")]
		public bool flipDirectionEnabled;

		// Token: 0x040013D2 RID: 5074
		[Tooltip("Dead zone for the downwards pull. High value recommended.")]
		[Range(0f, 1f)]
		public float flipDeadzone = 0.7f;

		// Token: 0x040013D3 RID: 5075
		[Tooltip("The delay before the next direction flip is allowed to happen.")]
		public float flipDelay = 1f;

		// Token: 0x040013D4 RID: 5076
		[Tooltip("Enables blink on flip.")]
		public bool flipBlink = true;

		// Token: 0x040013D5 RID: 5077
		[Header("Horizontal Axis")]
		[Tooltip("Selects the movement type to be performed when the horizontal axis changes occur.")]
		public VRTK_TouchpadMovement.HorizontalAxisMovement horizontalAxisMovement = VRTK_TouchpadMovement.HorizontalAxisMovement.Slide;

		// Token: 0x040013D6 RID: 5078
		[Tooltip("Dead zone for the horizontal axis. High value recommended for snap rotate and warp movement.")]
		[Range(0f, 1f)]
		public float horizontalDeadzone = 0.2f;

		// Token: 0x040013D7 RID: 5079
		[Tooltip("Multiplier for the horizontal axis movement when the multiplier button is pressed.")]
		public float horizontalMultiplier = 1.25f;

		// Token: 0x040013D8 RID: 5080
		[Tooltip("The delay before the next snap rotation is allowed to happen.")]
		public float snapRotateDelay = 0.5f;

		// Token: 0x040013D9 RID: 5081
		[Tooltip("The number of degrees to instantly rotate in to the given direction.")]
		public float snapRotateAngle = 30f;

		// Token: 0x040013DA RID: 5082
		[Tooltip("The maximum speed the play area will be rotated when the touchpad is being touched at the extremes of the axis. If a lower part of the touchpad axis is touched (nearer the centre) then the rotation speed is slower.")]
		public float rotateMaxSpeed = 3f;

		// Token: 0x040013DB RID: 5083
		[Header("Shared Axis Settings")]
		[Tooltip("Blink effect duration multiplier for the movement delay, ie. 1.0 means blink transition lasts until the delay has expired and 0.5 means the effect has completed when half of the delay time is done.")]
		[Range(0.1f, 1f)]
		public float blinkDurationMultiplier = 0.7f;

		// Token: 0x040013DC RID: 5084
		[Tooltip("The maximum speed the play area will be moved by sliding when the touchpad is being touched at the extremes of the axis. If a lower part of the touchpad axis is touched (nearer the centre) then the speed is slower.")]
		public float slideMaxSpeed = 3f;

		// Token: 0x040013DD RID: 5085
		[Tooltip("The speed in which the play area slows down to a complete stop when the user is no longer touching the touchpad. This deceleration effect can ease any motion sickness that may be suffered.")]
		public float slideDeceleration = 0.1f;

		// Token: 0x040013DE RID: 5086
		[Tooltip("The delay before the next warp is allowed to happen.")]
		public float warpDelay = 0.5f;

		// Token: 0x040013DF RID: 5087
		[Tooltip("The distance to warp in to the given direction.")]
		public float warpRange = 1f;

		// Token: 0x040013E0 RID: 5088
		[Tooltip("The maximum altitude change allowed for a warp to happen.")]
		public float warpMaxAltitudeChange = 1f;

		// Token: 0x040013E1 RID: 5089
		private GameObject controllerLeftHand;

		// Token: 0x040013E2 RID: 5090
		private GameObject controllerRightHand;

		// Token: 0x040013E3 RID: 5091
		private Transform playArea;

		// Token: 0x040013E4 RID: 5092
		private Vector2 touchAxis;

		// Token: 0x040013E5 RID: 5093
		private float movementSpeed;

		// Token: 0x040013E6 RID: 5094
		private float strafeSpeed;

		// Token: 0x040013E7 RID: 5095
		private float blinkFadeInTime;

		// Token: 0x040013E8 RID: 5096
		private float lastWarp;

		// Token: 0x040013E9 RID: 5097
		private float lastFlip;

		// Token: 0x040013EA RID: 5098
		private float lastSnapRotate;

		// Token: 0x040013EB RID: 5099
		private bool multiplyMovement;

		// Token: 0x040013EC RID: 5100
		private CapsuleCollider bodyCollider;

		// Token: 0x040013ED RID: 5101
		private Transform headset;

		// Token: 0x040013EE RID: 5102
		private bool leftSubscribed;

		// Token: 0x040013EF RID: 5103
		private bool rightSubscribed;

		// Token: 0x040013F0 RID: 5104
		private ControllerInteractionEventHandler touchpadAxisChanged;

		// Token: 0x040013F1 RID: 5105
		private ControllerInteractionEventHandler touchpadUntouched;

		// Token: 0x040013F2 RID: 5106
		private VRTK_ControllerEvents controllerEvents;

		// Token: 0x040013F3 RID: 5107
		private VRTK_BodyPhysics bodyPhysics;

		// Token: 0x040013F4 RID: 5108
		private bool wasFalling;

		// Token: 0x040013F5 RID: 5109
		private bool previousLeftControllerState;

		// Token: 0x040013F6 RID: 5110
		private bool previousRightControllerState;

		// Token: 0x020005ED RID: 1517
		public enum VerticalAxisMovement
		{
			// Token: 0x04002805 RID: 10245
			None,
			// Token: 0x04002806 RID: 10246
			Slide,
			// Token: 0x04002807 RID: 10247
			Warp,
			// Token: 0x04002808 RID: 10248
			WarpWithBlink
		}

		// Token: 0x020005EE RID: 1518
		public enum HorizontalAxisMovement
		{
			// Token: 0x0400280A RID: 10250
			None,
			// Token: 0x0400280B RID: 10251
			Slide,
			// Token: 0x0400280C RID: 10252
			Rotate,
			// Token: 0x0400280D RID: 10253
			SnapRotate,
			// Token: 0x0400280E RID: 10254
			SnapRotateWithBlink,
			// Token: 0x0400280F RID: 10255
			Warp,
			// Token: 0x04002810 RID: 10256
			WarpWithBlink
		}

		// Token: 0x020005EF RID: 1519
		public enum AxisMovementType
		{
			// Token: 0x04002812 RID: 10258
			Warp,
			// Token: 0x04002813 RID: 10259
			FlipDirection,
			// Token: 0x04002814 RID: 10260
			SnapRotate
		}

		// Token: 0x020005F0 RID: 1520
		public enum AxisMovementDirection
		{
			// Token: 0x04002816 RID: 10262
			None,
			// Token: 0x04002817 RID: 10263
			Left,
			// Token: 0x04002818 RID: 10264
			Right,
			// Token: 0x04002819 RID: 10265
			Forward,
			// Token: 0x0400281A RID: 10266
			Backward
		}
	}
}
