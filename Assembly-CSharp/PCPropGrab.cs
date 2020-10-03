using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VRTK;

// Token: 0x020001B2 RID: 434
public class PCPropGrab : MonoBehaviour
{
	// Token: 0x06000BC7 RID: 3015 RVA: 0x00048CD8 File Offset: 0x00046ED8
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x00048CE8 File Offset: 0x00046EE8
	private void Update()
	{
		this.playerAim = this.playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit raycastHit;
		if (Physics.Raycast(this.playerAim, out raycastHit, 3f, this.mask, QueryTriggerInteraction.Ignore) && raycastHit.collider.GetComponent<Door>())
		{
			if (raycastHit.collider.GetComponent<Door>().locked)
			{
				this.pcCanvas.SetState(PCCanvas.State.locked, false);
				return;
			}
			this.pcCanvas.SetState(PCCanvas.State.active, false);
			return;
		}
		else
		{
			if (!Physics.Raycast(this.playerAim, out raycastHit, this.grabDistance, this.mask, QueryTriggerInteraction.Ignore))
			{
				this.pcCanvas.SetState(PCCanvas.State.none, false);
				return;
			}
			if (raycastHit.collider.GetComponent<PhotonObjectInteract>())
			{
				if (raycastHit.collider.GetComponent<PhotonObjectInteract>().isProp)
				{
					this.pcCanvas.SetState(PCCanvas.State.active, false);
					return;
				}
				if (!raycastHit.collider.GetComponent<PhotonObjectInteract>().isUsable && !raycastHit.collider.GetComponent<PhotonObjectInteract>().isGrabbable)
				{
					this.pcCanvas.SetState(PCCanvas.State.none, false);
					return;
				}
				if (raycastHit.collider.GetComponent<LightSwitch>())
				{
					this.pcCanvas.SetState(PCCanvas.State.light, false);
					return;
				}
				this.pcCanvas.SetState(PCCanvas.State.active, false);
				return;
			}
			else
			{
				if (raycastHit.collider.CompareTag("MainMenuUI"))
				{
					this.pcCanvas.SetState(PCCanvas.State.active, false);
					return;
				}
				this.pcCanvas.SetState(PCCanvas.State.none, false);
				return;
			}
		}
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x00048E76 File Offset: 0x00047076
	public void ControlSchemeChanged()
	{
		if (this.player.playerInput.currentControlScheme != "Keyboard")
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x00048EA0 File Offset: 0x000470A0
	public void ChangeItemSpotWithFOV(float fov)
	{
		float num = 90f - fov;
		num /= 500f;
		num += 0.124f;
		this.cameraItemSpot.localPosition = new Vector3(this.cameraItemSpot.localPosition.x, this.cameraItemSpot.localPosition.y, num);
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x00048EF8 File Offset: 0x000470F8
	private void AttemptGrab()
	{
		if (this.player.isDead)
		{
			return;
		}
		bool flag = false;
		int index = 0;
		for (int i = 0; i < this.inventoryProps.Count; i++)
		{
			if (this.inventoryProps[i] == null)
			{
				flag = true;
				index = i;
				break;
			}
		}
		if (this.inventoryProps[this.inventoryIndex] == null)
		{
			flag = true;
			index = this.inventoryIndex;
		}
		this.playerAim = this.playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit raycastHit;
		if (Physics.Raycast(this.playerAim, out raycastHit, this.grabDistance, this.mask, QueryTriggerInteraction.Ignore) && raycastHit.collider.GetComponent<PhotonObjectInteract>() && raycastHit.collider.GetComponent<PhotonObjectInteract>().isProp)
		{
			if (raycastHit.collider.GetComponent<PhotonObjectInteract>().isGrabbed)
			{
				return;
			}
			if (raycastHit.collider.transform.root.CompareTag("Player"))
			{
				return;
			}
			if (raycastHit.collider.CompareTag("HeadCamera"))
			{
				if (!GameController.instance.myPlayer.player.playerHeadCamera.isEquipped)
				{
					GameController.instance.myPlayer.player.playerHeadCamera.GrabCamera(raycastHit.collider.GetComponent<CCTV>());
					return;
				}
				return;
			}
			else if (flag)
			{
				if (raycastHit.collider.GetComponent<Tripod>() || raycastHit.collider.GetComponent<OuijaBoard>())
				{
					if (this.inventoryProps[this.inventoryIndex] == null)
					{
						this.inventoryProps[this.inventoryIndex] = raycastHit.collider.GetComponent<PhotonObjectInteract>();
						this.Grab(this.inventoryProps[this.inventoryIndex]);
						return;
					}
				}
				else
				{
					if (raycastHit.collider.GetComponent<DNAEvidence>() || raycastHit.collider.GetComponent<global::Key>())
					{
						raycastHit.collider.GetComponent<PhotonObjectInteract>().OnPCGrabbed.Invoke();
						return;
					}
					if (raycastHit.collider.GetComponent<Torch>() && !raycastHit.collider.GetComponent<Torch>().isBlacklight)
					{
						for (int j = 0; j < this.inventoryProps.Count; j++)
						{
							if (this.inventoryProps[j] != null && this.inventoryProps[j].GetComponent<Torch>() && !this.inventoryProps[j].GetComponent<Torch>().isBlacklight)
							{
								return;
							}
						}
						this.pcFlashlight.GrabbedOrDroppedFlashlight(raycastHit.collider.GetComponent<Torch>(), true);
					}
					this.inventoryProps[index] = raycastHit.collider.GetComponent<PhotonObjectInteract>();
					this.Grab(this.inventoryProps[index]);
					return;
				}
			}
			else if (raycastHit.collider.GetComponent<DNAEvidence>() || raycastHit.collider.GetComponent<global::Key>())
			{
				raycastHit.collider.GetComponent<PhotonObjectInteract>().OnPCGrabbed.Invoke();
			}
		}
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x0004922C File Offset: 0x0004742C
	private void Grab(PhotonObjectInteract grabbedItem)
	{
		if (this.player.isDead)
		{
			return;
		}
		if (PhotonNetwork.inRoom)
		{
			grabbedItem.view.RequestOwnership();
		}
		grabbedItem.OnPCGrabbed.Invoke();
		if (grabbedItem != null)
		{
			if (this.inventoryProps[this.inventoryIndex] == grabbedItem)
			{
				if (grabbedItem.myRightHandModel != null)
				{
					grabbedItem.myRightHandModel.SetActive(true);
				}
			}
			else if (PhotonNetwork.inRoom)
			{
				this.view.RPC("EnableOrDisableObject", PhotonTargets.AllBuffered, new object[]
				{
					grabbedItem.view.viewID,
					false
				});
			}
			else
			{
				this.EnableOrDisableObject(grabbedItem.view.viewID, false);
			}
			Collider[] components = grabbedItem.GetComponents<Collider>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].enabled = false;
			}
			this.player.charAnim.SetTrigger("SwitchHolding");
			this.player.charAnim.SetBool("isHolding", true);
			if (PhotonNetwork.inRoom)
			{
				this.view.RPC("NetworkedGrab", PhotonTargets.AllBuffered, new object[]
				{
					grabbedItem.view.viewID
				});
			}
			Rigidbody component = grabbedItem.GetComponent<Rigidbody>();
			this.grabSpotJoint.connectedBody = component;
			component.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
			component.isKinematic = true;
			grabbedItem.transform.SetParent(this.cameraItemSpot);
			if (grabbedItem.GetComponent<Tripod>())
			{
				grabbedItem.transform.localPosition = new Vector3(0f, -0.6f, 0f);
			}
			else
			{
				grabbedItem.transform.localPosition = grabbedItem.localPlayerPosition;
			}
			Quaternion localRotation = grabbedItem.transform.localRotation;
			localRotation.eulerAngles = grabbedItem.localPlayerRotation;
			grabbedItem.transform.localRotation = localRotation;
		}
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x00049404 File Offset: 0x00047604
	[PunRPC]
	private void NetworkedGrab(int id)
	{
		if (PhotonView.Find(id) == null)
		{
			return;
		}
		PhotonObjectInteract component = PhotonView.Find(id).GetComponent<PhotonObjectInteract>();
		if (component.transformView)
		{
			component.transformView.m_RotationModel.SynchronizeEnabled = false;
			component.transformView.m_PositionModel.SynchronizeEnabled = false;
		}
		if (!this.view.isMine)
		{
			this.inventoryProps[this.inventoryIndex] = component;
		}
		component.isGrabbed = true;
		component.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
		component.GetComponent<Rigidbody>().isKinematic = true;
		component.transform.SetParent(this.grabSpotJoint.transform);
		if (component.GetComponent<Tripod>())
		{
			component.transform.localPosition = new Vector3(0f, -0.9f, 0f);
		}
		else
		{
			component.transform.localPosition = Vector3.zero;
		}
		Quaternion localRotation = component.transform.localRotation;
		localRotation.eulerAngles = component.localPlayerRotation;
		component.transform.localRotation = localRotation;
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x00049514 File Offset: 0x00047714
	[PunRPC]
	private void NetworkedUnGrab(int id, string itemName)
	{
		if (PhotonView.Find(id) == null)
		{
			return;
		}
		PhotonObjectInteract component = PhotonView.Find(id).GetComponent<PhotonObjectInteract>();
		if (component.transformView)
		{
			component.transformView.m_RotationModel.SynchronizeEnabled = true;
			component.transformView.m_PositionModel.SynchronizeEnabled = true;
		}
		component.isGrabbed = false;
		if (PhotonNetwork.inRoom && !this.view.isMine)
		{
			this.inventoryProps[this.inventoryIndex] = null;
		}
		if (this.view.isMine || !PhotonNetwork.inRoom)
		{
			component.GetComponent<Rigidbody>().isKinematic = component.wasKinematic;
		}
		component.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
		component.transform.SetParent(null);
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x000495D8 File Offset: 0x000477D8
	public void Drop(bool resetRigid)
	{
		if (this.inventoryProps[this.inventoryIndex] == null)
		{
			return;
		}
		if (this.inventoryProps[this.inventoryIndex].GetComponent<Torch>() && !this.inventoryProps[this.inventoryIndex].GetComponent<Torch>().isBlacklight)
		{
			this.pcFlashlight.GrabbedOrDroppedFlashlight(this.inventoryProps[this.inventoryIndex].GetComponent<Torch>(), false);
		}
		this.player.charAnim.SetTrigger("SwitchHolding");
		this.player.charAnim.SetBool("isHolding", false);
		this.player.currentHeldObject = null;
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("NetworkedUnGrab", PhotonTargets.AllBuffered, new object[]
			{
				this.inventoryProps[this.inventoryIndex].view.viewID,
				this.inventoryProps[this.inventoryIndex].gameObject.name
			});
		}
		else
		{
			this.NetworkedUnGrab(this.inventoryProps[this.inventoryIndex].view.viewID, this.inventoryProps[this.inventoryIndex].gameObject.name);
		}
		if (this.inventoryProps[this.inventoryIndex].myRightHandModel)
		{
			this.inventoryProps[this.inventoryIndex].myRightHandModel.SetActive(false);
		}
		this.inventoryProps[this.inventoryIndex].transform.SetParent(null);
		this.grabSpotJoint.connectedBody = null;
		this.inventoryProps[this.inventoryIndex].transform.position = this.player.cam.transform.position + -this.player.cam.transform.up * 0.05f + this.player.cam.transform.forward * 0.15f;
		if (resetRigid && !this.inventoryProps[this.inventoryIndex].GetComponent<Tripod>())
		{
			this.inventoryProps[this.inventoryIndex].GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
			this.inventoryProps[this.inventoryIndex].GetComponent<Rigidbody>().useGravity = this.inventoryProps[this.inventoryIndex].wasGravity;
			this.inventoryProps[this.inventoryIndex].GetComponent<Rigidbody>().isKinematic = this.inventoryProps[this.inventoryIndex].wasKinematic;
			if (this.inventoryProps[this.inventoryIndex].wasGravity)
			{
				this.inventoryProps[this.inventoryIndex].GetComponent<Rigidbody>().AddForce(this.player.cam.transform.forward * 3f, ForceMode.Impulse);
			}
		}
		this.inventoryProps[this.inventoryIndex].OnPCUnGrabbed.Invoke();
		Collider[] components = this.inventoryProps[this.inventoryIndex].GetComponents<Collider>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].enabled = true;
		}
		this.inventoryProps[this.inventoryIndex] = null;
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x00049960 File Offset: 0x00047B60
	private void AttemptUse()
	{
		if (this.player.isDead)
		{
			return;
		}
		this.playerAim = this.playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit raycastHit;
		if (Physics.Raycast(this.playerAim, out raycastHit, this.grabDistance, this.mask, QueryTriggerInteraction.Ignore) && raycastHit.collider.GetComponent<PhotonObjectInteract>() != null && raycastHit.collider.GetComponent<PhotonObjectInteract>() && !raycastHit.collider.GetComponent<PhotonObjectInteract>().useOnlyIfGrabbed)
		{
			raycastHit.collider.GetComponent<PhotonObjectInteract>().OnUse.Invoke();
		}
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x00049A14 File Offset: 0x00047C14
	public void SwitchHand(int modifier)
	{
		if (this.inventoryProps[this.inventoryIndex])
		{
			if (this.inventoryProps[this.inventoryIndex].GetComponent<CCTV>())
			{
				this.inventoryProps[this.inventoryIndex].GetComponent<CCTV>().TurnOff();
			}
			else if (this.inventoryProps[this.inventoryIndex].GetComponent<EVPRecorder>())
			{
				this.inventoryProps[this.inventoryIndex].GetComponent<EVPRecorder>().TurnOff();
			}
			else if (this.inventoryProps[this.inventoryIndex].GetComponent<EMFReader>())
			{
				if (this.inventoryProps[this.inventoryIndex].GetComponent<EMFReader>().isOn)
				{
					this.inventoryProps[this.inventoryIndex].GetComponent<EMFReader>().Use();
				}
			}
			else if (this.inventoryProps[this.inventoryIndex].GetComponent<Tripod>())
			{
				this.Drop(true);
			}
			else if (this.inventoryProps[this.inventoryIndex].GetComponent<OuijaBoard>())
			{
				this.Drop(true);
			}
		}
		int index = this.inventoryIndex;
		this.inventoryIndex += modifier;
		if (this.inventoryIndex > this.inventoryProps.Count - 1)
		{
			this.inventoryIndex = 0;
		}
		else if (this.inventoryIndex < 0)
		{
			this.inventoryIndex = this.inventoryProps.Count - 1;
		}
		if (this.inventoryProps[this.inventoryIndex])
		{
			PhotonObjectInteract photonObjectInteract = this.inventoryProps[this.inventoryIndex];
		}
		if (this.inventoryProps[index])
		{
			if (PhotonNetwork.inRoom)
			{
				this.view.RPC("EnableOrDisableObject", PhotonTargets.AllBuffered, new object[]
				{
					this.inventoryProps[index].view.viewID,
					false
				});
			}
			else
			{
				this.EnableOrDisableObject(this.inventoryProps[index].view.viewID, false);
			}
		}
		if (this.inventoryProps[this.inventoryIndex])
		{
			if (PhotonNetwork.inRoom)
			{
				this.view.RPC("EnableOrDisableObject", PhotonTargets.AllBuffered, new object[]
				{
					this.inventoryProps[this.inventoryIndex].view.viewID,
					true
				});
			}
			else
			{
				this.EnableOrDisableObject(this.inventoryProps[this.inventoryIndex].view.viewID, true);
			}
			this.Grab(this.inventoryProps[this.inventoryIndex]);
			this.player.currentHeldObject = this.inventoryProps[this.inventoryIndex];
			if (this.inventoryProps[this.inventoryIndex].GetComponent<Torch>())
			{
				this.pcFlashlight.EnableOrDisableLight(false, true);
				return;
			}
		}
		else
		{
			this.player.currentHeldObject = null;
		}
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x00049D3A File Offset: 0x00047F3A
	[PunRPC]
	private void EnableOrDisableObject(int id, bool enable)
	{
		if (PhotonView.Find(id) != null)
		{
			PhotonView.Find(id).gameObject.SetActive(enable);
		}
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x00049D5B File Offset: 0x00047F5B
	private void OnDisable()
	{
		if (!this.view.isMine)
		{
			return;
		}
		this.DropAllInventoryProps();
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x00049D74 File Offset: 0x00047F74
	public void DropAllInventoryProps()
	{
		this.Drop(true);
		this.player.currentHeldObject = null;
		for (int i = 0; i < this.inventoryProps.Count; i++)
		{
			if (this.inventoryProps[i] != null && this.inventoryProps[i].GetComponent<Torch>() && !this.inventoryProps[i].GetComponent<Torch>().isBlacklight)
			{
				this.pcFlashlight.GrabbedOrDroppedFlashlight(this.inventoryProps[i].GetComponent<Torch>(), false);
			}
		}
		for (int j = 0; j < this.inventoryProps.Count; j++)
		{
			if (this.inventoryProps[j] != null)
			{
				this.inventoryProps[j].gameObject.SetActive(true);
				if (PhotonNetwork.inRoom)
				{
					this.view.RPC("NetworkedUnGrab", PhotonTargets.AllBuffered, new object[]
					{
						this.inventoryProps[j].view.viewID,
						this.inventoryProps[j].gameObject.name
					});
				}
				else
				{
					this.NetworkedUnGrab(this.inventoryProps[j].view.viewID, this.inventoryProps[j].gameObject.name);
				}
				if (this.inventoryProps[j].myRightHandModel)
				{
					this.inventoryProps[j].myRightHandModel.SetActive(false);
				}
				this.inventoryProps[j].transform.SetParent(null);
				if (this.grabSpotJoint)
				{
					this.grabSpotJoint.connectedBody = null;
				}
				if (!this.inventoryProps[j].GetComponent<Tripod>())
				{
					this.inventoryProps[j].GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
					this.inventoryProps[j].GetComponent<Rigidbody>().useGravity = this.inventoryProps[j].wasGravity;
					this.inventoryProps[j].GetComponent<Rigidbody>().isKinematic = this.inventoryProps[j].wasKinematic;
					if (this.inventoryProps[j].wasGravity)
					{
						this.inventoryProps[j].GetComponent<Rigidbody>().AddForce(this.player.cam.transform.forward * 3f, ForceMode.Impulse);
					}
				}
				this.inventoryProps[j].GetComponent<Collider>().enabled = true;
				this.inventoryProps[j] = null;
			}
		}
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x0004A02C File Offset: 0x0004822C
	public void OnPickup(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed && !this.player.isDead)
		{
			this.playerAim = this.playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(this.playerAim, out raycastHit, this.grabDistance, this.mask, QueryTriggerInteraction.Ignore) && raycastHit.collider.GetComponent<PhotonObjectInteract>() && raycastHit.collider.GetComponent<PhotonObjectInteract>().isProp)
			{
				this.AttemptGrab();
			}
		}
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x0004A0C4 File Offset: 0x000482C4
	public void OnDrop(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed && this.inventoryProps[this.inventoryIndex] != null)
		{
			if (this.inventoryProps[this.inventoryIndex].validDrop == VRTK_InteractableObject.ValidDropTypes.DropValidSnapDropZone)
			{
				return;
			}
			this.Drop(true);
		}
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x0004A115 File Offset: 0x00048315
	public void OnInteract(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			if (this.pcCanvas.isPaused)
			{
				return;
			}
			if (LevelController.instance && LevelController.instance.journalController.isOpen)
			{
				return;
			}
			this.AttemptUse();
		}
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x0004A154 File Offset: 0x00048354
	public void OnPrimaryUse(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started && this.inventoryProps[this.inventoryIndex] != null)
		{
			this.inventoryProps[this.inventoryIndex].OnUse.Invoke();
		}
		if (context.phase == InputActionPhase.Canceled && this.inventoryProps[this.inventoryIndex] != null)
		{
			this.inventoryProps[this.inventoryIndex].OnPCStopUse.Invoke();
		}
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x0004A1DD File Offset: 0x000483DD
	public void OnSecondaryUse(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started && this.inventoryProps[this.inventoryIndex] != null)
		{
			this.inventoryProps[this.inventoryIndex].OnPCSecondaryUse.Invoke();
		}
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x0004A21D File Offset: 0x0004841D
	public void OnInventorySwap(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			if (this.pcCanvas.isPaused)
			{
				return;
			}
			if (LevelController.instance && LevelController.instance.journalController.isOpen)
			{
				return;
			}
			this.SwitchHand(1);
		}
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x0004A25C File Offset: 0x0004845C
	public void OnInventorySwapScroll(InputAction.CallbackContext context)
	{
		if (this.pcCanvas.isPaused)
		{
			return;
		}
		if (!this.canSwap)
		{
			return;
		}
		base.StopCoroutine(this.SwapTimer());
		base.StartCoroutine(this.SwapTimer());
		if (LevelController.instance && LevelController.instance.journalController.isOpen)
		{
			return;
		}
		Vector2 vector = context.ReadValue<Vector2>();
		if (vector.y > 0f)
		{
			this.SwitchHand(1);
			return;
		}
		if (vector.y < 0f)
		{
			this.SwitchHand(-1);
		}
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x0004A2E8 File Offset: 0x000484E8
	private IEnumerator SwapTimer()
	{
		this.canSwap = false;
		yield return new WaitForSeconds(0.5f);
		this.canSwap = true;
		yield break;
	}

	// Token: 0x04000C14 RID: 3092
	private float grabDistance = 1.6f;

	// Token: 0x04000C15 RID: 3093
	private Ray playerAim;

	// Token: 0x04000C16 RID: 3094
	private PhotonView view;

	// Token: 0x04000C17 RID: 3095
	[SerializeField]
	private Player player;

	// Token: 0x04000C18 RID: 3096
	[SerializeField]
	private PCCanvas pcCanvas;

	// Token: 0x04000C19 RID: 3097
	[SerializeField]
	private PCFlashlight pcFlashlight;

	// Token: 0x04000C1A RID: 3098
	public List<PhotonObjectInteract> inventoryProps = new List<PhotonObjectInteract>();

	// Token: 0x04000C1B RID: 3099
	[HideInInspector]
	public int inventoryIndex;

	// Token: 0x04000C1C RID: 3100
	[SerializeField]
	private Camera playerCam;

	// Token: 0x04000C1D RID: 3101
	[SerializeField]
	private LayerMask mask;

	// Token: 0x04000C1E RID: 3102
	[HideInInspector]
	public FixedJoint grabSpotJoint;

	// Token: 0x04000C1F RID: 3103
	public Transform cameraItemSpot;

	// Token: 0x04000C20 RID: 3104
	private bool canSwap = true;
}
