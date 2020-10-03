using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000282 RID: 642
	public static class VRTK_SDK_Bridge
	{
		// Token: 0x06001358 RID: 4952 RVA: 0x0006BFAA File Offset: 0x0006A1AA
		public static void ControllerProcessUpdate(VRTK_ControllerReference controllerReference, Dictionary<string, object> options = null)
		{
			VRTK_SDK_Bridge.GetControllerSDK().ProcessUpdate(controllerReference, options);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0006BFB8 File Offset: 0x0006A1B8
		public static void ControllerProcessFixedUpdate(VRTK_ControllerReference controllerReference, Dictionary<string, object> options = null)
		{
			VRTK_SDK_Bridge.GetControllerSDK().ProcessFixedUpdate(controllerReference, options);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0006BFC6 File Offset: 0x0006A1C6
		public static SDK_BaseController.ControllerType GetCurrentControllerType()
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetCurrentControllerType();
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0006BFD2 File Offset: 0x0006A1D2
		public static string GetControllerDefaultColliderPath(SDK_BaseController.ControllerHand hand)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetControllerDefaultColliderPath(hand);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0006BFDF File Offset: 0x0006A1DF
		public static string GetControllerElementPath(SDK_BaseController.ControllerElements element, SDK_BaseController.ControllerHand hand, bool fullPath = false)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetControllerElementPath(element, hand, fullPath);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0006BFEE File Offset: 0x0006A1EE
		public static uint GetControllerIndex(GameObject controller)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetControllerIndex(controller);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0006BFFB File Offset: 0x0006A1FB
		public static GameObject GetControllerByIndex(uint index, bool actual)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetControllerByIndex(index, actual);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0006C009 File Offset: 0x0006A209
		[Obsolete("`VRTK_SDK_Bridge.GetControllerOrigin(controller)` has been replaced with `VRTK_SDK_Bridge.GetControllerOrigin(controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static Transform GetControllerOrigin(GameObject controller)
		{
			return VRTK_SDK_Bridge.GetControllerOrigin(VRTK_ControllerReference.GetControllerReference(controller));
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0006C016 File Offset: 0x0006A216
		public static Transform GetControllerOrigin(VRTK_ControllerReference controllerReference)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetControllerOrigin(controllerReference);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0006C023 File Offset: 0x0006A223
		public static Transform GenerateControllerPointerOrigin(GameObject parent)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GenerateControllerPointerOrigin(parent);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0006C030 File Offset: 0x0006A230
		public static GameObject GetControllerLeftHand(bool actual)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetControllerLeftHand(actual);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0006C03D File Offset: 0x0006A23D
		public static GameObject GetControllerRightHand(bool actual)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetControllerRightHand(actual);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0006C04A File Offset: 0x0006A24A
		public static GameObject GetControllerByHand(SDK_BaseController.ControllerHand hand, bool actual)
		{
			if (hand == SDK_BaseController.ControllerHand.Left)
			{
				return VRTK_SDK_Bridge.GetControllerLeftHand(actual);
			}
			if (hand != SDK_BaseController.ControllerHand.Right)
			{
				return null;
			}
			return VRTK_SDK_Bridge.GetControllerRightHand(actual);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0006C065 File Offset: 0x0006A265
		public static bool IsControllerLeftHand(GameObject controller)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().IsControllerLeftHand(controller);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0006C072 File Offset: 0x0006A272
		public static bool IsControllerRightHand(GameObject controller)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().IsControllerRightHand(controller);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0006C07F File Offset: 0x0006A27F
		public static bool IsControllerLeftHand(GameObject controller, bool actual)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().IsControllerLeftHand(controller, actual);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0006C08D File Offset: 0x0006A28D
		public static bool IsControllerRightHand(GameObject controller, bool actual)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().IsControllerRightHand(controller, actual);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0006C09B File Offset: 0x0006A29B
		public static GameObject GetControllerModel(GameObject controller)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetControllerModel(controller);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0006C0A8 File Offset: 0x0006A2A8
		public static SDK_BaseController.ControllerHand GetControllerModelHand(GameObject controllerModel)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetControllerModelHand(controllerModel);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0006C0B5 File Offset: 0x0006A2B5
		public static GameObject GetControllerModel(SDK_BaseController.ControllerHand hand)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetControllerModel(hand);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0006C0C2 File Offset: 0x0006A2C2
		[Obsolete("`VRTK_SDK_Bridge.GetControllerRenderModel(controller)` has been replaced with `VRTK_SDK_Bridge.GetControllerRenderModel(controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static GameObject GetControllerRenderModel(GameObject controller)
		{
			return VRTK_SDK_Bridge.GetControllerRenderModel(VRTK_ControllerReference.GetControllerReference(controller));
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0006C0CF File Offset: 0x0006A2CF
		public static GameObject GetControllerRenderModel(VRTK_ControllerReference controllerReference)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetControllerRenderModel(controllerReference);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0006C0DC File Offset: 0x0006A2DC
		public static void SetControllerRenderModelWheel(GameObject renderModel, bool state)
		{
			VRTK_SDK_Bridge.GetControllerSDK().SetControllerRenderModelWheel(renderModel, state);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0006C0EA File Offset: 0x0006A2EA
		[Obsolete("`VRTK_SDK_Bridge.HapticPulseOnIndex(index, strength)` has been replaced with `VRTK_SDK_Bridge.HapticPulse(controllerReference, strength)`. This method will be removed in a future version of VRTK.")]
		public static void HapticPulseOnIndex(uint index, float strength = 0.5f)
		{
			VRTK_SDK_Bridge.HapticPulse(VRTK_ControllerReference.GetControllerReference(index), strength);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0006C0F8 File Offset: 0x0006A2F8
		public static void HapticPulse(VRTK_ControllerReference controllerReference, float strength = 0.5f)
		{
			VRTK_SDK_Bridge.GetControllerSDK().HapticPulse(controllerReference, strength);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0006C106 File Offset: 0x0006A306
		public static bool HapticPulse(VRTK_ControllerReference controllerReference, AudioClip clip)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().HapticPulse(controllerReference, clip);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0006C114 File Offset: 0x0006A314
		public static SDK_ControllerHapticModifiers GetHapticModifiers()
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetHapticModifiers();
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0006C120 File Offset: 0x0006A320
		[Obsolete("`VRTK_SDK_Bridge.GetVelocityOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerVelocity(controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static Vector3 GetVelocityOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerVelocity(VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0006C12D File Offset: 0x0006A32D
		public static Vector3 GetControllerVelocity(VRTK_ControllerReference controllerReference)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetVelocity(controllerReference);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0006C13A File Offset: 0x0006A33A
		[Obsolete("`VRTK_SDK_Bridge.GetAngularVelocityOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerAngularVelocity(controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static Vector3 GetAngularVelocityOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerAngularVelocity(VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0006C147 File Offset: 0x0006A347
		public static Vector3 GetControllerAngularVelocity(VRTK_ControllerReference controllerReference)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetAngularVelocity(controllerReference);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0006C154 File Offset: 0x0006A354
		public static bool IsTouchpadStatic(bool isTouched, Vector2 currentAxisValues, Vector2 previousAxisValues, int compareFidelity)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().IsTouchpadStatic(isTouched, currentAxisValues, previousAxisValues, compareFidelity);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0006C164 File Offset: 0x0006A364
		public static Vector2 GetControllerAxis(SDK_BaseController.ButtonTypes buttonType, VRTK_ControllerReference controllerReference)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetButtonAxis(buttonType, controllerReference);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0006C172 File Offset: 0x0006A372
		public static float GetControllerHairlineDelta(SDK_BaseController.ButtonTypes buttonType, VRTK_ControllerReference controllerReference)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetButtonHairlineDelta(buttonType, controllerReference);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0006C180 File Offset: 0x0006A380
		public static bool GetControllerButtonState(SDK_BaseController.ButtonTypes buttonType, SDK_BaseController.ButtonPressTypes pressType, VRTK_ControllerReference controllerReference)
		{
			return VRTK_SDK_Bridge.GetControllerSDK().GetControllerButtonState(buttonType, pressType, controllerReference);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0006C18F File Offset: 0x0006A38F
		[Obsolete("`VRTK_SDK_Bridge.GetTouchpadAxisOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerAxis(buttonType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static Vector2 GetTouchpadAxisOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerAxis(SDK_BaseController.ButtonTypes.Touchpad, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0006C19D File Offset: 0x0006A39D
		[Obsolete("`VRTK_SDK_Bridge.GetTriggerAxisOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerAxis(buttonType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static Vector2 GetTriggerAxisOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerAxis(SDK_BaseController.ButtonTypes.Trigger, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0006C1AB File Offset: 0x0006A3AB
		[Obsolete("`VRTK_SDK_Bridge.GetGripAxisOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerAxis(buttonType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static Vector2 GetGripAxisOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerAxis(SDK_BaseController.ButtonTypes.Grip, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0006C1B9 File Offset: 0x0006A3B9
		[Obsolete("`VRTK_SDK_Bridge.GetTriggerHairlineDeltaOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerHairlineDelta(buttonType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static float GetTriggerHairlineDeltaOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerHairlineDelta(SDK_BaseController.ButtonTypes.TriggerHairline, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0006C1C7 File Offset: 0x0006A3C7
		[Obsolete("`VRTK_SDK_Bridge.GetGripHairlineDeltaOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerHairlineDelta(buttonType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static float GetGripHairlineDeltaOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerHairlineDelta(SDK_BaseController.ButtonTypes.GripHairline, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0006C1D5 File Offset: 0x0006A3D5
		[Obsolete("`VRTK_SDK_Bridge.IsTriggerPressedOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsTriggerPressedOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Trigger, SDK_BaseController.ButtonPressTypes.Press, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0006C1E4 File Offset: 0x0006A3E4
		[Obsolete("`VRTK_SDK_Bridge.IsTriggerPressedDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsTriggerPressedDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Trigger, SDK_BaseController.ButtonPressTypes.PressDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0006C1F3 File Offset: 0x0006A3F3
		[Obsolete("`VRTK_SDK_Bridge.IsTriggerPressedUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsTriggerPressedUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Trigger, SDK_BaseController.ButtonPressTypes.PressUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0006C202 File Offset: 0x0006A402
		[Obsolete("`VRTK_SDK_Bridge.IsTriggerTouchedOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsTriggerTouchedOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Trigger, SDK_BaseController.ButtonPressTypes.Touch, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0006C211 File Offset: 0x0006A411
		[Obsolete("`VRTK_SDK_Bridge.IsTriggerTouchedDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsTriggerTouchedDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Trigger, SDK_BaseController.ButtonPressTypes.TouchDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0006C220 File Offset: 0x0006A420
		[Obsolete("`VRTK_SDK_Bridge.IsTriggerTouchedUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsTriggerTouchedUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Trigger, SDK_BaseController.ButtonPressTypes.TouchUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0006C22F File Offset: 0x0006A42F
		[Obsolete("`VRTK_SDK_Bridge.IsHairTriggerDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsHairTriggerDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.TriggerHairline, SDK_BaseController.ButtonPressTypes.PressDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0006C23E File Offset: 0x0006A43E
		[Obsolete("`VRTK_SDK_Bridge.IsHairTriggerUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsHairTriggerUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.TriggerHairline, SDK_BaseController.ButtonPressTypes.PressUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0006C24D File Offset: 0x0006A44D
		[Obsolete("`VRTK_SDK_Bridge.IsGripPressedOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsGripPressedOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Grip, SDK_BaseController.ButtonPressTypes.Press, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0006C25C File Offset: 0x0006A45C
		[Obsolete("`VRTK_SDK_Bridge.IsGripPressedDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsGripPressedDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Grip, SDK_BaseController.ButtonPressTypes.PressDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0006C26B File Offset: 0x0006A46B
		[Obsolete("`VRTK_SDK_Bridge.IsGripPressedUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsGripPressedUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Grip, SDK_BaseController.ButtonPressTypes.PressUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0006C27A File Offset: 0x0006A47A
		[Obsolete("`VRTK_SDK_Bridge.IsGripTouchedOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsGripTouchedOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Grip, SDK_BaseController.ButtonPressTypes.Touch, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0006C289 File Offset: 0x0006A489
		[Obsolete("`VRTK_SDK_Bridge.IsGripTouchedDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsGripTouchedDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Grip, SDK_BaseController.ButtonPressTypes.TouchDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0006C298 File Offset: 0x0006A498
		[Obsolete("`VRTK_SDK_Bridge.IsGripTouchedUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsGripTouchedUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Grip, SDK_BaseController.ButtonPressTypes.TouchUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0006C2A7 File Offset: 0x0006A4A7
		[Obsolete("`VRTK_SDK_Bridge.IsHairGripDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsHairGripDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.GripHairline, SDK_BaseController.ButtonPressTypes.PressDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0006C2B6 File Offset: 0x0006A4B6
		[Obsolete("`VRTK_SDK_Bridge.IsHairGripUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsHairGripUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.GripHairline, SDK_BaseController.ButtonPressTypes.PressUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0006C2C5 File Offset: 0x0006A4C5
		[Obsolete("`VRTK_SDK_Bridge.IsTouchpadPressedOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsTouchpadPressedOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Touchpad, SDK_BaseController.ButtonPressTypes.Press, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0006C2D4 File Offset: 0x0006A4D4
		[Obsolete("`VRTK_SDK_Bridge.IsTouchpadPressedDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsTouchpadPressedDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Touchpad, SDK_BaseController.ButtonPressTypes.PressDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0006C2E3 File Offset: 0x0006A4E3
		[Obsolete("`VRTK_SDK_Bridge.IsTouchpadPressedUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsTouchpadPressedUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Touchpad, SDK_BaseController.ButtonPressTypes.PressUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0006C2F2 File Offset: 0x0006A4F2
		[Obsolete("`VRTK_SDK_Bridge.IsTouchpadTouchedOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsTouchpadTouchedOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Touchpad, SDK_BaseController.ButtonPressTypes.Touch, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0006C301 File Offset: 0x0006A501
		[Obsolete("`VRTK_SDK_Bridge.IsTouchpadTouchedDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsTouchpadTouchedDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Touchpad, SDK_BaseController.ButtonPressTypes.TouchDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0006C310 File Offset: 0x0006A510
		[Obsolete("`VRTK_SDK_Bridge.IsTouchpadTouchedUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsTouchpadTouchedUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Touchpad, SDK_BaseController.ButtonPressTypes.TouchUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0006C31F File Offset: 0x0006A51F
		[Obsolete("`VRTK_SDK_Bridge.IsButtonOnePressedOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsButtonOnePressedOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonOne, SDK_BaseController.ButtonPressTypes.Press, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0006C32E File Offset: 0x0006A52E
		[Obsolete("`VRTK_SDK_Bridge.IsButtonOnePressedDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsButtonOnePressedDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonOne, SDK_BaseController.ButtonPressTypes.PressDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0006C33D File Offset: 0x0006A53D
		[Obsolete("`VRTK_SDK_Bridge.IsButtonOnePressedUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsButtonOnePressedUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonOne, SDK_BaseController.ButtonPressTypes.PressUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0006C34C File Offset: 0x0006A54C
		[Obsolete("`VRTK_SDK_Bridge.IsButtonOneTouchedOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsButtonOneTouchedOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonOne, SDK_BaseController.ButtonPressTypes.Touch, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0006C35B File Offset: 0x0006A55B
		[Obsolete("`VRTK_SDK_Bridge.IsButtonOneTouchedDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsButtonOneTouchedDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonOne, SDK_BaseController.ButtonPressTypes.TouchDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0006C36A File Offset: 0x0006A56A
		[Obsolete("`VRTK_SDK_Bridge.IsButtonOneTouchedUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsButtonOneTouchedUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonOne, SDK_BaseController.ButtonPressTypes.TouchUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0006C379 File Offset: 0x0006A579
		[Obsolete("`VRTK_SDK_Bridge.IsButtonTwoPressedOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsButtonTwoPressedOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonTwo, SDK_BaseController.ButtonPressTypes.Press, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0006C388 File Offset: 0x0006A588
		[Obsolete("`VRTK_SDK_Bridge.IsButtonTwoPressedDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsButtonTwoPressedDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonTwo, SDK_BaseController.ButtonPressTypes.PressDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0006C397 File Offset: 0x0006A597
		[Obsolete("`VRTK_SDK_Bridge.IsButtonTwoPressedUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsButtonTwoPressedUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonTwo, SDK_BaseController.ButtonPressTypes.PressUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0006C3A6 File Offset: 0x0006A5A6
		[Obsolete("`VRTK_SDK_Bridge.IsButtonTwoTouchedOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsButtonTwoTouchedOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonTwo, SDK_BaseController.ButtonPressTypes.Touch, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0006C3B5 File Offset: 0x0006A5B5
		[Obsolete("`VRTK_SDK_Bridge.IsButtonTwoTouchedDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsButtonTwoTouchedDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonTwo, SDK_BaseController.ButtonPressTypes.TouchDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0006C3C4 File Offset: 0x0006A5C4
		[Obsolete("`VRTK_SDK_Bridge.IsButtonTwoTouchedUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsButtonTwoTouchedUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonTwo, SDK_BaseController.ButtonPressTypes.TouchUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0006C3D3 File Offset: 0x0006A5D3
		[Obsolete("`VRTK_SDK_Bridge.IsStartMenuPressedOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsStartMenuPressedOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.StartMenu, SDK_BaseController.ButtonPressTypes.Press, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0006C3E2 File Offset: 0x0006A5E2
		[Obsolete("`VRTK_SDK_Bridge.IsStartMenuPressedDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsStartMenuPressedDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.StartMenu, SDK_BaseController.ButtonPressTypes.PressDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0006C3F1 File Offset: 0x0006A5F1
		[Obsolete("`VRTK_SDK_Bridge.IsStartMenuPressedUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsStartMenuPressedUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.StartMenu, SDK_BaseController.ButtonPressTypes.PressUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0006C400 File Offset: 0x0006A600
		[Obsolete("`VRTK_SDK_Bridge.IsStartMenuTouchedOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsStartMenuTouchedOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.StartMenu, SDK_BaseController.ButtonPressTypes.Touch, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0006C40F File Offset: 0x0006A60F
		[Obsolete("`VRTK_SDK_Bridge.IsStartMenuTouchedDownOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsStartMenuTouchedDownOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.StartMenu, SDK_BaseController.ButtonPressTypes.TouchDown, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0006C41E File Offset: 0x0006A61E
		[Obsolete("`VRTK_SDK_Bridge.IsStartMenuTouchedUpOnIndex(index)` has been replaced with `VRTK_SDK_Bridge.GetControllerButtonState(buttonType, pressType, controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static bool IsStartMenuTouchedUpOnIndex(uint index)
		{
			return VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.StartMenu, SDK_BaseController.ButtonPressTypes.TouchUp, VRTK_ControllerReference.GetControllerReference(index));
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0006C42D File Offset: 0x0006A62D
		public static void HeadsetProcessUpdate(Dictionary<string, object> options = null)
		{
			VRTK_SDK_Bridge.GetHeadsetSDK().ProcessUpdate(options);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0006C43A File Offset: 0x0006A63A
		public static void HeadsetProcessFixedUpdate(Dictionary<string, object> options = null)
		{
			VRTK_SDK_Bridge.GetHeadsetSDK().ProcessFixedUpdate(options);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0006C447 File Offset: 0x0006A647
		public static Transform GetHeadset()
		{
			return VRTK_SDK_Bridge.GetHeadsetSDK().GetHeadset();
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0006C453 File Offset: 0x0006A653
		public static Transform GetHeadsetCamera()
		{
			return VRTK_SDK_Bridge.GetHeadsetSDK().GetHeadsetCamera();
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0006C45F File Offset: 0x0006A65F
		public static Vector3 GetHeadsetVelocity()
		{
			return VRTK_SDK_Bridge.GetHeadsetSDK().GetHeadsetVelocity();
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0006C46B File Offset: 0x0006A66B
		public static Vector3 GetHeadsetAngularVelocity()
		{
			return VRTK_SDK_Bridge.GetHeadsetSDK().GetHeadsetAngularVelocity();
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0006C477 File Offset: 0x0006A677
		public static void HeadsetFade(Color color, float duration, bool fadeOverlay = false)
		{
			VRTK_SDK_Bridge.GetHeadsetSDK().HeadsetFade(color, duration, fadeOverlay);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0006C486 File Offset: 0x0006A686
		public static bool HasHeadsetFade(Transform obj)
		{
			return VRTK_SDK_Bridge.GetHeadsetSDK().HasHeadsetFade(obj);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0006C493 File Offset: 0x0006A693
		public static void AddHeadsetFade(Transform camera)
		{
			VRTK_SDK_Bridge.GetHeadsetSDK().AddHeadsetFade(camera);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0006C4A0 File Offset: 0x0006A6A0
		public static Transform GetPlayArea()
		{
			return VRTK_SDK_Bridge.GetBoundariesSDK().GetPlayArea();
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0006C4AC File Offset: 0x0006A6AC
		public static Vector3[] GetPlayAreaVertices()
		{
			return VRTK_SDK_Bridge.GetBoundariesSDK().GetPlayAreaVertices();
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0006C4B8 File Offset: 0x0006A6B8
		public static float GetPlayAreaBorderThickness()
		{
			return VRTK_SDK_Bridge.GetBoundariesSDK().GetPlayAreaBorderThickness();
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0006C4C4 File Offset: 0x0006A6C4
		public static bool IsPlayAreaSizeCalibrated()
		{
			return VRTK_SDK_Bridge.GetBoundariesSDK().IsPlayAreaSizeCalibrated();
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0006C4D0 File Offset: 0x0006A6D0
		public static bool GetDrawAtRuntime()
		{
			return VRTK_SDK_Bridge.GetBoundariesSDK().GetDrawAtRuntime();
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0006C4DC File Offset: 0x0006A6DC
		public static void SetDrawAtRuntime(bool value)
		{
			VRTK_SDK_Bridge.GetBoundariesSDK().SetDrawAtRuntime(value);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0006C4E9 File Offset: 0x0006A6E9
		public static bool IsDisplayOnDesktop()
		{
			return VRTK_SDK_Bridge.GetSystemSDK().IsDisplayOnDesktop();
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0006C4F5 File Offset: 0x0006A6F5
		public static bool ShouldAppRenderWithLowResources()
		{
			return VRTK_SDK_Bridge.GetSystemSDK().ShouldAppRenderWithLowResources();
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0006C501 File Offset: 0x0006A701
		public static void ForceInterleavedReprojectionOn(bool force)
		{
			VRTK_SDK_Bridge.GetSystemSDK().ForceInterleavedReprojectionOn(force);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0006C510 File Offset: 0x0006A710
		public static SDK_BaseSystem GetSystemSDK()
		{
			if (VRTK_SDKManager.instance != null && VRTK_SDKManager.instance.loadedSetup != null)
			{
				return VRTK_SDKManager.instance.loadedSetup.systemSDK;
			}
			if (VRTK_SDK_Bridge.systemSDK == null)
			{
				VRTK_SDK_Bridge.systemSDK = ScriptableObject.CreateInstance<SDK_FallbackSystem>();
			}
			return VRTK_SDK_Bridge.systemSDK;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0006C568 File Offset: 0x0006A768
		public static SDK_BaseHeadset GetHeadsetSDK()
		{
			if (VRTK_SDKManager.instance != null && VRTK_SDKManager.instance.loadedSetup != null)
			{
				return VRTK_SDKManager.instance.loadedSetup.headsetSDK;
			}
			if (VRTK_SDK_Bridge.headsetSDK == null)
			{
				VRTK_SDK_Bridge.headsetSDK = ScriptableObject.CreateInstance<SDK_FallbackHeadset>();
			}
			return VRTK_SDK_Bridge.headsetSDK;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0006C5C0 File Offset: 0x0006A7C0
		public static SDK_BaseController GetControllerSDK()
		{
			if (VRTK_SDKManager.instance != null && VRTK_SDKManager.instance.loadedSetup != null)
			{
				return VRTK_SDKManager.instance.loadedSetup.controllerSDK;
			}
			if (VRTK_SDK_Bridge.controllerSDK == null)
			{
				VRTK_SDK_Bridge.controllerSDK = ScriptableObject.CreateInstance<SDK_FallbackController>();
			}
			return VRTK_SDK_Bridge.controllerSDK;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0006C618 File Offset: 0x0006A818
		public static SDK_BaseBoundaries GetBoundariesSDK()
		{
			if (VRTK_SDKManager.instance != null && VRTK_SDKManager.instance.loadedSetup != null)
			{
				return VRTK_SDKManager.instance.loadedSetup.boundariesSDK;
			}
			if (VRTK_SDK_Bridge.boundariesSDK == null)
			{
				VRTK_SDK_Bridge.boundariesSDK = ScriptableObject.CreateInstance<SDK_FallbackBoundaries>();
			}
			return VRTK_SDK_Bridge.boundariesSDK;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0006C670 File Offset: 0x0006A870
		public static void InvalidateCaches()
		{
			Object.Destroy(VRTK_SDK_Bridge.systemSDK);
			Object.Destroy(VRTK_SDK_Bridge.headsetSDK);
			Object.Destroy(VRTK_SDK_Bridge.controllerSDK);
			Object.Destroy(VRTK_SDK_Bridge.boundariesSDK);
			VRTK_SDK_Bridge.systemSDK = null;
			VRTK_SDK_Bridge.headsetSDK = null;
			VRTK_SDK_Bridge.controllerSDK = null;
			VRTK_SDK_Bridge.boundariesSDK = null;
		}

		// Token: 0x040010FB RID: 4347
		private static SDK_BaseSystem systemSDK;

		// Token: 0x040010FC RID: 4348
		private static SDK_BaseHeadset headsetSDK;

		// Token: 0x040010FD RID: 4349
		private static SDK_BaseController controllerSDK;

		// Token: 0x040010FE RID: 4350
		private static SDK_BaseBoundaries boundariesSDK;
	}
}
