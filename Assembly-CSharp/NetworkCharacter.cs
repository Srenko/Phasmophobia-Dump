using System;
using Photon;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class NetworkCharacter : Photon.MonoBehaviour
{
	// Token: 0x060002A7 RID: 679 RVA: 0x000119B4 File Offset: 0x0000FBB4
	private void Update()
	{
		if (!base.photonView.isMine)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.correctPlayerPos, Time.deltaTime * 5f);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.correctPlayerRot, Time.deltaTime * 5f);
		}
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x00011A28 File Offset: 0x0000FC28
	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(base.transform.position);
			stream.SendNext(base.transform.rotation);
			myThirdPersonController component = base.GetComponent<myThirdPersonController>();
			stream.SendNext((int)component._characterState);
			return;
		}
		this.correctPlayerPos = (Vector3)stream.ReceiveNext();
		this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
		base.GetComponent<myThirdPersonController>()._characterState = (CharacterState)stream.ReceiveNext();
	}

	// Token: 0x040002EA RID: 746
	private Vector3 correctPlayerPos = Vector3.zero;

	// Token: 0x040002EB RID: 747
	private Quaternion correctPlayerRot = Quaternion.identity;
}
