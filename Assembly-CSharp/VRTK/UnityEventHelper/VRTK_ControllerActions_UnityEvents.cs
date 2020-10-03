using System;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200031D RID: 797
	public sealed class VRTK_ControllerActions_UnityEvents : VRTK_UnityEvents<VRTK_ControllerActions>
	{
		// Token: 0x06001C0A RID: 7178 RVA: 0x000921C3 File Offset: 0x000903C3
		protected override void AddListeners(VRTK_ControllerActions component)
		{
			component.ControllerModelVisible += this.ControllerModelVisible;
			component.ControllerModelInvisible += this.ControllerModelInvisible;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x000921E9 File Offset: 0x000903E9
		protected override void RemoveListeners(VRTK_ControllerActions component)
		{
			component.ControllerModelVisible -= this.ControllerModelVisible;
			component.ControllerModelInvisible -= this.ControllerModelInvisible;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x0009220F File Offset: 0x0009040F
		private void ControllerModelVisible(object o, ControllerActionsEventArgs e)
		{
			this.OnControllerModelVisible.Invoke(o, e);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x0009221E File Offset: 0x0009041E
		private void ControllerModelInvisible(object o, ControllerActionsEventArgs e)
		{
			this.OnControllerModelInvisible.Invoke(o, e);
		}

		// Token: 0x04001664 RID: 5732
		public VRTK_ControllerActions_UnityEvents.ControllerActionsEvent OnControllerModelVisible = new VRTK_ControllerActions_UnityEvents.ControllerActionsEvent();

		// Token: 0x04001665 RID: 5733
		public VRTK_ControllerActions_UnityEvents.ControllerActionsEvent OnControllerModelInvisible = new VRTK_ControllerActions_UnityEvents.ControllerActionsEvent();

		// Token: 0x02000621 RID: 1569
		[Serializable]
		public sealed class ControllerActionsEvent : UnityEvent<object, ControllerActionsEventArgs>
		{
		}
	}
}
