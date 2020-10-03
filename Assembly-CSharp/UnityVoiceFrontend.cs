using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.LoadBalancing;
using ExitGames.Client.Photon.Voice;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class UnityVoiceFrontend : LoadBalancingFrontend
{
	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000049 RID: 73 RVA: 0x00003239 File Offset: 0x00001439
	// (set) Token: 0x0600004A RID: 74 RVA: 0x00003241 File Offset: 0x00001441
	public Action<int, byte, VoiceInfo> OnRemoteVoiceInfoAction { get; set; }

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x0600004B RID: 75 RVA: 0x0000324A File Offset: 0x0000144A
	// (set) Token: 0x0600004C RID: 76 RVA: 0x00003252 File Offset: 0x00001452
	public Action<int, byte> OnRemoteVoiceRemoveAction { get; set; }

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x0600004D RID: 77 RVA: 0x0000325B File Offset: 0x0000145B
	// (set) Token: 0x0600004E RID: 78 RVA: 0x00003263 File Offset: 0x00001463
	public Action<int, byte, float[]> OnAudioFrameAction { get; set; }

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x0600004F RID: 79 RVA: 0x0000326C File Offset: 0x0000146C
	// (set) Token: 0x06000050 RID: 80 RVA: 0x00003274 File Offset: 0x00001474
	public new Action<ExitGames.Client.Photon.LoadBalancing.ClientState> OnStateChangeAction { get; set; }

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000051 RID: 81 RVA: 0x0000327D File Offset: 0x0000147D
	// (set) Token: 0x06000052 RID: 82 RVA: 0x00003285 File Offset: 0x00001485
	public new Action<OperationResponse> OnOpResponseAction { get; set; }

	// Token: 0x06000053 RID: 83 RVA: 0x00003290 File Offset: 0x00001490
	internal UnityVoiceFrontend(ConnectionProtocol connetProtocol) : base(connetProtocol)
	{
		VoiceClient voiceClient = this.voiceClient;
		voiceClient.OnRemoteVoiceInfoAction = (VoiceClient.RemoteVoiceInfoDelegate)Delegate.Combine(voiceClient.OnRemoteVoiceInfoAction, new VoiceClient.RemoteVoiceInfoDelegate(this.OnRemoteVoiceInfo));
		base.AutoJoinLobby = false;
		base.OnStateChangeAction = (Action<ExitGames.Client.Photon.LoadBalancing.ClientState>)Delegate.Combine(base.OnStateChangeAction, new Action<ExitGames.Client.Photon.LoadBalancing.ClientState>(this.OnStateChange));
		base.OnOpResponseAction += this.OnOpResponse;
		this.loadBalancingPeer.DebugOut = DebugLevel.INFO;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x0000331D File Offset: 0x0000151D
	public void Reconnect()
	{
		if (base.State == ExitGames.Client.Photon.LoadBalancing.ClientState.Disconnected || base.State == ExitGames.Client.Photon.LoadBalancing.ClientState.PeerCreated)
		{
			PhotonVoiceNetwork.Connect();
			return;
		}
		this.reconnect = true;
		base.Disconnect();
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003348 File Offset: 0x00001548
	public override void DebugReturn(DebugLevel level, string message)
	{
		message = string.Format("PUNVoice: {0}", message);
		if (level == DebugLevel.ERROR)
		{
			Debug.LogError(message);
			return;
		}
		if (level == DebugLevel.WARNING)
		{
			Debug.LogWarning(message);
			return;
		}
		if (level == DebugLevel.INFO && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
		{
			Debug.Log(message);
			return;
		}
		if (level == DebugLevel.ALL && PhotonNetwork.logLevel == PhotonLogLevel.Full)
		{
			Debug.Log(message);
		}
	}

	// Token: 0x06000056 RID: 86 RVA: 0x0000339D File Offset: 0x0000159D
	public void OnOpResponse(OperationResponse resp)
	{
		if (this.OnOpResponseAction != null)
		{
			this.OnOpResponseAction(resp);
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x000033B4 File Offset: 0x000015B4
	private void linkVoice(int playerId, byte voiceId, VoiceInfo voiceInfo, PhotonVoiceSpeaker speaker)
	{
		speaker.OnVoiceLinked(voiceInfo.SamplingRate, voiceInfo.Channels, voiceInfo.FrameDurationSamples, PhotonVoiceSettings.Instance.PlayDelayMs);
		KeyValuePair<int, byte> key = new KeyValuePair<int, byte>(playerId, voiceId);
		PhotonVoiceSpeaker x;
		if (this.voiceSpeakers.TryGetValue(key, out x))
		{
			if (x == speaker)
			{
				return;
			}
			Debug.LogFormat("PUNVoice: Player {0} voice #{1} speaker replaced.", new object[]
			{
				playerId,
				voiceId
			});
		}
		else
		{
			Debug.LogFormat("PUNVoice: Player {0} voice #{1} speaker created.", new object[]
			{
				playerId,
				voiceId
			});
		}
		this.voiceSpeakers[key] = speaker;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003460 File Offset: 0x00001660
	public void OnRemoteVoiceInfo(int channelId, int playerId, byte voiceId, VoiceInfo voiceInfo, ref RemoteVoiceOptions options)
	{
		options.OnDecodedFrameFloatAction = (Action<float[]>)Delegate.Combine(options.OnDecodedFrameFloatAction, new Action<float[]>(delegate(float[] frame)
		{
			this.OnAudioFrame(playerId, voiceId, frame);
		}));
		options.OnRemoteVoiceRemoveAction = (Action)Delegate.Combine(options.OnRemoteVoiceRemoveAction, new Action(delegate()
		{
			this.OnRemoteVoiceRemove(playerId, voiceId);
		}));
		KeyValuePair<int, byte> key = new KeyValuePair<int, byte>(playerId, voiceId);
		if (this.voiceSpeakers.ContainsKey(key))
		{
			Debug.LogWarningFormat("PUNVoice: Info duplicate for voice #{0} of player {1}", new object[]
			{
				voiceId,
				playerId
			});
		}
		PhotonVoiceSpeaker photonVoiceSpeaker = null;
		foreach (PhotonVoiceSpeaker photonVoiceSpeaker2 in Object.FindObjectsOfType<PhotonVoiceSpeaker>())
		{
			if (photonVoiceSpeaker2.photonView.viewID == (int)voiceInfo.UserData)
			{
				photonVoiceSpeaker = photonVoiceSpeaker2;
				break;
			}
		}
		if (!(photonVoiceSpeaker == null))
		{
			this.linkVoice(playerId, voiceId, voiceInfo, photonVoiceSpeaker);
		}
		if (this.OnRemoteVoiceInfoAction != null)
		{
			this.OnRemoteVoiceInfoAction(playerId, voiceId, voiceInfo);
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x0000359C File Offset: 0x0000179C
	public void LinkSpeakerToRemoteVoice(PhotonVoiceSpeaker speaker)
	{
		foreach (RemoteVoiceInfo remoteVoiceInfo in base.VoiceClient.RemoteVoiceInfos)
		{
			if (speaker.photonView.viewID == (int)remoteVoiceInfo.Info.UserData)
			{
				this.linkVoice(remoteVoiceInfo.PlayerId, remoteVoiceInfo.VoiceId, remoteVoiceInfo.Info, speaker);
			}
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00003620 File Offset: 0x00001820
	public void OnRemoteVoiceRemove(int playerId, byte voiceId)
	{
		KeyValuePair<int, byte> key = new KeyValuePair<int, byte>(playerId, voiceId);
		if (!this.unlinkSpeaker(key))
		{
			Debug.LogWarningFormat("PUNVoice: Voice #{0} of player {1} not found.", new object[]
			{
				voiceId,
				playerId
			});
		}
		else
		{
			Debug.LogFormat("PUNVoice: Player {0} voice # {1} speaker unlinked.", new object[]
			{
				playerId,
				voiceId
			});
		}
		if (this.OnRemoteVoiceRemoveAction != null)
		{
			this.OnRemoteVoiceRemoveAction(playerId, voiceId);
		}
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000369C File Offset: 0x0000189C
	private bool unlinkSpeaker(KeyValuePair<int, byte> key)
	{
		PhotonVoiceSpeaker photonVoiceSpeaker;
		if (this.voiceSpeakers.TryGetValue(key, out photonVoiceSpeaker))
		{
			photonVoiceSpeaker.OnVoiceUnlinked();
		}
		return this.voiceSpeakers.Remove(key);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000036CC File Offset: 0x000018CC
	public void UnlinkSpeakerFromRemoteVoice(PhotonVoiceSpeaker speaker)
	{
		List<KeyValuePair<int, byte>> list = new List<KeyValuePair<int, byte>>();
		foreach (KeyValuePair<KeyValuePair<int, byte>, PhotonVoiceSpeaker> keyValuePair in this.voiceSpeakers)
		{
			if (keyValuePair.Value == speaker)
			{
				list.Add(keyValuePair.Key);
				Debug.LogFormat("PUNVoice: Player {0} voice # {1} speaker unlinked.", new object[]
				{
					keyValuePair.Key.Key,
					keyValuePair.Key.Value
				});
			}
		}
		foreach (KeyValuePair<int, byte> key in list)
		{
			this.unlinkSpeaker(key);
		}
	}

	// Token: 0x0600005D RID: 93 RVA: 0x000037BC File Offset: 0x000019BC
	public void OnAudioFrame(int playerId, byte voiceId, float[] frame)
	{
		PhotonVoiceSpeaker photonVoiceSpeaker = null;
		if (this.voiceSpeakers.TryGetValue(new KeyValuePair<int, byte>(playerId, voiceId), out photonVoiceSpeaker))
		{
			photonVoiceSpeaker.OnAudioFrame(frame);
		}
		else
		{
			Debug.LogWarningFormat("PUNVoice: Audio Frame event for not existing speaker for voice #{0} of player {1}.", new object[]
			{
				voiceId,
				playerId
			});
		}
		if (this.OnAudioFrameAction != null)
		{
			this.OnAudioFrameAction(playerId, voiceId, frame);
		}
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00003824 File Offset: 0x00001A24
	public void OnStateChange(ExitGames.Client.Photon.LoadBalancing.ClientState state)
	{
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
		{
			Debug.LogFormat("PUNVoice: Voice Client state: {0}", new object[]
			{
				state
			});
		}
		if (state != ExitGames.Client.Photon.LoadBalancing.ClientState.Disconnected)
		{
			if (state == ExitGames.Client.Photon.LoadBalancing.ClientState.ConnectedToMasterserver)
			{
				if (PhotonNetwork.inRoom)
				{
					base.OpJoinOrCreateRoom(string.Format("{0}_voice_", PhotonNetwork.room.Name), new ExitGames.Client.Photon.LoadBalancing.RoomOptions
					{
						IsVisible = false
					}, null, null);
				}
				else
				{
					Debug.LogWarning("PUNVoice: PUN client is not in room yet. Disconnecting voice client.");
					base.Disconnect();
				}
			}
		}
		else
		{
			if (this.reconnect)
			{
				PhotonVoiceNetwork.Connect();
			}
			this.reconnect = false;
		}
		if (this.OnStateChangeAction != null)
		{
			this.OnStateChangeAction(state);
		}
	}

	// Token: 0x0400003F RID: 63
	private Dictionary<KeyValuePair<int, byte>, PhotonVoiceSpeaker> voiceSpeakers = new Dictionary<KeyValuePair<int, byte>, PhotonVoiceSpeaker>();

	// Token: 0x04000045 RID: 69
	private bool reconnect;
}
