using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RedScarf.EasyCSV
{
	// Token: 0x02000470 RID: 1136
	public static class CsvHelper
	{
		// Token: 0x06002360 RID: 9056 RVA: 0x000AD39A File Offset: 0x000AB59A
		public static void Init(char separator = ',')
		{
			CsvTable.Init(separator);
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000AD3A4 File Offset: 0x000AB5A4
		public static CsvTable Create(string csvName, string data = "", bool resolveColumnName = true, bool firstColumnIsID = true)
		{
			CsvTable csvTable = new CsvTable(csvName, data, resolveColumnName, firstColumnIsID);
			CsvHelper.tableDict.Remove(csvName);
			CsvHelper.tableDict.Add(csvName, csvTable);
			return csvTable;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000AD3D4 File Offset: 0x000AB5D4
		public static CsvTable Get(string csvName)
		{
			if (CsvHelper.tableDict.ContainsKey(csvName))
			{
				return CsvHelper.tableDict[csvName];
			}
			return null;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000AD3F0 File Offset: 0x000AB5F0
		public static T PaddingData<T>(string csvName, string id) where T : new()
		{
			T result = Activator.CreateInstance<T>();
			CsvTable csvTable = CsvHelper.Get(csvName);
			if (csvTable == null)
			{
				return result;
			}
			int rowByID = csvTable.GetRowByID(id);
			return CsvHelper.PaddingData<T>(csvName, rowByID);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000AD420 File Offset: 0x000AB620
		public static T PaddingData<T>(string csvName, int row) where T : new()
		{
			T t = Activator.CreateInstance<T>();
			object obj = t;
			CsvTable csvTable = CsvHelper.Get(csvName);
			if (csvTable == null)
			{
				return t;
			}
			if (row < 0 || row > csvTable.RowCount)
			{
				return t;
			}
			foreach (FieldInfo fieldInfo in typeof(T).GetFields())
			{
				string value = csvTable.Read(row, fieldInfo.Name);
				if (!string.IsNullOrEmpty(value))
				{
					try
					{
						object value2 = Convert.ChangeType(value, fieldInfo.FieldType);
						fieldInfo.SetValue(obj, value2);
					}
					catch (Exception ex)
					{
						Debug.LogErrorFormat("Csv padding data error! {0}", new object[]
						{
							ex
						});
					}
				}
			}
			return (T)((object)obj);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000AD4E4 File Offset: 0x000AB6E4
		public static void Clear()
		{
			CsvHelper.tableDict.Clear();
		}

		// Token: 0x040020B5 RID: 8373
		private static Dictionary<string, CsvTable> tableDict = new Dictionary<string, CsvTable>();
	}
}
