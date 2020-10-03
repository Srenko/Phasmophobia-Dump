using System;
using UnityEngine;
using UnityEngine.UI;

namespace VRTK.Examples.PanelMenu
{
	// Token: 0x0200037B RID: 891
	public class PanelMenuUISlider : MonoBehaviour
	{
		// Token: 0x06001EA8 RID: 7848 RVA: 0x0009BDA0 File Offset: 0x00099FA0
		private void Start()
		{
			this.slider = base.GetComponent<Slider>();
			if (this.slider == null)
			{
				VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"PanelMenuUISlider",
					"Slider",
					"the same"
				}));
				return;
			}
			base.GetComponentInParent<VRTK_PanelMenuItemController>().PanelMenuItemSwipeLeft += this.OnPanelMenuItemSwipeLeft;
			base.GetComponentInParent<VRTK_PanelMenuItemController>().PanelMenuItemSwipeRight += this.OnPanelMenuItemSwipeRight;
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x0009BE1F File Offset: 0x0009A01F
		private void OnPanelMenuItemSwipeLeft(object sender, PanelMenuItemControllerEventArgs e)
		{
			this.slider.value -= 1f;
			this.SendMessageToInteractableObject(e.interactableObject);
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0009BE44 File Offset: 0x0009A044
		private void OnPanelMenuItemSwipeRight(object sender, PanelMenuItemControllerEventArgs e)
		{
			this.slider.value += 1f;
			this.SendMessageToInteractableObject(e.interactableObject);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0009BE69 File Offset: 0x0009A069
		private void SendMessageToInteractableObject(GameObject interactableObject)
		{
			interactableObject.SendMessage("UpdateSliderValue", this.slider.value);
		}

		// Token: 0x040017E7 RID: 6119
		private Slider slider;
	}
}
