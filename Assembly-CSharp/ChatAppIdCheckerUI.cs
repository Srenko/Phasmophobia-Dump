using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200004D RID: 77
[ExecuteInEditMode]
public class ChatAppIdCheckerUI : MonoBehaviour
{
	// Token: 0x0600018F RID: 399 RVA: 0x0000AF0D File Offset: 0x0000910D
	public void Update()
	{
		if (string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.ChatAppID))
		{
			this.Description.text = "<Color=Red>WARNING:</Color>\nTo run this demo, please set the Chat AppId in the PhotonServerSettings file.";
			return;
		}
		this.Description.text = string.Empty;
	}

	// Token: 0x040001C2 RID: 450
	public Text Description;
}
