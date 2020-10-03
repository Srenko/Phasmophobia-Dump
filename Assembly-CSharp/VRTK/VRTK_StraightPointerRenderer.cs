using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002E0 RID: 736
	[AddComponentMenu("VRTK/Scripts/Pointers/Pointer Renderers/VRTK_StraightPointerRenderer")]
	public class VRTK_StraightPointerRenderer : VRTK_BasePointerRenderer
	{
		// Token: 0x060018BA RID: 6330 RVA: 0x00083C58 File Offset: 0x00081E58
		public override void UpdateRenderer()
		{
			if ((this.controllingPointer && this.controllingPointer.IsPointerActive()) || this.IsVisible())
			{
				float pointerAppearance = this.CastRayForward();
				this.SetPointerAppearance(pointerAppearance);
				this.MakeRenderersVisible();
			}
			base.UpdateRenderer();
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00083CA1 File Offset: 0x00081EA1
		public override GameObject[] GetPointerObjects()
		{
			return new GameObject[]
			{
				this.actualContainer,
				this.actualCursor,
				this.actualTracer
			};
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00083CC4 File Offset: 0x00081EC4
		protected override void ToggleRenderer(bool pointerState, bool actualState)
		{
			this.ToggleElement(this.actualTracer, pointerState, actualState, this.tracerVisibility, ref this.tracerVisible);
			this.ToggleElement(this.actualCursor, pointerState, actualState, this.cursorVisibility, ref this.cursorVisible);
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00083CFC File Offset: 0x00081EFC
		protected override void CreatePointerObjects()
		{
			this.actualContainer = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				base.gameObject.name,
				"StraightPointerRenderer_Container"
			}));
			this.actualContainer.transform.localPosition = Vector3.zero;
			VRTK_PlayerObject.SetPlayerObject(this.actualContainer, VRTK_PlayerObject.ObjectTypes.Pointer);
			this.CreateTracer();
			this.CreateCursor();
			this.Toggle(false, false);
			if (this.controllingPointer)
			{
				this.controllingPointer.ResetActivationTimer(true);
				this.controllingPointer.ResetSelectionTimer(true);
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00083D90 File Offset: 0x00081F90
		protected override void DestroyPointerObjects()
		{
			if (this.actualContainer != null)
			{
				Object.Destroy(this.actualContainer);
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00083DAB File Offset: 0x00081FAB
		protected override void ChangeMaterial(Color givenColor)
		{
			base.ChangeMaterial(givenColor);
			this.ChangeMaterialColor(this.actualTracer, givenColor);
			this.ChangeMaterialColor(this.actualCursor, givenColor);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00083DD0 File Offset: 0x00081FD0
		protected override void UpdateObjectInteractor()
		{
			base.UpdateObjectInteractor();
			if (this.objectInteractor && this.actualCursor && Vector3.Distance(this.objectInteractor.transform.position, this.actualCursor.transform.position) > 0f)
			{
				this.objectInteractor.transform.position = this.actualCursor.transform.position;
			}
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00083E4C File Offset: 0x0008204C
		protected virtual void CreateTracer()
		{
			if (this.customTracer)
			{
				this.actualTracer = Object.Instantiate<GameObject>(this.customTracer);
			}
			else
			{
				this.actualTracer = GameObject.CreatePrimitive(PrimitiveType.Cube);
				this.actualTracer.GetComponent<BoxCollider>().isTrigger = true;
				this.actualTracer.AddComponent<Rigidbody>().isKinematic = true;
				this.actualTracer.layer = LayerMask.NameToLayer("Ignore Raycast");
				this.SetupMaterialRenderer(this.actualTracer);
			}
			this.actualTracer.transform.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				base.gameObject.name,
				"StraightPointerRenderer_Tracer"
			});
			this.actualTracer.transform.SetParent(this.actualContainer.transform);
			VRTK_PlayerObject.SetPlayerObject(this.actualTracer, VRTK_PlayerObject.ObjectTypes.Pointer);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00083F24 File Offset: 0x00082124
		protected virtual void CreateCursor()
		{
			if (this.customCursor)
			{
				this.actualCursor = Object.Instantiate<GameObject>(this.customCursor);
			}
			else
			{
				this.actualCursor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				this.actualCursor.transform.localScale = Vector3.one * (this.scaleFactor * this.cursorScaleMultiplier);
				this.actualCursor.GetComponent<Collider>().isTrigger = true;
				this.actualCursor.AddComponent<Rigidbody>().isKinematic = true;
				this.actualCursor.layer = LayerMask.NameToLayer("Ignore Raycast");
				this.SetupMaterialRenderer(this.actualCursor);
			}
			this.cursorOriginalScale = this.actualCursor.transform.localScale;
			this.actualCursor.transform.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				base.gameObject.name,
				"StraightPointerRenderer_Cursor"
			});
			this.actualCursor.transform.SetParent(this.actualContainer.transform);
			VRTK_PlayerObject.SetPlayerObject(this.actualCursor, VRTK_PlayerObject.ObjectTypes.Pointer);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00084038 File Offset: 0x00082238
		protected virtual void CheckRayMiss(bool rayHit, RaycastHit pointerCollidedWith)
		{
			if (!rayHit || (this.destinationHit.collider && this.destinationHit.collider != pointerCollidedWith.collider))
			{
				if (this.destinationHit.collider != null)
				{
					this.PointerExit(this.destinationHit);
				}
				this.destinationHit = default(RaycastHit);
				this.ChangeColor(this.invalidCollisionColor);
			}
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x000840AA File Offset: 0x000822AA
		protected virtual void CheckRayHit(bool rayHit, RaycastHit pointerCollidedWith)
		{
			if (rayHit)
			{
				this.PointerEnter(pointerCollidedWith);
				this.destinationHit = pointerCollidedWith;
				this.ChangeColor(this.validCollisionColor);
			}
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000840CC File Offset: 0x000822CC
		protected virtual float CastRayForward()
		{
			Transform origin = base.GetOrigin(true);
			Ray ray = new Ray(origin.position, origin.forward);
			RaycastHit pointerCollidedWith;
			bool flag = VRTK_CustomRaycast.Raycast(this.customRaycast, ray, out pointerCollidedWith, this.layersToIgnore, this.maximumLength, QueryTriggerInteraction.UseGlobal);
			this.CheckRayMiss(flag, pointerCollidedWith);
			this.CheckRayHit(flag, pointerCollidedWith);
			float distance = this.maximumLength;
			if (flag && pointerCollidedWith.distance < this.maximumLength)
			{
				distance = pointerCollidedWith.distance;
			}
			return this.OverrideBeamLength(distance);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0008414C File Offset: 0x0008234C
		protected virtual void SetPointerAppearance(float tracerLength)
		{
			if (this.actualContainer)
			{
				float d = tracerLength / 2.0001f;
				this.actualTracer.transform.localScale = new Vector3(this.scaleFactor, this.scaleFactor, tracerLength);
				this.actualTracer.transform.localPosition = Vector3.forward * d;
				this.actualCursor.transform.localScale = Vector3.one * (this.scaleFactor * this.cursorScaleMultiplier);
				this.actualCursor.transform.localPosition = new Vector3(0f, 0f, tracerLength);
				Transform origin = base.GetOrigin(true);
				this.actualContainer.transform.position = origin.position;
				this.actualContainer.transform.rotation = origin.rotation;
				float d2 = 1.05f;
				this.ScaleObjectInteractor(this.actualCursor.transform.lossyScale * d2);
				if (this.destinationHit.transform)
				{
					if (this.cursorMatchTargetRotation)
					{
						this.actualCursor.transform.forward = -this.destinationHit.normal;
					}
					if (this.cursorDistanceRescale)
					{
						float d3 = Vector3.Distance(this.destinationHit.point, origin.position);
						this.actualCursor.transform.localScale = Vector3.Min(this.cursorOriginalScale * d3, this.maximumCursorScale);
					}
				}
				else
				{
					if (this.cursorMatchTargetRotation)
					{
						this.actualCursor.transform.forward = origin.forward;
					}
					if (this.cursorDistanceRescale)
					{
						this.actualCursor.transform.localScale = Vector3.Min(this.cursorOriginalScale * tracerLength, this.maximumCursorScale);
					}
				}
				this.ToggleRenderer(this.controllingPointer.IsPointerActive(), false);
				this.UpdateDependencies(this.actualCursor.transform.position);
			}
		}

		// Token: 0x0400143E RID: 5182
		[Header("Straight Pointer Appearance Settings")]
		[Tooltip("The maximum length the pointer tracer can reach.")]
		public float maximumLength = 100f;

		// Token: 0x0400143F RID: 5183
		[Tooltip("The scale factor to scale the pointer tracer object by.")]
		public float scaleFactor = 0.002f;

		// Token: 0x04001440 RID: 5184
		[Tooltip("The scale multiplier to scale the pointer cursor object by in relation to the `Scale Factor`.")]
		public float cursorScaleMultiplier = 25f;

		// Token: 0x04001441 RID: 5185
		[Tooltip("The cursor will be rotated to match the angle of the target surface if this is true, if it is false then the pointer cursor will always be horizontal.")]
		public bool cursorMatchTargetRotation;

		// Token: 0x04001442 RID: 5186
		[Tooltip("Rescale the cursor proportionally to the distance from the tracer origin.")]
		public bool cursorDistanceRescale;

		// Token: 0x04001443 RID: 5187
		[Tooltip("The maximum scale the cursor is allowed to reach. This is only used when rescaling the cursor proportionally to the distance from the tracer origin.")]
		public Vector3 maximumCursorScale = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x04001444 RID: 5188
		[Header("Straight Pointer Custom Appearance Settings")]
		[Tooltip("A custom game object to use as the appearance for the pointer tracer. If this is empty then a Box primitive will be created and used.")]
		public GameObject customTracer;

		// Token: 0x04001445 RID: 5189
		[Tooltip("A custom game object to use as the appearance for the pointer cursor. If this is empty then a Sphere primitive will be created and used.")]
		public GameObject customCursor;

		// Token: 0x04001446 RID: 5190
		protected GameObject actualContainer;

		// Token: 0x04001447 RID: 5191
		protected GameObject actualTracer;

		// Token: 0x04001448 RID: 5192
		protected GameObject actualCursor;

		// Token: 0x04001449 RID: 5193
		protected Vector3 cursorOriginalScale = Vector3.one;
	}
}
