using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

// Token: 0x02000159 RID: 345
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(AudioSource))]
public class Glowstick : MonoBehaviour
{
	// Token: 0x0600092A RID: 2346 RVA: 0x00037BDC File Offset: 0x00035DDC
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
		if (XRDevice.isPresent)
		{
			this.photonInteract.AddGrabbedEvent(new UnityAction(this.Grabbed));
			this.photonInteract.AddUnGrabbedEvent(new UnityAction(this.Dropped));
			return;
		}
		this.photonInteract.AddPCGrabbedEvent(new UnityAction(this.Grabbed));
		this.photonInteract.AddPCUnGrabbedEvent(new UnityAction(this.Dropped));
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x00037C64 File Offset: 0x00035E64
	private void Grabbed()
	{
		if (!this.used)
		{
			return;
		}
		this.view.RPC("SyncGrab", PhotonTargets.All, new object[]
		{
			true
		});
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x00037C8F File Offset: 0x00035E8F
	public void Dropped()
	{
		if (!this.used)
		{
			return;
		}
		base.StartCoroutine(this.DropDelay());
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00037CA7 File Offset: 0x00035EA7
	private IEnumerator DropDelay()
	{
		yield return new WaitForSeconds(0.1f);
		if (!base.transform.root.CompareTag("Player"))
		{
			this.view.RPC("SyncGrab", PhotonTargets.All, new object[]
			{
				false
			});
		}
		yield break;
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00037CB8 File Offset: 0x00035EB8
	private void Use()
	{
		if (this.used)
		{
			return;
		}
		this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
		if (this.photonInteract.isGrabbed)
		{
			this.view.RPC("SyncGrab", PhotonTargets.All, new object[]
			{
				true
			});
		}
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x00037D11 File Offset: 0x00035F11
	[PunRPC]
	private void NetworkedUse()
	{
		this.used = true;
		this.myLight.enabled = true;
		this.rend.material.EnableKeyword("_EMISSION");
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x00037D3B File Offset: 0x00035F3B
	[PunRPC]
	private void SyncGrab(bool isGrabbed)
	{
		this.myLight.range = (isGrabbed ? 0.5f : 1.5f);
	}

	// Token: 0x0400093B RID: 2363
	[SerializeField]
	private Light myLight;

	// Token: 0x0400093C RID: 2364
	[SerializeField]
	private Renderer rend;

	// Token: 0x0400093D RID: 2365
	[SerializeField]
	private PhotonView view;

	// Token: 0x0400093E RID: 2366
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x0400093F RID: 2367
	private bool used;
}
