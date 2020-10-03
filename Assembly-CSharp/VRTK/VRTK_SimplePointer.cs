using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace VRTK
{
	// Token: 0x020002EB RID: 747
	[Obsolete("`VRTK_SimplePointer` has been replaced with `VRTK_StraightPointerRenderer` attached to a `VRTK_Pointer`. This script will be removed in a future version of VRTK.")]
	public class VRTK_SimplePointer : VRTK_BasePointer
	{
		// Token: 0x0600198B RID: 6539 RVA: 0x00087F6F File Offset: 0x0008616F
		protected override void OnEnable()
		{
			base.OnEnable();
			this.InitPointer();
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00087F7D File Offset: 0x0008617D
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.pointerHolder != null)
			{
				Object.Destroy(this.pointerHolder);
			}
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00087FA0 File Offset: 0x000861A0
		protected override void Update()
		{
			base.Update();
			if (this.pointerBeam && this.pointerBeam.activeSelf)
			{
				Transform origin = base.GetOrigin(true);
				RaycastHit collidedWith;
				bool flag = Physics.Raycast(new Ray(origin.position, origin.forward), out collidedWith, this.pointerLength, ~this.layersToIgnore);
				float pointerBeamLength = this.GetPointerBeamLength(flag, collidedWith);
				this.SetPointerTransform(pointerBeamLength, this.pointerThickness);
				if (flag)
				{
					if (this.pointerCursorMatchTargetNormal)
					{
						this.pointerTip.transform.forward = -collidedWith.normal;
					}
					if (this.pointerCursorRescaledAlongDistance)
					{
						float d = Vector3.Distance(collidedWith.point, origin.position);
						this.pointerTip.transform.localScale = this.pointerCursorOriginalScale * d;
					}
				}
				else
				{
					if (this.pointerCursorMatchTargetNormal)
					{
						this.pointerTip.transform.forward = origin.forward;
					}
					if (this.pointerCursorRescaledAlongDistance)
					{
						this.pointerTip.transform.localScale = this.pointerCursorOriginalScale * pointerBeamLength;
					}
				}
				if (this.activeEnabled)
				{
					this.activeEnabled = false;
					this.pointerBeam.GetComponentInChildren<Renderer>().enabled = this.storedBeamState;
					this.pointerTip.GetComponentInChildren<Renderer>().enabled = this.storedTipState;
				}
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000880FC File Offset: 0x000862FC
		protected override void UpdateObjectInteractor()
		{
			base.UpdateObjectInteractor();
			if (Vector3.Distance(this.objectInteractor.transform.position, this.pointerTip.transform.position) > 0f)
			{
				this.objectInteractor.transform.position = this.pointerTip.transform.position;
			}
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0008815C File Offset: 0x0008635C
		protected override void InitPointer()
		{
			this.pointerHolder = new GameObject(string.Format("[{0}]BasePointer_SimplePointer_Holder", base.gameObject.name));
			this.pointerHolder.transform.localPosition = Vector3.zero;
			VRTK_PlayerObject.SetPlayerObject(this.pointerHolder, VRTK_PlayerObject.ObjectTypes.Pointer);
			this.pointerBeam = GameObject.CreatePrimitive(PrimitiveType.Cube);
			this.pointerBeam.transform.name = string.Format("[{0}]BasePointer_SimplePointer_Pointer", base.gameObject.name);
			this.pointerBeam.transform.SetParent(this.pointerHolder.transform);
			this.pointerBeam.GetComponent<BoxCollider>().isTrigger = true;
			this.pointerBeam.AddComponent<Rigidbody>().isKinematic = true;
			this.pointerBeam.layer = LayerMask.NameToLayer("Ignore Raycast");
			MeshRenderer component = this.pointerBeam.GetComponent<MeshRenderer>();
			component.shadowCastingMode = ShadowCastingMode.Off;
			component.receiveShadows = false;
			component.material = this.pointerMaterial;
			VRTK_PlayerObject.SetPlayerObject(this.pointerBeam, VRTK_PlayerObject.ObjectTypes.Pointer);
			if (this.customPointerCursor)
			{
				this.pointerTip = Object.Instantiate<GameObject>(this.customPointerCursor);
			}
			else
			{
				this.pointerTip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				this.pointerTip.transform.localScale = this.pointerTipScale;
				MeshRenderer component2 = this.pointerTip.GetComponent<MeshRenderer>();
				component2.shadowCastingMode = ShadowCastingMode.Off;
				component2.receiveShadows = false;
				component2.material = this.pointerMaterial;
			}
			this.pointerCursorOriginalScale = this.pointerTip.transform.localScale;
			this.pointerTip.transform.name = string.Format("[{0}]BasePointer_SimplePointer_PointerTip", base.gameObject.name);
			this.pointerTip.transform.SetParent(this.pointerHolder.transform);
			this.pointerTip.GetComponent<Collider>().isTrigger = true;
			this.pointerTip.AddComponent<Rigidbody>().isKinematic = true;
			this.pointerTip.layer = LayerMask.NameToLayer("Ignore Raycast");
			VRTK_PlayerObject.SetPlayerObject(this.pointerTip, VRTK_PlayerObject.ObjectTypes.Pointer);
			base.InitPointer();
			this.ResizeObjectInteractor();
			this.SetPointerTransform(this.pointerLength, this.pointerThickness);
			this.TogglePointer(false);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00088381 File Offset: 0x00086581
		protected override void SetPointerMaterial(Color color)
		{
			base.SetPointerMaterial(color);
			base.ChangeMaterialColor(this.pointerBeam, color);
			base.ChangeMaterialColor(this.pointerTip, color);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000883A4 File Offset: 0x000865A4
		protected override void TogglePointer(bool state)
		{
			state = (this.pointerVisibility == VRTK_BasePointer.pointerVisibilityStates.Always_On || state);
			base.TogglePointer(state);
			if (this.pointerBeam)
			{
				this.pointerBeam.SetActive(state);
			}
			bool active = this.showPointerTip && state;
			if (this.pointerTip)
			{
				this.pointerTip.SetActive(active);
			}
			if (this.pointerBeam && this.pointerBeam.GetComponentInChildren<Renderer>() && this.pointerVisibility == VRTK_BasePointer.pointerVisibilityStates.Always_Off)
			{
				this.pointerBeam.GetComponentInChildren<Renderer>().enabled = false;
			}
			this.activeEnabled = state;
			if (this.activeEnabled)
			{
				this.storedBeamState = this.pointerBeam.GetComponentInChildren<Renderer>().enabled;
				this.storedTipState = this.pointerTip.GetComponentInChildren<Renderer>().enabled;
				this.pointerBeam.GetComponentInChildren<Renderer>().enabled = false;
				this.pointerTip.GetComponentInChildren<Renderer>().enabled = false;
			}
			this.ResizeObjectInteractor();
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000884A4 File Offset: 0x000866A4
		private void ResizeObjectInteractor()
		{
			if (this.showPointerTip && this.pointerTip && this.objectInteractor)
			{
				this.objectInteractor.transform.localScale = this.pointerTip.transform.localScale * 1.05f;
			}
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00088500 File Offset: 0x00086700
		private void SetPointerTransform(float setLength, float setThicknes)
		{
			float z = setLength / 2.00001f;
			Transform origin = base.GetOrigin(true);
			this.pointerBeam.transform.localScale = new Vector3(setThicknes, setThicknes, setLength);
			this.pointerBeam.transform.localPosition = new Vector3(0f, 0f, z);
			this.pointerHolder.transform.position = base.GetOrigin(false).position;
			this.pointerTip.transform.position = origin.position + origin.forward * (setLength - this.pointerTip.transform.localScale.z / 2f);
			this.pointerHolder.transform.LookAt(this.pointerTip.transform);
			base.UpdateDependencies(this.pointerTip.transform.position);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000885E8 File Offset: 0x000867E8
		private float GetPointerBeamLength(bool hasRayHit, RaycastHit collidedWith)
		{
			float pointerContactDistance = this.pointerLength;
			if (!hasRayHit || (this.pointerContactRaycastHit.collider && this.pointerContactRaycastHit.collider != collidedWith.collider))
			{
				if (this.pointerContactRaycastHit.collider != null)
				{
					base.PointerOut();
				}
				this.pointerContactDistance = 0f;
				this.pointerContactTarget = null;
				this.pointerContactRaycastHit = default(RaycastHit);
				this.destinationPosition = Vector3.zero;
				base.UpdatePointerMaterial(this.pointerMissColor);
			}
			if (hasRayHit)
			{
				this.pointerContactDistance = collidedWith.distance;
				this.pointerContactTarget = collidedWith.transform;
				this.pointerContactRaycastHit = collidedWith;
				this.destinationPosition = this.pointerTip.transform.position;
				base.UpdatePointerMaterial(this.pointerHitColor);
				base.PointerIn();
			}
			if (hasRayHit && this.pointerContactDistance < this.pointerLength)
			{
				pointerContactDistance = this.pointerContactDistance;
			}
			return this.OverrideBeamLength(pointerContactDistance);
		}

		// Token: 0x040014D7 RID: 5335
		[Header("Simple Pointer Settings", order = 3)]
		[Tooltip("The thickness and length of the beam can also be set on the script as well as the ability to toggle the sphere beam tip that is displayed at the end of the beam (to represent a cursor).")]
		public float pointerThickness = 0.002f;

		// Token: 0x040014D8 RID: 5336
		[Tooltip("The distance the beam will project before stopping.")]
		public float pointerLength = 100f;

		// Token: 0x040014D9 RID: 5337
		[Tooltip("Toggle whether the cursor is shown on the end of the pointer beam.")]
		public bool showPointerTip = true;

		// Token: 0x040014DA RID: 5338
		[Header("Custom Appearance Settings", order = 4)]
		[Tooltip("A custom Game Object can be applied here to use instead of the default sphere for the pointer cursor.")]
		public GameObject customPointerCursor;

		// Token: 0x040014DB RID: 5339
		[Tooltip("Rotate the pointer cursor to match the normal of the target surface (or the pointer direction if no target was hit).")]
		public bool pointerCursorMatchTargetNormal;

		// Token: 0x040014DC RID: 5340
		[Tooltip("Rescale the pointer cursor proportionally to the distance from this game object (useful when used as a gaze pointer).")]
		public bool pointerCursorRescaledAlongDistance;

		// Token: 0x040014DD RID: 5341
		private GameObject pointerHolder;

		// Token: 0x040014DE RID: 5342
		private GameObject pointerBeam;

		// Token: 0x040014DF RID: 5343
		private GameObject pointerTip;

		// Token: 0x040014E0 RID: 5344
		private Vector3 pointerTipScale = new Vector3(0.05f, 0.05f, 0.05f);

		// Token: 0x040014E1 RID: 5345
		private Vector3 pointerCursorOriginalScale = Vector3.one;

		// Token: 0x040014E2 RID: 5346
		private bool activeEnabled;

		// Token: 0x040014E3 RID: 5347
		private bool storedBeamState;

		// Token: 0x040014E4 RID: 5348
		private bool storedTipState;
	}
}
