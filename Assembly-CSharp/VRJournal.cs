using System;
using Photon;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

// Token: 0x020001CF RID: 463
public class VRJournal : Photon.MonoBehaviour
{
	// Token: 0x06000CA5 RID: 3237 RVA: 0x000509C8 File Offset: 0x0004EBC8
	private void Awake()
	{
		this.rend = base.GetComponent<Renderer>();
		this.view = base.GetComponent<PhotonView>();
		this.boxCollider = base.GetComponent<BoxCollider>();
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.localPosition = new Vector3(0.4f, 0f, 0f);
		this.rend.enabled = false;
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00050A2B File Offset: 0x0004EC2B
	private void Start()
	{
		this.Initialise();
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00050A34 File Offset: 0x0004EC34
	private void Initialise()
	{
		if (this.view.isMine || !PhotonNetwork.inRoom)
		{
			if (XRDevice.isPresent)
			{
				if (LevelController.instance)
				{
					LevelController.instance.journalController.gameObject.SetActive(false);
					LevelController.instance.journalController = this.journalController;
				}
				this.boxCollider.center = new Vector3(0f, 0f, 0f);
				this.boxCollider.size = new Vector3(0.4f, 0.4f, 0.4f);
				this.photonInteract.AddGrabbedEvent(new UnityAction(this.OnGrab));
				this.photonInteract.AddUnGrabbedEvent(new UnityAction(this.UnGrabbed));
				if (GameController.instance)
				{
					if (GameController.instance.myPlayer != null)
					{
						base.Invoke("ResetTransform", 5f);
					}
					else
					{
						GameController.instance.OnLocalPlayerSpawned.AddListener(new UnityAction(this.DelayReset));
					}
				}
				else
				{
					base.Invoke("ResetTransform", 5f);
				}
			}
		}
		else
		{
			this.boxCollider.enabled = false;
		}
		this.journalController.gameObject.SetActive(false);
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x00050B78 File Offset: 0x0004ED78
	private void UnGrabbed()
	{
		if (GameController.instance)
		{
			GameController.instance.myPlayer.player.movementSettings.InMenuOrJournal(false);
		}
		else
		{
			MainManager.instance.localPlayer.movementSettings.InMenuOrJournal(false);
		}
		this.boxCollider.center = new Vector3(0f, 0f, 0f);
		this.boxCollider.size = new Vector3(0.4f, 0.4f, 0.4f);
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("ShowOrHide", PhotonTargets.All, new object[]
			{
				false
			});
		}
		else
		{
			this.ShowOrHide(false);
		}
		this.ResetTransform();
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00050C36 File Offset: 0x0004EE36
	private void DelayReset()
	{
		GameController.instance.OnLocalPlayerSpawned.RemoveListener(new UnityAction(this.DelayReset));
		base.Invoke("ResetTransform", 5f);
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00050C64 File Offset: 0x0004EE64
	private void ResetTransform()
	{
		if (GameController.instance)
		{
			base.transform.SetParent(GameController.instance.myPlayer.player.cam.transform);
		}
		else
		{
			base.transform.SetParent(MainManager.instance.localPlayer.cam.transform);
		}
		base.transform.localPosition = this.localPosition;
		base.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00050CF8 File Offset: 0x0004EEF8
	private void OnGrab()
	{
		if (GameController.instance)
		{
			GameController.instance.myPlayer.player.movementSettings.InMenuOrJournal(true);
		}
		else
		{
			MainManager.instance.localPlayer.movementSettings.InMenuOrJournal(true);
		}
		this.boxCollider.center = new Vector3(-0.025f, 0.0175f, 0.15f);
		this.boxCollider.size = new Vector3(0.45f, 0.035f, 0.3f);
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("ShowOrHide", PhotonTargets.All, new object[]
			{
				true
			});
		}
		else
		{
			this.ShowOrHide(true);
		}
		this.openSource.clip = this.openClip;
		if (!this.openSource.isPlaying)
		{
			this.openSource.Play();
		}
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x00050DD9 File Offset: 0x0004EFD9
	[PunRPC]
	private void ShowOrHide(bool show)
	{
		this.rend.enabled = show;
		this.journalController.gameObject.SetActive(show);
	}

	// Token: 0x04000D52 RID: 3410
	private PhotonView view;

	// Token: 0x04000D53 RID: 3411
	private Renderer rend;

	// Token: 0x04000D54 RID: 3412
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000D55 RID: 3413
	private Vector3 localPosition;

	// Token: 0x04000D56 RID: 3414
	private BoxCollider boxCollider;

	// Token: 0x04000D57 RID: 3415
	[SerializeField]
	private JournalController journalController;

	// Token: 0x04000D58 RID: 3416
	[SerializeField]
	private AudioSource openSource;

	// Token: 0x04000D59 RID: 3417
	[SerializeField]
	private AudioClip openClip;
}
