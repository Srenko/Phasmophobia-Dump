using System;
using Photon;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018B RID: 395
public class PasswordManager : Photon.MonoBehaviour
{
	// Token: 0x06000AAE RID: 2734 RVA: 0x00042734 File Offset: 0x00040934
	private void OnEnable()
	{
		this.hasEnteredCode = false;
		this.password = "";
		this.lobbyManager.RefreshList();
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x00042754 File Offset: 0x00040954
	public void NumberButton(int number)
	{
		if (!this.hasEnteredCode)
		{
			this.codeText.text = "";
			this.hasEnteredCode = true;
		}
		if (this.password.Length < 6)
		{
			this.password += number;
		}
		this.codeText.text = this.password;
		this.errorText.text = "";
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x000427C6 File Offset: 0x000409C6
	public void OnPhotonJoinRoomFailed(object[] codeAndMsg)
	{
		if (codeAndMsg[1].ToString() == "Game does not exist")
		{
			this.errorText.text = "Server version mismatch";
			return;
		}
		this.errorText.text = codeAndMsg[1].ToString();
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x00042800 File Offset: 0x00040A00
	public void EnterButton()
	{
		this.lobbyManager.JoinServerByName(this.password);
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x00042814 File Offset: 0x00040A14
	public void RemoveButton()
	{
		this.errorText.text = "";
		if (this.password.Length > 0 && this.hasEnteredCode)
		{
			this.password = this.password.Remove(this.password.Length - 1, 1);
			this.codeText.text = this.password;
		}
	}

	// Token: 0x04000AE3 RID: 2787
	[SerializeField]
	private Text codeText;

	// Token: 0x04000AE4 RID: 2788
	private bool hasEnteredCode;

	// Token: 0x04000AE5 RID: 2789
	private string password;

	// Token: 0x04000AE6 RID: 2790
	[SerializeField]
	private LobbyManager lobbyManager;

	// Token: 0x04000AE7 RID: 2791
	[SerializeField]
	private Text errorText;

	// Token: 0x04000AE8 RID: 2792
	private RoomInfo[] rooms;
}
