using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004BB RID: 1211
	public class ObjectOctree
	{
		// Token: 0x020007B6 RID: 1974
		public class LODParent
		{
			// Token: 0x06003073 RID: 12403 RVA: 0x000CDB1C File Offset: 0x000CBD1C
			public LODParent(int lodCount)
			{
				this.lodLevels = new ObjectOctree.LODLevel[lodCount];
				for (int i = 0; i < this.lodLevels.Length; i++)
				{
					this.lodLevels[i] = new ObjectOctree.LODLevel();
				}
			}

			// Token: 0x06003074 RID: 12404 RVA: 0x000CDB5C File Offset: 0x000CBD5C
			public void AssignLODGroup(MeshCombiner meshCombiner)
			{
				LOD[] array = new LOD[this.lodLevels.Length];
				int num = array.Length - 1;
				for (int i = 0; i < this.lodLevels.Length; i++)
				{
					ObjectOctree.LODLevel lodlevel = this.lodLevels[i];
					LOD[] array2 = array;
					int num2 = i;
					float screenRelativeTransitionHeight = meshCombiner.lodGroupsSettings[num].lodSettings[i].screenRelativeTransitionHeight;
					Renderer[] renderers = lodlevel.newMeshRenderers.ToArray();
					array2[num2] = new LOD(screenRelativeTransitionHeight, renderers);
				}
				this.lodGroup.SetLODs(array);
				this.lodGroup.size = (float)meshCombiner.cellSize;
			}

			// Token: 0x06003075 RID: 12405 RVA: 0x000CDBE8 File Offset: 0x000CBDE8
			public void ApplyChanges(MeshCombiner meshCombiner)
			{
				for (int i = 0; i < this.lodLevels.Length; i++)
				{
					this.lodLevels[i].ApplyChanges(meshCombiner);
				}
				this.hasChanged = false;
			}

			// Token: 0x04002A31 RID: 10801
			public GameObject cellGO;

			// Token: 0x04002A32 RID: 10802
			public Transform cellT;

			// Token: 0x04002A33 RID: 10803
			public LODGroup lodGroup;

			// Token: 0x04002A34 RID: 10804
			public ObjectOctree.LODLevel[] lodLevels;

			// Token: 0x04002A35 RID: 10805
			public bool hasChanged;

			// Token: 0x04002A36 RID: 10806
			public int jobsPending;
		}

		// Token: 0x020007B7 RID: 1975
		public class LODLevel
		{
			// Token: 0x06003076 RID: 12406 RVA: 0x000CDC20 File Offset: 0x000CBE20
			public void ApplyChanges(MeshCombiner meshCombiner)
			{
				for (int i = 0; i < this.changedMeshObjectsHolders.Count; i++)
				{
					this.changedMeshObjectsHolders.items[i].hasChanged = false;
				}
				this.changedMeshObjectsHolders.Clear();
			}

			// Token: 0x04002A37 RID: 10807
			public FastList<CachedGameObject> cachedGOs = new FastList<CachedGameObject>();

			// Token: 0x04002A38 RID: 10808
			public Dictionary<CombineCondition, MeshObjectsHolder> meshObjectsHoldersLookup;

			// Token: 0x04002A39 RID: 10809
			public FastList<MeshObjectsHolder> changedMeshObjectsHolders;

			// Token: 0x04002A3A RID: 10810
			public FastList<MeshRenderer> newMeshRenderers = new FastList<MeshRenderer>();

			// Token: 0x04002A3B RID: 10811
			public int vertCount;

			// Token: 0x04002A3C RID: 10812
			public int objectCount;
		}

		// Token: 0x020007B8 RID: 1976
		public class MaxCell : ObjectOctree.Cell
		{
			// Token: 0x06003078 RID: 12408 RVA: 0x000CDC80 File Offset: 0x000CBE80
			public void ApplyChanges(MeshCombiner meshCombiner)
			{
				for (int i = 0; i < this.changedLodParents.Count; i++)
				{
					this.changedLodParents[i].ApplyChanges(meshCombiner);
				}
				this.changedLodParents.Clear();
				this.hasChanged = false;
			}

			// Token: 0x04002A3D RID: 10813
			public static int maxCellCount;

			// Token: 0x04002A3E RID: 10814
			public ObjectOctree.LODParent[] lodParents;

			// Token: 0x04002A3F RID: 10815
			public List<ObjectOctree.LODParent> changedLodParents;

			// Token: 0x04002A40 RID: 10816
			public bool hasChanged;
		}

		// Token: 0x020007B9 RID: 1977
		public class Cell : BaseOctree.Cell
		{
			// Token: 0x0600307A RID: 12410 RVA: 0x000CB56E File Offset: 0x000C976E
			public Cell()
			{
			}

			// Token: 0x0600307B RID: 12411 RVA: 0x000CB576 File Offset: 0x000C9776
			public Cell(Vector3 position, Vector3 size, int maxLevels) : base(position, size, maxLevels)
			{
			}

			// Token: 0x0600307C RID: 12412 RVA: 0x000CDCCF File Offset: 0x000CBECF
			public CachedGameObject AddObject(Vector3 position, MeshCombiner meshCombiner, CachedGameObject cachedGO, int lodParentIndex, int lodLevel, bool isChangeMode = false)
			{
				if (base.InsideBounds(position))
				{
					this.AddObjectInternal(meshCombiner, cachedGO, position, lodParentIndex, lodLevel, isChangeMode);
					return cachedGO;
				}
				return null;
			}

			// Token: 0x0600307D RID: 12413 RVA: 0x000CDCEC File Offset: 0x000CBEEC
			private void AddObjectInternal(MeshCombiner meshCombiner, CachedGameObject cachedGO, Vector3 position, int lodParentIndex, int lodLevel, bool isChangeMode)
			{
				if (this.level == this.maxLevels)
				{
					ObjectOctree.MaxCell maxCell = (ObjectOctree.MaxCell)this;
					if (maxCell.lodParents == null)
					{
						maxCell.lodParents = new ObjectOctree.LODParent[10];
					}
					if (maxCell.lodParents[lodParentIndex] == null)
					{
						maxCell.lodParents[lodParentIndex] = new ObjectOctree.LODParent(lodParentIndex + 1);
					}
					ObjectOctree.LODParent lodparent = maxCell.lodParents[lodParentIndex];
					ObjectOctree.LODLevel lodlevel = lodparent.lodLevels[lodLevel];
					lodlevel.cachedGOs.Add(cachedGO);
					if (isChangeMode && this.SortObject(meshCombiner, lodlevel, cachedGO, false))
					{
						if (!maxCell.hasChanged)
						{
							maxCell.hasChanged = true;
							if (meshCombiner.changedCells == null)
							{
								meshCombiner.changedCells = new List<ObjectOctree.MaxCell>();
							}
							meshCombiner.changedCells.Add(maxCell);
						}
						if (!lodparent.hasChanged)
						{
							lodparent.hasChanged = true;
							maxCell.changedLodParents.Add(lodparent);
						}
					}
					lodlevel.objectCount++;
					lodlevel.vertCount += cachedGO.mesh.vertexCount;
					return;
				}
				bool flag;
				int num = base.AddCell<ObjectOctree.Cell, ObjectOctree.MaxCell>(ref this.cells, position, out flag);
				if (flag)
				{
					ObjectOctree.MaxCell.maxCellCount++;
				}
				this.cells[num].AddObjectInternal(meshCombiner, cachedGO, position, lodParentIndex, lodLevel, isChangeMode);
			}

			// Token: 0x0600307E RID: 12414 RVA: 0x000CDE1C File Offset: 0x000CC01C
			public void SortObjects(MeshCombiner meshCombiner)
			{
				if (this.level == this.maxLevels)
				{
					foreach (ObjectOctree.LODParent lodparent in ((ObjectOctree.MaxCell)this).lodParents)
					{
						if (lodparent != null)
						{
							for (int j = 0; j < lodparent.lodLevels.Length; j++)
							{
								ObjectOctree.LODLevel lodlevel = lodparent.lodLevels[j];
								if (lodlevel == null || lodlevel.cachedGOs.Count == 0)
								{
									return;
								}
								for (int k = 0; k < lodlevel.cachedGOs.Count; k++)
								{
									CachedGameObject cachedGO = lodlevel.cachedGOs.items[k];
									if (!this.SortObject(meshCombiner, lodlevel, cachedGO, false))
									{
										lodlevel.cachedGOs.RemoveAt(k--);
									}
								}
							}
						}
					}
					return;
				}
				for (int l = 0; l < 8; l++)
				{
					if (this.cellsUsed[l])
					{
						this.cells[l].SortObjects(meshCombiner);
					}
				}
			}

			// Token: 0x0600307F RID: 12415 RVA: 0x000CDF0C File Offset: 0x000CC10C
			public bool SortObject(MeshCombiner meshCombiner, ObjectOctree.LODLevel lod, CachedGameObject cachedGO, bool isChangeMode = false)
			{
				if (cachedGO.mr == null)
				{
					return false;
				}
				if (lod.meshObjectsHoldersLookup == null)
				{
					lod.meshObjectsHoldersLookup = new Dictionary<CombineCondition, MeshObjectsHolder>();
				}
				CombineConditionSettings combineConditionSettings = meshCombiner.combineConditionSettings;
				Material[] sharedMaterials = cachedGO.mr.sharedMaterials;
				int num = Mathf.Min(cachedGO.mesh.subMeshCount, sharedMaterials.Length);
				int i = 0;
				while (i < num)
				{
					Material material;
					if (!combineConditionSettings.sameMaterial)
					{
						material = combineConditionSettings.material;
						goto IL_75;
					}
					material = sharedMaterials[i];
					if (!(material == null))
					{
						goto IL_75;
					}
					IL_119:
					i++;
					continue;
					IL_75:
					CombineCondition combineCondition = default(CombineCondition);
					combineCondition.ReadFromGameObject(combineConditionSettings, meshCombiner.copyBakedLighting && meshCombiner.validCopyBakedLighting, cachedGO.go, cachedGO.mr, material);
					MeshObjectsHolder meshObjectsHolder;
					if (!lod.meshObjectsHoldersLookup.TryGetValue(combineCondition, out meshObjectsHolder))
					{
						meshCombiner.foundCombineConditions.combineConditions.Add(combineCondition);
						meshObjectsHolder = new MeshObjectsHolder(ref combineCondition, material);
						lod.meshObjectsHoldersLookup.Add(combineCondition, meshObjectsHolder);
					}
					meshObjectsHolder.meshObjects.Add(new MeshObject(cachedGO, i));
					if (isChangeMode && !meshObjectsHolder.hasChanged)
					{
						meshObjectsHolder.hasChanged = true;
						lod.changedMeshObjectsHolders.Add(meshObjectsHolder);
						goto IL_119;
					}
					goto IL_119;
				}
				return true;
			}

			// Token: 0x06003080 RID: 12416 RVA: 0x000CE040 File Offset: 0x000CC240
			public void CombineMeshes(MeshCombiner meshCombiner, int lodParentIndex)
			{
				if (this.level != this.maxLevels)
				{
					for (int i = 0; i < 8; i++)
					{
						if (this.cellsUsed[i])
						{
							this.cells[i].CombineMeshes(meshCombiner, lodParentIndex);
						}
					}
					return;
				}
				ObjectOctree.LODParent lodparent = ((ObjectOctree.MaxCell)this).lodParents[lodParentIndex];
				if (lodparent == null)
				{
					return;
				}
				lodparent.cellGO = new GameObject(meshCombiner.useCells ? ("Cell " + this.bounds.center) : "Combined Objects");
				lodparent.cellT = lodparent.cellGO.transform;
				lodparent.cellT.position = this.bounds.center;
				lodparent.cellT.parent = meshCombiner.lodParentHolders[lodParentIndex].t;
				if (lodParentIndex > 0)
				{
					lodparent.lodGroup = lodparent.cellGO.AddComponent<LODGroup>();
					lodparent.lodGroup.localReferencePoint = (lodparent.cellT.position = this.bounds.center);
				}
				ObjectOctree.LODLevel[] lodLevels = lodparent.lodLevels;
				for (int j = 0; j < lodLevels.Length; j++)
				{
					ObjectOctree.LODLevel lodlevel = lodparent.lodLevels[j];
					if (lodlevel == null || lodlevel.meshObjectsHoldersLookup == null)
					{
						return;
					}
					Transform transform = null;
					if (lodParentIndex > 0)
					{
						transform = new GameObject("LOD" + j).transform;
						transform.parent = lodparent.cellT;
					}
					foreach (MeshObjectsHolder meshObjectsHolder in lodlevel.meshObjectsHoldersLookup.Values)
					{
						meshObjectsHolder.lodParent = lodparent;
						meshObjectsHolder.lodLevel = j;
						MeshCombineJobManager.instance.AddJob(meshCombiner, meshObjectsHolder, (lodParentIndex > 0) ? transform : lodparent.cellT, this.bounds.center);
					}
				}
			}

			// Token: 0x06003081 RID: 12417 RVA: 0x000CE228 File Offset: 0x000CC428
			public void Draw(MeshCombiner meshCombiner, bool onlyMaxLevel, bool drawLevel0)
			{
				if (!onlyMaxLevel || this.level == this.maxLevels || (drawLevel0 && this.level == 0))
				{
					Gizmos.DrawWireCube(this.bounds.center, this.bounds.size);
					if (this.level == this.maxLevels && meshCombiner.drawMeshBounds)
					{
						ObjectOctree.LODParent[] lodParents = ((ObjectOctree.MaxCell)this).lodParents;
						for (int i = 0; i < lodParents.Length; i++)
						{
							if (lodParents[i] != null)
							{
								ObjectOctree.LODLevel[] lodLevels = lodParents[i].lodLevels;
								Gizmos.color = (meshCombiner.activeOriginal ? Color.blue : Color.green);
								for (int j = 0; j < lodLevels.Length; j++)
								{
									for (int k = 0; k < lodLevels[j].cachedGOs.Count; k++)
									{
										if (!(lodLevels[j].cachedGOs.items[k].mr == null))
										{
											Bounds bounds = lodLevels[j].cachedGOs.items[k].mr.bounds;
											Gizmos.DrawWireCube(bounds.center, bounds.size);
										}
									}
								}
								Gizmos.color = Color.white;
							}
						}
						return;
					}
				}
				if (this.cells == null || this.cellsUsed == null)
				{
					return;
				}
				for (int l = 0; l < 8; l++)
				{
					if (this.cellsUsed[l])
					{
						this.cells[l].Draw(meshCombiner, onlyMaxLevel, drawLevel0);
					}
				}
			}

			// Token: 0x04002A41 RID: 10817
			public ObjectOctree.Cell[] cells;
		}
	}
}
