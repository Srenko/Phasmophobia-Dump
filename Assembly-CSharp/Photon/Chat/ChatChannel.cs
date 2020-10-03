using System;
using System.Collections.Generic;
using System.Text;

namespace Photon.Chat
{
	// Token: 0x0200045A RID: 1114
	public class ChatChannel
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x000AA379 File Offset: 0x000A8579
		// (set) Token: 0x0600226D RID: 8813 RVA: 0x000AA381 File Offset: 0x000A8581
		public bool IsPrivate { get; protected internal set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x000AA38A File Offset: 0x000A858A
		public int MessageCount
		{
			get
			{
				return this.Messages.Count;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x000AA397 File Offset: 0x000A8597
		// (set) Token: 0x06002270 RID: 8816 RVA: 0x000AA39F File Offset: 0x000A859F
		public int LastMsgId { get; protected set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x000AA3A8 File Offset: 0x000A85A8
		// (set) Token: 0x06002272 RID: 8818 RVA: 0x000AA3B0 File Offset: 0x000A85B0
		public bool PublishSubscribers { get; protected set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x000AA3B9 File Offset: 0x000A85B9
		// (set) Token: 0x06002274 RID: 8820 RVA: 0x000AA3C1 File Offset: 0x000A85C1
		public int MaxSubscribers { get; protected set; }

		// Token: 0x06002275 RID: 8821 RVA: 0x000AA3CA File Offset: 0x000A85CA
		public ChatChannel(string name)
		{
			this.Name = name;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000AA3FA File Offset: 0x000A85FA
		public void Add(string sender, object message, int msgId)
		{
			this.Senders.Add(sender);
			this.Messages.Add(message);
			this.LastMsgId = msgId;
			this.TruncateMessages();
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000AA421 File Offset: 0x000A8621
		public void Add(string[] senders, object[] messages, int lastMsgId)
		{
			this.Senders.AddRange(senders);
			this.Messages.AddRange(messages);
			this.LastMsgId = lastMsgId;
			this.TruncateMessages();
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000AA448 File Offset: 0x000A8648
		public void TruncateMessages()
		{
			if (this.MessageLimit <= 0 || this.Messages.Count <= this.MessageLimit)
			{
				return;
			}
			int count = this.Messages.Count - this.MessageLimit;
			this.Senders.RemoveRange(0, count);
			this.Messages.RemoveRange(0, count);
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000AA49F File Offset: 0x000A869F
		public void ClearMessages()
		{
			this.Senders.Clear();
			this.Messages.Clear();
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000AA4B8 File Offset: 0x000A86B8
		public string ToStringMessages()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.Messages.Count; i++)
			{
				stringBuilder.AppendLine(string.Format("{0}: {1}", this.Senders[i], this.Messages[i]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x000AA510 File Offset: 0x000A8710
		internal void ReadProperties(Dictionary<object, object> newProperties)
		{
			if (newProperties != null && newProperties.Count > 0)
			{
				if (this.properties == null)
				{
					this.properties = new Dictionary<object, object>(newProperties.Count);
				}
				foreach (object key in newProperties.Keys)
				{
					if (newProperties[key] == null)
					{
						if (this.properties.ContainsKey(key))
						{
							this.properties.Remove(key);
						}
					}
					else
					{
						this.properties[key] = newProperties[key];
					}
				}
				object obj;
				if (this.properties.TryGetValue(254, out obj))
				{
					this.PublishSubscribers = (bool)obj;
				}
				if (this.properties.TryGetValue(255, out obj))
				{
					this.MaxSubscribers = (int)obj;
				}
			}
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000AA60C File Offset: 0x000A880C
		internal void AddSubscribers(string[] users)
		{
			if (users == null)
			{
				return;
			}
			for (int i = 0; i < users.Length; i++)
			{
				this.Subscribers.Add(users[i]);
			}
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000AA63A File Offset: 0x000A883A
		internal void ClearProperties()
		{
			if (this.properties != null && this.properties.Count > 0)
			{
				this.properties.Clear();
			}
		}

		// Token: 0x04001FF1 RID: 8177
		public readonly string Name;

		// Token: 0x04001FF2 RID: 8178
		public readonly List<string> Senders = new List<string>();

		// Token: 0x04001FF3 RID: 8179
		public readonly List<object> Messages = new List<object>();

		// Token: 0x04001FF4 RID: 8180
		public int MessageLimit;

		// Token: 0x04001FF7 RID: 8183
		private Dictionary<object, object> properties;

		// Token: 0x04001FFA RID: 8186
		public readonly HashSet<string> Subscribers = new HashSet<string>();
	}
}
