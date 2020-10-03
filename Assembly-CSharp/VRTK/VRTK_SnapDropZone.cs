using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Highlighters;

namespace VRTK
{
	// Token: 0x0200025E RID: 606
	[ExecuteInEditMode]
	public class VRTK_SnapDropZone : MonoBehaviour
	{
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060011E6 RID: 4582 RVA: 0x00067758 File Offset: 0x00065958
		// (remove) Token: 0x060011E7 RID: 4583 RVA: 0x00067790 File Offset: 0x00065990
		public event SnapDropZoneEventHandler ObjectEnteredSnapDropZone;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060011E8 RID: 4584 RVA: 0x000677C8 File Offset: 0x000659C8
		// (remove) Token: 0x060011E9 RID: 4585 RVA: 0x00067800 File Offset: 0x00065A00
		public event SnapDropZoneEventHandler ObjectExitedSnapDropZone;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060011EA RID: 4586 RVA: 0x00067838 File Offset: 0x00065A38
		// (remove) Token: 0x060011EB RID: 4587 RVA: 0x00067870 File Offset: 0x00065A70
		public event SnapDropZoneEventHandler ObjectSnappedToDropZone;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060011EC RID: 4588 RVA: 0x000678A8 File Offset: 0x00065AA8
		// (remove) Token: 0x060011ED RID: 4589 RVA: 0x000678E0 File Offset: 0x00065AE0
		public event SnapDropZoneEventHandler ObjectUnsnappedFromDropZone;

