using System;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

// Token: 0x020001F6 RID: 502
public class SteamVR_TrackedObject : MonoBehaviour
{
	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00059526 File Offset: 0x00057726
	// (set) Token: 0x06000E07 RID: 3591 RVA: 0x0005952E File Offset: 0x0005772E
	public bool isValid { get; private set; }

	// Token: 0x06000E08 RID: 3592 RVA: 0x00059538 File Offset: 0x00057738
	private void OnNewPoses(TrackedDevicePose_t[] poses)
	{
		if (this.index == SteamVR_TrackedObject.EIndex.None)
		{
			return;
		}
		int num = (int)this.index;
		this.isValid = false;
		if (poses.Length <= num)
		{
			return;
		}
		if (!poses[num].bDeviceIsConnected)
		{
			return;
		}
		if (!poses[num].bPoseIsValid)
		{
			return;
		}
		this.isValid = true;
		SteamVR_Utils.RigidTransform rigidTransform = new SteamVR_Utils.RigidTransform(poses[num].mDeviceToAbsoluteTracking);
		if (this.origin != null)
		{
			base.transform.position = this.origin.transform.TransformPoint(rigidTransform.pos);
			base.transform.rotation = this.origin.rotation * rigidTransform.rot;
			return;
		}
		base.transform.localPosition = rigidTransform.pos;
		base.transform.localRotation = rigidTransform.rot;
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x0005960F File Offset: 0x0005780F
	private SteamVR_TrackedObject()
	{
		this.newPosesAction = SteamVR_Events.NewPosesAction(new UnityAction<TrackedDevicePose_t[]>(this.OnNewPoses));
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x0005962E File Offset: 0x0005782E
	private void OnEnable()
	{
		if (SteamVR_Render.instance == null)
		{
			base.enabled = false;
			return;
		}
		this.newPosesAction.enabled = true;
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x00059651 File Offset: 0x00057851
	private void OnDisable()
	{
		this.newPosesAction.enabled = false;
		this.isValid = false;
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x00059666 File Offset: 0x00057866
	public void SetDeviceIndex(int index)
	{
		if (Enum.IsDefined(typeof(SteamVR_TrackedObject.EIndex), index))
		{
			this.index = (SteamVR_TrackedObject.EIndex)index;
		}
	}

	// Token: 0x04000E76 RID: 3702
	public SteamVR_TrackedObject.EIndex index;

	// Token: 0x04000E77 RID: 3703
	[Tooltip("If not set, relative to parent")]
	public Transform origin;

	// Token: 0x04000E79 RID: 3705
	private SteamVR_Events.Action newPosesAction;

	// Token: 0x02000581 RID: 1409
	public enum EIndex
	{
		// Token: 0x04002624 RID: 9764
		None = -1,
		// Token: 0x04002625 RID: 9765
		Hmd,
		// Token: 0x04002626 RID: 9766
		Device1,
		// Token: 0x04002627 RID: 9767
		Device2,
		// Token: 0x04002628 RID: 9768
		Device3,
		// Token: 0x04002629 RID: 9769
		Device4,
		// Token: 0x0400262A RID: 9770
		Device5,
		// Token: 0x0400262B RID: 9771
		Device6,
		// Token: 0x0400262C RID: 9772
		Device7,
		// Token: 0x0400262D RID: 9773
		Device8,
		// Token: 0x0400262E RID: 9774
		Device9,
		// Token: 0x0400262F RID: 9775
		Device10,
		// Token: 0x04002630 RID: 9776
		Device11,
		// Token: 0x04002631 RID: 9777
		Device12,
		// Token: 0x04002632 RID: 9778
		Device13,
		// Token: 0x04002633 RID: 9779
		Device14,
		// Token: 0x04002634 RID: 9780
		Device15
	}
}
