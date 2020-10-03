using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace VRTK
{
	// Token: 0x020002E8 RID: 744
	[AddComponentMenu("VRTK/Scripts/Pointers/VRTK_PlayAreaCursor")]
	public class VRTK_PlayAreaCursor : MonoBehaviour
	{
		// Token: 0x1400009D RID: 157
		// (add) Token: 0x0600191E RID: 6430 RVA: 0x00085E3C File Offset: 0x0008403C
		// (remove) Token: 0x0600191F RID: 6431 RVA: 0x00085E74 File Offset: 0x00084074
		public event PlayAreaCursorEventHandler PlayAreaCursorStartCollision;

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x06001920 RID: 6432 RVA: 0x00085EAC File Offset: 0x000840AC
		// (remove) Token: 0x06001921 RID: 6433 RVA: 0x00085EE4 File Offset: 0x000840E4
		public event PlayAreaCursorEventHandler PlayAreaCursorEndCollision;

		// Token: 0x06001922 RID: 6434 RVA: 0x00085F19 File Offset: 0x00084119
		public virtual void OnPlayAreaCursorStartCollision(PlayAreaCursorEventArgs e)
		{
			if (this.PlayAreaCursorStartCollision != null)
			{
				this.PlayAreaCursorStartCollision(this, e);
			}
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00085F30 File Offset: 0x00084130
		public virtual void OnPlayAreaCursorEndCollision(PlayAreaCursorEventArgs e)
		{
			if (this.PlayAreaCursorEndCollision != null)
			{
				this.PlayAreaCursorEndCollision(this, e);
			}
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00085F47 File Offset: 0x00084147
		public virtual bool HasCollided()
		{
			return this.playAreaCursorCollided || this.headsetOutOfBounds;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00085F59 File Offset: 0x00084159
		public virtual void SetHeadsetPositionCompensation(bool state)
		{
			this.headsetPositionCompensation = state;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00085F62 File Offset: 0x00084162
		public virtual void SetPlayAreaCursorCollision(bool state, Collider collider = null)
		{
			this.playAreaCursorCollided = false;
			if (this.handlePlayAreaCursorCollisions)
			{
				this.playAreaCursorCollided = (base.enabled && state);
				this.EmitEvent(collider);
			}
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00085F8C File Offset: 0x0008418C
		public virtual void SetMaterialColor(Color color, bool validity)
		{
			if (this.validLocationObject == null)
			{
				this.ToggleVisibility(validity || this.displayOnInvalidLocation);
				if (this.usePointerColor)
				{
					for (int i = 0; i < this.playAreaCursorBoundaries.Length; i++)
					{
						this.SetCursorColor(this.playAreaCursorBoundaries[i], color);
					}
					return;
				}
			}
			else
			{
				this.ToggleValidPlayAreaState(!this.playAreaCursorCollided);
				if (this.usePointerColor)
				{
					this.SetCursorColor(this.playAreaCursor, color);
				}
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00086008 File Offset: 0x00084208
		public virtual void SetPlayAreaCursorTransform(Vector3 location)
		{
			Vector3 b = Vector3.zero;
			if (this.headsetPositionCompensation)
			{
				Vector3 a = new Vector3(this.playArea.transform.position.x, 0f, this.playArea.transform.position.z);
				Vector3 b2 = new Vector3(this.headset.position.x, 0f, this.headset.position.z);
				b = a - b2;
			}
			if (this.playAreaCursor != null)
			{
				if (this.playAreaCursor.activeInHierarchy && this.handlePlayAreaCursorCollisions && this.headsetOutOfBoundsIsCollision)
				{
					Vector3 point = new Vector3(location.x, this.playAreaCursor.transform.position.y + this.playAreaCursor.transform.localScale.y * 2f, location.z);
					if (!this.playAreaCursorCollider.bounds.Contains(point))
					{
						this.headsetOutOfBounds = true;
					}
					else
					{
						this.headsetOutOfBounds = false;
					}
				}
				this.playAreaCursor.transform.position = location + b;
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00086139 File Offset: 0x00084339
		public virtual void ToggleState(bool state)
		{
			state = (base.enabled && state);
			if (this.playAreaCursor != null)
			{
				this.playAreaCursor.SetActive(state);
			}
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00086163 File Offset: 0x00084363
		public virtual bool IsActive()
		{
			return this.playAreaCursor != null && this.playAreaCursor.activeInHierarchy;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00086180 File Offset: 0x00084380
		public virtual GameObject GetPlayAreaContainer()
		{
			return this.playAreaCursor;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00086188 File Offset: 0x00084388
		public virtual void ToggleVisibility(bool state)
		{
			if (this.playAreaCursor != null && this.boundaryRenderers.Length == 0)
			{
				this.boundaryRenderers = this.playAreaCursor.GetComponentsInChildren<Renderer>();
			}
			for (int i = 0; i < this.boundaryRenderers.Length; i++)
			{
				if (this.boundaryRenderers[i] != null)
				{
					this.boundaryRenderers[i].enabled = state;
				}
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x000861EE File Offset: 0x000843EE
		protected virtual void OnEnable()
		{
			VRTK_PlayerObject.SetPlayerObject(base.gameObject, VRTK_PlayerObject.ObjectTypes.Pointer);
			this.headset = VRTK_DeviceFinder.HeadsetTransform();
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			this.playAreaCursorBoundaries = new GameObject[4];
			this.InitPlayAreaCursor();
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00086224 File Offset: 0x00084424
		protected virtual void OnDisable()
		{
			if (this.playAreaCursor != null)
			{
				Object.Destroy(this.playAreaCursor);
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0008623F File Offset: 0x0008443F
		protected virtual void Update()
		{
			if (base.enabled && this.IsActive())
			{
				this.UpdateCollider();
			}
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00086258 File Offset: 0x00084458
		protected virtual PlayAreaCursorEventArgs SetEventPayload(Collider collider)
		{
			PlayAreaCursorEventArgs result;
			result.collider = collider;
			return result;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0008626E File Offset: 0x0008446E
		protected virtual void EmitEvent(Collider collider)
		{
			if (collider != null)
			{
				if (this.playAreaCursorCollided)
				{
					this.OnPlayAreaCursorStartCollision(this.SetEventPayload(collider));
					return;
				}
				this.OnPlayAreaCursorEndCollision(this.SetEventPayload(collider));
			}
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0008629C File Offset: 0x0008449C
		protected virtual void InitPlayAreaCursor()
		{
			if (this.playArea == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.SDK_OBJECT_NOT_FOUND, new object[]
				{
					"PlayArea",
					"Boundaries SDK"
				}));
				return;
			}
			Vector3[] array = VRTK_SDK_Bridge.GetPlayAreaVertices();
			if (this.validLocationObject != null)
			{
				this.GeneratePlayAreaCursorFromPrefab(array);
			}
			else
			{
				if (array == null || array.Length < 8)
				{
					array = new Vector3[]
					{
						new Vector3(0.8f, 0f, -0.8f),
						new Vector3(-0.8f, 0f, -0.8f),
						new Vector3(-0.8f, 0f, 0.8f),
						new Vector3(0.8f, 0f, 0.8f),
						new Vector3(1f, 0f, -1f),
						new Vector3(-1f, 0f, -1f),
						new Vector3(-1f, 0f, 1f),
						new Vector3(1f, 0f, 1f)
					};
				}
				this.GeneratePlayAreaCursor(array);
			}
			if (this.playAreaCursor != null)
			{
				this.playAreaCursor.SetActive(false);
				VRTK_PlayerObject.SetPlayerObject(this.playAreaCursor, VRTK_PlayerObject.ObjectTypes.Pointer);
				this.CreateCursorCollider(this.playAreaCursor);
				this.playAreaCursor.AddComponent<Rigidbody>().isKinematic = true;
				VRTK_PlayAreaCollider vrtk_PlayAreaCollider = this.playAreaCursor.AddComponent<VRTK_PlayAreaCollider>();
				vrtk_PlayAreaCollider.SetParent(this);
				vrtk_PlayAreaCollider.SetIgnoreTarget(this.targetListPolicy);
				this.playAreaCursor.layer = LayerMask.NameToLayer("Ignore Raycast");
			}
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00086460 File Offset: 0x00084660
		protected virtual void SetCursorColor(GameObject cursorObject, Color color)
		{
			Renderer componentInChildren = cursorObject.GetComponentInChildren<Renderer>();
			if (componentInChildren && componentInChildren.material && componentInChildren.material.HasProperty("_Color"))
			{
				componentInChildren.material.color = color;
				componentInChildren.shadowCastingMode = ShadowCastingMode.Off;
				componentInChildren.receiveShadows = false;
			}
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x000864B8 File Offset: 0x000846B8
		protected virtual void ToggleValidPlayAreaState(bool state)
		{
			if (this.playAreaCursorValidChild != null)
			{
				this.playAreaCursorValidChild.SetActive(state);
			}
			if (this.playAreaCursorInvalidChild != null)
			{
				this.playAreaCursorInvalidChild.SetActive(this.displayOnInvalidLocation && !state);
			}
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00086507 File Offset: 0x00084707
		protected virtual string GeneratePlayAreaCursorName()
		{
			return VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				base.gameObject.name,
				"PlayAreaCursor"
			});
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0008652C File Offset: 0x0008472C
		protected virtual void GeneratePlayAreaCursorFromPrefab(Vector3[] cursorDrawVertices)
		{
			this.playAreaCursor = new GameObject(this.GeneratePlayAreaCursorName());
			float x = cursorDrawVertices[this.btmRightOuter].x - cursorDrawVertices[this.topLeftOuter].x;
			float z = cursorDrawVertices[this.topLeftOuter].z - cursorDrawVertices[this.btmRightOuter].z;
			if (this.playAreaCursorDimensions != Vector2.zero)
			{
				x = ((this.playAreaCursorDimensions.x == 0f) ? this.playAreaCursor.transform.localScale.x : this.playAreaCursorDimensions.x);
				z = ((this.playAreaCursorDimensions.y == 0f) ? this.playAreaCursor.transform.localScale.z : this.playAreaCursorDimensions.y);
			}
			float y = 0.01f;
			this.playAreaCursorValidChild = Object.Instantiate<GameObject>(this.validLocationObject);
			this.playAreaCursorValidChild.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				"ValidArea"
			});
			this.playAreaCursorValidChild.transform.SetParent(this.playAreaCursor.transform);
			if (this.invalidLocationObject != null)
			{
				this.playAreaCursorInvalidChild = Object.Instantiate<GameObject>(this.invalidLocationObject);
				this.playAreaCursorInvalidChild.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
				{
					"InvalidArea"
				});
				this.playAreaCursorInvalidChild.transform.SetParent(this.playAreaCursor.transform);
			}
			this.playAreaCursor.transform.localScale = new Vector3(x, y, z);
			this.playAreaCursorValidChild.transform.localScale = Vector3.one;
			if (this.invalidLocationObject != null)
			{
				this.playAreaCursorInvalidChild.transform.localScale = Vector3.one;
			}
			this.playAreaCursor.SetActive(false);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00086714 File Offset: 0x00084914
		protected virtual void GeneratePlayAreaCursor(Vector3[] cursorDrawVertices)
		{
			if (this.playAreaCursorDimensions != Vector2.zero)
			{
				float playAreaBorderThickness = VRTK_SDK_Bridge.GetPlayAreaBorderThickness();
				cursorDrawVertices[this.btmRightOuter] = new Vector3(this.playAreaCursorDimensions.x / 2f, 0f, this.playAreaCursorDimensions.y / 2f * -1f);
				cursorDrawVertices[this.btmLeftOuter] = new Vector3(this.playAreaCursorDimensions.x / 2f * -1f, 0f, this.playAreaCursorDimensions.y / 2f * -1f);
				cursorDrawVertices[this.topLeftOuter] = new Vector3(this.playAreaCursorDimensions.x / 2f * -1f, 0f, this.playAreaCursorDimensions.y / 2f);
				cursorDrawVertices[this.topRightOuter] = new Vector3(this.playAreaCursorDimensions.x / 2f, 0f, this.playAreaCursorDimensions.y / 2f);
				cursorDrawVertices[this.btmRightInner] = cursorDrawVertices[this.btmRightOuter] + new Vector3(-playAreaBorderThickness, 0f, playAreaBorderThickness);
				cursorDrawVertices[this.btmLeftInner] = cursorDrawVertices[this.btmLeftOuter] + new Vector3(playAreaBorderThickness, 0f, playAreaBorderThickness);
				cursorDrawVertices[this.topLeftInner] = cursorDrawVertices[this.topLeftOuter] + new Vector3(playAreaBorderThickness, 0f, -playAreaBorderThickness);
				cursorDrawVertices[this.topRightInner] = cursorDrawVertices[this.topRightOuter] + new Vector3(-playAreaBorderThickness, 0f, -playAreaBorderThickness);
			}
			float x = cursorDrawVertices[this.btmRightOuter].x - cursorDrawVertices[this.topLeftOuter].x;
			float z = cursorDrawVertices[this.topLeftOuter].z - cursorDrawVertices[this.btmRightOuter].z;
			float num = 0.01f;
			this.playAreaCursor = new GameObject(this.GeneratePlayAreaCursorName());
			this.playAreaCursor.transform.SetParent(null);
			this.playAreaCursor.transform.localScale = new Vector3(x, num, z);
			float num2 = this.playArea.transform.localScale.x / 2f;
			float num3 = this.playArea.transform.localScale.z / 2f;
			float y = 0f;
			this.DrawPlayAreaCursorBoundary(0, cursorDrawVertices[this.btmLeftOuter].x, cursorDrawVertices[this.btmRightOuter].x, cursorDrawVertices[this.btmRightInner].z, cursorDrawVertices[this.btmRightOuter].z, num, new Vector3(0f, y, num3));
			this.DrawPlayAreaCursorBoundary(1, cursorDrawVertices[this.btmLeftOuter].x, cursorDrawVertices[this.btmLeftInner].x, cursorDrawVertices[this.topLeftOuter].z, cursorDrawVertices[this.btmLeftOuter].z, num, new Vector3(num2, y, 0f));
			this.DrawPlayAreaCursorBoundary(2, cursorDrawVertices[this.btmLeftOuter].x, cursorDrawVertices[this.btmRightOuter].x, cursorDrawVertices[this.btmRightInner].z, cursorDrawVertices[this.btmRightOuter].z, num, new Vector3(0f, y, -num3));
			this.DrawPlayAreaCursorBoundary(3, cursorDrawVertices[this.btmLeftOuter].x, cursorDrawVertices[this.btmLeftInner].x, cursorDrawVertices[this.topLeftOuter].z, cursorDrawVertices[this.btmLeftOuter].z, num, new Vector3(-num2, y, 0f));
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00086B18 File Offset: 0x00084D18
		protected virtual void DrawPlayAreaCursorBoundary(int index, float left, float right, float top, float bottom, float thickness, Vector3 localPosition)
		{
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			gameObject.name = VRTK_SharedMethods.GenerateVRTKObjectName(true, new object[]
			{
				base.gameObject.name,
				"PlayAreaCursorBoundary",
				index
			});
			VRTK_PlayerObject.SetPlayerObject(gameObject, VRTK_PlayerObject.ObjectTypes.Pointer);
			float x = (right - left) / 1.065f;
			float z = (top - bottom) / 1.08f;
			gameObject.transform.localScale = new Vector3(x, thickness, z);
			Object.Destroy(gameObject.GetComponent<BoxCollider>());
			gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
			gameObject.transform.SetParent(this.playAreaCursor.transform);
			gameObject.transform.localPosition = localPosition;
			this.playAreaCursorBoundaries[index] = gameObject;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00086BD8 File Offset: 0x00084DD8
		protected virtual void CreateCursorCollider(GameObject cursor)
		{
			this.playAreaCursorCollider = cursor.AddComponent<BoxCollider>();
			this.playAreaCursorCollider.isTrigger = true;
			this.playAreaCursorCollider.center = new Vector3(0f, 65f, 0f);
			this.playAreaCursorCollider.size = new Vector3(1f, 1f, 1f);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00086C3C File Offset: 0x00084E3C
		protected virtual void UpdateCollider()
		{
			float num = 1f;
			float num2 = (this.headset.transform.position.y - this.playArea.transform.position.y) * 100f;
			float y = (num2 != 0f) ? (num2 / 2f + num) : 0f;
			this.playAreaCursorCollider.size = new Vector3(this.playAreaCursorCollider.size.x, num2, this.playAreaCursorCollider.size.z);
			this.playAreaCursorCollider.center = new Vector3(this.playAreaCursorCollider.center.x, y, this.playAreaCursorCollider.center.z);
		}

		// Token: 0x04001493 RID: 5267
		[Header("Appearance Settings")]
		[Tooltip("If this is checked then the pointer valid/invalid colours will also be used to change the colour of the play area cursor when colliding/not colliding.")]
		public bool usePointerColor = true;

		// Token: 0x04001494 RID: 5268
		[Tooltip("Determines the size of the play area cursor and collider. If the values are left as zero then the Play Area Cursor will be sized to the calibrated Play Area space.")]
		public Vector2 playAreaCursorDimensions = Vector2.zero;

		// Token: 0x04001495 RID: 5269
		[Tooltip("If this is checked then if the play area cursor is colliding with any other object then the pointer colour will change to the `Pointer Miss Color` and the `DestinationMarkerSet` event will not be triggered, which will prevent teleporting into areas where the play area will collide.")]
		public bool handlePlayAreaCursorCollisions;

		// Token: 0x04001496 RID: 5270
		[Tooltip("If this is checked then if the user's headset is outside of the play area cursor bounds then it is considered a collision even if the play area isn't colliding with anything.")]
		public bool headsetOutOfBoundsIsCollision;

		// Token: 0x04001497 RID: 5271
		[Tooltip("If this is checked then the play area cursor will be displayed when the location is invalid.")]
		public bool displayOnInvalidLocation = true;

		// Token: 0x04001498 RID: 5272
		[Tooltip("A specified VRTK_PolicyList to use to determine whether the play area cursor collisions will be acted upon.")]
		public VRTK_PolicyList targetListPolicy;

		// Token: 0x04001499 RID: 5273
		[Header("Custom Settings")]
		[Tooltip("A custom GameObject to use for the play area cursor representation for when the location is valid.")]
		public GameObject validLocationObject;

		// Token: 0x0400149A RID: 5274
		[Tooltip("A custom GameObject to use for the play area cursor representation for when the location is invalid.")]
		public GameObject invalidLocationObject;

		// Token: 0x0400149D RID: 5277
		protected bool headsetPositionCompensation;

		// Token: 0x0400149E RID: 5278
		protected bool playAreaCursorCollided;

		// Token: 0x0400149F RID: 5279
		protected bool headsetOutOfBounds;

		// Token: 0x040014A0 RID: 5280
		protected Transform playArea;

		// Token: 0x040014A1 RID: 5281
		protected GameObject playAreaCursor;

		// Token: 0x040014A2 RID: 5282
		protected GameObject[] playAreaCursorBoundaries;

		// Token: 0x040014A3 RID: 5283
		protected BoxCollider playAreaCursorCollider;

		// Token: 0x040014A4 RID: 5284
		protected Transform headset;

		// Token: 0x040014A5 RID: 5285
		protected Renderer[] boundaryRenderers = new Renderer[0];

		// Token: 0x040014A6 RID: 5286
		protected GameObject playAreaCursorValidChild;

		// Token: 0x040014A7 RID: 5287
		protected GameObject playAreaCursorInvalidChild;

		// Token: 0x040014A8 RID: 5288
		protected int btmRightInner;

		// Token: 0x040014A9 RID: 5289
		protected int btmLeftInner = 1;

		// Token: 0x040014AA RID: 5290
		protected int topLeftInner = 2;

		// Token: 0x040014AB RID: 5291
		protected int topRightInner = 3;

		// Token: 0x040014AC RID: 5292
		protected int btmRightOuter = 4;

		// Token: 0x040014AD RID: 5293
		protected int btmLeftOuter = 5;

		// Token: 0x040014AE RID: 5294
		protected int topLeftOuter = 6;

		// Token: 0x040014AF RID: 5295
		protected int topRightOuter = 7;
	}
}
