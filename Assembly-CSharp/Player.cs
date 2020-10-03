using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR;
using UnityStandardAssets.Characters.FirstPerson;
using VRTK;

// Token: 0x020001B8 RID: 440
public class Player : MonoBehaviour
{
	// Token: 0x06000C04 RID: 3076 RVA: 0x0004B28A File Offset: 0x0004948A
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.keys.Clear();
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x0004B2A4 File Offset: 0x000494A4
	private void Start()
	{
		if (this.hasRun)
		{
			return;
		}
		if (LevelController.instance)
		{
			this.currentRoom = LevelController.instance.outsideRoom;
		}
		if (this.view.isMine || !PhotonNetwork.inRoom)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			if (!XRDevice.isPresent && PlayerPrefs.GetInt("fovValue") != 0)
			{
				this.cam.fieldOfView = (float)PlayerPrefs.GetInt("fovValue");
				this.pcPropGrab.ChangeItemSpotWithFOV((float)PlayerPrefs.GetInt("fovValue"));
				this.itemSway.SetPosition();
			}
			if (this.firstPersonController)
			{
				bool flag = PlayerPrefs.GetInt("invertedXLookValue") == 1;
				bool flag2 = PlayerPrefs.GetInt("invertedYLookValue") == 1;
				this.firstPersonController.m_MouseLook.XSensitivity = ((PlayerPrefs.GetFloat("sensitivityValue") == 0f) ? 1f : (flag ? (-PlayerPrefs.GetFloat("sensitivityValue")) : PlayerPrefs.GetFloat("sensitivityValue")));
				this.firstPersonController.m_MouseLook.YSensitivity = ((PlayerPrefs.GetFloat("sensitivityValue") == 0f) ? 1f : (flag2 ? (-PlayerPrefs.GetFloat("sensitivityValue")) : PlayerPrefs.GetFloat("sensitivityValue")));
			}
			if (PhotonNetwork.inRoom && MainManager.instance == null)
			{
				this.view.RPC("SetPlayerData", PhotonTargets.AllBuffered, new object[]
				{
					PhotonNetwork.player.ID
				});
			}
			if (MainManager.instance == null)
			{
				if (PhotonNetwork.inRoom)
				{
					if (this.view.isMine)
					{
						Canvas[] array = Object.FindObjectsOfType<Canvas>();
						for (int i = 0; i < array.Length; i++)
						{
							array[i].worldCamera = this.cam;
						}
					}
				}
				else
				{
					Canvas[] array = Object.FindObjectsOfType<Canvas>();
					for (int i = 0; i < array.Length; i++)
					{
						array[i].worldCamera = this.cam;
					}
				}
			}
			else if (PhotonNetwork.inRoom && !XRDevice.isPresent)
			{
				this.pcMenu.ForceIntoMenu();
			}
			this.ApplyBrightnessSetting();
			this.ApplyAntiAliasing();
			this.ApplyAudioSetting();
			this.ApplyScreenSpaceReflectionSetting();
			this.ApplyAmbientOcclusionSetting();
			this.ApplyBloomSetting();
			if (MainManager.instance)
			{
				MainManager.instance.localPlayer = this;
			}
			if (GameController.instance)
			{
				if (GameController.instance.isTutorial)
				{
					this.difficultyRate = 0.5f;
				}
				else if (GameController.instance.levelDifficulty == Contract.LevelDifficulty.Intermediate)
				{
					this.difficultyRate = 1.5f;
				}
				else if (GameController.instance.levelDifficulty == Contract.LevelDifficulty.Professional)
				{
					this.difficultyRate = 2f;
				}
				if (LevelController.instance.type == LevelController.levelType.medium)
				{
					this.normalSanityRate = 0.08f;
					this.setupSanityRate = 0.05f;
				}
				else if (LevelController.instance.type == LevelController.levelType.large)
				{
					this.normalSanityRate = 0.05f;
					this.setupSanityRate = 0.03f;
				}
				if (PhotonNetwork.playerList.Length == 1 && !GameController.instance.isTutorial)
				{
					this.normalSanityRate /= 2f;
					this.setupSanityRate /= 2f;
				}
			}
			PhotonNetwork.isMessageQueueRunning = true;
		}
		this.hasRun = true;
		if (MainManager.instance)
		{
			this.currentPlayerSnapshot = SoundController.instance.firstFloorSnapshot;
			return;
		}
		this.currentPlayerSnapshot = this.truckSnapshot;
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x0004B600 File Offset: 0x00049800
	private void Update()
	{
		if (!this.view.isMine)
		{
			return;
		}
		if (this.charController != null && this.charAnim != null)
		{
			this.charAnim.SetFloat("speed", this.charController.velocity.magnitude);
		}
		if (!this.isDead && GameController.instance != null)
		{
			if (SetupPhaseController.instance == null && GhostController.instance == null)
			{
				return;
			}
			if (SetupPhaseController.instance.mainDoorHasUnlocked)
			{
				if (!this.playerIsInLight && this.currentRoom != LevelController.instance.outsideRoom)
				{
					this.insanity += Time.deltaTime * ((SetupPhaseController.instance.isSetupPhase ? this.setupSanityRate : this.normalSanityRate) * this.difficultyRate);
				}
				this.insanity = Mathf.Clamp(this.insanity, 0f, (float)(SetupPhaseController.instance.isSetupPhase ? 50 : 100));
				if (!this.sanityChallengeHasBeenSet && !this.isDead && this.insanity >= 99f)
				{
					DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.GetZeroSanity, 1);
					this.sanityChallengeHasBeenSet = true;
				}
				if (this.sanityCheckTimer < 0f)
				{
					this.CheckCurrentLight();
					this.sanityCheckTimer = 2f;
				}
				else
				{
					this.sanityCheckTimer -= Time.deltaTime;
				}
				if (this.sanityUpdateTimer < 0f)
				{
					GhostController.instance.UpdatePlayerSanity();
					this.sanityUpdateTimer = 5f;
					return;
				}
				this.sanityUpdateTimer -= Time.deltaTime;
			}
		}
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x0004B7B4 File Offset: 0x000499B4
	[PunRPC]
	private void SetPlayerData(int photonPlayerID)
	{
		if (MainManager.instance)
		{
			return;
		}
		PlayerData playerData = new PlayerData
		{
			player = this,
			photonPlayer = this.view.owner,
			actorID = photonPlayerID,
			playerName = this.view.owner.NickName
		};
		GameController.instance.playersData.Add(playerData);
		if (this.view.isMine)
		{
			GameController.instance.myPlayer = playerData;
			GameController.instance.OnLocalPlayerSpawned.Invoke();
		}
		GameController.instance.OnPlayerSpawned.Invoke();
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x0004B84F File Offset: 0x00049A4F
	public void ActivateOrDeactivateRecordingCam(bool isActive)
	{
		if (this.view.isMine || !PhotonNetwork.inRoom)
		{
			this.smoothVRCamera.SetActive(isActive);
		}
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x0004B871 File Offset: 0x00049A71
	public void ChangeSanity(int value)
	{
		this.insanity += (float)value;
		this.insanity = Mathf.Clamp(this.insanity, 0f, (float)(SetupPhaseController.instance.isSetupPhase ? 50 : 100));
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x0004B8AB File Offset: 0x00049AAB
	public void KillPlayer()
	{
		this.view.RPC("Dead", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x0004B8C3 File Offset: 0x00049AC3
	public void StartKillingPlayer(PhotonPlayer player)
	{
		this.ForceDropPropsSync();
		this.view.RPC("StartKillingPlayerNetworked", player, Array.Empty<object>());
		base.StartCoroutine(this.SpawnDeadBody());
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x0004B8EE File Offset: 0x00049AEE
	private IEnumerator SpawnDeadBody()
	{
		yield return new WaitForSeconds(4.5f);
		DeadPlayer component;
		if (this.VRIKObj != null)
		{
			component = PhotonNetwork.InstantiateSceneObject("DeadPlayerRagdoll", this.headObject.transform.position, LevelController.instance.currentGhost.transform.rotation, 0, null).GetComponent<DeadPlayer>();
		}
		else
		{
			component = PhotonNetwork.InstantiateSceneObject("DeadPlayerRagdoll", base.transform.position, LevelController.instance.currentGhost.transform.rotation, 0, null).GetComponent<DeadPlayer>();
		}
		component.Spawn(this.modelID, this.view.ownerId);
		yield break;
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x0004B900 File Offset: 0x00049B00
	[PunRPC]
	private void StartKillingPlayerNetworked()
	{
		this.isDead = true;
		if (this.pcFlashlight)
		{
			this.pcFlashlight.EnableOrDisableLight(false, true);
		}
		this.deathAudioSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		this.deathAudioSource.Play();
		this.ghostDeathHands.SetActive(true);
		this.chokingAudioSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		this.chokingAudioSource.Play();
		this.playerHeadCamera.DisableCamera();
		if (!XRDevice.isPresent)
		{
			this.pcPropGrab.DropAllInventoryProps();
		}
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x0004B9B7 File Offset: 0x00049BB7
	public void StopAllMovement()
	{
		if (!XRDevice.isPresent)
		{
			this.firstPersonController.m_WalkSpeed = 0f;
			this.firstPersonController.m_RunSpeed = 0f;
		}
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x0004B9E0 File Offset: 0x00049BE0
	[PunRPC]
	private void Dead()
	{
		this.isDead = true;
		base.gameObject.layer = LayerMask.NameToLayer("DeadPlayer");
		GameController.instance.PlayerDied();
		this.insanity = 0f;
		if (this.view.isMine)
		{
			FileBasedPrefs.SetInt("PlayerDied", 1);
			this.postProcessingVolume.profile = this.deadProfile;
			RenderSettings.fog = true;
			DeadZoneController.instance.EnableOrDisableDeadZone(true);
			if (XRDevice.isPresent)
			{
				this.DropAllVRObjects();
				if (this.vrBodyPhysics && this.vrBodyPhysics.GetBodyColliderContainer() != null)
				{
					this.vrBodyPhysics.GetBodyColliderContainer().layer = LayerMask.NameToLayer("DeadPlayer");
					this.vrBodyPhysics.GetFootColliderContainer().layer = LayerMask.NameToLayer("DeadPlayer");
				}
				this.smoothVRCamera.GetComponent<PostProcessVolume>().profile = this.deadProfile;
			}
			else
			{
				this.firstPersonController.m_WalkSpeed = 1.2f;
				this.firstPersonController.m_RunSpeed = 1.6f;
			}
			for (int i = 0; i < LevelController.instance.doors.Count; i++)
			{
				if (LevelController.instance.doors[i] != null && LevelController.instance.doors[i].GetComponent<Door>() != null && LevelController.instance.doors[i].GetComponent<Door>().rend != null)
				{
					LevelController.instance.doors[i].GetComponent<Door>().rend.enabled = false;
				}
			}
			for (int j = 0; j < LevelController.instance.rooms.Length; j++)
			{
				for (int k = 0; k < LevelController.instance.rooms[j].lightSwitches.Count; k++)
				{
					for (int l = 0; l < LevelController.instance.rooms[j].lightSwitches[k].probes.Count; l++)
					{
						LevelController.instance.rooms[j].lightSwitches[k].probes[l].RenderProbe();
					}
				}
			}
		}
		base.transform.SetParent(DeadZoneController.instance.zoneObjects.transform);
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x0004BC34 File Offset: 0x00049E34
	private void CheckCurrentLight()
	{
		Texture2D texture2D = new Texture2D(32, 32, TextureFormat.RGB24, false);
		Rect source = new Rect(0f, 0f, 32f, 32f);
		RenderTexture.active = this.shadowRenderTexture;
		texture2D.ReadPixels(source, 0, 0);
		texture2D.Apply();
		RenderTexture.active = null;
		int num = 0;
		for (int i = 0; i < 32; i++)
		{
			for (int j = 0; j < 32; j++)
			{
				if (texture2D.GetPixel(j, i).grayscale <= 0.008f)
				{
					num++;
				}
			}
		}
		this.playerIsInLight = (num <= 900);
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x0004BCD8 File Offset: 0x00049ED8
	private void ApplyBrightnessSetting()
	{
		ColorGrading colorGrading = null;
		this.postProcessingVolume.profile.TryGetSettings<ColorGrading>(out colorGrading);
		colorGrading.postExposure.value = PlayerPrefs.GetFloat("brightnessValue");
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x0004BD0F File Offset: 0x00049F0F
	public void ApplyAntiAliasing()
	{
		if (!XRDevice.isPresent)
		{
			if (PlayerPrefs.GetInt("taaValue") == 0)
			{
				this.postProcessingLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
				return;
			}
			this.postProcessingLayer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
		}
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x0004BD3D File Offset: 0x00049F3D
	public void ApplyAudioSetting()
	{
		this.masterAudio.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20f + 15f);
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x0004BD6C File Offset: 0x00049F6C
	public void ApplyScreenSpaceReflectionSetting()
	{
		if (!XRDevice.isPresent)
		{
			ScreenSpaceReflections screenSpaceReflections = null;
			this.postProcessingVolume.profile.TryGetSettings<ScreenSpaceReflections>(out screenSpaceReflections);
			screenSpaceReflections.enabled.value = (PlayerPrefs.GetInt("reflectionValue") == 1);
		}
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x0004BDB0 File Offset: 0x00049FB0
	public void ApplyAmbientOcclusionSetting()
	{
		if (!XRDevice.isPresent)
		{
			AmbientOcclusion ambientOcclusion = null;
			this.postProcessingVolume.profile.TryGetSettings<AmbientOcclusion>(out ambientOcclusion);
			ambientOcclusion.enabled.value = (PlayerPrefs.GetInt("ambientOcclusion") == 1);
		}
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x0004BDF4 File Offset: 0x00049FF4
	public void ApplyBloomSetting()
	{
		Bloom bloom = null;
		this.postProcessingVolume.profile.TryGetSettings<Bloom>(out bloom);
		bloom.enabled.value = (PlayerPrefs.GetInt("bloom") == 0);
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x0004BE2E File Offset: 0x0004A02E
	public void ForceDropPropsSync()
	{
		this.view.RPC("ForceDropPropsNetworked", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x0004BE48 File Offset: 0x0004A048
	[PunRPC]
	public void ForceDropPropsNetworked()
	{
		foreach (PhotonObjectInteract photonObjectInteract in base.GetComponentsInChildren<PhotonObjectInteract>(true))
		{
			if (photonObjectInteract.isProp && !photonObjectInteract.isFixedItem && (!photonObjectInteract.GetComponent<CCTV>() || !photonObjectInteract.GetComponent<CCTV>().isHeadCamera))
			{
				photonObjectInteract.gameObject.SetActive(true);
				photonObjectInteract.transform.SetParent(null);
				photonObjectInteract.isGrabbed = false;
				photonObjectInteract.OnPCUnGrabbed.Invoke();
				photonObjectInteract.GetComponent<Collider>().enabled = true;
				photonObjectInteract.GetComponent<Rigidbody>().useGravity = true;
				photonObjectInteract.GetComponent<Rigidbody>().isKinematic = false;
				if (photonObjectInteract.myLeftHandModel)
				{
					photonObjectInteract.myLeftHandModel.SetActive(false);
				}
				if (photonObjectInteract.myRightHandModel)
				{
					photonObjectInteract.myRightHandModel.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x0004BF28 File Offset: 0x0004A128
	public void DropAllVRObjects()
	{
		this.ForceDropPropsNetworked();
		if (this.view.isMine)
		{
			this.movementSettings.leftControllerTeleportGrab.enabled = false;
			this.movementSettings.rightControllerTeleportGrab.enabled = false;
			if (this.leftHandInteractGrab)
			{
				this.leftHandInteractGrab.ForceRelease(false);
			}
			if (this.rightHandInteractGrab)
			{
				this.rightHandInteractGrab.ForceRelease(false);
			}
			if (this.leftVRBeltDropZone)
			{
				this.leftVRBeltDropZone.ForceUnsnap();
				this.leftVRBeltDropZone.enabled = false;
			}
			if (this.rightVRBeltDropZone)
			{
				this.rightVRBeltDropZone.ForceUnsnap();
				this.rightVRBeltDropZone.enabled = false;
			}
			if (this.leftHandInteractGrab)
			{
				this.leftHandInteractGrab.grabButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
			}
			if (this.rightHandInteractGrab)
			{
				this.rightHandInteractGrab.grabButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
			}
			if (this.leftHandInteractUse)
			{
				this.leftHandInteractUse.useButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
			}
			if (this.rightHandInteractUse)
			{
				this.rightHandInteractUse.useButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
			}
		}
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x0004C04B File Offset: 0x0004A24B
	private void OnDisable()
	{
		if (MapController.instance)
		{
			MapController.instance.RemovePlayer(this);
		}
	}

	// Token: 0x04000C55 RID: 3157
	[HideInInspector]
	public PhotonView view;

	// Token: 0x04000C56 RID: 3158
	[HideInInspector]
	public bool beingHunted;

	// Token: 0x04000C57 RID: 3159
	public bool isDead;

	// Token: 0x04000C58 RID: 3160
	[HideInInspector]
	public int modelID;

	// Token: 0x04000C59 RID: 3161
	private bool sanityChallengeHasBeenSet;

	// Token: 0x04000C5A RID: 3162
	[Header("Post Processing")]
	public PostProcessVolume postProcessingVolume;

	// Token: 0x04000C5B RID: 3163
	public PostProcessLayer postProcessingLayer;

	// Token: 0x04000C5C RID: 3164
	[SerializeField]
	private PostProcessProfile mainProfile;

	// Token: 0x04000C5D RID: 3165
	[SerializeField]
	private PostProcessProfile deadProfile;

	// Token: 0x04000C5E RID: 3166
	[Header("Main")]
	public GameObject headObject;

	// Token: 0x04000C5F RID: 3167
	[SerializeField]
	private Breath breath;

	// Token: 0x04000C60 RID: 3168
	public List<global::Key.KeyType> keys = new List<global::Key.KeyType>();

	// Token: 0x04000C61 RID: 3169
	public Camera cam;

	// Token: 0x04000C62 RID: 3170
	[HideInInspector]
	public LevelRoom currentRoom;

	// Token: 0x04000C63 RID: 3171
	[HideInInspector]
	public Transform mapIcon;

	// Token: 0x04000C64 RID: 3172
	public PhotonObjectInteract currentHeldObject;

	// Token: 0x04000C65 RID: 3173
	public GameObject[] characterModels;

	// Token: 0x04000C66 RID: 3174
	public GameObject ghostDeathHands;

	// Token: 0x04000C67 RID: 3175
	public LayerMask ghostRaycastMask;

	// Token: 0x04000C68 RID: 3176
	public LayerMask mainLayerMask;

	// Token: 0x04000C69 RID: 3177
	public PlayerHeadCamera playerHeadCamera;

	// Token: 0x04000C6A RID: 3178
	public Transform aiTargetPoint;

	// Token: 0x04000C6B RID: 3179
	public LayerMask noLayersMask;

	// Token: 0x04000C6C RID: 3180
	[Header("Audio")]
	[SerializeField]
	private AudioMixerSnapshot interiorSnapshot;

	// Token: 0x04000C6D RID: 3181
	[SerializeField]
	private AudioMixerSnapshot deathSnapshot;

	// Token: 0x04000C6E RID: 3182
	[SerializeField]
	private AudioMixerSnapshot truckSnapshot;

	// Token: 0x04000C6F RID: 3183
	[HideInInspector]
	public AudioMixerSnapshot currentPlayerSnapshot;

	// Token: 0x04000C70 RID: 3184
	public VoiceVolume voiceVolume;

	// Token: 0x04000C71 RID: 3185
	public PhotonVoiceRecorder myVoiceRecorder;

	// Token: 0x04000C72 RID: 3186
	public FootstepController footstepController;

	// Token: 0x04000C73 RID: 3187
	public AudioSource evidenceAudioSource;

	// Token: 0x04000C74 RID: 3188
	public AudioSource keysAudioSource;

	// Token: 0x04000C75 RID: 3189
	[SerializeField]
	private AudioSource deathAudioSource;

	// Token: 0x04000C76 RID: 3190
	public AudioSource chokingAudioSource;

	// Token: 0x04000C77 RID: 3191
	public AudioSource heartBeatAudioSource;

	// Token: 0x04000C78 RID: 3192
	public AudioSource sanityPillsAudioSource;

	// Token: 0x04000C79 RID: 3193
	public VoiceOcclusion voiceOcclusion;

	// Token: 0x04000C7A RID: 3194
	[SerializeField]
	private AudioMixer masterAudio;

	// Token: 0x04000C7B RID: 3195
	[Header("Sanity")]
	[HideInInspector]
	public float insanity;

	// Token: 0x04000C7C RID: 3196
	private float sanityUpdateTimer = 15f;

	// Token: 0x04000C7D RID: 3197
	private float sanityCheckTimer = 2f;

	// Token: 0x04000C7E RID: 3198
	[SerializeField]
	private RenderTexture shadowRenderTexture;

	// Token: 0x04000C7F RID: 3199
	[SerializeField]
	private bool playerIsInLight;

	// Token: 0x04000C80 RID: 3200
	private float difficultyRate = 1f;

	// Token: 0x04000C81 RID: 3201
	private float normalSanityRate = 0.12f;

	// Token: 0x04000C82 RID: 3202
	private float setupSanityRate = 0.09f;

	// Token: 0x04000C83 RID: 3203
	[Header("PC")]
	public CharacterController charController;

	// Token: 0x04000C84 RID: 3204
	public AudioListener listener;

	// Token: 0x04000C85 RID: 3205
	public FirstPersonController firstPersonController;

	// Token: 0x04000C86 RID: 3206
	public PCPropGrab pcPropGrab;

	// Token: 0x04000C87 RID: 3207
	public DragRigidbodyUse dragRigidBodyUse;

	// Token: 0x04000C88 RID: 3208
	public PCCanvas pcCanvas;

	// Token: 0x04000C89 RID: 3209
	public PCCrouch pcCrouch;

	// Token: 0x04000C8A RID: 3210
	public PCPushToTalk pcPushToTalk;

	// Token: 0x04000C8B RID: 3211
	public PCMenu pcMenu;

	// Token: 0x04000C8C RID: 3212
	public PCControls pcControls;

	// Token: 0x04000C8D RID: 3213
	public PCFlashlight pcFlashlight;

	// Token: 0x04000C8E RID: 3214
	[HideInInspector]
	public Animator charAnim;

	// Token: 0x04000C8F RID: 3215
	public PlayerInput playerInput;

	// Token: 0x04000C90 RID: 3216
	public PCItemSway itemSway;

	// Token: 0x04000C91 RID: 3217
	[Header("VR")]
	public VRTK_InteractUse leftHandInteractUse;

	// Token: 0x04000C92 RID: 3218
	public VRTK_InteractUse rightHandInteractUse;

	// Token: 0x04000C93 RID: 3219
	[SerializeField]
	private VRTK_InteractGrab leftHandInteractGrab;

	// Token: 0x04000C94 RID: 3220
	[SerializeField]
	private VRTK_InteractGrab rightHandInteractGrab;

	// Token: 0x04000C95 RID: 3221
	public Transform steamVRObj;

	// Token: 0x04000C96 RID: 3222
	public VRMovementSettings movementSettings;

	// Token: 0x04000C97 RID: 3223
	public VRTK_SnapDropZone leftVRBeltDropZone;

	// Token: 0x04000C98 RID: 3224
	public VRTK_SnapDropZone rightVRBeltDropZone;

	// Token: 0x04000C99 RID: 3225
	[SerializeField]
	private GameObject smoothVRCamera;

	// Token: 0x04000C9A RID: 3226
	[SerializeField]
	private VRTK_BodyPhysics vrBodyPhysics;

	// Token: 0x04000C9B RID: 3227
	public Transform VRIKObj;

	// Token: 0x04000C9C RID: 3228
	public VRTK_BasicTeleport basicTeleport;

	// Token: 0x04000C9D RID: 3229
	private bool hasRun;
}
