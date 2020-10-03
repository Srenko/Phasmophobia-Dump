using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000190 RID: 400
public class ServerListItem : MonoBehaviour
{
	// Token: 0x06000AC3 RID: 2755 RVA: 0x000432E0 File Offset: 0x000414E0
	private void Awake()
	{
		this.lobbyManager = Object.FindObjectOfType<LobbyManager>();
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x000432F0 File Offset: 0x000414F0
	public void SetUI(string name, string population, RoomInfo info)
	{
		int num = name.LastIndexOf("#");
		if (num > 0)
		{
			name = name.Substring(0, num);
		}
		this.serverName.text = "Server: " + name;
		this.serverPopulation.text = population + "/4";
		this.myRoomInfo = info;
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x0004334A File Offset: 0x0004154A
	public void Clicked()
	{
		if (!this.myRoomInfo.IsOpen || !this.myRoomInfo.IsVisible)
		{
			this.lobbyManager.RefreshList();
			return;
		}
		this.lobbyManager.JoinServer(this.myRoomInfo);
	}

	// Token: 0x04000B0B RID: 2827
	public Text serverName;

	// Token: 0x04000B0C RID: 2828
	public Text serverPopulation;

	// Token: 0x04000B0D RID: 2829
	private LobbyManager lobbyManager;

	// Token: 0x04000B0E RID: 2830
	private RoomInfo myRoomInfo;
}
