using System;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.LoadBalancing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExitGames.Demos.DemoPunVoice
{
	// Token: 0x02000496 RID: 1174
	public class VoiceDemoUI : MonoBehaviour
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x000B293D File Offset: 0x000B0B3D
		// (set) Token: 0x06002492 RID: 9362 RVA: 0x000B2948 File Offset: 0x000B0B48
		public bool DebugMode
		{
			get
			{
				return this.debugMode;
			}
			set
			{
				this.debugMode = value;
				this.debugGO.SetActive(this.debugMode);
				this.voiceDebugText.text = "";
				if (this.debugMode)
				{
					this.previousDebugLevel = PhotonVoiceNetwork.Client.loadBalancingPeer.DebugOut;
					PhotonVoiceNetwork.Client.loadBalancingPeer.DebugOut = DebugLevel.ALL;
				}
				else
				{
					PhotonVoiceNetwork.Client.loadBalancingPeer.DebugOut = this.previousDebugLevel;
				}
				if (VoiceDemoUI.DebugToggled != null)
				{
					VoiceDemoUI.DebugToggled(this.debugMode);
				}
			}
		}

		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06002493 RID: 9363 RVA: 0x000B29D8 File Offset: 0x000B0BD8
		// (remove) Token: 0x06002494 RID: 9364 RVA: 0x000B2A0C File Offset: 0x000B0C0C
		public static event VoiceDemoUI.OnDebugToggle DebugToggled;

		// Token: 0x06002495 RID: 9365 RVA: 0x000B2A3F File Offset: 0x000B0C3F
		private void OnEnable()
		{
			ChangePOV.CameraChanged += this.OnCameraChanged;
			CharacterInstantiation.CharacterInstantiated += this.CharacterInstantiation_CharacterInstantiated;
			BetterToggle.ToggleValueChanged += this.BetterToggle_ToggleValueChanged;
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000B2A74 File Offset: 0x000B0C74
		private void OnDisable()
		{
			ChangePOV.CameraChanged -= this.OnCameraChanged;
			CharacterInstantiation.CharacterInstantiated -= this.CharacterInstantiation_CharacterInstantiated;
			BetterToggle.ToggleValueChanged -= this.BetterToggle_ToggleValueChanged;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000B2AAC File Offset: 0x000B0CAC
		private void InitToggles(Toggle[] toggles)
		{
			if (toggles == null)
			{
				return;
			}
			foreach (Toggle toggle in toggles)
			{
				string name = toggle.name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 1205983746U)
				{
					if (num <= 836321993U)
					{
						if (num != 307911202U)
						{
							if (num == 836321993U)
							{
								if (name == "Transmit")
								{
									toggle.isOn = (this.rec != null && this.rec.Transmit);
								}
							}
						}
						else if (name == "DebugVoice")
						{
							this.DebugMode = PhotonVoiceSettings.Instance.DebugInfo;
							toggle.isOn = this.DebugMode;
						}
					}
					else if (num != 1063296884U)
					{
						if (num == 1205983746U)
						{
							if (name == "AutoTransmit")
							{
								toggle.isOn = PhotonVoiceSettings.Instance.AutoTransmit;
							}
						}
					}
					else if (name == "Mute")
					{
						toggle.isOn = (AudioListener.volume <= 0.001f);
					}
				}
				else if (num <= 2555140824U)
				{
					if (num != 1813426340U)
					{
						if (num == 2555140824U)
						{
							if (name == "AutoDisconnect")
							{
								toggle.isOn = PhotonVoiceSettings.Instance.AutoDisconnect;
							}
						}
					}
					else if (name == "AutoConnect")
					{
						toggle.isOn = PhotonVoiceSettings.Instance.AutoConnect;
					}
				}
				else if (num != 2809822681U)
				{
					if (num == 3751824224U)
					{
						if (name == "VoiceDetection")
						{
							toggle.isOn = PhotonVoiceSettings.Instance.VoiceDetection;
						}
					}
				}
				else if (name == "DebugEcho")
				{
					toggle.isOn = (this.rec != null && this.rec.DebugEchoMode);
				}
			}
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000B2CCC File Offset: 0x000B0ECC
		private void CharacterInstantiation_CharacterInstantiated(GameObject character)
		{
			this.rec = character.GetComponent<PhotonVoiceRecorder>();
			this.rec.enabled = true;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000B2CE8 File Offset: 0x000B0EE8
		private void BetterToggle_ToggleValueChanged(Toggle toggle)
		{
			string name = toggle.name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1205983746U)
			{
				if (num <= 836321993U)
				{
					if (num != 307911202U)
					{
						if (num != 836321993U)
						{
							return;
						}
						if (!(name == "Transmit"))
						{
							return;
						}
						if (this.rec)
						{
							this.rec.Transmit = toggle.isOn;
							return;
						}
					}
					else
					{
						if (!(name == "DebugVoice"))
						{
							return;
						}
						this.DebugMode = toggle.isOn;
						PhotonVoiceSettings.Instance.DebugInfo = this.DebugMode;
					}
				}
				else if (num != 1063296884U)
				{
					if (num != 1205983746U)
					{
						return;
					}
					if (!(name == "AutoTransmit"))
					{
						return;
					}
					PhotonVoiceSettings.Instance.AutoTransmit = toggle.isOn;
					return;
				}
				else
				{
					if (!(name == "Mute"))
					{
						return;
					}
					if (toggle.isOn)
					{
						this.volumeBeforeMute = AudioListener.volume;
						AudioListener.volume = 0f;
						return;
					}
					AudioListener.volume = this.volumeBeforeMute;
					this.volumeBeforeMute = 0f;
					return;
				}
			}
			else if (num <= 2555140824U)
			{
				if (num != 1813426340U)
				{
					if (num != 2555140824U)
					{
						return;
					}
					if (!(name == "AutoDisconnect"))
					{
						return;
					}
					PhotonVoiceSettings.Instance.AutoDisconnect = toggle.isOn;
					return;
				}
				else
				{
					if (!(name == "AutoConnect"))
					{
						return;
					}
					PhotonVoiceSettings.Instance.AutoConnect = toggle.isOn;
					return;
				}
			}
			else if (num != 2809822681U)
			{
				if (num != 3751824224U)
				{
					return;
				}
				if (!(name == "VoiceDetection"))
				{
					return;
				}
				PhotonVoiceSettings.Instance.VoiceDetection = toggle.isOn;
				if (this.rec)
				{
					this.rec.Detect = toggle.isOn;
					return;
				}
			}
			else
			{
				if (!(name == "DebugEcho"))
				{
					return;
				}
				if (this.rec)
				{
					this.rec.DebugEchoMode = toggle.isOn;
					return;
				}
			}
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000B2EE1 File Offset: 0x000B10E1
		private void OnCameraChanged(Camera newCamera)
		{
			this.canvas.worldCamera = newCamera;
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000B2EF0 File Offset: 0x000B10F0
		private void Start()
		{
			this.canvas = base.GetComponentInChildren<Canvas>();
			if (this.punSwitch != null)
			{
				this.punSwitchText = this.punSwitch.GetComponentInChildren<Text>();
				this.punSwitch.onClick.AddListener(new UnityAction(this.PunSwitchOnClick));
			}
			if (this.voiceSwitch)
			{
				this.voiceSwitchText = this.voiceSwitch.GetComponentInChildren<Text>();
				this.voiceSwitch.onClick.AddListener(new UnityAction(this.VoiceSwitchOnClick));
			}
			if (this.calibrateButton != null)
			{
				this.calibrateButton.onClick.AddListener(new UnityAction(this.CalibrateButtonOnClick));
				this.calibrateText = this.calibrateButton.GetComponentInChildren<Text>();
			}
			if (this.punState != null)
			{
				this.debugGO = this.punState.transform.parent.gameObject;
			}
			this.volumeBeforeMute = AudioListener.volume;
			this.previousDebugLevel = PhotonVoiceNetwork.Client.loadBalancingPeer.DebugOut;
			if (this.globalSettings != null)
			{
				this.globalSettings.SetActive(true);
				this.InitToggles(this.globalSettings.GetComponentsInChildren<Toggle>());
			}
			if (this.devicesInfoText != null)
			{
				if (Microphone.devices == null || Microphone.devices.Length == 0)
				{
					this.devicesInfoText.enabled = true;
					this.devicesInfoText.color = Color.red;
					this.devicesInfoText.text = "No microphone device detected!";
					return;
				}
				if (Microphone.devices.Length == 1)
				{
					this.devicesInfoText.text = string.Format("Mic.: {0}", Microphone.devices[0]);
					return;
				}
				this.devicesInfoText.text = string.Format("Multi.Mic.Devices:\n0. {0} (active)\n", Microphone.devices[0]);
				for (int i = 1; i < Microphone.devices.Length; i++)
				{
					this.devicesInfoText.text = this.devicesInfoText.text + string.Format("{0}. {1}\n", i, Microphone.devices[i]);
				}
			}
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000B3104 File Offset: 0x000B1304
		private void PunSwitchOnClick()
		{
			if (PhotonNetwork.connectionStateDetailed == global::ClientState.Joined)
			{
				PhotonNetwork.Disconnect();
				return;
			}
			if (PhotonNetwork.connectionStateDetailed == global::ClientState.Disconnected || PhotonNetwork.connectionStateDetailed == global::ClientState.PeerCreated)
			{
				PhotonNetwork.ConnectUsingSettings(string.Format("1.{0}", SceneManager.GetActiveScene().buildIndex));
			}
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000B3153 File Offset: 0x000B1353
		private void VoiceSwitchOnClick()
		{
			if (PhotonVoiceNetwork.ClientState == ExitGames.Client.Photon.LoadBalancing.ClientState.Joined)
			{
				PhotonVoiceNetwork.Disconnect();
				return;
			}
			if (PhotonVoiceNetwork.ClientState == ExitGames.Client.Photon.LoadBalancing.ClientState.PeerCreated || PhotonVoiceNetwork.ClientState == ExitGames.Client.Photon.LoadBalancing.ClientState.Disconnected)
			{
				PhotonVoiceNetwork.Connect();
			}
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000B3179 File Offset: 0x000B1379
		private void CalibrateButtonOnClick()
		{
			if (this.rec && !this.rec.VoiceDetectorCalibrating)
			{
				this.rec.VoiceDetectorCalibrate(this.calibrationMilliSeconds);
			}
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000B31A8 File Offset: 0x000B13A8
		private void Update()
		{
			global::ClientState connectionStateDetailed = PhotonNetwork.connectionStateDetailed;
			if (connectionStateDetailed != global::ClientState.PeerCreated)
			{
				if (connectionStateDetailed == global::ClientState.Joined)
				{
					this.punSwitch.interactable = true;
					this.punSwitchText.text = "PUN Disconnect";
					goto IL_8D;
				}
				if (connectionStateDetailed != global::ClientState.Disconnected)
				{
					this.punSwitch.interactable = false;
					this.punSwitchText.text = "PUN busy";
					goto IL_8D;
				}
			}
			this.punSwitch.interactable = true;
			this.punSwitchText.text = "PUN Connect";
			if (this.rec != null)
			{
				this.rec.enabled = false;
				this.rec = null;
			}
			IL_8D:
			ExitGames.Client.Photon.LoadBalancing.ClientState clientState = PhotonVoiceNetwork.ClientState;
			if (clientState != ExitGames.Client.Photon.LoadBalancing.ClientState.PeerCreated)
			{
				if (clientState != ExitGames.Client.Photon.LoadBalancing.ClientState.Joined)
				{
					if (clientState != ExitGames.Client.Photon.LoadBalancing.ClientState.Disconnected)
					{
						this.voiceSwitch.interactable = false;
						this.voiceSwitchText.text = "Voice busy";
						goto IL_211;
					}
				}
				else
				{
					this.voiceSwitch.interactable = true;
					this.voiceSwitchText.text = "Voice Disconnect";
					this.inGameSettings.SetActive(true);
					this.InitToggles(this.inGameSettings.GetComponentsInChildren<Toggle>());
					if (this.rec != null)
					{
						this.calibrateButton.interactable = !this.rec.VoiceDetectorCalibrating;
						this.calibrateText.text = (this.rec.VoiceDetectorCalibrating ? "Calibrating" : string.Format("Calibrate ({0}s)", this.calibrationMilliSeconds / 1000));
						goto IL_211;
					}
					this.calibrateButton.interactable = false;
					this.calibrateText.text = "Unavailable";
					goto IL_211;
				}
			}
			if (PhotonNetwork.inRoom)
			{
				this.voiceSwitch.interactable = true;
				this.voiceSwitchText.text = "Voice Connect";
				this.voiceDebugText.text = "";
			}
			else
			{
				this.voiceSwitch.interactable = false;
				this.voiceSwitchText.text = "Voice N/A";
				this.voiceDebugText.text = "";
			}
			this.calibrateButton.interactable = false;
			this.calibrateText.text = "Unavailable";
			this.inGameSettings.SetActive(false);
			IL_211:
			if (this.debugMode)
			{
				this.punState.text = string.Format("PUN: {0}", PhotonNetwork.connectionStateDetailed);
				this.voiceState.text = string.Format("PhotonVoice: {0}", PhotonVoiceNetwork.ClientState);
				if (this.rec != null && this.rec.LevelMeter != null)
				{
					this.voiceDebugText.text = string.Format("Amp: avg. {0}, peak {1}", this.rec.LevelMeter.CurrentAvgAmp.ToString("0.000000"), this.rec.LevelMeter.CurrentPeakAmp.ToString("0.000000"));
				}
			}
		}

		// Token: 0x040021B8 RID: 8632
		[SerializeField]
		private Text punState;

		// Token: 0x040021B9 RID: 8633
		[SerializeField]
		private Text voiceState;

		// Token: 0x040021BA RID: 8634
		private Canvas canvas;

		// Token: 0x040021BB RID: 8635
		[SerializeField]
		private Button punSwitch;

		// Token: 0x040021BC RID: 8636
		private Text punSwitchText;

		// Token: 0x040021BD RID: 8637
		[SerializeField]
		private Button voiceSwitch;

		// Token: 0x040021BE RID: 8638
		private Text voiceSwitchText;

		// Token: 0x040021BF RID: 8639
		[SerializeField]
		private Button calibrateButton;

		// Token: 0x040021C0 RID: 8640
		private Text calibrateText;

		// Token: 0x040021C1 RID: 8641
		[SerializeField]
		private Text voiceDebugText;

		// Token: 0x040021C2 RID: 8642
		private PhotonVoiceRecorder rec;

		// Token: 0x040021C3 RID: 8643
		[SerializeField]
		private GameObject inGameSettings;

		// Token: 0x040021C4 RID: 8644
		[SerializeField]
		private GameObject globalSettings;

		// Token: 0x040021C5 RID: 8645
		[SerializeField]
		private Text devicesInfoText;

		// Token: 0x040021C6 RID: 8646
		private GameObject debugGO;

		// Token: 0x040021C7 RID: 8647
		private bool debugMode;

		// Token: 0x040021C8 RID: 8648
		private float volumeBeforeMute;

		// Token: 0x040021C9 RID: 8649
		private DebugLevel previousDebugLevel;

		// Token: 0x040021CB RID: 8651
		[SerializeField]
		private int calibrationMilliSeconds = 2000;

		// Token: 0x0200079B RID: 1947
		// (Invoke) Token: 0x06003038 RID: 12344
		public delegate void OnDebugToggle(bool debugMode);
	}
}
