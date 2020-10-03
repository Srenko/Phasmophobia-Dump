using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000332 RID: 818
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_PointerDirectionIndicator_UnityEvents")]
	public sealed class VRTK_PointerDirectionIndicator_UnityEvents : VRTK_UnityEvents<VRTK_PointerDirectionIndicator>
	{
		// Token: 0x06001CC5 RID: 7365 RVA: 0x00094216 File Offset: 0x00092416
		protected override void AddListeners(VRTK_PointerDirectionIndicator component)
		{
			component.PointerDirectionIndicatorPositionSet += this.PointerDirectionIndicatorPositionSet;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0009422A File Offset: 0x0009242A
		protected override void RemoveListeners(VRTK_PointerDirectionIndicator component)
		{
			component.PointerDirectionIndicatorPositionSet -= this.PointerDirectionIndicatorPositionSet;
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0009423E File Offset: 0x0009243E
		private void PointerDirectionIndicatorPositionSet(object o)
		{
			this.OnPointerDirectionIndicatorPositionSet.Invoke(o);
		}

		// Token: 0x040016E0 RID: 5856
		public VRTK_PointerDirectionIndicator_UnityEvents.PointerDirectionIndicatorEvent OnPointerDirectionIndicatorPositionSet = new VRTK_PointerDirectionIndicator_UnityEvents.PointerDirectionIndicatorEvent();

		// Token: 0x02000636 RID: 1590
		[Serializable]
		public sealed class PointerDirectionIndicatorEvent : UnityEvent<object>
		{
		}
	}
}
