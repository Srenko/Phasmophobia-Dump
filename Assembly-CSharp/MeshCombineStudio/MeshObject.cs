using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004C0 RID: 1216
	[Serializable]
	public class MeshObject
	{
		// Token: 0x060025B8 RID: 9656 RVA: 0x000BC968 File Offset: 0x000BAB68
		public MeshObject(CachedGameObject cachedGO, int subMeshIndex)
		{
			this.cachedGO = cachedGO;
			this.subMeshIndex = subMeshIndex;
			Transform t = cachedGO.t;
			this.position = t.position;
			this.rotation = t.rotation;
			this.scale = t.lossyScale;
			this.lightmapScaleOffset = cachedGO.mr.lightmapScaleOffset;
		}

		// Token: 0x04002328 RID: 9000
		public CachedGameObject cachedGO;

		// Token: 0x04002329 RID: 9001
		public MeshCache meshCache;

		// Token: 0x0400232A RID: 9002
		public int subMeshIndex;

		// Token: 0x0400232B RID: 9003
		public Vector3 position;

		// Token: 0x0400232C RID: 9004
		public Vector3 scale;

		// Token: 0x0400232D RID: 9005
		public Quaternion rotation;

		// Token: 0x0400232E RID: 9006
		public Vector4 lightmapScaleOffset;

		// Token: 0x0400232F RID: 9007
		public bool intersectsSurface;

		// Token: 0x04002330 RID: 9008
		public int startNewTriangleIndex;

		// Token: 0x04002331 RID: 9009
		public int newTriangleCount;

		// Token: 0x04002332 RID: 9010
		public bool skip;
	}
}
