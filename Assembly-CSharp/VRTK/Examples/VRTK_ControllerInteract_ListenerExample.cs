using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x0200036F RID: 879
	public class VRTK_ControllerInteract_ListenerExample : MonoBehaviour
	{
		// Token: 0x06001E59 RID: 7769 RVA: 0x00099BD0 File Offset: 0x00097DD0
		private void Start()
		{
			if (base.GetComponent<VRTK_InteractTouch>() == null || base.GetComponent<VRTK_InteractGrab>() == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_ControllerInteract_ListenerExample",
					"VRTK_InteractTouch and VRTK_InteractGrab",
					"the Controller Alias"
				}));
				return;
			}
			base.GetComponent<VRTK_InteractTouch>().ControllerTouchInteractableObject += this.DoInteractTouch;
			base.GetComponent<VRTK_InteractTouch>().ControllerUntouchInteractableObject += this.DoInteractUntouch;
			base.GetComponent<VRTK_InteractGrab>().ControllerGrabInteractableObject += this.DoInteractGrab;
			base.GetComponent<VRTK_InteractGrab>().ControllerUngrabInteractableObject += this.DoInteractUngrab;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00099C7F File Offset: 0x00097E7F
		private void DebugLogger(uint index, string action, GameObject target)
		{
			VRTK_Logger.Info(string.Concat(new object[]
			{
				"Controller on index '",
				index,
				"' is ",
				action,
				" an object named ",
				target.name
			}));
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00099CBF File Offset: 0x00097EBF
		private void DoInteractTouch(object sender, ObjectInteractEventArgs e)
		{
			if (e.target)
			{
				this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHING", e.target);
			}
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00099CEA File Offset: 0x00097EEA
		private void DoInteractUntouch(object sender, ObjectInteractEventArgs e)
		{
			if (e.target)
			{
				this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "NO LONGER TOUCHING", e.target);
			}
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x00099D15 File Offset: 0x00097F15
		private void DoInteractGrab(object sender, ObjectInteractEventArgs e)
		{
			if (e.target)
			{
				this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRABBING", e.target);
			}
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x00099D40 File Offset: 0x00097F40
		private void DoInteractUngrab(object sender, ObjectInteractEventArgs e)
		{
			if (e.target)
			{
				this.DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "NO LONGER GRABBING", e.target);
			}
		}
	}
}
