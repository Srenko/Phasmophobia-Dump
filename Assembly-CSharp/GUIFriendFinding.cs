using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class GUIFriendFinding : MonoBehaviour
{
	// Token: 0x060001C4 RID: 452 RVA: 0x0000C040 File Offset: 0x0000A240
	private void Start()
	{
		PhotonNetwork.playerName = "usr" + Random.Range(0, 9);
		this.friendListOfSomeCommunity = GUIFriendFinding.FetchFriendsFromCommunity();
		this.GuiRect = new Rect((float)(Screen.width / 4), 80f, (float)(Screen.width / 2), (float)(Screen.height - 100));
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
	public static string[] FetchFriendsFromCommunity()
	{
		string[] array = new string[9];
		int num = 0;
		for (int i = 0; i < array.Length; i++)
		{
			string text = "usr" + num++;
			if (text.Equals(PhotonNetwork.playerName))
			{
				text = "usr" + num++;
			}
			array[i] = text;
		}
		return array;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000C101 File Offset: 0x0000A301
	public void OnUpdatedFriendList()
	{
		Debug.Log("OnUpdatedFriendList is called when the list PhotonNetwork.Friends is refreshed.");
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000C110 File Offset: 0x0000A310
	public void OnGUI()
	{
		if (!PhotonNetwork.connectedAndReady || PhotonNetwork.Server != ServerConnection.MasterServer)
		{
			return;
		}
		GUILayout.BeginArea(this.GuiRect);
		GUILayout.Label("Your (random) name: " + PhotonNetwork.playerName, Array.Empty<GUILayoutOption>());
		GUILayout.Label("Your friends: " + string.Join(", ", this.friendListOfSomeCommunity), Array.Empty<GUILayoutOption>());
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Find Friends", Array.Empty<GUILayoutOption>()))
		{
			PhotonNetwork.FindFriends(this.friendListOfSomeCommunity, null);
		}
		if (GUILayout.Button("Create Room", Array.Empty<GUILayoutOption>()))
		{
			PhotonNetwork.CreateRoom(null);
		}
		GUILayout.EndHorizontal();
		if (PhotonNetwork.Friends != null)
		{
			foreach (FriendInfo friendInfo in PhotonNetwork.Friends)
			{
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Label(friendInfo.ToString(), Array.Empty<GUILayoutOption>());
				if (friendInfo.IsInRoom && GUILayout.Button("join", Array.Empty<GUILayoutOption>()))
				{
					PhotonNetwork.JoinRoom(friendInfo.Room);
				}
				GUILayout.EndHorizontal();
			}
		}
		GUILayout.EndArea();
	}

	// Token: 0x040001E6 RID: 486
	private string[] friendListOfSomeCommunity;

	// Token: 0x040001E7 RID: 487
	public Rect GuiRect;

	// Token: 0x040001E8 RID: 488
	private string ExpectedUsers;
}
