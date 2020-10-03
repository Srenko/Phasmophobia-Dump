using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Highlighters;

namespace VRTK
{
	// Token: 0x020002AC RID: 684
	public class VRTK_ObjectAppearance : MonoBehaviour
	{
		// Token: 0x0600169F RID: 5791 RVA: 0x0007A3F7 File Offset: 0x000785F7
		public static void SetOpacity(GameObject model, float alpha, float transitionDuration = 0f)
		{
			VRTK_ObjectAppearance.SetupInstance();
			if (VRTK_ObjectAppearance.instance != null)
			{
				VRTK_ObjectAppearance.instance.InternalSetOpacity(model, alpha, transitionDuration);
			}
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0007A418 File Offset: 0x00078618
		public static void SetRendererVisible(GameObject model, GameObject ignoredModel = null)
		{
			VRTK_ObjectAppearance.SetupInstance();
			if (VRTK_ObjectAppearance.instance != null)
			{
				VRTK_ObjectAppearance.instance.InternalSetRendererVisible(model, ignoredModel);
			}
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0007A438 File Offset: 0x00078638
		public static void SetRendererHidden(GameObject model, GameObject ignoredModel = null)
		{
			VRTK_ObjectAppearance.SetupInstance();
			if (VRTK_ObjectAppearance.instance != null)
			{
				VRTK_ObjectAppearance.instance.InternalSetRendererHidden(model, ignoredModel);
			}
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0007A458 File Offset: 0x00078658
		public static void ToggleRenderer(bool state, GameObject model, GameObject ignoredModel = null)
		{
			if (state)
			{
				VRTK_ObjectAppearance.SetRendererVisible(model, ignoredModel);
				return;
			}
			VRTK_ObjectAppearance.SetRendererHidden(model, ignoredModel);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0007A46C File Offset: 0x0007866C
		public static void HighlightObject(GameObject model, Color? highlightColor, float fadeDuration = 0f)
		{
			VRTK_ObjectAppearance.SetupInstance();
			if (VRTK_ObjectAppearance.instance != null)
			{
				VRTK_ObjectAppearance.instance.InternalHighlightObject(model, highlightColor, fadeDuration);
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0007A48D File Offset: 0x0007868D
		public static void UnhighlightObject(GameObject model)
		{
			VRTK_ObjectAppearance.SetupInstance();
			if (VRTK_ObjectAppearance.instance != null)
			{
				VRTK_ObjectAppearance.instance.InternalUnhighlightObject(model);
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0007A4AC File Offset: 0x000786AC
		protected virtual void OnDisable()
		{
			foreach (KeyValuePair<GameObject, Coroutine> keyValuePair in this.setOpacityCoroutines)
			{
				this.CancelSetOpacityCoroutine(keyValuePair.Key);
			}
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0007A508 File Offset: 0x00078708
		protected static void SetupInstance()
		{
			if (VRTK_ObjectAppearance.instance == null && VRTK_SDKManager.instance != null)
			{
				VRTK_ObjectAppearance.instance = VRTK_SDKManager.instance.gameObject.AddComponent<VRTK_ObjectAppearance>();
			}
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0007A538 File Offset: 0x00078738
		protected virtual void InternalSetOpacity(GameObject model, float alpha, float transitionDuration = 0f)
		{
			if (model && model.activeInHierarchy)
			{
				if (transitionDuration == 0f)
				{
					this.ChangeRendererOpacity(model, alpha);
					return;
				}
				this.CancelSetOpacityCoroutine(model);
				Coroutine value = base.StartCoroutine(this.TransitionRendererOpacity(model, this.GetInitialAlpha(model), alpha, transitionDuration));
				if (!this.setOpacityCoroutines.ContainsKey(model))
				{
					this.setOpacityCoroutines.Add(model, value);
				}
			}
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0007A5A0 File Offset: 0x000787A0
		protected virtual void InternalSetRendererVisible(GameObject model, GameObject ignoredModel = null)
		{
			if (model != null)
			{
				foreach (Renderer renderer in model.GetComponentsInChildren<Renderer>(true))
				{
					if (renderer.gameObject != ignoredModel && (ignoredModel == null || !renderer.transform.IsChildOf(ignoredModel.transform)))
					{
						renderer.enabled = true;
					}
				}
			}
			this.EmitControllerEvents(model, true);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0007A60C File Offset: 0x0007880C
		protected virtual void InternalSetRendererHidden(GameObject model, GameObject ignoredModel = null)
		{
			if (model != null)
			{
				foreach (Renderer renderer in model.GetComponentsInChildren<Renderer>(true))
				{
					if (renderer.gameObject != ignoredModel && (ignoredModel == null || !renderer.transform.IsChildOf(ignoredModel.transform)))
					{
						renderer.enabled = false;
					}
				}
			}
			this.EmitControllerEvents(model, false);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0007A678 File Offset: 0x00078878
		protected virtual void InternalHighlightObject(GameObject model, Color? highlightColor, float fadeDuration = 0f)
		{
			VRTK_BaseHighlighter componentInChildren = model.GetComponentInChildren<VRTK_BaseHighlighter>();
			if (model.activeInHierarchy && componentInChildren != null)
			{
				componentInChildren.Highlight((highlightColor != null) ? highlightColor : new Color?(Color.white), fadeDuration);
			}
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0007A6BC File Offset: 0x000788BC
		protected virtual void InternalUnhighlightObject(GameObject model)
		{
			VRTK_BaseHighlighter componentInChildren = model.GetComponentInChildren<VRTK_BaseHighlighter>();
			if (model.activeInHierarchy && componentInChildren != null)
			{
				componentInChildren.Unhighlight(null, 0f);
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0007A6F8 File Offset: 0x000788F8
		protected virtual void EmitControllerEvents(GameObject model, bool state)
		{
			GameObject gameObject = null;
			if (VRTK_DeviceFinder.GetModelAliasControllerHand(model) == SDK_BaseController.ControllerHand.Left)
			{
				gameObject = VRTK_DeviceFinder.GetControllerLeftHand(false);
			}
			else if (VRTK_DeviceFinder.GetModelAliasControllerHand(model) == SDK_BaseController.ControllerHand.Right)
			{
				gameObject = VRTK_DeviceFinder.GetControllerRightHand(false);
			}
			if (gameObject != null && gameObject.activeInHierarchy)
			{
				VRTK_ControllerEvents component = gameObject.GetComponent<VRTK_ControllerEvents>();
				if (component != null)
				{
					if (state)
					{
						component.OnControllerVisible(component.SetControllerEvent());
						return;
					}
					component.OnControllerHidden(component.SetControllerEvent());
				}
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0007A768 File Offset: 0x00078968
		protected virtual void ChangeRendererOpacity(GameObject model, float alpha)
		{
			if (model != null)
			{
				alpha = Mathf.Clamp(alpha, 0f, 1f);
				foreach (Renderer renderer in model.GetComponentsInChildren<Renderer>(true))
				{
					if (alpha < 1f)
					{
						renderer.material.SetInt("_SrcBlend", 1);
						renderer.material.SetInt("_DstBlend", 10);
						renderer.material.SetInt("_ZWrite", 0);
						renderer.material.DisableKeyword("_ALPHATEST_ON");
						renderer.material.DisableKeyword("_ALPHABLEND_ON");
						renderer.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
						renderer.material.renderQueue = 3000;
					}
					else
					{
						renderer.material.SetInt("_SrcBlend", 1);
						renderer.material.SetInt("_DstBlend", 0);
						renderer.material.SetInt("_ZWrite", 1);
						renderer.material.DisableKeyword("_ALPHATEST_ON");
						renderer.material.DisableKeyword("_ALPHABLEND_ON");
						renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
						renderer.material.renderQueue = -1;
					}
					if (renderer.material.HasProperty("_Color"))
					{
						renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, alpha);
					}
				}
			}
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0007A8F4 File Offset: 0x00078AF4
		protected virtual float GetInitialAlpha(GameObject model)
		{
			Renderer componentInChildren = model.GetComponentInChildren<Renderer>(true);
			if (componentInChildren.material.HasProperty("_Color"))
			{
				return componentInChildren.material.color.a;
			}
			return 0f;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0007A931 File Offset: 0x00078B31
		protected virtual IEnumerator TransitionRendererOpacity(GameObject model, float initialAlpha, float targetAlpha, float transitionDuration)
		{
			float elapsedTime = 0f;
			while (elapsedTime < transitionDuration)
			{
				float alpha = Mathf.Lerp(initialAlpha, targetAlpha, elapsedTime / transitionDuration);
				this.ChangeRendererOpacity(model, alpha);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			this.ChangeRendererOpacity(model, targetAlpha);
			yield break;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0007A95D File Offset: 0x00078B5D
		protected virtual void CancelSetOpacityCoroutine(GameObject model)
		{
			if (this.setOpacityCoroutines.ContainsKey(model) && this.setOpacityCoroutines[model] != null)
			{
				base.StopCoroutine(this.setOpacityCoroutines[model]);
			}
		}

		// Token: 0x040012C2 RID: 4802
		protected static VRTK_ObjectAppearance instance;

		// Token: 0x040012C3 RID: 4803
		protected Dictionary<GameObject, Coroutine> setOpacityCoroutines = new Dictionary<GameObject, Coroutine>();
	}
}
