using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

// Token: 0x02000007 RID: 7
[Serializable]
public class MouseLookHelper
{
	// Token: 0x0600001C RID: 28 RVA: 0x0000277B File Offset: 0x0000097B
	public void Init(Transform character, Transform camera)
	{
		this.m_CharacterTargetRot = character.localRotation;
		this.m_CameraTargetRot = camera.localRotation;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002798 File Offset: 0x00000998
	public void LookRotation(Transform character, Transform camera)
	{
		float y = CrossPlatformInputManager.GetAxis("Mouse X") * this.XSensitivity;
		float num = CrossPlatformInputManager.GetAxis("Mouse Y") * this.YSensitivity;
		this.m_CharacterTargetRot *= Quaternion.Euler(0f, y, 0f);
		this.m_CameraTargetRot *= Quaternion.Euler(-num, 0f, 0f);
		if (this.clampVerticalRotation)
		{
			this.m_CameraTargetRot = this.ClampRotationAroundXAxis(this.m_CameraTargetRot);
		}
		if (this.smooth)
		{
			character.localRotation = Quaternion.Slerp(character.localRotation, this.m_CharacterTargetRot, this.smoothTime * Time.deltaTime);
			camera.localRotation = Quaternion.Slerp(camera.localRotation, this.m_CameraTargetRot, this.smoothTime * Time.deltaTime);
			return;
		}
		character.localRotation = this.m_CharacterTargetRot;
		camera.localRotation = this.m_CameraTargetRot;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002890 File Offset: 0x00000A90
	private Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1f;
		float num = 114.59156f * Mathf.Atan(q.x);
		num = Mathf.Clamp(num, this.MinimumX, this.MaximumX);
		q.x = Mathf.Tan(0.008726646f * num);
		return q;
	}

	// Token: 0x04000026 RID: 38
	public float XSensitivity = 2f;

	// Token: 0x04000027 RID: 39
	public float YSensitivity = 2f;

	// Token: 0x04000028 RID: 40
	public bool clampVerticalRotation = true;

	// Token: 0x04000029 RID: 41
	public float MinimumX = -90f;

	// Token: 0x0400002A RID: 42
	public float MaximumX = 90f;

	// Token: 0x0400002B RID: 43
	public bool smooth;

	// Token: 0x0400002C RID: 44
	public float smoothTime = 5f;

	// Token: 0x0400002D RID: 45
	private Quaternion m_CharacterTargetRot;

	// Token: 0x0400002E RID: 46
	private Quaternion m_CameraTargetRot;
}
