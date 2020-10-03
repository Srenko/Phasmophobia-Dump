using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004A8 RID: 1192
	public class DisabledMeshRenderer : MonoBehaviour
	{
		// Token: 0x04002296 RID: 8854
		[HideInInspector]
		public MeshCombiner meshCombiner;

		// Token: 0x04002297 RID: 8855
		public CachedGameObject cachedGO;
	}
}
