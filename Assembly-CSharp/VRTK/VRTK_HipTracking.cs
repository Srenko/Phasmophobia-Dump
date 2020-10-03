using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002FA RID: 762
	[AddComponentMenu("VRTK/Scripts/Presence/VRTK_HipTracking")]
	public class VRTK_HipTracking : MonoBehaviour
	{
		// Token: 0x06001A6D RID: 6765 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0008BAA0 File Offset: 0x00089CA0
		protected virtual void OnEnable()
		{
			this.playerHead = ((this.headOverride != null) ? this.headOverride : VRTK_DeviceFinder.HeadsetTransform());
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0008BAC4 File Offset: 0x00089CC4
		protected virtual void LateUpdate()
		{
			if (this.playerHead == null)
			{
				return;
			}
			Vector3 up = Vector3.up;
			if (this.ReferenceUp != null)
			{
				up = this.ReferenceUp.up;
			}
			base.transform.position = this.playerHead.position + this.HeadOffset * up;
			Vector3 forward = this.playerHead.forward;
			Vector3 vector = forward;
			vector.y = 0f;
			vector.Normalize();
			Vector3 a = this.playerHead.up;
			if (forward.y > 0f)
			{
				a = -this.playerHead.up;
			}
			a.y = 0f;
			a.Normalize();
			float num = Mathf.Clamp(Vector3.Dot(vector, forward), 0f, 1f);
			Vector3 forward2 = Vector3.Lerp(a, vector, num * num);
			base.transform.rotation = Quaternion.LookRotation(forward2, up);
		}

		// Token: 0x04001569 RID: 5481
		[Tooltip("Distance underneath Player Head for hips to reside.")]
		public float HeadOffset = -0.35f;

		// Token: 0x0400156A RID: 5482
		[Header("Optional")]
		[Tooltip("Optional Transform to use as the Head Object for calculating hip position. If none is given one will try to be found in the scene.")]
		public Transform headOverride;

		// Token: 0x0400156B RID: 5483
		[Tooltip("Optional Transform to use for calculating which way is 'Up' relative to the player for hip positioning.")]
		public Transform ReferenceUp;

		// Token: 0x0400156C RID: 5484
		protected Transform playerHead;
	}
}
