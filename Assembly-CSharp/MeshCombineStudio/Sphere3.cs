using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004B3 RID: 1203
	public struct Sphere3
	{
		// Token: 0x06002580 RID: 9600 RVA: 0x000BB387 File Offset: 0x000B9587
		public Sphere3(Vector3 center, float radius)
		{
			this.center = center;
			this.radius = radius;
		}

		// Token: 0x040022F4 RID: 8948
		public Vector3 center;

		// Token: 0x040022F5 RID: 8949
		public float radius;
	}
}
