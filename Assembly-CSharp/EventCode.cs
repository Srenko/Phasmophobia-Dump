using System;

// Token: 0x0200008E RID: 142
public class EventCode
{
	// Token: 0x04000393 RID: 915
	public const byte GameList = 230;

	// Token: 0x04000394 RID: 916
	public const byte GameListUpdate = 229;

	// Token: 0x04000395 RID: 917
	public const byte QueueState = 228;

	// Token: 0x04000396 RID: 918
	public const byte Match = 227;

	// Token: 0x04000397 RID: 919
	public const byte AppStats = 226;

	// Token: 0x04000398 RID: 920
	public const byte LobbyStats = 224;

	// Token: 0x04000399 RID: 921
	[Obsolete("TCP routing was removed after becoming obsolete.")]
	public const byte AzureNodeInfo = 210;

	// Token: 0x0400039A RID: 922
	public const byte Join = 255;

	// Token: 0x0400039B RID: 923
	public const byte Leave = 254;

	// Token: 0x0400039C RID: 924
	public const byte PropertiesChanged = 253;

	// Token: 0x0400039D RID: 925
	[Obsolete("Use PropertiesChanged now.")]
	public const byte SetProperties = 253;

	// Token: 0x0400039E RID: 926
	public const byte ErrorInfo = 251;

	// Token: 0x0400039F RID: 927
	public const byte CacheSliceChanged = 250;

	// Token: 0x040003A0 RID: 928
	public const byte AuthEvent = 223;
}
