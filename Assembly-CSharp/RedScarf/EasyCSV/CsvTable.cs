using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RedScarf.EasyCSV
{
	// Token: 0x02000471 RID: 1137
	public sealed class CsvTable
	{
		// Token: 0x06002366 RID: 9062 RVA: 0x000AD4F0 File Offset: 0x000AB6F0
		internal static void Init(char separator)
		{
			CsvTable.s_Separator = separator;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000AD4F8 File Offset: 0x000AB6F8
		public CsvTable(string name, string data, bool resolveColumnName, bool firstColumnIsID)
		{
			this.m_Name = name;
			this.m_FirstColumnIsID = firstColumnIsID;
			this.m_ResolveColumnName = resolveColumnName;
			this.m_RawDataList = new List<List<string>>(999);
			this.stringBuilder = new StringBuilder(49950);
			this.columnNameDict = new Dictionary<string, int>(50);
			this.rowIdDict = new Dictionary<string, int>(999);
			this.Append(data);
			this.ResolveColumnName();
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000AD56C File Offset: 0x000AB76C
		private void ResolveRowID(int row)
		{
			if (row < 0 || row >= this.m_RawDataList.Count)
			{
				return;
			}
			string text = this.Read(row, 0);
			if (!string.IsNullOrEmpty(text))
			{
				this.rowIdDict.Remove(text);
				this.rowIdDict.Add(text, row);
			}
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000AD5B8 File Offset: 0x000AB7B8
		private void ResolveColumnName()
		{
			if (!this.m_ResolveColumnName)
			{
				return;
			}
			if (this.m_RawDataList.Count > 0)
			{
				List<string> list = this.m_RawDataList[0];
				for (int i = 0; i < list.Count; i++)
				{
					string text = list[i];
					if (!string.IsNullOrEmpty(text))
					{
						string.Intern(text);
						if (this.columnNameDict.ContainsKey(text))
						{
							this.columnNameDict.Remove(text);
						}
						this.columnNameDict.Add(text, i);
					}
				}
			}
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000AD63F File Offset: 0x000AB83F
		public int GetRowByID(string id)
		{
			if (this.m_FirstColumnIsID && this.rowIdDict.ContainsKey(id))
			{
				return this.rowIdDict[id];
			}
			return -1;
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000AD665 File Offset: 0x000AB865
		private int GetColumnByColumnName(string columnName)
		{
			if (!string.IsNullOrEmpty(columnName) && this.columnNameDict.ContainsKey(columnName))
			{
				return this.columnNameDict[columnName];
			}
			return -1;
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000AD68C File Offset: 0x000AB88C
		public string Read(int row, int column)
		{
			if (row < 0 || row >= this.m_RawDataList.Count || column < 0)
			{
				return string.Empty;
			}
			List<string> list = this.m_RawDataList[row];
			if (column >= list.Count)
			{
				return string.Empty;
			}
			return list[column];
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000AD6D8 File Offset: 0x000AB8D8
		public string Read(int row, string columnName)
		{
			if (!string.IsNullOrEmpty(columnName) && row >= 0 && row < this.m_RawDataList.Count)
			{
				int columnByColumnName = this.GetColumnByColumnName(columnName);
				if (columnByColumnName >= 0 && this.m_RawDataList[row].Count > columnByColumnName)
				{
					return this.m_RawDataList[row][columnByColumnName];
				}
			}
			return string.Empty;
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000AD737 File Offset: 0x000AB937
		public string Read(string id, string columnName)
		{
			return this.Read(this.GetRowByID(id), columnName);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000AD747 File Offset: 0x000AB947
		public string Read(string id, int column)
		{
			return this.Read(this.GetRowByID(id), column);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000AD758 File Offset: 0x000AB958
		public void Write(int row, int column, string value)
		{
			if (row < 0 || column < 0)
			{
				return;
			}
			if (!string.IsNullOrEmpty(value))
			{
				int num = row - this.m_RawDataList.Count + 1;
				if (num > 0)
				{
					for (int i = 0; i < num; i++)
					{
						List<string> item = new List<string>(50);
						this.m_RawDataList.Add(item);
					}
				}
				List<string> list = this.m_RawDataList[row];
				int num2 = column - list.Count + 1;
				if (num2 > 0)
				{
					for (int j = 0; j < num2; j++)
					{
						list.Add(string.Empty);
					}
				}
				list[column] = value;
			}
			else if (row < this.m_RawDataList.Count && column < this.m_RawDataList[row].Count)
			{
				this.m_RawDataList[row][column] = value;
			}
			this.ResolveRowID(row);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000AD82C File Offset: 0x000ABA2C
		public void Write(int row, string columnName, string value)
		{
			int columnByColumnName = this.GetColumnByColumnName(columnName);
			if (columnByColumnName >= 0)
			{
				this.Write(row, columnByColumnName, value);
			}
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000AD84E File Offset: 0x000ABA4E
		public void Write(string id, string columnName, string value)
		{
			this.Write(this.GetRowByID(id), columnName, value);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000AD85F File Offset: 0x000ABA5F
		public void Write(string id, int column, string value)
		{
			this.Write(this.GetRowByID(id), column, value);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000AD870 File Offset: 0x000ABA70
		public void Append(string data)
		{
			this.InsertData(this.m_RawDataList.Count, data);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000AD884 File Offset: 0x000ABA84
		public void InsertData(int row, string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				return;
			}
			row = Mathf.Clamp(row, 0, this.m_RawDataList.Count);
			List<List<string>> dataList = this.GetDataList(data);
			if (dataList != null)
			{
				this.m_RawDataList.InsertRange(row, dataList);
				int num = row + dataList.Count;
				for (int i = row; i < num; i++)
				{
					this.ResolveRowID(i);
				}
			}
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000AD8E4 File Offset: 0x000ABAE4
		private List<List<string>> GetDataList(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				return null;
			}
			this.stringBuilder.Length = 0;
			if (this.m_LineBreak == CsvTable.LineBreak.None)
			{
				if (data.IndexOf("\r\n") >= 0)
				{
					this.m_LineBreak = CsvTable.LineBreak.CRLF;
				}
				else if (data.IndexOf('\r') >= 0)
				{
					this.m_LineBreak = CsvTable.LineBreak.CR;
				}
				else if (data.IndexOf('\n') >= 0)
				{
					this.m_LineBreak = CsvTable.LineBreak.LF;
				}
			}
			List<List<string>> list = new List<List<string>>();
			int length = data.Length;
			int num = 0;
			List<string> list2 = new List<string>();
			int i = 0;
			while (i < length)
			{
				char c = data[i];
				if (c != '"')
				{
					goto IL_9C;
				}
				num++;
				if (num != 1 && num % 2 != 0)
				{
					goto IL_9C;
				}
				IL_140:
				i++;
				continue;
				IL_9C:
				if (num % 2 == 0)
				{
					bool flag = false;
					if (c == CsvTable.s_Separator)
					{
						list2.Add(this.stringBuilder.ToString());
						this.stringBuilder.Length = 0;
						num = 0;
						goto IL_140;
					}
					if (c == '\r')
					{
						flag = true;
						if (i + 1 < length && data[i + 1] == '\n')
						{
							i++;
						}
					}
					else if (c == '\n')
					{
						flag = true;
					}
					if (flag)
					{
						list2.Add(this.stringBuilder.ToString());
						list.Add(list2);
						list2 = new List<string>();
						this.stringBuilder.Length = 0;
						num = 0;
						goto IL_140;
					}
				}
				this.stringBuilder.Append(c);
				goto IL_140;
			}
			string text = this.stringBuilder.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				list2.Add(text);
				list.Add(list2);
			}
			this.stringBuilder.Length = 0;
			return list;
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000ADA74 File Offset: 0x000ABC74
		public void RemoveValue(int row, int column)
		{
			if (row < 0 || column < 0)
			{
				return;
			}
			if (row >= this.m_RawDataList.Count)
			{
				return;
			}
			List<string> list = this.m_RawDataList[row];
			if (column >= list.Count)
			{
				return;
			}
			list.RemoveAt(column);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000ADAB8 File Offset: 0x000ABCB8
		public void RemoveValue(int row, string columnName)
		{
			int columnByColumnName = this.GetColumnByColumnName(columnName);
			this.RemoveValue(row, columnByColumnName);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000ADAD5 File Offset: 0x000ABCD5
		public void RemoveRow(int row)
		{
			if (row < 0 || row >= this.m_RawDataList.Count)
			{
				return;
			}
			this.m_RawDataList.RemoveAt(row);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000ADAF8 File Offset: 0x000ABCF8
		public void RemoveColumn(int column)
		{
			if (column < 0)
			{
				return;
			}
			foreach (List<string> list in this.m_RawDataList)
			{
				if (column < list.Count)
				{
					list.RemoveAt(column);
				}
			}
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000ADB5C File Offset: 0x000ABD5C
		public void RemoveColumn(string columnName)
		{
			int columnByColumnName = this.GetColumnByColumnName(columnName);
			if (columnByColumnName >= 0)
			{
				this.RemoveColumn(columnByColumnName);
				this.columnNameDict.Remove(columnName);
			}
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x000ADB8C File Offset: 0x000ABD8C
		public CsvTableCoords FindValue(string value, CsvTableCoords start)
		{
			CsvTableCoords result = new CsvTableCoords(-1, -1);
			int num = Mathf.Clamp(start.row, 0, this.m_RawDataList.Count);
			List<string> list = this.m_RawDataList[num];
			for (int i = start.column; i < list.Count; i++)
			{
				if (list[i] == value)
				{
					result.Set(num, i);
					return result;
				}
			}
			num++;
			for (int j = num; j < this.m_RawDataList.Count; j++)
			{
				List<string> list2 = this.m_RawDataList[j];
				for (int k = 0; k < list2.Count; k++)
				{
					if (list2[k] == value)
					{
						result.Set(j, k);
						return result;
					}
				}
			}
			return result;
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000ADC59 File Offset: 0x000ABE59
		public CsvTableCoords FindValue(string value)
		{
			return this.FindValue(value, new CsvTableCoords(0, 0));
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x000ADC69 File Offset: 0x000ABE69
		public int RowCount
		{
			get
			{
				return this.m_RawDataList.Count;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x000ADC76 File Offset: 0x000ABE76
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x000ADC7E File Offset: 0x000ABE7E
		public List<List<string>> RawDataList
		{
			get
			{
				return this.m_RawDataList;
			}
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000ADC88 File Offset: 0x000ABE88
		public string GetData(CsvTable.LineBreak lineBreak = CsvTable.LineBreak.None, char separator = ',')
		{
			this.stringBuilder.Length = 0;
			if (lineBreak == CsvTable.LineBreak.None)
			{
				lineBreak = this.m_LineBreak;
			}
			if (lineBreak == CsvTable.LineBreak.None)
			{
				lineBreak = CsvTable.LineBreak.CRLF;
			}
			int num = 0;
			foreach (List<string> list in this.m_RawDataList)
			{
				num = Mathf.Max(num, list.Count);
			}
			foreach (List<string> list2 in this.m_RawDataList)
			{
				int count = list2.Count;
				for (int i = 0; i < num; i++)
				{
					bool flag = false;
					int length = this.stringBuilder.Length;
					if (i < count)
					{
						string text = list2[i];
						int length2 = text.Length;
						for (int j = 0; j < length2; j++)
						{
							char c = text[j];
							if (c == separator)
							{
								flag = true;
							}
							else if (c == '\n' || c == '\r')
							{
								flag = true;
							}
							else if (c == '"')
							{
								this.stringBuilder.Append('"');
								flag = true;
							}
							this.stringBuilder.Append(c);
						}
					}
					if (flag)
					{
						this.stringBuilder.Insert(length, '"');
						this.stringBuilder.Append('"');
					}
					this.stringBuilder.Append(separator);
				}
				this.stringBuilder.Remove(this.stringBuilder.Length - 1, 1);
				this.stringBuilder.Append(CsvTable.lineBreakDict[lineBreak]);
			}
			return this.stringBuilder.ToString();
		}

		// Token: 0x040020B6 RID: 8374
		public const char DEFAULT_SEPARATOR = ',';

		// Token: 0x040020B7 RID: 8375
		private const char ESCAPE_CHAR = '"';

		// Token: 0x040020B8 RID: 8376
		private const char CR = '\r';

		// Token: 0x040020B9 RID: 8377
		private const char LF = '\n';

		// Token: 0x040020BA RID: 8378
		private const string CRLF = "\r\n";

		// Token: 0x040020BB RID: 8379
		private const int DEFAULT_COLUMN_COUNT = 50;

		// Token: 0x040020BC RID: 8380
		private const int DEFAULT_ROW_COUNT = 999;

		// Token: 0x040020BD RID: 8381
		private static char s_Separator = ',';

		// Token: 0x040020BE RID: 8382
		private static readonly Dictionary<CsvTable.LineBreak, string> lineBreakDict = new Dictionary<CsvTable.LineBreak, string>
		{
			{
				CsvTable.LineBreak.CRLF,
				"\r\n"
			},
			{
				CsvTable.LineBreak.LF,
				'\n'.ToString()
			},
			{
				CsvTable.LineBreak.CR,
				'\r'.ToString()
			}
		};

		// Token: 0x040020BF RID: 8383
		private CsvTable.LineBreak m_LineBreak;

		// Token: 0x040020C0 RID: 8384
		private string m_Name;

		// Token: 0x040020C1 RID: 8385
		private List<List<string>> m_RawDataList;

		// Token: 0x040020C2 RID: 8386
		private StringBuilder stringBuilder;

		// Token: 0x040020C3 RID: 8387
		private Dictionary<string, int> columnNameDict;

		// Token: 0x040020C4 RID: 8388
		private Dictionary<string, int> rowIdDict;

		// Token: 0x040020C5 RID: 8389
		private bool m_FirstColumnIsID;

		// Token: 0x040020C6 RID: 8390
		private bool m_ResolveColumnName;

		// Token: 0x02000790 RID: 1936
		public enum LineBreak
		{
			// Token: 0x04002988 RID: 10632
			None,
			// Token: 0x04002989 RID: 10633
			CRLF,
			// Token: 0x0400298A RID: 10634
			LF,
			// Token: 0x0400298B RID: 10635
			CR
		}
	}
}
