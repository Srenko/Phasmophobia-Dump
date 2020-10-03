using System;

namespace Photon.Chat
{
	// Token: 0x02000464 RID: 1124
	public class ErrorCode
	{
		// Token: 0x0400205D RID: 8285
		public const int Ok = 0;

		// Token: 0x0400205E RID: 8286
		public const int OperationNotAllowedInCurrentState = -3;

		// Token: 0x0400205F RID: 8287
		public const int InvalidOperationCode = -2;

		// Token: 0x04002060 RID: 8288
		public const int InternalServerError = -1;

		// Token: 0x04002061 RID: 8289
		public const int InvalidAuthentication = 32767;

		// Token: 0x04002062 RID: 8290
		public const int GameIdAlreadyExists = 32766;

		// Token: 0x04002063 RID: 8291
		public const int GameFull = 32765;

		// Token: 0x04002064 RID: 8292
		public const int GameClosed = 32764;

		// Token: 0x04002065 RID: 8293
		public const int ServerFull = 32762;

		// Token: 0x04002066 RID: 8294
		public const int UserBlocked = 32761;

		// Token: 0x04002067 RID: 8295
		public const int NoRandomMatchFound = 32760;

		// Token: 0x04002068 RID: 8296
		public const int GameDoesNotExist = 32758;

		// Token: 0x04002069 RID: 8297
		public const int MaxCcuReached = 32757;

		// Token: 0x0400206A RID: 8298
		public const int InvalidRegion = 32756;

		// Token: 0x0400206B RID: 8299
		public const int CustomAuthenticationFailed = 32755;
	}
}
