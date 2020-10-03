using System;
using RedScarf.EasyCSV;
using UnityEngine;

// Token: 0x02000175 RID: 373
public class LocalisationSystem : MonoBehaviour
{
	// Token: 0x060009E2 RID: 2530 RVA: 0x0003CEF1 File Offset: 0x0003B0F1
	public static void Init()
	{
		LocalisationSystem.csvFile = Resources.Load<TextAsset>("localisation");
		CsvHelper.Init(',');
		LocalisationSystem.table = CsvHelper.Create(LocalisationSystem.csvFile.name, LocalisationSystem.csvFile.text, true, true);
		LocalisationSystem.isInit = true;
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0003CF30 File Offset: 0x0003B130
	public static string GetLocalisedValue(string key)
	{
		if (!LocalisationSystem.isInit)
		{
			LocalisationSystem.Init();
		}
		string result = "";
		switch (LocalisationSystem.language)
		{
		case LocalisationSystem.Language.English:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 1);
			break;
		case LocalisationSystem.Language.BrazilianPortuguese:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 2);
			break;
		case LocalisationSystem.Language.Spanish:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 3);
			break;
		case LocalisationSystem.Language.Portuguese:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 4);
			break;
		case LocalisationSystem.Language.German:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 5);
			break;
		case LocalisationSystem.Language.French:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 6);
			break;
		case LocalisationSystem.Language.Italian:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 7);
			break;
		case LocalisationSystem.Language.Czech:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 8);
			break;
		case LocalisationSystem.Language.Polish:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 9);
			break;
		case LocalisationSystem.Language.Russian:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 10);
			break;
		case LocalisationSystem.Language.Japanese:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 11);
			break;
		case LocalisationSystem.Language.Korean:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 12);
			break;
		case LocalisationSystem.Language.Turkish:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 13);
			break;
		case LocalisationSystem.Language.SimplifiedChinese:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 14);
			break;
		case LocalisationSystem.Language.TraditionalChinese:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 15);
			break;
		case LocalisationSystem.Language.Dutch:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 16);
			break;
		case LocalisationSystem.Language.Greek:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 17);
			break;
		case LocalisationSystem.Language.Norwegian:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 18);
			break;
		case LocalisationSystem.Language.Romanian:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 19);
			break;
		case LocalisationSystem.Language.Swedish:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 20);
			break;
		case LocalisationSystem.Language.Ukrainian:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 21);
			break;
		case LocalisationSystem.Language.Bulgarian:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 22);
			break;
		case LocalisationSystem.Language.Danish:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 23);
			break;
		case LocalisationSystem.Language.Finnish:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 24);
			break;
		case LocalisationSystem.Language.Hungarian:
			result = LocalisationSystem.table.Read(LocalisationSystem.table.FindValue(key).row, 25);
			break;
		}
		return result;
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x0003D2FE File Offset: 0x0003B4FE
	public static void ChangeLanguage(int id, int id2)
	{
		LocalisationSystem.language = (LocalisationSystem.Language)id;
		LocalisationSystem.voiceLanguage = (LocalisationSystem.Language)id2;
	}

	// Token: 0x04000A07 RID: 2567
	public static LocalisationSystem.Language language = (LocalisationSystem.Language)PlayerPrefs.GetInt("languageValue");

	// Token: 0x04000A08 RID: 2568
	public static LocalisationSystem.Language voiceLanguage = (LocalisationSystem.Language)PlayerPrefs.GetInt("voiceLanguageValue");

	// Token: 0x04000A09 RID: 2569
	public static bool isInit;

	// Token: 0x04000A0A RID: 2570
	private static TextAsset csvFile;

	// Token: 0x04000A0B RID: 2571
	private static CsvTable table;

	// Token: 0x02000542 RID: 1346
	public enum Language
	{
		// Token: 0x0400251E RID: 9502
		English,
		// Token: 0x0400251F RID: 9503
		BrazilianPortuguese,
		// Token: 0x04002520 RID: 9504
		Spanish,
		// Token: 0x04002521 RID: 9505
		Portuguese,
		// Token: 0x04002522 RID: 9506
		German,
		// Token: 0x04002523 RID: 9507
		French,
		// Token: 0x04002524 RID: 9508
		Italian,
		// Token: 0x04002525 RID: 9509
		Czech,
		// Token: 0x04002526 RID: 9510
		Polish,
		// Token: 0x04002527 RID: 9511
		Russian,
		// Token: 0x04002528 RID: 9512
		Japanese,
		// Token: 0x04002529 RID: 9513
		Korean,
		// Token: 0x0400252A RID: 9514
		Turkish,
		// Token: 0x0400252B RID: 9515
		SimplifiedChinese,
		// Token: 0x0400252C RID: 9516
		TraditionalChinese,
		// Token: 0x0400252D RID: 9517
		Dutch,
		// Token: 0x0400252E RID: 9518
		Greek,
		// Token: 0x0400252F RID: 9519
		Norwegian,
		// Token: 0x04002530 RID: 9520
		Romanian,
		// Token: 0x04002531 RID: 9521
		Swedish,
		// Token: 0x04002532 RID: 9522
		Ukrainian,
		// Token: 0x04002533 RID: 9523
		Bulgarian,
		// Token: 0x04002534 RID: 9524
		Danish,
		// Token: 0x04002535 RID: 9525
		Finnish,
		// Token: 0x04002536 RID: 9526
		Hungarian
	}
}
