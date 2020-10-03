using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004B1 RID: 1201
	public struct AABB3
	{
		// Token: 0x0600257E RID: 9598 RVA: 0x000BB183 File Offset: 0x000B9383
		public AABB3(Vector3 min, Vector3 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040022E4 RID: 8932
		public Vector3 min;

		// Token: 0x040022E5 RID: 8933
		public Vector3 max;
	}
}
