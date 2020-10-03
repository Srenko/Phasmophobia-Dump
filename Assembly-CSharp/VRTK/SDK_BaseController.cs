using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000263 RID: 611
	public abstract class SDK_BaseController : SDK_Base
	{
		// Token: 0x0600123E RID: 4670
		public abstract void ProcessUpdate(VRTK_ControllerReference controllerReference, Dictionary<string, object> options);

		// Token: 0x0600123F RID: 4671
		public abstract void ProcessFixedUpdate(VRTK_ControllerReference controllerReference, Dictionary<string, object> options);

		// Token: 0x06001240 RID: 4672
		public abstract SDK_BaseController.ControllerType GetCurrentControllerType();

		// Token: 0x06001241 RID: 4673
		public abstract string GetControllerDefaultColliderPath(SDK_BaseController.ControllerHand hand);

		// Token: 0x06001242 RID: 4674
		public abstract string GetControllerElementPath(SDK_BaseController.ControllerElements element, SDK_BaseController.ControllerHand hand, bool fullPath = false);

		// Token: 0x06001243 RID: 4675
		public abstract uint GetControllerIndex(GameObject controller);

		// Token: 0x06001244 RID: 4676
		public abstract GameObject GetControllerByIndex(uint index, bool actual = false);

		// Token: 0x06001245 RID: 4677
		public abstract Transform GetControllerOrigin(VRTK_ControllerReference controllerReference);

		// Token: 0x06001246 RID: 4678
		public abstract Transform GenerateControllerPointerOrigin(GameObject parent);

		// Token: 0x06001247 RID: 4679
		public abstract GameObject GetControllerLeftHand(bool actual = false);

		// Token: 0x06001248 RID: 4680
		public abstract GameObject GetControllerRightHand(bool actual = false);

		// Token: 0x06001249 RID: 4681
		public abstract bool IsControllerLeftHand(GameObject controller);

		// Token: 0x0600124A RID: 4682
		public abstract bool IsControllerRightHand(GameObject controller);

		// Token: 0x0600124B RID: 4683
		public abstract bool IsControllerLeftHand(GameObject controller, bool actual);

		// Token: 0x0600124C RID: 4684
		public abstract bool IsControllerRightHand(GameObject controller, bool actual);

		// Token: 0x0600124D RID: 4685
		public abstract GameObject GetControllerModel(GameObject controller);

		// Token: 0x0600124E RID: 4686
		public abstract GameObject GetControllerModel(SDK_BaseController.ControllerHand hand);

		// Token: 0x0600124F RID: 4687 RVA: 0x00068F4C File Offset: 0x0006714C
		public virtual SDK_BaseController.ControllerHand GetControllerModelHand(GameObject controllerModel)
		{
			VRTK_SDKManager instance = VRTK_SDKManager.instance;
			if (instance != null && instance.loadedSetup != null)
			{
				if (controllerModel == instance.loadedSetup.modelAliasLeftController)
				{
					return SDK_BaseController.ControllerHand.Left;
				}
				if (controllerModel == instance.loadedSetup.modelAliasRightController)
				{
					return SDK_BaseController.ControllerHand.Right;
				}
			}
			return SDK_BaseController.ControllerHand.None;
		}

		// Token: 0x06001250 RID: 4688
		public abstract GameObject GetControllerRenderModel(VRTK_ControllerReference controllerReference);

		// Token: 0x06001251 RID: 4689
		public abstract void SetControllerRenderModelWheel(GameObject renderModel, bool state);

		// Token: 0x06001252 RID: 4690
		public abstract void HapticPulse(VRTK_ControllerReference controllerReference, float strength = 0.5f);

		// Token: 0x06001253 RID: 4691
		public abstract bool HapticPulse(VRTK_ControllerReference controllerReference, AudioClip clip);

		// Token: 0x06001254 RID: 4692
		public abstract SDK_ControllerHapticModifiers GetHapticModifiers();

		// Token: 0x06001255 RID: 4693
		public abstract Vector3 GetVelocity(VRTK_ControllerReference controllerReference);

		// Token: 0x06001256 RID: 4694
		public abstract Vector3 GetAngularVelocity(VRTK_ControllerReference controllerReference);

		// Token: 0x06001257 RID: 4695
		public abstract bool IsTouchpadStatic(bool isTouched, Vector2 currentAxisValues, Vector2 previousAxisValues, int compareFidelity);

		// Token: 0x06001258 RID: 4696
		public abstract Vector2 GetButtonAxis(SDK_BaseController.ButtonTypes buttonType, VRTK_ControllerReference controllerReference);

		// Token: 0x06001259 RID: 4697
		public abstract float GetButtonHairlineDelta(SDK_BaseController.ButtonTypes buttonType, VRTK_ControllerReference controllerReference);

		// Token: 0x0600125A RID: 4698
		public abstract bool GetControllerButtonState(SDK_BaseController.ButtonTypes buttonType, SDK_BaseController.ButtonPressTypes pressType, VRTK_ControllerReference controllerReference);

		// Token: 0x0600125B RID: 4699 RVA: 0x00068FA4 File Offset: 0x000671A4
		protected virtual GameObject GetSDKManagerControllerLeftHand(bool actual = false)
		{
			VRTK_SDKManager instance = VRTK_SDKManager.instance;
			if (!(instance != null))
			{
				return null;
			}
			if (!actual)
			{
				return instance.scriptAliasLeftController;
			}
			return instance.loadedSetup.actualLeftController;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00068FD8 File Offset: 0x000671D8
		protected virtual GameObject GetSDKManagerControllerRightHand(bool actual = false)
		{
			VRTK_SDKManager instance = VRTK_SDKManager.instance;
			if (!(instance != null))
			{
				return null;
			}
			if (!actual)
			{
				return instance.scriptAliasRightController;
			}
			return instance.loadedSetup.actualRightController;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0006900B File Offset: 0x0006720B
		protected virtual bool CheckActualOrScriptAliasControllerIsLeftHand(GameObject controller)
		{
			return this.IsControllerLeftHand(controller, true) || this.IsControllerLeftHand(controller, false);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00069021 File Offset: 0x00067221
		protected virtual bool CheckActualOrScriptAliasControllerIsRightHand(GameObject controller)
		{
			return this.IsControllerRightHand(controller, true) || this.IsControllerRightHand(controller, false);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00069038 File Offset: 0x00067238
		protected virtual bool CheckControllerLeftHand(GameObject controller, bool actual)
		{
			VRTK_SDKManager instance = VRTK_SDKManager.instance;
			if (!(instance != null) || !(controller != null))
			{
				return false;
			}
			if (!actual)
			{
				return controller.Equals(instance.scriptAliasLeftController);
			}
			return controller.Equals(instance.loadedSetup.actualLeftController);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00069080 File Offset: 0x00067280
		protected virtual bool CheckControllerRightHand(GameObject controller, bool actual)
		{
			VRTK_SDKManager instance = VRTK_SDKManager.instance;
			if (!(instance != null) || !(controller != null))
			{
				return false;
			}
			if (!actual)
			{
				return controller.Equals(instance.scriptAliasRightController);
			}
			return controller.Equals(instance.loadedSetup.actualRightController);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000690C8 File Offset: 0x000672C8
		protected virtual GameObject GetControllerModelFromController(GameObject controller)
		{
			return this.GetControllerModel(VRTK_DeviceFinder.GetControllerHand(controller));
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x000690D8 File Offset: 0x000672D8
		protected virtual GameObject GetSDKManagerControllerModelForHand(SDK_BaseController.ControllerHand hand)
		{
			VRTK_SDKManager instance = VRTK_SDKManager.instance;
			if (instance != null)
			{
				if (hand == SDK_BaseController.ControllerHand.Left)
				{
					return instance.loadedSetup.modelAliasLeftController;
				}
				if (hand == SDK_BaseController.ControllerHand.Right)
				{
					return instance.loadedSetup.modelAliasRightController;
				}
			}
			return null;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00069118 File Offset: 0x00067318
		protected virtual GameObject GetActualController(GameObject controller)
		{
			GameObject result = null;
			VRTK_SDKManager instance = VRTK_SDKManager.instance;
			if (instance != null)
			{
				if (this.IsControllerLeftHand(controller))
				{
					result = instance.loadedSetup.actualLeftController;
				}
				else if (this.IsControllerRightHand(controller))
				{
					result = instance.loadedSetup.actualRightController;
				}
			}
			return result;
		}

		// Token: 0x020005BE RID: 1470
		public enum ButtonTypes
		{
			// Token: 0x04002713 RID: 10003
			ButtonOne,
			// Token: 0x04002714 RID: 10004
			ButtonTwo,
			// Token: 0x04002715 RID: 10005
			Grip,
			// Token: 0x04002716 RID: 10006
			GripHairline,
			// Token: 0x04002717 RID: 10007
			StartMenu,
			// Token: 0x04002718 RID: 10008
			Trigger,
			// Token: 0x04002719 RID: 10009
			TriggerHairline,
			// Token: 0x0400271A RID: 10010
			Touchpad
		}

		// Token: 0x020005BF RID: 1471
		public enum ButtonPressTypes
		{
			// Token: 0x0400271C RID: 10012
			Press,
			// Token: 0x0400271D RID: 10013
			PressDown,
			// Token: 0x0400271E RID: 10014
			PressUp,
			// Token: 0x0400271F RID: 10015
			Touch,
			// Token: 0x04002720 RID: 10016
			TouchDown,
			// Token: 0x04002721 RID: 10017
			TouchUp
		}

		// Token: 0x020005C0 RID: 1472
		public enum ControllerElements
		{
			// Token: 0x04002723 RID: 10019
			AttachPoint,
			// Token: 0x04002724 RID: 10020
			Trigger,
			// Token: 0x04002725 RID: 10021
			GripLeft,
			// Token: 0x04002726 RID: 10022
			GripRight,
			// Token: 0x04002727 RID: 10023
			Touchpad,
			// Token: 0x04002728 RID: 10024
			ButtonOne,
			// Token: 0x04002729 RID: 10025
			ButtonTwo,
			// Token: 0x0400272A RID: 10026
			SystemMenu,
			// Token: 0x0400272B RID: 10027
			Body,
			// Token: 0x0400272C RID: 10028
			StartMenu
		}

		// Token: 0x020005C1 RID: 1473
		public enum ControllerHand
		{
			// Token: 0x0400272E RID: 10030
			None,
			// Token: 0x0400272F RID: 10031
			Left,
			// Token: 0x04002730 RID: 10032
			Right
		}

		// Token: 0x020005C2 RID: 1474
		public enum ControllerType
		{
			// Token: 0x04002732 RID: 10034
			Undefined,
			// Token: 0x04002733 RID: 10035
			Custom,
			// Token: 0x04002734 RID: 10036
			Simulator_Hand,
			// Token: 0x04002735 RID: 10037
			SteamVR_ViveWand,
			// Token: 0x04002736 RID: 10038
			SteamVR_OculusTouch,
			// Token: 0x04002737 RID: 10039
			Oculus_OculusTouch,
			// Token: 0x04002738 RID: 10040
			Daydream_Controller,
			// Token: 0x04002739 RID: 10041
			Ximmerse_Flip
		}
	}
}
