using System;

namespace Photon.Chat
{
	// Token: 0x02000465 RID: 1125
	public enum ChatState
	{
		// Token: 0x0400206D RID: 8301
		Uninitialized,
		// Token: 0x0400206E RID: 8302
		ConnectingToNameServer,
		// Token: 0x0400206F RID: 8303
		ConnectedToNameServer,
		// Token: 0x04002070 RID: 8304
		Authenticating,
		// Token: 0x04002071 RID: 8305
		Authenticated,
		// Token: 0x04002072 RID: 8306
		DisconnectingFromNameServer,
		// Token: 0x04002073 RID: 8307
		ConnectingToFrontEnd,
		// Token: 0x04002074 RID: 8308
		ConnectedToFrontEnd,
		// Token: 0x04002075 RID: 8309
		DisconnectingFromFrontEnd,
		// Token: 0x04002076 RID: 8310
		QueuedComingFromFrontEnd,
		// Token: 0x04002077 RID: 8311
		Disconnecting,
		// Token: 0x04002078 RID: 8312
		Disconnected
	}
}
