using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000326 RID: 806
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_HeadsetFade_UnityEvents")]
	public sealed class VRTK_HeadsetFade_UnityEvents : VRTK_UnityEvents<VRTK_HeadsetFade>
	{
		// Token: 0x06001C6D RID: 7277 RVA: 0x00093410 File Offset: 0x00091610
		protected override void AddListeners(VRTK_HeadsetFade component)
		{
			component.HeadsetFadeStart += this.HeadsetFadeStart;
			component.HeadsetFadeComplete += this.HeadsetFadeComplete;
			component.HeadsetUnfadeStart += this.HeadsetUnfadeStart;
			component.HeadsetUnfadeComplete += this.HeadsetUnfadeComplete;
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00093468 File Offset: 0x00091668
		protected override void RemoveListeners(VRTK_HeadsetFade component)
		{
			component.HeadsetFadeStart -= this.HeadsetFadeStart;
			component.HeadsetFadeComplete -= this.HeadsetFadeComplete;
			component.HeadsetUnfadeStart -= this.HeadsetUnfadeStart;
			component.HeadsetUnfadeComplete -= this.HeadsetUnfadeComplete;
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000934BD File Offset: 0x000916BD
		private void HeadsetFadeStart(object o, HeadsetFadeEventArgs e)
		{
			this.OnHeadsetFadeStart.Invoke(o, e);
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x000934CC File Offset: 0x000916CC
		private void HeadsetFadeComplete(object o, HeadsetFadeEventArgs e)
		{
			this.OnHeadsetFadeComplete.Invoke(o, e);
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x000934DB File Offset: 0x000916DB
		private void HeadsetUnfadeStart(object o, HeadsetFadeEventArgs e)
		{
			this.OnHeadsetUnfadeStart.Invoke(o, e);
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000934EA File Offset: 0x000916EA
		private void HeadsetUnfadeComplete(object o, HeadsetFadeEventArgs e)
		{
			this.OnHeadsetUnfadeComplete.Invoke(o, e);
		}

		// Token: 0x040016AC RID: 5804
		public VRTK_HeadsetFade_UnityEvents.HeadsetFadeEvent OnHeadsetFadeStart = new VRTK_HeadsetFade_UnityEvents.HeadsetFadeEvent();

		// Token: 0x040016AD RID: 5805
		public VRTK_HeadsetFade_UnityEvents.HeadsetFadeEvent OnHeadsetFadeComplete = new VRTK_HeadsetFade_UnityEvents.HeadsetFadeEvent();

		// Token: 0x040016AE RID: 5806
		public VRTK_HeadsetFade_UnityEvents.HeadsetFadeEvent OnHeadsetUnfadeStart = new VRTK_HeadsetFade_UnityEvents.HeadsetFadeEvent();

		// Token: 0x040016AF RID: 5807
		public VRTK_HeadsetFade_UnityEvents.HeadsetFadeEvent OnHeadsetUnfadeComplete = new VRTK_HeadsetFade_UnityEvents.HeadsetFadeEvent();

		// Token: 0x0200062A RID: 1578
		[Serializable]
		public sealed class HeadsetFadeEvent : UnityEvent<object, HeadsetFadeEventArgs>
		{
		}
	}
}
