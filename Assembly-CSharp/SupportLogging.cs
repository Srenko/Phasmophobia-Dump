using System;
using System.Text;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class SupportLogging : MonoBehaviour
{
	// Token: 0x06000632 RID: 1586 RVA: 0x000229F8 File Offset: 0x00020BF8
	public void Start()
	{
		if (this.LogTrafficStats)
		{
			base.InvokeRepeating("LogStats", 10f, 10f);
		}
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x00022A18 File Offset: 0x00020C18
	protected void OnApplicationPause(bool pause)
	{
		Debug.Log("SupportLogger OnApplicationPause: " + pause.ToString() + " connected: " + PhotonNetwork.connected.ToString());
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x00022A4D File Offset: 0x00020C4D
	public void OnApplicationQuit()
	{
		base.CancelInvoke();
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x00022A55 File Offset: 0x00020C55
	public void LogStats()
	{
		if (this.LogTrafficStats)
		{
			Debug.Log("SupportLogger " + PhotonNetwork.NetworkStatisticsToString());
		}
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x00022A74 File Offset: 0x00020C74
	private void LogBasics()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("SupportLogger Info: PUN {0}: ", "1.103.1");
		stringBuilder.AppendFormat("AppID: {0}*** GameVersion: {1} PeerId: {2} ", PhotonNetwork.networkingPeer.AppId.Substring(0, 8), PhotonNetwork.networkingPeer.AppVersion, PhotonNetwork.networkingPeer.PeerID);
		stringBuilder.AppendFormat("Server: {0}. Region: {1} ", PhotonNetwork.ServerAddress, PhotonNetwork.networkingPeer.CloudRegion);
		stringBuilder.AppendFormat("HostType: {0} ", PhotonNetwork.PhotonServerSettings.HostType);
		Debug.Log(stringBuilder.ToString());
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x00022B0D File Offset: 0x00020D0D
	public void OnConnectedToPhoton()
	{
		Debug.Log("SupportLogger OnConnectedToPhoton().");
		this.LogBasics();
		if (this.LogTrafficStats)
		{
			PhotonNetwork.NetworkStatisticsEnabled = true;
		}
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x00022B2D File Offset: 0x00020D2D
	public void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		Debug.Log("SupportLogger OnFailedToConnectToPhoton(" + cause + ").");
		this.LogBasics();
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x00022B4F File Offset: 0x00020D4F
	public void OnJoinedLobby()
	{
		Debug.Log("SupportLogger OnJoinedLobby(" + PhotonNetwork.lobby + ").");
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x00022B6C File Offset: 0x00020D6C
	public void OnJoinedRoom()
	{
		Debug.Log(string.Concat(new object[]
		{
			"SupportLogger OnJoinedRoom(",
			PhotonNetwork.room,
			"). ",
			PhotonNetwork.lobby,
			" GameServer:",
			PhotonNetwork.ServerAddress
		}));
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x00022BBC File Offset: 0x00020DBC
	public void OnCreatedRoom()
	{
		Debug.Log(string.Concat(new object[]
		{
			"SupportLogger OnCreatedRoom(",
			PhotonNetwork.room,
			"). ",
			PhotonNetwork.lobby,
			" GameServer:",
			PhotonNetwork.ServerAddress
		}));
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x00022C09 File Offset: 0x00020E09
	public void OnLeftRoom()
	{
		Debug.Log("SupportLogger OnLeftRoom().");
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x00022C15 File Offset: 0x00020E15
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("SupportLogger OnDisconnectedFromPhoton().");
	}

	// Token: 0x0400061B RID: 1563
	public bool LogTrafficStats;
}
