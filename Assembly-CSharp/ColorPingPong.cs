using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class ColorPingPong : MonoBehaviour
{
	// Token: 0x060000D6 RID: 214 RVA: 0x000067A0 File Offset: 0x000049A0
	private void Update()
	{
		this.mat.SetColor("_TintColor", Color.Lerp(this.colorA, this.colorB, Mathf.PingPong(Time.time / 30f, 1f)));
	}

	// Token: 0x040000B9 RID: 185
	public Material mat;

	// Token: 0x040000BA RID: 186
	public Color colorA;

	// Token: 0x040000BB RID: 187
	public Color colorB;
}
