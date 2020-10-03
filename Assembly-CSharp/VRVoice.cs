using System;
using UnityEngine;
using Valve.VR;

// Token: 0x020001D1 RID: 465
public class VRVoice : MonoBehaviour
{
	// Token: 0x06000CB4 RID: 3252 RVA: 0x00050F61 File Offset: 0x0004F161
	private void Awake()
	{
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x00050F74 File Offset: 0x0004F174
	private void Start()
	{
		if (PhotonNetwork.inRoom)
		{
			if (this.view.isMine)
			{
				PhotonVoiceSettings.Instance.AutoConnect = true;
				PhotonVoiceSettings.Instance.AutoDisconnect = true;
				PhotonVoiceSettings.Instance.VoiceDetection = true;
				PhotonVoiceSettings.Instance.AutoTransmit = true;
			}
			if (GameController.instance != null && PhotonNetwork.playerList.Length == 1)
			{
				this.recorder.enabled = false;
				base.enabled = false;
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00050FF8 File Offset: 0x0004F1F8
	private void Update()
	{
		if (this.recorder.IsTransmitting)
		{
			if (!this.noise.gameObject.activeInHierarchy)
			{
				this.noise.gameObject.SetActive(true);
				if (!this.walkieTalkie.isOn)
				{
					this.noise.volume = 0.4f;
				}
			}
		}
		else if (this.noise.gameObject.activeInHierarchy)
		{
			this.noise.gameObject.SetActive(false);
		}
		if (this.view.isMine)
		{
			if (this.device == null)
			{
				this.device = SteamVR_Controller.Input((int)this.trackedObject.index);
			}
			if (this.device.GetPressDown(this.A_Button))
			{
				this.EnableOrDisableVOIP();
			}
			if (this.device.GetPressUp(this.A_Button))
			{
				this.EnableOrDisableVOIP();
			}
		}
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x000510D4 File Offset: 0x0004F2D4
	private void EnableOrDisableVOIP()
	{
		this.isVoiceEnabled = !this.isVoiceEnabled;
		PhotonVoiceSettings.Instance.VoiceDetection = this.isVoiceEnabled;
		PhotonVoiceSettings.Instance.AutoTransmit = this.isVoiceEnabled;
		this.recorder.Transmit = this.isVoiceEnabled;
		if (this.isVoiceEnabled)
		{
			this.device.TriggerHapticPulse(5000, EVRButtonId.k_EButton_Axis0);
			return;
		}
		this.device.TriggerHapticPulse(2000, EVRButtonId.k_EButton_Axis0);
	}

	// Token: 0x04000D5E RID: 3422
	[SerializeField]
	private Noise noise;

	// Token: 0x04000D5F RID: 3423
	[SerializeField]
	private WalkieTalkie walkieTalkie;

	// Token: 0x04000D60 RID: 3424
	[SerializeField]
	private PhotonVoiceRecorder recorder;

	// Token: 0x04000D61 RID: 3425
	private bool isVoiceEnabled = true;

	// Token: 0x04000D62 RID: 3426
	[SerializeField]
	private EVRButtonId A_Button = EVRButtonId.k_EButton_A;

	// Token: 0x04000D63 RID: 3427
	[SerializeField]
	private SteamVR_TrackedObject trackedObject;

	// Token: 0x04000D64 RID: 3428
	[SerializeField]
	private SteamVR_Controller.Device device;

	// Token: 0x04000D65 RID: 3429
	[SerializeField]
	private PhotonView view;
}
