using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200026F RID: 623
	[SDK_Description(typeof(SDK_FallbackSystem), 0)]
	public class SDK_FallbackController : SDK_BaseController
	{
		// Token: 0x0600128C RID: 4748 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ProcessUpdate(VRTK_ControllerReference controllerReference, Dictionary<string, object> options)
		{
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ProcessFixedUpdate(VRTK_ControllerReference controllerReference, Dictionary<string, object> options)
		{
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000694A8 File Offset: 0x000676A8
		public override SDK_BaseController.ControllerType GetCurrentControllerType()
		{
			return SDK_BaseController.ControllerType.Undefined;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0005DD8C File Offset: 0x0005BF8C
		public override string GetControllerDefaultColliderPath(SDK_BaseController.ControllerHand hand)
		{
			return "";
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0005DD8C File Offset: 0x0005BF8C
		public override string GetControllerElementPath(SDK_BaseController.ControllerElements element, SDK_BaseController.ControllerHand hand, bool fullPath = false)
		{
			return "";
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000694B3 File Offset: 0x000676B3
		public override uint GetControllerIndex(GameObject controller)
		{
			return uint.MaxValue;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0006949E File Offset: 0x0006769E
		public override GameObject GetControllerByIndex(uint index, bool actual = false)
		{
			return null;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0006949E File Offset: 0x0006769E
		public override Transform GetControllerOrigin(VRTK_ControllerReference controllerReference)
		{
			return null;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0006949E File Offset: 0x0006769E
		public override Transform GenerateControllerPointerOrigin(GameObject parent)
		{
			return null;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0006949E File Offset: 0x0006769E
		public override GameObject GetControllerLeftHand(bool actual = false)
		{
			return null;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0006949E File Offset: 0x0006769E
		public override GameObject GetControllerRightHand(bool actual = false)
		{
			return null;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool IsControllerLeftHand(GameObject controller)
		{
			return false;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool IsControllerRightHand(GameObject controller)
		{
			return false;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool IsControllerLeftHand(GameObject controller, bool actual)
		{
			return false;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool IsControllerRightHand(GameObject controller, bool actual)
		{
			return false;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0006949E File Offset: 0x0006769E
		public override GameObject GetControllerModel(GameObject controller)
		{
			return null;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0006949E File Offset: 0x0006769E
		public override GameObject GetControllerModel(SDK_BaseController.ControllerHand hand)
		{
			return null;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0006949E File Offset: 0x0006769E
		public override GameObject GetControllerRenderModel(VRTK_ControllerReference controllerReference)
		{
			return null;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00003F60 File Offset: 0x00002160
		public override void SetControllerRenderModelWheel(GameObject renderModel, bool state)
		{
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00003F60 File Offset: 0x00002160
		public override void HapticPulse(VRTK_ControllerReference controllerReference, float strength = 0.5f)
		{
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000694B6 File Offset: 0x000676B6
		public override bool HapticPulse(VRTK_ControllerReference controllerReference, AudioClip clip)
		{
			return true;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000694B9 File Offset: 0x000676B9
		public override SDK_ControllerHapticModifiers GetHapticModifiers()
		{
			return new SDK_ControllerHapticModifiers();
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000694C0 File Offset: 0x000676C0
		public override Vector3 GetVelocity(VRTK_ControllerReference controllerReference)
		{
			return Vector3.zero;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x000694C0 File Offset: 0x000676C0
		public override Vector3 GetAngularVelocity(VRTK_ControllerReference controllerReference)
		{
			return Vector3.zero;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x000694C7 File Offset: 0x000676C7
		public override bool IsTouchpadStatic(bool isTouched, Vector2 currentAxisValues, Vector2 previousAxisValues, int compareFidelity)
		{
			return !isTouched || VRTK_SharedMethods.Vector2ShallowCompare(currentAxisValues, previousAxisValues, compareFidelity);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000694D7 File Offset: 0x000676D7
		public override Vector2 GetButtonAxis(SDK_BaseController.ButtonTypes buttonType, VRTK_ControllerReference controllerReference)
		{
			return Vector2.zero;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000694A1 File Offset: 0x000676A1
		public override float GetButtonHairlineDelta(SDK_BaseController.ButtonTypes buttonType, VRTK_ControllerReference controllerReference)
		{
			return 0f;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool GetControllerButtonState(SDK_BaseController.ButtonTypes buttonType, SDK_BaseController.ButtonPressTypes pressType, VRTK_ControllerReference controllerReference)
		{
			return false;
		}
	}
}
