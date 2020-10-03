using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

// Token: 0x0200015B RID: 347
public class IRLightSensor : MonoBehaviour
{
	// Token: 0x0600093C RID: 2364 RVA: 0x00038017 File Offset: 0x00036217
	private void Awake()
	{
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.rigidbdy = base.GetComponent<Rigidbody>();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x00038040 File Offset: 0x00036240
	private void Start()
	{
		if (XRDevice.isPresent)
		{
			this.photonInteract.AddUseEvent(new UnityAction(this.MotionUse));
			this.photonInteract.AddGrabbedEvent(new UnityAction(this.OnGrabbed));
		}
		else
		{
			this.photonInteract.AddPCSecondaryUseEvent(new UnityAction(this.SecondaryUse));
			this.photonInteract.AddPCGrabbedEvent(new UnityAction(this.OnGrabbed));
		}
		this.rend.material.DisableKeyword("_EMISSION");
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x000380C8 File Offset: 0x000362C8
	private void Update()
	{
		if (!this.isPlaced && this.view.isMine)
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
						rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, Mathf.Round(rotation.eulerAngles.y / 45f) * 45f, rotation.eulerAngles.z);
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
					if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.back), out raycastHit, 0.5f, this.mask))
					{
						if (!this.helperObject.activeInHierarchy)
						{
							this.helperObject.SetActive(true);
						}
						this.helperObject.transform.position = raycastHit.point;
						Quaternion rotation2 = this.helperObject.transform.rotation;
						rotation2.SetLookRotation(raycastHit.normal);
						rotation2.eulerAngles = new Vector3(rotation2.eulerAngles.x, Mathf.Round(rotation2.eulerAngles.y / 45f) * 45f, rotation2.eulerAngles.z);
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

	// Token: 0x0600093F RID: 2367 RVA: 0x00038320 File Offset: 0x00036520
	private void SecondaryUse()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(GameController.instance.myPlayer.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit, this.grabDistance, this.mask))
		{
			GameController.instance.myPlayer.player.pcPropGrab.Drop(false);
			this.view.RPC("PlaceOrPickupSensor", PhotonTargets.All, new object[]
			{
				true,
				raycastHit.point,
				raycastHit.normal,
				LevelController.instance.currentPlayerRoom.roomName
			});
		}
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x000383DE File Offset: 0x000365DE
	public void Detection()
	{
		if (!this.detected)
		{
			this.view.RPC("DetectionNetworked", PhotonTargets.All, Array.Empty<object>());
		}
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x000383FE File Offset: 0x000365FE
	[PunRPC]
	private void DetectionNetworked()
	{
		this.detected = true;
		this.rend.material.EnableKeyword("_EMISSION");
		this.myLight.enabled = true;
		base.StartCoroutine(this.StopDetection());
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00038435 File Offset: 0x00036635
	private IEnumerator StopDetection()
	{
		yield return new WaitForSeconds(2f);
		this.myLight.enabled = false;
		this.detected = false;
		this.rend.material.DisableKeyword("_EMISSION");
		yield break;
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x00038444 File Offset: 0x00036644
	private void MotionUse()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.back), out raycastHit, 0.5f, this.mask))
		{
			this.view.RPC("PlaceOrPickupSensor", PhotonTargets.All, new object[]
			{
				true,
				raycastHit.point,
				raycastHit.normal,
				LevelController.instance.currentPlayerRoom.roomName
			});
		}
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x000384D8 File Offset: 0x000366D8
	private void OnGrabbed()
	{
		if (this.isPlaced)
		{
			this.view.RPC("PlaceOrPickupSensor", PhotonTargets.All, new object[]
			{
				false,
				Vector3.zero,
				Vector3.zero,
				LevelController.instance.currentPlayerRoom.roomName
			});
		}
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x0003853C File Offset: 0x0003673C
	[PunRPC]
	private void PlaceOrPickupSensor(bool isBeingPlaced, Vector3 position, Vector3 normal, string _roomName)
	{
		if (this.isPlaced)
		{
			base.GetComponent<PhotonTransformView>().m_PositionModel.SynchronizeEnabled = false;
			base.GetComponent<PhotonTransformView>().m_RotationModel.SynchronizeEnabled = false;
		}
		this.isPlaced = isBeingPlaced;
		this.roomName = _roomName;
		if (this.isPlaced)
		{
			this.photonInteract.ForceStopInteracting();
		}
		if (this.isPlaced)
		{
			base.StartCoroutine(this.Place(position, normal));
		}
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x000385AC File Offset: 0x000367AC
	private IEnumerator Place(Vector3 position, Vector3 normal)
	{
		base.transform.SetParent(null);
		yield return new WaitForSeconds(0.1f);
		this.rigidbdy.isKinematic = true;
		base.transform.position = position;
		Quaternion rotation = base.transform.rotation;
		rotation.SetLookRotation(normal);
		Vector3 eulerAngles = rotation.eulerAngles;
		eulerAngles.x = 0f;
		eulerAngles.y = Mathf.Round(eulerAngles.y / 45f) * 45f;
		eulerAngles.z = 0f;
		rotation.eulerAngles = eulerAngles;
		base.transform.rotation = rotation;
		this.helperObject.SetActive(false);
		yield break;
	}

	// Token: 0x0400094E RID: 2382
	[HideInInspector]
	public bool isPlaced;

	// Token: 0x0400094F RID: 2383
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000950 RID: 2384
	private Rigidbody rigidbdy;

	// Token: 0x04000951 RID: 2385
	private PhotonView view;

	// Token: 0x04000952 RID: 2386
	[SerializeField]
	private Renderer rend;

	// Token: 0x04000953 RID: 2387
	[HideInInspector]
	public string roomName;

	// Token: 0x04000954 RID: 2388
	[SerializeField]
	private LayerMask mask;

	// Token: 0x04000955 RID: 2389
	[SerializeField]
	private GameObject helperObject;

	// Token: 0x04000956 RID: 2390
	[SerializeField]
	private Light myLight;

	// Token: 0x04000957 RID: 2391
	[HideInInspector]
	public int id;

	// Token: 0x04000958 RID: 2392
	private bool detected;

	// Token: 0x04000959 RID: 2393
	[Header("PC")]
	private float grabDistance = 1f;

	// Token: 0x0400095A RID: 2394
	private Ray playerAim;
}
