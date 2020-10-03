using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class OnClickRightDestroy : MonoBehaviour
{
	// Token: 0x06000187 RID: 391 RVA: 0x0000AD10 File Offset: 0x00008F10
	public void OnPressRight()
	{
		Debug.Log("RightClick Destroy");
		PhotonNetwork.Destroy(base.gameObject);
	}
}
