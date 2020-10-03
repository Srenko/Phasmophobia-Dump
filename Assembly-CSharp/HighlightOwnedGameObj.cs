using System;
using Photon;
using UnityEngine;

// Token: 0x02000045 RID: 69
[RequireComponent(typeof(PhotonView))]
public class HighlightOwnedGameObj : Photon.MonoBehaviour
{
	// Token: 0x0600017B RID: 379 RVA: 0x0000AA18 File Offset: 0x00008C18
	private void Update()
	{
		if (base.photonView.isMine)
		{
			if (this.markerTransform == null)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.PointerPrefab);
				gameObject.transform.parent = base.gameObject.transform;
				this.markerTransform = gameObject.transform;
			}
			Vector3 position = base.gameObject.transform.position;
			this.markerTransform.position = new Vector3(position.x, position.y + this.Offset, position.z);
			this.markerTransform.rotation = Quaternion.identity;
			return;
		}
		if (this.markerTransform != null)
		{
			Object.Destroy(this.markerTransform.gameObject);
			this.markerTransform = null;
		}
	}

	// Token: 0x040001B4 RID: 436
	public GameObject PointerPrefab;

	// Token: 0x040001B5 RID: 437
	public float Offset = 0.5f;

	// Token: 0x040001B6 RID: 438
	private Transform markerTransform;
}
