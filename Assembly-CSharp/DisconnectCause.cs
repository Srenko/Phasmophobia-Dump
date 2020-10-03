using System;

// Token: 0x020000A1 RID: 161
public enum DisconnectCause
{
	// Token: 0x04000468 RID: 1128
	DisconnectByServerUserLimit = 1042,
	// Token: 0x04000469 RID: 1129
	ExceptionOnConnect = 1023,
	// Token: 0x0400046A RID: 1130
	DisconnectByServerTimeout = 1041,
	// Token: 0x0400046B RID: 1131
	DisconnectByServerLogic = 1043,
	// Token: 0x0400046C RID: 1132
	Exception = 1026,
	// Token: 0x0400046D RID: 1133
	InvalidAuthentication = 32767,
	// Token: 0x0400046E RID: 1134
	MaxCcuReached = 32757,
	// Token: 0x0400046F RID: 1135
	InvalidRegion = 32756,
	// Token: 0x04000470 RID: 1136
	SecurityExceptionOnConnect = 1022,
	// Token: 0x04000471 RID: 1137
	DisconnectByClientTimeout = 1040,
	// Token: 0x04000472 RID: 1138
	InternalReceiveException = 1039,
	// Token: 0x04000473 RID: 1139
	AuthenticationTicketExpired = 32753
}
