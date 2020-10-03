using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002CF RID: 719
	[AddComponentMenu("VRTK/Scripts/Locomotion/VRTK_MoveInPlace")]
	public class VRTK_MoveInPlace : MonoBehaviour
	{
		// Token: 0x060017DC RID: 6108 RVA: 0x0007F294 File Offset: 0x0007D494
		public virtual void SetControlOptions(VRTK_MoveInPlace.ControlOptions givenControlOptions)
		{
			this.controlOptions = givenControlOptions;
			this.trackedObjects.Clear();
			if (this.controllerLeftHand != null && this.controllerRightHand != null && (this.controlOptions.Equals(VRTK_MoveInPlace.ControlOptions.HeadsetAndControllers) || this.controlOptions.Equals(VRTK_MoveInPlace.ControlOptions.ControllersOnly)))
			{
				this.trackedObjects.Add(VRTK_DeviceFinder.GetActualController(this.controllerLeftHand).transform);
				this.trackedObjects.Add(VRTK_DeviceFinder.GetActualController(this.controllerRightHand).transform);
			}
			if (this.headset != null && (this.controlOptions.Equals(VRTK_MoveInPlace.ControlOptions.HeadsetAndControllers) || this.controlOptions.Equals(VRTK_MoveInPlace.ControlOptions.HeadsetOnly)))
			{
				this.trackedObjects.Add(this.headset.transform);
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0007F38D File Offset: 0x0007D58D
		public virtual Vector3 GetMovementDirection()
		{
			return this.direction;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0007F395 File Offset: 0x0007D595
		public virtual float GetSpeed()
		{
			return this.currentSpeed;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0007F3A0 File Offset: 0x0007D5A0
		protected virtual void OnEnable()
		{
			this.trackedObjects = new List<Transform>();
			this.movementList = new Dictionary<Transform, List<float>>();
			this.previousYPositions = new Dictionary<Transform, float>();
			this.initalGaze = Vector3.zero;
			this.direction = Vector3.zero;
			this.previousDirection = Vector3.zero;
			this.averagePeriod = 60;
			this.currentSpeed = 0f;
			this.active = false;
			this.previousEngageButton = this.engageButton;
			this.bodyPhysics = ((this.bodyPhysics != null) ? this.bodyPhysics : base.GetComponentInChildren<VRTK_BodyPhysics>());
			this.controllerLeftHand = VRTK_DeviceFinder.GetControllerLeftHand(false);
			this.controllerRightHand = VRTK_DeviceFinder.GetControllerRightHand(false);
			this.SetControllerListeners(this.controllerLeftHand, this.leftController, ref this.leftSubscribed, false);
			this.SetControllerListeners(this.controllerRightHand, this.rightController, ref this.rightSubscribed, false);
			this.headset = VRTK_DeviceFinder.HeadsetTransform();
			this.SetControlOptions(this.controlOptions);
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			for (int i = 0; i < this.trackedObjects.Count; i++)
			{
				Transform transform = this.trackedObjects[i];
				this.movementList.Add(transform, new List<float>());
				this.previousYPositions.Add(transform, transform.transform.localPosition.y);
			}
			if (this.playArea == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.SDK_OBJECT_NOT_FOUND, new object[]
				{
					"PlayArea",
					"Boundaries SDK"
				}));
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0007F524 File Offset: 0x0007D724
		protected virtual void OnDisable()
		{
			this.SetControllerListeners(this.controllerLeftHand, this.leftController, ref this.leftSubscribed, true);
			this.SetControllerListeners(this.controllerRightHand, this.rightController, ref this.rightSubscribed, true);
			this.controllerLeftHand = null;
			this.controllerRightHand = null;
			this.headset = null;
			this.playArea = null;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0007F580 File Offset: 0x0007D780
		protected virtual void Update()
		{
			this.CheckControllerState(this.controllerLeftHand, this.leftController, ref this.leftSubscribed, ref this.previousLeftControllerState);
			this.CheckControllerState(this.controllerRightHand, this.rightController, ref this.leftSubscribed, ref this.previousRightControllerState);
			this.previousEngageButton = this.engageButton;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0007F5D8 File Offset: 0x0007D7D8
		protected virtual void FixedUpdate()
		{
			this.HandleFalling();
			if (this.MovementActivated() && !this.currentlyFalling)
			{
				float num = Mathf.Clamp(this.speedScale * 350f * (this.CalculateListAverage() / (float)this.trackedObjects.Count), 0f, this.maxSpeed);
				this.previousDirection = this.direction;
				this.direction = this.SetDirection();
				this.currentSpeed = num;
			}
			else if (this.currentSpeed > 0f)
			{
				this.currentSpeed -= (this.currentlyFalling ? this.fallingDeceleration : this.deceleration);
			}
			else
			{
				this.currentSpeed = 0f;
				this.direction = Vector3.zero;
				this.previousDirection = Vector3.zero;
			}
			this.SetDeltaTransformData();
			this.MovePlayArea(this.direction, this.currentSpeed);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0007F6B8 File Offset: 0x0007D8B8
		protected virtual bool MovementActivated()
		{
			return this.active || this.engageButton == VRTK_ControllerEvents.ButtonAlias.Undefined;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0007F6CD File Offset: 0x0007D8CD
		protected virtual void CheckControllerState(GameObject controller, bool controllerState, ref bool subscribedState, ref bool previousState)
		{
			if (controllerState != previousState || this.engageButton != this.previousEngageButton)
			{
				this.SetControllerListeners(controller, controllerState, ref subscribedState, false);
			}
			previousState = controllerState;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0007F6F4 File Offset: 0x0007D8F4
		protected virtual float CalculateListAverage()
		{
			float num = 0f;
			for (int i = 0; i < this.trackedObjects.Count; i++)
			{
				Transform transform = this.trackedObjects[i];
				float num2 = Mathf.Abs(this.previousYPositions[transform] - transform.transform.localPosition.y);
				List<float> list = this.movementList[transform];
				if (num2 > this.sensitivity)
				{
					list.Add(this.sensitivity);
				}
				else
				{
					list.Add(num2);
				}
				if (list.Count > this.averagePeriod)
				{
					list.RemoveAt(0);
				}
				float num3 = 0f;
				for (int j = 0; j < list.Count; j++)
				{
					float num4 = list[j];
					num3 += num4;
				}
				float num5 = num3 / (float)this.averagePeriod;
				num += num5;
			}
			return num;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0007F7DC File Offset: 0x0007D9DC
		protected virtual Vector3 SetDirection()
		{
			Vector3 result = Vector3.zero;
			if (this.directionMethod == VRTK_MoveInPlace.DirectionalMethod.SmartDecoupling || this.directionMethod == VRTK_MoveInPlace.DirectionalMethod.DumbDecoupling)
			{
				if (this.initalGaze.Equals(Vector3.zero))
				{
					this.initalGaze = new Vector3(this.headset.forward.x, 0f, this.headset.forward.z);
				}
				if (this.directionMethod == VRTK_MoveInPlace.DirectionalMethod.SmartDecoupling)
				{
					bool flag = true;
					float num = this.headset.rotation.eulerAngles.y;
					if (num <= this.smartDecoupleThreshold)
					{
						num += 360f;
					}
					if (flag && Mathf.Abs(num - this.controllerLeftHand.transform.rotation.eulerAngles.y) <= this.smartDecoupleThreshold && Mathf.Abs(num - this.controllerRightHand.transform.rotation.eulerAngles.y) <= this.smartDecoupleThreshold)
					{
						this.initalGaze = new Vector3(this.headset.forward.x, 0f, this.headset.forward.z);
					}
				}
				result = this.initalGaze;
			}
			else if (this.directionMethod.Equals(VRTK_MoveInPlace.DirectionalMethod.ControllerRotation))
			{
				Vector3 calculatedControllerDirection = this.DetermineAverageControllerRotation() * Vector3.forward;
				result = this.CalculateControllerRotationDirection(calculatedControllerDirection);
			}
			else if (this.directionMethod.Equals(VRTK_MoveInPlace.DirectionalMethod.LeftControllerRotationOnly))
			{
				Vector3 calculatedControllerDirection2 = ((this.controllerLeftHand != null) ? this.controllerLeftHand.transform.rotation : Quaternion.identity) * Vector3.forward;
				result = this.CalculateControllerRotationDirection(calculatedControllerDirection2);
			}
			else if (this.directionMethod.Equals(VRTK_MoveInPlace.DirectionalMethod.RightControllerRotationOnly))
			{
				Vector3 calculatedControllerDirection3 = ((this.controllerRightHand != null) ? this.controllerRightHand.transform.rotation : Quaternion.identity) * Vector3.forward;
				result = this.CalculateControllerRotationDirection(calculatedControllerDirection3);
			}
			else if (this.directionMethod.Equals(VRTK_MoveInPlace.DirectionalMethod.EngageControllerRotationOnly))
			{
				Vector3 calculatedControllerDirection4 = ((this.engagedController != null) ? this.engagedController.scriptAlias.transform.rotation : Quaternion.identity) * Vector3.forward;
				result = this.CalculateControllerRotationDirection(calculatedControllerDirection4);
			}
			else if (this.directionMethod.Equals(VRTK_MoveInPlace.DirectionalMethod.Gaze))
			{
				result = new Vector3(this.headset.forward.x, 0f, this.headset.forward.z);
			}
			return result;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0007FAA4 File Offset: 0x0007DCA4
		protected virtual Vector3 CalculateControllerRotationDirection(Vector3 calculatedControllerDirection)
		{
			if (Vector3.Angle(this.previousDirection, calculatedControllerDirection) > 90f)
			{
				return this.previousDirection;
			}
			return calculatedControllerDirection;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0007FAC4 File Offset: 0x0007DCC4
		protected virtual void SetDeltaTransformData()
		{
			for (int i = 0; i < this.trackedObjects.Count; i++)
			{
				Transform transform = this.trackedObjects[i];
				this.previousYPositions[transform] = transform.transform.localPosition.y;
			}
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0007FB10 File Offset: 0x0007DD10
		protected virtual void MovePlayArea(Vector3 moveDirection, float moveSpeed)
		{
			Vector3 vector = moveDirection * moveSpeed * Time.fixedDeltaTime;
			Vector3 vector2 = new Vector3(vector.x + this.playArea.position.x, this.playArea.position.y, vector.z + this.playArea.position.z);
			if (this.playArea != null && this.CanMove(this.bodyPhysics, this.playArea.position, vector2))
			{
				this.playArea.position = vector2;
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0007FBA8 File Offset: 0x0007DDA8
		protected virtual bool CanMove(VRTK_BodyPhysics givenBodyPhysics, Vector3 currentPosition, Vector3 proposedPosition)
		{
			if (givenBodyPhysics == null)
			{
				return true;
			}
			Vector3 normalized = (proposedPosition - currentPosition).normalized;
			float maxDistance = Vector3.Distance(currentPosition, proposedPosition);
			return !givenBodyPhysics.SweepCollision(normalized, maxDistance);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0007FBE4 File Offset: 0x0007DDE4
		protected virtual void HandleFalling()
		{
			if (this.bodyPhysics != null && this.bodyPhysics.IsFalling())
			{
				this.currentlyFalling = true;
			}
			if (this.bodyPhysics != null && !this.bodyPhysics.IsFalling() && this.currentlyFalling)
			{
				this.currentlyFalling = false;
				this.currentSpeed = 0f;
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0007FC48 File Offset: 0x0007DE48
		protected virtual void EngageButtonPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.engagedController = e.controllerReference;
			this.active = true;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0007FC60 File Offset: 0x0007DE60
		protected virtual void EngageButtonReleased(object sender, ControllerInteractionEventArgs e)
		{
			for (int i = 0; i < this.trackedObjects.Count; i++)
			{
				Transform key = this.trackedObjects[i];
				this.movementList[key].Clear();
			}
			this.initalGaze = Vector3.zero;
			this.active = false;
			this.engagedController = null;
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0007FCBC File Offset: 0x0007DEBC
		protected virtual Quaternion DetermineAverageControllerRotation()
		{
			Quaternion result;
			if (this.controllerLeftHand != null && this.controllerRightHand != null)
			{
				result = this.AverageRotation(this.controllerLeftHand.transform.rotation, this.controllerRightHand.transform.rotation);
			}
			else if (this.controllerLeftHand != null && this.controllerRightHand == null)
			{
				result = this.controllerLeftHand.transform.rotation;
			}
			else if (this.controllerRightHand != null && this.controllerLeftHand == null)
			{
				result = this.controllerRightHand.transform.rotation;
			}
			else
			{
				result = Quaternion.identity;
			}
			return result;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0007FD73 File Offset: 0x0007DF73
		protected virtual Quaternion AverageRotation(Quaternion rot1, Quaternion rot2)
		{
			return Quaternion.Slerp(rot1, rot2, 0.5f);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0007FD84 File Offset: 0x0007DF84
		protected virtual void SetControllerListeners(GameObject controller, bool controllerState, ref bool subscribedState, bool forceDisabled = false)
		{
			if (controller != null)
			{
				bool toggle = !forceDisabled && controllerState;
				this.ToggleControllerListeners(controller, toggle, ref subscribedState);
			}
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0007FDAC File Offset: 0x0007DFAC
		protected virtual void ToggleControllerListeners(GameObject controller, bool toggle, ref bool subscribed)
		{
			VRTK_ControllerEvents component = controller.GetComponent<VRTK_ControllerEvents>();
			if (component != null)
			{
				if (this.engageButton != this.previousEngageButton & subscribed)
				{
					component.UnsubscribeToButtonAliasEvent(this.previousEngageButton, true, new ControllerInteractionEventHandler(this.EngageButtonPressed));
					component.UnsubscribeToButtonAliasEvent(this.previousEngageButton, false, new ControllerInteractionEventHandler(this.EngageButtonReleased));
					subscribed = false;
				}
				if (toggle && !subscribed)
				{
					component.SubscribeToButtonAliasEvent(this.engageButton, true, new ControllerInteractionEventHandler(this.EngageButtonPressed));
					component.SubscribeToButtonAliasEvent(this.engageButton, false, new ControllerInteractionEventHandler(this.EngageButtonReleased));
					subscribed = true;
					return;
				}
				if (!toggle & subscribed)
				{
					component.UnsubscribeToButtonAliasEvent(this.engageButton, true, new ControllerInteractionEventHandler(this.EngageButtonPressed));
					component.UnsubscribeToButtonAliasEvent(this.engageButton, false, new ControllerInteractionEventHandler(this.EngageButtonReleased));
					subscribed = false;
				}
			}
		}

		// Token: 0x04001363 RID: 4963
		[Header("Control Settings")]
		[Tooltip("If this is checked then the left controller touchpad will be enabled to move the play area.")]
		public bool leftController = true;

		// Token: 0x04001364 RID: 4964
		[Tooltip("If this is checked then the right controller touchpad will be enabled to move the play area.")]
		public bool rightController = true;

		// Token: 0x04001365 RID: 4965
		[Tooltip("Select which button to hold to engage Move In Place.")]
		public VRTK_ControllerEvents.ButtonAlias engageButton = VRTK_ControllerEvents.ButtonAlias.TouchpadPress;

		// Token: 0x04001366 RID: 4966
		[Tooltip("Select which trackables are used to determine movement.")]
		public VRTK_MoveInPlace.ControlOptions controlOptions;

		// Token: 0x04001367 RID: 4967
		[Tooltip("How the user's movement direction will be determined.  The Gaze method tends to lead to the least motion sickness.  Smart decoupling is still a Work In Progress.")]
		public VRTK_MoveInPlace.DirectionalMethod directionMethod;

		// Token: 0x04001368 RID: 4968
		[Header("Speed Settings")]
		[Tooltip("Lower to decrease speed, raise to increase.")]
		public float speedScale = 1f;

		// Token: 0x04001369 RID: 4969
		[Tooltip("The max speed the user can move in game units. (If 0 or less, max speed is uncapped)")]
		public float maxSpeed = 4f;

		// Token: 0x0400136A RID: 4970
		[Tooltip("The speed in which the play area slows down to a complete stop when the user is no longer pressing the engage button. This deceleration effect can ease any motion sickness that may be suffered.")]
		public float deceleration = 0.1f;

		// Token: 0x0400136B RID: 4971
		[Tooltip("The speed in which the play area slows down to a complete stop when the user is falling.")]
		public float fallingDeceleration = 0.01f;

		// Token: 0x0400136C RID: 4972
		[Header("Advanced Settings")]
		[Tooltip("The degree threshold that all tracked objects (controllers, headset) must be within to change direction when using the Smart Decoupling Direction Method.")]
		public float smartDecoupleThreshold = 30f;

		// Token: 0x0400136D RID: 4973
		[Tooltip("The maximum amount of movement required to register in the virtual world.  Decreasing this will increase acceleration, and vice versa.")]
		public float sensitivity = 0.02f;

		// Token: 0x0400136E RID: 4974
		[Header("Custom Settings")]
		[Tooltip("An optional Body Physics script to check for potential collisions in the moving direction. If any potential collision is found then the move will not take place. This can help reduce collision tunnelling.")]
		public VRTK_BodyPhysics bodyPhysics;

		// Token: 0x0400136F RID: 4975
		protected Transform playArea;

		// Token: 0x04001370 RID: 4976
		protected GameObject controllerLeftHand;

		// Token: 0x04001371 RID: 4977
		protected GameObject controllerRightHand;

		// Token: 0x04001372 RID: 4978
		protected VRTK_ControllerReference engagedController;

		// Token: 0x04001373 RID: 4979
		protected Transform headset;

		// Token: 0x04001374 RID: 4980
		protected bool leftSubscribed;

		// Token: 0x04001375 RID: 4981
		protected bool rightSubscribed;

		// Token: 0x04001376 RID: 4982
		protected bool previousLeftControllerState;

		// Token: 0x04001377 RID: 4983
		protected bool previousRightControllerState;

		// Token: 0x04001378 RID: 4984
		protected VRTK_ControllerEvents.ButtonAlias previousEngageButton;

		// Token: 0x04001379 RID: 4985
		protected bool currentlyFalling;

		// Token: 0x0400137A RID: 4986
		protected int averagePeriod;

		// Token: 0x0400137B RID: 4987
		protected List<Transform> trackedObjects;

		// Token: 0x0400137C RID: 4988
		protected Dictionary<Transform, List<float>> movementList;

		// Token: 0x0400137D RID: 4989
		protected Dictionary<Transform, float> previousYPositions;

		// Token: 0x0400137E RID: 4990
		protected Vector3 initalGaze;

		// Token: 0x0400137F RID: 4991
		protected float currentSpeed;

		// Token: 0x04001380 RID: 4992
		protected Vector3 direction;

		// Token: 0x04001381 RID: 4993
		protected Vector3 previousDirection;

		// Token: 0x04001382 RID: 4994
		protected bool active;

		// Token: 0x020005E7 RID: 1511
		public enum ControlOptions
		{
			// Token: 0x040027EB RID: 10219
			HeadsetAndControllers,
			// Token: 0x040027EC RID: 10220
			ControllersOnly,
			// Token: 0x040027ED RID: 10221
			HeadsetOnly
		}

		// Token: 0x020005E8 RID: 1512
		public enum DirectionalMethod
		{
			// Token: 0x040027EF RID: 10223
			Gaze,
			// Token: 0x040027F0 RID: 10224
			ControllerRotation,
			// Token: 0x040027F1 RID: 10225
			DumbDecoupling,
			// Token: 0x040027F2 RID: 10226
			SmartDecoupling,
			// Token: 0x040027F3 RID: 10227
			EngageControllerRotationOnly,
			// Token: 0x040027F4 RID: 10228
			LeftControllerRotationOnly,
			// Token: 0x040027F5 RID: 10229
			RightControllerRotationOnly
		}
	}
}
