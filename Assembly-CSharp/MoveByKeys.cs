using System;
using Photon;
using UnityEngine;

// Token: 0x020000CF RID: 207
[RequireComponent(typeof(PhotonView))]
public class MoveByKeys : Photon.MonoBehaviour
{
	// Token: 0x060005CE RID: 1486 RVA: 0x00020EDE File Offset: 0x0001F0DE
	public void Start()
	{
		this.isSprite = (base.GetComponent<SpriteRenderer>() != null);
		this.body2d = base.GetComponent<Rigidbody2D>();
		this.body = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x00020F0C File Offset: 0x0001F10C
	public void FixedUpdate()
	{
		if (!base.photonView.isMine)
		{
			return;
		}
		if (Input.GetAxisRaw("Horizontal") < -0.1f || Input.GetAxisRaw("Horizontal") > 0.1f)
		{
			base.transform.position += Vector3.right * (this.Speed * Time.deltaTime) * Input.GetAxisRaw("Horizontal");
		}
		if (this.jumpingTime <= 0f)
		{
			if ((this.body != null || this.body2d != null) && Input.GetKey(KeyCode.Space))
			{
				this.jumpingTime = this.JumpTimeout;
				Vector2 vector = Vector2.up * this.JumpForce;
				if (this.body2d != null)
				{
					this.body2d.AddForce(vector);
				}
				else if (this.body != null)
				{
					this.body.AddForce(vector);
				}
			}
		}
		else
		{
			this.jumpingTime -= Time.deltaTime;
		}
		if (!this.isSprite && (Input.GetAxisRaw("Vertical") < -0.1f || Input.GetAxisRaw("Vertical") > 0.1f))
		{
			base.transform.position += Vector3.forward * (this.Speed * Time.deltaTime) * Input.GetAxisRaw("Vertical");
		}
	}

	// Token: 0x040005E7 RID: 1511
	public float Speed = 10f;

	// Token: 0x040005E8 RID: 1512
	public float JumpForce = 200f;

	// Token: 0x040005E9 RID: 1513
	public float JumpTimeout = 0.5f;

	// Token: 0x040005EA RID: 1514
	private bool isSprite;

	// Token: 0x040005EB RID: 1515
	private float jumpingTime;

	// Token: 0x040005EC RID: 1516
	private Rigidbody body;

	// Token: 0x040005ED RID: 1517
	private Rigidbody2D body2d;
}
