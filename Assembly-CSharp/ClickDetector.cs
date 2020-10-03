using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class ClickDetector : MonoBehaviour
{
	// Token: 0x0600029A RID: 666 RVA: 0x000117F0 File Offset: 0x0000F9F0
	public void Update()
	{
		if (PhotonNetwork.player.ID != GameLogic.playerWhoIsIt)
		{
			return;
		}
		if (Input.GetButton("Fire1"))
		{
			GameObject gameObject = this.RaycastObject(Input.mousePosition);
			if (gameObject != null && gameObject != base.gameObject && gameObject.name.Equals("monsterprefab(Clone)", StringComparison.OrdinalIgnoreCase))
			{
				GameLogic.TagPlayer(gameObject.transform.root.GetComponent<PhotonView>().owner.ID);
			}
		}
	}

	// Token: 0x0600029B RID: 667 RVA: 0x00011878 File Offset: 0x0000FA78
	private GameObject RaycastObject(Vector2 screenPos)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(screenPos), out raycastHit, 200f))
		{
			return raycastHit.collider.gameObject;
		}
		return null;
	}
}
