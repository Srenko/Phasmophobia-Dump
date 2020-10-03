using System;
using Photon;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class OnDoubleclickDestroy : Photon.MonoBehaviour
{
	// Token: 0x06000172 RID: 370 RVA: 0x0000A658 File Offset: 0x00008858
	private void OnClick()
	{
		if (!base.photonView.isMine)
		{
			return;
		}
		if (Time.time - this.timeOfLastClick < 0.2f)
		{
			PhotonNetwork.Destroy(base.gameObject);
			return;
		}
		this.timeOfLastClick = Time.time;
	}

	// Token: 0x040001AD RID: 429
	private float timeOfLastClick;

	// Token: 0x040001AE RID: 430
	private const float ClickDeltaForDoubleclick = 0.2f;
}
