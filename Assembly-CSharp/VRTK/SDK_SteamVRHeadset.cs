using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000280 RID: 640
	[SDK_Description(typeof(SDK_SteamVRSystem), 0)]
	public class SDK_SteamVRHeadset : SDK_BaseHeadset
	{
		// Token: 0x0600134A RID: 4938 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ProcessUpdate(Dictionary<string, object> options)
		{
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ProcessFixedUpdate(Dictionary<string, object> options)
		{
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0006BE84 File Offset: 0x0006A084
		public override Transform GetHeadset()
		{
			this.cachedHeadset = base.GetSDKManagerHeadset();
			if (this.cachedHeadset == null)
			{
				SteamVR_Camera steamVR_Camera = VRTK_SharedMethods.FindEvenInactiveComponent<SteamVR_Camera>();
				if (steamVR_Camera)
				{
					this.cachedHeadset = steamVR_Camera.transform;
				}
			}
			return this.cachedHeadset;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0006BECC File Offset: 0x0006A0CC
		public override Transform GetHeadsetCamera()
		{
			this.cachedHeadsetCamera = base.GetSDKManagerHeadset();
			if (this.cachedHeadsetCamera == null)
			{
				SteamVR_Camera steamVR_Camera = VRTK_SharedMethods.FindEvenInactiveComponent<SteamVR_Camera>();
				if (steamVR_Camera)
				{
					this.cachedHeadsetCamera = steamVR_Camera.transform;
				}
			}
			return this.cachedHeadsetCamera;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0006BF13 File Offset: 0x0006A113
		public override Vector3 GetHeadsetVelocity()
		{
			return SteamVR_Controller.Input(0).velocity;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0006BF20 File Offset: 0x0006A120
		public override Vector3 GetHeadsetAngularVelocity()
		{
			return SteamVR_Controller.Input(0).angularVelocity;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0006BF2D File Offset: 0x0006A12D
		public override void HeadsetFade(Color color, float duration, bool fadeOverlay = false)
		{
			SteamVR_Fade.Start(color, duration, fadeOverlay);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0006BF37 File Offset: 0x0006A137
		public override bool HasHeadsetFade(Transform obj)
		{
			return obj.GetComponentInChildren<SteamVR_Fade>();
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0006BF49 File Offset: 0x0006A149
		public override void AddHeadsetFade(Transform camera)
		{
			if (camera && !camera.GetComponent<SteamVR_Fade>())
			{
				camera.gameObject.AddComponent<SteamVR_Fade>();
			}
		}
	}
}
