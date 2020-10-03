using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace VRTK
{
	// Token: 0x020002DF RID: 735
	[AddComponentMenu("VRTK/Scripts/Pointers/Pointer Renderers/VRTK_BezierPointerRenderer")]
	public class VRTK_BezierPointerRenderer : VRTK_BasePointerRenderer
	{
		// Token: 0x060018A7 RID: 6311 RVA: 0x00083124 File Offset: 0x00081324
		public override void UpdateRenderer()
		{
			if ((this.controllingPointer && this.controllingPointer.IsPointerActive()) || this.IsVisible())
			{
				Vector3 jointPosition = this.ProjectForwardBeam();
				Vector3 downPosition = this.ProjectDownBeam(jointPosition);
				this.AdjustForEarlyCollisions(jointPosition, downPosition);
				this.MakeRenderersVisible();
			}
			base.UpdateRenderer();
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00083176 File Offset: 0x00081376
		public override GameObject[] GetPointerObjects()
		{
			return new GameObject[]
			{
				this.actualContainer,
				this.actualCursor
			};
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00083190 File Offset: 0x00081390
		protected override void ToggleRenderer(bool pointerState, bool actualState)
		{
			this.TogglePointerCursor(pointerState, actualState);
			this.TogglePointerTracer(pointerState, actualState);
			if (this.actualTracer != null && actualState && this.tracerVisibility != VRTK_BasePointerRenderer.VisibilityStates.AlwaysOn)
			{
				this.ToggleRendererVisibility(this.actualTracer.gameObject, false);
				this.AddVisibleRenderer(this.actualTracer.gameObject);
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x000831EC File Offset: 0x000813EC
		protected override void CreatePointerObjects()
		{
			this.actualContainer = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				base.gameObject.name,
				"BezierPointerRenderer_Container"
			}));
			VRTK_PlayerObject.SetPlayerObject(this.actualContainer, VRTK_PlayerObject.ObjectTypes.Pointer);
			this.actualContainer.SetActive(false);
			this.CreateTracer();
			this.CreateCursor();
			this.Toggle(false, false);
			if (this.controllingPointer)
			{
				this.controllingPointer.ResetActivationTimer(true);
				this.controllingPointer.ResetSelectionTimer(true);
			}
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00083278 File Offset: 0x00081478
		protected override void DestroyPointerObjects()
		{
			if (this.actualCursor != null)
			{
				Object.Destroy(this.actualCursor);
			}
			if (this.actualTracer != null)
			{
				Object.Destroy(this.actualTracer);
			}
			if (this.actualContainer != null)
			{
				Object.Destroy(this.actualContainer);
			}
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000832D0 File Offset: 0x000814D0
		protected override void UpdateObjectInteractor()
		{
			base.UpdateObjectInteractor();
			if (this.objectInteractor && this.actualCursor && Vector3.Distance(this.objectInteractor.transform.position, this.actualCursor.transform.position) > 0f)
			{
				this.objectInteractor.transform.position = this.actualCursor.transform.position;
			}
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00083349 File Offset: 0x00081549
		protected override void ChangeMaterial(Color givenColor)
		{
			base.ChangeMaterial(givenColor);
			this.ChangeMaterialColor(this.actualCursor, givenColor);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00083360 File Offset: 0x00081560
		protected virtual void CreateTracer()
		{
			this.actualTracer = this.actualContainer.gameObject.AddComponent<VRTK_CurveGenerator>();
			this.actualTracer.transform.SetParent(null);
			this.actualTracer.Create(this.tracerDensity, this.cursorRadius, this.customTracer, this.rescaleTracer);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x000833B8 File Offset: 0x000815B8
		protected virtual GameObject CreateCursorObject()
		{
			float y = 0.02f;
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
			gameObject.transform.localScale = new Vector3(this.cursorRadius, y, this.cursorRadius);
			component.shadowCastingMode = ShadowCastingMode.Off;
			component.receiveShadows = false;
			component.material = this.defaultMaterial;
			Object.Destroy(gameObject.GetComponent<CapsuleCollider>());
			return gameObject;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0008341C File Offset: 0x0008161C
		protected virtual void CreateCursorLocations()
		{
			if (this.validLocationObject != null)
			{
				this.actualValidLocationObject = Object.Instantiate<GameObject>(this.validLocationObject);
				this.actualValidLocationObject.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
				{
					base.gameObject.name,
					"BezierPointerRenderer_ValidLocation"
				});
				this.actualValidLocationObject.transform.SetParent(this.actualCursor.transform);
				this.actualValidLocationObject.layer = LayerMask.NameToLayer("Ignore Raycast");
				this.actualValidLocationObject.SetActive(false);
			}
			if (this.invalidLocationObject != null)
			{
				this.actualInvalidLocationObject = Object.Instantiate<GameObject>(this.invalidLocationObject);
				this.actualInvalidLocationObject.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
				{
					base.gameObject.name,
					"BezierPointerRenderer_InvalidLocation"
				});
				this.actualInvalidLocationObject.transform.SetParent(this.actualCursor.transform);
				this.actualInvalidLocationObject.layer = LayerMask.NameToLayer("Ignore Raycast");
				this.actualInvalidLocationObject.SetActive(false);
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0008353C File Offset: 0x0008173C
		protected virtual void CreateCursor()
		{
			this.actualCursor = (this.customCursor ? Object.Instantiate<GameObject>(this.customCursor) : this.CreateCursorObject());
			this.CreateCursorLocations();
			this.actualCursor.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				base.gameObject.name,
				"BezierPointerRenderer_Cursor"
			});
			VRTK_PlayerObject.SetPlayerObject(this.actualCursor, VRTK_PlayerObject.ObjectTypes.Pointer);
			this.actualCursor.layer = LayerMask.NameToLayer("Ignore Raycast");
			this.actualCursor.SetActive(false);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x000835D0 File Offset: 0x000817D0
		protected virtual Vector3 ProjectForwardBeam()
		{
			Transform origin = base.GetOrigin(true);
			float num = Vector3.Dot(Vector3.up, origin.forward.normalized);
			float num2 = this.maximumLength.x;
			Vector3 forward = origin.forward;
			if (num * 100f > this.heightLimitAngle)
			{
				forward = new Vector3(forward.x, this.fixedForwardBeamForward.y, forward.z);
				float num3 = 1f - (num - this.heightLimitAngle / 100f);
				num2 = this.maximumLength.x * num3 * num3;
			}
			else
			{
				this.fixedForwardBeamForward = origin.forward;
			}
			float num4 = num2;
			Ray ray = new Ray(origin.position, forward);
			RaycastHit raycastHit;
			bool flag = VRTK_CustomRaycast.Raycast(this.customRaycast, ray, out raycastHit, this.layersToIgnore, num2, QueryTriggerInteraction.UseGlobal);
			float num5 = 0f;
			if (!flag || (this.destinationHit.collider && this.destinationHit.collider != raycastHit.collider))
			{
				num5 = 0f;
			}
			if (flag)
			{
				num5 = raycastHit.distance;
			}
			if (flag && num5 < num2)
			{
				num4 = num5;
			}
			return ray.GetPoint(num4 - 0.0001f) + Vector3.up * 0.0001f;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00083714 File Offset: 0x00081914
		protected virtual Vector3 ProjectDownBeam(Vector3 jointPosition)
		{
			Vector3 result = Vector3.zero;
			Ray ray = new Ray(jointPosition, Vector3.down);
			RaycastHit raycastHit;
			bool flag = VRTK_CustomRaycast.Raycast(this.customRaycast, ray, out raycastHit, this.layersToIgnore, this.maximumLength.y, QueryTriggerInteraction.UseGlobal);
			if (!flag || (this.destinationHit.collider && this.destinationHit.collider != raycastHit.collider))
			{
				if (this.destinationHit.collider != null)
				{
					this.PointerExit(this.destinationHit);
				}
				this.destinationHit = default(RaycastHit);
				result = ray.GetPoint(0f);
			}
			if (flag)
			{
				result = ray.GetPoint(raycastHit.distance);
				this.PointerEnter(raycastHit);
				this.destinationHit = raycastHit;
			}
			return result;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x000837DC File Offset: 0x000819DC
		protected virtual void AdjustForEarlyCollisions(Vector3 jointPosition, Vector3 downPosition)
		{
			Vector3 vector = downPosition;
			Vector3 jointPosition2 = jointPosition;
			if (this.collisionCheckFrequency > 0 && this.actualTracer != null)
			{
				this.collisionCheckFrequency = Mathf.Clamp(this.collisionCheckFrequency, 0, this.tracerDensity);
				Vector3[] controlPoints = new Vector3[]
				{
					base.GetOrigin(true).position,
					jointPosition + new Vector3(0f, this.curveOffset, 0f),
					downPosition,
					downPosition
				};
				Vector3[] points = this.actualTracer.GetPoints(controlPoints);
				int num = this.tracerDensity / this.collisionCheckFrequency;
				for (int i = 0; i < this.tracerDensity - num; i += num)
				{
					Vector3 vector2 = points[i];
					Vector3 vector3 = (i + num < points.Length) ? points[i + num] : points[points.Length - 1];
					Vector3 normalized = (vector3 - vector2).normalized;
					float length = Vector3.Distance(vector2, vector3);
					Ray ray = new Ray(vector2, normalized);
					RaycastHit raycastHit;
					if (VRTK_CustomRaycast.Raycast(this.customRaycast, ray, out raycastHit, this.layersToIgnore, length, QueryTriggerInteraction.UseGlobal))
					{
						Vector3 point = ray.GetPoint(raycastHit.distance);
						Ray ray2 = new Ray(point + Vector3.up * 0.01f, Vector3.down);
						RaycastHit destinationHit;
						if (VRTK_CustomRaycast.Raycast(this.customRaycast, ray2, out destinationHit, this.layersToIgnore, float.PositiveInfinity, QueryTriggerInteraction.UseGlobal))
						{
							this.destinationHit = destinationHit;
							vector = ray2.GetPoint(destinationHit.distance);
							jointPosition2 = ((vector.y < jointPosition.y) ? new Vector3(vector.x, jointPosition.y, vector.z) : jointPosition);
							break;
						}
					}
				}
			}
			this.DisplayCurvedBeam(jointPosition2, vector);
			this.SetPointerCursor();
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x000839C0 File Offset: 0x00081BC0
		protected virtual void DisplayCurvedBeam(Vector3 jointPosition, Vector3 downPosition)
		{
			if (this.actualTracer != null)
			{
				Vector3[] controlPoints = new Vector3[]
				{
					base.GetOrigin(false).position,
					jointPosition + new Vector3(0f, this.curveOffset, 0f),
					downPosition,
					downPosition
				};
				Material material = this.customTracer ? null : this.defaultMaterial;
				this.actualTracer.SetPoints(controlPoints, material, this.currentColor);
				if (this.tracerVisibility == VRTK_BasePointerRenderer.VisibilityStates.AlwaysOff)
				{
					this.TogglePointerTracer(false, false);
					return;
				}
				if (this.controllingPointer)
				{
					this.TogglePointerTracer(this.controllingPointer.IsPointerActive(), this.controllingPointer.IsPointerActive());
				}
			}
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00083A8F File Offset: 0x00081C8F
		protected virtual void TogglePointerCursor(bool pointerState, bool actualState)
		{
			this.ToggleElement(this.actualCursor, pointerState, actualState, this.cursorVisibility, ref this.cursorVisible);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00083AAB File Offset: 0x00081CAB
		protected virtual void TogglePointerTracer(bool pointerState, bool actualState)
		{
			this.tracerVisible = (this.tracerVisibility == VRTK_BasePointerRenderer.VisibilityStates.AlwaysOn || pointerState);
			if (this.actualTracer != null)
			{
				this.actualTracer.TogglePoints(this.tracerVisible);
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00083AE0 File Offset: 0x00081CE0
		protected virtual void SetPointerCursor()
		{
			if (this.controllingPointer && this.destinationHit.transform)
			{
				this.TogglePointerCursor(this.controllingPointer.IsPointerActive(), this.controllingPointer.IsPointerActive());
				this.actualCursor.transform.position = this.destinationHit.point;
				if (this.cursorMatchTargetRotation)
				{
					this.actualCursor.transform.rotation = Quaternion.FromToRotation(Vector3.up, this.destinationHit.normal);
				}
				base.UpdateDependencies(this.actualCursor.transform.position);
				this.ChangeColor(this.validCollisionColor);
				if (this.actualValidLocationObject)
				{
					this.actualValidLocationObject.SetActive(this.ValidDestination() && this.IsValidCollision());
				}
				if (this.actualInvalidLocationObject)
				{
					this.actualInvalidLocationObject.SetActive(!this.ValidDestination() || !this.IsValidCollision());
					return;
				}
			}
			else
			{
				this.TogglePointerCursor(false, false);
				this.ChangeColor(this.invalidCollisionColor);
			}
		}

		// Token: 0x0400142C RID: 5164
		[Header("Bezier Pointer Appearance Settings")]
		[Tooltip("The maximum length of the projected beam. The x value is the length of the forward beam, the y value is the length of the downward beam.")]
		public Vector2 maximumLength = new Vector2(10f, float.PositiveInfinity);

		// Token: 0x0400142D RID: 5165
		[Tooltip("The number of items to render in the bezier curve tracer beam. A high number here will most likely have a negative impact of game performance due to large number of rendered objects.")]
		public int tracerDensity = 10;

		// Token: 0x0400142E RID: 5166
		[Tooltip("The size of the ground cursor. This number also affects the size of the objects in the bezier curve tracer beam. The larger the radius, the larger the objects will be.")]
		public float cursorRadius = 0.5f;

		// Token: 0x0400142F RID: 5167
		[Header("Bezier Pointer Render Settings")]
		[Tooltip("The maximum angle in degrees of the origin before the beam curve height is restricted. A lower angle setting will prevent the beam being projected high into the sky and curving back down.")]
		[Range(1f, 100f)]
		public float heightLimitAngle = 100f;

		// Token: 0x04001430 RID: 5168
		[Tooltip("The amount of height offset to apply to the projected beam to generate a smoother curve even when the beam is pointing straight.")]
		public float curveOffset = 1f;

		// Token: 0x04001431 RID: 5169
		[Tooltip("Rescale each tracer element according to the length of the Bezier curve.")]
		public bool rescaleTracer;

		// Token: 0x04001432 RID: 5170
		[Tooltip("The cursor will be rotated to match the angle of the target surface if this is true, if it is false then the pointer cursor will always be horizontal.")]
		public bool cursorMatchTargetRotation;

		// Token: 0x04001433 RID: 5171
		[Tooltip("The number of points along the bezier curve to check for an early beam collision. Useful if the bezier curve is appearing to clip through teleport locations. 0 won't make any checks and it will be capped at `Pointer Density`. The higher the number, the more CPU intensive the checks become.")]
		public int collisionCheckFrequency;

		// Token: 0x04001434 RID: 5172
		[Header("Bezier Pointer Custom Appearance Settings")]
		[Tooltip("A custom game object to use as the appearance for the pointer tracer. If this is empty then a collection of Sphere primitives will be created and used.")]
		public GameObject customTracer;

		// Token: 0x04001435 RID: 5173
		[Tooltip("A custom game object to use as the appearance for the pointer cursor. If this is empty then a Cylinder primitive will be created and used.")]
		public GameObject customCursor;

		// Token: 0x04001436 RID: 5174
		[Tooltip("A custom game object can be applied here to appear only if the location is valid.")]
		public GameObject validLocationObject;

		// Token: 0x04001437 RID: 5175
		[Tooltip("A custom game object can be applied here to appear only if the location is invalid.")]
		public GameObject invalidLocationObject;

		// Token: 0x04001438 RID: 5176
		protected VRTK_CurveGenerator actualTracer;

		// Token: 0x04001439 RID: 5177
		protected GameObject actualContainer;

		// Token: 0x0400143A RID: 5178
		protected GameObject actualCursor;

		// Token: 0x0400143B RID: 5179
		protected GameObject actualValidLocationObject;

		// Token: 0x0400143C RID: 5180
		protected GameObject actualInvalidLocationObject;

		// Token: 0x0400143D RID: 5181
		protected Vector3 fixedForwardBeamForward;
	}
}
