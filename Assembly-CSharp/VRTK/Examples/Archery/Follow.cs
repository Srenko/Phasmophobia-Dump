using System;
using UnityEngine;

namespace VRTK.Examples.Archery
{
	// Token: 0x02000382 RID: 898
	public class Follow : MonoBehaviour
	{
		// Token: 0x06001ED3 RID: 7891 RVA: 0x0009C7C0 File Offset: 0x0009A9C0
		private void Update()
		{
			if (this.target != null)
			{
				if (this.followRotation)
				{
					base.transform.rotation = this.target.rotation;
				}
				if (this.followPosition)
				{
					base.transform.position = this.target.position;
					return;
				}
			}
			else
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.NOT_DEFINED, new object[]
				{
					"target"
				}));
			}
		}

		// Token: 0x0400180C RID: 6156
		public bool followPosition;

		// Token: 0x0400180D RID: 6157
		public bool followRotation;

		// Token: 0x0400180E RID: 6158
		public Transform target;
	}
}
