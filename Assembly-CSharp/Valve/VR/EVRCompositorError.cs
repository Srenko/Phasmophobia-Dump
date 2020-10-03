using System;

namespace Valve.VR
{
	// Token: 0x020003C2 RID: 962
	public enum EVRCompositorError
	{
		// Token: 0x04001B67 RID: 7015
		None,
		// Token: 0x04001B68 RID: 7016
		RequestFailed,
		// Token: 0x04001B69 RID: 7017
		IncompatibleVersion = 100,
		// Token: 0x04001B6A RID: 7018
		DoNotHaveFocus,
		// Token: 0x04001B6B RID: 7019
		InvalidTexture,
		// Token: 0x04001B6C RID: 7020
		IsNotSceneApplication,
		// Token: 0x04001B6D RID: 7021
		TextureIsOnWrongDevice,
		// Token: 0x04001B6E RID: 7022
		TextureUsesUnsupportedFormat,
		// Token: 0x04001B6F RID: 7023
		SharedTexturesNotSupported,
		// Token: 0x04001B70 RID: 7024
		IndexOutOfRange,
		// Token: 0x04001B71 RID: 7025
		AlreadySubmitted,
		// Token: 0x04001B72 RID: 7026
		InvalidBounds
	}
}
