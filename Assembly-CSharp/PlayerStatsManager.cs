using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018D RID: 397
public class PlayerStatsManager : MonoBehaviour
{
	// Token: 0x06000AB6 RID: 2742 RVA: 0x00042941 File Offset: 0x00040B41
	private void OnEnable()
	{
		this.UpdateLevel();
		this.UpdateExperience();
		this.UpdateMoney();
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x00042955 File Offset: 0x00040B55
	public void UpdateLevel()
	{
		this.levelText.text = LocalisationSystem.GetLocalisedValue("Experience_Level") + ": " + Mathf.FloorToInt((float)(FileBasedPrefs.GetInt("myTotalExp", 0) / 100));
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x00042990 File Offset: 0x00040B90
	public void UpdateExperience()
	{
		int num = 100 - (100 - ((Mathf.FloorToInt((float)(FileBasedPrefs.GetInt("myTotalExp", 0) / 100)) + 1) * 100 - FileBasedPrefs.GetInt("myTotalExp", 0)));
		this.experienceText.text = string.Concat(new object[]
		{
			LocalisationSystem.GetLocalisedValue("Menu_Experience"),
			": ",
			num,
			"XP"
		});
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x00042A08 File Offset: 0x00040C08
	public void UpdateMoney()
	{
		this.moneyText.text = LocalisationSystem.GetLocalisedValue("Menu_Money") + ": $" + FileBasedPrefs.GetInt("PlayersMoney", 0).ToString();
	}

	// Token: 0x04000AEA RID: 2794
	[SerializeField]
	private Text levelText;

	// Token: 0x04000AEB RID: 2795
	[SerializeField]
	private Text experienceText;

	// Token: 0x04000AEC RID: 2796
	[SerializeField]
	private Text moneyText;
}
