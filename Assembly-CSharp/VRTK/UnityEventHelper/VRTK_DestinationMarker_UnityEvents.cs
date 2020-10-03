using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000322 RID: 802
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_DestinationMarker_UnityEvents")]
	public sealed class VRTK_DestinationMarker_UnityEvents : VRTK_UnityEvents<VRTK_DestinationMarker>
	{
		// Token: 0x06001C52 RID: 7250 RVA: 0x00092FF4 File Offset: 0x000911F4
		protected override void AddListeners(VRTK_DestinationMarker component)
		{
			component.DestinationMarkerEnter += this.DestinationMarkerEnter;
			component.DestinationMarkerExit += this.DestinationMarkerExit;
			component.DestinationMarkerHover += this.DestinationMarkerHover;
			component.DestinationMarkerSet += this.DestinationMarkerSet;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0009304C File Offset: 0x0009124C
		protected override void RemoveListeners(VRTK_DestinationMarker component)
		{
			component.DestinationMarkerEnter -= this.DestinationMarkerEnter;
			component.DestinationMarkerExit -= this.DestinationMarkerExit;
			component.DestinationMarkerHover -= this.DestinationMarkerHover;
			component.DestinationMarkerSet -= this.DestinationMarkerSet;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x000930A1 File Offset: 0x000912A1
		private void DestinationMarkerEnter(object o, DestinationMarkerEventArgs e)
		{
			this.OnDestinationMarkerEnter.Invoke(o, e);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x000930B0 File Offset: 0x000912B0
		private void DestinationMarkerExit(object o, DestinationMarkerEventArgs e)
		{
			this.OnDestinationMarkerExit.Invoke(o, e);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000930BF File Offset: 0x000912BF
		private void DestinationMarkerHover(object o, DestinationMarkerEventArgs e)
		{
			this.OnDestinationMarkerHover.Invoke(o, e);
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x000930CE File Offset: 0x000912CE
		private void DestinationMarkerSet(object o, DestinationMarkerEventArgs e)
		{
			this.OnDestinationMarkerSet.Invoke(o, e);
		}

		// Token: 0x0400169D RID: 5789
		public VRTK_DestinationMarker_UnityEvents.DestinationMarkerEvent OnDestinationMarkerEnter = new VRTK_DestinationMarker_UnityEvents.DestinationMarkerEvent();

		// Token: 0x0400169E RID: 5790
		public VRTK_DestinationMarker_UnityEvents.DestinationMarkerEvent OnDestinationMarkerExit = new VRTK_DestinationMarker_UnityEvents.DestinationMarkerEvent();

		// Token: 0x0400169F RID: 5791
		public VRTK_DestinationMarker_UnityEvents.DestinationMarkerEvent OnDestinationMarkerHover = new VRTK_DestinationMarker_UnityEvents.DestinationMarkerEvent();

		// Token: 0x040016A0 RID: 5792
		public VRTK_DestinationMarker_UnityEvents.DestinationMarkerEvent OnDestinationMarkerSet = new VRTK_DestinationMarker_UnityEvents.DestinationMarkerEvent();

		// Token: 0x02000626 RID: 1574
		[Serializable]
		public sealed class DestinationMarkerEvent : UnityEvent<object, DestinationMarkerEventArgs>
		{
		}
	}
}
