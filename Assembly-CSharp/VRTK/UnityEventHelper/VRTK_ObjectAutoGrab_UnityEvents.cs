using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200032D RID: 813
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_ObjectAutoGrab_UnityEvents")]
	public sealed class VRTK_ObjectAutoGrab_UnityEvents : VRTK_UnityEvents<VRTK_ObjectAutoGrab>
	{
		// Token: 0x06001CAD RID: 7341 RVA: 0x00093FAD File Offset: 0x000921AD
		protected override void AddListeners(VRTK_ObjectAutoGrab component)
		{
			component.ObjectAutoGrabCompleted += this.ObjectAutoGrabCompleted;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00093FC1 File Offset: 0x000921C1
		protected override void RemoveListeners(VRTK_ObjectAutoGrab component)
		{
			component.ObjectAutoGrabCompleted -= this.ObjectAutoGrabCompleted;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x00093FD5 File Offset: 0x000921D5
		private void ObjectAutoGrabCompleted(object o)
		{
			this.OnObjectAutoGrabCompleted.Invoke(o);
		}

		// Token: 0x040016D7 RID: 5847
		public VRTK_ObjectAutoGrab_UnityEvents.ObjectAutoGrabEvent OnObjectAutoGrabCompleted = new VRTK_ObjectAutoGrab_UnityEvents.ObjectAutoGrabEvent();

		// Token: 0x02000631 RID: 1585
		[Serializable]
		public sealed class ObjectAutoGrabEvent : UnityEvent<object>
		{
		}
	}
}
