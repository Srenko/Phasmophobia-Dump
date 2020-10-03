using System;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class OnStartDelete : MonoBehaviour
{
	// Token: 0x060005E1 RID: 1505 RVA: 0x00021808 File Offset: 0x0001FA08
	private void Start()
	{
		Object.Destroy(base.gameObject);
	}
}
