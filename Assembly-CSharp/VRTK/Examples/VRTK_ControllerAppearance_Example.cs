using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x0200036D RID: 877
	public class VRTK_ControllerAppearance_Example : MonoBehaviour
	{
		// Token: 0x06001E20 RID: 7712 RVA: 0x00098C3C File Offset: 0x00096E3C
		private void Start()
		{
			if (base.GetComponent<VRTK_ControllerEvents>() == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_ControllerAppearance_Example",
					"VRTK_ControllerEvents",
					"the same"
				}));
				return;
			}
			this.events = base.GetComponent<VRTK_ControllerEvents>();
			this.highligher = base.GetComponent<VRTK_ControllerHighlighter>();
			this.tooltips = base.GetComponentInChildren<VRTK_ControllerTooltips>();
			this.currentPulseColor = this.pulseColor;
			this.highlighted = false;
			this.events.TriggerPressed += this.DoTriggerPressed;
			this.events.TriggerReleased += this.DoTriggerReleased;
			this.events.ButtonOnePressed += this.DoButtonOnePressed;
			this.events.ButtonOneReleased += this.DoButtonOneReleased;
			this.events.ButtonTwoPressed += this.DoButtonTwoPressed;
			this.events.ButtonTwoReleased += this.DoButtonTwoReleased;
			this.events.StartMenuPressed += this.DoStartMenuPressed;
			this.events.StartMenuReleased += this.DoStartMenuReleased;
			this.events.GripPressed += this.DoGripPressed;
			this.events.GripReleased += this.DoGripReleased;
			this.events.TouchpadPressed += this.DoTouchpadPressed;
			this.events.TouchpadReleased += this.DoTouchpadReleased;
			this.tooltips.ToggleTips(false, VRTK_ControllerTooltips.TooltipButtons.None);
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00098DDC File Offset: 0x00096FDC
		private void PulseTrigger()
		{
			this.highligher.HighlightElement(SDK_BaseController.ControllerElements.Trigger, this.currentPulseColor, this.pulseTimer);
			this.currentPulseColor = ((this.currentPulseColor == this.pulseColor) ? this.highlightColor : this.pulseColor);
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x00098E28 File Offset: 0x00097028
		private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.tooltips.ToggleTips(true, VRTK_ControllerTooltips.TooltipButtons.TriggerTooltip);
			this.highligher.HighlightElement(SDK_BaseController.ControllerElements.Trigger, this.highlightColor, this.pulseTriggerHighlightColor ? this.pulseTimer : this.highlightTimer);
			if (this.pulseTriggerHighlightColor)
			{
				base.InvokeRepeating("PulseTrigger", this.pulseTimer, this.pulseTimer);
			}
			VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(this.events.gameObject), this.dimOpacity, 0f);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00098EAC File Offset: 0x000970AC
		private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.tooltips.ToggleTips(false, VRTK_ControllerTooltips.TooltipButtons.TriggerTooltip);
			this.highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Trigger);
			if (this.pulseTriggerHighlightColor)
			{
				base.CancelInvoke("PulseTrigger");
			}
			if (!this.events.AnyButtonPressed())
			{
				VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(this.events.gameObject), this.defaultOpacity, 0f);
			}
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00098F14 File Offset: 0x00097114
		private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)
		{
			this.tooltips.ToggleTips(true, VRTK_ControllerTooltips.TooltipButtons.ButtonOneTooltip);
			this.highligher.HighlightElement(SDK_BaseController.ControllerElements.ButtonOne, this.highlightColor, this.highlightTimer);
			VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(this.events.gameObject), this.dimOpacity, 0f);
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00098F68 File Offset: 0x00097168
		private void DoButtonOneReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.tooltips.ToggleTips(false, VRTK_ControllerTooltips.TooltipButtons.ButtonOneTooltip);
			this.highligher.UnhighlightElement(SDK_BaseController.ControllerElements.ButtonOne);
			if (!this.events.AnyButtonPressed())
			{
				VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(this.events.gameObject), this.defaultOpacity, 0f);
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00098FBC File Offset: 0x000971BC
		private void DoButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.tooltips.ToggleTips(true, VRTK_ControllerTooltips.TooltipButtons.ButtonTwoTooltip);
			this.highligher.HighlightElement(SDK_BaseController.ControllerElements.ButtonTwo, this.highlightColor, this.highlightTimer);
			VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(this.events.gameObject), this.dimOpacity, 0f);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00099010 File Offset: 0x00097210
		private void DoButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.tooltips.ToggleTips(false, VRTK_ControllerTooltips.TooltipButtons.ButtonTwoTooltip);
			this.highligher.UnhighlightElement(SDK_BaseController.ControllerElements.ButtonTwo);
			if (!this.events.AnyButtonPressed())
			{
				VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(this.events.gameObject), this.defaultOpacity, 0f);
			}
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00099064 File Offset: 0x00097264
		private void DoStartMenuPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.tooltips.ToggleTips(true, VRTK_ControllerTooltips.TooltipButtons.StartMenuTooltip);
			this.highligher.HighlightElement(SDK_BaseController.ControllerElements.StartMenu, this.highlightColor, this.highlightTimer);
			VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(this.events.gameObject), this.dimOpacity, 0f);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000990B8 File Offset: 0x000972B8
		private void DoStartMenuReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.tooltips.ToggleTips(false, VRTK_ControllerTooltips.TooltipButtons.StartMenuTooltip);
			this.highligher.UnhighlightElement(SDK_BaseController.ControllerElements.StartMenu);
			if (!this.events.AnyButtonPressed())
			{
				VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(this.events.gameObject), this.defaultOpacity, 0f);
			}
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0009910C File Offset: 0x0009730C
		private void DoGripPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.tooltips.ToggleTips(true, VRTK_ControllerTooltips.TooltipButtons.GripTooltip);
			this.highligher.HighlightElement(SDK_BaseController.ControllerElements.GripLeft, this.highlightColor, this.highlightTimer);
			this.highligher.HighlightElement(SDK_BaseController.ControllerElements.GripRight, this.highlightColor, this.highlightTimer);
			VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(this.events.gameObject), this.dimOpacity, 0f);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00099178 File Offset: 0x00097378
		private void DoGripReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.tooltips.ToggleTips(false, VRTK_ControllerTooltips.TooltipButtons.GripTooltip);
			this.highligher.UnhighlightElement(SDK_BaseController.ControllerElements.GripLeft);
			this.highligher.UnhighlightElement(SDK_BaseController.ControllerElements.GripRight);
			if (!this.events.AnyButtonPressed())
			{
				VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(this.events.gameObject), this.defaultOpacity, 0f);
			}
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x000991D8 File Offset: 0x000973D8
		private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
		{
			this.tooltips.ToggleTips(true, VRTK_ControllerTooltips.TooltipButtons.TouchpadTooltip);
			this.highligher.HighlightElement(SDK_BaseController.ControllerElements.Touchpad, this.highlightColor, this.highlightTimer);
			VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(this.events.gameObject), this.dimOpacity, 0f);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0009922C File Offset: 0x0009742C
		private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
		{
			this.tooltips.ToggleTips(false, VRTK_ControllerTooltips.TooltipButtons.TouchpadTooltip);
			this.highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Touchpad);
			if (!this.events.AnyButtonPressed())
			{
				VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(this.events.gameObject), this.defaultOpacity, 0f);
			}
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0009927F File Offset: 0x0009747F
		private void OnTriggerEnter(Collider collider)
		{
			this.OnTriggerStay(collider);
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00099288 File Offset: 0x00097488
		private void OnTriggerStay(Collider collider)
		{
			if (!VRTK_PlayerObject.IsPlayerObject(collider.gameObject, VRTK_PlayerObject.ObjectTypes.Null) && !this.highlighted)
			{
				if (this.highlightBodyOnlyOnCollision)
				{
					this.highligher.HighlightElement(SDK_BaseController.ControllerElements.Body, this.highlightColor, this.highlightTimer);
				}
				else
				{
					this.highligher.HighlightController(this.highlightColor, this.highlightTimer);
				}
				this.highlighted = true;
			}
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x000992EB File Offset: 0x000974EB
		private void OnTriggerExit(Collider collider)
		{
			if (!VRTK_PlayerObject.IsPlayerObject(collider.gameObject, VRTK_PlayerObject.ObjectTypes.Null))
			{
				if (this.highlightBodyOnlyOnCollision)
				{
					this.highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Body);
				}
				else
				{
					this.highligher.UnhighlightController();
				}
				this.highlighted = false;
			}
		}

		// Token: 0x040017B7 RID: 6071
		public bool highlightBodyOnlyOnCollision;

		// Token: 0x040017B8 RID: 6072
		public bool pulseTriggerHighlightColor;

		// Token: 0x040017B9 RID: 6073
		private VRTK_ControllerTooltips tooltips;

		// Token: 0x040017BA RID: 6074
		private VRTK_ControllerHighlighter highligher;

		// Token: 0x040017BB RID: 6075
		private VRTK_ControllerEvents events;

		// Token: 0x040017BC RID: 6076
		private Color highlightColor = Color.yellow;

		// Token: 0x040017BD RID: 6077
		private Color pulseColor = Color.black;

		// Token: 0x040017BE RID: 6078
		private Color currentPulseColor;

		// Token: 0x040017BF RID: 6079
		private float highlightTimer = 0.5f;

		// Token: 0x040017C0 RID: 6080
		private float pulseTimer = 0.75f;

		// Token: 0x040017C1 RID: 6081
		private float dimOpacity = 0.8f;

		// Token: 0x040017C2 RID: 6082
		private float defaultOpacity = 1f;

		// Token: 0x040017C3 RID: 6083
		private bool highlighted;
	}
}
