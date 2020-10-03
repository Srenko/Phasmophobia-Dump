using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x02000498 RID: 1176
	[ExecuteInEditMode]
	public class FindLodGroups : MonoBehaviour
	{
		// Token: 0x060024A2 RID: 9378 RVA: 0x000B348C File Offset: 0x000B168C
		private void Start()
		{
			this.FindLods();
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000B3494 File Offset: 0x000B1694
		private void Update()
		{
			if (this.find)
			{
				this.find = false;
				this.FindLods();
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000B34AC File Offset: 0x000B16AC
		private void FindLods()
		{
			LODGroup[] componentsInChildren = base.GetComponentsInChildren<LODGroup>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Debug.Log(componentsInChildren[i].name);
			}
			Debug.Log("---------------------------------------------");
			Debug.Log("LODGroups found " + componentsInChildren.Length);
			Debug.Log("---------------------------------------------");
		}

		// Token: 0x040021CE RID: 8654
		public bool find;
	}
}
