using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000261 RID: 609
	public abstract class SDK_Base : ScriptableObject
	{
		// Token: 0x06001230 RID: 4656 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnBeforeSetupLoad(VRTK_SDKSetup setup)
		{
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnAfterSetupLoad(VRTK_SDKSetup setup)
		{
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnBeforeSetupUnload(VRTK_SDKSetup setup)
		{
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00003F60 File Offset: 0x00002160
		public virtual void OnAfterSetupUnload(VRTK_SDKSetup setup)
		{
		}
	}
}
