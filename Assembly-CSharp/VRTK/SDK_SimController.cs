using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200027A RID: 634
	[SDK_Description(typeof(SDK_SimSystem), 0)]
	public class SDK_SimController : SDK_BaseController
	{
		// Token: 0x060012DE RID: 4830 RVA: 0x0006A8A2 File Offset: 0x00068AA2
		public virtual void SetKeyMappings(Dictionary<string, KeyCode> givenKeyMappings)
		{
			this.keyMappings = givenKeyMappings;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ProcessUpdate(VRTK_ControllerReference controllerReference, Dictionary<string, object> options)
		{
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ProcessFixedUpdate(VRTK_ControllerReference controllerReference, Dictionary<string, object> options)
		{
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00004EB8 File Offset: 0x000030B8
		public override SDK_BaseController.ControllerType GetCurrentControllerType()
		{
			return SDK_BaseController.ControllerType.Simulator_Hand;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0006A8AB File Offset: 0x00068AAB
		public override string GetControllerDefaultColliderPath(SDK_BaseController.ControllerHand hand)
		{
			return "ControllerColliders/Simulator";
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0006A8B4 File Offset: 0x00068AB4
		public override string GetControllerElementPath(SDK_BaseController.ControllerElements element, SDK_BaseController.ControllerHand hand, bool fullPath = false)
		{
			string text = fullPath ? "/attach" : "";
			switch (element)
			{
			case SDK_BaseController.ControllerElements.AttachPoint:
				return "";
			case SDK_BaseController.ControllerElements.Trigger:
				return text ?? "";
			case SDK_BaseController.ControllerElements.GripLeft:
				return text ?? "";
			case SDK_BaseController.ControllerElements.GripRight:
				return text ?? "";
			case SDK_BaseController.ControllerElements.Touchpad:
				return text ?? "";
			case SDK_BaseController.ControllerElements.ButtonOne:
				return text ?? "";
			case SDK_BaseController.ControllerElements.SystemMenu:
				return text ?? "";
			case SDK_BaseController.ControllerElements.Body:
				return "";
			}
			return null;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0006A94C File Offset: 0x00068B4C
		public override uint GetControllerIndex(GameObject controller)
		{
			if (this.CheckActualOrScriptAliasControllerIsRightHand(controller))
			{
				return 1U;
			}
			if (this.CheckActualOrScriptAliasControllerIsLeftHand(controller))
			{
				return 2U;
			}
			return uint.MaxValue;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0006A968 File Offset: 0x00068B68
		public override GameObject GetControllerByIndex(uint index, bool actual = false)
		{
			this.SetupPlayer();
			VRTK_SDKManager instance = VRTK_SDKManager.instance;
			if (index != 1U)
			{
				if (index != 2U)
				{
					return null;
				}
				if (instance != null && !actual)
				{
					return instance.scriptAliasLeftController;
				}
				if (!(this.leftController != null))
				{
					return null;
				}
				return this.leftController.gameObject;
			}
			else
			{
				if (instance != null && !actual)
				{
					return instance.scriptAliasRightController;
				}
				if (!(this.rightController != null))
				{
					return null;
				}
				return this.rightController.gameObject;
			}
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0006A9EA File Offset: 0x00068BEA
		public override Transform GetControllerOrigin(VRTK_ControllerReference controllerReference)
		{
			if (controllerReference == null || !(controllerReference.actual != null))
			{
				return null;
			}
			return controllerReference.actual.transform;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0006949E File Offset: 0x0006769E
		public override Transform GenerateControllerPointerOrigin(GameObject parent)
		{
			return null;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0006AA0C File Offset: 0x00068C0C
		public override GameObject GetControllerLeftHand(bool actual = false)
		{
			GameObject gameObject = this.GetSDKManagerControllerLeftHand(actual);
			if (gameObject == null && actual)
			{
				gameObject = this.GetActualController(SDK_BaseController.ControllerHand.Left);
			}
			return gameObject;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0006AA38 File Offset: 0x00068C38
		public override GameObject GetControllerRightHand(bool actual = false)
		{
			GameObject gameObject = this.GetSDKManagerControllerRightHand(actual);
			if (gameObject == null && actual)
			{
				gameObject = this.GetActualController(SDK_BaseController.ControllerHand.Right);
			}
			return gameObject;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0006AA61 File Offset: 0x00068C61
		public override bool IsControllerLeftHand(GameObject controller)
		{
			return this.CheckActualOrScriptAliasControllerIsLeftHand(controller);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0006AA6A File Offset: 0x00068C6A
		public override bool IsControllerRightHand(GameObject controller)
		{
			return this.CheckActualOrScriptAliasControllerIsRightHand(controller);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0006AA73 File Offset: 0x00068C73
		public override bool IsControllerLeftHand(GameObject controller, bool actual)
		{
			return this.CheckControllerLeftHand(controller, actual);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0006AA7D File Offset: 0x00068C7D
		public override bool IsControllerRightHand(GameObject controller, bool actual)
		{
			return this.CheckControllerRightHand(controller, actual);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0006AA87 File Offset: 0x00068C87
		public override GameObject GetControllerModel(GameObject controller)
		{
			return this.GetControllerModelFromController(controller);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0006AA90 File Offset: 0x00068C90
		public override GameObject GetControllerModel(SDK_BaseController.ControllerHand hand)
		{
			GameObject result = null;
			GameObject gameObject = SDK_InputSimulator.FindInScene();
			if (gameObject != null)
			{
				if (hand != SDK_BaseController.ControllerHand.Left)
				{
					if (hand == SDK_BaseController.ControllerHand.Right)
					{
						result = gameObject.transform.Find(string.Format("{0}/Hand", "RightHand")).gameObject;
					}
				}
				else
				{
					result = gameObject.transform.Find(string.Format("{0}/Hand", "LeftHand")).gameObject;
				}
			}
			return result;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0006AAFB File Offset: 0x00068CFB
		public override GameObject GetControllerRenderModel(VRTK_ControllerReference controllerReference)
		{
			return controllerReference.scriptAlias.transform.parent.Find("Hand").gameObject;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00003F60 File Offset: 0x00002160
		public override void SetControllerRenderModelWheel(GameObject renderModel, bool state)
		{
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00003F60 File Offset: 0x00002160
		public override void HapticPulse(VRTK_ControllerReference controllerReference, float strength = 0.5f)
		{
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000694B6 File Offset: 0x000676B6
		public override bool HapticPulse(VRTK_ControllerReference controllerReference, AudioClip clip)
		{
			return true;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000694B9 File Offset: 0x000676B9
		public override SDK_ControllerHapticModifiers GetHapticModifiers()
		{
			return new SDK_ControllerHapticModifiers();
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0006AB1C File Offset: 0x00068D1C
		public override Vector3 GetVelocity(VRTK_ControllerReference controllerReference)
		{
			this.SetupPlayer();
			uint realIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			if (realIndex != 1U)
			{
				if (realIndex != 2U)
				{
					return Vector3.zero;
				}
				if (!(this.leftController != null))
				{
					return Vector3.zero;
				}
				return this.leftController.GetVelocity();
			}
			else
			{
				if (!(this.rightController != null))
				{
					return Vector3.zero;
				}
				return this.rightController.GetVelocity();
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0006AB88 File Offset: 0x00068D88
		public override Vector3 GetAngularVelocity(VRTK_ControllerReference controllerReference)
		{
			this.SetupPlayer();
			uint realIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			if (realIndex != 1U)
			{
				if (realIndex != 2U)
				{
					return Vector3.zero;
				}
				if (!(this.leftController != null))
				{
					return Vector3.zero;
				}
				return this.leftController.GetAngularVelocity();
			}
			else
			{
				if (!(this.rightController != null))
				{
					return Vector3.zero;
				}
				return this.rightController.GetAngularVelocity();
			}
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000694C7 File Offset: 0x000676C7
		public override bool IsTouchpadStatic(bool isTouched, Vector2 currentAxisValues, Vector2 previousAxisValues, int compareFidelity)
		{
			return !isTouched || VRTK_SharedMethods.Vector2ShallowCompare(currentAxisValues, previousAxisValues, compareFidelity);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000694D7 File Offset: 0x000676D7
		public override Vector2 GetButtonAxis(SDK_BaseController.ButtonTypes buttonType, VRTK_ControllerReference controllerReference)
		{
			return Vector2.zero;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000694A1 File Offset: 0x000676A1
		public override float GetButtonHairlineDelta(SDK_BaseController.ButtonTypes buttonType, VRTK_ControllerReference controllerReference)
		{
			return 0f;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0006ABF4 File Offset: 0x00068DF4
		public override bool GetControllerButtonState(SDK_BaseController.ButtonTypes buttonType, SDK_BaseController.ButtonPressTypes pressType, VRTK_ControllerReference controllerReference)
		{
			uint realIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			switch (buttonType)
			{
			case SDK_BaseController.ButtonTypes.ButtonOne:
				return this.GetControllerButtonState(realIndex, "ButtonOne", pressType);
			case SDK_BaseController.ButtonTypes.ButtonTwo:
				return this.GetControllerButtonState(realIndex, "ButtonTwo", pressType);
			case SDK_BaseController.ButtonTypes.Grip:
			case SDK_BaseController.ButtonTypes.GripHairline:
				return this.GetControllerButtonState(realIndex, "Grip", pressType);
			case SDK_BaseController.ButtonTypes.StartMenu:
				return this.GetControllerButtonState(realIndex, "StartMenu", pressType);
			case SDK_BaseController.ButtonTypes.Trigger:
			case SDK_BaseController.ButtonTypes.TriggerHairline:
				return this.GetControllerButtonState(realIndex, "Trigger", pressType);
			case SDK_BaseController.ButtonTypes.Touchpad:
				return this.GetControllerButtonState(realIndex, "TouchpadPress", pressType);
			default:
				return false;
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0006AC85 File Offset: 0x00068E85
		protected virtual void OnEnable()
		{
			this.SetupPlayer();
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0006AC90 File Offset: 0x00068E90
		protected virtual void SetupPlayer()
		{
			if (this.rightController == null || this.leftController == null)
			{
				GameObject gameObject = SDK_InputSimulator.FindInScene();
				if (gameObject != null)
				{
					this.rightController = ((this.rightController == null) ? gameObject.transform.Find("RightHand").GetComponent<SDK_ControllerSim>() : this.rightController);
					this.leftController = ((this.leftController == null) ? gameObject.transform.Find("LeftHand").GetComponent<SDK_ControllerSim>() : this.leftController);
				}
			}
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0006AD2A File Offset: 0x00068F2A
		protected virtual bool IsTouchModifierPressed()
		{
			return Input.GetKey(this.keyMappings["TouchModifier"]);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0006AD41 File Offset: 0x00068F41
		protected virtual bool IsHairTouchModifierPressed()
		{
			return Input.GetKey(this.keyMappings["HairTouchModifier"]);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0006AD58 File Offset: 0x00068F58
		protected virtual bool IsButtonPressIgnored()
		{
			return this.IsHairTouchModifierPressed() || this.IsTouchModifierPressed();
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0006AD6A File Offset: 0x00068F6A
		protected virtual bool IsButtonHairTouchIgnored()
		{
			return this.IsTouchModifierPressed() && !this.IsHairTouchModifierPressed();
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0006AD80 File Offset: 0x00068F80
		protected virtual bool GetControllerButtonState(uint index, string keyMapping, SDK_BaseController.ButtonPressTypes pressType)
		{
			if (pressType == SDK_BaseController.ButtonPressTypes.Touch)
			{
				return this.IsButtonPressed(index, SDK_BaseController.ButtonPressTypes.Press, this.keyMappings[keyMapping]);
			}
			if (pressType == SDK_BaseController.ButtonPressTypes.TouchDown)
			{
				return this.IsButtonPressed(index, SDK_BaseController.ButtonPressTypes.PressDown, this.keyMappings[keyMapping]);
			}
			if (pressType == SDK_BaseController.ButtonPressTypes.TouchUp)
			{
				return this.IsButtonPressed(index, SDK_BaseController.ButtonPressTypes.PressUp, this.keyMappings[keyMapping]);
			}
			if (pressType == SDK_BaseController.ButtonPressTypes.Press)
			{
				return !this.IsButtonPressIgnored() && this.IsButtonPressed(index, SDK_BaseController.ButtonPressTypes.Press, this.keyMappings[keyMapping]);
			}
			if (pressType == SDK_BaseController.ButtonPressTypes.PressDown)
			{
				return !this.IsButtonPressIgnored() && this.IsButtonPressed(index, SDK_BaseController.ButtonPressTypes.PressDown, this.keyMappings[keyMapping]);
			}
			return pressType == SDK_BaseController.ButtonPressTypes.PressUp && !this.IsButtonPressIgnored() && this.IsButtonPressed(index, SDK_BaseController.ButtonPressTypes.PressUp, this.keyMappings[keyMapping]);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0006AE44 File Offset: 0x00069044
		protected virtual bool IsButtonPressed(uint index, SDK_BaseController.ButtonPressTypes type, KeyCode button)
		{
			this.SetupPlayer();
			if (index >= 4294967295U)
			{
				return false;
			}
			if (index == 1U)
			{
				if (!this.rightController.Selected)
				{
					return false;
				}
			}
			else
			{
				if (index != 2U)
				{
					return false;
				}
				if (!this.leftController.Selected)
				{
					return false;
				}
			}
			switch (type)
			{
			case SDK_BaseController.ButtonPressTypes.Press:
				return Input.GetKey(button);
			case SDK_BaseController.ButtonPressTypes.PressDown:
				return Input.GetKeyDown(button);
			case SDK_BaseController.ButtonPressTypes.PressUp:
				return Input.GetKeyUp(button);
			default:
				return false;
			}
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0006AEB0 File Offset: 0x000690B0
		protected virtual GameObject GetActualController(SDK_BaseController.ControllerHand hand)
		{
			GameObject gameObject = SDK_InputSimulator.FindInScene();
			GameObject result = null;
			if (gameObject != null)
			{
				if (hand != SDK_BaseController.ControllerHand.Left)
				{
					if (hand == SDK_BaseController.ControllerHand.Right)
					{
						result = gameObject.transform.Find("RightHand").gameObject;
					}
				}
				else
				{
					result = gameObject.transform.Find("LeftHand").gameObject;
				}
			}
			return result;
		}

		// Token: 0x040010E7 RID: 4327
		protected SDK_ControllerSim rightController;

		// Token: 0x040010E8 RID: 4328
		protected SDK_ControllerSim leftController;

		// Token: 0x040010E9 RID: 4329
		protected Dictionary<string, KeyCode> keyMappings = new Dictionary<string, KeyCode>
		{
			{
				"Trigger",
				KeyCode.Mouse1
			},
			{
				"Grip",
				KeyCode.Mouse0
			},
			{
				"TouchpadPress",
				KeyCode.Q
			},
			{
				"ButtonOne",
				KeyCode.E
			},
			{
				"ButtonTwo",
				KeyCode.R
			},
			{
				"StartMenu",
				KeyCode.F
			},
			{
				"TouchModifier",
				KeyCode.T
			},
			{
				"HairTouchModifier",
				KeyCode.H
			}
		};

		// Token: 0x040010EA RID: 4330
		protected const string RIGHT_HAND_CONTROLLER_NAME = "RightHand";

		// Token: 0x040010EB RID: 4331
		protected const string LEFT_HAND_CONTROLLER_NAME = "LeftHand";
	}
}
