using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VRTK
{
	// Token: 0x02000303 RID: 771
	[AddComponentMenu("VRTK/Scripts/UI/VRTK_UIDropZone")]
	public class VRTK_UIDropZone : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x06001AB2 RID: 6834 RVA: 0x0008CC50 File Offset: 0x0008AE50
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			if (eventData.pointerDrag)
			{
				VRTK_UIDraggableItem component = eventData.pointerDrag.GetComponent<VRTK_UIDraggableItem>();
				if (component && component.restrictToDropZone)
				{
					component.validDropZone = base.gameObject;
					this.droppableItem = component;
				}
			}
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0008CC99 File Offset: 0x0008AE99
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			if (this.droppableItem)
			{
				this.droppableItem.validDropZone = null;
			}
			this.droppableItem = null;
		}

		// Token: 0x04001599 RID: 5529
		protected VRTK_UIDraggableItem droppableItem;
	}
}
