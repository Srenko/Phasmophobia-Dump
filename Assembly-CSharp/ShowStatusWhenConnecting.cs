using System;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class ShowStatusWhenConnecting : MonoBehaviour
{
	// Token: 0x06000629 RID: 1577 RVA: 0x00022740 File Offset: 0x00020940
	private void OnGUI()
	{
		if (this.Skin != null)
		{
			GUI.skin = this.Skin;
		}
		float num = 400f;
		float num2 = 100f;
		GUILayout.BeginArea(new Rect(((float)Screen.width - num) / 2f, ((float)Screen.height - num2) / 2f, num, num2), GUI.skin.box);
		GUILayout.Label("Connecting" + this.GetConnectingDots(), GUI.skin.customStyles[0], Array.Empty<GUILayoutOption>());
		GUILayout.Label("Status: " + PhotonNetwork.connectionStateDetailed, Array.Empty<GUILayoutOption>());
		GUILayout.EndArea();
		if (PhotonNetwork.inRoom)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x000227FC File Offset: 0x000209FC
	private string GetConnectingDots()
	{
		string text = "";
		int num = Mathf.FloorToInt(Time.timeSinceLevelLoad * 3f % 4f);
		for (int i = 0; i < num; i++)
		{
			text += " .";
		}
		return text;
	}

	// Token: 0x04000616 RID: 1558
	public GUISkin Skin;
}
