using System;
using System.Net.Sockets;
using Photon.Realtime;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class PingMonoEditor : PhotonPing
{
	// Token: 0x06000505 RID: 1285 RVA: 0x0001DA24 File Offset: 0x0001BC24
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

	// Token: 0x06000506 RID: 1286 RVA: 0x0001DAEC File Offset: 0x0001BCEC
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
			Debug.Log("ReplyMatch is false! ");
		}
		this.Successful = (num == this.PingBytes.Length && this.PingBytes[this.PingBytes.Length - 1] == this.PingId);
		this.GotResult = true;
		return true;
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x0001DB94 File Offset: 0x0001BD94
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

	// Token: 0x04000553 RID: 1363
	private Socket sock;
}
