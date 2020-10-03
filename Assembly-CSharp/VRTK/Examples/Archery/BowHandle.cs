using System;
using UnityEngine;

namespace VRTK.Examples.Archery
{
	// Token: 0x02000381 RID: 897
	public class BowHandle : MonoBehaviour
	{
		// Token: 0x04001809 RID: 6153
		public Transform arrowNockingPoint;

		// Token: 0x0400180A RID: 6154
		public BowAim aim;

		// Token: 0x0400180B RID: 6155
		[HideInInspector]
		public Transform nockSide;
	}
}
