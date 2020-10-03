using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000265 RID: 613
	public abstract class SDK_BaseHeadset : SDK_Base
	{
		// Token: 0x06001266 RID: 4710
		public abstract void ProcessUpdate(Dictionary<string, object> options);

		// Token: 0x06001267 RID: 4711
		public abstract void ProcessFixedUpdate(Dictionary<string, object> options);

		// Token: 0x06001268 RID: 4712
		public abstract Transform GetHeadset();

		// Token: 0x06001269 RID: 4713
		public abstract Transform GetHeadsetCamera();

		// Token: 0x0600126A RID: 4714
		public abstract Vector3 GetHeadsetVelocity();

		// Token: 0x0600126B RID: 4715
		public abstract Vector3 GetHeadsetAngularVelocity();

		// Token: 0x0600126C RID: 4716
		public abstract void HeadsetFade(Color color, float duration, bool fadeOverlay = false);

		// Token: 0x0600126D RID: 4717
		public abstract bool HasHeadsetFade(Transform obj);

		// Token: 0x0600126E RID: 4718
		public abstract void AddHeadsetFade(Transform camera);

		// Token: 0x0600126F RID: 4719 RVA: 0x00069194 File Offset: 0x00067394
		protected Transform GetSDKManagerHeadset()
		{
			VRTK_SDKManager instance = VRTK_SDKManager.instance;
			if (instance != null && instance.loadedSetup.actualHeadset != null)
			{
				this.cachedHeadset = (instance.loadedSetup.actualHeadset ? instance.loadedSetup.actualHeadset.transform : null);
				return this.cachedHeadset;
			}
			return null;
		}

		// Token: 0x040010A4 RID: 4260
		protected Transform cachedHeadset;

		// Token: 0x040010A5 RID: 4261
		protected Transform cachedHeadsetCamera;
	}
}
