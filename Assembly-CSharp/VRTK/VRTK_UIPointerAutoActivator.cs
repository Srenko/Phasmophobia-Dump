using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002FF RID: 767
	public class VRTK_UIPointerAutoActivator : MonoBehaviour
	{
		// Token: 0x06001A9C RID: 6812 RVA: 0x0008C700 File Offset: 0x0008A900
		protected virtual void OnTriggerEnter(Collider collider)
		{
			VRTK_PlayerObject componentInParent = collider.GetComponentInParent<VRTK_PlayerObject>();
			VRTK_UIPointer componentInParent2 = collider.GetComponentInParent<VRTK_UIPointer>();
			if (componentInParent2 && componentInParent && componentInParent.objectType == VRTK_PlayerObject.ObjectTypes.Collider)
			{
				componentInParent2.autoActivatingCanvas = base.gameObject;
			}
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0008C740 File Offset: 0x0008A940
		protected virtual void OnTriggerExit(Collider collider)
		{
			VRTK_UIPointer componentInParent = collider.GetComponentInParent<VRTK_UIPointer>();
			if (componentInParent && componentInParent.autoActivatingCanvas == base.gameObject)
			{
				componentInParent.autoActivatingCanvas = null;
			}
		}
	}
}
