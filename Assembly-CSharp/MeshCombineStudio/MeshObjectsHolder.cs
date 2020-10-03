using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004BC RID: 1212
	[Serializable]
	public class MeshObjectsHolder
	{
		// Token: 0x060025B0 RID: 9648 RVA: 0x000BC329 File Offset: 0x000BA529
		public MeshObjectsHolder(ref CombineCondition combineCondition, Material mat)
		{
			this.mat = mat;
			this.combineCondition = combineCondition;
		}

		// Token: 0x040022FE RID: 8958
		public FastList<MeshObject> meshObjects = new FastList<MeshObject>();

		// Token: 0x040022FF RID: 8959
		public ObjectOctree.LODParent lodParent;

		// Token: 0x04002300 RID: 8960
		public FastList<CachedGameObject> newCachedGOs;

		// Token: 0x04002301 RID: 8961
		public int lodLevel;

		// Token: 0x04002302 RID: 8962
		public Material mat;

		// Token: 0x04002303 RID: 8963
		public bool hasChanged;

		// Token: 0x04002304 RID: 8964
		public CombineCondition combineCondition;
	}
}
