using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000051 RID: 81
[RequireComponent(typeof(ChatGui))]
public class NamePickGui : MonoBehaviour
{
	// Token: 0x060001B5 RID: 437 RVA: 0x0000BBD0 File Offset: 0x00009DD0
	public void Start()
	{
		this.chatNewComponent = Object.FindObjectOfType<ChatGui>();
		string @string = PlayerPrefs.GetString("NamePickUserName");
		if (!string.IsNullOrEmpty(@string))
		{
			this.idInput.text = @string;
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000BC07 File Offset: 0x00009E07
	public void EndEditOnEnter()
	{
		if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
		{
			this.StartChat();
		}
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000BC24 File Offset: 0x00009E24
	public void StartChat()
	{
		ChatGui chatGui = Object.FindObjectOfType<ChatGui>();
		chatGui.UserName = this.idInput.text.Trim();
		chatGui.Connect();
		base.enabled = false;
		PlayerPrefs.SetString("NamePickUserName", chatGui.UserName);
	}

	// Token: 0x040001DD RID: 477
	private const string UserNamePlayerPref = "NamePickUserName";

	// Token: 0x040001DE RID: 478
	public ChatGui chatNewComponent;

	// Token: 0x040001DF RID: 479
	public InputField idInput;
}
