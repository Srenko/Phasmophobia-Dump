using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x0200049D RID: 1181
	public class MeshCache
	{
		// Token: 0x060024D1 RID: 9425 RVA: 0x000B4DA8 File Offset: 0x000B2FA8
		public MeshCache(Mesh mesh)
		{
			this.mesh = mesh;
			this.subMeshCount = mesh.subMeshCount;
			this.subMeshCache = new MeshCache.SubMeshCache[this.subMeshCount];
			if (this.subMeshCount == 1)
			{
				this.subMeshCache[0] = new MeshCache.SubMeshCache(mesh, true);
				return;
			}
			MeshCache.SubMeshCache sub = new MeshCache.SubMeshCache(mesh, false);
			for (int i = 0; i < this.subMeshCache.Length; i++)
			{
				this.subMeshCache[i] = new MeshCache.SubMeshCache(mesh, i);
				this.subMeshCache[i].RebuildVertexBuffer(sub, true);
			}
		}

		// Token: 0x040021FA RID: 8698
		public Mesh mesh;

		// Token: 0x040021FB RID: 8699
		public MeshCache.SubMeshCache[] subMeshCache;

		// Token: 0x040021FC RID: 8700
		public int subMeshCount;

		// Token: 0x020007A8 RID: 1960
		public class SubMeshCache
		{
			// Token: 0x06003056 RID: 12374 RVA: 0x00008842 File Offset: 0x00006A42
			public SubMeshCache()
			{
			}

			// Token: 0x06003057 RID: 12375 RVA: 0x000CCF80 File Offset: 0x000CB180
			public void CopySubMeshCache(MeshCache.SubMeshCache source)
			{
				this.vertexCount = source.vertexCount;
				Array.Copy(source.vertices, 0, this.vertices, 0, this.vertexCount);
				this.hasNormals = source.hasNormals;
				this.hasTangents = source.hasTangents;
				this.hasColors = source.hasColors;
				this.hasUv = source.hasUv;
				this.hasUv2 = source.hasUv2;
				this.hasUv3 = source.hasUv3;
				this.hasUv4 = source.hasUv4;
				if (source.hasNormals)
				{
					this.CopyArray<Vector3>(source.normals, ref this.normals, this.vertexCount);
				}
				if (source.hasTangents)
				{
					this.CopyArray<Vector4>(source.tangents, ref this.tangents, this.vertexCount);
				}
				if (source.hasUv)
				{
					this.CopyArray<Vector2>(source.uv, ref this.uv, this.vertexCount);
				}
				if (source.hasUv2)
				{
					this.CopyArray<Vector2>(source.uv2, ref this.uv2, this.vertexCount);
				}
				if (source.hasUv3)
				{
					this.CopyArray<Vector2>(source.uv3, ref this.uv3, this.vertexCount);
				}
				if (source.hasUv4)
				{
					this.CopyArray<Vector2>(source.uv4, ref this.uv4, this.vertexCount);
				}
				if (source.hasColors)
				{
					this.CopyArray<Color32>(source.colors32, ref this.colors32, this.vertexCount);
				}
			}

			// Token: 0x06003058 RID: 12376 RVA: 0x000CD0E6 File Offset: 0x000CB2E6
			public void CopyArray<T>(Array sourceArray, ref T[] destinationArray, int vertexCount)
			{
				if (destinationArray == null)
				{
					destinationArray = new T[65534];
				}
				Array.Copy(sourceArray, 0, destinationArray, 0, vertexCount);
			}

			// Token: 0x06003059 RID: 12377 RVA: 0x000CD103 File Offset: 0x000CB303
			public SubMeshCache(Mesh mesh, int subMeshIndex)
			{
				this.triangles = mesh.GetTriangles(subMeshIndex);
				this.triangleCount = this.triangles.Length;
			}

			// Token: 0x0600305A RID: 12378 RVA: 0x000CD128 File Offset: 0x000CB328
			public SubMeshCache(Mesh mesh, bool assignTriangles)
			{
				this.vertices = mesh.vertices;
				this.normals = mesh.normals;
				this.tangents = mesh.tangents;
				this.uv = mesh.uv;
				this.uv2 = mesh.uv2;
				this.uv3 = mesh.uv3;
				this.uv4 = mesh.uv4;
				this.colors32 = mesh.colors32;
				if (assignTriangles)
				{
					this.triangles = mesh.triangles;
					this.triangleCount = this.triangles.Length;
				}
				this.CheckHasArrays();
				this.vertexCount = this.vertices.Length;
			}

			// Token: 0x0600305B RID: 12379 RVA: 0x000CD1CC File Offset: 0x000CB3CC
			public void CheckHasArrays()
			{
				if (this.normals != null && this.normals.Length != 0)
				{
					this.hasNormals = true;
				}
				if (this.tangents != null && this.tangents.Length != 0)
				{
					this.hasTangents = true;
				}
				if (this.uv != null && this.uv.Length != 0)
				{
					this.hasUv = true;
				}
				if (this.uv2 != null && this.uv2.Length != 0)
				{
					this.hasUv2 = true;
				}
				if (this.uv3 != null && this.uv3.Length != 0)
				{
					this.hasUv3 = true;
				}
				if (this.uv4 != null && this.uv4.Length != 0)
				{
					this.hasUv4 = true;
				}
				if (this.colors32 != null && this.colors32.Length != 0)
				{
					this.hasColors = true;
				}
			}

			// Token: 0x0600305C RID: 12380 RVA: 0x000CD284 File Offset: 0x000CB484
			public void ResetHasBooleans()
			{
				this.hasNormals = (this.hasTangents = (this.hasUv = (this.hasUv2 = (this.hasUv3 = (this.hasUv4 = (this.hasColors = false))))));
			}

			// Token: 0x0600305D RID: 12381 RVA: 0x000CD2CE File Offset: 0x000CB4CE
			public void Init(bool initTriangles = true)
			{
				this.vertices = new Vector3[65534];
				if (initTriangles)
				{
					this.triangles = new int[786408];
				}
			}

			// Token: 0x0600305E RID: 12382 RVA: 0x000CD2F4 File Offset: 0x000CB4F4
			public void RebuildVertexBuffer(MeshCache.SubMeshCache sub, bool resizeArrays)
			{
				int[] array = new int[sub.vertices.Length];
				int[] array2 = new int[array.Length];
				this.vertexCount = 0;
				for (int i = 0; i < this.triangleCount; i++)
				{
					int num = this.triangles[i];
					if (array[num] == 0)
					{
						array[num] = this.vertexCount + 1;
						array2[this.vertexCount] = num;
						this.triangles[i] = this.vertexCount;
						this.vertexCount++;
					}
					else
					{
						this.triangles[i] = array[num] - 1;
					}
				}
				if (resizeArrays)
				{
					this.vertices = new Vector3[this.vertexCount];
				}
				this.hasNormals = sub.hasNormals;
				this.hasTangents = sub.hasTangents;
				this.hasColors = sub.hasColors;
				this.hasUv = sub.hasUv;
				this.hasUv2 = sub.hasUv2;
				this.hasUv3 = sub.hasUv3;
				this.hasUv4 = sub.hasUv4;
				if (resizeArrays)
				{
					if (this.hasNormals)
					{
						this.normals = new Vector3[this.vertexCount];
					}
					if (this.hasTangents)
					{
						this.tangents = new Vector4[this.vertexCount];
					}
					if (this.hasUv)
					{
						this.uv = new Vector2[this.vertexCount];
					}
					if (this.hasUv2)
					{
						this.uv2 = new Vector2[this.vertexCount];
					}
					if (this.hasUv3)
					{
						this.uv3 = new Vector2[this.vertexCount];
					}
					if (this.hasUv4)
					{
						this.uv4 = new Vector2[this.vertexCount];
					}
					if (this.hasColors)
					{
						this.colors32 = new Color32[this.vertexCount];
					}
				}
				for (int j = 0; j < this.vertexCount; j++)
				{
					int num2 = array2[j];
					this.vertices[j] = sub.vertices[num2];
					if (this.hasNormals)
					{
						this.normals[j] = sub.normals[num2];
					}
					if (this.hasTangents)
					{
						this.tangents[j] = sub.tangents[num2];
					}
					if (this.hasUv)
					{
						this.uv[j] = sub.uv[num2];
					}
					if (this.hasUv2)
					{
						this.uv2[j] = sub.uv2[num2];
					}
					if (this.hasUv3)
					{
						this.uv3[j] = sub.uv3[num2];
					}
					if (this.hasUv4)
					{
						this.uv4[j] = sub.uv4[num2];
					}
					if (this.hasColors)
					{
						this.colors32[j] = sub.colors32[num2];
					}
				}
			}

			// Token: 0x040029DB RID: 10715
			public Vector3[] vertices;

			// Token: 0x040029DC RID: 10716
			public Vector3[] normals;

			// Token: 0x040029DD RID: 10717
			public Vector4[] tangents;

			// Token: 0x040029DE RID: 10718
			public Vector2[] uv;

			// Token: 0x040029DF RID: 10719
			public Vector2[] uv2;

			// Token: 0x040029E0 RID: 10720
			public Vector2[] uv3;

			// Token: 0x040029E1 RID: 10721
			public Vector2[] uv4;

			// Token: 0x040029E2 RID: 10722
			public Color32[] colors32;

			// Token: 0x040029E3 RID: 10723
			public int[] triangles;

			// Token: 0x040029E4 RID: 10724
			public bool hasNormals;

			// Token: 0x040029E5 RID: 10725
			public bool hasTangents;

			// Token: 0x040029E6 RID: 10726
			public bool hasUv;

			// Token: 0x040029E7 RID: 10727
			public bool hasUv2;

			// Token: 0x040029E8 RID: 10728
			public bool hasUv3;

			// Token: 0x040029E9 RID: 10729
			public bool hasUv4;

			// Token: 0x040029EA RID: 10730
			public bool hasColors;

			// Token: 0x040029EB RID: 10731
			public int vertexCount;

			// Token: 0x040029EC RID: 10732
			public int triangleCount;
		}
	}
}
