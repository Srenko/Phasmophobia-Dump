using System;
using UnityEngine;

// Token: 0x020001C2 RID: 450
public class TrailerCamera : MonoBehaviour
{
	// Token: 0x06000C56 RID: 3158 RVA: 0x0004D4AD File Offset: 0x0004B6AD
	private void Awake()
	{
		base.transform.SetParent(null);
		if (this.cam == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x0004D4D0 File Offset: 0x0004B6D0
	private void OnEnable()
	{
		base.transform.position = this.cam.transform.position;
		base.transform.rotation = this.cam.transform.rotation;
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0004D508 File Offset: 0x0004B708
	private void Update()
	{
		if (this.cam != null)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.cam.transform.position, Time.deltaTime * this.posSpeed);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.cam.transform.rotation, Time.deltaTime * this.rotSpeed);
		}
	}

	// Token: 0x04000CE2 RID: 3298
	[SerializeField]
	private Camera cam;

	// Token: 0x04000CE3 RID: 3299
	[SerializeField]
	private float posSpeed = 5f;

	// Token: 0x04000CE4 RID: 3300
	[SerializeField]
	private float rotSpeed = 5f;
}
