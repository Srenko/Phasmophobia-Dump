using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

// Token: 0x020001AD RID: 429
public class PCCrouch : MonoBehaviour
{
	// Token: 0x06000BA2 RID: 2978 RVA: 0x00048164 File Offset: 0x00046364
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.startPos = this.player.headObject.transform.localPosition;
		this.crouchPos = new Vector3(this.startPos.x, 0f, this.startPos.z);
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x000481C0 File Offset: 0x000463C0
	private void Crouch()
	{
		this.isCrouched = !this.isCrouched;
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("NetworkedCrouch", PhotonTargets.OthersBuffered, new object[]
			{
				this.isCrouched
			});
		}
		base.StopCoroutine("CrouchAnim");
		base.StartCoroutine("CrouchAnim");
		if (this.player.charAnim)
		{
			this.player.charAnim.SetBool("isCrouched", this.isCrouched);
		}
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x0004824C File Offset: 0x0004644C
	private IEnumerator CrouchAnim()
	{
		float elapsedTime = 0f;
		while (elapsedTime < 0.2f)
		{
			if (this.isCrouched)
			{
				this.player.headObject.transform.localPosition = Vector3.Lerp(this.startPos, this.crouchPos, elapsedTime / 0.2f);
			}
			else
			{
				this.player.headObject.transform.localPosition = Vector3.Lerp(this.crouchPos, this.startPos, elapsedTime / 0.2f);
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		this.player.headObject.transform.localPosition = (this.isCrouched ? this.crouchPos : this.startPos);
		yield return null;
		yield break;
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x0004825B File Offset: 0x0004645B
	[PunRPC]
	private void NetworkedCrouch(bool isCrouched)
	{
		this.player.headObject.transform.localPosition = (isCrouched ? this.crouchPos : this.startPos);
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00048283 File Offset: 0x00046483
	public void OnCrouch(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started && !XRDevice.isPresent)
		{
			this.Crouch();
		}
	}

	// Token: 0x04000BF6 RID: 3062
	private Vector3 startPos;

	// Token: 0x04000BF7 RID: 3063
	private Vector3 crouchPos;

	// Token: 0x04000BF8 RID: 3064
	private PhotonView view;

	// Token: 0x04000BF9 RID: 3065
	[SerializeField]
	private Player player;

	// Token: 0x04000BFA RID: 3066
	[HideInInspector]
	public bool isCrouched;
}
