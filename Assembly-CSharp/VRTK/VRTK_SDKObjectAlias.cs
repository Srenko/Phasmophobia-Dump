using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000314 RID: 788
	public class VRTK_SDKObjectAlias : MonoBehaviour
	{
		// Token: 0x06001BC1 RID: 7105 RVA: 0x0009122A File Offset: 0x0008F42A
		protected virtual void OnEnable()
		{
			this.sdkManager = VRTK_SDKManager.instance;
			if (this.sdkManager != null)
			{
				this.sdkManager.LoadedSetupChanged += this.LoadedSetupChanged;
			}
			this.ChildToSDKObject();
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00091263 File Offset: 0x0008F463
		protected virtual void OnDisable()
		{
			if (this.sdkManager != null && !base.gameObject.activeSelf)
			{
				this.sdkManager.LoadedSetupChanged -= this.LoadedSetupChanged;
			}
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00091298 File Offset: 0x0008F498
		protected virtual void LoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
		{
			if (this.sdkManager != null)
			{
				this.ChildToSDKObject();
			}
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x000912B0 File Offset: 0x0008F4B0
		protected virtual void ChildToSDKObject()
		{
			Vector3 localPosition = base.transform.localPosition;
			Quaternion localRotation = base.transform.localRotation;
			Vector3 localScale = base.transform.localScale;
			Transform parent = null;
			VRTK_SDKObjectAlias.SDKObject sdkobject = this.sdkObject;
			if (sdkobject != VRTK_SDKObjectAlias.SDKObject.Boundary)
			{
				if (sdkobject == VRTK_SDKObjectAlias.SDKObject.Headset)
				{
					parent = VRTK_DeviceFinder.HeadsetTransform();
				}
			}
			else
			{
				parent = VRTK_DeviceFinder.PlayAreaTransform();
			}
			base.transform.SetParent(parent);
			base.transform.localPosition = localPosition;
			base.transform.localRotation = localRotation;
			base.transform.localScale = localScale;
		}

		// Token: 0x04001643 RID: 5699
		[Tooltip("The specific SDK Object to child this GameObject to.")]
		public VRTK_SDKObjectAlias.SDKObject sdkObject;

		// Token: 0x04001644 RID: 5700
		protected VRTK_SDKManager sdkManager;

		// Token: 0x02000616 RID: 1558
		public enum SDKObject
		{
			// Token: 0x040028AC RID: 10412
			Boundary,
			// Token: 0x040028AD RID: 10413
			Headset
		}
	}
}
