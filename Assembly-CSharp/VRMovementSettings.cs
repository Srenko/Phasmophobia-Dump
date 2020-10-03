using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

// Token: 0x020001BB RID: 443
public class VRMovementSettings : MonoBehaviour
{
	// Token: 0x06000C27 RID: 3111 RVA: 0x0004C667 File Offset: 0x0004A867
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.defaultMask = this.teleportRaycast.layersToIgnore;
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x0004C688 File Offset: 0x0004A888
	private void Start()
	{
		if (PhotonNetwork.inRoom)
		{
			if (this.view.isMine)
			{
				base.Invoke("ApplySettings", 1f);
				return;
			}
			base.enabled = false;
			return;
		}
		else
		{
			if (MainManager.instance)
			{
				base.Invoke("ApplySettings", 1f);
				return;
			}
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0004C6E6 File Offset: 0x0004A8E6
	public void InMenuOrJournal(bool inMenu)
	{
		if (PlayerPrefs.GetInt("locomotionValue") == 1)
		{
			this.teleportLocomotion.enabled = !inMenu;
			this.teleportRaycast.triggerInteraction = (inMenu ? QueryTriggerInteraction.Collide : QueryTriggerInteraction.Ignore);
		}
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x0004C718 File Offset: 0x0004A918
	public void ApplySettings()
	{
		if (SceneManager.GetActiveScene().name != "Menu_New")
		{
			this.leftControllerRenderer.maximumLength = 2f;
			this.rightControllerRenderer.maximumLength = 2f;
		}
		this.ApplyRotationSetting();
		this.ApplyMovementSetting();
		this.ApplySnapAngleSetting();
		this.ApplyMovementDirectionSetting();
		this.ApplyGrabTypeSetting();
		this.ApplyGripTypeSetting();
		this.ApplyTeleportGrabSetting();
		this.ApplySmoothRotationSpeed();
		this.ApplyControllerRotation();
		this.player.ActivateOrDeactivateRecordingCam(PlayerPrefs.GetInt("SmoothCamValue") == 1);
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x0004C7AC File Offset: 0x0004A9AC
	private void ApplyMovementSetting()
	{
		int @int = PlayerPrefs.GetInt("locomotionValue");
		if (@int == 0)
		{
			this.smoothLocomotion.gameObject.SetActive(true);
			this.teleportLocomotion.enabled = false;
			this.bodyPhysics.enableTeleport = false;
			this.leftControllerPointer.enableTeleport = false;
			this.leftControllerPointer.activationButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
			this.leftControllerPointer.selectionButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
			this.leftControllerUIPointer.enabled = true;
			this.teleportRaycast.layersToIgnore = this.defaultMask;
			this.teleportRaycast.triggerInteraction = QueryTriggerInteraction.Collide;
			return;
		}
		if (@int != 1)
		{
			Debug.LogError(PlayerPrefs.GetInt("locomotionValue") + " hasn't been applied to ApplyMovementSetting.");
			return;
		}
		this.smoothLocomotion.gameObject.SetActive(false);
		this.teleportLocomotion.enabled = true;
		this.bodyPhysics.enableTeleport = true;
		this.leftControllerPointer.enableTeleport = true;
		this.leftControllerPointer.activationButton = VRTK_ControllerEvents.ButtonAlias.TouchpadTouch;
		this.leftControllerPointer.selectionButton = VRTK_ControllerEvents.ButtonAlias.TouchpadPress;
		this.leftControllerUIPointer.enabled = false;
		this.teleportRaycast.layersToIgnore = this.teleportMask;
		this.teleportRaycast.triggerInteraction = QueryTriggerInteraction.Ignore;
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x0004C8E0 File Offset: 0x0004AAE0
	private void Update()
	{
		if (this.anim)
		{
			if (this.leftControllerEvents.GetTouchpadAxis().magnitude == 0f)
			{
				this.anim.SetFloat("speed", 0f);
				return;
			}
			this.anim.SetFloat("speed", this.leftControllerEvents.touchpadPressed ? 1f : 0.5f);
		}
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x0004C954 File Offset: 0x0004AB54
	private void ApplyRotationSetting()
	{
		this.smoothRotation.gameObject.SetActive(false);
		this.snapRotation.gameObject.SetActive(false);
		int @int = PlayerPrefs.GetInt("turningValue");
		if (@int == 0)
		{
			this.smoothRotation.gameObject.SetActive(true);
			return;
		}
		if (@int != 1)
		{
			Debug.LogError(PlayerPrefs.GetInt("turningValue") + " hasn't been applied to ApplyRotationSetting");
			return;
		}
		this.snapRotation.gameObject.SetActive(true);
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x0004C9D9 File Offset: 0x0004ABD9
	private void ApplySmoothRotationSpeed()
	{
		this.smoothRotation.maximumRotationSpeed = (float)((PlayerPrefs.GetInt("turningSpeedValue") == 0) ? 6 : PlayerPrefs.GetInt("turningSpeedValue"));
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x0004CA00 File Offset: 0x0004AC00
	private void ApplySnapAngleSetting()
	{
		switch (PlayerPrefs.GetInt("turningAngleValue"))
		{
		case 0:
			this.snapRotation.anglePerSnap = 15f;
			return;
		case 1:
			this.snapRotation.anglePerSnap = 45f;
			return;
		case 2:
			this.snapRotation.anglePerSnap = 90f;
			return;
		default:
			Debug.LogError(PlayerPrefs.GetInt("turningAngleValue") + " hasn't been applied to ApplySnapAngleSetting.");
			return;
		}
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x0004CA80 File Offset: 0x0004AC80
	private void ApplyMovementDirectionSetting()
	{
		int @int = PlayerPrefs.GetInt("movementDirectionValue");
		if (@int == 0)
		{
			this.leftControllerTouchpadControl.deviceForDirection = VRTK_ObjectControl.DirectionDevices.LeftController;
			return;
		}
		if (@int != 1)
		{
			Debug.LogError(PlayerPrefs.GetInt("movementDirectionValue") + " hasn't been applied to ApplyMovementDirectionSetting.");
			return;
		}
		this.leftControllerTouchpadControl.deviceForDirection = VRTK_ObjectControl.DirectionDevices.Headset;
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x0004CADC File Offset: 0x0004ACDC
	private void ApplyGrabTypeSetting()
	{
		int @int = PlayerPrefs.GetInt("grabTypeValue");
		if (@int == 0)
		{
			this.leftControllerGrab.isToggleGrab = true;
			this.rightControllerGrab.isToggleGrab = true;
			return;
		}
		if (@int != 1)
		{
			Debug.LogError(PlayerPrefs.GetInt("grabTypeValue") + " hasn't been applied to ApplyGrabTypeSetting.");
			return;
		}
		this.leftControllerGrab.isToggleGrab = false;
		this.rightControllerGrab.isToggleGrab = false;
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x0004CB50 File Offset: 0x0004AD50
	private void ApplyGripTypeSetting()
	{
		switch (PlayerPrefs.GetInt("gripTypeValue"))
		{
		case 0:
			this.leftControllerGrab.grabButton = VRTK_ControllerEvents.ButtonAlias.GripPress;
			this.rightControllerGrab.grabButton = VRTK_ControllerEvents.ButtonAlias.GripPress;
			return;
		case 1:
			this.leftControllerGrab.grabButton = VRTK_ControllerEvents.ButtonAlias.GripTouch;
			this.rightControllerGrab.grabButton = VRTK_ControllerEvents.ButtonAlias.GripTouch;
			return;
		case 2:
			this.leftControllerGrab.grabButton = VRTK_ControllerEvents.ButtonAlias.GripClick;
			this.rightControllerGrab.grabButton = VRTK_ControllerEvents.ButtonAlias.GripClick;
			return;
		default:
			Debug.LogError(PlayerPrefs.GetInt("gripTypeValue") + " hasn't been applied to ApplyGripTypeSetting.");
			return;
		}
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x0004CBE8 File Offset: 0x0004ADE8
	private void ApplyTeleportGrabSetting()
	{
		int @int = PlayerPrefs.GetInt("teleportGrabValue");
		if (@int == 0)
		{
			this.leftControllerTeleportGrab.enabled = false;
			this.rightControllerTeleportGrab.enabled = false;
			return;
		}
		if (@int != 1)
		{
			Debug.LogError(PlayerPrefs.GetInt("teleportGrabValue") + " hasn't been applied to ApplyTeleportGrabSetting.");
			return;
		}
		this.leftControllerTeleportGrab.enabled = true;
		this.rightControllerTeleportGrab.enabled = true;
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x0004CC5C File Offset: 0x0004AE5C
	private void ApplyControllerRotation()
	{
		Vector3 vector = new Vector3(this.GetControllerRotationValue((float)PlayerPrefs.GetInt("controllerRotationXValue")), this.GetControllerRotationValue((float)PlayerPrefs.GetInt("controllerRotationYValue")), this.GetControllerRotationValue((float)PlayerPrefs.GetInt("controllerRotationZValue")));
		this.leftControllerGrabModel.localRotation = Quaternion.Euler(vector);
		this.rightControllerGrabModel.localRotation = Quaternion.Euler(vector);
		this.leftController.localRotation = Quaternion.Euler(new Vector3(vector.x, -vector.y, -vector.z));
		this.rightController.localRotation = Quaternion.Euler(vector);
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x0004CD00 File Offset: 0x0004AF00
	private float GetControllerRotationValue(float value)
	{
		if (value == -6f)
		{
			return -90f;
		}
		if (value == -5f)
		{
			return -75f;
		}
		if (value == -4f)
		{
			return -60f;
		}
		if (value == -3f)
		{
			return -45f;
		}
		if (value == -2f)
		{
			return -30f;
		}
		if (value == -1f)
		{
			return -15f;
		}
		if (value == 0f)
		{
			return 0f;
		}
		if (value == 1f)
		{
			return 15f;
		}
		if (value == 2f)
		{
			return 30f;
		}
		if (value == 3f)
		{
			return 45f;
		}
		if (value == 4f)
		{
			return 60f;
		}
		if (value == 5f)
		{
			return 75f;
		}
		return 90f;
	}

	// Token: 0x04000CAB RID: 3243
	private PhotonView view;

	// Token: 0x04000CAC RID: 3244
	[SerializeField]
	private Player player;

	// Token: 0x04000CAD RID: 3245
	[SerializeField]
	private VRTK_SlideObjectControlAction smoothLocomotion;

	// Token: 0x04000CAE RID: 3246
	[SerializeField]
	private VRTK_HeightAdjustTeleport teleportLocomotion;

	// Token: 0x04000CAF RID: 3247
	[SerializeField]
	private VRTK_Pointer leftControllerPointer;

	// Token: 0x04000CB0 RID: 3248
	[SerializeField]
	private VRTK_StraightPointerRenderer leftControllerRenderer;

	// Token: 0x04000CB1 RID: 3249
	[SerializeField]
	private VRTK_StraightPointerRenderer rightControllerRenderer;

	// Token: 0x04000CB2 RID: 3250
	[SerializeField]
	private VRTK_CustomRaycast teleportRaycast;

	// Token: 0x04000CB3 RID: 3251
	private LayerMask defaultMask;

	// Token: 0x04000CB4 RID: 3252
	[SerializeField]
	private LayerMask teleportMask;

	// Token: 0x04000CB5 RID: 3253
	[SerializeField]
	private VRTK_RotateObjectControlAction smoothRotation;

	// Token: 0x04000CB6 RID: 3254
	[SerializeField]
	private VRTK_SnapRotateObjectControlAction snapRotation;

	// Token: 0x04000CB7 RID: 3255
	[SerializeField]
	private VRTK_TouchpadControl leftControllerTouchpadControl;

	// Token: 0x04000CB8 RID: 3256
	[SerializeField]
	private VRTK_BodyPhysics bodyPhysics;

	// Token: 0x04000CB9 RID: 3257
	public Animator anim;

	// Token: 0x04000CBA RID: 3258
	[SerializeField]
	private VRTK_ControllerEvents leftControllerEvents;

	// Token: 0x04000CBB RID: 3259
	[SerializeField]
	private VRTK_InteractGrab leftControllerGrab;

	// Token: 0x04000CBC RID: 3260
	[SerializeField]
	private VRTK_InteractGrab rightControllerGrab;

	// Token: 0x04000CBD RID: 3261
	public VRTeleportGrab leftControllerTeleportGrab;

	// Token: 0x04000CBE RID: 3262
	public VRTeleportGrab rightControllerTeleportGrab;

	// Token: 0x04000CBF RID: 3263
	[SerializeField]
	private VRTK_UIPointer leftControllerUIPointer;

	// Token: 0x04000CC0 RID: 3264
	[SerializeField]
	private Transform leftControllerGrabModel;

	// Token: 0x04000CC1 RID: 3265
	[SerializeField]
	private Transform rightControllerGrabModel;

	// Token: 0x04000CC2 RID: 3266
	[SerializeField]
	private Transform leftController;

	// Token: 0x04000CC3 RID: 3267
	[SerializeField]
	private Transform rightController;
}
