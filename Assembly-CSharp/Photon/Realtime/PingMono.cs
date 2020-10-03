using System;
using System.Net.Sockets;

namespace Photon.Realtime
{
	// Token: 0x02000469 RID: 1129
	public class PingMono : PhotonPing
	{
		// Token: 0x060022F3 RID: 8947 RVA: 0x000AC1D8 File Offset: 0x000AA3D8
		public override bool StartPing(string ip)
		{
			base.Init();
			try
			{
				if (ip.Contains("."))
				{
					this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				}
				else
				{
					this.sock = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
				}
				this.sock.ReceiveTimeout = 5000;
				this.sock.Connect(ip, 5055);
				this.PingBytes[this.PingBytes.Length - 1] = this.PingId;
				this.sock.Send(this.PingBytes);
				this.PingBytes[this.PingBytes.Length - 1] = this.PingId - 1;
			}
			catch (Exception value)
			{
				this.sock = null;
				Console.WriteLine(value);
			}
			return false;
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000AC2A0 File Offset: 0x000AA4A0
		public override bool Done()
		{
			if (this.GotResult || this.sock == null)
			{
				return true;
			}
			if (this.sock.Available <= 0)
			{
				return false;
			}
			int num = this.sock.Receive(this.PingBytes, SocketFlags.None);
			if (this.PingBytes[this.PingBytes.Length - 1] != this.PingId || num != this.PingLength)
			{
				this.DebugString += " ReplyMatch is false! ";
			}
			this.Successful = (num == this.PingBytes.Length && this.PingBytes[this.PingBytes.Length - 1] == this.PingId);
			this.GotResult = true;
			return true;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000AC354 File Offset: 0x000AA554
		public override void Dispose()
		{
			try
			{
				this.sock.Close();
			}
			catch
			{
			}
			this.sock = null;
		}

		// Token: 0x04002086 RID: 8326
		private Socket sock;
	}
}
