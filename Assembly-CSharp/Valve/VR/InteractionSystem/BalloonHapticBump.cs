using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000446 RID: 1094
	public class BalloonHapticBump : MonoBehaviour
	{
		// Token: 0x060021AA RID: 8618 RVA: 0x000A6ADC File Offset: 0x000A4CDC
		private void OnCollisionEnter(Collision other)
		{
			if (other.collider.GetComponentInParent<Balloon>() != null)
			{
				Hand componentInParent = this.physParent.GetComponentInParent<Hand>();
				if (componentInParent != null)
				{
					componentInParent.controller.TriggerHapticPulse(500, EVRButtonId.k_EButton_Axis0);
				}
			}
		}

		// Token: 0x04001F0C RID: 7948
		public GameObject physParent;
	}
}
