using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class StitchedRollingScene : MonoBehaviour
{
	// Token: 0x06000135 RID: 309 RVA: 0x000097C8 File Offset: 0x000079C8
	private void Awake()
	{
		for (int i = 0; i < this.NumberOfSegments; i++)
		{
			Stitchable component = Object.Instantiate<GameObject>(this.SegmentPrefab, base.transform.position, Quaternion.identity).GetComponent<Stitchable>();
			this.Segments.Add(component);
			if (i > 0)
			{
				component.transform.position = this.Segments[i - 1].StitchPoint.position;
			}
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0000983C File Offset: 0x00007A3C
	private void FixedUpdate()
	{
		for (int i = 0; i < this.Segments.Count; i++)
		{
			this.Segments[i].transform.position += Vector3.forward * this.speed * Time.deltaTime;
			if (this.Segments[i].StitchPoint.position.z >= base.transform.position.z)
			{
				int num = i + (this.Segments.Count - 1);
				if (num >= this.Segments.Count)
				{
					num -= this.Segments.Count;
				}
				this.Segments[i].transform.position = this.Segments[num].StitchPoint.position;
			}
		}
	}

	// Token: 0x04000188 RID: 392
	public GameObject SegmentPrefab;

	// Token: 0x04000189 RID: 393
	public int NumberOfSegments = 4;

	// Token: 0x0400018A RID: 394
	public List<Stitchable> Segments;

	// Token: 0x0400018B RID: 395
	public float speed = 2f;
}
