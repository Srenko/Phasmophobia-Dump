using System;
using System.Collections;
using UnityEngine;

namespace VRTK.Examples.Archery
{
	// Token: 0x0200037F RID: 895
	public class BowAim : MonoBehaviour
	{
		// Token: 0x06001EC2 RID: 7874 RVA: 0x0009C2FC File Offset: 0x0009A4FC
		public VRTK_InteractGrab GetPullHand()
		{
			return this.stringControl;
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0009C304 File Offset: 0x0009A504
		public bool IsHeld()
		{
			return this.interact.IsGrabbed(null);
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0009C312 File Offset: 0x0009A512
		public bool HasArrow()
		{
			return this.currentArrow != null;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x0009C320 File Offset: 0x0009A520
		public void SetArrow(GameObject arrow)
		{
			this.currentArrow = arrow;
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x0009C329 File Offset: 0x0009A529
		private void Start()
		{
			this.bowAnimation = base.GetComponent<BowAnimation>();
			this.handle = base.GetComponentInChildren<BowHandle>();
			this.interact = base.GetComponent<VRTK_InteractableObject>();
			this.interact.InteractableObjectGrabbed += this.DoObjectGrab;
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x0009C368 File Offset: 0x0009A568
		private void DoObjectGrab(object sender, InteractableObjectEventArgs e)
		{
			if (VRTK_DeviceFinder.IsControllerLeftHand(e.interactingObject))
			{
				this.holdControl = VRTK_DeviceFinder.GetControllerLeftHand(false).GetComponent<VRTK_InteractGrab>();
				this.stringControl = VRTK_DeviceFinder.GetControllerRightHand(false).GetComponent<VRTK_InteractGrab>();
			}
			else
			{
				this.stringControl = VRTK_DeviceFinder.GetControllerLeftHand(false).GetComponent<VRTK_InteractGrab>();
				this.holdControl = VRTK_DeviceFinder.GetControllerRightHand(false).GetComponent<VRTK_InteractGrab>();
			}
			base.StartCoroutine("GetBaseRotation");
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x0009C3D4 File Offset: 0x0009A5D4
		private IEnumerator GetBaseRotation()
		{
			yield return new WaitForEndOfFrame();
			this.baseRotation = base.transform.localRotation;
			yield break;
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x0009C3E4 File Offset: 0x0009A5E4
		private void Update()
		{
			if (this.currentArrow != null && this.IsHeld())
			{
				this.AimArrow();
				this.AimBow();
				this.PullString();
				if (!this.stringControl.IsGrabButtonPressed())
				{
					this.currentArrow.GetComponent<Arrow>().Fired();
					this.fired = true;
					this.releaseRotation = base.transform.localRotation;
					this.Release();
				}
			}
			else if (this.IsHeld())
			{
				if (this.fired)
				{
					this.fired = false;
					this.fireOffset = Time.time;
				}
				if (!this.releaseRotation.Equals(this.baseRotation))
				{
					base.transform.localRotation = Quaternion.Lerp(this.releaseRotation, this.baseRotation, (Time.time - this.fireOffset) * 8f);
				}
			}
			if (!this.IsHeld() && this.currentArrow != null)
			{
				this.Release();
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x0009C4D8 File Offset: 0x0009A6D8
		private void Release()
		{
			this.bowAnimation.SetFrame(0f);
			this.currentArrow.transform.SetParent(null);
			Collider[] componentsInChildren = this.currentArrow.GetComponentsInChildren<Collider>();
			Collider[] componentsInChildren2 = base.GetComponentsInChildren<Collider>();
			foreach (Collider collider in componentsInChildren)
			{
				collider.enabled = true;
				foreach (Collider collider2 in componentsInChildren2)
				{
					Physics.IgnoreCollision(collider, collider2);
				}
			}
			this.currentArrow.GetComponent<Rigidbody>().isKinematic = false;
			this.currentArrow.GetComponent<Rigidbody>().velocity = this.currentPull * this.powerMultiplier * this.currentArrow.transform.TransformDirection(Vector3.forward);
			this.currentArrow.GetComponent<Arrow>().inFlight = true;
			this.currentArrow = null;
			this.currentPull = 0f;
			this.ReleaseArrow();
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x0009C5C8 File Offset: 0x0009A7C8
		private void ReleaseArrow()
		{
			if (this.stringControl)
			{
				this.stringControl.ForceRelease(false);
			}
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0009C5E3 File Offset: 0x0009A7E3
		private void AimArrow()
		{
			this.currentArrow.transform.localPosition = Vector3.zero;
			this.currentArrow.transform.LookAt(this.handle.nockSide.position);
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x0009C61C File Offset: 0x0009A81C
		private void AimBow()
		{
			base.transform.rotation = Quaternion.LookRotation(this.holdControl.transform.position - this.stringControl.transform.position, this.holdControl.transform.TransformDirection(Vector3.forward));
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x0009C674 File Offset: 0x0009A874
		private void PullString()
		{
			this.currentPull = Mathf.Clamp((Vector3.Distance(this.holdControl.transform.position, this.stringControl.transform.position) - this.pullOffset) * this.pullMultiplier, 0f, this.maxPullDistance);
			this.bowAnimation.SetFrame(this.currentPull);
			if (!this.currentPull.ToString("F2").Equals(this.previousPull.ToString("F2")))
			{
				VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(this.holdControl.gameObject), this.bowVibration);
				VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(this.stringControl.gameObject), this.stringVibration);
			}
			this.previousPull = this.currentPull;
		}

		// Token: 0x040017F6 RID: 6134
		public float powerMultiplier;

		// Token: 0x040017F7 RID: 6135
		public float pullMultiplier;

		// Token: 0x040017F8 RID: 6136
		public float pullOffset;

		// Token: 0x040017F9 RID: 6137
		public float maxPullDistance = 1.1f;

		// Token: 0x040017FA RID: 6138
		public float bowVibration = 0.062f;

		// Token: 0x040017FB RID: 6139
		public float stringVibration = 0.087f;

		// Token: 0x040017FC RID: 6140
		private BowAnimation bowAnimation;

		// Token: 0x040017FD RID: 6141
		private GameObject currentArrow;

		// Token: 0x040017FE RID: 6142
		private BowHandle handle;

		// Token: 0x040017FF RID: 6143
		private VRTK_InteractableObject interact;

		// Token: 0x04001800 RID: 6144
		private VRTK_InteractGrab holdControl;

		// Token: 0x04001801 RID: 6145
		private VRTK_InteractGrab stringControl;

		// Token: 0x04001802 RID: 6146
		private Quaternion releaseRotation;

		// Token: 0x04001803 RID: 6147
		private Quaternion baseRotation;

		// Token: 0x04001804 RID: 6148
		private bool fired;

		// Token: 0x04001805 RID: 6149
		private float fireOffset;

		// Token: 0x04001806 RID: 6150
		private float currentPull;

		// Token: 0x04001807 RID: 6151
		private float previousPull;
	}
}
