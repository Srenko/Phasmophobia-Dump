using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000327 RID: 807
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_InteractControllerAppearance_UnityEvents")]
	public sealed class VRTK_InteractControllerAppearance_UnityEvents : VRTK_UnityEvents<VRTK_InteractControllerAppearance>
	{
		// Token: 0x06001C74 RID: 7284 RVA: 0x00093530 File Offset: 0x00091730
		protected override void AddListeners(VRTK_InteractControllerAppearance component)
		{
			component.ControllerHidden += this.ControllerHidden;
			component.ControllerVisible += this.ControllerVisible;
			component.HiddenOnTouch += this.HiddenOnTouch;
			component.VisibleOnTouch += this.VisibleOnTouch;
			component.HiddenOnGrab += this.HiddenOnGrab;
			component.VisibleOnGrab += this.VisibleOnGrab;
			component.HiddenOnUse += this.HiddenOnUse;
			component.VisibleOnUse += this.VisibleOnUse;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x000935D0 File Offset: 0x000917D0
		protected override void RemoveListeners(VRTK_InteractControllerAppearance component)
		{
			component.ControllerHidden -= this.ControllerHidden;
			component.ControllerVisible -= this.ControllerVisible;
			component.HiddenOnTouch -= this.HiddenOnTouch;
			component.VisibleOnTouch -= this.VisibleOnTouch;
			component.HiddenOnGrab -= this.HiddenOnGrab;
			component.VisibleOnGrab -= this.VisibleOnGrab;
			component.HiddenOnUse -= this.HiddenOnUse;
			component.VisibleOnUse -= this.VisibleOnUse;
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0009366D File Offset: 0x0009186D
		private void ControllerHidden(object o, InteractControllerAppearanceEventArgs e)
		{
			this.OnControllerHidden.Invoke(o, e);
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0009367C File Offset: 0x0009187C
		private void ControllerVisible(object o, InteractControllerAppearanceEventArgs e)
		{
			this.OnControllerVisible.Invoke(o, e);
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0009368B File Offset: 0x0009188B
		private void HiddenOnTouch(object o, InteractControllerAppearanceEventArgs e)
		{
			this.OnHiddenOnTouch.Invoke(o, e);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x0009369A File Offset: 0x0009189A
		private void VisibleOnTouch(object o, InteractControllerAppearanceEventArgs e)
		{
			this.OnVisibleOnTouch.Invoke(o, e);
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000936A9 File Offset: 0x000918A9
		private void HiddenOnGrab(object o, InteractControllerAppearanceEventArgs e)
		{
			this.OnHiddenOnGrab.Invoke(o, e);
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000936B8 File Offset: 0x000918B8
		private void VisibleOnGrab(object o, InteractControllerAppearanceEventArgs e)
		{
			this.OnVisibleOnGrab.Invoke(o, e);
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x000936C7 File Offset: 0x000918C7
		private void HiddenOnUse(object o, InteractControllerAppearanceEventArgs e)
		{
			this.OnHiddenOnUse.Invoke(o, e);
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x000936D6 File Offset: 0x000918D6
		private void VisibleOnUse(object o, InteractControllerAppearanceEventArgs e)
		{
			this.OnVisibleOnUse.Invoke(o, e);
		}

		// Token: 0x040016B0 RID: 5808
		public VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent OnControllerHidden = new VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent();

		// Token: 0x040016B1 RID: 5809
		public VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent OnControllerVisible = new VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent();

		// Token: 0x040016B2 RID: 5810
		public VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent OnHiddenOnTouch = new VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent();

		// Token: 0x040016B3 RID: 5811
		public VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent OnVisibleOnTouch = new VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent();

		// Token: 0x040016B4 RID: 5812
		public VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent OnHiddenOnGrab = new VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent();

		// Token: 0x040016B5 RID: 5813
		public VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent OnVisibleOnGrab = new VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent();

		// Token: 0x040016B6 RID: 5814
		public VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent OnHiddenOnUse = new VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent();

		// Token: 0x040016B7 RID: 5815
		public VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent OnVisibleOnUse = new VRTK_InteractControllerAppearance_UnityEvents.InteractControllerAppearanceEvent();

		// Token: 0x0200062B RID: 1579
		[Serializable]
		public sealed class InteractControllerAppearanceEvent : UnityEvent<object, InteractControllerAppearanceEventArgs>
		{
		}
	}
}
