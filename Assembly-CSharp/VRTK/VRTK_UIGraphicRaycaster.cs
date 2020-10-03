using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VRTK
{
	// Token: 0x020002C1 RID: 705
	public class VRTK_UIGraphicRaycaster : GraphicRaycaster
	{
		// Token: 0x06001758 RID: 5976 RVA: 0x0007CCCC File Offset: 0x0007AECC
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.canvas == null)
			{
				return;
			}
			Ray ray = new Ray(eventData.pointerCurrentRaycast.worldPosition, eventData.pointerCurrentRaycast.worldNormal);
			this.Raycast(this.canvas, this.eventCamera, ray, ref VRTK_UIGraphicRaycaster.s_RaycastResults);
			this.SetNearestRaycast(ref eventData, ref resultAppendList, ref VRTK_UIGraphicRaycaster.s_RaycastResults);
			VRTK_UIGraphicRaycaster.s_RaycastResults.Clear();
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0007CD38 File Offset: 0x0007AF38
		protected virtual void SetNearestRaycast(ref PointerEventData eventData, ref List<RaycastResult> resultAppendList, ref List<RaycastResult> raycastResults)
		{
			RaycastResult? raycastResult = null;
			for (int i = 0; i < raycastResults.Count; i++)
			{
				RaycastResult raycastResult2 = raycastResults[i];
				raycastResult2.index = (float)resultAppendList.Count;
				if (raycastResult == null || raycastResult2.distance < raycastResult.Value.distance)
				{
					raycastResult = new RaycastResult?(raycastResult2);
				}
				resultAppendList.Add(raycastResult2);
			}
			if (raycastResult != null)
			{
				eventData.position = raycastResult.Value.screenPosition;
				eventData.delta = eventData.position - this.lastKnownPosition;
				this.lastKnownPosition = eventData.position;
				eventData.pointerCurrentRaycast = raycastResult.Value;
			}
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0007CDF4 File Offset: 0x0007AFF4
		protected virtual float GetHitDistance(Ray ray)
		{
			float result = float.MaxValue;
			if (this.canvas.renderMode != RenderMode.ScreenSpaceOverlay && base.blockingObjects != GraphicRaycaster.BlockingObjects.None)
			{
				float num = Vector3.Distance(ray.origin, this.canvas.transform.position);
				if (base.blockingObjects == GraphicRaycaster.BlockingObjects.ThreeD || base.blockingObjects == GraphicRaycaster.BlockingObjects.All)
				{
					RaycastHit raycastHit;
					Physics.Raycast(ray, out raycastHit, num);
					if (raycastHit.collider && !VRTK_PlayerObject.IsPlayerObject(raycastHit.collider.gameObject, VRTK_PlayerObject.ObjectTypes.Null))
					{
						result = raycastHit.distance;
					}
				}
				if (base.blockingObjects == GraphicRaycaster.BlockingObjects.TwoD || base.blockingObjects == GraphicRaycaster.BlockingObjects.All)
				{
					RaycastHit2D raycastHit2D = Physics2D.Raycast(ray.origin, ray.direction, num);
					if (raycastHit2D.collider != null && !VRTK_PlayerObject.IsPlayerObject(raycastHit2D.collider.gameObject, VRTK_PlayerObject.ObjectTypes.Null))
					{
						result = raycastHit2D.fraction * num;
					}
				}
			}
			return result;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0007CEE4 File Offset: 0x0007B0E4
		protected virtual void Raycast(Canvas canvas, Camera eventCamera, Ray ray, ref List<RaycastResult> results)
		{
			float hitDistance = this.GetHitDistance(ray);
			IList<Graphic> graphicsForCanvas = GraphicRegistry.GetGraphicsForCanvas(canvas);
			for (int i = 0; i < graphicsForCanvas.Count; i++)
			{
				Graphic graphic = graphicsForCanvas[i];
				if (graphic.depth != -1 && graphic.raycastTarget)
				{
					Transform transform = graphic.transform;
					Vector3 forward = transform.forward;
					float num = Vector3.Dot(forward, transform.position - ray.origin) / Vector3.Dot(forward, ray.direction);
					if (num >= 0f && num - 1E-05f <= hitDistance)
					{
						Vector3 point = ray.GetPoint(num);
						Vector2 vector = eventCamera.WorldToScreenPoint(point);
						if (RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, vector, eventCamera) && graphic.Raycast(vector, eventCamera))
						{
							RaycastResult item = new RaycastResult
							{
								gameObject = graphic.gameObject,
								module = this,
								distance = num,
								screenPosition = vector,
								worldPosition = point,
								depth = graphic.depth,
								sortingLayer = canvas.sortingLayerID,
								sortingOrder = canvas.sortingOrder
							};
							results.Add(item);
						}
					}
				}
			}
			results.Sort((RaycastResult g1, RaycastResult g2) => g2.depth.CompareTo(g1.depth));
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x0007D055 File Offset: 0x0007B255
		protected virtual Canvas canvas
		{
			get
			{
				if (this.currentCanvas != null)
				{
					return this.currentCanvas;
				}
				this.currentCanvas = base.gameObject.GetComponent<Canvas>();
				return this.currentCanvas;
			}
		}

		// Token: 0x04001310 RID: 4880
		protected Canvas currentCanvas;

		// Token: 0x04001311 RID: 4881
		protected Vector2 lastKnownPosition;

		// Token: 0x04001312 RID: 4882
		protected const float UI_CONTROL_OFFSET = 1E-05f;

		// Token: 0x04001313 RID: 4883
		[NonSerialized]
		private static List<RaycastResult> s_RaycastResults = new List<RaycastResult>();
	}
}
