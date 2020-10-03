using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003F RID: 63
public class DemoBoxesGui : MonoBehaviour
{
	// Token: 0x06000169 RID: 361 RVA: 0x0000A448 File Offset: 0x00008648
	private void Update()
	{
		if (this.GuiTextForTips == null)
		{
			return;
		}
		this.timeSinceLastTip += Time.deltaTime;
		if (this.timeSinceLastTip > 3f)
		{
			this.timeSinceLastTip = 0f;
			base.StartCoroutine("SwapTip");
		}
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0000A49A File Offset: 0x0000869A
	public IEnumerator SwapTip()
	{
		float alpha = 1f;
		while (alpha > 0f)
		{
			alpha -= 0.05f;
			this.timeSinceLastTip = 0f;
			this.GuiTextForTips.color = new Color(this.GuiTextForTips.color.r, this.GuiTextForTips.color.r, this.GuiTextForTips.color.r, alpha);
			yield return null;
		}
		this.tipsIndex = (this.tipsIndex + 1) % this.tips.Length;
		this.GuiTextForTips.text = this.tips[this.tipsIndex];
		while (alpha < 1f)
		{
			alpha += 0.05f;
			this.timeSinceLastTip = 0f;
			this.GuiTextForTips.color = new Color(this.GuiTextForTips.color.r, this.GuiTextForTips.color.r, this.GuiTextForTips.color.r, alpha);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x0000A4AC File Offset: 0x000086AC
	private void OnGUI()
	{
		if (this.HideUI)
		{
			return;
		}
		GUILayout.BeginArea(new Rect(0f, 0f, 300f, (float)Screen.height));
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		if (!PhotonNetwork.connected)
		{
			if (GUILayout.Button("Connect", new GUILayoutOption[]
			{
				GUILayout.Width(100f)
			}))
			{
				PhotonNetwork.ConnectUsingSettings(null);
			}
		}
		else if (GUILayout.Button("Disconnect", new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		}))
		{
			PhotonNetwork.Disconnect();
		}
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString(), Array.Empty<GUILayoutOption>());
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	// Token: 0x040001A3 RID: 419
	public bool HideUI;

	// Token: 0x040001A4 RID: 420
	public Text GuiTextForTips;

	// Token: 0x040001A5 RID: 421
	private int tipsIndex;

	// Token: 0x040001A6 RID: 422
	private readonly string[] tips = new string[]
	{
		"Click planes to instantiate boxes.",
		"Click a box to send an RPC. This will flash the box.",
		"Double click a box to destroy it. If it's yours.",
		"Boxes send ~10 updates per second when moving.",
		"Movement is not smoothed at all. It shows the updates 1:1.",
		"The script ColorPerPlayer assigns a color per player.",
		"When players leave, their boxes get destroyed. That's called clean up.",
		"Scene Objects are not cleaned up. The Master Client can Instantiate them.",
		"Scene Objects are not colored. They are controlled by the Master Client.",
		"The elevated planes instantiate Scene Objects. Those don't get cleaned up.",
		"Are you still reading?"
	};

	// Token: 0x040001A7 RID: 423
	private const float TimePerTip = 3f;

	// Token: 0x040001A8 RID: 424
	private float timeSinceLastTip;

	// Token: 0x040001A9 RID: 425
	private const float FadeSpeedForTip = 0.05f;
}
