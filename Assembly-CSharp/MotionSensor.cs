using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x0200015F RID: 351
public class MotionSensor : MonoBehaviour
{
	// Token: 0x06000958 RID: 2392 RVA: 0x00038A38 File Offset: 0x00036C38
	private void Awake()
	{
		this.noise.gameObject.SetActive(false);
		this.iconStartLocalPosition = this.mapIcon.localPosition;
		this.iconStartLocalRotation = this.mapIcon.localRotation;
		this.iconStartLocalScale = this.mapIcon.localScale;
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x00038A8C File Offset: 0x00036C8C
	private void Start()
	{
		if (XRDevice.isPresent)
		{
			this.photonInteract.AddUseEvent(new UnityAction(this.MotionUse));
			this.photonInteract.AddGrabbedEvent(new UnityAction(this.OnGrabbed));
			return;
		}
		this.photonInteract.AddPCSecondaryUseEvent(new UnityAction(this.SecondaryUse));
		this.photonInteract.AddPCGrabbedEvent(new UnityAction(this.OnGrabbed));
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x00038AFD File Offset: 0x00036CFD
	private void OnEnable()
	{
		this.detected = false;
		this.rend.material.SetTexture("_EmissionMap", this.redTexture);
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00038B24 File Offset: 0x00036D24
	private void Update()
	{
		if (this.isPlaced)
		{
			if (this.detected)
			{
				this.timer -= Time.deltaTime;
				if (this.timer < 0f)
				{
					this.detected = false;
					this.rend.material.SetTexture("_EmissionMap", this.redTexture);
					return;
				}
			}
		}
		else if (this.view.isMine)
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
					if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.back), out raycastHit, 0.5f, this.mask, QueryTriggerInteraction.Ignore))
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

	// Token: 0x0600095C RID: 2396 RVA: 0x00038DCC File Offset: 0x00036FCC
	private void SecondaryUse()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(GameController.instance.myPlayer.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit, this.grabDistance, this.mask, QueryTriggerInteraction.Ignore))
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

	// Token: 0x0600095D RID: 2397 RVA: 0x00038E8B File Offset: 0x0003708B
	public void Detection(bool isGhost)
	{
		this.view.RPC("DetectionNetworked", PhotonTargets.All, new object[]
		{
			isGhost
		});
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x00038EB0 File Offset: 0x000370B0
	[PunRPC]
	private void DetectionNetworked(bool isGhost)
	{
		this.detected = true;
		this.timer = 2f;
		this.rend.material.SetTexture("_EmissionMap", this.greenTexture);
		if (isGhost && MissionMotionSensor.instance != null && !MissionMotionSensor.instance.completed)
		{
			MissionMotionSensor.instance.CompleteMission();
		}
		MotionSensorData.instance.Detected(this);
		base.StartCoroutine(this.PlayNoiseObject());
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x00038F28 File Offset: 0x00037128
	private void MotionUse()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.back), out raycastHit, 0.5f, this.mask, QueryTriggerInteraction.Ignore))
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

	// Token: 0x06000960 RID: 2400 RVA: 0x00038FBC File Offset: 0x000371BC
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

	// Token: 0x06000961 RID: 2401 RVA: 0x00039020 File Offset: 0x00037220
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
			return;
		}
		this.mapIcon.SetParent(base.transform);
		this.mapIcon.localPosition = this.iconStartLocalPosition;
		this.mapIcon.localRotation = this.iconStartLocalRotation;
		this.mapIcon.localScale = this.iconStartLocalScale;
		MotionSensorData.instance.RemoveText(this);
		this.mapIcon.gameObject.SetActive(false);
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x000390F1 File Offset: 0x000372F1
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
		MotionSensorData.instance.SetText(this);
		this.helperObject.SetActive(false);
		yield break;
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x0003910E File Offset: 0x0003730E
	private IEnumerator MapIconDelay()
	{
		yield return new WaitForSeconds(3f);
		if (MapController.instance)
		{
			this.view.RPC("AssignSensorOnMap", PhotonTargets.All, new object[]
			{
				(int)LevelController.instance.currentPlayerRoom.floorType
			});
		}
		yield break;
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0003911D File Offset: 0x0003731D
	[PunRPC]
	private void AssignSensorOnMap(int floorID)
	{
		this.mapIcon.gameObject.SetActive(true);
		MapController.instance.AssignSensor(base.transform, this.mapIcon, floorID, this);
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x00039148 File Offset: 0x00037348
	private IEnumerator PlayNoiseObject()
	{
		this.noise.gameObject.SetActive(true);
		yield return 0;
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0400096C RID: 2412
	[HideInInspector]
	public bool isPlaced;

	// Token: 0x0400096D RID: 2413
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x0400096E RID: 2414
	[SerializeField]
	private Rigidbody rigidbdy;

	// Token: 0x0400096F RID: 2415
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000970 RID: 2416
	[SerializeField]
	private Renderer rend;

	// Token: 0x04000971 RID: 2417
	[SerializeField]
	private Noise noise;

	// Token: 0x04000972 RID: 2418
	[HideInInspector]
	public string roomName;

	// Token: 0x04000973 RID: 2419
	[SerializeField]
	private Texture greenTexture;

	// Token: 0x04000974 RID: 2420
	[SerializeField]
	private Texture redTexture;

	// Token: 0x04000975 RID: 2421
	[SerializeField]
	private LayerMask mask;

	// Token: 0x04000976 RID: 2422
	[SerializeField]
	private GameObject helperObject;

	// Token: 0x04000977 RID: 2423
	[HideInInspector]
	public int id;

	// Token: 0x04000978 RID: 2424
	private float timer = 2f;

	// Token: 0x04000979 RID: 2425
	private bool detected;

	// Token: 0x0400097A RID: 2426
	[SerializeField]
	private Transform mapIcon;

	// Token: 0x0400097B RID: 2427
	public Image sensorIcon;

	// Token: 0x0400097C RID: 2428
	private Quaternion iconStartLocalRotation;

	// Token: 0x0400097D RID: 2429
	private Vector3 iconStartLocalScale;

	// Token: 0x0400097E RID: 2430
	private Vector3 iconStartLocalPosition;

	// Token: 0x0400097F RID: 2431
	[Header("PC")]
	private float grabDistance = 1f;

	// Token: 0x04000980 RID: 2432
	private Ray playerAim;
}
