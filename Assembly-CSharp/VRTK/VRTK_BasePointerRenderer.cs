using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace VRTK
{
	// Token: 0x020002DE RID: 734
	public abstract class VRTK_BasePointerRenderer : MonoBehaviour
	{
		// Token: 0x0600187B RID: 6267
		public abstract GameObject[] GetPointerObjects();

		// Token: 0x0600187C RID: 6268 RVA: 0x000824A4 File Offset: 0x000806A4
		public virtual void InitalizePointer(VRTK_Pointer givenPointer, VRTK_PolicyList givenInvalidListPolicy, float givenNavMeshCheckDistance, bool givenHeadsetPositionCompensation)
		{
			this.controllingPointer = givenPointer;
			this.invalidListPolicy = givenInvalidListPolicy;
			this.navMeshCheckDistance = givenNavMeshCheckDistance;
			this.headsetPositionCompensation = givenHeadsetPositionCompensation;
			if (this.controllingPointer != null && this.controllingPointer.interactWithObjects && this.controllingPointer.controller != null && this.objectInteractor == null)
			{
				this.controllerGrabScript = this.controllingPointer.controller.GetComponent<VRTK_InteractGrab>();
				this.CreateObjectInteractor();
			}
			this.SetupDirectionIndicator();
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0008252C File Offset: 0x0008072C
		public virtual void ResetPointerObjects()
		{
			this.DestroyPointerObjects();
			this.CreatePointerObjects();
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0008253C File Offset: 0x0008073C
		public virtual void Toggle(bool pointerState, bool actualState)
		{
			if (pointerState)
			{
				this.destinationHit = default(RaycastHit);
			}
			else if (this.controllingPointer != null)
			{
				this.controllingPointer.ResetActivationTimer(false);
				this.PointerExit(this.destinationHit);
			}
			this.ToggleInteraction(pointerState);
			this.ToggleRenderer(pointerState, actualState);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0008258F File Offset: 0x0008078F
		public virtual void ToggleInteraction(bool state)
		{
			this.ToggleObjectInteraction(state);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00082598 File Offset: 0x00080798
		public virtual void UpdateRenderer()
		{
			if (this.playareaCursor != null)
			{
				this.playareaCursor.SetHeadsetPositionCompensation(this.headsetPositionCompensation);
				this.playareaCursor.ToggleState(this.IsCursorVisible());
			}
			if (this.directionIndicator != null)
			{
				this.UpdateDirectionIndicator();
			}
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x000825E9 File Offset: 0x000807E9
		public virtual RaycastHit GetDestinationHit()
		{
			return this.destinationHit;
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x000825F1 File Offset: 0x000807F1
		public virtual bool ValidPlayArea()
		{
			return this.playareaCursor == null || !this.playareaCursor.IsActive() || !this.playareaCursor.HasCollided();
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0008261E File Offset: 0x0008081E
		public virtual bool IsVisible()
		{
			return this.IsTracerVisible() || this.IsCursorVisible();
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00082630 File Offset: 0x00080830
		public virtual bool IsTracerVisible()
		{
			return this.tracerVisibility == VRTK_BasePointerRenderer.VisibilityStates.AlwaysOn || this.tracerVisible;
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00082643 File Offset: 0x00080843
		public virtual bool IsCursorVisible()
		{
			return this.cursorVisibility == VRTK_BasePointerRenderer.VisibilityStates.AlwaysOn || this.cursorVisible;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00082656 File Offset: 0x00080856
		public virtual bool IsValidCollision()
		{
			return this.currentColor != this.invalidCollisionColor;
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00082669 File Offset: 0x00080869
		public virtual GameObject GetObjectInteractor()
		{
			return this.objectInteractor;
		}

		// Token: 0x06001888 RID: 6280
		protected abstract void CreatePointerObjects();

		// Token: 0x06001889 RID: 6281
		protected abstract void DestroyPointerObjects();

		// Token: 0x0600188A RID: 6282
		protected abstract void ToggleRenderer(bool pointerState, bool actualState);

		// Token: 0x0600188B RID: 6283 RVA: 0x00082671 File Offset: 0x00080871
		protected virtual void OnEnable()
		{
			this.defaultMaterial = (Resources.Load("WorldPointer") as Material);
			this.makeRendererVisible = new List<GameObject>();
			this.CreatePointerOriginTransformFollow();
			this.CreatePointerObjects();
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0008269F File Offset: 0x0008089F
		protected virtual void OnDisable()
		{
			this.DestroyPointerObjects();
			if (this.objectInteractor != null)
			{
				Object.Destroy(this.objectInteractor);
			}
			this.controllerGrabScript = null;
			Object.Destroy(this.pointerOriginTransformFollowGameObject);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x000826D4 File Offset: 0x000808D4
		protected virtual void OnValidate()
		{
			this.pointerOriginSmoothingSettings.maxAllowedPerFrameDistanceDifference = Mathf.Max(0.0001f, this.pointerOriginSmoothingSettings.maxAllowedPerFrameDistanceDifference);
			this.pointerOriginSmoothingSettings.maxAllowedPerFrameAngleDifference = Mathf.Max(0.0001f, this.pointerOriginSmoothingSettings.maxAllowedPerFrameAngleDifference);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00082724 File Offset: 0x00080924
		protected virtual void FixedUpdate()
		{
			if (this.controllingPointer != null && this.controllingPointer.interactWithObjects && this.objectInteractor != null && this.objectInteractor.activeInHierarchy)
			{
				this.UpdateObjectInteractor();
			}
			this.UpdatePointerOriginTransformFollow();
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00082774 File Offset: 0x00080974
		protected virtual void ToggleObjectInteraction(bool state)
		{
			if (this.controllingPointer != null && this.controllingPointer.interactWithObjects)
			{
				if (state && this.controllingPointer.grabToPointerTip && this.controllerGrabScript != null && this.objectInteractorAttachPoint != null)
				{
					this.savedAttachPoint = this.controllerGrabScript.controllerAttachPoint;
					this.controllerGrabScript.controllerAttachPoint = this.objectInteractorAttachPoint.GetComponent<Rigidbody>();
					this.attachedToInteractorAttachPoint = true;
				}
				if (!state && this.controllingPointer.grabToPointerTip && this.controllerGrabScript != null)
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
				if (this.objectInteractor != null)
				{
					this.objectInteractor.SetActive(state);
				}
			}
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00082872 File Offset: 0x00080A72
		protected virtual void UpdateObjectInteractor()
		{
			this.objectInteractor.transform.position = this.destinationHit.point;
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00082890 File Offset: 0x00080A90
		protected virtual void UpdatePointerOriginTransformFollow()
		{
			this.pointerOriginTransformFollow.gameObject.SetActive(this.controllingPointer != null);
			if (this.controllingPointer != null)
			{
				this.pointerOriginTransformFollow.gameObjectToFollow = ((this.controllingPointer.customOrigin == null) ? base.transform : this.controllingPointer.customOrigin).gameObject;
				this.pointerOriginTransformFollow.enabled = (this.controllingPointer != null);
				this.pointerOriginTransformFollowGameObject.SetActive(this.controllingPointer != null);
				this.pointerOriginTransformFollow.smoothsPosition = this.pointerOriginSmoothingSettings.smoothsPosition;
				this.pointerOriginTransformFollow.maxAllowedPerFrameDistanceDifference = this.pointerOriginSmoothingSettings.maxAllowedPerFrameDistanceDifference;
				this.pointerOriginTransformFollow.smoothsRotation = this.pointerOriginSmoothingSettings.smoothsRotation;
				this.pointerOriginTransformFollow.maxAllowedPerFrameAngleDifference = this.pointerOriginSmoothingSettings.maxAllowedPerFrameAngleDifference;
			}
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00082986 File Offset: 0x00080B86
		protected Transform GetOrigin(bool smoothed = true)
		{
			if (smoothed)
			{
				return this.pointerOriginTransformFollow.gameObjectToChange.transform;
			}
			if (!(this.controllingPointer.customOrigin == null))
			{
				return this.controllingPointer.customOrigin;
			}
			return base.transform;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000829C1 File Offset: 0x00080BC1
		protected virtual void PointerEnter(RaycastHit givenHit)
		{
			this.controllingPointer.PointerEnter(givenHit);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000829CF File Offset: 0x00080BCF
		protected virtual void PointerExit(RaycastHit givenHit)
		{
			this.controllingPointer.PointerExit(givenHit);
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x000829E0 File Offset: 0x00080BE0
		protected virtual bool ValidDestination()
		{
			bool flag = false;
			if (this.destinationHit.transform != null)
			{
				NavMeshHit navMeshHit;
				flag = NavMesh.SamplePosition(this.destinationHit.point, out navMeshHit, this.navMeshCheckDistance, -1);
			}
			if (this.navMeshCheckDistance == 0f)
			{
				flag = true;
			}
			return flag && this.destinationHit.collider != null && !VRTK_PolicyList.Check(this.destinationHit.collider.gameObject, this.invalidListPolicy);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00082A64 File Offset: 0x00080C64
		protected virtual void ToggleElement(GameObject givenObject, bool pointerState, bool actualState, VRTK_BasePointerRenderer.VisibilityStates givenVisibility, ref bool currentVisible)
		{
			if (givenObject != null)
			{
				currentVisible = (givenVisibility == VRTK_BasePointerRenderer.VisibilityStates.AlwaysOn || pointerState);
				givenObject.SetActive(currentVisible);
				if (givenVisibility == VRTK_BasePointerRenderer.VisibilityStates.AlwaysOff)
				{
					currentVisible = false;
					this.ToggleRendererVisibility(givenObject, false);
					return;
				}
				if (actualState && givenVisibility != VRTK_BasePointerRenderer.VisibilityStates.AlwaysOn)
				{
					this.ToggleRendererVisibility(givenObject, false);
					this.AddVisibleRenderer(givenObject);
					return;
				}
				this.ToggleRendererVisibility(givenObject, true);
			}
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00082AC1 File Offset: 0x00080CC1
		protected virtual void AddVisibleRenderer(GameObject givenObject)
		{
			if (!this.makeRendererVisible.Contains(givenObject))
			{
				this.makeRendererVisible.Add(givenObject);
			}
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00082AE0 File Offset: 0x00080CE0
		protected virtual void MakeRenderersVisible()
		{
			for (int i = 0; i < this.makeRendererVisible.Count; i++)
			{
				this.ToggleRendererVisibility(this.makeRendererVisible[i], true);
				this.makeRendererVisible.Remove(this.makeRendererVisible[i]);
			}
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00082B30 File Offset: 0x00080D30
		protected virtual void ToggleRendererVisibility(GameObject givenObject, bool state)
		{
			if (givenObject != null)
			{
				Renderer[] componentsInChildren = givenObject.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = state;
				}
			}
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00082B64 File Offset: 0x00080D64
		protected virtual void SetupMaterialRenderer(GameObject givenObject)
		{
			if (givenObject != null)
			{
				MeshRenderer component = givenObject.GetComponent<MeshRenderer>();
				component.shadowCastingMode = ShadowCastingMode.Off;
				component.receiveShadows = false;
				component.material = this.defaultMaterial;
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00082B90 File Offset: 0x00080D90
		protected virtual void ChangeColor(Color givenColor)
		{
			this.previousColor = this.currentColor;
			if ((this.playareaCursor != null && this.playareaCursor.IsActive() && this.playareaCursor.HasCollided()) || !this.ValidDestination() || (this.controllingPointer != null && !this.controllingPointer.CanSelect()))
			{
				givenColor = this.invalidCollisionColor;
			}
			if (givenColor != Color.clear)
			{
				this.currentColor = givenColor;
				this.ChangeMaterial(givenColor);
			}
			if (this.previousColor != this.currentColor)
			{
				this.EmitStateEvent();
			}
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00082C30 File Offset: 0x00080E30
		protected virtual void EmitStateEvent()
		{
			if (this.controllingPointer != null)
			{
				if (this.IsValidCollision())
				{
					this.controllingPointer.OnPointerStateValid();
					return;
				}
				this.controllingPointer.OnPointerStateInvalid();
			}
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00082C60 File Offset: 0x00080E60
		protected virtual void ChangeMaterial(Color givenColor)
		{
			if (this.playareaCursor != null)
			{
				this.playareaCursor.SetMaterialColor(givenColor, this.IsValidCollision());
			}
			if (this.directionIndicator != null)
			{
				this.directionIndicator.SetMaterialColor(givenColor, this.IsValidCollision());
			}
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00082CB0 File Offset: 0x00080EB0
		protected virtual void ChangeMaterialColor(GameObject givenObject, Color givenColor)
		{
			if (givenObject != null)
			{
				foreach (Renderer renderer in givenObject.GetComponentsInChildren<Renderer>())
				{
					if (renderer.material != null)
					{
						renderer.material.EnableKeyword("_EMISSION");
						if (renderer.material.HasProperty("_Color"))
						{
							renderer.material.color = givenColor;
						}
						if (renderer.material.HasProperty("_EmissionColor"))
						{
							renderer.material.SetColor("_EmissionColor", VRTK_SharedMethods.ColorDarken(givenColor, 50f));
						}
					}
				}
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00082D4C File Offset: 0x00080F4C
		protected virtual void CreateObjectInteractor()
		{
			this.objectInteractor = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				base.gameObject.name,
				"BasePointerRenderer_ObjectInteractor_Container"
			}));
			this.objectInteractor.transform.SetParent(this.controllingPointer.controller.transform);
			this.objectInteractor.transform.localPosition = Vector3.zero;
			this.objectInteractor.layer = LayerMask.NameToLayer("Ignore Raycast");
			VRTK_PlayerObject.SetPlayerObject(this.objectInteractor, VRTK_PlayerObject.ObjectTypes.Pointer);
			GameObject gameObject = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				base.gameObject.name,
				"BasePointerRenderer_ObjectInteractor_Collider"
			}));
			gameObject.transform.SetParent(this.objectInteractor.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
			gameObject.AddComponent<SphereCollider>().isTrigger = true;
			VRTK_PlayerObject.SetPlayerObject(gameObject, VRTK_PlayerObject.ObjectTypes.Pointer);
			if (this.controllingPointer.grabToPointerTip)
			{
				this.objectInteractorAttachPoint = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
				{
					base.gameObject.name,
					"BasePointerRenderer_ObjectInteractor_AttachPoint"
				}));
				this.objectInteractorAttachPoint.transform.SetParent(this.objectInteractor.transform);
				this.objectInteractorAttachPoint.transform.localPosition = Vector3.zero;
				this.objectInteractorAttachPoint.layer = LayerMask.NameToLayer("Ignore Raycast");
				Rigidbody rigidbody = this.objectInteractorAttachPoint.AddComponent<Rigidbody>();
				rigidbody.isKinematic = true;
				rigidbody.freezeRotation = true;
				rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
				VRTK_PlayerObject.SetPlayerObject(this.objectInteractorAttachPoint, VRTK_PlayerObject.ObjectTypes.Pointer);
			}
			this.ScaleObjectInteractor(Vector3.one);
			this.objectInteractor.SetActive(false);
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00082F0F File Offset: 0x0008110F
		protected virtual void ScaleObjectInteractor(Vector3 scaleAmount)
		{
			if (this.objectInteractor != null)
			{
				base.transform.SetGlobalScale(scaleAmount);
			}
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00082F2C File Offset: 0x0008112C
		protected virtual void CreatePointerOriginTransformFollow()
		{
			this.pointerOriginTransformFollowGameObject = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				base.gameObject.name,
				"BasePointerRenderer_Origin_Smoothed"
			}));
			this.pointerOriginTransformFollow = this.pointerOriginTransformFollowGameObject.AddComponent<VRTK_TransformFollow>();
			this.pointerOriginTransformFollow.enabled = false;
			this.pointerOriginTransformFollow.followsScale = false;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00082F90 File Offset: 0x00081190
		protected virtual float OverrideBeamLength(float currentLength)
		{
			if (this.controllerGrabScript == null || !this.controllerGrabScript.GetGrabbedObject())
			{
				this.savedBeamLength = 0f;
			}
			if (this.controllingPointer != null && this.controllingPointer.interactWithObjects && this.controllingPointer.grabToPointerTip && this.attachedToInteractorAttachPoint && this.controllerGrabScript != null && this.controllerGrabScript.GetGrabbedObject())
			{
				this.savedBeamLength = ((this.savedBeamLength == 0f) ? currentLength : this.savedBeamLength);
				return this.savedBeamLength;
			}
			return currentLength;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0008303C File Offset: 0x0008123C
		protected virtual void UpdateDependencies(Vector3 location)
		{
			if (this.playareaCursor != null)
			{
				this.playareaCursor.SetPlayAreaCursorTransform(location);
			}
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00083058 File Offset: 0x00081258
		protected virtual void SetupDirectionIndicator()
		{
			if (this.directionIndicator != null && this.controllingPointer != null && this.controllingPointer.controller != null)
			{
				this.directionIndicator.Initialize(this.controllingPointer.controller);
			}
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x000830AC File Offset: 0x000812AC
		protected virtual void UpdateDirectionIndicator()
		{
			RaycastHit raycastHit = this.GetDestinationHit();
			this.directionIndicator.SetPosition(this.controllingPointer.IsPointerActive() && raycastHit.collider != null, raycastHit.point);
		}

		// Token: 0x0400140F RID: 5135
		[Header("Renderer Supplement Settings")]
		[Tooltip("An optional Play Area Cursor generator to add to the destination position of the pointer tip.")]
		public VRTK_PlayAreaCursor playareaCursor;

		// Token: 0x04001410 RID: 5136
		[Tooltip("A custom VRTK_PointerDirectionIndicator to use to determine the rotation given to the destination set event.")]
		public VRTK_PointerDirectionIndicator directionIndicator;

		// Token: 0x04001411 RID: 5137
		[Header("General Renderer Settings")]
		[Tooltip("A custom raycaster to use for the pointer's raycasts to ignore.")]
		public VRTK_CustomRaycast customRaycast;

		// Token: 0x04001412 RID: 5138
		[Tooltip("**OBSOLETE [Use customRaycast]** The layers for the pointer's raycasts to ignore.")]
		[Obsolete("`VRTK_BasePointerRenderer.layersToIgnore` is no longer used in the `VRTK_BasePointerRenderer` class, use the `customRaycast` parameter instead. This parameter will be removed in a future version of VRTK.")]
		public LayerMask layersToIgnore = 4;

		// Token: 0x04001413 RID: 5139
		[Tooltip("Specifies the smoothing to be applied to the pointer origin when positioning the pointer tip.")]
		public VRTK_BasePointerRenderer.PointerOriginSmoothingSettings pointerOriginSmoothingSettings = new VRTK_BasePointerRenderer.PointerOriginSmoothingSettings();

		// Token: 0x04001414 RID: 5140
		[Header("General Appearance Settings")]
		[Tooltip("The colour to change the pointer materials when the pointer collides with a valid object. Set to `Color.clear` to bypass changing material colour on valid collision.")]
		public Color validCollisionColor = Color.green;

		// Token: 0x04001415 RID: 5141
		[Tooltip("The colour to change the pointer materials when the pointer is not colliding with anything or with an invalid object. Set to `Color.clear` to bypass changing material colour on invalid collision.")]
		public Color invalidCollisionColor = Color.red;

		// Token: 0x04001416 RID: 5142
		[Tooltip("Determines when the main tracer of the pointer renderer will be visible.")]
		public VRTK_BasePointerRenderer.VisibilityStates tracerVisibility;

		// Token: 0x04001417 RID: 5143
		[Tooltip("Determines when the cursor/tip of the pointer renderer will be visible.")]
		public VRTK_BasePointerRenderer.VisibilityStates cursorVisibility;

		// Token: 0x04001418 RID: 5144
		protected const float BEAM_ADJUST_OFFSET = 0.0001f;

		// Token: 0x04001419 RID: 5145
		protected VRTK_Pointer controllingPointer;

		// Token: 0x0400141A RID: 5146
		protected RaycastHit destinationHit;

		// Token: 0x0400141B RID: 5147
		protected Material defaultMaterial;

		// Token: 0x0400141C RID: 5148
		protected Color previousColor;

		// Token: 0x0400141D RID: 5149
		protected Color currentColor;

		// Token: 0x0400141E RID: 5150
		protected VRTK_PolicyList invalidListPolicy;

		// Token: 0x0400141F RID: 5151
		protected float navMeshCheckDistance;

		// Token: 0x04001420 RID: 5152
		protected bool headsetPositionCompensation;

		// Token: 0x04001421 RID: 5153
		protected GameObject objectInteractor;

		// Token: 0x04001422 RID: 5154
		protected GameObject objectInteractorAttachPoint;

		// Token: 0x04001423 RID: 5155
		protected GameObject pointerOriginTransformFollowGameObject;

		// Token: 0x04001424 RID: 5156
		protected VRTK_TransformFollow pointerOriginTransformFollow;

		// Token: 0x04001425 RID: 5157
		protected VRTK_InteractGrab controllerGrabScript;

		// Token: 0x04001426 RID: 5158
		protected Rigidbody savedAttachPoint;

		// Token: 0x04001427 RID: 5159
		protected bool attachedToInteractorAttachPoint;

		// Token: 0x04001428 RID: 5160
		protected float savedBeamLength;

		// Token: 0x04001429 RID: 5161
		protected List<GameObject> makeRendererVisible;

		// Token: 0x0400142A RID: 5162
		protected bool tracerVisible;

		// Token: 0x0400142B RID: 5163
		protected bool cursorVisible;

		// Token: 0x020005F1 RID: 1521
		public enum VisibilityStates
		{
			// Token: 0x0400281C RID: 10268
			OnWhenActive,
			// Token: 0x0400281D RID: 10269
			AlwaysOn,
			// Token: 0x0400281E RID: 10270
			AlwaysOff
		}

		// Token: 0x020005F2 RID: 1522
		[Serializable]
		public sealed class PointerOriginSmoothingSettings
		{
			// Token: 0x0400281F RID: 10271
			[Tooltip("Whether or not to smooth the position of the pointer origin when positioning the pointer tip.")]
			public bool smoothsPosition;

			// Token: 0x04002820 RID: 10272
			[Tooltip("The maximum allowed distance between the unsmoothed pointer origin and the smoothed pointer origin per frame to use for smoothing.")]
			public float maxAllowedPerFrameDistanceDifference = 0.003f;

			// Token: 0x04002821 RID: 10273
			[Tooltip("Whether or not to smooth the rotation of the pointer origin when positioning the pointer tip.")]
			public bool smoothsRotation;

			// Token: 0x04002822 RID: 10274
			[Tooltip("The maximum allowed angle between the unsmoothed pointer origin and the smoothed pointer origin per frame to use for smoothing.")]
			public float maxAllowedPerFrameAngleDifference = 1.5f;
		}
	}
}
