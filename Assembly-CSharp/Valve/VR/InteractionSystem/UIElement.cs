using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200043A RID: 1082
	[RequireComponent(typeof(Interactable))]
	public class UIElement : MonoBehaviour
	{
		// Token: 0x0600211A RID: 8474 RVA: 0x000A3570 File Offset: 0x000A1770
		private void Awake()
		{
			Button component = base.GetComponent<Button>();
			if (component)
			{
				component.onClick.AddListener(new UnityAction(this.OnButtonClick));
			}
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x000A35A3 File Offset: 0x000A17A3
		private void OnHandHoverBegin(Hand hand)
		{
			this.currentHand = hand;
			InputModule.instance.HoverBegin(base.gameObject);
			ControllerButtonHints.ShowButtonHint(hand, new EVRButtonId[]
			{
				EVRButtonId.k_EButton_Axis1
			});
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x000A35CD File Offset: 0x000A17CD
		private void OnHandHoverEnd(Hand hand)
		{
			InputModule.instance.HoverEnd(base.gameObject);
			ControllerButtonHints.HideButtonHint(hand, new EVRButtonId[]
			{
				EVRButtonId.k_EButton_Axis1
			});
			this.currentHand = null;
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000A35F7 File Offset: 0x000A17F7
		private void HandHoverUpdate(Hand hand)
		{
			if (hand.GetStandardInteractionButtonDown())
			{
				InputModule.instance.Submit(base.gameObject);
				ControllerButtonHints.HideButtonHint(hand, new EVRButtonId[]
				{
					EVRButtonId.k_EButton_Axis1
				});
			}
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000A3622 File Offset: 0x000A1822
		private void OnButtonClick()
		{
			this.onHandClick.Invoke(this.currentHand);
		}

		// Token: 0x04001EA6 RID: 7846
		public CustomEvents.UnityEventHand onHandClick;

		// Token: 0x04001EA7 RID: 7847
		private Hand currentHand;
	}
}
