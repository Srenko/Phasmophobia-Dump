using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

namespace MeshCombineStudio
{
	// Token: 0x0200049C RID: 1180
	[ExecuteInEditMode]
	public class MeshCombineJobManager : MonoBehaviour
	{
		// Token: 0x060024C0 RID: 9408 RVA: 0x000B427C File Offset: 0x000B247C
		public static MeshCombineJobManager CreateInstance(MeshCombiner meshCombiner, GameObject instantiatePrefab)
		{
			if (MeshCombineJobManager.instance != null)
			{
				MeshCombineJobManager.instance.camGeometryCapture.computeDepthToArray = meshCombiner.computeDepthToArray;
				return MeshCombineJobManager.instance;
			}
			GameObject gameObject = new GameObject("MCS Job Manager");
			MeshCombineJobManager.instance = gameObject.AddComponent<MeshCombineJobManager>();
			MeshCombineJobManager.instance.SetJobMode(meshCombiner.jobSettings);
			gameObject.AddComponent<Camera>().enabled = false;
			MeshCombineJobManager.instance.camGeometryCapture = gameObject.AddComponent<CamGeometryCapture>();
			MeshCombineJobManager.instance.camGeometryCapture.computeDepthToArray = meshCombiner.computeDepthToArray;
			MeshCombineJobManager.instance.camGeometryCapture.Init();
			return MeshCombineJobManager.instance;
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000B431C File Offset: 0x000B251C
		public static void ResetMeshCache()
		{
			if (MeshCombineJobManager.instance)
			{
				MeshCombineJobManager.instance.meshCacheDictionary.Clear();
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000B4339 File Offset: 0x000B2539
		private void Awake()
		{
			MeshCombineJobManager.instance = this;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000B4341 File Offset: 0x000B2541
		private void OnEnable()
		{
			MeshCombineJobManager.instance = this;
			base.gameObject.hideFlags = (HideFlags.HideInHierarchy | HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
			this.Init();
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000B435C File Offset: 0x000B255C
		public void Init()
		{
			this.cores = Environment.ProcessorCount;
			if (this.meshCombineJobsThreads == null || this.meshCombineJobsThreads.Length != this.cores)
			{
				this.meshCombineJobsThreads = new MeshCombineJobManager.MeshCombineJobsThread[this.cores];
				for (int i = 0; i < this.meshCombineJobsThreads.Length; i++)
				{
					this.meshCombineJobsThreads[i] = new MeshCombineJobManager.MeshCombineJobsThread(i);
				}
			}
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x00003F60 File Offset: 0x00002160
		private void OnDisable()
		{
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000B43BE File Offset: 0x000B25BE
		private void OnDestroy()
		{
			this.AbortJobs();
			MeshCombineJobManager.instance = null;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000B43CC File Offset: 0x000B25CC
		private void Update()
		{
			if (Application.isPlaying)
			{
				this.MyUpdate();
			}
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000B43DB File Offset: 0x000B25DB
		private void MyUpdate()
		{
			this.ExecuteJobs();
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000B43E4 File Offset: 0x000B25E4
		public void SetJobMode(MeshCombineJobManager.JobSettings newJobSettings)
		{
			if (newJobSettings.combineMeshesPerFrame < 1)
			{
				Debug.LogError("MCS Job Manager -> CombineMeshesPerFrame is " + newJobSettings.combineMeshesPerFrame + " and should be 1 or higher.");
				return;
			}
			if (newJobSettings.combineMeshesPerFrame > 128)
			{
				Debug.LogError("MCS Job Manager -> CombineMeshesPerFrame is " + newJobSettings.combineMeshesPerFrame + " and should be 128 or lower.");
				return;
			}
			if (newJobSettings.customThreadAmount < 1)
			{
				Debug.LogError("MCS Job Manager -> customThreadAmount is " + newJobSettings.combineMeshesPerFrame + " and should be 1 or higher.");
				return;
			}
			if (newJobSettings.customThreadAmount > this.cores)
			{
				newJobSettings.customThreadAmount = this.cores;
			}
			this.jobSettings.CopySettings(newJobSettings);
			if (this.jobSettings.useMultiThreading)
			{
				this.startThreadId = (this.jobSettings.useMainThread ? 0 : 1);
				if (this.jobSettings.threadAmountMode == MeshCombineJobManager.ThreadAmountMode.Custom)
				{
					if (this.jobSettings.customThreadAmount > this.cores - this.startThreadId)
					{
						this.jobSettings.customThreadAmount = this.cores - this.startThreadId;
					}
					this.threadAmount = this.jobSettings.customThreadAmount;
				}
				else
				{
					if (this.jobSettings.threadAmountMode == MeshCombineJobManager.ThreadAmountMode.AllThreads)
					{
						this.threadAmount = this.cores;
					}
					else
					{
						this.threadAmount = this.cores / 2;
					}
					this.threadAmount -= this.startThreadId;
				}
				this.endThreadId = this.startThreadId + this.threadAmount;
			}
			else
			{
				this.startThreadId = 0;
				this.endThreadId = 1;
				this.threadAmount = 1;
			}
			int combineMeshesPerFrame;
			if (this.jobSettings.combineJobMode == MeshCombineJobManager.CombineJobMode.CombinePerFrame)
			{
				combineMeshesPerFrame = this.jobSettings.combineMeshesPerFrame;
			}
			else
			{
				combineMeshesPerFrame = this.threadAmount;
			}
			while (this.newMeshObjectsPool.Count > combineMeshesPerFrame)
			{
				this.newMeshObjectsPool.RemoveLast();
			}
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000B45B4 File Offset: 0x000B27B4
		public void AddJob(MeshCombiner meshCombiner, MeshObjectsHolder meshObjectsHolder, Transform parent, Vector3 position)
		{
			FastList<MeshObject> meshObjects = meshObjectsHolder.meshObjects;
			int num = 0;
			int num2 = 0;
			int startIndex = 0;
			int num3 = 0;
			bool firstMesh = true;
			bool intersectsSurface = false;
			Mesh y = null;
			MeshCache meshCache = null;
			int num4 = meshCombiner.useVertexOutputLimit ? meshCombiner.vertexOutputLimit : 65534;
			int i = 0;
			while (i < meshObjects.Count)
			{
				MeshObject meshObject = meshObjects.items[i];
				meshObject.skip = false;
				meshCombiner.originalDrawCalls++;
				Mesh mesh = meshObject.cachedGO.mesh;
				if (mesh != y && !this.meshCacheDictionary.TryGetValue(mesh, out meshCache))
				{
					meshCache = new MeshCache(mesh);
					this.meshCacheDictionary.Add(mesh, meshCache);
				}
				y = mesh;
				meshObject.meshCache = meshCache;
				int vertexCount = meshCache.subMeshCache[meshObject.subMeshIndex].vertexCount;
				int triangleCount = meshCache.subMeshCache[meshObject.subMeshIndex].triangleCount;
				meshCombiner.originalTotalVertices += vertexCount;
				meshCombiner.originalTotalTriangles += triangleCount;
				if (num + vertexCount > num4)
				{
					MeshCombineJobManager.MeshCombineJob meshCombineJob = new MeshCombineJobManager.MeshCombineJob(meshCombiner, meshObjectsHolder, parent, position, startIndex, num3, firstMesh, intersectsSurface);
					this.EnqueueJob(meshCombiner, meshCombineJob);
					intersectsSurface = (firstMesh = false);
					num2 = (num = (num3 = 0));
					startIndex = i;
				}
				if (meshCombiner.removeOverlappingTriangles)
				{
					meshObject.startNewTriangleIndex = num2;
					meshObject.newTriangleCount = triangleCount;
				}
				if (!meshCombiner.removeTrianglesBelowSurface)
				{
					goto IL_1B4;
				}
				int num5 = 0;
				if (!meshCombiner.noColliders)
				{
					num5 = this.MeshIntersectsSurface(meshCombiner, meshObject.cachedGO);
				}
				meshObject.startNewTriangleIndex = num2;
				meshObject.newTriangleCount = triangleCount;
				if (num5 == 0)
				{
					intersectsSurface = (meshObject.intersectsSurface = true);
					meshObject.skip = false;
					goto IL_1B4;
				}
				meshObject.intersectsSurface = false;
				if (num5 != -1)
				{
					meshObject.skip = false;
					goto IL_1B4;
				}
				meshObject.skip = true;
				num3++;
				IL_1C4:
				i++;
				continue;
				IL_1B4:
				num += vertexCount;
				num2 += triangleCount;
				num3++;
				goto IL_1C4;
			}
			if (num > 0)
			{
				MeshCombineJobManager.MeshCombineJob meshCombineJob2 = new MeshCombineJobManager.MeshCombineJob(meshCombiner, meshObjectsHolder, parent, position, startIndex, num3, firstMesh, intersectsSurface);
				this.EnqueueJob(meshCombiner, meshCombineJob2);
			}
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000B47B8 File Offset: 0x000B29B8
		private void EnqueueJob(MeshCombiner meshCombiner, MeshCombineJobManager.MeshCombineJob meshCombineJob)
		{
			meshCombiner.meshCombineJobs.Add(meshCombineJob);
			meshCombiner.totalMeshCombineJobs++;
			this.meshCombineJobs.Enqueue(meshCombineJob);
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000B47E4 File Offset: 0x000B29E4
		public int MeshIntersectsSurface(MeshCombiner meshCombiner, CachedGameObject cachedGO)
		{
			MeshRenderer mr = cachedGO.mr;
			LayerMask surfaceLayerMask = meshCombiner.surfaceLayerMask;
			float maxSurfaceHeight = meshCombiner.maxSurfaceHeight;
			if (Physics.CheckBox(mr.bounds.center, mr.bounds.extents, Quaternion.identity, surfaceLayerMask))
			{
				return 0;
			}
			Vector3 min = mr.bounds.min;
			float maxDistance = meshCombiner.maxSurfaceHeight - min.y;
			this.ray.origin = new Vector3(min.x, maxSurfaceHeight, min.z);
			if (Physics.Raycast(this.ray, out this.hitInfo, maxDistance, surfaceLayerMask) && min.y < this.hitInfo.point.y)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000B48AC File Offset: 0x000B2AAC
		public void AbortJobs()
		{
			foreach (MeshCombineJobManager.MeshCombineJob meshCombineJob in this.meshCombineJobs)
			{
				meshCombineJob.meshCombiner.ClearMeshCombineJobs();
			}
			this.meshCombineJobs.Clear();
			for (int i = 0; i < this.meshCombineJobsThreads.Length; i++)
			{
				MeshCombineJobManager.MeshCombineJobsThread meshCombineJobsThread = this.meshCombineJobsThreads[i];
				Queue<MeshCombineJobManager.MeshCombineJob> obj = meshCombineJobsThread.meshCombineJobs;
				lock (obj)
				{
					foreach (MeshCombineJobManager.MeshCombineJob meshCombineJob2 in meshCombineJobsThread.meshCombineJobs)
					{
						meshCombineJob2.meshCombiner.ClearMeshCombineJobs();
					}
					meshCombineJobsThread.meshCombineJobs.Clear();
				}
			}
			this.totalNewMeshObjects = 0;
			this.abort = true;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000B49B0 File Offset: 0x000B2BB0
		public void ExecuteJobs()
		{
			while (this.meshCombineJobs.Count > 0)
			{
				int num = 999999;
				int num2 = 0;
				for (int i = this.startThreadId; i < this.endThreadId; i++)
				{
					int count = this.meshCombineJobsThreads[i].meshCombineJobs.Count;
					if (count < num)
					{
						num2 = i;
						num = count;
						if (num == 0)
						{
							break;
						}
					}
				}
				Queue<MeshCombineJobManager.MeshCombineJob> obj = this.meshCombineJobsThreads[num2].meshCombineJobs;
				lock (obj)
				{
					MeshCombineJobManager.MeshCombineJob meshCombineJob = this.meshCombineJobs.Dequeue();
					if (!meshCombineJob.abort)
					{
						this.meshCombineJobsThreads[num2].meshCombineJobs.Enqueue(meshCombineJob);
					}
				}
			}
			try
			{
				for (;;)
				{
					bool flag2 = false;
					if (this.jobSettings.useMultiThreading)
					{
						for (int j = 1; j < this.endThreadId; j++)
						{
							MeshCombineJobManager.MeshCombineJobsThread meshCombineJobsThread = this.meshCombineJobsThreads[j];
							if (meshCombineJobsThread.meshCombineJobs.Count > 0)
							{
								flag2 = true;
								if (meshCombineJobsThread.threadState == MeshCombineJobManager.ThreadState.isFree)
								{
									if (MeshCombineJobManager.instance.jobSettings.combineJobMode == MeshCombineJobManager.CombineJobMode.CombinePerFrame && MeshCombineJobManager.instance.totalNewMeshObjects + 1 > MeshCombineJobManager.instance.jobSettings.combineMeshesPerFrame)
									{
										break;
									}
									meshCombineJobsThread.threadState = MeshCombineJobManager.ThreadState.isRunning;
									ThreadPool.QueueUserWorkItem(new WaitCallback(meshCombineJobsThread.ExecuteJobsThread));
								}
								if (meshCombineJobsThread.threadState == MeshCombineJobManager.ThreadState.hasError)
								{
									goto Block_12;
								}
							}
						}
						for (int k = 1; k < this.endThreadId; k++)
						{
							if (this.meshCombineJobsThreads[k].threadState == MeshCombineJobManager.ThreadState.isReady)
							{
								this.CombineMeshesDone(this.meshCombineJobsThreads[k]);
							}
						}
					}
					if (!this.jobSettings.useMultiThreading || this.jobSettings.useMainThread)
					{
						MeshCombineJobManager.MeshCombineJobsThread meshCombineJobsThread2 = this.meshCombineJobsThreads[0];
						if (meshCombineJobsThread2.meshCombineJobs.Count > 0)
						{
							flag2 = true;
							meshCombineJobsThread2.threadState = MeshCombineJobManager.ThreadState.isRunning;
							meshCombineJobsThread2.ExecuteJobsThread(null);
							if (meshCombineJobsThread2.threadState == MeshCombineJobManager.ThreadState.isReady)
							{
								this.CombineMeshesDone(meshCombineJobsThread2);
							}
						}
					}
					if (this.jobSettings.combineJobMode != MeshCombineJobManager.CombineJobMode.CombineAtOnce || !flag2)
					{
						goto IL_1FB;
					}
				}
				Block_12:
				this.AbortJobs();
				IL_1FB:;
			}
			catch (Exception ex)
			{
				Debug.LogError("Mesh Combine Studio error -> " + ex.ToString());
				this.AbortJobs();
			}
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000B4C10 File Offset: 0x000B2E10
		public void CombineMeshesDone(MeshCombineJobManager.MeshCombineJobsThread meshCombineJobThread)
		{
			Queue<MeshCombineJobManager.NewMeshObject> newMeshObjectsDone = meshCombineJobThread.newMeshObjectsDone;
			int num = 0;
			while (newMeshObjectsDone.Count > 0)
			{
				MeshCombineJobManager.NewMeshObject newMeshObject = newMeshObjectsDone.Dequeue();
				MeshCombiner meshCombiner = newMeshObject.meshCombineJob.meshCombiner;
				if (!this.abort && !newMeshObject.meshCombineJob.abort)
				{
					meshCombiner.meshCombineJobs.Remove(newMeshObject.meshCombineJob);
					try
					{
						if (!newMeshObject.allSkipped)
						{
							newMeshObject.CreateMesh();
						}
						if (meshCombiner.meshCombineJobs.Count == 0)
						{
							if (meshCombiner.addMeshColliders)
							{
								meshCombiner.AddMeshColliders();
							}
							meshCombiner.ExecuteOnCombiningReady();
						}
					}
					catch (Exception ex)
					{
						Debug.LogError("Mesh Combine Studio error -> " + ex.ToString());
						MeshCombineJobManager.instance.AbortJobs();
					}
				}
				FastList<MeshCombineJobManager.NewMeshObject> obj = this.newMeshObjectsPool;
				lock (obj)
				{
					this.newMeshObjectsPool.Add(newMeshObject);
				}
				Interlocked.Decrement(ref this.totalNewMeshObjects);
				if (this.jobSettings.combineJobMode == MeshCombineJobManager.CombineJobMode.CombinePerFrame && ++num > this.jobSettings.combineMeshesPerFrame && !this.abort)
				{
					break;
				}
			}
			meshCombineJobThread.threadState = MeshCombineJobManager.ThreadState.isFree;
			this.abort = false;
		}

		// Token: 0x040021EA RID: 8682
		public static MeshCombineJobManager instance;

		// Token: 0x040021EB RID: 8683
		public MeshCombineJobManager.JobSettings jobSettings = new MeshCombineJobManager.JobSettings();

		// Token: 0x040021EC RID: 8684
		[NonSerialized]
		public FastList<MeshCombineJobManager.NewMeshObject> newMeshObjectsPool = new FastList<MeshCombineJobManager.NewMeshObject>();

		// Token: 0x040021ED RID: 8685
		public Dictionary<Mesh, MeshCache> meshCacheDictionary = new Dictionary<Mesh, MeshCache>();

		// Token: 0x040021EE RID: 8686
		[NonSerialized]
		public int totalNewMeshObjects;

		// Token: 0x040021EF RID: 8687
		public Queue<MeshCombineJobManager.MeshCombineJob> meshCombineJobs = new Queue<MeshCombineJobManager.MeshCombineJob>();

		// Token: 0x040021F0 RID: 8688
		public MeshCombineJobManager.MeshCombineJobsThread[] meshCombineJobsThreads;

		// Token: 0x040021F1 RID: 8689
		public CamGeometryCapture camGeometryCapture;

		// Token: 0x040021F2 RID: 8690
		public int cores;

		// Token: 0x040021F3 RID: 8691
		public int threadAmount;

		// Token: 0x040021F4 RID: 8692
		public int startThreadId;

		// Token: 0x040021F5 RID: 8693
		public int endThreadId;

		// Token: 0x040021F6 RID: 8694
		public bool abort;

		// Token: 0x040021F7 RID: 8695
		private MeshCache.SubMeshCache tempMeshCache;

		// Token: 0x040021F8 RID: 8696
		private Ray ray = new Ray(Vector3.zero, Vector3.down);

		// Token: 0x040021F9 RID: 8697
		private RaycastHit hitInfo;

		// Token: 0x020007A1 RID: 1953
		[Serializable]
		public class JobSettings
		{
			// Token: 0x06003046 RID: 12358 RVA: 0x000CB9C0 File Offset: 0x000C9BC0
			public void CopySettings(MeshCombineJobManager.JobSettings source)
			{
				this.combineJobMode = source.combineJobMode;
				this.threadAmountMode = source.threadAmountMode;
				this.combineMeshesPerFrame = source.combineMeshesPerFrame;
				this.useMultiThreading = source.useMultiThreading;
				this.useMainThread = source.useMainThread;
				this.customThreadAmount = source.customThreadAmount;
			}

			// Token: 0x06003047 RID: 12359 RVA: 0x000CBA18 File Offset: 0x000C9C18
			public void ReportStatus()
			{
				Debug.Log("---------------------");
				Debug.Log("combineJobMode " + this.combineJobMode);
				Debug.Log("threadAmountMode " + this.threadAmountMode);
				Debug.Log("combineMeshesPerFrame " + this.combineMeshesPerFrame);
				Debug.Log("useMultiThreading " + this.useMultiThreading.ToString());
				Debug.Log("useMainThread " + this.useMainThread.ToString());
				Debug.Log("customThreadAmount " + this.customThreadAmount);
			}

			// Token: 0x040029B0 RID: 10672
			public MeshCombineJobManager.CombineJobMode combineJobMode;

			// Token: 0x040029B1 RID: 10673
			public MeshCombineJobManager.ThreadAmountMode threadAmountMode;

			// Token: 0x040029B2 RID: 10674
			public int combineMeshesPerFrame = 4;

			// Token: 0x040029B3 RID: 10675
			public bool useMultiThreading = true;

			// Token: 0x040029B4 RID: 10676
			public bool useMainThread = true;

			// Token: 0x040029B5 RID: 10677
			public int customThreadAmount = 1;

			// Token: 0x040029B6 RID: 10678
			public bool showStats;
		}

		// Token: 0x020007A2 RID: 1954
		public enum CombineJobMode
		{
			// Token: 0x040029B8 RID: 10680
			CombineAtOnce,
			// Token: 0x040029B9 RID: 10681
			CombinePerFrame
		}

		// Token: 0x020007A3 RID: 1955
		public enum ThreadAmountMode
		{
			// Token: 0x040029BB RID: 10683
			AllThreads,
			// Token: 0x040029BC RID: 10684
			HalfThreads,
			// Token: 0x040029BD RID: 10685
			Custom
		}

		// Token: 0x020007A4 RID: 1956
		public enum ThreadState
		{
			// Token: 0x040029BF RID: 10687
			isFree,
			// Token: 0x040029C0 RID: 10688
			isReady,
			// Token: 0x040029C1 RID: 10689
			isRunning,
			// Token: 0x040029C2 RID: 10690
			hasError
		}

		// Token: 0x020007A5 RID: 1957
		public class MeshCombineJobsThread
		{
			// Token: 0x06003049 RID: 12361 RVA: 0x000CBAEF File Offset: 0x000C9CEF
			public MeshCombineJobsThread(int threadId)
			{
				this.threadId = threadId;
			}

			// Token: 0x0600304A RID: 12362 RVA: 0x000CBB14 File Offset: 0x000C9D14
			public void ExecuteJobsThread(object state)
			{
				MeshCombineJobManager.NewMeshObject newMeshObject = null;
				try
				{
					newMeshObject = null;
					Queue<MeshCombineJobManager.MeshCombineJob> obj = this.meshCombineJobs;
					MeshCombineJobManager.MeshCombineJob meshCombineJob;
					lock (obj)
					{
						meshCombineJob = this.meshCombineJobs.Dequeue();
					}
					Interlocked.Increment(ref MeshCombineJobManager.instance.totalNewMeshObjects);
					FastList<MeshCombineJobManager.NewMeshObject> newMeshObjectsPool = MeshCombineJobManager.instance.newMeshObjectsPool;
					lock (newMeshObjectsPool)
					{
						if (MeshCombineJobManager.instance.newMeshObjectsPool.Count == 0)
						{
							newMeshObject = new MeshCombineJobManager.NewMeshObject();
						}
						else
						{
							newMeshObject = MeshCombineJobManager.instance.newMeshObjectsPool.Dequeue();
						}
					}
					newMeshObject.newPosition = meshCombineJob.position;
					newMeshObject.Combine(meshCombineJob);
					Queue<MeshCombineJobManager.NewMeshObject> obj2 = this.newMeshObjectsDone;
					lock (obj2)
					{
						this.newMeshObjectsDone.Enqueue(newMeshObject);
					}
					this.threadState = MeshCombineJobManager.ThreadState.isReady;
				}
				catch (Exception ex)
				{
					if (newMeshObject != null)
					{
						FastList<MeshCombineJobManager.NewMeshObject> newMeshObjectsPool = MeshCombineJobManager.instance.newMeshObjectsPool;
						lock (newMeshObjectsPool)
						{
							MeshCombineJobManager.instance.newMeshObjectsPool.Add(newMeshObject);
						}
						Interlocked.Decrement(ref MeshCombineJobManager.instance.totalNewMeshObjects);
					}
					Queue<MeshCombineJobManager.MeshCombineJob> obj = this.meshCombineJobs;
					lock (obj)
					{
						this.meshCombineJobs.Clear();
					}
					Debug.LogError("Mesh Combine Studio thread error -> " + ex.ToString());
					this.threadState = MeshCombineJobManager.ThreadState.hasError;
				}
			}

			// Token: 0x040029C3 RID: 10691
			public int threadId;

			// Token: 0x040029C4 RID: 10692
			public MeshCombineJobManager.ThreadState threadState;

			// Token: 0x040029C5 RID: 10693
			public Queue<MeshCombineJobManager.MeshCombineJob> meshCombineJobs = new Queue<MeshCombineJobManager.MeshCombineJob>();

			// Token: 0x040029C6 RID: 10694
			public Queue<MeshCombineJobManager.NewMeshObject> newMeshObjectsDone = new Queue<MeshCombineJobManager.NewMeshObject>();
		}

		// Token: 0x020007A6 RID: 1958
		public class MeshCombineJob
		{
			// Token: 0x0600304B RID: 12363 RVA: 0x000CBCD4 File Offset: 0x000C9ED4
			public MeshCombineJob(MeshCombiner meshCombiner, MeshObjectsHolder meshObjectsHolder, Transform parent, Vector3 position, int startIndex, int length, bool firstMesh, bool intersectsSurface)
			{
				this.meshCombiner = meshCombiner;
				this.meshObjectsHolder = meshObjectsHolder;
				this.parent = parent;
				this.position = position;
				this.startIndex = startIndex;
				this.firstMesh = firstMesh;
				this.intersectsSurface = intersectsSurface;
				this.endIndex = startIndex + length;
				meshObjectsHolder.lodParent.jobsPending++;
				this.name = this.GetHashCode().ToString();
			}

			// Token: 0x040029C7 RID: 10695
			public MeshCombiner meshCombiner;

			// Token: 0x040029C8 RID: 10696
			public MeshObjectsHolder meshObjectsHolder;

			// Token: 0x040029C9 RID: 10697
			public Transform parent;

			// Token: 0x040029CA RID: 10698
			public Vector3 position;

			// Token: 0x040029CB RID: 10699
			public int startIndex;

			// Token: 0x040029CC RID: 10700
			public int endIndex;

			// Token: 0x040029CD RID: 10701
			public bool firstMesh;

			// Token: 0x040029CE RID: 10702
			public bool intersectsSurface;

			// Token: 0x040029CF RID: 10703
			public int backFaceTrianglesRemoved;

			// Token: 0x040029D0 RID: 10704
			public int trianglesRemoved;

			// Token: 0x040029D1 RID: 10705
			public bool abort;

			// Token: 0x040029D2 RID: 10706
			public string name;
		}

		// Token: 0x020007A7 RID: 1959
		public class NewMeshObject
		{
			// Token: 0x0600304C RID: 12364 RVA: 0x000CBD4E File Offset: 0x000C9F4E
			public NewMeshObject()
			{
				this.newMeshCache.Init(true);
			}

			// Token: 0x0600304D RID: 12365 RVA: 0x000CBD70 File Offset: 0x000C9F70
			public void Combine(MeshCombineJobManager.MeshCombineJob meshCombineJob)
			{
				this.meshCombineJob = meshCombineJob;
				if (meshCombineJob.abort)
				{
					return;
				}
				int startIndex = meshCombineJob.startIndex;
				int endIndex = meshCombineJob.endIndex;
				FastList<MeshObject> meshObjects = meshCombineJob.meshObjectsHolder.meshObjects;
				this.newMeshCache.ResetHasBooleans();
				int num = 0;
				int num2 = 0;
				int num3 = endIndex - startIndex;
				MeshCombiner meshCombiner = meshCombineJob.meshCombiner;
				bool validCopyBakedLighting = meshCombiner.validCopyBakedLighting;
				bool validRebakeLighting = meshCombiner.validRebakeLighting;
				bool flag = meshCombiner.rebakeLightingMode == MeshCombiner.RebakeLightingMode.RegenarateLightmapUvs;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				float num7 = 0f;
				if (validRebakeLighting)
				{
					num4 = Mathf.CeilToInt(Mathf.Sqrt((float)num3));
					num7 = 1f / (float)num4;
				}
				this.allSkipped = true;
				for (int i = startIndex; i < endIndex; i++)
				{
					MeshObject meshObject = meshObjects.items[i];
					int subMeshIndex = meshObject.subMeshIndex;
					MeshCache.SubMeshCache subMeshCache = meshObject.meshCache.subMeshCache[subMeshIndex];
					int vertexCount = subMeshCache.vertexCount;
					this.HasArray<Vector3>(ref this.newMeshCache.hasNormals, subMeshCache.hasNormals, ref this.newMeshCache.normals, subMeshCache.normals, vertexCount, num, false, default(Vector3));
					this.HasArray<Vector4>(ref this.newMeshCache.hasTangents, subMeshCache.hasTangents, ref this.newMeshCache.tangents, subMeshCache.tangents, vertexCount, num, true, new Vector4(1f, 1f, 1f, 1f));
					this.HasArray<Vector2>(ref this.newMeshCache.hasUv, subMeshCache.hasUv, ref this.newMeshCache.uv, subMeshCache.uv, vertexCount, num, false, default(Vector2));
					this.HasArray<Vector2>(ref this.newMeshCache.hasUv2, subMeshCache.hasUv2, ref this.newMeshCache.uv2, subMeshCache.uv2, vertexCount, num, false, default(Vector2));
					this.HasArray<Vector2>(ref this.newMeshCache.hasUv3, subMeshCache.hasUv3, ref this.newMeshCache.uv3, subMeshCache.uv3, vertexCount, num, false, default(Vector2));
					this.HasArray<Vector2>(ref this.newMeshCache.hasUv4, subMeshCache.hasUv4, ref this.newMeshCache.uv4, subMeshCache.uv4, vertexCount, num, false, default(Vector2));
					this.HasArray<Color32>(ref this.newMeshCache.hasColors, subMeshCache.hasColors, ref this.newMeshCache.colors32, subMeshCache.colors32, vertexCount, num, true, new Color32(1, 1, 1, 1));
					num += vertexCount;
				}
				num = 0;
				for (int j = startIndex; j < endIndex; j++)
				{
					MeshObject meshObject2 = meshObjects.items[j];
					if (!meshObject2.skip)
					{
						this.allSkipped = false;
						MeshCache meshCache = meshObject2.meshCache;
						int subMeshIndex2 = meshObject2.subMeshIndex;
						MeshCache.SubMeshCache subMeshCache2 = meshCache.subMeshCache[subMeshIndex2];
						Vector3 scale = meshObject2.scale;
						bool flag2 = false;
						if (scale.x < 0f)
						{
							flag2 = !flag2;
						}
						if (scale.y < 0f)
						{
							flag2 = !flag2;
						}
						if (scale.z < 0f)
						{
							flag2 = !flag2;
						}
						int num8 = 1;
						if (flag2)
						{
							num8 = -1;
						}
						Vector3[] vertices = subMeshCache2.vertices;
						Vector3[] normals = subMeshCache2.normals;
						Vector4[] tangents = subMeshCache2.tangents;
						Vector2[] uv = subMeshCache2.uv;
						Vector2[] uv2 = subMeshCache2.uv2;
						Vector2[] uv3 = subMeshCache2.uv3;
						Vector2[] uv4 = subMeshCache2.uv4;
						Color32[] colors = subMeshCache2.colors32;
						int[] triangles = subMeshCache2.triangles;
						int vertexCount2 = subMeshCache2.vertexCount;
						int[] triangles2 = this.newMeshCache.triangles;
						Vector3[] vertices2 = this.newMeshCache.vertices;
						Vector3[] normals2 = this.newMeshCache.normals;
						Vector4[] tangents2 = this.newMeshCache.tangents;
						Vector2[] uv5 = this.newMeshCache.uv;
						Vector2[] uv6 = this.newMeshCache.uv2;
						Vector2[] uv7 = this.newMeshCache.uv3;
						Vector2[] uv8 = this.newMeshCache.uv4;
						Color32[] colors2 = this.newMeshCache.colors32;
						bool hasNormals = subMeshCache2.hasNormals;
						bool hasTangents = subMeshCache2.hasTangents;
						Vector3 position = meshCombineJob.position;
						Matrix4x4 mt = meshObject2.cachedGO.mt;
						Matrix4x4 mtNormals = meshObject2.cachedGO.mtNormals;
						for (int k = 0; k < vertices.Length; k++)
						{
							int num9 = k + num;
							vertices2[num9] = mt.MultiplyPoint3x4(vertices[k]) - position;
						}
						if (hasNormals)
						{
							for (int l = 0; l < vertices.Length; l++)
							{
								int num10 = l + num;
								normals2[num10] = mtNormals.MultiplyVector(normals[l]);
							}
						}
						if (hasTangents)
						{
							for (int m = 0; m < vertices.Length; m++)
							{
								int num11 = m + num;
								tangents2[num11] = mt.MultiplyVector(tangents[m]);
								tangents2[num11].w = tangents[m].w * (float)num8;
							}
						}
						if (subMeshCache2.hasUv)
						{
							Array.Copy(uv, 0, uv5, num, vertexCount2);
						}
						if (subMeshCache2.hasUv2)
						{
							if (validCopyBakedLighting)
							{
								Vector4 lightmapScaleOffset = meshObject2.lightmapScaleOffset;
								Vector2 b = new Vector2(lightmapScaleOffset.z, lightmapScaleOffset.w);
								Vector2 vector = new Vector2(lightmapScaleOffset.x, lightmapScaleOffset.y);
								for (int n = 0; n < vertices.Length; n++)
								{
									int num12 = n + num;
									uv6[num12] = new Vector2(uv2[n].x * vector.x, uv2[n].y * vector.y) + b;
								}
							}
							else if (validRebakeLighting)
							{
								if (!flag)
								{
									Vector2 b2 = new Vector2(num7 * (float)num5, num7 * (float)num6);
									for (int num13 = 0; num13 < vertices.Length; num13++)
									{
										int num14 = num13 + num;
										uv6[num14] = uv2[num13] * num7 + b2;
									}
								}
							}
							else
							{
								Array.Copy(uv2, 0, uv6, num, vertexCount2);
							}
						}
						if (subMeshCache2.hasUv3)
						{
							Array.Copy(uv3, 0, uv7, num, vertexCount2);
						}
						if (subMeshCache2.hasUv4)
						{
							Array.Copy(uv4, 0, uv8, num, vertexCount2);
						}
						if (subMeshCache2.hasColors)
						{
							Array.Copy(colors, 0, colors2, num, vertexCount2);
						}
						if (flag2)
						{
							for (int num15 = 0; num15 < triangles.Length; num15 += 3)
							{
								triangles2[num15 + num2] = triangles[num15 + 2] + num;
								triangles2[num15 + num2 + 1] = triangles[num15 + 1] + num;
								triangles2[num15 + num2 + 2] = triangles[num15] + num;
							}
						}
						else
						{
							for (int num16 = 0; num16 < triangles.Length; num16++)
							{
								triangles2[num16 + num2] = triangles[num16] + num;
							}
						}
						num += vertexCount2;
						num2 += triangles.Length;
						if (++num5 >= num4)
						{
							num5 = 0;
							num6++;
						}
					}
				}
				this.newMeshCache.vertexCount = num;
				this.newMeshCache.triangleCount = num2;
				if (meshCombiner.removeBackFaceTriangles)
				{
					this.RemoveBackFaceTriangles();
				}
			}

			// Token: 0x0600304E RID: 12366 RVA: 0x000CC480 File Offset: 0x000CA680
			private void PrintMissingArrayWarning(MeshCombiner meshCombiner, GameObject go, Mesh mesh, string text)
			{
				Debug.Log(string.Concat(new string[]
				{
					"GameObject: ",
					go.name,
					" Mesh ",
					mesh.name,
					" has missing ",
					text,
					" while the other meshes have them. Click the 'Select Meshes in Project' button to change the import settings."
				}));
				meshCombiner.selectImportSettingsMeshes.Add(mesh);
			}

			// Token: 0x0600304F RID: 12367 RVA: 0x000CC4E4 File Offset: 0x000CA6E4
			private void HasArray<T>(ref bool hasNewArray, bool hasArray, ref T[] newArray, Array array, int vertexCount, int totalVertices, bool useDefaultValue = false, T defaultValue = default(T))
			{
				if (hasArray)
				{
					if (!hasNewArray)
					{
						if (newArray == null)
						{
							newArray = new T[65534];
							if (useDefaultValue)
							{
								this.FillArray<T>(newArray, 0, totalVertices, defaultValue);
							}
						}
						else if (useDefaultValue)
						{
							this.FillArray<T>(newArray, 0, totalVertices, defaultValue);
						}
						else
						{
							Array.Clear(newArray, 0, totalVertices);
						}
					}
					hasNewArray = true;
					return;
				}
				if (hasNewArray)
				{
					if (useDefaultValue)
					{
						this.FillArray<T>(newArray, totalVertices, vertexCount, defaultValue);
						return;
					}
					Array.Clear(newArray, totalVertices, vertexCount);
				}
			}

			// Token: 0x06003050 RID: 12368 RVA: 0x000CC560 File Offset: 0x000CA760
			private void FillArray<T>(T[] array, int offset, int length, T value)
			{
				length += offset;
				for (int i = offset; i < length; i++)
				{
					array[i] = value;
				}
			}

			// Token: 0x06003051 RID: 12369 RVA: 0x000CC588 File Offset: 0x000CA788
			public void RemoveTrianglesBelowSurface(Transform t, MeshCombineJobManager.MeshCombineJob meshCombineJob)
			{
				if (this.vertexIsBelow == null)
				{
					this.vertexIsBelow = new byte[65534];
				}
				Ray ray = MeshCombineJobManager.instance.ray;
				RaycastHit hitInfo = MeshCombineJobManager.instance.hitInfo;
				Vector3 vector = Vector3.zero;
				int layerMask = meshCombineJob.meshCombiner.surfaceLayerMask;
				float maxSurfaceHeight = meshCombineJob.meshCombiner.maxSurfaceHeight;
				Vector3[] vertices = this.newMeshCache.vertices;
				int[] triangles = this.newMeshCache.triangles;
				FastList<MeshObject> meshObjects = meshCombineJob.meshObjectsHolder.meshObjects;
				int startIndex = meshCombineJob.startIndex;
				int endIndex = meshCombineJob.endIndex;
				for (int i = startIndex; i < endIndex; i++)
				{
					MeshObject meshObject = meshObjects.items[i];
					if (meshObject.intersectsSurface)
					{
						int startNewTriangleIndex = meshObject.startNewTriangleIndex;
						int num = meshObject.newTriangleCount + startNewTriangleIndex;
						for (int j = startNewTriangleIndex; j < num; j += 3)
						{
							bool flag = false;
							for (int k = 0; k < 3; k++)
							{
								int num2 = triangles[j + k];
								if (num2 != -1)
								{
									byte b = this.vertexIsBelow[num2];
									if (b == 0)
									{
										vector = t.TransformPoint(vertices[num2]);
										ray.origin = new Vector3(vector.x, maxSurfaceHeight, vector.z);
										if (!Physics.Raycast(ray, out hitInfo, maxSurfaceHeight - vector.y, layerMask))
										{
											this.vertexIsBelow[num2] = 2;
											flag = true;
											break;
										}
										if (vector.y >= hitInfo.point.y)
										{
											this.vertexIsBelow[num2] = 2;
											break;
										}
										b = (this.vertexIsBelow[num2] = 1);
									}
									if (b != 1)
									{
										flag = true;
										break;
									}
								}
							}
							if (!flag)
							{
								meshCombineJob.trianglesRemoved += 3;
								triangles[j] = -1;
							}
						}
					}
				}
				Array.Clear(this.vertexIsBelow, 0, vertices.Length);
			}

			// Token: 0x06003052 RID: 12370 RVA: 0x000CC764 File Offset: 0x000CA964
			public void RemoveBackFaceTriangles()
			{
				int[] triangles = this.newMeshCache.triangles;
				Vector3[] normals = this.newMeshCache.normals;
				int triangleCount = this.newMeshCache.triangleCount;
				MeshCombiner meshCombiner = this.meshCombineJob.meshCombiner;
				bool flag = meshCombiner.backFaceTriangleMode == MeshCombiner.BackFaceTriangleMode.Direction;
				Bounds backFaceBounds = meshCombiner.backFaceBounds;
				Vector3 min = backFaceBounds.min;
				Vector3 max = backFaceBounds.max;
				Vector3[] vertices = this.newMeshCache.vertices;
				Vector3 lhs = Quaternion.Euler(meshCombiner.backFaceDirection) * Vector3.forward;
				for (int i = 0; i < triangleCount; i += 3)
				{
					Vector3 vector = Vector3.zero;
					Vector3 vector2 = Vector3.zero;
					for (int j = 0; j < 3; j++)
					{
						int num = triangles[i + j];
						vector2 += vertices[num];
						vector += normals[num];
					}
					vector2 /= 3f;
					vector /= 3f;
					if (!flag)
					{
						Vector3 b;
						b.x = ((vector.x > 0f) ? max.x : min.x);
						b.y = ((vector.y > 0f) ? max.y : min.y);
						b.z = ((vector.z > 0f) ? max.z : min.z);
						lhs = this.newPosition + vector2 - b;
					}
					if (Vector3.Dot(lhs, vector) >= 0f)
					{
						triangles[i] = -1;
						this.meshCombineJob.backFaceTrianglesRemoved += 3;
					}
				}
			}

			// Token: 0x06003053 RID: 12371 RVA: 0x000CC918 File Offset: 0x000CAB18
			public void WeldVertices(MeshCombineJobManager.MeshCombineJob meshCombineJob)
			{
				if (MeshCombineJobManager.NewMeshObject.weldVertices == null)
				{
					MeshCombineJobManager.NewMeshObject.weldVertices = new FastList<Vector3>(65534);
				}
				else
				{
					MeshCombineJobManager.NewMeshObject.weldVertices.FastClear();
				}
				Vector3[] vertices = this.newMeshCache.vertices;
				int vertexCount = this.newMeshCache.vertexCount;
				int[] array = new int[vertexCount];
				Dictionary<Vector3, int> dictionary = new Dictionary<Vector3, int>();
				if (meshCombineJob.meshCombiner.weldSnapVertices)
				{
					float num = meshCombineJob.meshCombiner.weldSnapSize;
					if (num < 1E-05f)
					{
						num = 1E-05f;
					}
					for (int i = 0; i < vertexCount; i++)
					{
						Vector3 vector = Mathw.SnapRound(vertices[i], num);
						int num2;
						if (dictionary.TryGetValue(vector, out num2))
						{
							array[i] = num2;
						}
						else
						{
							dictionary[vector] = (array[i] = MeshCombineJobManager.NewMeshObject.weldVertices.Count);
							MeshCombineJobManager.NewMeshObject.weldVertices.Add(vector);
						}
					}
				}
				else
				{
					for (int j = 0; j < vertexCount; j++)
					{
						Vector3 vector2 = vertices[j];
						int num2;
						if (dictionary.TryGetValue(vector2, out num2))
						{
							array[j] = num2;
						}
						else
						{
							dictionary[vector2] = (array[j] = MeshCombineJobManager.NewMeshObject.weldVertices.Count);
							MeshCombineJobManager.NewMeshObject.weldVertices.Add(vector2);
						}
					}
				}
				int[] triangles = this.newMeshCache.triangles;
				int triangleCount = this.newMeshCache.triangleCount;
				for (int k = 0; k < triangleCount; k++)
				{
					if (triangles[k] != -1)
					{
						triangles[k] = array[triangles[k]];
					}
				}
				Array.Copy(MeshCombineJobManager.NewMeshObject.weldVertices.items, this.newMeshCache.vertices, MeshCombineJobManager.NewMeshObject.weldVertices.Count);
				this.newMeshCache.vertexCount = MeshCombineJobManager.NewMeshObject.weldVertices.Count;
			}

			// Token: 0x06003054 RID: 12372 RVA: 0x000CCAC4 File Offset: 0x000CACC4
			private void ArrangeTriangles()
			{
				int num = this.newMeshCache.triangleCount;
				int[] triangles = this.newMeshCache.triangles;
				for (int i = 0; i < num; i += 3)
				{
					if (triangles[i] == -1)
					{
						triangles[i] = triangles[num - 3];
						triangles[i + 1] = triangles[num - 2];
						triangles[i + 2] = triangles[num - 1];
						i -= 3;
						num -= 3;
					}
				}
				this.newMeshCache.triangleCount = num;
			}

			// Token: 0x06003055 RID: 12373 RVA: 0x000CCB2C File Offset: 0x000CAD2C
			public void CreateMesh()
			{
				MeshCombiner meshCombiner = this.meshCombineJob.meshCombiner;
				if (meshCombiner.instantiatePrefab == null)
				{
					Debug.LogError("Mesh Combine Studio -> Instantiate Prefab = null");
					return;
				}
				MeshObjectsHolder meshObjectsHolder = this.meshCombineJob.meshObjectsHolder;
				if (this.meshCombineJob.parent == null)
				{
					this.meshCombineJob.parent = this.meshCombineJob.meshCombiner.transform;
				}
				GameObject gameObject = Object.Instantiate<GameObject>(meshCombiner.instantiatePrefab, this.newPosition, Quaternion.identity, this.meshCombineJob.parent);
				CachedComponents component = gameObject.GetComponent<CachedComponents>();
				MeshRenderer mr = component.mr;
				MeshFilter mf = component.mf;
				string name = meshObjectsHolder.mat.name;
				gameObject.name = name;
				if (this.meshCombineJob.intersectsSurface)
				{
					if (meshCombiner.noColliders)
					{
						MeshCombineJobManager.instance.camGeometryCapture.RemoveTrianglesBelowSurface(gameObject.transform, this.meshCombineJob, this.newMeshCache, ref this.vertexIsBelow);
					}
					else
					{
						this.RemoveTrianglesBelowSurface(gameObject.transform, this.meshCombineJob);
					}
				}
				if (meshCombiner.weldVertices)
				{
					this.WeldVertices(this.meshCombineJob);
				}
				if (this.meshCombineJob.trianglesRemoved > 0 || this.meshCombineJob.backFaceTrianglesRemoved > 0 || meshCombiner.weldVertices)
				{
					this.ArrangeTriangles();
					if (MeshCombineJobManager.instance.tempMeshCache == null)
					{
						MeshCombineJobManager.instance.tempMeshCache = new MeshCache.SubMeshCache();
						MeshCombineJobManager.instance.tempMeshCache.Init(false);
					}
					MeshCombineJobManager.instance.tempMeshCache.CopySubMeshCache(this.newMeshCache);
					this.newMeshCache.RebuildVertexBuffer(MeshCombineJobManager.instance.tempMeshCache, false);
				}
				Mesh mesh = new Mesh();
				mesh.name = name;
				int vertexCount = this.newMeshCache.vertexCount;
				int triangleCount = this.newMeshCache.triangleCount;
				meshCombiner.totalVertices += vertexCount;
				meshCombiner.totalTriangles += triangleCount;
				MeshExtension.ApplyVertices(mesh, this.newMeshCache.vertices, vertexCount);
				MeshExtension.ApplyTriangles(mesh, this.newMeshCache.triangles, triangleCount);
				if (meshCombiner.weldVertices)
				{
					if (this.newMeshCache.hasNormals && meshCombiner.weldIncludeNormals)
					{
						mesh.RecalculateNormals();
					}
				}
				else
				{
					if (this.newMeshCache.hasNormals)
					{
						MeshExtension.ApplyNormals(mesh, this.newMeshCache.normals, vertexCount);
					}
					if (this.newMeshCache.hasTangents)
					{
						MeshExtension.ApplyTangents(mesh, this.newMeshCache.tangents, vertexCount);
					}
					if (this.newMeshCache.hasUv)
					{
						MeshExtension.ApplyUvs(mesh, this.newMeshCache.uv, 0, vertexCount);
					}
					if (this.newMeshCache.hasUv2)
					{
						MeshExtension.ApplyUvs(mesh, this.newMeshCache.uv2, 1, vertexCount);
					}
					if (this.newMeshCache.hasUv3)
					{
						MeshExtension.ApplyUvs(mesh, this.newMeshCache.uv3, 2, vertexCount);
					}
					if (this.newMeshCache.hasUv4)
					{
						MeshExtension.ApplyUvs(mesh, this.newMeshCache.uv4, 3, vertexCount);
					}
					if (this.newMeshCache.hasColors)
					{
						MeshExtension.ApplyColors32(mesh, this.newMeshCache.colors32, vertexCount);
					}
				}
				if (meshCombiner.addMeshColliders)
				{
					bool flag = true;
					if (meshCombiner.addMeshCollidersInRange && !meshCombiner.addMeshCollidersBounds.Contains(gameObject.transform.position))
					{
						flag = false;
					}
					if (flag)
					{
						meshCombiner.addMeshCollidersList.Add(new MeshColliderAdd(gameObject, mesh));
					}
				}
				if (meshCombiner.makeMeshesUnreadable)
				{
					mesh.UploadMeshData(true);
				}
				meshCombiner.newDrawCalls++;
				mr.sharedMaterial = meshObjectsHolder.mat;
				mf.sharedMesh = mesh;
				component.garbageCollectMesh.mesh = mesh;
				meshObjectsHolder.combineCondition.WriteToGameObject(gameObject, mr);
				if (meshCombiner.twoSidedShadows && this.meshCombineJob.backFaceTrianglesRemoved > 0)
				{
					mr.shadowCastingMode = ShadowCastingMode.TwoSided;
				}
				if (meshObjectsHolder.newCachedGOs == null)
				{
					meshObjectsHolder.newCachedGOs = new FastList<CachedGameObject>();
				}
				meshObjectsHolder.newCachedGOs.Add(new CachedGameObject(component));
				meshObjectsHolder.lodParent.lodLevels[meshObjectsHolder.lodLevel].newMeshRenderers.Add(mr);
				ObjectOctree.LODParent lodParent = meshObjectsHolder.lodParent;
				int num = lodParent.jobsPending - 1;
				lodParent.jobsPending = num;
				if (num == 0 && meshObjectsHolder.lodParent.lodLevels.Length > 1)
				{
					meshObjectsHolder.lodParent.AssignLODGroup(meshCombiner);
				}
			}

			// Token: 0x040029D3 RID: 10707
			public static FastList<Vector3> weldVertices;

			// Token: 0x040029D4 RID: 10708
			public MeshCombineJobManager.MeshCombineJob meshCombineJob;

			// Token: 0x040029D5 RID: 10709
			public MeshCache.SubMeshCache newMeshCache = new MeshCache.SubMeshCache();

			// Token: 0x040029D6 RID: 10710
			public bool allSkipped;

			// Token: 0x040029D7 RID: 10711
			public Vector3 newPosition;

			// Token: 0x040029D8 RID: 10712
			private byte[] vertexIsBelow;

			// Token: 0x040029D9 RID: 10713
			private const byte belowSurface = 1;

			// Token: 0x040029DA RID: 10714
			private const byte aboveSurface = 2;
		}
	}
}
