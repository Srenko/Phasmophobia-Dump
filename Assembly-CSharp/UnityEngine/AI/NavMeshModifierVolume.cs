using System;
using System.Collections.Generic;

namespace UnityEngine.AI
{
	// Token: 0x0200046D RID: 1133
	[ExecuteInEditMode]
	[AddComponentMenu("Navigation/NavMeshModifierVolume", 31)]
	[HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
	public class NavMeshModifierVolume : MonoBehaviour
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x000AC7EA File Offset: 0x000AA9EA
		// (set) Token: 0x06002322 RID: 8994 RVA: 0x000AC7F2 File Offset: 0x000AA9F2
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

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x000AC7FB File Offset: 0x000AA9FB
		// (set) Token: 0x06002324 RID: 8996 RVA: 0x000AC803 File Offset: 0x000AAA03
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

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x000AC80C File Offset: 0x000AAA0C
		// (set) Token: 0x06002326 RID: 8998 RVA: 0x000AC814 File Offset: 0x000AAA14
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

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x000AC81D File Offset: 0x000AAA1D
		public static List<NavMeshModifierVolume> activeModifiers
		{
			get
			{
				return NavMeshModifierVolume.s_NavMeshModifiers;
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000AC824 File Offset: 0x000AAA24
		private void OnEnable()
		{
			if (!NavMeshModifierVolume.s_NavMeshModifiers.Contains(this))
			{
				NavMeshModifierVolume.s_NavMeshModifiers.Add(this);
			}
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000AC83E File Offset: 0x000AAA3E
		private void OnDisable()
		{
			NavMeshModifierVolume.s_NavMeshModifiers.Remove(this);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000AC84C File Offset: 0x000AAA4C
		public bool AffectsAgentType(int agentTypeID)
		{
			return this.m_AffectedAgents.Count != 0 && (this.m_AffectedAgents[0] == -1 || this.m_AffectedAgents.IndexOf(agentTypeID) != -1);
		}

		// Token: 0x04002099 RID: 8345
		[SerializeField]
		private Vector3 m_Size = new Vector3(4f, 3f, 4f);

		// Token: 0x0400209A RID: 8346
		[SerializeField]
		private Vector3 m_Center = new Vector3(0f, 1f, 0f);

		// Token: 0x0400209B RID: 8347
		[SerializeField]
		private int m_Area;

		// Token: 0x0400209C RID: 8348
		[SerializeField]
		private List<int> m_AffectedAgents = new List<int>(new int[]
		{
			-1
		});

		// Token: 0x0400209D RID: 8349
		private static readonly List<NavMeshModifierVolume> s_NavMeshModifiers = new List<NavMeshModifierVolume>();
	}
}
