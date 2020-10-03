using System;
using System.Collections.Generic;
using Photon;
using UnityEngine;

// Token: 0x020000CC RID: 204
[RequireComponent(typeof(PhotonView))]
public class InRoomChat : Photon.MonoBehaviour
{
	// Token: 0x060005BC RID: 1468 RVA: 0x00020967 File Offset: 0x0001EB67
	public void Start()
	{
		if (this.AlignBottom)
		{
			this.GuiRect.y = (float)Screen.height - this.GuiRect.height;
		}
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x00020990 File Offset: 0x0001EB90
	public void OnGUI()
	{
		if (!this.IsVisible || !PhotonNetwork.inRoom)
		{
			return;
		}
		if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return))
		{
			if (!string.IsNullOrEmpty(this.inputLine))
			{
				base.photonView.RPC("Chat", PhotonTargets.All, new object[]
				{
					this.inputLine
				});
				this.inputLine = "";
				GUI.FocusControl("");
				return;
			}
			GUI.FocusControl("ChatInput");
		}
		GUI.SetNextControlName("");
		GUILayout.BeginArea(this.GuiRect);
		this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, Array.Empty<GUILayoutOption>());
		GUILayout.FlexibleSpace();
		for (int i = this.messages.Count - 1; i >= 0; i--)
		{
			GUILayout.Label(this.messages[i], Array.Empty<GUILayoutOption>());
		}
		GUILayout.EndScrollView();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUI.SetNextControlName("ChatInput");
		this.inputLine = GUILayout.TextField(this.inputLine, Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Send", new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(false)
		}))
		{
			base.photonView.RPC("Chat", PhotonTargets.All, new object[]
			{
				this.inputLine
			});
			this.inputLine = "";
			GUI.FocusControl("");
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x00020B10 File Offset: 0x0001ED10
	[PunRPC]
	public void Chat(string newLine, PhotonMessageInfo mi)
	{
		string str = "anonymous";
		if (mi.sender != null)
		{
			if (!string.IsNullOrEmpty(mi.sender.NickName))
			{
				str = mi.sender.NickName;
			}
			else
			{
				str = "player " + mi.sender.ID;
			}
		}
		this.messages.Add(str + ": " + newLine);
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x00020B7D File Offset: 0x0001ED7D
	public void AddLine(string newLine)
	{
		this.messages.Add(newLine);
	}

	// Token: 0x040005D7 RID: 1495
	public Rect GuiRect = new Rect(0f, 0f, 250f, 300f);

	// Token: 0x040005D8 RID: 1496
	public bool IsVisible = true;

	// Token: 0x040005D9 RID: 1497
	public bool AlignBottom;

	// Token: 0x040005DA RID: 1498
	public List<string> messages = new List<string>();

	// Token: 0x040005DB RID: 1499
	private string inputLine = "";

	// Token: 0x040005DC RID: 1500
	private Vector2 scrollPos = Vector2.zero;

	// Token: 0x040005DD RID: 1501
	public static readonly string ChatRPC = "Chat";
}
