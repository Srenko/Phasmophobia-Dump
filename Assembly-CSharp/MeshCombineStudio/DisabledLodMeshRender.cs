using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004A7 RID: 1191
	public class DisabledLodMeshRender : MonoBehaviour
	{
		// Token: 0x04002294 RID: 8852
		[HideInInspector]
		public MeshCombiner meshCombiner;

		// Token: 0x04002295 RID: 8853
		public CachedLodGameObject cachedLodGO;
	}
}
