using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExitGames.UtilityScripts
{
	// Token: 0x0200047C RID: 1148
	public class ButtonInsideScrollList : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x060023E3 RID: 9187 RVA: 0x000B0022 File Offset: 0x000AE222
		private void Start()
		{
			this.scrollRect = base.GetComponentInParent<ScrollRect>();
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000B0030 File Offset: 0x000AE230
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (this.scrollRect != null)
			{
				this.scrollRect.StopMovement();
				this.scrollRect.enabled = false;
			}
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000B0057 File Offset: 0x000AE257
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (this.scrollRect != null && !this.scrollRect.enabled)
			{
				this.scrollRect.enabled = true;
			}
		}

		// Token: 0x0400213D RID: 8509
		private ScrollRect scrollRect;
	}
}
