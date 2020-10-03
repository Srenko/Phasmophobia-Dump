using System;

namespace Valve.VR
{
	// Token: 0x020003CB RID: 971
	public enum EVRRenderModelError
	{
		// Token: 0x04001BA5 RID: 7077
		None,
		// Token: 0x04001BA6 RID: 7078
		Loading = 100,
		// Token: 0x04001BA7 RID: 7079
		NotSupported = 200,
		// Token: 0x04001BA8 RID: 7080
		InvalidArg = 300,
		// Token: 0x04001BA9 RID: 7081
		InvalidModel,
		// Token: 0x04001BAA RID: 7082
		NoShapes,
		// Token: 0x04001BAB RID: 7083
		MultipleShapes,
		// Token: 0x04001BAC RID: 7084
		TooManyVertices,
		// Token: 0x04001BAD RID: 7085
		MultipleTextures,
		// Token: 0x04001BAE RID: 7086
		BufferTooSmall,
		// Token: 0x04001BAF RID: 7087
		NotEnoughNormals,
		// Token: 0x04001BB0 RID: 7088
		NotEnoughTexCoords,
		// Token: 0x04001BB1 RID: 7089
		InvalidTexture = 400
	}
}
