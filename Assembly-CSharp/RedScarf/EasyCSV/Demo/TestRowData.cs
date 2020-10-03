using System;
using System.Reflection;

namespace RedScarf.EasyCSV.Demo
{
	// Token: 0x02000474 RID: 1140
	public class TestRowData
	{
		// Token: 0x06002388 RID: 9096 RVA: 0x000AE2B0 File Offset: 0x000AC4B0
		public override string ToString()
		{
			FieldInfo[] fields = base.GetType().GetFields();
			string text = "";
			foreach (FieldInfo fieldInfo in fields)
			{
				text = string.Concat(new object[]
				{
					text,
					"    ",
					fieldInfo.Name,
					":",
					fieldInfo.GetValue(this),
					"\r\n"
				});
			}
			return "【TestRowData】\r\n{\r\n" + text + "}";
		}

		// Token: 0x040020D4 RID: 8404
		public string id;

		// Token: 0x040020D5 RID: 8405
		public string columnName1;

		// Token: 0x040020D6 RID: 8406
		public string columnName2;

		// Token: 0x040020D7 RID: 8407
		public string columnName3;

		// Token: 0x040020D8 RID: 8408
		public string columnName4;

		// Token: 0x040020D9 RID: 8409
		public int columnName5;
	}
}
