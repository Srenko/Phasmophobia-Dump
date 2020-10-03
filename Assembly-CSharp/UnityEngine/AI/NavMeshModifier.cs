using System;
using System.Collections.Generic;

namespace UnityEngine.AI
{
	// Token: 0x0200046C RID: 1132
	[ExecuteInEditMode]
	[AddComponentMenu("Navigation/NavMeshModifier", 32)]
	[HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
	public class NavMeshModifier : MonoBehaviour
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x000AC72B File Offset: 0x000AA92B
		// (set) Token: 0x06002316 RID: 8982 RVA: 0x000AC733 File Offset: 0x000AA933
		public bool overrideArea
		{
			get
			{
				return this.m_OverrideArea;
			}
			set
			{
				this.m_OverrideArea = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x000AC73C File Offset: 0x000AA93C
		// (set) Token: 0x06002318 RID: 8984 RVA: 0x000AC744 File Offset: 0x000AA944
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x000AC74D File Offset: 0x000AA94D
		// (set) Token: 0x0600231A RID: 8986 RVA: 0x000AC755 File Offset: 0x000AA955
		public bool ignoreFromBuild
		{
			get
			{
				return this.m_IgnoreFromBuild;
			}
			set
			{
				this.m_IgnoreFromBuild = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x000AC75E File Offset: 0x000AA95E
		public static List<NavMeshModifier> activeModifiers
		{
			get
			{
				return NavMeshModifier.s_NavMeshModifiers;
			}
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000AC765 File Offset: 0x000AA965
		private void OnEnable()
		{
			if (!NavMeshModifier.s_NavMeshModifiers.Contains(this))
			{
				NavMeshModifier.s_NavMeshModifiers.Add(this);
			}
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x000AC77F File Offset: 0x000AA97F
		private void OnDisable()
		{
			NavMeshModifier.s_NavMeshModifiers.Remove(this);
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x000AC78D File Offset: 0x000AA98D
		public bool AffectsAgentType(int agentTypeID)
		{
			return this.m_AffectedAgents.Count != 0 && (this.m_AffectedAgents[0] == -1 || this.m_AffectedAgents.IndexOf(agentTypeID) != -1);
		}

		// Token: 0x04002094 RID: 8340
		[SerializeField]
		private bool m_OverrideArea;

		// Token: 0x04002095 RID: 8341
		[SerializeField]
		private int m_Area;

		// Token: 0x04002096 RID: 8342
		[SerializeField]
		private bool m_IgnoreFromBuild;

		// Token: 0x04002097 RID: 8343
		[SerializeField]
		private List<int> m_AffectedAgents = new List<int>(new int[]
		{
			-1
		});

		// Token: 0x04002098 RID: 8344
		private static readonly List<NavMeshModifier> s_NavMeshModifiers = new List<NavMeshModifier>();
	}
}
