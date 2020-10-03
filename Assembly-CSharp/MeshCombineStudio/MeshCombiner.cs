using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x0200049F RID: 1183
	[ExecuteInEditMode]
	public class MeshCombiner : MonoBehaviour
	{
		// Token: 0x140000CE RID: 206
		// (add) Token: 0x060024D8 RID: 9432 RVA: 0x000B4F30 File Offset: 0x000B3130
		// (remove) Token: 0x060024D9 RID: 9433 RVA: 0x000B4F68 File Offset: 0x000B3168
		public event MeshCombiner.DefaultMethod OnCombiningReady;

		// Token: 0x060024DA RID: 9434 RVA: 0x000B4FA0 File Offset: 0x000B31A0
		public void AddMeshColliders()
		{
			for (int i = 0; i < this.addMeshCollidersList.Count; i++)
			{
				MeshColliderAdd meshColliderAdd = this.addMeshCollidersList.items[i];
				meshColliderAdd.go.AddComponent<MeshCollider>().sharedMesh = meshColliderAdd.mesh;
			}
			this.addMeshCollidersList.Clear();
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000B4FF6 File Offset: 0x000B31F6
		public void ExecuteOnCombiningReady()
		{
			if (this.OnCombiningReady != null)
			{
				this.OnCombiningReady();
			}
			this.totalMeshCombineJobs = 0;
			this.stopwatch.Stop();
			this.combineTime = (float)this.stopwatch.ElapsedMilliseconds / 1000f;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000B5035 File Offset: 0x000B3235
		private void Awake()
		{
			MeshCombiner.instances.Add(this);
			this.thisInstance = this;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000B5049 File Offset: 0x000B3249
		private void OnEnable()
		{
			if (this.thisInstance == null)
			{
				MeshCombiner.instances.Add(this);
				this.thisInstance = this;
			}
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000B506C File Offset: 0x000B326C
		private void Start()
		{
			if (Application.isPlaying && !this.combineInRuntime)
			{
				return;
			}
			this.InitMeshCombineJobManager();
			if (MeshCombiner.instances[0] == this)
			{
				MeshCombineJobManager.instance.SetJobMode(this.jobSettings);
			}
			if (!Application.isPlaying && Application.isEditor)
			{
				return;
			}
			this.StartRuntime();
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000B50C8 File Offset: 0x000B32C8
		private void OnDestroy()
		{
			this.RestoreOriginalRenderersAndLODGroups();
			this.thisInstance = null;
			MeshCombiner.instances.Remove(this);
			if (MeshCombiner.instances.Count == 0 && MeshCombineJobManager.instance != null)
			{
				Methods.Destroy(MeshCombineJobManager.instance.gameObject);
				MeshCombineJobManager.instance = null;
			}
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000B511C File Offset: 0x000B331C
		public static MeshCombiner GetInstance(string name)
		{
			for (int i = 0; i < MeshCombiner.instances.Count; i++)
			{
				if (MeshCombiner.instances[i].gameObject.name == name)
				{
					return MeshCombiner.instances[i];
				}
			}
			return null;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000B5168 File Offset: 0x000B3368
		public void CopyJobSettingsToAllInstances()
		{
			for (int i = 0; i < MeshCombiner.instances.Count; i++)
			{
				MeshCombiner.instances[i].jobSettings.CopySettings(this.jobSettings);
			}
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000B51A5 File Offset: 0x000B33A5
		public void InitMeshCombineJobManager()
		{
			if (MeshCombineJobManager.instance == null)
			{
				MeshCombineJobManager.CreateInstance(this, this.instantiatePrefab);
			}
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000B51C4 File Offset: 0x000B33C4
		public void CreateLodGroupsSettings()
		{
			this.lodGroupsSettings = new MeshCombiner.LODGroupSettings[8];
			for (int i = 0; i < this.lodGroupsSettings.Length; i++)
			{
				this.lodGroupsSettings[i] = new MeshCombiner.LODGroupSettings(i);
			}
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000B5200 File Offset: 0x000B3400
		private void StartRuntime()
		{
			if (this.combineInRuntime)
			{
				if (this.combineOnStart)
				{
					this.CombineAll();
				}
				if (this.useCombineSwapKey && this.originalMeshRenderers == MeshCombiner.HandleComponent.Disable && this.originalLODGroups == MeshCombiner.HandleComponent.Disable)
				{
					if (SwapCombineKey.instance == null)
					{
						base.gameObject.AddComponent<SwapCombineKey>();
						return;
					}
					SwapCombineKey.instance.meshCombinerList.Add(this);
				}
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000B5265 File Offset: 0x000B3465
		public void DestroyCombinedObjects()
		{
			this.AbortAndClearMeshCombineJobs();
			this.RestoreOriginalRenderersAndLODGroups();
			Methods.DestroyChildren(base.transform);
			this.combined = false;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000B5288 File Offset: 0x000B3488
		private void Reset()
		{
			this.DestroyCombinedObjects();
			this.foundColliders.Clear();
			this.foundObjects.Clear();
			this.foundLodGroups.Clear();
			this.uniqueLodObjects.Clear();
			this.uniqueFoundLodGroups.Clear();
			this.foundLodObjects.Clear();
			this.unreadableMeshes.Clear();
			this.foundCombineConditions.combineConditions.Clear();
			this.ResetOctree();
			this.hasFoundFirstObject = false;
			this.bounds.center = (this.bounds.size = Vector3.zero);
			if (this.searchOptions.useSearchBox)
			{
				this.searchOptions.GetSearchBoxBounds();
			}
			this.InitAndResetLodParentsCount();
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000B5344 File Offset: 0x000B3544
		public void AbortAndClearMeshCombineJobs()
		{
			foreach (MeshCombineJobManager.MeshCombineJob meshCombineJob in this.meshCombineJobs)
			{
				meshCombineJob.abort = true;
			}
			this.ClearMeshCombineJobs();
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000B539C File Offset: 0x000B359C
		public void ClearMeshCombineJobs()
		{
			this.meshCombineJobs.Clear();
			this.totalMeshCombineJobs = 0;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000B53B0 File Offset: 0x000B35B0
		public void AddObjects(List<Transform> transforms, bool useSearchOptions, bool checkForLODGroups = true)
		{
			List<LODGroup> list = new List<LODGroup>();
			if (checkForLODGroups)
			{
				for (int i = 0; i < transforms.Count; i++)
				{
					LODGroup component = transforms[i].GetComponent<LODGroup>();
					if (component != null)
					{
						list.Add(component);
					}
				}
				if (list.Count > 0)
				{
					this.AddLodGroups(list.ToArray(), useSearchOptions);
				}
			}
			this.AddTransforms(transforms.ToArray(), useSearchOptions);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000B5418 File Offset: 0x000B3618
		public void AddObjectsAutomatically()
		{
			this.Reset();
			this.AddObjectsFromSearchParent();
			this.AddFoundObjectsToOctree();
			if (this.octreeContainsObjects)
			{
				this.octree.SortObjects(this);
				CombineCondition.MakeFoundReport(this.foundCombineConditions);
			}
			if (Console.instance != null)
			{
				this.LogOctreeInfo();
			}
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000B546C File Offset: 0x000B366C
		public void AddFoundObjectsToOctree()
		{
			if (this.foundObjects.Count > 0 || this.foundLodObjects.Count > 0)
			{
				this.octreeContainsObjects = true;
				this.CalcOctreeSize(this.bounds);
				ObjectOctree.MaxCell.maxCellCount = 0;
				for (int i = 0; i < this.foundObjects.Count; i++)
				{
					CachedGameObject cachedGameObject = this.foundObjects[i];
					Vector3 position = (this.searchOptions.objectCenter == MeshCombiner.ObjectCenter.TransformPosition) ? cachedGameObject.t.position : cachedGameObject.mr.bounds.center;
					this.octree.AddObject(position, this, cachedGameObject, 0, 0, false);
				}
				for (int j = 0; j < this.foundLodObjects.Count; j++)
				{
					CachedLodGameObject cachedLodGameObject = this.foundLodObjects[j];
					this.octree.AddObject(cachedLodGameObject.center, this, cachedLodGameObject, cachedLodGameObject.lodCount, cachedLodGameObject.lodLevel, false);
				}
				return;
			}
			Debug.Log("No matching GameObjects with chosen search options are found for combining.");
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000B5570 File Offset: 0x000B3770
		public void ResetOctree()
		{
			this.octreeContainsObjects = false;
			if (this.octree == null)
			{
				this.octree = new ObjectOctree.Cell();
				return;
			}
			BaseOctree.Cell[] cells = this.octree.cells;
			BaseOctree.Cell[] array = cells;
			this.octree.Reset(ref array);
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000B55B4 File Offset: 0x000B37B4
		public void CalcOctreeSize(Bounds bounds)
		{
			Methods.SnapBoundsAndPreserveArea(ref bounds, (float)this.cellSize, this.useCells ? this.cellOffset : Vector3.zero);
			int num;
			float num2;
			if (this.useCells)
			{
				num = Mathf.CeilToInt(Mathf.Log(Mathf.Max(Mathw.GetMax(bounds.size), (float)this.cellSize) / (float)this.cellSize, 2f));
				num2 = (float)((int)Mathf.Pow(2f, (float)num) * this.cellSize);
			}
			else
			{
				num2 = Mathw.GetMax(bounds.size);
				num = 0;
			}
			if (num == 0 && this.octree != null)
			{
				this.octree = new ObjectOctree.MaxCell();
			}
			else if (num > 0 && this.octree is ObjectOctree.MaxCell)
			{
				this.octree = new ObjectOctree.Cell();
			}
			this.octree.maxLevels = num;
			this.octree.bounds = new Bounds(bounds.center, new Vector3(num2, num2, num2));
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000B56A4 File Offset: 0x000B38A4
		public void ApplyChanges()
		{
			this.validRebakeLighting = (this.rebakeLighting && !this.validCopyBakedLighting && !Application.isPlaying && Application.isEditor);
			for (int i = 0; i < this.changedCells.Count; i++)
			{
				ObjectOctree.MaxCell maxCell = this.changedCells[i];
				maxCell.hasChanged = false;
				maxCell.ApplyChanges(this);
			}
			this.changedCells.Clear();
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000B5710 File Offset: 0x000B3910
		public void CombineAll()
		{
			this.stopwatch.Reset();
			this.stopwatch.Start();
			this.addMeshCollidersList.Clear();
			this.unreadableMeshes.Clear();
			this.selectImportSettingsMeshes.Clear();
			this.AddObjectsAutomatically();
			if (!this.octreeContainsObjects)
			{
				return;
			}
			this.SetOriginalCollidersActive(false);
			this.validRebakeLighting = (this.rebakeLighting && !this.validCopyBakedLighting && !Application.isPlaying && Application.isEditor);
			this.totalVertices = (this.totalTriangles = (this.originalTotalVertices = (this.originalTotalTriangles = (this.originalDrawCalls = (this.newDrawCalls = 0)))));
			for (int i = 0; i < this.lodParentHolders.Length; i++)
			{
				MeshCombiner.LodParentHolder lodParentHolder = this.lodParentHolders[i];
				if (lodParentHolder.found)
				{
					if (lodParentHolder.go == null)
					{
						lodParentHolder.Create(this, i);
					}
					this.octree.CombineMeshes(this, i);
				}
			}
			if (MeshCombineJobManager.instance.jobSettings.combineJobMode == MeshCombineJobManager.CombineJobMode.CombineAtOnce)
			{
				MeshCombineJobManager.instance.ExecuteJobs();
			}
			this.combinedActive = true;
			this.combined = true;
			this.activeOriginal = false;
			this.ExecuteHandleObjects(this.activeOriginal, MeshCombiner.HandleComponent.Disable, MeshCombiner.HandleComponent.Disable, false);
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000B584C File Offset: 0x000B3A4C
		private void InitAndResetLodParentsCount()
		{
			for (int i = 0; i < this.lodParentHolders.Length; i++)
			{
				if (this.lodParentHolders[i] == null)
				{
					this.lodParentHolders[i] = new MeshCombiner.LodParentHolder(i + 1);
				}
				else
				{
					this.lodParentHolders[i].Reset();
				}
			}
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000B5898 File Offset: 0x000B3A98
		public void AddObjectsFromSearchParent()
		{
			if (this.searchOptions.parent == null)
			{
				Debug.Log("You need to assign a 'Parent' GameObject in which meshes will be searched");
				return;
			}
			LODGroup[] componentsInChildren = this.searchOptions.parent.GetComponentsInChildren<LODGroup>(true);
			this.AddLodGroups(componentsInChildren, true);
			Transform[] componentsInChildren2 = this.searchOptions.parent.GetComponentsInChildren<Transform>(true);
			this.AddTransforms(componentsInChildren2, true);
			if (this.addMeshColliders)
			{
				for (int i = 0; i < this.foundObjects.Count; i++)
				{
					this.foundColliders.AddRange(this.foundObjects[i].go.GetComponentsInChildren<Collider>(false));
				}
				for (int j = 0; j < this.foundLodGroups.Count; j++)
				{
					this.foundColliders.AddRange(this.foundLodGroups[j].gameObject.GetComponentsInChildren<Collider>(false));
				}
			}
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000B5970 File Offset: 0x000B3B70
		private void AddLodGroups(LODGroup[] lodGroups, bool useSearchOptions = true)
		{
			List<CachedLodGameObject> list = new List<CachedLodGameObject>();
			CachedGameObject cachedGameObject = null;
			int i = 0;
			while (i < lodGroups.Length)
			{
				LODGroup lodgroup = lodGroups[i];
				if (this.searchOptions.lodGroupSearchMode == MeshCombiner.SearchOptions.LODGroupSearchMode.LodGroup)
				{
					bool flag = this.ValidObject(lodgroup.transform, MeshCombiner.ObjectType.LodGroup, useSearchOptions, ref cachedGameObject) == 1;
					goto IL_57;
				}
				if (!this.searchOptions.onlyActive || lodgroup.gameObject.activeInHierarchy)
				{
					bool flag = true;
					goto IL_57;
				}
				IL_24B:
				i++;
				continue;
				IL_57:
				LOD[] lods = lodgroup.GetLODs();
				int num = lods.Length - 1;
				if (num <= 0)
				{
					goto IL_24B;
				}
				Vector3 vector = Vector3.zero;
				int num2 = 0;
				for (int j = 0; j < lods.Length; j++)
				{
					LOD lod = lods[j];
					for (int k = 0; k < lod.renderers.Length; k++)
					{
						Renderer renderer = lod.renderers[k];
						if (renderer)
						{
							bool flag;
							if (flag)
							{
								CachedGameObject cachedGameObject2 = null;
								int num3 = this.ValidObject(renderer.transform, MeshCombiner.ObjectType.LodRenderer, useSearchOptions, ref cachedGameObject2);
								if (num3 == -1)
								{
									goto IL_12E;
								}
								if (num3 == -2)
								{
									list.Clear();
									goto IL_155;
								}
								list.Add(new CachedLodGameObject(cachedGameObject2, num, j));
								if (this.searchOptions.objectCenter == MeshCombiner.ObjectCenter.BoundsCenter)
								{
									vector += cachedGameObject2.mr.bounds.center;
									num2++;
								}
							}
							this.uniqueLodObjects.Add(renderer.transform);
						}
						IL_12E:;
					}
				}
				IL_155:
				if (list.Count > 0)
				{
					if (this.searchOptions.objectCenter == MeshCombiner.ObjectCenter.BoundsCenter)
					{
						vector /= (float)num2;
					}
					else
					{
						vector = lodgroup.transform.position;
					}
					for (int l = 0; l < list.Count; l++)
					{
						CachedLodGameObject cachedLodGameObject = list[l];
						cachedLodGameObject.center = vector;
						if (!this.hasFoundFirstObject)
						{
							this.bounds.center = cachedLodGameObject.mr.bounds.center;
							this.hasFoundFirstObject = true;
						}
						this.bounds.Encapsulate(cachedLodGameObject.mr.bounds);
						this.foundLodObjects.Add(cachedLodGameObject);
						this.lodParentHolders[num].found = true;
						this.lodParentHolders[num].lods[cachedLodGameObject.lodLevel]++;
					}
					this.uniqueFoundLodGroups.Add(lodgroup);
					list.Clear();
					goto IL_24B;
				}
				goto IL_24B;
			}
			this.foundLodGroups = new List<LODGroup>(this.uniqueFoundLodGroups);
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000B5BE8 File Offset: 0x000B3DE8
		private void AddTransforms(Transform[] transforms, bool useSearchOptions = true)
		{
			int count = this.uniqueLodObjects.Count;
			foreach (Transform transform in transforms)
			{
				if (count <= 0 || !this.uniqueLodObjects.Contains(transform))
				{
					CachedGameObject cachedGameObject = null;
					if (this.ValidObject(transform, MeshCombiner.ObjectType.Normal, useSearchOptions, ref cachedGameObject) == 1)
					{
						if (!this.hasFoundFirstObject)
						{
							this.bounds.center = cachedGameObject.mr.bounds.center;
							this.hasFoundFirstObject = true;
						}
						this.bounds.Encapsulate(cachedGameObject.mr.bounds);
						this.foundObjects.Add(cachedGameObject);
						this.lodParentHolders[0].lods[0]++;
					}
				}
			}
			if (this.foundObjects.Count > 0)
			{
				this.lodParentHolders[0].found = true;
			}
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000B5CC0 File Offset: 0x000B3EC0
		private int ValidObject(Transform t, MeshCombiner.ObjectType objectType, bool useSearchOptions, ref CachedGameObject cachedGameObject)
		{
			GameObject gameObject = t.gameObject;
			MeshRenderer meshRenderer = null;
			MeshFilter meshFilter = null;
			Mesh mesh = null;
			if (objectType != MeshCombiner.ObjectType.LodGroup || this.searchOptions.lodGroupSearchMode == MeshCombiner.SearchOptions.LODGroupSearchMode.LodRenderers)
			{
				meshRenderer = t.GetComponent<MeshRenderer>();
				if (meshRenderer == null || (!meshRenderer.enabled && this.searchOptions.onlyActiveMeshRenderers))
				{
					return -1;
				}
				meshFilter = t.GetComponent<MeshFilter>();
				if (meshFilter == null)
				{
					return -1;
				}
				mesh = meshFilter.sharedMesh;
				if (mesh == null)
				{
					return -1;
				}
				if (mesh.vertexCount > 65534)
				{
					return -2;
				}
				if (!mesh.isReadable)
				{
					Debug.LogError("Mesh Combine Studio -> Read/Write is disabled on the mesh on GameObject " + gameObject.name + " and can't be combined. Click the 'Make Meshes Readable' in the MCS Inspector to make it automatically readable in the mesh import settings.");
					this.unreadableMeshes.Add(mesh);
					return -1;
				}
			}
			if (useSearchOptions)
			{
				if (this.searchOptions.onlyActive && !gameObject.activeInHierarchy)
				{
					return -1;
				}
				if (objectType != MeshCombiner.ObjectType.LodRenderer || this.searchOptions.lodGroupSearchMode == MeshCombiner.SearchOptions.LODGroupSearchMode.LodRenderers)
				{
					if (this.searchOptions.useLayerMask)
					{
						int num = 1 << t.gameObject.layer;
						if ((this.searchOptions.layerMask.value & num) != num)
						{
							return -1;
						}
					}
					if (this.searchOptions.onlyStatic && !gameObject.isStatic)
					{
						return -1;
					}
					if (this.searchOptions.useTag && !t.CompareTag(this.searchOptions.tag))
					{
						return -1;
					}
					if (this.searchOptions.useComponentsFilter)
					{
						if (this.searchOptions.componentCondition == MeshCombiner.SearchOptions.ComponentCondition.And)
						{
							bool flag = true;
							for (int i = 0; i < this.searchOptions.componentNameList.Count; i++)
							{
								if (t.GetComponent(this.searchOptions.componentNameList[i]) == null)
								{
									flag = false;
									break;
								}
							}
							if (!flag)
							{
								return -1;
							}
						}
						else if (this.searchOptions.componentCondition == MeshCombiner.SearchOptions.ComponentCondition.Or)
						{
							bool flag2 = false;
							for (int j = 0; j < this.searchOptions.componentNameList.Count; j++)
							{
								if (t.GetComponent(this.searchOptions.componentNameList[j]) != null)
								{
									flag2 = true;
									break;
								}
							}
							if (!flag2)
							{
								return -1;
							}
						}
						else
						{
							bool flag3 = true;
							for (int k = 0; k < this.searchOptions.componentNameList.Count; k++)
							{
								if (t.GetComponent(this.searchOptions.componentNameList[k]) != null)
								{
									flag3 = false;
									break;
								}
							}
							if (!flag3)
							{
								return -1;
							}
						}
					}
					if (this.searchOptions.useNameContains)
					{
						bool flag4 = false;
						for (int l = 0; l < this.searchOptions.nameContainList.Count; l++)
						{
							if (Methods.Contains(t.name, this.searchOptions.nameContainList[l]))
							{
								flag4 = true;
								break;
							}
						}
						if (!flag4)
						{
							return -1;
						}
					}
					if (this.searchOptions.useSearchBox)
					{
						if (this.searchOptions.objectCenter == MeshCombiner.ObjectCenter.BoundsCenter)
						{
							if (!this.searchOptions.searchBoxBounds.Contains(meshRenderer.bounds.center))
							{
								return -2;
							}
						}
						else if (!this.searchOptions.searchBoxBounds.Contains(t.position))
						{
							return -2;
						}
					}
				}
				if (objectType != MeshCombiner.ObjectType.LodGroup)
				{
					if (this.searchOptions.useVertexInputLimit && mesh.vertexCount > this.searchOptions.vertexInputLimit)
					{
						return -2;
					}
					if (this.useVertexOutputLimit && mesh.vertexCount > this.vertexOutputLimit)
					{
						return -2;
					}
					if (this.searchOptions.useMaxBoundsFactor && this.useCells && Mathw.GetMax(meshRenderer.bounds.size) > (float)this.cellSize * this.searchOptions.maxBoundsFactor)
					{
						return -2;
					}
				}
			}
			if (objectType != MeshCombiner.ObjectType.LodGroup)
			{
				cachedGameObject = new CachedGameObject(gameObject, t, meshRenderer, meshFilter, mesh);
			}
			return 1;
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000B6078 File Offset: 0x000B4278
		public void RestoreOriginalRenderersAndLODGroups()
		{
			if (this.activeOriginal)
			{
				return;
			}
			this.activeOriginal = true;
			this.ExecuteHandleObjects(this.activeOriginal, MeshCombiner.HandleComponent.Disable, MeshCombiner.HandleComponent.Disable, true);
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000B6099 File Offset: 0x000B4299
		public void SwapCombine()
		{
			if (!this.combined)
			{
				this.CombineAll();
			}
			this.combinedActive = !this.combinedActive;
			this.ExecuteHandleObjects(!this.combinedActive, this.originalMeshRenderers, this.originalLODGroups, true);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000B60D4 File Offset: 0x000B42D4
		private void SetOriginalCollidersActive(bool active)
		{
			for (int i = 0; i < this.foundColliders.Count; i++)
			{
				Collider collider = this.foundColliders[i];
				if (collider)
				{
					collider.enabled = active;
				}
				else
				{
					Methods.ListRemoveAt<Collider>(this.foundColliders, i--);
				}
			}
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000B6128 File Offset: 0x000B4328
		public void ExecuteHandleObjects(bool active, MeshCombiner.HandleComponent handleOriginalObjects, MeshCombiner.HandleComponent handleOriginalLodGroups, bool includeColliders = true)
		{
			Methods.SetChildrenActive(base.transform, !active);
			if (handleOriginalObjects == MeshCombiner.HandleComponent.Disable)
			{
				if (includeColliders)
				{
					this.SetOriginalCollidersActive(active);
				}
				for (int i = 0; i < this.foundObjects.Count; i++)
				{
					CachedGameObject cachedGameObject = this.foundObjects[i];
					if (cachedGameObject.mr)
					{
						cachedGameObject.mr.enabled = active;
					}
					else
					{
						Methods.ListRemoveAt<CachedGameObject>(this.foundObjects, i--);
					}
				}
				for (int j = 0; j < this.foundLodObjects.Count; j++)
				{
					CachedLodGameObject cachedLodGameObject = this.foundLodObjects[j];
					if (cachedLodGameObject.mr)
					{
						cachedLodGameObject.mr.enabled = active;
					}
					else
					{
						Methods.ListRemoveAt<CachedLodGameObject>(this.foundLodObjects, j--);
					}
				}
			}
			if (handleOriginalObjects == MeshCombiner.HandleComponent.Destroy)
			{
				for (int k = 0; k < this.foundColliders.Count; k++)
				{
					Collider collider = this.foundColliders[k];
					if (collider)
					{
						Object.Destroy(collider);
					}
					else
					{
						Methods.ListRemoveAt<Collider>(this.foundColliders, k--);
					}
				}
				for (int l = 0; l < this.foundObjects.Count; l++)
				{
					bool flag = false;
					CachedGameObject cachedGameObject2 = this.foundObjects[l];
					if (cachedGameObject2.mf)
					{
						Object.Destroy(cachedGameObject2.mf);
					}
					else
					{
						flag = true;
					}
					if (cachedGameObject2.mr)
					{
						Object.Destroy(cachedGameObject2.mr);
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						Methods.ListRemoveAt<CachedGameObject>(this.foundObjects, l--);
					}
				}
				for (int m = 0; m < this.foundLodObjects.Count; m++)
				{
					bool flag2 = false;
					CachedGameObject cachedGameObject3 = this.foundLodObjects[m];
					if (cachedGameObject3.mf)
					{
						Object.Destroy(cachedGameObject3.mf);
					}
					else
					{
						flag2 = true;
					}
					if (cachedGameObject3.mr)
					{
						Object.Destroy(cachedGameObject3.mr);
					}
					else
					{
						flag2 = true;
					}
					if (flag2)
					{
						Methods.ListRemoveAt<CachedLodGameObject>(this.foundLodObjects, m--);
					}
				}
			}
			if (handleOriginalLodGroups == MeshCombiner.HandleComponent.Disable)
			{
				for (int n = 0; n < this.foundLodGroups.Count; n++)
				{
					LODGroup lodgroup = this.foundLodGroups[n];
					if (lodgroup)
					{
						lodgroup.enabled = active;
					}
				}
				return;
			}
			if (handleOriginalLodGroups == MeshCombiner.HandleComponent.Destroy)
			{
				for (int num = 0; num < this.foundLodGroups.Count; num++)
				{
					LODGroup lodgroup2 = this.foundLodGroups[num];
					if (lodgroup2 != null)
					{
						Object.Destroy(lodgroup2);
					}
				}
			}
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000B63C4 File Offset: 0x000B45C4
		private void DrawGizmosCube(Bounds bounds, Color color)
		{
			Gizmos.color = color;
			Gizmos.DrawWireCube(bounds.center, bounds.size);
			Gizmos.color = new Color(color.r, color.g, color.b, 0.5f);
			Gizmos.DrawCube(bounds.center, bounds.size);
			Gizmos.color = Color.white;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000B6428 File Offset: 0x000B4628
		private void OnDrawGizmosSelected()
		{
			if (this.addMeshColliders && this.addMeshCollidersInRange)
			{
				this.DrawGizmosCube(this.addMeshCollidersBounds, Color.green);
			}
			if (this.removeBackFaceTriangles && this.backFaceTriangleMode == MeshCombiner.BackFaceTriangleMode.Box)
			{
				this.DrawGizmosCube(this.backFaceBounds, Color.blue);
			}
			if (!this.drawGizmos)
			{
				return;
			}
			if (this.octree != null && this.octreeContainsObjects)
			{
				this.octree.Draw(this, true, !this.searchOptions.useSearchBox);
			}
			if (this.searchOptions.useSearchBox)
			{
				this.searchOptions.GetSearchBoxBounds();
				Gizmos.color = Color.green;
				Gizmos.DrawWireCube(this.searchOptions.searchBoxBounds.center, this.searchOptions.searchBoxBounds.size);
				Gizmos.color = Color.white;
			}
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000B64FC File Offset: 0x000B46FC
		private void LogOctreeInfo()
		{
			Console.Log("Cells " + ObjectOctree.MaxCell.maxCellCount + " -> Found Objects: ", 0, null, null);
			MeshCombiner.LodParentHolder[] array = this.lodParentHolders;
			if (array == null || array.Length == 0)
			{
				return;
			}
			for (int i = 0; i < array.Length; i++)
			{
				MeshCombiner.LodParentHolder lodParentHolder = array[i];
				if (lodParentHolder.found)
				{
					string text = "LOD Group " + (i + 1) + " |";
					int[] lods = lodParentHolder.lods;
					for (int j = 0; j < lods.Length; j++)
					{
						text = text + " " + lods[j].ToString() + " |";
					}
					Console.Log(text, 0, null, null);
				}
			}
		}

		// Token: 0x040021FD RID: 8701
		public static List<MeshCombiner> instances = new List<MeshCombiner>();

		// Token: 0x040021FF RID: 8703
		public MeshCombineJobManager.JobSettings jobSettings = new MeshCombineJobManager.JobSettings();

		// Token: 0x04002200 RID: 8704
		public MeshCombiner.LODGroupSettings[] lodGroupsSettings;

		// Token: 0x04002201 RID: 8705
		public ComputeShader computeDepthToArray;

		// Token: 0x04002202 RID: 8706
		public GameObject instantiatePrefab;

		// Token: 0x04002203 RID: 8707
		public const int maxLodCount = 8;

		// Token: 0x04002204 RID: 8708
		public string saveMeshesFolder;

		// Token: 0x04002205 RID: 8709
		public ObjectOctree.Cell octree;

		// Token: 0x04002206 RID: 8710
		public List<ObjectOctree.MaxCell> changedCells;

		// Token: 0x04002207 RID: 8711
		[NonSerialized]
		public bool octreeContainsObjects;

		// Token: 0x04002208 RID: 8712
		public bool unitySettingsFoldout = true;

		// Token: 0x04002209 RID: 8713
		public MeshCombiner.SearchOptions searchOptions;

		// Token: 0x0400220A RID: 8714
		public CombineConditionSettings combineConditionSettings;

		// Token: 0x0400220B RID: 8715
		public bool outputSettingsFoldout = true;

		// Token: 0x0400220C RID: 8716
		public bool useCells = true;

		// Token: 0x0400220D RID: 8717
		public int cellSize = 32;

		// Token: 0x0400220E RID: 8718
		public Vector3 cellOffset;

		// Token: 0x0400220F RID: 8719
		public bool useVertexOutputLimit;

		// Token: 0x04002210 RID: 8720
		public int vertexOutputLimit = 65534;

		// Token: 0x04002211 RID: 8721
		public MeshCombiner.RebakeLightingMode rebakeLightingMode;

		// Token: 0x04002212 RID: 8722
		public bool copyBakedLighting;

		// Token: 0x04002213 RID: 8723
		public bool validCopyBakedLighting;

		// Token: 0x04002214 RID: 8724
		public bool rebakeLighting;

		// Token: 0x04002215 RID: 8725
		public bool validRebakeLighting;

		// Token: 0x04002216 RID: 8726
		public float scaleInLightmap = 1f;

		// Token: 0x04002217 RID: 8727
		public bool addMeshColliders;

		// Token: 0x04002218 RID: 8728
		public bool addMeshCollidersInRange;

		// Token: 0x04002219 RID: 8729
		public Bounds addMeshCollidersBounds;

		// Token: 0x0400221A RID: 8730
		public bool makeMeshesUnreadable = true;

		// Token: 0x0400221B RID: 8731
		public bool removeTrianglesBelowSurface;

		// Token: 0x0400221C RID: 8732
		public bool noColliders;

		// Token: 0x0400221D RID: 8733
		public LayerMask surfaceLayerMask;

		// Token: 0x0400221E RID: 8734
		public float maxSurfaceHeight = 1000f;

		// Token: 0x0400221F RID: 8735
		public bool removeOverlappingTriangles;

		// Token: 0x04002220 RID: 8736
		public bool removeSamePositionTriangles;

		// Token: 0x04002221 RID: 8737
		public GameObject overlappingCollidersGO;

		// Token: 0x04002222 RID: 8738
		public LayerMask overlapLayerMask;

		// Token: 0x04002223 RID: 8739
		public int voxelizeLayer;

		// Token: 0x04002224 RID: 8740
		public int lodGroupLayer;

		// Token: 0x04002225 RID: 8741
		public bool removeBackFaceTriangles;

		// Token: 0x04002226 RID: 8742
		public MeshCombiner.BackFaceTriangleMode backFaceTriangleMode;

		// Token: 0x04002227 RID: 8743
		public Vector3 backFaceDirection;

		// Token: 0x04002228 RID: 8744
		public Bounds backFaceBounds;

		// Token: 0x04002229 RID: 8745
		public bool twoSidedShadows = true;

		// Token: 0x0400222A RID: 8746
		public bool weldVertices;

		// Token: 0x0400222B RID: 8747
		public bool weldSnapVertices;

		// Token: 0x0400222C RID: 8748
		public float weldSnapSize = 0.025f;

		// Token: 0x0400222D RID: 8749
		public bool weldIncludeNormals;

		// Token: 0x0400222E RID: 8750
		public bool jobSettingsFoldout = true;

		// Token: 0x0400222F RID: 8751
		public bool runtimeSettingsFoldout = true;

		// Token: 0x04002230 RID: 8752
		public bool combineInRuntime;

		// Token: 0x04002231 RID: 8753
		public bool combineOnStart = true;

		// Token: 0x04002232 RID: 8754
		public bool useCombineSwapKey;

		// Token: 0x04002233 RID: 8755
		public KeyCode combineSwapKey = KeyCode.Tab;

		// Token: 0x04002234 RID: 8756
		public MeshCombiner.HandleComponent originalMeshRenderers;

		// Token: 0x04002235 RID: 8757
		public MeshCombiner.HandleComponent originalLODGroups;

		// Token: 0x04002236 RID: 8758
		public Vector3 oldPosition;

		// Token: 0x04002237 RID: 8759
		public Vector3 oldScale;

		// Token: 0x04002238 RID: 8760
		public MeshCombiner.LodParentHolder[] lodParentHolders = new MeshCombiner.LodParentHolder[8];

		// Token: 0x04002239 RID: 8761
		[HideInInspector]
		public List<CachedGameObject> foundObjects = new List<CachedGameObject>();

		// Token: 0x0400223A RID: 8762
		[HideInInspector]
		public List<CachedLodGameObject> foundLodObjects = new List<CachedLodGameObject>();

		// Token: 0x0400223B RID: 8763
		[HideInInspector]
		public List<LODGroup> foundLodGroups = new List<LODGroup>();

		// Token: 0x0400223C RID: 8764
		[HideInInspector]
		public List<Collider> foundColliders = new List<Collider>();

		// Token: 0x0400223D RID: 8765
		public HashSet<LODGroup> uniqueFoundLodGroups = new HashSet<LODGroup>();

		// Token: 0x0400223E RID: 8766
		public HashSet<Mesh> unreadableMeshes = new HashSet<Mesh>();

		// Token: 0x0400223F RID: 8767
		public HashSet<Mesh> selectImportSettingsMeshes = new HashSet<Mesh>();

		// Token: 0x04002240 RID: 8768
		public FoundCombineConditions foundCombineConditions = new FoundCombineConditions();

		// Token: 0x04002241 RID: 8769
		public HashSet<MeshCombineJobManager.MeshCombineJob> meshCombineJobs = new HashSet<MeshCombineJobManager.MeshCombineJob>();

		// Token: 0x04002242 RID: 8770
		public int totalMeshCombineJobs;

		// Token: 0x04002243 RID: 8771
		public int mrDisabledCount;

		// Token: 0x04002244 RID: 8772
		public bool combined;

		// Token: 0x04002245 RID: 8773
		public bool activeOriginal = true;

		// Token: 0x04002246 RID: 8774
		public bool combinedActive;

		// Token: 0x04002247 RID: 8775
		public bool drawGizmos = true;

		// Token: 0x04002248 RID: 8776
		public bool drawMeshBounds = true;

		// Token: 0x04002249 RID: 8777
		public int originalTotalVertices;

		// Token: 0x0400224A RID: 8778
		public int originalTotalTriangles;

		// Token: 0x0400224B RID: 8779
		public int totalVertices;

		// Token: 0x0400224C RID: 8780
		public int totalTriangles;

		// Token: 0x0400224D RID: 8781
		public int originalDrawCalls;

		// Token: 0x0400224E RID: 8782
		public int newDrawCalls;

		// Token: 0x0400224F RID: 8783
		public float combineTime;

		// Token: 0x04002250 RID: 8784
		public FastList<MeshColliderAdd> addMeshCollidersList = new FastList<MeshColliderAdd>();

		// Token: 0x04002251 RID: 8785
		private HashSet<Transform> uniqueLodObjects = new HashSet<Transform>();

		// Token: 0x04002252 RID: 8786
		[NonSerialized]
		private MeshCombiner thisInstance;

		// Token: 0x04002253 RID: 8787
		private bool hasFoundFirstObject;

		// Token: 0x04002254 RID: 8788
		private Bounds bounds;

		// Token: 0x04002255 RID: 8789
		private Stopwatch stopwatch = new Stopwatch();

		// Token: 0x020007A9 RID: 1961
		public enum ObjectType
		{
			// Token: 0x040029EE RID: 10734
			Normal,
			// Token: 0x040029EF RID: 10735
			LodGroup,
			// Token: 0x040029F0 RID: 10736
			LodRenderer
		}

		// Token: 0x020007AA RID: 1962
		public enum HandleComponent
		{
			// Token: 0x040029F2 RID: 10738
			Disable,
			// Token: 0x040029F3 RID: 10739
			Destroy
		}

		// Token: 0x020007AB RID: 1963
		public enum ObjectCenter
		{
			// Token: 0x040029F5 RID: 10741
			BoundsCenter,
			// Token: 0x040029F6 RID: 10742
			TransformPosition
		}

		// Token: 0x020007AC RID: 1964
		public enum BackFaceTriangleMode
		{
			// Token: 0x040029F8 RID: 10744
			Box,
			// Token: 0x040029F9 RID: 10745
			Direction
		}

		// Token: 0x020007AD RID: 1965
		// (Invoke) Token: 0x06003060 RID: 12384
		public delegate void DefaultMethod();

		// Token: 0x020007AE RID: 1966
		public enum RebakeLightingMode
		{
			// Token: 0x040029FB RID: 10747
			CopyLightmapUvs,
			// Token: 0x040029FC RID: 10748
			RegenarateLightmapUvs
		}

		// Token: 0x020007AF RID: 1967
		[Serializable]
		public class SearchOptions
		{
			// Token: 0x06003063 RID: 12387 RVA: 0x000CD5C4 File Offset: 0x000CB7C4
			public SearchOptions(GameObject parent)
			{
				this.parent = parent;
			}

			// Token: 0x06003064 RID: 12388 RVA: 0x000CD65A File Offset: 0x000CB85A
			public void GetSearchBoxBounds()
			{
				this.searchBoxBounds = new Bounds(this.searchBoxPivot + new Vector3(0f, this.searchBoxSize.y * 0.5f, 0f), this.searchBoxSize);
			}

			// Token: 0x040029FD RID: 10749
			public bool foldout = true;

			// Token: 0x040029FE RID: 10750
			public GameObject parent;

			// Token: 0x040029FF RID: 10751
			public MeshCombiner.ObjectCenter objectCenter;

			// Token: 0x04002A00 RID: 10752
			public MeshCombiner.SearchOptions.LODGroupSearchMode lodGroupSearchMode;

			// Token: 0x04002A01 RID: 10753
			public bool useSearchBox;

			// Token: 0x04002A02 RID: 10754
			public Bounds searchBoxBounds;

			// Token: 0x04002A03 RID: 10755
			public bool searchBoxSquare;

			// Token: 0x04002A04 RID: 10756
			public Vector3 searchBoxPivot;

			// Token: 0x04002A05 RID: 10757
			public Vector3 searchBoxSize = new Vector3(25f, 25f, 25f);

			// Token: 0x04002A06 RID: 10758
			public bool useMaxBoundsFactor = true;

			// Token: 0x04002A07 RID: 10759
			public float maxBoundsFactor = 1.5f;

			// Token: 0x04002A08 RID: 10760
			public bool useVertexInputLimit = true;

			// Token: 0x04002A09 RID: 10761
			public int vertexInputLimit = 5000;

			// Token: 0x04002A0A RID: 10762
			public bool useLayerMask;

			// Token: 0x04002A0B RID: 10763
			public LayerMask layerMask = -1;

			// Token: 0x04002A0C RID: 10764
			public bool useTag;

			// Token: 0x04002A0D RID: 10765
			public string tag;

			// Token: 0x04002A0E RID: 10766
			public bool useNameContains;

			// Token: 0x04002A0F RID: 10767
			public List<string> nameContainList = new List<string>();

			// Token: 0x04002A10 RID: 10768
			public bool onlyActive = true;

			// Token: 0x04002A11 RID: 10769
			public bool onlyStatic = true;

			// Token: 0x04002A12 RID: 10770
			public bool onlyActiveMeshRenderers = true;

			// Token: 0x04002A13 RID: 10771
			public bool useComponentsFilter;

			// Token: 0x04002A14 RID: 10772
			public MeshCombiner.SearchOptions.ComponentCondition componentCondition;

			// Token: 0x04002A15 RID: 10773
			public List<string> componentNameList = new List<string>();

			// Token: 0x020007CD RID: 1997
			public enum ComponentCondition
			{
				// Token: 0x04002A57 RID: 10839
				And,
				// Token: 0x04002A58 RID: 10840
				Or,
				// Token: 0x04002A59 RID: 10841
				Not
			}

			// Token: 0x020007CE RID: 1998
			public enum LODGroupSearchMode
			{
				// Token: 0x04002A5B RID: 10843
				LodGroup,
				// Token: 0x04002A5C RID: 10844
				LodRenderers
			}
		}

		// Token: 0x020007B0 RID: 1968
		[Serializable]
		public class LODGroupSettings
		{
			// Token: 0x06003065 RID: 12389 RVA: 0x000CD698 File Offset: 0x000CB898
			public LODGroupSettings(int lodParentIndex)
			{
				int num = lodParentIndex + 1;
				this.lodSettings = new MeshCombiner.LODSettings[num];
				float num2 = 1f / (float)num;
				for (int i = 0; i < this.lodSettings.Length; i++)
				{
					this.lodSettings[i] = new MeshCombiner.LODSettings(1f - num2 * (float)(i + 1));
				}
			}

			// Token: 0x04002A16 RID: 10774
			public MeshCombiner.LODSettings[] lodSettings;
		}

		// Token: 0x020007B1 RID: 1969
		[Serializable]
		public class LODSettings
		{
			// Token: 0x06003066 RID: 12390 RVA: 0x000CD6F0 File Offset: 0x000CB8F0
			public LODSettings(float screenRelativeTransitionHeight)
			{
				this.screenRelativeTransitionHeight = screenRelativeTransitionHeight;
			}

			// Token: 0x04002A17 RID: 10775
			public float screenRelativeTransitionHeight;

			// Token: 0x04002A18 RID: 10776
			public float fadeTransitionWidth;
		}

		// Token: 0x020007B2 RID: 1970
		public class LodParentHolder
		{
			// Token: 0x06003067 RID: 12391 RVA: 0x000CD6FF File Offset: 0x000CB8FF
			public LodParentHolder(int lodCount)
			{
				this.lods = new int[lodCount];
			}

			// Token: 0x06003068 RID: 12392 RVA: 0x000CD714 File Offset: 0x000CB914
			public void Create(MeshCombiner meshCombiner, int lodParentIndex)
			{
				if (meshCombiner.foundLodGroups.Count == 0)
				{
					this.go = new GameObject("Cells");
				}
				else
				{
					this.go = new GameObject("LODGroup " + (lodParentIndex + 1));
					this.go.AddComponent<LODGroupSetup>().Init(meshCombiner, lodParentIndex);
				}
				this.t = this.go.transform;
				this.t.transform.parent = meshCombiner.transform;
			}

			// Token: 0x06003069 RID: 12393 RVA: 0x000CD796 File Offset: 0x000CB996
			public void Reset()
			{
				this.found = false;
				Array.Clear(this.lods, 0, this.lods.Length);
			}

			// Token: 0x04002A19 RID: 10777
			public GameObject go;

			// Token: 0x04002A1A RID: 10778
			public Transform t;

			// Token: 0x04002A1B RID: 10779
			public bool found;

			// Token: 0x04002A1C RID: 10780
			public int[] lods;
		}
	}
}
