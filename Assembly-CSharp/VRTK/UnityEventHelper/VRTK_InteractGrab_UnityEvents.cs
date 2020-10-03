using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000328 RID: 808
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_InteractGrab_UnityEvents")]
	public sealed class VRTK_InteractGrab_UnityEvents : VRTK_UnityEvents<VRTK_InteractGrab>
	{
		// Token: 0x06001C7F RID: 7295 RVA: 0x00093754 File Offset: 0x00091954
		protected override void AddListeners(VRTK_InteractGrab component)
		{
			component.ControllerStartGrabInteractableObject += this.ControllerStartGrabInteractableObject;
			component.ControllerGrabInteractableObject += this.ControllerGrabInteractableObject;
			component.ControllerStartUngrabInteractableObject += this.ControllerStartUngrabInteractableObject;
			component.ControllerUngrabInteractableObject += this.ControllerUngrabInteractableObject;
			component.GrabButtonPressed += this.GrabButtonPressed;
			component.GrabButtonReleased += this.GrabButtonReleased;
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x000937D0 File Offset: 0x000919D0
		protected override void RemoveListeners(VRTK_InteractGrab component)
		{
			component.ControllerStartGrabInteractableObject -= this.ControllerStartGrabInteractableObject;
			component.ControllerGrabInteractableObject -= this.ControllerGrabInteractableObject;
			component.ControllerStartUngrabInteractableObject -= this.ControllerStartUngrabInteractableObject;
			component.ControllerUngrabInteractableObject -= this.ControllerUngrabInteractableObject;
			component.GrabButtonPressed -= this.GrabButtonPressed;
			component.GrabButtonReleased -= this.GrabButtonReleased;
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x00093849 File Offset: 0x00091A49
		private void ControllerStartGrabInteractableObject(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerStartGrabInteractableObject.Invoke(o, e);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x00093858 File Offset: 0x00091A58
		private void ControllerGrabInteractableObject(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerGrabInteractableObject.Invoke(o, e);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x00093867 File Offset: 0x00091A67
		private void ControllerStartUngrabInteractableObject(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerStartUngrabInteractableObject.Invoke(o, e);
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x00093876 File Offset: 0x00091A76
		private void ControllerUngrabInteractableObject(object o, ObjectInteractEventArgs e)
		{
			this.OnControllerUngrabInteractableObject.Invoke(o, e);
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00093885 File Offset: 0x00091A85
		private void GrabButtonPressed(object o, ControllerInteractionEventArgs e)
		{
			this.OnGrabButtonPressed.Invoke(o, e);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x00093894 File Offset: 0x00091A94
		private void GrabButtonReleased(object o, ControllerInteractionEventArgs e)
		{
			this.OnGrabButtonReleased.Invoke(o, e);
		}

		// Token: 0x040016B8 RID: 5816
		public VRTK_InteractGrab_UnityEvents.ObjectInteractEvent OnControllerStartGrabInteractableObject = new VRTK_InteractGrab_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016B9 RID: 5817
		public VRTK_InteractGrab_UnityEvents.ObjectInteractEvent OnControllerGrabInteractableObject = new VRTK_InteractGrab_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016BA RID: 5818
		public VRTK_InteractGrab_UnityEvents.ObjectInteractEvent OnControllerStartUngrabInteractableObject = new VRTK_InteractGrab_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016BB RID: 5819
		public VRTK_InteractGrab_UnityEvents.ObjectInteractEvent OnControllerUngrabInteractableObject = new VRTK_InteractGrab_UnityEvents.ObjectInteractEvent();

		// Token: 0x040016BC RID: 5820
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnGrabButtonPressed = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x040016BD RID: 5821
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnGrabButtonReleased = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0200062C RID: 1580
		[Serializable]
		public sealed class ObjectInteractEvent : UnityEvent<object, ObjectInteractEventArgs>
		{
		}
	}
}
