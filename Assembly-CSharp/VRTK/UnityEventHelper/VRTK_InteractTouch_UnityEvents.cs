using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200032A RID: 810
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_InteractTouch_UnityEvents")]
	public sealed class VRTK_InteractTouch_UnityEvents : VRTK_UnityEvents<VRTK_InteractTouch>
	{
		// Token: 0x06001C8E RID: 7310 RVA: 0x000939C0 File Offset: 0x00091BC0
		protected override void AddListeners(VRTK_InteractTouch component)
		{
			component.ControllerStartTouchInteractableObject += this.ControllerStartTouchInteractableObject;
			component.ControllerTouchInteractableObject += this.ControllerTouchInteractableObject;
			component.ControllerStartUntouchInteractableObject += this.ControllerStartUntouchInteractableObject;
			component.ControllerUntouchInteractableObject += this.ControllerUntouchInteractableObject;
			component.ControllerRigidbodyActivated += this.ControllerRigidbodyActivated;
			component.ControllerRigidbodyDeactivated += this.ControllerRigidbodyDeactivated;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00093A3C File Offset: 0x00091C3C
		protected override void RemoveListeners(VRTK_InteractTouch component)
		{
			component.ControllerStartTouchInteractableObject -= this.ControllerStartTouchInteractableObject;
			component.ControllerTouchInteractableObject -= this.ControllerTouchInteractableObject;
			component.ControllerStartUntouchInteractableObject -= this.ControllerStartUntouchInteractableObject;
			component.ControllerUntouchInteractableObject -= this.ControllerUntouchInteractableObject;
			component.ControllerRigidbodyActivated -= this.ControllerRigidbodyActivated;
			component.ControllerRigidbodyDeactivated -= this.ControllerRigidbodyDeactivated;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00093AB5 File Offset: 0x00091CB5
		private void ControllerStartTouchInteractableObject(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerStartTouchInteractableObject.Invoke(o, e);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00093AC4 File Offset: 0x00091CC4
		private void ControllerTouchInteractableObject(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerTouchInteractableObject.Invoke(o, e);
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00093AD3 File Offset: 0x00091CD3
		private void ControllerStartUntouchInteractableObject(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerStartUntouchInteractableObject.Invoke(o, e);
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00093AE2 File Offset: 0x00091CE2
		private void ControllerUntouchInteractableObject(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerUntouchInteractableObject.Invoke(o, e);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00093AF1 File Offset: 0x00091CF1
		private void ControllerRigidbodyActivated(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerRigidbodyActivated.Invoke(o, e);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00093B00 File Offset: 0x00091D00
		private void ControllerRigidbodyDeactivated(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerRigidbodyDeactivated.Invoke(o, e);
		}

		// Token: 0x040016C1 RID: 5825
		public VRTK_InteractTouch_UnityEvents.ObjectInteractEvent OnControllerStartTouchInteractableObject = new VRTK_InteractTouch_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016C2 RID: 5826
		public VRTK_InteractTouch_UnityEvents.ObjectInteractEvent OnControllerTouchInteractableObject = new VRTK_InteractTouch_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016C3 RID: 5827
		public VRTK_InteractTouch_UnityEvents.ObjectInteractEvent OnControllerStartUntouchInteractableObject = new VRTK_InteractTouch_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016C4 RID: 5828
		public VRTK_InteractTouch_UnityEvents.ObjectInteractEvent OnControllerUntouchInteractableObject = new VRTK_InteractTouch_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016C5 RID: 5829
		public VRTK_InteractTouch_UnityEvents.ObjectInteractEvent OnControllerRigidbodyActivated = new VRTK_InteractTouch_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016C6 RID: 5830
		public VRTK_InteractTouch_UnityEvents.ObjectInteractEvent OnControllerRigidbodyDeactivated = new VRTK_InteractTouch_UnityEvents.ObjectInteractEvent();

		// Token: 0x0200062E RID: 1582
		[Serializable]
		public sealed class ObjectInteractEvent : UnityEvent<object, ObjectInteractEventArgs>
		{
		}
	}
}
