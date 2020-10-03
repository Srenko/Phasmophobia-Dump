using System;
using System.Collections;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200024D RID: 589
	public class VRTK_DestinationPoint : VRTK_DestinationMarker
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06001112 RID: 4370 RVA: 0x00064364 File Offset: 0x00062564
		// (remove) Token: 0x06001113 RID: 4371 RVA: 0x0006439C File Offset: 0x0006259C
		public event DestinationPointEventHandler DestinationPointEnabled;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06001114 RID: 4372 RVA: 0x000643D4 File Offset: 0x000625D4
		// (remove) Token: 0x06001115 RID: 4373 RVA: 0x0006440C File Offset: 0x0006260C
		public event DestinationPointEventHandler DestinationPointDisabled;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06001116 RID: 4374 RVA: 0x00064444 File Offset: 0x00062644
		// (remove) Token: 0x06001117 RID: 4375 RVA: 0x0006447C File Offset: 0x0006267C
		public event DestinationPointEventHandler DestinationPointLocked;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06001118 RID: 4376 RVA: 0x000644B4 File Offset: 0x000626B4
		// (remove) Token: 0x06001119 RID: 4377 RVA: 0x000644EC File Offset: 0x000626EC
		public event DestinationPointEventHandler DestinationPointUnlocked;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600111A RID: 4378 RVA: 0x00064524 File Offset: 0x00062724
		// (remove) Token: 0x0600111B RID: 4379 RVA: 0x0006455C File Offset: 0x0006275C
		public event DestinationPointEventHandler DestinationPointReset;

		// Token: 0x0600111C RID: 4380 RVA: 0x00064591 File Offset: 0x00062791
		public virtual void OnDestinationPointEnabled()
		{
			if (this.DestinationPointEnabled != null)
			{
				this.DestinationPointEnabled(this);
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000645A7 File Offset: 0x000627A7
		public virtual void OnDestinationPointDisabled()
		{
			if (this.DestinationPointDisabled != null)
			{
				this.DestinationPointDisabled(this);
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000645BD File Offset: 0x000627BD
		public virtual void OnDestinationPointLocked()
		{
			if (this.DestinationPointLocked != null)
			{
				this.DestinationPointLocked(this);
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000645D3 File Offset: 0x000627D3
		public virtual void OnDestinationPointUnlocked()
		{
			if (this.DestinationPointUnlocked != null)
			{
				this.DestinationPointUnlocked(this);
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000645E9 File Offset: 0x000627E9
		public virtual void OnDestinationPointReset()
		{
			if (this.DestinationPointReset != null)
			{
				this.DestinationPointReset(this);
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x000645FF File Offset: 0x000627FF
		public virtual void ResetDestinationPoint()
		{
			this.ResetPoint();
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00064614 File Offset: 0x00062814
		protected override void OnEnable()
		{
			base.OnEnable();
			this.CreateColliderIfRequired();
			this.SetupRigidbody();
			this.initaliseListeners = base.StartCoroutine(this.ManageDestinationMarkersAtEndOfFrame());
			this.ResetPoint();
			this.currentTeleportState = this.enableTeleport;
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			this.headset = VRTK_DeviceFinder.HeadsetTransform();
			this.destinationLocation = ((this.destinationLocation != null) ? this.destinationLocation : base.transform);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00064690 File Offset: 0x00062890
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.initaliseListeners != null)
			{
				base.StopCoroutine(this.initaliseListeners);
			}
			if (this.setDestination != null)
			{
				base.StopCoroutine(this.setDestination);
			}
			this.ManageDestinationMarkers(false);
			if (this.createdCollider)
			{
				Object.Destroy(this.pointCollider);
			}
			if (this.createdRigidbody)
			{
				Object.Destroy(this.pointRigidbody);
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000646F8 File Offset: 0x000628F8
		protected virtual void Update()
		{
			if (this.enableTeleport != this.currentTeleportState)
			{
				this.ResetPoint();
			}
			this.currentTeleportState = this.enableTeleport;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0006471C File Offset: 0x0006291C
		protected virtual void CreateColliderIfRequired()
		{
			this.pointCollider = base.GetComponentInChildren<Collider>();
			this.createdCollider = false;
			if (this.pointCollider == null)
			{
				this.pointCollider = base.gameObject.AddComponent<SphereCollider>();
				this.createdCollider = true;
			}
			this.pointCollider.isTrigger = true;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00064770 File Offset: 0x00062970
		protected virtual void SetupRigidbody()
		{
			this.pointRigidbody = base.GetComponent<Rigidbody>();
			this.createdRigidbody = false;
			if (this.pointRigidbody == null)
			{
				this.pointRigidbody = base.gameObject.AddComponent<Rigidbody>();
				this.createdRigidbody = true;
			}
			this.pointRigidbody.isKinematic = true;
			this.pointRigidbody.useGravity = false;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x000647CE File Offset: 0x000629CE
		protected virtual IEnumerator ManageDestinationMarkersAtEndOfFrame()
		{
			yield return new WaitForEndOfFrame();
			if (base.enabled)
			{
				this.ManageDestinationMarkers(true);
			}
			yield break;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x000647E0 File Offset: 0x000629E0
		protected virtual void ManageDestinationMarkers(bool state)
		{
			this.ManageDestinationMarkerListeners(VRTK_DeviceFinder.GetControllerLeftHand(false), state);
			this.ManageDestinationMarkerListeners(VRTK_DeviceFinder.GetControllerRightHand(false), state);
			foreach (VRTK_DestinationMarker vrtk_DestinationMarker in VRTK_ObjectCache.registeredDestinationMarkers)
			{
				this.ManageDestinationMarkerListeners(vrtk_DestinationMarker.gameObject, state);
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00064854 File Offset: 0x00062A54
		protected virtual void ManageDestinationMarkerListeners(GameObject markerMaker, bool register)
		{
			if (markerMaker != null)
			{
				foreach (VRTK_DestinationMarker vrtk_DestinationMarker in markerMaker.GetComponentsInChildren<VRTK_DestinationMarker>())
				{
					if (!(vrtk_DestinationMarker == this))
					{
						if (register)
						{
							vrtk_DestinationMarker.DestinationMarkerEnter += this.DoDestinationMarkerEnter;
							vrtk_DestinationMarker.DestinationMarkerExit += this.DoDestinationMarkerExit;
							vrtk_DestinationMarker.DestinationMarkerSet += this.DoDestinationMarkerSet;
						}
						else
						{
							vrtk_DestinationMarker.DestinationMarkerEnter -= this.DoDestinationMarkerEnter;
							vrtk_DestinationMarker.DestinationMarkerExit -= this.DoDestinationMarkerExit;
							vrtk_DestinationMarker.DestinationMarkerSet -= this.DoDestinationMarkerSet;
						}
					}
				}
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0006490C File Offset: 0x00062B0C
		protected virtual void DoDestinationMarkerEnter(object sender, DestinationMarkerEventArgs e)
		{
			if (!this.isActive && e.raycastHit.transform == base.transform)
			{
				this.isActive = true;
				this.ToggleCursor(sender, false);
				this.EnablePoint();
				this.OnDestinationMarkerEnter(this.SetDestinationMarkerEvent(0f, e.raycastHit.transform, e.raycastHit, e.raycastHit.transform.position, e.controllerReference, false, this.GetRotation()));
			}
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00064990 File Offset: 0x00062B90
		protected virtual void DoDestinationMarkerExit(object sender, DestinationMarkerEventArgs e)
		{
			if (this.isActive && e.raycastHit.transform == base.transform)
			{
				this.isActive = false;
				this.ToggleCursor(sender, true);
				this.ResetPoint();
				this.OnDestinationMarkerExit(this.SetDestinationMarkerEvent(0f, e.raycastHit.transform, e.raycastHit, e.raycastHit.transform.position, e.controllerReference, false, this.GetRotation()));
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00064A14 File Offset: 0x00062C14
		protected virtual void DoDestinationMarkerSet(object sender, DestinationMarkerEventArgs e)
		{
			if (e.raycastHit.transform == base.transform)
			{
				VRTK_DestinationPoint.currentDestinationPoint = this;
				if (this.snapToPoint)
				{
					e.raycastHit.point = this.destinationLocation.position;
					this.setDestination = base.StartCoroutine(this.DoDestinationMarkerSetAtEndOfFrame(e));
					return;
				}
			}
			else
			{
				if (VRTK_DestinationPoint.currentDestinationPoint != this)
				{
					this.ResetPoint();
					return;
				}
				if (VRTK_DestinationPoint.currentDestinationPoint != null && e.raycastHit.transform != VRTK_DestinationPoint.currentDestinationPoint.transform)
				{
					VRTK_DestinationPoint.currentDestinationPoint = null;
					this.ResetPoint();
				}
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00064ABD File Offset: 0x00062CBD
		protected virtual IEnumerator DoDestinationMarkerSetAtEndOfFrame(DestinationMarkerEventArgs e)
		{
			yield return new WaitForEndOfFrame();
			if (base.enabled)
			{
				e.raycastHit.point = this.destinationLocation.position;
				this.DisablePoint();
				this.OnDestinationMarkerSet(this.SetDestinationMarkerEvent(e.distance, base.transform, e.raycastHit, this.destinationLocation.position, e.controllerReference, false, this.GetRotation()));
			}
			yield break;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00064AD4 File Offset: 0x00062CD4
		protected virtual void ToggleCursor(object sender, bool state)
		{
			if ((this.hidePointerCursorOnHover || this.hideDirectionIndicatorOnHover) && sender.GetType().Equals(typeof(VRTK_Pointer)))
			{
				VRTK_Pointer vrtk_Pointer = (VRTK_Pointer)sender;
				if (vrtk_Pointer != null && vrtk_Pointer.pointerRenderer != null)
				{
					this.TogglePointerCursor(vrtk_Pointer.pointerRenderer, state);
					this.ToggleDirectionIndicator(vrtk_Pointer.pointerRenderer, state);
				}
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00064B40 File Offset: 0x00062D40
		protected virtual void TogglePointerCursor(VRTK_BasePointerRenderer pointerRenderer, bool state)
		{
			if (this.hidePointerCursorOnHover)
			{
				if (!state)
				{
					this.storedCursorState = pointerRenderer.cursorVisibility;
					pointerRenderer.cursorVisibility = VRTK_BasePointerRenderer.VisibilityStates.AlwaysOff;
					return;
				}
				pointerRenderer.cursorVisibility = this.storedCursorState;
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00064B70 File Offset: 0x00062D70
		protected virtual void ToggleDirectionIndicator(VRTK_BasePointerRenderer pointerRenderer, bool state)
		{
			if (pointerRenderer.directionIndicator != null && this.hideDirectionIndicatorOnHover)
			{
				if (!state)
				{
					this.storedDirectionIndicatorState = pointerRenderer.directionIndicator.isActive;
					pointerRenderer.directionIndicator.isActive = false;
					return;
				}
				pointerRenderer.directionIndicator.isActive = this.storedDirectionIndicatorState;
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00064BC5 File Offset: 0x00062DC5
		protected virtual void EnablePoint()
		{
			this.ToggleObject(this.lockedCursorObject, false);
			this.ToggleObject(this.defaultCursorObject, false);
			this.ToggleObject(this.hoverCursorObject, true);
			this.OnDestinationPointEnabled();
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00064BF4 File Offset: 0x00062DF4
		protected virtual void DisablePoint()
		{
			this.pointCollider.enabled = false;
			this.ToggleObject(this.lockedCursorObject, false);
			this.ToggleObject(this.defaultCursorObject, false);
			this.ToggleObject(this.hoverCursorObject, false);
			this.OnDestinationPointDisabled();
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00064C30 File Offset: 0x00062E30
		protected virtual void ResetPoint()
		{
			if (this.snapToPoint && VRTK_DestinationPoint.currentDestinationPoint == this)
			{
				return;
			}
			this.ToggleObject(this.hoverCursorObject, false);
			if (this.enableTeleport)
			{
				this.pointCollider.enabled = true;
				this.ToggleObject(this.defaultCursorObject, true);
				this.ToggleObject(this.lockedCursorObject, false);
				this.OnDestinationPointUnlocked();
			}
			else
			{
				this.pointCollider.enabled = false;
				this.ToggleObject(this.lockedCursorObject, true);
				this.ToggleObject(this.defaultCursorObject, false);
				this.OnDestinationPointLocked();
			}
			this.OnDestinationPointReset();
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00064CC8 File Offset: 0x00062EC8
		protected virtual void ToggleObject(GameObject givenObject, bool state)
		{
			if (givenObject != null)
			{
				givenObject.SetActive(state);
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00064CDC File Offset: 0x00062EDC
		protected virtual Quaternion? GetRotation()
		{
			if (this.snapToRotation == VRTK_DestinationPoint.RotationTypes.NoRotation)
			{
				return null;
			}
			float num = (this.snapToRotation == VRTK_DestinationPoint.RotationTypes.RotateWithHeadsetOffset && this.playArea != null && this.headset != null) ? (this.playArea.eulerAngles.y - this.headset.eulerAngles.y) : 0f;
			return new Quaternion?(Quaternion.Euler(0f, this.destinationLocation.localEulerAngles.y + num, 0f));
		}

		// Token: 0x04000FF2 RID: 4082
		[Header("Destination Point Settings")]
		[Tooltip("The GameObject to use to represent the default cursor state.")]
		public GameObject defaultCursorObject;

		// Token: 0x04000FF3 RID: 4083
		[Tooltip("The GameObject to use to represent the hover cursor state.")]
		public GameObject hoverCursorObject;

		// Token: 0x04000FF4 RID: 4084
		[Tooltip("The GameObject to use to represent the locked cursor state.")]
		public GameObject lockedCursorObject;

		// Token: 0x04000FF5 RID: 4085
		[Tooltip("An optional transform to determine the destination location for the destination marker. This can be useful to offset the destination location from the destination point. If this is left empty then the destiantion point transform will be used.")]
		public Transform destinationLocation;

		// Token: 0x04000FF6 RID: 4086
		[Tooltip("If this is checked then after teleporting, the play area will be snapped to the origin of the destination point. If this is false then it's possible to teleport to anywhere within the destination point collider.")]
		public bool snapToPoint = true;

		// Token: 0x04000FF7 RID: 4087
		[Tooltip("If this is checked, then the pointer cursor will be hidden when a valid destination point is hovered over.")]
		public bool hidePointerCursorOnHover = true;

		// Token: 0x04000FF8 RID: 4088
		[Tooltip("If this is checked, then the pointer direction indicator will be hidden when a valid destination point is hovered over. A pointer direction indicator will always be hidden if snap to rotation is set.")]
		public bool hideDirectionIndicatorOnHover;

		// Token: 0x04000FF9 RID: 4089
		[Tooltip("Determines if the play area will be rotated to the rotation of the destination point upon the destination marker being set.")]
		public VRTK_DestinationPoint.RotationTypes snapToRotation;

		// Token: 0x04000FFA RID: 4090
		public static VRTK_DestinationPoint currentDestinationPoint;

		// Token: 0x04001000 RID: 4096
		protected Collider pointCollider;

		// Token: 0x04001001 RID: 4097
		protected bool createdCollider;

		// Token: 0x04001002 RID: 4098
		protected Rigidbody pointRigidbody;

		// Token: 0x04001003 RID: 4099
		protected bool createdRigidbody;

		// Token: 0x04001004 RID: 4100
		protected Coroutine initaliseListeners;

		// Token: 0x04001005 RID: 4101
		protected bool isActive;

		// Token: 0x04001006 RID: 4102
		protected VRTK_BasePointerRenderer.VisibilityStates storedCursorState;

		// Token: 0x04001007 RID: 4103
		protected bool storedDirectionIndicatorState;

		// Token: 0x04001008 RID: 4104
		protected Coroutine setDestination;

		// Token: 0x04001009 RID: 4105
		protected bool currentTeleportState;

		// Token: 0x0400100A RID: 4106
		protected Transform playArea;

		// Token: 0x0400100B RID: 4107
		protected Transform headset;

		// Token: 0x020005B2 RID: 1458
		public enum RotationTypes
		{
			// Token: 0x040026D6 RID: 9942
			NoRotation,
			// Token: 0x040026D7 RID: 9943
			RotateWithNoHeadsetOffset,
			// Token: 0x040026D8 RID: 9944
			RotateWithHeadsetOffset
		}
	}
}
