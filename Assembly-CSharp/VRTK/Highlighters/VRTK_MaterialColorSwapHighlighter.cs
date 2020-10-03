using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK.Highlighters
{
	// Token: 0x02000340 RID: 832
	[AddComponentMenu("VRTK/Scripts/Interactions/Highlighters/VRTK_MaterialColorSwapHighlighter")]
	public class VRTK_MaterialColorSwapHighlighter : VRTK_BaseHighlighter
	{
		// Token: 0x06001D22 RID: 7458 RVA: 0x000950FF File Offset: 0x000932FF
		public override void Initialise(Color? color = null, Dictionary<string, object> options = null)
		{
			this.originalSharedRendererMaterials = new Dictionary<string, Material[]>();
			this.originalRendererMaterials = new Dictionary<string, Material[]>();
			this.faderRoutines = new Dictionary<string, Coroutine>();
			this.resetMainTexture = this.GetOption<bool>(options, "resetMainTexture");
			this.ResetHighlighter();
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0009513A File Offset: 0x0009333A
		public override void ResetHighlighter()
		{
			this.StoreOriginalMaterials();
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x00095142 File Offset: 0x00093342
		public override void Highlight(Color? color, float duration = 0f)
		{
			if (color == null)
			{
				return;
			}
			this.ChangeToHighlightColor(color.Value, duration);
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0009515C File Offset: 0x0009335C
		public override void Unhighlight(Color? color = null, float duration = 0f)
		{
			if (this.originalRendererMaterials == null)
			{
				return;
			}
			if (this.faderRoutines != null)
			{
				foreach (KeyValuePair<string, Coroutine> keyValuePair in this.faderRoutines)
				{
					base.StopCoroutine(keyValuePair.Value);
				}
				this.faderRoutines.Clear();
			}
			foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>(true))
			{
				string key = renderer.gameObject.GetInstanceID().ToString();
				if (this.originalRendererMaterials.ContainsKey(key))
				{
					renderer.materials = this.originalRendererMaterials[key];
					renderer.sharedMaterials = this.originalSharedRendererMaterials[key];
				}
			}
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x0009523C File Offset: 0x0009343C
		protected virtual void StoreOriginalMaterials()
		{
			this.originalSharedRendererMaterials.Clear();
			this.originalRendererMaterials.Clear();
			foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>(true))
			{
				string key = renderer.gameObject.GetInstanceID().ToString();
				this.originalSharedRendererMaterials[key] = renderer.sharedMaterials;
				this.originalRendererMaterials[key] = renderer.materials;
				renderer.sharedMaterials = this.originalSharedRendererMaterials[key];
			}
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000952C4 File Offset: 0x000934C4
		protected virtual void ChangeToHighlightColor(Color color, float duration = 0f)
		{
			foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>(true))
			{
				Material[] array = new Material[renderer.materials.Length];
				for (int j = 0; j < renderer.materials.Length; j++)
				{
					Material material = renderer.materials[j];
					if (this.customMaterial)
					{
						material = this.customMaterial;
						array[j] = material;
					}
					string key = material.GetInstanceID().ToString();
					if (this.faderRoutines.ContainsKey(key) && this.faderRoutines[key] != null)
					{
						base.StopCoroutine(this.faderRoutines[key]);
						this.faderRoutines.Remove(key);
					}
					material.EnableKeyword("_EMISSION");
					if (this.resetMainTexture && material.HasProperty("_MainTex"))
					{
						renderer.material.SetTexture("_MainTex", new Texture2D(0, 0));
					}
					if (material.HasProperty("_Color"))
					{
						if (duration > 0f)
						{
							this.faderRoutines[key] = base.StartCoroutine(this.CycleColor(material, material.color, color, duration));
						}
						else
						{
							material.color = color;
							if (material.HasProperty("_EmissionColor"))
							{
								material.SetColor("_EmissionColor", VRTK_SharedMethods.ColorDarken(color, this.emissionDarken));
							}
						}
					}
				}
				if (this.customMaterial)
				{
					renderer.materials = array;
				}
			}
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0009544A File Offset: 0x0009364A
		protected virtual IEnumerator CycleColor(Material material, Color startColor, Color endColor, float duration)
		{
			float elapsedTime = 0f;
			while (elapsedTime <= duration)
			{
				elapsedTime += Time.deltaTime;
				if (material.HasProperty("_Color"))
				{
					material.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
				}
				if (material.HasProperty("_EmissionColor"))
				{
					material.SetColor("_EmissionColor", Color.Lerp(startColor, VRTK_SharedMethods.ColorDarken(endColor, this.emissionDarken), elapsedTime / duration));
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x04001715 RID: 5909
		[Tooltip("The emission colour of the texture will be the highlight colour but this percent darker.")]
		public float emissionDarken = 50f;

		// Token: 0x04001716 RID: 5910
		[Tooltip("A custom material to use on the highlighted object.")]
		public Material customMaterial;

		// Token: 0x04001717 RID: 5911
		protected Dictionary<string, Material[]> originalSharedRendererMaterials = new Dictionary<string, Material[]>();

		// Token: 0x04001718 RID: 5912
		protected Dictionary<string, Material[]> originalRendererMaterials = new Dictionary<string, Material[]>();

		// Token: 0x04001719 RID: 5913
		protected Dictionary<string, Coroutine> faderRoutines;

		// Token: 0x0400171A RID: 5914
		protected bool resetMainTexture;
	}
}
