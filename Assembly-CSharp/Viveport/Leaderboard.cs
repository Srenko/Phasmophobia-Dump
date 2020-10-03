using System;

namespace Viveport
{
	// Token: 0x02000208 RID: 520
	public class Leaderboard
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0005D3B2 File Offset: 0x0005B5B2
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x0005D3BA File Offset: 0x0005B5BA
		public int Rank { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0005D3C3 File Offset: 0x0005B5C3
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x0005D3CB File Offset: 0x0005B5CB
		public int Score { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0005D3D4 File Offset: 0x0005B5D4
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x0005D3DC File Offset: 0x0005B5DC
		public string UserName { get; set; }
	}
}
