using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000183 RID: 387
public class LevelSelectionManager : MonoBehaviour
{
	// Token: 0x06000A4C RID: 2636 RVA: 0x0003FD10 File Offset: 0x0003DF10
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0003FD20 File Offset: 0x0003DF20
	private void Start()
	{
		if (this.storeSDKManager.storeBranchType == StoreSDKManager.StoreBranchType.youtube)
		{
			this.AssignYoutuberContracts();
		}
		else
		{
			this.AssignContracts();
		}
		if (this.selectedLevelName == string.Empty)
		{
			this.readyButton.interactable = false;
			this.readyText.color = new Color32(50, 50, 50, 119);
		}
		this.localisedExperience_Required = LocalisationSystem.GetLocalisedValue("Experience_Required");
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0003FD94 File Offset: 0x0003DF94
	private void AssignContracts()
	{
		for (int i = 0; i < this.contracts.Count; i++)
		{
			Contract value = this.contracts[i];
			int index = Random.Range(i, this.contracts.Count);
			this.contracts[i] = this.contracts[index];
			this.contracts[index] = value;
			this.contracts[i].gameObject.SetActive(false);
		}
		int num = Random.Range(2, 5);
		for (int j = 0; j < num; j++)
		{
			this.contracts[j].gameObject.SetActive(true);
			this.contracts[j].GenerateContract();
		}
		this.ForceGenerateSmallContract(num);
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0003FE5C File Offset: 0x0003E05C
	public void UpdateLocalisation()
	{
		for (int i = 0; i < this.contracts.Count; i++)
		{
			this.contracts[i].CreateDescriptions();
		}
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0003FE90 File Offset: 0x0003E090
	private void AssignYoutuberContracts()
	{
		this.contracts[0].gameObject.SetActive(true);
		this.contracts[0].GenerateSmallLevel(true, 0);
		this.contracts[0].levelNameText.text = this.contracts[0].levelName;
		this.contracts[1].gameObject.SetActive(true);
		this.contracts[1].GenerateSmallLevel(true, 1);
		this.contracts[1].levelNameText.text = this.contracts[1].levelName;
		this.contracts[2].gameObject.SetActive(true);
		this.contracts[2].GenerateSmallLevel(true, 2);
		this.contracts[2].levelNameText.text = this.contracts[2].levelName;
		this.contracts[3].gameObject.SetActive(true);
		this.contracts[3].GenerateSmallLevel(true, 3);
		this.contracts[3].levelNameText.text = this.contracts[3].levelName;
		this.contracts[4].gameObject.SetActive(true);
		this.contracts[4].GenerateSmallLevel(true, 4);
		this.contracts[4].levelNameText.text = this.contracts[4].levelName;
		this.contracts[5].gameObject.SetActive(true);
		this.contracts[5].GenerateMediumLevel();
		this.contracts[5].levelNameText.text = this.contracts[5].levelName;
		this.contracts[6].gameObject.SetActive(true);
		this.contracts[6].GenerateLargeLevel();
		this.contracts[6].levelNameText.text = this.contracts[6].levelName;
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x000400D0 File Offset: 0x0003E2D0
	public void SelectContractButton(Contract contract)
	{
		this.selectedContract = contract;
		this.ShowContract();
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x000400E0 File Offset: 0x0003E2E0
	public void SyncContract()
	{
		this.view.RPC("NetworkedLevelSelect", PhotonTargets.AllBufferedViaServer, new object[]
		{
			this.selectedContract.levelName,
			this.selectedContract.levelType.ToString(),
			(int)this.selectedContract.levelDiffulty
		});
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00040140 File Offset: 0x0003E340
	public void SelectButton()
	{
		this.view.RPC("NetworkedLevelSelect", PhotonTargets.AllBufferedViaServer, new object[]
		{
			this.selectedContract.levelName,
			this.selectedContract.levelType.ToString(),
			(int)this.selectedContract.levelDiffulty
		});
		this.descriptionObject.SetActive(false);
		this.mapObject.SetActive(true);
		this.serverManager.SelectJob(false);
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x000401C2 File Offset: 0x0003E3C2
	[PunRPC]
	public void NetworkedLevelSelect(string name, string levelName, int difficulty)
	{
		this.selectedLevelName = levelName;
		this.contractLevelName = name;
		this.contractLevelDifficulty = (Contract.LevelDifficulty)difficulty;
		this.serverManager.UpdateUI();
		FileBasedPrefs.SetInt("LevelDifficulty", difficulty);
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x000401F0 File Offset: 0x0003E3F0
	private void ShowContract()
	{
		this.titleText.text = this.selectedContract.levelName;
		this.descriptionText.text = string.Concat(new string[]
		{
			this.selectedContract.basicDescription,
			" ",
			this.selectedContract.secondDescription,
			" ",
			this.selectedContract.thirdDescription
		});
		this.firstBulletPointText.text = this.selectedContract.firstBulletPoint;
		this.secondBulletPointText.text = this.selectedContract.secondBulletPoint;
		this.thirdBulletPointText.text = this.selectedContract.thirdBulletPoint;
		if (this.selectedContract.levelDiffulty == Contract.LevelDifficulty.Amateur)
		{
			this.difficultyLevelText.text = LocalisationSystem.GetLocalisedValue("Contract_Amateur");
		}
		else if (this.selectedContract.levelDiffulty == Contract.LevelDifficulty.Intermediate)
		{
			this.difficultyLevelText.text = LocalisationSystem.GetLocalisedValue("Contract_Intermediate");
		}
		else
		{
			this.difficultyLevelText.text = LocalisationSystem.GetLocalisedValue("Contract_Professional");
		}
		this.readyButton.interactable = true;
		this.selectButton.interactable = true;
		this.selectButtonText.color = new Color32(50, 50, 50, byte.MaxValue);
		this.descriptionObject.SetActive(true);
		this.mapObject.SetActive(false);
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x00040354 File Offset: 0x0003E554
	private void ForceGenerateSmallContract(int amount)
	{
		for (int i = 0; i < this.currentContracts.Count; i++)
		{
			if (this.currentContracts[i].gameObject.activeInHierarchy && this.currentContracts[i].levelSize == Contract.LevelSize.small)
			{
				return;
			}
		}
		Contract contract = this.contracts[amount + 1];
		contract.gameObject.SetActive(true);
		contract.GenerateSmallLevel(false, 0);
		contract.levelNameText.text = contract.levelName;
	}

	// Token: 0x04000A69 RID: 2665
	[SerializeField]
	private ServerManager serverManager;

	// Token: 0x04000A6A RID: 2666
	[SerializeField]
	private StoreSDKManager storeSDKManager;

	// Token: 0x04000A6B RID: 2667
	[HideInInspector]
	public List<Contract> currentContracts = new List<Contract>();

	// Token: 0x04000A6C RID: 2668
	[SerializeField]
	private List<Contract> contracts = new List<Contract>();

	// Token: 0x04000A6D RID: 2669
	[HideInInspector]
	public string selectedLevelName;

	// Token: 0x04000A6E RID: 2670
	[HideInInspector]
	public string contractLevelName;

	// Token: 0x04000A6F RID: 2671
	[HideInInspector]
	public Contract.LevelDifficulty contractLevelDifficulty;

	// Token: 0x04000A70 RID: 2672
	[SerializeField]
	private Button readyButton;

	// Token: 0x04000A71 RID: 2673
	[SerializeField]
	private Text readyText;

	// Token: 0x04000A72 RID: 2674
	[SerializeField]
	private Text titleText;

	// Token: 0x04000A73 RID: 2675
	[SerializeField]
	private Text descriptionText;

	// Token: 0x04000A74 RID: 2676
	[SerializeField]
	private Text firstBulletPointText;

	// Token: 0x04000A75 RID: 2677
	[SerializeField]
	private Text secondBulletPointText;

	// Token: 0x04000A76 RID: 2678
	[SerializeField]
	private Text thirdBulletPointText;

	// Token: 0x04000A77 RID: 2679
	[SerializeField]
	private Text difficultyLevelText;

	// Token: 0x04000A78 RID: 2680
	[SerializeField]
	private Button selectButton;

	// Token: 0x04000A79 RID: 2681
	[SerializeField]
	private Text selectButtonText;

	// Token: 0x04000A7A RID: 2682
	private string localisedExperience_Required;

	// Token: 0x04000A7B RID: 2683
	private PhotonView view;

	// Token: 0x04000A7C RID: 2684
	[HideInInspector]
	public Contract selectedContract;

	// Token: 0x04000A7D RID: 2685
	[SerializeField]
	private GameObject mapObject;

	// Token: 0x04000A7E RID: 2686
	[SerializeField]
	private GameObject descriptionObject;
}
