using System;
using System.Runtime.InteropServices;

namespace Viveport.Internal
{
	// Token: 0x0200021F RID: 543
	internal struct LeaderboardEntry_t
	{
		// Token: 0x04000F34 RID: 3892
		internal int m_nGlobalRank;

		// Token: 0x04000F35 RID: 3893
		internal int m_nScore;

		// Token: 0x04000F36 RID: 3894
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string m_pUserName;
	}
}
