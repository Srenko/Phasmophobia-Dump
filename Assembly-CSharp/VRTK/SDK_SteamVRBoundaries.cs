using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200027D RID: 637
	[SDK_Description(typeof(SDK_SteamVRSystem), 0)]
	public class SDK_SteamVRBoundaries : SDK_BaseBoundaries
	{
		// Token: 0x06001314 RID: 4884 RVA: 0x0006B258 File Offset: 0x00069458
		public override void InitBoundaries()
		{
			SteamVR_PlayArea steamVR_PlayArea = this.GetCachedSteamVRPlayArea();
			if (steamVR_PlayArea != null)
			{
				steamVR_PlayArea.BuildMesh();
			}
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0006B27C File Offset: 0x0006947C
		public override Transform GetPlayArea()
		{
			this.cachedPlayArea = base.GetSDKManagerPlayArea();
			if (this.cachedPlayArea == null)
			{
				SteamVR_PlayArea steamVR_PlayArea = VRTK_SharedMethods.FindEvenInactiveComponent<SteamVR_PlayArea>();
				if (steamVR_PlayArea != null)
				{
					this.cachedSteamVRPlayArea = steamVR_PlayArea;
					this.cachedPlayArea = steamVR_PlayArea.transform;
				}
			}
			return this.cachedPlayArea;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0006B2CC File Offset: 0x000694CC
		public override Vector3[] GetPlayAreaVertices()
		{
			SteamVR_PlayArea steamVR_PlayArea = this.GetCachedSteamVRPlayArea();
			if (steamVR_PlayArea != null)
			{
				return this.ProcessVertices(steamVR_PlayArea.vertices);
			}
			return null;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0006B2F8 File Offset: 0x000694F8
		public override float GetPlayAreaBorderThickness()
		{
			SteamVR_PlayArea steamVR_PlayArea = this.GetCachedSteamVRPlayArea();
			if (steamVR_PlayArea != null)
			{
				return steamVR_PlayArea.borderThickness;
			}
			return 0f;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0006B324 File Offset: 0x00069524
		public override bool IsPlayAreaSizeCalibrated()
		{
			SteamVR_PlayArea steamVR_PlayArea = this.GetCachedSteamVRPlayArea();
			return steamVR_PlayArea != null && steamVR_PlayArea.size == SteamVR_PlayArea.Size.Calibrated;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0006B34C File Offset: 0x0006954C
		public override bool GetDrawAtRuntime()
		{
			SteamVR_PlayArea steamVR_PlayArea = this.GetCachedSteamVRPlayArea();
			return steamVR_PlayArea != null && steamVR_PlayArea.drawInGame;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0006B374 File Offset: 0x00069574
		public override void SetDrawAtRuntime(bool value)
		{
			SteamVR_PlayArea steamVR_PlayArea = this.GetCachedSteamVRPlayArea();
			if (steamVR_PlayArea != null)
			{
				steamVR_PlayArea.drawInGame = value;
				steamVR_PlayArea.enabled = true;
			}
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0006B3A0 File Offset: 0x000695A0
		protected virtual SteamVR_PlayArea GetCachedSteamVRPlayArea()
		{
			if (this.cachedSteamVRPlayArea == null)
			{
				Transform playArea = this.GetPlayArea();
				if (playArea != null)
				{
					this.cachedSteamVRPlayArea = playArea.GetComponent<SteamVR_PlayArea>();
				}
			}
			return this.cachedSteamVRPlayArea;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0006B3DD File Offset: 0x000695DD
		protected virtual Vector3[] ProcessVertices(Vector3[] vertices)
		{
			return vertices;
		}

		// Token: 0x040010F3 RID: 4339
		protected SteamVR_PlayArea cachedSteamVRPlayArea;
	}
}
