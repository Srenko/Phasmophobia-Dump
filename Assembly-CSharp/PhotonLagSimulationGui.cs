using System;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class PhotonLagSimulationGui : MonoBehaviour
{
	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000416 RID: 1046 RVA: 0x0001A330 File Offset: 0x00018530
	// (set) Token: 0x06000417 RID: 1047 RVA: 0x0001A338 File Offset: 0x00018538
	public PhotonPeer Peer { get; set; }

	// Token: 0x06000418 RID: 1048 RVA: 0x0001A341 File Offset: 0x00018541
	public void Start()
	{
		this.Peer = PhotonNetwork.networkingPeer;
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0001A350 File Offset: 0x00018550
	public void OnGUI()
	{
		if (!this.Visible)
		{
			return;
		}
		if (this.Peer == null)
		{
			this.WindowRect = GUILayout.Window(this.WindowId, this.WindowRect, new GUI.WindowFunction(this.NetSimHasNoPeerWindow), "Netw. Sim.", Array.Empty<GUILayoutOption>());
			return;
		}
		this.WindowRect = GUILayout.Window(this.WindowId, this.WindowRect, new GUI.WindowFunction(this.NetSimWindow), "Netw. Sim.", Array.Empty<GUILayoutOption>());
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x0001A3C9 File Offset: 0x000185C9
	private void NetSimHasNoPeerWindow(int windowId)
	{
		GUILayout.Label("No peer to communicate with. ", Array.Empty<GUILayoutOption>());
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x0001A3DC File Offset: 0x000185DC
	private void NetSimWindow(int windowId)
	{
		GUILayout.Label(string.Format("Rtt:{0,4} +/-{1,3}", this.Peer.RoundTripTime, this.Peer.RoundTripTimeVariance), Array.Empty<GUILayoutOption>());
		bool isSimulationEnabled = this.Peer.IsSimulationEnabled;
		bool flag = GUILayout.Toggle(isSimulationEnabled, "Simulate", Array.Empty<GUILayoutOption>());
		if (flag != isSimulationEnabled)
		{
			this.Peer.IsSimulationEnabled = flag;
		}
		float num = (float)this.Peer.NetworkSimulationSettings.IncomingLag;
		GUILayout.Label("Lag " + num, Array.Empty<GUILayoutOption>());
		num = GUILayout.HorizontalSlider(num, 0f, 500f, Array.Empty<GUILayoutOption>());
		this.Peer.NetworkSimulationSettings.IncomingLag = (int)num;
		this.Peer.NetworkSimulationSettings.OutgoingLag = (int)num;
		float num2 = (float)this.Peer.NetworkSimulationSettings.IncomingJitter;
		GUILayout.Label("Jit " + num2, Array.Empty<GUILayoutOption>());
		num2 = GUILayout.HorizontalSlider(num2, 0f, 100f, Array.Empty<GUILayoutOption>());
		this.Peer.NetworkSimulationSettings.IncomingJitter = (int)num2;
		this.Peer.NetworkSimulationSettings.OutgoingJitter = (int)num2;
		float num3 = (float)this.Peer.NetworkSimulationSettings.IncomingLossPercentage;
		GUILayout.Label("Loss " + num3, Array.Empty<GUILayoutOption>());
		num3 = GUILayout.HorizontalSlider(num3, 0f, 10f, Array.Empty<GUILayoutOption>());
		this.Peer.NetworkSimulationSettings.IncomingLossPercentage = (int)num3;
		this.Peer.NetworkSimulationSettings.OutgoingLossPercentage = (int)num3;
		if (GUI.changed)
		{
			this.WindowRect.height = 100f;
		}
		GUI.DragWindow();
	}

	// Token: 0x040004ED RID: 1261
	public Rect WindowRect = new Rect(0f, 100f, 120f, 100f);

	// Token: 0x040004EE RID: 1262
	public int WindowId = 101;

	// Token: 0x040004EF RID: 1263
	public bool Visible = true;
}
