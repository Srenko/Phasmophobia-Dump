using System;
using UnityEngine;

namespace ExitGames.Demos.DemoAnimator
{
	// Token: 0x02000484 RID: 1156
	public class CameraWork : MonoBehaviour
	{
		// Token: 0x0600240B RID: 9227 RVA: 0x000B093A File Offset: 0x000AEB3A
		private void Start()
		{
			if (this.followOnStart)
			{
				this.OnStartFollowing();
			}
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000B094A File Offset: 0x000AEB4A
		private void LateUpdate()
		{
			if (this.cameraTransform == null && this.isFollowing)
			{
				this.OnStartFollowing();
			}
			if (this.isFollowing)
			{
				this.Apply();
			}
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000B0976 File Offset: 0x000AEB76
		public void OnStartFollowing()
		{
			this.cameraTransform = Camera.main.transform;
			this.isFollowing = true;
			this.Cut();
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000B0998 File Offset: 0x000AEB98
		private void Apply()
		{
			Vector3 vector = base.transform.position + this.centerOffset;
			float y = base.transform.eulerAngles.y;
			float y2 = this.cameraTransform.eulerAngles.y;
			y2 = y;
			this.targetHeight = vector.y + this.height;
			float num = this.cameraTransform.position.y;
			num = Mathf.SmoothDamp(num, this.targetHeight, ref this.heightVelocity, this.heightSmoothLag);
			Quaternion rotation = Quaternion.Euler(0f, y2, 0f);
			this.cameraTransform.position = vector;
			this.cameraTransform.position += rotation * Vector3.back * this.distance;
			this.cameraTransform.position = new Vector3(this.cameraTransform.position.x, num, this.cameraTransform.position.z);
			this.SetUpRotation(vector);
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000B0A9C File Offset: 0x000AEC9C
		private void Cut()
		{
			float num = this.heightSmoothLag;
			this.heightSmoothLag = 0.001f;
			this.Apply();
			this.heightSmoothLag = num;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000B0AC8 File Offset: 0x000AECC8
		private void SetUpRotation(Vector3 centerPos)
		{
			Vector3 position = this.cameraTransform.position;
			Vector3 vector = centerPos - position;
			Quaternion lhs = Quaternion.LookRotation(new Vector3(vector.x, 0f, vector.z));
			Vector3 forward = Vector3.forward * this.distance + Vector3.down * this.height;
			this.cameraTransform.rotation = lhs * Quaternion.LookRotation(forward);
		}

		// Token: 0x0400215C RID: 8540
		[Tooltip("The distance in the local x-z plane to the target")]
		public float distance = 7f;

		// Token: 0x0400215D RID: 8541
		[Tooltip("The height we want the camera to be above the target")]
		public float height = 3f;

		// Token: 0x0400215E RID: 8542
		[Tooltip("The Smooth time lag for the height of the camera.")]
		public float heightSmoothLag = 0.3f;

		// Token: 0x0400215F RID: 8543
		[Tooltip("Allow the camera to be offseted vertically from the target, for example giving more view of the sceneray and less ground.")]
		public Vector3 centerOffset = Vector3.zero;

		// Token: 0x04002160 RID: 8544
		[Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
		public bool followOnStart;

		// Token: 0x04002161 RID: 8545
		private Transform cameraTransform;

		// Token: 0x04002162 RID: 8546
		private bool isFollowing;

		// Token: 0x04002163 RID: 8547
		private float heightVelocity;

		// Token: 0x04002164 RID: 8548
		private float targetHeight = 100000f;
	}
}
