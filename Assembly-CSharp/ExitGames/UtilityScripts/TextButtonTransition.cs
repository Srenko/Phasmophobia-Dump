using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExitGames.UtilityScripts
{
	// Token: 0x0200047D RID: 1149
	[RequireComponent(typeof(Text))]
	public class TextButtonTransition : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x060023E7 RID: 9191 RVA: 0x000B0080 File Offset: 0x000AE280
		public void Awake()
		{
			this._text = base.GetComponent<Text>();
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000B008E File Offset: 0x000AE28E
		public void OnPointerEnter(PointerEventData eventData)
		{
			this._text.color = this.HoverColor;
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000B00A1 File Offset: 0x000AE2A1
		public void OnPointerExit(PointerEventData eventData)
		{
			this._text.color = this.NormalColor;
		}

		// Token: 0x0400213E RID: 8510
		private Text _text;

		// Token: 0x0400213F RID: 8511
		public Color NormalColor = Color.white;

		// Token: 0x04002140 RID: 8512
		public Color HoverColor = Color.black;
	}
}
