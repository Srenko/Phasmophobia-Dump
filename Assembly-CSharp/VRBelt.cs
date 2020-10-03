using System;
using UnityEngine;

// Token: 0x020001CE RID: 462
public class VRBelt : MonoBehaviour
{
	// Token: 0x06000CA2 RID: 3234 RVA: 0x00050848 File Offset: 0x0004EA48
	private void Start()
	{
		if (!PhotonNetwork.inRoom)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x00050860 File Offset: 0x0004EA60
	public void Update()
	{
		if (this.view.isMine)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(this.playerCam.transform.position, Vector3.down, out raycastHit, 3.5f, this.mask, QueryTriggerInteraction.Ignore))
			{
				this.newPos = new Vector3(this.playerCam.transform.position.x, raycastHit.point.y + (this.playerCam.position.y - raycastHit.point.y) / 2f + 0.1f, this.playerCam.transform.position.z);
			}
			this.myQuat = base.transform.rotation;
			this.myEul = this.myQuat.eulerAngles;
			this.myEul.y = this.playerCam.rotation.eulerAngles.y;
			this.myQuat.eulerAngles = this.myEul;
			base.transform.position = Vector3.Lerp(base.transform.position, this.newPos, Time.deltaTime * 4f);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.myQuat, Time.deltaTime * 4f);
		}
	}

	// Token: 0x04000D4C RID: 3404
	[SerializeField]
	private Transform playerCam;

	// Token: 0x04000D4D RID: 3405
	[SerializeField]
	private LayerMask mask;

	// Token: 0x04000D4E RID: 3406
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000D4F RID: 3407
	private Quaternion myQuat;

	// Token: 0x04000D50 RID: 3408
	private Vector3 myEul;

	// Token: 0x04000D51 RID: 3409
	private Vector3 newPos;
}
