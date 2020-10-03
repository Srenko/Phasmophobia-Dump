using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class OnPickedUpScript : MonoBehaviour
{
	// Token: 0x060001E7 RID: 487 RVA: 0x0000D289 File Offset: 0x0000B489
	public void OnPickedUp(PickupItem item)
	{
		if (item.PickupIsMine)
		{
			Debug.Log("I picked up something. That's a score!");
			PhotonNetwork.player.AddScore(1);
			return;
		}
		Debug.Log("Someone else picked up something. Lucky!");
	}
}
