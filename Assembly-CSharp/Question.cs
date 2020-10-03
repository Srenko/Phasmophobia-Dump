using System;
using System.Collections.Generic;

// Token: 0x02000128 RID: 296
public struct Question
{
	// Token: 0x040007CA RID: 1994
	public List<string> questions;

	// Token: 0x040007CB RID: 1995
	public Question.QuestionType questionType;

	// Token: 0x040007CC RID: 1996
	public Question.AnswerType answerType;

	// Token: 0x0200051A RID: 1306
	public enum QuestionType
	{
		// Token: 0x0400248B RID: 9355
		difficulty,
		// Token: 0x0400248C RID: 9356
		location,
		// Token: 0x0400248D RID: 9357
		age,
		// Token: 0x0400248E RID: 9358
		gender,
		// Token: 0x0400248F RID: 9359
		none
	}

	// Token: 0x0200051B RID: 1307
	public enum AnswerType
	{
		// Token: 0x04002491 RID: 9361
		victim,
		// Token: 0x04002492 RID: 9362
		dead,
		// Token: 0x04002493 RID: 9363
		roomAmount,
		// Token: 0x04002494 RID: 9364
		location,
		// Token: 0x04002495 RID: 9365
		age
	}
}
