using System;
using System.Collections;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002C9 RID: 713
	[AddComponentMenu("VRTK/Scripts/Locomotion/VRTK_BasicTeleport")]
	public class VRTK_BasicTeleport : MonoBehaviour
	{
		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06001794 RID: 6036 RVA: 0x0007E364 File Offset: 0x0007C564
		// (remove) Token: 0x06001795 RID: 6037 RVA: 0x0007E39C File Offset: 0x0007C59C
		public event TeleportEventHandler Teleporting;

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x06001796 RID: 6038 RVA: 0x0007E3D4 File Offset: 0x0007C5D4
		// (remove) Token: 0x06001797 RID: 6039 RVA: 0x0007E40C File Offset: 0x0007C60C
		public event TeleportEventHandler Teleported;

		// Token: 0x06001798 RID: 6040 RVA: 0x0007E444 File Offset: 0x0007C644
		public virtual void InitDestinationSetListener(GameObject markerMaker, bool register)
		{
			if (markerMaker != null)
			{
				foreach (VRTK_DestinationMarker vrtk_DestinationMarker in markerMaker.GetComponentsInChildren<VRTK_DestinationMarker>())
				{
					if (register)
					{
						vrtk_DestinationMarker.DestinationMarkerSet += this.DoTeleport;
						if (vrtk_DestinationMarker.targetListPolicy == null)
						{
							vrtk_DestinationMarker.targetListPolicy = this.targetListPolicy;
						}
						vrtk_DestinationMarker.SetHeadsetPositionCompensation(this.headsetPositionCompensation);
					}
					else
					{
						vrtk_DestinationMarker.DestinationMarkerSet -= this.DoTeleport;
					}
				}
			}
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0007E4C4 File Offset: 0x0007C6C4
		public virtual void ToggleTeleportEnabled(bool state)
		{
			this.enableTeleport = state;
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0007E4D0 File Offset: 0x0007C6D0
		public virtual bool ValidLocation(Transform target, Vector3 destinationPosition)
		{
			return !VRTK_PlayerObject.IsPlayerObject(target.gameObject, VRTK_PlayerObject.ObjectTypes.Null) && !target.GetComponent<VRTK_UIGraphicRaycaster>() && !Physics.Linecast(this.player.headObject.transform.position, destinationPosition + new Vector3(0f, 0.2f, 0f), this.player.mainLayerMask, QueryTriggerInteraction.Ignore) && target != null && !VRTK_PolicyList.Check(target.gameObject, this.targetListPolicy);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0007E562 File Offset: 0x0007C762
		public virtual void Teleport(DestinationMarkerEventArgs teleportArgs)
		{
			this.DoTeleport(this, teleportArgs);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0007E56C File Offset: 0x0007C76C
		public virtual void Teleport(Transform target, Vector3 destinationPosition, Quaternion? destinationRotation = null, bool forceDestinationPosition = false)
		{
			Debug.Log("Teleport: " + target.gameObject.name);
			DestinationMarkerEventArgs teleportArgs = this.BuildTeleportArgs(target, destinationPosition, destinationRotation, forceDestinationPosition);
			this.Teleport(teleportArgs);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0007E5A8 File Offset: 0x0007C7A8
		public virtual void ForceTeleport(Vector3 destinationPosition, Quaternion? destinationRotation = null)
		{
			DestinationMarkerEventArgs e = this.BuildTeleportArgs(null, destinationPosition, destinationRotation, false);
			this.StartTeleport(this, e);
			this.CalculateBlinkDelay(this.blinkTransitionSpeed, destinationPosition);
			this.Blink(this.blinkTransitionSpeed);
			if (this.ValidRigObjects())
			{
				this.playArea.position = destinationPosition;
			}
			Quaternion updatedRotation = this.SetNewRotation(destinationRotation);
			this.ProcessOrientation(this, e, destinationPosition, updatedRotation);
			this.EndTeleport(this, e);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0007E610 File Offset: 0x0007C810
		protected virtual void OnEnable()
		{
			VRTK_PlayerObject.SetPlayerObject(base.gameObject, VRTK_PlayerObject.ObjectTypes.CameraRig);
			this.headset = VRTK_SharedMethods.AddCameraFade();
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			this.adjustYForTerrain = false;
			this.enableTeleport = true;
			this.initaliseListeners = base.StartCoroutine(this.InitListenersAtEndOfFrame());
			VRTK_ObjectCache.registeredTeleporters.Add(this);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0007E66A File Offset: 0x0007C86A
		protected virtual void OnDisable()
		{
			if (this.initaliseListeners != null)
			{
				base.StopCoroutine(this.initaliseListeners);
			}
			this.InitDestinationMarkerListeners(false);
			VRTK_ObjectCache.registeredTeleporters.Remove(this);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0007E694 File Offset: 0x0007C894
		protected virtual void Blink(float transitionSpeed)
		{
			base.StartCoroutine(this.PreventTeleport(transitionSpeed));
			this.fadeInTime = transitionSpeed;
			if (transitionSpeed > 0f)
			{
				VRTK_SDK_Bridge.HeadsetFade(this.blinkToColor, 0f, false);
			}
			base.Invoke("ReleaseBlink", this.blinkPause);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0007E6E0 File Offset: 0x0007C8E0
		protected virtual DestinationMarkerEventArgs BuildTeleportArgs(Transform target, Vector3 destinationPosition, Quaternion? destinationRotation = null, bool forceDestinationPosition = false)
		{
			return new DestinationMarkerEventArgs
			{
				distance = (this.ValidRigObjects() ? Vector3.Distance(new Vector3(this.headset.position.x, this.playArea.position.y, this.headset.position.z), destinationPosition) : 0f),
				target = target,
				raycastHit = default(RaycastHit),
				destinationPosition = destinationPosition,
				destinationRotation = destinationRotation,
				forceDestinationPosition = forceDestinationPosition,
				enableTeleport = true
			};
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0007E780 File Offset: 0x0007C980
		protected virtual bool ValidRigObjects()
		{
			if (this.headset == null)
			{
				VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_SCENE, new object[]
				{
					"VRTK_BasicTeleport",
					"rig headset",
					". Are you trying to access the headset before the SDK Manager has initialised it?"
				}));
				return false;
			}
			if (this.playArea == null)
			{
				VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_SCENE, new object[]
				{
					"VRTK_BasicTeleport",
					"rig boundaries",
					". Are you trying to access the boundaries before the SDK Manager has initialised it?"
				}));
				return false;
			}
			return true;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0007E800 File Offset: 0x0007CA00
		protected virtual void DoTeleport(object sender, DestinationMarkerEventArgs e)
		{
			if (!this.canTeleport)
			{
				return;
			}
			if (this.enableTeleport && this.ValidLocation(e.target, e.destinationPosition) && e.enableTeleport)
			{
				this.StartTeleport(sender, e);
				Vector3 newPosition = this.GetNewPosition(e.destinationPosition, e.target, e.forceDestinationPosition);
				this.CalculateBlinkDelay(this.blinkTransitionSpeed, newPosition);
				this.Blink(this.blinkTransitionSpeed);
				Vector3 updatedPosition = this.SetNewPosition(newPosition, e.target, e.forceDestinationPosition);
				Quaternion updatedRotation = this.SetNewRotation(e.destinationRotation);
				this.ProcessOrientation(sender, e, updatedPosition, updatedRotation);
				this.EndTeleport(sender, e);
			}
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0007E8AA File Offset: 0x0007CAAA
		protected virtual void StartTeleport(object sender, DestinationMarkerEventArgs e)
		{
			this.OnTeleporting(sender, e);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00003F60 File Offset: 0x00002160
		protected virtual void ProcessOrientation(object sender, DestinationMarkerEventArgs e, Vector3 updatedPosition, Quaternion updatedRotation)
		{
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0007E8B4 File Offset: 0x0007CAB4
		protected virtual void EndTeleport(object sender, DestinationMarkerEventArgs e)
		{
			this.OnTeleported(sender, e);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0007E8BE File Offset: 0x0007CABE
		protected virtual Vector3 SetNewPosition(Vector3 position, Transform target, bool forceDestinationPosition)
		{
			if (this.ValidRigObjects())
			{
				this.playArea.position = this.CheckTerrainCollision(position, target, forceDestinationPosition);
				return this.playArea.position;
			}
			return Vector3.zero;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0007E8ED File Offset: 0x0007CAED
		protected virtual Quaternion SetNewRotation(Quaternion? rotation)
		{
			if (this.ValidRigObjects())
			{
				if (rotation != null)
				{
					this.playArea.rotation = rotation.Value;
				}
				return this.playArea.rotation;
			}
			return Quaternion.identity;
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0007E924 File Offset: 0x0007CB24
		protected virtual Vector3 GetNewPosition(Vector3 tipPosition, Transform target, bool returnOriginalPosition)
		{
			if (returnOriginalPosition)
			{
				return tipPosition;
			}
			float x = 0f;
			float y = 0f;
			float z = 0f;
			if (this.ValidRigObjects())
			{
				x = (this.headsetPositionCompensation ? (tipPosition.x - (this.headset.position.x - this.playArea.position.x)) : tipPosition.x);
				y = this.playArea.position.y;
				z = (this.headsetPositionCompensation ? (tipPosition.z - (this.headset.position.z - this.playArea.position.z)) : tipPosition.z);
			}
			return new Vector3(x, y, z);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0007E9E0 File Offset: 0x0007CBE0
		protected virtual Vector3 CheckTerrainCollision(Vector3 position, Transform target, bool useHeadsetForPosition)
		{
			Terrain component = target.GetComponent<Terrain>();
			if (this.adjustYForTerrain && component != null)
			{
				Vector3 worldPosition = (useHeadsetForPosition && this.ValidRigObjects()) ? new Vector3(this.headset.position.x, position.y, this.headset.position.z) : position;
				float num = component.SampleHeight(worldPosition);
				position.y = ((num > position.y) ? position.y : (component.GetPosition().y + num));
			}
			return position;
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0007EA6D File Offset: 0x0007CC6D
		protected virtual void OnTeleporting(object sender, DestinationMarkerEventArgs e)
		{
			if (this.Teleporting != null)
			{
				this.Teleporting(this, e);
			}
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0007EA84 File Offset: 0x0007CC84
		protected virtual void OnTeleported(object sender, DestinationMarkerEventArgs e)
		{
			if (this.Teleported != null)
			{
				this.Teleported(this, e);
			}
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0007EA9C File Offset: 0x0007CC9C
		protected virtual void CalculateBlinkDelay(float blinkSpeed, Vector3 newPosition)
		{
			this.blinkPause = 0f;
			if (this.distanceBlinkDelay > 0f)
			{
				float num = 0.5f;
				float num2 = this.ValidRigObjects() ? Vector3.Distance(this.playArea.position, newPosition) : 0f;
				this.blinkPause = Mathf.Clamp(num2 * this.blinkTransitionSpeed / (this.maxBlinkDistance - this.distanceBlinkDelay), num, this.maxBlinkTransitionSpeed);
				this.blinkPause = (((double)blinkSpeed <= 0.25) ? num : this.blinkPause);
			}
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0007EB2C File Offset: 0x0007CD2C
		protected virtual void ReleaseBlink()
		{
			VRTK_SDK_Bridge.HeadsetFade(Color.clear, this.fadeInTime, false);
			this.fadeInTime = 0f;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0007EB4A File Offset: 0x0007CD4A
		private IEnumerator PreventTeleport(float delay)
		{
			this.canTeleport = false;
			yield return new WaitForSeconds(delay);
			this.canTeleport = true;
			yield break;
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0007EB60 File Offset: 0x0007CD60
		protected virtual IEnumerator InitListenersAtEndOfFrame()
		{
			yield return new WaitForEndOfFrame();
			if (base.enabled)
			{
				this.InitDestinationMarkerListeners(true);
			}
			yield break;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0007EB70 File Offset: 0x0007CD70
		protected virtual void InitDestinationMarkerListeners(bool state)
		{
			GameObject controllerLeftHand = VRTK_DeviceFinder.GetControllerLeftHand(false);
			GameObject controllerRightHand = VRTK_DeviceFinder.GetControllerRightHand(false);
			this.InitDestinationSetListener(controllerLeftHand, state);
			this.InitDestinationSetListener(controllerRightHand, state);
			foreach (VRTK_DestinationMarker vrtk_DestinationMarker in VRTK_ObjectCache.registeredDestinationMarkers)
			{
				if (vrtk_DestinationMarker.gameObject != controllerLeftHand && vrtk_DestinationMarker.gameObject != controllerRightHand)
				{
					this.InitDestinationSetListener(vrtk_DestinationMarker.gameObject, state);
				}
			}
		}

		// Token: 0x04001337 RID: 4919
		[Header("Base Settings")]
		[Tooltip("The colour to fade to when blinking on teleport.")]
		public Color blinkToColor = Color.black;

		// Token: 0x04001338 RID: 4920
		[Tooltip("The fade blink speed can be changed on the basic teleport script to provide a customised teleport experience. Setting the speed to 0 will mean no fade blink effect is present.")]
		public float blinkTransitionSpeed = 0.6f;

		// Token: 0x04001339 RID: 4921
		[Tooltip("A range between 0 and 32 that determines how long the blink transition will stay blacked out depending on the distance being teleported. A value of 0 will not delay the teleport blink effect over any distance, a value of 32 will delay the teleport blink fade in even when the distance teleported is very close to the original position. This can be used to simulate time taking longer to pass the further a user teleports. A value of 16 provides a decent basis to simulate this to the user.")]
		[Range(0f, 32f)]
		public float distanceBlinkDelay;

		// Token: 0x0400133A RID: 4922
		[Tooltip("If this is checked then the teleported location will be the position of the headset within the play area. If it is unchecked then the teleported location will always be the centre of the play area even if the headset position is not in the centre of the play area.")]
		public bool headsetPositionCompensation = true;

		// Token: 0x0400133B RID: 4923
		[Tooltip("A specified VRTK_PolicyList to use to determine whether destination targets will be acted upon by the Teleporter.")]
		public VRTK_PolicyList targetListPolicy;

		// Token: 0x0400133E RID: 4926
		protected Transform headset;

		// Token: 0x0400133F RID: 4927
		protected Transform playArea;

		// Token: 0x04001340 RID: 4928
		protected bool adjustYForTerrain;

		// Token: 0x04001341 RID: 4929
		protected bool enableTeleport = true;

		// Token: 0x04001342 RID: 4930
		protected float blinkPause;

		// Token: 0x04001343 RID: 4931
		protected float fadeInTime;

		// Token: 0x04001344 RID: 4932
		protected float maxBlinkTransitionSpeed = 1.5f;

		// Token: 0x04001345 RID: 4933
		protected float maxBlinkDistance = 33f;

		// Token: 0x04001346 RID: 4934
		protected Coroutine initaliseListeners;

		// Token: 0x04001347 RID: 4935
		[SerializeField]
		private Player player;

		// Token: 0x04001348 RID: 4936
		private bool canTeleport = true;
	}
}
