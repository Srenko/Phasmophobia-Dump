using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class InstantiateCube : MonoBehaviour
{
	// Token: 0x0600017D RID: 381 RVA: 0x0000AAF4 File Offset: 0x00008CF4
	private void OnClick()
	{
		if (PhotonNetwork.connectionStateDetailed != ClientState.Joined)
		{
			return;
		}
		int instantiateType = this.InstantiateType;
		if (instantiateType == 0)
		{
			PhotonNetwork.Instantiate(this.Prefab.name, base.transform.position + 3f * Vector3.up, Quaternion.identity, 0);
			return;
		}
		if (instantiateType != 1)
		{
			return;
		}
		PhotonNetwork.InstantiateSceneObject(this.Prefab.name, InputToEvent.inputHitPos + new Vector3(0f, 5f, 0f), Quaternion.identity, 0, null);
	}

	// Token: 0x040001B7 RID: 439
	public GameObject Prefab;

	// Token: 0x040001B8 RID: 440
	public int InstantiateType;

	// Token: 0x040001B9 RID: 441
	public bool showGui;
}
