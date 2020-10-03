using System;
using Photon;
using UnityEngine;

// Token: 0x02000049 RID: 73
[RequireComponent(typeof(PhotonView))]
public class OnClickRequestOwnership : Photon.MonoBehaviour
{
	// Token: 0x06000184 RID: 388 RVA: 0x0000AC2C File Offset: 0x00008E2C
	public void OnClick()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			Vector3 vector = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			base.photonView.RPC("ColorRpc", PhotonTargets.AllBufferedViaServer, new object[]
			{
				vector
			});
			return;
		}
		if (base.photonView.ownerId == PhotonNetwork.player.ID)
		{
			Debug.Log("Not requesting ownership. Already mine.");
			return;
		}
		base.photonView.RequestOwnership();
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000ACD4 File Offset: 0x00008ED4
	[PunRPC]
	public void ColorRpc(Vector3 col)
	{
		Color color = new Color(col.x, col.y, col.z);
		base.gameObject.GetComponent<Renderer>().material.color = color;
	}
}
