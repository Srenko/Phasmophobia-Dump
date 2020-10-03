using System;
using ExitGames.Client.Photon.Voice;
using POpusCodec.Enums;
using UnityEngine;

// Token: 0x0200000D RID: 13
[DisallowMultipleComponent]
public class PhotonVoiceSettings : MonoBehaviour
{
	// Token: 0x17000021 RID: 33
	// (get) Token: 0x0600007B RID: 123 RVA: 0x00004098 File Offset: 0x00002298
	// (set) Token: 0x0600007C RID: 124 RVA: 0x00004110 File Offset: 0x00002310
	public static PhotonVoiceSettings Instance
	{
		get
		{
			object obj = PhotonVoiceSettings.instanceLock;
			PhotonVoiceSettings result;
			lock (obj)
			{
				if (PhotonVoiceSettings.instance == null)
				{
					PhotonVoiceSettings x = Object.FindObjectOfType<PhotonVoiceSettings>();
					if (x != null)
					{
						PhotonVoiceSettings.instance = x;
					}
					else
					{
						PhotonVoiceSettings.instance = PhotonVoiceNetwork.instance.gameObject.AddComponent<PhotonVoiceSettings>();
					}
				}
				result = PhotonVoiceSettings.instance;
			}
			return result;
		}
		private set
		{
			object obj = PhotonVoiceSettings.instanceLock;
			lock (obj)
			{
				if (PhotonVoiceSettings.instance != null && value != null)
				{
					if (PhotonVoiceSettings.instance.GetInstanceID() != value.GetInstanceID())
					{
						Debug.LogErrorFormat("PUNVoice: Destroying a duplicate instance of PhotonVoiceSettings as only one is allowed.", Array.Empty<object>());
						Object.Destroy(value);
					}
				}
				else
				{
					PhotonVoiceSettings.instance = value;
				}
			}
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00004190 File Offset: 0x00002390
	private void Awake()
	{
		PhotonVoiceSettings.Instance = this;
	}

	// Token: 0x04000050 RID: 80
	public bool WorkInOfflineMode;

	// Token: 0x04000051 RID: 81
	public bool AutoConnect = true;

	// Token: 0x04000052 RID: 82
	public bool AutoDisconnect = true;

	// Token: 0x04000053 RID: 83
	public bool AutoTransmit = true;

	// Token: 0x04000054 RID: 84
	public SamplingRate SamplingRate = SamplingRate.Sampling24000;

	// Token: 0x04000055 RID: 85
	public OpusCodec.FrameDuration FrameDuration = OpusCodec.FrameDuration.Frame20ms;

	// Token: 0x04000056 RID: 86
	public int Bitrate = 30000;

	// Token: 0x04000057 RID: 87
	public bool VoiceDetection;

	// Token: 0x04000058 RID: 88
	public float VoiceDetectionThreshold = 0.01f;

	// Token: 0x04000059 RID: 89
	public int PlayDelayMs = 200;

	// Token: 0x0400005A RID: 90
	public PhotonVoiceSettings.MicAudioSourceType MicrophoneType;

	// Token: 0x0400005B RID: 91
	public int DebugLostPercent;

	// Token: 0x0400005C RID: 92
	public bool DebugInfo;

	// Token: 0x0400005D RID: 93
	public bool Encrypt;

	// Token: 0x0400005E RID: 94
	private static PhotonVoiceSettings instance;

	// Token: 0x0400005F RID: 95
	private static object instanceLock = new object();

	// Token: 0x020004CD RID: 1229
	public enum MicAudioSourceType
	{
		// Token: 0x04002364 RID: 9060
		Unity,
		// Token: 0x04002365 RID: 9061
		Photon
	}
}
