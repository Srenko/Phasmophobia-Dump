using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

namespace VRTK
{
	// Token: 0x0200027E RID: 638
	[SDK_Description(typeof(SDK_SteamVRSystem), 0)]
	public class SDK_SteamVRController : SDK_BaseController
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ProcessUpdate(VRTK_ControllerReference controllerReference, Dictionary<string, object> options)
		{
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ProcessFixedUpdate(VRTK_ControllerReference controllerReference, Dictionary<string, object> options)
		{
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0006B3E0 File Offset: 0x000695E0
		public override SDK_BaseController.ControllerType GetCurrentControllerType()
		{
			VRTK_DeviceFinder.Headsets headsetType = VRTK_DeviceFinder.GetHeadsetType(true);
			if (headsetType == VRTK_DeviceFinder.Headsets.OculusRift)
			{
				return SDK_BaseController.ControllerType.SteamVR_OculusTouch;
			}
			if (headsetType == VRTK_DeviceFinder.Headsets.Vive)
			{
				return SDK_BaseController.ControllerType.SteamVR_ViveWand;
			}
			return SDK_BaseController.ControllerType.Custom;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0006B404 File Offset: 0x00069604
		public override string GetControllerDefaultColliderPath(SDK_BaseController.ControllerHand hand)
		{
			string result = "ControllerColliders/Fallback";
			VRTK_DeviceFinder.Headsets headsetType = VRTK_DeviceFinder.GetHeadsetType(true);
			if (headsetType != VRTK_DeviceFinder.Headsets.OculusRift)
			{
				if (headsetType == VRTK_DeviceFinder.Headsets.Vive)
				{
					result = "ControllerColliders/HTCVive";
				}
			}
			else
			{
				result = ((hand == SDK_BaseController.ControllerHand.Left) ? "ControllerColliders/SteamVROculusTouch_Left" : "ControllerColliders/SteamVROculusTouch_Right");
			}
			return result;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0006B444 File Offset: 0x00069644
		public override string GetControllerElementPath(SDK_BaseController.ControllerElements element, SDK_BaseController.ControllerHand hand, bool fullPath = false)
		{
			string text = fullPath ? "/attach" : "";
			switch (element)
			{
			case SDK_BaseController.ControllerElements.AttachPoint:
				return "tip/attach";
			case SDK_BaseController.ControllerElements.Trigger:
				return "trigger" + text;
			case SDK_BaseController.ControllerElements.GripLeft:
				return this.GetControllerGripPath(hand, text, SDK_BaseController.ControllerHand.Left);
			case SDK_BaseController.ControllerElements.GripRight:
				return this.GetControllerGripPath(hand, text, SDK_BaseController.ControllerHand.Right);
			case SDK_BaseController.ControllerElements.Touchpad:
				return this.GetControllerTouchpadPath(hand, text);
			case SDK_BaseController.ControllerElements.ButtonOne:
				return this.GetControllerButtonOnePath(hand, text);
			case SDK_BaseController.ControllerElements.ButtonTwo:
				return this.GetControllerButtonTwoPath(hand, text);
			case SDK_BaseController.ControllerElements.SystemMenu:
				return this.GetControllerSystemMenuPath(hand, text);
			case SDK_BaseController.ControllerElements.Body:
				return "body";
			case SDK_BaseController.ControllerElements.StartMenu:
				return this.GetControllerStartMenuPath(hand, text);
			default:
				return null;
			}
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0006B4EC File Offset: 0x000696EC
		public override uint GetControllerIndex(GameObject controller)
		{
			SteamVR_TrackedObject trackedObject = this.GetTrackedObject(controller);
			if (!(trackedObject != null))
			{
				return uint.MaxValue;
			}
			return (uint)trackedObject.index;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0006B514 File Offset: 0x00069714
		public override GameObject GetControllerByIndex(uint index, bool actual = false)
		{
			this.SetTrackedControllerCaches(false);
			if (index < 4294967295U)
			{
				VRTK_SDKManager instance = VRTK_SDKManager.instance;
				if (instance != null)
				{
					if (this.cachedLeftTrackedObject != null && this.cachedLeftTrackedObject.index == (SteamVR_TrackedObject.EIndex)index)
					{
						if (!actual)
						{
							return instance.scriptAliasLeftController;
						}
						return instance.loadedSetup.actualLeftController;
					}
					else if (this.cachedRightTrackedObject != null && this.cachedRightTrackedObject.index == (SteamVR_TrackedObject.EIndex)index)
					{
						if (!actual)
						{
							return instance.scriptAliasRightController;
						}
						return instance.loadedSetup.actualRightController;
					}
				}
				if (this.cachedTrackedObjectsByIndex.ContainsKey(index) && this.cachedTrackedObjectsByIndex[index] != null)
				{
					return this.cachedTrackedObjectsByIndex[index].gameObject;
				}
			}
			return null;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0006B5D8 File Offset: 0x000697D8
		public override Transform GetControllerOrigin(VRTK_ControllerReference controllerReference)
		{
			SteamVR_TrackedObject trackedObject = this.GetTrackedObject(controllerReference.actual);
			if (!(trackedObject != null))
			{
				return null;
			}
			if (!(trackedObject.origin != null))
			{
				return trackedObject.transform.parent;
			}
			return trackedObject.origin;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0006B620 File Offset: 0x00069820
		public override Transform GenerateControllerPointerOrigin(GameObject parent)
		{
			VRTK_DeviceFinder.Headsets headsetType = VRTK_DeviceFinder.GetHeadsetType(true);
			if (headsetType == VRTK_DeviceFinder.Headsets.OculusRift && (this.IsControllerLeftHand(parent) || this.IsControllerRightHand(parent)))
			{
				GameObject gameObject = new GameObject(parent.name + " _CustomPointerOrigin");
				gameObject.transform.SetParent(parent.transform);
				gameObject.transform.localEulerAngles = new Vector3(40f, 0f, 0f);
				gameObject.transform.localPosition = new Vector3(this.IsControllerLeftHand(parent) ? 0.0081f : -0.0081f, -0.0273f, -0.0311f);
				return gameObject.transform;
			}
			return null;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0006B6C8 File Offset: 0x000698C8
		public override GameObject GetControllerLeftHand(bool actual = false)
		{
			GameObject gameObject = this.GetSDKManagerControllerLeftHand(actual);
			if (gameObject == null && actual)
			{
				gameObject = VRTK_SharedMethods.FindEvenInactiveGameObject<SteamVR_ControllerManager>("Controller (left)");
			}
			return gameObject;
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0006B6F4 File Offset: 0x000698F4
		public override GameObject GetControllerRightHand(bool actual = false)
		{
			GameObject gameObject = this.GetSDKManagerControllerRightHand(actual);
			if (gameObject == null && actual)
			{
				gameObject = VRTK_SharedMethods.FindEvenInactiveGameObject<SteamVR_ControllerManager>("Controller (right)");
			}
			return gameObject;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0006AA61 File Offset: 0x00068C61
		public override bool IsControllerLeftHand(GameObject controller)
		{
			return this.CheckActualOrScriptAliasControllerIsLeftHand(controller);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0006AA6A File Offset: 0x00068C6A
		public override bool IsControllerRightHand(GameObject controller)
		{
			return this.CheckActualOrScriptAliasControllerIsRightHand(controller);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0006AA73 File Offset: 0x00068C73
		public override bool IsControllerLeftHand(GameObject controller, bool actual)
		{
			return this.CheckControllerLeftHand(controller, actual);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0006AA7D File Offset: 0x00068C7D
		public override bool IsControllerRightHand(GameObject controller, bool actual)
		{
			return this.CheckControllerRightHand(controller, actual);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0006AA87 File Offset: 0x00068C87
		public override GameObject GetControllerModel(GameObject controller)
		{
			return this.GetControllerModelFromController(controller);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0006B720 File Offset: 0x00069920
		public override GameObject GetControllerModel(SDK_BaseController.ControllerHand hand)
		{
			GameObject gameObject = this.GetSDKManagerControllerModelForHand(hand);
			if (gameObject == null)
			{
				GameObject gameObject2 = null;
				if (hand != SDK_BaseController.ControllerHand.Left)
				{
					if (hand == SDK_BaseController.ControllerHand.Right)
					{
						gameObject2 = this.GetControllerRightHand(true);
					}
				}
				else
				{
					gameObject2 = this.GetControllerLeftHand(true);
				}
				if (gameObject2 != null)
				{
					gameObject = gameObject2.transform.Find("Model").gameObject;
				}
			}
			return gameObject;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0006B77C File Offset: 0x0006997C
		public override GameObject GetControllerRenderModel(VRTK_ControllerReference controllerReference)
		{
			SteamVR_RenderModel componentInChildren = controllerReference.actual.GetComponentInChildren<SteamVR_RenderModel>();
			if (!(componentInChildren != null))
			{
				return null;
			}
			return componentInChildren.gameObject;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0006B7A8 File Offset: 0x000699A8
		public override void SetControllerRenderModelWheel(GameObject renderModel, bool state)
		{
			SteamVR_RenderModel component = renderModel.GetComponent<SteamVR_RenderModel>();
			if (component != null)
			{
				component.controllerModeState.bScrollWheelVisible = state;
			}
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0006B7D4 File Offset: 0x000699D4
		public override void HapticPulse(VRTK_ControllerReference controllerReference, float strength = 0.5f)
		{
			uint realIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			if (realIndex < 4294967295U)
			{
				float num = (float)this.maxHapticVibration * strength;
				SteamVR_Controller.Input((int)realIndex).TriggerHapticPulse((ushort)num, EVRButtonId.k_EButton_Axis0);
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool HapticPulse(VRTK_ControllerReference controllerReference, AudioClip clip)
		{
			return false;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0006B805 File Offset: 0x00069A05
		public override SDK_ControllerHapticModifiers GetHapticModifiers()
		{
			return new SDK_ControllerHapticModifiers
			{
				maxHapticVibration = this.maxHapticVibration
			};
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0006B818 File Offset: 0x00069A18
		public override Vector3 GetVelocity(VRTK_ControllerReference controllerReference)
		{
			uint realIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			if (realIndex <= 0U || realIndex >= 4294967295U)
			{
				return Vector3.zero;
			}
			return SteamVR_Controller.Input((int)realIndex).velocity;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0006B848 File Offset: 0x00069A48
		public override Vector3 GetAngularVelocity(VRTK_ControllerReference controllerReference)
		{
			uint realIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			if (realIndex <= 0U || realIndex >= 4294967295U)
			{
				return Vector3.zero;
			}
			return SteamVR_Controller.Input((int)realIndex).angularVelocity;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000694C7 File Offset: 0x000676C7
		public override bool IsTouchpadStatic(bool isTouched, Vector2 currentAxisValues, Vector2 previousAxisValues, int compareFidelity)
		{
			return !isTouched || VRTK_SharedMethods.Vector2ShallowCompare(currentAxisValues, previousAxisValues, compareFidelity);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0006B878 File Offset: 0x00069A78
		public override Vector2 GetButtonAxis(SDK_BaseController.ButtonTypes buttonType, VRTK_ControllerReference controllerReference)
		{
			uint realIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			if (realIndex >= 4294967295U)
			{
				return Vector2.zero;
			}
			SteamVR_Controller.Device device = SteamVR_Controller.Input((int)realIndex);
			if (buttonType == SDK_BaseController.ButtonTypes.Grip)
			{
				return device.GetAxis(EVRButtonId.k_EButton_Axis2);
			}
			if (buttonType == SDK_BaseController.ButtonTypes.Trigger)
			{
				return device.GetAxis(EVRButtonId.k_EButton_Axis1);
			}
			if (buttonType == SDK_BaseController.ButtonTypes.Touchpad)
			{
				return device.GetAxis(EVRButtonId.k_EButton_Axis0);
			}
			return Vector2.zero;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0006B8CC File Offset: 0x00069ACC
		public override float GetButtonHairlineDelta(SDK_BaseController.ButtonTypes buttonType, VRTK_ControllerReference controllerReference)
		{
			uint realIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			if (realIndex >= 4294967295U)
			{
				return 0f;
			}
			SteamVR_Controller.Device device = SteamVR_Controller.Input((int)realIndex);
			if (buttonType != SDK_BaseController.ButtonTypes.Trigger && buttonType != SDK_BaseController.ButtonTypes.TriggerHairline)
			{
				return 0f;
			}
			return device.hairTriggerDelta;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0006B908 File Offset: 0x00069B08
		public override bool GetControllerButtonState(SDK_BaseController.ButtonTypes buttonType, SDK_BaseController.ButtonPressTypes pressType, VRTK_ControllerReference controllerReference)
		{
			uint realIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			if (realIndex >= 4294967295U)
			{
				return false;
			}
			switch (buttonType)
			{
			case SDK_BaseController.ButtonTypes.ButtonOne:
				return this.IsButtonPressed(realIndex, pressType, 128UL);
			case SDK_BaseController.ButtonTypes.ButtonTwo:
				return this.IsButtonPressed(realIndex, pressType, 2UL);
			case SDK_BaseController.ButtonTypes.Grip:
				return this.IsButtonPressed(realIndex, pressType, 4UL);
			case SDK_BaseController.ButtonTypes.StartMenu:
				return this.IsButtonPressed(realIndex, pressType, 1UL);
			case SDK_BaseController.ButtonTypes.Trigger:
				return this.IsButtonPressed(realIndex, pressType, 8589934592UL);
			case SDK_BaseController.ButtonTypes.TriggerHairline:
				if (pressType == SDK_BaseController.ButtonPressTypes.PressDown)
				{
					return SteamVR_Controller.Input((int)realIndex).GetHairTriggerDown();
				}
				if (pressType == SDK_BaseController.ButtonPressTypes.PressUp)
				{
					return SteamVR_Controller.Input((int)realIndex).GetHairTriggerUp();
				}
				break;
			case SDK_BaseController.ButtonTypes.Touchpad:
				return this.IsButtonPressed(realIndex, pressType, 4294967296UL);
			}
			return false;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0006B9BF File Offset: 0x00069BBF
		protected virtual void Awake()
		{
			SteamVR_Events.System(EVREventType.VREvent_TrackedDeviceRoleChanged).Listen(new UnityAction<VREvent_t>(this.OnTrackedDeviceRoleChanged<VREvent_t>));
			this.SetTrackedControllerCaches(true);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0006B9E1 File Offset: 0x00069BE1
		protected virtual void OnTrackedDeviceRoleChanged<T>(T ignoredArgument)
		{
			this.SetTrackedControllerCaches(true);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0006B9EC File Offset: 0x00069BEC
		protected virtual void SetTrackedControllerCaches(bool forceRefresh = false)
		{
			if (forceRefresh)
			{
				this.cachedLeftTrackedObject = null;
				this.cachedRightTrackedObject = null;
				this.cachedTrackedObjectsByGameObject.Clear();
				this.cachedTrackedObjectsByIndex.Clear();
			}
			VRTK_SDKManager instance = VRTK_SDKManager.instance;
			if (instance != null)
			{
				if (this.cachedLeftTrackedObject == null && instance.loadedSetup.actualLeftController)
				{
					this.cachedLeftTrackedObject = instance.loadedSetup.actualLeftController.GetComponent<SteamVR_TrackedObject>();
				}
				if (this.cachedRightTrackedObject == null && instance.loadedSetup.actualRightController)
				{
					this.cachedRightTrackedObject = instance.loadedSetup.actualRightController.GetComponent<SteamVR_TrackedObject>();
				}
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0006BA9C File Offset: 0x00069C9C
		protected virtual SteamVR_TrackedObject GetTrackedObject(GameObject controller)
		{
			this.SetTrackedControllerCaches(false);
			if (this.IsControllerLeftHand(controller))
			{
				return this.cachedLeftTrackedObject;
			}
			if (this.IsControllerRightHand(controller))
			{
				return this.cachedRightTrackedObject;
			}
			if (controller == null)
			{
				return null;
			}
			if (this.cachedTrackedObjectsByGameObject.ContainsKey(controller) && this.cachedTrackedObjectsByGameObject[controller] != null)
			{
				return this.cachedTrackedObjectsByGameObject[controller];
			}
			SteamVR_TrackedObject component = controller.GetComponent<SteamVR_TrackedObject>();
			if (component != null)
			{
				this.cachedTrackedObjectsByGameObject.Add(controller, component);
				this.cachedTrackedObjectsByIndex.Add((uint)component.index, component);
			}
			return component;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0006BB3C File Offset: 0x00069D3C
		protected virtual bool IsButtonPressed(uint index, SDK_BaseController.ButtonPressTypes type, ulong button)
		{
			if (index >= 4294967295U)
			{
				return false;
			}
			SteamVR_Controller.Device device = SteamVR_Controller.Input((int)index);
			switch (type)
			{
			case SDK_BaseController.ButtonPressTypes.Press:
				return device.GetPress(button);
			case SDK_BaseController.ButtonPressTypes.PressDown:
				return device.GetPressDown(button);
			case SDK_BaseController.ButtonPressTypes.PressUp:
				return device.GetPressUp(button);
			case SDK_BaseController.ButtonPressTypes.Touch:
				return device.GetTouch(button);
			case SDK_BaseController.ButtonPressTypes.TouchDown:
				return device.GetTouchDown(button);
			case SDK_BaseController.ButtonPressTypes.TouchUp:
				return device.GetTouchUp(button);
			default:
				return false;
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0006BBA8 File Offset: 0x00069DA8
		protected virtual string GetControllerGripPath(SDK_BaseController.ControllerHand hand, string suffix, SDK_BaseController.ControllerHand forceHand)
		{
			VRTK_DeviceFinder.Headsets headsetType = VRTK_DeviceFinder.GetHeadsetType(true);
			if (headsetType == VRTK_DeviceFinder.Headsets.OculusRift)
			{
				return "grip" + suffix;
			}
			if (headsetType == VRTK_DeviceFinder.Headsets.Vive)
			{
				return ((forceHand == SDK_BaseController.ControllerHand.Left) ? "lgrip" : "rgrip") + suffix;
			}
			return null;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0006BBE8 File Offset: 0x00069DE8
		protected virtual string GetControllerTouchpadPath(SDK_BaseController.ControllerHand hand, string suffix)
		{
			VRTK_DeviceFinder.Headsets headsetType = VRTK_DeviceFinder.GetHeadsetType(true);
			if (headsetType == VRTK_DeviceFinder.Headsets.OculusRift)
			{
				return "thumbstick" + suffix;
			}
			if (headsetType == VRTK_DeviceFinder.Headsets.Vive)
			{
				return "trackpad" + suffix;
			}
			return null;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0006BC20 File Offset: 0x00069E20
		protected virtual string GetControllerButtonOnePath(SDK_BaseController.ControllerHand hand, string suffix)
		{
			VRTK_DeviceFinder.Headsets headsetType = VRTK_DeviceFinder.GetHeadsetType(true);
			if (headsetType != VRTK_DeviceFinder.Headsets.OculusRift)
			{
				return null;
			}
			return ((hand == SDK_BaseController.ControllerHand.Left) ? "x_button" : "a_button") + suffix;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0006BC54 File Offset: 0x00069E54
		protected virtual string GetControllerButtonTwoPath(SDK_BaseController.ControllerHand hand, string suffix)
		{
			VRTK_DeviceFinder.Headsets headsetType = VRTK_DeviceFinder.GetHeadsetType(true);
			if (headsetType == VRTK_DeviceFinder.Headsets.OculusRift)
			{
				return ((hand == SDK_BaseController.ControllerHand.Left) ? "y_button" : "b_button") + suffix;
			}
			if (headsetType == VRTK_DeviceFinder.Headsets.Vive)
			{
				return "button" + suffix;
			}
			return null;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0006BC94 File Offset: 0x00069E94
		protected virtual string GetControllerSystemMenuPath(SDK_BaseController.ControllerHand hand, string suffix)
		{
			VRTK_DeviceFinder.Headsets headsetType = VRTK_DeviceFinder.GetHeadsetType(true);
			if (headsetType == VRTK_DeviceFinder.Headsets.OculusRift)
			{
				return ((hand == SDK_BaseController.ControllerHand.Left) ? "enter_button" : "home_button") + suffix;
			}
			if (headsetType == VRTK_DeviceFinder.Headsets.Vive)
			{
				return "sys_button" + suffix;
			}
			return null;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0006BCD4 File Offset: 0x00069ED4
		protected virtual string GetControllerStartMenuPath(SDK_BaseController.ControllerHand hand, string suffix)
		{
			VRTK_DeviceFinder.Headsets headsetType = VRTK_DeviceFinder.GetHeadsetType(true);
			if (headsetType != VRTK_DeviceFinder.Headsets.OculusRift)
			{
				return null;
			}
			return ((hand == SDK_BaseController.ControllerHand.Left) ? "enter_button" : "home_button") + suffix;
		}

		// Token: 0x040010F4 RID: 4340
		protected SteamVR_TrackedObject cachedLeftTrackedObject;

		// Token: 0x040010F5 RID: 4341
		protected SteamVR_TrackedObject cachedRightTrackedObject;

		// Token: 0x040010F6 RID: 4342
		protected Dictionary<GameObject, SteamVR_TrackedObject> cachedTrackedObjectsByGameObject = new Dictionary<GameObject, SteamVR_TrackedObject>();

		// Token: 0x040010F7 RID: 4343
		protected Dictionary<uint, SteamVR_TrackedObject> cachedTrackedObjectsByIndex = new Dictionary<uint, SteamVR_TrackedObject>();

		// Token: 0x040010F8 RID: 4344
		protected ushort maxHapticVibration = 3999;
	}
}
