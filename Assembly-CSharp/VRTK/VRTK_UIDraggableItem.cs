using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VRTK
{
	// Token: 0x02000302 RID: 770
	[RequireComponent(typeof(CanvasGroup))]
	[AddComponentMenu("VRTK/Scripts/UI/VRTK_UIDraggableItem")]
	public class VRTK_UIDraggableItem : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06001AA3 RID: 6819 RVA: 0x0008C778 File Offset: 0x0008A978
		// (remove) Token: 0x06001AA4 RID: 6820 RVA: 0x0008C7B0 File Offset: 0x0008A9B0
		public event UIDraggableItemEventHandler DraggableItemDropped;

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x06001AA5 RID: 6821 RVA: 0x0008C7E8 File Offset: 0x0008A9E8
		// (remove) Token: 0x06001AA6 RID: 6822 RVA: 0x0008C820 File Offset: 0x0008AA20
		public event UIDraggableItemEventHandler DraggableItemReset;

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0008C855 File Offset: 0x0008AA55
		public virtual void OnDraggableItemDropped(UIDraggableItemEventArgs e)
		{
			if (this.DraggableItemDropped != null)
			{
				this.DraggableItemDropped(this, e);
			}
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0008C86C File Offset: 0x0008AA6C
		public virtual void OnDraggableItemReset(UIDraggableItemEventArgs e)
		{
			if (this.DraggableItemReset != null)
			{
				this.DraggableItemReset(this, e);
			}
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0008C884 File Offset: 0x0008AA84
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.startPosition = base.transform.position;
			this.startRotation = base.transform.rotation;
			this.startParent = base.transform.parent;
			this.startCanvas = base.GetComponentInParent<Canvas>();
			this.canvasGroup.blocksRaycasts = false;
			if (this.restrictToDropZone)
			{
				this.startDropZone = base.GetComponentInParent<VRTK_UIDropZone>().gameObject;
				this.validDropZone = this.startDropZone;
			}
			this.SetDragPosition(eventData);
			VRTK_UIPointer pointer = this.GetPointer(eventData);
			if (pointer != null)
			{
				pointer.OnUIPointerElementDragStart(pointer.SetUIPointerEvent(pointer.pointerEventData.pointerPressRaycast, base.gameObject, null));
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0008C937 File Offset: 0x0008AB37
		public virtual void OnDrag(PointerEventData eventData)
		{
			this.SetDragPosition(eventData);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0008C940 File Offset: 0x0008AB40
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			this.canvasGroup.blocksRaycasts = true;
			this.dragTransform = null;
			base.transform.position += base.transform.forward * this.forwardOffset;
			bool flag = true;
			if (this.restrictToDropZone)
			{
				if (this.validDropZone != null && this.validDropZone != this.startDropZone)
				{
					base.transform.SetParent(this.validDropZone.transform);
				}
				else
				{
					this.ResetElement();
					flag = false;
				}
			}
			Canvas x = (eventData.pointerEnter != null) ? eventData.pointerEnter.GetComponentInParent<Canvas>() : null;
			if (this.restrictToOriginalCanvas && x != null && x != this.startCanvas)
			{
				this.ResetElement();
				flag = false;
			}
			if (x == null)
			{
				this.ResetElement();
				flag = false;
			}
			if (flag)
			{
				VRTK_UIPointer pointer = this.GetPointer(eventData);
				if (pointer != null)
				{
					pointer.OnUIPointerElementDragEnd(pointer.SetUIPointerEvent(pointer.pointerEventData.pointerPressRaycast, base.gameObject, null));
				}
				this.OnDraggableItemDropped(this.SetEventPayload(this.validDropZone));
			}
			this.validDropZone = null;
			this.startParent = null;
			this.startCanvas = null;
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0008CA88 File Offset: 0x0008AC88
		protected virtual void OnEnable()
		{
			this.canvasGroup = base.GetComponent<CanvasGroup>();
			if (this.restrictToDropZone && base.GetComponentInParent<VRTK_UIDropZone>() == null)
			{
				base.enabled = false;
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_UIDraggableItem",
					"VRTK_UIDropZone",
					"the parent",
					" if `freeDrop = false`"
				}));
			}
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0008CAF0 File Offset: 0x0008ACF0
		protected virtual VRTK_UIPointer GetPointer(PointerEventData eventData)
		{
			GameObject controllerByIndex = VRTK_DeviceFinder.GetControllerByIndex((uint)eventData.pointerId, false);
			if (!(controllerByIndex != null))
			{
				return null;
			}
			return controllerByIndex.GetComponent<VRTK_UIPointer>();
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0008CB1C File Offset: 0x0008AD1C
		protected virtual void SetDragPosition(PointerEventData eventData)
		{
			if (eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
			{
				this.dragTransform = (eventData.pointerEnter.transform as RectTransform);
			}
			Vector3 a;
			if (this.dragTransform != null && RectTransformUtility.ScreenPointToWorldPointInRectangle(this.dragTransform, eventData.position, eventData.pressEventCamera, out a))
			{
				base.transform.position = a - base.transform.forward * this.forwardOffset;
				base.transform.rotation = this.dragTransform.rotation;
			}
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0008CBCC File Offset: 0x0008ADCC
		protected virtual void ResetElement()
		{
			base.transform.position = this.startPosition;
			base.transform.rotation = this.startRotation;
			base.transform.SetParent(this.startParent);
			this.OnDraggableItemReset(this.SetEventPayload(this.startParent.gameObject));
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x0008CC24 File Offset: 0x0008AE24
		protected virtual UIDraggableItemEventArgs SetEventPayload(GameObject target)
		{
			UIDraggableItemEventArgs result;
			result.target = target;
			return result;
		}

		// Token: 0x0400158C RID: 5516
		[Tooltip("If checked then the UI element can only be dropped in valid a VRTK_UIDropZone object and must start as a child of a VRTK_UIDropZone object. If unchecked then the UI element can be dropped anywhere on the canvas.")]
		public bool restrictToDropZone;

		// Token: 0x0400158D RID: 5517
		[Tooltip("If checked then the UI element can only be dropped on the original parent canvas. If unchecked the UI element can be dropped on any valid VRTK_UICanvas.")]
		public bool restrictToOriginalCanvas;

		// Token: 0x0400158E RID: 5518
		[Tooltip("The offset to bring the UI element forward when it is being dragged.")]
		public float forwardOffset = 0.1f;

		// Token: 0x0400158F RID: 5519
		[HideInInspector]
		public GameObject validDropZone;

		// Token: 0x04001592 RID: 5522
		protected RectTransform dragTransform;

		// Token: 0x04001593 RID: 5523
		protected Vector3 startPosition;

		// Token: 0x04001594 RID: 5524
		protected Quaternion startRotation;

		// Token: 0x04001595 RID: 5525
		protected GameObject startDropZone;

		// Token: 0x04001596 RID: 5526
		protected Transform startParent;

		// Token: 0x04001597 RID: 5527
		protected Canvas startCanvas;

		// Token: 0x04001598 RID: 5528
		protected CanvasGroup canvasGroup;
	}
}
