using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000324 RID: 804
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_HeadsetCollision_UnityEvents")]
	public sealed class VRTK_HeadsetCollision_UnityEvents : VRTK_UnityEvents<VRTK_HeadsetCollision>
	{
		// Token: 0x06001C61 RID: 7265 RVA: 0x00093268 File Offset: 0x00091468
		protected override void AddListeners(VRTK_HeadsetCollision component)
		{
			component.HeadsetCollisionDetect += this.HeadsetCollisionDetect;
			component.HeadsetCollisionEnded += this.HeadsetCollisionEnded;
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0009328E File Offset: 0x0009148E
		protected override void RemoveListeners(VRTK_HeadsetCollision component)
		{
			component.HeadsetCollisionDetect -= this.HeadsetCollisionDetect;
			component.HeadsetCollisionEnded -= this.HeadsetCollisionEnded;
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x000932B4 File Offset: 0x000914B4
		private void HeadsetCollisionDetect(object o, HeadsetCollisionEventArgs e)
		{
			this.OnHeadsetCollisionDetect.Invoke(o, e);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000932C3 File Offset: 0x000914C3
		private void HeadsetCollisionEnded(object o, HeadsetCollisionEventArgs e)
		{
			this.OnHeadsetCollisionEnded.Invoke(o, e);
		}

		// Token: 0x040016A6 RID: 5798
		public VRTK_HeadsetCollision_UnityEvents.HeadsetCollisionEvent OnHeadsetCollisionDetect = new VRTK_HeadsetCollision_UnityEvents.HeadsetCollisionEvent();

		// Token: 0x040016A7 RID: 5799
		public VRTK_HeadsetCollision_UnityEvents.HeadsetCollisionEvent OnHeadsetCollisionEnded = new VRTK_HeadsetCollision_UnityEvents.HeadsetCollisionEvent();

		// Token: 0x02000628 RID: 1576
		[Serializable]
		public sealed class HeadsetCollisionEvent : UnityEvent<object, HeadsetCollisionEventArgs>
		{
		}
	}
}
