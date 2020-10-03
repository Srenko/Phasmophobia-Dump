using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200024B RID: 587
	public class VRTK_ControllerTooltips : MonoBehaviour
	{
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060010F6 RID: 4342 RVA: 0x00063AEC File Offset: 0x00061CEC
		// (remove) Token: 0x060010F7 RID: 4343 RVA: 0x00063B24 File Offset: 0x00061D24
		public event ControllerTooltipsEventHandler ControllerTooltipOn;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060010F8 RID: 4344 RVA: 0x00063B5C File Offset: 0x00061D5C
		// (remove) Token: 0x060010F9 RID: 4345 RVA: 0x00063B94 File Offset: 0x00061D94
		public event ControllerTooltipsEventHandler ControllerTooltipOff;

		// Token: 0x060010FA RID: 4346 RVA: 0x00063BC9 File Offset: 0x00061DC9
		public virtual void OnControllerTooltipOn(ControllerTooltipsEventArgs e)
		{
			if (this.ControllerTooltipOn != null)
			{
				this.ControllerTooltipOn(this, e);
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00063BE0 File Offset: 0x00061DE0
		public virtual void OnControllerTooltipOff(ControllerTooltipsEventArgs e)
		{
			if (this.ControllerTooltipOff != null)
			{
				this.ControllerTooltipOff(this, e);
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00063BF7 File Offset: 0x00061DF7
		public virtual void ResetTooltip()
		{
			this.InitialiseTips();
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00063C00 File Offset: 0x00061E00
		public virtual void UpdateText(VRTK_ControllerTooltips.TooltipButtons element, string newText)
		{
			switch (element)
			{
			case VRTK_ControllerTooltips.TooltipButtons.TriggerTooltip:
				this.triggerText = newText;
				break;
			case VRTK_ControllerTooltips.TooltipButtons.GripTooltip:
				this.gripText = newText;
				break;
			case VRTK_ControllerTooltips.TooltipButtons.TouchpadTooltip:
				this.touchpadText = newText;
				break;
			case VRTK_ControllerTooltips.TooltipButtons.ButtonOneTooltip:
				this.buttonOneText = newText;
				break;
			case VRTK_ControllerTooltips.TooltipButtons.ButtonTwoTooltip:
				this.buttonTwoText = newText;
				break;
			case VRTK_ControllerTooltips.TooltipButtons.StartMenuTooltip:
				this.startMenuText = newText;
				break;
			}
			this.ResetTooltip();
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00063C6C File Offset: 0x00061E6C
		public virtual void ToggleTips(bool state, VRTK_ControllerTooltips.TooltipButtons element = VRTK_ControllerTooltips.TooltipButtons.None)
		{
			if (element == VRTK_ControllerTooltips.TooltipButtons.None)
			{
				for (int i = 1; i < this.buttonTooltips.Length; i++)
				{
					if (this.buttonTooltips[i].displayText.Length > 0)
					{
						this.buttonTooltips[i].gameObject.SetActive(state);
					}
				}
			}
			else if (this.buttonTooltips[(int)element].displayText.Length > 0)
			{
				this.buttonTooltips[(int)element].gameObject.SetActive(state);
			}
			this.EmitEvent(state, element);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00063CE9 File Offset: 0x00061EE9
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
			this.InitButtonsArray();
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00063CFC File Offset: 0x00061EFC
		protected virtual void OnEnable()
		{
			this.controllerEvents = ((this.controllerEvents != null) ? this.controllerEvents : base.GetComponentInParent<VRTK_ControllerEvents>());
			this.InitButtonsArray();
			this.InitListeners();
			this.ResetTooltip();
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00063D34 File Offset: 0x00061F34
		protected virtual void OnDisable()
		{
			if (this.controllerEvents != null)
			{
				this.controllerEvents.ControllerEnabled -= this.DoControllerEnabled;
				this.controllerEvents.ControllerVisible -= this.DoControllerVisible;
				this.controllerEvents.ControllerHidden -= this.DoControllerInvisible;
			}
			if (this.headsetControllerAware != null)
			{
				this.headsetControllerAware.ControllerGlanceEnter -= this.DoGlanceEnterController;
				this.headsetControllerAware.ControllerGlanceExit -= this.DoGlanceExitController;
			}
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00063DE4 File Offset: 0x00061FE4
		protected virtual void EmitEvent(bool state, VRTK_ControllerTooltips.TooltipButtons element)
		{
			ControllerTooltipsEventArgs e;
			e.element = element;
			if (state)
			{
				this.OnControllerTooltipOn(e);
				return;
			}
			this.OnControllerTooltipOff(e);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00063E0C File Offset: 0x0006200C
		protected virtual void InitButtonsArray()
		{
			this.availableButtons = new VRTK_ControllerTooltips.TooltipButtons[]
			{
				VRTK_ControllerTooltips.TooltipButtons.None,
				VRTK_ControllerTooltips.TooltipButtons.TriggerTooltip,
				VRTK_ControllerTooltips.TooltipButtons.GripTooltip,
				VRTK_ControllerTooltips.TooltipButtons.TouchpadTooltip,
				VRTK_ControllerTooltips.TooltipButtons.ButtonOneTooltip,
				VRTK_ControllerTooltips.TooltipButtons.ButtonTwoTooltip,
				VRTK_ControllerTooltips.TooltipButtons.StartMenuTooltip
			};
			this.buttonTooltips = new VRTK_ObjectTooltip[this.availableButtons.Length];
			this.tooltipStates = new bool[this.availableButtons.Length];
			for (int i = 1; i < this.availableButtons.Length; i++)
			{
				this.buttonTooltips[i] = base.transform.Find(this.availableButtons[i].ToString()).GetComponent<VRTK_ObjectTooltip>();
			}
			this.retryInitCurrentTries = this.retryInitMaxTries;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00063EA4 File Offset: 0x000620A4
		protected virtual void InitListeners()
		{
			if (this.controllerEvents != null)
			{
				this.controllerEvents.ControllerEnabled += this.DoControllerEnabled;
				this.controllerEvents.ControllerVisible += this.DoControllerVisible;
				this.controllerEvents.ControllerHidden += this.DoControllerInvisible;
			}
			this.headsetControllerAware = ((this.headsetControllerAware != null) ? this.headsetControllerAware : Object.FindObjectOfType<VRTK_HeadsetControllerAware>());
			if (this.headsetControllerAware != null)
			{
				this.headsetControllerAware.ControllerGlanceEnter += this.DoGlanceEnterController;
				this.headsetControllerAware.ControllerGlanceExit += this.DoGlanceExitController;
				this.ToggleTips(false, VRTK_ControllerTooltips.TooltipButtons.None);
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00063F70 File Offset: 0x00062170
		protected virtual void DoControllerEnabled(object sender, ControllerInteractionEventArgs e)
		{
			if (this.controllerEvents != null)
			{
				GameObject actualController = VRTK_DeviceFinder.GetActualController(this.controllerEvents.gameObject);
				if (actualController != null && actualController.activeInHierarchy)
				{
					this.ResetTooltip();
				}
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00063FB4 File Offset: 0x000621B4
		protected virtual void DoControllerVisible(object sender, ControllerInteractionEventArgs e)
		{
			for (int i = 0; i < this.availableButtons.Length; i++)
			{
				this.ToggleTips(this.tooltipStates[i], this.availableButtons[i]);
			}
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00063FEC File Offset: 0x000621EC
		protected virtual void DoControllerInvisible(object sender, ControllerInteractionEventArgs e)
		{
			for (int i = 1; i < this.buttonTooltips.Length; i++)
			{
				this.tooltipStates[i] = this.buttonTooltips[i].gameObject.activeSelf;
			}
			this.ToggleTips(false, VRTK_ControllerTooltips.TooltipButtons.None);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0006402E File Offset: 0x0006222E
		protected virtual void DoGlanceEnterController(object sender, HeadsetControllerAwareEventArgs e)
		{
			if (this.controllerEvents != null && this.hideWhenNotInView && VRTK_ControllerReference.GetControllerReference(this.controllerEvents.gameObject) == e.controllerReference)
			{
				this.ToggleTips(true, VRTK_ControllerTooltips.TooltipButtons.None);
			}
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00064066 File Offset: 0x00062266
		protected virtual void DoGlanceExitController(object sender, HeadsetControllerAwareEventArgs e)
		{
			if (this.controllerEvents != null && this.hideWhenNotInView && VRTK_ControllerReference.GetControllerReference(this.controllerEvents.gameObject) == e.controllerReference)
			{
				this.ToggleTips(false, VRTK_ControllerTooltips.TooltipButtons.None);
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000640A0 File Offset: 0x000622A0
		protected virtual void InitialiseTips()
		{
			bool flag = false;
			foreach (VRTK_ObjectTooltip vrtk_ObjectTooltip in base.GetComponentsInChildren<VRTK_ObjectTooltip>(true))
			{
				string text = "";
				Transform transform = null;
				string a = vrtk_ObjectTooltip.name.Replace("Tooltip", "").ToLower();
				if (!(a == "trigger"))
				{
					if (!(a == "grip"))
					{
						if (!(a == "touchpad"))
						{
							if (!(a == "buttonone"))
							{
								if (!(a == "buttontwo"))
								{
									if (a == "startmenu")
									{
										text = this.startMenuText;
										transform = this.GetTransform(this.startMenu, SDK_BaseController.ControllerElements.StartMenu);
									}
								}
								else
								{
									text = this.buttonTwoText;
									transform = this.GetTransform(this.buttonTwo, SDK_BaseController.ControllerElements.ButtonTwo);
								}
							}
							else
							{
								text = this.buttonOneText;
								transform = this.GetTransform(this.buttonOne, SDK_BaseController.ControllerElements.ButtonOne);
							}
						}
						else
						{
							text = this.touchpadText;
							transform = this.GetTransform(this.touchpad, SDK_BaseController.ControllerElements.Touchpad);
						}
					}
					else
					{
						text = this.gripText;
						transform = this.GetTransform(this.grip, SDK_BaseController.ControllerElements.GripLeft);
					}
				}
				else
				{
					text = this.triggerText;
					transform = this.GetTransform(this.trigger, SDK_BaseController.ControllerElements.Trigger);
				}
				flag = (transform != null);
				vrtk_ObjectTooltip.displayText = text;
				vrtk_ObjectTooltip.drawLineTo = transform;
				vrtk_ObjectTooltip.containerColor = this.tipBackgroundColor;
				vrtk_ObjectTooltip.fontColor = this.tipTextColor;
				vrtk_ObjectTooltip.lineColor = this.tipLineColor;
				vrtk_ObjectTooltip.ResetTooltip();
				if (transform == null || text.Trim().Length == 0)
				{
					vrtk_ObjectTooltip.gameObject.SetActive(false);
				}
			}
			if (!flag && this.retryInitCurrentTries > 0)
			{
				this.retryInitCurrentTries--;
				base.Invoke("ResetTooltip", this.retryInitCounter);
				return;
			}
			if (this.headsetControllerAware == null || !this.hideWhenNotInView)
			{
				this.ToggleTips(true, VRTK_ControllerTooltips.TooltipButtons.None);
			}
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00064298 File Offset: 0x00062498
		protected virtual Transform GetTransform(Transform setTransform, SDK_BaseController.ControllerElements findElement)
		{
			Transform result = null;
			if (setTransform != null)
			{
				result = setTransform;
			}
			else if (this.controllerEvents != null)
			{
				GameObject modelAliasController = VRTK_DeviceFinder.GetModelAliasController(this.controllerEvents.gameObject);
				if (modelAliasController != null && modelAliasController.activeInHierarchy)
				{
					SDK_BaseController.ControllerHand controllerHand = VRTK_DeviceFinder.GetControllerHand(this.controllerEvents.gameObject);
					string controllerElementPath = VRTK_SDK_Bridge.GetControllerElementPath(findElement, controllerHand, true);
					result = ((controllerElementPath != null) ? modelAliasController.transform.Find(controllerElementPath) : null);
				}
			}
			return result;
		}

		// Token: 0x04000FD8 RID: 4056
		[Header("Button Text Settings")]
		[Tooltip("The text to display for the trigger button action.")]
		public string triggerText;

		// Token: 0x04000FD9 RID: 4057
		[Tooltip("The text to display for the grip button action.")]
		public string gripText;

		// Token: 0x04000FDA RID: 4058
		[Tooltip("The text to display for the touchpad action.")]
		public string touchpadText;

		// Token: 0x04000FDB RID: 4059
		[Tooltip("The text to display for button one action.")]
		public string buttonOneText;

		// Token: 0x04000FDC RID: 4060
		[Tooltip("The text to display for button two action.")]
		public string buttonTwoText;

		// Token: 0x04000FDD RID: 4061
		[Tooltip("The text to display for the start menu action.")]
		public string startMenuText;

		// Token: 0x04000FDE RID: 4062
		[Header("Tooltip Colour Settings")]
		[Tooltip("The colour to use for the tooltip background container.")]
		public Color tipBackgroundColor = Color.black;

		// Token: 0x04000FDF RID: 4063
		[Tooltip("The colour to use for the text within the tooltip.")]
		public Color tipTextColor = Color.white;

		// Token: 0x04000FE0 RID: 4064
		[Tooltip("The colour to use for the line between the tooltip and the relevant controller button.")]
		public Color tipLineColor = Color.black;

		// Token: 0x04000FE1 RID: 4065
		[Header("Button Transform Settings")]
		[Tooltip("The transform for the position of the trigger button on the controller.")]
		public Transform trigger;

		// Token: 0x04000FE2 RID: 4066
		[Tooltip("The transform for the position of the grip button on the controller.")]
		public Transform grip;

		// Token: 0x04000FE3 RID: 4067
		[Tooltip("The transform for the position of the touchpad button on the controller.")]
		public Transform touchpad;

		// Token: 0x04000FE4 RID: 4068
		[Tooltip("The transform for the position of button one on the controller.")]
		public Transform buttonOne;

		// Token: 0x04000FE5 RID: 4069
		[Tooltip("The transform for the position of button two on the controller.")]
		public Transform buttonTwo;

		// Token: 0x04000FE6 RID: 4070
		[Tooltip("The transform for the position of the start menu on the controller.")]
		public Transform startMenu;

		// Token: 0x04000FE7 RID: 4071
		[Header("Custom Settings")]
		[Tooltip("The controller to read the controller events from. If this is blank then it will attempt to get a controller events script from the same or parent GameObject.")]
		public VRTK_ControllerEvents controllerEvents;

		// Token: 0x04000FE8 RID: 4072
		[Tooltip("The headset controller aware script to use to see if the headset is looking at the controller. If this is blank then it will attempt to get a controller events script from the same or parent GameObject.")]
		public VRTK_HeadsetControllerAware headsetControllerAware;

		// Token: 0x04000FE9 RID: 4073
		[Tooltip("If this is checked then the tooltips will be hidden when the headset is not looking at the controller.")]
		public bool hideWhenNotInView = true;

		// Token: 0x04000FEA RID: 4074
		[Tooltip("The total number of initialisation attempts to make when waiting for the button transforms to initialise.")]
		public int retryInitMaxTries = 10;

		// Token: 0x04000FEB RID: 4075
		[Tooltip("The amount of seconds to wait before re-attempting to initialise the controller tooltips if the button transforms have not been initialised yet.")]
		public float retryInitCounter = 0.1f;

		// Token: 0x04000FEE RID: 4078
		protected VRTK_ControllerTooltips.TooltipButtons[] availableButtons;

		// Token: 0x04000FEF RID: 4079
		protected VRTK_ObjectTooltip[] buttonTooltips;

		// Token: 0x04000FF0 RID: 4080
		protected bool[] tooltipStates;

		// Token: 0x04000FF1 RID: 4081
		protected int retryInitCurrentTries;

		// Token: 0x020005B1 RID: 1457
		public enum TooltipButtons
		{
			// Token: 0x040026CE RID: 9934
			None,
			// Token: 0x040026CF RID: 9935
			TriggerTooltip,
			// Token: 0x040026D0 RID: 9936
			GripTooltip,
			// Token: 0x040026D1 RID: 9937
			TouchpadTooltip,
			// Token: 0x040026D2 RID: 9938
			ButtonOneTooltip,
			// Token: 0x040026D3 RID: 9939
			ButtonTwoTooltip,
			// Token: 0x040026D4 RID: 9940
			StartMenuTooltip
		}
	}
}
