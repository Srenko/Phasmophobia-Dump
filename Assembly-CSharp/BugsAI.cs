using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class BugsAI : MonoBehaviour
{
	// Token: 0x06000111 RID: 273 RVA: 0x00008B38 File Offset: 0x00006D38
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.bugscale = Random.Range(this.ScaleRange.x, this.ScaleRange.y);
		base.transform.localScale = new Vector3(this.bugscale, this.bugscale, this.bugscale);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00008B94 File Offset: 0x00006D94
	private void Start()
	{
		this.SetStartingValues();
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00008B9C File Offset: 0x00006D9C
	private void SetStartingValues()
	{
		this.speed = Random.Range(this.SpeedRange.x, this.SpeedRange.y);
		this.RunTime = Random.Range(this.RunRange.x, this.RunRange.y);
		this.waittime = Random.Range(this.WaitRange.x, this.WaitRange.y);
		if (this.view.isMine)
		{
			base.StartCoroutine(this.Alive());
		}
		this.BugTargetPos = new Vector3(Random.Range(-(this.col.size.x / 2f), this.col.size.x / 2f), 0f, Random.Range(-(this.col.size.z / 2f), this.col.size.z / 2f));
		if (this.view.isMine)
		{
			base.transform.localPosition = new Vector3(Random.Range(-(this.col.size.x / 2f), this.col.size.x / 2f), 0f, Random.Range(-(this.col.size.z / 2f), this.col.size.z / 2f));
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00008D20 File Offset: 0x00006F20
	private IEnumerator Alive()
	{
		for (;;)
		{
			this.Sleep = false;
			this.waittime = Random.Range(this.WaitRange.x, this.WaitRange.y);
			yield return new WaitForSeconds(this.RunTime);
			this.Sleep = true;
			this.RunTime = Random.Range(this.RunRange.x, this.RunRange.y);
			yield return new WaitForSeconds(this.waittime);
		}
		yield break;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00008D30 File Offset: 0x00006F30
	private void Update()
	{
		if (this.view.isMine)
		{
			this.timer -= Time.deltaTime;
			if (this.timer < 0f)
			{
				this.BugTargetPos = new Vector3(Random.Range(-(this.col.size.x / 2f), this.col.size.x / 2f), 0f, Random.Range(-(this.col.size.z / 2f), this.col.size.z / 2f));
				this.timer = 10f;
			}
			if (this.Sleep)
			{
				return;
			}
			base.transform.Translate(Vector3.forward * this.speed * 1.5f * Time.deltaTime, Space.Self);
			Vector3 forward = this.BugTargetPos - base.transform.localPosition;
			Quaternion localRotation = Quaternion.Slerp(base.transform.localRotation, Quaternion.LookRotation(forward), this.rotSpeed * Time.deltaTime);
			base.transform.localRotation = localRotation;
			base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
		}
	}

	// Token: 0x04000144 RID: 324
	public Vector2 SpeedRange = new Vector2(1f, 3f);

	// Token: 0x04000145 RID: 325
	public Vector2 ScaleRange = new Vector2(1f, 3f);

	// Token: 0x04000146 RID: 326
	public Vector2 RunRange = new Vector2(1f, 5f);

	// Token: 0x04000147 RID: 327
	public Vector2 WaitRange = new Vector2(1f, 5f);

	// Token: 0x04000148 RID: 328
	public float smoothing = 1f;

	// Token: 0x04000149 RID: 329
	public float rotSpeed = 3f;

	// Token: 0x0400014A RID: 330
	[SerializeField]
	private Vector3 BugTargetPos;

	// Token: 0x0400014B RID: 331
	private float speed;

	// Token: 0x0400014C RID: 332
	private Vector3 targetpos;

	// Token: 0x0400014D RID: 333
	private float bugscale;

	// Token: 0x0400014E RID: 334
	private float waittime = 1f;

	// Token: 0x0400014F RID: 335
	private float RunTime = 1f;

	// Token: 0x04000150 RID: 336
	private bool Sleep;

	// Token: 0x04000151 RID: 337
	[HideInInspector]
	public BoxCollider col;

	// Token: 0x04000152 RID: 338
	private float timer = 10f;

	// Token: 0x04000153 RID: 339
	private PhotonView view;
}
