using System;
using System.Collections.Generic;

namespace UnityEngine.AI
{
	// Token: 0x0200046B RID: 1131
	[ExecuteInEditMode]
	[DefaultExecutionOrder(-101)]
	[AddComponentMenu("Navigation/NavMeshLink", 33)]
	[HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
	public class NavMeshLink : MonoBehaviour
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x000AC3B4 File Offset: 0x000AA5B4
		// (set) Token: 0x060022FA RID: 8954 RVA: 0x000AC3BC File Offset: 0x000AA5BC
		public int agentTypeID
		{
			get
			{
				return this.m_AgentTypeID;
			}
			set
			{
				this.m_AgentTypeID = value;
				this.UpdateLink();
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x000AC3CB File Offset: 0x000AA5CB
		// (set) Token: 0x060022FC RID: 8956 RVA: 0x000AC3D3 File Offset: 0x000AA5D3
		public Vector3 startPoint
		{
			get
			{
				return this.m_StartPoint;
			}
			set
			{
				this.m_StartPoint = value;
				this.UpdateLink();
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x000AC3E2 File Offset: 0x000AA5E2
		// (set) Token: 0x060022FE RID: 8958 RVA: 0x000AC3EA File Offset: 0x000AA5EA
		public Vector3 endPoint
		{
			get
			{
				return this.m_EndPoint;
			}
			set
			{
				this.m_EndPoint = value;
				this.UpdateLink();
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x000AC3F9 File Offset: 0x000AA5F9
		// (set) Token: 0x06002300 RID: 8960 RVA: 0x000AC401 File Offset: 0x000AA601
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
				this.UpdateLink();
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x000AC410 File Offset: 0x000AA610
		// (set) Token: 0x06002302 RID: 8962 RVA: 0x000AC418 File Offset: 0x000AA618
		public int costModifier
		{
			get
			{
				return this.m_CostModifier;
			}
			set
			{
				this.m_CostModifier = value;
				this.UpdateLink();
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x000AC427 File Offset: 0x000AA627
		// (set) Token: 0x06002304 RID: 8964 RVA: 0x000AC42F File Offset: 0x000AA62F
		public bool bidirectional
		{
			get
			{
				return this.m_Bidirectional;
			}
			set
			{
				this.m_Bidirectional = value;
				this.UpdateLink();
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x000AC43E File Offset: 0x000AA63E
		// (set) Token: 0x06002306 RID: 8966 RVA: 0x000AC446 File Offset: 0x000AA646
		public bool autoUpdate
		{
			get
			{
				return this.m_AutoUpdatePosition;
			}
			set
			{
				this.SetAutoUpdate(value);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x000AC44F File Offset: 0x000AA64F
		// (set) Token: 0x06002308 RID: 8968 RVA: 0x000AC457 File Offset: 0x000AA657
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
				this.UpdateLink();
			}
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x000AC466 File Offset: 0x000AA666
		private void OnEnable()
		{
			this.AddLink();
			if (this.m_AutoUpdatePosition && this.m_LinkInstance.valid)
			{
				NavMeshLink.AddTracking(this);
			}
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000AC489 File Offset: 0x000AA689
		private void OnDisable()
		{
			NavMeshLink.RemoveTracking(this);
			this.m_LinkInstance.Remove();
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000AC49C File Offset: 0x000AA69C
		public void UpdateLink()
		{
			this.m_LinkInstance.Remove();
			this.AddLink();
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x000AC4AF File Offset: 0x000AA6AF
		private static void AddTracking(NavMeshLink link)
		{
			if (NavMeshLink.s_Tracked.Count == 0)
			{
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Combine(NavMesh.onPreUpdate, new NavMesh.OnNavMeshPreUpdate(NavMeshLink.UpdateTrackedInstances));
			}
			NavMeshLink.s_Tracked.Add(link);
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x000AC4E8 File Offset: 0x000AA6E8
		private static void RemoveTracking(NavMeshLink link)
		{
			NavMeshLink.s_Tracked.Remove(link);
			if (NavMeshLink.s_Tracked.Count == 0)
			{
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Remove(NavMesh.onPreUpdate, new NavMesh.OnNavMeshPreUpdate(NavMeshLink.UpdateTrackedInstances));
			}
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x000AC522 File Offset: 0x000AA722
		private void SetAutoUpdate(bool value)
		{
			if (this.m_AutoUpdatePosition == value)
			{
				return;
			}
			this.m_AutoUpdatePosition = value;
			if (value)
			{
				NavMeshLink.AddTracking(this);
				return;
			}
			NavMeshLink.RemoveTracking(this);
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000AC548 File Offset: 0x000AA748
		private void AddLink()
		{
			this.m_LinkInstance = NavMesh.AddLink(new NavMeshLinkData
			{
				startPosition = this.m_StartPoint,
				endPosition = this.m_EndPoint,
				width = this.m_Width,
				costModifier = (float)this.m_CostModifier,
				bidirectional = this.m_Bidirectional,
				area = this.m_Area,
				agentTypeID = this.m_AgentTypeID
			}, base.transform.position, base.transform.rotation);
			if (this.m_LinkInstance.valid)
			{
				this.m_LinkInstance.owner = this;
			}
			this.m_LastPosition = base.transform.position;
			this.m_LastRotation = base.transform.rotation;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000AC616 File Offset: 0x000AA816
		private bool HasTransformChanged()
		{
			return this.m_LastPosition != base.transform.position || this.m_LastRotation != base.transform.rotation;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000AC64D File Offset: 0x000AA84D
		private void OnDidApplyAnimationProperties()
		{
			this.UpdateLink();
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000AC658 File Offset: 0x000AA858
		private static void UpdateTrackedInstances()
		{
			foreach (NavMeshLink navMeshLink in NavMeshLink.s_Tracked)
			{
				if (navMeshLink.HasTransformChanged())
				{
					navMeshLink.UpdateLink();
				}
			}
		}

		// Token: 0x04002088 RID: 8328
		[SerializeField]
		private int m_AgentTypeID;

		// Token: 0x04002089 RID: 8329
		[SerializeField]
		private Vector3 m_StartPoint = new Vector3(0f, 0f, -2.5f);

		// Token: 0x0400208A RID: 8330
		[SerializeField]
		private Vector3 m_EndPoint = new Vector3(0f, 0f, 2.5f);

		// Token: 0x0400208B RID: 8331
		[SerializeField]
		private float m_Width;

		// Token: 0x0400208C RID: 8332
		[SerializeField]
		private int m_CostModifier = -1;

		// Token: 0x0400208D RID: 8333
		[SerializeField]
		private bool m_Bidirectional = true;

		// Token: 0x0400208E RID: 8334
		[SerializeField]
		private bool m_AutoUpdatePosition;

		// Token: 0x0400208F RID: 8335
		[SerializeField]
		private int m_Area;

		// Token: 0x04002090 RID: 8336
		private NavMeshLinkInstance m_LinkInstance;

		// Token: 0x04002091 RID: 8337
		private Vector3 m_LastPosition = Vector3.zero;

		// Token: 0x04002092 RID: 8338
		private Quaternion m_LastRotation = Quaternion.identity;

		// Token: 0x04002093 RID: 8339
		private static readonly List<NavMeshLink> s_Tracked = new List<NavMeshLink>();
	}
}
