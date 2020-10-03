using System;
using RedScarf.EasyCSV;
using UnityEngine;

// Token: 0x0200011D RID: 285
public class LocalisationController : MonoBehaviour
{
	// Token: 0x060007A6 RID: 1958 RVA: 0x0002D69A File Offset: 0x0002B89A
	private void Awake()
	{
		this.text = Resources.Load<TextAsset>("localisation");
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x0002D6AC File Offset: 0x0002B8AC
	private void Start()
	{
		CsvHelper.Init(',');
		this.table = CsvHelper.Create(this.text.name, this.text.text, true, true);
	}

	// Token: 0x04000778 RID: 1912
	private TextAsset text;

	// Token: 0x04000779 RID: 1913
	private CsvTable table;
}
