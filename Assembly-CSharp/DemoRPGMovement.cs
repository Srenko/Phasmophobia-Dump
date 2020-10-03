using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class DemoRPGMovement : MonoBehaviour
{
	// Token: 0x0600020A RID: 522 RVA: 0x0000E6CE File Offset: 0x0000C8CE
	private void OnJoinedRoom()
	{
		this.CreatePlayerObject();
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000E6D8 File Offset: 0x0000C8D8
	private void CreatePlayerObject()
	{
		Vector3 position = new Vector3(33.5f, 1.5f, 20.5f);
		GameObject gameObject = PhotonNetwork.Instantiate("Robot Kyle RPG", position, Quaternion.identity, 0);
		this.Camera.Target = gameObject.transform;
	}

	// Token: 0x0400024B RID: 587
	public RPGCamera Camera;
}
