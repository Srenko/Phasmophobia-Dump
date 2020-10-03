using System;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class RPGCamera : MonoBehaviour
{
	// Token: 0x0600020D RID: 525 RVA: 0x0000E720 File Offset: 0x0000C920
	private void Start()
	{
		this.m_CameraTransform = base.transform.GetChild(0);
		this.m_LocalForwardVector = this.m_CameraTransform.forward;
		this.m_Distance = -this.m_CameraTransform.localPosition.z / this.m_CameraTransform.forward.z;
		this.m_Distance = Mathf.Clamp(this.m_Distance, this.MinimumDistance, this.MaximumDistance);
		this.m_LookAtPoint = this.m_CameraTransform.localPosition + this.m_LocalForwardVector * this.m_Distance;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000E7BC File Offset: 0x0000C9BC
	private void LateUpdate()
	{
		this.UpdateDistance();
		this.UpdateZoom();
		this.UpdatePosition();
		this.UpdateRotation();
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0000E7D6 File Offset: 0x0000C9D6
	private void UpdateDistance()
	{
		this.m_Distance = Mathf.Clamp(this.m_Distance - Input.GetAxis("Mouse ScrollWheel") * this.ScrollModifier, this.MinimumDistance, this.MaximumDistance);
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000E807 File Offset: 0x0000CA07
	private void UpdateZoom()
	{
		this.m_CameraTransform.localPosition = this.m_LookAtPoint - this.m_LocalForwardVector * this.m_Distance;
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000E830 File Offset: 0x0000CA30
	private void UpdatePosition()
	{
		if (this.Target == null)
		{
			return;
		}
		base.transform.position = this.Target.transform.position;
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0000E85C File Offset: 0x0000CA5C
	private void UpdateRotation()
	{
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetButton("Fire1") || Input.GetButton("Fire2"))
		{
			base.transform.Rotate(0f, Input.GetAxis("Mouse X") * this.TurnModifier, 0f);
		}
		if ((Input.GetMouseButton(1) || Input.GetButton("Fire2")) && this.Target != null)
		{
			this.Target.rotation = Quaternion.Euler(0f, base.transform.rotation.eulerAngles.y, 0f);
		}
	}

	// Token: 0x0400024C RID: 588
	public Transform Target;

	// Token: 0x0400024D RID: 589
	public float MaximumDistance;

	// Token: 0x0400024E RID: 590
	public float MinimumDistance;

	// Token: 0x0400024F RID: 591
	public float ScrollModifier;

	// Token: 0x04000250 RID: 592
	public float TurnModifier;

	// Token: 0x04000251 RID: 593
	private Transform m_CameraTransform;

	// Token: 0x04000252 RID: 594
	private Vector3 m_LookAtPoint;

	// Token: 0x04000253 RID: 595
	private Vector3 m_LocalForwardVector;

	// Token: 0x04000254 RID: 596
	private float m_Distance;
}
