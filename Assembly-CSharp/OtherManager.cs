using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000189 RID: 393
public class OtherManager : MonoBehaviour
{
	// Token: 0x06000A8D RID: 2701 RVA: 0x00041AC0 File Offset: 0x0003FCC0
	private void Awake()
	{
		this.languageValue = PlayerPrefs.GetInt("languageValue");
		this.degreesValue = PlayerPrefs.GetInt("degreesValue");
		this.voiceLanguageValue = PlayerPrefs.GetInt("voiceLanguageValue");
		if (PlayerPrefs.GetInt("voiceLanguageValue") == 0)
		{
			this.voiceLanguageValue = this.languageValue;
		}
		LocalisationSystem.ChangeLanguage(this.languageValue, this.voiceLanguageValue);
		for (int i = 0; i < this.localiserText.Length; i++)
		{
			this.localiserText[i].LoadText();
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x00041B4C File Offset: 0x0003FD4C
	public void SetValues()
	{
		PlayerPrefs.SetInt("languageValue", this.languageValue);
		PlayerPrefs.SetInt("degreesValue", this.degreesValue);
		PlayerPrefs.SetInt("voiceLanguageValue", this.voiceLanguageValue);
		LocalisationSystem.ChangeLanguage(this.languageValue, this.voiceLanguageValue);
		for (int i = 0; i < this.localiserText.Length; i++)
		{
			this.localiserText[i].LoadText();
		}
		this.UpdateUIValues();
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x00041BC0 File Offset: 0x0003FDC0
	private void UpdateUIValues()
	{
		this.languageValueText.text = this.GetLanguageText();
		this.languageValueText2.text = this.GetLanguageText();
		this.degreesValueText.text = this.GetDegreesText();
		this.voiceLanguageValueText.text = this.GetVoiceLanguageText();
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x00041C11 File Offset: 0x0003FE11
	public void ApplyButton()
	{
		this.SetValues();
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x00041C19 File Offset: 0x0003FE19
	public void LanguageChangeValue(int value)
	{
		this.languageValue += value;
		if (this.languageValue < 0)
		{
			this.languageValue = 0;
		}
		else if (this.languageValue > 24)
		{
			this.languageValue = 24;
		}
		this.SetValues();
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x00041C53 File Offset: 0x0003FE53
	public void DegreesChangeValue(int value)
	{
		this.degreesValue += value;
		if (this.degreesValue < 0)
		{
			this.degreesValue = 0;
		}
		else if (this.degreesValue > 1)
		{
			this.degreesValue = 1;
		}
		this.SetValues();
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x00041C8B File Offset: 0x0003FE8B
	public void VoiceLanguageChangeValue(int value)
	{
		this.voiceLanguageValue += value;
		if (this.voiceLanguageValue < 0)
		{
			this.voiceLanguageValue = 0;
		}
		else if (this.voiceLanguageValue > 24)
		{
			this.voiceLanguageValue = 24;
		}
		this.SetValues();
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x00041CC8 File Offset: 0x0003FEC8
	private string GetLanguageText()
	{
		switch (this.languageValue)
		{
		case 0:
			return "English";
		case 1:
			return "Brazilian Portuguese";
		case 2:
			return "Spanish";
		case 3:
			return "Portuguese";
		case 4:
			return "German";
		case 5:
			return "French";
		case 6:
			return "Italian";
		case 7:
			return "Czech";
		case 8:
			return "Polish";
		case 9:
			return "Russian";
		case 10:
			return "Japanese";
		case 11:
			return "Korean";
		case 12:
			return "Turkish";
		case 13:
			return "Simplified Chinese";
		case 14:
			return "Traditional Chinese";
		case 15:
			return "Dutch";
		case 16:
			return "Greek";
		case 17:
			return "Norwegian";
		case 18:
			return "Romanian";
		case 19:
			return "Swedish";
		case 20:
			return "Ukrainian";
		case 21:
			return "Bulgarian";
		case 22:
			return "Danish";
		case 23:
			return "Finnish";
		case 24:
			return "Hungarian";
		default:
			return "";
		}
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x00041DE6 File Offset: 0x0003FFE6
	private string GetDegreesText()
	{
		if (this.degreesValue == 0)
		{
			return LocalisationSystem.GetLocalisedValue("Other_Celsius");
		}
		return LocalisationSystem.GetLocalisedValue("Other_Farenheit");
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x00041E08 File Offset: 0x00040008
	private string GetVoiceLanguageText()
	{
		switch (this.voiceLanguageValue)
		{
		case 0:
			return "English";
		case 1:
			return "Brazilian Portuguese";
		case 2:
			return "Spanish";
		case 3:
			return "Portuguese";
		case 4:
			return "German";
		case 5:
			return "French";
		case 6:
			return "Italian";
		case 7:
			return "Czech";
		case 8:
			return "Polish";
		case 9:
			return "Russian";
		case 10:
			return "Japanese";
		case 11:
			return "Korean";
		case 12:
			return "Turkish";
		case 13:
			return "Simplified Chinese";
		case 14:
			return "Traditional Chinese";
		case 15:
			return "Dutch";
		case 16:
			return "Greek";
		case 17:
			return "Norwegian";
		case 18:
			return "Romanian";
		case 19:
			return "Swedish";
		case 20:
			return "Ukrainian";
		case 21:
			return "Bulgarian";
		case 22:
			return "Danish";
		case 23:
			return "Finnish";
		case 24:
			return "Hungarian";
		default:
			return "";
		}
	}

	// Token: 0x04000AC4 RID: 2756
	private int languageValue;

	// Token: 0x04000AC5 RID: 2757
	private int degreesValue;

	// Token: 0x04000AC6 RID: 2758
	private int voiceLanguageValue;

	// Token: 0x04000AC7 RID: 2759
	[SerializeField]
	private Text languageValueText;

	// Token: 0x04000AC8 RID: 2760
	[SerializeField]
	private Text languageValueText2;

	// Token: 0x04000AC9 RID: 2761
	[SerializeField]
	private Text degreesValueText;

	// Token: 0x04000ACA RID: 2762
	[SerializeField]
	private Text voiceLanguageValueText;

	// Token: 0x04000ACB RID: 2763
	[SerializeField]
	private TextLocaliserUI[] localiserText;
}
