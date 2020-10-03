using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VRTK
{
	// Token: 0x020002FE RID: 766
	[AddComponentMenu("VRTK/Scripts/UI/VRTK_UICanvas")]
	public class VRTK_UICanvas : GraphicRaycaster
	{
		// Token: 0x06001A92 RID: 6802 RVA: 0x0008C253 File Offset: 0x0008A453
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetupCanvas();
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0008C261 File Offset: 0x0008A461
		protected override void OnDisable()
		{
			base.OnDisable();
			this.RemoveCanvas();
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0008C26F File Offset: 0x0008A46F
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.RemoveCanvas();
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0008C280 File Offset: 0x0008A480
		protected virtual void OnTriggerEnter(Collider collider)
		{
			VRTK_PlayerObject componentInParent = collider.GetComponentInParent<VRTK_PlayerObject>();
			VRTK_UIPointer componentInParent2 = collider.GetComponentInParent<VRTK_UIPointer>();
			if (componentInParent2 && componentInParent && componentInParent.objectType == VRTK_PlayerObject.ObjectTypes.Collider)
			{
				componentInParent2.collisionClick = this.clickOnPointerCollision;
			}
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0008C2C8 File Offset: 0x0008A4C8
		protected virtual void OnTriggerExit(Collider collider)
		{
			VRTK_UIPointer componentInParent = collider.GetComponentInParent<VRTK_UIPointer>();
			if (componentInParent)
			{
				componentInParent.collisionClick = false;
			}
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x0008C2EC File Offset: 0x0008A4EC
		protected virtual void SetupCanvas()
		{
			Canvas component = base.GetComponent<Canvas>();
			if (!component || component.renderMode != RenderMode.WorldSpace)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_UICanvas",
					"Canvas",
					"the same",
					" that is set to `Render Mode = World Space`"
				}));
				return;
			}
			RectTransform component2 = component.GetComponent<RectTransform>();
			Vector2 sizeDelta = component2.sizeDelta;
			GraphicRaycaster component3 = component.gameObject.GetComponent<GraphicRaycaster>();
			VRTK_UIGraphicRaycaster vrtk_UIGraphicRaycaster = component.gameObject.GetComponent<VRTK_UIGraphicRaycaster>();
			if (!vrtk_UIGraphicRaycaster)
			{
				vrtk_UIGraphicRaycaster = component.gameObject.AddComponent<VRTK_UIGraphicRaycaster>();
			}
			if (component3 && component3.enabled)
			{
				vrtk_UIGraphicRaycaster.ignoreReversedGraphics = component3.ignoreReversedGraphics;
				vrtk_UIGraphicRaycaster.blockingObjects = component3.blockingObjects;
				component3.enabled = false;
			}
			if (!component.gameObject.GetComponent<BoxCollider>())
			{
				Vector2 pivot = component2.pivot;
				float num = 0.1f / component2.localScale.z;
				this.canvasBoxCollider = component.gameObject.AddComponent<BoxCollider>();
				this.canvasBoxCollider.size = new Vector3(sizeDelta.x, sizeDelta.y, num);
				this.canvasBoxCollider.center = new Vector3(sizeDelta.x / 2f - sizeDelta.x * pivot.x, sizeDelta.y / 2f - sizeDelta.y * pivot.y, num / 2f);
				this.canvasBoxCollider.isTrigger = true;
			}
			if (!component.gameObject.GetComponent<Rigidbody>())
			{
				this.canvasRigidBody = component.gameObject.AddComponent<Rigidbody>();
				this.canvasRigidBody.isKinematic = true;
			}
			this.draggablePanelCreation = base.StartCoroutine(this.CreateDraggablePanel(component, sizeDelta));
			this.CreateActivator(component, sizeDelta);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0008C4B5 File Offset: 0x0008A6B5
		protected virtual IEnumerator CreateDraggablePanel(Canvas canvas, Vector2 canvasSize)
		{
			if (canvas && !canvas.transform.Find("VRTK_UICANVAS_DRAGGABLE_PANEL"))
			{
				yield return null;
				GameObject gameObject = new GameObject("VRTK_UICANVAS_DRAGGABLE_PANEL", new Type[]
				{
					typeof(RectTransform)
				});
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				gameObject.AddComponent<Image>().color = Color.clear;
				gameObject.AddComponent<EventTrigger>();
				gameObject.transform.SetParent(canvas.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.SetAsFirstSibling();
				gameObject.GetComponent<RectTransform>().sizeDelta = canvasSize;
			}
			yield break;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x0008C4CC File Offset: 0x0008A6CC
		protected virtual void CreateActivator(Canvas canvas, Vector2 canvasSize)
		{
			if (this.autoActivateWithinDistance > 0f && canvas && !canvas.transform.Find("VRTK_UICANVAS_ACTIVATOR_FRONT_TRIGGER"))
			{
				RectTransform component = canvas.GetComponent<RectTransform>();
				Vector2 pivot = component.pivot;
				GameObject gameObject = new GameObject("VRTK_UICANVAS_ACTIVATOR_FRONT_TRIGGER");
				gameObject.transform.SetParent(canvas.transform);
				gameObject.transform.SetAsFirstSibling();
				gameObject.transform.localPosition = new Vector3(canvasSize.x / 2f - canvasSize.x * pivot.x, canvasSize.y / 2f - canvasSize.y * pivot.y);
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
				float num = this.autoActivateWithinDistance / component.localScale.z;
				BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
				boxCollider.isTrigger = true;
				boxCollider.size = new Vector3(canvasSize.x, canvasSize.y, num);
				boxCollider.center = new Vector3(0f, 0f, -(num / 2f));
				gameObject.AddComponent<Rigidbody>().isKinematic = true;
				gameObject.AddComponent<VRTK_UIPointerAutoActivator>();
				gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
			}
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0008C61C File Offset: 0x0008A81C
		protected virtual void RemoveCanvas()
		{
			Canvas component = base.GetComponent<Canvas>();
			if (!component)
			{
				return;
			}
			GraphicRaycaster component2 = component.gameObject.GetComponent<GraphicRaycaster>();
			VRTK_UIGraphicRaycaster component3 = component.gameObject.GetComponent<VRTK_UIGraphicRaycaster>();
			if (component3)
			{
				Object.Destroy(component3);
			}
			if (component2 && !component2.enabled)
			{
				component2.enabled = true;
			}
			if (this.canvasBoxCollider)
			{
				Object.Destroy(this.canvasBoxCollider);
			}
			if (this.canvasRigidBody)
			{
				Object.Destroy(this.canvasRigidBody);
			}
			base.StopCoroutine(this.draggablePanelCreation);
			Transform transform = component.transform.Find("VRTK_UICANVAS_DRAGGABLE_PANEL");
			if (transform)
			{
				Object.Destroy(transform.gameObject);
			}
			Transform transform2 = component.transform.Find("VRTK_UICANVAS_ACTIVATOR_FRONT_TRIGGER");
			if (transform2)
			{
				Object.Destroy(transform2.gameObject);
			}
		}

		// Token: 0x04001584 RID: 5508
		[Tooltip("Determines if a UI Click action should happen when a UI Pointer game object collides with this canvas.")]
		public bool clickOnPointerCollision;

		// Token: 0x04001585 RID: 5509
		[Tooltip("Determines if a UI Pointer will be auto activated if a UI Pointer game object comes within the given distance of this canvas. If a value of `0` is given then no auto activation will occur.")]
		public float autoActivateWithinDistance;

		// Token: 0x04001586 RID: 5510
		protected BoxCollider canvasBoxCollider;

		// Token: 0x04001587 RID: 5511
		protected Rigidbody canvasRigidBody;

		// Token: 0x04001588 RID: 5512
		protected Coroutine draggablePanelCreation;

		// Token: 0x04001589 RID: 5513
		protected const string CANVAS_DRAGGABLE_PANEL = "VRTK_UICANVAS_DRAGGABLE_PANEL";

		// Token: 0x0400158A RID: 5514
		protected const string ACTIVATOR_FRONT_TRIGGER_GAMEOBJECT = "VRTK_UICANVAS_ACTIVATOR_FRONT_TRIGGER";
	}
}
