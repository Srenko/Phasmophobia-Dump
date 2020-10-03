using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VRTK
{
	// Token: 0x020002C2 RID: 706
	public class VRTK_VRInputModule : PointerInputModule
	{
		// Token: 0x0600175F RID: 5983 RVA: 0x0007D097 File Offset: 0x0007B297
		public virtual void Initialise()
		{
			this.pointers.Clear();
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool IsModuleSupported()
		{
			return false;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0007D0A4 File Offset: 0x0007B2A4
		public override void Process()
		{
			for (int i = 0; i < this.pointers.Count; i++)
			{
				VRTK_UIPointer vrtk_UIPointer = this.pointers[i];
				if (vrtk_UIPointer.gameObject.activeInHierarchy && vrtk_UIPointer.enabled)
				{
					List<RaycastResult> results = new List<RaycastResult>();
					if (vrtk_UIPointer.PointerActive())
					{
						results = this.CheckRaycasts(vrtk_UIPointer);
					}
					this.Hover(vrtk_UIPointer, results);
					this.Click(vrtk_UIPointer, results);
					this.Drag(vrtk_UIPointer, results);
					this.Scroll(vrtk_UIPointer, results);
				}
			}
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0007D120 File Offset: 0x0007B320
		protected virtual List<RaycastResult> CheckRaycasts(VRTK_UIPointer pointer)
		{
			RaycastResult pointerCurrentRaycast = default(RaycastResult);
			pointerCurrentRaycast.worldPosition = pointer.GetOriginPosition();
			pointerCurrentRaycast.worldNormal = pointer.GetOriginForward();
			pointer.pointerEventData.pointerCurrentRaycast = pointerCurrentRaycast;
			List<RaycastResult> list = new List<RaycastResult>();
			base.eventSystem.RaycastAll(pointer.pointerEventData, list);
			return list;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0007D174 File Offset: 0x0007B374
		protected virtual bool CheckTransformTree(Transform target, Transform source)
		{
			return !(target == null) && (target.Equals(source) || this.CheckTransformTree(target.transform.parent, source));
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0007D1A0 File Offset: 0x0007B3A0
		protected virtual bool NoValidCollision(VRTK_UIPointer pointer, List<RaycastResult> results)
		{
			return results.Count == 0 || !this.CheckTransformTree(results[0].gameObject.transform, pointer.pointerEventData.pointerEnter.transform);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0007D1E4 File Offset: 0x0007B3E4
		protected virtual bool IsHovering(VRTK_UIPointer pointer)
		{
			foreach (GameObject gameObject in pointer.pointerEventData.hovered)
			{
				if (pointer.pointerEventData.pointerEnter && gameObject && this.CheckTransformTree(gameObject.transform, pointer.pointerEventData.pointerEnter.transform))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0007D274 File Offset: 0x0007B474
		protected virtual bool ValidElement(GameObject obj)
		{
			VRTK_UICanvas componentInParent = obj.GetComponentInParent<VRTK_UICanvas>();
			return componentInParent && componentInParent.enabled;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0007D29C File Offset: 0x0007B49C
		protected virtual void CheckPointerHoverClick(VRTK_UIPointer pointer, List<RaycastResult> results)
		{
			if (pointer.hoverDurationTimer > 0f)
			{
				pointer.hoverDurationTimer -= Time.deltaTime;
			}
			if (pointer.canClickOnHover && pointer.hoverDurationTimer <= 0f)
			{
				pointer.canClickOnHover = false;
				this.ClickOnDown(pointer, results, true);
			}
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0007D2F0 File Offset: 0x0007B4F0
		protected virtual void Hover(VRTK_UIPointer pointer, List<RaycastResult> results)
		{
			if (pointer.pointerEventData.pointerEnter)
			{
				this.CheckPointerHoverClick(pointer, results);
				if (!this.ValidElement(pointer.pointerEventData.pointerEnter))
				{
					pointer.pointerEventData.pointerEnter = null;
					return;
				}
				if (this.NoValidCollision(pointer, results))
				{
					ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointer.pointerEventData.pointerEnter, pointer.pointerEventData, ExecuteEvents.pointerExitHandler);
					pointer.pointerEventData.hovered.Remove(pointer.pointerEventData.pointerEnter);
					pointer.pointerEventData.pointerEnter = null;
					return;
				}
			}
			else
			{
				foreach (RaycastResult raycastResult in results)
				{
					if (this.ValidElement(raycastResult.gameObject))
					{
						GameObject gameObject = ExecuteEvents.ExecuteHierarchy<IPointerEnterHandler>(raycastResult.gameObject, pointer.pointerEventData, ExecuteEvents.pointerEnterHandler);
						if (gameObject != null)
						{
							Selectable component = gameObject.GetComponent<Selectable>();
							if (component)
							{
								component.navigation = new Navigation
								{
									mode = Navigation.Mode.None
								};
							}
							pointer.OnUIPointerElementEnter(pointer.SetUIPointerEvent(raycastResult, gameObject, pointer.hoveringElement));
							pointer.hoveringElement = gameObject;
							pointer.pointerEventData.pointerCurrentRaycast = raycastResult;
							pointer.pointerEventData.pointerEnter = gameObject;
							pointer.pointerEventData.hovered.Add(pointer.pointerEventData.pointerEnter);
							break;
						}
						if (raycastResult.gameObject != pointer.hoveringElement)
						{
							pointer.OnUIPointerElementEnter(pointer.SetUIPointerEvent(raycastResult, raycastResult.gameObject, pointer.hoveringElement));
						}
						pointer.hoveringElement = raycastResult.gameObject;
					}
				}
				if (pointer.hoveringElement && results.Count == 0)
				{
					pointer.OnUIPointerElementExit(pointer.SetUIPointerEvent(default(RaycastResult), null, pointer.hoveringElement));
					pointer.hoveringElement = null;
				}
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0007D4F4 File Offset: 0x0007B6F4
		protected virtual void Click(VRTK_UIPointer pointer, List<RaycastResult> results)
		{
			VRTK_UIPointer.ClickMethods clickMethod = pointer.clickMethod;
			if (clickMethod == VRTK_UIPointer.ClickMethods.ClickOnButtonUp)
			{
				this.ClickOnUp(pointer, results);
				return;
			}
			if (clickMethod != VRTK_UIPointer.ClickMethods.ClickOnButtonDown)
			{
				return;
			}
			this.ClickOnDown(pointer, results, false);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0007D522 File Offset: 0x0007B722
		protected virtual void ClickOnUp(VRTK_UIPointer pointer, List<RaycastResult> results)
		{
			pointer.pointerEventData.eligibleForClick = pointer.ValidClick(false, false);
			if (!this.AttemptClick(pointer))
			{
				this.IsEligibleClick(pointer, results);
			}
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0007D549 File Offset: 0x0007B749
		protected virtual void ClickOnDown(VRTK_UIPointer pointer, List<RaycastResult> results, bool forceClick = false)
		{
			pointer.pointerEventData.eligibleForClick = (forceClick || pointer.ValidClick(true, false));
			if (this.IsEligibleClick(pointer, results))
			{
				pointer.pointerEventData.eligibleForClick = false;
				this.AttemptClick(pointer);
			}
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0007D584 File Offset: 0x0007B784
		protected virtual bool IsEligibleClick(VRTK_UIPointer pointer, List<RaycastResult> results)
		{
			if (pointer.pointerEventData.eligibleForClick)
			{
				foreach (RaycastResult pointerPressRaycast in results)
				{
					if (this.ValidElement(pointerPressRaycast.gameObject))
					{
						GameObject gameObject = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(pointerPressRaycast.gameObject, pointer.pointerEventData, ExecuteEvents.pointerDownHandler);
						if (gameObject != null)
						{
							pointer.pointerEventData.pressPosition = pointer.pointerEventData.position;
							pointer.pointerEventData.pointerPressRaycast = pointerPressRaycast;
							pointer.pointerEventData.pointerPress = gameObject;
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0007D640 File Offset: 0x0007B840
		protected virtual bool AttemptClick(VRTK_UIPointer pointer)
		{
			if (!pointer.pointerEventData.pointerPress)
			{
				return false;
			}
			if (!this.ValidElement(pointer.pointerEventData.pointerPress))
			{
				pointer.pointerEventData.pointerPress = null;
				return true;
			}
			if (pointer.pointerEventData.eligibleForClick)
			{
				if (!this.IsHovering(pointer))
				{
					ExecuteEvents.ExecuteHierarchy<IPointerUpHandler>(pointer.pointerEventData.pointerPress, pointer.pointerEventData, ExecuteEvents.pointerUpHandler);
					pointer.pointerEventData.pointerPress = null;
				}
			}
			else
			{
				pointer.OnUIPointerElementClick(pointer.SetUIPointerEvent(pointer.pointerEventData.pointerPressRaycast, pointer.pointerEventData.pointerPress, null));
				ExecuteEvents.ExecuteHierarchy<IPointerClickHandler>(pointer.pointerEventData.pointerPress, pointer.pointerEventData, ExecuteEvents.pointerClickHandler);
				ExecuteEvents.ExecuteHierarchy<IPointerUpHandler>(pointer.pointerEventData.pointerPress, pointer.pointerEventData, ExecuteEvents.pointerUpHandler);
				pointer.pointerEventData.pointerPress = null;
			}
			return true;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0007D730 File Offset: 0x0007B930
		protected virtual void Drag(VRTK_UIPointer pointer, List<RaycastResult> results)
		{
			pointer.pointerEventData.dragging = (pointer.IsSelectionButtonPressed() && pointer.pointerEventData.delta != Vector2.zero);
			if (pointer.pointerEventData.pointerDrag)
			{
				if (!this.ValidElement(pointer.pointerEventData.pointerDrag))
				{
					pointer.pointerEventData.pointerDrag = null;
					return;
				}
				if (!pointer.pointerEventData.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDragHandler>(pointer.pointerEventData.pointerDrag, pointer.pointerEventData, ExecuteEvents.dragHandler);
					ExecuteEvents.ExecuteHierarchy<IEndDragHandler>(pointer.pointerEventData.pointerDrag, pointer.pointerEventData, ExecuteEvents.endDragHandler);
					foreach (RaycastResult raycastResult in results)
					{
						ExecuteEvents.ExecuteHierarchy<IDropHandler>(raycastResult.gameObject, pointer.pointerEventData, ExecuteEvents.dropHandler);
					}
					pointer.pointerEventData.pointerDrag = null;
					return;
				}
				if (this.IsHovering(pointer))
				{
					ExecuteEvents.ExecuteHierarchy<IDragHandler>(pointer.pointerEventData.pointerDrag, pointer.pointerEventData, ExecuteEvents.dragHandler);
					return;
				}
			}
			else if (pointer.pointerEventData.dragging)
			{
				foreach (RaycastResult raycastResult2 in results)
				{
					if (this.ValidElement(raycastResult2.gameObject))
					{
						ExecuteEvents.ExecuteHierarchy<IInitializePotentialDragHandler>(raycastResult2.gameObject, pointer.pointerEventData, ExecuteEvents.initializePotentialDrag);
						ExecuteEvents.ExecuteHierarchy<IBeginDragHandler>(raycastResult2.gameObject, pointer.pointerEventData, ExecuteEvents.beginDragHandler);
						GameObject gameObject = ExecuteEvents.ExecuteHierarchy<IDragHandler>(raycastResult2.gameObject, pointer.pointerEventData, ExecuteEvents.dragHandler);
						if (gameObject != null)
						{
							pointer.pointerEventData.pointerDrag = gameObject;
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0007D920 File Offset: 0x0007BB20
		protected virtual void Scroll(VRTK_UIPointer pointer, List<RaycastResult> results)
		{
			pointer.pointerEventData.scrollDelta = pointer.controller.GetTouchpadAxis();
			bool state = false;
			foreach (RaycastResult raycastResult in results)
			{
				if (pointer.pointerEventData.scrollDelta != Vector2.zero && ExecuteEvents.ExecuteHierarchy<IScrollHandler>(raycastResult.gameObject, pointer.pointerEventData, ExecuteEvents.scrollHandler))
				{
					state = true;
				}
			}
			if (pointer.controllerRenderModel)
			{
				VRTK_SDK_Bridge.SetControllerRenderModelWheel(pointer.controllerRenderModel, state);
			}
		}

		// Token: 0x04001314 RID: 4884
		public List<VRTK_UIPointer> pointers = new List<VRTK_UIPointer>();
	}
}
