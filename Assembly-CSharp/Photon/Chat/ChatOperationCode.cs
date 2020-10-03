using System;

namespace Photon.Chat
{
	// Token: 0x0200045E RID: 1118
	public class ChatOperationCode
	{
		// Token: 0x04002027 RID: 8231
		public const byte Authenticate = 230;

		// Token: 0x04002028 RID: 8232
		public const byte Subscribe = 0;

		// Token: 0x04002029 RID: 8233
		public const byte Unsubscribe = 1;

		// Token: 0x0400202A RID: 8234
		public const byte Publish = 2;

		// Token: 0x0400202B RID: 8235
		public const byte SendPrivate = 3;

		// Token: 0x0400202C RID: 8236
		public const byte ChannelHistory = 4;

		// Token: 0x0400202D RID: 8237
		public const byte UpdateStatus = 5;

		// Token: 0x0400202E RID: 8238
		public const byte AddFriends = 6;

		// Token: 0x0400202F RID: 8239
		public const byte RemoveFriends = 7;
	}
}
