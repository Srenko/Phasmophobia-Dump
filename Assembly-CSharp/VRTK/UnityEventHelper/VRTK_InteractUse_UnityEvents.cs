using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200032B RID: 811
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_InteractUse_UnityEvents")]
	public sealed class VRTK_InteractUse_UnityEvents : VRTK_UnityEvents<VRTK_InteractUse>
	{
		// Token: 0x06001C97 RID: 7319 RVA: 0x00093B68 File Offset: 0x00091D68
		protected override void AddListeners(VRTK_InteractUse component)
		{
			component.ControllerStartUseInteractableObject += this.ControllerStartUseInteractableObject;
			component.ControllerUseInteractableObject += this.ControllerUseInteractableObject;
			component.ControllerStartUnuseInteractableObject += this.ControllerStartUnuseInteractableObject;
			component.ControllerUnuseInteractableObject += this.ControllerUnuseInteractableObject;
			component.UseButtonPressed += this.UseButtonPressed;
			component.UseButtonReleased += this.UseButtonReleased;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x00093BE4 File Offset: 0x00091DE4
		protected override void RemoveListeners(VRTK_InteractUse component)
		{
			component.ControllerStartUseInteractableObject -= this.ControllerStartUseInteractableObject;
			component.ControllerUseInteractableObject -= this.ControllerUseInteractableObject;
			component.ControllerStartUnuseInteractableObject -= this.ControllerStartUnuseInteractableObject;
			component.ControllerUnuseInteractableObject -= this.ControllerUnuseInteractableObject;
			component.UseButtonPressed -= this.UseButtonPressed;
			component.UseButtonReleased -= this.UseButtonReleased;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00093C5D File Offset: 0x00091E5D
		private void ControllerStartUseInteractableObject(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerStartUseInteractableObject.Invoke(o, e);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00093C6C File Offset: 0x00091E6C
		private void ControllerUseInteractableObject(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerUseInteractableObject.Invoke(o, e);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00093C7B File Offset: 0x00091E7B
		private void ControllerStartUnuseInteractableObject(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerStartUnuseInteractableObject.Invoke(o, e);
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00093C8A File Offset: 0x00091E8A
		private void ControllerUnuseInteractableObject(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerUnuseInteractableObject.Invoke(o, e);
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00093C99 File Offset: 0x00091E99
		private void UseButtonPressed(object o, ControllerInteractionEventArgs e)
		{
			this.OnUseButtonPressed.Invoke(o, e);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00093CA8 File Offset: 0x00091EA8
		private void UseButtonReleased(object o, ControllerInteractionEventArgs e)
		{
			this.OnUseButtonReleased.Invoke(o, e);
		}

		// Token: 0x040016C7 RID: 5831
		public VRTK_InteractUse_UnityEvents.ObjectInteractEvent OnControllerStartUseInteractableObject = new VRTK_InteractUse_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016C8 RID: 5832
		public VRTK_InteractUse_UnityEvents.ObjectInteractEvent OnControllerUseInteractableObject = new VRTK_InteractUse_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016C9 RID: 5833
		public VRTK_InteractUse_UnityEvents.ObjectInteractEvent OnControllerStartUnuseInteractableObject = new VRTK_InteractUse_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016CA RID: 5834
		public VRTK_InteractUse_UnityEvents.ObjectInteractEvent OnControllerUnuseInteractableObject = new VRTK_InteractUse_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016CB RID: 5835
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnUseButtonPressed = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x040016CC RID: 5836
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnUseButtonReleased = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0200062F RID: 1583
		[Serializable]
		public sealed class ObjectInteractEvent : UnityEvent<object, ObjectInteractEventArgs>
		{
		}
	}
}
