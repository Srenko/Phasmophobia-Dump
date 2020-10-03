using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000329 RID: 809
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_InteractHaptics_UnityEvents")]
	public sealed class VRTK_InteractHaptics_UnityEvents : VRTK_UnityEvents<VRTK_InteractHaptics>
	{
		// Token: 0x06001C88 RID: 7304 RVA: 0x000938F9 File Offset: 0x00091AF9
		protected override void AddListeners(VRTK_InteractHaptics component)
		{
			component.InteractHapticsTouched += this.InteractHapticsTouched;
			component.InteractHapticsGrabbed += this.InteractHapticsGrabbed;
			component.InteractHapticsUsed += this.InteractHapticsUsed;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x00093931 File Offset: 0x00091B31
		protected override void RemoveListeners(VRTK_InteractHaptics component)
		{
			component.InteractHapticsTouched -= this.InteractHapticsTouched;
			component.InteractHapticsGrabbed -= this.InteractHapticsGrabbed;
			component.InteractHapticsUsed -= this.InteractHapticsUsed;
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00093969 File Offset: 0x00091B69
		private void InteractHapticsTouched(object o, InteractHapticsEventArgs e)
		{
			this.OnInteractHapticsTouched.Invoke(o, e);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00093978 File Offset: 0x00091B78
		private void InteractHapticsGrabbed(object o, InteractHapticsEventArgs e)
		{
			this.OnInteractHapticsGrabbed.Invoke(o, e);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00093987 File Offset: 0x00091B87
		private void InteractHapticsUsed(object o, InteractHapticsEventArgs e)
		{
			this.OnInteractHapticsUsed.Invoke(o, e);
		}

		// Token: 0x040016BE RID: 5822
		public VRTK_InteractHaptics_UnityEvents.InteractHapticsEvent OnInteractHapticsTouched = new VRTK_InteractHaptics_UnityEvents.InteractHapticsEvent();

		// Token: 0x040016BF RID: 5823
		public VRTK_InteractHaptics_UnityEvents.InteractHapticsEvent OnInteractHapticsGrabbed = new VRTK_InteractHaptics_UnityEvents.InteractHapticsEvent();

		// Token: 0x040016C0 RID: 5824
		public VRTK_InteractHaptics_UnityEvents.InteractHapticsEvent OnInteractHapticsUsed = new VRTK_InteractHaptics_UnityEvents.InteractHapticsEvent();

		// Token: 0x0200062D RID: 1581
		[Serializable]
		public sealed class InteractHapticsEvent : UnityEvent<object, InteractHapticsEventArgs>
		{
		}
	}
}
