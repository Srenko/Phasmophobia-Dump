using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000373 RID: 883
	public class Whirlygig : VRTK_InteractableObject
	{
		// Token: 0x06001E75 RID: 7797 RVA: 0x0009A23A File Offset: 0x0009843A
		public override void StartUsing(VRTK_InteractUse usingObject)
		{
			base.StartUsing(usingObject);
			this.spinSpeed = 360f;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0009A24E File Offset: 0x0009844E
		public override void StopUsing(VRTK_InteractUse usingObject)
		{
			base.StopUsing(usingObject);
			this.spinSpeed = 0f;
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0009A262 File Offset: 0x00098462
		protected void Start()
		{
			this.rotator = base.transform.Find("Capsule");
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0009A27A File Offset: 0x0009847A
		protected override void Update()
		{
			base.Update();
			this.rotator.transform.Rotate(new Vector3(this.spinSpeed * Time.deltaTime, 0f, 0f));
		}

		// Token: 0x040017C7 RID: 6087
		private float spinSpeed;

		// Token: 0x040017C8 RID: 6088
		private Transform rotator;
	}
}
