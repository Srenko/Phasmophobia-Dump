using System;

namespace MeshCombineStudio
{
	// Token: 0x020004B5 RID: 1205
	public struct Int3
	{
		// Token: 0x06002582 RID: 9602 RVA: 0x000BB3A7 File Offset: 0x000B95A7
		public Int3(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000BB3BE File Offset: 0x000B95BE
		public static Int3 operator +(Int3 a, Int3 b)
		{
			return a + b;
		}

		// Token: 0x040022F8 RID: 8952
		public int x;

		// Token: 0x040022F9 RID: 8953
		public int y;

		// Token: 0x040022FA RID: 8954
		public int z;
	}
}
