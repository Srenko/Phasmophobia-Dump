using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

// Token: 0x02000166 RID: 358
[RequireComponent(typeof(PhotonView))]
public class SaltShaker : MonoBehaviour
{
	// Token: 0x0600098F RID: 2447 RVA: 0x0003AB46 File Offset: 0x00038D46
	private void Awake()
	{
		this.source = base.GetComponent<AudioSource>();
		this.view = base.GetComponent<PhotonView>();
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0003AB6C File Offset: 0x00038D6C
	private void Start()
	{
		if (XRDevice.isPresent)
		{
			this.photonInteract.AddUseEvent(new UnityAction(this.Use));
			return;
		}
		this.photonInteract.AddPCSecondaryUseEvent(new UnityAction(this.Use));
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0003ABA4 File Offset: 0x00038DA4
	private void Use()
	{
		if (this.usesLeft == 0)
		{
			return;
		}
		RaycastHit raycastHit;
		if (!XRDevice.isPresent)
		{
			if (Physics.Raycast(GameController.instance.myPlayer.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit, this.grabDistance, this.mask))
			{
				this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
				this.view.RPC("SpawnSalt", PhotonTargets.All, new object[]
				{
					raycastHit.point,
					raycastHit.normal
				});
				return;
			}
		}
		else if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.forward), out raycastHit, 1.5f, this.mask))
		{
			this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
			this.view.RPC("SpawnSalt", PhotonTargets.All, new object[]
			{
				raycastHit.point,
				raycastHit.normal
			});
		}
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0003ACDC File Offset: 0x00038EDC
	private void Update()
	{
		if (this.usesLeft == 0)
		{
			if (this.helperObject.activeInHierarchy)
			{
				this.helperObject.SetActive(false);
			}
			return;
		}
		if (this.view.isMine)
		{
			if (this.photonInteract.isGrabbed)
			{
				if (!XRDevice.isPresent)
				{
					RaycastHit raycastHit;
					if (Physics.Raycast(GameController.instance.myPlayer.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit, this.grabDistance, this.mask))
					{
						if (!this.helperObject.activeInHierarchy)
						{
							this.helperObject.SetActive(true);
						}
						this.helperObject.transform.position = raycastHit.point;
						Quaternion rotation = this.helperObject.transform.rotation;
						rotation.SetLookRotation(raycastHit.normal);
						rotation.eulerAngles = new Vector3(Mathf.Round(rotation.eulerAngles.x / 90f) * 90f + 90f, Mathf.Round(rotation.eulerAngles.y / 90f) * 90f, rotation.eulerAngles.z);
						this.helperObject.transform.rotation = rotation;
						return;
					}
					if (this.helperObject.activeInHierarchy)
					{
						this.helperObject.SetActive(false);
						return;
					}
				}
				else
				{
					RaycastHit raycastHit;
					if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.forward), out raycastHit, 1.5f, this.mask))
					{
						if (!this.helperObject.activeInHierarchy)
						{
							this.helperObject.SetActive(true);
						}
						this.helperObject.transform.position = raycastHit.point;
						Quaternion rotation2 = this.helperObject.transform.rotation;
						rotation2.SetLookRotation(raycastHit.normal);
						rotation2.eulerAngles = new Vector3(Mathf.Round(rotation2.eulerAngles.x / 90f) * 90f + 90f, Mathf.Round(rotation2.eulerAngles.y / 90f) * 90f, rotation2.eulerAngles.z);
						this.helperObject.transform.rotation = rotation2;
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

	// Token: 0x06000993 RID: 2451 RVA: 0x0003AF78 File Offset: 0x00039178
	[PunRPC]
	private void NetworkedUse()
	{
		this.usesLeft--;
		this.source.Play();
		base.StartCoroutine(this.PlayNoiseObject());
		if (this.usesLeft == 0)
		{
			this.helperObject.SetActive(false);
		}
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0003AFB4 File Offset: 0x000391B4
	[PunRPC]
	private void SpawnSalt(Vector3 hitPos, Vector3 normal)
	{
		Quaternion rotation = this.helperObject.transform.rotation;
		rotation.SetLookRotation(normal);
		rotation.eulerAngles = new Vector3(Mathf.Round(rotation.eulerAngles.x / 90f) * 90f + 90f, Mathf.Round(rotation.eulerAngles.y / 90f) * 90f, rotation.eulerAngles.z);
		this.helperObject.transform.rotation = rotation;
		PhotonNetwork.Instantiate(this.saltPrefab.name, hitPos, Quaternion.identity, 0);
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0003B05B File Offset: 0x0003925B
	private IEnumerator PlayNoiseObject()
	{
		this.noise.gameObject.SetActive(true);
		yield return 0;
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0400099E RID: 2462
	private PhotonView view;

	// Token: 0x0400099F RID: 2463
	private PhotonObjectInteract photonInteract;

	// Token: 0x040009A0 RID: 2464
	private AudioSource source;

	// Token: 0x040009A1 RID: 2465
	[SerializeField]
	private LayerMask mask;

	// Token: 0x040009A2 RID: 2466
	[SerializeField]
	private GameObject helperObject;

	// Token: 0x040009A3 RID: 2467
	[SerializeField]
	private GameObject saltPrefab;

	// Token: 0x040009A4 RID: 2468
	[SerializeField]
	private Noise noise;

	// Token: 0x040009A5 RID: 2469
	private int usesLeft = 3;

	// Token: 0x040009A6 RID: 2470
	[Header("PC")]
	private float grabDistance = 3f;

	// Token: 0x040009A7 RID: 2471
	private Ray playerAim;
}
