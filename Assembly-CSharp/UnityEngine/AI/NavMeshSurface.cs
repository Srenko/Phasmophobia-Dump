using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.AI
{
	// Token: 0x0200046F RID: 1135
	[ExecuteInEditMode]
	[DefaultExecutionOrder(-102)]
	[AddComponentMenu("Navigation/NavMeshSurface", 30)]
	[HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
	public class NavMeshSurface : MonoBehaviour
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x000AC8E8 File Offset: 0x000AAAE8
		// (set) Token: 0x0600232E RID: 9006 RVA: 0x000AC8F0 File Offset: 0x000AAAF0
		public int agentTypeID
		{
			get
			{
				return this.m_AgentTypeID;
			}
			set
			{
				this.m_AgentTypeID = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x000AC8F9 File Offset: 0x000AAAF9
		// (set) Token: 0x06002330 RID: 9008 RVA: 0x000AC901 File Offset: 0x000AAB01
		public CollectObjects collectObjects
		{
			get
			{
				return this.m_CollectObjects;
			}
			set
			{
				this.m_CollectObjects = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x000AC90A File Offset: 0x000AAB0A
		// (set) Token: 0x06002332 RID: 9010 RVA: 0x000AC912 File Offset: 0x000AAB12
		public Vector3 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x000AC91B File Offset: 0x000AAB1B
		// (set) Token: 0x06002334 RID: 9012 RVA: 0x000AC923 File Offset: 0x000AAB23
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x000AC92C File Offset: 0x000AAB2C
		// (set) Token: 0x06002336 RID: 9014 RVA: 0x000AC934 File Offset: 0x000AAB34
		public LayerMask layerMask
		{
			get
			{
				return this.m_LayerMask;
			}
			set
			{
				this.m_LayerMask = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x000AC93D File Offset: 0x000AAB3D
		// (set) Token: 0x06002338 RID: 9016 RVA: 0x000AC945 File Offset: 0x000AAB45
		public NavMeshCollectGeometry useGeometry
		{
			get
			{
				return this.m_UseGeometry;
			}
			set
			{
				this.m_UseGeometry = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x000AC94E File Offset: 0x000AAB4E
		// (set) Token: 0x0600233A RID: 9018 RVA: 0x000AC956 File Offset: 0x000AAB56
		public int defaultArea
		{
			get
			{
				return this.m_DefaultArea;
			}
			set
			{
				this.m_DefaultArea = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x000AC95F File Offset: 0x000AAB5F
		// (set) Token: 0x0600233C RID: 9020 RVA: 0x000AC967 File Offset: 0x000AAB67
		public bool ignoreNavMeshAgent
		{
			get
			{
				return this.m_IgnoreNavMeshAgent;
			}
			set
			{
				this.m_IgnoreNavMeshAgent = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x000AC970 File Offset: 0x000AAB70
		// (set) Token: 0x0600233E RID: 9022 RVA: 0x000AC978 File Offset: 0x000AAB78
		public bool ignoreNavMeshObstacle
		{
			get
			{
				return this.m_IgnoreNavMeshObstacle;
			}
			set
			{
				this.m_IgnoreNavMeshObstacle = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x000AC981 File Offset: 0x000AAB81
		// (set) Token: 0x06002340 RID: 9024 RVA: 0x000AC989 File Offset: 0x000AAB89
		public bool overrideTileSize
		{
			get
			{
				return this.m_OverrideTileSize;
			}
			set
			{
				this.m_OverrideTileSize = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x000AC992 File Offset: 0x000AAB92
		// (set) Token: 0x06002342 RID: 9026 RVA: 0x000AC99A File Offset: 0x000AAB9A
		public int tileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				this.m_TileSize = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x000AC9A3 File Offset: 0x000AABA3
		// (set) Token: 0x06002344 RID: 9028 RVA: 0x000AC9AB File Offset: 0x000AABAB
		public bool overrideVoxelSize
		{
			get
			{
				return this.m_OverrideVoxelSize;
			}
			set
			{
				this.m_OverrideVoxelSize = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x000AC9B4 File Offset: 0x000AABB4
		// (set) Token: 0x06002346 RID: 9030 RVA: 0x000AC9BC File Offset: 0x000AABBC
		public float voxelSize
		{
			get
			{
				return this.m_VoxelSize;
			}
			set
			{
				this.m_VoxelSize = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x000AC9C5 File Offset: 0x000AABC5
		// (set) Token: 0x06002348 RID: 9032 RVA: 0x000AC9CD File Offset: 0x000AABCD
		public bool buildHeightMesh
		{
			get
			{
				return this.m_BuildHeightMesh;
			}
			set
			{
				this.m_BuildHeightMesh = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x000AC9D6 File Offset: 0x000AABD6
		// (set) Token: 0x0600234A RID: 9034 RVA: 0x000AC9DE File Offset: 0x000AABDE
		public NavMeshData navMeshData
		{
			get
			{
				return this.m_NavMeshData;
			}
			set
			{
				this.m_NavMeshData = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x000AC9E7 File Offset: 0x000AABE7
		public static List<NavMeshSurface> activeSurfaces
		{
			get
			{
				return NavMeshSurface.s_NavMeshSurfaces;
			}
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000AC9EE File Offset: 0x000AABEE
		private void OnEnable()
		{
			NavMeshSurface.Register(this);
			this.AddData();
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000AC9FC File Offset: 0x000AABFC
		private void OnDisable()
		{
			this.RemoveData();
			NavMeshSurface.Unregister(this);
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000ACA0C File Offset: 0x000AAC0C
		public void AddData()
		{
			if (this.m_NavMeshDataInstance.valid)
			{
				return;
			}
			if (this.m_NavMeshData != null)
			{
				this.m_NavMeshDataInstance = NavMesh.AddNavMeshData(this.m_NavMeshData, base.transform.position, base.transform.rotation);
				this.m_NavMeshDataInstance.owner = this;
			}
			this.m_LastPosition = base.transform.position;
			this.m_LastRotation = base.transform.rotation;
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000ACA8A File Offset: 0x000AAC8A
		public void RemoveData()
		{
			this.m_NavMeshDataInstance.Remove();
			this.m_NavMeshDataInstance = default(NavMeshDataInstance);
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000ACAA4 File Offset: 0x000AACA4
		public NavMeshBuildSettings GetBuildSettings()
		{
			NavMeshBuildSettings settingsByID = NavMesh.GetSettingsByID(this.m_AgentTypeID);
			if (settingsByID.agentTypeID == -1)
			{
				Debug.LogWarning("No build settings for agent type ID " + this.agentTypeID, this);
				settingsByID.agentTypeID = this.m_AgentTypeID;
			}
			if (this.overrideTileSize)
			{
				settingsByID.overrideTileSize = true;
				settingsByID.tileSize = this.tileSize;
			}
			if (this.overrideVoxelSize)
			{
				settingsByID.overrideVoxelSize = true;
				settingsByID.voxelSize = this.voxelSize;
			}
			return settingsByID;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000ACB2C File Offset: 0x000AAD2C
		public void BuildNavMesh()
		{
			List<NavMeshBuildSource> sources = this.CollectSources();
			Bounds localBounds = new Bounds(this.m_Center, NavMeshSurface.Abs(this.m_Size));
			if (this.m_CollectObjects == CollectObjects.All || this.m_CollectObjects == CollectObjects.Children)
			{
				localBounds = this.CalculateWorldBounds(sources);
			}
			NavMeshData navMeshData = NavMeshBuilder.BuildNavMeshData(this.GetBuildSettings(), sources, localBounds, base.transform.position, base.transform.rotation);
			if (navMeshData != null)
			{
				navMeshData.name = base.gameObject.name;
				this.RemoveData();
				this.m_NavMeshData = navMeshData;
				if (base.isActiveAndEnabled)
				{
					this.AddData();
				}
			}
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000ACBCC File Offset: 0x000AADCC
		public AsyncOperation UpdateNavMesh(NavMeshData data)
		{
			List<NavMeshBuildSource> sources = this.CollectSources();
			Bounds localBounds = new Bounds(this.m_Center, NavMeshSurface.Abs(this.m_Size));
			if (this.m_CollectObjects == CollectObjects.All || this.m_CollectObjects == CollectObjects.Children)
			{
				localBounds = this.CalculateWorldBounds(sources);
			}
			return NavMeshBuilder.UpdateNavMeshDataAsync(data, this.GetBuildSettings(), sources, localBounds);
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000ACC20 File Offset: 0x000AAE20
		private static void Register(NavMeshSurface surface)
		{
			if (NavMeshSurface.s_NavMeshSurfaces.Count == 0)
			{
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Combine(NavMesh.onPreUpdate, new NavMesh.OnNavMeshPreUpdate(NavMeshSurface.UpdateActive));
			}
			if (!NavMeshSurface.s_NavMeshSurfaces.Contains(surface))
			{
				NavMeshSurface.s_NavMeshSurfaces.Add(surface);
			}
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000ACC71 File Offset: 0x000AAE71
		private static void Unregister(NavMeshSurface surface)
		{
			NavMeshSurface.s_NavMeshSurfaces.Remove(surface);
			if (NavMeshSurface.s_NavMeshSurfaces.Count == 0)
			{
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Remove(NavMesh.onPreUpdate, new NavMesh.OnNavMeshPreUpdate(NavMeshSurface.UpdateActive));
			}
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000ACCAC File Offset: 0x000AAEAC
		private static void UpdateActive()
		{
			for (int i = 0; i < NavMeshSurface.s_NavMeshSurfaces.Count; i++)
			{
				NavMeshSurface.s_NavMeshSurfaces[i].UpdateDataIfTransformChanged();
			}
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000ACCE0 File Offset: 0x000AAEE0
		private void AppendModifierVolumes(ref List<NavMeshBuildSource> sources)
		{
			List<NavMeshModifierVolume> list;
			if (this.m_CollectObjects == CollectObjects.Children)
			{
				list = new List<NavMeshModifierVolume>(base.GetComponentsInChildren<NavMeshModifierVolume>());
				list.RemoveAll((NavMeshModifierVolume x) => !x.isActiveAndEnabled);
			}
			else
			{
				list = NavMeshModifierVolume.activeModifiers;
			}
			foreach (NavMeshModifierVolume navMeshModifierVolume in list)
			{
				if ((this.m_LayerMask & 1 << navMeshModifierVolume.gameObject.layer) != 0 && navMeshModifierVolume.AffectsAgentType(this.m_AgentTypeID))
				{
					Vector3 pos = navMeshModifierVolume.transform.TransformPoint(navMeshModifierVolume.center);
					Vector3 lossyScale = navMeshModifierVolume.transform.lossyScale;
					Vector3 size = new Vector3(navMeshModifierVolume.size.x * Mathf.Abs(lossyScale.x), navMeshModifierVolume.size.y * Mathf.Abs(lossyScale.y), navMeshModifierVolume.size.z * Mathf.Abs(lossyScale.z));
					NavMeshBuildSource item = default(NavMeshBuildSource);
					item.shape = NavMeshBuildSourceShape.ModifierBox;
					item.transform = Matrix4x4.TRS(pos, navMeshModifierVolume.transform.rotation, Vector3.one);
					item.size = size;
					item.area = navMeshModifierVolume.area;
					sources.Add(item);
				}
			}
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000ACE68 File Offset: 0x000AB068
		private List<NavMeshBuildSource> CollectSources()
		{
			List<NavMeshBuildSource> list = new List<NavMeshBuildSource>();
			List<NavMeshBuildMarkup> list2 = new List<NavMeshBuildMarkup>();
			List<NavMeshModifier> list3;
			if (this.m_CollectObjects == CollectObjects.Children)
			{
				list3 = new List<NavMeshModifier>(base.GetComponentsInChildren<NavMeshModifier>());
				list3.RemoveAll((NavMeshModifier x) => !x.isActiveAndEnabled);
			}
			else
			{
				list3 = NavMeshModifier.activeModifiers;
			}
			foreach (NavMeshModifier navMeshModifier in list3)
			{
				if ((this.m_LayerMask & 1 << navMeshModifier.gameObject.layer) != 0 && navMeshModifier.AffectsAgentType(this.m_AgentTypeID))
				{
					list2.Add(new NavMeshBuildMarkup
					{
						root = navMeshModifier.transform,
						overrideArea = navMeshModifier.overrideArea,
						area = navMeshModifier.area,
						ignoreFromBuild = navMeshModifier.ignoreFromBuild
					});
				}
			}
			if (this.m_CollectObjects == CollectObjects.All)
			{
				NavMeshBuilder.CollectSources(null, this.m_LayerMask, this.m_UseGeometry, this.m_DefaultArea, list2, list);
			}
			else if (this.m_CollectObjects == CollectObjects.Children)
			{
				NavMeshBuilder.CollectSources(base.transform, this.m_LayerMask, this.m_UseGeometry, this.m_DefaultArea, list2, list);
			}
			else if (this.m_CollectObjects == CollectObjects.Volume)
			{
				NavMeshBuilder.CollectSources(NavMeshSurface.GetWorldBounds(Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one), new Bounds(this.m_Center, this.m_Size)), this.m_LayerMask, this.m_UseGeometry, this.m_DefaultArea, list2, list);
			}
			if (this.m_IgnoreNavMeshAgent)
			{
				list.RemoveAll((NavMeshBuildSource x) => x.component != null && x.component.gameObject.GetComponent<NavMeshAgent>() != null);
			}
			if (this.m_IgnoreNavMeshObstacle)
			{
				list.RemoveAll((NavMeshBuildSource x) => x.component != null && x.component.gameObject.GetComponent<NavMeshObstacle>() != null);
			}
			this.AppendModifierVolumes(ref list);
			return list;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000AD098 File Offset: 0x000AB298
		private static Vector3 Abs(Vector3 v)
		{
			return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000AD0C0 File Offset: 0x000AB2C0
		private static Bounds GetWorldBounds(Matrix4x4 mat, Bounds bounds)
		{
			Vector3 a = NavMeshSurface.Abs(mat.MultiplyVector(Vector3.right));
			Vector3 a2 = NavMeshSurface.Abs(mat.MultiplyVector(Vector3.up));
			Vector3 a3 = NavMeshSurface.Abs(mat.MultiplyVector(Vector3.forward));
			Vector3 center = mat.MultiplyPoint(bounds.center);
			Vector3 size = a * bounds.size.x + a2 * bounds.size.y + a3 * bounds.size.z;
			return new Bounds(center, size);
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000AD158 File Offset: 0x000AB358
		private Bounds CalculateWorldBounds(List<NavMeshBuildSource> sources)
		{
			Matrix4x4 inverse = Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one).inverse;
			Bounds result = default(Bounds);
			foreach (NavMeshBuildSource navMeshBuildSource in sources)
			{
				switch (navMeshBuildSource.shape)
				{
				case NavMeshBuildSourceShape.Mesh:
				{
					Mesh mesh = navMeshBuildSource.sourceObject as Mesh;
					result.Encapsulate(NavMeshSurface.GetWorldBounds(inverse * navMeshBuildSource.transform, mesh.bounds));
					break;
				}
				case NavMeshBuildSourceShape.Terrain:
				{
					TerrainData terrainData = navMeshBuildSource.sourceObject as TerrainData;
					result.Encapsulate(NavMeshSurface.GetWorldBounds(inverse * navMeshBuildSource.transform, new Bounds(0.5f * terrainData.size, terrainData.size)));
					break;
				}
				case NavMeshBuildSourceShape.Box:
				case NavMeshBuildSourceShape.Sphere:
				case NavMeshBuildSourceShape.Capsule:
				case NavMeshBuildSourceShape.ModifierBox:
					result.Encapsulate(NavMeshSurface.GetWorldBounds(inverse * navMeshBuildSource.transform, new Bounds(Vector3.zero, navMeshBuildSource.size)));
					break;
				}
			}
			result.Expand(0.1f);
			return result;
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000AD2B0 File Offset: 0x000AB4B0
		private bool HasTransformChanged()
		{
			return this.m_LastPosition != base.transform.position || this.m_LastRotation != base.transform.rotation;
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000AD2E7 File Offset: 0x000AB4E7
		private void UpdateDataIfTransformChanged()
		{
			if (this.HasTransformChanged())
			{
				this.RemoveData();
				this.AddData();
			}
		}

		// Token: 0x040020A2 RID: 8354
		[SerializeField]
		private int m_AgentTypeID;

		// Token: 0x040020A3 RID: 8355
		[SerializeField]
		private CollectObjects m_CollectObjects;

		// Token: 0x040020A4 RID: 8356
		[SerializeField]
		private Vector3 m_Size = new Vector3(10f, 10f, 10f);

		// Token: 0x040020A5 RID: 8357
		[SerializeField]
		private Vector3 m_Center = new Vector3(0f, 2f, 0f);

		// Token: 0x040020A6 RID: 8358
		[SerializeField]
		private LayerMask m_LayerMask = -1;

		// Token: 0x040020A7 RID: 8359
		[SerializeField]
		private NavMeshCollectGeometry m_UseGeometry;

		// Token: 0x040020A8 RID: 8360
		[SerializeField]
		private int m_DefaultArea;

		// Token: 0x040020A9 RID: 8361
		[SerializeField]
		private bool m_IgnoreNavMeshAgent = true;

		// Token: 0x040020AA RID: 8362
		[SerializeField]
		private bool m_IgnoreNavMeshObstacle = true;

		// Token: 0x040020AB RID: 8363
		[SerializeField]
		private bool m_OverrideTileSize;

		// Token: 0x040020AC RID: 8364
		[SerializeField]
		private int m_TileSize = 256;

		// Token: 0x040020AD RID: 8365
		[SerializeField]
		private bool m_OverrideVoxelSize;

		// Token: 0x040020AE RID: 8366
		[SerializeField]
		private float m_VoxelSize;

		// Token: 0x040020AF RID: 8367
		[SerializeField]
		private bool m_BuildHeightMesh;

		// Token: 0x040020B0 RID: 8368
		[FormerlySerializedAs("m_BakedNavMeshData")]
		[SerializeField]
		private NavMeshData m_NavMeshData;

		// Token: 0x040020B1 RID: 8369
		private NavMeshDataInstance m_NavMeshDataInstance;

		// Token: 0x040020B2 RID: 8370
		private Vector3 m_LastPosition = Vector3.zero;

		// Token: 0x040020B3 RID: 8371
		private Quaternion m_LastRotation = Quaternion.identity;

		// Token: 0x040020B4 RID: 8372
		private static readonly List<NavMeshSurface> s_NavMeshSurfaces = new List<NavMeshSurface>();
	}
}
