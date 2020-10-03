using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000337 RID: 823
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_SnapDropZone_UnityEvents")]
	public sealed class VRTK_SnapDropZone_UnityEvents : VRTK_UnityEvents<VRTK_SnapDropZone>
	{
		// Token: 0x06001CDF RID: 7391 RVA: 0x00094524 File Offset: 0x00092724
		protected override void AddListeners(VRTK_SnapDropZone component)
		{
			component.ObjectEnteredSnapDropZone += this.ObjectEnteredSnapDropZone;
			component.ObjectExitedSnapDropZone += this.ObjectExitedSnapDropZone;
			component.ObjectSnappedToDropZone += this.ObjectSnappedToDropZone;
			component.ObjectUnsnappedFromDropZone += this.ObjectUnsnappedFromDropZone;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0009457C File Offset: 0x0009277C
		protected override void RemoveListeners(VRTK_SnapDropZone component)
		{
			component.ObjectEnteredSnapDropZone -= this.ObjectEnteredSnapDropZone;
			component.ObjectExitedSnapDropZone -= this.ObjectExitedSnapDropZone;
			component.ObjectSnappedToDropZone -= this.ObjectSnappedToDropZone;
			component.ObjectUnsnappedFromDropZone -= this.ObjectUnsnappedFromDropZone;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x000945D1 File Offset: 0x000927D1
		private void ObjectEnteredSnapDropZone(object o, SnapDropZoneEventArgs e)
		{
			this.OnObjectEnteredSnapDropZone.Invoke(o, e);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x000945E0 File Offset: 0x000927E0
		private void ObjectExitedSnapDropZone(object o, SnapDropZoneEventArgs e)
		{
			this.OnObjectExitedSnapDropZone.Invoke(o, e);
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x000945EF File Offset: 0x000927EF
		private void ObjectSnappedToDropZone(object o, SnapDropZoneEventArgs e)
		{
			this.OnObjectSnappedToDropZone.Invoke(o, e);
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x000945FE File Offset: 0x000927FE
		private void ObjectUnsnappedFromDropZone(object o, SnapDropZoneEventArgs e)
		{
			this.OnObjectUnsnappedFromDropZone.Invoke(o, e);
		}

		// Token: 0x040016EB RID: 5867
		public VRTK_SnapDropZone_UnityEvents.SnapDropZoneEvent OnObjectEnteredSnapDropZone = new VRTK_SnapDropZone_UnityEvents.SnapDropZoneEvent();

		// Token: 0x040016EC RID: 5868
		public VRTK_SnapDropZone_UnityEvents.SnapDropZoneEvent OnObjectExitedSnapDropZone = new VRTK_SnapDropZone_UnityEvents.SnapDropZoneEvent();

		// Token: 0x040016ED RID: 5869
		public VRTK_SnapDropZone_UnityEvents.SnapDropZoneEvent OnObjectSnappedToDropZone = new VRTK_SnapDropZone_UnityEvents.SnapDropZoneEvent();

		// Token: 0x040016EE RID: 5870
		public VRTK_SnapDropZone_UnityEvents.SnapDropZoneEvent OnObjectUnsnappedFromDropZone = new VRTK_SnapDropZone_UnityEvents.SnapDropZoneEvent();

		// Token: 0x0200063A RID: 1594
		[Serializable]
		public sealed class SnapDropZoneEvent : UnityEvent<object, SnapDropZoneEventArgs>
		{
		}
	}
}
