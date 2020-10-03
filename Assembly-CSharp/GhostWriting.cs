using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

// Token: 0x02000158 RID: 344
public class GhostWriting : MonoBehaviour
{
	// Token: 0x06000922 RID: 2338 RVA: 0x00037835 File Offset: 0x00035A35
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.rend = base.GetComponent<Renderer>();
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x0003785B File Offset: 0x00035A5B
	private void Start()
	{
		if (!XRDevice.isPresent)
		{
			this.photonInteract.AddPCSecondaryUseEvent(new UnityAction(this.SecondaryUse));
		}
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x0003787C File Offset: 0x00035A7C
	private void Update()
	{
		if (this.view.isMine)
		{
			if (this.photonInteract.isGrabbed)
			{
				if (!XRDevice.isPresent)
				{
					RaycastHit raycastHit;
					if (Physics.Raycast(GameController.instance.myPlayer.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit, this.grabDistance, this.mask, QueryTriggerInteraction.Ignore))
					{
						if (!this.helperObject.activeInHierarchy)
						{
							this.helperObject.SetActive(true);
						}
						this.helperObject.transform.position = raycastHit.point;
						this.helperObject.transform.rotation = Quaternion.LookRotation(raycastHit.normal);
						return;
					}
					if (this.helperObject.activeInHierarchy)
					{
						this.helperObject.SetActive(false);
						return;
					}
				}
			}
			else if (this.helperObject.activeInHierarchy)
			{
				this.helperObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x0003797C File Offset: 0x00035B7C
	public void SecondaryUse()
	{
		if (!XRDevice.isPresent && GameController.instance.myPlayer.player.pcPropGrab.inventoryProps[GameController.instance.myPlayer.player.pcPropGrab.inventoryIndex] == this.photonInteract)
		{
			this.playerAim = GameController.instance.myPlayer.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(this.playerAim, out raycastHit, 1.6f, this.mask, QueryTriggerInteraction.Ignore))
			{
				GameController.instance.myPlayer.player.pcPropGrab.Drop(true);
				this.view.RPC("NonVRPlaceGhostBook", PhotonTargets.All, new object[]
				{
					raycastHit.point,
					Quaternion.LookRotation(raycastHit.normal)
				});
			}
		}
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x00037A81 File Offset: 0x00035C81
	[PunRPC]
	private void NonVRPlaceGhostBook(Vector3 point, Quaternion rot)
	{
		base.transform.SetParent(null);
		this.helperObject.SetActive(false);
		base.transform.position = point;
		base.transform.rotation = rot;
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x00037AB4 File Offset: 0x00035CB4
	public void Use()
	{
		if (!this.hasUsed && (LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Spirit || LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Revenant || LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Shade || LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Demon || LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Yurei || LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Oni))
		{
			this.view.RPC("SetTexture", PhotonTargets.AllBuffered, new object[]
			{
				Random.Range(0, this.textures.Length)
			});
		}
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x00037BA8 File Offset: 0x00035DA8
	[PunRPC]
	private void SetTexture(int index)
	{
		this.hasUsed = true;
		this.rend.material.mainTexture = this.textures[index];
	}

	// Token: 0x04000932 RID: 2354
	[SerializeField]
	private Texture[] textures;

	// Token: 0x04000933 RID: 2355
	private Renderer rend;

	// Token: 0x04000934 RID: 2356
	private PhotonView view;

	// Token: 0x04000935 RID: 2357
	private bool hasUsed;

	// Token: 0x04000936 RID: 2358
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000937 RID: 2359
	[Header("PC")]
	private readonly float grabDistance = 3f;

	// Token: 0x04000938 RID: 2360
	private Ray playerAim;

	// Token: 0x04000939 RID: 2361
	[SerializeField]
	private LayerMask mask;

	// Token: 0x0400093A RID: 2362
	[SerializeField]
	private GameObject helperObject;
}
