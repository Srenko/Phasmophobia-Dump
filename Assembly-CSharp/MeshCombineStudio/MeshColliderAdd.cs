using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004A0 RID: 1184
	public struct MeshColliderAdd
	{
		// Token: 0x060024FE RID: 9470 RVA: 0x000B66F7 File Offset: 0x000B48F7
		public MeshColliderAdd(GameObject go, Mesh mesh)
		{
			this.go = go;
			this.mesh = mesh;
		}

		// Token: 0x04002256 RID: 8790
		public GameObject go;

		// Token: 0x04002257 RID: 8791
		public Mesh mesh;
	}
}
