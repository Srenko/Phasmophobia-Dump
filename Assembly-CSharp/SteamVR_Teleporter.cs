using System;
using UnityEngine;

// Token: 0x020001D8 RID: 472
public class SteamVR_Teleporter : MonoBehaviour
{
	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00051828 File Offset: 0x0004FA28
	private Transform reference
	{
		get
		{
			SteamVR_Camera steamVR_Camera = SteamVR_Render.Top();
			if (!(steamVR_Camera != null))
			{
				return null;
			}
			return steamVR_Camera.origin;
		}
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0005184C File Offset: 0x0004FA4C
	private void Start()
	{
		SteamVR_TrackedController steamVR_TrackedController = base.GetComponent<SteamVR_TrackedController>();
		if (steamVR_TrackedController == null)
		{
			steamVR_TrackedController = base.gameObject.AddComponent<SteamVR_TrackedController>();
		}
		steamVR_TrackedController.TriggerClicked += this.DoClick;
		if (this.teleportType == SteamVR_Teleporter.TeleportType.TeleportTypeUseTerrain)
		{
			Transform reference = this.reference;
			if (reference != null)
			{
				reference.position = new Vector3(reference.position.x, Terrain.activeTerrain.SampleHeight(reference.position), reference.position.z);
			}
		}
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x000518D0 File Offset: 0x0004FAD0
	private void DoClick(object sender, ClickedEventArgs e)
	{
		if (this.teleportOnClick)
		{
			Transform reference = this.reference;
			if (reference == null)
			{
				return;
			}
			float y = reference.position.y;
			Plane plane = new Plane(Vector3.up, -y);
			Ray ray = new Ray(base.transform.position, base.transform.forward);
			float d = 0f;
			bool flag;
			if (this.teleportType == SteamVR_Teleporter.TeleportType.TeleportTypeUseTerrain)
			{
				RaycastHit raycastHit;
				flag = Terrain.activeTerrain.GetComponent<TerrainCollider>().Raycast(ray, out raycastHit, 1000f);
				d = raycastHit.distance;
			}
			else if (this.teleportType == SteamVR_Teleporter.TeleportType.TeleportTypeUseCollider)
			{
				RaycastHit raycastHit2;
				flag = Physics.Raycast(ray, out raycastHit2);
				d = raycastHit2.distance;
			}
			else
			{
				flag = plane.Raycast(ray, out d);
			}
			if (flag)
			{
				Vector3 b = new Vector3(SteamVR_Render.Top().head.position.x, y, SteamVR_Render.Top().head.position.z);
				reference.position = reference.position + (ray.origin + ray.direction * d) - b;
			}
		}
	}

	// Token: 0x04000D7C RID: 3452
	public bool teleportOnClick;

	// Token: 0x04000D7D RID: 3453
	public SteamVR_Teleporter.TeleportType teleportType = SteamVR_Teleporter.TeleportType.TeleportTypeUseZeroY;

	// Token: 0x02000567 RID: 1383
	public enum TeleportType
	{
		// Token: 0x040025B4 RID: 9652
		TeleportTypeUseTerrain,
		// Token: 0x040025B5 RID: 9653
		TeleportTypeUseCollider,
		// Token: 0x040025B6 RID: 9654
		TeleportTypeUseZeroY
	}
}
