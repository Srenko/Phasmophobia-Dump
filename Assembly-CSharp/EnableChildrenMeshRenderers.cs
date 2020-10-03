using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
[ExecuteInEditMode]
public class EnableChildrenMeshRenderers : MonoBehaviour
{
	// Token: 0x06000016 RID: 22 RVA: 0x00002573 File Offset: 0x00000773
	private void Update()
	{
		if (this.execute)
		{
			this.execute = false;
			this.Execute();
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x0000258C File Offset: 0x0000078C
	private void Execute()
	{
		MeshRenderer[] componentsInChildren = base.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = true;
		}
	}

	// Token: 0x0400001C RID: 28
	public bool execute;
}
