using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200031B RID: 795
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_Button_UnityEvents")]
	public sealed class VRTK_Button_UnityEvents : VRTK_UnityEvents<VRTK_Button>
	{
		// Token: 0x06001C01 RID: 7169 RVA: 0x000920F1 File Offset: 0x000902F1
		protected override void AddListeners(VRTK_Button component)
		{
			component.Pushed += this.Pushed;
			component.Released += this.Released;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00092117 File Offset: 0x00090317
		protected override void RemoveListeners(VRTK_Button component)
		{
			component.Pushed -= this.Pushed;
			component.Released -= this.Released;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0009213D File Offset: 0x0009033D
		private void Pushed(object o, Control3DEventArgs e)
		{
			this.OnPushed.Invoke(o, e);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0009214C File Offset: 0x0009034C
		public void Released(object o, Control3DEventArgs e)
		{
			this.OnReleased.Invoke(o, e);
		}

		// Token: 0x04001661 RID: 5729
		public VRTK_Button_UnityEvents.Button3DEvent OnPushed = new VRTK_Button_UnityEvents.Button3DEvent();

		// Token: 0x04001662 RID: 5730
		public VRTK_Button_UnityEvents.Button3DEvent OnReleased = new VRTK_Button_UnityEvents.Button3DEvent();

		// Token: 0x0200061F RID: 1567
		[Serializable]
		public sealed class Button3DEvent : UnityEvent<object, Control3DEventArgs>
		{
		}
	}
}