		// Token: 0x060011EE RID: 4590 RVA: 0x00067915 File Offset: 0x00065B15
		public virtual void OnObjectEnteredSnapDropZone(SnapDropZoneEventArgs e)
		{
			if (this.ObjectEnteredSnapDropZone != null)
			{
				this.ObjectEnteredSnapDropZone(this, e);
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0006792C File Offset: 0x00065B2C
		public virtual void OnObjectExitedSnapDropZone(SnapDropZoneEventArgs e)
		{
			if (this.ObjectExitedSnapDropZone != null)
			{
				this.ObjectExitedSnapDropZone(this, e);
			}
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00067943 File Offset: 0x00065B43
		public virtual void OnObjectSnappedToDropZone(SnapDropZoneEventArgs e)
		{
			if (this.ObjectSnappedToDropZone != null)
			{
				this.ObjectSnappedToDropZone(this, e);
			}
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0006795A File Offset: 0x00065B5A
		public virtual void OnObjectUnsnappedFromDropZone(SnapDropZoneEventArgs e)
		{
			this.UnsnapObject();
			if (this.ObjectUnsnappedFromDropZone != null)
			{
				this.ObjectUnsnappedFromDropZone(this, e);
			}
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00067978 File Offset: 0x00065B78
		public virtual SnapDropZoneEventArgs SetSnapDropZoneEvent(GameObject interactableObject)
		{
			SnapDropZoneEventArgs result;
			result.snappedObject = interactableObject;
			return result;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0006798E File Offset: 0x00065B8E
		public virtual void InitaliseHighlightObject(bool removeOldObject = false)
		{
			if (removeOldObject)
			{
				this.DeleteHighlightObject();
			}
			this.ChooseDestroyType(base.transform.Find(this.ObjectPath("EditorHighlightObject")));
			this.highlightEditorObject = null;
			this.GenerateObjects();
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x000679C4 File Offset: 0x00065BC4
		public virtual void ForceSnap(GameObject objectToSnap)
		{
			VRTK_InteractableObject componentInParent = objectToSnap.GetComponentInParent<VRTK_InteractableObject>();
			if (componentInParent != null)
			{
				componentInParent.SaveCurrentState();
				base.StopCoroutine("AttemptForceSnapAtEndOfFrame");
				if (componentInParent.IsGrabbed(null))
				{
					componentInParent.ForceStopInteracting();
					base.StartCoroutine(this.AttemptForceSnapAtEndOfFrame(objectToSnap));
					return;
				}
				this.AttemptForceSnap(objectToSnap);
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00067A18 File Offset: 0x00065C18
		public virtual void ForceUnsnap()
		{
			if (this.isSnapped && this.currentSnappedObject != null)
			{
				PhotonObjectInteract photonObjectInteract = this.ValidSnapObject(this.currentSnappedObject, false, true);
				if (photonObjectInteract != null)
				{
					photonObjectInteract.ToggleSnapDropZone(this, false);
				}
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00067A5B File Offset: 0x00065C5B
		public virtual bool ValidSnappableObjectIsHovering()
		{
			return this.currentValidSnapObjects.Count > 0;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00067A6B File Offset: 0x00065C6B
		public virtual bool IsObjectHovering(GameObject checkObject)
		{
			return this.currentValidSnapObjects.Contains(checkObject);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00067A79 File Offset: 0x00065C79
		public virtual List<GameObject> GetHoveringObjects()
		{
			return this.currentValidSnapObjects;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00067A81 File Offset: 0x00065C81
		public virtual GameObject GetCurrentSnappedObject()
		{
			return this.currentSnappedObject;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00067A89 File Offset: 0x00065C89
		protected virtual void Awake()
		{
			if (Application.isPlaying)
			{
				this.InitaliseHighlightObject(false);
				this.view = base.GetComponent<PhotonView>();
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00067AA5 File Offset: 0x00065CA5
		private void Start()
		{
			if (Application.isPlaying && (!this.view.isMine || !PhotonNetwork.inRoom) && base.transform.root.CompareTag("Player"))
			{
				base.GetComponent<Collider>().enabled = false;
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00067AE8 File Offset: 0x00065CE8
		protected virtual void OnApplicationQuit()
		{
			if (this.objectHighlighter != null)
			{
				this.objectHighlighter.Unhighlight(null, 0f);
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00067B1C File Offset: 0x00065D1C
		protected virtual void OnEnable()
		{
			if (!VRTK_SharedMethods.IsEditTime() && Application.isPlaying && this.defaultSnappedObject != null)
			{
				this.ForceSnap(this.defaultSnappedObject);
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00067B46 File Offset: 0x00065D46
		protected virtual void Update()
		{
			this.CheckSnappedItemExists();
			this.CheckPrefabUpdate();
			this.CreateHighlightersInEditor();
			this.CheckCurrentValidSnapObjectStillValid();
			this.previousPrefab = this.highlightObjectPrefab;
			this.SetObjectHighlight();
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00067B72 File Offset: 0x00065D72
		protected virtual void OnTriggerEnter(Collider collider)
		{
			if (!this.isSnapped)
			{
				this.ToggleHighlight(collider, true);
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00067B84 File Offset: 0x00065D84
		protected virtual void OnTriggerExit(Collider collider)
		{
			if (this.IsObjectHovering(collider.gameObject))
			{
				this.ToggleHighlight(collider, false);
			}
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00067B9C File Offset: 0x00065D9C
		protected virtual void OnTriggerStay(Collider collider)
		{
			if (!this.isSnapped && this.ValidSnapObject(collider.gameObject, true, true) && !collider.isTrigger)
			{
				this.AddCurrentValidSnapObject(collider.gameObject);
			}
			if (this.IsObjectHovering(collider.gameObject))
			{
				if (!this.isSnapped)
				{
					this.ToggleHighlight(collider, true);
				}
				if (!collider.isTrigger)
				{
					this.SnapObject(collider);
				}
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00067C08 File Offset: 0x00065E08
		protected virtual PhotonObjectInteract ValidSnapObject(GameObject checkObject, bool grabState, bool checkGrabState = true)
		{
			PhotonObjectInteract component = checkObject.GetComponent<PhotonObjectInteract>();
			if (component == null)
			{
				return null;
			}
			if (VRTK_PolicyList.Check(component.gameObject, this.validObjectListPolicy))
			{
				return null;
			}
			if (component.view == null)
			{
				return null;
			}
			if (component.isFixedItem || !component.isProp)
			{
				return null;
			}
			if (!this.isHeadCamDropZone && component.GetComponent<CCTV>() && component.GetComponent<CCTV>().isHeadCamera)
			{
				return null;
			}
			if (component.GetComponent<Tripod>())
			{
				return null;
			}
			if (component.isGrabbed != grabState)
			{
				return null;
			}
			if (!component.view.isMine)
			{
				return null;
			}
			return component;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00067CAC File Offset: 0x00065EAC
		protected virtual string ObjectPath(string name)
		{
			return "HighlightContainer/" + name;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00067CB9 File Offset: 0x00065EB9
		protected virtual void CheckSnappedItemExists()
		{
			if (this.isSnapped && this.currentSnappedObject == null)
			{
				this.OnObjectUnsnappedFromDropZone(this.SetSnapDropZoneEvent(this.currentSnappedObject));
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00067CE3 File Offset: 0x00065EE3
		protected virtual void CheckPrefabUpdate()
		{
			if (this.previousPrefab != null && this.previousPrefab != this.highlightObjectPrefab)
			{
				this.DeleteHighlightObject();
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00067D0C File Offset: 0x00065F0C
		protected virtual void SetObjectHighlight()
		{
			if (this.highlightAlwaysActive && !this.isSnapped && !this.isHighlighted)
			{
				this.highlightObject.SetActive(true);
			}
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00067D34 File Offset: 0x00065F34
		protected virtual void CreateHighlightersInEditor()
		{
			if (VRTK_SharedMethods.IsEditTime())
			{
				this.GenerateHighlightObject();
				if (this.snapType == VRTK_SnapDropZone.SnapTypes.UseJoint && base.GetComponent<Joint>() == null)
				{
					VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
					{
						"SnapDropZone:" + base.name,
						"Joint",
						"the same",
						" because the `Snap Type` is set to `Use Joint`"
					}));
				}
				this.GenerateEditorHighlightObject();
				this.ForceSetObjects();
				if (this.highlightEditorObject != null)
				{
					this.highlightEditorObject.SetActive(this.displayDropZoneInEditor);
				}
			}
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00067DD0 File Offset: 0x00065FD0
		protected virtual void CheckCurrentValidSnapObjectStillValid()
		{
			for (int i = 0; i < this.currentValidSnapObjects.Count; i++)
			{
				if (this.currentValidSnapObjects[i] != null)
				{
					VRTK_InteractableObject componentInParent = this.currentValidSnapObjects[i].GetComponentInParent<VRTK_InteractableObject>();
					if (componentInParent != null && componentInParent.GetStoredSnapDropZone() != null && componentInParent.GetStoredSnapDropZone() != base.gameObject)
					{
						this.RemoveCurrentValidSnapObject(this.currentValidSnapObjects[i]);
						if (this.isHighlighted && this.highlightObject != null && !this.highlightAlwaysActive)
						{
							this.highlightObject.SetActive(false);
						}
					}
				}
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00067E88 File Offset: 0x00066088
		protected virtual void ForceSetObjects()
		{
			if (this.highlightEditorObject == null)
			{
				Transform transform = base.transform.Find(this.ObjectPath("EditorHighlightObject"));
				this.highlightEditorObject = (transform ? transform.gameObject : null);
			}
			if (this.highlightObject == null)
			{
				Transform transform2 = base.transform.Find(this.ObjectPath("HighlightObject"));
				this.highlightObject = (transform2 ? transform2.gameObject : null);
			}
			if (this.highlightContainer == null)
			{
				Transform transform3 = base.transform.Find("HighlightContainer");
				this.highlightContainer = (transform3 ? transform3.gameObject : null);
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00067F44 File Offset: 0x00066144
		protected virtual void GenerateContainer()
		{
			if (this.highlightContainer == null || base.transform.Find("HighlightContainer") == null)
			{
				this.highlightContainer = new GameObject("HighlightContainer");
				this.highlightContainer.transform.SetParent(base.transform);
				this.highlightContainer.transform.localPosition = Vector3.zero;
				this.highlightContainer.transform.localRotation = Quaternion.identity;
				this.highlightContainer.transform.localScale = Vector3.one;
			}
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00067FDC File Offset: 0x000661DC
		protected virtual void SetContainer()
		{
			Transform transform = base.transform.Find("HighlightContainer");
			if (transform != null)
			{
				this.highlightContainer = transform.gameObject;
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0006800F File Offset: 0x0006620F
		protected virtual void GenerateObjects()
		{
			this.GenerateHighlightObject();
			if (this.highlightObject != null && this.objectHighlighter == null)
			{
				this.InitialiseHighlighter();
			}
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0006803C File Offset: 0x0006623C
		protected virtual void SnapObject(Collider collider)
		{
			PhotonObjectInteract photonObjectInteract = this.ValidSnapObject(collider.gameObject, false, true);
			if (this.willSnap && !this.isSnapped && photonObjectInteract != null && !photonObjectInteract.IsInSnapDropZone())
			{
				if (base.GetComponentInChildren<PhotonObjectInteract>())
				{
					return;
				}
				if (base.transform.root.CompareTag("Player") && !this.view.isMine)
				{
					return;
				}
				if (this.highlightObject != null)
				{
					this.highlightObject.SetActive(false);
				}
				Vector3 newLocalScale = this.GetNewLocalScale(photonObjectInteract);
				if (this.transitionInPlace != null)
				{
					base.StopCoroutine(this.transitionInPlace);
				}
				this.isSnapped = true;
				this.currentSnappedObject = photonObjectInteract.gameObject;
				if (this.cloneNewOnUnsnap)
				{
					this.CreatePermanentClone();
				}
				this.transitionInPlace = base.StartCoroutine(this.UpdateTransformDimensions(photonObjectInteract, this.highlightContainer, newLocalScale, this.snapDuration));
				photonObjectInteract.ToggleSnapDropZone(this, true);
			}
			this.isSnapped = ((!this.isSnapped || !photonObjectInteract || !photonObjectInteract.IsGrabbed(null)) && this.isSnapped);
			this.wasSnapped = false;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00068168 File Offset: 0x00066368
		protected virtual void CreatePermanentClone()
		{
			VRTK_BaseHighlighter component = this.currentSnappedObject.GetComponent<VRTK_BaseHighlighter>();
			if (component != null)
			{
				component.Unhighlight(null, 0f);
			}
			this.objectToClone = Object.Instantiate<GameObject>(this.currentSnappedObject);
			Collider[] componentsInChildren = this.currentSnappedObject.GetComponentsInChildren<Collider>();
			this.clonedObjectColliderStates = new bool[componentsInChildren.Length];
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Collider collider = componentsInChildren[i];
				this.clonedObjectColliderStates[i] = collider.isTrigger;
				collider.isTrigger = true;
			}
			this.objectToClone.SetActive(false);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00068200 File Offset: 0x00066400
		protected virtual void ResetPermanentCloneColliders(GameObject objectToReset)
		{
			if (objectToReset != null && this.clonedObjectColliderStates.Length != 0)
			{
				Collider[] componentsInChildren = objectToReset.GetComponentsInChildren<Collider>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Collider collider = componentsInChildren[i];
					if (this.clonedObjectColliderStates.Length > i)
					{
						collider.isTrigger = this.clonedObjectColliderStates[i];
					}
				}
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00068254 File Offset: 0x00066454
		protected virtual void ResnapPermanentClone()
		{
			if (this.objectToClone != null)
			{
				float num = this.snapDuration;
				this.snapDuration = 0f;
				this.objectToClone.SetActive(true);
				this.ResetPermanentCloneColliders(this.objectToClone);
				this.ForceSnap(this.objectToClone);
				this.snapDuration = num;
			}
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000682AC File Offset: 0x000664AC
		protected virtual void UnsnapObject()
		{
			this.ResetPermanentCloneColliders(this.currentSnappedObject);
			this.isSnapped = false;
			this.wasSnapped = true;
			this.currentSnappedObject = null;
			this.ResetSnapDropZoneJoint();
			if (this.transitionInPlace != null)
			{
				base.StopCoroutine(this.transitionInPlace);
			}
			if (this.cloneNewOnUnsnap)
			{
				this.ResnapPermanentClone();
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00068304 File Offset: 0x00066504
		protected virtual Vector3 GetNewLocalScale(VRTK_InteractableObject ioCheck)
		{
			Vector3 result = ioCheck.transform.localScale;
			if (this.applyScalingOnSnap)
			{
				ioCheck.StoreLocalScale();
				result = Vector3.Scale(ioCheck.transform.localScale, base.transform.localScale);
			}
			return result;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00068348 File Offset: 0x00066548
		protected virtual IEnumerator UpdateTransformDimensions(VRTK_InteractableObject ioCheck, GameObject endSettings, Vector3 endScale, float duration)
		{
			Transform transform = ioCheck.transform;
			Vector3 position = transform.position;
			Quaternion rotation = transform.rotation;
			Vector3 localScale = transform.localScale;
			bool isKinematic = ioCheck.isKinematic;
			ioCheck.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
			ioCheck.isKinematic = true;
			transform.position = endSettings.transform.position;
			transform.rotation = endSettings.transform.rotation;
			transform.localScale = endScale;
			if (isKinematic)
			{
				ioCheck.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
			}
			ioCheck.isKinematic = isKinematic;
			this.SetDropSnapType(ioCheck);
			yield return new WaitForEndOfFrame();
			yield break;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0006836C File Offset: 0x0006656C
		protected virtual void SetDropSnapType(VRTK_InteractableObject ioCheck)
		{
			if (PhotonNetwork.inRoom)
			{
				this.view.RPC("SyncDropZone", PhotonTargets.AllBuffered, new object[]
				{
					ioCheck.GetComponent<PhotonView>().viewID
				});
			}
			else
			{
				this.SyncDropZone(ioCheck.GetComponent<PhotonView>().viewID);
			}
			if (this.isHeadCamDropZone && base.transform.root.CompareTag("Player") && ioCheck.GetComponent<CCTV>() && ioCheck.GetComponent<CCTV>().isHeadCamera && GameController.instance != null)
			{
				GameController.instance.myPlayer.player.playerHeadCamera.VRGrabOrPlaceCamera(ioCheck.GetComponent<PhotonView>().viewID, true);
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00068428 File Offset: 0x00066628
		[PunRPC]
		private void SyncDropZone(int viewID)
		{
			if (PhotonView.Find(viewID) == null)
			{
				return;
			}
			PhotonObjectInteract component = PhotonView.Find(viewID).GetComponent<PhotonObjectInteract>();
			if (component == null)
			{
				return;
			}
			if (this.isBeltDropZone && !this.view.isMine)
			{
				component.isGrabbed = true;
			}
			switch (this.snapType)
			{
			case VRTK_SnapDropZone.SnapTypes.UseKinematic:
				component.SaveCurrentState();
				component.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
				component.isKinematic = true;
				break;
			case VRTK_SnapDropZone.SnapTypes.UseJoint:
				this.SetSnapDropZoneJoint(component.GetComponent<Rigidbody>());
				break;
			case VRTK_SnapDropZone.SnapTypes.UseParenting:
				component.SaveCurrentState();
				component.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
				component.isKinematic = true;
				component.transform.SetParent(base.transform);
				if (this.isBeltDropZone)
				{
					Quaternion localRotation = component.transform.localRotation;
					localRotation.eulerAngles = component.localPlayerRotation;
					component.transform.localRotation = localRotation;
				}
				break;
			}
			this.OnObjectSnappedToDropZone(this.SetSnapDropZoneEvent(component.gameObject));
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00068528 File Offset: 0x00066728
		protected virtual void SetSnapDropZoneJoint(Rigidbody snapTo)
		{
			Joint component = base.GetComponent<Joint>();
			if (component == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"SnapDropZone:" + base.name,
					"Joint",
					"the same",
					" because the `Snap Type` is set to `Use Joint`"
				}));
				return;
			}
			if (snapTo == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_SnapDropZone",
					"Rigidbody",
					"the `VRTK_InteractableObject`"
				}));
				return;
			}
			component.connectedBody = snapTo;
			this.originalJointCollisionState = component.enableCollision;
			component.enableCollision = true;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000685D0 File Offset: 0x000667D0
		protected virtual void ResetSnapDropZoneJoint()
		{
			Joint component = base.GetComponent<Joint>();
			if (component != null)
			{
				component.enableCollision = this.originalJointCollisionState;
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x000685F9 File Offset: 0x000667F9
		protected virtual void AddCurrentValidSnapObject(GameObject givenObject)
		{
			if (!this.currentValidSnapObjects.Contains(givenObject))
			{
				this.currentValidSnapObjects.Add(givenObject);
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00068615 File Offset: 0x00066815
		protected virtual void RemoveCurrentValidSnapObject(GameObject givenObject)
		{
			if (this.currentValidSnapObjects.Contains(givenObject))
			{
				this.currentValidSnapObjects.Remove(givenObject);
			}
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00068632 File Offset: 0x00066832
		protected virtual void AttemptForceSnap(GameObject objectToSnap)
		{
			this.willSnap = true;
			this.AddCurrentValidSnapObject(objectToSnap);
			this.OnTriggerStay(objectToSnap.GetComponentInChildren<Collider>());
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0006864E File Offset: 0x0006684E
		protected virtual IEnumerator AttemptForceSnapAtEndOfFrame(GameObject objectToSnap)
		{
			yield return new WaitForEndOfFrame();
			this.AttemptForceSnap(objectToSnap);
			yield break;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00068664 File Offset: 0x00066864
		protected virtual void ToggleHighlight(Collider collider, bool state)
		{
			PhotonObjectInteract photonObjectInteract = this.ValidSnapObject(collider.gameObject, true, state);
			if (this.highlightObject != null && photonObjectInteract != null)
			{
				this.highlightObject.SetActive(state);
				photonObjectInteract.SetSnapDropZoneHover(this, state);
				this.willSnap = state;
				this.isHighlighted = state;
				if (state)
				{
					if (!this.IsObjectHovering(collider.gameObject) || this.wasSnapped)
					{
						this.OnObjectEnteredSnapDropZone(this.SetSnapDropZoneEvent(collider.gameObject));
					}
					this.AddCurrentValidSnapObject(collider.gameObject);
					return;
				}
				this.OnObjectExitedSnapDropZone(this.SetSnapDropZoneEvent(collider.gameObject));
				this.RemoveCurrentValidSnapObject(collider.gameObject);
			}
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00068714 File Offset: 0x00066914
		protected virtual void CopyObject(GameObject objectBlueprint, ref GameObject clonedObject, string givenName)
		{
			this.GenerateContainer();
			Vector3 localScale = base.transform.localScale;
			base.transform.localScale = Vector3.one;
			clonedObject = Object.Instantiate<GameObject>(objectBlueprint, this.highlightContainer.transform);
			clonedObject.name = givenName;
			clonedObject.transform.localPosition = Vector3.zero;
			clonedObject.transform.localRotation = Quaternion.identity;
			base.transform.localScale = localScale;
			this.CleanHighlightObject(clonedObject);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00068794 File Offset: 0x00066994
		protected virtual void GenerateHighlightObject()
		{
			if (this.highlightObjectPrefab != null && this.highlightObject == null && base.transform.Find(this.ObjectPath("HighlightObject")) == null)
			{
				this.CopyObject(this.highlightObjectPrefab, ref this.highlightObject, "HighlightObject");
			}
			Transform transform = base.transform.Find(this.ObjectPath("HighlightObject"));
			if (transform != null && this.highlightObject == null)
			{
				this.highlightObject = transform.gameObject;
			}
			if (this.highlightObjectPrefab == null && this.highlightObject != null)
			{
				this.DeleteHighlightObject();
			}
			if (this.highlightObject != null)
			{
				this.highlightObject.SetActive(false);
			}
			this.SetContainer();
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0006886E File Offset: 0x00066A6E
		protected virtual void DeleteHighlightObject()
		{
			this.ChooseDestroyType(base.transform.Find("HighlightContainer"));
			this.highlightContainer = null;
			this.highlightObject = null;
			this.objectHighlighter = null;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0006889C File Offset: 0x00066A9C
		protected virtual void GenerateEditorHighlightObject()
		{
			if (this.highlightObject != null && this.highlightEditorObject == null && base.transform.Find(this.ObjectPath("EditorHighlightObject")) == null)
			{
				this.CopyObject(this.highlightObject, ref this.highlightEditorObject, "EditorHighlightObject");
				Renderer[] componentsInChildren = this.highlightEditorObject.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].material = (Resources.Load("SnapDropZoneEditorObject") as Material);
				}
				this.highlightEditorObject.SetActive(true);
			}
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0006893C File Offset: 0x00066B3C
		protected virtual void CleanHighlightObject(GameObject objectToClean)
		{
			VRTK_SnapDropZone[] componentsInChildren = objectToClean.GetComponentsInChildren<VRTK_SnapDropZone>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.ChooseDestroyType(componentsInChildren[i].gameObject);
			}
			string[] array = new string[]
			{
				"Transform",
				"MeshFilter",
				"MeshRenderer",
				"SkinnedMeshRenderer",
				"VRTK_GameObjectLinker"
			};
			foreach (Component component in objectToClean.GetComponentsInChildren<Component>(true))
			{
				bool flag = false;
				foreach (string str in array)
				{
					if (component.GetType().ToString().Contains("." + str))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.ChooseDestroyType(component);
				}
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00068A0C File Offset: 0x00066C0C
		protected virtual void InitialiseHighlighter()
		{
			VRTK_BaseHighlighter activeHighlighter = VRTK_BaseHighlighter.GetActiveHighlighter(base.gameObject);
			if (activeHighlighter == null)
			{
				this.highlightObject.AddComponent<VRTK_MaterialColorSwapHighlighter>();
			}
			else
			{
				VRTK_SharedMethods.CloneComponent(activeHighlighter, this.highlightObject, false);
			}
			this.objectHighlighter = this.highlightObject.GetComponent<VRTK_BaseHighlighter>();
			this.objectHighlighter.unhighlightOnDisable = false;
			this.objectHighlighter.Initialise(new Color?(this.highlightColor), null);
			this.objectHighlighter.Highlight(new Color?(this.highlightColor), 0f);
			if (this.objectHighlighter.UsesClonedObject())
			{
				foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>(true))
				{
					if (!VRTK_PlayerObject.IsPlayerObject(renderer.gameObject, VRTK_PlayerObject.ObjectTypes.Highlighter))
					{
						renderer.enabled = false;
					}
				}
			}
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00068AD5 File Offset: 0x00066CD5
		protected virtual void ChooseDestroyType(Transform deleteTransform)
		{
			if (deleteTransform != null)
			{
				this.ChooseDestroyType(deleteTransform.gameObject);
			}
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00068AEC File Offset: 0x00066CEC
		protected virtual void ChooseDestroyType(GameObject deleteObject)
		{
			if (VRTK_SharedMethods.IsEditTime())
			{
				if (deleteObject != null)
				{
					Object.DestroyImmediate(deleteObject);
					return;
				}
			}
			else if (deleteObject != null)
			{
				Object.Destroy(deleteObject);
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00068AEC File Offset: 0x00066CEC
		protected virtual void ChooseDestroyType(Component deleteComponent)
		{
			if (VRTK_SharedMethods.IsEditTime())
			{
				if (deleteComponent != null)
				{
					Object.DestroyImmediate(deleteComponent);
					return;
				}
			}
			else if (deleteComponent != null)
			{
				Object.Destroy(deleteComponent);
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00068B14 File Offset: 0x00066D14
		protected virtual void OnDrawGizmosSelected()
		{
			if (this.highlightObject != null && !this.displayDropZoneInEditor)
			{
				Vector3 size = VRTK_SharedMethods.GetBounds(this.highlightObject.transform, null, null).size * 1.05f;
				Gizmos.color = Color.red;
				Gizmos.DrawWireCube(this.highlightObject.transform.position, size);
			}
		}

		// Token: 0x04001077 RID: 4215
		[Tooltip("A game object that is used to draw the highlighted destination for within the drop zone. This object will also be created in the Editor for easy placement.")]
		public GameObject highlightObjectPrefab;

		// Token: 0x04001078 RID: 4216
		[Tooltip("The Snap Type to apply when a valid interactable object is dropped within the snap zone.")]
		public VRTK_SnapDropZone.SnapTypes snapType;

		// Token: 0x04001079 RID: 4217
		[Tooltip("The amount of time it takes for the object being snapped to move into the new snapped position, rotation and scale.")]
		public float snapDuration;

		// Token: 0x0400107A RID: 4218
		[Tooltip("If this is checked then the scaled size of the snap drop zone will be applied to the object that is snapped to it.")]
		public bool applyScalingOnSnap;

		// Token: 0x0400107B RID: 4219
		[Tooltip("If this is checked then when the snapped object is unsnapped from the drop zone, a clone of the unsnapped object will be snapped back into the drop zone.")]
		public bool cloneNewOnUnsnap;

		// Token: 0x0400107C RID: 4220
		[Tooltip("The colour to use when showing the snap zone is active.")]
		public Color highlightColor;

		// Token: 0x0400107D RID: 4221
		[Tooltip("The highlight object will always be displayed when the snap drop zone is available even if a valid item isn't being hovered over.")]
		public bool highlightAlwaysActive;

		// Token: 0x0400107E RID: 4222
		[Tooltip("A specified VRTK_PolicyList to use to determine which interactable objects will be snapped to the snap drop zone on release.")]
		public VRTK_PolicyList validObjectListPolicy;

		// Token: 0x0400107F RID: 4223
		[Tooltip("If this is checked then the drop zone highlight section will be displayed in the scene editor window.")]
		public bool displayDropZoneInEditor = true;

		// Token: 0x04001080 RID: 4224
		[Tooltip("The game object to snap into the dropzone when the drop zone is enabled. The game object must be valid in any given policy list to snap.")]
		public GameObject defaultSnappedObject;

		// Token: 0x04001085 RID: 4229
		protected GameObject previousPrefab;

		// Token: 0x04001086 RID: 4230
		protected GameObject highlightContainer;

		// Token: 0x04001087 RID: 4231
		protected GameObject highlightObject;

		// Token: 0x04001088 RID: 4232
		protected GameObject highlightEditorObject;

		// Token: 0x04001089 RID: 4233
		protected List<GameObject> currentValidSnapObjects = new List<GameObject>();

		// Token: 0x0400108A RID: 4234
		protected GameObject currentSnappedObject;

		// Token: 0x0400108B RID: 4235
		protected GameObject objectToClone;

		// Token: 0x0400108C RID: 4236
		protected bool[] clonedObjectColliderStates = new bool[0];

		// Token: 0x0400108D RID: 4237
		protected VRTK_BaseHighlighter objectHighlighter;

		// Token: 0x0400108E RID: 4238
		protected bool willSnap;

		// Token: 0x0400108F RID: 4239
		protected bool isSnapped;

		// Token: 0x04001090 RID: 4240
		protected bool wasSnapped;

		// Token: 0x04001091 RID: 4241
		protected bool isHighlighted;

		// Token: 0x04001092 RID: 4242
		protected Coroutine transitionInPlace;

		// Token: 0x04001093 RID: 4243
		protected bool originalJointCollisionState;

		// Token: 0x04001094 RID: 4244
		protected const string HIGHLIGHT_CONTAINER_NAME = "HighlightContainer";

		// Token: 0x04001095 RID: 4245
		protected const string HIGHLIGHT_OBJECT_NAME = "HighlightObject";

		// Token: 0x04001096 RID: 4246
		protected const string HIGHLIGHT_EDITOR_OBJECT_NAME = "EditorHighlightObject";

		// Token: 0x04001097 RID: 4247
		private PhotonView view;

		// Token: 0x04001098 RID: 4248
		[SerializeField]
		private bool isHeadCamDropZone;

		// Token: 0x04001099 RID: 4249
		[SerializeField]
		private bool isBeltDropZone;

		// Token: 0x020005BB RID: 1467
		public enum SnapTypes
		{
			// Token: 0x04002705 RID: 9989
			UseKinematic,
			// Token: 0x04002706 RID: 9990
			UseJoint,
			// Token: 0x04002707 RID: 9991
			UseParenting
		}
	}
}
