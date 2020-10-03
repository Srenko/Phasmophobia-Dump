using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000321 RID: 801
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_DashTeleport_UnityEvents")]
	public sealed class VRTK_DashTeleport_UnityEvents : VRTK_UnityEvents<VRTK_DashTeleport>
	{
		// Token: 0x06001C4D RID: 7245 RVA: 0x00092F6A File Offset: 0x0009116A
		protected override void AddListeners(VRTK_DashTeleport component)
		{
			component.WillDashThruObjects += this.WillDashThruObjects;
			component.DashedThruObjects += this.DashedThruObjects;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00092F90 File Offset: 0x00091190
		protected override void RemoveListeners(VRTK_DashTeleport component)
		{
			component.WillDashThruObjects -= this.WillDashThruObjects;
			component.DashedThruObjects -= this.DashedThruObjects;
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00092FB6 File Offset: 0x000911B6
		private void WillDashThruObjects(object o, DashTeleportEventArgs e)
		{
			this.OnWillDashThruObjects.Invoke(o, e);
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00092FC5 File Offset: 0x000911C5
		private void DashedThruObjects(object o, DashTeleportEventArgs e)
		{
			this.OnDashedThruObjects.Invoke(o, e);
		}

		// Token: 0x0400169B RID: 5787
		public VRTK_DashTeleport_UnityEvents.DashTeleportEvent OnWillDashThruObjects = new VRTK_DashTeleport_UnityEvents.DashTeleportEvent();

		// Token: 0x0400169C RID: 5788
		public VRTK_DashTeleport_UnityEvents.DashTeleportEvent OnDashedThruObjects = new VRTK_DashTeleport_UnityEvents.DashTeleportEvent();

		// Token: 0x02000625 RID: 1573
		[Serializable]
		public sealed class DashTeleportEvent : UnityEvent<object, DashTeleportEventArgs>
		{
		}
	}
}
