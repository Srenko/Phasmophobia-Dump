using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000316 RID: 790
	public class VRTK_SDKTransformModify : MonoBehaviour
	{
		// Token: 0x06001BC7 RID: 7111 RVA: 0x00091360 File Offset: 0x0008F560
		protected virtual void OnEnable()
		{
			this.target = ((this.target != null) ? this.target : base.transform);
			this.sdkManager = VRTK_SDKManager.instance;
			if (this.sdkManager != null)
			{
				this.sdkManager.LoadedSetupChanged += this.LoadedSetupChanged;
				if (this.sdkManager.loadedSetup != null)
				{
					this.UpdateTransform();
				}
			}
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x000913D9 File Offset: 0x0008F5D9
		protected virtual void OnDisable()
		{
			if (this.sdkManager != null && !base.gameObject.activeSelf)
			{
				this.sdkManager.LoadedSetupChanged -= this.LoadedSetupChanged;
			}
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0009140E File Offset: 0x0008F60E
		protected virtual void LoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
		{
			this.UpdateTransform();
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00091418 File Offset: 0x0008F618
		protected virtual VRTK_SDKTransformModifiers GetSelectedModifier()
		{
			VRTK_SDKTransformModifiers vrtk_SDKTransformModifiers = this.sdkOverrides.FirstOrDefault((VRTK_SDKTransformModifiers item) => item.loadedSDKSetup == this.sdkManager.loadedSetup);
			if (vrtk_SDKTransformModifiers == null)
			{
				SDK_BaseController.ControllerType currentController = VRTK_DeviceFinder.GetCurrentControllerType();
				vrtk_SDKTransformModifiers = this.sdkOverrides.FirstOrDefault((VRTK_SDKTransformModifiers item) => item.controllerType == currentController);
			}
			return vrtk_SDKTransformModifiers;
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x0009146C File Offset: 0x0008F66C
		protected virtual void UpdateTransform()
		{
			if (this.target == null)
			{
				return;
			}
			VRTK_SDKTransformModifiers selectedModifier = this.GetSelectedModifier();
			if (selectedModifier != null)
			{
				this.target.localPosition = selectedModifier.position;
				this.target.localEulerAngles = selectedModifier.rotation;
				this.target.localScale = selectedModifier.scale;
			}
		}

		// Token: 0x0400164A RID: 5706
		[Tooltip("The target transform to modify on enable. If this is left blank then the transform the script is attached to will be used.")]
		public Transform target;

		// Token: 0x0400164B RID: 5707
		[Tooltip("A collection of SDK Transform overrides to change the given target transform for each specified SDK.")]
		public List<VRTK_SDKTransformModifiers> sdkOverrides = new List<VRTK_SDKTransformModifiers>();

		// Token: 0x0400164C RID: 5708
		protected VRTK_SDKManager sdkManager;
	}
}
