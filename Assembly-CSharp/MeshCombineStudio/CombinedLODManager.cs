using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x0200049B RID: 1179
	public class CombinedLODManager : MonoBehaviour
	{
		// Token: 0x060024B5 RID: 9397 RVA: 0x000B3F2C File Offset: 0x000B212C
		private void Awake()
		{
			this.cameraMainT = Camera.main.transform;
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000B3F3E File Offset: 0x000B213E
		private void InitOctree()
		{
			this.octree = new CombinedLODManager.Cell(this.octreeCenter, this.octreeSize, this.maxLevels);
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000B3F5D File Offset: 0x000B215D
		private void Start()
		{
			if (this.search)
			{
				this.search = false;
				this.InitOctree();
				this.Search();
			}
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000B3F7A File Offset: 0x000B217A
		private void Update()
		{
			if (this.octree.cellsUsed != null)
			{
				this.Lod(this.lodMode);
			}
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000B3F98 File Offset: 0x000B2198
		public void UpdateLods(MeshCombiner meshCombiner, int lodAmount)
		{
			if (this.lods != null && this.lods.Length == lodAmount)
			{
				return;
			}
			this.lods = new CombinedLODManager.LOD[lodAmount];
			float[] array = new float[lodAmount];
			for (int i = 0; i < this.lods.Length; i++)
			{
				this.lods[i] = new CombinedLODManager.LOD();
				if (this.lodDistanceMode == CombinedLODManager.LodDistanceMode.Automatic)
				{
					array[i] = (float)(meshCombiner.cellSize * i);
				}
				else if (this.distances != null && i < this.distances.Length)
				{
					array[i] = this.distances[i];
				}
			}
			this.distances = array;
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000B4028 File Offset: 0x000B2228
		public void UpdateDistances(MeshCombiner meshCombiner)
		{
			if (this.lodDistanceMode != CombinedLODManager.LodDistanceMode.Automatic)
			{
				return;
			}
			for (int i = 0; i < this.distances.Length; i++)
			{
				this.distances[i] = (float)(meshCombiner.cellSize * i);
			}
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000B4064 File Offset: 0x000B2264
		public void Search()
		{
			for (int i = 0; i < this.lods.Length; i++)
			{
				this.lods[i].searchParent.gameObject.SetActive(true);
				MeshRenderer[] componentsInChildren = this.lods[i].searchParent.GetComponentsInChildren<MeshRenderer>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					this.octree.AddMeshRenderer(componentsInChildren[j], componentsInChildren[j].transform.position, i, this.lods.Length);
				}
			}
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000B40E4 File Offset: 0x000B22E4
		public void ResetOctree()
		{
			if (this.octree == null)
			{
				return;
			}
			this.octree.cells = null;
			this.octree.cellsUsed = null;
			for (int i = 0; i < this.lods.Length; i++)
			{
				if (this.lods[i].searchParent != null)
				{
					Object.Destroy(this.lods[i].searchParent.gameObject);
				}
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000B4154 File Offset: 0x000B2354
		public void Lod(CombinedLODManager.LodMode lodMode)
		{
			Vector3 position = this.cameraMainT.position;
			for (int i = 0; i < this.lods.Length - 1; i++)
			{
				this.lods[i].sphere.center = position;
				this.lods[i].sphere.radius = this.distances[i + 1];
			}
			if (lodMode == CombinedLODManager.LodMode.Automatic)
			{
				this.octree.AutoLodInternal(this.lods, this.lodCulled ? this.lodCullDistance : -1f);
				return;
			}
			this.octree.LodInternal(this.lods, this.showLod);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000B41F2 File Offset: 0x000B23F2
		private void OnDrawGizmosSelected()
		{
			if (this.drawGizmos && this.octree != null && this.octree.cells != null)
			{
				this.octree.DrawGizmos(this.lods);
			}
		}

		// Token: 0x040021DC RID: 8668
		public bool drawGizmos = true;

		// Token: 0x040021DD RID: 8669
		public CombinedLODManager.LOD[] lods;

		// Token: 0x040021DE RID: 8670
		public float[] distances;

		// Token: 0x040021DF RID: 8671
		public CombinedLODManager.LodDistanceMode lodDistanceMode;

		// Token: 0x040021E0 RID: 8672
		public CombinedLODManager.LodMode lodMode;

		// Token: 0x040021E1 RID: 8673
		public int showLod;

		// Token: 0x040021E2 RID: 8674
		public bool lodCulled;

		// Token: 0x040021E3 RID: 8675
		public float lodCullDistance = 500f;

		// Token: 0x040021E4 RID: 8676
		public Vector3 octreeCenter = Vector3.zero;

		// Token: 0x040021E5 RID: 8677
		public Vector3 octreeSize = new Vector3(256f, 256f, 256f);

		// Token: 0x040021E6 RID: 8678
		public int maxLevels = 4;

		// Token: 0x040021E7 RID: 8679
		public bool search = true;

		// Token: 0x040021E8 RID: 8680
		private CombinedLODManager.Cell octree;

		// Token: 0x040021E9 RID: 8681
		private Transform cameraMainT;

		// Token: 0x0200079C RID: 1948
		public enum LodMode
		{
			// Token: 0x040029A5 RID: 10661
			Automatic,
			// Token: 0x040029A6 RID: 10662
			DebugLod
		}

		// Token: 0x0200079D RID: 1949
		public enum LodDistanceMode
		{
			// Token: 0x040029A8 RID: 10664
			Automatic,
			// Token: 0x040029A9 RID: 10665
			Manual
		}

		// Token: 0x0200079E RID: 1950
		[Serializable]
		public class LOD
		{
			// Token: 0x0600303B RID: 12347 RVA: 0x00008842 File Offset: 0x00006A42
			public LOD()
			{
			}

			// Token: 0x0600303C RID: 12348 RVA: 0x000CB55F File Offset: 0x000C975F
			public LOD(Transform searchParent)
			{
				this.searchParent = searchParent;
			}

			// Token: 0x040029AA RID: 10666
			public Transform searchParent;

			// Token: 0x040029AB RID: 10667
			public Sphere3 sphere;
		}

		// Token: 0x0200079F RID: 1951
		public class Cell : BaseOctree.Cell
		{
			// Token: 0x0600303D RID: 12349 RVA: 0x000CB56E File Offset: 0x000C976E
			public Cell()
			{
			}

			// Token: 0x0600303E RID: 12350 RVA: 0x000CB576 File Offset: 0x000C9776
			public Cell(Vector3 position, Vector3 size, int maxLevels) : base(position, size, maxLevels)
			{
			}

			// Token: 0x0600303F RID: 12351 RVA: 0x000CB581 File Offset: 0x000C9781
			public void AddMeshRenderer(MeshRenderer mr, Vector3 position, int lodLevel, int lodLevels)
			{
				if (base.InsideBounds(position))
				{
					this.AddMeshRendererInternal(mr, position, lodLevel, lodLevels);
				}
			}

			// Token: 0x06003040 RID: 12352 RVA: 0x000CB598 File Offset: 0x000C9798
			private void AddMeshRendererInternal(MeshRenderer mr, Vector3 position, int lodLevel, int lodLevels)
			{
				if (this.level == this.maxLevels)
				{
					CombinedLODManager.MaxCell maxCell = (CombinedLODManager.MaxCell)this;
					if (maxCell.mrList == null)
					{
						maxCell.mrList = new List<MeshRenderer>[lodLevels];
					}
					List<MeshRenderer>[] mrList = maxCell.mrList;
					if (mrList[lodLevel] == null)
					{
						mrList[lodLevel] = new List<MeshRenderer>();
					}
					mrList[lodLevel].Add(mr);
					maxCell.currentLod = -1;
					return;
				}
				bool flag;
				int num = base.AddCell<CombinedLODManager.Cell, CombinedLODManager.MaxCell>(ref this.cells, position, out flag);
				this.cells[num].box = new AABB3(this.cells[num].bounds.min, this.cells[num].bounds.max);
				this.cells[num].AddMeshRendererInternal(mr, position, lodLevel, lodLevels);
			}

			// Token: 0x06003041 RID: 12353 RVA: 0x000CB64C File Offset: 0x000C984C
			public void AutoLodInternal(CombinedLODManager.LOD[] lods, float lodCulledDistance)
			{
				if (this.level == this.maxLevels)
				{
					CombinedLODManager.MaxCell maxCell = (CombinedLODManager.MaxCell)this;
					if (lodCulledDistance != -1f && (this.bounds.center - lods[0].sphere.center).sqrMagnitude > lodCulledDistance * lodCulledDistance)
					{
						if (maxCell.currentLod != -1)
						{
							for (int i = 0; i < lods.Length; i++)
							{
								for (int j = 0; j < maxCell.mrList[i].Count; j++)
								{
									maxCell.mrList[i][j].enabled = false;
								}
							}
							maxCell.currentLod = -1;
						}
						return;
					}
					int k = 0;
					while (k < lods.Length)
					{
						bool flag = k >= lods.Length - 1 || Mathw.IntersectAABB3Sphere3(this.box, lods[k].sphere);
						if (flag)
						{
							if (maxCell.currentLod != k)
							{
								for (int l = 0; l < lods.Length; l++)
								{
									bool enabled = l == k;
									for (int m = 0; m < maxCell.mrList[l].Count; m++)
									{
										maxCell.mrList[l][m].enabled = enabled;
									}
								}
								maxCell.currentLod = k;
								return;
							}
							return;
						}
						else
						{
							k++;
						}
					}
					return;
				}
				else
				{
					for (int n = 0; n < 8; n++)
					{
						if (this.cellsUsed[n])
						{
							this.cells[n].AutoLodInternal(lods, lodCulledDistance);
						}
					}
				}
			}

			// Token: 0x06003042 RID: 12354 RVA: 0x000CB7C0 File Offset: 0x000C99C0
			public void LodInternal(CombinedLODManager.LOD[] lods, int lodLevel)
			{
				if (this.level == this.maxLevels)
				{
					CombinedLODManager.MaxCell maxCell = (CombinedLODManager.MaxCell)this;
					if (maxCell.currentLod != lodLevel)
					{
						for (int i = 0; i < lods.Length; i++)
						{
							bool enabled = i == lodLevel;
							for (int j = 0; j < maxCell.mrList[i].Count; j++)
							{
								maxCell.mrList[i][j].enabled = enabled;
							}
						}
						maxCell.currentLod = lodLevel;
						return;
					}
				}
				else
				{
					for (int k = 0; k < 8; k++)
					{
						if (this.cellsUsed[k])
						{
							this.cells[k].LodInternal(lods, lodLevel);
						}
					}
				}
			}

			// Token: 0x06003043 RID: 12355 RVA: 0x000CB860 File Offset: 0x000C9A60
			public void DrawGizmos(CombinedLODManager.LOD[] lods)
			{
				for (int i = 0; i < lods.Length; i++)
				{
					if (i == 0)
					{
						Gizmos.color = Color.red;
					}
					else if (i == 1)
					{
						Gizmos.color = Color.green;
					}
					else if (i == 2)
					{
						Gizmos.color = Color.yellow;
					}
					else if (i == 3)
					{
						Gizmos.color = Color.blue;
					}
					Gizmos.DrawWireSphere(lods[i].sphere.center, lods[i].sphere.radius);
				}
				this.DrawGizmosInternal();
			}

			// Token: 0x06003044 RID: 12356 RVA: 0x000CB8E0 File Offset: 0x000C9AE0
			public void DrawGizmosInternal()
			{
				if (this.level == this.maxLevels)
				{
					CombinedLODManager.MaxCell maxCell = (CombinedLODManager.MaxCell)this;
					if (maxCell.currentLod == 0)
					{
						Gizmos.color = Color.red;
					}
					else if (maxCell.currentLod == 1)
					{
						Gizmos.color = Color.green;
					}
					else if (maxCell.currentLod == 2)
					{
						Gizmos.color = Color.yellow;
					}
					else if (maxCell.currentLod == 3)
					{
						Gizmos.color = Color.blue;
					}
					Gizmos.DrawWireCube(this.bounds.center, this.bounds.size - new Vector3(0.25f, 0.25f, 0.25f));
					Gizmos.color = Color.white;
					return;
				}
				for (int i = 0; i < 8; i++)
				{
					if (this.cellsUsed[i])
					{
						this.cells[i].DrawGizmosInternal();
					}
				}
			}

			// Token: 0x040029AC RID: 10668
			public CombinedLODManager.Cell[] cells;

			// Token: 0x040029AD RID: 10669
			private AABB3 box;
		}

		// Token: 0x020007A0 RID: 1952
		public class MaxCell : CombinedLODManager.Cell
		{
			// Token: 0x040029AE RID: 10670
			public List<MeshRenderer>[] mrList;

			// Token: 0x040029AF RID: 10671
			public int currentLod;
		}
	}
}
