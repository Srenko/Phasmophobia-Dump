using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000320 RID: 800
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_ControllerTooltips_UnityEvents")]
	public sealed class VRTK_ControllerTooltips_UnityEvents : VRTK_UnityEvents<VRTK_ControllerTooltips>
	{
		// Token: 0x06001C48 RID: 7240 RVA: 0x00092EE2 File Offset: 0x000910E2
		protected override void AddListeners(VRTK_ControllerTooltips component)
		{
			component.ControllerTooltipOn += this.ControllerTooltipOn;
			component.ControllerTooltipOff += this.ControllerTooltipOff;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x00092F08 File Offset: 0x00091108
		protected override void RemoveListeners(VRTK_ControllerTooltips component)
		{
			component.ControllerTooltipOn -= this.ControllerTooltipOn;
			component.ControllerTooltipOff -= this.ControllerTooltipOff;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00092F2E File Offset: 0x0009112E
		private void ControllerTooltipOn(object o, ControllerTooltipsEventArgs e)
		{
			this.OnControllerTooltipOn.Invoke(o, e);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00092F3D File Offset: 0x0009113D
		private void ControllerTooltipOff(object o, ControllerTooltipsEventArgs e)
		{
			this.OnControllerTooltipOff.Invoke(o, e);
		}

		// Token: 0x04001699 RID: 5785
		public VRTK_ControllerTooltips_UnityEvents.ControllerTooltipsEvent OnControllerTooltipOn = new VRTK_ControllerTooltips_UnityEvents.ControllerTooltipsEvent();

		// Token: 0x0400169A RID: 5786
		public VRTK_ControllerTooltips_UnityEvents.ControllerTooltipsEvent OnControllerTooltipOff = new VRTK_ControllerTooltips_UnityEvents.ControllerTooltipsEvent();

		// Token: 0x02000624 RID: 1572
		[Serializable]
		public sealed class ControllerTooltipsEvent : UnityEvent<object, ControllerTooltipsEventArgs>
		{
		}
	}
}
