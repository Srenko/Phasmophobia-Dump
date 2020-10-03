using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200032F RID: 815
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_ObjectTooltip_UnityEvents")]
	public sealed class VRTK_ObjectTooltip_UnityEvents : VRTK_UnityEvents<VRTK_ObjectTooltip>
	{
		// Token: 0x06001CB6 RID: 7350 RVA: 0x0009407E File Offset: 0x0009227E
		protected override void AddListeners(VRTK_ObjectTooltip component)
		{
			component.ObjectTooltipReset += this.ObjectTooltipReset;
			component.ObjectTooltipTextUpdated += this.ObjectTooltipTextUpdated;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000940A4 File Offset: 0x000922A4
		protected override void RemoveListeners(VRTK_ObjectTooltip component)
		{
			component.ObjectTooltipReset -= this.ObjectTooltipReset;
			component.ObjectTooltipTextUpdated -= this.ObjectTooltipTextUpdated;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000940CA File Offset: 0x000922CA
		private void ObjectTooltipReset(object o, ObjectTooltipEventArgs e)
		{
			this.OnObjectTooltipReset.Invoke(o, e);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x000940D9 File Offset: 0x000922D9
		private void ObjectTooltipTextUpdated(object o, ObjectTooltipEventArgs e)
		{
			this.OnObjectTooltipTextUpdated.Invoke(o, e);
		}

		// Token: 0x040016DA RID: 5850
		public VRTK_ObjectTooltip_UnityEvents.ObjectTooltipEvent OnObjectTooltipReset = new VRTK_ObjectTooltip_UnityEvents.ObjectTooltipEvent();

		// Token: 0x040016DB RID: 5851
		public VRTK_ObjectTooltip_UnityEvents.ObjectTooltipEvent OnObjectTooltipTextUpdated = new VRTK_ObjectTooltip_UnityEvents.ObjectTooltipEvent();

		// Token: 0x02000633 RID: 1587
		[Serializable]
		public sealed class ObjectTooltipEvent : UnityEvent<object, ObjectTooltipEventArgs>
		{
		}
	}
}
