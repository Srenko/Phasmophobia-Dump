using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExitGames.UtilityScripts
{
	// Token: 0x0200047E RID: 1150
	[RequireComponent(typeof(Text))]
	public class TextToggleIsOnTransition : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x060023EB RID: 9195 RVA: 0x000B00D2 File Offset: 0x000AE2D2
		public void OnEnable()
		{
			this._text = base.GetComponent<Text>();
			this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000B00FC File Offset: 0x000AE2FC
		public void OnDisable()
		{
			this.toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged));
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000B011A File Offset: 0x000AE31A
		public void OnValueChanged(bool isOn)
		{
			this._text.color = (isOn ? (this.isHover ? this.HoverOnColor : this.HoverOffColor) : (this.isHover ? this.NormalOnColor : this.NormalOffColor));
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000B0158 File Offset: 0x000AE358
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.isHover = true;
			this._text.color = (this.toggle.isOn ? this.HoverOnColor : this.HoverOffColor);
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000B0187 File Offset: 0x000AE387
		public void OnPointerExit(PointerEventData eventData)
		{
			this.isHover = false;
			this._text.color = (this.toggle.isOn ? this.NormalOnColor : this.NormalOffColor);
		}

		// Token: 0x04002141 RID: 8513
		public Toggle toggle;

		// Token: 0x04002142 RID: 8514
		private Text _text;

		// Token: 0x04002143 RID: 8515
		public Color NormalOnColor = Color.white;

		// Token: 0x04002144 RID: 8516
		public Color NormalOffColor = Color.black;

		// Token: 0x04002145 RID: 8517
		public Color HoverOnColor = Color.black;

		// Token: 0x04002146 RID: 8518
		public Color HoverOffColor = Color.black;

		// Token: 0x04002147 RID: 8519
		private bool isHover;
	}
}
