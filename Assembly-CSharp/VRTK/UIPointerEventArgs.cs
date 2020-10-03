using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VRTK
{
	// Token: 0x02000304 RID: 772
	public struct UIPointerEventArgs
	{
		// Token: 0x0400159A RID: 5530
		[Obsolete("`UIPointerEventArgs.controllerIndex` has been replaced with `UIPointerEventArgs.controllerReference`. This parameter will be removed in a future version of VRTK.")]
		public uint controllerIndex;

		// Token: 0x0400159B RID: 5531
		public VRTK_ControllerReference controllerReference;

		// Token: 0x0400159C RID: 5532
		public bool isActive;

		// Token: 0x0400159D RID: 5533
		public GameObject currentTarget;

		// Token: 0x0400159E RID: 5534
		public GameObject previousTarget;

		// Token: 0x0400159F RID: 5535
		public RaycastResult raycastResult;
	}
}
