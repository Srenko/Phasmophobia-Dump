using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x020003FD RID: 1021
	public struct Compositor_OverlaySettings
	{
		// Token: 0x04001C85 RID: 7301
		public uint size;

		// Token: 0x04001C86 RID: 7302
		[MarshalAs(UnmanagedType.I1)]
		public bool curved;

		// Token: 0x04001C87 RID: 7303
		[MarshalAs(UnmanagedType.I1)]
		public bool antialias;

		// Token: 0x04001C88 RID: 7304
		public float scale;

		// Token: 0x04001C89 RID: 7305
		public float distance;

		// Token: 0x04001C8A RID: 7306
		public float alpha;

		// Token: 0x04001C8B RID: 7307
		public float uOffset;

		// Token: 0x04001C8C RID: 7308
		public float vOffset;

		// Token: 0x04001C8D RID: 7309
		public float uScale;

		// Token: 0x04001C8E RID: 7310
		public float vScale;

		// Token: 0x04001C8F RID: 7311
		public float gridDivs;

		// Token: 0x04001C90 RID: 7312
		public float gridWidth;

		// Token: 0x04001C91 RID: 7313
		public float gridScale;

		// Token: 0x04001C92 RID: 7314
		public HmdMatrix44_t transform;
	}
}
