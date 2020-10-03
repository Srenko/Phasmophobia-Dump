using System;
using ExitGames.UtilityScripts;
using Photon;
using UnityEngine;

// Token: 0x02000047 RID: 71
[RequireComponent(typeof(PhotonView))]
public class MaterialPerOwner : Photon.MonoBehaviour
{
	// Token: 0x0600017F RID: 383 RVA: 0x0000AB87 File Offset: 0x00008D87
	private void Start()
	{
		this.m_Renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000AB98 File Offset: 0x00008D98
	private void Update()
	{
		if (base.photonView.ownerId != this.assignedColorForUserId)
		{
			int num = Array.IndexOf<int>(PlayerRoomIndexing.instance.PlayerIds, base.photonView.ownerId);
			try
			{
				this.m_Renderer.material.color = Object.FindObjectOfType<ColorPerPlayer>().Colors[num];
				this.assignedColorForUserId = base.photonView.ownerId;
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x040001BA RID: 442
	private int assignedColorForUserId;

	// Token: 0x040001BB RID: 443
	private Renderer m_Renderer;
}
