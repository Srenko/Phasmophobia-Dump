using System;

namespace Photon.Chat
{
	// Token: 0x0200045C RID: 1116
	public enum ChatDisconnectCause
	{
		// Token: 0x04002013 RID: 8211
		None,
		// Token: 0x04002014 RID: 8212
		DisconnectByServerUserLimit,
		// Token: 0x04002015 RID: 8213
		ExceptionOnConnect,
		// Token: 0x04002016 RID: 8214
		DisconnectByServer,
		// Token: 0x04002017 RID: 8215
		TimeoutDisconnect,
		// Token: 0x04002018 RID: 8216
		Exception,
		// Token: 0x04002019 RID: 8217
		InvalidAuthentication,
		// Token: 0x0400201A RID: 8218
		MaxCcuReached,
		// Token: 0x0400201B RID: 8219
		InvalidRegion,
		// Token: 0x0400201C RID: 8220
		OperationNotAllowedInCurrentState,
		// Token: 0x0400201D RID: 8221
		CustomAuthenticationFailed
	}
}
