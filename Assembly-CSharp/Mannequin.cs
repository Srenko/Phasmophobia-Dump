using System;
using UnityEngine;

// Token: 0x02000149 RID: 329
[RequireComponent(typeof(PhotonView))]
public class Mannequin : MonoBehaviour
{
	// Token: 0x060008AC RID: 2220 RVA: 0x0003472F File Offset: 0x0003292F
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x00034740 File Offset: 0x00032940
	public void Teleport()
	{
		int num = Random.Range(0, LevelController.instance.MannequinTeleportSpots.Length);
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i] != null && !Physics.Linecast(base.transform.position + Vector3.up, GameController.instance.playersData[i].player.headObject.transform.position, this.mask, QueryTriggerInteraction.Ignore))
			{
				flag = true;
				break;
			}
		}
		for (int j = 0; j < GameController.instance.playersData.Count; j++)
		{
			if (GameController.instance.playersData[j] != null && !Physics.Linecast(LevelController.instance.MannequinTeleportSpots[num].position + Vector3.up, GameController.instance.playersData[j].player.headObject.transform.position, this.mask, QueryTriggerInteraction.Ignore))
			{
				flag2 = true;
				break;
			}
		}
		if (!flag && !flag2 && LevelController.instance.MannequinTeleportSpots[num].childCount == 0)
		{
			this.view.RPC("TeleportNetworked", PhotonTargets.All, new object[]
			{
				num
			});
		}
		this.Rotate();
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x000348AC File Offset: 0x00032AAC
	public void Rotate()
	{
		this.view.RPC("RotateNetworked", PhotonTargets.All, new object[]
		{
			Vector3.up * (float)Random.Range(0, 360)
		});
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x000348EE File Offset: 0x00032AEE
	[PunRPC]
	private void TeleportNetworked(int id)
	{
		base.transform.position = LevelController.instance.MannequinTeleportSpots[id].position;
		base.transform.SetParent(LevelController.instance.MannequinTeleportSpots[id]);
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x00034923 File Offset: 0x00032B23
	[PunRPC]
	private void RotateNetworked(Vector3 rot)
	{
		base.transform.Rotate(rot);
	}

	// Token: 0x040008C9 RID: 2249
	private PhotonView view;

	// Token: 0x040008CA RID: 2250
	[SerializeField]
	private LayerMask mask;
}
