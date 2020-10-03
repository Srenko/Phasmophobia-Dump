using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Highlighters;

namespace VRTK
{
	// Token: 0x0200029D RID: 669
	[AddComponentMenu("VRTK/Scripts/Interactions/VRTK_ControllerHighlighter")]
	public class VRTK_ControllerHighlighter : MonoBehaviour
	{
		// Token: 0x06001537 RID: 5431 RVA: 0x00074EC0 File Offset: 0x000730C0
		public virtual void ConfigureControllerPaths()
		{
			this.cachedElements = new Dictionary<string, Transform>();
			this.modelElementPaths.bodyModelPath = this.GetElementPath(this.modelElementPaths.bodyModelPath, SDK_BaseController.ControllerElements.Body);
			this.modelElementPaths.triggerModelPath = this.GetElementPath(this.modelElementPaths.triggerModelPath, SDK_BaseController.ControllerElements.Trigger);
			this.modelElementPaths.leftGripModelPath = this.GetElementPath(this.modelElementPaths.leftGripModelPath, SDK_BaseController.ControllerElements.GripLeft);
			this.modelElementPaths.rightGripModelPath = this.GetElementPath(this.modelElementPaths.rightGripModelPath, SDK_BaseController.ControllerElements.GripRight);
			this.modelElementPaths.touchpadModelPath = this.GetElementPath(this.modelElementPaths.touchpadModelPath, SDK_BaseController.ControllerElements.Touchpad);
			this.modelElementPaths.buttonOneModelPath = this.GetElementPath(this.modelElementPaths.buttonOneModelPath, SDK_BaseController.ControllerElements.ButtonOne);
			this.modelElementPaths.buttonTwoModelPath = this.GetElementPath(this.modelElementPaths.buttonTwoModelPath, SDK_BaseController.ControllerElements.ButtonTwo);
			this.modelElementPaths.systemMenuModelPath = this.GetElementPath(this.modelElementPaths.systemMenuModelPath, SDK_BaseController.ControllerElements.SystemMenu);
			this.modelElementPaths.startMenuModelPath = this.GetElementPath(this.modelElementPaths.systemMenuModelPath, SDK_BaseController.ControllerElements.StartMenu);
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x00074FE0 File Offset: 0x000731E0
		public virtual void PopulateHighlighters()
		{
			this.highlighterOptions = new Dictionary<string, object>();
			this.highlighterOptions.Add("resetMainTexture", true);
			VRTK_BaseHighlighter vrtk_BaseHighlighter = VRTK_BaseHighlighter.GetActiveHighlighter(this.controllerAlias);
			if (vrtk_BaseHighlighter == null)
			{
				vrtk_BaseHighlighter = this.controllerAlias.AddComponent<VRTK_MaterialColorSwapHighlighter>();
			}
			SDK_BaseController.ControllerHand controllerHand = VRTK_DeviceFinder.GetControllerHand(this.controllerAlias);
			vrtk_BaseHighlighter.Initialise(null, this.highlighterOptions);
			this.AddHighlighterToElement(this.GetElementTransform(VRTK_SDK_Bridge.GetControllerElementPath(SDK_BaseController.ControllerElements.ButtonOne, controllerHand, false)), vrtk_BaseHighlighter, this.elementHighlighterOverrides.buttonOne);
			this.AddHighlighterToElement(this.GetElementTransform(VRTK_SDK_Bridge.GetControllerElementPath(SDK_BaseController.ControllerElements.ButtonTwo, controllerHand, false)), vrtk_BaseHighlighter, this.elementHighlighterOverrides.buttonTwo);
			this.AddHighlighterToElement(this.GetElementTransform(VRTK_SDK_Bridge.GetControllerElementPath(SDK_BaseController.ControllerElements.Body, controllerHand, false)), vrtk_BaseHighlighter, this.elementHighlighterOverrides.body);
			this.AddHighlighterToElement(this.GetElementTransform(VRTK_SDK_Bridge.GetControllerElementPath(SDK_BaseController.ControllerElements.GripLeft, controllerHand, false)), vrtk_BaseHighlighter, this.elementHighlighterOverrides.gripLeft);
			this.AddHighlighterToElement(this.GetElementTransform(VRTK_SDK_Bridge.GetControllerElementPath(SDK_BaseController.ControllerElements.GripRight, controllerHand, false)), vrtk_BaseHighlighter, this.elementHighlighterOverrides.gripRight);
			this.AddHighlighterToElement(this.GetElementTransform(VRTK_SDK_Bridge.GetControllerElementPath(SDK_BaseController.ControllerElements.StartMenu, controllerHand, false)), vrtk_BaseHighlighter, this.elementHighlighterOverrides.startMenu);
			this.AddHighlighterToElement(this.GetElementTransform(VRTK_SDK_Bridge.GetControllerElementPath(SDK_BaseController.ControllerElements.SystemMenu, controllerHand, false)), vrtk_BaseHighlighter, this.elementHighlighterOverrides.systemMenu);
			this.AddHighlighterToElement(this.GetElementTransform(VRTK_SDK_Bridge.GetControllerElementPath(SDK_BaseController.ControllerElements.Touchpad, controllerHand, false)), vrtk_BaseHighlighter, this.elementHighlighterOverrides.touchpad);
			this.AddHighlighterToElement(this.GetElementTransform(VRTK_SDK_Bridge.GetControllerElementPath(SDK_BaseController.ControllerElements.Trigger, controllerHand, false)), vrtk_BaseHighlighter, this.elementHighlighterOverrides.trigger);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00075174 File Offset: 0x00073374
		public virtual void HighlightController(Color color, float fadeDuration = 0f)
		{
			this.HighlightElement(SDK_BaseController.ControllerElements.ButtonOne, color, fadeDuration);
			this.HighlightElement(SDK_BaseController.ControllerElements.ButtonTwo, color, fadeDuration);
			this.HighlightElement(SDK_BaseController.ControllerElements.Body, color, fadeDuration);
			this.HighlightElement(SDK_BaseController.ControllerElements.GripLeft, color, fadeDuration);
			this.HighlightElement(SDK_BaseController.ControllerElements.GripRight, color, fadeDuration);
			this.HighlightElement(SDK_BaseController.ControllerElements.StartMenu, color, fadeDuration);
			this.HighlightElement(SDK_BaseController.ControllerElements.SystemMenu, color, fadeDuration);
			this.HighlightElement(SDK_BaseController.ControllerElements.Touchpad, color, fadeDuration);
			this.HighlightElement(SDK_BaseController.ControllerElements.Trigger, color, fadeDuration);
			this.controllerHighlighted = true;
			this.highlightController = color;
			this.lastHighlightController = color;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x000751E8 File Offset: 0x000733E8
		public virtual void UnhighlightController()
		{
			this.controllerHighlighted = false;
			this.highlightController = Color.clear;
			this.lastHighlightController = Color.clear;
			this.UnhighlightElement(SDK_BaseController.ControllerElements.ButtonOne);
			this.UnhighlightElement(SDK_BaseController.ControllerElements.ButtonTwo);
			this.UnhighlightElement(SDK_BaseController.ControllerElements.Body);
			this.UnhighlightElement(SDK_BaseController.ControllerElements.GripLeft);
			this.UnhighlightElement(SDK_BaseController.ControllerElements.GripRight);
			this.UnhighlightElement(SDK_BaseController.ControllerElements.StartMenu);
			this.UnhighlightElement(SDK_BaseController.ControllerElements.SystemMenu);
			this.UnhighlightElement(SDK_BaseController.ControllerElements.Touchpad);
			this.UnhighlightElement(SDK_BaseController.ControllerElements.Trigger);
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00075254 File Offset: 0x00073454
		public virtual void HighlightElement(SDK_BaseController.ControllerElements elementType, Color color, float fadeDuration = 0f)
		{
			Transform elementTransform = this.GetElementTransform(this.GetPathForControllerElement(elementType));
			if (elementTransform != null)
			{
				VRTK_ObjectAppearance.HighlightObject(elementTransform.gameObject, new Color?(color), fadeDuration);
				this.SetColourParameter(elementType, color);
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x00075294 File Offset: 0x00073494
		public virtual void UnhighlightElement(SDK_BaseController.ControllerElements elementType)
		{
			if (!this.controllerHighlighted)
			{
				Transform elementTransform = this.GetElementTransform(this.GetPathForControllerElement(elementType));
				if (elementTransform != null)
				{
					VRTK_ObjectAppearance.UnhighlightObject(elementTransform.gameObject);
					this.SetColourParameter(elementType, Color.clear);
					return;
				}
			}
			else if (this.highlightController != Color.clear && this.GetColourParameter(elementType) != this.highlightController)
			{
				this.HighlightElement(elementType, this.highlightController, 0f);
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00075310 File Offset: 0x00073510
		protected virtual void Awake()
		{
			this.originalControllerAlias = this.controllerAlias;
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0007532C File Offset: 0x0007352C
		protected virtual void OnEnable()
		{
			this.controllerAlias = this.originalControllerAlias;
			if (this.controllerAlias == null)
			{
				VRTK_TrackedController componentInParent = base.GetComponentInParent<VRTK_TrackedController>();
				this.controllerAlias = ((componentInParent != null) ? componentInParent.gameObject : null);
			}
			if (this.controllerAlias == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_NOT_INJECTED, new object[]
				{
					"VRTK_ControllerHighlighter",
					"Controller Alias GameObject",
					"controllerAlias",
					"the same"
				}));
				return;
			}
			this.ConfigureControllerPaths();
			this.modelContainer = ((this.modelContainer != null) ? this.modelContainer : VRTK_DeviceFinder.GetModelAliasController(this.controllerAlias));
			this.initHighlightersRoutine = base.StartCoroutine(this.WaitForModel());
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x000753F1 File Offset: 0x000735F1
		protected virtual void OnDisable()
		{
			if (this.initHighlightersRoutine != null)
			{
				base.StopCoroutine(this.initHighlightersRoutine);
			}
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00075408 File Offset: 0x00073608
		protected virtual void Update()
		{
			this.ToggleControllerState();
			this.ToggleHighlightState(this.highlightBody, ref this.lastHighlightBody, this.bodyElements);
			this.ToggleHighlightState(this.highlightTrigger, ref this.lastHighlightTrigger, this.triggerElements);
			this.ToggleHighlightState(this.highlightGrip, ref this.lastHighlightGrip, this.gripElements);
			this.ToggleHighlightState(this.highlightTouchpad, ref this.lastHighlightTouchpad, this.touchpadElements);
			this.ToggleHighlightState(this.highlightButtonOne, ref this.lastHighlightButtonOne, this.buttonOneElements);
			this.ToggleHighlightState(this.highlightButtonTwo, ref this.lastHighlightButtonTwo, this.buttonTwoElements);
			this.ToggleHighlightState(this.highlightSystemMenu, ref this.lastHighlightSystemMenu, this.systemMenuElements);
			this.ToggleHighlightState(this.highlightStartMenu, ref this.lastHighlightStartMenu, this.startMenuElements);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x000754DC File Offset: 0x000736DC
		protected virtual void ResetLastHighlights()
		{
			this.lastHighlightController = Color.clear;
			this.lastHighlightBody = Color.clear;
			this.lastHighlightTrigger = Color.clear;
			this.lastHighlightGrip = Color.clear;
			this.lastHighlightTouchpad = Color.clear;
			this.lastHighlightButtonOne = Color.clear;
			this.lastHighlightButtonTwo = Color.clear;
			this.lastHighlightSystemMenu = Color.clear;
			this.lastHighlightStartMenu = Color.clear;
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0007554C File Offset: 0x0007374C
		protected virtual void SetColourParameter(SDK_BaseController.ControllerElements element, Color color)
		{
			color = ((color == Color.clear && this.highlightController != Color.clear) ? this.highlightController : color);
			switch (element)
			{
			case SDK_BaseController.ControllerElements.Trigger:
				this.highlightTrigger = color;
				this.lastHighlightTrigger = color;
				return;
			case SDK_BaseController.ControllerElements.GripLeft:
			case SDK_BaseController.ControllerElements.GripRight:
				this.highlightGrip = color;
				this.lastHighlightGrip = color;
				return;
			case SDK_BaseController.ControllerElements.Touchpad:
				this.highlightTouchpad = color;
				this.lastHighlightTouchpad = color;
				return;
			case SDK_BaseController.ControllerElements.ButtonOne:
				this.highlightButtonOne = color;
				this.lastHighlightButtonOne = color;
				return;
			case SDK_BaseController.ControllerElements.ButtonTwo:
				this.highlightButtonTwo = color;
				this.lastHighlightButtonTwo = color;
				return;
			case SDK_BaseController.ControllerElements.SystemMenu:
				this.highlightSystemMenu = color;
				this.lastHighlightSystemMenu = color;
				return;
			case SDK_BaseController.ControllerElements.Body:
				this.highlightBody = color;
				this.lastHighlightBody = color;
				return;
			case SDK_BaseController.ControllerElements.StartMenu:
				this.highlightStartMenu = color;
				this.lastHighlightStartMenu = color;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00075628 File Offset: 0x00073828
		protected virtual Color GetColourParameter(SDK_BaseController.ControllerElements element)
		{
			switch (element)
			{
			case SDK_BaseController.ControllerElements.Trigger:
				return this.highlightTrigger;
			case SDK_BaseController.ControllerElements.GripLeft:
			case SDK_BaseController.ControllerElements.GripRight:
				return this.highlightGrip;
			case SDK_BaseController.ControllerElements.Touchpad:
				return this.highlightTouchpad;
			case SDK_BaseController.ControllerElements.ButtonOne:
				return this.highlightButtonOne;
			case SDK_BaseController.ControllerElements.ButtonTwo:
				return this.highlightButtonTwo;
			case SDK_BaseController.ControllerElements.SystemMenu:
				return this.highlightSystemMenu;
			case SDK_BaseController.ControllerElements.Body:
				return this.highlightBody;
			case SDK_BaseController.ControllerElements.StartMenu:
				return this.highlightStartMenu;
			default:
				return Color.clear;
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x000756A0 File Offset: 0x000738A0
		protected virtual void ToggleControllerState()
		{
			if (this.highlightController != this.lastHighlightController)
			{
				if (this.highlightController == Color.clear)
				{
					this.UnhighlightController();
					return;
				}
				this.HighlightController(this.highlightController, this.transitionDuration);
			}
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x000756E0 File Offset: 0x000738E0
		protected virtual void ToggleHighlightState(Color currentColor, ref Color lastColorState, SDK_BaseController.ControllerElements[] elements)
		{
			if (currentColor != lastColorState)
			{
				if (currentColor == Color.clear)
				{
					for (int i = 0; i < elements.Length; i++)
					{
						this.UnhighlightElement(elements[i]);
					}
				}
				else
				{
					for (int j = 0; j < elements.Length; j++)
					{
						this.HighlightElement(elements[j], currentColor, this.transitionDuration);
					}
				}
				lastColorState = currentColor;
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00075746 File Offset: 0x00073946
		protected virtual IEnumerator WaitForModel()
		{
			while (this.GetElementTransform(this.modelElementPaths.bodyModelPath) == null)
			{
				yield return null;
			}
			this.PopulateHighlighters();
			this.ResetLastHighlights();
			yield break;
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00075758 File Offset: 0x00073958
		protected virtual void AddHighlighterToElement(Transform element, VRTK_BaseHighlighter parentHighlighter, VRTK_BaseHighlighter overrideHighlighter)
		{
			if (element != null)
			{
				((VRTK_BaseHighlighter)VRTK_SharedMethods.CloneComponent((overrideHighlighter != null) ? overrideHighlighter : parentHighlighter, element.gameObject, false)).Initialise(null, this.highlighterOptions);
			}
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x000757A0 File Offset: 0x000739A0
		protected virtual string GetElementPath(string currentPath, SDK_BaseController.ControllerElements elementType)
		{
			SDK_BaseController.ControllerHand controllerHand = VRTK_DeviceFinder.GetControllerHand(this.controllerAlias);
			string controllerElementPath = VRTK_SDK_Bridge.GetControllerElementPath(elementType, controllerHand, false);
			if (!(currentPath.Trim() == "") || controllerElementPath == null)
			{
				return currentPath.Trim();
			}
			return controllerElementPath;
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000757E0 File Offset: 0x000739E0
		protected virtual string GetPathForControllerElement(SDK_BaseController.ControllerElements controllerElement)
		{
			switch (controllerElement)
			{
			case SDK_BaseController.ControllerElements.Trigger:
				return this.modelElementPaths.triggerModelPath;
			case SDK_BaseController.ControllerElements.GripLeft:
				return this.modelElementPaths.leftGripModelPath;
			case SDK_BaseController.ControllerElements.GripRight:
				return this.modelElementPaths.rightGripModelPath;
			case SDK_BaseController.ControllerElements.Touchpad:
				return this.modelElementPaths.touchpadModelPath;
			case SDK_BaseController.ControllerElements.ButtonOne:
				return this.modelElementPaths.buttonOneModelPath;
			case SDK_BaseController.ControllerElements.ButtonTwo:
				return this.modelElementPaths.buttonTwoModelPath;
			case SDK_BaseController.ControllerElements.SystemMenu:
				return this.modelElementPaths.systemMenuModelPath;
			case SDK_BaseController.ControllerElements.Body:
				return this.modelElementPaths.bodyModelPath;
			case SDK_BaseController.ControllerElements.StartMenu:
				return this.modelElementPaths.startMenuModelPath;
			default:
				return "";
			}
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0007588C File Offset: 0x00073A8C
		protected virtual Transform GetElementTransform(string path)
		{
			if (this.cachedElements == null || path == null)
			{
				return null;
			}
			if (!this.cachedElements.ContainsKey(path) || this.cachedElements[path] == null)
			{
				if (!this.modelContainer)
				{
					VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.SDK_OBJECT_NOT_FOUND, new object[]
					{
						"Controller Model",
						"Controller SDK"
					}));
					return null;
				}
				this.cachedElements[path] = this.modelContainer.transform.Find(path);
			}
			return this.cachedElements[path];
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00075924 File Offset: 0x00073B24
		protected virtual void ToggleHighlightAlias(bool state, string transformPath, Color? highlight, float duration = 0f)
		{
			Transform elementTransform = this.GetElementTransform(transformPath);
			if (elementTransform)
			{
				if (state)
				{
					VRTK_ObjectAppearance.HighlightObject(elementTransform.gameObject, (highlight != null) ? highlight : new Color?(Color.white), duration);
					return;
				}
				VRTK_ObjectAppearance.UnhighlightObject(elementTransform.gameObject);
			}
		}

		// Token: 0x04001209 RID: 4617
		[Header("General Settings")]
		[Tooltip("The amount of time to take to transition to the set highlight colour.")]
		public float transitionDuration;

		// Token: 0x0400120A RID: 4618
		[Header("Controller Highlight")]
		[Tooltip("The colour to set the entire controller highlight colour to.")]
		public Color highlightController = Color.clear;

		// Token: 0x0400120B RID: 4619
		[Header("Element Highlights")]
		[Tooltip("The colour to set the body highlight colour to.")]
		public Color highlightBody = Color.clear;

		// Token: 0x0400120C RID: 4620
		[Tooltip("The colour to set the trigger highlight colour to.")]
		public Color highlightTrigger = Color.clear;

		// Token: 0x0400120D RID: 4621
		[Tooltip("The colour to set the grip highlight colour to.")]
		public Color highlightGrip = Color.clear;

		// Token: 0x0400120E RID: 4622
		[Tooltip("The colour to set the touchpad highlight colour to.")]
		public Color highlightTouchpad = Color.clear;

		// Token: 0x0400120F RID: 4623
		[Tooltip("The colour to set the button one highlight colour to.")]
		public Color highlightButtonOne = Color.clear;

		// Token: 0x04001210 RID: 4624
		[Tooltip("The colour to set the button two highlight colour to.")]
		public Color highlightButtonTwo = Color.clear;

		// Token: 0x04001211 RID: 4625
		[Tooltip("The colour to set the system menu highlight colour to.")]
		public Color highlightSystemMenu = Color.clear;

		// Token: 0x04001212 RID: 4626
		[Tooltip("The colour to set the start menu highlight colour to.")]
		public Color highlightStartMenu = Color.clear;

		// Token: 0x04001213 RID: 4627
		[Header("Custom Settings")]
		[Tooltip("A collection of strings that determine the path to the controller model sub elements for identifying the model parts at runtime. If the paths are left empty they will default to the model element paths of the selected SDK Bridge.")]
		public VRTK_ControllerModelElementPaths modelElementPaths = new VRTK_ControllerModelElementPaths();

		// Token: 0x04001214 RID: 4628
		[Tooltip("A collection of highlighter overrides for each controller model sub element. If no highlighter override is given then highlighter on the Controller game object is used.")]
		public VRTK_ControllerElementHighlighters elementHighlighterOverrides = new VRTK_ControllerElementHighlighters();

		// Token: 0x04001215 RID: 4629
		[Tooltip("An optional GameObject to specify which controller to apply the script methods to. If this is left blank then this script is required to be placed on a Controller Alias GameObject.")]
		public GameObject controllerAlias;

		// Token: 0x04001216 RID: 4630
		[Tooltip("An optional GameObject to specifiy where the controller models are. If this is left blank then the Model Alias object will be used.")]
		public GameObject modelContainer;

		// Token: 0x04001217 RID: 4631
		protected bool controllerHighlighted;

		// Token: 0x04001218 RID: 4632
		protected Dictionary<string, Transform> cachedElements;

		// Token: 0x04001219 RID: 4633
		protected Dictionary<string, object> highlighterOptions;

		// Token: 0x0400121A RID: 4634
		protected Coroutine initHighlightersRoutine;

		// Token: 0x0400121B RID: 4635
		protected GameObject originalControllerAlias;

		// Token: 0x0400121C RID: 4636
		protected Color lastHighlightController;

		// Token: 0x0400121D RID: 4637
		protected Color lastHighlightBody;

		// Token: 0x0400121E RID: 4638
		protected Color lastHighlightTrigger;

		// Token: 0x0400121F RID: 4639
		protected Color lastHighlightGrip;

		// Token: 0x04001220 RID: 4640
		protected Color lastHighlightTouchpad;

		// Token: 0x04001221 RID: 4641
		protected Color lastHighlightButtonOne;

		// Token: 0x04001222 RID: 4642
		protected Color lastHighlightButtonTwo;

		// Token: 0x04001223 RID: 4643
		protected Color lastHighlightSystemMenu;

		// Token: 0x04001224 RID: 4644
		protected Color lastHighlightStartMenu;

		// Token: 0x04001225 RID: 4645
		protected SDK_BaseController.ControllerElements[] bodyElements = new SDK_BaseController.ControllerElements[]
		{
			SDK_BaseController.ControllerElements.Body
		};

		// Token: 0x04001226 RID: 4646
		protected SDK_BaseController.ControllerElements[] triggerElements = new SDK_BaseController.ControllerElements[]
		{
			SDK_BaseController.ControllerElements.Trigger
		};

		// Token: 0x04001227 RID: 4647
		protected SDK_BaseController.ControllerElements[] gripElements = new SDK_BaseController.ControllerElements[]
		{
			SDK_BaseController.ControllerElements.GripLeft,
			SDK_BaseController.ControllerElements.GripRight
		};

		// Token: 0x04001228 RID: 4648
		protected SDK_BaseController.ControllerElements[] touchpadElements = new SDK_BaseController.ControllerElements[]
		{
			SDK_BaseController.ControllerElements.Touchpad
		};

		// Token: 0x04001229 RID: 4649
		protected SDK_BaseController.ControllerElements[] buttonOneElements = new SDK_BaseController.ControllerElements[]
		{
			SDK_BaseController.ControllerElements.ButtonOne
		};

		// Token: 0x0400122A RID: 4650
		protected SDK_BaseController.ControllerElements[] buttonTwoElements = new SDK_BaseController.ControllerElements[]
		{
			SDK_BaseController.ControllerElements.ButtonTwo
		};

		// Token: 0x0400122B RID: 4651
		protected SDK_BaseController.ControllerElements[] systemMenuElements = new SDK_BaseController.ControllerElements[]
		{
			SDK_BaseController.ControllerElements.SystemMenu
		};

		// Token: 0x0400122C RID: 4652
		protected SDK_BaseController.ControllerElements[] startMenuElements = new SDK_BaseController.ControllerElements[]
		{
			SDK_BaseController.ControllerElements.StartMenu
		};
	}
}
