using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004A6 RID: 1190
	public class DirectDraw : MonoBehaviour
	{
		// Token: 0x06002538 RID: 9528 RVA: 0x000B9014 File Offset: 0x000B7214
		private void Awake()
		{
			this.mrs = base.GetComponentsInChildren<MeshRenderer>(false);
			this.SetMeshRenderersEnabled(false);
			this.meshes = new Mesh[this.mrs.Length];
			this.mats = new Material[this.mrs.Length];
			this.positions = new Vector3[this.mrs.Length];
			this.rotations = new Quaternion[this.mrs.Length];
			for (int i = 0; i < this.mrs.Length; i++)
			{
				MeshFilter component = this.mrs[i].GetComponent<MeshFilter>();
				this.meshes[i] = component.sharedMesh;
				this.mats[i] = this.mrs[i].sharedMaterial;
				this.positions[i] = this.mrs[i].transform.position;
				this.rotations[i] = this.mrs[i].transform.rotation;
			}
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000B9104 File Offset: 0x000B7304
		private void SetMeshRenderersEnabled(bool enabled)
		{
			for (int i = 0; i < this.mrs.Length; i++)
			{
				this.mrs[i].enabled = enabled;
			}
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000B9134 File Offset: 0x000B7334
		private void Update()
		{
			for (int i = 0; i < this.mrs.Length; i++)
			{
				Graphics.DrawMesh(this.meshes[i], this.positions[i], this.rotations[i], this.mats[i], 0);
			}
		}

		// Token: 0x0400228F RID: 8847
		private MeshRenderer[] mrs;

		// Token: 0x04002290 RID: 8848
		private Mesh[] meshes;

		// Token: 0x04002291 RID: 8849
		private Material[] mats;

		// Token: 0x04002292 RID: 8850
		private Vector3[] positions;

		// Token: 0x04002293 RID: 8851
		private Quaternion[] rotations;
	}
}
