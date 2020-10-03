using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000319 RID: 793
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_BasicTeleport_UnityEvents")]
	public sealed class VRTK_BasicTeleport_UnityEvents : VRTK_UnityEvents<VRTK_BasicTeleport>
	{
		// Token: 0x06001BEF RID: 7151 RVA: 0x00091DCB File Offset: 0x0008FFCB
		protected override void AddListeners(VRTK_BasicTeleport component)
		{
			component.Teleporting += this.Teleporting;
			component.Teleported += this.Teleported;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00091DF1 File Offset: 0x0008FFF1
		protected override void RemoveListeners(VRTK_BasicTeleport component)
		{
			component.Teleporting -= this.Teleporting;
			component.Teleported -= this.Teleported;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00091E17 File Offset: 0x00090017
		private void Teleporting(object o, DestinationMarkerEventArgs e)
		{
			this.OnTeleporting.Invoke(o, e);
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00091E26 File Offset: 0x00090026
		private void Teleported(object o, DestinationMarkerEventArgs e)
		{
			this.OnTeleported.Invoke(o, e);
		}

		// Token: 0x04001655 RID: 5717
		public VRTK_BasicTeleport_UnityEvents.TeleportEvent OnTeleporting = new VRTK_BasicTeleport_UnityEvents.TeleportEvent();

		// Token: 0x04001656 RID: 5718
		public VRTK_BasicTeleport_UnityEvents.TeleportEvent OnTeleported = new VRTK_BasicTeleport_UnityEvents.TeleportEvent();

		// Token: 0x0200061D RID: 1565
		[Serializable]
		public sealed class TeleportEvent : UnityEvent<object, DestinationMarkerEventArgs>
		{
		}
	}
}
