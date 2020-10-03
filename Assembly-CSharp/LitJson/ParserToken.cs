using System;

namespace LitJson
{
	// Token: 0x02000244 RID: 580
	internal enum ParserToken
	{
		// Token: 0x04000FB1 RID: 4017
		None = 65536,
		// Token: 0x04000FB2 RID: 4018
		Number,
		// Token: 0x04000FB3 RID: 4019
		True,
		// Token: 0x04000FB4 RID: 4020
		False,
		// Token: 0x04000FB5 RID: 4021
		Null,
		// Token: 0x04000FB6 RID: 4022
		CharSeq,
		// Token: 0x04000FB7 RID: 4023
		Char,
		// Token: 0x04000FB8 RID: 4024
		Text,
		// Token: 0x04000FB9 RID: 4025
		Object,
		// Token: 0x04000FBA RID: 4026
		ObjectPrime,
		// Token: 0x04000FBB RID: 4027
		Pair,
		// Token: 0x04000FBC RID: 4028
		PairRest,
		// Token: 0x04000FBD RID: 4029
		Array,
		// Token: 0x04000FBE RID: 4030
		ArrayPrime,
		// Token: 0x04000FBF RID: 4031
		Value,
		// Token: 0x04000FC0 RID: 4032
		ValueRest,
		// Token: 0x04000FC1 RID: 4033
		String,
		// Token: 0x04000FC2 RID: 4034
		End,
		// Token: 0x04000FC3 RID: 4035
		Epsilon
	}
}
