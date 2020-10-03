using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace Photon.Chat
{
	// Token: 0x0200045B RID: 1115
	public class ChatClient : IPhotonPeerListener
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x000AA65D File Offset: 0x000A885D
		// (set) Token: 0x0600227F RID: 8831 RVA: 0x000AA665 File Offset: 0x000A8865
		public string NameServerAddress { get; private set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x000AA66E File Offset: 0x000A886E
		// (set) Token: 0x06002281 RID: 8833 RVA: 0x000AA676 File Offset: 0x000A8876
		public string FrontendAddress { get; private set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x000AA67F File Offset: 0x000A887F
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x000AA687 File Offset: 0x000A8887
		public string ChatRegion
		{
			get
			{
				return this.chatRegion;
			}
			set
			{
				this.chatRegion = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x000AA690 File Offset: 0x000A8890
		// (set) Token: 0x06002285 RID: 8837 RVA: 0x000AA698 File Offset: 0x000A8898
		public ChatState State { get; private set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x000AA6A1 File Offset: 0x000A88A1
		// (set) Token: 0x06002287 RID: 8839 RVA: 0x000AA6A9 File Offset: 0x000A88A9
		public ChatDisconnectCause DisconnectedCause { get; private set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x000AA6B2 File Offset: 0x000A88B2
		public bool CanChat
		{
			get
			{
				return this.State == ChatState.ConnectedToFrontEnd && this.HasPeer;
			}
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000AA6C5 File Offset: 0x000A88C5
		public bool CanChatInChannel(string channelName)
		{
			return this.CanChat && this.PublicChannels.ContainsKey(channelName) && !this.PublicChannelsUnsubscribing.Contains(channelName);
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x000AA6EE File Offset: 0x000A88EE
		private bool HasPeer
		{
			get
			{
				return this.chatPeer != null;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x000AA6F9 File Offset: 0x000A88F9
		// (set) Token: 0x0600228C RID: 8844 RVA: 0x000AA701 File Offset: 0x000A8901
		public string AppVersion { get; private set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x000AA70A File Offset: 0x000A890A
		// (set) Token: 0x0600228E RID: 8846 RVA: 0x000AA712 File Offset: 0x000A8912
		public string AppId { get; private set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x000AA71B File Offset: 0x000A891B
		// (set) Token: 0x06002290 RID: 8848 RVA: 0x000AA723 File Offset: 0x000A8923
		public AuthenticationValues AuthValues { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x000AA72C File Offset: 0x000A892C
		// (set) Token: 0x06002292 RID: 8850 RVA: 0x000AA743 File Offset: 0x000A8943
		public string UserId
		{
			get
			{
				if (this.AuthValues == null)
				{
					return null;
				}
				return this.AuthValues.UserId;
			}
			private set
			{
				if (this.AuthValues == null)
				{
					this.AuthValues = new AuthenticationValues();
				}
				this.AuthValues.UserId = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x000AA764 File Offset: 0x000A8964
		// (set) Token: 0x06002294 RID: 8852 RVA: 0x000AA76C File Offset: 0x000A896C
		public bool UseBackgroundWorkerForSending { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x000AA775 File Offset: 0x000A8975
		// (set) Token: 0x06002296 RID: 8854 RVA: 0x000AA784 File Offset: 0x000A8984
		public ConnectionProtocol TransportProtocol
		{
			get
			{
				return this.chatPeer.TransportProtocol;
			}
			set
			{
				if (this.chatPeer == null || this.chatPeer.PeerState != PeerStateValue.Disconnected)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "Can't set TransportProtocol. Disconnect first! " + ((this.chatPeer != null) ? ("PeerState: " + this.chatPeer.PeerState) : "The chatPeer is null."));
					return;
				}
				this.chatPeer.TransportProtocol = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x000AA7F2 File Offset: 0x000A89F2
		public Dictionary<ConnectionProtocol, Type> SocketImplementationConfig
		{
			get
			{
				return this.chatPeer.SocketImplementationConfig;
			}
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000AA800 File Offset: 0x000A8A00
		public ChatClient(IChatClientListener listener, ConnectionProtocol protocol = ConnectionProtocol.Udp)
		{
			this.listener = listener;
			this.State = ChatState.Uninitialized;
			this.chatPeer = new ChatPeer(this, protocol);
			this.chatPeer.SerializationProtocolType = SerializationProtocol.GpBinaryV18;
			this.PublicChannels = new Dictionary<string, ChatChannel>();
			this.PrivateChannels = new Dictionary<string, ChatChannel>();
			this.PublicChannelsUnsubscribing = new HashSet<string>();
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000AA870 File Offset: 0x000A8A70
		public bool Connect(string appId, string appVersion, AuthenticationValues authValues)
		{
			this.chatPeer.TimePingInterval = 3000;
			this.DisconnectedCause = ChatDisconnectCause.None;
			this.AuthValues = authValues;
			this.AppId = appId;
			this.AppVersion = appVersion;
			this.didAuthenticate = false;
			this.chatPeer.QuickResendAttempts = 2;
			this.chatPeer.SentCountAllowance = 7;
			this.PublicChannels.Clear();
			this.PrivateChannels.Clear();
			this.PublicChannelsUnsubscribing.Clear();
			this.NameServerAddress = this.chatPeer.NameServerAddress;
			bool flag = this.chatPeer.Connect();
			if (flag)
			{
				this.State = ChatState.ConnectingToNameServer;
			}
			if (this.UseBackgroundWorkerForSending)
			{
				SupportClass.StartBackgroundCalls(new Func<bool>(this.SendOutgoingInBackground), this.msDeltaForServiceCalls, "ChatClient Service Thread");
			}
			return flag;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000AA934 File Offset: 0x000A8B34
		public bool ConnectAndSetStatus(string appId, string appVersion, AuthenticationValues authValues, int status = 2, object message = null)
		{
			this.statusToSetWhenConnected = new int?(status);
			this.messageToSetWhenConnected = message;
			return this.Connect(appId, appVersion, authValues);
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000AA954 File Offset: 0x000A8B54
		public void Service()
		{
			while (this.HasPeer && this.chatPeer.DispatchIncomingCommands())
			{
			}
			if (!this.UseBackgroundWorkerForSending && (Environment.TickCount - this.msTimestampOfLastServiceCall > this.msDeltaForServiceCalls || this.msTimestampOfLastServiceCall == 0))
			{
				this.msTimestampOfLastServiceCall = Environment.TickCount;
				while (this.HasPeer && this.chatPeer.SendOutgoingCommands())
				{
				}
			}
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000AA9BA File Offset: 0x000A8BBA
		private bool SendOutgoingInBackground()
		{
			while (this.HasPeer && this.chatPeer.SendOutgoingCommands())
			{
			}
			return this.State != ChatState.Disconnected;
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000AA9DE File Offset: 0x000A8BDE
		[Obsolete("Better use UseBackgroundWorkerForSending and Service().")]
		public void SendAcksOnly()
		{
			if (this.HasPeer)
			{
				this.chatPeer.SendAcksOnly();
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000AA9F4 File Offset: 0x000A8BF4
		public void Disconnect()
		{
			if (this.HasPeer && this.chatPeer.PeerState != PeerStateValue.Disconnected)
			{
				this.chatPeer.Disconnect();
			}
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000AAA16 File Offset: 0x000A8C16
		public void StopThread()
		{
			if (this.HasPeer)
			{
				this.chatPeer.StopThread();
			}
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000AAA2B File Offset: 0x000A8C2B
		public bool Subscribe(string[] channels)
		{
			return this.Subscribe(channels, 0);
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x000AAA38 File Offset: 0x000A8C38
		public bool Subscribe(string[] channels, int[] lastMsgIds)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Subscribe called while not connected to front end server.");
				}
				return false;
			}
			if (channels == null || channels.Length == 0)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "Subscribe can't be called for empty or null channels-list.");
				}
				return false;
			}
			for (int i = 0; i < channels.Length; i++)
			{
				if (string.IsNullOrEmpty(channels[i]))
				{
					if (this.DebugOut >= DebugLevel.ERROR)
					{
						this.listener.DebugReturn(DebugLevel.ERROR, string.Format("Subscribe can't be called with a null or empty channel name at index {0}.", i));
					}
					return false;
				}
			}
			if (lastMsgIds == null || lastMsgIds.Length != channels.Length)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Subscribe can't be called when \"lastMsgIds\" array is null or does not have the same length as \"channels\" array.");
				}
				return false;
			}
			Dictionary<byte, object> operationParameters = new Dictionary<byte, object>
			{
				{
					0,
					channels
				},
				{
					9,
					lastMsgIds
				},
				{
					14,
					-1
				}
			};
			return this.chatPeer.SendOperation(0, operationParameters, SendOptions.SendReliable);
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x000AAB2C File Offset: 0x000A8D2C
		public bool Subscribe(string[] channels, int messagesFromHistory)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Subscribe called while not connected to front end server.");
				}
				return false;
			}
			if (channels == null || channels.Length == 0)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "Subscribe can't be called for empty or null channels-list.");
				}
				return false;
			}
			return this.SendChannelOperation(channels, 0, messagesFromHistory);
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000AAB8C File Offset: 0x000A8D8C
		public bool Unsubscribe(string[] channels)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Unsubscribe called while not connected to front end server.");
				}
				return false;
			}
			if (channels == null || channels.Length == 0)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "Unsubscribe can't be called for empty or null channels-list.");
				}
				return false;
			}
			foreach (string item in channels)
			{
				this.PublicChannelsUnsubscribing.Add(item);
			}
			return this.SendChannelOperation(channels, 1, 0);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000AAC0A File Offset: 0x000A8E0A
		public bool PublishMessage(string channelName, object message, bool forwardAsWebhook = false)
		{
			return this.publishMessage(channelName, message, true, forwardAsWebhook);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000AAC16 File Offset: 0x000A8E16
		internal bool PublishMessageUnreliable(string channelName, object message, bool forwardAsWebhook = false)
		{
			return this.publishMessage(channelName, message, false, forwardAsWebhook);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000AAC24 File Offset: 0x000A8E24
		private bool publishMessage(string channelName, object message, bool reliable, bool forwardAsWebhook = false)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "PublishMessage called while not connected to front end server.");
				}
				return false;
			}
			if (string.IsNullOrEmpty(channelName) || message == null)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "PublishMessage parameters must be non-null and not empty.");
				}
				return false;
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>
			{
				{
					1,
					channelName
				},
				{
					3,
					message
				}
			};
			if (forwardAsWebhook)
			{
				dictionary.Add(21, 1);
			}
			return this.chatPeer.SendOperation(2, dictionary, new SendOptions
			{
				Reliability = reliable
			});
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000AACC2 File Offset: 0x000A8EC2
		public bool SendPrivateMessage(string target, object message, bool forwardAsWebhook = false)
		{
			return this.SendPrivateMessage(target, message, false, forwardAsWebhook);
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000AACCE File Offset: 0x000A8ECE
		public bool SendPrivateMessage(string target, object message, bool encrypt, bool forwardAsWebhook)
		{
			return this.sendPrivateMessage(target, message, encrypt, true, forwardAsWebhook);
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000AACDC File Offset: 0x000A8EDC
		internal bool SendPrivateMessageUnreliable(string target, object message, bool encrypt, bool forwardAsWebhook = false)
		{
			return this.sendPrivateMessage(target, message, encrypt, false, forwardAsWebhook);
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000AACEC File Offset: 0x000A8EEC
		private bool sendPrivateMessage(string target, object message, bool encrypt, bool reliable, bool forwardAsWebhook = false)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "SendPrivateMessage called while not connected to front end server.");
				}
				return false;
			}
			if (string.IsNullOrEmpty(target) || message == null)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "SendPrivateMessage parameters must be non-null and not empty.");
				}
				return false;
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>
			{
				{
					225,
					target
				},
				{
					3,
					message
				}
			};
			if (forwardAsWebhook)
			{
				dictionary.Add(21, 1);
			}
			return this.chatPeer.SendOperation(3, dictionary, new SendOptions
			{
				Reliability = reliable,
				Encrypt = encrypt
			});
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000AAD98 File Offset: 0x000A8F98
		private bool SetOnlineStatus(int status, object message, bool skipMessage)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "SetOnlineStatus called while not connected to front end server.");
				}
				return false;
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>
			{
				{
					10,
					status
				}
			};
			if (skipMessage)
			{
				dictionary[12] = true;
			}
			else
			{
				dictionary[3] = message;
			}
			return this.chatPeer.SendOperation(5, dictionary, SendOptions.SendReliable);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000AAE0A File Offset: 0x000A900A
		public bool SetOnlineStatus(int status)
		{
			return this.SetOnlineStatus(status, null, true);
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000AAE15 File Offset: 0x000A9015
		public bool SetOnlineStatus(int status, object message)
		{
			return this.SetOnlineStatus(status, message, false);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000AAE20 File Offset: 0x000A9020
		public bool AddFriends(string[] friends)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "AddFriends called while not connected to front end server.");
				}
				return false;
			}
			if (friends == null || friends.Length == 0)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "AddFriends can't be called for empty or null list.");
				}
				return false;
			}
			if (friends.Length > 1024)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, string.Concat(new object[]
					{
						"AddFriends max list size exceeded: ",
						friends.Length,
						" > ",
						1024
					}));
				}
				return false;
			}
			Dictionary<byte, object> operationParameters = new Dictionary<byte, object>
			{
				{
					11,
					friends
				}
			};
			return this.chatPeer.SendOperation(6, operationParameters, SendOptions.SendReliable);
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000AAEEC File Offset: 0x000A90EC
		public bool RemoveFriends(string[] friends)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "RemoveFriends called while not connected to front end server.");
				}
				return false;
			}
			if (friends == null || friends.Length == 0)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "RemoveFriends can't be called for empty or null list.");
				}
				return false;
			}
			if (friends.Length > 1024)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, string.Concat(new object[]
					{
						"RemoveFriends max list size exceeded: ",
						friends.Length,
						" > ",
						1024
					}));
				}
				return false;
			}
			Dictionary<byte, object> operationParameters = new Dictionary<byte, object>
			{
				{
					11,
					friends
				}
			};
			return this.chatPeer.SendOperation(7, operationParameters, SendOptions.SendReliable);
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000AAFB5 File Offset: 0x000A91B5
		public string GetPrivateChannelNameByUser(string userName)
		{
			return string.Format("{0}:{1}", this.UserId, userName);
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000AAFC8 File Offset: 0x000A91C8
		public bool TryGetChannel(string channelName, bool isPrivate, out ChatChannel channel)
		{
			if (!isPrivate)
			{
				return this.PublicChannels.TryGetValue(channelName, out channel);
			}
			return this.PrivateChannels.TryGetValue(channelName, out channel);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000AAFE8 File Offset: 0x000A91E8
		public bool TryGetChannel(string channelName, out ChatChannel channel)
		{
			return this.PublicChannels.TryGetValue(channelName, out channel) || this.PrivateChannels.TryGetValue(channelName, out channel);
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000AB008 File Offset: 0x000A9208
		public bool TryGetPrivateChannelByUser(string userId, out ChatChannel channel)
		{
			channel = null;
			if (string.IsNullOrEmpty(userId))
			{
				return false;
			}
			string privateChannelNameByUser = this.GetPrivateChannelNameByUser(userId);
			return this.TryGetChannel(privateChannelNameByUser, true, out channel);
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x000AB041 File Offset: 0x000A9241
		// (set) Token: 0x060022B4 RID: 8884 RVA: 0x000AB033 File Offset: 0x000A9233
		public DebugLevel DebugOut
		{
			get
			{
				return this.chatPeer.DebugOut;
			}
			set
			{
				this.chatPeer.DebugOut = value;
			}
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x000AB04E File Offset: 0x000A924E
		void IPhotonPeerListener.DebugReturn(DebugLevel level, string message)
		{
			this.listener.DebugReturn(level, message);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000AB060 File Offset: 0x000A9260
		void IPhotonPeerListener.OnEvent(EventData eventData)
		{
			switch (eventData.Code)
			{
			case 0:
				this.HandleChatMessagesEvent(eventData);
				return;
			case 1:
			case 3:
			case 7:
				break;
			case 2:
				this.HandlePrivateMessageEvent(eventData);
				return;
			case 4:
				this.HandleStatusUpdate(eventData);
				return;
			case 5:
				this.HandleSubscribeEvent(eventData);
				return;
			case 6:
				this.HandleUnsubscribeEvent(eventData);
				return;
			case 8:
				this.HandleUserSubscribedEvent(eventData);
				return;
			case 9:
				this.HandleUserUnsubscribedEvent(eventData);
				break;
			default:
				return;
			}
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000AB0DC File Offset: 0x000A92DC
		void IPhotonPeerListener.OnOperationResponse(OperationResponse operationResponse)
		{
			byte operationCode = operationResponse.OperationCode;
			if (operationCode > 3 && operationCode == 230)
			{
				this.HandleAuthResponse(operationResponse);
				return;
			}
			if (operationResponse.ReturnCode != 0 && this.DebugOut >= DebugLevel.ERROR)
			{
				if (operationResponse.ReturnCode == -2)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, string.Format("Chat Operation {0} unknown on server. Check your AppId and make sure it's for a Chat application.", operationResponse.OperationCode));
					return;
				}
				this.listener.DebugReturn(DebugLevel.ERROR, string.Format("Chat Operation {0} failed (Code: {1}). Debug Message: {2}", operationResponse.OperationCode, operationResponse.ReturnCode, operationResponse.DebugMessage));
			}
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000AB174 File Offset: 0x000A9374
		void IPhotonPeerListener.OnStatusChanged(StatusCode statusCode)
		{
			if (statusCode <= StatusCode.Disconnect)
			{
				if (statusCode != StatusCode.Connect)
				{
					if (statusCode != StatusCode.Disconnect)
					{
						return;
					}
					if (this.State == ChatState.Authenticated)
					{
						this.ConnectToFrontEnd();
						return;
					}
					this.State = ChatState.Disconnected;
					this.listener.OnChatStateChange(ChatState.Disconnected);
					this.listener.OnDisconnected();
				}
				else
				{
					if (!this.chatPeer.IsProtocolSecure)
					{
						this.chatPeer.EstablishEncryption();
					}
					else if (!this.didAuthenticate)
					{
						this.didAuthenticate = this.chatPeer.AuthenticateOnNameServer(this.AppId, this.AppVersion, this.chatRegion, this.AuthValues);
						if (!this.didAuthenticate && this.DebugOut >= DebugLevel.ERROR)
						{
							((IPhotonPeerListener)this).DebugReturn(DebugLevel.ERROR, "Error calling OpAuthenticate! Did not work. Check log output, AuthValues and if you're connected. State: " + this.State);
						}
					}
					if (this.State == ChatState.ConnectingToNameServer)
					{
						this.State = ChatState.ConnectedToNameServer;
						this.listener.OnChatStateChange(this.State);
						return;
					}
					if (this.State == ChatState.ConnectingToFrontEnd)
					{
						this.AuthenticateOnFrontEnd();
						return;
					}
				}
			}
			else if (statusCode != StatusCode.EncryptionEstablished)
			{
				if (statusCode != StatusCode.EncryptionFailedToEstablish)
				{
					return;
				}
				this.State = ChatState.Disconnecting;
				this.chatPeer.Disconnect();
				return;
			}
			else if (!this.didAuthenticate)
			{
				this.didAuthenticate = this.chatPeer.AuthenticateOnNameServer(this.AppId, this.AppVersion, this.chatRegion, this.AuthValues);
				if (!this.didAuthenticate && this.DebugOut >= DebugLevel.ERROR)
				{
					((IPhotonPeerListener)this).DebugReturn(DebugLevel.ERROR, "Error calling OpAuthenticate! Did not work. Check log output, AuthValues and if you're connected. State: " + this.State);
					return;
				}
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000AB30C File Offset: 0x000A950C
		private bool SendChannelOperation(string[] channels, byte operation, int historyLength)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>
			{
				{
					0,
					channels
				}
			};
			if (historyLength != 0)
			{
				dictionary.Add(14, historyLength);
			}
			return this.chatPeer.SendOperation(operation, dictionary, SendOptions.SendReliable);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000AB34C File Offset: 0x000A954C
		private void HandlePrivateMessageEvent(EventData eventData)
		{
			object message = eventData.Parameters[3];
			string text = (string)eventData.Parameters[5];
			int msgId = (int)eventData.Parameters[8];
			string privateChannelNameByUser;
			if (this.UserId != null && this.UserId.Equals(text))
			{
				string userName = (string)eventData.Parameters[225];
				privateChannelNameByUser = this.GetPrivateChannelNameByUser(userName);
			}
			else
			{
				privateChannelNameByUser = this.GetPrivateChannelNameByUser(text);
			}
			ChatChannel chatChannel;
			if (!this.PrivateChannels.TryGetValue(privateChannelNameByUser, out chatChannel))
			{
				chatChannel = new ChatChannel(privateChannelNameByUser);
				chatChannel.IsPrivate = true;
				chatChannel.MessageLimit = this.MessageLimit;
				this.PrivateChannels.Add(chatChannel.Name, chatChannel);
			}
			chatChannel.Add(text, message, msgId);
			this.listener.OnPrivateMessage(text, message, privateChannelNameByUser);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000AB424 File Offset: 0x000A9624
		private void HandleChatMessagesEvent(EventData eventData)
		{
			object[] messages = (object[])eventData.Parameters[2];
			string[] senders = (string[])eventData.Parameters[4];
			string text = (string)eventData.Parameters[1];
			int lastMsgId = (int)eventData.Parameters[8];
			ChatChannel chatChannel;
			if (!this.PublicChannels.TryGetValue(text, out chatChannel))
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "Channel " + text + " for incoming message event not found.");
				}
				return;
			}
			chatChannel.Add(senders, messages, lastMsgId);
			this.listener.OnGetMessages(text, senders, messages);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x000AB4C8 File Offset: 0x000A96C8
		private void HandleSubscribeEvent(EventData eventData)
		{
			string[] array = (string[])eventData.Parameters[0];
			bool[] array2 = (bool[])eventData.Parameters[15];
			object obj;
			if (eventData.Parameters.TryGetValue(22, out obj))
			{
				Dictionary<object, object> newProperties = obj as Dictionary<object, object>;
				if (array.Length == 1)
				{
					if (array2[0])
					{
						string text = array[0];
						ChatChannel chatChannel;
						if (this.PublicChannels.TryGetValue(text, out chatChannel))
						{
							chatChannel.Subscribers.Clear();
							chatChannel.ClearProperties();
						}
						else
						{
							chatChannel = new ChatChannel(text);
							chatChannel.MessageLimit = this.MessageLimit;
							this.PublicChannels.Add(chatChannel.Name, chatChannel);
						}
						chatChannel.ReadProperties(newProperties);
						if (chatChannel.PublishSubscribers)
						{
							chatChannel.Subscribers.Add(this.UserId);
							if (eventData.Parameters.TryGetValue(23, out obj))
							{
								string[] users = obj as string[];
								chatChannel.AddSubscribers(users);
							}
						}
					}
					this.listener.OnSubscribed(array, array2);
					return;
				}
				this.listener.DebugReturn(DebugLevel.ERROR, "Unexpected: Subscribe event for multiple channels with channels properties returned. Ignoring properties.");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array2[i])
				{
					string text2 = array[i];
					ChatChannel chatChannel2;
					if (!this.PublicChannels.TryGetValue(text2, out chatChannel2))
					{
						chatChannel2 = new ChatChannel(text2);
						chatChannel2.MessageLimit = this.MessageLimit;
						this.PublicChannels.Add(chatChannel2.Name, chatChannel2);
					}
				}
			}
			this.listener.OnSubscribed(array, array2);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000AB648 File Offset: 0x000A9848
		private void HandleUnsubscribeEvent(EventData eventData)
		{
			foreach (string text in (string[])eventData[0])
			{
				this.PublicChannels.Remove(text);
				this.PublicChannelsUnsubscribing.Remove(text);
			}
			string[] array;
			this.listener.OnUnsubscribed(array);
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x000AB69C File Offset: 0x000A989C
		private void HandleAuthResponse(OperationResponse operationResponse)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				this.listener.DebugReturn(DebugLevel.INFO, operationResponse.ToStringFull() + " on: " + this.chatPeer.NameServerAddress);
			}
			if (operationResponse.ReturnCode == 0)
			{
				if (this.State == ChatState.ConnectedToNameServer)
				{
					this.State = ChatState.Authenticated;
					this.listener.OnChatStateChange(this.State);
					if (operationResponse.Parameters.ContainsKey(221))
					{
						if (this.AuthValues == null)
						{
							this.AuthValues = new AuthenticationValues();
						}
						this.AuthValues.Token = (operationResponse[221] as string);
						this.FrontendAddress = (string)operationResponse[230];
						this.chatPeer.Disconnect();
					}
					else if (this.DebugOut >= DebugLevel.ERROR)
					{
						this.listener.DebugReturn(DebugLevel.ERROR, "No secret in authentication response.");
					}
					if (operationResponse.Parameters.ContainsKey(225))
					{
						string text = operationResponse.Parameters[225] as string;
						if (!string.IsNullOrEmpty(text))
						{
							this.UserId = text;
							this.listener.DebugReturn(DebugLevel.INFO, string.Format("Received your UserID from server. Updating local value to: {0}", this.UserId));
							return;
						}
					}
				}
				else if (this.State == ChatState.ConnectingToFrontEnd)
				{
					this.State = ChatState.ConnectedToFrontEnd;
					this.listener.OnChatStateChange(this.State);
					this.listener.OnConnected();
					if (this.statusToSetWhenConnected != null)
					{
						this.SetOnlineStatus(this.statusToSetWhenConnected.Value, this.messageToSetWhenConnected);
						this.statusToSetWhenConnected = null;
						return;
					}
				}
			}
			else
			{
				short returnCode = operationResponse.ReturnCode;
				if (returnCode != -3)
				{
					switch (returnCode)
					{
					case 32755:
						this.DisconnectedCause = ChatDisconnectCause.CustomAuthenticationFailed;
						break;
					case 32756:
						this.DisconnectedCause = ChatDisconnectCause.InvalidRegion;
						break;
					case 32757:
						this.DisconnectedCause = ChatDisconnectCause.MaxCcuReached;
						break;
					default:
						if (returnCode == 32767)
						{
							this.DisconnectedCause = ChatDisconnectCause.InvalidAuthentication;
						}
						break;
					}
				}
				else
				{
					this.DisconnectedCause = ChatDisconnectCause.OperationNotAllowedInCurrentState;
				}
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Authentication request error: " + operationResponse.ReturnCode + ". Disconnecting.");
				}
				this.State = ChatState.Disconnecting;
				this.chatPeer.Disconnect();
			}
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000AB8E0 File Offset: 0x000A9AE0
		private void HandleStatusUpdate(EventData eventData)
		{
			string user = (string)eventData.Parameters[5];
			int status = (int)eventData.Parameters[10];
			object message = null;
			bool flag = eventData.Parameters.ContainsKey(3);
			if (flag)
			{
				message = eventData.Parameters[3];
			}
			this.listener.OnStatusUpdate(user, status, flag, message);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000AB940 File Offset: 0x000A9B40
		private void ConnectToFrontEnd()
		{
			this.State = ChatState.ConnectingToFrontEnd;
			if (this.DebugOut >= DebugLevel.INFO)
			{
				this.listener.DebugReturn(DebugLevel.INFO, "Connecting to frontend " + this.FrontendAddress);
			}
			this.chatPeer.Connect(this.FrontendAddress, "chat");
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x000AB990 File Offset: 0x000A9B90
		private bool AuthenticateOnFrontEnd()
		{
			if (this.AuthValues == null)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Can't authenticate on front end server. Authentication Values are not set");
				}
				return false;
			}
			if (string.IsNullOrEmpty(this.AuthValues.Token))
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Can't authenticate on front end server. Secret is not set");
				}
				return false;
			}
			Dictionary<byte, object> operationParameters = new Dictionary<byte, object>
			{
				{
					221,
					this.AuthValues.Token
				}
			};
			return this.chatPeer.SendOperation(230, operationParameters, SendOptions.SendReliable);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000ABA24 File Offset: 0x000A9C24
		private void HandleUserUnsubscribedEvent(EventData eventData)
		{
			string text = eventData.Parameters[1] as string;
			string text2 = eventData.Parameters[225] as string;
			ChatChannel chatChannel;
			if (this.PublicChannels.TryGetValue(text, out chatChannel))
			{
				if (!chatChannel.PublishSubscribers && this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, string.Format("Channel \"{0}\" for incoming UserUnsubscribed (\"{1}\") event does not have PublishSubscribers enabled.", text, text2));
				}
				if (!chatChannel.Subscribers.Remove(text2) && this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, string.Format("Channel \"{0}\" does not contain unsubscribed user \"{1}\".", text, text2));
				}
			}
			else if (this.DebugOut >= DebugLevel.WARNING)
			{
				this.listener.DebugReturn(DebugLevel.WARNING, string.Format("Channel \"{0}\" not found for incoming UserUnsubscribed (\"{1}\") event.", text, text2));
			}
			this.listener.OnUserUnsubscribed(text, text2);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000ABAF4 File Offset: 0x000A9CF4
		private void HandleUserSubscribedEvent(EventData eventData)
		{
			string text = eventData.Parameters[1] as string;
			string text2 = eventData.Parameters[225] as string;
			ChatChannel chatChannel;
			if (this.PublicChannels.TryGetValue(text, out chatChannel))
			{
				if (!chatChannel.PublishSubscribers && this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, string.Format("Channel \"{0}\" for incoming UserSubscribed (\"{1}\") event does not have PublishSubscribers enabled.", text, text2));
				}
				if (!chatChannel.Subscribers.Add(text2))
				{
					if (this.DebugOut >= DebugLevel.WARNING)
					{
						this.listener.DebugReturn(DebugLevel.WARNING, string.Format("Channel \"{0}\" already contains newly subscribed user \"{1}\".", text, text2));
					}
				}
				else if (chatChannel.MaxSubscribers > 0 && chatChannel.Subscribers.Count > chatChannel.MaxSubscribers && this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, string.Format("Channel \"{0}\"'s MaxSubscribers exceeded. count={1} > MaxSubscribers={2}.", text, chatChannel.Subscribers.Count, chatChannel.MaxSubscribers));
				}
			}
			else if (this.DebugOut >= DebugLevel.WARNING)
			{
				this.listener.DebugReturn(DebugLevel.WARNING, string.Format("Channel \"{0}\" not found for incoming UserSubscribed (\"{1}\") event.", text, text2));
			}
			this.listener.OnUserSubscribed(text, text2);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000ABC20 File Offset: 0x000A9E20
		public bool Subscribe(string channel, int lastMsgId = 0, int messagesFromHistory = -1, ChannelCreationOptions creationOptions = null)
		{
			if (creationOptions == null)
			{
				creationOptions = ChannelCreationOptions.Default;
			}
			int maxSubscribers = creationOptions.MaxSubscribers;
			bool publishSubscribers = creationOptions.PublishSubscribers;
			if (maxSubscribers < 0)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Cannot set MaxSubscribers < 0.");
				}
				return false;
			}
			if (lastMsgId < 0)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "lastMsgId cannot be < 0.");
				}
				return false;
			}
			if (messagesFromHistory < -1)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "messagesFromHistory < -1, setting it to -1");
				}
				messagesFromHistory = -1;
			}
			if (lastMsgId > 0 && messagesFromHistory == 0)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "lastMsgId will be ignored because messagesFromHistory == 0");
				}
				lastMsgId = 0;
			}
			Dictionary<object, object> dictionary = null;
			if (publishSubscribers)
			{
				if (maxSubscribers > 100)
				{
					if (this.DebugOut >= DebugLevel.ERROR)
					{
						this.listener.DebugReturn(DebugLevel.ERROR, string.Format("Cannot set MaxSubscribers > {0} when PublishSubscribers == true.", 100));
					}
					return false;
				}
				dictionary = new Dictionary<object, object>();
				dictionary[254] = true;
			}
			if (maxSubscribers > 0)
			{
				if (dictionary == null)
				{
					dictionary = new Dictionary<object, object>();
				}
				dictionary[byte.MaxValue] = maxSubscribers;
			}
			Dictionary<byte, object> dictionary2 = new Dictionary<byte, object>
			{
				{
					0,
					new string[]
					{
						channel
					}
				}
			};
			if (messagesFromHistory != 0)
			{
				dictionary2.Add(14, messagesFromHistory);
			}
			if (lastMsgId > 0)
			{
				dictionary2.Add(9, new int[]
				{
					lastMsgId
				});
			}
			if (dictionary != null && dictionary.Count > 0)
			{
				dictionary2.Add(22, dictionary);
			}
			return this.chatPeer.SendOperation(0, dictionary2, SendOptions.SendReliable);
		}

		// Token: 0x04001FFB RID: 8187
		private const int FriendRequestListMax = 1024;

		// Token: 0x04001FFC RID: 8188
		public const int DefaultMaxSubscribers = 100;

		// Token: 0x04001FFF RID: 8191
		private string chatRegion = "EU";

		// Token: 0x04002005 RID: 8197
		public int MessageLimit;

		// Token: 0x04002006 RID: 8198
		public readonly Dictionary<string, ChatChannel> PublicChannels;

		// Token: 0x04002007 RID: 8199
		public readonly Dictionary<string, ChatChannel> PrivateChannels;

		// Token: 0x04002008 RID: 8200
		private readonly HashSet<string> PublicChannelsUnsubscribing;

		// Token: 0x04002009 RID: 8201
		private readonly IChatClientListener listener;

		// Token: 0x0400200A RID: 8202
		public ChatPeer chatPeer;

		// Token: 0x0400200B RID: 8203
		private const string ChatAppName = "chat";

		// Token: 0x0400200C RID: 8204
		private bool didAuthenticate;

		// Token: 0x0400200D RID: 8205
		private int? statusToSetWhenConnected;

		// Token: 0x0400200E RID: 8206
		private object messageToSetWhenConnected;

		// Token: 0x0400200F RID: 8207
		private int msDeltaForServiceCalls = 50;

		// Token: 0x04002010 RID: 8208
		private int msTimestampOfLastServiceCall;
	}
}
