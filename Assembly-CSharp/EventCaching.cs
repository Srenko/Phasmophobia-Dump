using System;

// Token: 0x02000094 RID: 148
public enum EventCaching : byte
{
	// Token: 0x04000408 RID: 1032
	DoNotCache,
	// Token: 0x04000409 RID: 1033
	[Obsolete]
	MergeCache,
	// Token: 0x0400040A RID: 1034
	[Obsolete]
	ReplaceCache,
	// Token: 0x0400040B RID: 1035
	[Obsolete]
	RemoveCache,
	// Token: 0x0400040C RID: 1036
	AddToRoomCache,
	// Token: 0x0400040D RID: 1037
	AddToRoomCacheGlobal,
	// Token: 0x0400040E RID: 1038
	RemoveFromRoomCache,
	// Token: 0x0400040F RID: 1039
	RemoveFromRoomCacheForActorsLeft,
	// Token: 0x04000410 RID: 1040
	SliceIncreaseIndex = 10,
	// Token: 0x04000411 RID: 1041
	SliceSetIndex,
	// Token: 0x04000412 RID: 1042
	SlicePurgeIndex,
	// Token: 0x04000413 RID: 1043
	SlicePurgeUpToIndex
}
