using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000339 RID: 825
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_UIPointer_UnityEvents")]
	public sealed class VRTK_UIPointer_UnityEvents : VRTK_UnityEvents<VRTK_UIPointer>
	{
		// Token: 0x06001CEB RID: 7403 RVA: 0x000946CC File Offset: 0x000928CC
		protected override void AddListeners(VRTK_UIPointer component)
		{
			component.UIPointerElementEnter += this.UIPointerElementEnter;
			component.UIPointerElementExit += this.UIPointerElementExit;
			component.UIPointerElementClick += this.UIPointerElementClick;
			component.UIPointerElementDragStart += this.UIPointerElementDragStart;
			component.UIPointerElementDragEnd += this.UIPointerElementDragEnd;
			component.ActivationButtonPressed += this.ActivationButtonPressed;
			component.ActivationButtonReleased += this.ActivationButtonReleased;
			component.SelectionButtonPressed += this.SelectionButtonPressed;
			component.SelectionButtonReleased += this.SelectionButtonReleased;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0009477C File Offset: 0x0009297C
		protected override void RemoveListeners(VRTK_UIPointer component)
		{
			component.UIPointerElementEnter -= this.UIPointerElementEnter;
			component.UIPointerElementExit -= this.UIPointerElementExit;
			component.UIPointerElementClick -= this.UIPointerElementClick;
			component.UIPointerElementDragStart -= this.UIPointerElementDragStart;
			component.UIPointerElementDragEnd -= this.UIPointerElementDragEnd;
			component.ActivationButtonPressed -= this.ActivationButtonPressed;
			component.ActivationButtonReleased -= this.ActivationButtonReleased;
			component.SelectionButtonPressed -= this.SelectionButtonPressed;
			component.SelectionButtonReleased -= this.SelectionButtonReleased;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0009482B File Offset: 0x00092A2B
		private void UIPointerElementEnter(object o, UIPointerEventArgs e)
		{
			this.OnUIPointerElementEnter.Invoke(o, e);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0009483A File Offset: 0x00092A3A
		private void UIPointerElementExit(object o, UIPointerEventArgs e)
		{
			this.OnUIPointerElementExit.Invoke(o, e);
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x00094849 File Offset: 0x00092A49
		private void UIPointerElementClick(object o, UIPointerEventArgs e)
		{
			this.OnUIPointerElementClick.Invoke(o, e);
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x00094858 File Offset: 0x00092A58
		private void UIPointerElementDragStart(object o, UIPointerEventArgs e)
		{
			this.OnUIPointerElementDragStart.Invoke(o, e);
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x00094867 File Offset: 0x00092A67
		private void UIPointerElementDragEnd(object o, UIPointerEventArgs e)
		{
			this.OnUIPointerElementDragEnd.Invoke(o, e);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x00094876 File Offset: 0x00092A76
		private void ActivationButtonPressed(object o, ControllerInteractionEventArgs e)
		{
			this.OnActivationButtonPressed.Invoke(o, e);
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x00094885 File Offset: 0x00092A85
		private void ActivationButtonReleased(object o, ControllerInteractionEventArgs e)
		{
			this.OnActivationButtonReleased.Invoke(o, e);
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00094894 File Offset: 0x00092A94
		private void SelectionButtonPressed(object o, ControllerInteractionEventArgs e)
		{
			this.OnSelectionButtonPressed.Invoke(o, e);
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000948A3 File Offset: 0x00092AA3
		private void SelectionButtonReleased(object o, ControllerInteractionEventArgs e)
		{
			this.OnSelectionButtonReleased.Invoke(o, e);
		}

		// Token: 0x040016F1 RID: 5873
		public VRTK_UIPointer_UnityEvents.UIPointerEvent OnUIPointerElementEnter = new VRTK_UIPointer_UnityEvents.UIPointerEvent();

		// Token: 0x040016F2 RID: 5874
		public VRTK_UIPointer_UnityEvents.UIPointerEvent OnUIPointerElementExit = new VRTK_UIPointer_UnityEvents.UIPointerEvent();

		// Token: 0x040016F3 RID: 5875
		public VRTK_UIPointer_UnityEvents.UIPointerEvent OnUIPointerElementClick = new VRTK_UIPointer_UnityEvents.UIPointerEvent();

		// Token: 0x040016F4 RID: 5876
		public VRTK_UIPointer_UnityEvents.UIPointerEvent OnUIPointerElementDragStart = new VRTK_UIPointer_UnityEvents.UIPointerEvent();

		// Token: 0x040016F5 RID: 5877
		public VRTK_UIPointer_UnityEvents.UIPointerEvent OnUIPointerElementDragEnd = new VRTK_UIPointer_UnityEvents.UIPointerEvent();

		// Token: 0x040016F6 RID: 5878
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnActivationButtonPressed = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x040016F7 RID: 5879
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnActivationButtonReleased = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x040016F8 RID: 5880
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnSelectionButtonPressed = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x040016F9 RID: 5881
		public VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent OnSelectionButtonReleased = new VRTK_ControllerEvents_UnityEvents.ControllerInteractionEvent();

		// Token: 0x0200063C RID: 1596
		[Serializable]
		public sealed class UIPointerEvent : UnityEvent<object, UIPointerEventArgs>
		{
		}
	}
}
