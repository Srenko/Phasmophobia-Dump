using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK.Highlighters
{
	// Token: 0x0200033F RID: 831
	public abstract class VRTK_BaseHighlighter : MonoBehaviour
	{
		// Token: 0x06001D19 RID: 7449
		public abstract void Initialise(Color? color = null, Dictionary<string, object> options = null);

		// Token: 0x06001D1A RID: 7450
		public abstract void ResetHighlighter();

		// Token: 0x06001D1B RID: 7451
		public abstract void Highlight(Color? color = null, float duration = 0f);

		// Token: 0x06001D1C RID: 7452
		public abstract void Unhighlight(Color? color = null, float duration = 0f);

		// Token: 0x06001D1D RID: 7453 RVA: 0x00095048 File Offset: 0x00093248
		public virtual T GetOption<T>(Dictionary<string, object> options, string key)
		{
			if (options != null && options.ContainsKey(key) && options[key] != null)
			{
				return (T)((object)options[key]);
			}
			return default(T);
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x00095080 File Offset: 0x00093280
		public virtual bool UsesClonedObject()
		{
			return this.usesClonedObject;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x00095088 File Offset: 0x00093288
		public static VRTK_BaseHighlighter GetActiveHighlighter(GameObject obj)
		{
			VRTK_BaseHighlighter result = null;
			foreach (VRTK_BaseHighlighter vrtk_BaseHighlighter in obj.GetComponents<VRTK_BaseHighlighter>())
			{
				if (vrtk_BaseHighlighter.active)
				{
					result = vrtk_BaseHighlighter;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x000950C0 File Offset: 0x000932C0
		protected virtual void OnDisable()
		{
			if (this.unhighlightOnDisable)
			{
				this.Unhighlight(null, 0f);
			}
		}

		// Token: 0x04001712 RID: 5906
		[Tooltip("Determines if this highlighter is the active highlighter for the object the component is attached to. Only 1 active highlighter can be applied to a game object.")]
		public bool active = true;

		// Token: 0x04001713 RID: 5907
		[Tooltip("Determines if the highlighted object should be unhighlighted when it is disabled.")]
		public bool unhighlightOnDisable = true;

		// Token: 0x04001714 RID: 5908
		protected bool usesClonedObject;
	}
}
