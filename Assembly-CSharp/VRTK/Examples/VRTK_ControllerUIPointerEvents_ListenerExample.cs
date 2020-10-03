using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000371 RID: 881
	public class VRTK_ControllerUIPointerEvents_ListenerExample : MonoBehaviour
	{
		// Token: 0x06001E67 RID: 7783 RVA: 0x00099F8C File Offset: 0x0009818C
		private void Start()
		{
			if (base.GetComponent<VRTK_UIPointer>() == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_ControllerUIPointerEvents_ListenerExample",
					"VRTK_UIPointer",
					"the Controller Alias"
				}));
				return;
			}
			if (this.togglePointerOnHit)
			{
				base.GetComponent<VRTK_UIPointer>().activationMode = VRTK_UIPointer.ActivationMethods.AlwaysOn;
			}
			base.GetComponent<VRTK_UIPointer>().UIPointerElementEnter += this.VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementEnter;
			base.GetComponent<VRTK_UIPointer>().UIPointerElementExit += this.VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementExit;
			base.GetComponent<VRTK_UIPointer>().UIPointerElementClick += this.VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementClick;
			base.GetComponent<VRTK_UIPointer>().UIPointerElementDragStart += this.VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementDragStart;
			base.GetComponent<VRTK_UIPointer>().UIPointerElementDragEnd += this.VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementDragEnd;
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0009A058 File Offset: 0x00098258
		private void VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementEnter(object sender, UIPointerEventArgs e)
		{
			if (this.togglePointerOnHit && base.GetComponent<VRTK_Pointer>())
			{
				base.GetComponent<VRTK_Pointer>().Toggle(true);
			}
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0009A07B File Offset: 0x0009827B
		private void VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementExit(object sender, UIPointerEventArgs e)
		{
			if (this.togglePointerOnHit && base.GetComponent<VRTK_Pointer>())
			{
				base.GetComponent<VRTK_Pointer>().Toggle(false);
			}
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x00003F60 File Offset: 0x00002160
		private void VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementClick(object sender, UIPointerEventArgs e)
		{
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00003F60 File Offset: 0x00002160
		private void VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementDragStart(object sender, UIPointerEventArgs e)
		{
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x00003F60 File Offset: 0x00002160
		private void VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementDragEnd(object sender, UIPointerEventArgs e)
		{
		}

		// Token: 0x040017C5 RID: 6085
		public bool togglePointerOnHit;
	}
}
