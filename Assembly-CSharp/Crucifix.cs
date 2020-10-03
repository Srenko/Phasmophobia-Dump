using System;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class Crucifix : MonoBehaviour
{
	// Token: 0x060008FE RID: 2302 RVA: 0x00035F56 File Offset: 0x00034156
	private void Start()
	{
		if (LevelController.instance)
		{
			LevelController.instance.crucifix.Add(this);
		}
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00035F74 File Offset: 0x00034174
	public void Used()
	{
		this.view.RPC("NetworkedUsed", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00035F8C File Offset: 0x0003418C
	[PunRPC]
	public void NetworkedUsed()
	{
		this.usesCount++;
		if (this.usesCount > 1)
		{
			LevelController.instance.crucifix.Remove(this);
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000910 RID: 2320
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000911 RID: 2321
	private int usesCount;
}
