using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000354 RID: 852
	public class FireExtinguisher_Base : VRTK_InteractableObject
	{
		// Token: 0x06001D99 RID: 7577 RVA: 0x00096F6A File Offset: 0x0009516A
		public override void StartUsing(VRTK_InteractUse usingObject)
		{
			base.StartUsing(usingObject);
			this.controllerEvents = usingObject.GetComponent<VRTK_ControllerEvents>();
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00096F7F File Offset: 0x0009517F
		public override void StopUsing(VRTK_InteractUse previousUsingObject)
		{
			base.StopUsing(previousUsingObject);
			this.controllerEvents = null;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00096F90 File Offset: 0x00095190
		protected override void Update()
		{
			base.Update();
			if (this.controllerEvents)
			{
				float triggerAxis = this.controllerEvents.GetTriggerAxis();
				this.Spray(triggerAxis);
				VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(this.controllerEvents.gameObject), triggerAxis * 0.25f, 0.1f, 0.01f);
				return;
			}
			this.Spray(0f);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x00096FF5 File Offset: 0x000951F5
		private void Spray(float power)
		{
			this.SetHandleFrame(power);
			this.sprayer.Spray(power);
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x0009700C File Offset: 0x0009520C
		private void SetHandleFrame(float frame)
		{
			this.leverAnimation["FireExtinguisherLever"].speed = 0f;
			this.leverAnimation["FireExtinguisherLever"].time = frame;
			this.leverAnimation.Play("FireExtinguisherLever");
		}

		// Token: 0x04001756 RID: 5974
		public Animation leverAnimation;

		// Token: 0x04001757 RID: 5975
		public FireExtinguisher_Sprayer sprayer;

		// Token: 0x04001758 RID: 5976
		private VRTK_ControllerEvents controllerEvents;
	}
}
