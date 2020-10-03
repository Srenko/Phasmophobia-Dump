using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using VRTK;

// Token: 0x020001A8 RID: 424
[RequireComponent(typeof(PhotonView))]
public class PhotonObjectInteract : VRTK_InteractableObject
{
	// Token: 0x06000B6F RID: 2927 RVA: 0x00046603 File Offset: 0x00044803
	public override void StartUsing(VRTK_InteractUse usingObject)
	{
		if (!this.isGrabbable)
		{
			base.StartUsing(usingObject);
			if (!this.view.isMine)
			{
				this.view.RequestOwnership();
			}
			this.OnUse.Invoke();
		}
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00046637 File Offset: 0x00044837
	public override void StopUsing(VRTK_InteractUse previousUsingObject = null)
	{
		base.StopUsing(previousUsingObject);
		this.OnStopUse.Invoke();
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x0004664C File Offset: 0x0004484C
	public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
	{
		if (this.isGrabbed)
		{
			return;
		}
		if (base.transform.root.CompareTag("Player") && !this.view.isMine)
		{
			return;
		}
		if (base.GetComponent<Door>())
		{
			if (base.GetComponent<Door>().locked)
			{
				base.GetComponent<Door>().AttemptToUnlockDoor();
				return;
			}
			this.isGrabbed = true;
			if (!this.isGrabbed)
			{
				return;
			}
		}
		else
		{
			this.isGrabbed = true;
		}
		if (PhotonNetwork.inRoom)
		{
			if (this.isProp)
			{
				if (!this.view.isMine)
				{
					this.transformView.m_RotationModel.SynchronizeEnabled = false;
					this.transformView.m_PositionModel.SynchronizeEnabled = false;
					this.view.RequestOwnership();
					base.StartCoroutine(this.OwnershipDelay());
				}
			}
			else if (base.gameObject.CompareTag("Door") && !this.view.isMine)
			{
				this.view.RequestOwnership();
			}
		}
		base.Grabbed(currentGrabbingObject);
		this._currentGrabbingObject = currentGrabbingObject;
		this.OnGrabbed.Invoke();
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("NetworkedGrab", PhotonTargets.AllBuffered, new object[]
			{
				currentGrabbingObject.transform.parent.GetComponent<PhotonView>().viewID
			});
		}
		else if (this.isProp)
		{
			if (!this.isFixedItem)
			{
				base.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
				base.GetComponent<Rigidbody>().isKinematic = false;
			}
			base.transform.SetParent(currentGrabbingObject.transform.transform);
		}
		if (this.isProp)
		{
			if (this.RightHandModel == null)
			{
				foreach (VRTK_InteractGrab vrtk_InteractGrab in Object.FindObjectsOfType<VRTK_InteractGrab>())
				{
					if (vrtk_InteractGrab.isRightHand)
					{
						this.RightHandModel = vrtk_InteractGrab.transform.Find("vr_male_hand right").gameObject;
					}
				}
			}
			if (this.leftHandModel == null)
			{
				foreach (VRTK_InteractGrab vrtk_InteractGrab2 in Object.FindObjectsOfType<VRTK_InteractGrab>())
				{
					if (!vrtk_InteractGrab2.isRightHand)
					{
						this.leftHandModel = vrtk_InteractGrab2.transform.Find("vr_male_hand left").gameObject;
					}
				}
			}
		}
		if (currentGrabbingObject.isRightHand)
		{
			if (this.myRightHandModel == null || this.RightHandModel == null)
			{
				return;
			}
			this.RightHandModel.SetActive(false);
			this.myRightHandModel.SetActive(true);
			return;
		}
		else
		{
			if (this.myLeftHandModel == null || this.leftHandModel == null)
			{
				return;
			}
			this.leftHandModel.SetActive(false);
			this.myLeftHandModel.SetActive(true);
			return;
		}
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x000468EC File Offset: 0x00044AEC
	public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
	{
		this.isGrabbed = false;
		base.Ungrabbed(previousGrabbingObject);
		this.OnUnGrabbed.Invoke();
		this._currentGrabbingObject = null;
		if (base.GetComponent<Door>())
		{
			base.GetComponent<Door>().UnGrabbedDoor();
		}
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("NetworkedUnGrab", PhotonTargets.AllBuffered, Array.Empty<object>());
		}
		else if (this.isProp && !this.isFixedItem)
		{
			base.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
			base.GetComponent<Rigidbody>().isKinematic = false;
			base.transform.SetParent(null);
		}
		if (previousGrabbingObject.isRightHand)
		{
			if (this.myRightHandModel == null || this.RightHandModel == null)
			{
				return;
			}
			this.RightHandModel.SetActive(true);
			this.myRightHandModel.SetActive(false);
			return;
		}
		else
		{
			if (this.myLeftHandModel == null || this.leftHandModel == null)
			{
				return;
			}
			this.leftHandModel.SetActive(true);
			this.myLeftHandModel.SetActive(false);
			return;
		}
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x000469F8 File Offset: 0x00044BF8
	[PunRPC]
	private void NetworkedGrab(int viewID)
	{
		this.isGrabbed = true;
		if (!this.isProp)
		{
			return;
		}
		if (PhotonView.Find(viewID) == null)
		{
			return;
		}
		if (PhotonView.Find(viewID).isMine)
		{
			if (!this.isFixedItem)
			{
				base.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
				base.GetComponent<Rigidbody>().isKinematic = false;
			}
		}
		else
		{
			base.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
			base.GetComponent<Rigidbody>().isKinematic = true;
		}
		if (PhotonView.Find(viewID) != null)
		{
			base.transform.SetParent(PhotonView.Find(viewID).transform);
		}
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x00046A90 File Offset: 0x00044C90
	[PunRPC]
	private void NetworkedUnGrab()
	{
		this.isGrabbed = false;
		if (!this.isProp)
		{
			if (PhotonNetwork.isMasterClient && !this.view.isMine && base.GetComponent<Door>())
			{
				this.view.RequestOwnership();
			}
			return;
		}
		if (this.view.isMine && !this.isFixedItem)
		{
			base.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
			base.GetComponent<Rigidbody>().isKinematic = false;
		}
		if (!this.isFixedItem)
		{
			base.transform.SetParent(null);
		}
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x00046B1A File Offset: 0x00044D1A
	private IEnumerator OwnershipDelay()
	{
		yield return new WaitUntil(() => this.view.isMine);
		this.transformView.m_RotationModel.SynchronizeEnabled = true;
		this.transformView.m_PositionModel.SynchronizeEnabled = true;
		yield break;
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x00046B2C File Offset: 0x00044D2C
	private new void Awake()
	{
		base.Awake();
		this.holdButtonToGrab = true;
		this.stayGrabbedOnTeleport = true;
		if (this.view == null)
		{
			this.view = base.GetComponent<PhotonView>();
		}
		if (base.GetComponent<PhotonTransformView>())
		{
			this.transformView = base.GetComponent<PhotonTransformView>();
		}
		this.spawnPoint = base.transform.position;
		this.wasGravity = base.GetComponent<Rigidbody>().useGravity;
		this.wasKinematic = base.GetComponent<Rigidbody>().isKinematic;
		if (this.isDraw)
		{
			this.drawer = base.GetComponent<Drawer>();
		}
		if ((base.gameObject.CompareTag("Item") || base.gameObject.CompareTag("DSLR")) && !this.isFixedItem)
		{
			base.transform.SetParent(null);
		}
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x00046BFF File Offset: 0x00044DFF
	private void Start()
	{
		if (this.view.ownershipTransfer != OwnershipOption.Takeover)
		{
			this.view.ownershipTransfer = OwnershipOption.Takeover;
		}
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x00046C1C File Offset: 0x00044E1C
	private new void Update()
	{
		base.Update();
		if (this.isGrabbed && this.isUsable)
		{
			if (!this.hasUsed)
			{
				if (this._currentGrabbingObject)
				{
					if (this._currentGrabbingObject.isRightHand)
					{
						if (GameController.instance)
						{
							if (GameController.instance.myPlayer.player.rightHandInteractUse.IsUseButtonPressed())
							{
								this.OnUse.Invoke();
								this.hasUsed = true;
								return;
							}
						}
						else if (MainManager.instance.localPlayer.rightHandInteractUse.IsUseButtonPressed())
						{
							this.OnUse.Invoke();
							this.hasUsed = true;
							return;
						}
					}
					else if (GameController.instance)
					{
						if (GameController.instance.myPlayer.player.leftHandInteractUse.IsUseButtonPressed())
						{
							this.OnUse.Invoke();
							this.hasUsed = true;
							return;
						}
					}
					else if (MainManager.instance.localPlayer.leftHandInteractUse.IsUseButtonPressed())
					{
						this.OnUse.Invoke();
						this.hasUsed = true;
						return;
					}
				}
			}
			else if (this._currentGrabbingObject)
			{
				if (this._currentGrabbingObject.isRightHand)
				{
					if (GameController.instance)
					{
						if (!GameController.instance.myPlayer.player.rightHandInteractUse.IsUseButtonPressed())
						{
							this.OnStopUse.Invoke();
							this.hasUsed = false;
							return;
						}
					}
					else if (!MainManager.instance.localPlayer.rightHandInteractUse.IsUseButtonPressed())
					{
						this.OnStopUse.Invoke();
						this.hasUsed = false;
						return;
					}
				}
				else if (GameController.instance)
				{
					if (!GameController.instance.myPlayer.player.leftHandInteractUse.IsUseButtonPressed())
					{
						this.OnStopUse.Invoke();
						this.hasUsed = false;
						return;
					}
				}
				else if (!MainManager.instance.localPlayer.leftHandInteractUse.IsUseButtonPressed())
				{
					this.OnStopUse.Invoke();
					this.hasUsed = false;
				}
			}
		}
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x00046E24 File Offset: 0x00045024
	public override void OnEnable()
	{
		if (this.isGrabbed)
		{
			return;
		}
		if (base.transform.root.CompareTag("Player") && this.isProp)
		{
			return;
		}
		base.OnEnable();
		if (this.drawer != null)
		{
			this.drawer.enabled = true;
		}
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x00046E7C File Offset: 0x0004507C
	public void ActivateHands()
	{
		if (this._currentGrabbingObject.isRightHand)
		{
			if (this.RightHandModel != null)
			{
				this.RightHandModel.SetActive(true);
				return;
			}
		}
		else if (this.leftHandModel != null)
		{
			this.leftHandModel.SetActive(true);
		}
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x00046ECC File Offset: 0x000450CC
	public override void OnDisable()
	{
		if (this.isGrabbed)
		{
			return;
		}
		if (base.transform.root.CompareTag("Player") && this.isProp)
		{
			return;
		}
		base.OnDisable();
		if (this.drawer != null)
		{
			this.drawer.enabled = false;
		}
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x00046F22 File Offset: 0x00045122
	public void AddUseEvent(UnityAction action)
	{
		this.OnUse.AddListener(action);
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x00046F30 File Offset: 0x00045130
	public void AddStopEvent(UnityAction action)
	{
		this.OnStopUse.AddListener(action);
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x00046F3E File Offset: 0x0004513E
	public void AddGrabbedEvent(UnityAction action)
	{
		this.OnGrabbed.AddListener(action);
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x00046F4C File Offset: 0x0004514C
	public void AddUnGrabbedEvent(UnityAction action)
	{
		this.OnUnGrabbed.AddListener(action);
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x00046F5A File Offset: 0x0004515A
	public void AddPCGrabbedEvent(UnityAction action)
	{
		this.OnPCGrabbed.AddListener(action);
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x00046F68 File Offset: 0x00045168
	public void AddPCUnGrabbedEvent(UnityAction action)
	{
		this.OnPCUnGrabbed.AddListener(action);
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x00046F76 File Offset: 0x00045176
	public void AddPCStopUseEvent(UnityAction action)
	{
		this.OnPCStopUse.AddListener(action);
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x00046F84 File Offset: 0x00045184
	public void AddPCSecondaryUseEvent(UnityAction action)
	{
		this.OnPCSecondaryUse.AddListener(action);
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x00046F92 File Offset: 0x00045192
	private void OnDestroy()
	{
		this.OnUse.RemoveAllListeners();
		this.OnStopUse.RemoveAllListeners();
		this.OnGrabbed.RemoveAllListeners();
		this.OnUnGrabbed.RemoveAllListeners();
	}

	// Token: 0x04000BB3 RID: 2995
	public PhotonView view;

	// Token: 0x04000BB4 RID: 2996
	[HideInInspector]
	public PhotonTransformView transformView;

	// Token: 0x04000BB5 RID: 2997
	public bool isDraw;

	// Token: 0x04000BB6 RID: 2998
	public bool isProp;

	// Token: 0x04000BB7 RID: 2999
	private Drawer drawer;

	// Token: 0x04000BB8 RID: 3000
	[HideInInspector]
	public UnityEvent OnUse = new UnityEvent();

	// Token: 0x04000BB9 RID: 3001
	private UnityEvent OnStopUse = new UnityEvent();

	// Token: 0x04000BBA RID: 3002
	private UnityEvent OnGrabbed = new UnityEvent();

	// Token: 0x04000BBB RID: 3003
	private UnityEvent OnUnGrabbed = new UnityEvent();

	// Token: 0x04000BBC RID: 3004
	[HideInInspector]
	public UnityEvent OnPCGrabbed = new UnityEvent();

	// Token: 0x04000BBD RID: 3005
	[HideInInspector]
	public UnityEvent OnPCUnGrabbed = new UnityEvent();

	// Token: 0x04000BBE RID: 3006
	[HideInInspector]
	public UnityEvent OnPCStopUse = new UnityEvent();

	// Token: 0x04000BBF RID: 3007
	[HideInInspector]
	public UnityEvent OnPCSecondaryUse = new UnityEvent();

	// Token: 0x04000BC0 RID: 3008
	public bool isGrabbed;

	// Token: 0x04000BC1 RID: 3009
	private GameObject leftHandModel;

	// Token: 0x04000BC2 RID: 3010
	private GameObject RightHandModel;

	// Token: 0x04000BC3 RID: 3011
	public GameObject myLeftHandModel;

	// Token: 0x04000BC4 RID: 3012
	public GameObject myRightHandModel;

	// Token: 0x04000BC5 RID: 3013
	[HideInInspector]
	public Vector3 spawnPoint;

	// Token: 0x04000BC6 RID: 3014
	public Vector3 localPlayerRotation;

	// Token: 0x04000BC7 RID: 3015
	public Vector3 localPlayerPosition;

	// Token: 0x04000BC8 RID: 3016
	[HideInInspector]
	public bool wasGravity;

	// Token: 0x04000BC9 RID: 3017
	[HideInInspector]
	public bool wasKinematic;

	// Token: 0x04000BCA RID: 3018
	public bool isFixedItem;

	// Token: 0x04000BCB RID: 3019
	private VRTK_InteractGrab _currentGrabbingObject;

	// Token: 0x04000BCC RID: 3020
	private bool hasUsed;
}
