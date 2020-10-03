using System;
using System.Collections.Generic;
using Photon;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000182 RID: 386
public class InventoryManager : Photon.MonoBehaviour
{
	// Token: 0x06000A40 RID: 2624 RVA: 0x0003F4B0 File Offset: 0x0003D6B0
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		for (int i = 0; i < this.maskTransforms.Length; i++)
		{
			this.maskTransforms[i].sizeDelta = new Vector2(0f, 0f);
		}
		this.maskTransforms[0].sizeDelta = new Vector2(1200f, 1200f);
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x0003F514 File Offset: 0x0003D714
	public void AddButton(InventoryItem item)
	{
		if (item.canChangeAmount && item.totalAmount < item.maxAmount)
		{
			int i = 0;
			while (i < item.players.Count)
			{
				if (item.players[i].isLocalPlayer)
				{
					if (FileBasedPrefs.GetInt(item.itemName + "Inventory", 0) > item.players[i].currentAmount)
					{
						item.canChangeAmount = false;
						item.ChangeTotalAmount(PhotonNetwork.player.ID, 1);
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0003F5A4 File Offset: 0x0003D7A4
	public void UpdateText()
	{
		this.inventoryText.text = "";
		for (int i = 0; i < this.items.Count; i++)
		{
			if (this.items[i].totalAmount > 0)
			{
				Text text = this.inventoryText;
				text.text = string.Concat(new object[]
				{
					text.text,
					(i == 0) ? "" : "\n",
					LocalisationSystem.GetLocalisedValue(this.items[i].localisedItemName),
					" x [",
					this.items[i].totalAmount,
					"/",
					this.items[i].maxAmount,
					"]"
				});
			}
		}
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x0003F68C File Offset: 0x0003D88C
	public void MinusButton(InventoryItem item)
	{
		if (item.canChangeAmount)
		{
			for (int i = 0; i < item.players.Count; i++)
			{
				if (item.players[i].isLocalPlayer && item.players[i].currentAmount >= 1)
				{
					item.canChangeAmount = false;
					item.ChangeTotalAmount(PhotonNetwork.player.ID, -1);
					return;
				}
			}
		}
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x00003F60 File Offset: 0x00002160
	public void LeftRoom()
	{
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x0003F6F8 File Offset: 0x0003D8F8
	private void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		if (PhotonNetwork.isMasterClient)
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				this.items[i].LeftRoom(player.ID);
			}
		}
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0003F73C File Offset: 0x0003D93C
	public void SaveInventory()
	{
		PlayerPrefs.SetInt("totalItemCost", 0);
		for (int i = 0; i < this.items.Count; i++)
		{
			this.items[i].SaveItem();
		}
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0003F77C File Offset: 0x0003D97C
	public void ChangePageButton(int pageID)
	{
		for (int i = 0; i < this.maskTransforms.Length; i++)
		{
			this.maskTransforms[i].sizeDelta = new Vector2(0f, 0f);
		}
		this.maskTransforms[pageID].sizeDelta = new Vector2(1200f, 1200f);
		if (pageID == 0)
		{
			this.serverSelector.SetSelection();
			return;
		}
		if (pageID == 1)
		{
			this.inventorySelector.SetSelection();
		}
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
	public static void RemoveItemsFromInventory()
	{
		FileBasedPrefs.SetInt("EMFReaderInventory", FileBasedPrefs.GetInt("EMFReaderInventory", 0) - PlayerPrefs.GetInt("currentEMFReaderAmount"));
		FileBasedPrefs.SetInt("FlashlightInventory", FileBasedPrefs.GetInt("FlashlightInventory", 0) - PlayerPrefs.GetInt("currentFlashlightAmount"));
		FileBasedPrefs.SetInt("CameraInventory", FileBasedPrefs.GetInt("CameraInventory", 0) - PlayerPrefs.GetInt("currentCameraAmount"));
		FileBasedPrefs.SetInt("LighterInventory", FileBasedPrefs.GetInt("LighterInventory", 0) - PlayerPrefs.GetInt("currentLighterAmount"));
		FileBasedPrefs.SetInt("CandleInventory", FileBasedPrefs.GetInt("CandleInventory", 0) - PlayerPrefs.GetInt("currentCandleAmount"));
		FileBasedPrefs.SetInt("UVFlashlightInventory", FileBasedPrefs.GetInt("UVFlashlightInventory", 0) - PlayerPrefs.GetInt("currentUVFlashlightAmount"));
		FileBasedPrefs.SetInt("CrucifixInventory", FileBasedPrefs.GetInt("CrucifixInventory", 0) - PlayerPrefs.GetInt("currentCrucifixAmount"));
		FileBasedPrefs.SetInt("DSLRCameraInventory", FileBasedPrefs.GetInt("DSLRCameraInventory", 0) - PlayerPrefs.GetInt("currentDSLRCameraAmount"));
		FileBasedPrefs.SetInt("EVPRecorderInventory", FileBasedPrefs.GetInt("EVPRecorderInventory", 0) - PlayerPrefs.GetInt("currentEVPRecorderAmount"));
		FileBasedPrefs.SetInt("SaltInventory", FileBasedPrefs.GetInt("SaltInventory", 0) - PlayerPrefs.GetInt("currentSaltAmount"));
		FileBasedPrefs.SetInt("SageInventory", FileBasedPrefs.GetInt("SageInventory", 0) - PlayerPrefs.GetInt("currentSageAmount"));
		FileBasedPrefs.SetInt("TripodInventory", FileBasedPrefs.GetInt("TripodInventory", 0) - PlayerPrefs.GetInt("currentTripodAmount"));
		FileBasedPrefs.SetInt("StrongFlashlightInventory", FileBasedPrefs.GetInt("StrongFlashlightInventory", 0) - PlayerPrefs.GetInt("currentStrongFlashlightAmount"));
		FileBasedPrefs.SetInt("MotionSensorInventory", FileBasedPrefs.GetInt("MotionSensorInventory", 0) - PlayerPrefs.GetInt("currentMotionSensorAmount"));
		FileBasedPrefs.SetInt("SoundSensorInventory", FileBasedPrefs.GetInt("SoundSensorInventory", 0) - PlayerPrefs.GetInt("currentSoundSensorAmount"));
		int value = FileBasedPrefs.GetInt("SanityPillsInventory", 0) - PlayerPrefs.GetInt("currentSanityPillsAmount");
		value = Mathf.Clamp(value, 0, 1000);
		FileBasedPrefs.SetInt("SanityPillsInventory", value);
		FileBasedPrefs.SetInt("ThermometerInventory", FileBasedPrefs.GetInt("ThermometerInventory", 0) - PlayerPrefs.GetInt("currentThermometerAmount"));
		FileBasedPrefs.SetInt("GhostWritingBookInventory", FileBasedPrefs.GetInt("GhostWritingBookInventory", 0) - PlayerPrefs.GetInt("currentGhostWritingBookAmount"));
		FileBasedPrefs.SetInt("IRLightSensorInventory", FileBasedPrefs.GetInt("IRLightSensorInventory", 0) - PlayerPrefs.GetInt("currentIRLightSensorAmount"));
		FileBasedPrefs.SetInt("ParabolicMicrophoneInventory", FileBasedPrefs.GetInt("ParabolicMicrophoneInventory", 0) - PlayerPrefs.GetInt("currentParabolicMicrophoneAmount"));
		FileBasedPrefs.SetInt("GlowstickInventory", FileBasedPrefs.GetInt("GlowstickInventory", 0) - PlayerPrefs.GetInt("currentGlowstickAmount"));
		FileBasedPrefs.SetInt("HeadMountedCameraInventory", FileBasedPrefs.GetInt("HeadMountedCameraInventory", 0) - PlayerPrefs.GetInt("currentHeadMountedCameraAmount"));
		InventoryManager.ResetTemporaryInventory();
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x0003FAD8 File Offset: 0x0003DCD8
	public void UpdateItemsTotalText()
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			this.items[i].UpdateTotalText();
		}
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0003FB0C File Offset: 0x0003DD0C
	public static void ResetTemporaryInventory()
	{
		PlayerPrefs.SetInt("currentEMFReaderAmount", 0);
		PlayerPrefs.SetInt("currentFlashlightAmount", 0);
		PlayerPrefs.SetInt("currentCameraAmount", 0);
		PlayerPrefs.SetInt("currentLighterAmount", 0);
		PlayerPrefs.SetInt("currentCandleAmount", 0);
		PlayerPrefs.SetInt("currentUVFlashlightAmount", 0);
		PlayerPrefs.SetInt("currentCrucifixAmount", 0);
		PlayerPrefs.SetInt("currentDSLRCameraAmount", 0);
		PlayerPrefs.SetInt("currentEVPRecorderAmount", 0);
		PlayerPrefs.SetInt("currentSaltAmount", 0);
		PlayerPrefs.SetInt("currentSageAmount", 0);
		PlayerPrefs.SetInt("currentTripodAmount", 0);
		PlayerPrefs.SetInt("currentStrongFlashlightAmount", 0);
		PlayerPrefs.SetInt("currentMotionSensorAmount", 0);
		PlayerPrefs.SetInt("currentSoundSensorAmount", 0);
		PlayerPrefs.SetInt("currentSanityPillsAmount", 0);
		PlayerPrefs.SetInt("currentThermometerAmount", 0);
		PlayerPrefs.SetInt("currentGhostWritingBookAmount", 0);
		PlayerPrefs.SetInt("currentIRLightSensorAmount", 0);
		PlayerPrefs.SetInt("currentParabolicMicrophoneAmount", 0);
		PlayerPrefs.SetInt("currentGlowstickAmount", 0);
		PlayerPrefs.SetInt("currentHeadMountedCameraAmount", 0);
		PlayerPrefs.SetInt("totalEMFReaderAmount", 0);
		PlayerPrefs.SetInt("totalFlashlightAmount", 0);
		PlayerPrefs.SetInt("totalCameraAmount", 0);
		PlayerPrefs.SetInt("totalLighterAmount", 0);
		PlayerPrefs.SetInt("totalCandleAmount", 0);
		PlayerPrefs.SetInt("totalUVFlashlightAmount", 0);
		PlayerPrefs.SetInt("totalCrucifixAmount", 0);
		PlayerPrefs.SetInt("totalDSLRCameraAmount", 0);
		PlayerPrefs.SetInt("totalEVPRecorderAmount", 0);
		PlayerPrefs.SetInt("totalSaltAmount", 0);
		PlayerPrefs.SetInt("totalSageAmount", 0);
		PlayerPrefs.SetInt("totalTripodAmount", 0);
		PlayerPrefs.SetInt("totalStrongFlashlightAmount", 0);
		PlayerPrefs.SetInt("totalMotionSensorAmount", 0);
		PlayerPrefs.SetInt("totalSoundSensorAmount", 0);
		PlayerPrefs.SetInt("totalSanityPillsAmount", 0);
		PlayerPrefs.SetInt("totalThermometerAmount", 0);
		PlayerPrefs.SetInt("totalGhostWritingBookAmount", 0);
		PlayerPrefs.SetInt("totalIRLightSensorAmount", 0);
		PlayerPrefs.SetInt("totalParabolicMicrophoneAmount", 0);
		PlayerPrefs.SetInt("totalGlowstickAmount", 0);
		PlayerPrefs.SetInt("totalGHeadMountedCameraAmount", 0);
	}

	// Token: 0x04000A63 RID: 2659
	private PhotonView view;

	// Token: 0x04000A64 RID: 2660
	public List<InventoryItem> items = new List<InventoryItem>();

	// Token: 0x04000A65 RID: 2661
	[SerializeField]
	private RectTransform[] maskTransforms;

	// Token: 0x04000A66 RID: 2662
	[SerializeField]
	private Text inventoryText;

	// Token: 0x04000A67 RID: 2663
	[SerializeField]
	private GamepadUISelector serverSelector;

	// Token: 0x04000A68 RID: 2664
	[SerializeField]
	private GamepadUISelector inventorySelector;
}
