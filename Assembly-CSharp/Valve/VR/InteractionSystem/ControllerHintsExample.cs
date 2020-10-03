using System;
using System.Collections;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200044C RID: 1100
	public class ControllerHintsExample : MonoBehaviour
	{
		// Token: 0x060021D2 RID: 8658 RVA: 0x000A77FC File Offset: 0x000A59FC
		public void ShowButtonHints(Hand hand)
		{
			if (this.buttonHintCoroutine != null)
			{
				base.StopCoroutine(this.buttonHintCoroutine);
			}
			this.buttonHintCoroutine = base.StartCoroutine(this.TestButtonHints(hand));
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x000A7825 File Offset: 0x000A5A25
		public void ShowTextHints(Hand hand)
		{
			if (this.textHintCoroutine != null)
			{
				base.StopCoroutine(this.textHintCoroutine);
			}
			this.textHintCoroutine = base.StartCoroutine(this.TestTextHints(hand));
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x000A7850 File Offset: 0x000A5A50
		public void DisableHints()
		{
			if (this.buttonHintCoroutine != null)
			{
				base.StopCoroutine(this.buttonHintCoroutine);
				this.buttonHintCoroutine = null;
			}
			if (this.textHintCoroutine != null)
			{
				base.StopCoroutine(this.textHintCoroutine);
				this.textHintCoroutine = null;
			}
			foreach (Hand hand in Player.instance.hands)
			{
				ControllerButtonHints.HideAllButtonHints(hand);
				ControllerButtonHints.HideAllTextHints(hand);
			}
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000A78BA File Offset: 0x000A5ABA
		private IEnumerator TestButtonHints(Hand hand)
		{
			ControllerButtonHints.HideAllButtonHints(hand);
			for (;;)
			{
				ControllerButtonHints.ShowButtonHint(hand, new EVRButtonId[]
				{
					EVRButtonId.k_EButton_ApplicationMenu
				});
				yield return new WaitForSeconds(1f);
				ControllerButtonHints.ShowButtonHint(hand, new EVRButtonId[1]);
				yield return new WaitForSeconds(1f);
				ControllerButtonHints.ShowButtonHint(hand, new EVRButtonId[]
				{
					EVRButtonId.k_EButton_Grip
				});
				yield return new WaitForSeconds(1f);
				ControllerButtonHints.ShowButtonHint(hand, new EVRButtonId[]
				{
					EVRButtonId.k_EButton_Axis1
				});
				yield return new WaitForSeconds(1f);
				ControllerButtonHints.ShowButtonHint(hand, new EVRButtonId[]
				{
					EVRButtonId.k_EButton_Axis0
				});
				yield return new WaitForSeconds(1f);
				ControllerButtonHints.HideAllButtonHints(hand);
				yield return new WaitForSeconds(1f);
			}
			yield break;
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x000A78C9 File Offset: 0x000A5AC9
		private IEnumerator TestTextHints(Hand hand)
		{
			ControllerButtonHints.HideAllTextHints(hand);
			for (;;)
			{
				ControllerButtonHints.ShowTextHint(hand, EVRButtonId.k_EButton_ApplicationMenu, "Application", true);
				yield return new WaitForSeconds(3f);
				ControllerButtonHints.ShowTextHint(hand, EVRButtonId.k_EButton_System, "System", true);
				yield return new WaitForSeconds(3f);
				ControllerButtonHints.ShowTextHint(hand, EVRButtonId.k_EButton_Grip, "Grip", true);
				yield return new WaitForSeconds(3f);
				ControllerButtonHints.ShowTextHint(hand, EVRButtonId.k_EButton_Axis1, "Trigger", true);
				yield return new WaitForSeconds(3f);
				ControllerButtonHints.ShowTextHint(hand, EVRButtonId.k_EButton_Axis0, "Touchpad", true);
				yield return new WaitForSeconds(3f);
				ControllerButtonHints.HideAllTextHints(hand);
				yield return new WaitForSeconds(3f);
			}
			yield break;
		}

		// Token: 0x04001F5A RID: 8026
		private Coroutine buttonHintCoroutine;

		// Token: 0x04001F5B RID: 8027
		private Coroutine textHintCoroutine;
	}
}
