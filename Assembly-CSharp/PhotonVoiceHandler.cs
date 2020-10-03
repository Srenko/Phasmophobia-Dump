using System;
using System.Diagnostics;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.LoadBalancing;
using UnityEngine;

// Token: 0x02000009 RID: 9
[DisallowMultipleComponent]
public class PhotonVoiceHandler : MonoBehaviour
{
	// Token: 0x06000023 RID: 35 RVA: 0x000029E5 File Offset: 0x00000BE5
	private static void StartFallbackSendAckThread()
	{
		if (PhotonVoiceHandler.sendThreadShouldRun)
		{
			return;
		}
		PhotonVoiceHandler.sendThreadShouldRun = true;
		SupportClass.StartBackgroundCalls(new Func<bool>(PhotonVoiceHandler.FallbackSendAckThread), 100, "");
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002A0E File Offset: 0x00000C0E
	private static void StopFallbackSendAckThread()
	{
		PhotonVoiceHandler.sendThreadShouldRun = false;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002A18 File Offset: 0x00000C18
	private static bool FallbackSendAckThread()
	{
		if (PhotonVoiceHandler.sendThreadShouldRun && PhotonVoiceNetwork.Client != null && PhotonVoiceNetwork.Client.loadBalancingPeer != null)
		{
			ExitGames.Client.Photon.LoadBalancing.LoadBalancingPeer loadBalancingPeer = PhotonVoiceNetwork.Client.loadBalancingPeer;
			ExitGames.Client.Photon.LoadBalancing.ClientState state = PhotonVoiceNetwork.Client.State;
			if (PhotonVoiceHandler.timerToStopConnectionInBackground != null && PhotonVoiceNetwork.BackgroundTimeout > 0.1f && (float)PhotonVoiceHandler.timerToStopConnectionInBackground.ElapsedMilliseconds > PhotonVoiceNetwork.BackgroundTimeout * 1000f)
			{
				bool flag = true;
				if (state == ExitGames.Client.Photon.LoadBalancing.ClientState.PeerCreated || state - ExitGames.Client.Photon.LoadBalancing.ClientState.Disconnecting <= 1 || state == ExitGames.Client.Photon.LoadBalancing.ClientState.ConnectedToNameServer)
				{
					flag = false;
				}
				if (flag)
				{
					PhotonVoiceNetwork.Disconnect();
				}
				PhotonVoiceHandler.timerToStopConnectionInBackground.Stop();
				PhotonVoiceHandler.timerToStopConnectionInBackground.Reset();
				return PhotonVoiceHandler.sendThreadShouldRun;
			}
			if (loadBalancingPeer.ConnectionTime - loadBalancingPeer.LastSendOutgoingTime > 200)
			{
				loadBalancingPeer.SendAcksOnly();
			}
		}
		return PhotonVoiceHandler.sendThreadShouldRun;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002AE0 File Offset: 0x00000CE0
	[RuntimeInitializeOnLoadMethod]
	private static void RuntimeInitializeOnLoad()
	{
		string name = "PhotonVoiceMono";
		GameObject gameObject = GameObject.Find(name);
		if (gameObject == null)
		{
			gameObject = new GameObject();
			gameObject.name = name;
		}
		if (gameObject.GetComponent<PhotonVoiceHandler>() == null)
		{
			gameObject.AddComponent<PhotonVoiceHandler>();
		}
		gameObject.hideFlags = HideFlags.HideInHierarchy;
		Object.DontDestroyOnLoad(gameObject);
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002B32 File Offset: 0x00000D32
	private void Awake()
	{
		if (null != PhotonVoiceNetwork.instance)
		{
			PhotonVoiceHandler.StartFallbackSendAckThread();
			return;
		}
		Debug.LogError("[PUNVoice]: \"FallbackSendAckThread\" not started because PhotonVoiceNetwork instance not ready yet.");
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002B54 File Offset: 0x00000D54
	protected void Update()
	{
		ExitGames.Client.Photon.LoadBalancing.LoadBalancingPeer loadBalancingPeer = PhotonVoiceNetwork.Client.loadBalancingPeer;
		if (loadBalancingPeer == null)
		{
			Debug.LogError("[PUNVoice]: LoadBalancingPeer broke!");
			return;
		}
		ExitGames.Client.Photon.LoadBalancing.ClientState state = PhotonVoiceNetwork.Client.State;
		bool flag = true;
		if (state == ExitGames.Client.Photon.LoadBalancing.ClientState.PeerCreated || state - ExitGames.Client.Photon.LoadBalancing.ClientState.Disconnecting <= 1 || state == ExitGames.Client.Photon.LoadBalancing.ClientState.ConnectedToNameServer)
		{
			flag = false;
		}
		if (!flag)
		{
			return;
		}
		bool flag2 = true;
		while (PhotonNetwork.isMessageQueueRunning && flag2)
		{
			flag2 = loadBalancingPeer.DispatchIncomingCommands();
		}
		int num = (int)(Time.realtimeSinceStartup * 1000f);
		if (num > this.nextSendTickCount)
		{
			bool flag3 = true;
			while (PhotonNetwork.isMessageQueueRunning && flag3)
			{
				flag3 = loadBalancingPeer.SendOutgoingCommands();
			}
			this.nextSendTickCount = num + this.updateInterval;
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002BF0 File Offset: 0x00000DF0
	protected void OnApplicationPause(bool pause)
	{
		if (PhotonVoiceNetwork.BackgroundTimeout > 0.1f)
		{
			if (PhotonVoiceHandler.timerToStopConnectionInBackground == null)
			{
				PhotonVoiceHandler.timerToStopConnectionInBackground = new Stopwatch();
			}
			PhotonVoiceHandler.timerToStopConnectionInBackground.Reset();
			if (pause)
			{
				PhotonVoiceHandler.timerToStopConnectionInBackground.Start();
				return;
			}
			PhotonVoiceHandler.timerToStopConnectionInBackground.Stop();
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002C3C File Offset: 0x00000E3C
	protected void OnDestroy()
	{
		PhotonVoiceHandler.StopFallbackSendAckThread();
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002C43 File Offset: 0x00000E43
	protected void OnApplicationQuit()
	{
		PhotonVoiceHandler.StopFallbackSendAckThread();
		PhotonVoiceNetwork.Disconnect();
	}

	// Token: 0x04000032 RID: 50
	public int updateInterval;

	// Token: 0x04000033 RID: 51
	private int nextSendTickCount;

	// Token: 0x04000034 RID: 52
	private static bool sendThreadShouldRun;

	// Token: 0x04000035 RID: 53
	private static Stopwatch timerToStopConnectionInBackground;
}
