using System;
using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;

// Token: 0x020001B7 RID: 439
public class PauseMenuManager : Photon.MonoBehaviour
{
	// Token: 0x06000BF4 RID: 3060 RVA: 0x0004AAFC File Offset: 0x00048CFC
	private void Awake()
	{
		if (SceneManager.GetActiveScene().name == "Menu_New")
		{
			base.enabled = false;
		}
		this.trackedObject = base.GetComponent<SteamVR_TrackedObject>();
		if (this.view.isMine)
		{
			PauseMenuManager.instance = this;
		}
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x0004AB48 File Offset: 0x00048D48
	private void Start()
	{
		if (!PlayerPrefs.HasKey("player1Volume"))
		{
			PlayerPrefs.SetFloat("player1Volume", 1f);
		}
		if (!PlayerPrefs.HasKey("player2Volume"))
		{
			PlayerPrefs.SetFloat("player2Volume", 1f);
		}
		if (!PlayerPrefs.HasKey("player2Volume"))
		{
			PlayerPrefs.SetFloat("player3Volume", 1f);
		}
		if (PhotonNetwork.otherPlayers.Length != 0)
		{
			this.player2Text.text = PhotonNetwork.otherPlayers[0].NickName;
			this.player2Object.SetActive(true);
			this.player2ActorID = PhotonNetwork.otherPlayers[0].ID;
			this.player2Slider.value = this.GetSavedPlayerVolume(PhotonNetwork.otherPlayers[0].NickName);
		}
		if (PhotonNetwork.otherPlayers.Length > 1)
		{
			this.player3Text.text = PhotonNetwork.otherPlayers[1].NickName;
			this.player3Object.SetActive(true);
			this.player3ActorID = PhotonNetwork.otherPlayers[1].ID;
			this.player3Slider.value = this.GetSavedPlayerVolume(PhotonNetwork.otherPlayers[1].NickName);
		}
		if (PhotonNetwork.otherPlayers.Length > 2)
		{
			this.player4Text.text = PhotonNetwork.otherPlayers[2].NickName;
			this.player4Object.SetActive(true);
			this.player4ActorID = PhotonNetwork.otherPlayers[2].ID;
			this.player4Slider.value = this.GetSavedPlayerVolume(PhotonNetwork.otherPlayers[2].NickName);
		}
		if (PhotonNetwork.otherPlayers.Length != 0)
		{
			PlayerPrefs.SetString("player1Name", PhotonNetwork.otherPlayers[0].NickName);
		}
		if (PhotonNetwork.otherPlayers.Length > 1)
		{
			PlayerPrefs.SetString("player2Name", PhotonNetwork.otherPlayers[1].NickName);
		}
		if (PhotonNetwork.otherPlayers.Length > 2)
		{
			PlayerPrefs.SetString("player3Name", PhotonNetwork.otherPlayers[2].NickName);
		}
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x0004AD18 File Offset: 0x00048F18
	public void Player2SliderValueChanged()
	{
		this.player2ValueText.text = (this.player2Slider.value * 100f).ToString("0") + "%";
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i].actorID == this.player2ActorID)
			{
				GameController.instance.playersData[i].player.voiceVolume.ApplyVoiceVolume(this.player2Slider.value);
			}
		}
		this.SetPlayerVolumes();
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x0004ADC0 File Offset: 0x00048FC0
	public void Player3SliderValueChanged()
	{
		this.player3ValueText.text = (this.player3Slider.value * 100f).ToString("0") + "%";
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i].actorID == this.player3ActorID)
			{
				GameController.instance.playersData[i].player.voiceVolume.ApplyVoiceVolume(this.player3Slider.value);
			}
		}
		this.SetPlayerVolumes();
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x0004AE68 File Offset: 0x00049068
	public void Player4SliderValueChanged()
	{
		this.player4ValueText.text = (this.player4Slider.value * 100f).ToString("0") + "%";
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i].actorID == this.player4ActorID)
			{
				GameController.instance.playersData[i].player.voiceVolume.ApplyVoiceVolume(this.player4Slider.value);
			}
		}
		this.SetPlayerVolumes();
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x0004AF10 File Offset: 0x00049110
	public float GetPlayerVolume(int actorID)
	{
		if (actorID == this.player2ActorID)
		{
			return this.player2Slider.value;
		}
		if (actorID == this.player3ActorID)
		{
			return this.player3Slider.value;
		}
		if (actorID == this.player4ActorID)
		{
			return this.player4Slider.value;
		}
		return 1f;
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x0004AF64 File Offset: 0x00049164
	private void SetPlayerVolumes()
	{
		if (this._isPaused)
		{
			PlayerPrefs.SetFloat("player1Volume", this.player2Slider.value);
			PlayerPrefs.SetFloat("player2Volume", this.player3Slider.value);
			PlayerPrefs.SetFloat("player3Volume", this.player4Slider.value);
		}
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x0004AFB8 File Offset: 0x000491B8
	private float GetSavedPlayerVolume(string playerName)
	{
		if (playerName == PlayerPrefs.GetString("player1Name") && PlayerPrefs.GetFloat("player1Volume") != 0f)
		{
			return PlayerPrefs.GetFloat("player1Volume");
		}
		if (playerName == PlayerPrefs.GetString("player2Name") && PlayerPrefs.GetFloat("player2Volume") != 0f)
		{
			return PlayerPrefs.GetFloat("player2Volume");
		}
		if (playerName == PlayerPrefs.GetString("player3Name") && PlayerPrefs.GetFloat("player3Volume") != 0f)
		{
			return PlayerPrefs.GetFloat("player3Volume");
		}
		return 1f;
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x0004B054 File Offset: 0x00049254
	private void OnEnable()
	{
		if (this.pauseMenuObject.GetComponent<Canvas>().renderMode == RenderMode.WorldSpace && GameController.instance.myPlayer != null)
		{
			this.pauseMenuObject.GetComponent<Canvas>().worldCamera = GameController.instance.myPlayer.player.cam;
		}
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x0004B0A4 File Offset: 0x000492A4
	private void Paused(bool isPaused)
	{
		this._isPaused = isPaused;
		GameController.instance.myPlayer.player.movementSettings.InMenuOrJournal(isPaused);
		this.pauseMenuObject.SetActive(isPaused);
		if (isPaused)
		{
			this.pauseMenuObject.transform.position = this.eye.transform.position + this.eye.transform.forward * 1f;
			this.pauseMenuObject.transform.rotation = this.eye.transform.rotation;
		}
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x0004B140 File Offset: 0x00049340
	private void Update()
	{
		if (this.device == null)
		{
			this.device = SteamVR_Controller.Input((int)this.trackedObject.index);
		}
		if (this.device.GetPressDown(this.B_Button))
		{
			this.Paused(!this.pauseMenuObject.activeInHierarchy);
		}
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x0004B192 File Offset: 0x00049392
	public void ResumeButton()
	{
		this.pauseMenuObject.SetActive(false);
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x00003F60 File Offset: 0x00002160
	public void OptionsButton()
	{
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x0004B1A0 File Offset: 0x000493A0
	public void LeaveButton()
	{
		if (GameController.instance.isTutorial)
		{
			FileBasedPrefs.SetInt("MissionStatus", 3);
			FileBasedPrefs.SetInt("setupPhase", 0);
			FileBasedPrefs.SetInt("completedTraining", 1);
			FileBasedPrefs.SetInt("StayInServerRoom", 0);
		}
		if (PhotonNetwork.offlineMode)
		{
			FileBasedPrefs.SetInt("StayInServerRoom", 0);
		}
		SteamVR_LoadLevel.Begin("Menu_New", false, 0.5f, 0f, 0f, 0f, 1f);
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x0004B21B File Offset: 0x0004941B
	public void QuitButton()
	{
		if (GameController.instance.isTutorial)
		{
			FileBasedPrefs.SetInt("MissionStatus", 3);
			FileBasedPrefs.SetInt("setupPhase", 0);
			FileBasedPrefs.SetInt("completedTraining", 1);
			FileBasedPrefs.SetInt("StayInServerRoom", 0);
		}
		Application.Quit();
	}

	// Token: 0x04000C3E RID: 3134
	public static PauseMenuManager instance;

	// Token: 0x04000C3F RID: 3135
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000C40 RID: 3136
	private EVRButtonId B_Button = EVRButtonId.k_EButton_ApplicationMenu;

	// Token: 0x04000C41 RID: 3137
	private SteamVR_TrackedObject trackedObject;

	// Token: 0x04000C42 RID: 3138
	private SteamVR_Controller.Device device;

	// Token: 0x04000C43 RID: 3139
	[SerializeField]
	private GameObject pauseMenuObject;

	// Token: 0x04000C44 RID: 3140
	[SerializeField]
	private Transform eye;

	// Token: 0x04000C45 RID: 3141
	[SerializeField]
	private GameObject player2Object;

	// Token: 0x04000C46 RID: 3142
	[SerializeField]
	private Text player2Text;

	// Token: 0x04000C47 RID: 3143
	[SerializeField]
	private Text player2ValueText;

	// Token: 0x04000C48 RID: 3144
	[SerializeField]
	private Slider player2Slider;

	// Token: 0x04000C49 RID: 3145
	private int player2ActorID = 999;

	// Token: 0x04000C4A RID: 3146
	[SerializeField]
	private GameObject player3Object;

	// Token: 0x04000C4B RID: 3147
	[SerializeField]
	private Text player3Text;

	// Token: 0x04000C4C RID: 3148
	[SerializeField]
	private Text player3ValueText;

	// Token: 0x04000C4D RID: 3149
	[SerializeField]
	private Slider player3Slider;

	// Token: 0x04000C4E RID: 3150
	private int player3ActorID = 999;

	// Token: 0x04000C4F RID: 3151
	[SerializeField]
	private GameObject player4Object;

	// Token: 0x04000C50 RID: 3152
	[SerializeField]
	private Text player4Text;

	// Token: 0x04000C51 RID: 3153
	[SerializeField]
	private Text player4ValueText;

	// Token: 0x04000C52 RID: 3154
	[SerializeField]
	private Slider player4Slider;

	// Token: 0x04000C53 RID: 3155
	private int player4ActorID = 999;

	// Token: 0x04000C54 RID: 3156
	private bool _isPaused;
}
