using System;
using UnityEngine;

// Token: 0x020001B0 RID: 432
public class PCLook : MonoBehaviour
{
	// Token: 0x06000BB7 RID: 2999 RVA: 0x00048896 File Offset: 0x00046A96
	private void LateUpdate()
	{
		this.spineBoneTransform.rotation = this.cam.transform.rotation;
	}

	// Token: 0x04000C0D RID: 3085
	[SerializeField]
	private Transform spineBoneTransform;

	// Token: 0x04000C0E RID: 3086
	[SerializeField]
	private Camera cam;
}
