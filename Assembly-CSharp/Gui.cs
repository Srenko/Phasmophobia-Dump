using System;
using ExitGames.Client.Photon.LoadBalancing;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class Gui : MonoBehaviour
{
	// Token: 0x06000091 RID: 145 RVA: 0x000043A8 File Offset: 0x000025A8
	private void Start()
	{
		PhotonNetwork.networkingPeer.TrafficStatsEnabled = true;
		PhotonVoiceNetwork.Client.loadBalancingPeer.TrafficStatsEnabled = true;
		PhotonVoiceNetwork.AudioSourceFactory = ((PhotonVoiceRecorder rec) => new MicWrapper((rec.MicrophoneDevice != null) ? rec.MicrophoneDevice : PhotonVoiceNetwork.MicrophoneDevice, (int)PhotonVoiceSettings.Instance.SamplingRate));
	}

	// Token: 0x06000092 RID: 146 RVA: 0x000043F4 File Offset: 0x000025F4
	private void OnGUI()
	{
		PhotonVoiceRecorder photonVoiceRecorder = null;
		foreach (PhotonVoiceRecorder photonVoiceRecorder2 in Object.FindObjectsOfType<PhotonVoiceRecorder>())
		{
			if (photonVoiceRecorder2.photonView.isMine)
			{
				photonVoiceRecorder = photonVoiceRecorder2;
				break;
			}
		}
		GUIStyle guistyle = new GUIStyle("label");
		guistyle.fontSize = 24 * Screen.height / 600;
		guistyle.wordWrap = false;
		GUIStyle guistyle2 = new GUIStyle("label");
		guistyle2.fontSize = 16 * Screen.height / 600;
		guistyle2.wordWrap = false;
		GUIStyle guistyle3 = new GUIStyle("button");
		guistyle3.fontSize = 28 * Screen.height / 600;
		GUIStyle guistyle4 = new GUIStyle("button");
		guistyle4.fontSize = 16 * Screen.height / 600;
		string text = "";
		if (PhotonNetwork.inRoom)
		{
			text = PhotonNetwork.room.Name;
		}
		string text2 = string.Format("RTT/Var/Que: {0}/{1}/{2}", PhotonNetwork.networkingPeer.RoundTripTime.ToString(), PhotonNetwork.networkingPeer.RoundTripTimeVariance, PhotonNetwork.networkingPeer.QueuedIncomingCommands);
		GUILayout.Label(string.Concat(new string[]
		{
			"PUN: ",
			PhotonNetwork.connectionStateDetailed.ToString(),
			" ",
			text,
			" ",
			text2
		}), guistyle, Array.Empty<GUILayoutOption>());
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Connect", guistyle3, Array.Empty<GUILayoutOption>()))
		{
			PhotonNetwork.ConnectUsingSettings(string.Format("1.{0}", SceneManagerHelper.ActiveSceneBuildIndex));
		}
		if (GUILayout.Button("Disconnect", guistyle3, Array.Empty<GUILayoutOption>()))
		{
			PhotonNetwork.Disconnect();
		}
		GUILayout.EndHorizontal();
		text = "";
		if (PhotonVoiceNetwork.ClientState == ExitGames.Client.Photon.LoadBalancing.ClientState.Joined)
		{
			text = PhotonVoiceNetwork.CurrentRoomName;
		}
		if (this.dataRateNextTime < Time.time)
		{
			this.dataRateNextTime = Time.time + 1f;
			this.dataRateIn = (PhotonVoiceNetwork.Client.loadBalancingPeer.TrafficStatsIncoming.TotalPacketBytes - this.prevInBytes) / 1;
			this.dataRateOut = (PhotonVoiceNetwork.Client.loadBalancingPeer.TrafficStatsOutgoing.TotalPacketBytes - this.prevOutBytes) / 1;
			this.prevInBytes = PhotonVoiceNetwork.Client.loadBalancingPeer.TrafficStatsIncoming.TotalPacketBytes;
			this.prevOutBytes = PhotonVoiceNetwork.Client.loadBalancingPeer.TrafficStatsOutgoing.TotalPacketBytes;
		}
		text2 = string.Format("RTT/Var/Que: {0}/{1}/{2}", PhotonVoiceNetwork.Client.loadBalancingPeer.RoundTripTime.ToString(), PhotonVoiceNetwork.Client.loadBalancingPeer.RoundTripTimeVariance, PhotonVoiceNetwork.Client.loadBalancingPeer.QueuedIncomingCommands);
		GUILayout.Label(string.Concat(new string[]
		{
			"PhotonVoice: ",
			PhotonVoiceNetwork.ClientState.ToString(),
			" ",
			text,
			" ",
			text2
		}), guistyle, Array.Empty<GUILayoutOption>());
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label(string.Concat(new object[]
		{
			"Data rate in/out bytes/sec: ",
			this.dataRateIn,
			"/",
			this.dataRateOut
		}), guistyle2, Array.Empty<GUILayoutOption>());
		if (PhotonVoiceNetwork.Client.loadBalancingPeer != null)
		{
			GUILayout.Label(string.Concat(new object[]
			{
				"Traffic bytes: ",
				PhotonVoiceNetwork.Client.loadBalancingPeer.TrafficStatsIncoming.TotalPacketBytes,
				"/",
				PhotonVoiceNetwork.Client.loadBalancingPeer.TrafficStatsOutgoing.TotalPacketBytes
			}), guistyle2, Array.Empty<GUILayoutOption>());
		}
		GUILayout.Label(string.Concat(new object[]
		{
			"Frames Sent/Rcvd/Lost: ",
			PhotonVoiceNetwork.VoiceClient.FramesSent,
			"/",
			PhotonVoiceNetwork.VoiceClient.FramesReceived,
			"/",
			PhotonVoiceNetwork.VoiceClient.FramesLost
		}), guistyle2, Array.Empty<GUILayoutOption>());
		GUILayout.Label(string.Concat(new object[]
		{
			"Voice RTT/Var: ",
			PhotonVoiceNetwork.VoiceClient.RoundTripTime,
			"/",
			PhotonVoiceNetwork.VoiceClient.RoundTripTimeVariance
		}), guistyle2, Array.Empty<GUILayoutOption>());
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label("Speakers:", guistyle2, Array.Empty<GUILayoutOption>());
		foreach (PhotonVoiceSpeaker photonVoiceSpeaker in Object.FindObjectsOfType<PhotonVoiceSpeaker>())
		{
			if (photonVoiceSpeaker.IsVoiceLinked)
			{
				GUILayout.Label("lag=" + photonVoiceSpeaker.CurrentBufferLag, guistyle2, Array.Empty<GUILayoutOption>());
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Connect", guistyle3, Array.Empty<GUILayoutOption>()))
		{
			PhotonVoiceNetwork.Connect();
		}
		if (GUILayout.Button("Disconnect", guistyle3, Array.Empty<GUILayoutOption>()))
		{
			PhotonVoiceNetwork.Disconnect();
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		if (photonVoiceRecorder != null && photonVoiceRecorder.photonView.isMine)
		{
			if (GUILayout.Button((photonVoiceRecorder.DebugEchoMode ? "[X] " : "[ ] ") + "Debug Echo", guistyle3, Array.Empty<GUILayoutOption>()))
			{
				photonVoiceRecorder.DebugEchoMode = !photonVoiceRecorder.DebugEchoMode;
			}
			if (GUILayout.Button((photonVoiceRecorder.Transmit ? "[X] " : "[ ] ") + "Transmit", guistyle3, Array.Empty<GUILayoutOption>()))
			{
				photonVoiceRecorder.Transmit = !photonVoiceRecorder.Transmit;
			}
			if (GUILayout.Button((photonVoiceRecorder.VoiceDetector.On ? "[X] " : "[ ] ") + "Detect", guistyle3, Array.Empty<GUILayoutOption>()))
			{
				photonVoiceRecorder.Detect = !photonVoiceRecorder.Detect;
			}
			if (GUILayout.Button((photonVoiceRecorder.VoiceDetectorCalibrating ? "[X] " : "[ ] ") + "Calibrate Detector", guistyle3, Array.Empty<GUILayoutOption>()))
			{
				photonVoiceRecorder.VoiceDetectorCalibrate(2000);
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label("Transmitting: " + photonVoiceRecorder.IsTransmitting.ToString(), guistyle2, Array.Empty<GUILayoutOption>());
			GUILayout.Label("Avg Amp: " + ((photonVoiceRecorder.LevelMeter == null) ? "" : (photonVoiceRecorder.LevelMeter.CurrentAvgAmp.ToString("0.000000") + "/" + photonVoiceRecorder.LevelMeter.AccumAvgPeakAmp.ToString("0.000000"))), guistyle2, Array.Empty<GUILayoutOption>());
			GUILayout.Label("Peak Amp: " + ((photonVoiceRecorder.LevelMeter == null) ? "" : photonVoiceRecorder.LevelMeter.CurrentPeakAmp.ToString("0.000000")), guistyle2, Array.Empty<GUILayoutOption>());
			GUILayout.Label("Detector Threshold: " + ((photonVoiceRecorder.VoiceDetector == null) ? "" : photonVoiceRecorder.VoiceDetector.Threshold.ToString("0.000000")), guistyle2, Array.Empty<GUILayoutOption>());
			GUILayout.Label("Audio group (rec): " + photonVoiceRecorder.AudioGroup.ToString(), guistyle2, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
		}
		else
		{
			GUILayout.EndHorizontal();
		}
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label("Set Group (offs Debug Echo): ", guistyle2, Array.Empty<GUILayoutOption>());
		for (byte b = 0; b < 5; b += 1)
		{
			if (GUILayout.Button(((PhotonVoiceNetwork.Client.GlobalAudioGroup == b) ? "[X] " : "[ ] ") + ((b == 0) ? "No" : b.ToString()), guistyle4, Array.Empty<GUILayoutOption>()))
			{
				PhotonVoiceNetwork.Client.GlobalAudioGroup = b;
			}
		}
		GUILayout.EndHorizontal();
		PhotonVoiceSettings instance = PhotonVoiceSettings.Instance;
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label("Unity Mic: ", guistyle2, Array.Empty<GUILayoutOption>());
		foreach (string text3 in Microphone.devices)
		{
			if (GUILayout.Button(((instance.MicrophoneType == PhotonVoiceSettings.MicAudioSourceType.Unity && PhotonVoiceNetwork.MicrophoneDevice == text3) ? "[X] " : "[ ] ") + text3, guistyle4, Array.Empty<GUILayoutOption>()))
			{
				instance.MicrophoneType = PhotonVoiceSettings.MicAudioSourceType.Unity;
				PhotonVoiceNetwork.MicrophoneDevice = text3;
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label("Photon Mic: ", guistyle2, Array.Empty<GUILayoutOption>());
		if (PhotonVoiceNetwork.PhotonMicrophoneEnumerator.IsSupported)
		{
			for (int j = 0; j < PhotonVoiceNetwork.PhotonMicrophoneEnumerator.Count; j++)
			{
				if (GUILayout.Button(((instance.MicrophoneType == PhotonVoiceSettings.MicAudioSourceType.Photon && PhotonVoiceNetwork.PhotonMicrophoneDeviceID == PhotonVoiceNetwork.PhotonMicrophoneEnumerator.IDAtIndex(j)) ? "[X] " : "[ ] ") + PhotonVoiceNetwork.PhotonMicrophoneEnumerator.NameAtIndex(j), guistyle4, Array.Empty<GUILayoutOption>()))
				{
					instance.MicrophoneType = PhotonVoiceSettings.MicAudioSourceType.Photon;
					PhotonVoiceNetwork.PhotonMicrophoneDeviceID = PhotonVoiceNetwork.PhotonMicrophoneEnumerator.IDAtIndex(j);
				}
			}
			if (GUILayout.Button("Refresh", guistyle4, Array.Empty<GUILayoutOption>()))
			{
				PhotonVoiceNetwork.PhotonMicrophoneEnumerator.Refresh();
			}
		}
		else if (Microphone.devices.Length != 0 && GUILayout.Button(((instance.MicrophoneType == PhotonVoiceSettings.MicAudioSourceType.Photon) ? "[X] " : "[ ] ") + Microphone.devices[0], guistyle4, Array.Empty<GUILayoutOption>()))
		{
			instance.MicrophoneType = PhotonVoiceSettings.MicAudioSourceType.Photon;
			PhotonVoiceNetwork.PhotonMicrophoneDeviceID = -1;
		}
		GUILayout.EndHorizontal();
		GUI.enabled = true;
	}

	// Token: 0x04000063 RID: 99
	private float dataRateNextTime;

	// Token: 0x04000064 RID: 100
	private int prevInBytes;

	// Token: 0x04000065 RID: 101
	private int prevOutBytes;

	// Token: 0x04000066 RID: 102
	private int dataRateIn;

	// Token: 0x04000067 RID: 103
	private int dataRateOut;
}
