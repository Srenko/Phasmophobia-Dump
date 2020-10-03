using System;

namespace Photon.Realtime
{
	// Token: 0x02000468 RID: 1128
	public abstract class PhotonPing : IDisposable
	{
		// Token: 0x060022EE RID: 8942 RVA: 0x000AC17A File Offset: 0x000AA37A
		public virtual bool StartPing(string ip)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000AC17A File Offset: 0x000AA37A
		public virtual bool Done()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000AC17A File Offset: 0x000AA37A
		public virtual void Dispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000AC181 File Offset: 0x000AA381
		protected internal void Init()
		{
			this.GotResult = false;
			this.Successful = false;
			this.PingId = (byte)(Environment.TickCount % 255);
		}

		// Token: 0x04002080 RID: 8320
		public string DebugString = "";

		// Token: 0x04002081 RID: 8321
		public bool Successful;

		// Token: 0x04002082 RID: 8322
		protected internal bool GotResult;

		// Token: 0x04002083 RID: 8323
		protected internal int PingLength = 13;

		// Token: 0x04002084 RID: 8324
		protected internal byte[] PingBytes = new byte[]
		{
			125,
			125,
			125,
			125,
			125,
			125,
			125,
			125,
			125,
			125,
			125,
			125,
			0
		};

		// Token: 0x04002085 RID: 8325
		protected internal byte PingId;
	}
}
