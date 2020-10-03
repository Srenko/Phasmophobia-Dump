using System;
using System.Collections.Generic;
using System.Diagnostics;
using ExitGames.Client.Photon;

namespace Photon.Chat
{
	// Token: 0x02000460 RID: 1120
	public class ChatPeer : PhotonPeer
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x000ABDA8 File Offset: 0x000A9FA8
		public string NameServerAddress
		{
			get
			{
				return this.GetNameServerAddress();
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x00012867 File Offset: 0x00010A67
		internal virtual bool IsProtocolSecure
		{
			get
			{
				return base.UsedProtocol == ConnectionProtocol.WebSocketSecure;
			}
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000ABDB0 File Offset: 0x000A9FB0
		public ChatPeer(IPhotonPeerListener listener, ConnectionProtocol protocol) : base(listener, protocol)
		{
			this.ConfigUnitySockets();
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000ABDC0 File Offset: 0x000A9FC0
		[Conditional("SUPPORTED_UNITY")]
		private void ConfigUnitySockets()
		{
			Type type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, PhotonWebSocket", false);
			if (type == null)
			{
				type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp-firstpass", false);
			}
			if (type == null)
			{
				type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp", false);
			}
			if (type != null)
			{
				this.SocketImplementationConfig[ConnectionProtocol.WebSocket] = type;
				this.SocketImplementationConfig[ConnectionProtocol.WebSocketSecure] = type;
			}
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000ABE28 File Offset: 0x000AA028
		private string GetNameServerAddress()
		{
			int num = 0;
			ChatPeer.ProtocolToNameServerPort.TryGetValue(base.TransportProtocol, out num);
			switch (base.TransportProtocol)
			{
			case ConnectionProtocol.Udp:
			case ConnectionProtocol.Tcp:
				return string.Format("{0}:{1}", "ns.exitgames.com", num);
			case ConnectionProtocol.WebSocket:
				return string.Format("ws://{0}:{1}", "ns.exitgames.com", num);
			case ConnectionProtocol.WebSocketSecure:
				return string.Format("wss://{0}:{1}", "ns.exitgames.com", num);
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000ABEB8 File Offset: 0x000AA0B8
		public bool Connect()
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "Connecting to nameserver " + this.NameServerAddress);
			}
			return this.Connect(this.NameServerAddress, "NameServer");
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000ABEF0 File Offset: 0x000AA0F0
		public bool AuthenticateOnNameServer(string appId, string appVersion, string region, AuthenticationValues authValues)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpAuthenticate()");
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary[220] = appVersion;
			dictionary[224] = appId;
			dictionary[210] = region;
			if (authValues != null)
			{
				if (!string.IsNullOrEmpty(authValues.UserId))
				{
					dictionary[225] = authValues.UserId;
				}
				if (authValues != null && authValues.AuthType != CustomAuthenticationType.None)
				{
					dictionary[217] = (byte)authValues.AuthType;
					if (!string.IsNullOrEmpty(authValues.Token))
					{
						dictionary[221] = authValues.Token;
					}
					else
					{
						if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
						{
							dictionary[216] = authValues.AuthGetParameters;
						}
						if (authValues.AuthPostData != null)
						{
							dictionary[214] = authValues.AuthPostData;
						}
					}
				}
			}
			return this.SendOperation(230, dictionary, new SendOptions
			{
				Reliability = true,
				Encrypt = base.IsEncryptionAvailable
			});
		}

		// Token: 0x04002043 RID: 8259
		public const string NameServerHost = "ns.exitgames.com";

		// Token: 0x04002044 RID: 8260
		public const string NameServerHttp = "http://ns.exitgamescloud.com:80/photon/n";

		// Token: 0x04002045 RID: 8261
		private static readonly Dictionary<ConnectionProtocol, int> ProtocolToNameServerPort = new Dictionary<ConnectionProtocol, int>
		{
			{
				ConnectionProtocol.Udp,
				5058
			},
			{
				ConnectionProtocol.Tcp,
				4533
			},
			{
				ConnectionProtocol.WebSocket,
				9093
			},
			{
				ConnectionProtocol.WebSocketSecure,
				19093
			}
		};
	}
}
