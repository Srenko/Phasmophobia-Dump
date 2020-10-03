using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
[RequireComponent(typeof(Camera))]
public class MoveCam : MonoBehaviour
{
	// Token: 0x060001D1 RID: 465 RVA: 0x0000C950 File Offset: 0x0000AB50
	private void Start()
	{
		this.camTransform = base.GetComponent<Camera>().transform;
		this.originalPos = this.camTransform.position;
		this.randomPos = this.originalPos + new Vector3((float)Random.Range(-2, 2), (float)Random.Range(-2, 2), (float)Random.Range(-1, 1));
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
	private void Update()
	{
		this.camTransform.position = Vector3.Slerp(this.camTransform.position, this.randomPos, Time.deltaTime);
		this.camTransform.LookAt(this.lookAt);
		if (Vector3.Distance(this.camTransform.position, this.randomPos) < 0.5f)
		{
			this.randomPos = this.originalPos + new Vector3((float)Random.Range(-2, 2), (float)Random.Range(-2, 2), (float)Random.Range(-1, 1));
		}
	}

	// Token: 0x040001F2 RID: 498
	private Vector3 originalPos;

	// Token: 0x040001F3 RID: 499
	private Vector3 randomPos;

	// Token: 0x040001F4 RID: 500
	private Transform camTransform;

	// Token: 0x040001F5 RID: 501
	public Transform lookAt;
}
