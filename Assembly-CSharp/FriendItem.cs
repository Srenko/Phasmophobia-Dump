using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200004F RID: 79
public class FriendItem : MonoBehaviour
{
	// Token: 0x1700002A RID: 42
	// (get) Token: 0x060001AF RID: 431 RVA: 0x0000BAFA File Offset: 0x00009CFA
	// (set) Token: 0x060001AE RID: 430 RVA: 0x0000BAEC File Offset: 0x00009CEC
	[HideInInspector]
	public string FriendId
	{
		get
		{
			return this.NameLabel.text;
		}
		set
		{
			this.NameLabel.text = value;
		}
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x0000BB07 File Offset: 0x00009D07
	public void Awake()
	{
		this.Health.text = string.Empty;
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0000BB1C File Offset: 0x00009D1C
	public void OnFriendStatusUpdate(int status, bool gotMessage, object message)
	{
		string text;
		switch (status)
		{
		case 1:
			text = "Invisible";
			break;
		case 2:
			text = "Online";
			break;
		case 3:
			text = "Away";
			break;
		case 4:
			text = "Do not disturb";
			break;
		case 5:
			text = "Looking For Game/Group";
			break;
		case 6:
			text = "Playing";
			break;
		default:
			text = "Offline";
			break;
		}
		this.StatusLabel.text = text;
		if (gotMessage)
		{
			string text2 = string.Empty;
			if (message != null)
			{
				string[] array = message as string[];
				if (array != null && array.Length >= 2)
				{
					text2 = array[1] + "%";
				}
			}
			this.Health.text = text2;
		}
	}

	// Token: 0x040001DA RID: 474
	public Text NameLabel;

	// Token: 0x040001DB RID: 475
	public Text StatusLabel;

	// Token: 0x040001DC RID: 476
	public Text Health;
}
