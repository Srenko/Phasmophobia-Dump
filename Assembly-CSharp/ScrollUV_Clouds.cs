using System;
using UnityEngine;

// Token: 0x0200002F RID: 47
public class ScrollUV_Clouds : MonoBehaviour
{
	// Token: 0x06000122 RID: 290 RVA: 0x0000905B File Offset: 0x0000725B
	private void Start()
	{
		this._myRenderer = base.GetComponent<Renderer>();
		if (this._myRenderer == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00009080 File Offset: 0x00007280
	public void FixedUpdate()
	{
		if (this.scroll)
		{
			float y = Time.time * this.verticalScrollSpeed;
			float x = Time.time * this.horizontalScrollSpeed;
			this._myRenderer.material.mainTextureOffset = new Vector2(x, y);
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x000090C6 File Offset: 0x000072C6
	public void DoActivateTrigger()
	{
		this.scroll = !this.scroll;
	}

	// Token: 0x04000168 RID: 360
	public float horizontalScrollSpeed = 0.25f;

	// Token: 0x04000169 RID: 361
	public float verticalScrollSpeed = 0.25f;

	// Token: 0x0400016A RID: 362
	private Renderer _myRenderer;

	// Token: 0x0400016B RID: 363
	private bool scroll = true;
}
