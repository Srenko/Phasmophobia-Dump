using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004A2 RID: 1186
	public class RemoveGeometryBelowTerrain : MonoBehaviour
	{
		// Token: 0x06002505 RID: 9477 RVA: 0x000B6B14 File Offset: 0x000B4D14
		private void Start()
		{
			if (this.runOnStart)
			{
				this.Remove(base.gameObject);
			}
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000B6B2C File Offset: 0x000B4D2C
		public void Remove(GameObject go)
		{
			MeshFilter[] componentsInChildren = go.GetComponentsInChildren<MeshFilter>(true);
			this.totalTriangles = 0;
			this.removeTriangles = 0;
			this.skippedObjects = 0;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.RemoveMesh(componentsInChildren[i].transform, componentsInChildren[i].mesh);
			}
			Debug.Log(string.Concat(new object[]
			{
				"Removeable ",
				this.removeTriangles,
				" total ",
				this.totalTriangles,
				" improvement ",
				((float)this.removeTriangles / (float)this.totalTriangles * 100f).ToString("F2")
			}));
			Debug.Log("Skipped Objects " + this.skippedObjects);
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000B6C00 File Offset: 0x000B4E00
		public void RemoveMesh(Transform t, Mesh mesh)
		{
			if (mesh == null)
			{
				return;
			}
			if (!this.IsMeshUnderTerrain(t, mesh))
			{
				this.skippedObjects++;
				return;
			}
			Vector3[] vertices = mesh.vertices;
			List<int> list = new List<int>();
			for (int i = 0; i < mesh.subMeshCount; i++)
			{
				list.AddRange(mesh.GetTriangles(i));
				int count = list.Count;
				this.RemoveTriangles(t, list, vertices);
				if (list.Count < count)
				{
					mesh.SetTriangles(list.ToArray(), i);
				}
			}
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000B6C84 File Offset: 0x000B4E84
		public bool IsMeshUnderTerrain(Transform t, Mesh mesh)
		{
			Bounds bounds = mesh.bounds;
			bounds.center += t.position;
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			Vector2 vector = new Vector2(max.x - min.x, max.z - min.z);
			for (float num = 0f; num < 1f; num += 0.125f)
			{
				for (float num2 = 0f; num2 < 1f; num2 += 0.125f)
				{
					ref Vector3 ptr = new Vector3(min.x + num2 * vector.x, min.y, min.z + num * vector.y);
					float num3 = 0f;
					if (ptr.y < num3)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000B6D58 File Offset: 0x000B4F58
		public void GetTerrainComponents()
		{
			this.terrainComponents = new Terrain[this.terrains.Count];
			for (int i = 0; i < this.terrains.Count; i++)
			{
				Terrain component = this.terrains[i].GetComponent<Terrain>();
				this.terrainComponents[i] = component;
			}
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000B6DAC File Offset: 0x000B4FAC
		public void GetMeshRenderersAndComponents()
		{
			this.mrs = new MeshRenderer[this.meshTerrains.Count];
			this.meshTerrainComponents = new Mesh[this.meshTerrains.Count];
			for (int i = 0; i < this.meshTerrains.Count; i++)
			{
				this.mrs[i] = this.meshTerrains[i].GetComponent<MeshRenderer>();
				MeshFilter component = this.meshTerrains[i].GetComponent<MeshFilter>();
				this.meshTerrainComponents[i] = component.sharedMesh;
			}
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000B6E34 File Offset: 0x000B5034
		public void CreateTerrainBounds()
		{
			this.terrainBounds = new Bounds[this.terrainComponents.Length];
			for (int i = 0; i < this.terrainBounds.Length; i++)
			{
				this.terrainBounds[i] = default(Bounds);
				this.terrainBounds[i].min = this.terrains[i].position;
				this.terrainBounds[i].max = this.terrainBounds[i].min + this.terrainComponents[i].terrainData.size;
			}
			this.meshBounds = new Bounds[this.meshTerrains.Count];
			for (int j = 0; j < this.meshTerrains.Count; j++)
			{
				this.meshBounds[j] = this.mrs[j].bounds;
			}
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000B6F1C File Offset: 0x000B511C
		public void MakeIntersectLists(Bounds bounds)
		{
			List<Terrain> list = new List<Terrain>();
			List<Mesh> list2 = new List<Mesh>();
			List<Bounds> list3 = new List<Bounds>();
			List<Bounds> list4 = new List<Bounds>();
			Vector3[] array = new Vector3[8];
			Vector3 size = bounds.size;
			array[0] = bounds.min;
			array[1] = array[0] + new Vector3(size.x, 0f, 0f);
			array[2] = array[0] + new Vector3(0f, 0f, size.z);
			array[3] = array[0] + new Vector3(size.x, 0f, size.z);
			array[4] = array[0] + new Vector3(0f, size.y, 0f);
			array[5] = array[0] + new Vector3(size.x, size.y, 0f);
			array[6] = array[0] + new Vector3(0f, size.y, size.z);
			array[7] = array[0] + size;
			for (int i = 0; i < 8; i++)
			{
				int num = this.InterectTerrain(array[i]);
				if (num != -1)
				{
					list.Add(this.terrainArray[num]);
					list3.Add(this.terrainBounds[num]);
				}
				num = this.InterectMesh(array[i]);
				if (num != -1)
				{
					list2.Add(this.meshArray[num]);
					list4.Add(this.meshBounds[num]);
				}
			}
			this.terrainArray = list.ToArray();
			this.meshArray = list2.ToArray();
			this.terrainBoundsArray = list3.ToArray();
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000B7124 File Offset: 0x000B5324
		public int InterectTerrain(Vector3 pos)
		{
			for (int i = 0; i < this.terrainBounds.Length; i++)
			{
				if (this.terrainBounds[i].Contains(pos))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000B715C File Offset: 0x000B535C
		public int InterectMesh(Vector3 pos)
		{
			for (int i = 0; i < this.meshBounds.Length; i++)
			{
				if (this.meshBounds[i].Contains(pos))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000B7194 File Offset: 0x000B5394
		public float GetTerrainHeight(Vector3 pos)
		{
			int num = -1;
			for (int i = 0; i < this.terrainArray.Length; i++)
			{
				if (this.terrainBoundsArray[i].Contains(pos))
				{
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				return this.terrainArray[num].SampleHeight(pos);
			}
			return float.PositiveInfinity;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000B71E8 File Offset: 0x000B53E8
		public void RemoveTriangles(Transform t, List<int> newTriangles, Vector3[] vertices)
		{
			bool[] array = new bool[vertices.Length];
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < newTriangles.Count; i += 3)
			{
				this.totalTriangles++;
				int num = newTriangles[i];
				bool flag = array[num];
				if (!flag)
				{
					vector = t.TransformPoint(vertices[num]);
					float terrainHeight = this.GetTerrainHeight(vector);
					flag = (vector.y < terrainHeight);
				}
				if (flag)
				{
					array[num] = true;
					num = newTriangles[i + 1];
					flag = array[num];
					if (!flag)
					{
						vector = t.TransformPoint(vertices[num]);
						float terrainHeight = this.GetTerrainHeight(vector);
						flag = (vector.y < terrainHeight);
					}
					if (flag)
					{
						array[num] = true;
						num = newTriangles[i + 2];
						flag = array[num];
						if (!flag)
						{
							vector = t.TransformPoint(vertices[num]);
							float terrainHeight = this.GetTerrainHeight(vector);
							flag = (vector.y < terrainHeight);
						}
						if (flag)
						{
							array[num] = true;
							this.removeTriangles++;
							newTriangles.RemoveAt(i + 2);
							newTriangles.RemoveAt(i + 1);
							newTriangles.RemoveAt(i);
							if (i + 3 < newTriangles.Count)
							{
								i -= 3;
							}
						}
					}
				}
			}
		}

		// Token: 0x04002263 RID: 8803
		private int totalTriangles;

		// Token: 0x04002264 RID: 8804
		private int removeTriangles;

		// Token: 0x04002265 RID: 8805
		private int skippedObjects;

		// Token: 0x04002266 RID: 8806
		public List<Transform> terrains = new List<Transform>();

		// Token: 0x04002267 RID: 8807
		public List<Transform> meshTerrains = new List<Transform>();

		// Token: 0x04002268 RID: 8808
		public Bounds[] terrainBounds;

		// Token: 0x04002269 RID: 8809
		public Bounds[] meshBounds;

		// Token: 0x0400226A RID: 8810
		private Terrain[] terrainComponents;

		// Token: 0x0400226B RID: 8811
		private Terrain[] terrainArray;

		// Token: 0x0400226C RID: 8812
		private Bounds[] terrainBoundsArray;

		// Token: 0x0400226D RID: 8813
		private MeshRenderer[] mrs;

		// Token: 0x0400226E RID: 8814
		private Mesh[] meshTerrainComponents;

		// Token: 0x0400226F RID: 8815
		private Mesh[] meshArray;

		// Token: 0x04002270 RID: 8816
		public bool runOnStart;
	}
}
