using System;
using UnityEngine;

// Token: 0x020001D4 RID: 468
public class SteamVR_GazeTracker : MonoBehaviour
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000CBD RID: 3261 RVA: 0x00051164 File Offset: 0x0004F364
	// (remove) Token: 0x06000CBE RID: 3262 RVA: 0x0005119C File Offset: 0x0004F39C
	public event GazeEventHandler GazeOn;

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000CBF RID: 3263 RVA: 0x000511D4 File Offset: 0x0004F3D4
	// (remove) Token: 0x06000CC0 RID: 3264 RVA: 0x0005120C File Offset: 0x0004F40C
	public event GazeEventHandler GazeOff;

	// Token: 0x06000CC1 RID: 3265 RVA: 0x00003F60 File Offset: 0x00002160
	private void Start()
	{
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x00051241 File Offset: 0x0004F441
	public virtual void OnGazeOn(GazeEventArgs e)
	{
		if (this.GazeOn != null)
		{
			this.GazeOn(this, e);
		}
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x00051258 File Offset: 0x0004F458
	public virtual void OnGazeOff(GazeEventArgs e)
	{
		if (this.GazeOff != null)
		{
			this.GazeOff(this, e);
		}
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x00051270 File Offset: 0x0004F470
	private void Update()
	{
		if (this.hmdTrackedObject == null)
		{
			foreach (SteamVR_TrackedObject steamVR_TrackedObject in Object.FindObjectsOfType<SteamVR_TrackedObject>())
			{
				if (steamVR_TrackedObject.index == SteamVR_TrackedObject.EIndex.Hmd)
				{
					this.hmdTrackedObject = steamVR_TrackedObject.transform;
					break;
				}
			}
		}
		if (this.hmdTrackedObject)
		{
			Ray ray = new Ray(this.hmdTrackedObject.position, this.hmdTrackedObject.forward);
			Plane plane = new Plane(this.hmdTrackedObject.forward, base.transform.position);
			float d = 0f;
			if (plane.Raycast(ray, out d))
			{
				float num = Vector3.Distance(this.hmdTrackedObject.position + this.hmdTrackedObject.forward * d, base.transform.position);
				if (num < this.gazeInCutoff && !this.isInGaze)
				{
					this.isInGaze = true;
					GazeEventArgs e;
					e.distance = num;
					this.OnGazeOn(e);
					return;
				}
				if (num >= this.gazeOutCutoff && this.isInGaze)
				{
					this.isInGaze = false;
					GazeEventArgs e2;
					e2.distance = num;
					this.OnGazeOff(e2);
				}
			}
		}
	}

	// Token: 0x04000D67 RID: 3431
	public bool isInGaze;

	// Token: 0x04000D6A RID: 3434
	public float gazeInCutoff = 0.15f;

	// Token: 0x04000D6B RID: 3435
	public float gazeOutCutoff = 0.4f;

	// Token: 0x04000D6C RID: 3436
	private Transform hmdTrackedObject;
}
