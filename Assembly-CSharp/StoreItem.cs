using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000194 RID: 404
public class StoreItem : MonoBehaviour
{
	// Token: 0x06000AEA RID: 2794 RVA: 0x0004456F File Offset: 0x0004276F
	private void Start()
	{
		this.costText.text = "$" + this.cost.ToString();
	}

	// Token: 0x04000B3B RID: 2875
	public int cost;

	// Token: 0x04000B3C RID: 2876
	public GameObject description;

	// Token: 0x04000B3D RID: 2877
	public Text costText;

	// Token: 0x04000B3E RID: 2878
	public Text amountOwnedText;

	// Token: 0x04000B3F RID: 2879
	public string itemName;

	// Token: 0x04000B40 RID: 2880
	public int requiredLevel;

	// Token: 0x04000B41 RID: 2881
	public Button buyButton;

	// Token: 0x04000B42 RID: 2882
	public Text buyButtonText;
}
