using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x0200040A RID: 1034
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct RenderModel_TextureMap_t_Packed
	{
		// Token: 0x0600200B RID: 8203 RVA: 0x0009E659 File Offset: 0x0009C859
		public RenderModel_TextureMap_t_Packed(RenderModel_TextureMap_t unpacked)
		{
			this.unWidth = unpacked.unWidth;
			this.unHeight = unpacked.unHeight;
			this.rubTextureMapData = unpacked.rubTextureMapData;
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x0009E67F File Offset: 0x0009C87F
		public void Unpack(ref RenderModel_TextureMap_t unpacked)
		{
			unpacked.unWidth = this.unWidth;
			unpacked.unHeight = this.unHeight;
			unpacked.rubTextureMapData = this.rubTextureMapData;
		}

		// Token: 0x04001CDC RID: 7388
		public char unWidth;

		// Token: 0x04001CDD RID: 7389
		public char unHeight;

		// Token: 0x04001CDE RID: 7390
		public IntPtr rubTextureMapData;
	}
}
