using System;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class Unparent : MonoBehaviour
{
	// Token: 0x06000117 RID: 279 RVA: 0x00008F32 File Offset: 0x00007132
	private void Start()
	{
		base.gameObject.transform.parent = null;
	}
}
