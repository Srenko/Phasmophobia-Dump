using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000325 RID: 805
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_HeadsetControllerAware_UnityEvents")]
	public sealed class VRTK_HeadsetControllerAware_UnityEvents : VRTK_UnityEvents<VRTK_HeadsetControllerAware>
	{
		// Token: 0x06001C66 RID: 7270 RVA: 0x000932F0 File Offset: 0x000914F0
		protected override void AddListeners(VRTK_HeadsetControllerAware component)
		{
			component.ControllerObscured += this.ControllerObscured;
			component.ControllerUnobscured += this.ControllerUnobscured;
			component.ControllerGlanceEnter += this.ControllerGlanceEnter;
			component.ControllerGlanceExit += this.ControllerGlanceExit;
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00093348 File Offset: 0x00091548
		protected override void RemoveListeners(VRTK_HeadsetControllerAware component)
		{
			component.ControllerObscured -= this.ControllerObscured;
			component.ControllerUnobscured -= this.ControllerUnobscured;
			component.ControllerGlanceEnter -= this.ControllerGlanceEnter;
			component.ControllerGlanceExit -= this.ControllerGlanceExit;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0009339D File Offset: 0x0009159D
		private void ControllerObscured(object o, HeadsetControllerAwareEventArgs e)
		{
			this.OnControllerObscured.Invoke(o, e);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000933AC File Offset: 0x000915AC
		private void ControllerUnobscured(object o, HeadsetControllerAwareEventArgs e)
		{
			this.OnControllerUnobscured.Invoke(o, e);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x000933BB File Offset: 0x000915BB
		private void ControllerGlanceEnter(object o, HeadsetControllerAwareEventArgs e)
		{
			this.OnControllerGlanceEnter.Invoke(o, e);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x000933CA File Offset: 0x000915CA
		private void ControllerGlanceExit(object o, HeadsetControllerAwareEventArgs e)
		{
			this.OnControllerGlanceExit.Invoke(o, e);
		}

		// Token: 0x040016A8 RID: 5800
		public VRTK_HeadsetControllerAware_UnityEvents.HeadsetControllerAwareEvent OnControllerObscured = new VRTK_HeadsetControllerAware_UnityEvents.HeadsetControllerAwareEvent();

		// Token: 0x040016A9 RID: 5801
		public VRTK_HeadsetControllerAware_UnityEvents.HeadsetControllerAwareEvent OnControllerUnobscured = new VRTK_HeadsetControllerAware_UnityEvents.HeadsetControllerAwareEvent();

		// Token: 0x040016AA RID: 5802
		public VRTK_HeadsetControllerAware_UnityEvents.HeadsetControllerAwareEvent OnControllerGlanceEnter = new VRTK_HeadsetControllerAware_UnityEvents.HeadsetControllerAwareEvent();

		// Token: 0x040016AB RID: 5803
		public VRTK_HeadsetControllerAware_UnityEvents.HeadsetControllerAwareEvent OnControllerGlanceExit = new VRTK_HeadsetControllerAware_UnityEvents.HeadsetControllerAwareEvent();

		// Token: 0x02000629 RID: 1577
		[Serializable]
		public sealed class HeadsetControllerAwareEvent : UnityEvent<object, HeadsetControllerAwareEventArgs>
		{
		}
	}
}
