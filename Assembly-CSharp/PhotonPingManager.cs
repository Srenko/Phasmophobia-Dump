using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using Photon.Realtime;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class PhotonPingManager
{
	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06000509 RID: 1289 RVA: 0x0001DBD0 File Offset: 0x0001BDD0
	public Region BestRegion
	{
		get
		{
			Region result = null;
			int num = int.MaxValue;
			foreach (Region region in PhotonNetwork.networkingPeer.AvailableRegions)
			{
				Debug.Log("BestRegion checks region: " + region);
				if (region.Ping != 0 && region.Ping < num)
				{
					num = region.Ping;
					result = region;
				}
			}
			return result;
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001DC54 File Offset: 0x0001BE54
	public bool Done
	{
		get
		{
			return this.PingsRunning == 0;
		}
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x0001DC5F File Offset: 0x0001BE5F
	public IEnumerator PingSocket(Region region)
	{
		region.Ping = PhotonPingManager.Attempts * PhotonPingManager.MaxMilliseconsPerPing;
		this.PingsRunning++;
		PhotonPing ping = null;
		if (PhotonHandler.PingImplementation == typeof(PingMono))
		{
			ping = new PingMono();
		}
		if (ping == null)
		{
			ping = (PhotonPing)Activator.CreateInstance(PhotonHandler.PingImplementation);
		}
		float rttSum = 0f;
		int replyCount = 0;
		string regionAddress = region.HostAndPort;
		int num = regionAddress.LastIndexOf(':');
		if (num > 1)
		{
			regionAddress = regionAddress.Substring(0, num);
		}
		int num2 = regionAddress.IndexOf("wss://");
		if (num2 > -1)
		{
			regionAddress = regionAddress.Substring(num2 + "wss://".Length);
		}
		regionAddress = PhotonPingManager.ResolveHost(regionAddress);
		int i = 0;
		while (i < PhotonPingManager.Attempts)
		{
			bool overtime = false;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			try
			{
				ping.StartPing(regionAddress);
				goto IL_1BC;
			}
			catch (Exception arg)
			{
				Debug.Log("catched: " + arg);
				this.PingsRunning--;
				break;
			}
			goto IL_184;
			IL_1C9:
			int num3 = (int)sw.ElapsedMilliseconds;
			int num4;
			if ((!PhotonPingManager.IgnoreInitialAttempt || i != 0) && ping.Successful && !overtime)
			{
				rttSum += (float)num3;
				num4 = replyCount;
				replyCount = num4 + 1;
				region.Ping = (int)(rttSum / (float)replyCount);
			}
			yield return new WaitForSeconds(0.1f);
			sw = null;
			num4 = i;
			i = num4 + 1;
			continue;
			IL_184:
			if (sw.ElapsedMilliseconds >= (long)PhotonPingManager.MaxMilliseconsPerPing)
			{
				overtime = true;
				goto IL_1C9;
			}
			yield return 0;
			IL_1BC:
			if (ping.Done())
			{
				goto IL_1C9;
			}
			goto IL_184;
		}
		ping.Dispose();
		this.PingsRunning--;
		yield return null;
		yield break;
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x0001DC78 File Offset: 0x0001BE78
	public static string ResolveHost(string hostName)
	{
		string text = string.Empty;
		try
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
			if (hostAddresses.Length == 1)
			{
				return hostAddresses[0].ToString();
			}
			foreach (IPAddress ipaddress in hostAddresses)
			{
				if (ipaddress != null)
				{
					if (ipaddress.ToString().Contains(":"))
					{
						return ipaddress.ToString();
					}
					if (string.IsNullOrEmpty(text))
					{
						text = hostAddresses.ToString();
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log("Exception caught! " + ex.Source + " Message: " + ex.Message);
		}
		return text;
	}

	// Token: 0x04000554 RID: 1364
	public bool UseNative;

	// Token: 0x04000555 RID: 1365
	public static int Attempts = 5;

	// Token: 0x04000556 RID: 1366
	public static bool IgnoreInitialAttempt = true;

	// Token: 0x04000557 RID: 1367
	public static int MaxMilliseconsPerPing = 800;

	// Token: 0x04000558 RID: 1368
	private const string wssProtocolString = "wss://";

	// Token: 0x04000559 RID: 1369
	private int PingsRunning;
}
