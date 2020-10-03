using System;

namespace ExitGames.Client.DemoParticle
{
	// Token: 0x0200047F RID: 1151
	public class TimeKeeper
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x000B01EA File Offset: 0x000AE3EA
		// (set) Token: 0x060023F2 RID: 9202 RVA: 0x000B01F2 File Offset: 0x000AE3F2
		public int Interval { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x000B01FB File Offset: 0x000AE3FB
		// (set) Token: 0x060023F4 RID: 9204 RVA: 0x000B0203 File Offset: 0x000AE403
		public bool IsEnabled { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x000B020C File Offset: 0x000AE40C
		// (set) Token: 0x060023F6 RID: 9206 RVA: 0x000B0236 File Offset: 0x000AE436
		public bool ShouldExecute
		{
			get
			{
				return this.IsEnabled && (this.shouldExecute || Environment.TickCount - this.lastExecutionTime > this.Interval);
			}
			set
			{
				this.shouldExecute = value;
			}
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000B023F File Offset: 0x000AE43F
		public TimeKeeper(int interval)
		{
			this.IsEnabled = true;
			this.Interval = interval;
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000B0260 File Offset: 0x000AE460
		public void Reset()
		{
			this.shouldExecute = false;
			this.lastExecutionTime = Environment.TickCount;
		}

		// Token: 0x04002148 RID: 8520
		private int lastExecutionTime = Environment.TickCount;

		// Token: 0x04002149 RID: 8521
		private bool shouldExecute;
	}
}
