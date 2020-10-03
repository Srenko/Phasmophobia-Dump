using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000315 RID: 789
	[Serializable]
	public class VRTK_SDKTransformModifiers
	{
		// Token: 0x04001645 RID: 5701
		[Header("SDK settings")]
		[Tooltip("An optional SDK Setup to use to determine when to modify the transform.")]
		public VRTK_SDKSetup loadedSDKSetup;

		// Token: 0x04001646 RID: 5702
		[Tooltip("An optional SDK controller type to use to determine when to modify the transform.")]
		public SDK_BaseController.ControllerType controllerType;

		// Token: 0x04001647 RID: 5703
		[Header("Transform Override Settings")]
		[Tooltip("The new local position to change the transform to.")]
		public Vector3 position = Vector3.zero;

		// Token: 0x04001648 RID: 5704
		[Tooltip("The new local rotation in eular angles to change the transform to.")]
		public Vector3 rotation = Vector3.zero;

		// Token: 0x04001649 RID: 5705
		[Tooltip("The new local scale to change the transform to.")]
		public Vector3 scale = Vector3.one;
	}
}
