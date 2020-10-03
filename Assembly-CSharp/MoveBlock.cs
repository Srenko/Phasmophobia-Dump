using System;
using UnityEngine;

// Token: 0x020001F9 RID: 505
public class MoveBlock : MonoBehaviour
{
	// Token: 0x06000E20 RID: 3616 RVA: 0x0005A118 File Offset: 0x00058318
	protected virtual void Start()
	{
		this.startY = base.transform.position.y;
		this.moveUpAmount = Mathf.Abs(this.moveYAmount);
		if (this.moveYAmount < 0f)
		{
			this.startY -= this.moveYAmount;
			this.goingUp = false;
		}
		this.stoppedUntilTime = Time.time + this.waitTime;
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x0005A188 File Offset: 0x00058388
	protected virtual void Update()
	{
		if (Time.time > this.stoppedUntilTime)
		{
			if (this.goingUp)
			{
				if (base.transform.position.y < this.startY + this.moveUpAmount)
				{
					Vector3 position = base.transform.position;
					position.y += Time.deltaTime * this.moveSpeed;
					base.transform.position = position;
				}
				else
				{
					this.goingUp = false;
					this.stoppedUntilTime = Time.time + this.waitTime;
				}
			}
			else if (base.transform.position.y > this.startY)
			{
				Vector3 position2 = base.transform.position;
				position2.y -= Time.deltaTime * this.moveSpeed;
				base.transform.position = position2;
			}
			else
			{
				this.goingUp = true;
				this.stoppedUntilTime = Time.time + this.waitTime;
			}
		}
		base.transform.Rotate(new Vector3(0f, this.rotateSpeed * Time.deltaTime, 0f));
	}

	// Token: 0x04000E7A RID: 3706
	public float moveYAmount = 20f;

	// Token: 0x04000E7B RID: 3707
	public float moveSpeed = 1f;

	// Token: 0x04000E7C RID: 3708
	public float waitTime = 5f;

	// Token: 0x04000E7D RID: 3709
	public float rotateSpeed = 10f;

	// Token: 0x04000E7E RID: 3710
	private float startY;

	// Token: 0x04000E7F RID: 3711
	private bool goingUp = true;

	// Token: 0x04000E80 RID: 3712
	private float stoppedUntilTime;

	// Token: 0x04000E81 RID: 3713
	private float moveUpAmount;
}
