using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000AC RID: 172
internal class PhotonHandler : MonoBehaviour
{
	// Token: 0x06000405 RID: 1029 RVA: 0x00019FDC File Offset: 0x000181DC
	protected void Awake()
	{
		if (PhotonHandler.SP != null && PhotonHandler.SP != this && PhotonHandler.SP.gameObject != null)
		{
			Object.DestroyImmediate(PhotonHandler.SP.gameObject);
		}
		PhotonHandler.SP = this;
		Object.DontDestroyOnLoad(base.gameObject);
		this.updateInterval = 1000 / PhotonNetwork.sendRate;
		this.updateIntervalOnSerialize = 1000 / PhotonNetwork.sendRateOnSerialize;
		PhotonHandler.StartFallbackSendAckThread();
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0001A05C File Offset: 0x0001825C
	protected void Start()
	{
		SceneManager.sceneLoaded += delegate(Scene scene, LoadSceneMode loadingMode)
		{
			PhotonNetwork.networkingPeer.NewSceneLoaded();
			PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced(SceneManagerHelper.ActiveSceneName, false, false);
			PhotonNetwork.networkingPeer.IsReloadingLevel = false;
		};
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x0001A082 File Offset: 0x00018282
	protected void OnApplicationQuit()
	{
		PhotonHandler.AppQuits = true;
		PhotonHandler.StopFallbackSendAckThread();
		PhotonNetwork.Disconnect();
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x0001A094 File Offset: 0x00018294
	protected void OnApplicationPause(bool pause)
	{
		if (PhotonNetwork.BackgroundTimeout > 0.1f)
		{
			if (PhotonHandler.timerToStopConnectionInBackground == null)
			{
				PhotonHandler.timerToStopConnectionInBackground = new Stopwatch();
			}
			PhotonHandler.timerToStopConnectionInBackground.Reset();
			if (pause)
			{
				PhotonHandler.timerToStopConnectionInBackground.Start();
				return;
			}
			PhotonHandler.timerToStopConnectionInBackground.Stop();
		}
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0001A0E0 File Offset: 0x000182E0
	protected void OnDestroy()
	{
		PhotonHandler.StopFallbackSendAckThread();
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x0001A0E8 File Offset: 0x000182E8
	protected void Update()
	{
		if (PhotonNetwork.networkingPeer == null)
		{
			Debug.LogError("NetworkPeer broke!");
			return;
		}
		if (PhotonNetwork.connectionStateDetailed == ClientState.PeerCreated || PhotonNetwork.connectionStateDetailed == ClientState.Disconnected || PhotonNetwork.offlineMode)
		{
			return;
		}
		if (!PhotonNetwork.isMessageQueueRunning)
		{
			return;
		}
		bool flag = true;
		while (PhotonNetwork.isMessageQueueRunning && flag)
		{
			flag = PhotonNetwork.networkingPeer.DispatchIncomingCommands();
		}
		int num = (int)(Time.realtimeSinceStartup * 1000f);
		if (PhotonNetwork.isMessageQueueRunning && num > this.nextSendTickCountOnSerialize)
		{
			PhotonNetwork.networkingPeer.RunViewUpdate();
			this.nextSendTickCountOnSerialize = num + this.updateIntervalOnSerialize;
			this.nextSendTickCount = 0;
		}
		num = (int)(Time.realtimeSinceStartup * 1000f);
		if (num > this.nextSendTickCount)
		{
			bool flag2 = true;
			while (PhotonNetwork.isMessageQueueRunning && flag2)
			{
				flag2 = PhotonNetwork.networkingPeer.SendOutgoingCommands();
			}
			this.nextSendTickCount = num + this.updateInterval;
		}
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x0001A1B8 File Offset: 0x000183B8
	protected void OnJoinedRoom()
	{
		PhotonNetwork.networkingPeer.LoadLevelIfSynced();
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0001A1C4 File Offset: 0x000183C4
	protected void OnCreatedRoom()
	{
		PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced(SceneManagerHelper.ActiveSceneName, false, false);
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x0001A1D7 File Offset: 0x000183D7
	public static void StartFallbackSendAckThread()
	{
		if (PhotonHandler.sendThreadShouldRun)
		{
			return;
		}
		PhotonHandler.sendThreadShouldRun = true;
		SupportClass.StartBackgroundCalls(new Func<bool>(PhotonHandler.FallbackSendAckThread), 100, "");
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0001A200 File Offset: 0x00018400
	public static void StopFallbackSendAckThread()
	{
		PhotonHandler.sendThreadShouldRun = false;
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x0001A208 File Offset: 0x00018408
	public static bool FallbackSendAckThread()
	{
		if (PhotonHandler.sendThreadShouldRun && !PhotonNetwork.offlineMode && PhotonNetwork.networkingPeer != null)
		{
			if (PhotonHandler.timerToStopConnectionInBackground != null && PhotonNetwork.BackgroundTimeout > 0.1f && (float)PhotonHandler.timerToStopConnectionInBackground.ElapsedMilliseconds > PhotonNetwork.BackgroundTimeout * 1000f)
			{
				if (PhotonNetwork.connected)
				{
					PhotonNetwork.Disconnect();
				}
				PhotonHandler.timerToStopConnectionInBackground.Stop();
				PhotonHandler.timerToStopConnectionInBackground.Reset();
				return PhotonHandler.sendThreadShouldRun;
			}
			if (!PhotonNetwork.isMessageQueueRunning || PhotonNetwork.networkingPeer.ConnectionTime - PhotonNetwork.networkingPeer.LastSendOutgoingTime > 200)
			{
				PhotonNetwork.networkingPeer.SendAcksOnly();
			}
		}
		return PhotonHandler.sendThreadShouldRun;
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000410 RID: 1040 RVA: 0x0001A2B4 File Offset: 0x000184B4
	// (set) Token: 0x06000411 RID: 1041 RVA: 0x0001A2E1 File Offset: 0x000184E1
	internal static CloudRegionCode BestRegionCodeInPreferences
	{
		get
		{
			string @string = PlayerPrefs.GetString("PUNCloudBestRegion", "");
			if (!string.IsNullOrEmpty(@string))
			{
				return Region.Parse(@string);
			}
			return CloudRegionCode.none;
		}
		set
		{
			if (value == CloudRegionCode.none)
			{
				PlayerPrefs.DeleteKey("PUNCloudBestRegion");
				return;
			}
			PlayerPrefs.SetString("PUNCloudBestRegion", value.ToString());
		}
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0001A309 File Offset: 0x00018509
	protected internal static void PingAvailableRegionsAndConnectToBest()
	{
		PhotonHandler.SP.StartCoroutine(PhotonHandler.SP.PingAvailableRegionsCoroutine(true));
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0001A321 File Offset: 0x00018521
	internal IEnumerator PingAvailableRegionsCoroutine(bool connectToBest)
	{
		while (PhotonNetwork.networkingPeer.AvailableRegions == null)
		{
			if (PhotonNetwork.connectionStateDetailed != ClientState.ConnectingToNameServer && PhotonNetwork.connectionStateDetailed != ClientState.ConnectedToNameServer)
			{
				Debug.LogError("Call ConnectToNameServer to ping available regions.");
				yield break;
			}
			Debug.Log(string.Concat(new object[]
			{
				"Waiting for AvailableRegions. State: ",
				PhotonNetwork.connectionStateDetailed,
				" Server: ",
				PhotonNetwork.Server,
				" PhotonNetwork.networkingPeer.AvailableRegions ",
				(PhotonNetwork.networkingPeer.AvailableRegions != null).ToString()
			}));
			yield return new WaitForSeconds(0.25f);
		}
		if (PhotonNetwork.networkingPeer.AvailableRegions == null || PhotonNetwork.networkingPeer.AvailableRegions.Count == 0)
		{
			Debug.LogError("No regions available. Are you sure your appid is valid and setup?");
			yield break;
		}
		PhotonPingManager pingManager = new PhotonPingManager();
		using (List<Region>.Enumerator enumerator = PhotonNetwork.networkingPeer.AvailableRegions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Region region = enumerator.Current;
				PhotonHandler.SP.StartCoroutine(pingManager.PingSocket(region));
			}
			goto IL_16E;
		}
		IL_14E:
		yield return new WaitForSeconds(0.1f);
		IL_16E:
		if (pingManager.Done)
		{
			Region bestRegion = pingManager.BestRegion;
			PhotonHandler.BestRegionCodeInPreferences = bestRegion.Code;
			Debug.Log(string.Concat(new object[]
			{
				"Found best region: '",
				bestRegion.Code,
				"' ping: ",
				bestRegion.Ping,
				". Calling ConnectToRegionMaster() is: ",
				connectToBest.ToString()
			}));
			if (connectToBest)
			{
				PhotonNetwork.networkingPeer.ConnectToRegionMaster(bestRegion.Code, null);
			}
			yield break;
		}
		goto IL_14E;
	}

	// Token: 0x040004E3 RID: 1251
	public static PhotonHandler SP;

	// Token: 0x040004E4 RID: 1252
	public int updateInterval;

	// Token: 0x040004E5 RID: 1253
	public int updateIntervalOnSerialize;

	// Token: 0x040004E6 RID: 1254
	private int nextSendTickCount;

	// Token: 0x040004E7 RID: 1255
	private int nextSendTickCountOnSerialize;

	// Token: 0x040004E8 RID: 1256
	private static bool sendThreadShouldRun;

	// Token: 0x040004E9 RID: 1257
	private static Stopwatch timerToStopConnectionInBackground;

	// Token: 0x040004EA RID: 1258
	protected internal static bool AppQuits;

	// Token: 0x040004EB RID: 1259
	protected internal static Type PingImplementation;

	// Token: 0x040004EC RID: 1260
	private const string PlayerPrefsKey = "PUNCloudBestRegion";
}
