using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200032E RID: 814
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_ObjectControl_UnityEvents")]
	public sealed class VRTK_ObjectControl_UnityEvents : VRTK_UnityEvents<VRTK_ObjectControl>
	{
		// Token: 0x06001CB1 RID: 7345 RVA: 0x00093FF6 File Offset: 0x000921F6
		protected override void AddListeners(VRTK_ObjectControl component)
		{
			component.XAxisChanged += this.XAxisChanged;
			component.YAxisChanged += this.YAxisChanged;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0009401C File Offset: 0x0009221C
		protected override void RemoveListeners(VRTK_ObjectControl component)
		{
			component.XAxisChanged -= this.XAxisChanged;
			component.YAxisChanged -= this.YAxisChanged;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00094042 File Offset: 0x00092242
		private void XAxisChanged(object o, ObjectControlEventArgs e)
		{
			this.OnXAxisChanged.Invoke(o, e);
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00094051 File Offset: 0x00092251
		private void YAxisChanged(object o, ObjectControlEventArgs e)
		{
			this.OnYAxisChanged.Invoke(o, e);
		}

		// Token: 0x040016D8 RID: 5848
		public VRTK_ObjectControl_UnityEvents.ObjectControlEvent OnXAxisChanged = new VRTK_ObjectControl_UnityEvents.ObjectControlEvent();

		// Token: 0x040016D9 RID: 5849
		public VRTK_ObjectControl_UnityEvents.ObjectControlEvent OnYAxisChanged = new VRTK_ObjectControl_UnityEvents.ObjectControlEvent();

		// Token: 0x02000632 RID: 1586
		[Serializable]
		public sealed class ObjectControlEvent : UnityEvent<object, ObjectControlEventArgs>
		{
		}
	}
}
