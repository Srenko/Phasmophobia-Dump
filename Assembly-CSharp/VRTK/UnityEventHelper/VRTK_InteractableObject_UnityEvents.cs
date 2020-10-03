using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200032C RID: 812
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_InteractableObject_UnityEvents")]
	public sealed class VRTK_InteractableObject_UnityEvents : VRTK_UnityEvents<VRTK_InteractableObject>
	{
		// Token: 0x06001CA0 RID: 7328 RVA: 0x00093D10 File Offset: 0x00091F10
		protected override void AddListeners(VRTK_InteractableObject component)
		{
			component.InteractableObjectTouched += this.Touch;
			component.InteractableObjectUntouched += this.UnTouch;
			component.InteractableObjectGrabbed += this.Grab;
			component.InteractableObjectUngrabbed += this.UnGrab;
			component.InteractableObjectUsed += this.Use;
			component.InteractableObjectUnused += this.Unuse;
			component.InteractableObjectEnteredSnapDropZone += this.EnterSnapDropZone;
			component.InteractableObjectExitedSnapDropZone += this.ExitSnapDropZone;
			component.InteractableObjectSnappedToDropZone += this.SnapToDropZone;
			component.InteractableObjectUnsnappedFromDropZone += this.UnsnapFromDropZone;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00093DD4 File Offset: 0x00091FD4
		protected override void RemoveListeners(VRTK_InteractableObject component)
		{
			component.InteractableObjectTouched -= this.Touch;
			component.InteractableObjectUntouched -= this.UnTouch;
			component.InteractableObjectGrabbed -= this.Grab;
			component.InteractableObjectUngrabbed -= this.UnGrab;
			component.InteractableObjectUsed -= this.Use;
			component.InteractableObjectUnused -= this.Unuse;
			component.InteractableObjectEnteredSnapDropZone -= this.EnterSnapDropZone;
			component.InteractableObjectExitedSnapDropZone -= this.ExitSnapDropZone;
			component.InteractableObjectSnappedToDropZone -= this.SnapToDropZone;
			component.InteractableObjectUnsnappedFromDropZone -= this.UnsnapFromDropZone;
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00093E95 File Offset: 0x00092095
		private void Touch(object o, InteractableObjectEventArgs e)
		{
			this.OnTouch.Invoke(o, e);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00093EA4 File Offset: 0x000920A4
		private void UnTouch(object o, InteractableObjectEventArgs e)
		{
			this.OnUntouch.Invoke(o, e);
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00093EB3 File Offset: 0x000920B3
		private void Grab(object o, InteractableObjectEventArgs e)
		{
			this.OnGrab.Invoke(o, e);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00093EC2 File Offset: 0x000920C2
		private void UnGrab(object o, InteractableObjectEventArgs e)
		{
			this.OnUngrab.Invoke(o, e);
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00093ED1 File Offset: 0x000920D1
		private void Use(object o, InteractableObjectEventArgs e)
		{
			this.OnUse.Invoke(o, e);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00093EE0 File Offset: 0x000920E0
		private void Unuse(object o, InteractableObjectEventArgs e)
		{
			this.OnUnuse.Invoke(o, e);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00093EEF File Offset: 0x000920EF
		private void EnterSnapDropZone(object o, InteractableObjectEventArgs e)
		{
			this.OnEnterSnapDropZone.Invoke(o, e);
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x00093EFE File Offset: 0x000920FE
		private void ExitSnapDropZone(object o, InteractableObjectEventArgs e)
		{
			this.OnExitSnapDropZone.Invoke(o, e);
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00093F0D File Offset: 0x0009210D
		private void SnapToDropZone(object o, InteractableObjectEventArgs e)
		{
			this.OnSnapToDropZone.Invoke(o, e);
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00093F1C File Offset: 0x0009211C
		private void UnsnapFromDropZone(object o, InteractableObjectEventArgs e)
		{
			this.OnUnsnapFromDropZone.Invoke(o, e);
		}

		// Token: 0x040016CD RID: 5837
		public VRTK_InteractableObject_UnityEvents.InteractableObjectEvent OnTouch = new VRTK_InteractableObject_UnityEvents.InteractableObjectEvent();

		// Token: 0x040016CE RID: 5838
		public VRTK_InteractableObject_UnityEvents.InteractableObjectEvent OnUntouch = new VRTK_InteractableObject_UnityEvents.InteractableObjectEvent();

		// Token: 0x040016CF RID: 5839
		public VRTK_InteractableObject_UnityEvents.InteractableObjectEvent OnGrab = new VRTK_InteractableObject_UnityEvents.InteractableObjectEvent();

		// Token: 0x040016D0 RID: 5840
		public VRTK_InteractableObject_UnityEvents.InteractableObjectEvent OnUngrab = new VRTK_InteractableObject_UnityEvents.InteractableObjectEvent();

		// Token: 0x040016D1 RID: 5841
		public VRTK_InteractableObject_UnityEvents.InteractableObjectEvent OnUse = new VRTK_InteractableObject_UnityEvents.InteractableObjectEvent();

		// Token: 0x040016D2 RID: 5842
		public VRTK_InteractableObject_UnityEvents.InteractableObjectEvent OnUnuse = new VRTK_InteractableObject_UnityEvents.InteractableObjectEvent();

		// Token: 0x040016D3 RID: 5843
		public VRTK_InteractableObject_UnityEvents.InteractableObjectEvent OnEnterSnapDropZone = new VRTK_InteractableObject_UnityEvents.InteractableObjectEvent();

		// Token: 0x040016D4 RID: 5844
		public VRTK_InteractableObject_UnityEvents.InteractableObjectEvent OnExitSnapDropZone = new VRTK_InteractableObject_UnityEvents.InteractableObjectEvent();

		// Token: 0x040016D5 RID: 5845
		public VRTK_InteractableObject_UnityEvents.InteractableObjectEvent OnSnapToDropZone = new VRTK_InteractableObject_UnityEvents.InteractableObjectEvent();

		// Token: 0x040016D6 RID: 5846
		public VRTK_InteractableObject_UnityEvents.InteractableObjectEvent OnUnsnapFromDropZone = new VRTK_InteractableObject_UnityEvents.InteractableObjectEvent();

		// Token: 0x02000630 RID: 1584
		[Serializable]
		public sealed class InteractableObjectEvent : UnityEvent<object, InteractableObjectEventArgs>
		{
		}
	}
}
