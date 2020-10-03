using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004C2 RID: 1218
	[Serializable]
	public class CachedLodGameObject : CachedGameObject
	{
		// Token: 0x060025BB RID: 9659 RVA: 0x000BCAA6 File Offset: 0x000BACA6
		public CachedLodGameObject(CachedGameObject cachedGO, int lodCount, int lodLevel) : base(cachedGO.go, cachedGO.t, cachedGO.mr, cachedGO.mf, cachedGO.mesh)
		{
			this.lodCount = lodCount;
			this.lodLevel = lodLevel;
		}

		// Token: 0x0400233A RID: 9018
		public Vector3 center;

		// Token: 0x0400233B RID: 9019
		public int lodCount;

		// Token: 0x0400233C RID: 9020
		public int lodLevel;
	}
}
