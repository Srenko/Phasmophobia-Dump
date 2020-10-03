using System;

// Token: 0x020000BC RID: 188
public class Region
{
	// Token: 0x06000555 RID: 1365 RVA: 0x0001E70F File Offset: 0x0001C90F
	public Region(CloudRegionCode code)
	{
		this.Code = code;
		this.Cluster = code.ToString();
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0001E731 File Offset: 0x0001C931
	public Region(CloudRegionCode code, string regionCodeString, string address)
	{
		this.Code = code;
		this.Cluster = regionCodeString;
		this.HostAndPort = address;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0001E750 File Offset: 0x0001C950
	public static CloudRegionCode Parse(string codeAsString)
	{
		if (codeAsString == null)
		{
			return CloudRegionCode.none;
		}
		int num = codeAsString.IndexOf('/');
		if (num > 0)
		{
			codeAsString = codeAsString.Substring(0, num);
		}
		codeAsString = codeAsString.ToLower();
		if (Enum.IsDefined(typeof(CloudRegionCode), codeAsString))
		{
			return (CloudRegionCode)Enum.Parse(typeof(CloudRegionCode), codeAsString);
		}
		return CloudRegionCode.none;
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0001E7AC File Offset: 0x0001C9AC
	internal static CloudRegionFlag ParseFlag(CloudRegionCode region)
	{
		if (Enum.IsDefined(typeof(CloudRegionFlag), region.ToString()))
		{
			return (CloudRegionFlag)Enum.Parse(typeof(CloudRegionFlag), region.ToString());
		}
		return (CloudRegionFlag)0;
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0001E7FC File Offset: 0x0001C9FC
	[Obsolete]
	internal static CloudRegionFlag ParseFlag(string codeAsString)
	{
		codeAsString = codeAsString.ToLower();
		CloudRegionFlag result = (CloudRegionFlag)0;
		if (Enum.IsDefined(typeof(CloudRegionFlag), codeAsString))
		{
			result = (CloudRegionFlag)Enum.Parse(typeof(CloudRegionFlag), codeAsString);
		}
		return result;
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x0001E83C File Offset: 0x0001CA3C
	public override string ToString()
	{
		return string.Format("'{0}' \t{1}ms \t{2}", this.Cluster, this.Ping, this.HostAndPort);
	}

	// Token: 0x04000569 RID: 1385
	public CloudRegionCode Code;

	// Token: 0x0400056A RID: 1386
	public string Cluster;

	// Token: 0x0400056B RID: 1387
	public string HostAndPort;

	// Token: 0x0400056C RID: 1388
	public int Ping;
}
