using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000180 RID: 384
public class HoopCounter : MonoBehaviour
{
	// Token: 0x06000A33 RID: 2611 RVA: 0x0003EF89 File Offset: 0x0003D189
	private void Start()
	{
		this.counter = PlayerPrefs.GetInt("HoopCounter");
		this.counterText.text = this.counter.ToString();
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x0003EFB4 File Offset: 0x0003D1B4
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Ball"))
		{
			if (other.GetComponent<PhotonObjectInteract>().isGrabbed)
			{
				return;
			}
			if (!other.GetComponent<PhotonObjectInteract>().view.isMine && PhotonNetwork.inRoom)
			{
				return;
			}
			this.counter++;
			this.counterText.text = this.counter.ToString();
			PlayerPrefs.SetInt("HoopCounter", this.counter);
		}
	}

	// Token: 0x04000A56 RID: 2646
	private int counter;

	// Token: 0x04000A57 RID: 2647
	[SerializeField]
	private Text counterText;
}
