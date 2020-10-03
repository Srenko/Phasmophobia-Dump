using System;
using System.Collections.Generic;

namespace Viveport
{
	// Token: 0x02000209 RID: 521
	public class SubscriptionStatus
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0005D3E5 File Offset: 0x0005B5E5
		// (set) Token: 0x06000E91 RID: 3729 RVA: 0x0005D3ED File Offset: 0x0005B5ED
		public List<SubscriptionStatus.Platform> Platforms { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0005D3F6 File Offset: 0x0005B5F6
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x0005D3FE File Offset: 0x0005B5FE
		public SubscriptionStatus.TransactionType Type { get; set; }

		// Token: 0x06000E94 RID: 3732 RVA: 0x0005D407 File Offset: 0x0005B607
		public SubscriptionStatus()
		{
			this.Platforms = new List<SubscriptionStatus.Platform>();
			this.Type = SubscriptionStatus.TransactionType.Unknown;
		}

		// Token: 0x02000595 RID: 1429
		public enum Platform
		{
			// Token: 0x04002661 RID: 9825
			Windows,
			// Token: 0x04002662 RID: 9826
			Android
		}

		// Token: 0x02000596 RID: 1430
		public enum TransactionType
		{
			// Token: 0x04002664 RID: 9828
			Unknown,
			// Token: 0x04002665 RID: 9829
			Paid,
			// Token: 0x04002666 RID: 9830
			Redeem,
			// Token: 0x04002667 RID: 9831
			FreeTrial
		}
	}
}
