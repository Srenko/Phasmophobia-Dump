using System;
using Photon;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class ThirdPersonNetwork : Photon.MonoBehaviour
{
	// Token: 0x06000278 RID: 632 RVA: 0x00010E40 File Offset: 0x0000F040
	private void OnEnable()
	{
		this.firstTake = true;
	}

	// Token: 0x06000279 RID: 633 RVA: 0x00010E4C File Offset: 0x0000F04C
	private void Awake()
	{
		this.cameraScript = base.GetComponent<ThirdPersonCamera>();
		this.controllerScript = base.GetComponent<ThirdPersonController>();
		if (base.photonView.isMine)
		{
			this.cameraScript.enabled = true;
			this.controllerScript.enabled = true;
		}
		else
		{
			this.cameraScript.enabled = false;
			this.controllerScript.enabled = true;
			this.controllerScript.isControllable = false;
		}
		base.gameObject.name = base.gameObject.name + base.photonView.viewID;
	}

	// Token: 0x0600027A RID: 634 RVA: 0x00010EE8 File Offset: 0x0000F0E8
	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext((int)this.controllerScript._characterState);
			stream.SendNext(base.transform.position);
			stream.SendNext(base.transform.rotation);
			return;
		}
		this.controllerScript._characterState = (CharacterState)((int)stream.ReceiveNext());
		this.correctPlayerPos = (Vector3)stream.ReceiveNext();
		this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
		if (this.firstTake)
		{
			this.firstTake = false;
			base.transform.position = this.correctPlayerPos;
			base.transform.rotation = this.correctPlayerRot;
		}
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00010FAC File Offset: 0x0000F1AC
	private void Update()
	{
		if (!base.photonView.isMine)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.correctPlayerPos, Time.deltaTime * 5f);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.correctPlayerRot, Time.deltaTime * 5f);
		}
	}

	// Token: 0x040002D6 RID: 726
	private ThirdPersonCamera cameraScript;

	// Token: 0x040002D7 RID: 727
	private ThirdPersonController controllerScript;

	// Token: 0x040002D8 RID: 728
	private bool firstTake;

	// Token: 0x040002D9 RID: 729
	private Vector3 correctPlayerPos = Vector3.zero;

	// Token: 0x040002DA RID: 730
	private Quaternion correctPlayerRot = Quaternion.identity;
}
