using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x020001A9 RID: 425
public class DragRigidbodyUse : MonoBehaviour
{
	// Token: 0x06000B87 RID: 2951 RVA: 0x00047038 File Offset: 0x00045238
	private void Awake()
	{
		this.isObjectHeld = false;
		this.tryPickupObject = false;
		this.objectHeld = null;
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x0004704F File Offset: 0x0004524F
	private void FixedUpdate()
	{
		if (!this.interactKeyIsPressed)
		{
			if (this.isObjectHeld)
			{
				this.DropObject();
			}
			return;
		}
		if (!this.isObjectHeld)
		{
			this.TryPickObject();
			this.tryPickupObject = true;
			return;
		}
		this.HoldObject();
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x00047084 File Offset: 0x00045284
	private void TryOpenDoor()
	{
		if (this.player.isDead)
		{
			return;
		}
		this.playerAim = this.playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit raycastHit;
		if (Physics.Raycast(this.playerAim, out raycastHit, this.maxDistanceGrab, this.mask) && raycastHit.collider.CompareTag("Door") && raycastHit.collider.GetComponent<Door>() && raycastHit.collider.GetComponent<Door>().locked)
		{
			if (!LevelController.instance.currentGhost.isHunting)
			{
				for (int i = 0; i < this.player.keys.Count; i++)
				{
					if (this.player.keys[i] == raycastHit.collider.GetComponent<Door>().type)
					{
						raycastHit.collider.GetComponent<Door>().UnlockDoor();
					}
				}
			}
			if (raycastHit.collider.GetComponent<Door>().locked)
			{
				raycastHit.collider.GetComponent<Door>().PlayLockedSound();
			}
		}
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x000471B0 File Offset: 0x000453B0
	private void TryPickObject()
	{
		if (this.player.isDead)
		{
			return;
		}
		this.playerAim = this.playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit raycastHit;
		if (Physics.Raycast(this.playerAim, out raycastHit, this.maxDistanceGrab, this.mask) && raycastHit.collider.CompareTag("Door") && this.tryPickupObject)
		{
			this.objectHeld = raycastHit.collider.gameObject;
			if (this.objectHeld.GetComponent<Door>())
			{
				if (this.objectHeld.GetComponent<PhotonObjectInteract>().isGrabbed)
				{
					return;
				}
				if (this.objectHeld.GetComponent<Door>().locked || !this.objectHeld.GetComponent<Door>().canBeGrabbed)
				{
					return;
				}
				this.objectHeld.GetComponent<Door>().GrabbedDoor();
			}
			else if (this.objectHeld.GetComponent<Drawer>())
			{
				this.objectHeld.GetComponent<Drawer>().Grab();
			}
			if (PhotonNetwork.inRoom)
			{
				this.objectHeld.GetComponent<PhotonView>().RequestOwnership();
			}
			this.isObjectHeld = true;
			this.wasKinematic = this.objectHeld.GetComponent<Rigidbody>().isKinematic;
			this.wasGravity = this.objectHeld.GetComponent<Rigidbody>().useGravity;
			this.objectHeld.GetComponent<Rigidbody>().useGravity = true;
			this.objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
			this.objectHeld.GetComponent<Rigidbody>().isKinematic = false;
		}
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x00047344 File Offset: 0x00045544
	private void HoldObject()
	{
		this.playerAim = this.playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		Vector3 a = this.playerCam.transform.position + this.playerAim.direction * this.distance;
		Vector3 position = this.objectHeld.transform.position;
		this.objectHeld.GetComponent<Rigidbody>().velocity = (a - position) * 10f;
		if (Vector3.Distance(this.objectHeld.transform.position, this.playerCam.transform.position) > this.maxDistanceGrab)
		{
			this.DropObject();
		}
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x00047408 File Offset: 0x00045608
	public void DropObject()
	{
		this.isObjectHeld = false;
		this.tryPickupObject = false;
		this.objectHeld.GetComponent<Rigidbody>().useGravity = this.wasGravity;
		this.objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
		this.objectHeld.GetComponent<Rigidbody>().isKinematic = this.wasKinematic;
		if (this.objectHeld.GetComponent<Door>())
		{
			this.objectHeld.GetComponent<Door>().UnGrabbedDoor();
		}
		else if (this.objectHeld.GetComponent<Drawer>())
		{
			this.objectHeld.GetComponent<Drawer>().UnGrab();
		}
		this.objectHeld = null;
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x000474B0 File Offset: 0x000456B0
	public void OnInteract(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			if (this.player.pcCanvas.isPaused)
			{
				return;
			}
			if (LevelController.instance && LevelController.instance.journalController.isOpen)
			{
				return;
			}
			this.interactKeyIsPressed = true;
		}
		if (context.phase == InputActionPhase.Performed)
		{
			if (this.player.pcCanvas.isPaused)
			{
				return;
			}
			if (LevelController.instance && LevelController.instance.journalController.isOpen)
			{
				return;
			}
			this.TryOpenDoor();
		}
		if (context.phase == InputActionPhase.Canceled)
		{
			this.interactKeyIsPressed = false;
			if (this.isObjectHeld)
			{
				this.DropObject();
			}
			this.tryPickupObject = false;
		}
	}

	// Token: 0x04000BCD RID: 3021
	[SerializeField]
	private Camera playerCam;

	// Token: 0x04000BCE RID: 3022
	private float distance = 1f;

	// Token: 0x04000BCF RID: 3023
	private float maxDistanceGrab = 3f;

	// Token: 0x04000BD0 RID: 3024
	private Ray playerAim;

	// Token: 0x04000BD1 RID: 3025
	[HideInInspector]
	public GameObject objectHeld;

	// Token: 0x04000BD2 RID: 3026
	private bool isObjectHeld;

	// Token: 0x04000BD3 RID: 3027
	private bool tryPickupObject;

	// Token: 0x04000BD4 RID: 3028
	private bool wasKinematic;

	// Token: 0x04000BD5 RID: 3029
	private bool wasGravity;

	// Token: 0x04000BD6 RID: 3030
	[SerializeField]
	private LayerMask mask;

	// Token: 0x04000BD7 RID: 3031
	[SerializeField]
	private Player player;

	// Token: 0x04000BD8 RID: 3032
	private bool interactKeyIsPressed;
}
