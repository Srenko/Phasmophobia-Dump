using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x02000499 RID: 1177
	public class LODGroupSetup : MonoBehaviour
	{
		// Token: 0x060024A6 RID: 9382 RVA: 0x000B3507 File Offset: 0x000B1707
		public void Init(MeshCombiner meshCombiner, int lodGroupParentIndex)
		{
			this.meshCombiner = meshCombiner;
			this.lodGroupParentIndex = lodGroupParentIndex;
			this.lodCount = lodGroupParentIndex + 1;
			if (this.lodGroup == null)
			{
				this.lodGroup = base.gameObject.AddComponent<LODGroup>();
			}
			this.GetSetup();
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000B3548 File Offset: 0x000B1748
		private void GetSetup()
		{
			LOD[] array = new LOD[this.lodGroupParentIndex + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = default(LOD);
				array[i].screenRelativeTransitionHeight = this.meshCombiner.lodGroupsSettings[this.lodGroupParentIndex].lodSettings[i].screenRelativeTransitionHeight;
			}
			this.lodGroup.SetLODs(array);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000B35B4 File Offset: 0x000B17B4
		public void ApplySetup()
		{
			LOD[] lods = this.lodGroup.GetLODs();
			if (this.lodGroups == null)
			{
				this.lodGroups = base.GetComponentsInChildren<LODGroup>();
			}
			if (lods.Length != this.lodCount)
			{
				return;
			}
			bool flag = false;
			if (this.lodGroupParentIndex == 0)
			{
				if (lods[0].screenRelativeTransitionHeight != 0f)
				{
					if (this.lodGroups == null || this.lodGroups.Length == 1)
					{
						this.AddLODGroupsToChildren();
					}
				}
				else
				{
					if (this.lodGroup != null && this.lodGroups.Length != 1)
					{
						this.RemoveLODGroupFromChildren();
					}
					flag = true;
				}
			}
			if (this.meshCombiner != null)
			{
				for (int i = 0; i < lods.Length; i++)
				{
					this.meshCombiner.lodGroupsSettings[this.lodGroupParentIndex].lodSettings[i].screenRelativeTransitionHeight = lods[i].screenRelativeTransitionHeight;
				}
			}
			if (flag)
			{
				return;
			}
			for (int j = 0; j < this.lodGroups.Length; j++)
			{
				LOD[] lods2 = this.lodGroups[j].GetLODs();
				for (int k = 0; k < lods2.Length; k++)
				{
					lods2[k].screenRelativeTransitionHeight = lods[k].screenRelativeTransitionHeight;
				}
				this.lodGroups[j].SetLODs(lods2);
			}
			if (this.meshCombiner != null)
			{
				this.lodGroup.size = (float)this.meshCombiner.cellSize;
			}
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000B3714 File Offset: 0x000B1914
		public void AddLODGroupsToChildren()
		{
			Transform transform = base.transform;
			List<LODGroup> list = new List<LODGroup>();
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				Debug.Log(child.name);
				LODGroup lodgroup = child.GetComponent<LODGroup>();
				if (lodgroup == null)
				{
					lodgroup = child.gameObject.AddComponent<LODGroup>();
					LOD[] array = new LOD[1];
					LOD[] array2 = array;
					int num = 0;
					float screenRelativeTransitionHeight = 0f;
					Renderer[] componentsInChildren = child.GetComponentsInChildren<MeshRenderer>();
					array2[num] = new LOD(screenRelativeTransitionHeight, componentsInChildren);
					lodgroup.SetLODs(array);
				}
				list.Add(lodgroup);
			}
			this.lodGroups = list.ToArray();
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000B37B4 File Offset: 0x000B19B4
		public void RemoveLODGroupFromChildren()
		{
			Transform transform = base.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				LODGroup component = transform.GetChild(i).GetComponent<LODGroup>();
				if (component != null)
				{
					Object.DestroyImmediate(component);
				}
			}
			this.lodGroups = null;
		}

		// Token: 0x040021CF RID: 8655
		public MeshCombiner meshCombiner;

		// Token: 0x040021D0 RID: 8656
		public LODGroup lodGroup;

		// Token: 0x040021D1 RID: 8657
		public int lodGroupParentIndex;

		// Token: 0x040021D2 RID: 8658
		public int lodCount;

		// Token: 0x040021D3 RID: 8659
		private LODGroup[] lodGroups;
	}
}
