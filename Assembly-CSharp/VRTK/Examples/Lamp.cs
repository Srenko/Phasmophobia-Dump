using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000358 RID: 856
	public class Lamp : VRTK_InteractableObject
	{
		// Token: 0x06001DAA RID: 7594 RVA: 0x000973B6 File Offset: 0x000955B6
		public override void Grabbed(VRTK_InteractGrab grabbingObject)
		{
			base.Grabbed(grabbingObject);
			this.ToggleKinematics(false);
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x000973C6 File Offset: 0x000955C6
		public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
		{
			base.Ungrabbed(previousGrabbingObject);
			this.ToggleKinematics(true);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x000973D8 File Offset: 0x000955D8
		private void ToggleKinematics(bool state)
		{
			Rigidbody[] componentsInChildren = base.transform.parent.GetComponentsInChildren<Rigidbody>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].isKinematic = state;
			}
		}
	}
}
