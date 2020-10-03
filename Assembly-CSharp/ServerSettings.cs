using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x020000BD RID: 189
[Serializable]
public class ServerSettings : ScriptableObject
{
	// Token: 0x0600055B RID: 1371 RVA: 0x0001E85F File Offset: 0x0001CA5F
	public void UseCloudBestRegion(string cloudAppid)
	{
		this.HostType = ServerSettings.HostingOption.BestRegion;
		this.AppID = cloudAppid;
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0001E86F File Offset: 0x0001CA6F
	public void UseCloud(string cloudAppid)
	{
		this.HostType = ServerSettings.HostingOption.PhotonCloud;
		this.AppID = cloudAppid;
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0001E87F File Offset: 0x0001CA7F
	public void UseCloud(string cloudAppid, CloudRegionCode code)
	{
		this.HostType = ServerSettings.HostingOption.PhotonCloud;
		this.AppID = cloudAppid;
		this.PreferredRegion = code;
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0001E896 File Offset: 0x0001CA96
	public void UseMyServer(string serverAddress, int serverPort, string application)
	{
		this.HostType = ServerSettings.HostingOption.SelfHosted;
		this.AppID = ((application != null) ? application : "master");
		this.ServerAddress = serverAddress;
		this.ServerPort = serverPort;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0001E8C0 File Offset: 0x0001CAC0
	public static bool IsAppId(string val)
	{
		try
		{
			new Guid(val);
		}
		catch
		{
			return false;
		}
		return true;
	}

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06000560 RID: 1376 RVA: 0x0001E8F0 File Offset: 0x0001CAF0
	public static CloudRegionCode BestRegionCodeInPreferences
	{
		get
		{
			return PhotonHandler.BestRegionCodeInPreferences;
		}
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0001E8F7 File Offset: 0x0001CAF7
	public static void ResetBestRegionCodeInPreferences()
	{
		PhotonHandler.BestRegionCodeInPreferences = CloudRegionCode.none;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0001E8FF File Offset: 0x0001CAFF
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"ServerSettings: ",
			this.HostType,
			" ",
			this.ServerAddress
		});
	}

	// Token: 0x0400056D RID: 1389
	public string AppID = "";

	// Token: 0x0400056E RID: 1390
	public string VoiceAppID = "";

	// Token: 0x0400056F RID: 1391
	public string ChatAppID = "";

	// Token: 0x04000570 RID: 1392
	public ServerSettings.HostingOption HostType;

	// Token: 0x04000571 RID: 1393
	public CloudRegionCode PreferredRegion;

	// Token: 0x04000572 RID: 1394
	public CloudRegionFlag EnabledRegions = (CloudRegionFlag)(-1);

	// Token: 0x04000573 RID: 1395
	public ConnectionProtocol Protocol;

	// Token: 0x04000574 RID: 1396
	public string ServerAddress = "";

	// Token: 0x04000575 RID: 1397
	public int ServerPort = 5055;

	// Token: 0x04000576 RID: 1398
	public int VoiceServerPort = 5055;

	// Token: 0x04000577 RID: 1399
	public bool JoinLobby;

	// Token: 0x04000578 RID: 1400
	public bool EnableLobbyStatistics;

	// Token: 0x04000579 RID: 1401
	public PhotonLogLevel PunLogging;

	// Token: 0x0400057A RID: 1402
	public DebugLevel NetworkLogging = DebugLevel.ERROR;

	// Token: 0x0400057B RID: 1403
	public bool RunInBackground = true;

	// Token: 0x0400057C RID: 1404
	public List<string> RpcList = new List<string>();

	// Token: 0x0400057D RID: 1405
	[HideInInspector]
	public bool DisableAutoOpenWizard;

	// Token: 0x020004F1 RID: 1265
	public enum HostingOption
	{
		// Token: 0x040023DC RID: 9180
		NotSet,
		// Token: 0x040023DD RID: 9181
		PhotonCloud,
		// Token: 0x040023DE RID: 9182
		SelfHosted,
		// Token: 0x040023DF RID: 9183
		OfflineMode,
		// Token: 0x040023E0 RID: 9184
		BestRegion
	}
}
