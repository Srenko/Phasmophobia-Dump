using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200041E RID: 1054
	public class Hand : MonoBehaviour
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x0009FE6A File Offset: 0x0009E06A
		public ReadOnlyCollection<Hand.AttachedObject> AttachedObjects
		{
			get
			{
				return this.attachedObjects.AsReadOnly();
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x0009FE77 File Offset: 0x0009E077
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x0009FE7F File Offset: 0x0009E07F
		public bool hoverLocked { get; private set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x0009FE88 File Offset: 0x0009E088
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x0009FE90 File Offset: 0x0009E090
		public Interactable hoveringInteractable
		{
			get
			{
				return this._hoveringInteractable;
			}
			set
			{
				if (this._hoveringInteractable != value)
				{
					if (this._hoveringInteractable != null)
					{
						this.HandDebugLog("HoverEnd " + this._hoveringInteractable.gameObject);
						this._hoveringInteractable.SendMessage("OnHandHoverEnd", this, SendMessageOptions.DontRequireReceiver);
						if (this._hoveringInteractable != null)
						{
							base.BroadcastMessage("OnParentHandHoverEnd", this._hoveringInteractable, SendMessageOptions.DontRequireReceiver);
						}
					}
					this._hoveringInteractable = value;
					if (this._hoveringInteractable != null)
					{
						this.HandDebugLog("HoverBegin " + this._hoveringInteractable.gameObject);
						this._hoveringInteractable.SendMessage("OnHandHoverBegin", this, SendMessageOptions.DontRequireReceiver);
						if (this._hoveringInteractable != null)
						{
							base.BroadcastMessage("OnParentHandHoverBegin", this._hoveringInteractable, SendMessageOptions.DontRequireReceiver);
						}
					}
				}
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x0009FF6B File Offset: 0x0009E16B
		public GameObject currentAttachedObject
		{
			get
			{
				this.CleanUpAttachedObjectStack();
				if (this.attachedObjects.Count > 0)
				{
					return this.attachedObjects[this.attachedObjects.Count - 1].attachedObject;
				}
				return null;
			}
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x0009FFA0 File Offset: 0x0009E1A0
		public Transform GetAttachmentTransform(string attachmentPoint = "")
		{
			Transform transform = null;
			if (!string.IsNullOrEmpty(attachmentPoint))
			{
				transform = base.transform.Find(attachmentPoint);
			}
			if (!transform)
			{
				transform = base.transform;
			}
			return transform;
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x0009FFD4 File Offset: 0x0009E1D4
		public Hand.HandType GuessCurrentHandType()
		{
			if (this.startingHandType == Hand.HandType.Left || this.startingHandType == Hand.HandType.Right)
			{
				return this.startingHandType;
			}
			if (this.startingHandType == Hand.HandType.Any && this.otherHand != null && this.otherHand.controller == null)
			{
				return Hand.HandType.Right;
			}
			if (this.controller == null || this.otherHand == null || this.otherHand.controller == null)
			{
				return this.startingHandType;
			}
			if ((ulong)this.controller.index == (ulong)((long)SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost, ETrackedDeviceClass.Controller, 0)))
			{
				return Hand.HandType.Left;
			}
			return Hand.HandType.Right;
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x000A0064 File Offset: 0x0009E264
		public void AttachObject(GameObject objectToAttach, Hand.AttachmentFlags flags = Hand.AttachmentFlags.SnapOnAttach | Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.ParentToHand, string attachmentPoint = "")
		{
			if (flags == (Hand.AttachmentFlags)0)
			{
				flags = (Hand.AttachmentFlags.SnapOnAttach | Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.ParentToHand);
			}
			this.CleanUpAttachedObjectStack();
			this.DetachObject(objectToAttach, true);
			if ((flags & Hand.AttachmentFlags.DetachFromOtherHand) == Hand.AttachmentFlags.DetachFromOtherHand && this.otherHand)
			{
				this.otherHand.DetachObject(objectToAttach, true);
			}
			if ((flags & Hand.AttachmentFlags.DetachOthers) == Hand.AttachmentFlags.DetachOthers)
			{
				while (this.attachedObjects.Count > 0)
				{
					this.DetachObject(this.attachedObjects[0].attachedObject, true);
				}
			}
			if (this.currentAttachedObject)
			{
				this.currentAttachedObject.SendMessage("OnHandFocusLost", this, SendMessageOptions.DontRequireReceiver);
			}
			Hand.AttachedObject item = default(Hand.AttachedObject);
			item.attachedObject = objectToAttach;
			item.originalParent = ((objectToAttach.transform.parent != null) ? objectToAttach.transform.parent.gameObject : null);
			if ((flags & Hand.AttachmentFlags.ParentToHand) == Hand.AttachmentFlags.ParentToHand)
			{
				objectToAttach.transform.parent = this.GetAttachmentTransform(attachmentPoint);
				item.isParentedToHand = true;
			}
			else
			{
				item.isParentedToHand = false;
			}
			this.attachedObjects.Add(item);
			if ((flags & Hand.AttachmentFlags.SnapOnAttach) == Hand.AttachmentFlags.SnapOnAttach)
			{
				objectToAttach.transform.localPosition = Vector3.zero;
				objectToAttach.transform.localRotation = Quaternion.identity;
			}
			this.HandDebugLog("AttachObject " + objectToAttach);
			objectToAttach.SendMessage("OnAttachedToHand", this, SendMessageOptions.DontRequireReceiver);
			this.UpdateHovering();
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000A01B0 File Offset: 0x0009E3B0
		public void DetachObject(GameObject objectToDetach, bool restoreOriginalParent = true)
		{
			int num = this.attachedObjects.FindIndex((Hand.AttachedObject l) => l.attachedObject == objectToDetach);
			if (num != -1)
			{
				this.HandDebugLog("DetachObject " + objectToDetach);
				GameObject currentAttachedObject = this.currentAttachedObject;
				Transform parent = null;
				if (this.attachedObjects[num].isParentedToHand)
				{
					if (restoreOriginalParent && this.attachedObjects[num].originalParent != null)
					{
						parent = this.attachedObjects[num].originalParent.transform;
					}
					this.attachedObjects[num].attachedObject.transform.parent = parent;
				}
				this.attachedObjects[num].attachedObject.SetActive(true);
				this.attachedObjects[num].attachedObject.SendMessage("OnDetachedFromHand", this, SendMessageOptions.DontRequireReceiver);
				this.attachedObjects.RemoveAt(num);
				GameObject currentAttachedObject2 = this.currentAttachedObject;
				if (currentAttachedObject2 != null && currentAttachedObject2 != currentAttachedObject)
				{
					currentAttachedObject2.SetActive(true);
					currentAttachedObject2.SendMessage("OnHandFocusAcquired", this, SendMessageOptions.DontRequireReceiver);
				}
			}
			this.CleanUpAttachedObjectStack();
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000A02E2 File Offset: 0x0009E4E2
		public Vector3 GetTrackedObjectVelocity()
		{
			if (this.controller != null)
			{
				return base.transform.parent.TransformVector(this.controller.velocity);
			}
			return Vector3.zero;
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000A030D File Offset: 0x0009E50D
		public Vector3 GetTrackedObjectAngularVelocity()
		{
			if (this.controller != null)
			{
				return base.transform.parent.TransformVector(this.controller.angularVelocity);
			}
			return Vector3.zero;
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000A0338 File Offset: 0x0009E538
		private void CleanUpAttachedObjectStack()
		{
			this.attachedObjects.RemoveAll((Hand.AttachedObject l) => l.attachedObject == null);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000A0368 File Offset: 0x0009E568
		private void Awake()
		{
			this.inputFocusAction = SteamVR_Events.InputFocusAction(new UnityAction<bool>(this.OnInputFocus));
			if (this.hoverSphereTransform == null)
			{
				this.hoverSphereTransform = base.transform;
			}
			this.applicationLostFocusObject = new GameObject("_application_lost_focus");
			this.applicationLostFocusObject.transform.parent = base.transform;
			this.applicationLostFocusObject.SetActive(false);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000A03D8 File Offset: 0x0009E5D8
		private IEnumerator Start()
		{
			this.playerInstance = Player.instance;
			if (!this.playerInstance)
			{
				Debug.LogError("No player instance found in Hand Start()");
			}
			this.overlappingColliders = new Collider[16];
			if (this.noSteamVRFallbackCamera)
			{
				yield break;
			}
			for (;;)
			{
				yield return new WaitForSeconds(1f);
				if (this.controller != null)
				{
					break;
				}
				if (this.startingHandType == Hand.HandType.Left || this.startingHandType == Hand.HandType.Right)
				{
					int deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost, ETrackedDeviceClass.Controller, 0);
					int deviceIndex2 = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost, ETrackedDeviceClass.Controller, 0);
					if (deviceIndex != -1 && deviceIndex2 != -1 && deviceIndex != deviceIndex2)
					{
						int index = (this.startingHandType == Hand.HandType.Right) ? deviceIndex2 : deviceIndex;
						int index2 = (this.startingHandType == Hand.HandType.Right) ? deviceIndex : deviceIndex2;
						this.InitController(index);
						if (this.otherHand)
						{
							this.otherHand.InitController(index2);
						}
					}
				}
				else
				{
					SteamVR instance = SteamVR.instance;
					int num = 0;
					while ((long)num < 16L)
					{
						if (instance.hmd.GetTrackedDeviceClass((uint)num) == ETrackedDeviceClass.Controller && SteamVR_Controller.Input(num).valid && (!(this.otherHand != null) || this.otherHand.controller == null || num != (int)this.otherHand.controller.index))
						{
							this.InitController(num);
						}
						num++;
					}
				}
			}
			yield break;
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000A03E8 File Offset: 0x0009E5E8
		private void UpdateHovering()
		{
			if (this.noSteamVRFallbackCamera == null && this.controller == null)
			{
				return;
			}
			if (this.hoverLocked)
			{
				return;
			}
			if (this.applicationLostFocusObject.activeSelf)
			{
				return;
			}
			float num = float.MaxValue;
			Interactable hoveringInteractable = null;
			float x = this.playerInstance.transform.lossyScale.x;
			float num2 = this.hoverSphereRadius * x;
			float num3 = Util.RemapNumberClamped(Mathf.Abs(base.transform.position.y - this.playerInstance.trackingOriginTransform.position.y), 0f, 0.5f * x, 5f, 1f) * x;
			for (int i = 0; i < this.overlappingColliders.Length; i++)
			{
				this.overlappingColliders[i] = null;
			}
			Physics.OverlapBoxNonAlloc(this.hoverSphereTransform.position - new Vector3(0f, num2 * num3 - num2, 0f), new Vector3(num2, num2 * num3 * 2f, num2), this.overlappingColliders, Quaternion.identity, this.hoverLayerMask.value);
			int num4 = 0;
			Collider[] array = this.overlappingColliders;
			for (int j = 0; j < array.Length; j++)
			{
				Collider collider = array[j];
				if (!(collider == null))
				{
					Interactable contacting = collider.GetComponentInParent<Interactable>();
					if (!(contacting == null))
					{
						IgnoreHovering component = collider.GetComponent<IgnoreHovering>();
						if ((!(component != null) || (!(component.onlyIgnoreHand == null) && !(component.onlyIgnoreHand == this))) && this.attachedObjects.FindIndex((Hand.AttachedObject l) => l.attachedObject == contacting.gameObject) == -1 && (!this.otherHand || !(this.otherHand.hoveringInteractable == contacting)))
						{
							float num5 = Vector3.Distance(contacting.transform.position, this.hoverSphereTransform.position);
							if (num5 < num)
							{
								num = num5;
								hoveringInteractable = contacting;
							}
							num4++;
						}
					}
				}
			}
			this.hoveringInteractable = hoveringInteractable;
			if (num4 > 0 && num4 != this.prevOverlappingColliders)
			{
				this.prevOverlappingColliders = num4;
				this.HandDebugLog("Found " + num4 + " overlapping colliders.");
			}
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000A0650 File Offset: 0x0009E850
		private void UpdateNoSteamVRFallback()
		{
			if (this.noSteamVRFallbackCamera)
			{
				Ray ray = this.noSteamVRFallbackCamera.ScreenPointToRay(Input.mousePosition);
				if (this.attachedObjects.Count > 0)
				{
					base.transform.position = ray.origin + this.noSteamVRFallbackInteractorDistance * ray.direction;
					return;
				}
				Vector3 position = base.transform.position;
				base.transform.position = this.noSteamVRFallbackCamera.transform.forward * -1000f;
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, this.noSteamVRFallbackMaxDistanceNoItem))
				{
					base.transform.position = raycastHit.point;
					this.noSteamVRFallbackInteractorDistance = Mathf.Min(this.noSteamVRFallbackMaxDistanceNoItem, raycastHit.distance);
					return;
				}
				if (this.noSteamVRFallbackInteractorDistance > 0f)
				{
					base.transform.position = ray.origin + Mathf.Min(this.noSteamVRFallbackMaxDistanceNoItem, this.noSteamVRFallbackInteractorDistance) * ray.direction;
					return;
				}
				base.transform.position = position;
			}
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000A0770 File Offset: 0x0009E970
		private void UpdateDebugText()
		{
			if (this.showDebugText)
			{
				if (this.debugText == null)
				{
					this.debugText = new GameObject("_debug_text").AddComponent<TextMesh>();
					this.debugText.fontSize = 120;
					this.debugText.characterSize = 0.001f;
					this.debugText.transform.parent = base.transform;
					this.debugText.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
				}
				if (this.GuessCurrentHandType() == Hand.HandType.Right)
				{
					this.debugText.transform.localPosition = new Vector3(-0.05f, 0f, 0f);
					this.debugText.alignment = TextAlignment.Right;
					this.debugText.anchor = TextAnchor.UpperRight;
				}
				else
				{
					this.debugText.transform.localPosition = new Vector3(0.05f, 0f, 0f);
					this.debugText.alignment = TextAlignment.Left;
					this.debugText.anchor = TextAnchor.UpperLeft;
				}
				this.debugText.text = string.Format("Hovering: {0}\nHover Lock: {1}\nAttached: {2}\nTotal Attached: {3}\nType: {4}\n", new object[]
				{
					this.hoveringInteractable ? this.hoveringInteractable.gameObject.name : "null",
					this.hoverLocked,
					this.currentAttachedObject ? this.currentAttachedObject.name : "null",
					this.attachedObjects.Count,
					this.GuessCurrentHandType().ToString()
				});
				return;
			}
			if (this.debugText != null)
			{
				Object.Destroy(this.debugText.gameObject);
			}
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000A0940 File Offset: 0x0009EB40
		private void OnEnable()
		{
			this.inputFocusAction.enabled = true;
			float time = (this.otherHand != null && this.otherHand.GetInstanceID() < base.GetInstanceID()) ? (0.5f * this.hoverUpdateInterval) : 0f;
			base.InvokeRepeating("UpdateHovering", time, this.hoverUpdateInterval);
			base.InvokeRepeating("UpdateDebugText", time, this.hoverUpdateInterval);
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000A09B2 File Offset: 0x0009EBB2
		private void OnDisable()
		{
			this.inputFocusAction.enabled = false;
			base.CancelInvoke();
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000A09C8 File Offset: 0x0009EBC8
		private void Update()
		{
			this.UpdateNoSteamVRFallback();
			GameObject currentAttachedObject = this.currentAttachedObject;
			if (currentAttachedObject)
			{
				currentAttachedObject.SendMessage("HandAttachedUpdate", this, SendMessageOptions.DontRequireReceiver);
			}
			if (this.hoveringInteractable)
			{
				this.hoveringInteractable.SendMessage("HandHoverUpdate", this, SendMessageOptions.DontRequireReceiver);
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000A0A16 File Offset: 0x0009EC16
		private void LateUpdate()
		{
			if (this.controllerObject != null && this.attachedObjects.Count == 0)
			{
				this.AttachObject(this.controllerObject, Hand.AttachmentFlags.SnapOnAttach | Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.ParentToHand, "");
			}
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000A0A48 File Offset: 0x0009EC48
		private void OnInputFocus(bool hasFocus)
		{
			if (hasFocus)
			{
				this.DetachObject(this.applicationLostFocusObject, true);
				this.applicationLostFocusObject.SetActive(false);
				this.UpdateHandPoses();
				this.UpdateHovering();
				base.BroadcastMessage("OnParentHandInputFocusAcquired", SendMessageOptions.DontRequireReceiver);
				return;
			}
			this.applicationLostFocusObject.SetActive(true);
			this.AttachObject(this.applicationLostFocusObject, Hand.AttachmentFlags.ParentToHand, "");
			base.BroadcastMessage("OnParentHandInputFocusLost", SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000A0AB4 File Offset: 0x0009ECB4
		private void FixedUpdate()
		{
			this.UpdateHandPoses();
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000A0ABC File Offset: 0x0009ECBC
		private void OnDrawGizmos()
		{
			Gizmos.color = new Color(0.5f, 1f, 0.5f, 0.9f);
			Gizmos.DrawWireSphere((this.hoverSphereTransform ? this.hoverSphereTransform : base.transform).position, this.hoverSphereRadius);
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000A0B12 File Offset: 0x0009ED12
		private void HandDebugLog(string msg)
		{
			if (this.spewDebugText)
			{
				Debug.Log("Hand (" + base.name + "): " + msg);
			}
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000A0B38 File Offset: 0x0009ED38
		private void UpdateHandPoses()
		{
			if (this.controller != null)
			{
				SteamVR instance = SteamVR.instance;
				if (instance != null)
				{
					TrackedDevicePose_t trackedDevicePose_t = default(TrackedDevicePose_t);
					TrackedDevicePose_t trackedDevicePose_t2 = default(TrackedDevicePose_t);
					if (instance.compositor.GetLastPoseForTrackedDeviceIndex(this.controller.index, ref trackedDevicePose_t, ref trackedDevicePose_t2) == EVRCompositorError.None)
					{
						SteamVR_Utils.RigidTransform rigidTransform = new SteamVR_Utils.RigidTransform(trackedDevicePose_t2.mDeviceToAbsoluteTracking);
						base.transform.localPosition = rigidTransform.pos;
						base.transform.localRotation = rigidTransform.rot;
					}
				}
			}
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x000A0BB1 File Offset: 0x0009EDB1
		public void HoverLock(Interactable interactable)
		{
			this.HandDebugLog("HoverLock " + interactable);
			this.hoverLocked = true;
			this.hoveringInteractable = interactable;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000A0BD2 File Offset: 0x0009EDD2
		public void HoverUnlock(Interactable interactable)
		{
			this.HandDebugLog("HoverUnlock " + interactable);
			if (this.hoveringInteractable == interactable)
			{
				this.hoverLocked = false;
			}
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000A0BFA File Offset: 0x0009EDFA
		public bool GetStandardInteractionButtonDown()
		{
			if (this.noSteamVRFallbackCamera)
			{
				return Input.GetMouseButtonDown(0);
			}
			return this.controller != null && this.controller.GetHairTriggerDown();
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000A0C25 File Offset: 0x0009EE25
		public bool GetStandardInteractionButtonUp()
		{
			if (this.noSteamVRFallbackCamera)
			{
				return Input.GetMouseButtonUp(0);
			}
			return this.controller != null && this.controller.GetHairTriggerUp();
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000A0C50 File Offset: 0x0009EE50
		public bool GetStandardInteractionButton()
		{
			if (this.noSteamVRFallbackCamera)
			{
				return Input.GetMouseButton(0);
			}
			return this.controller != null && this.controller.GetHairTrigger();
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000A0C7C File Offset: 0x0009EE7C
		private void InitController(int index)
		{
			if (this.controller == null)
			{
				this.controller = SteamVR_Controller.Input(index);
				this.HandDebugLog(string.Concat(new object[]
				{
					"Hand ",
					base.name,
					" connected with device index ",
					this.controller.index
				}));
				this.controllerObject = Object.Instantiate<GameObject>(this.controllerPrefab);
				this.controllerObject.SetActive(true);
				this.controllerObject.name = this.controllerPrefab.name + "_" + base.name;
				this.controllerObject.layer = base.gameObject.layer;
				this.controllerObject.tag = base.gameObject.tag;
				this.AttachObject(this.controllerObject, Hand.AttachmentFlags.SnapOnAttach | Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.ParentToHand, "");
				this.controller.TriggerHapticPulse(800, EVRButtonId.k_EButton_Axis0);
				this.controllerObject.transform.localScale = this.controllerPrefab.transform.localScale;
				base.BroadcastMessage("OnHandInitialized", index, SendMessageOptions.DontRequireReceiver);
			}
		}

		// Token: 0x04001DF2 RID: 7666
		public const Hand.AttachmentFlags defaultAttachmentFlags = Hand.AttachmentFlags.SnapOnAttach | Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.ParentToHand;

		// Token: 0x04001DF3 RID: 7667
		public Hand otherHand;

		// Token: 0x04001DF4 RID: 7668
		public Hand.HandType startingHandType;

		// Token: 0x04001DF5 RID: 7669
		public Transform hoverSphereTransform;

		// Token: 0x04001DF6 RID: 7670
		public float hoverSphereRadius = 0.05f;

		// Token: 0x04001DF7 RID: 7671
		public LayerMask hoverLayerMask = -1;

		// Token: 0x04001DF8 RID: 7672
		public float hoverUpdateInterval = 0.1f;

		// Token: 0x04001DF9 RID: 7673
		public Camera noSteamVRFallbackCamera;

		// Token: 0x04001DFA RID: 7674
		public float noSteamVRFallbackMaxDistanceNoItem = 10f;

		// Token: 0x04001DFB RID: 7675
		public float noSteamVRFallbackMaxDistanceWithItem = 0.5f;

		// Token: 0x04001DFC RID: 7676
		private float noSteamVRFallbackInteractorDistance = -1f;

		// Token: 0x04001DFD RID: 7677
		public SteamVR_Controller.Device controller;

		// Token: 0x04001DFE RID: 7678
		public GameObject controllerPrefab;

		// Token: 0x04001DFF RID: 7679
		private GameObject controllerObject;

		// Token: 0x04001E00 RID: 7680
		public bool showDebugText;

		// Token: 0x04001E01 RID: 7681
		public bool spewDebugText;

		// Token: 0x04001E02 RID: 7682
		private List<Hand.AttachedObject> attachedObjects = new List<Hand.AttachedObject>();

		// Token: 0x04001E04 RID: 7684
		private Interactable _hoveringInteractable;

		// Token: 0x04001E05 RID: 7685
		private TextMesh debugText;

		// Token: 0x04001E06 RID: 7686
		private int prevOverlappingColliders;

		// Token: 0x04001E07 RID: 7687
		private const int ColliderArraySize = 16;

		// Token: 0x04001E08 RID: 7688
		private Collider[] overlappingColliders;

		// Token: 0x04001E09 RID: 7689
		private Player playerInstance;

		// Token: 0x04001E0A RID: 7690
		private GameObject applicationLostFocusObject;

		// Token: 0x04001E0B RID: 7691
		private SteamVR_Events.Action inputFocusAction;

		// Token: 0x02000772 RID: 1906
		public enum HandType
		{
			// Token: 0x0400290D RID: 10509
			Left,
			// Token: 0x0400290E RID: 10510
			Right,
			// Token: 0x0400290F RID: 10511
			Any
		}

		// Token: 0x02000773 RID: 1907
		[Flags]
		public enum AttachmentFlags
		{
			// Token: 0x04002911 RID: 10513
			SnapOnAttach = 1,
			// Token: 0x04002912 RID: 10514
			DetachOthers = 2,
			// Token: 0x04002913 RID: 10515
			DetachFromOtherHand = 4,
			// Token: 0x04002914 RID: 10516
			ParentToHand = 8
		}

		// Token: 0x02000774 RID: 1908
		public struct AttachedObject
		{
			// Token: 0x04002915 RID: 10517
			public GameObject attachedObject;

			// Token: 0x04002916 RID: 10518
			public GameObject originalParent;

			// Token: 0x04002917 RID: 10519
			public bool isParentedToHand;
		}
	}
}
