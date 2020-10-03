using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000334 RID: 820
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_PositionRewind_UnityEvents")]
	public sealed class VRTK_PositionRewind_UnityEvents : VRTK_UnityEvents<VRTK_PositionRewind>
	{
		// Token: 0x06001CD2 RID: 7378 RVA: 0x00094405 File Offset: 0x00092605
		protected override void AddListeners(VRTK_PositionRewind component)
		{
			component.PositionRewindToSafe += this.PositionRewindToSafe;
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00094419 File Offset: 0x00092619
		protected override void RemoveListeners(VRTK_PositionRewind component)
		{
			component.PositionRewindToSafe -= this.PositionRewindToSafe;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0009442D File Offset: 0x0009262D
		private void PositionRewindToSafe(object o, PositionRewindEventArgs e)
		{
			this.OnPositionRewindToSafe.Invoke(o, e);
		}

		// Token: 0x040016E7 RID: 5863
		public VRTK_PositionRewind_UnityEvents.PositionRewindEvent OnPositionRewindToSafe = new VRTK_PositionRewind_UnityEvents.PositionRewindEvent();

		// Token: 0x02000637 RID: 1591
		[Serializable]
		public sealed class PositionRewindEvent : UnityEvent<object, PositionRewindEventArgs>
		{
		}
	}
}
