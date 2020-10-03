using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000066 RID: 102
public class RpsDebug : MonoBehaviour
{
	// Token: 0x0600023D RID: 573 RVA: 0x0000F45D File Offset: 0x0000D65D
	public void ToggleConnectionDebug()
	{
		this.ShowConnectionDebug = !this.ShowConnectionDebug;
	}

	// Token: 0x0600023E RID: 574 RVA: 0x0000F470 File Offset: 0x0000D670
	public void Update()
	{
		if (this.ShowConnectionDebug)
		{
			this.ConnectionDebugButton.GetComponentInChildren<Text>().text = PhotonNetwork.connectionStateDetailed.ToString();
			return;
		}
		this.ConnectionDebugButton.GetComponentInChildren<Text>().text = "";
	}

	// Token: 0x04000279 RID: 633
	[SerializeField]
	private Button ConnectionDebugButton;

	// Token: 0x0400027A RID: 634
	public bool ShowConnectionDebug;
}
