using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x0200040C RID: 1036
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct RenderModel_t_Packed
	{
		// Token: 0x0600200D RID: 8205 RVA: 0x0009E6A5 File Offset: 0x0009C8A5
		public RenderModel_t_Packed(RenderModel_t unpacked)
		{
			this.rVertexData = unpacked.rVertexData;
			this.unVertexCount = unpacked.unVertexCount;
			this.rIndexData = unpacked.rIndexData;
			this.unTriangleCount = unpacked.unTriangleCount;
			this.diffuseTextureId = unpacked.diffuseTextureId;
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x0009E6E3 File Offset: 0x0009C8E3
		public void Unpack(ref RenderModel_t unpacked)
		{
			unpacked.rVertexData = this.rVertexData;
			unpacked.unVertexCount = this.unVertexCount;
			unpacked.rIndexData = this.rIndexData;
			unpacked.unTriangleCount = this.unTriangleCount;
			unpacked.diffuseTextureId = this.diffuseTextureId;
		}

		// Token: 0x04001CE4 RID: 7396
		public IntPtr rVertexData;

		// Token: 0x04001CE5 RID: 7397
		public uint unVertexCount;

		// Token: 0x04001CE6 RID: 7398
		public IntPtr rIndexData;

		// Token: 0x04001CE7 RID: 7399
		public uint unTriangleCount;

		// Token: 0x04001CE8 RID: 7400
		public int diffuseTextureId;
	}
}
