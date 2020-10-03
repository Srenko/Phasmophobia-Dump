using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004AD RID: 1197
	[ExecuteInEditMode]
	public class GarbageCollectMesh : MonoBehaviour
	{
		// Token: 0x06002569 RID: 9577 RVA: 0x000B9EE1 File Offset: 0x000B80E1
		private void OnDestroy()
		{
			if (this.mesh != null)
			{
				Object.Destroy(this.mesh);
			}
		}

		// Token: 0x0400229D RID: 8861
		public Mesh mesh;
	}
}
