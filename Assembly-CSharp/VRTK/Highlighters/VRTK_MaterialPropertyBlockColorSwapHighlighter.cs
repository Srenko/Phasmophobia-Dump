using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK.Highlighters
{
	// Token: 0x02000341 RID: 833
	[AddComponentMenu("VRTK/Scripts/Interactions/Highlighters/VRTK_MaterialPropertyBlockColorSwapHighlighter")]
	public class VRTK_MaterialPropertyBlockColorSwapHighlighter : VRTK_MaterialColorSwapHighlighter
	{
		// Token: 0x06001D2A RID: 7466 RVA: 0x0009549F File Offset: 0x0009369F
		public override void Initialise(Color? color = null, Dictionary<string, object> options = null)
		{
			this.originalMaterialPropertyBlocks = new Dictionary<string, MaterialPropertyBlock>();
			this.highlightMaterialPropertyBlocks = new Dictionary<string, MaterialPropertyBlock>();
			base.Initialise(color, options);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x000954C0 File Offset: 0x000936C0
		public override void Unhighlight(Color? color = null, float duration = 0f)
		{
			if (this.originalMaterialPropertyBlocks == null)
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
				if (this.originalMaterialPropertyBlocks.ContainsKey(key))
				{
					renderer.SetPropertyBlock(this.originalMaterialPropertyBlocks[key]);
				}
			}
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0009558C File Offset: 0x0009378C
		protected override void StoreOriginalMaterials()
		{
			this.originalMaterialPropertyBlocks.Clear();
			this.highlightMaterialPropertyBlocks.Clear();
			foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>(true))
			{
				string key = renderer.gameObject.GetInstanceID().ToString();
				this.originalMaterialPropertyBlocks[key] = new MaterialPropertyBlock();
				renderer.GetPropertyBlock(this.originalMaterialPropertyBlocks[key]);
				this.highlightMaterialPropertyBlocks[key] = new MaterialPropertyBlock();
			}
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00095610 File Offset: 0x00093810
		protected override void ChangeToHighlightColor(Color color, float duration = 0f)
		{
			foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>(true))
			{
				string key = renderer.gameObject.GetInstanceID().ToString();
				if (this.originalMaterialPropertyBlocks.ContainsKey(key))
				{
					if (this.faderRoutines.ContainsKey(key) && this.faderRoutines[key] != null)
					{
						base.StopCoroutine(this.faderRoutines[key]);
						this.faderRoutines.Remove(key);
					}
					MaterialPropertyBlock materialPropertyBlock = this.highlightMaterialPropertyBlocks[key];
					renderer.GetPropertyBlock(materialPropertyBlock);
					if (this.resetMainTexture)
					{
						materialPropertyBlock.SetTexture("_MainTex", Texture2D.whiteTexture);
					}
					if (duration > 0f)
					{
						this.faderRoutines[key] = base.StartCoroutine(this.CycleColor(renderer, materialPropertyBlock, color, duration));
					}
					else
					{
						materialPropertyBlock.SetColor("_Color", color);
						materialPropertyBlock.SetColor("_EmissionColor", VRTK_SharedMethods.ColorDarken(color, this.emissionDarken));
						renderer.SetPropertyBlock(materialPropertyBlock);
					}
				}
			}
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x00095721 File Offset: 0x00093921
		protected virtual IEnumerator CycleColor(Renderer renderer, MaterialPropertyBlock highlightMaterialPropertyBlock, Color endColor, float duration)
		{
			float elapsedTime = 0f;
			while (elapsedTime <= duration)
			{
				elapsedTime += Time.deltaTime;
				Color a = highlightMaterialPropertyBlock.GetVector("_Color");
				highlightMaterialPropertyBlock.SetColor("_Color", Color.Lerp(a, endColor, elapsedTime / duration));
				highlightMaterialPropertyBlock.SetColor("_EmissionColor", Color.Lerp(a, endColor, elapsedTime / duration));
				if (!renderer)
				{
					yield break;
				}
				renderer.SetPropertyBlock(highlightMaterialPropertyBlock);
				yield return null;
			}
			yield break;
		}

		// Token: 0x0400171B RID: 5915
		protected Dictionary<string, MaterialPropertyBlock> originalMaterialPropertyBlocks = new Dictionary<string, MaterialPropertyBlock>();

		// Token: 0x0400171C RID: 5916
		protected Dictionary<string, MaterialPropertyBlock> highlightMaterialPropertyBlocks = new Dictionary<string, MaterialPropertyBlock>();
	}
}
