using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004AE RID: 1198
	[ExecuteInEditMode]
	public class LightmapSettings : MonoBehaviour
	{
		// Token: 0x0600256B RID: 9579 RVA: 0x000B9EFC File Offset: 0x000B80FC
		private void OnEnable()
		{
			if (this.mr)
			{
				this.mr.lightmapIndex = this.lightmapIndex;
			}
		}

		// Token: 0x0400229E RID: 8862
		public MeshRenderer mr;

		// Token: 0x0400229F RID: 8863
		public int lightmapIndex;
	}
}
