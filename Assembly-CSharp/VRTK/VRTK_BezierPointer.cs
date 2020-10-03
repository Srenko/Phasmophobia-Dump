using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace VRTK
{
	// Token: 0x020002E2 RID: 738
	[Obsolete("`VRTK_BezierPointer` has been replaced with `VRTK_BezierPointerRenderer` attached to a `VRTK_Pointer`. This script will be removed in a future version of VRTK.")]
	public class VRTK_BezierPointer : VRTK_BasePointer
	{
		// Token: 0x060018F2 RID: 6386 RVA: 0x00085207 File Offset: 0x00083407
		protected override void OnEnable()
		{
			base.OnEnable();
			this.beamActive = false;
			this.InitPointer();
			this.TogglePointer(false);
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00085224 File Offset: 0x00083424
		protected override void OnDisable()
		{
			base.OnDisable();
			this.beamActive = false;
			if (this.pointerCursor != null)
			{
				Object.Destroy(this.pointerCursor);
			}
			if (this.curvedBeam != null)
			{
				Object.Destroy(this.curvedBeam);
			}
			if (this.curvedBeamContainer != null)
			{
				Object.Destroy(this.curvedBeamContainer);
			}
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0008528C File Offset: 0x0008348C
		protected override void Update()
		{
			base.Update();
			if (this.beamActive)
			{
				Vector3 jointPosition = this.ProjectForwardBeam();
				Vector3 downPosition = this.ProjectDownBeam(jointPosition);
				this.AdjustForEarlyCollisions(jointPosition, downPosition);
			}
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000852C0 File Offset: 0x000834C0
		protected override void InitPointer()
		{
			this.pointerCursor = (this.customPointerCursor ? Object.Instantiate<GameObject>(this.customPointerCursor) : this.CreateCursor());
			if (this.validTeleportLocationObject != null)
			{
				this.validTeleportLocationInstance = Object.Instantiate<GameObject>(this.validTeleportLocationObject);
				this.validTeleportLocationInstance.name = string.Format("[{0}]BasePointer_BezierPointer_TeleportBeam", base.gameObject.name);
				this.validTeleportLocationInstance.transform.SetParent(this.pointerCursor.transform);
				this.validTeleportLocationInstance.layer = LayerMask.NameToLayer("Ignore Raycast");
				this.validTeleportLocationInstance.SetActive(false);
			}
			this.pointerCursor.name = string.Format("[{0}]BasePointer_BezierPointer_PointerCursor", base.gameObject.name);
			VRTK_PlayerObject.SetPlayerObject(this.pointerCursor, VRTK_PlayerObject.ObjectTypes.Pointer);
			this.pointerCursor.layer = LayerMask.NameToLayer("Ignore Raycast");
			this.pointerCursor.SetActive(false);
			this.curvedBeamContainer = new GameObject(string.Format("[{0}]BasePointer_BezierPointer_CurvedBeamContainer", base.gameObject.name));
			VRTK_PlayerObject.SetPlayerObject(this.curvedBeamContainer, VRTK_PlayerObject.ObjectTypes.Pointer);
			this.curvedBeamContainer.SetActive(false);
			this.curvedBeam = this.curvedBeamContainer.gameObject.AddComponent<VRTK_CurveGenerator>();
			this.curvedBeam.transform.SetParent(null);
			this.curvedBeam.Create(this.pointerDensity, this.pointerCursorRadius, this.customPointerTracer, this.rescalePointerTracer);
			base.InitPointer();
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00085443 File Offset: 0x00083643
		protected override void SetPointerMaterial(Color color)
		{
			base.ChangeMaterialColor(this.pointerCursor, color);
			base.SetPointerMaterial(color);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00085459 File Offset: 0x00083659
		protected override void TogglePointer(bool state)
		{
			state = (this.pointerVisibility == VRTK_BasePointer.pointerVisibilityStates.Always_On || state);
			this.beamActive = state;
			if (!this.beamActive)
			{
				this.ToggleBezierBeam(this.beamActive);
			}
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00085485 File Offset: 0x00083685
		protected override void DisablePointerBeam(object sender, ControllerInteractionEventArgs e)
		{
			base.DisablePointerBeam(sender, e);
			this.ToggleBezierBeam(false);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00085496 File Offset: 0x00083696
		private void ToggleBezierBeam(bool state)
		{
			if (base.gameObject.activeInHierarchy)
			{
				this.TogglePointerCursor(state);
				this.curvedBeam.TogglePoints(state);
			}
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000854B8 File Offset: 0x000836B8
		private GameObject CreateCursor()
		{
			float y = 0.02f;
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
			gameObject.transform.localScale = new Vector3(this.pointerCursorRadius, y, this.pointerCursorRadius);
			component.shadowCastingMode = ShadowCastingMode.Off;
			component.receiveShadows = false;
			component.material = this.pointerMaterial;
			Object.Destroy(gameObject.GetComponent<CapsuleCollider>());
			return gameObject;
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0008551C File Offset: 0x0008371C
		private void TogglePointerCursor(bool state)
		{
			bool active = this.showPointerCursor && state && this.showPointerCursor;
			this.pointerCursor.gameObject.SetActive(active);
			base.TogglePointer(state);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x00085558 File Offset: 0x00083758
		private Vector3 ProjectForwardBeam()
		{
			Transform origin = base.GetOrigin(true);
			float num = Vector3.Dot(Vector3.up, origin.forward.normalized);
			float num2 = this.pointerLength;
			Vector3 forward = origin.forward;
			if (num * 100f > this.beamHeightLimitAngle)
			{
				forward = new Vector3(forward.x, this.fixedForwardBeamForward.y, forward.z);
				float num3 = 1f - (num - this.beamHeightLimitAngle / 100f);
				num2 = this.pointerLength * num3 * num3;
			}
			else
			{
				this.fixedForwardBeamForward = origin.forward;
			}
			float num4 = num2;
			Ray ray = new Ray(origin.position, forward);
			RaycastHit raycastHit;
			bool flag = Physics.Raycast(ray, out raycastHit, num2, ~this.layersToIgnore);
			if (!flag || (this.pointerContactRaycastHit.collider && this.pointerContactRaycastHit.collider != raycastHit.collider))
			{
				this.pointerContactDistance = 0f;
			}
			if (flag)
			{
				this.pointerContactDistance = raycastHit.distance;
			}
			if (flag && this.pointerContactDistance < num2)
			{
				num4 = this.pointerContactDistance;
			}
			return ray.GetPoint(num4 - 1E-05f) + Vector3.up * 1E-05f;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0008569C File Offset: 0x0008389C
		private Vector3 ProjectDownBeam(Vector3 jointPosition)
		{
			Vector3 result = Vector3.zero;
			Ray ray = new Ray(jointPosition, Vector3.down);
			RaycastHit pointerContactRaycastHit;
			bool flag = Physics.Raycast(ray, out pointerContactRaycastHit, float.PositiveInfinity, ~this.layersToIgnore);
			if (!flag || (this.pointerContactRaycastHit.collider && this.pointerContactRaycastHit.collider != pointerContactRaycastHit.collider))
			{
				if (this.pointerContactRaycastHit.collider != null)
				{
					base.PointerOut();
				}
				this.pointerContactTarget = null;
				this.pointerContactRaycastHit = default(RaycastHit);
				this.contactNormal = Vector3.zero;
				result = ray.GetPoint(0f);
			}
			if (flag)
			{
				this.pointerContactTarget = pointerContactRaycastHit.transform;
				this.pointerContactRaycastHit = pointerContactRaycastHit;
				this.contactNormal = pointerContactRaycastHit.normal;
				result = ray.GetPoint(pointerContactRaycastHit.distance);
				base.PointerIn();
			}
			return result;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00085780 File Offset: 0x00083980
		private void SetPointerCursor(Vector3 cursorPosition)
		{
			this.destinationPosition = cursorPosition;
			if (this.pointerContactTarget != null)
			{
				this.TogglePointerCursor(true);
				this.pointerCursor.transform.position = cursorPosition;
				if (this.pointerCursorMatchTargetRotation)
				{
					this.pointerCursor.transform.rotation = Quaternion.FromToRotation(Vector3.up, this.contactNormal);
				}
				base.UpdateDependencies(this.pointerCursor.transform.position);
				base.UpdatePointerMaterial(this.pointerHitColor);
				if (this.validTeleportLocationInstance != null)
				{
					this.validTeleportLocationInstance.SetActive(this.ValidDestination(this.pointerContactTarget, this.destinationPosition));
					return;
				}
			}
			else
			{
				this.TogglePointerCursor(false);
				base.UpdatePointerMaterial(this.pointerMissColor);
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00085848 File Offset: 0x00083A48
		private void AdjustForEarlyCollisions(Vector3 jointPosition, Vector3 downPosition)
		{
			Vector3 vector = downPosition;
			Vector3 jointPosition2 = jointPosition;
			if (this.collisionCheckFrequency > 0)
			{
				this.collisionCheckFrequency = Mathf.Clamp(this.collisionCheckFrequency, 0, this.pointerDensity);
				Vector3[] controlPoints = new Vector3[]
				{
					base.GetOrigin(true).position,
					jointPosition + new Vector3(0f, this.beamCurveOffset, 0f),
					downPosition,
					downPosition
				};
				Vector3[] points = this.curvedBeam.GetPoints(controlPoints);
				int num = this.pointerDensity / this.collisionCheckFrequency;
				for (int i = 0; i < this.pointerDensity - num; i += num)
				{
					Vector3 vector2 = points[i];
					Vector3 vector3 = (i + num < points.Length) ? points[i + num] : points[points.Length - 1];
					Vector3 normalized = (vector3 - vector2).normalized;
					float maxDistance = Vector3.Distance(vector2, vector3);
					Ray ray = new Ray(vector2, normalized);
					RaycastHit raycastHit;
					if (Physics.Raycast(ray, out raycastHit, maxDistance, ~this.layersToIgnore))
					{
						Vector3 point = ray.GetPoint(raycastHit.distance);
						Ray ray2 = new Ray(point + Vector3.up * 0.01f, Vector3.down);
						RaycastHit raycastHit2;
						if (Physics.Raycast(ray2, out raycastHit2, float.PositiveInfinity, ~this.layersToIgnore))
						{
							vector = ray2.GetPoint(raycastHit2.distance);
							jointPosition2 = ((vector.y < jointPosition.y) ? new Vector3(vector.x, jointPosition.y, vector.z) : jointPosition);
							break;
						}
					}
				}
			}
			this.DisplayCurvedBeam(jointPosition2, vector);
			this.SetPointerCursor(vector);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00085A10 File Offset: 0x00083C10
		private void DisplayCurvedBeam(Vector3 jointPosition, Vector3 downPosition)
		{
			Vector3[] controlPoints = new Vector3[]
			{
				base.GetOrigin(false).position,
				jointPosition + new Vector3(0f, this.beamCurveOffset, 0f),
				downPosition,
				downPosition
			};
			Material material = this.customPointerTracer ? null : this.pointerMaterial;
			this.curvedBeam.SetPoints(controlPoints, material, this.currentPointerColor);
			if (this.pointerVisibility != VRTK_BasePointer.pointerVisibilityStates.Always_Off)
			{
				this.curvedBeam.TogglePoints(true);
			}
		}

		// Token: 0x0400146B RID: 5227
		[Header("Bezier Pointer Settings", order = 3)]
		[Tooltip("The length of the projected forward pointer beam, this is basically the distance able to point from the origin position.")]
		public float pointerLength = 10f;

		// Token: 0x0400146C RID: 5228
		[Tooltip("The number of items to render in the beam bezier curve. A high number here will most likely have a negative impact of game performance due to large number of rendered objects.")]
		public int pointerDensity = 10;

		// Token: 0x0400146D RID: 5229
		[Tooltip("The number of points along the bezier curve to check for an early beam collision. Useful if the bezier curve is appearing to clip through teleport locations. 0 won't make any checks and it will be capped at `Pointer Density`. The higher the number, the more CPU intensive the checks become.")]
		public int collisionCheckFrequency;

		// Token: 0x0400146E RID: 5230
		[Tooltip("The amount of height offset to apply to the projected beam to generate a smoother curve even when the beam is pointing straight.")]
		public float beamCurveOffset = 1f;

		// Token: 0x0400146F RID: 5231
		[Tooltip("The maximum angle in degrees of the origin before the beam curve height is restricted. A lower angle setting will prevent the beam being projected high into the sky and curving back down.")]
		[Range(1f, 100f)]
		public float beamHeightLimitAngle = 100f;

		// Token: 0x04001470 RID: 5232
		[Tooltip("Rescale each pointer tracer element according to the length of the Bezier curve.")]
		public bool rescalePointerTracer;

		// Token: 0x04001471 RID: 5233
		[Tooltip("A cursor is displayed on the ground at the location the beam ends at, it is useful to see what height the beam end location is, however it can be turned off by toggling this.")]
		public bool showPointerCursor = true;

		// Token: 0x04001472 RID: 5234
		[Tooltip("The size of the ground pointer cursor. This number also affects the size of the objects in the bezier curve beam. The larger the radius, the larger the objects will be.")]
		public float pointerCursorRadius = 0.5f;

		// Token: 0x04001473 RID: 5235
		[Tooltip("The pointer cursor will be rotated to match the angle of the target surface if this is true, if it is false then the pointer cursor will always be horizontal.")]
		public bool pointerCursorMatchTargetRotation;

		// Token: 0x04001474 RID: 5236
		[Header("Custom Appearance Settings", order = 4)]
		[Tooltip("A custom Game Object can be applied here to use instead of the default sphere for the beam tracer. The custom Game Object will match the rotation of the object attached to.")]
		public GameObject customPointerTracer;

		// Token: 0x04001475 RID: 5237
		[Tooltip("A custom Game Object can be applied here to use instead of the default flat cylinder for the pointer cursor.")]
		public GameObject customPointerCursor;

		// Token: 0x04001476 RID: 5238
		[Tooltip("A custom Game Object can be applied here to appear only if the teleport is allowed (its material will not be changed ).")]
		public GameObject validTeleportLocationObject;

		// Token: 0x04001477 RID: 5239
		private GameObject pointerCursor;

		// Token: 0x04001478 RID: 5240
		private GameObject curvedBeamContainer;

		// Token: 0x04001479 RID: 5241
		private VRTK_CurveGenerator curvedBeam;

		// Token: 0x0400147A RID: 5242
		private GameObject validTeleportLocationInstance;

		// Token: 0x0400147B RID: 5243
		private bool beamActive;

		// Token: 0x0400147C RID: 5244
		private Vector3 fixedForwardBeamForward;

		// Token: 0x0400147D RID: 5245
		private Vector3 contactNormal;

		// Token: 0x0400147E RID: 5246
		private const float BEAM_ADJUST_OFFSET = 1E-05f;
	}
}
