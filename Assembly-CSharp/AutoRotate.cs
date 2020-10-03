using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class AutoRotate : MonoBehaviour
{
	// Token: 0x060000D3 RID: 211 RVA: 0x00006775 File Offset: 0x00004975
	private void Start()
	{
		this.tr = base.GetComponent<Transform>();
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00006783 File Offset: 0x00004983
	private void Update()
	{
		this.tr.Rotate(this.rotation * Time.deltaTime);
	}

	// Token: 0x040000B7 RID: 183
	private Transform tr;

	// Token: 0x040000B8 RID: 184
	public Vector3 rotation;
}
