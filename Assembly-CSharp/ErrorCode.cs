using System;

// Token: 0x0200008B RID: 139
public class ErrorCode
{
	// Token: 0x04000368 RID: 872
	public const int Ok = 0;

	// Token: 0x04000369 RID: 873
	public const int OperationNotAllowedInCurrentState = -3;

	// Token: 0x0400036A RID: 874
	[Obsolete("Use InvalidOperation.")]
	public const int InvalidOperationCode = -2;

	// Token: 0x0400036B RID: 875
	public const int InvalidOperation = -2;

	// Token: 0x0400036C RID: 876
	public const int InternalServerError = -1;

	// Token: 0x0400036D RID: 877
	public const int InvalidAuthentication = 32767;

	// Token: 0x0400036E RID: 878
	public const int GameIdAlreadyExists = 32766;

	// Token: 0x0400036F RID: 879
	public const int GameFull = 32765;

	// Token: 0x04000370 RID: 880
	public const int GameClosed = 32764;

	// Token: 0x04000371 RID: 881
	[Obsolete("No longer used, cause random matchmaking is no longer a process.")]
	public const int AlreadyMatched = 32763;

	// Token: 0x04000372 RID: 882
	public const int ServerFull = 32762;

	// Token: 0x04000373 RID: 883
	public const int UserBlocked = 32761;

	// Token: 0x04000374 RID: 884
	public const int NoRandomMatchFound = 32760;

	// Token: 0x04000375 RID: 885
	public const int GameDoesNotExist = 32758;

	// Token: 0x04000376 RID: 886
	public const int MaxCcuReached = 32757;

	// Token: 0x04000377 RID: 887
	public const int InvalidRegion = 32756;

	// Token: 0x04000378 RID: 888
	public const int CustomAuthenticationFailed = 32755;

	// Token: 0x04000379 RID: 889
	public const int AuthenticationTicketExpired = 32753;

	// Token: 0x0400037A RID: 890
	public const int PluginReportedError = 32752;

	// Token: 0x0400037B RID: 891
	public const int PluginMismatch = 32751;

	// Token: 0x0400037C RID: 892
	public const int JoinFailedPeerAlreadyJoined = 32750;

	// Token: 0x0400037D RID: 893
	public const int JoinFailedFoundInactiveJoiner = 32749;

	// Token: 0x0400037E RID: 894
	public const int JoinFailedWithRejoinerNotFound = 32748;

	// Token: 0x0400037F RID: 895
	public const int JoinFailedFoundExcludedUserId = 32747;

	// Token: 0x04000380 RID: 896
	public const int JoinFailedFoundActiveJoiner = 32746;

	// Token: 0x04000381 RID: 897
	public const int HttpLimitReached = 32745;

	// Token: 0x04000382 RID: 898
	public const int ExternalHttpCallFailed = 32744;

	// Token: 0x04000383 RID: 899
	public const int SlotError = 32742;

	// Token: 0x04000384 RID: 900
	public const int InvalidEncryptionParameters = 32741;
}
