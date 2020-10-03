using System;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class Demo2DJumpAndRun : MonoBehaviour
{
	// Token: 0x06000149 RID: 329 RVA: 0x00009DAC File Offset: 0x00007FAC
	private void OnJoinedRoom()
	{
		if (!PhotonNetwork.isMasterClient)
		{
			return;
		}
		PhotonNetwork.InstantiateSceneObject("Physics Box", new Vector3(-4.5f, 5.5f, 0f), Quaternion.identity, 0, null);
		PhotonNetwork.InstantiateSceneObject("Physics Box", new Vector3(-4.5f, 4.5f, 0f), Quaternion.identity, 0, null);
		PhotonNetwork.InstantiateSceneObject("Physics Box", new Vector3(-4.5f, 3.5f, 0f), Quaternion.identity, 0, null);
		PhotonNetwork.InstantiateSceneObject("Physics Box", new Vector3(4.5f, 5.5f, 0f), Quaternion.identity, 0, null);
		PhotonNetwork.InstantiateSceneObject("Physics Box", new Vector3(4.5f, 4.5f, 0f), Quaternion.identity, 0, null);
		PhotonNetwork.InstantiateSceneObject("Physics Box", new Vector3(4.5f, 3.5f, 0f), Quaternion.identity, 0, null);
	}
}
