using System;
using ExitGames.Client.Photon.Voice;
using Photon;
using UnityEngine;

// Token: 0x0200000E RID: 14
[RequireComponent(typeof(AudioSource))]
[DisallowMultipleComponent]
[AddComponentMenu("Photon Voice/Photon Voice Speaker")]
public class PhotonVoiceSpeaker : Photon.MonoBehaviour
{
	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000080 RID: 128 RVA: 0x00004203 File Offset: 0x00002403
	// (set) Token: 0x06000081 RID: 129 RVA: 0x0000420B File Offset: 0x0000240B
	public long LastRecvTime { get; private set; }

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000082 RID: 130 RVA: 0x00004214 File Offset: 0x00002414
	public bool IsPlaying
	{
		get
		{
			return this.player.IsPlaying;
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000083 RID: 131 RVA: 0x00004221 File Offset: 0x00002421
	public int CurrentBufferLag
	{
		get
		{
			return this.player.CurrentBufferLag;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000084 RID: 132 RVA: 0x0000422E File Offset: 0x0000242E
	public bool IsVoiceLinked
	{
		get
		{
			return this.player != null && this.started;
		}
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00004240 File Offset: 0x00002440
	private void Awake()
	{
		this.player = new AudioStreamPlayer(base.GetComponent<AudioSource>(), "PUNVoice: PhotonVoiceSpeaker:", PhotonVoiceSettings.Instance.DebugInfo);
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00004262 File Offset: 0x00002462
	private void Start()
	{
		PhotonVoiceNetwork.LinkSpeakerToRemoteVoice(this);
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000426A File Offset: 0x0000246A
	internal void OnVoiceLinked(int frequency, int channels, int frameSamplesPerChannel, int playDelayMs)
	{
		this.player.Start(frequency, channels, frameSamplesPerChannel, playDelayMs);
		this.started = true;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00004283 File Offset: 0x00002483
	internal void OnVoiceUnlinked()
	{
		this.Cleanup();
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0000428B File Offset: 0x0000248B
	private void Update()
	{
		this.player.Service();
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00004298 File Offset: 0x00002498
	private void OnDestroy()
	{
		PhotonVoiceNetwork.UnlinkSpeakerFromRemoteVoice(this);
		this.Cleanup();
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00004283 File Offset: 0x00002483
	private void OnApplicationQuit()
	{
		this.Cleanup();
	}

	// Token: 0x0600008C RID: 140 RVA: 0x000042A6 File Offset: 0x000024A6
	private void Cleanup()
	{
		this.player.Stop();
		this.started = false;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x000042BC File Offset: 0x000024BC
	internal void OnAudioFrame(float[] frame)
	{
		this.LastRecvTime = DateTime.Now.Ticks;
		this.player.OnAudioFrame(frame);
	}

	// Token: 0x04000060 RID: 96
	private IAudioOut player;

	// Token: 0x04000061 RID: 97
	private bool started;
}
