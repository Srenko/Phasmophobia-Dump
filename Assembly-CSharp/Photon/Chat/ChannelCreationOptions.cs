using System;

namespace Photon.Chat
{
	// Token: 0x02000458 RID: 1112
	public class ChannelCreationOptions
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x000AA34B File Offset: 0x000A854B
		// (set) Token: 0x06002266 RID: 8806 RVA: 0x000AA353 File Offset: 0x000A8553
		public bool PublishSubscribers { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x000AA35C File Offset: 0x000A855C
		// (set) Token: 0x06002268 RID: 8808 RVA: 0x000AA364 File Offset: 0x000A8564
		public int MaxSubscribers { get; set; }

		// Token: 0x04001FEC RID: 8172
		public static ChannelCreationOptions Default = new ChannelCreationOptions();
	}
}
