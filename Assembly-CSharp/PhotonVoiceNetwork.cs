using System;
using System.Linq;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.LoadBalancing;
using ExitGames.Client.Photon.Voice;
using UnityEngine;

// Token: 0x0200000A RID: 10
[DisallowMultipleComponent]
public class PhotonVoiceNetwork : MonoBehaviour
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600002D RID: 45 RVA: 0x00002C50 File Offset: 0x00000E50
	// (set) Token: 0x0600002E RID: 46 RVA: 0x00002CE0 File Offset: 0x00000EE0
	internal static PhotonVoiceNetwork instance
	{
		get
		{
			object obj = PhotonVoiceNetwork.instanceLock;
			PhotonVoiceNetwork result;
			lock (obj)
			{
				if (PhotonVoiceNetwork.destroyed)
				{
					result = null;
				}
				else
				{
					if (PhotonVoiceNetwork._instance == null)
					{
						PhotonVoiceNetwork photonVoiceNetwork = Object.FindObjectOfType<PhotonVoiceNetwork>();
						if (photonVoiceNetwork != null)
						{
							PhotonVoiceNetwork._instance = photonVoiceNetwork;
						}
						else
						{
							GameObject gameObject = new GameObject();
							PhotonVoiceNetwork._instance = gameObject.AddComponent<PhotonVoiceNetwork>();
							gameObject.name = "PhotonVoiceNetworkSingleton";
							Object.DontDestroyOnLoad(gameObject);
						}
					}
					result = PhotonVoiceNetwork._instance;
				}
			}
			return result;
		}
		set
		{
			object obj = PhotonVoiceNetwork.instanceLock;
			lock (obj)
			{
				if (PhotonVoiceNetwork._instance != null && value != null)
				{
					if (PhotonVoiceNetwork._instance.GetInstanceID() != value.GetInstanceID())
					{
						Debug.LogErrorFormat("PUNVoice: Destroying a duplicate instance of PhotonVoiceNetwork as only one is allowed.", Array.Empty<object>());
						Object.Destroy(value);
					}
				}
				else
				{
					PhotonVoiceNetwork._instance = value;
				}
			}
		}
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002D60 File Offset: 0x00000F60
	private void OnDestroy()
	{
		if (this != PhotonVoiceNetwork._instance)
		{
			return;
		}
		PhotonVoiceNetwork.destroyed = true;
		this.photonMicEnumerator.Dispose();
		this.client.Dispose();
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002D8C File Offset: 0x00000F8C
	private PhotonVoiceNetwork()
	{
		this.client = new UnityVoiceFrontend(ConnectionProtocol.Udp);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002DB2 File Offset: 0x00000FB2
	[RuntimeInitializeOnLoadMethod]
	public static void RuntimeInitializeOnLoad()
	{
		if (Microphone.devices.Length < 1)
		{
			Debug.LogError("PUNVoice: No microphone device found");
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002DC8 File Offset: 0x00000FC8
	private void Awake()
	{
		PhotonVoiceNetwork.instance = this;
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000033 RID: 51 RVA: 0x00002DD0 File Offset: 0x00000FD0
	// (set) Token: 0x06000034 RID: 52 RVA: 0x00002DD7 File Offset: 0x00000FD7
	public static Func<PhotonVoiceRecorder, IAudioSource> AudioSourceFactory { get; set; }

	// Token: 0x06000035 RID: 53 RVA: 0x00002DE0 File Offset: 0x00000FE0
	public static bool Connect()
	{
		if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.SelfHosted)
		{
			string masterServerAddress = string.Format("{0}:{1}", PhotonNetwork.PhotonServerSettings.ServerAddress, PhotonNetwork.PhotonServerSettings.VoiceServerPort);
			if (PhotonVoiceNetwork.Client.AuthValues == null)
			{
				PhotonVoiceNetwork.Client.AuthValues = new ExitGames.Client.Photon.LoadBalancing.AuthenticationValues();
			}
			if (string.IsNullOrEmpty(PhotonVoiceNetwork.Client.AuthValues.UserId))
			{
				if (PhotonNetwork.AuthValues != null && !string.IsNullOrEmpty(PhotonNetwork.AuthValues.UserId))
				{
					PhotonVoiceNetwork.Client.AuthValues.UserId = PhotonNetwork.AuthValues.UserId;
				}
				else
				{
					PhotonVoiceNetwork.Client.AuthValues.UserId = Guid.NewGuid().ToString();
				}
			}
			return PhotonVoiceNetwork.instance.client.Connect(masterServerAddress, null, null, null, PhotonVoiceNetwork.Client.AuthValues);
		}
		PhotonVoiceNetwork.instance.client.AppId = PhotonNetwork.PhotonServerSettings.VoiceAppID;
		PhotonVoiceNetwork.instance.client.AppVersion = PhotonNetwork.gameVersion;
		return PhotonVoiceNetwork.instance.client.ConnectToRegionMaster(PhotonNetwork.networkingPeer.CloudRegion.ToString());
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002F19 File Offset: 0x00001119
	public static void Disconnect()
	{
		PhotonVoiceNetwork.instance.client.Disconnect();
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000037 RID: 55 RVA: 0x00002F2A File Offset: 0x0000112A
	public static UnityVoiceFrontend Client
	{
		get
		{
			return PhotonVoiceNetwork.instance.client;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000038 RID: 56 RVA: 0x00002F36 File Offset: 0x00001136
	public static VoiceClient VoiceClient
	{
		get
		{
			return PhotonVoiceNetwork.instance.client.VoiceClient;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000039 RID: 57 RVA: 0x00002F47 File Offset: 0x00001147
	public static ExitGames.Client.Photon.LoadBalancing.ClientState ClientState
	{
		get
		{
			return PhotonVoiceNetwork.instance.client.State;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600003A RID: 58 RVA: 0x00002F58 File Offset: 0x00001158
	public static string CurrentRoomName
	{
		get
		{
			if (PhotonVoiceNetwork.instance.client.CurrentRoom != null)
			{
				return PhotonVoiceNetwork.instance.client.CurrentRoom.Name;
			}
			return "";
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600003B RID: 59 RVA: 0x00002F85 File Offset: 0x00001185
	public static AudioInEnumerator PhotonMicrophoneEnumerator
	{
		get
		{
			return PhotonVoiceNetwork.instance.photonMicEnumerator;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600003C RID: 60 RVA: 0x00002F91 File Offset: 0x00001191
	// (set) Token: 0x0600003D RID: 61 RVA: 0x00002FA0 File Offset: 0x000011A0
	public static string MicrophoneDevice
	{
		get
		{
			return PhotonVoiceNetwork.instance.unityMicrophoneDevice;
		}
		set
		{
			if (value != null && !Microphone.devices.Contains(value))
			{
				Debug.LogError("PUNVoice: " + value + " is not a valid microphone device");
				return;
			}
			PhotonVoiceNetwork.instance.unityMicrophoneDevice = value;
			if (PhotonVoiceSettings.Instance.DebugInfo)
			{
				Debug.LogFormat("PUNVoice: Setting global Unity microphone device to {0}", new object[]
				{
					PhotonVoiceNetwork.instance.unityMicrophoneDevice
				});
			}
			foreach (PhotonVoiceRecorder photonVoiceRecorder in Object.FindObjectsOfType<PhotonVoiceRecorder>())
			{
				if (photonVoiceRecorder.photonView.isMine && photonVoiceRecorder.MicrophoneDevice == null)
				{
					photonVoiceRecorder.MicrophoneDevice = null;
				}
			}
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x0600003E RID: 62 RVA: 0x0000303B File Offset: 0x0000123B
	// (set) Token: 0x0600003F RID: 63 RVA: 0x00003048 File Offset: 0x00001248
	public static int PhotonMicrophoneDeviceID
	{
		get
		{
			return PhotonVoiceNetwork.instance.photonMicrophoneDeviceID;
		}
		set
		{
			if (!PhotonVoiceNetwork.PhotonMicrophoneEnumerator.IDIsValid(value))
			{
				Debug.LogError("PUNVoice: " + value + " is not a valid Photon microphone device");
				return;
			}
			PhotonVoiceNetwork.instance.photonMicrophoneDeviceID = value;
			if (PhotonVoiceSettings.Instance.DebugInfo)
			{
				Debug.LogFormat("PUNVoice: Setting global Photon microphone device to {0}", new object[]
				{
					PhotonVoiceNetwork.instance.photonMicrophoneDeviceID
				});
			}
			foreach (PhotonVoiceRecorder photonVoiceRecorder in Object.FindObjectsOfType<PhotonVoiceRecorder>())
			{
				if (photonVoiceRecorder.photonView.isMine && photonVoiceRecorder.PhotonMicrophoneDeviceID == -1)
				{
					photonVoiceRecorder.PhotonMicrophoneDeviceID = -1;
				}
			}
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x000030EB File Offset: 0x000012EB
	protected void OnEnable()
	{
		this != PhotonVoiceNetwork._instance;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x000030F9 File Offset: 0x000012F9
	protected void OnApplicationQuit()
	{
		if (this != PhotonVoiceNetwork._instance)
		{
			return;
		}
		this.client.Disconnect();
		this.client.Dispose();
	}

	// Token: 0x06000042 RID: 66 RVA: 0x0000311F File Offset: 0x0000131F
	protected void Update()
	{
		if (this != PhotonVoiceNetwork._instance)
		{
			return;
		}
		this.client.VoiceClient.DebugLostPercent = PhotonVoiceSettings.Instance.DebugLostPercent;
		this.client.Service();
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003154 File Offset: 0x00001354
	private void OnJoinedRoom()
	{
		if (this != PhotonVoiceNetwork._instance)
		{
			return;
		}
		if ((!PhotonVoiceSettings.Instance.WorkInOfflineMode && PhotonNetwork.offlineMode) || !PhotonVoiceSettings.Instance.AutoConnect)
		{
			return;
		}
		ExitGames.Client.Photon.LoadBalancing.ClientState state = this.client.State;
		if (state == ExitGames.Client.Photon.LoadBalancing.ClientState.Joined)
		{
			this.client.OpLeaveRoom();
			return;
		}
		this.client.Reconnect();
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000031B7 File Offset: 0x000013B7
	private void OnLeftRoom()
	{
		if (this != PhotonVoiceNetwork._instance)
		{
			return;
		}
		if (!PhotonVoiceSettings.Instance.WorkInOfflineMode && PhotonNetwork.offlineMode)
		{
			return;
		}
		if (PhotonVoiceSettings.Instance.AutoDisconnect)
		{
			this.client.Disconnect();
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000031B7 File Offset: 0x000013B7
	private void OnDisconnectedFromPhoton()
	{
		if (this != PhotonVoiceNetwork._instance)
		{
			return;
		}
		if (!PhotonVoiceSettings.Instance.WorkInOfflineMode && PhotonNetwork.offlineMode)
		{
			return;
		}
		if (PhotonVoiceSettings.Instance.AutoDisconnect)
		{
			this.client.Disconnect();
		}
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000031F2 File Offset: 0x000013F2
	internal static void LinkSpeakerToRemoteVoice(PhotonVoiceSpeaker speaker)
	{
		PhotonVoiceNetwork.instance.client.LinkSpeakerToRemoteVoice(speaker);
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003204 File Offset: 0x00001404
	internal static void UnlinkSpeakerFromRemoteVoice(PhotonVoiceSpeaker speaker)
	{
		if (!PhotonVoiceNetwork.destroyed)
		{
			PhotonVoiceNetwork.instance.client.UnlinkSpeakerFromRemoteVoice(speaker);
		}
	}

	// Token: 0x04000036 RID: 54
	private static PhotonVoiceNetwork _instance;

	// Token: 0x04000037 RID: 55
	private static object instanceLock = new object();

	// Token: 0x04000038 RID: 56
	private static bool destroyed = false;

	// Token: 0x04000039 RID: 57
	public static float BackgroundTimeout = 60f;

	// Token: 0x0400003A RID: 58
	internal UnityVoiceFrontend client;

	// Token: 0x0400003C RID: 60
	private string unityMicrophoneDevice;

	// Token: 0x0400003D RID: 61
	private int photonMicrophoneDeviceID = -1;

	// Token: 0x0400003E RID: 62
	private AudioInEnumerator photonMicEnumerator = new AudioInEnumerator();
}
