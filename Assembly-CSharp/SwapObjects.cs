using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class SwapObjects : MonoBehaviour
{
	// Token: 0x06000138 RID: 312 RVA: 0x0000993F File Offset: 0x00007B3F
	private void Start()
	{
		this.A.SetActive(true);
		this.B.SetActive(false);
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00009959 File Offset: 0x00007B59
	private void Update()
	{
		if (Input.GetKeyDown(this.Key))
		{
			this.A.SetActive(this.B.activeInHierarchy);
			this.B.SetActive(!this.B.activeInHierarchy);
		}
	}

	// Token: 0x0400018C RID: 396
	public KeyCode Key = KeyCode.Space;

	// Token: 0x0400018D RID: 397
	public GameObject A;

	// Token: 0x0400018E RID: 398
	public GameObject B;
}
