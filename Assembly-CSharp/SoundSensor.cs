using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

// Token: 0x02000168 RID: 360
public class SoundSensor : MonoBehaviour
{
	// Token: 0x0600099A RID: 2458 RVA: 0x0003B13C File Offset: 0x0003933C
	private void Awake()
	{
		this.helperObject.transform.SetParent(null);
		this.iconStartLocalPosition = this.mapIcon.localPosition;
		this.iconStartLocalRotation = this.mapIcon.localRotation;
		this.iconStartLocalScale = this.mapIcon.localScale;
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0003B190 File Offset: 0x00039390
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
		this.rend.material.SetColor("_EmissionColor", Color.red);
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x0003B21C File Offset: 0x0003941C
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

	// Token: 0x0600099D RID: 2461 RVA: 0x0003B280 File Offset: 0x00039480
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

	// Token: 0x0600099E RID: 2462 RVA: 0x0003B314 File Offset: 0x00039514
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
		this.rend.material.SetColor("_EmissionColor", this.isPlaced ? Color.green : Color.red);
		if (this.isPlaced)
		{
			this.photonInteract.ForceStopInteracting();
		}
		if (this.isPlaced)
		{
			base.StartCoroutine(this.Place(position, normal));
			return;
		}
		this.mapIcon.SetParent(base.transform);
		this.mapIcon.localPosition = this.iconStartLocalPosition;
		this.mapIcon.localRotation = this.iconStartLocalRotation;
		this.mapIcon.localScale = this.iconStartLocalScale;
		SoundSensorData.instance.RemoveText(this);
		this.mapIcon.gameObject.SetActive(false);
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0003B40E File Offset: 0x0003960E
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
		if (this.view.isMine)
		{
			base.StartCoroutine(this.MapIconDelay());
		}
		SoundSensorData.instance.SetText(this);
		this.helperObject.SetActive(false);
		yield break;
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x0003B42B File Offset: 0x0003962B
	private IEnumerator MapIconDelay()
	{
		yield return new WaitForSeconds(3f);
		if (MapController.instance)
		{
			this.view.RPC("AssignSoundSensorToMap", PhotonTargets.All, new object[]
			{
				(int)LevelController.instance.currentPlayerRoom.floorType
			});
		}
		yield break;
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0003B43A File Offset: 0x0003963A
	[PunRPC]
	private void AssignSoundSensorToMap(int floorID)
	{
		this.mapIcon.gameObject.SetActive(true);
		MapController.instance.AssignSensor(base.transform, this.mapIcon, floorID, null);
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x0003B468 File Offset: 0x00039668
	private void Update()
	{
		if (this.isPlaced)
		{
			this.checkTimer -= Time.deltaTime;
			if (this.checkTimer < 0f)
			{
				SoundSensorData.instance.UpdateSensorValue(this.id, this.highestVolume);
				this.highestVolume = 0f;
				base.StartCoroutine(this.ResetTrigger());
				this.checkTimer = 5f;
				return;
			}
		}
		else if (this.view.isMine)
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

	// Token: 0x060009A3 RID: 2467 RVA: 0x0003B718 File Offset: 0x00039918
	private void SecondaryUse()
	{
		this.playerAim = GameController.instance.myPlayer.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit raycastHit;
		if (Physics.Raycast(this.playerAim, out raycastHit, this.grabDistance, this.mask))
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

	// Token: 0x060009A4 RID: 2468 RVA: 0x0003B7E2 File Offset: 0x000399E2
	private IEnumerator ResetTrigger()
	{
		this.col.enabled = false;
		yield return 0;
		this.col.enabled = true;
		yield break;
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0003B7F1 File Offset: 0x000399F1
	private void OnDisable()
	{
		if (this.helperObject)
		{
			this.helperObject.SetActive(false);
		}
	}

	// Token: 0x040009AD RID: 2477
	[HideInInspector]
	public bool isPlaced;

	// Token: 0x040009AE RID: 2478
	[HideInInspector]
	public int id;

	// Token: 0x040009AF RID: 2479
	[HideInInspector]
	public string roomName;

	// Token: 0x040009B0 RID: 2480
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x040009B1 RID: 2481
	[SerializeField]
	private Rigidbody rigidbdy;

	// Token: 0x040009B2 RID: 2482
	[SerializeField]
	private PhotonView view;

	// Token: 0x040009B3 RID: 2483
	[SerializeField]
	private Renderer rend;

	// Token: 0x040009B4 RID: 2484
	private float checkTimer = 5f;

	// Token: 0x040009B5 RID: 2485
	public float highestVolume;

	// Token: 0x040009B6 RID: 2486
	[SerializeField]
	private BoxCollider col;

	// Token: 0x040009B7 RID: 2487
	[SerializeField]
	private GameObject helperObject;

	// Token: 0x040009B8 RID: 2488
	[SerializeField]
	private Transform mapIcon;

	// Token: 0x040009B9 RID: 2489
	private Quaternion iconStartLocalRotation;

	// Token: 0x040009BA RID: 2490
	private Vector3 iconStartLocalScale;

	// Token: 0x040009BB RID: 2491
	private Vector3 iconStartLocalPosition;

	// Token: 0x040009BC RID: 2492
	[Header("PC")]
	private float grabDistance = 1f;

	// Token: 0x040009BD RID: 2493
	private Ray playerAim;

	// Token: 0x040009BE RID: 2494
	[SerializeField]
	private LayerMask mask;
}
