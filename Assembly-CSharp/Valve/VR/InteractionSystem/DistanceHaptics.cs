using System;
using System.Collections;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200041A RID: 1050
	public class DistanceHaptics : MonoBehaviour
	{
		// Token: 0x0600205E RID: 8286 RVA: 0x0009FC07 File Offset: 0x0009DE07
		private IEnumerator Start()
		{
			for (;;)
			{
				float time = Vector3.Distance(this.firstTransform.position, this.secondTransform.position);
				SteamVR_TrackedObject componentInParent = base.GetComponentInParent<SteamVR_TrackedObject>();
				if (componentInParent)
				{
					float num = this.distanceIntensityCurve.Evaluate(time);
					SteamVR_Controller.Input((int)componentInParent.index).TriggerHapticPulse((ushort)num, EVRButtonId.k_EButton_Axis0);
				}
				float seconds = this.pulseIntervalCurve.Evaluate(time);
				yield return new WaitForSeconds(seconds);
			}
			yield break;
		}

		// Token: 0x04001DE8 RID: 7656
		public Transform firstTransform;

		// Token: 0x04001DE9 RID: 7657
		public Transform secondTransform;

		// Token: 0x04001DEA RID: 7658
		public AnimationCurve distanceIntensityCurve = AnimationCurve.Linear(0f, 800f, 1f, 800f);

		// Token: 0x04001DEB RID: 7659
		public AnimationCurve pulseIntervalCurve = AnimationCurve.Linear(0f, 0.01f, 1f, 0f);
	}
}
