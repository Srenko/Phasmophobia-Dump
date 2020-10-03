using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002D0 RID: 720
	public struct ObjectControlEventArgs
	{
		// Token: 0x04001383 RID: 4995
		public GameObject controlledGameObject;

		// Token: 0x04001384 RID: 4996
		public Transform directionDevice;

		// Token: 0x04001385 RID: 4997
		public Vector3 axisDirection;

		// Token: 0x04001386 RID: 4998
		public float axis;

		// Token: 0x04001387 RID: 4999
		public float deadzone;

		// Token: 0x04001388 RID: 5000
		public bool currentlyFalling;

		// Token: 0x04001389 RID: 5001
		public bool modifierActive;
	}
}
