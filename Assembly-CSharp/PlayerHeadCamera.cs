using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using VRTK;

// Token: 0x020001B9 RID: 441
[RequireComponent(typeof(PhotonView))]
public class PlayerHeadCamera : MonoBehaviour
{
	// Token: 0x06000C1C RID: 3100 RVA: 0x0004C0B9 File Offset: 0x0004A2B9
	public void GrabCamera(CCTV headCam)
	{
		this.view.RPC("EquippedCamera", PhotonTargets.All, new object[]
		{
			headCam.GetComponent<PhotonView>().viewID
		});
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x0004C0E8 File Offset: 0x0004A2E8
	private void SecondaryUse()
	{
		if (!XRDevice.isPresent)
		{
			this.playerAim = GameController.instance.myPlayer.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(this.playerAim, out raycastHit, this.grabDistance, this.mask, QueryTriggerInteraction.Ignore) && raycastHit.collider.CompareTag("HeadCameraDropZone") && !raycastHit.collider.GetComponentInChildren<VRTK_SnapDropZone>().GetComponentInChildren<CCTV>())
			{
				this.view.RPC("PlaceCamera", PhotonTargets.AllBuffered, new object[]
				{
					raycastHit.collider.GetComponentInChildren<VRTK_SnapDropZone>().GetComponent<PhotonView>().viewID
				});
			}
		}
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x0004C1B3 File Offset: 0x0004A3B3
	public void DisableCamera()
	{
		if (this.isEquipped)
		{
			this.headCamera.TurnOff();
			this.headDropZone.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x0004C1D9 File Offset: 0x0004A3D9
	public void VRGrabOrPlaceCamera(int viewID, bool isPlaced)
	{
		this.view.RPC("VRGrabOrPlaceCameraNetworked", PhotonTargets.All, new object[]
		{
			viewID,
			isPlaced
		});
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x0004C204 File Offset: 0x0004A404
	[PunRPC]
	private void VRGrabOrPlaceCameraNetworked(int viewID, bool isPlaced)
	{
		if (PhotonView.Find(viewID) == null)
		{
			return;
		}
		CCTV component = PhotonView.Find(viewID).GetComponent<CCTV>();
		if (isPlaced)
		{
			if (this.view.isMine)
			{
				component.TurnOn();
			}
		}
		else if (this.view.isMine)
		{
			component.TurnOff();
		}
		for (int i = 0; i < component.rends.Length; i++)
		{
			component.rends[i].enabled = !isPlaced;
		}
		if (!this.view.isMine)
		{
			for (int j = 0; j < this.headCameraModels.Length; j++)
			{
				this.headCameraModels[j].SetActive(isPlaced);
			}
		}
		this.isEquipped = isPlaced;
		this.headCamera = (isPlaced ? component : null);
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x0004C2C0 File Offset: 0x0004A4C0
	[PunRPC]
	private void PlaceCamera(int id)
	{
		this.headCamera.transform.SetParent(PhotonView.Find(id).transform);
		this.headCamera.transform.localPosition = Vector3.zero;
		this.headCamera.transform.localRotation = Quaternion.identity;
		this.headCamera.cam.transform.SetParent(this.headCamera.headCamParent);
		this.headCamera.cam.transform.localPosition = Vector3.zero;
		this.headCamera.cam.transform.localRotation = Quaternion.identity;
		this.headCamera.GetComponent<Collider>().enabled = true;
		this.headCamera.TurnOff();
		this.headCamera.cam.enabled = false;
		this.headCamera.myLight.enabled = false;
		for (int i = 0; i < this.headCamera.rends.Length; i++)
		{
			this.headCamera.rends[i].enabled = true;
		}
		for (int j = 0; j < this.headCameraModels.Length; j++)
		{
			this.headCameraModels[j].SetActive(false);
		}
		this.headCamera = null;
		this.isEquipped = false;
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x0004C400 File Offset: 0x0004A600
	[PunRPC]
	private void EquippedCamera(int viewID)
	{
		CCTV component = PhotonView.Find(viewID).GetComponent<CCTV>();
		this.isEquipped = true;
		this.headCamera = component;
		this.headCamera.transform.SetParent(this.headDropZone.transform);
		this.headCamera.transform.localPosition = Vector3.zero;
		this.headCamera.transform.localRotation = Quaternion.identity;
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i].actorID == this.view.ownerId)
			{
				this.headCamera.cam.transform.SetParent(this.characterHeadCamSpots[GameController.instance.playersData[i].player.modelID]);
				this.headCamera.cam.transform.localPosition = Vector3.zero;
				this.headCamera.cam.transform.localRotation = Quaternion.identity;
				if (this.view.isMine)
				{
					this.headCamera.cam.transform.SetParent(this.headCameraModels[GameController.instance.playersData[i].player.modelID].transform.parent);
				}
			}
		}
		this.headCamera.GetComponent<Collider>().enabled = false;
		if (this.view.isMine)
		{
			this.headCamera.TurnOn();
		}
		for (int j = 0; j < this.headCamera.rends.Length; j++)
		{
			this.headCamera.rends[j].enabled = false;
		}
		if (!this.view.isMine)
		{
			for (int k = 0; k < this.headCameraModels.Length; k++)
			{
				this.headCameraModels[k].SetActive(true);
			}
		}
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x0004C5EA File Offset: 0x0004A7EA
	public void OnSecondaryUse(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started && this.isEquipped)
		{
			this.SecondaryUse();
		}
	}

	// Token: 0x04000C9E RID: 3230
	[HideInInspector]
	public bool isEquipped;

	// Token: 0x04000C9F RID: 3231
	[HideInInspector]
	public CCTV headCamera;

	// Token: 0x04000CA0 RID: 3232
	[SerializeField]
	private VRTK_SnapDropZone headDropZone;

	// Token: 0x04000CA1 RID: 3233
	[SerializeField]
	private GameObject[] headCameraModels;

	// Token: 0x04000CA2 RID: 3234
	[SerializeField]
	private Transform[] characterHeadCamSpots;

	// Token: 0x04000CA3 RID: 3235
	private readonly float grabDistance = 1.6f;

	// Token: 0x04000CA4 RID: 3236
	private Ray playerAim;

	// Token: 0x04000CA5 RID: 3237
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000CA6 RID: 3238
	[SerializeField]
	private LayerMask mask;
}
