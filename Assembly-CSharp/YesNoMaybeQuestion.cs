using System;
using System.Collections.Generic;

// Token: 0x02000129 RID: 297
public struct YesNoMaybeQuestion
{
	// Token: 0x040007CD RID: 1997
	public List<string> questions;

	// Token: 0x040007CE RID: 1998
	public YesNoMaybeQuestion.QuestionType questionType;

	// Token: 0x0200051C RID: 1308
	public enum QuestionType
	{
		// Token: 0x04002497 RID: 9367
		location,
		// Token: 0x04002498 RID: 9368
		none
	}
}
