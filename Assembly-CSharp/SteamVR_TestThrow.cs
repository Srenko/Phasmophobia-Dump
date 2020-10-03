using System;
using UnityEngine;

// Token: 0x020001D9 RID: 473
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class SteamVR_TestThrow : MonoBehaviour
{
	// Token: 0x06000CD7 RID: 3287 RVA: 0x00051A05 File Offset: 0x0004FC05
	private void Awake()
	{
		this.trackedObj = base.GetComponent<SteamVR_TrackedObject>();
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x00051A14 File Offset: 0x0004FC14
	private void FixedUpdate()
	{
		SteamVR_Controller.Device device = SteamVR_Controller.Input((int)this.trackedObj.index);
		if (this.joint == null && device.GetTouchDown(8589934592UL))
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.prefab);
			gameObject.transform.position = this.attachPoint.transform.position;
			this.joint = gameObject.AddComponent<FixedJoint>();
			this.joint.connectedBody = this.attachPoint;
			return;
		}
		if (this.joint != null && device.GetTouchUp(8589934592UL))
		{
			GameObject gameObject2 = this.joint.gameObject;
			Rigidbody component = gameObject2.GetComponent<Rigidbody>();
			Object.DestroyImmediate(this.joint);
			this.joint = null;
			Object.Destroy(gameObject2, 15f);
			Transform transform = this.trackedObj.origin ? this.trackedObj.origin : this.trackedObj.transform.parent;
			if (transform != null)
			{
				component.velocity = transform.TransformVector(device.velocity);
				component.angularVelocity = transform.TransformVector(device.angularVelocity);
			}
			else
			{
				component.velocity = device.velocity;
				component.angularVelocity = device.angularVelocity;
			}
			component.maxAngularVelocity = component.angularVelocity.magnitude;
		}
	}

	// Token: 0x04000D7E RID: 3454
	public GameObject prefab;

	// Token: 0x04000D7F RID: 3455
	public Rigidbody attachPoint;

	// Token: 0x04000D80 RID: 3456
	private SteamVR_TrackedObject trackedObj;

	// Token: 0x04000D81 RID: 3457
	private FixedJoint joint;
}
