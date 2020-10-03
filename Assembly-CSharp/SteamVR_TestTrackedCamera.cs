using System;
using UnityEngine;
using Valve.VR;

// Token: 0x020001DA RID: 474
public class SteamVR_TestTrackedCamera : MonoBehaviour
{
	// Token: 0x06000CDA RID: 3290 RVA: 0x00051B75 File Offset: 0x0004FD75
	private void OnEnable()
	{
		SteamVR_TrackedCamera.VideoStreamTexture videoStreamTexture = SteamVR_TrackedCamera.Source(this.undistorted, 0);
		videoStreamTexture.Acquire();
		if (!videoStreamTexture.hasCamera)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x00051B98 File Offset: 0x0004FD98
	private void OnDisable()
	{
		this.material.mainTexture = null;
		SteamVR_TrackedCamera.Source(this.undistorted, 0).Release();
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x00051BB8 File Offset: 0x0004FDB8
	private void Update()
	{
		SteamVR_TrackedCamera.VideoStreamTexture videoStreamTexture = SteamVR_TrackedCamera.Source(this.undistorted, 0);
		Texture2D texture = videoStreamTexture.texture;
		if (texture == null)
		{
			return;
		}
		this.material.mainTexture = texture;
		float num = (float)texture.width / (float)texture.height;
		if (this.cropped)
		{
			VRTextureBounds_t frameBounds = videoStreamTexture.frameBounds;
			this.material.mainTextureOffset = new Vector2(frameBounds.uMin, frameBounds.vMin);
			float num2 = frameBounds.uMax - frameBounds.uMin;
			float num3 = frameBounds.vMax - frameBounds.vMin;
			this.material.mainTextureScale = new Vector2(num2, num3);
			num *= Mathf.Abs(num2 / num3);
		}
		else
		{
			this.material.mainTextureOffset = Vector2.zero;
			this.material.mainTextureScale = new Vector2(1f, -1f);
		}
		this.target.localScale = new Vector3(1f, 1f / num, 1f);
		if (videoStreamTexture.hasTracking)
		{
			SteamVR_Utils.RigidTransform transform = videoStreamTexture.transform;
			this.target.localPosition = transform.pos;
			this.target.localRotation = transform.rot;
		}
	}

	// Token: 0x04000D82 RID: 3458
	public Material material;

	// Token: 0x04000D83 RID: 3459
	public Transform target;

	// Token: 0x04000D84 RID: 3460
	public bool undistorted = true;

	// Token: 0x04000D85 RID: 3461
	public bool cropped = true;
}
