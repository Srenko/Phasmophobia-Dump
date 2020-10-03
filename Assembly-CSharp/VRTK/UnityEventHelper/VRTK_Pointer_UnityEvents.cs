using System;
using UnityEngine;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000333 RID: 819
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_Pointer_UnityEvents")]
	public sealed class VRTK_Pointer_UnityEvents : VRTK_UnityEvents<VRTK_Pointer>
	{
		// Token: 0x06001CC9 RID: 7369 RVA: 0x00094260 File Offset: 0x00092460
		protected override void AddListeners(VRTK_Pointer component)
		{
			component.ActivationButtonPressed += this.ActivationButtonPressed;
			component.ActivationButtonReleased += this.ActivationButtonReleased;
			component.SelectionButtonPressed += this.SelectionButtonPressed;
			component.SelectionButtonReleased += this.SelectionButtonReleased;
			component.PointerStateValid += this.PointerStateValid;
			component.PointerStateInvalid += this.PointerStateInvalid;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x000942DC File Offset: 0x000924DC
		protected override void RemoveListeners(VRTK_Pointer component)
		{
			component.ActivationButtonPressed -= this.ActivationButtonPressed;
			component.ActivationButtonReleased -= this.ActivationButtonReleased;
			component.SelectionButtonPressed -= this.SelectionButtonPressed;
			component.SelectionButtonReleased -= this.SelectionButtonReleased;
			component.PointerStateValid -= this.PointerStateValid;
			component.PointerStateInvalid -= this.PointerStateInvalid;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x00094355 File Offset: 0x00092555
		private void ActivationButtonPressed(object o, ControllerInteractionEventArgs e)
		{
			this.OnActivationButtonPressed.Invoke(o, e);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00094364 File Offset: 0x00092564
		private void ActivationButtonReleased(object o, ControllerInteractionEventArgs e)
		{
			this.OnActivationButtonReleased.Invoke(o, e);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00094373 File Offset: 0x00092573
		private void SelectionButtonPressed(object o, ControllerInteractionEventArgs e)
		{
			this.OnSelectionButtonPressed.Invoke(o, e);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x00094382 File Offset: 0x00092582
		private void SelectionButtonReleased(object o, ControllerInteractionEventArgs e)
		{
			this.OnSelectionButtonReleased.Invoke(o, e);
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x00094391 File Offset: 0x00092591
		private void PointerStateValid(object o, DestinationMarkerEventArgs e)
		{
			this.OnPointerStateValid.Invoke(o, e);
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000943A0 File Offset: 0x000925A0
		private void PointerStateInvalid(object o, DestinationMarkerEventArgs e)
		{
			this.OnPointerStateInvalid.Invoke(o, e);
		}

		// Token: 0x040016E1 RID: 5857
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnActivationButtonPressed = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x040016E2 RID: 5858
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnActivationButtonReleased = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x040016E3 RID: 5859
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnSelectionButtonPressed = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x040016E4 RID: 5860
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnSelectionButtonReleased = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x040016E5 RID: 5861
		public VRTK_DestinationMarker_UnityEvents.DestinationMarkerEvent OnPointerStateValid = new VRTK_DestinationMarker_UnityEvents.DestinationMarkerEvent();

		// Token: 0x040016E6 RID: 5862
		public VRTK_DestinationMarker_UnityEvents.DestinationMarkerEvent OnPointerStateInvalid = new VRTK_DestinationMarker_UnityEvents.DestinationMarkerEvent();
	}
}
