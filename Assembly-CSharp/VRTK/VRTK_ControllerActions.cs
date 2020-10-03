using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000298 RID: 664
	[Obsolete("`VRTK_ControllerActions` has been replaced with a combination of `VRTK_ControllerHighlighter` and calls to `VRTK_SharedMethods`. This script will be removed in a future version of VRTK.")]
	public class VRTK_ControllerActions : MonoBehaviour
	{
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600145A RID: 5210 RVA: 0x00071AA4 File Offset: 0x0006FCA4
		// (remove) Token: 0x0600145B RID: 5211 RVA: 0x00071ADC File Offset: 0x0006FCDC
		[Obsolete("`VRTK_ControllerActions.ControllerModelVisible` has been replaced with `VRTK_ControllerEvents.ControllerVisible`. This method will be removed in a future version of VRTK.")]
		public event ControllerActionsEventHandler ControllerModelVisible;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600145C RID: 5212 RVA: 0x00071B14 File Offset: 0x0006FD14
		// (remove) Token: 0x0600145D RID: 5213 RVA: 0x00071B4C File Offset: 0x0006FD4C
		[Obsolete("`VRTK_ControllerActions.ControllerModelInvisible` has been replaced with `VRTK_ControllerEvents.ControllerHidden`. This method will be removed in a future version of VRTK.")]
		public event ControllerActionsEventHandler ControllerModelInvisible;

		// Token: 0x0600145E RID: 5214 RVA: 0x00071B81 File Offset: 0x0006FD81
		[Obsolete("`VRTK_ControllerActions.OnControllerModelVisible(e)` has been replaced with `VRTK_ControllerEvents.OnControllerVisible(e)`. This method will be removed in a future version of VRTK.")]
		public virtual void OnControllerModelVisible(ControllerActionsEventArgs e)
		{
			if (this.ControllerModelVisible != null)
			{
				this.ControllerModelVisible(this, e);
			}
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00071B98 File Offset: 0x0006FD98
		[Obsolete("`VRTK_ControllerActions.OnControllerModelInvisible(e)` has been replaced with `VRTK_ControllerEvents.OnControllerHidden(e)`. This method will be removed in a future version of VRTK.")]
		public virtual void OnControllerModelInvisible(ControllerActionsEventArgs e)
		{
			if (this.ControllerModelInvisible != null)
			{
				this.ControllerModelInvisible(this, e);
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00071BAF File Offset: 0x0006FDAF
		[Obsolete("`VRTK_ControllerActions.IsControllerVisible()` has been replaced with `VRTK_ControllerEvents.controllerVisible`. This method will be removed in a future version of VRTK.")]
		public virtual bool IsControllerVisible()
		{
			return this.controllerVisible;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00071BB8 File Offset: 0x0006FDB8
		[Obsolete("`VRTK_ControllerActions.ToggleControllerModel(state, grabbedChildObject)` has been replaced with `VRTK_SharedMethods.ToggleRenderer(model, state, ignoredModel)`. This method will be removed in a future version of VRTK.")]
		public virtual void ToggleControllerModel(bool state, GameObject grabbedChildObject)
		{
			if (!base.enabled)
			{
				return;
			}
			VRTK_SharedMethods.ToggleRenderer(state, this.modelContainer, grabbedChildObject);
			this.controllerVisible = state;
			uint controllerIndex = VRTK_DeviceFinder.GetControllerIndex(base.gameObject);
			if (state)
			{
				this.OnControllerModelVisible(this.SetActionEvent(controllerIndex));
				return;
			}
			this.OnControllerModelInvisible(this.SetActionEvent(controllerIndex));
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00071C0C File Offset: 0x0006FE0C
		[Obsolete("`VRTK_ControllerActions.SetControllerOpacity(alpha)` has been replaced with `VRTK_SharedMethods.SetOpacity(model, alpha)`. This method will be removed in a future version of VRTK.")]
		public virtual void SetControllerOpacity(float alpha)
		{
			if (!base.enabled)
			{
				return;
			}
			VRTK_SharedMethods.SetOpacity(this.modelContainer, alpha, 0f);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00071C28 File Offset: 0x0006FE28
		[Obsolete("`VRTK_ControllerActions.HighlightControllerElement(element, highlight, fadeDuration)` has been replaced with `VRTK_SharedMethods.HighlightObject(model, highlight, fadeDuration)`. This method will be removed in a future version of VRTK.")]
		public virtual void HighlightControllerElement(GameObject element, Color? highlight, float fadeDuration = 0f)
		{
			if (!base.enabled)
			{
				return;
			}
			VRTK_SharedMethods.HighlightObject(element, highlight, fadeDuration);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00071C3B File Offset: 0x0006FE3B
		[Obsolete("`VRTK_ControllerActions.UnhighlightControllerElement(element)` has been replaced with `VRTK_SharedMethods.UnhighlightObject(model)`. This method will be removed in a future version of VRTK.")]
		public virtual void UnhighlightControllerElement(GameObject element)
		{
			if (!base.enabled)
			{
				return;
			}
			VRTK_SharedMethods.UnhighlightObject(element);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00071C4C File Offset: 0x0006FE4C
		[Obsolete("`VRTK_ControllerActions.ToggleHighlightControllerElement(state, element, highlight, duration)` has been replaced with `VRTK_ControllerHighlighter.HighlightElement(elementType, color, fadeDuration)/UnhighlightElement(elementType)`. This method will be removed in a future version of VRTK.")]
		public virtual void ToggleHighlightControllerElement(bool state, GameObject element, Color? highlight = null, float duration = 0f)
		{
			if (element)
			{
				if (state)
				{
					VRTK_SharedMethods.HighlightObject(element, (highlight != null) ? highlight : new Color?(Color.white), duration);
					return;
				}
				VRTK_SharedMethods.UnhighlightObject(element);
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00071C7E File Offset: 0x0006FE7E
		[Obsolete("`VRTK_ControllerActions.ToggleHighlightTrigger(state, highlight, duration)` has been replaced with `VRTK_ControllerHighlighter.HighlightElement(elementType, color, fadeDuration)`. This method will be removed in a future version of VRTK.")]
		public virtual void ToggleHighlightTrigger(bool state, Color? highlight = null, float duration = 0f)
		{
			this.ToggleElementHighlight(state, SDK_BaseController.ControllerElements.Trigger, highlight, duration);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00071C8A File Offset: 0x0006FE8A
		[Obsolete("`VRTK_ControllerActions.ToggleHighlightGrip(state, highlight, duration)` has been replaced with `VRTK_ControllerHighlighter.HighlightElement(elementType, color, fadeDuration)`. This method will be removed in a future version of VRTK.")]
		public virtual void ToggleHighlightGrip(bool state, Color? highlight = null, float duration = 0f)
		{
			this.ToggleElementHighlight(state, SDK_BaseController.ControllerElements.GripLeft, highlight, duration);
			this.ToggleElementHighlight(state, SDK_BaseController.ControllerElements.GripRight, highlight, duration);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00071CA0 File Offset: 0x0006FEA0
		[Obsolete("`VRTK_ControllerActions.ToggleHighlightTouchpad(state, highlight, duration)` has been replaced with `VRTK_ControllerHighlighter.HighlightElement(elementType, color, fadeDuration)`. This method will be removed in a future version of VRTK.")]
		public virtual void ToggleHighlightTouchpad(bool state, Color? highlight = null, float duration = 0f)
		{
			this.ToggleElementHighlight(state, SDK_BaseController.ControllerElements.Touchpad, highlight, duration);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00071CAC File Offset: 0x0006FEAC
		[Obsolete("`VRTK_ControllerActions.ToggleHighlightButtonOne(state, highlight, duration)` has been replaced with `VRTK_ControllerHighlighter.HighlightElement(elementType, color, fadeDuration)`. This method will be removed in a future version of VRTK.")]
		public virtual void ToggleHighlightButtonOne(bool state, Color? highlight = null, float duration = 0f)
		{
			this.ToggleElementHighlight(state, SDK_BaseController.ControllerElements.ButtonOne, highlight, duration);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00071CB8 File Offset: 0x0006FEB8
		[Obsolete("`VRTK_ControllerActions.ToggleHighlightButtonTwo(state, highlight, duration)` has been replaced with `VRTK_ControllerHighlighter.HighlightElement(elementType, color, fadeDuration)`. This method will be removed in a future version of VRTK.")]
		public virtual void ToggleHighlightButtonTwo(bool state, Color? highlight = null, float duration = 0f)
		{
			this.ToggleElementHighlight(state, SDK_BaseController.ControllerElements.ButtonTwo, highlight, duration);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00071CC4 File Offset: 0x0006FEC4
		[Obsolete("`VRTK_ControllerActions.ToggleHighlightStartMenu(state, highlight, duration)` has been replaced with `VRTK_ControllerHighlighter.HighlightElement(elementType, color, fadeDuration)`. This method will be removed in a future version of VRTK.")]
		public virtual void ToggleHighlightStartMenu(bool state, Color? highlight = null, float duration = 0f)
		{
			this.ToggleElementHighlight(state, SDK_BaseController.ControllerElements.StartMenu, highlight, duration);
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00071CD1 File Offset: 0x0006FED1
		[Obsolete("`VRTK_ControllerActions.ToggleHighlighBody(state, highlight, duration)` has been replaced with `VRTK_ControllerHighlighter.HighlightElement(elementType, color, fadeDuration)`. This method will be removed in a future version of VRTK.")]
		public virtual void ToggleHighlighBody(bool state, Color? highlight = null, float duration = 0f)
		{
			this.ToggleElementHighlight(state, SDK_BaseController.ControllerElements.Body, highlight, duration);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x00071CE0 File Offset: 0x0006FEE0
		[Obsolete("`VRTK_ControllerActions.ToggleHighlightController(state, highlight, duration)` has been replaced with `VRTK_ControllerHighlighter.HighlightController(color, fadeDuration)`. This method will be removed in a future version of VRTK.")]
		public virtual void ToggleHighlightController(bool state, Color? highlight = null, float duration = 0f)
		{
			if (this.controllerHighlighter != null)
			{
				if (state)
				{
					this.controllerHighlighter.HighlightController(((highlight == null) ? new Color?(Color.white) : highlight).Value, duration);
					return;
				}
				this.controllerHighlighter.UnhighlightController();
			}
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00071D34 File Offset: 0x0006FF34
		[Obsolete("`VRTK_ControllerActions.TriggerHapticPulse(strength)` has been replaced with `VRTK_SharedMethods.TriggerHapticPulse(index, strength)`. This method will be removed in a future version of VRTK.")]
		public virtual void TriggerHapticPulse(float strength)
		{
			VRTK_SharedMethods.TriggerHapticPulse(VRTK_DeviceFinder.GetControllerIndex(base.gameObject), strength);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00071D47 File Offset: 0x0006FF47
		[Obsolete("`VRTK_ControllerActions.TriggerHapticPulse(strength, duration, pulseInterval)` has been replaced with `VRTK_SharedMethods.TriggerHapticPulse(index, strength, duration, pulseInterval)`. This method will be removed in a future version of VRTK.")]
		public virtual void TriggerHapticPulse(float strength, float duration, float pulseInterval)
		{
			VRTK_SharedMethods.TriggerHapticPulse(VRTK_DeviceFinder.GetControllerIndex(base.gameObject), strength, duration, pulseInterval);
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00071D5C File Offset: 0x0006FF5C
		[Obsolete("`VRTK_ControllerActions.InitaliseHighlighters()` has been replaced with `VRTK_ControllerHighlighter.PopulateHighlighters()`. This method will be removed in a future version of VRTK.")]
		public virtual void InitaliseHighlighters()
		{
			this.controllerHighlighter.PopulateHighlighters();
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00071D6C File Offset: 0x0006FF6C
		protected virtual void OnEnable()
		{
			this.modelContainer = VRTK_DeviceFinder.GetModelAliasController(base.gameObject);
			this.generateControllerHighlighter = false;
			VRTK_ControllerHighlighter component = base.GetComponent<VRTK_ControllerHighlighter>();
			if (component == null)
			{
				this.generateControllerHighlighter = true;
				this.controllerHighlighter = base.gameObject.AddComponent<VRTK_ControllerHighlighter>();
				this.controllerHighlighter.modelElementPaths = this.modelElementPaths;
				this.controllerHighlighter.elementHighlighterOverrides = this.elementHighlighterOverrides;
				this.controllerHighlighter.ConfigureControllerPaths();
				return;
			}
			this.controllerHighlighter = component;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x00071DEE File Offset: 0x0006FFEE
		protected virtual void OnDisable()
		{
			if (this.generateControllerHighlighter)
			{
				Object.Destroy(this.controllerHighlighter);
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00071E04 File Offset: 0x00070004
		protected virtual void ToggleElementHighlight(bool state, SDK_BaseController.ControllerElements elementType, Color? color, float fadeDuration = 0f)
		{
			if (this.controllerHighlighter != null)
			{
				if (state)
				{
					this.controllerHighlighter.HighlightElement(elementType, ((color == null) ? new Color?(Color.white) : color).Value, fadeDuration);
					return;
				}
				this.controllerHighlighter.UnhighlightElement(elementType);
			}
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00071E5C File Offset: 0x0007005C
		protected virtual ControllerActionsEventArgs SetActionEvent(uint index)
		{
			ControllerActionsEventArgs result;
			result.controllerIndex = index;
			return result;
		}

		// Token: 0x0400119F RID: 4511
		[Tooltip("A collection of strings that determine the path to the controller model sub elements for identifying the model parts at runtime. If the paths are left empty they will default to the model element paths of the selected SDK Bridge.\n\n* The available model sub elements are:\n\n * `Body Model Path`: The overall shape of the controller.\n * `Trigger Model Path`: The model that represents the trigger button.\n * `Grip Left Model Path`: The model that represents the left grip button.\n * `Grip Right Model Path`: The model that represents the right grip button.\n * `Touchpad Model Path`: The model that represents the touchpad.\n * `Button One Model Path`: The model that represents button one.\n * `Button Two Model Path`: The model that represents button two.\n * `System Menu Model Path`: The model that represents the system menu button. * `Start Menu Model Path`: The model that represents the start menu button.")]
		[Obsolete("`VRTK_ControllerActions.modelElementPaths` has been replaced with `VRTK_ControllerHighlighter.modelElementPaths`, it will be removed in a future version of VRTK.")]
		public VRTK_ControllerModelElementPaths modelElementPaths;

		// Token: 0x040011A0 RID: 4512
		[Tooltip("A collection of highlighter overrides for each controller model sub element. If no highlighter override is given then highlighter on the Controller game object is used.\n\n* The available model sub elements are:\n\n * `Body`: The highlighter to use on the overall shape of the controller.\n * `Trigger`: The highlighter to use on the trigger button.\n * `Grip Left`: The highlighter to use on the left grip button.\n * `Grip Right`: The highlighter to use on the  right grip button.\n * `Touchpad`: The highlighter to use on the touchpad.\n * `Button One`: The highlighter to use on button one.\n * `Button Two`: The highlighter to use on button two.\n * `System Menu`: The highlighter to use on the system menu button. * `Start Menu`: The highlighter to use on the start menu button.")]
		[Obsolete("`VRTK_ControllerActions.elementHighlighterOverrides` has been replaced with `VRTK_ControllerHighlighter.elementHighlighterOverrides`, it will be removed in a future version of VRTK.")]
		public VRTK_ControllerElementHighlighters elementHighlighterOverrides;

		// Token: 0x040011A3 RID: 4515
		protected GameObject modelContainer;

		// Token: 0x040011A4 RID: 4516
		protected bool controllerVisible = true;

		// Token: 0x040011A5 RID: 4517
		protected VRTK_ControllerHighlighter controllerHighlighter;

		// Token: 0x040011A6 RID: 4518
		protected bool generateControllerHighlighter;
	}
}
