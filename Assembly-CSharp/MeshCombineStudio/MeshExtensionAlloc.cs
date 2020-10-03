using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x0200049E RID: 1182
	public static class MeshExtensionAlloc
	{
		// Token: 0x060024D2 RID: 9426 RVA: 0x000B4E34 File Offset: 0x000B3034
		public static void ApplyVertices(Mesh mesh, Vector3[] vertices, int length)
		{
			Vector3[] array = new Vector3[length];
			Array.Copy(vertices, array, length);
			mesh.vertices = array;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000B4E58 File Offset: 0x000B3058
		public static void ApplyNormals(Mesh mesh, Vector3[] normals, int length)
		{
			Vector3[] array = new Vector3[length];
			Array.Copy(normals, array, length);
			mesh.normals = array;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000B4E7C File Offset: 0x000B307C
		public static void ApplyTangents(Mesh mesh, Vector4[] tangents, int length)
		{
			Vector4[] array = new Vector4[length];
			Array.Copy(tangents, array, length);
			mesh.tangents = array;
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000B4EA0 File Offset: 0x000B30A0
		public static void ApplyUvs(Mesh mesh, int channel, Vector2[] uvs, int length)
		{
			Vector2[] array = new Vector2[length];
			Array.Copy(uvs, array, length);
			if (channel == 0)
			{
				mesh.uv = array;
				return;
			}
			if (channel == 1)
			{
				mesh.uv2 = array;
				return;
			}
			if (channel == 2)
			{
				mesh.uv3 = array;
				return;
			}
			mesh.uv4 = array;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000B4EE8 File Offset: 0x000B30E8
		public static void ApplyColors32(Mesh mesh, Color32[] colors, int length)
		{
			Color32[] array = new Color32[length];
			Array.Copy(colors, array, length);
			mesh.colors32 = array;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000B4F0C File Offset: 0x000B310C
		public static void ApplyTriangles(Mesh mesh, int[] triangles, int length)
		{
			int[] array = new int[length];
			Array.Copy(triangles, array, length);
			mesh.triangles = array;
		}
	}
}
