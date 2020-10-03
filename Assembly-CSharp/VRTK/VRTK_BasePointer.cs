using System;
using UnityEngine;
using UnityEngine.AI;

namespace VRTK
{
	// Token: 0x020002E1 RID: 737
	[Obsolete("`VRTK_BasePointer` has been replaced with `VRTK_Pointer`. This script will be removed in a future version of VRTK.")]
	public abstract class VRTK_BasePointer : VRTK_DestinationMarker
	{
		// Token: 0x060018C8 RID: 6344 RVA: 0x000843A1 File Offset: 0x000825A1
		public virtual bool IsActive()
		{
			return this.isActive;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000843A9 File Offset: 0x000825A9
		public virtual bool CanActivate()
		{
			return Time.time >= this.activateDelayTimer;
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000843BC File Offset: 0x000825BC
		public virtual void ToggleBeam(bool state)
		{
			VRTK_ControllerReference givenControllerReference = (this.controller != null) ? VRTK_ControllerReference.GetControllerReference(this.controller.gameObject) : null;
			if (state)
			{
				this.TurnOnBeam(givenControllerReference);
				return;
			}
			this.TurnOffBeam(givenControllerReference);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x000843FD File Offset: 0x000825FD
		protected virtual void Awake()
		{
			VRTK_PlayerObject.SetPlayerObject(base.gameObject, VRTK_PlayerObject.ObjectTypes.Pointer);
			this.originalPointerOriginTransform = this.pointerOriginTransform;
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00084424 File Offset: 0x00082624
		protected override void OnEnable()
		{
			base.OnEnable();
			this.pointerOriginTransform = ((this.originalPointerOriginTransform == null) ? VRTK_SDK_Bridge.GenerateControllerPointerOrigin(base.gameObject) : this.originalPointerOriginTransform);
			this.AttemptSetController();
			this.CreatePointerOriginTransformFollow();
			Material source = Resources.Load("WorldPointer") as Material;
			if (this.pointerMaterial != null)
			{
				source = this.pointerMaterial;
			}
			this.pointerMaterial = new Material(source);
			this.pointerMaterial.color = this.pointerMissColor;
			this.playAreaCursor = base.GetComponent<VRTK_PlayAreaCursor>();
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x000844B8 File Offset: 0x000826B8
		protected virtual void Start()
		{
			this.SetupController();
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000844C0 File Offset: 0x000826C0
		protected override void OnDisable()
		{
			base.OnDisable();
			this.DisableBeam();
			Object.Destroy(this.objectInteractor);
			this.destinationSetActive = false;
			this.pointerContactDistance = 0f;
			this.pointerContactTarget = null;
			this.destinationPosition = Vector3.zero;
			this.AliasRegistration(false);
			this.controllerGrabScript = null;
			Object.Destroy(this.pointerOriginTransformFollowGameObject);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00003F60 File Offset: 0x00002160
		protected virtual void Update()
		{
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00084521 File Offset: 0x00082721
		protected virtual void FixedUpdate()
		{
			if (this.interactWithObjects && this.objectInteractor && this.objectInteractor.activeInHierarchy)
			{
				this.UpdateObjectInteractor();
			}
			if (this.pointerOriginTransformFollow.isActiveAndEnabled)
			{
				this.UpdatePointerOriginTransformFollow();
			}
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00084560 File Offset: 0x00082760
		protected virtual void AliasRegistration(bool state)
		{
			if (this.controller)
			{
				if (state)
				{
					this.controller.AliasPointerOn += this.EnablePointerBeam;
					this.controller.AliasPointerOff += this.DisablePointerBeam;
					this.controller.AliasPointerSet += this.SetPointerDestination;
					return;
				}
				this.controller.AliasPointerOn -= this.EnablePointerBeam;
				this.controller.AliasPointerOff -= this.DisablePointerBeam;
				this.controller.AliasPointerSet -= this.SetPointerDestination;
			}
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00084614 File Offset: 0x00082814
		protected virtual void OnValidate()
		{
			this.pointerOriginSmoothingSettings.maxAllowedPerFrameDistanceDifference = Mathf.Max(0.0001f, this.pointerOriginSmoothingSettings.maxAllowedPerFrameDistanceDifference);
			this.pointerOriginSmoothingSettings.maxAllowedPerFrameAngleDifference = Mathf.Max(0.0001f, this.pointerOriginSmoothingSettings.maxAllowedPerFrameAngleDifference);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00084661 File Offset: 0x00082861
		protected Transform GetOrigin(bool smoothed = true)
		{
			if (smoothed && this.isActive)
			{
				return this.pointerOriginTransformFollow.gameObjectToChange.transform;
			}
			if (!(this.pointerOriginTransform == null))
			{
				return this.pointerOriginTransform;
			}
			return base.transform;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0008469A File Offset: 0x0008289A
		protected virtual void UpdateObjectInteractor()
		{
			this.objectInteractor.transform.position = this.destinationPosition;
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x000846B4 File Offset: 0x000828B4
		protected virtual void UpdatePointerOriginTransformFollow()
		{
			this.pointerOriginTransformFollow.gameObjectToFollow = ((this.pointerOriginTransform == null) ? base.transform : this.pointerOriginTransform).gameObject;
			this.pointerOriginTransformFollow.smoothsPosition = this.pointerOriginSmoothingSettings.smoothsPosition;
			this.pointerOriginTransformFollow.maxAllowedPerFrameDistanceDifference = this.pointerOriginSmoothingSettings.maxAllowedPerFrameDistanceDifference;
			this.pointerOriginTransformFollow.smoothsRotation = this.pointerOriginSmoothingSettings.smoothsRotation;
			this.pointerOriginTransformFollow.maxAllowedPerFrameAngleDifference = this.pointerOriginSmoothingSettings.maxAllowedPerFrameAngleDifference;
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00003F60 File Offset: 0x00002160
		protected virtual void InitPointer()
		{
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00084745 File Offset: 0x00082945
		protected virtual void UpdateDependencies(Vector3 location)
		{
			if (this.playAreaCursor)
			{
				this.playAreaCursor.SetPlayAreaCursorTransform(location);
			}
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00084760 File Offset: 0x00082960
		protected virtual void EnablePointerBeam(object sender, ControllerInteractionEventArgs e)
		{
			this.TurnOnBeam(e.controllerReference);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0008476E File Offset: 0x0008296E
		protected virtual void DisablePointerBeam(object sender, ControllerInteractionEventArgs e)
		{
			this.TurnOffBeam(e.controllerReference);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0008477C File Offset: 0x0008297C
		protected virtual void SetPointerDestination(object sender, ControllerInteractionEventArgs e)
		{
			this.PointerSet();
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x00084784 File Offset: 0x00082984
		protected virtual void PointerIn()
		{
			if (!base.enabled || !this.pointerContactTarget)
			{
				return;
			}
			this.OnDestinationMarkerEnter(this.SetDestinationMarkerEvent(this.pointerContactDistance, this.pointerContactTarget, this.pointerContactRaycastHit, this.destinationPosition, this.controllerReference, false, null));
			this.StartUseAction(this.pointerContactTarget);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x000847E8 File Offset: 0x000829E8
		protected virtual void PointerOut()
		{
			if (!base.enabled || !this.pointerContactTarget)
			{
				return;
			}
			this.OnDestinationMarkerExit(this.SetDestinationMarkerEvent(this.pointerContactDistance, this.pointerContactTarget, this.pointerContactRaycastHit, this.destinationPosition, this.controllerReference, false, null));
			this.StopUseAction();
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x00084848 File Offset: 0x00082A48
		protected virtual void PointerSet()
		{
			if (!base.enabled || !this.destinationSetActive || !this.pointerContactTarget || !this.CanActivate() || this.InvalidConstantBeam())
			{
				return;
			}
			this.activateDelayTimer = Time.time + this.activateDelay;
			VRTK_InteractableObject component = this.pointerContactTarget.GetComponent<VRTK_InteractableObject>();
			if (this.PointerActivatesUseAction(component))
			{
				if (component.IsUsing(null))
				{
					component.StopUsing(this.controller.gameObject);
					this.interactableObject.usingState = 0;
				}
				else if (!component.holdButtonToUse)
				{
					component.StartUsing(this.controller.gameObject);
					this.interactableObject.usingState++;
				}
			}
			if ((!this.playAreaCursor || !this.playAreaCursor.HasCollided()) && !this.PointerActivatesUseAction(this.interactableObject))
			{
				this.OnDestinationMarkerSet(this.SetDestinationMarkerEvent(this.pointerContactDistance, this.pointerContactTarget, this.pointerContactRaycastHit, this.destinationPosition, this.controllerReference, false, null));
			}
			if (!this.isActive)
			{
				this.destinationSetActive = false;
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0008496C File Offset: 0x00082B6C
		protected virtual void TogglePointer(bool state)
		{
			this.ToggleObjectInteraction(state);
			if (this.playAreaCursor)
			{
				this.playAreaCursor.SetHeadsetPositionCompensation(this.headsetPositionCompensation);
				this.playAreaCursor.ToggleState(state);
			}
			if (!state && this.PointerActivatesUseAction(this.interactableObject) && this.interactableObject.holdButtonToUse && this.interactableObject.IsUsing(null))
			{
				this.interactableObject.StopUsing(this.controller.gameObject);
			}
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x000849EC File Offset: 0x00082BEC
		protected virtual void ToggleObjectInteraction(bool state)
		{
			if (this.interactWithObjects)
			{
				if (state && this.grabToPointerTip && this.controllerGrabScript)
				{
					this.savedAttachPoint = this.controllerGrabScript.controllerAttachPoint;
					this.controllerGrabScript.controllerAttachPoint = this.objectInteractorAttachPoint.GetComponent<Rigidbody>();
					this.attachedToInteractorAttachPoint = true;
				}
				if (!state && this.grabToPointerTip && this.controllerGrabScript)
				{
					if (this.attachedToInteractorAttachPoint)
					{
						this.controllerGrabScript.ForceRelease(true);
					}
					this.controllerGrabScript.controllerAttachPoint = this.savedAttachPoint;
					this.savedAttachPoint = null;
					this.attachedToInteractorAttachPoint = false;
					this.savedBeamLength = 0f;
				}
				if (this.objectInteractor)
				{
					this.objectInteractor.SetActive(state);
				}
			}
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00084ABC File Offset: 0x00082CBC
		protected virtual void ChangeMaterialColor(GameObject obj, Color color)
		{
			foreach (Renderer renderer in obj.GetComponentsInChildren<Renderer>())
			{
				if (renderer.material)
				{
					renderer.material.EnableKeyword("_EMISSION");
					if (renderer.material.HasProperty("_Color"))
					{
						renderer.material.color = color;
					}
					if (renderer.material.HasProperty("_EmissionColor"))
					{
						renderer.material.SetColor("_EmissionColor", VRTK_SharedMethods.ColorDarken(color, 50f));
					}
				}
			}
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x00084B4A File Offset: 0x00082D4A
		protected virtual void SetPointerMaterial(Color color)
		{
			if (this.playAreaCursor)
			{
				this.playAreaCursor.SetMaterialColor(color, true);
			}
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00084B68 File Offset: 0x00082D68
		protected void UpdatePointerMaterial(Color color)
		{
			if ((this.playAreaCursor && this.playAreaCursor.HasCollided()) || !this.ValidDestination(this.pointerContactTarget, this.destinationPosition))
			{
				color = this.pointerMissColor;
			}
			this.currentPointerColor = color;
			this.SetPointerMaterial(color);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00084BBC File Offset: 0x00082DBC
		protected virtual bool ValidDestination(Transform target, Vector3 destinationPosition)
		{
			bool flag = false;
			if (target)
			{
				NavMeshHit navMeshHit;
				flag = NavMesh.SamplePosition(destinationPosition, out navMeshHit, this.navMeshCheckDistance, -1);
			}
			if (this.navMeshCheckDistance == 0f)
			{
				flag = true;
			}
			return flag && target && !VRTK_PolicyList.Check(target.gameObject, this.targetListPolicy);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00084C14 File Offset: 0x00082E14
		protected virtual void CreateObjectInteractor()
		{
			this.objectInteractor = new GameObject(string.Format("[{0}]BasePointer_ObjectInteractor_Holder", base.gameObject.name));
			this.objectInteractor.transform.SetParent(this.controller.transform);
			this.objectInteractor.transform.localPosition = Vector3.zero;
			this.objectInteractor.layer = LayerMask.NameToLayer("Ignore Raycast");
			VRTK_PlayerObject.SetPlayerObject(this.objectInteractor, VRTK_PlayerObject.ObjectTypes.Pointer);
			GameObject gameObject = new GameObject(string.Format("[{0}]BasePointer_ObjectInteractor_Collider", base.gameObject.name));
			gameObject.transform.SetParent(this.objectInteractor.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
			gameObject.AddComponent<SphereCollider>().isTrigger = true;
			VRTK_PlayerObject.SetPlayerObject(gameObject, VRTK_PlayerObject.ObjectTypes.Pointer);
			if (this.grabToPointerTip)
			{
				this.objectInteractorAttachPoint = new GameObject(string.Format("[{0}]BasePointer_ObjectInteractor_AttachPoint", base.gameObject.name));
				this.objectInteractorAttachPoint.transform.SetParent(this.objectInteractor.transform);
				this.objectInteractorAttachPoint.transform.localPosition = Vector3.zero;
				this.objectInteractorAttachPoint.layer = LayerMask.NameToLayer("Ignore Raycast");
				Rigidbody rigidbody = this.objectInteractorAttachPoint.AddComponent<Rigidbody>();
				rigidbody.isKinematic = true;
				rigidbody.freezeRotation = true;
				rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
				VRTK_PlayerObject.SetPlayerObject(this.objectInteractorAttachPoint, VRTK_PlayerObject.ObjectTypes.Pointer);
			}
			float num = 0.025f;
			this.objectInteractor.transform.localScale = new Vector3(num, num, num);
			this.objectInteractor.SetActive(false);
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00084DBC File Offset: 0x00082FBC
		protected virtual void CreatePointerOriginTransformFollow()
		{
			this.pointerOriginTransformFollowGameObject = new GameObject(string.Format("[{0}]BasePointer_Origin_Smoothed", base.gameObject.name));
			this.pointerOriginTransformFollowGameObject.SetActive(false);
			this.pointerOriginTransformFollow = this.pointerOriginTransformFollowGameObject.AddComponent<VRTK_TransformFollow>();
			this.pointerOriginTransformFollow.followsScale = false;
			this.UpdatePointerOriginTransformFollow();
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00084E18 File Offset: 0x00083018
		protected virtual float OverrideBeamLength(float currentLength)
		{
			if (!this.controllerGrabScript || !this.controllerGrabScript.GetGrabbedObject())
			{
				this.savedBeamLength = 0f;
			}
			if (this.interactWithObjects && this.grabToPointerTip && this.attachedToInteractorAttachPoint && this.controllerGrabScript && this.controllerGrabScript.GetGrabbedObject())
			{
				this.savedBeamLength = ((this.savedBeamLength == 0f) ? currentLength : this.savedBeamLength);
				return this.savedBeamLength;
			}
			return currentLength;
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00084EAC File Offset: 0x000830AC
		private void SetupController()
		{
			if (this.controller == null)
			{
				this.controller = base.GetComponent<VRTK_ControllerEvents>();
				this.AttemptSetController();
			}
			if (this.controller == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_NOT_INJECTED, new object[]
				{
					"VRTK_BasePointer",
					"VRTK_ControllerEvents",
					"controller",
					"the same"
				}));
			}
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00084F18 File Offset: 0x00083118
		private void AttemptSetController()
		{
			if (this.controller)
			{
				this.AliasRegistration(true);
				this.controllerGrabScript = this.controller.GetComponent<VRTK_InteractGrab>();
				if (this.interactWithObjects)
				{
					this.CreateObjectInteractor();
				}
			}
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x00084F50 File Offset: 0x00083150
		private bool InvalidConstantBeam()
		{
			bool flag = this.controller.pointerToggleButton == this.controller.pointerSetButton;
			return !this.holdButtonToActivate && ((flag && this.beamEnabledState != 0) || (!flag && !this.isActive));
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00084F9B File Offset: 0x0008319B
		private bool PointerActivatesUseAction(VRTK_InteractableObject io)
		{
			return io && io.pointerActivatesUseAction && io.IsValidInteractableController(base.gameObject, io.allowedUseControllers);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00084FC4 File Offset: 0x000831C4
		private void StartUseAction(Transform target)
		{
			this.interactableObject = target.GetComponent<VRTK_InteractableObject>();
			bool flag = this.interactableObject && this.interactableObject.useOnlyIfGrabbed && !this.interactableObject.IsGrabbed(null);
			if (this.PointerActivatesUseAction(this.interactableObject) && this.interactableObject.holdButtonToUse && !flag && this.interactableObject.usingState == 0)
			{
				this.interactableObject.StartUsing(this.controller.gameObject);
				this.interactableObject.usingState++;
			}
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0008505E File Offset: 0x0008325E
		private void StopUseAction()
		{
			if (this.PointerActivatesUseAction(this.interactableObject) && this.interactableObject.holdButtonToUse)
			{
				this.interactableObject.StopUsing(this.controller.gameObject);
				this.interactableObject.usingState = 0;
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x000850A0 File Offset: 0x000832A0
		private void TurnOnBeam(VRTK_ControllerReference givenControllerReference)
		{
			this.beamEnabledState++;
			if (base.enabled && !this.isActive && this.CanActivate())
			{
				if (this.playAreaCursor)
				{
					this.playAreaCursor.SetPlayAreaCursorCollision(false, null);
				}
				this.controllerReference = givenControllerReference;
				this.TogglePointer(true);
				this.isActive = true;
				this.destinationSetActive = true;
				if (this.pointerOriginTransformFollowGameObject != null)
				{
					this.pointerOriginTransformFollowGameObject.SetActive(true);
					this.pointerOriginTransformFollow.Follow();
				}
			}
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0008512E File Offset: 0x0008332E
		private void TurnOffBeam(VRTK_ControllerReference givenControllerReference)
		{
			if (base.enabled && this.isActive && (this.holdButtonToActivate || (!this.holdButtonToActivate && this.beamEnabledState >= 2)))
			{
				this.controllerReference = givenControllerReference;
				this.DisableBeam();
			}
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00085166 File Offset: 0x00083366
		private void DisableBeam()
		{
			this.TogglePointer(false);
			this.isActive = false;
			this.beamEnabledState = 0;
			if (this.pointerOriginTransformFollowGameObject != null)
			{
				this.pointerOriginTransformFollowGameObject.SetActive(false);
			}
		}

		// Token: 0x0400144A RID: 5194
		[Header("Base Pointer Settings", order = 2)]
		[Tooltip("The controller that will be used to toggle the pointer. If the script is being applied onto a controller then this parameter can be left blank as it will be auto populated by the controller the script is on at runtime.")]
		public VRTK_ControllerEvents controller;

		// Token: 0x0400144B RID: 5195
		[Tooltip("A custom transform to use as the origin of the pointer. If no pointer origin transform is provided then the transform the script is attached to is used.")]
		public Transform pointerOriginTransform;

		// Token: 0x0400144C RID: 5196
		[Tooltip("Specifies the smoothing to be applied to the pointer origin when positioning the pointer tip.")]
		public VRTK_BasePointer.PointerOriginSmoothingSettings pointerOriginSmoothingSettings = new VRTK_BasePointer.PointerOriginSmoothingSettings();

		// Token: 0x0400144D RID: 5197
		[Tooltip("The material to use on the rendered version of the pointer. If no material is selected then the default `WorldPointer` material will be used.")]
		public Material pointerMaterial;

		// Token: 0x0400144E RID: 5198
		[Tooltip("The colour of the beam when it is colliding with a valid target. It can be set to a different colour for each controller.")]
		public Color pointerHitColor = new Color(0f, 0.5f, 0f, 1f);

		// Token: 0x0400144F RID: 5199
		[Tooltip("The colour of the beam when it is not hitting a valid target. It can be set to a different colour for each controller.")]
		public Color pointerMissColor = new Color(0.8f, 0f, 0f, 1f);

		// Token: 0x04001450 RID: 5200
		[Tooltip("If this is checked then the pointer beam will be activated on first press of the pointer alias button and will stay active until the pointer alias button is pressed again. The destination set event is emitted when the beam is deactivated on the second button press.")]
		public bool holdButtonToActivate = true;

		// Token: 0x04001451 RID: 5201
		[Tooltip("If this is checked then the pointer will be an extension of the controller and able to interact with Interactable Objects.")]
		public bool interactWithObjects;

		// Token: 0x04001452 RID: 5202
		[Tooltip("If `Interact With Objects` is checked and this is checked then when an object is grabbed with the pointer touching it, the object will attach to the pointer tip and not snap to the controller.")]
		public bool grabToPointerTip;

		// Token: 0x04001453 RID: 5203
		[Tooltip("The time in seconds to delay the pointer beam being able to be active again. Useful for preventing constant teleportation.")]
		public float activateDelay;

		// Token: 0x04001454 RID: 5204
		[Tooltip("Determines when the pointer beam should be displayed.")]
		public VRTK_BasePointer.pointerVisibilityStates pointerVisibility;

		// Token: 0x04001455 RID: 5205
		[Tooltip("The layers to ignore when raycasting.")]
		public LayerMask layersToIgnore = 4;

		// Token: 0x04001456 RID: 5206
		protected Vector3 destinationPosition;

		// Token: 0x04001457 RID: 5207
		protected float pointerContactDistance;

		// Token: 0x04001458 RID: 5208
		protected Transform pointerContactTarget;

		// Token: 0x04001459 RID: 5209
		protected RaycastHit pointerContactRaycastHit;

		// Token: 0x0400145A RID: 5210
		protected VRTK_ControllerReference controllerReference;

		// Token: 0x0400145B RID: 5211
		protected VRTK_PlayAreaCursor playAreaCursor;

		// Token: 0x0400145C RID: 5212
		protected Color currentPointerColor;

		// Token: 0x0400145D RID: 5213
		protected GameObject objectInteractor;

		// Token: 0x0400145E RID: 5214
		protected GameObject objectInteractorAttachPoint;

		// Token: 0x0400145F RID: 5215
		private bool isActive;

		// Token: 0x04001460 RID: 5216
		private bool destinationSetActive;

		// Token: 0x04001461 RID: 5217
		private float activateDelayTimer;

		// Token: 0x04001462 RID: 5218
		private int beamEnabledState;

		// Token: 0x04001463 RID: 5219
		private VRTK_InteractableObject interactableObject;

		// Token: 0x04001464 RID: 5220
		private Rigidbody savedAttachPoint;

		// Token: 0x04001465 RID: 5221
		private bool attachedToInteractorAttachPoint;

		// Token: 0x04001466 RID: 5222
		private float savedBeamLength;

		// Token: 0x04001467 RID: 5223
		private VRTK_InteractGrab controllerGrabScript;

		// Token: 0x04001468 RID: 5224
		private GameObject pointerOriginTransformFollowGameObject;

		// Token: 0x04001469 RID: 5225
		private VRTK_TransformFollow pointerOriginTransformFollow;

		// Token: 0x0400146A RID: 5226
		private Transform originalPointerOriginTransform;

		// Token: 0x020005F3 RID: 1523
		public enum pointerVisibilityStates
		{
			// Token: 0x04002824 RID: 10276
			On_When_Active,
			// Token: 0x04002825 RID: 10277
			Always_On,
			// Token: 0x04002826 RID: 10278
			Always_Off
		}

		// Token: 0x020005F4 RID: 1524
		[Serializable]
		public sealed class PointerOriginSmoothingSettings
		{
			// Token: 0x04002827 RID: 10279
			[Tooltip("Whether or not to smooth the position of the pointer origin when positioning the pointer tip.")]
			public bool smoothsPosition;

			// Token: 0x04002828 RID: 10280
			[Tooltip("The maximum allowed distance between the unsmoothed pointer origin and the smoothed pointer origin per frame to use for smoothing.")]
			public float maxAllowedPerFrameDistanceDifference = 0.003f;

			// Token: 0x04002829 RID: 10281
			[Tooltip("Whether or not to smooth the rotation of the pointer origin when positioning the pointer tip.")]
			public bool smoothsRotation;

			// Token: 0x0400282A RID: 10282
			[Tooltip("The maximum allowed angle between the unsmoothed pointer origin and the smoothed pointer origin per frame to use for smoothing.")]
			public float maxAllowedPerFrameAngleDifference = 1.5f;
		}
	}
}
