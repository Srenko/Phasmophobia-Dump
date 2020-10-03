using System;
using UnityEngine;
using UnityEngine.UI;

namespace ExitGames.Demos.DemoPunVoice
{
	// Token: 0x0200048D RID: 1165
	[RequireComponent(typeof(Toggle))]
	[DisallowMultipleComponent]
	public class BetterToggle : MonoBehaviour
	{
		// Token: 0x140000CA RID: 202
		// (add) Token: 0x0600244B RID: 9291 RVA: 0x000B17B0 File Offset: 0x000AF9B0
		// (remove) Token: 0x0600244C RID: 9292 RVA: 0x000B17E4 File Offset: 0x000AF9E4
		public static event BetterToggle.OnToggle ToggleValueChanged;

		// Token: 0x0600244D RID: 9293 RVA: 0x000B1817 File Offset: 0x000AFA17
		private void Start()
		{
			this.toggle = base.GetComponent<Toggle>();
			this.toggle.onValueChanged.AddListener(delegate(bool <p0>)
			{
				this.OnToggleValueChanged();
			});
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000B1841 File Offset: 0x000AFA41
		public void OnToggleValueChanged()
		{
			if (BetterToggle.ToggleValueChanged != null)
			{
				BetterToggle.ToggleValueChanged(this.toggle);
			}
		}

		// Token: 0x0400218D RID: 8589
		private Toggle toggle;

		// Token: 0x02000798 RID: 1944
		// (Invoke) Token: 0x0600302C RID: 12332
		public delegate void OnToggle(Toggle toggle);
	}
}
