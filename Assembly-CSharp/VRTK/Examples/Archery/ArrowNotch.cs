using System;
using UnityEngine;

namespace VRTK.Examples.Archery
{
	// Token: 0x0200037D RID: 893
	public class ArrowNotch : MonoBehaviour
	{
		// Token: 0x06001EB9 RID: 7865 RVA: 0x0009C06F File Offset: 0x0009A26F
		private void Start()
		{
			this.arrow = base.transform.Find("Arrow").gameObject;
			this.obj = base.GetComponent<VRTK_InteractableObject>();
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x0009C098 File Offset: 0x0009A298
		private void OnTriggerEnter(Collider collider)
		{
			BowHandle componentInParent = collider.GetComponentInParent<BowHandle>();
			if (componentInParent != null && this.obj != null && componentInParent.aim.IsHeld() && this.obj.IsGrabbed(null))
			{
				componentInParent.nockSide = collider.transform;
				this.arrow.transform.SetParent(componentInParent.arrowNockingPoint);
				this.CopyNotchToArrow();
				collider.GetComponentInParent<BowAim>().SetArrow(this.arrow);
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x0009C124 File Offset: 0x0009A324
		private void CopyNotchToArrow()
		{
			GameObject gameObject = Object.Instantiate<GameObject>(base.gameObject, base.transform.position, base.transform.rotation);
			gameObject.name = base.name;
			this.arrow.GetComponent<Arrow>().SetArrowHolder(gameObject);
			this.arrow.GetComponent<Arrow>().OnNock();
		}

		// Token: 0x040017F0 RID: 6128
		private GameObject arrow;

		// Token: 0x040017F1 RID: 6129
		private VRTK_InteractableObject obj;
	}
}
