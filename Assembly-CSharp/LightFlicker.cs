using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
	// Token: 0x06000130 RID: 304 RVA: 0x000096CB File Offset: 0x000078CB
	private void Awake()
	{
		this.myLight = base.GetComponent<Light>();
	}

	// Token: 0x06000131 RID: 305 RVA: 0x000096D9 File Offset: 0x000078D9
	private void Start()
	{
		if (this.RandomInterval)
		{
			this.Interval = Random.Range(0f, this.MaxInterval);
			return;
		}
		this.Interval = this.MaxInterval;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00009708 File Offset: 0x00007908
	private void Update()
	{
		if (this.timer < this.Interval)
		{
			this.timer += Time.deltaTime;
			return;
		}
		if (this.StayOnAfter > 0)
		{
			if (this.counter >= this.StayOnAfter)
			{
				this.myLight.enabled = true;
			}
			else
			{
				this.counter++;
				this.myLight.enabled = !this.myLight.enabled;
			}
		}
		else
		{
			this.myLight.enabled = !this.myLight.enabled;
		}
		this.timer = 0f;
		if (this.RandomInterval)
		{
			this.Interval = Random.Range(0f, this.MaxInterval);
		}
	}

	// Token: 0x04000180 RID: 384
	public float MaxInterval;

	// Token: 0x04000181 RID: 385
	private float Interval;

	// Token: 0x04000182 RID: 386
	public bool RandomInterval;

	// Token: 0x04000183 RID: 387
	private float timer;

	// Token: 0x04000184 RID: 388
	private Light myLight;

	// Token: 0x04000185 RID: 389
	public int StayOnAfter;

	// Token: 0x04000186 RID: 390
	private int counter;
}
