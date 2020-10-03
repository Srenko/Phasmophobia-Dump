using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017E RID: 382
public class FailureManager : MonoBehaviour
{
	// Token: 0x06000A12 RID: 2578 RVA: 0x0003E34F File Offset: 0x0003C54F
	private void Awake()
	{
		if (FileBasedPrefs.GetInt("PlayerDied", 0) == 1)
		{
			InventoryManager.RemoveItemsFromInventory();
		}
		else
		{
			this.failureMessage.enabled = false;
		}
		FileBasedPrefs.SetInt("PlayerDied", 0);
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x0003E380 File Offset: 0x0003C580
	private void Start()
	{
		if (FileBasedPrefs.GetInt("setupPhase", 0) == 1 && FileBasedPrefs.GetInt("LevelDifficulty", 0) == 0)
		{
			FileBasedPrefs.SetInt("PlayersMoney", FileBasedPrefs.GetInt("PlayersMoney", 0) + 10);
			this.rewardMessage.text = LocalisationSystem.GetLocalisedValue("Failure_ContractPay") + ": $10";
		}
		else
		{
			this.rewardMessage.text = LocalisationSystem.GetLocalisedValue("Failure_ContractPay") + ": $0";
		}
		FileBasedPrefs.SetInt("setupPhase", 0);
		this.storeManager.UpdatePlayerMoneyText();
		FileBasedPrefs.SetInt("MissionStatus", 0);
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x0003E421 File Offset: 0x0003C621
	public void ResumeButton()
	{
		if (PhotonNetwork.inRoom)
		{
			MainManager.instance.serverManager.EnableMasks(true);
			base.gameObject.SetActive(false);
			return;
		}
		this.mainObject.SetActive(true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000A3B RID: 2619
	[SerializeField]
	private Text rewardMessage;

	// Token: 0x04000A3C RID: 2620
	[SerializeField]
	private Text failureMessage;

	// Token: 0x04000A3D RID: 2621
	[SerializeField]
	private StoreManager storeManager;

	// Token: 0x04000A3E RID: 2622
	[SerializeField]
	private GameObject mainObject;
}
