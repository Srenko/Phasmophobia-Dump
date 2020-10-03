using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Token: 0x02000188 RID: 392
public class MyAudioManager : MonoBehaviour
{
	// Token: 0x06000A84 RID: 2692 RVA: 0x00041728 File Offset: 0x0003F928
	private void Awake()
	{
		if (!this.isPauseMenu)
		{
			this.AssignAudioDevices();
			if (PlayerPrefs.GetInt("AudioHasBeenSet") == 0)
			{
				PlayerPrefs.SetFloat("MasterVolume", this.masterVolumeSlider.maxValue);
			}
		}
		if (PlayerPrefs.GetFloat("MasterVolume") > this.masterVolumeSlider.maxValue)
		{
			PlayerPrefs.SetFloat("MasterVolume", this.masterVolumeSlider.maxValue);
		}
		if (PlayerPrefs.GetFloat("MasterVolume") < this.masterVolumeSlider.minValue)
		{
			PlayerPrefs.SetFloat("MasterVolume", this.masterVolumeSlider.maxValue);
		}
		if (!this.isPauseMenu)
		{
			this.AssignSavedDevice(PlayerPrefs.GetString("microphoneDevice"));
		}
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x000417D4 File Offset: 0x0003F9D4
	private void Start()
	{
		this.masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
		this.masterVolumeValueText.text = (this.masterVolumeSlider.value * 100f).ToString("0");
		if (MainManager.instance)
		{
			this.masterAudio.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20f + 15f);
		}
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x00041858 File Offset: 0x0003FA58
	private void AssignAudioDevices()
	{
		this.deviceNames.Clear();
		foreach (string item in Microphone.devices)
		{
			this.deviceNames.Add(item);
		}
		if (this.deviceNames.Count > 0)
		{
			this.deviceIndex = 0;
			this.deviceNameText.text = this.deviceNames[0];
			return;
		}
		this.deviceNameText.text = "No Microphone detected";
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x000418D4 File Offset: 0x0003FAD4
	private void AssignSavedDevice(string savedDeviceName)
	{
		if (this.deviceNames.Count == 0)
		{
			foreach (string item in Microphone.devices)
			{
				this.deviceNames.Add(item);
			}
		}
		for (int j = 0; j < this.deviceNames.Count; j++)
		{
			if (this.deviceNames[j] == savedDeviceName)
			{
				this.deviceIndex = j;
			}
		}
		if (this.deviceNames.Count > 1)
		{
			this.deviceNameText.text = this.deviceNames[this.deviceIndex];
			PhotonVoiceNetwork.MicrophoneDevice = this.deviceNames[this.deviceIndex];
		}
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x00041984 File Offset: 0x0003FB84
	public void AudioDeviceButton(int value)
	{
		this.deviceIndex += value;
		if (this.deviceIndex < 0)
		{
			this.deviceIndex = this.deviceNames.Count - 1;
		}
		else if (this.deviceIndex == this.deviceNames.Count)
		{
			this.deviceIndex = 0;
		}
		this.deviceNameText.text = this.deviceNames[this.deviceIndex];
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x000419F4 File Offset: 0x0003FBF4
	public void MasterVolumeValueChange()
	{
		this.masterVolumeValueText.text = (this.masterVolumeSlider.value * 100f).ToString("0");
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x00041A2A File Offset: 0x0003FC2A
	public void ApplyButton()
	{
		this.SetValues();
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x00041A34 File Offset: 0x0003FC34
	public void SetValues()
	{
		PlayerPrefs.SetFloat("MasterVolume", this.masterVolumeSlider.value);
		this.masterAudio.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20f + 15f);
		if (!this.isPauseMenu)
		{
			PlayerPrefs.SetInt("AudioHasBeenSet", 1);
			PhotonVoiceNetwork.MicrophoneDevice = this.deviceNames[this.deviceIndex];
		}
	}

	// Token: 0x04000ABD RID: 2749
	[SerializeField]
	private Slider masterVolumeSlider;

	// Token: 0x04000ABE RID: 2750
	[SerializeField]
	private Text masterVolumeValueText;

	// Token: 0x04000ABF RID: 2751
	[SerializeField]
	private AudioMixer masterAudio;

	// Token: 0x04000AC0 RID: 2752
	private int deviceIndex;

	// Token: 0x04000AC1 RID: 2753
	[SerializeField]
	private Text deviceNameText;

	// Token: 0x04000AC2 RID: 2754
	private List<string> deviceNames = new List<string>();

	// Token: 0x04000AC3 RID: 2755
	[SerializeField]
	private bool isPauseMenu;
}
