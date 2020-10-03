﻿using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class SmoothMouseLook : MonoBehaviour
{
	// Token: 0x060000DD RID: 221 RVA: 0x00006C20 File Offset: 0x00004E20
	private void Start()
	{
		this.targetDirection = base.transform.localRotation.eulerAngles;
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00006C4C File Offset: 0x00004E4C
	private void Update()
	{
		if (this.lockCursor)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		Quaternion quaternion = Quaternion.Euler(this.targetDirection);
		Vector2 vector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		vector = Vector2.Scale(vector, new Vector2(this.sensitivity.x * this.smoothing.x, this.sensitivity.y * this.smoothing.y));
		this.smoothMouse.x = Mathf.Lerp(this.smoothMouse.x, vector.x, 1f / this.smoothing.x);
		this.smoothMouse.y = Mathf.Lerp(this.smoothMouse.y, vector.y, 1f / this.smoothing.y);
		this.mouseAbsolute += this.smoothMouse;
		if (this.clampInDegrees.x < 360f)
		{
			this.mouseAbsolute.x = Mathf.Clamp(this.mouseAbsolute.x, -this.clampInDegrees.x * 0.5f, this.clampInDegrees.x * 0.5f);
		}
		if (this.clampInDegrees.y < 360f)
		{
			this.mouseAbsolute.y = Mathf.Clamp(this.mouseAbsolute.y, -this.clampInDegrees.y * 0.5f, this.clampInDegrees.y * 0.5f);
		}
		base.transform.localRotation = Quaternion.AngleAxis(-this.mouseAbsolute.y, quaternion * Vector3.right) * quaternion;
		Quaternion rhs = Quaternion.AngleAxis(this.mouseAbsolute.x, base.transform.InverseTransformDirection(Vector3.up));
		base.transform.localRotation *= rhs;
	}

	// Token: 0x040000C8 RID: 200
	public Vector2 clampInDegrees = new Vector2(360f, 180f);

	// Token: 0x040000C9 RID: 201
	public bool lockCursor;

	// Token: 0x040000CA RID: 202
	public Vector2 sensitivity = new Vector2(2f, 2f);

	// Token: 0x040000CB RID: 203
	public Vector2 smoothing = new Vector2(3f, 3f);

	// Token: 0x040000CC RID: 204
	public Vector2 targetDirection;

	// Token: 0x040000CD RID: 205
	private Vector2 mouseAbsolute;

	// Token: 0x040000CE RID: 206
	private Vector2 smoothMouse;
}
