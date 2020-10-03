using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class IELdemo : MonoBehaviour
{
	// Token: 0x06000259 RID: 601 RVA: 0x0000FDB8 File Offset: 0x0000DFB8
	public void OnGUI()
	{
		if (this.Skin != null)
		{
			GUI.skin = this.Skin;
		}
		if (PhotonNetwork.isMasterClient)
		{
			GUILayout.Label("Controlling client.\nPing: " + PhotonNetwork.GetPing(), Array.Empty<GUILayoutOption>());
			if (GUILayout.Button("disconnect", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			}))
			{
				PhotonNetwork.Disconnect();
			}
		}
		else if (PhotonNetwork.isNonMasterClientInRoom)
		{
			GUILayout.Label("Receiving updates.\nPing: " + PhotonNetwork.GetPing(), Array.Empty<GUILayoutOption>());
			if (GUILayout.Button("disconnect", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			}))
			{
				PhotonNetwork.Disconnect();
			}
		}
		else
		{
			GUILayout.Label("Not in room yet\n" + PhotonNetwork.connectionStateDetailed, Array.Empty<GUILayoutOption>());
		}
		if (!PhotonNetwork.connected && !PhotonNetwork.connecting && GUILayout.Button("connect", new GUILayoutOption[]
		{
			GUILayout.Width(80f)
		}))
		{
			PhotonNetwork.ConnectUsingSettings(null);
		}
	}

	// Token: 0x04000293 RID: 659
	public GUISkin Skin;
}
