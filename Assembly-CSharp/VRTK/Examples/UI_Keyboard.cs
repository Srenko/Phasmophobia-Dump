using System;
using UnityEngine;
using UnityEngine.UI;

namespace VRTK.Examples
{
	// Token: 0x0200036B RID: 875
	public class UI_Keyboard : MonoBehaviour
	{
		// Token: 0x06001E16 RID: 7702 RVA: 0x00098AFD File Offset: 0x00096CFD
		public void ClickKey(string character)
		{
			InputField inputField = this.input;
			inputField.text += character;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00098B18 File Offset: 0x00096D18
		public void Backspace()
		{
			if (this.input.text.Length > 0)
			{
				this.input.text = this.input.text.Substring(0, this.input.text.Length - 1);
			}
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00098B66 File Offset: 0x00096D66
		public void Enter()
		{
			VRTK_Logger.Info("You've typed [" + this.input.text + "]");
			this.input.text = "";
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00098B97 File Offset: 0x00096D97
		private void Start()
		{
			this.input = base.GetComponentInChildren<InputField>();
		}

		// Token: 0x040017B1 RID: 6065
		private InputField input;
	}
}
