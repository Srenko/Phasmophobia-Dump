using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x02000152 RID: 338
public class CCTV : MonoBehaviour
{
	// Token: 0x060008DF RID: 2271 RVA: 0x00035200 File Offset: 0x00033400
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.myLight = this.cam.GetComponentInChildren<Light>();
		this.boxCollider = base.GetComponent<BoxCollider>();
		this.startColSize = this.boxCollider.size;
		if (!this.isFixedCamera)
		{
			this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		}
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x0003525C File Offset: 0x0003345C
	private void Start()
	{
		if (MainManager.instance)
		{
			base.gameObject.SetActive(false);
			return;
		}
		this.IsThisCameraSwitchedOn = false;
		this.isThisCameraActiveOnACCTVScreen = false;
		this.cam.enabled = false;
		this.nightVision.Power = LevelController.instance.nightVisionPower;
		this.rt = new RenderTexture(600, 500, 16, RenderTextureFormat.ARGB32);
		this.rt.Create();
		this.cam.targetTexture = this.rt;
		if (this.isFixedCamera)
		{
			this.SetupCCTV();
			return;
		}
		this.SetupDSLR();
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x000352FC File Offset: 0x000334FC
	private void SetupCCTV()
	{
		if (MapController.instance)
		{
			MapController.instance.AssignIcon(this.mapIcon.transform, this.floorType);
			return;
		}
		Object.FindObjectOfType<MapController>().AssignIcon(this.mapIcon.transform, this.floorType);
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x0003534C File Offset: 0x0003354C
	private void SetupDSLR()
	{
		if (!this.isHeadCamera)
		{
			this.photonInteract.AddUseEvent(new UnityAction(this.Use));
			this.photonInteract.AddPCSecondaryUseEvent(new UnityAction(this.SecondaryUse));
		}
		this.photonInteract.AddGrabbedEvent(new UnityAction(this.OnGrabbed));
		this.photonInteract.AddPCGrabbedEvent(new UnityAction(this.OnGrabbed));
		if (PhotonNetwork.isMasterClient)
		{
			this.TurnOff();
		}
		if (CCTVController.instance)
		{
			CCTVController.instance.allcctvCameras.Add(this);
		}
		else
		{
			Object.FindObjectOfType<CCTVController>().allcctvCameras.Add(this);
		}
		if (!XRDevice.isPresent && !this.isHeadCamera)
		{
			if (GameController.instance.myPlayer == null)
			{
				GameController.instance.OnLocalPlayerSpawned.AddListener(new UnityAction(this.OnPlayerSpawned));
			}
			else
			{
				this.OnPlayerSpawned();
			}
		}
		if (this.helperObject)
		{
			this.helperObject.SetActive(false);
			this.helperObject.transform.SetParent(null);
		}
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00035460 File Offset: 0x00033660
	public void Use()
	{
		if (this.isHeadCamera)
		{
			return;
		}
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
			return;
		}
		this.NetworkedUse();
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0003548F File Offset: 0x0003368F
	public void TurnOff()
	{
		if (this.IsThisCameraSwitchedOn)
		{
			if (PhotonNetwork.inRoom)
			{
				this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
				return;
			}
			this.NetworkedUse();
		}
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x000354BD File Offset: 0x000336BD
	public void TurnOn()
	{
		if (!this.IsThisCameraSwitchedOn)
		{
			if (PhotonNetwork.inRoom)
			{
				this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
				return;
			}
			this.NetworkedUse();
		}
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x000354EC File Offset: 0x000336EC
	public void SecondaryUse()
	{
		if (this.isFixedCamera)
		{
			return;
		}
		bool flag = false;
		if (!XRDevice.isPresent && GameController.instance.myPlayer.player.pcPropGrab.inventoryProps[GameController.instance.myPlayer.player.pcPropGrab.inventoryIndex] == this.photonInteract)
		{
			this.playerAim = GameController.instance.myPlayer.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(this.playerAim, out raycastHit, 1.6f, this.mask, QueryTriggerInteraction.Ignore) && raycastHit.collider.GetComponent<Tripod>() && !raycastHit.collider.GetComponent<Tripod>().snapZone.GetComponentInChildren<CCTV>())
			{
				GameController.instance.myPlayer.player.pcPropGrab.Drop(false);
				this.view.RPC("PlaceCamera", PhotonTargets.All, new object[]
				{
					raycastHit.collider.GetComponent<Tripod>().GetComponent<PhotonView>().viewID
				});
				flag = true;
			}
			RaycastHit raycastHit2;
			if (!flag && Physics.Raycast(this.playerAim, out raycastHit2, 1.6f, this.helperMask, QueryTriggerInteraction.Ignore))
			{
				if (raycastHit2.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
				{
					return;
				}
				GameController.instance.myPlayer.player.pcPropGrab.Drop(true);
				this.view.RPC("NonVRPlaceCamera", PhotonTargets.All, new object[]
				{
					raycastHit2.point,
					this.helperObject.transform.rotation
				});
			}
		}
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x000356C8 File Offset: 0x000338C8
	[PunRPC]
	private void NonVRPlaceCamera(Vector3 point, Quaternion rot)
	{
		base.transform.SetParent(null);
		this.helperObject.SetActive(false);
		base.transform.position = point;
		base.transform.rotation = rot;
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x000356FC File Offset: 0x000338FC
	[PunRPC]
	private void PlaceCamera(int id)
	{
		Tripod component = PhotonView.Find(id).GetComponent<Tripod>();
		base.GetComponent<Rigidbody>().isKinematic = true;
		base.transform.SetParent(component.snapZone);
		base.transform.localPosition = Vector3.zero;
		base.transform.localRotation = Quaternion.identity;
		this.boxCollider.size = this.startColSize / 2f;
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00035770 File Offset: 0x00033970
	[PunRPC]
	private void NetworkedUse()
	{
		this.IsThisCameraSwitchedOn = !this.IsThisCameraSwitchedOn;
		if (this.IsThisCameraSwitchedOn)
		{
			CCTVController.instance.AddCamera(this);
			if (!this.isHeadCamera)
			{
				if (!this.isFixedCamera)
				{
					for (int i = 0; i < this.rends.Length; i++)
					{
						this.rends[i].material.SetColor("_EmissionColor", Color.green);
					}
					return;
				}
				for (int j = 0; j < this.rends.Length; j++)
				{
					this.rends[j].material.EnableKeyword("_EMISSION");
				}
				return;
			}
		}
		else
		{
			CCTVController.instance.RemoveCamera(this);
			if (!this.isHeadCamera)
			{
				if (!this.isFixedCamera)
				{
					for (int k = 0; k < this.rends.Length; k++)
					{
						this.rends[k].material.SetColor("_EmissionColor", Color.red);
					}
					return;
				}
				for (int l = 0; l < this.rends.Length; l++)
				{
					this.rends[l].material.DisableKeyword("_EMISSION");
				}
			}
		}
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x00035884 File Offset: 0x00033A84
	private void OnGrabbed()
	{
		if (!this.isHeadCamera)
		{
			this.view.RPC("OnGrabbedSync", PhotonTargets.All, Array.Empty<object>());
			return;
		}
		if (XRDevice.isPresent && GameController.instance.myPlayer.player.playerHeadCamera.headCamera == this)
		{
			GameController.instance.myPlayer.player.playerHeadCamera.VRGrabOrPlaceCamera(this.view.viewID, false);
		}
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x000358FD File Offset: 0x00033AFD
	[PunRPC]
	private void OnGrabbedSync()
	{
		this.boxCollider.size = this.startColSize;
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00035910 File Offset: 0x00033B10
	private void Update()
	{
		if (this.isFixedCamera || this.isHeadCamera)
		{
			return;
		}
		if (this.view.isMine)
		{
			if (this.photonInteract.isGrabbed)
			{
				if (!XRDevice.isPresent)
				{
					RaycastHit raycastHit;
					if (Physics.Raycast(GameController.instance.myPlayer.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit, this.grabDistance, this.helperMask, QueryTriggerInteraction.Ignore))
					{
						if (!this.helperObject.activeInHierarchy)
						{
							this.helperObject.SetActive(true);
						}
						if (raycastHit.collider.GetComponent<Tripod>())
						{
							this.helperObject.transform.position = raycastHit.collider.GetComponent<Tripod>().snapZone.transform.position;
							this.helperObject.transform.rotation = raycastHit.collider.GetComponent<Tripod>().snapZone.transform.rotation;
						}
						else
						{
							this.helperObject.transform.position = raycastHit.point;
						}
					}
					else if (this.helperObject.activeInHierarchy)
					{
						this.helperObject.SetActive(false);
					}
					if (this.useKeyIsPressed)
					{
						this.helperObject.transform.Rotate(Vector3.up * Time.deltaTime * 100f);
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

	// Token: 0x060008ED RID: 2285 RVA: 0x00035AA4 File Offset: 0x00033CA4
	private void OnDisable()
	{
		if (this.helperObject)
		{
			this.helperObject.SetActive(false);
		}
		if (!this.isFixedCamera && !this.isHeadCamera && !XRDevice.isPresent && GameController.instance.myPlayer != null && GameController.instance.myPlayer.player != null && GameController.instance.myPlayer.player.playerInput != null)
		{
			GameController.instance.myPlayer.player.playerInput.actions["Interact"].started -= delegate(InputAction.CallbackContext _)
			{
				this.UseKeyPressed();
			};
			GameController.instance.myPlayer.player.playerInput.actions["Interact"].canceled -= delegate(InputAction.CallbackContext _)
			{
				this.UseKeyStopped();
			};
		}
		if (!GameController.instance.isLoadingBackToMenu && CCTVController.instance)
		{
			CCTVController.instance.RemoveCamera(this);
		}
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00035BBC File Offset: 0x00033DBC
	private void OnPlayerSpawned()
	{
		if (!this.isFixedCamera && !this.isHeadCamera && !XRDevice.isPresent)
		{
			GameController.instance.myPlayer.player.playerInput.actions["Interact"].started += delegate(InputAction.CallbackContext _)
			{
				this.UseKeyPressed();
			};
			GameController.instance.myPlayer.player.playerInput.actions["Interact"].canceled += delegate(InputAction.CallbackContext _)
			{
				this.UseKeyStopped();
			};
		}
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00035C48 File Offset: 0x00033E48
	public void UseKeyPressed()
	{
		this.useKeyIsPressed = true;
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00035C51 File Offset: 0x00033E51
	public void UseKeyStopped()
	{
		this.useKeyIsPressed = false;
	}

	// Token: 0x040008F2 RID: 2290
	[HideInInspector]
	public RenderTexture rt;

	// Token: 0x040008F3 RID: 2291
	public Camera cam;

	// Token: 0x040008F4 RID: 2292
	[HideInInspector]
	public bool isThisCameraActiveOnACCTVScreen;

	// Token: 0x040008F5 RID: 2293
	private PhotonObjectInteract photonInteract;

	// Token: 0x040008F6 RID: 2294
	private PhotonView view;

	// Token: 0x040008F7 RID: 2295
	public Renderer[] rends;

	// Token: 0x040008F8 RID: 2296
	private bool IsThisCameraSwitchedOn;

	// Token: 0x040008F9 RID: 2297
	public bool isFixedCamera;

	// Token: 0x040008FA RID: 2298
	public bool isHeadCamera;

	// Token: 0x040008FB RID: 2299
	[SerializeField]
	private Nightvision nightVision;

	// Token: 0x040008FC RID: 2300
	[SerializeField]
	private GameObject helperObject;

	// Token: 0x040008FD RID: 2301
	[HideInInspector]
	public Light myLight;

	// Token: 0x040008FE RID: 2302
	private BoxCollider boxCollider;

	// Token: 0x040008FF RID: 2303
	private Vector3 startColSize;

	// Token: 0x04000900 RID: 2304
	public Transform headCamParent;

	// Token: 0x04000901 RID: 2305
	public Image mapIcon;

	// Token: 0x04000902 RID: 2306
	[SerializeField]
	private LevelRoom.Type floorType = LevelRoom.Type.firstFloor;

	// Token: 0x04000903 RID: 2307
	[Header("PC")]
	private readonly float grabDistance = 3f;

	// Token: 0x04000904 RID: 2308
	private Ray playerAim;

	// Token: 0x04000905 RID: 2309
	[SerializeField]
	private LayerMask mask;

	// Token: 0x04000906 RID: 2310
	[SerializeField]
	private LayerMask helperMask;

	// Token: 0x04000907 RID: 2311
	private bool useKeyIsPressed;
}
