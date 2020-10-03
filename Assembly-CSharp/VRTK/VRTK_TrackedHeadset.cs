using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002C0 RID: 704
	public class VRTK_TrackedHeadset : MonoBehaviour
	{
		// Token: 0x06001755 RID: 5973 RVA: 0x0007CCB9 File Offset: 0x0007AEB9
		protected virtual void FixedUpdate()
		{
			VRTK_SDK_Bridge.HeadsetProcessFixedUpdate(null);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0007CCC1 File Offset: 0x0007AEC1
		protected virtual void Update()
		{
			VRTK_SDK_Bridge.HeadsetProcessUpdate(null);
		}
	}
}
