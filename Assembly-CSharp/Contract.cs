using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017A RID: 378
public class Contract : MonoBehaviour
{
	// Token: 0x060009FA RID: 2554 RVA: 0x0003D938 File Offset: 0x0003BB38
	private void Awake()
	{
		this.CreateDescriptions();
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x0003D940 File Offset: 0x0003BB40
	public void SelectContractButton()
	{
		this.levelSelectionManager.SelectContractButton(this);
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x0003D950 File Offset: 0x0003BB50
	public void CreateDescriptions()
	{
		this.secondDescription = LocalisationSystem.GetLocalisedValue("Map_SecondDescription" + Random.Range(1, 6));
		this.thirdDescription = LocalisationSystem.GetLocalisedValue("Map_ThirdDescription" + Random.Range(1, 6));
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x0003D9A0 File Offset: 0x0003BBA0
	public void GenerateContract()
	{
		int num = Mathf.FloorToInt((float)(FileBasedPrefs.GetInt("myTotalExp", 0) / 100));
		if (num < 3)
		{
			this.GenerateSmallLevel(false, 0);
		}
		else if (num < 5)
		{
			if (Random.Range(0, 2) == 1)
			{
				this.GenerateSmallLevel(false, 0);
			}
			else
			{
				this.GenerateMediumLevel();
			}
		}
		else
		{
			int num2 = Random.Range(0, 5);
			if (num2 == 0 || num2 == 1)
			{
				this.GenerateSmallLevel(false, 0);
			}
			else if (num2 == 2 || num2 == 3)
			{
				this.GenerateMediumLevel();
			}
			else
			{
				this.GenerateLargeLevel();
			}
		}
		if (num < 10)
		{
			this.levelDiffulty = Contract.LevelDifficulty.Amateur;
		}
		else if (num >= 10 && num < 15)
		{
			if (Random.Range(0, 4) == 1)
			{
				this.levelDiffulty = Contract.LevelDifficulty.Intermediate;
			}
			else
			{
				this.levelDiffulty = Contract.LevelDifficulty.Amateur;
			}
		}
		else if (num >= 25)
		{
			int num3 = Random.Range(0, 10);
			if (num3 < 2)
			{
				this.levelDiffulty = Contract.LevelDifficulty.Amateur;
			}
			else if (num3 < 6)
			{
				this.levelDiffulty = Contract.LevelDifficulty.Intermediate;
			}
			else
			{
				this.levelDiffulty = Contract.LevelDifficulty.Professional;
			}
		}
		else
		{
			int num4 = Random.Range(0, 10);
			if (num4 < 4)
			{
				this.levelDiffulty = Contract.LevelDifficulty.Amateur;
			}
			else if (num4 < 7)
			{
				this.levelDiffulty = Contract.LevelDifficulty.Intermediate;
			}
			else
			{
				this.levelDiffulty = Contract.LevelDifficulty.Professional;
			}
		}
		this.levelNameText.text = this.levelName;
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x0003DAC4 File Offset: 0x0003BCC4
	public void GenerateSmallLevel(bool forceLevel, int _levelID)
	{
		int num;
		if (forceLevel)
		{
			num = _levelID;
		}
		else
		{
			num = Random.Range(0, 5);
		}
		switch (num)
		{
		case 0:
			this.levelName = "Tanglewood Street House";
			this.levelType = Contract.LevelType.Tanglewood_Street_House;
			this.levelSize = Contract.LevelSize.small;
			this.basicDescription = LocalisationSystem.GetLocalisedValue("Map_TanglewoodDescription");
			this.firstBulletPoint = LocalisationSystem.GetLocalisedValue("Map_TeamSize") + "1";
			this.secondBulletPoint = LocalisationSystem.GetLocalisedValue("Map_RecommendedItem") + "None";
			this.thirdBulletPoint = LocalisationSystem.GetLocalisedValue("Map_LocationSizeSmall");
			break;
		case 1:
			this.levelName = "Edgefield Street House";
			this.levelType = Contract.LevelType.Edgefield_Street_House;
			this.levelSize = Contract.LevelSize.small;
			this.basicDescription = LocalisationSystem.GetLocalisedValue("Map_EdgefieldDescription");
			this.firstBulletPoint = LocalisationSystem.GetLocalisedValue("Map_TeamSize") + "2";
			this.secondBulletPoint = LocalisationSystem.GetLocalisedValue("Map_RecommendedItem") + LocalisationSystem.GetLocalisedValue("Equipment_Thermometer");
			this.thirdBulletPoint = LocalisationSystem.GetLocalisedValue("Map_LocationSizeSmall");
			break;
		case 2:
			this.levelName = "Ridgeview Road House";
			this.levelType = Contract.LevelType.Ridgeview_Road_House;
			this.levelSize = Contract.LevelSize.small;
			this.basicDescription = LocalisationSystem.GetLocalisedValue("Map_RidgeviewDescription");
			this.firstBulletPoint = LocalisationSystem.GetLocalisedValue("Map_TeamSize") + "2";
			this.secondBulletPoint = LocalisationSystem.GetLocalisedValue("Map_RecommendedItem") + LocalisationSystem.GetLocalisedValue("Equipment_Thermometer");
			this.thirdBulletPoint = LocalisationSystem.GetLocalisedValue("Map_LocationSizeSmall");
			break;
		case 3:
			this.levelName = "Bleasdale Farmhouse";
			this.levelType = Contract.LevelType.Bleasdale_Farmhouse;
			this.levelSize = Contract.LevelSize.small;
			this.basicDescription = LocalisationSystem.GetLocalisedValue("Map_BleasdaleDescription");
			this.firstBulletPoint = LocalisationSystem.GetLocalisedValue("Map_TeamSize") + "2";
			this.secondBulletPoint = LocalisationSystem.GetLocalisedValue("Map_RecommendedItem") + LocalisationSystem.GetLocalisedValue("Equipment_Thermometer");
			this.thirdBulletPoint = LocalisationSystem.GetLocalisedValue("Map_LocationSizeSmall");
			break;
		case 4:
			this.levelName = "Grafton Farmhouse";
			this.levelType = Contract.LevelType.Grafton_Farmhouse;
			this.levelSize = Contract.LevelSize.small;
			this.basicDescription = LocalisationSystem.GetLocalisedValue("Map_GraftonDescription");
			this.firstBulletPoint = LocalisationSystem.GetLocalisedValue("Map_TeamSize") + "2";
			this.secondBulletPoint = LocalisationSystem.GetLocalisedValue("Map_RecommendedItem") + LocalisationSystem.GetLocalisedValue("Equipment_Thermometer");
			this.thirdBulletPoint = LocalisationSystem.GetLocalisedValue("Map_LocationSizeSmall");
			break;
		default:
			Debug.LogError("Cannot generate a small map with id: " + num);
			break;
		}
		for (int i = 0; i < this.levelSelectionManager.currentContracts.Count; i++)
		{
			if (this.levelSelectionManager.currentContracts[i].levelType == this.levelType)
			{
				this.GenerateContract();
				return;
			}
		}
		this.levelSelectionManager.currentContracts.Add(this);
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x0003DDB4 File Offset: 0x0003BFB4
	public void GenerateMediumLevel()
	{
		this.levelName = "Brownstone High School";
		this.levelType = Contract.LevelType.Brownstone_High_School;
		this.levelSize = Contract.LevelSize.medium;
		this.basicDescription = LocalisationSystem.GetLocalisedValue("Map_SchoolDescription");
		this.firstBulletPoint = LocalisationSystem.GetLocalisedValue("Map_TeamSize") + "3";
		this.secondBulletPoint = LocalisationSystem.GetLocalisedValue("Map_RecommendedItem") + LocalisationSystem.GetLocalisedValue("Equipment_SanityPills");
		this.thirdBulletPoint = LocalisationSystem.GetLocalisedValue("Map_LocationSizeMedium");
		for (int i = 0; i < this.levelSelectionManager.currentContracts.Count; i++)
		{
			if (this.levelSelectionManager.currentContracts[i].levelType == this.levelType)
			{
				this.GenerateContract();
				return;
			}
		}
		this.levelSelectionManager.currentContracts.Add(this);
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x0003DE84 File Offset: 0x0003C084
	public void GenerateLargeLevel()
	{
		this.levelName = "Asylum";
		this.levelType = Contract.LevelType.Asylum;
		this.levelSize = Contract.LevelSize.large;
		this.basicDescription = LocalisationSystem.GetLocalisedValue("Map_AsylumDescription");
		this.firstBulletPoint = LocalisationSystem.GetLocalisedValue("Map_TeamSize") + "4";
		this.secondBulletPoint = LocalisationSystem.GetLocalisedValue("Map_RecommendedItem") + LocalisationSystem.GetLocalisedValue("Equipment_SanityPills");
		this.thirdBulletPoint = LocalisationSystem.GetLocalisedValue("Map_LocationSizeLarge");
		for (int i = 0; i < this.levelSelectionManager.currentContracts.Count; i++)
		{
			if (this.levelSelectionManager.currentContracts[i].levelType == this.levelType)
			{
				this.GenerateContract();
				return;
			}
		}
		this.levelSelectionManager.currentContracts.Add(this);
	}

	// Token: 0x04000A21 RID: 2593
	[HideInInspector]
	public string levelName;

	// Token: 0x04000A22 RID: 2594
	[HideInInspector]
	public Contract.LevelType levelType;

	// Token: 0x04000A23 RID: 2595
	[HideInInspector]
	public Contract.LevelSize levelSize;

	// Token: 0x04000A24 RID: 2596
	[HideInInspector]
	public Contract.LevelDifficulty levelDiffulty;

	// Token: 0x04000A25 RID: 2597
	[HideInInspector]
	public string basicDescription;

	// Token: 0x04000A26 RID: 2598
	[HideInInspector]
	public string secondDescription;

	// Token: 0x04000A27 RID: 2599
	[HideInInspector]
	public string thirdDescription;

	// Token: 0x04000A28 RID: 2600
	[HideInInspector]
	public string firstBulletPoint;

	// Token: 0x04000A29 RID: 2601
	[HideInInspector]
	public string secondBulletPoint;

	// Token: 0x04000A2A RID: 2602
	[HideInInspector]
	public string thirdBulletPoint;

	// Token: 0x04000A2B RID: 2603
	[SerializeField]
	private LevelSelectionManager levelSelectionManager;

	// Token: 0x04000A2C RID: 2604
	public Text levelNameText;

	// Token: 0x02000543 RID: 1347
	public enum LevelDifficulty
	{
		// Token: 0x04002538 RID: 9528
		Amateur,
		// Token: 0x04002539 RID: 9529
		Intermediate,
		// Token: 0x0400253A RID: 9530
		Professional
	}

	// Token: 0x02000544 RID: 1348
	public enum LevelSize
	{
		// Token: 0x0400253C RID: 9532
		none,
		// Token: 0x0400253D RID: 9533
		small,
		// Token: 0x0400253E RID: 9534
		medium,
		// Token: 0x0400253F RID: 9535
		large
	}

	// Token: 0x02000545 RID: 1349
	public enum LevelType
	{
		// Token: 0x04002541 RID: 9537
		none,
		// Token: 0x04002542 RID: 9538
		Tanglewood_Street_House,
		// Token: 0x04002543 RID: 9539
		Asylum,
		// Token: 0x04002544 RID: 9540
		Edgefield_Street_House,
		// Token: 0x04002545 RID: 9541
		Ridgeview_Road_House,
		// Token: 0x04002546 RID: 9542
		Brownstone_High_School,
		// Token: 0x04002547 RID: 9543
		Bleasdale_Farmhouse,
		// Token: 0x04002548 RID: 9544
		Grafton_Farmhouse
	}
}
