using System;

namespace LitJson
{
	// Token: 0x0200023D RID: 573
	public enum JsonToken
	{
		// Token: 0x04000F6E RID: 3950
		None,
		// Token: 0x04000F6F RID: 3951
		ObjectStart,
		// Token: 0x04000F70 RID: 3952
		PropertyName,
		// Token: 0x04000F71 RID: 3953
		ObjectEnd,
		// Token: 0x04000F72 RID: 3954
		ArrayStart,
		// Token: 0x04000F73 RID: 3955
		ArrayEnd,
		// Token: 0x04000F74 RID: 3956
		Int,
		// Token: 0x04000F75 RID: 3957
		Long,
		// Token: 0x04000F76 RID: 3958
		Double,
		// Token: 0x04000F77 RID: 3959
		String,
		// Token: 0x04000F78 RID: 3960
		Boolean,
		// Token: 0x04000F79 RID: 3961
		Null
	}
}
