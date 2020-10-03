using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000338 RID: 824
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_UIDraggableItem_UnityEvents")]
	public sealed class VRTK_UIDraggableItem_UnityEvents : VRTK_UnityEvents<VRTK_UIDraggableItem>
	{
		// Token: 0x06001CE6 RID: 7398 RVA: 0x00094641 File Offset: 0x00092841
		protected override void AddListeners(VRTK_UIDraggableItem component)
		{
			component.DraggableItemDropped += this.DraggableItemDropped;
			component.DraggableItemReset += this.DraggableItemReset;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x00094667 File Offset: 0x00092867
		protected override void RemoveListeners(VRTK_UIDraggableItem component)
		{
			component.DraggableItemDropped -= this.DraggableItemDropped;
			component.DraggableItemReset -= this.DraggableItemReset;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0009468D File Offset: 0x0009288D
		private void DraggableItemDropped(object o, UIDraggableItemEventArgs e)
		{
			this.OnDraggableItemDropped.Invoke(o, e);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0009469C File Offset: 0x0009289C
		private void DraggableItemReset(object o, UIDraggableItemEventArgs e)
		{
			this.OnDraggableItemReset.Invoke(o, e);
		}

		// Token: 0x040016EF RID: 5871
		public VRTK_UIDraggableItem_UnityEvents.UIDraggableItemEvent OnDraggableItemDropped = new VRTK_UIDraggableItem_UnityEvents.UIDraggableItemEvent();

		// Token: 0x040016F0 RID: 5872
		public VRTK_UIDraggableItem_UnityEvents.UIDraggableItemEvent OnDraggableItemReset = new VRTK_UIDraggableItem_UnityEvents.UIDraggableItemEvent();

		// Token: 0x0200063B RID: 1595
		[Serializable]
		public sealed class UIDraggableItemEvent : UnityEvent<object, UIDraggableItemEventArgs>
		{
		}
	}
}
