using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002A7 RID: 679
	[AddComponentMenu("VRTK/Scripts/Interactions/VRTK_InteractTouch")]
	public class VRTK_InteractTouch : MonoBehaviour
	{
		// Token: 0x14000070 RID: 112
		// (add) Token: 0x060015CD RID: 5581 RVA: 0x00077504 File Offset: 0x00075704
		// (remove) Token: 0x060015CE RID: 5582 RVA: 0x0007753C File Offset: 0x0007573C
		public event ObjectInteractEventHandler ControllerStartTouchInteractableObject;

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x060015CF RID: 5583 RVA: 0x00077574 File Offset: 0x00075774
		// (remove) Token: 0x060015D0 RID: 5584 RVA: 0x000775AC File Offset: 0x000757AC
		public event ObjectInteractEventHandler ControllerTouchInteractableObject;

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x060015D1 RID: 5585 RVA: 0x000775E4 File Offset: 0x000757E4
		// (remove) Token: 0x060015D2 RID: 5586 RVA: 0x0007761C File Offset: 0x0007581C
		public event ObjectInteractEventHandler ControllerStartUntouchInteractableObject;

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x060015D3 RID: 5587 RVA: 0x00077654 File Offset: 0x00075854
		// (remove) Token: 0x060015D4 RID: 5588 RVA: 0x0007768C File Offset: 0x0007588C
		public event ObjectInteractEventHandler ControllerUntouchInteractableObject;

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x060015D5 RID: 5589 RVA: 0x000776C4 File Offset: 0x000758C4
		// (remove) Token: 0x060015D6 RID: 5590 RVA: 0x000776FC File Offset: 0x000758FC
		public event ObjectInteractEventHandler ControllerRigidbodyActivated;

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x060015D7 RID: 5591 RVA: 0x00077734 File Offset: 0x00075934
		// (remove) Token: 0x060015D8 RID: 5592 RVA: 0x0007776C File Offset: 0x0007596C
		public event ObjectInteractEventHandler ControllerRigidbodyDeactivated;

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x000777A1 File Offset: 0x000759A1
		protected VRTK_ControllerReference controllerReference
		{
			get
			{
				return VRTK_ControllerReference.GetControllerReference(base.gameObject);
			}
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000777AE File Offset: 0x000759AE
		public virtual void OnControllerStartTouchInteractableObject(ObjectInteractEventArgs e)
		{
			if (this.ControllerStartTouchInteractableObject != null)
			{
				this.ControllerStartTouchInteractableObject(this, e);
			}
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000777C5 File Offset: 0x000759C5
		public virtual void OnControllerTouchInteractableObject(ObjectInteractEventArgs e)
		{
			if (this.ControllerTouchInteractableObject != null)
			{
				this.ControllerTouchInteractableObject(this, e);
			}
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000777DC File Offset: 0x000759DC
		public virtual void OnControllerStartUntouchInteractableObject(ObjectInteractEventArgs e)
		{
			if (this.ControllerStartUntouchInteractableObject != null)
			{
				this.ControllerStartUntouchInteractableObject(this, e);
			}
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000777F3 File Offset: 0x000759F3
		public virtual void OnControllerUntouchInteractableObject(ObjectInteractEventArgs e)
		{
			if (this.ControllerUntouchInteractableObject != null)
			{
				this.ControllerUntouchInteractableObject(this, e);
			}
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0007780A File Offset: 0x00075A0A
		public virtual void OnControllerRigidbodyActivated(ObjectInteractEventArgs e)
		{
			if (this.ControllerRigidbodyActivated != null)
			{
				this.ControllerRigidbodyActivated(this, e);
			}
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00077821 File Offset: 0x00075A21
		public virtual void OnControllerRigidbodyDeactivated(ObjectInteractEventArgs e)
		{
			if (this.ControllerRigidbodyDeactivated != null)
			{
				this.ControllerRigidbodyDeactivated(this, e);
			}
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00077838 File Offset: 0x00075A38
		public virtual ObjectInteractEventArgs SetControllerInteractEvent(GameObject target)
		{
			ObjectInteractEventArgs result;
			result.controllerIndex = VRTK_ControllerReference.GetRealIndex(this.controllerReference);
			result.controllerReference = this.controllerReference;
			result.target = target;
			return result;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00077870 File Offset: 0x00075A70
		public virtual void ForceTouch(GameObject obj)
		{
			Collider collider = (obj != null) ? obj.GetComponentInChildren<Collider>() : null;
			if (collider != null)
			{
				this.OnTriggerStay(collider);
			}
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x000778A0 File Offset: 0x00075AA0
		public virtual GameObject GetTouchedObject()
		{
			return this.touchedObject;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000778A8 File Offset: 0x00075AA8
		public virtual bool IsObjectInteractable(GameObject obj)
		{
			if (obj != null)
			{
				VRTK_InteractableObject componentInParent = obj.GetComponentInParent<VRTK_InteractableObject>();
				if (componentInParent != null)
				{
					return (componentInParent.disableWhenIdle && !componentInParent.enabled) || componentInParent.enabled;
				}
			}
			return false;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x000778E8 File Offset: 0x00075AE8
		public virtual void ToggleControllerRigidBody(bool state, bool forceToggle = false)
		{
			if (this.controllerCollisionDetector != null && this.touchRigidBody != null)
			{
				this.touchRigidBody.isKinematic = !state;
				this.rigidBodyForcedActive = forceToggle;
				Collider[] componentsInChildren = this.controllerCollisionDetector.GetComponentsInChildren<Collider>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].isTrigger = !state;
				}
				this.EmitControllerRigidbodyEvent(state);
			}
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00077954 File Offset: 0x00075B54
		public virtual bool IsRigidBodyActive()
		{
			return !this.touchRigidBody.isKinematic;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00077964 File Offset: 0x00075B64
		public virtual bool IsRigidBodyForcedActive()
		{
			return this.IsRigidBodyActive() && this.rigidBodyForcedActive;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00077976 File Offset: 0x00075B76
		public virtual void ForceStopTouching()
		{
			if (this.touchedObject != null)
			{
				this.StopTouching(this.touchedObject);
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00077992 File Offset: 0x00075B92
		public virtual Collider[] ControllerColliders()
		{
			if (!(this.controllerCollisionDetector != null) || this.controllerCollisionDetector.GetComponents<Collider>().Length == 0)
			{
				return this.controllerCollisionDetector.GetComponentsInChildren<Collider>();
			}
			return this.controllerCollisionDetector.GetComponents<Collider>();
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000779C8 File Offset: 0x00075BC8
		protected virtual void OnEnable()
		{
			this.destroyColliderOnDisable = false;
			SDK_BaseController.ControllerHand controllerHand = VRTK_DeviceFinder.GetControllerHand(base.gameObject);
			this.defaultColliderPrefab = Resources.Load(VRTK_SDK_Bridge.GetControllerDefaultColliderPath(controllerHand));
			VRTK_PlayerObject.SetPlayerObject(base.gameObject, VRTK_PlayerObject.ObjectTypes.Controller);
			this.triggerRumble = false;
			this.CreateTouchCollider();
			this.CreateTouchRigidBody();
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00077A18 File Offset: 0x00075C18
		protected virtual void OnDisable()
		{
			this.ForceStopTouching();
			this.DestroyTouchCollider();
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00077A28 File Offset: 0x00075C28
		protected virtual void OnTriggerEnter(Collider collider)
		{
			GameObject gameObject = this.TriggerStart(collider);
			VRTK_InteractableObject vrtk_InteractableObject = (this.touchedObject != null) ? this.touchedObject.GetComponent<VRTK_InteractableObject>() : null;
			if (this.touchedObject != null && gameObject != null && this.touchedObject != gameObject && vrtk_InteractableObject != null && !vrtk_InteractableObject.IsGrabbed(null))
			{
				base.CancelInvoke("ResetTriggerRumble");
				this.ResetTriggerRumble();
				this.ForceStopTouching();
				this.triggerIsColliding = true;
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00077AAF File Offset: 0x00075CAF
		protected virtual void OnTriggerExit(Collider collider)
		{
			if (this.touchedObjectActiveColliders.Contains(collider))
			{
				this.touchedObjectActiveColliders.Remove(collider);
			}
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00077ACC File Offset: 0x00075CCC
		protected virtual void OnTriggerStay(Collider collider)
		{
			GameObject x = this.TriggerStart(collider);
			if (this.touchedObject == null || collider.transform.IsChildOf(this.touchedObject.transform))
			{
				this.triggerIsColliding = true;
			}
			if (this.touchedObject == null && x != null && this.IsObjectInteractable(collider.gameObject))
			{
				this.touchedObject = x;
				VRTK_InteractableObject component = this.touchedObject.GetComponent<VRTK_InteractableObject>();
				if (component != null && !component.IsValidInteractableController(base.gameObject, component.allowedTouchControllers))
				{
					this.CleanupEndTouch();
					return;
				}
				this.OnControllerStartTouchInteractableObject(this.SetControllerInteractEvent(this.touchedObject));
				this.StoreTouchedObjectColliders(collider);
				component.ToggleHighlight(true);
				this.ToggleControllerVisibility(false);
				this.CheckRumbleController(component);
				component.StartTouching(this);
				this.OnControllerTouchInteractableObject(this.SetControllerInteractEvent(this.touchedObject));
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00077BB7 File Offset: 0x00075DB7
		protected virtual void FixedUpdate()
		{
			if (!this.triggerIsColliding && !this.triggerWasColliding)
			{
				this.CheckStopTouching();
			}
			this.triggerWasColliding = this.triggerIsColliding;
			this.triggerIsColliding = false;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00077BE2 File Offset: 0x00075DE2
		protected virtual void LateUpdate()
		{
			if (this.touchedObjectActiveColliders.Count == 0)
			{
				this.CheckStopTouching();
			}
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00077BF8 File Offset: 0x00075DF8
		protected virtual GameObject GetColliderInteractableObject(Collider collider)
		{
			VRTK_InteractableObject componentInParent = collider.GetComponentInParent<VRTK_InteractableObject>();
			if (!(componentInParent != null))
			{
				return null;
			}
			return componentInParent.gameObject;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00077C1D File Offset: 0x00075E1D
		protected virtual void AddActiveCollider(Collider collider)
		{
			if (this.touchedObject != null && !this.touchedObjectActiveColliders.Contains(collider) && this.touchedObjectColliders.Contains(collider))
			{
				this.touchedObjectActiveColliders.Add(collider);
			}
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00077C58 File Offset: 0x00075E58
		protected virtual void StoreTouchedObjectColliders(Collider collider)
		{
			this.touchedObjectColliders.Clear();
			this.touchedObjectActiveColliders.Clear();
			Collider[] componentsInChildren = this.touchedObject.GetComponentsInChildren<Collider>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.touchedObjectColliders.Add(componentsInChildren[i]);
			}
			this.touchedObjectActiveColliders.Add(collider);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00077CB0 File Offset: 0x00075EB0
		protected virtual void ToggleControllerVisibility(bool visible)
		{
			GameObject modelAliasController = VRTK_DeviceFinder.GetModelAliasController(base.gameObject);
			if (this.touchedObject != null)
			{
				VRTK_InteractControllerAppearance[] componentsInParent = this.touchedObject.GetComponentsInParent<VRTK_InteractControllerAppearance>(true);
				if (componentsInParent.Length != 0)
				{
					componentsInParent[0].ToggleControllerOnTouch(visible, modelAliasController, this.touchedObject);
					return;
				}
			}
			else if (visible)
			{
				VRTK_ObjectAppearance.SetRendererVisible(modelAliasController, this.touchedObject);
			}
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00077D08 File Offset: 0x00075F08
		protected virtual void CheckRumbleController(VRTK_InteractableObject touchedObjectScript)
		{
			if (!this.triggerRumble)
			{
				VRTK_InteractHaptics componentInParent = this.touchedObject.GetComponentInParent<VRTK_InteractHaptics>();
				if (componentInParent != null)
				{
					this.triggerRumble = true;
					componentInParent.HapticsOnTouch(this.controllerReference);
					base.Invoke("ResetTriggerRumble", componentInParent.durationOnTouch);
				}
			}
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00077D58 File Offset: 0x00075F58
		protected virtual void CheckStopTouching()
		{
			if (this.touchedObject != null)
			{
				VRTK_InteractableObject component = this.touchedObject.GetComponent<VRTK_InteractableObject>();
				if (component != null && component.GetGrabbingObject() != base.gameObject)
				{
					this.StopTouching(this.touchedObject);
				}
			}
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00077DA7 File Offset: 0x00075FA7
		protected virtual GameObject TriggerStart(Collider collider)
		{
			if (this.IsSnapDropZone(collider))
			{
				return null;
			}
			this.AddActiveCollider(collider);
			return this.GetColliderInteractableObject(collider);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00077DC2 File Offset: 0x00075FC2
		protected virtual bool IsSnapDropZone(Collider collider)
		{
			return collider.GetComponent<VRTK_SnapDropZone>();
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00077DD4 File Offset: 0x00075FD4
		protected virtual void ResetTriggerRumble()
		{
			this.triggerRumble = false;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00077DE0 File Offset: 0x00075FE0
		protected virtual void StopTouching(GameObject untouched)
		{
			this.OnControllerStartUntouchInteractableObject(this.SetControllerInteractEvent(untouched));
			if (this.IsObjectInteractable(untouched))
			{
				VRTK_InteractableObject vrtk_InteractableObject = (untouched != null) ? untouched.GetComponent<VRTK_InteractableObject>() : null;
				if (vrtk_InteractableObject != null)
				{
					vrtk_InteractableObject.StopTouching(this);
					if (!vrtk_InteractableObject.IsTouched())
					{
						vrtk_InteractableObject.ToggleHighlight(false);
					}
				}
			}
			this.ToggleControllerVisibility(true);
			this.OnControllerUntouchInteractableObject(this.SetControllerInteractEvent(untouched));
			this.CleanupEndTouch();
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00077E4F File Offset: 0x0007604F
		protected virtual void CleanupEndTouch()
		{
			this.touchedObject = null;
			this.touchedObjectActiveColliders.Clear();
			this.touchedObjectColliders.Clear();
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00077E6E File Offset: 0x0007606E
		protected virtual void DestroyTouchCollider()
		{
			if (this.destroyColliderOnDisable)
			{
				Object.Destroy(this.controllerCollisionDetector);
			}
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00077E84 File Offset: 0x00076084
		protected virtual bool CustomRigidBodyIsChild()
		{
			foreach (Transform x in base.GetComponentsInChildren<Transform>())
			{
				if (x != base.transform && x == this.customColliderContainer.transform)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00077ED0 File Offset: 0x000760D0
		protected virtual void CreateTouchCollider()
		{
			if (this.customColliderContainer == null)
			{
				if (this.defaultColliderPrefab == null)
				{
					VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.SDK_OBJECT_NOT_FOUND, new object[]
					{
						"default collider prefab",
						"Controller SDK"
					}));
					return;
				}
				this.controllerCollisionDetector = (Object.Instantiate(this.defaultColliderPrefab, base.transform.position, base.transform.rotation) as GameObject);
				this.controllerCollisionDetector.transform.SetParent(base.transform);
				this.controllerCollisionDetector.transform.localScale = base.transform.localScale;
				this.controllerCollisionDetector.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
				{
					"Controller",
					"CollidersContainer"
				});
				this.destroyColliderOnDisable = true;
			}
			else if (this.CustomRigidBodyIsChild())
			{
				this.controllerCollisionDetector = this.customColliderContainer;
				this.destroyColliderOnDisable = false;
			}
			else
			{
				this.controllerCollisionDetector = Object.Instantiate<GameObject>(this.customColliderContainer, base.transform.position, base.transform.rotation);
				this.controllerCollisionDetector.transform.SetParent(base.transform);
				this.controllerCollisionDetector.transform.localScale = base.transform.localScale;
				this.destroyColliderOnDisable = true;
			}
			this.controllerCollisionDetector.AddComponent<VRTK_PlayerObject>().objectType = VRTK_PlayerObject.ObjectTypes.Collider;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00078038 File Offset: 0x00076238
		protected virtual void CreateTouchRigidBody()
		{
			this.touchRigidBody = (base.GetComponent<Rigidbody>() ? base.GetComponent<Rigidbody>() : base.gameObject.AddComponent<Rigidbody>());
			this.touchRigidBody.isKinematic = true;
			this.touchRigidBody.useGravity = false;
			this.touchRigidBody.constraints = RigidbodyConstraints.FreezeAll;
			this.touchRigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0007809C File Offset: 0x0007629C
		protected virtual void EmitControllerRigidbodyEvent(bool state)
		{
			if (state)
			{
				this.OnControllerRigidbodyActivated(this.SetControllerInteractEvent(null));
				return;
			}
			this.OnControllerRigidbodyDeactivated(this.SetControllerInteractEvent(null));
		}

		// Token: 0x0400126E RID: 4718
		[Tooltip("An optional GameObject that contains the compound colliders to represent the touching object. If this is empty then the collider will be auto generated at runtime to match the SDK default controller.")]
		public GameObject customColliderContainer;

		// Token: 0x04001275 RID: 4725
		protected GameObject touchedObject;

		// Token: 0x04001276 RID: 4726
		protected List<Collider> touchedObjectColliders = new List<Collider>();

		// Token: 0x04001277 RID: 4727
		protected List<Collider> touchedObjectActiveColliders = new List<Collider>();

		// Token: 0x04001278 RID: 4728
		protected GameObject controllerCollisionDetector;

		// Token: 0x04001279 RID: 4729
		protected bool triggerRumble;

		// Token: 0x0400127A RID: 4730
		protected bool destroyColliderOnDisable;

		// Token: 0x0400127B RID: 4731
		protected bool triggerIsColliding;

		// Token: 0x0400127C RID: 4732
		protected bool triggerWasColliding;

		// Token: 0x0400127D RID: 4733
		protected bool rigidBodyForcedActive;

		// Token: 0x0400127E RID: 4734
		protected Rigidbody touchRigidBody;

		// Token: 0x0400127F RID: 4735
		protected Object defaultColliderPrefab;
	}
}
