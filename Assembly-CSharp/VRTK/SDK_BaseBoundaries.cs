using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000262 RID: 610
	public abstract class SDK_BaseBoundaries : SDK_Base
	{
		// Token: 0x06001235 RID: 4661
		public abstract void InitBoundaries();

		// Token: 0x06001236 RID: 4662
		public abstract Transform GetPlayArea();

		// Token: 0x06001237 RID: 4663
		public abstract Vector3[] GetPlayAreaVertices();

		// Token: 0x06001238 RID: 4664
		public abstract float GetPlayAreaBorderThickness();

		// Token: 0x06001239 RID: 4665
		public abstract bool IsPlayAreaSizeCalibrated();

		// Token: 0x0600123A RID: 4666
		public abstract bool GetDrawAtRuntime();

		// Token: 0x0600123B RID: 4667
		public abstract void SetDrawAtRuntime(bool value);

		// Token: 0x0600123C RID: 4668 RVA: 0x00068EE0 File Offset: 0x000670E0
		protected Transform GetSDKManagerPlayArea()
		{
			VRTK_SDKManager instance = VRTK_SDKManager.instance;
			if (instance != null && instance.loadedSetup.actualBoundaries != null)
			{
				this.cachedPlayArea = (instance.loadedSetup.actualBoundaries ? instance.loadedSetup.actualBoundaries.transform : null);
				return this.cachedPlayArea;
			}
			return null;
		}

		// Token: 0x0400109F RID: 4255
		protected Transform cachedPlayArea;
	}
}
