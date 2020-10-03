using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class HE_H_distortedLight : MonoBehaviour
{
	// Token: 0x060000A3 RID: 163 RVA: 0x000050C7 File Offset: 0x000032C7
	private void Start()
	{
		this.myLight = base.gameObject.GetComponent<Light>();
		this.baseColor = this.myLight.color;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000050EC File Offset: 0x000032EC
	private void Update()
	{
		this.blinkIterator += 1f * Time.deltaTime;
		if (this.blinkIterator >= this.blinkFrequency)
		{
			this.blinkIterator = Random.Range(0f, this.blinkFrequency) * 0.5f;
			if (this.myLight.color != this.distortColor)
			{
				this.myLight.color = this.distortColor;
				return;
			}
			this.myLight.color = this.baseColor;
		}
	}

	// Token: 0x04000074 RID: 116
	public Color distortColor = Color.white;

	// Token: 0x04000075 RID: 117
	private Color baseColor = Color.white;

	// Token: 0x04000076 RID: 118
	public float blinkFrequency = 1f;

	// Token: 0x04000077 RID: 119
	private float blinkIterator;

	// Token: 0x04000078 RID: 120
	private Light myLight;
}
