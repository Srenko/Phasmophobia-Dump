using System;
using ExitGames.Client.Photon;

namespace Photon.Chat
{
	// Token: 0x02000467 RID: 1127
	public interface IChatClientListener
	{
		// Token: 0x060022E3 RID: 8931
		void DebugReturn(DebugLevel level, string message);

		// Token: 0x060022E4 RID: 8932
		void OnDisconnected();

		// Token: 0x060022E5 RID: 8933
		void OnConnected();

		// Token: 0x060022E6 RID: 8934
		void OnChatStateChange(ChatState state);

		// Token: 0x060022E7 RID: 8935
		void OnGetMessages(string channelName, string[] senders, object[] messages);

		// Token: 0x060022E8 RID: 8936
		void OnPrivateMessage(string sender, object message, string channelName);

		// Token: 0x060022E9 RID: 8937
		void OnSubscribed(string[] channels, bool[] results);

		// Token: 0x060022EA RID: 8938
		void OnUnsubscribed(string[] channels);

		// Token: 0x060022EB RID: 8939
		void OnStatusUpdate(string user, int status, bool gotMessage, object message);

		// Token: 0x060022EC RID: 8940
		void OnUserSubscribed(string channel, string user);

		// Token: 0x060022ED RID: 8941
		void OnUserUnsubscribed(string channel, string user);
	}
}
