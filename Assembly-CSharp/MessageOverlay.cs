using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class MessageOverlay : MonoBehaviour
{
	// Token: 0x060001E0 RID: 480 RVA: 0x0000D215 File Offset: 0x0000B415
	public void Start()
	{
		this.SetActive(true);
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000D21E File Offset: 0x0000B41E
	public void OnJoinedRoom()
	{
		this.SetActive(false);
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000D215 File Offset: 0x0000B415
	public void OnLeftRoom()
	{
		this.SetActive(true);
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000D228 File Offset: 0x0000B428
	private void SetActive(bool enable)
	{
		GameObject[] objects = this.Objects;
		for (int i = 0; i < objects.Length; i++)
		{
			objects[i].SetActive(enable);
		}
	}

	// Token: 0x040001FF RID: 511
	public GameObject[] Objects;
}
