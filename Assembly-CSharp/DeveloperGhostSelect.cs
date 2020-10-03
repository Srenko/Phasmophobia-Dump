using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017C RID: 380
public class DeveloperGhostSelect : MonoBehaviour
{
	// Token: 0x06000A0C RID: 2572 RVA: 0x0003E228 File Offset: 0x0003C428
	private void Start()
	{
		if (this.storeSDKManager.storeBranchType == StoreSDKManager.StoreBranchType.developer)
		{
			this.GhostTypeText.gameObject.SetActive(true);
		}
		else
		{
			this.GhostTypeText.gameObject.SetActive(false);
		}
		this.GhostTypeText.text = this.ghostType.ToString();
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0003E284 File Offset: 0x0003C484
	public void ChangeButton(int amount)
	{
		this.id += amount;
		this.id = Mathf.Clamp(this.id, 0, 12);
		this.ghostType = (GhostTraits.Type)this.id;
		this.GhostTypeText.text = this.ghostType.ToString();
		PlayerPrefs.SetInt("Developer_GhostType", this.id);
	}

	// Token: 0x04000A35 RID: 2613
	[SerializeField]
	private StoreSDKManager storeSDKManager;

	// Token: 0x04000A36 RID: 2614
	[SerializeField]
	private Text GhostTypeText;

	// Token: 0x04000A37 RID: 2615
	private int id;

	// Token: 0x04000A38 RID: 2616
	public GhostTraits.Type ghostType;
}
