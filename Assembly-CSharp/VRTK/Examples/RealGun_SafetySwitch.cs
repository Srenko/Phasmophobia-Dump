using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000362 RID: 866
	public class RealGun_SafetySwitch : VRTK_InteractableObject
	{
		// Token: 0x06001DE7 RID: 7655 RVA: 0x000981A8 File Offset: 0x000963A8
		public override void StartUsing(VRTK_InteractUse currentUsingObject)
		{
			base.StartUsing(currentUsingObject);
			this.SetSafety(!this.safetyOff);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x000981C0 File Offset: 0x000963C0
		protected void Start()
		{
			this.fixedPosition = base.transform.localPosition;
			this.SetSafety(this.safetyOff);
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000981E0 File Offset: 0x000963E0
		protected override void Update()
		{
			base.Update();
			base.transform.localPosition = this.fixedPosition;
			if (this.safetyOff)
			{
				base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				return;
			}
			base.transform.localEulerAngles = new Vector3(0f, this.offAngle, 0f);
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0009824C File Offset: 0x0009644C
		private void SetSafety(bool safety)
		{
			this.safetyOff = safety;
		}

		// Token: 0x04001799 RID: 6041
		public bool safetyOff = true;

		// Token: 0x0400179A RID: 6042
		private float offAngle = 40f;

		// Token: 0x0400179B RID: 6043
		private Vector3 fixedPosition;
	}
}
