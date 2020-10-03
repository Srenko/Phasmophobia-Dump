using System;
using System.Collections.Generic;
using UnityEngine;

namespace RedScarf.EasyCSV.Demo
{
	// Token: 0x02000473 RID: 1139
	public class CsvTest : MonoBehaviour
	{
		// Token: 0x06002385 RID: 9093 RVA: 0x000ADED4 File Offset: 0x000AC0D4
		private void Start()
		{
			CsvHelper.Init(',');
			this.table = CsvHelper.Create(this.text.name, this.text.text, true, true);
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000ADF00 File Offset: 0x000AC100
		private void OnGUI()
		{
			GUILayout.Space(30f);
			foreach (List<string> list in this.table.RawDataList)
			{
				using (new GUILayout.HorizontalScope(Array.Empty<GUILayoutOption>()))
				{
					foreach (string text in list)
					{
						GUILayout.Label(text, new GUILayoutOption[]
						{
							GUILayout.Width(150f)
						});
					}
				}
			}
			GUILayout.Space(100f);
			using (new GUILayout.HorizontalScope(Array.Empty<GUILayoutOption>()))
			{
				GUILayout.Label("Row:", new GUILayoutOption[]
				{
					GUILayout.Width((float)this.buttonWidth)
				});
				this.rowStr = GUILayout.TextField(this.rowStr, Array.Empty<GUILayoutOption>());
				int.TryParse(this.rowStr, out this.row);
				this.rowStr = this.row.ToString();
				GUILayout.Space(20f);
				GUILayout.Label("Column:", new GUILayoutOption[]
				{
					GUILayout.Width((float)this.buttonWidth)
				});
				this.columnStr = GUILayout.TextField(this.columnStr, Array.Empty<GUILayoutOption>());
				int.TryParse(this.columnStr, out this.column);
				this.columnStr = this.column.ToString();
			}
			using (new GUILayout.HorizontalScope(Array.Empty<GUILayoutOption>()))
			{
				if (GUILayout.Button("Read", new GUILayoutOption[]
				{
					GUILayout.Width((float)this.buttonWidth)
				}))
				{
					this.readValue = this.table.Read(this.row, this.column);
				}
				GUILayout.TextArea(this.readValue, Array.Empty<GUILayoutOption>());
			}
			using (new GUILayout.HorizontalScope(Array.Empty<GUILayoutOption>()))
			{
				if (GUILayout.Button("Write", new GUILayoutOption[]
				{
					GUILayout.Width((float)this.buttonWidth)
				}))
				{
					this.table.Write(this.row, this.column, this.writeValue);
				}
				this.writeValue = GUILayout.TextArea(this.writeValue, Array.Empty<GUILayoutOption>());
			}
			using (new GUILayout.HorizontalScope(Array.Empty<GUILayoutOption>()))
			{
				using (new GUILayout.VerticalScope(new GUILayoutOption[]
				{
					GUILayout.Width((float)this.buttonWidth)
				}))
				{
					if (GUILayout.Button("PaddingData", Array.Empty<GUILayoutOption>()))
					{
						TestRowData testRowData = CsvHelper.PaddingData<TestRowData>(this.text.name, this.rowID);
						this.rowDataStr = testRowData.ToString();
					}
					this.rowID = GUILayout.TextField(this.rowID, Array.Empty<GUILayoutOption>());
				}
				GUILayout.TextArea(this.rowDataStr, Array.Empty<GUILayoutOption>());
			}
		}

		// Token: 0x040020C9 RID: 8393
		public TextAsset text;

		// Token: 0x040020CA RID: 8394
		private CsvTable table;

		// Token: 0x040020CB RID: 8395
		private int row;

		// Token: 0x040020CC RID: 8396
		private int column;

		// Token: 0x040020CD RID: 8397
		private string rowStr = "";

		// Token: 0x040020CE RID: 8398
		private string columnStr = "";

		// Token: 0x040020CF RID: 8399
		private string readValue = "";

		// Token: 0x040020D0 RID: 8400
		private string writeValue = "";

		// Token: 0x040020D1 RID: 8401
		private string rowDataStr = "";

		// Token: 0x040020D2 RID: 8402
		private string rowID = "Jack";

		// Token: 0x040020D3 RID: 8403
		private int buttonWidth = 150;
	}
}
