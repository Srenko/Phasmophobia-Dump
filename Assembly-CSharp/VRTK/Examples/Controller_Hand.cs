using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000350 RID: 848
	public class Controller_Hand : MonoBehaviour
	{
		// Token: 0x06001D88 RID: 7560 RVA: 0x00096BCC File Offset: 0x00094DCC
		private void Start()
		{
			base.GetComponentInParent<VRTK_InteractGrab>().GrabButtonPressed += this.DoGrabOn;
			base.GetComponentInParent<VRTK_InteractGrab>().GrabButtonReleased += this.DoGrabOff;
			base.GetComponentInParent<VRTK_InteractUse>().UseButtonPressed += this.DoUseOn;
			base.GetComponentInParent<VRTK_InteractUse>().UseButtonReleased += this.DoUseOff;
			string str = "ModelPieces";
			this.pointerFinger = base.transform.Find(str + "/PointerFingerContainer");
			this.gripFingers = base.transform.Find(str + "/GripFingerContainer");
			if (this.hand == Controller_Hand.Hands.Left)
			{
				this.InversePosition(this.pointerFinger);
				this.InversePosition(this.gripFingers);
				this.InversePosition(base.transform.Find(str + "/Palm"));
				this.InversePosition(base.transform.Find(str + "/Thumb"));
			}
			this.originalPointerRotation = this.pointerFinger.localEulerAngles.y;
			this.originalGripRotation = this.gripFingers.localEulerAngles.y;
			this.targetPointerRotation = this.originalPointerRotation;
			this.targetGripRotation = this.originalGripRotation;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x00096D10 File Offset: 0x00094F10
		private void InversePosition(Transform givenTransform)
		{
			givenTransform.localPosition = new Vector3(givenTransform.localPosition.x * -1f, givenTransform.localPosition.y, givenTransform.localPosition.z);
			givenTransform.localEulerAngles = new Vector3(givenTransform.localEulerAngles.x, givenTransform.localEulerAngles.y * -1f, givenTransform.localEulerAngles.z);
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x00096D81 File Offset: 0x00094F81
		private void DoGrabOn(object sender, ControllerInteractionEventArgs e)
		{
			this.targetGripRotation = this.maxRotation;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x00096D8F File Offset: 0x00094F8F
		private void DoGrabOff(object sender, ControllerInteractionEventArgs e)
		{
			this.targetGripRotation = this.originalGripRotation;
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x00096D9D File Offset: 0x00094F9D
		private void DoUseOn(object sender, ControllerInteractionEventArgs e)
		{
			this.targetPointerRotation = this.maxRotation;
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x00096DAB File Offset: 0x00094FAB
		private void DoUseOff(object sender, ControllerInteractionEventArgs e)
		{
			this.targetPointerRotation = this.originalPointerRotation;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x00096DBC File Offset: 0x00094FBC
		private void Update()
		{
			this.pointerFinger.localEulerAngles = new Vector3(this.targetPointerRotation, 0f, 0f);
			this.gripFingers.localEulerAngles = new Vector3(this.targetGripRotation, 0f, 0f);
		}

		// Token: 0x0400174A RID: 5962
		public Controller_Hand.Hands hand;

		// Token: 0x0400174B RID: 5963
		private Transform pointerFinger;

		// Token: 0x0400174C RID: 5964
		private Transform gripFingers;

		// Token: 0x0400174D RID: 5965
		private float maxRotation = 90f;

		// Token: 0x0400174E RID: 5966
		private float originalPointerRotation;

		// Token: 0x0400174F RID: 5967
		private float originalGripRotation;

		// Token: 0x04001750 RID: 5968
		private float targetPointerRotation;

		// Token: 0x04001751 RID: 5969
		private float targetGripRotation;

		// Token: 0x02000640 RID: 1600
		public enum Hands
		{
			// Token: 0x040028D3 RID: 10451
			Right,
			// Token: 0x040028D4 RID: 10452
			Left
		}
	}
}
