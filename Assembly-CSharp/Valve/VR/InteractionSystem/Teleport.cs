using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000451 RID: 1105
	public class Teleport : MonoBehaviour
	{
		// Token: 0x060021F1 RID: 8689 RVA: 0x000A7B85 File Offset: 0x000A5D85
		public static SteamVR_Events.Action<float> ChangeSceneAction(UnityAction<float> action)
		{
			return new SteamVR_Events.Action<float>(Teleport.ChangeScene, action);
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x000A7B92 File Offset: 0x000A5D92
		public static SteamVR_Events.Action<TeleportMarkerBase> PlayerAction(UnityAction<TeleportMarkerBase> action)
		{
			return new SteamVR_Events.Action<TeleportMarkerBase>(Teleport.Player, action);
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000A7B9F File Offset: 0x000A5D9F
		public static SteamVR_Events.Action<TeleportMarkerBase> PlayerPreAction(UnityAction<TeleportMarkerBase> action)
		{
			return new SteamVR_Events.Action<TeleportMarkerBase>(Teleport.PlayerPre, action);
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x000A7BAC File Offset: 0x000A5DAC
		public static Teleport instance
		{
			get
			{
				if (Teleport._instance == null)
				{
					Teleport._instance = Object.FindObjectOfType<Teleport>();
				}
				return Teleport._instance;
			}
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x000A7BCC File Offset: 0x000A5DCC
		private void Awake()
		{
			Teleport._instance = this;
			this.chaperoneInfoInitializedAction = ChaperoneInfo.InitializedAction(new UnityAction(this.OnChaperoneInfoInitialized));
			this.pointerLineRenderer = base.GetComponentInChildren<LineRenderer>();
			this.teleportPointerObject = this.pointerLineRenderer.gameObject;
			int nameID = Shader.PropertyToID("_TintColor");
			this.fullTintAlpha = this.pointVisibleMaterial.GetColor(nameID).a;
			this.teleportArc = base.GetComponent<TeleportArc>();
			this.teleportArc.traceLayerMask = this.traceLayerMask;
			this.loopingAudioMaxVolume = this.loopingAudioSource.volume;
			this.playAreaPreviewCorner.SetActive(false);
			this.playAreaPreviewSide.SetActive(false);
			float x = this.invalidReticleTransform.localScale.x;
			this.invalidReticleMinScale *= x;
			this.invalidReticleMaxScale *= x;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000A7CB0 File Offset: 0x000A5EB0
		private void Start()
		{
			this.teleportMarkers = Object.FindObjectsOfType<TeleportMarkerBase>();
			this.HidePointer();
			this.player = Valve.VR.InteractionSystem.Player.instance;
			if (this.player == null)
			{
				Debug.LogError("Teleport: No Player instance found in map.");
				Object.Destroy(base.gameObject);
				return;
			}
			this.CheckForSpawnPoint();
			base.Invoke("ShowTeleportHint", 5f);
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000A7D13 File Offset: 0x000A5F13
		private void OnEnable()
		{
			this.chaperoneInfoInitializedAction.enabled = true;
			this.OnChaperoneInfoInitialized();
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000A7D27 File Offset: 0x000A5F27
		private void OnDisable()
		{
			this.chaperoneInfoInitializedAction.enabled = false;
			this.HidePointer();
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000A7D3C File Offset: 0x000A5F3C
		private void CheckForSpawnPoint()
		{
			foreach (TeleportMarkerBase teleportMarkerBase in this.teleportMarkers)
			{
				TeleportPoint teleportPoint = teleportMarkerBase as TeleportPoint;
				if (teleportPoint && teleportPoint.playerSpawnPoint)
				{
					this.teleportingToMarker = teleportMarkerBase;
					this.TeleportPlayer();
					return;
				}
			}
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000A7D87 File Offset: 0x000A5F87
		public void HideTeleportPointer()
		{
			if (this.pointerHand != null)
			{
				this.HidePointer();
			}
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000A7DA0 File Offset: 0x000A5FA0
		private void Update()
		{
			Hand oldPointerHand = this.pointerHand;
			Hand hand = null;
			foreach (Hand hand2 in this.player.hands)
			{
				if (this.visible && this.WasTeleportButtonReleased(hand2) && this.pointerHand == hand2)
				{
					this.TryTeleportPlayer();
				}
				if (this.WasTeleportButtonPressed(hand2))
				{
					hand = hand2;
				}
			}
			if (this.allowTeleportWhileAttached && !this.allowTeleportWhileAttached.teleportAllowed)
			{
				this.HidePointer();
			}
			else if (!this.visible && hand != null)
			{
				this.ShowPointer(hand, oldPointerHand);
			}
			else if (this.visible)
			{
				if (hand == null && !this.IsTeleportButtonDown(this.pointerHand))
				{
					this.HidePointer();
				}
				else if (hand != null)
				{
					this.ShowPointer(hand, oldPointerHand);
				}
			}
			if (this.visible)
			{
				this.UpdatePointer();
				if (this.meshFading)
				{
					this.UpdateTeleportColors();
				}
				if (this.onActivateObjectTransform.gameObject.activeSelf && Time.time - this.pointerShowStartTime > this.activateObjectTime)
				{
					this.onActivateObjectTransform.gameObject.SetActive(false);
					return;
				}
			}
			else if (this.onDeactivateObjectTransform.gameObject.activeSelf && Time.time - this.pointerHideStartTime > this.deactivateObjectTime)
			{
				this.onDeactivateObjectTransform.gameObject.SetActive(false);
			}
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000A7F0C File Offset: 0x000A610C
		private void UpdatePointer()
		{
			Vector3 position = this.pointerStartTransform.position;
			Vector3 forward = this.pointerStartTransform.forward;
			bool flag = false;
			bool active = false;
			Vector3 vector = this.player.trackingOriginTransform.position - this.player.feetPositionGuess;
			Vector3 velocity = forward * this.arcDistance;
			TeleportMarkerBase teleportMarkerBase = null;
			float num = Vector3.Dot(forward, Vector3.up);
			float num2 = Vector3.Dot(forward, this.player.hmdTransform.forward);
			bool flag2 = false;
			if ((num2 > 0f && num > 0.75f) || (num2 < 0f && num > 0.5f))
			{
				flag2 = true;
			}
			this.teleportArc.SetArcData(position, velocity, true, flag2);
			RaycastHit raycastHit;
			if (this.teleportArc.DrawArc(out raycastHit))
			{
				flag = true;
				teleportMarkerBase = raycastHit.collider.GetComponentInParent<TeleportMarkerBase>();
			}
			if (flag2)
			{
				teleportMarkerBase = null;
			}
			this.HighlightSelected(teleportMarkerBase);
			Vector3 vector2;
			if (teleportMarkerBase != null)
			{
				if (teleportMarkerBase.locked)
				{
					this.teleportArc.SetColor(this.pointerLockedColor);
					this.pointerLineRenderer.startColor = this.pointerLockedColor;
					this.pointerLineRenderer.endColor = this.pointerLockedColor;
					this.destinationReticleTransform.gameObject.SetActive(false);
				}
				else
				{
					this.teleportArc.SetColor(this.pointerValidColor);
					this.pointerLineRenderer.startColor = this.pointerValidColor;
					this.pointerLineRenderer.endColor = this.pointerValidColor;
					this.destinationReticleTransform.gameObject.SetActive(teleportMarkerBase.showReticle);
				}
				this.offsetReticleTransform.gameObject.SetActive(true);
				this.invalidReticleTransform.gameObject.SetActive(false);
				this.pointedAtTeleportMarker = teleportMarkerBase;
				this.pointedAtPosition = raycastHit.point;
				if (this.showPlayAreaMarker)
				{
					TeleportArea teleportArea = this.pointedAtTeleportMarker as TeleportArea;
					if (teleportArea != null && !teleportArea.locked && this.playAreaPreviewTransform != null)
					{
						Vector3 b = vector;
						if (!this.movedFeetFarEnough)
						{
							float num3 = Vector3.Distance(vector, this.startingFeetOffset);
							if (num3 < 0.1f)
							{
								b = this.startingFeetOffset;
							}
							else if (num3 < 0.4f)
							{
								b = Vector3.Lerp(this.startingFeetOffset, vector, (num3 - 0.1f) / 0.3f);
							}
							else
							{
								this.movedFeetFarEnough = true;
							}
						}
						this.playAreaPreviewTransform.position = this.pointedAtPosition + b;
						active = true;
					}
				}
				vector2 = raycastHit.point;
			}
			else
			{
				this.destinationReticleTransform.gameObject.SetActive(false);
				this.offsetReticleTransform.gameObject.SetActive(false);
				this.teleportArc.SetColor(this.pointerInvalidColor);
				this.pointerLineRenderer.startColor = this.pointerInvalidColor;
				this.pointerLineRenderer.endColor = this.pointerInvalidColor;
				this.invalidReticleTransform.gameObject.SetActive(!flag2);
				Vector3 toDirection = raycastHit.normal;
				if (Vector3.Angle(raycastHit.normal, Vector3.up) < 15f)
				{
					toDirection = Vector3.up;
				}
				this.invalidReticleTargetRotation = Quaternion.FromToRotation(Vector3.up, toDirection);
				this.invalidReticleTransform.rotation = Quaternion.Slerp(this.invalidReticleTransform.rotation, this.invalidReticleTargetRotation, 0.1f);
				float num4 = Util.RemapNumberClamped(Vector3.Distance(raycastHit.point, this.player.hmdTransform.position), this.invalidReticleMinScaleDistance, this.invalidReticleMaxScaleDistance, this.invalidReticleMinScale, this.invalidReticleMaxScale);
				this.invalidReticleScale.x = num4;
				this.invalidReticleScale.y = num4;
				this.invalidReticleScale.z = num4;
				this.invalidReticleTransform.transform.localScale = this.invalidReticleScale;
				this.pointedAtTeleportMarker = null;
				if (flag)
				{
					vector2 = raycastHit.point;
				}
				else
				{
					vector2 = this.teleportArc.GetArcPositionAtTime(this.teleportArc.arcDuration);
				}
				if (this.debugFloor)
				{
					this.floorDebugSphere.gameObject.SetActive(false);
					this.floorDebugLine.gameObject.SetActive(false);
				}
			}
			if (this.playAreaPreviewTransform != null)
			{
				this.playAreaPreviewTransform.gameObject.SetActive(active);
			}
			if (!this.showOffsetReticle)
			{
				this.offsetReticleTransform.gameObject.SetActive(false);
			}
			this.destinationReticleTransform.position = this.pointedAtPosition;
			this.invalidReticleTransform.position = vector2;
			this.onActivateObjectTransform.position = vector2;
			this.onDeactivateObjectTransform.position = vector2;
			this.offsetReticleTransform.position = vector2 - vector;
			this.reticleAudioSource.transform.position = this.pointedAtPosition;
			this.pointerLineRenderer.SetPosition(0, position);
			this.pointerLineRenderer.SetPosition(1, vector2);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000A83DC File Offset: 0x000A65DC
		private void FixedUpdate()
		{
			if (!this.visible)
			{
				return;
			}
			if (this.debugFloor && this.pointedAtTeleportMarker as TeleportArea != null && this.floorFixupMaximumTraceDistance > 0f)
			{
				this.floorDebugSphere.gameObject.SetActive(true);
				this.floorDebugLine.gameObject.SetActive(true);
				Vector3 down = Vector3.down;
				down.x = 0.01f;
				RaycastHit raycastHit;
				if (Physics.Raycast(this.pointedAtPosition + 0.05f * down, down, out raycastHit, this.floorFixupMaximumTraceDistance, this.floorFixupTraceLayerMask))
				{
					this.floorDebugSphere.transform.position = raycastHit.point;
					this.floorDebugSphere.material.color = Color.green;
					this.floorDebugLine.startColor = Color.green;
					this.floorDebugLine.endColor = Color.green;
					this.floorDebugLine.SetPosition(0, this.pointedAtPosition);
					this.floorDebugLine.SetPosition(1, raycastHit.point);
					return;
				}
				Vector3 position = this.pointedAtPosition + down * this.floorFixupMaximumTraceDistance;
				this.floorDebugSphere.transform.position = position;
				this.floorDebugSphere.material.color = Color.red;
				this.floorDebugLine.startColor = Color.red;
				this.floorDebugLine.endColor = Color.red;
				this.floorDebugLine.SetPosition(0, this.pointedAtPosition);
				this.floorDebugLine.SetPosition(1, position);
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x000A8578 File Offset: 0x000A6778
		private void OnChaperoneInfoInitialized()
		{
			ChaperoneInfo instance = ChaperoneInfo.instance;
			if (instance.initialized && instance.roomscale)
			{
				if (this.playAreaPreviewTransform == null)
				{
					this.playAreaPreviewTransform = new GameObject("PlayAreaPreviewTransform").transform;
					this.playAreaPreviewTransform.parent = base.transform;
					Util.ResetTransform(this.playAreaPreviewTransform, true);
					this.playAreaPreviewCorner.SetActive(true);
					this.playAreaPreviewCorners = new Transform[4];
					this.playAreaPreviewCorners[0] = this.playAreaPreviewCorner.transform;
					this.playAreaPreviewCorners[1] = Object.Instantiate<Transform>(this.playAreaPreviewCorners[0]);
					this.playAreaPreviewCorners[2] = Object.Instantiate<Transform>(this.playAreaPreviewCorners[0]);
					this.playAreaPreviewCorners[3] = Object.Instantiate<Transform>(this.playAreaPreviewCorners[0]);
					this.playAreaPreviewCorners[0].transform.parent = this.playAreaPreviewTransform;
					this.playAreaPreviewCorners[1].transform.parent = this.playAreaPreviewTransform;
					this.playAreaPreviewCorners[2].transform.parent = this.playAreaPreviewTransform;
					this.playAreaPreviewCorners[3].transform.parent = this.playAreaPreviewTransform;
					this.playAreaPreviewSide.SetActive(true);
					this.playAreaPreviewSides = new Transform[4];
					this.playAreaPreviewSides[0] = this.playAreaPreviewSide.transform;
					this.playAreaPreviewSides[1] = Object.Instantiate<Transform>(this.playAreaPreviewSides[0]);
					this.playAreaPreviewSides[2] = Object.Instantiate<Transform>(this.playAreaPreviewSides[0]);
					this.playAreaPreviewSides[3] = Object.Instantiate<Transform>(this.playAreaPreviewSides[0]);
					this.playAreaPreviewSides[0].transform.parent = this.playAreaPreviewTransform;
					this.playAreaPreviewSides[1].transform.parent = this.playAreaPreviewTransform;
					this.playAreaPreviewSides[2].transform.parent = this.playAreaPreviewTransform;
					this.playAreaPreviewSides[3].transform.parent = this.playAreaPreviewTransform;
				}
				float playAreaSizeX = instance.playAreaSizeX;
				float playAreaSizeZ = instance.playAreaSizeZ;
				this.playAreaPreviewSides[0].localPosition = new Vector3(0f, 0f, 0.5f * playAreaSizeZ - 0.25f);
				this.playAreaPreviewSides[1].localPosition = new Vector3(0f, 0f, -0.5f * playAreaSizeZ + 0.25f);
				this.playAreaPreviewSides[2].localPosition = new Vector3(0.5f * playAreaSizeX - 0.25f, 0f, 0f);
				this.playAreaPreviewSides[3].localPosition = new Vector3(-0.5f * playAreaSizeX + 0.25f, 0f, 0f);
				this.playAreaPreviewSides[0].localScale = new Vector3(playAreaSizeX - 0.5f, 1f, 1f);
				this.playAreaPreviewSides[1].localScale = new Vector3(playAreaSizeX - 0.5f, 1f, 1f);
				this.playAreaPreviewSides[2].localScale = new Vector3(playAreaSizeZ - 0.5f, 1f, 1f);
				this.playAreaPreviewSides[3].localScale = new Vector3(playAreaSizeZ - 0.5f, 1f, 1f);
				this.playAreaPreviewSides[0].localRotation = Quaternion.Euler(0f, 0f, 0f);
				this.playAreaPreviewSides[1].localRotation = Quaternion.Euler(0f, 180f, 0f);
				this.playAreaPreviewSides[2].localRotation = Quaternion.Euler(0f, 90f, 0f);
				this.playAreaPreviewSides[3].localRotation = Quaternion.Euler(0f, 270f, 0f);
				this.playAreaPreviewCorners[0].localPosition = new Vector3(0.5f * playAreaSizeX - 0.25f, 0f, 0.5f * playAreaSizeZ - 0.25f);
				this.playAreaPreviewCorners[1].localPosition = new Vector3(0.5f * playAreaSizeX - 0.25f, 0f, -0.5f * playAreaSizeZ + 0.25f);
				this.playAreaPreviewCorners[2].localPosition = new Vector3(-0.5f * playAreaSizeX + 0.25f, 0f, -0.5f * playAreaSizeZ + 0.25f);
				this.playAreaPreviewCorners[3].localPosition = new Vector3(-0.5f * playAreaSizeX + 0.25f, 0f, 0.5f * playAreaSizeZ - 0.25f);
				this.playAreaPreviewCorners[0].localRotation = Quaternion.Euler(0f, 0f, 0f);
				this.playAreaPreviewCorners[1].localRotation = Quaternion.Euler(0f, 90f, 0f);
				this.playAreaPreviewCorners[2].localRotation = Quaternion.Euler(0f, 180f, 0f);
				this.playAreaPreviewCorners[3].localRotation = Quaternion.Euler(0f, 270f, 0f);
				this.playAreaPreviewTransform.gameObject.SetActive(false);
			}
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000A8A94 File Offset: 0x000A6C94
		private void HidePointer()
		{
			if (this.visible)
			{
				this.pointerHideStartTime = Time.time;
			}
			this.visible = false;
			if (this.pointerHand)
			{
				if (this.ShouldOverrideHoverLock())
				{
					if (this.originalHoverLockState)
					{
						this.pointerHand.HoverLock(this.originalHoveringInteractable);
					}
					else
					{
						this.pointerHand.HoverUnlock(null);
					}
				}
				this.loopingAudioSource.Stop();
				this.PlayAudioClip(this.pointerAudioSource, this.pointerStopSound);
			}
			this.teleportPointerObject.SetActive(false);
			this.teleportArc.Hide();
			foreach (TeleportMarkerBase teleportMarkerBase in this.teleportMarkers)
			{
				if (teleportMarkerBase != null && teleportMarkerBase.markerActive && teleportMarkerBase.gameObject != null)
				{
					teleportMarkerBase.gameObject.SetActive(false);
				}
			}
			this.destinationReticleTransform.gameObject.SetActive(false);
			this.invalidReticleTransform.gameObject.SetActive(false);
			this.offsetReticleTransform.gameObject.SetActive(false);
			if (this.playAreaPreviewTransform != null)
			{
				this.playAreaPreviewTransform.gameObject.SetActive(false);
			}
			if (this.onActivateObjectTransform.gameObject.activeSelf)
			{
				this.onActivateObjectTransform.gameObject.SetActive(false);
			}
			this.onDeactivateObjectTransform.gameObject.SetActive(true);
			this.pointerHand = null;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000A8BFC File Offset: 0x000A6DFC
		private void ShowPointer(Hand newPointerHand, Hand oldPointerHand)
		{
			if (!this.visible)
			{
				this.pointedAtTeleportMarker = null;
				this.pointerShowStartTime = Time.time;
				this.visible = true;
				this.meshFading = true;
				this.teleportPointerObject.SetActive(false);
				this.teleportArc.Show();
				foreach (TeleportMarkerBase teleportMarkerBase in this.teleportMarkers)
				{
					if (teleportMarkerBase.markerActive && teleportMarkerBase.ShouldActivate(this.player.feetPositionGuess))
					{
						teleportMarkerBase.gameObject.SetActive(true);
						teleportMarkerBase.Highlight(false);
					}
				}
				this.startingFeetOffset = this.player.trackingOriginTransform.position - this.player.feetPositionGuess;
				this.movedFeetFarEnough = false;
				if (this.onDeactivateObjectTransform.gameObject.activeSelf)
				{
					this.onDeactivateObjectTransform.gameObject.SetActive(false);
				}
				this.onActivateObjectTransform.gameObject.SetActive(true);
				this.loopingAudioSource.clip = this.pointerLoopSound;
				this.loopingAudioSource.loop = true;
				this.loopingAudioSource.Play();
				this.loopingAudioSource.volume = 0f;
			}
			if (oldPointerHand && this.ShouldOverrideHoverLock())
			{
				if (this.originalHoverLockState)
				{
					oldPointerHand.HoverLock(this.originalHoveringInteractable);
				}
				else
				{
					oldPointerHand.HoverUnlock(null);
				}
			}
			this.pointerHand = newPointerHand;
			if (this.visible && oldPointerHand != this.pointerHand)
			{
				this.PlayAudioClip(this.pointerAudioSource, this.pointerStartSound);
			}
			if (this.pointerHand)
			{
				this.pointerStartTransform = this.GetPointerStartTransform(this.pointerHand);
				if (this.pointerHand.currentAttachedObject != null)
				{
					this.allowTeleportWhileAttached = this.pointerHand.currentAttachedObject.GetComponent<AllowTeleportWhileAttachedToHand>();
				}
				this.originalHoverLockState = this.pointerHand.hoverLocked;
				this.originalHoveringInteractable = this.pointerHand.hoveringInteractable;
				if (this.ShouldOverrideHoverLock())
				{
					this.pointerHand.HoverLock(null);
				}
				this.pointerAudioSource.transform.SetParent(this.pointerStartTransform);
				this.pointerAudioSource.transform.localPosition = Vector3.zero;
				this.loopingAudioSource.transform.SetParent(this.pointerStartTransform);
				this.loopingAudioSource.transform.localPosition = Vector3.zero;
			}
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x000A8E60 File Offset: 0x000A7060
		private void UpdateTeleportColors()
		{
			float num = Time.time - this.pointerShowStartTime;
			if (num > this.meshFadeTime)
			{
				this.meshAlphaPercent = 1f;
				this.meshFading = false;
			}
			else
			{
				this.meshAlphaPercent = Mathf.Lerp(0f, 1f, num / this.meshFadeTime);
			}
			TeleportMarkerBase[] array = this.teleportMarkers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetAlpha(this.fullTintAlpha * this.meshAlphaPercent, this.meshAlphaPercent);
			}
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x000A8EE4 File Offset: 0x000A70E4
		private void PlayAudioClip(AudioSource source, AudioClip clip)
		{
			source.clip = clip;
			source.Play();
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000A8EF3 File Offset: 0x000A70F3
		private void PlayPointerHaptic(bool validLocation)
		{
			if (this.pointerHand.controller != null)
			{
				if (validLocation)
				{
					this.pointerHand.controller.TriggerHapticPulse(800, EVRButtonId.k_EButton_Axis0);
					return;
				}
				this.pointerHand.controller.TriggerHapticPulse(100, EVRButtonId.k_EButton_Axis0);
			}
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000A8F34 File Offset: 0x000A7134
		private void TryTeleportPlayer()
		{
			if (this.visible && !this.teleporting && this.pointedAtTeleportMarker != null && !this.pointedAtTeleportMarker.locked)
			{
				this.teleportingToMarker = this.pointedAtTeleportMarker;
				this.InitiateTeleportFade();
				this.CancelTeleportHint();
			}
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000A8F84 File Offset: 0x000A7184
		private void InitiateTeleportFade()
		{
			this.teleporting = true;
			this.currentFadeTime = this.teleportFadeTime;
			TeleportPoint teleportPoint = this.teleportingToMarker as TeleportPoint;
			if (teleportPoint != null && teleportPoint.teleportType == TeleportPoint.TeleportPointType.SwitchToNewScene)
			{
				this.currentFadeTime *= 3f;
				Teleport.ChangeScene.Send(this.currentFadeTime);
			}
			SteamVR_Fade.Start(Color.clear, 0f, false);
			SteamVR_Fade.Start(Color.black, this.currentFadeTime, false);
			this.headAudioSource.transform.SetParent(this.player.hmdTransform);
			this.headAudioSource.transform.localPosition = Vector3.zero;
			this.PlayAudioClip(this.headAudioSource, this.teleportSound);
			base.Invoke("TeleportPlayer", this.currentFadeTime);
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000A9058 File Offset: 0x000A7258
		private void TeleportPlayer()
		{
			this.teleporting = false;
			Teleport.PlayerPre.Send(this.pointedAtTeleportMarker);
			SteamVR_Fade.Start(Color.clear, this.currentFadeTime, false);
			TeleportPoint teleportPoint = this.teleportingToMarker as TeleportPoint;
			Vector3 a = this.pointedAtPosition;
			if (teleportPoint != null)
			{
				a = teleportPoint.transform.position;
				if (teleportPoint.teleportType == TeleportPoint.TeleportPointType.SwitchToNewScene)
				{
					teleportPoint.TeleportToScene();
					return;
				}
			}
			RaycastHit raycastHit;
			if (this.teleportingToMarker as TeleportArea != null && this.floorFixupMaximumTraceDistance > 0f && Physics.Raycast(a + 0.05f * Vector3.down, Vector3.down, out raycastHit, this.floorFixupMaximumTraceDistance, this.floorFixupTraceLayerMask))
			{
				a = raycastHit.point;
			}
			if (this.teleportingToMarker.ShouldMovePlayer())
			{
				Vector3 b = this.player.trackingOriginTransform.position - this.player.feetPositionGuess;
				this.player.trackingOriginTransform.position = a + b;
			}
			else
			{
				this.teleportingToMarker.TeleportPlayer(this.pointedAtPosition);
			}
			Teleport.Player.Send(this.pointedAtTeleportMarker);
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000A918C File Offset: 0x000A738C
		private void HighlightSelected(TeleportMarkerBase hitTeleportMarker)
		{
			if (this.pointedAtTeleportMarker != hitTeleportMarker)
			{
				if (this.pointedAtTeleportMarker != null)
				{
					this.pointedAtTeleportMarker.Highlight(false);
				}
				if (hitTeleportMarker != null)
				{
					hitTeleportMarker.Highlight(true);
					this.prevPointedAtPosition = this.pointedAtPosition;
					this.PlayPointerHaptic(!hitTeleportMarker.locked);
					this.PlayAudioClip(this.reticleAudioSource, this.goodHighlightSound);
					this.loopingAudioSource.volume = this.loopingAudioMaxVolume;
					return;
				}
				if (this.pointedAtTeleportMarker != null)
				{
					this.PlayAudioClip(this.reticleAudioSource, this.badHighlightSound);
					this.loopingAudioSource.volume = 0f;
					return;
				}
			}
			else if (hitTeleportMarker != null && Vector3.Distance(this.prevPointedAtPosition, this.pointedAtPosition) > 1f)
			{
				this.prevPointedAtPosition = this.pointedAtPosition;
				this.PlayPointerHaptic(!hitTeleportMarker.locked);
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000A9280 File Offset: 0x000A7480
		public void ShowTeleportHint()
		{
			this.CancelTeleportHint();
			this.hintCoroutine = base.StartCoroutine(this.TeleportHintCoroutine());
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000A929C File Offset: 0x000A749C
		public void CancelTeleportHint()
		{
			if (this.hintCoroutine != null)
			{
				Hand[] hands = this.player.hands;
				for (int i = 0; i < hands.Length; i++)
				{
					ControllerButtonHints.HideTextHint(hands[i], EVRButtonId.k_EButton_Axis0);
				}
				base.StopCoroutine(this.hintCoroutine);
				this.hintCoroutine = null;
			}
			base.CancelInvoke("ShowTeleportHint");
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000A92F3 File Offset: 0x000A74F3
		private IEnumerator TeleportHintCoroutine()
		{
			float prevBreakTime = Time.time;
			float prevHapticPulseTime = Time.time;
			for (;;)
			{
				bool pulsed = false;
				foreach (Hand hand in this.player.hands)
				{
					bool flag = this.IsEligibleForTeleport(hand);
					bool flag2 = !string.IsNullOrEmpty(ControllerButtonHints.GetActiveHintText(hand, EVRButtonId.k_EButton_Axis0));
					if (flag)
					{
						if (!flag2)
						{
							ControllerButtonHints.ShowTextHint(hand, EVRButtonId.k_EButton_Axis0, "Teleport", true);
							prevBreakTime = Time.time;
							prevHapticPulseTime = Time.time;
						}
						if (Time.time > prevHapticPulseTime + 0.05f)
						{
							pulsed = true;
							hand.controller.TriggerHapticPulse(500, EVRButtonId.k_EButton_Axis0);
						}
					}
					else if (!flag && flag2)
					{
						ControllerButtonHints.HideTextHint(hand, EVRButtonId.k_EButton_Axis0);
					}
				}
				if (Time.time > prevBreakTime + 3f)
				{
					yield return new WaitForSeconds(3f);
					prevBreakTime = Time.time;
				}
				if (pulsed)
				{
					prevHapticPulseTime = Time.time;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000A9304 File Offset: 0x000A7504
		public bool IsEligibleForTeleport(Hand hand)
		{
			if (hand == null)
			{
				return false;
			}
			if (!hand.gameObject.activeInHierarchy)
			{
				return false;
			}
			if (hand.hoveringInteractable != null)
			{
				return false;
			}
			if (hand.noSteamVRFallbackCamera == null)
			{
				if (hand.controller == null)
				{
					return false;
				}
				if (hand.currentAttachedObject != null)
				{
					AllowTeleportWhileAttachedToHand component = hand.currentAttachedObject.GetComponent<AllowTeleportWhileAttachedToHand>();
					return component != null && component.teleportAllowed;
				}
			}
			return true;
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000A9383 File Offset: 0x000A7583
		private bool ShouldOverrideHoverLock()
		{
			return !this.allowTeleportWhileAttached || this.allowTeleportWhileAttached.overrideHoverLock;
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000A93A2 File Offset: 0x000A75A2
		private bool WasTeleportButtonReleased(Hand hand)
		{
			if (!this.IsEligibleForTeleport(hand))
			{
				return false;
			}
			if (hand.noSteamVRFallbackCamera != null)
			{
				return Input.GetKeyUp(KeyCode.T);
			}
			return hand.controller.GetPressUp(4294967296UL);
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000A93D9 File Offset: 0x000A75D9
		private bool IsTeleportButtonDown(Hand hand)
		{
			if (!this.IsEligibleForTeleport(hand))
			{
				return false;
			}
			if (hand.noSteamVRFallbackCamera != null)
			{
				return Input.GetKey(KeyCode.T);
			}
			return hand.controller.GetPress(4294967296UL);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000A9410 File Offset: 0x000A7610
		private bool WasTeleportButtonPressed(Hand hand)
		{
			if (!this.IsEligibleForTeleport(hand))
			{
				return false;
			}
			if (hand.noSteamVRFallbackCamera != null)
			{
				return Input.GetKeyDown(KeyCode.T);
			}
			return hand.controller.GetPressDown(4294967296UL);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000A9447 File Offset: 0x000A7647
		private Transform GetPointerStartTransform(Hand hand)
		{
			if (hand.noSteamVRFallbackCamera != null)
			{
				return hand.noSteamVRFallbackCamera.transform;
			}
			return this.pointerHand.GetAttachmentTransform("Attach_ControllerTip");
		}

		// Token: 0x04001F69 RID: 8041
		public LayerMask traceLayerMask;

		// Token: 0x04001F6A RID: 8042
		public LayerMask floorFixupTraceLayerMask;

		// Token: 0x04001F6B RID: 8043
		public float floorFixupMaximumTraceDistance = 1f;

		// Token: 0x04001F6C RID: 8044
		public Material areaVisibleMaterial;

		// Token: 0x04001F6D RID: 8045
		public Material areaLockedMaterial;

		// Token: 0x04001F6E RID: 8046
		public Material areaHighlightedMaterial;

		// Token: 0x04001F6F RID: 8047
		public Material pointVisibleMaterial;

		// Token: 0x04001F70 RID: 8048
		public Material pointLockedMaterial;

		// Token: 0x04001F71 RID: 8049
		public Material pointHighlightedMaterial;

		// Token: 0x04001F72 RID: 8050
		public Transform destinationReticleTransform;

		// Token: 0x04001F73 RID: 8051
		public Transform invalidReticleTransform;

		// Token: 0x04001F74 RID: 8052
		public GameObject playAreaPreviewCorner;

		// Token: 0x04001F75 RID: 8053
		public GameObject playAreaPreviewSide;

		// Token: 0x04001F76 RID: 8054
		public Color pointerValidColor;

		// Token: 0x04001F77 RID: 8055
		public Color pointerInvalidColor;

		// Token: 0x04001F78 RID: 8056
		public Color pointerLockedColor;

		// Token: 0x04001F79 RID: 8057
		public bool showPlayAreaMarker = true;

		// Token: 0x04001F7A RID: 8058
		public float teleportFadeTime = 0.1f;

		// Token: 0x04001F7B RID: 8059
		public float meshFadeTime = 0.2f;

		// Token: 0x04001F7C RID: 8060
		public float arcDistance = 10f;

		// Token: 0x04001F7D RID: 8061
		[Header("Effects")]
		public Transform onActivateObjectTransform;

		// Token: 0x04001F7E RID: 8062
		public Transform onDeactivateObjectTransform;

		// Token: 0x04001F7F RID: 8063
		public float activateObjectTime = 1f;

		// Token: 0x04001F80 RID: 8064
		public float deactivateObjectTime = 1f;

		// Token: 0x04001F81 RID: 8065
		[Header("Audio Sources")]
		public AudioSource pointerAudioSource;

		// Token: 0x04001F82 RID: 8066
		public AudioSource loopingAudioSource;

		// Token: 0x04001F83 RID: 8067
		public AudioSource headAudioSource;

		// Token: 0x04001F84 RID: 8068
		public AudioSource reticleAudioSource;

		// Token: 0x04001F85 RID: 8069
		[Header("Sounds")]
		public AudioClip teleportSound;

		// Token: 0x04001F86 RID: 8070
		public AudioClip pointerStartSound;

		// Token: 0x04001F87 RID: 8071
		public AudioClip pointerLoopSound;

		// Token: 0x04001F88 RID: 8072
		public AudioClip pointerStopSound;

		// Token: 0x04001F89 RID: 8073
		public AudioClip goodHighlightSound;

		// Token: 0x04001F8A RID: 8074
		public AudioClip badHighlightSound;

		// Token: 0x04001F8B RID: 8075
		[Header("Debug")]
		public bool debugFloor;

		// Token: 0x04001F8C RID: 8076
		public bool showOffsetReticle;

		// Token: 0x04001F8D RID: 8077
		public Transform offsetReticleTransform;

		// Token: 0x04001F8E RID: 8078
		public MeshRenderer floorDebugSphere;

		// Token: 0x04001F8F RID: 8079
		public LineRenderer floorDebugLine;

		// Token: 0x04001F90 RID: 8080
		private LineRenderer pointerLineRenderer;

		// Token: 0x04001F91 RID: 8081
		private GameObject teleportPointerObject;

		// Token: 0x04001F92 RID: 8082
		private Transform pointerStartTransform;

		// Token: 0x04001F93 RID: 8083
		private Hand pointerHand;

		// Token: 0x04001F94 RID: 8084
		private Player player;

		// Token: 0x04001F95 RID: 8085
		private TeleportArc teleportArc;

		// Token: 0x04001F96 RID: 8086
		private bool visible;

		// Token: 0x04001F97 RID: 8087
		private TeleportMarkerBase[] teleportMarkers;

		// Token: 0x04001F98 RID: 8088
		private TeleportMarkerBase pointedAtTeleportMarker;

		// Token: 0x04001F99 RID: 8089
		private TeleportMarkerBase teleportingToMarker;

		// Token: 0x04001F9A RID: 8090
		private Vector3 pointedAtPosition;

		// Token: 0x04001F9B RID: 8091
		private Vector3 prevPointedAtPosition;

		// Token: 0x04001F9C RID: 8092
		private bool teleporting;

		// Token: 0x04001F9D RID: 8093
		private float currentFadeTime;

		// Token: 0x04001F9E RID: 8094
		private float meshAlphaPercent = 1f;

		// Token: 0x04001F9F RID: 8095
		private float pointerShowStartTime;

		// Token: 0x04001FA0 RID: 8096
		private float pointerHideStartTime;

		// Token: 0x04001FA1 RID: 8097
		private bool meshFading;

		// Token: 0x04001FA2 RID: 8098
		private float fullTintAlpha;

		// Token: 0x04001FA3 RID: 8099
		private float invalidReticleMinScale = 0.2f;

		// Token: 0x04001FA4 RID: 8100
		private float invalidReticleMaxScale = 1f;

		// Token: 0x04001FA5 RID: 8101
		private float invalidReticleMinScaleDistance = 0.4f;

		// Token: 0x04001FA6 RID: 8102
		private float invalidReticleMaxScaleDistance = 2f;

		// Token: 0x04001FA7 RID: 8103
		private Vector3 invalidReticleScale = Vector3.one;

		// Token: 0x04001FA8 RID: 8104
		private Quaternion invalidReticleTargetRotation = Quaternion.identity;

		// Token: 0x04001FA9 RID: 8105
		private Transform playAreaPreviewTransform;

		// Token: 0x04001FAA RID: 8106
		private Transform[] playAreaPreviewCorners;

		// Token: 0x04001FAB RID: 8107
		private Transform[] playAreaPreviewSides;

		// Token: 0x04001FAC RID: 8108
		private float loopingAudioMaxVolume;

		// Token: 0x04001FAD RID: 8109
		private Coroutine hintCoroutine;

		// Token: 0x04001FAE RID: 8110
		private bool originalHoverLockState;

		// Token: 0x04001FAF RID: 8111
		private Interactable originalHoveringInteractable;

		// Token: 0x04001FB0 RID: 8112
		private AllowTeleportWhileAttachedToHand allowTeleportWhileAttached;

		// Token: 0x04001FB1 RID: 8113
		private Vector3 startingFeetOffset = Vector3.zero;

		// Token: 0x04001FB2 RID: 8114
		private bool movedFeetFarEnough;

		// Token: 0x04001FB3 RID: 8115
		private SteamVR_Events.Action chaperoneInfoInitializedAction;

		// Token: 0x04001FB4 RID: 8116
		public static SteamVR_Events.Event<float> ChangeScene = new SteamVR_Events.Event<float>();

		// Token: 0x04001FB5 RID: 8117
		public static SteamVR_Events.Event<TeleportMarkerBase> Player = new SteamVR_Events.Event<TeleportMarkerBase>();

		// Token: 0x04001FB6 RID: 8118
		public static SteamVR_Events.Event<TeleportMarkerBase> PlayerPre = new SteamVR_Events.Event<TeleportMarkerBase>();

		// Token: 0x04001FB7 RID: 8119
		private static Teleport _instance;
	}
}
