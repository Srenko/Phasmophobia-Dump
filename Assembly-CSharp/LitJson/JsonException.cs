using System;

namespace LitJson
{
	// Token: 0x02000233 RID: 563
	public class JsonException : ApplicationException
	{
		// Token: 0x06001036 RID: 4150 RVA: 0x0005FCE9 File Offset: 0x0005DEE9
		public JsonException()
		{
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0005FCF1 File Offset: 0x0005DEF1
		internal JsonException(ParserToken token) : base(string.Format("Invalid token '{0}' in input string", token))
		{
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0005FD09 File Offset: 0x0005DF09
		internal JsonException(ParserToken token, Exception inner_exception) : base(string.Format("Invalid token '{0}' in input string", token), inner_exception)
		{
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0005FD22 File Offset: 0x0005DF22
		internal JsonException(int c) : base(string.Format("Invalid character '{0}' in input string", (char)c))
		{
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0005FD3B File Offset: 0x0005DF3B
		internal JsonException(int c, Exception inner_exception) : base(string.Format("Invalid character '{0}' in input string", (char)c), inner_exception)
		{
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0005FD55 File Offset: 0x0005DF55
		public JsonException(string message) : base(message)
		{
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0005FD5E File Offset: 0x0005DF5E
		public JsonException(string message, Exception inner_exception) : base(message, inner_exception)
		{
		}
	}
}
