using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200004C RID: 76
public class ChannelSelector : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x0600018C RID: 396 RVA: 0x0000AEE1 File Offset: 0x000090E1
	public void SetChannel(string channel)
	{
		this.Channel = channel;
		base.GetComponentInChildren<Text>().text = this.Channel;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000AEFB File Offset: 0x000090FB
	public void OnPointerClick(PointerEventData eventData)
	{
		Object.FindObjectOfType<ChatGui>().ShowChannel(this.Channel);
	}

	// Token: 0x040001C1 RID: 449
	public string Channel;
}
