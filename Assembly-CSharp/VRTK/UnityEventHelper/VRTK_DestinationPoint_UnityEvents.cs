using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000323 RID: 803
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_DestinationPoint_UnityEvents")]
	public sealed class VRTK_DestinationPoint_UnityEvents : VRTK_UnityEvents<VRTK_DestinationPoint>
	{
		// Token: 0x06001C59 RID: 7257 RVA: 0x00093114 File Offset: 0x00091314
		protected override void AddListeners(VRTK_DestinationPoint component)
		{
			component.DestinationPointEnabled += this.DestinationPointEnabled;
			component.DestinationPointDisabled += this.DestinationPointDisabled;
			component.DestinationPointLocked += this.DestinationPointLocked;
			component.DestinationPointUnlocked += this.DestinationPointUnlocked;
			component.DestinationPointReset += this.DestinationPointReset;
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0009317C File Offset: 0x0009137C
		protected override void RemoveListeners(VRTK_DestinationPoint component)
		{
			component.DestinationPointEnabled -= this.DestinationPointEnabled;
			component.DestinationPointDisabled -= this.DestinationPointDisabled;
			component.DestinationPointLocked -= this.DestinationPointLocked;
			component.DestinationPointUnlocked -= this.DestinationPointUnlocked;
			component.DestinationPointReset -= this.DestinationPointReset;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x000931E3 File Offset: 0x000913E3
		private void DestinationPointEnabled(object o)
		{
			this.OnDestinationPointEnabled.Invoke(o);
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x000931F1 File Offset: 0x000913F1
		private void DestinationPointDisabled(object o)
		{
			this.OnDestinationPointDisabled.Invoke(o);
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x000931FF File Offset: 0x000913FF
		private void DestinationPointLocked(object o)
		{
			this.OnDestinationPointLocked.Invoke(o);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0009320D File Offset: 0x0009140D
		private void DestinationPointUnlocked(object o)
		{
			this.OnDestinationPointUnlocked.Invoke(o);
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0009321B File Offset: 0x0009141B
		private void DestinationPointReset(object o)
		{
			this.OnDestinationPointReset.Invoke(o);
		}

		// Token: 0x040016A1 RID: 5793
		public VRTK_DestinationPoint_UnityEvents.DestinationPointEvent OnDestinationPointEnabled = new VRTK_DestinationPoint_UnityEvents.DestinationPointEvent();

		// Token: 0x040016A2 RID: 5794
		public VRTK_DestinationPoint_UnityEvents.DestinationPointEvent OnDestinationPointDisabled = new VRTK_DestinationPoint_UnityEvents.DestinationPointEvent();

		// Token: 0x040016A3 RID: 5795
		public VRTK_DestinationPoint_UnityEvents.DestinationPointEvent OnDestinationPointLocked = new VRTK_DestinationPoint_UnityEvents.DestinationPointEvent();

		// Token: 0x040016A4 RID: 5796
		public VRTK_DestinationPoint_UnityEvents.DestinationPointEvent OnDestinationPointUnlocked = new VRTK_DestinationPoint_UnityEvents.DestinationPointEvent();

		// Token: 0x040016A5 RID: 5797
		public VRTK_DestinationPoint_UnityEvents.DestinationPointEvent OnDestinationPointReset = new VRTK_DestinationPoint_UnityEvents.DestinationPointEvent();

		// Token: 0x02000627 RID: 1575
		[Serializable]
		public sealed class DestinationPointEvent : UnityEvent<object>
		{
		}
	}
}
