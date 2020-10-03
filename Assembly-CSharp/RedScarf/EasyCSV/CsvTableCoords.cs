using System;

namespace RedScarf.EasyCSV
{
	// Token: 0x02000472 RID: 1138
	public struct CsvTableCoords
	{
		// Token: 0x06002383 RID: 9091 RVA: 0x000ADEC4 File Offset: 0x000AC0C4
		public CsvTableCoords(int row, int column)
		{
			this.row = row;
			this.column = column;
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000ADEC4 File Offset: 0x000AC0C4
		public void Set(int row, int column)
		{
			this.row = row;
			this.column = column;
		}

		// Token: 0x040020C7 RID: 8391
		public int row;

		// Token: 0x040020C8 RID: 8392
		public int column;
	}
}
