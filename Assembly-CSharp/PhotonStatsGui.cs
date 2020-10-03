using System;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class PhotonStatsGui : MonoBehaviour
{
	// Token: 0x060004D7 RID: 1239 RVA: 0x0001CA95 File Offset: 0x0001AC95
	public void Start()
	{
		if (this.statsRect.x <= 0f)
		{
			this.statsRect.x = (float)Screen.width - this.statsRect.width;
		}
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0001CAC6 File Offset: 0x0001ACC6
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
		{
			this.statsWindowOn = !this.statsWindowOn;
			this.statsOn = true;
		}
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0001CAF4 File Offset: 0x0001ACF4
	public void OnGUI()
	{
		if (PhotonNetwork.networkingPeer.TrafficStatsEnabled != this.statsOn)
		{
			PhotonNetwork.networkingPeer.TrafficStatsEnabled = this.statsOn;
		}
		if (!this.statsWindowOn)
		{
			return;
		}
		this.statsRect = GUILayout.Window(this.WindowId, this.statsRect, new GUI.WindowFunction(this.TrafficStatsWindow), "Messages (shift+tab)", Array.Empty<GUILayoutOption>());
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0001CB5C File Offset: 0x0001AD5C
	public void TrafficStatsWindow(int windowID)
	{
		bool flag = false;
		TrafficStatsGameLevel trafficStatsGameLevel = PhotonNetwork.networkingPeer.TrafficStatsGameLevel;
		long num = PhotonNetwork.networkingPeer.TrafficStatsElapsedMs / 1000L;
		if (num == 0L)
		{
			num = 1L;
		}
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		this.buttonsOn = GUILayout.Toggle(this.buttonsOn, "buttons", Array.Empty<GUILayoutOption>());
		this.healthStatsVisible = GUILayout.Toggle(this.healthStatsVisible, "health", Array.Empty<GUILayoutOption>());
		this.trafficStatsOn = GUILayout.Toggle(this.trafficStatsOn, "traffic", Array.Empty<GUILayoutOption>());
		GUILayout.EndHorizontal();
		string text = string.Format("Out {0,4} | In {1,4} | Sum {2,4}", trafficStatsGameLevel.TotalOutgoingMessageCount, trafficStatsGameLevel.TotalIncomingMessageCount, trafficStatsGameLevel.TotalMessageCount);
		string text2 = string.Format("{0}sec average:", num);
		string text3 = string.Format("Out {0,4} | In {1,4} | Sum {2,4}", (long)trafficStatsGameLevel.TotalOutgoingMessageCount / num, (long)trafficStatsGameLevel.TotalIncomingMessageCount / num, (long)trafficStatsGameLevel.TotalMessageCount / num);
		GUILayout.Label(text, Array.Empty<GUILayoutOption>());
		GUILayout.Label(text2, Array.Empty<GUILayoutOption>());
		GUILayout.Label(text3, Array.Empty<GUILayoutOption>());
		if (this.buttonsOn)
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.statsOn = GUILayout.Toggle(this.statsOn, "stats on", Array.Empty<GUILayoutOption>());
			if (GUILayout.Button("Reset", Array.Empty<GUILayoutOption>()))
			{
				PhotonNetwork.networkingPeer.TrafficStatsReset();
				PhotonNetwork.networkingPeer.TrafficStatsEnabled = true;
			}
			flag = GUILayout.Button("To Log", Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
		}
		string text4 = string.Empty;
		string text5 = string.Empty;
		if (this.trafficStatsOn)
		{
			GUILayout.Box("Traffic Stats", Array.Empty<GUILayoutOption>());
			text4 = "Incoming: \n" + PhotonNetwork.networkingPeer.TrafficStatsIncoming.ToString();
			text5 = "Outgoing: \n" + PhotonNetwork.networkingPeer.TrafficStatsOutgoing.ToString();
			GUILayout.Label(text4, Array.Empty<GUILayoutOption>());
			GUILayout.Label(text5, Array.Empty<GUILayoutOption>());
		}
		string text6 = string.Empty;
		if (this.healthStatsVisible)
		{
			GUILayout.Box("Health Stats", Array.Empty<GUILayoutOption>());
			text6 = string.Format("ping: {6}[+/-{7}]ms resent:{8} \n\nmax ms between\nsend: {0,4} \ndispatch: {1,4} \n\nlongest dispatch for: \nev({3}):{2,3}ms \nop({5}):{4,3}ms", new object[]
			{
				trafficStatsGameLevel.LongestDeltaBetweenSending,
				trafficStatsGameLevel.LongestDeltaBetweenDispatching,
				trafficStatsGameLevel.LongestEventCallback,
				trafficStatsGameLevel.LongestEventCallbackCode,
				trafficStatsGameLevel.LongestOpResponseCallback,
				trafficStatsGameLevel.LongestOpResponseCallbackOpCode,
				PhotonNetwork.networkingPeer.RoundTripTime,
				PhotonNetwork.networkingPeer.RoundTripTimeVariance,
				PhotonNetwork.networkingPeer.ResentReliableCommands
			});
			GUILayout.Label(text6, Array.Empty<GUILayoutOption>());
		}
		if (flag)
		{
			Debug.Log(string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", new object[]
			{
				text,
				text2,
				text3,
				text4,
				text5,
				text6
			}));
		}
		if (GUI.changed)
		{
			this.statsRect.height = 100f;
		}
		GUI.DragWindow();
	}

	// Token: 0x0400051A RID: 1306
	public bool statsWindowOn = true;

	// Token: 0x0400051B RID: 1307
	public bool statsOn = true;

	// Token: 0x0400051C RID: 1308
	public bool healthStatsVisible;

	// Token: 0x0400051D RID: 1309
	public bool trafficStatsOn;

	// Token: 0x0400051E RID: 1310
	public bool buttonsOn;

	// Token: 0x0400051F RID: 1311
	public Rect statsRect = new Rect(0f, 100f, 200f, 50f);

	// Token: 0x04000520 RID: 1312
	public int WindowId = 100;
}
