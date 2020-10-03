using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000195 RID: 405
public class StoreManager : MonoBehaviour
{
	// Token: 0x06000AEC RID: 2796 RVA: 0x00044591 File Offset: 0x00042791
	private void Awake()
	{
		this.UpdatePlayerMoneyText();
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x00044599 File Offset: 0x00042799
	public void StartStore()
	{
		if (Application.isEditor || this.storeSDKManager.storeBranchType == StoreSDKManager.StoreBranchType.youtube)
		{
			this.freeMoneyButton.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x000445C4 File Offset: 0x000427C4
	public void ItemButtonPress(StoreItem item)
	{
		this.currentItem = item;
		for (int i = 0; i < this.itemCards.Length; i++)
		{
			this.itemCards[i].SetActive(false);
		}
		this.currentItem.description.SetActive(true);
		this.currentItem.amountOwnedText.text = LocalisationSystem.GetLocalisedValue("Store_Owned") + ": " + FileBasedPrefs.GetInt(this.currentItem.itemName + "Inventory", 0);
		if (Mathf.FloorToInt((float)(FileBasedPrefs.GetInt("myTotalExp", 0) / 100)) < item.requiredLevel && this.storeSDKManager.storeBranchType != StoreSDKManager.StoreBranchType.youtube)
		{
			this.currentItem.buyButton.interactable = false;
			this.currentItem.buyButtonText.color = new Color32(50, 50, 50, 119);
			this.currentItem.amountOwnedText.text = LocalisationSystem.GetLocalisedValue("Experience_Required") + item.requiredLevel;
		}
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x000446D8 File Offset: 0x000428D8
	public void BuyButton()
	{
		if (FileBasedPrefs.GetInt("PlayersMoney", 0) >= this.currentItem.cost)
		{
			FileBasedPrefs.SetInt(this.currentItem.itemName + "Inventory", FileBasedPrefs.GetInt(this.currentItem.itemName + "Inventory", 0) + 1);
			FileBasedPrefs.SetInt("PlayersMoney", FileBasedPrefs.GetInt("PlayersMoney", 0) - this.currentItem.cost);
			this.UpdatePlayerMoneyText();
			this.currentItem.amountOwnedText.text = LocalisationSystem.GetLocalisedValue("Store_Owned") + ": " + FileBasedPrefs.GetInt(this.currentItem.itemName + "Inventory", 0);
			DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.BuyAnItem, 1);
		}
		AntiCheatSystem.CheckPlayerMoney(FileBasedPrefs.GetInt("PlayersMoney", 0));
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x000447BE File Offset: 0x000429BE
	public void BackButton()
	{
		if (PhotonNetwork.inRoom)
		{
			this.serverManager.OpenStore(false);
			return;
		}
		this.mainObject.SetActive(true);
		this.storeObject.SetActive(false);
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x000447EC File Offset: 0x000429EC
	public void UpdatePlayerMoneyText()
	{
		this.playerMoneyText.text = "$" + FileBasedPrefs.GetInt("PlayersMoney", 0).ToString();
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x00044821 File Offset: 0x00042A21
	public void GivePlayerMoneyButton()
	{
		FileBasedPrefs.SetInt("PlayersMoney", FileBasedPrefs.GetInt("PlayersMoney", 0) + 100);
		this.UpdatePlayerMoneyText();
		AntiCheatSystem.CheckPlayerMoney(FileBasedPrefs.GetInt("PlayersMoney", 0));
	}

	// Token: 0x04000B43 RID: 2883
	[SerializeField]
	private Text playerMoneyText;

	// Token: 0x04000B44 RID: 2884
	[SerializeField]
	private GameObject[] itemCards;

	// Token: 0x04000B45 RID: 2885
	private StoreItem currentItem;

	// Token: 0x04000B46 RID: 2886
	[SerializeField]
	private Button freeMoneyButton;

	// Token: 0x04000B47 RID: 2887
	[SerializeField]
	private GameObject mainObject;

	// Token: 0x04000B48 RID: 2888
	[SerializeField]
	private GameObject storeObject;

	// Token: 0x04000B49 RID: 2889
	[SerializeField]
	private ServerManager serverManager;

	// Token: 0x04000B4A RID: 2890
	[SerializeField]
	private StoreSDKManager storeSDKManager;
}
