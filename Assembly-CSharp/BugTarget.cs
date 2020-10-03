using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class BugTarget : MonoBehaviour
{
	// Token: 0x0600010B RID: 267 RVA: 0x00008A6C File Offset: 0x00006C6C
	private void Awake()
	{
		this.TargetInterval = Random.Range(this.TargetIntervalRange.x, this.TargetIntervalRange.y);
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00008A8F File Offset: 0x00006C8F
	private void Start()
	{
		base.StartCoroutine(this.RandomTargetLocation());
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00008A9E File Offset: 0x00006C9E
	private void Update()
	{
		this.placenewtarget();
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00008AA6 File Offset: 0x00006CA6
	private void placenewtarget()
	{
		base.transform.localPosition = new Vector3(this.x, 0f, this.z);
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00008AC9 File Offset: 0x00006CC9
	private IEnumerator RandomTargetLocation()
	{
		for (;;)
		{
			this.x = Random.Range(this.xRegionSize.x, this.xRegionSize.y);
			this.z = Random.Range(this.yRegionSize.x, this.yRegionSize.y);
			yield return new WaitForSeconds(this.TargetInterval);
		}
		yield break;
	}

	// Token: 0x0400013B RID: 315
	public Vector2 TargetIntervalRange = new Vector2(0.1f, 0.2f);

	// Token: 0x0400013C RID: 316
	public float smoothing = 1f;

	// Token: 0x0400013D RID: 317
	public float speed;

	// Token: 0x0400013E RID: 318
	private Vector3 targetpos;

	// Token: 0x0400013F RID: 319
	private float x;

	// Token: 0x04000140 RID: 320
	private float z;

	// Token: 0x04000141 RID: 321
	private float TargetInterval;

	// Token: 0x04000142 RID: 322
	public Vector2 xRegionSize = new Vector2(-1f, 1f);

	// Token: 0x04000143 RID: 323
	public Vector2 yRegionSize = new Vector2(-1f, 1f);
}
