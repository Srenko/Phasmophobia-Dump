using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017D RID: 381
public class ErrorManager : MonoBehaviour
{
	// Token: 0x06000A0F RID: 2575 RVA: 0x0003E2EB File Offset: 0x0003C4EB
	private void Start()
	{
		this.ErrorScreenText.text = PlayerPrefs.GetString("ErrorMessage");
		PlayerPrefs.SetString("ErrorMessage", string.Empty);
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0003E311 File Offset: 0x0003C511
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

	// Token: 0x04000A39 RID: 2617
	[SerializeField]
	private Text ErrorScreenText;

	// Token: 0x04000A3A RID: 2618
	[SerializeField]
	private GameObject mainObject;
}
