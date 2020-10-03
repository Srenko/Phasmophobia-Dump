using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004C1 RID: 1217
	[Serializable]
	public class CachedGameObject
	{
		// Token: 0x060025B9 RID: 9657 RVA: 0x000BC9C8 File Offset: 0x000BABC8
		public CachedGameObject(GameObject go, Transform t, MeshRenderer mr, MeshFilter mf, Mesh mesh)
		{
			this.go = go;
			this.t = t;
			this.mr = mr;
			this.mf = mf;
			this.mesh = mesh;
			this.mt = t.localToWorldMatrix;
			this.mtNormals = this.mt.inverse.transpose;
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000BCA28 File Offset: 0x000BAC28
		public CachedGameObject(CachedComponents cachedComponent)
		{
			this.go = cachedComponent.go;
			this.t = cachedComponent.t;
			this.mr = cachedComponent.mr;
			this.mf = cachedComponent.mf;
			this.mesh = cachedComponent.mf.sharedMesh;
			this.mt = this.t.localToWorldMatrix;
			this.mtNormals = this.mt.inverse.transpose;
		}

		// Token: 0x04002333 RID: 9011
		public GameObject go;

		// Token: 0x04002334 RID: 9012
		public Transform t;

		// Token: 0x04002335 RID: 9013
		public MeshRenderer mr;

		// Token: 0x04002336 RID: 9014
		public MeshFilter mf;

		// Token: 0x04002337 RID: 9015
		public Mesh mesh;

		// Token: 0x04002338 RID: 9016
		public Matrix4x4 mt;

		// Token: 0x04002339 RID: 9017
		public Matrix4x4 mtNormals;
	}
}
