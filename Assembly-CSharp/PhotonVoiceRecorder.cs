using System;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon.Voice;
using Photon;
using UnityEngine;

// Token: 0x0200000C RID: 12
[RequireComponent(typeof(PhotonVoiceSpeaker))]
[DisallowMultipleComponent]
[AddComponentMenu("Photon Voice/Photon Voice Recorder")]
public class PhotonVoiceRecorder : Photon.MonoBehaviour
{
	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600005F RID: 95 RVA: 0x000038C9 File Offset: 0x00001AC9
	protected ILocalVoiceAudio voiceAudio
	{
		get
		{
			return (ILocalVoiceAudio)this.voice;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000060 RID: 96 RVA: 0x000038D6 File Offset: 0x00001AD6
	public AudioUtil.IVoiceDetector VoiceDetector
	{
		get
		{
			if (!base.photonView.isMine)
			{
				return null;
			}
			return this.voiceAudio.VoiceDetector;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000061 RID: 97 RVA: 0x000038F2 File Offset: 0x00001AF2
	// (set) Token: 0x06000062 RID: 98 RVA: 0x000038FA File Offset: 0x00001AFA
	public string MicrophoneDevice
	{
		get
		{
			return this.microphoneDevice;
		}
		set
		{
			if (value != null && !Microphone.devices.Contains(value))
			{
				Debug.LogError("PUNVoice: " + value + " is not a valid microphone device");
				return;
			}
			this.microphoneDevice = value;
			this.UpdateAudioSource();
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000063 RID: 99 RVA: 0x0000392F File Offset: 0x00001B2F
	// (set) Token: 0x06000064 RID: 100 RVA: 0x00003937 File Offset: 0x00001B37
	public int PhotonMicrophoneDeviceID
	{
		get
		{
			return this.photonMicrophoneDeviceID;
		}
		set
		{
			if (!PhotonVoiceNetwork.PhotonMicrophoneEnumerator.IDIsValid(value))
			{
				Debug.LogError("PUNVoice: " + value + " is not a valid Photon microphone device");
				return;
			}
			this.photonMicrophoneDeviceID = value;
			this.UpdateAudioSource();
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00003970 File Offset: 0x00001B70
	public void UpdateAudioSource()
	{
		if (this.voice != LocalVoiceAudio.Dummy)
		{
			this.audioSource.Dispose();
			this.voice.RemoveSelf();
			base.gameObject.SendMessage("PhotonVoiceRemoved", SendMessageOptions.DontRequireReceiver);
			bool debugEchoMode = this.DebugEchoMode;
			this.DebugEchoMode = false;
			LocalVoice localVoice = this.voice;
			this.voice = this.createLocalVoiceAudioAndSource();
			this.voice.Group = localVoice.Group;
			this.voice.Transmit = localVoice.Transmit;
			this.voiceAudio.VoiceDetector.On = this.voiceAudio.VoiceDetector.On;
			this.voiceAudio.VoiceDetector.Threshold = this.voiceAudio.VoiceDetector.Threshold;
			this.sendPhotonVoiceCreatedMessage();
			this.DebugEchoMode = debugEchoMode;
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000066 RID: 102 RVA: 0x00003A44 File Offset: 0x00001C44
	// (set) Token: 0x06000067 RID: 103 RVA: 0x00003A51 File Offset: 0x00001C51
	public byte AudioGroup
	{
		get
		{
			return this.voice.Group;
		}
		set
		{
			this.voice.Group = value;
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000068 RID: 104 RVA: 0x00003A5F File Offset: 0x00001C5F
	public bool IsTransmitting
	{
		get
		{
			return this.voice.IsTransmitting;
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000069 RID: 105 RVA: 0x00003A6C File Offset: 0x00001C6C
	public AudioUtil.ILevelMeter LevelMeter
	{
		get
		{
			return this.voiceAudio.LevelMeter;
		}
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00003A7C File Offset: 0x00001C7C
	private void Start()
	{
		if (base.photonView.isMine)
		{
			PhotonVoiceRecorder.SampleTypeConv typeConvert = this.TypeConvert;
			if (typeConvert == PhotonVoiceRecorder.SampleTypeConv.Short)
			{
				this.forceShort = true;
				if (PhotonVoiceSettings.Instance.DebugInfo)
				{
					Debug.LogFormat("PUNVoice: Type Conversion set to Short. Audio samples will be converted if source samples type differs.", Array.Empty<object>());
				}
			}
			this.voice = this.createLocalVoiceAudioAndSource();
			this.VoiceDetector.On = PhotonVoiceSettings.Instance.VoiceDetection;
			this.VoiceDetector.Threshold = PhotonVoiceSettings.Instance.VoiceDetectionThreshold;
			if (this.voice != LocalVoiceAudio.Dummy)
			{
				this.voice.Transmit = PhotonVoiceSettings.Instance.AutoTransmit;
			}
			else if (PhotonVoiceSettings.Instance.AutoTransmit)
			{
				Debug.LogWarning("PUNVoice: Cannot Transmit.");
			}
			this.sendPhotonVoiceCreatedMessage();
		}
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00003B3C File Offset: 0x00001D3C
	private LocalVoice createLocalVoiceAudioAndSource()
	{
		PhotonVoiceSettings instance = PhotonVoiceSettings.Instance;
		switch (this.Source)
		{
		case PhotonVoiceRecorder.AudioSource.Microphone:
			if ((this.MicrophoneType == PhotonVoiceRecorder.MicAudioSourceType.Settings && instance.MicrophoneType == PhotonVoiceSettings.MicAudioSourceType.Photon) || this.MicrophoneType == PhotonVoiceRecorder.MicAudioSourceType.Photon)
			{
				int num = (this.PhotonMicrophoneDeviceID != -1) ? this.PhotonMicrophoneDeviceID : PhotonVoiceNetwork.PhotonMicrophoneDeviceID;
				if (PhotonVoiceSettings.Instance.DebugInfo)
				{
					Debug.LogFormat("PUNVoice: Setting recorder's source to Photon microphone device {0}", new object[]
					{
						num
					});
				}
				this.audioSource = new WindowsAudioInPusher(num);
				if (PhotonVoiceSettings.Instance.DebugInfo)
				{
					Debug.LogFormat("PUNVoice: Setting recorder's source to WindowsAudioInPusher", Array.Empty<object>());
				}
			}
			else
			{
				if (Microphone.devices.Length < 1)
				{
					return LocalVoiceAudio.Dummy;
				}
				string text = (this.MicrophoneDevice != null) ? this.MicrophoneDevice : PhotonVoiceNetwork.MicrophoneDevice;
				if (PhotonVoiceSettings.Instance.DebugInfo)
				{
					Debug.LogFormat("PUNVoice: Setting recorder's source to microphone device {0}", new object[]
					{
						text
					});
				}
				MicWrapper micWrapper = new MicWrapper(text, (int)instance.SamplingRate);
				this.audioSource = micWrapper;
			}
			break;
		case PhotonVoiceRecorder.AudioSource.AudioClip:
			if (this.AudioClip == null)
			{
				Debug.LogErrorFormat("PUNVoice: AudioClip property must be set for AudioClip audio source", Array.Empty<object>());
				return LocalVoiceAudio.Dummy;
			}
			this.audioSource = new AudioClipWrapper(this.AudioClip);
			if (this.LoopAudioClip)
			{
				((AudioClipWrapper)this.audioSource).Loop = true;
			}
			break;
		case PhotonVoiceRecorder.AudioSource.Factory:
			if (PhotonVoiceNetwork.AudioSourceFactory == null)
			{
				Debug.LogErrorFormat("PUNVoice: PhotonVoiceNetwork.AudioSourceFactory must be specified if PhotonVoiceRecorder.Source set to Factory", Array.Empty<object>());
				return LocalVoiceAudio.Dummy;
			}
			this.audioSource = PhotonVoiceNetwork.AudioSourceFactory(this);
			break;
		default:
			Debug.LogErrorFormat("PUNVoice: unknown Source value {0}", new object[]
			{
				this.Source
			});
			return LocalVoiceAudio.Dummy;
		}
		VoiceInfo voiceInfo = VoiceInfo.CreateAudioOpus(instance.SamplingRate, this.audioSource.SamplingRate, this.audioSource.Channels, instance.FrameDuration, instance.Bitrate, base.photonView.viewID);
		return this.createLocalVoiceAudio(voiceInfo, this.audioSource);
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00003D44 File Offset: 0x00001F44
	protected virtual LocalVoice createLocalVoiceAudio(VoiceInfo voiceInfo, IAudioSource source)
	{
		if (source is IAudioPusher<float>)
		{
			if (this.forceShort)
			{
				throw new NotImplementedException("Voice.IAudioPusher<float> at 'short' voice is not supported currently");
			}
			LocalVoiceAudio<float> localVoice = PhotonVoiceNetwork.VoiceClient.CreateLocalVoiceAudio<float>(voiceInfo, -1, null);
			((IAudioPusher<float>)source).SetCallback(delegate(float[] buf)
			{
				localVoice.PushDataAsync(buf);
			}, localVoice);
			localVoice.Encrypt = PhotonVoiceSettings.Instance.Encrypt;
			return localVoice;
		}
		else
		{
			if (source is IAudioPusher<short>)
			{
				LocalVoiceAudio<short> localVoice = PhotonVoiceNetwork.VoiceClient.CreateLocalVoiceAudio<short>(voiceInfo, -1, null);
				((IAudioPusher<short>)source).SetCallback(delegate(short[] buf)
				{
					localVoice.PushDataAsync(buf);
				}, localVoice);
				localVoice.Encrypt = PhotonVoiceSettings.Instance.Encrypt;
				return localVoice;
			}
			if (source is IAudioReader<float>)
			{
				if (this.forceShort)
				{
					if (PhotonVoiceSettings.Instance.DebugInfo)
					{
						Debug.LogFormat("PUNVoice: Creating local voice with source samples type conversion from float to short.", Array.Empty<object>());
					}
					LocalVoiceAudio<short> localVoiceAudio = PhotonVoiceNetwork.VoiceClient.CreateLocalVoiceAudio<short>(voiceInfo, -1, null);
					localVoiceAudio.LocalUserServiceable = new BufferReaderPushAdapterAsyncPoolFloatToShort(localVoiceAudio, source as IAudioReader<float>);
					localVoiceAudio.Encrypt = PhotonVoiceSettings.Instance.Encrypt;
					return localVoiceAudio;
				}
				LocalVoiceAudio<float> localVoiceAudio2 = PhotonVoiceNetwork.VoiceClient.CreateLocalVoiceAudio<float>(voiceInfo, -1, null);
				localVoiceAudio2.LocalUserServiceable = new BufferReaderPushAdapterAsyncPool<float>(localVoiceAudio2, source as IAudioReader<float>);
				localVoiceAudio2.Encrypt = PhotonVoiceSettings.Instance.Encrypt;
				return localVoiceAudio2;
			}
			else
			{
				if (source is IAudioReader<short>)
				{
					LocalVoiceAudio<short> localVoiceAudio3 = PhotonVoiceNetwork.VoiceClient.CreateLocalVoiceAudio<short>(voiceInfo, -1, null);
					localVoiceAudio3.LocalUserServiceable = new BufferReaderPushAdapterAsyncPool<short>(localVoiceAudio3, source as IAudioReader<short>);
					localVoiceAudio3.Encrypt = PhotonVoiceSettings.Instance.Encrypt;
					return localVoiceAudio3;
				}
				Debug.LogErrorFormat("PUNVoice: PhotonVoiceRecorder createLocalVoiceAudio does not support Voice.IAudioReader of type {0}", new object[]
				{
					source.GetType()
				});
				return LocalVoiceAudio.Dummy;
			}
		}
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00003EFC File Offset: 0x000020FC
	protected virtual void sendPhotonVoiceCreatedMessage()
	{
		base.gameObject.SendMessage("PhotonVoiceCreated", new PhotonVoiceRecorder.PhotonVoiceCreatedParams
		{
			Voice = this.voice,
			AudioSource = this.audioSource
		}, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00003F2C File Offset: 0x0000212C
	private void OnDestroy()
	{
		if (this.voice != LocalVoiceAudio.Dummy)
		{
			this.voice.RemoveSelf();
			if (this.audioSource != null)
			{
				this.audioSource.Dispose();
				this.audioSource = null;
			}
		}
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00003F60 File Offset: 0x00002160
	private void OnEnable()
	{
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000070 RID: 112 RVA: 0x00003F62 File Offset: 0x00002162
	// (set) Token: 0x06000071 RID: 113 RVA: 0x00003F6F File Offset: 0x0000216F
	public bool Transmit
	{
		get
		{
			return this.voice.Transmit;
		}
		set
		{
			this.voice.Transmit = value;
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x06000072 RID: 114 RVA: 0x00003F7D File Offset: 0x0000217D
	// (set) Token: 0x06000073 RID: 115 RVA: 0x00003F8F File Offset: 0x0000218F
	public bool Detect
	{
		get
		{
			return this.voiceAudio.VoiceDetector.On;
		}
		set
		{
			this.voiceAudio.VoiceDetector.On = value;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x06000074 RID: 116 RVA: 0x00003FA2 File Offset: 0x000021A2
	// (set) Token: 0x06000075 RID: 117 RVA: 0x00003FAF File Offset: 0x000021AF
	public bool DebugEchoMode
	{
		get
		{
			return this.voice.DebugEchoMode;
		}
		set
		{
			this.voice.DebugEchoMode = value;
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003FBD File Offset: 0x000021BD
	public void VoiceDetectorCalibrate(int durationMs)
	{
		if (base.photonView.isMine)
		{
			this.voiceAudio.VoiceDetectorCalibrate(durationMs);
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000077 RID: 119 RVA: 0x00003FD8 File Offset: 0x000021D8
	public bool VoiceDetectorCalibrating
	{
		get
		{
			return this.voiceAudio.VoiceDetectorCalibrating;
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00003FE8 File Offset: 0x000021E8
	private string tostr<T>(T[] x, int lim = 10)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < ((x.Length < lim) ? x.Length : lim); i++)
		{
			stringBuilder.Append("-");
			stringBuilder.Append(x[i]);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00004038 File Offset: 0x00002238
	public string ToStringFull()
	{
		int num = 0;
		int num2 = 0;
		Microphone.GetDeviceCaps(this.MicrophoneDevice, out num, out num2);
		return string.Format("Mic '{0}': {1}..{2} Hz", this.MicrophoneDevice, num, num2);
	}

	// Token: 0x04000046 RID: 70
	private LocalVoice voice = LocalVoiceAudio.Dummy;

	// Token: 0x04000047 RID: 71
	private string microphoneDevice;

	// Token: 0x04000048 RID: 72
	private int photonMicrophoneDeviceID = -1;

	// Token: 0x04000049 RID: 73
	private IAudioSource audioSource;

	// Token: 0x0400004A RID: 74
	public PhotonVoiceRecorder.AudioSource Source;

	// Token: 0x0400004B RID: 75
	public PhotonVoiceRecorder.MicAudioSourceType MicrophoneType;

	// Token: 0x0400004C RID: 76
	public PhotonVoiceRecorder.SampleTypeConv TypeConvert;

	// Token: 0x0400004D RID: 77
	private bool forceShort;

	// Token: 0x0400004E RID: 78
	public AudioClip AudioClip;

	// Token: 0x0400004F RID: 79
	public bool LoopAudioClip = true;

	// Token: 0x020004C7 RID: 1223
	public enum AudioSource
	{
		// Token: 0x04002354 RID: 9044
		Microphone,
		// Token: 0x04002355 RID: 9045
		AudioClip,
		// Token: 0x04002356 RID: 9046
		Factory
	}

	// Token: 0x020004C8 RID: 1224
	public enum MicAudioSourceType
	{
		// Token: 0x04002358 RID: 9048
		Settings,
		// Token: 0x04002359 RID: 9049
		Unity,
		// Token: 0x0400235A RID: 9050
		Photon
	}

	// Token: 0x020004C9 RID: 1225
	public enum SampleTypeConv
	{
		// Token: 0x0400235C RID: 9052
		None,
		// Token: 0x0400235D RID: 9053
		Short,
		// Token: 0x0400235E RID: 9054
		ShortAuto
	}

	// Token: 0x020004CA RID: 1226
	public class PhotonVoiceCreatedParams
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x000BD828 File Offset: 0x000BBA28
		// (set) Token: 0x060025EA RID: 9706 RVA: 0x000BD830 File Offset: 0x000BBA30
		public LocalVoice Voice { get; internal set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x000BD839 File Offset: 0x000BBA39
		// (set) Token: 0x060025EC RID: 9708 RVA: 0x000BD841 File Offset: 0x000BBA41
		public IAudioSource AudioSource { get; internal set; }
	}
}
