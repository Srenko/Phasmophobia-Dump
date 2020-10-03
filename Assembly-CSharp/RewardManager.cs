using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018E RID: 398
public class RewardManager : MonoBehaviour
{
	// Token: 0x06000ABB RID: 2747 RVA: 0x00042A47 File Offset: 0x00040C47
	private void Start()
	{
		this.levelDifficulty = (Contract.LevelDifficulty)FileBasedPrefs.GetInt("LevelDifficulty", 0);
		this.SetupExperienceReward();
		this.SetupMoneyReward();
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x00042A68 File Offset: 0x00040C68
	private void SetupMoneyReward()
	{
		int num;
		if (PlayerPrefs.GetInt("MissionType") == 0)
		{
			num = 10;
		}
		else if (PlayerPrefs.GetInt("MissionType") == 1)
		{
			num = 30;
		}
		else
		{
			num = 45;
		}
		int num2 = 0;
		if (FileBasedPrefs.GetInt("PlayerDied", 0) == 0)
		{
			this.mainMissionRewardText.text = ((PlayerPrefs.GetInt("MainMission") == 1) ? ("$" + num) : "$0");
			this.sideMission1RewardText.text = ((PlayerPrefs.GetInt("SideMission1") == 1) ? "$10" : "$0");
			this.sideMission2RewardText.text = ((PlayerPrefs.GetInt("SideMission2") == 1) ? "$10" : "$0");
			this.hiddenMissionRewardText.text = ((PlayerPrefs.GetInt("SideMission3") == 1) ? "$10" : "$0");
			this.totalPhotosRewardText.text = "$" + this.GetPhotosRewardAmount().ToString();
			this.totalDNARewardText.text = ((PlayerPrefs.GetInt("DNAMission") == 1) ? "$10" : "$0");
			num2 += ((PlayerPrefs.GetInt("MainMission") == 1) ? num : 0);
			num2 += ((PlayerPrefs.GetInt("SideMission1") == 1) ? 10 : 0);
			num2 += ((PlayerPrefs.GetInt("SideMission2") == 1) ? 10 : 0);
			num2 += ((PlayerPrefs.GetInt("SideMission3") == 1) ? 10 : 0);
			num2 += this.GetPhotosRewardAmount();
			num2 += ((PlayerPrefs.GetInt("DNAMission") == 1) ? 10 : 0);
			if (this.levelDifficulty == Contract.LevelDifficulty.Intermediate)
			{
				num2 *= 2;
			}
			else if (this.levelDifficulty == Contract.LevelDifficulty.Professional)
			{
				num2 *= 3;
			}
			if (PlayerPrefs.GetInt("playerCount") == 1)
			{
				if (PlayerPrefs.GetInt("MainMission") == 1)
				{
					this.insuranceAmountText.text = "$10";
					num2 += 10;
				}
			}
			else
			{
				this.insuranceAmountText.text = "$0";
			}
		}
		else
		{
			this.mainMissionRewardText.text = ((PlayerPrefs.GetInt("MainMission") == 1) ? ("$" + num / 2) : "$0");
			this.sideMission1RewardText.text = ((PlayerPrefs.GetInt("SideMission1") == 1) ? "$5" : "$0");
			this.sideMission2RewardText.text = ((PlayerPrefs.GetInt("SideMission2") == 1) ? "$5" : "$0");
			this.hiddenMissionRewardText.text = ((PlayerPrefs.GetInt("SideMission3") == 1) ? "$5" : "$0");
			this.totalPhotosRewardText.text = "$" + (this.GetPhotosRewardAmount() / 2).ToString();
			this.totalDNARewardText.text = ((PlayerPrefs.GetInt("DNAMission") == 1) ? "$5" : "$0");
			num2 += ((PlayerPrefs.GetInt("MainMission") == 1) ? (num / 2) : 0);
			num2 += ((PlayerPrefs.GetInt("SideMission1") == 1) ? 5 : 0);
			num2 += ((PlayerPrefs.GetInt("SideMission2") == 1) ? 5 : 0);
			num2 += ((PlayerPrefs.GetInt("SideMission3") == 1) ? 5 : 0);
			num2 += this.GetPhotosRewardAmount() / 2;
			num2 += ((PlayerPrefs.GetInt("DNAMission") == 1) ? 5 : 0);
			int num3 = 0;
			if (this.levelDifficulty == Contract.LevelDifficulty.Amateur)
			{
				num3 += PlayerPrefs.GetInt("totalItemCost") / 2;
			}
			else if (this.levelDifficulty == Contract.LevelDifficulty.Intermediate)
			{
				num3 += PlayerPrefs.GetInt("totalItemCost") / 4;
			}
			this.insuranceAmountText.text = "$" + num3.ToString();
			num2 += num3;
			PlayerPrefs.SetInt("totalItemCost", 0);
		}
		this.ghostTypeText.text = LocalisationSystem.GetLocalisedValue("Reward_Ghost") + " " + FileBasedPrefs.GetString("GhostType", "");
		FileBasedPrefs.SetInt("PlayersMoney", FileBasedPrefs.GetInt("PlayersMoney", 0) + num2);
		this.totalMissionRewardText.text = "$" + num2.ToString();
		InventoryManager.ResetTemporaryInventory();
		PlayerPrefs.SetInt("PhotosMission", 0);
		PlayerPrefs.SetInt("MainMission", 0);
		PlayerPrefs.SetInt("MissionType", 0);
		PlayerPrefs.SetInt("SideMission1", 0);
		PlayerPrefs.SetInt("SideMission2", 0);
		PlayerPrefs.SetInt("SideMission3", 0);
		PlayerPrefs.SetInt("DNAMission", 0);
		PlayerPrefs.SetInt("PhotosMission", 0);
		FileBasedPrefs.SetInt("PlayerDied", 0);
		this.storeManager.UpdatePlayerMoneyText();
		this.playerStatsManager.UpdateMoney();
		this.inventoryManager.UpdateItemsTotalText();
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x00042F0C File Offset: 0x0004110C
	private void SetupExperienceReward()
	{
		int @int = FileBasedPrefs.GetInt("myTotalExp", 0);
		int int2 = FileBasedPrefs.GetInt("totalExp", 0);
		int num = Mathf.FloorToInt((float)(@int / 100));
		int num2 = num + 1;
		this.currentLevelText.text = num.ToString();
		this.nextLevelText.text = num2.ToString();
		this.experienceGainedText.text = LocalisationSystem.GetLocalisedValue("Experience_Gained") + int2.ToString();
		if (Mathf.FloorToInt((float)((@int - int2) / 100)) < num && int2 > 0)
		{
			this.levelUpText.enabled = true;
			this.levelUpText.text = LocalisationSystem.GetLocalisedValue("Experience_Congrats") + num.ToString();
			this.CheckUnlocks(num);
		}
		else
		{
			this.levelUpText.enabled = false;
		}
		this.expSlider.value = (float)(100 - (num2 * 100 - @int));
		this.expSliderValueText.text = 100 - (num2 * 100 - @int) + "/100 XP".ToString();
		this.playerStatsManager.UpdateLevel();
		this.playerStatsManager.UpdateExperience();
		FileBasedPrefs.GetInt("totalExp", 0);
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x0004303C File Offset: 0x0004123C
	private int GetPhotosRewardAmount()
	{
		if (PlayerPrefs.GetInt("PhotosMission") == 0)
		{
			return 0;
		}
		if (PlayerPrefs.GetInt("PhotosMission") < 50)
		{
			return 10;
		}
		if (PlayerPrefs.GetInt("PhotosMission") < 100)
		{
			return 15;
		}
		if (PlayerPrefs.GetInt("PhotosMission") < 200)
		{
			return 20;
		}
		if (PlayerPrefs.GetInt("PhotosMission") < 300)
		{
			return 25;
		}
		if (PlayerPrefs.GetInt("PhotosMission") < 400)
		{
			return 30;
		}
		if (PlayerPrefs.GetInt("PhotosMission") < 500)
		{
			return 35;
		}
		return 40;
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x000430CC File Offset: 0x000412CC
	private void CheckUnlocks(int level)
	{
		if (level == 3)
		{
			this.unlock1Text.text = LocalisationSystem.GetLocalisedValue("Experience_Unlocked") + LocalisationSystem.GetLocalisedValue("Experience_Medium");
			this.unlock2Text.text = LocalisationSystem.GetLocalisedValue("Experience_Unlocked") + LocalisationSystem.GetLocalisedValue("Equipment_StrongFlashlight");
			return;
		}
		if (level == 5)
		{
			this.unlock1Text.text = LocalisationSystem.GetLocalisedValue("Experience_Unlocked") + LocalisationSystem.GetLocalisedValue("Experience_Large");
			this.unlock2Text.text = LocalisationSystem.GetLocalisedValue("Experience_Unlocked") + LocalisationSystem.GetLocalisedValue("Equipment_SanityPills");
			return;
		}
		if (level == 6)
		{
			this.unlock1Text.text = LocalisationSystem.GetLocalisedValue("Experience_Unlocked") + LocalisationSystem.GetLocalisedValue("Equipment_MotionSensor");
			this.unlock2Text.text = LocalisationSystem.GetLocalisedValue("Experience_Unlocked") + LocalisationSystem.GetLocalisedValue("Equipment_IRLightSensor");
			return;
		}
		if (level == 7)
		{
			this.unlock1Text.text = LocalisationSystem.GetLocalisedValue("Experience_Unlocked") + LocalisationSystem.GetLocalisedValue("Equipment_SoundSensor");
			this.unlock2Text.text = LocalisationSystem.GetLocalisedValue("Experience_Unlocked") + LocalisationSystem.GetLocalisedValue("Equipment_ParabolicMicrophone");
			return;
		}
		if (level == 8)
		{
			this.unlock1Text.text = LocalisationSystem.GetLocalisedValue("Experience_Unlocked") + LocalisationSystem.GetLocalisedValue("Equipment_HeadMountedCamera");
			return;
		}
		if (level == 10)
		{
			this.unlock1Text.text = LocalisationSystem.GetLocalisedValue("Experience_Unlocked") + LocalisationSystem.GetLocalisedValue("Experience_Intermediate");
			return;
		}
		if (level == 15)
		{
			this.unlock1Text.text = LocalisationSystem.GetLocalisedValue("Experience_Unlocked") + LocalisationSystem.GetLocalisedValue("Experience_Professional");
		}
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x0004328C File Offset: 0x0004148C
	public void ResumeButton()
	{
		if (PhotonNetwork.inRoom)
		{
			MainManager.instance.serverManager.EnableMasks(true);
			base.gameObject.SetActive(false);
			this.serverSelector.SetSelection();
			return;
		}
		this.mainObject.SetActive(true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000AED RID: 2797
	[Header("Main")]
	[SerializeField]
	private GameObject mainObject;

	// Token: 0x04000AEE RID: 2798
	[SerializeField]
	private PlayerStatsManager playerStatsManager;

	// Token: 0x04000AEF RID: 2799
	[SerializeField]
	private GamepadUISelector serverSelector;

	// Token: 0x04000AF0 RID: 2800
	[SerializeField]
	private InventoryManager inventoryManager;

	// Token: 0x04000AF1 RID: 2801
	[Header("Money Reward")]
	[SerializeField]
	private Text mainMissionRewardText;

	// Token: 0x04000AF2 RID: 2802
	[SerializeField]
	private Text sideMission1RewardText;

	// Token: 0x04000AF3 RID: 2803
	[SerializeField]
	private Text sideMission2RewardText;

	// Token: 0x04000AF4 RID: 2804
	[SerializeField]
	private Text hiddenMissionRewardText;

	// Token: 0x04000AF5 RID: 2805
	[SerializeField]
	private Text totalPhotosRewardText;

	// Token: 0x04000AF6 RID: 2806
	[SerializeField]
	private Text totalDNARewardText;

	// Token: 0x04000AF7 RID: 2807
	[SerializeField]
	private Text totalMissionRewardText;

	// Token: 0x04000AF8 RID: 2808
	[SerializeField]
	private Text ghostTypeText;

	// Token: 0x04000AF9 RID: 2809
	[SerializeField]
	private StoreManager storeManager;

	// Token: 0x04000AFA RID: 2810
	[SerializeField]
	private Text insuranceAmountText;

	// Token: 0x04000AFB RID: 2811
	[Header("Experience Reward")]
	[SerializeField]
	private Text levelUpText;

	// Token: 0x04000AFC RID: 2812
	[SerializeField]
	private Text experienceGainedText;

	// Token: 0x04000AFD RID: 2813
	[SerializeField]
	private Slider expSlider;

	// Token: 0x04000AFE RID: 2814
	[SerializeField]
	private Text expSliderValueText;

	// Token: 0x04000AFF RID: 2815
	[SerializeField]
	private Text currentLevelText;

	// Token: 0x04000B00 RID: 2816
	[SerializeField]
	private Text nextLevelText;

	// Token: 0x04000B01 RID: 2817
	[SerializeField]
	private Text unlock1Text;

	// Token: 0x04000B02 RID: 2818
	[SerializeField]
	private Text unlock2Text;

	// Token: 0x04000B03 RID: 2819
	[SerializeField]
	private Text unlock3Text;

	// Token: 0x04000B04 RID: 2820
	[SerializeField]
	private GameObject mainExperience;

	// Token: 0x04000B05 RID: 2821
	[SerializeField]
	private GameObject deadExperience;

	// Token: 0x04000B06 RID: 2822
	private Contract.LevelDifficulty levelDifficulty;
}
