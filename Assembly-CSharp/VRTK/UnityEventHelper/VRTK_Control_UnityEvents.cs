using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200031C RID: 796
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_Control_UnityEvents")]
	public sealed class VRTK_Control_UnityEvents : VRTK_UnityEvents<VRTK_Control>
	{
		// Token: 0x06001C06 RID: 7174 RVA: 0x00092179 File Offset: 0x00090379
		protected override void AddListeners(VRTK_Control component)
		{
			component.ValueChanged += this.ValueChanged;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x0009218D File Offset: 0x0009038D
		protected override void RemoveListeners(VRTK_Control component)
		{
			component.ValueChanged -= this.ValueChanged;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x000921A1 File Offset: 0x000903A1
		private void ValueChanged(object o, Control3DEventArgs e)
		{
			this.OnValueChanged.Invoke(o, e);
		}

		// Token: 0x04001663 RID: 5731
		public VRTK_Control_UnityEvents.Control3DEvent OnValueChanged = new VRTK_Control_UnityEvents.Control3DEvent();

		// Token: 0x02000620 RID: 1568
		[Serializable]
		public sealed class Control3DEvent : UnityEvent<object, Control3DEventArgs>
		{
		}
	}
}
