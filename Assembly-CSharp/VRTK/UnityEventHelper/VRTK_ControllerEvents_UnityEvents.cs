using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200031E RID: 798
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_ControllerEvents_UnityEvents")]
	public sealed class VRTK_ControllerEvents_UnityEvents : VRTK_UnityEvents<VRTK_ControllerEvents>
	{
		// Token: 0x06001C0F RID: 7183 RVA: 0x0009224C File Offset: 0x0009044C
		protected override void AddListeners(VRTK_ControllerEvents component)
		{
			component.TriggerPressed += this.TriggerPressed;
			component.TriggerReleased += this.TriggerReleased;
			component.TriggerTouchStart += this.TriggerTouchStart;
			component.TriggerTouchEnd += this.TriggerTouchEnd;
			component.TriggerHairlineStart += this.TriggerHairlineStart;
			component.TriggerHairlineEnd += this.TriggerHairlineEnd;
			component.TriggerClicked += this.TriggerClicked;
			component.TriggerUnclicked += this.TriggerUnclicked;
			component.TriggerAxisChanged += this.TriggerAxisChanged;
			component.GripPressed += this.GripPressed;
			component.GripReleased += this.GripReleased;
			component.GripTouchStart += this.GripTouchStart;
			component.GripTouchEnd += this.GripTouchEnd;
			component.GripHairlineStart += this.GripHairlineStart;
			component.GripHairlineEnd += this.GripHairlineEnd;
			component.GripClicked += this.GripClicked;
			component.GripUnclicked += this.GripUnclicked;
			component.GripAxisChanged += this.GripAxisChanged;
			component.TouchpadPressed += this.TouchpadPressed;
			component.TouchpadReleased += this.TouchpadReleased;
			component.TouchpadTouchStart += this.TouchpadTouchStart;
			component.TouchpadTouchEnd += this.TouchpadTouchEnd;
			component.TouchpadAxisChanged += this.TouchpadAxisChanged;
			component.ButtonOnePressed += this.ButtonOnePressed;
			component.ButtonOneReleased += this.ButtonOneReleased;
			component.ButtonOneTouchStart += this.ButtonOneTouchStart;
			component.ButtonOneTouchEnd += this.ButtonOneTouchEnd;
			component.ButtonTwoPressed += this.ButtonTwoPressed;
			component.ButtonTwoReleased += this.ButtonTwoReleased;
			component.ButtonTwoTouchStart += this.ButtonTwoTouchStart;
			component.ButtonTwoTouchEnd += this.ButtonTwoTouchEnd;
			component.StartMenuPressed += this.StartMenuPressed;
			component.StartMenuReleased += this.StartMenuReleased;
			component.AliasPointerOn += this.AliasPointerOn;
			component.AliasPointerOff += this.AliasPointerOff;
			component.AliasPointerSet += this.AliasPointerSet;
			component.AliasGrabOn += this.AliasGrabOn;
			component.AliasGrabOff += this.AliasGrabOff;
			component.AliasUseOn += this.AliasUseOn;
			component.AliasUseOff += this.AliasUseOff;
			component.AliasUIClickOn += this.AliasUIClickOn;
			component.AliasUIClickOff += this.AliasUIClickOff;
			component.AliasMenuOn += this.AliasMenuOn;
			component.AliasMenuOff += this.AliasMenuOff;
			component.ControllerEnabled += this.ControllerEnabled;
			component.ControllerDisabled += this.ControllerDisabled;
			component.ControllerIndexChanged += this.ControllerIndexChanged;
			component.ControllerVisible += this.ControllerVisible;
			component.ControllerHidden += this.ControllerHidden;
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x000925CC File Offset: 0x000907CC
		protected override void RemoveListeners(VRTK_ControllerEvents component)
		{
			component.TriggerPressed -= this.TriggerPressed;
			component.TriggerReleased -= this.TriggerReleased;
			component.TriggerTouchStart -= this.TriggerTouchStart;
			component.TriggerTouchEnd -= this.TriggerTouchEnd;
			component.TriggerHairlineStart -= this.TriggerHairlineStart;
			component.TriggerHairlineEnd -= this.TriggerHairlineEnd;
			component.TriggerClicked -= this.TriggerClicked;
			component.TriggerUnclicked -= this.TriggerUnclicked;
			component.TriggerAxisChanged -= this.TriggerAxisChanged;
			component.GripPressed -= this.GripPressed;
			component.GripReleased -= this.GripReleased;
			component.GripTouchStart -= this.GripTouchStart;
			component.GripTouchEnd -= this.GripTouchEnd;
			component.GripHairlineStart -= this.GripHairlineStart;
			component.GripHairlineEnd -= this.GripHairlineEnd;
			component.GripClicked -= this.GripClicked;
			component.GripUnclicked -= this.GripUnclicked;
			component.GripAxisChanged -= this.GripAxisChanged;
			component.TouchpadPressed -= this.TouchpadPressed;
			component.TouchpadReleased -= this.TouchpadReleased;
			component.TouchpadTouchStart -= this.TouchpadTouchStart;
			component.TouchpadTouchEnd -= this.TouchpadTouchEnd;
			component.TouchpadAxisChanged -= this.TouchpadAxisChanged;
			component.ButtonOnePressed -= this.ButtonOnePressed;
			component.ButtonOneReleased -= this.ButtonOneReleased;
			component.ButtonOneTouchStart -= this.ButtonOneTouchStart;
			component.ButtonOneTouchEnd -= this.ButtonOneTouchEnd;
			component.ButtonTwoPressed -= this.ButtonTwoPressed;
			component.ButtonTwoReleased -= this.ButtonTwoReleased;
			component.ButtonTwoTouchStart -= this.ButtonTwoTouchStart;
			component.ButtonTwoTouchEnd -= this.ButtonTwoTouchEnd;
			component.StartMenuPressed -= this.StartMenuPressed;
			component.StartMenuReleased -= this.StartMenuReleased;
			component.AliasPointerOn -= this.AliasPointerOn;
			component.AliasPointerOff -= this.AliasPointerOff;
			component.AliasPointerSet -= this.AliasPointerSet;
			component.AliasGrabOn -= this.AliasGrabOn;
			component.AliasGrabOff -= this.AliasGrabOff;
			component.AliasUseOn -= this.AliasUseOn;
			component.AliasUseOff -= this.AliasUseOff;
			component.AliasUIClickOn -= this.AliasUIClickOn;
			component.AliasUIClickOff -= this.AliasUIClickOff;
			component.AliasMenuOn -= this.AliasMenuOn;
			component.AliasMenuOff -= this.AliasMenuOff;
			component.ControllerEnabled -= this.ControllerEnabled;
			component.ControllerDisabled -= this.ControllerDisabled;
			component.ControllerIndexChanged -= this.ControllerIndexChanged;
			component.ControllerVisible -= this.ControllerVisible;
			component.ControllerHidden -= this.ControllerHidden;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0009294B File Offset: 0x00090B4B
		private void TriggerPressed(object o, ControllerInteractionEventArgs e)
		{
			this.OnTriggerPressed.Invoke(o, e);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0009295A File Offset: 0x00090B5A
		private void TriggerReleased(object o, ControllerInteractionEventArgs e)
		{
			this.OnTriggerReleased.Invoke(o, e);
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00092969 File Offset: 0x00090B69
		private void TriggerTouchStart(object o, ControllerInteractionEventArgs e)
		{
			this.OnTriggerTouchStart.Invoke(o, e);
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x00092978 File Offset: 0x00090B78
		private void TriggerTouchEnd(object o, ControllerInteractionEventArgs e)
		{
			this.OnTriggerTouchEnd.Invoke(o, e);
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00092987 File Offset: 0x00090B87
		private void TriggerHairlineStart(object o, ControllerInteractionEventArgs e)
		{
			this.OnTriggerHairlineStart.Invoke(o, e);
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00092996 File Offset: 0x00090B96
		private void TriggerHairlineEnd(object o, ControllerInteractionEventArgs e)
		{
			this.OnTriggerHairlineEnd.Invoke(o, e);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000929A5 File Offset: 0x00090BA5
		private void TriggerClicked(object o, ControllerInteractionEventArgs e)
		{
			this.OnTriggerClicked.Invoke(o, e);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000929B4 File Offset: 0x00090BB4
		private void TriggerUnclicked(object o, ControllerInteractionEventArgs e)
		{
			this.OnTriggerUnclicked.Invoke(o, e);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x000929C3 File Offset: 0x00090BC3
		private void TriggerAxisChanged(object o, ControllerInteractionEventArgs e)
		{
			this.OnTriggerAxisChanged.Invoke(o, e);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x000929D2 File Offset: 0x00090BD2
		private void GripPressed(object o, ControllerInteractionEventArgs e)
		{
			this.OnGripPressed.Invoke(o, e);
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000929E1 File Offset: 0x00090BE1
		private void GripReleased(object o, ControllerInteractionEventArgs e)
		{
			this.OnGripReleased.Invoke(o, e);
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000929F0 File Offset: 0x00090BF0
		private void GripTouchStart(object o, ControllerInteractionEventArgs e)
		{
			this.OnGripTouchStart.Invoke(o, e);
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000929FF File Offset: 0x00090BFF
		private void GripTouchEnd(object o, ControllerInteractionEventArgs e)
		{
			this.OnGripTouchEnd.Invoke(o, e);
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00092A0E File Offset: 0x00090C0E
		private void GripHairlineStart(object o, ControllerInteractionEventArgs e)
		{
			this.OnGripHairlineStart.Invoke(o, e);
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00092A1D File Offset: 0x00090C1D
		private void GripHairlineEnd(object o, ControllerInteractionEventArgs e)
		{
			this.OnGripHairlineEnd.Invoke(o, e);
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x00092A2C File Offset: 0x00090C2C
		private void GripClicked(object o, ControllerInteractionEventArgs e)
		{
			this.OnGripClicked.Invoke(o, e);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00092A3B File Offset: 0x00090C3B
		private void GripUnclicked(object o, ControllerInteractionEventArgs e)
		{
			this.OnGripUnclicked.Invoke(o, e);
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00092A4A File Offset: 0x00090C4A
		private void GripAxisChanged(object o, ControllerInteractionEventArgs e)
		{
			this.OnGripAxisChanged.Invoke(o, e);
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00092A59 File Offset: 0x00090C59
		private void TouchpadPressed(object o, ControllerInteractionEventArgs e)
		{
			this.OnTouchpadPressed.Invoke(o, e);
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00092A68 File Offset: 0x00090C68
		private void TouchpadReleased(object o, ControllerInteractionEventArgs e)
		{
			this.OnTouchpadReleased.Invoke(o, e);
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00092A77 File Offset: 0x00090C77
		private void TouchpadTouchStart(object o, ControllerInteractionEventArgs e)
		{
			this.OnTouchpadTouchStart.Invoke(o, e);
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00092A86 File Offset: 0x00090C86
		private void TouchpadTouchEnd(object o, ControllerInteractionEventArgs e)
		{
			this.OnTouchpadTouchEnd.Invoke(o, e);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x00092A95 File Offset: 0x00090C95
		private void TouchpadAxisChanged(object o, ControllerInteractionEventArgs e)
		{
			this.OnTouchpadAxisChanged.Invoke(o, e);
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00092AA4 File Offset: 0x00090CA4
		private void ButtonOnePressed(object o, ControllerInteractionEventArgs e)
		{
			this.OnButtonOnePressed.Invoke(o, e);
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x00092AB3 File Offset: 0x00090CB3
		private void ButtonOneReleased(object o, ControllerInteractionEventArgs e)
		{
			this.OnButtonOneReleased.Invoke(o, e);
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x00092AC2 File Offset: 0x00090CC2
		private void ButtonOneTouchStart(object o, ControllerInteractionEventArgs e)
		{
			this.OnButtonOneTouchStart.Invoke(o, e);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00092AD1 File Offset: 0x00090CD1
		private void ButtonOneTouchEnd(object o, ControllerInteractionEventArgs e)
		{
			this.OnButtonOneTouchEnd.Invoke(o, e);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00092AE0 File Offset: 0x00090CE0
		private void ButtonTwoPressed(object o, ControllerInteractionEventArgs e)
		{
			this.OnButtonTwoPressed.Invoke(o, e);
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00092AEF File Offset: 0x00090CEF
		private void ButtonTwoReleased(object o, ControllerInteractionEventArgs e)
		{
			this.OnButtonTwoReleased.Invoke(o, e);
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x00092AFE File Offset: 0x00090CFE
		private void ButtonTwoTouchStart(object o, ControllerInteractionEventArgs e)
		{
			this.OnButtonTwoTouchStart.Invoke(o, e);
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00092B0D File Offset: 0x00090D0D
		private void ButtonTwoTouchEnd(object o, ControllerInteractionEventArgs e)
		{
			this.OnButtonTwoTouchEnd.Invoke(o, e);
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x00092B1C File Offset: 0x00090D1C
		private void StartMenuPressed(object o, ControllerInteractionEventArgs e)
		{
			this.OnStartMenuPressed.Invoke(o, e);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00092B2B File Offset: 0x00090D2B
		private void StartMenuReleased(object o, ControllerInteractionEventArgs e)
		{
			this.OnStartMenuReleased.Invoke(o, e);
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00092B3A File Offset: 0x00090D3A
		private void AliasPointerOn(object o, ControllerInteractionEventArgs e)
		{
			this.OnAliasPointerOn.Invoke(o, e);
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00092B49 File Offset: 0x00090D49
		private void AliasPointerOff(object o, ControllerInteractionEventArgs e)
		{
			this.OnAliasPointerOff.Invoke(o, e);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00092B58 File Offset: 0x00090D58
		private void AliasPointerSet(object o, ControllerInteractionEventArgs e)
		{
			this.OnAliasPointerSet.Invoke(o, e);
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00092B67 File Offset: 0x00090D67
		private void AliasGrabOn(object o, ControllerInteractionEventArgs e)
		{
			this.OnAliasGrabOn.Invoke(o, e);
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00092B76 File Offset: 0x00090D76
		private void AliasGrabOff(object o, ControllerInteractionEventArgs e)
		{
			this.OnAliasGrabOff.Invoke(o, e);
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x00092B85 File Offset: 0x00090D85
		private void AliasUseOn(object o, ControllerInteractionEventArgs e)
		{
			this.OnAliasUseOn.Invoke(o, e);
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00092B94 File Offset: 0x00090D94
		private void AliasUseOff(object o, ControllerInteractionEventArgs e)
		{
			this.OnAliasUseOff.Invoke(o, e);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x00092BA3 File Offset: 0x00090DA3
		private void AliasUIClickOn(object o, ControllerInteractionEventArgs e)
		{
			this.OnAliasUIClickOn.Invoke(o, e);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00092BB2 File Offset: 0x00090DB2
		private void AliasUIClickOff(object o, ControllerInteractionEventArgs e)
		{
			this.OnAliasUIClickOff.Invoke(o, e);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00092BC1 File Offset: 0x00090DC1
		private void AliasMenuOn(object o, ControllerInteractionEventArgs e)
		{
			this.OnAliasMenuOn.Invoke(o, e);
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x00092BD0 File Offset: 0x00090DD0
		private void AliasMenuOff(object o, ControllerInteractionEventArgs e)
		{
			this.OnAliasMenuOff.Invoke(o, e);
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00092BDF File Offset: 0x00090DDF
		private void ControllerEnabled(object o, ControllerInteractionEventArgs e)
		{
			this.OnControllerEnabled.Invoke(o, e);
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x00092BEE File Offset: 0x00090DEE
		private void ControllerDisabled(object o, ControllerInteractionEventArgs e)
		{
			this.OnControllerDisabled.Invoke(o, e);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x00092BFD File Offset: 0x00090DFD
		private void ControllerIndexChanged(object o, ControllerInteractionEventArgs e)
		{
			this.OnControllerIndexChanged.Invoke(o, e);
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x00092C0C File Offset: 0x00090E0C
		private void ControllerVisible(object o, ControllerInteractionEventArgs e)
		{
			this.OnControllerVisible.Invoke(o, e);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00092C1B File Offset: 0x00090E1B
		private void ControllerHidden(object o, ControllerInteractionEventArgs e)
		{
			this.OnControllerHidden.Invoke(o, e);
		}

		// Token: 0x04001666 RID: 5734
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTriggerPressed = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001667 RID: 5735
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTriggerReleased = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001668 RID: 5736
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTriggerTouchStart = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001669 RID: 5737
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTriggerTouchEnd = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400166A RID: 5738
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTriggerHairlineStart = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400166B RID: 5739
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTriggerHairlineEnd = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400166C RID: 5740
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTriggerClicked = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400166D RID: 5741
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTriggerUnclicked = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400166E RID: 5742
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTriggerAxisChanged = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400166F RID: 5743
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnGripPressed = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001670 RID: 5744
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnGripReleased = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001671 RID: 5745
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnGripTouchStart = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001672 RID: 5746
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnGripTouchEnd = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001673 RID: 5747
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnGripHairlineStart = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001674 RID: 5748
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnGripHairlineEnd = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001675 RID: 5749
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnGripClicked = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001676 RID: 5750
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnGripUnclicked = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001677 RID: 5751
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnGripAxisChanged = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001678 RID: 5752
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTouchpadPressed = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001679 RID: 5753
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTouchpadReleased = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400167A RID: 5754
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTouchpadTouchStart = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400167B RID: 5755
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTouchpadTouchEnd = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400167C RID: 5756
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnTouchpadAxisChanged = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400167D RID: 5757
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnButtonOnePressed = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400167E RID: 5758
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnButtonOneReleased = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400167F RID: 5759
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnButtonOneTouchStart = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001680 RID: 5760
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnButtonOneTouchEnd = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001681 RID: 5761
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnButtonTwoPressed = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001682 RID: 5762
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnButtonTwoReleased = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001683 RID: 5763
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnButtonTwoTouchStart = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001684 RID: 5764
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnButtonTwoTouchEnd = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001685 RID: 5765
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnStartMenuPressed = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001686 RID: 5766
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnStartMenuReleased = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001687 RID: 5767
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnAliasPointerOn = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001688 RID: 5768
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnAliasPointerOff = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001689 RID: 5769
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnAliasPointerSet = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400168A RID: 5770
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnAliasGrabOn = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400168B RID: 5771
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnAliasGrabOff = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400168C RID: 5772
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnAliasUseOn = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400168D RID: 5773
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnAliasUseOff = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400168E RID: 5774
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnAliasUIClickOn = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0400168F RID: 5775
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnAliasUIClickOff = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001690 RID: 5776
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnAliasMenuOn = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001691 RID: 5777
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnAliasMenuOff = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001692 RID: 5778
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnControllerEnabled = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001693 RID: 5779
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnControllerDisabled = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001694 RID: 5780
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnControllerIndexChanged = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001695 RID: 5781
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnControllerVisible = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x04001696 RID: 5782
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnControllerHidden = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x02000622 RID: 1570
		[Serializable]
		public sealed class ControllerInteractionEvent : UnityEvent<object, ControllerInteractionEventArgs>
		{
		}
	}
}
