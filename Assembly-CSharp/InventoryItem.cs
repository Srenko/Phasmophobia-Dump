using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000181 RID: 385
public class InventoryItem : MonoBehaviour
{
	// Token: 0x06000A36 RID: 2614 RVA: 0x0003F02C File Offset: 0x0003D22C
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		PlayerPrefs.SetInt("current" + this.itemName + "Amount", 0);
		PlayerPrefs.SetInt("total" + this.itemName + "Amount", 0);
		for (int i = 0; i < 4; i++)
		{
			this.players.Add(new InventoryItem.PlayerInventoryItem());
		}
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0003F098 File Offset: 0x0003D298
	private void Start()
	{
		if (this.defaultAmount > 0)
		{
			this.ChangeTotalAmountNetworked(999, this.defaultAmount);
		}
		this.view.RPC("AddPlayer", PhotonTargets.AllBuffered, new object[]
		{
			PhotonNetwork.player.ID
		});
		this.UpdateTotalText();
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x0003F0F0 File Offset: 0x0003D2F0
	[PunRPC]
	private void AddPlayer(int id)
	{
		int i = 0;
		while (i < this.players.Count)
		{
			if (!this.players[i].isAssigned)
			{
				this.players[i].isAssigned = true;
				this.players[i].actorID = id;
				this.players[i].currentAmount = 0;
				if (id == PhotonNetwork.player.ID)
				{
					this.players[i].isLocalPlayer = true;
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

	// Token: 0x06000A39 RID: 2617 RVA: 0x0003F17C File Offset: 0x0003D37C
	[PunRPC]
	private void RemovePlayer(int id)
	{
		for (int i = 0; i < this.players.Count; i++)
		{
			if (this.players[i].actorID == id)
			{
				this.ChangeTotalAmount(id, -this.players[i].currentAmount);
				this.players[i].isAssigned = false;
				this.players[i].actorID = 0;
				this.players[i].currentAmount = 0;
				return;
			}
		}
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x0003F204 File Offset: 0x0003D404
	public void UpdateTotalText()
	{
		for (int i = 0; i < this.players.Count; i++)
		{
			if (this.players[i].isLocalPlayer)
			{
				this.totalText.text = (FileBasedPrefs.GetInt(this.itemName + "Inventory", 0) - this.players[i].currentAmount).ToString();
				return;
			}
		}
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x0003F276 File Offset: 0x0003D476
	public void ChangeTotalAmount(int actorID, int amount)
	{
		this.view.RPC("ChangeTotalAmountNetworked", PhotonTargets.AllBufferedViaServer, new object[]
		{
			actorID,
			amount
		});
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x0003F2A4 File Offset: 0x0003D4A4
	[PunRPC]
	private void ChangeTotalAmountNetworked(int actorID, int amount)
	{
		if (actorID != 999)
		{
			if (amount == 1 && this.totalAmount >= this.maxAmount)
			{
				this.totalAmount = this.maxAmount;
				this.canChangeAmount = true;
				this.inventoryManager.UpdateText();
				return;
			}
			for (int i = 0; i < this.players.Count; i++)
			{
				if (this.players[i].actorID == actorID)
				{
					this.players[i].currentAmount += amount;
				}
			}
		}
		this.totalAmount += amount;
		this.totalAmount = Mathf.Clamp(this.totalAmount, this.defaultAmount, this.maxAmount);
		this.UpdateTotalText();
		this.canChangeAmount = true;
		this.inventoryManager.UpdateText();
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x0003F370 File Offset: 0x0003D570
	public void SaveItem()
	{
		int i = 0;
		while (i < this.players.Count)
		{
			if (this.players[i].isLocalPlayer)
			{
				PlayerPrefs.SetInt("current" + this.itemName + "Amount", this.players[i].currentAmount);
				if (this.players[i].currentAmount > 0)
				{
					PlayerPrefs.SetInt("totalItemCost", PlayerPrefs.GetInt("totalItemCost") + this.itemCost * this.players[i].currentAmount);
					break;
				}
				break;
			}
			else
			{
				i++;
			}
		}
		PlayerPrefs.SetInt("total" + this.itemName + "Amount", this.totalAmount);
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0003F43C File Offset: 0x0003D63C
	public void LeftRoom(int actorID)
	{
		for (int i = 0; i < this.players.Count; i++)
		{
			if (this.players[i].actorID == actorID)
			{
				this.view.RPC("RemovePlayer", PhotonTargets.AllBuffered, new object[]
				{
					actorID
				});
			}
		}
	}

	// Token: 0x04000A58 RID: 2648
	public string itemName;

	// Token: 0x04000A59 RID: 2649
	public string localisedItemName;

	// Token: 0x04000A5A RID: 2650
	public int itemCost;

	// Token: 0x04000A5B RID: 2651
	[SerializeField]
	private int defaultAmount;

	// Token: 0x04000A5C RID: 2652
	[HideInInspector]
	public int totalAmount;

	// Token: 0x04000A5D RID: 2653
	public int maxAmount;

	// Token: 0x04000A5E RID: 2654
	[SerializeField]
	private Text totalText;

	// Token: 0x04000A5F RID: 2655
	[HideInInspector]
	public PhotonView view;

	// Token: 0x04000A60 RID: 2656
	[HideInInspector]
	public bool canChangeAmount = true;

	// Token: 0x04000A61 RID: 2657
	[SerializeField]
	private InventoryManager inventoryManager;

	// Token: 0x04000A62 RID: 2658
	public List<InventoryItem.PlayerInventoryItem> players = new List<InventoryItem.PlayerInventoryItem>();

	// Token: 0x02000546 RID: 1350
	public class PlayerInventoryItem
	{
		// Token: 0x04002549 RID: 9545
		public bool isAssigned;

		// Token: 0x0400254A RID: 9546
		public int actorID;

		// Token: 0x0400254B RID: 9547
		public int currentAmount;

		// Token: 0x0400254C RID: 9548
		public bool isLocalPlayer;
	}
}
