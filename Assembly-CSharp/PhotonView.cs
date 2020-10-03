using System;
using System.Collections.Generic;
using System.Reflection;
using Photon;
using UnityEngine;

// Token: 0x020000B6 RID: 182
[AddComponentMenu("Photon Networking/Photon View")]
public class PhotonView : Photon.MonoBehaviour
{
	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0001D1F0 File Offset: 0x0001B3F0
	// (set) Token: 0x060004E5 RID: 1253 RVA: 0x0001D218 File Offset: 0x0001B418
	public int prefix
	{
		get
		{
			if (this.prefixBackup == -1 && PhotonNetwork.networkingPeer != null)
			{
				this.prefixBackup = (int)PhotonNetwork.networkingPeer.currentLevelPrefix;
			}
			return this.prefixBackup;
		}
		set
		{
			this.prefixBackup = value;
		}
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001D221 File Offset: 0x0001B421
	// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0001D247 File Offset: 0x0001B447
	public object[] instantiationData
	{
		get
		{
			if (!this.didAwake)
			{
				this.instantiationDataField = PhotonNetwork.networkingPeer.FetchInstantiationData(this.instantiationId);
			}
			return this.instantiationDataField;
		}
		set
		{
			this.instantiationDataField = value;
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0001D250 File Offset: 0x0001B450
	// (set) Token: 0x060004E9 RID: 1257 RVA: 0x0001D258 File Offset: 0x0001B458
	public int viewID
	{
		get
		{
			return this.viewIdField;
		}
		set
		{
			bool flag = this.didAwake && this.viewIdField == 0;
			this.ownerId = value / PhotonNetwork.MAX_VIEW_IDS;
			this.viewIdField = value;
			if (flag)
			{
				PhotonNetwork.networkingPeer.RegisterPhotonView(this);
			}
		}
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001D28F File Offset: 0x0001B48F
	public bool isSceneView
	{
		get
		{
			return this.CreatorActorNr == 0;
		}
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001D29A File Offset: 0x0001B49A
	public PhotonPlayer owner
	{
		get
		{
			return PhotonPlayer.Find(this.ownerId);
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001D2A7 File Offset: 0x0001B4A7
	public int OwnerActorNr
	{
		get
		{
			return this.ownerId;
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x060004ED RID: 1261 RVA: 0x0001D2AF File Offset: 0x0001B4AF
	public bool isOwnerActive
	{
		get
		{
			return this.ownerId != 0 && PhotonNetwork.networkingPeer.mActors.ContainsKey(this.ownerId);
		}
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x060004EE RID: 1262 RVA: 0x0001D2D0 File Offset: 0x0001B4D0
	public int CreatorActorNr
	{
		get
		{
			return this.viewIdField / PhotonNetwork.MAX_VIEW_IDS;
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x060004EF RID: 1263 RVA: 0x0001D2DE File Offset: 0x0001B4DE
	public bool isMine
	{
		get
		{
			return this.ownerId == PhotonNetwork.player.ID || (!this.isOwnerActive && PhotonNetwork.isMasterClient);
		}
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0001D303 File Offset: 0x0001B503
	protected internal void Awake()
	{
		if (this.viewID != 0)
		{
			PhotonNetwork.networkingPeer.RegisterPhotonView(this);
			this.instantiationDataField = PhotonNetwork.networkingPeer.FetchInstantiationData(this.instantiationId);
		}
		this.didAwake = true;
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0001D335 File Offset: 0x0001B535
	public void RequestOwnership()
	{
		PhotonNetwork.networkingPeer.RequestOwnership(this.viewID, this.ownerId);
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0001D34D File Offset: 0x0001B54D
	public void TransferOwnership(PhotonPlayer newOwner)
	{
		this.TransferOwnership(newOwner.ID);
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0001D35B File Offset: 0x0001B55B
	public void TransferOwnership(int newOwnerId)
	{
		PhotonNetwork.networkingPeer.TransferOwnership(this.viewID, newOwnerId);
		this.ownerId = newOwnerId;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0001D378 File Offset: 0x0001B578
	public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
	{
		if (this.CreatorActorNr == 0 && !this.OwnerShipWasTransfered && (this.currentMasterID == -1 || this.ownerId == this.currentMasterID))
		{
			this.ownerId = newMasterClient.ID;
		}
		this.currentMasterID = newMasterClient.ID;
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0001D3C4 File Offset: 0x0001B5C4
	protected internal void OnDestroy()
	{
		if (!this.removedFromLocalViewList)
		{
			bool flag = PhotonNetwork.networkingPeer.LocalCleanPhotonView(this);
			bool flag2 = false;
			if (flag && !flag2 && this.instantiationId > 0 && !PhotonHandler.AppQuits && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
			{
				Debug.Log("PUN-instantiated '" + base.gameObject.name + "' got destroyed by engine. This is OK when loading levels. Otherwise use: PhotonNetwork.Destroy().");
			}
		}
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0001D424 File Offset: 0x0001B624
	public void SerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (this.ObservedComponents != null && this.ObservedComponents.Count > 0)
		{
			for (int i = 0; i < this.ObservedComponents.Count; i++)
			{
				this.SerializeComponent(this.ObservedComponents[i], stream, info);
			}
		}
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x0001D474 File Offset: 0x0001B674
	public void DeserializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (this.ObservedComponents != null && this.ObservedComponents.Count > 0)
		{
			for (int i = 0; i < this.ObservedComponents.Count; i++)
			{
				this.DeserializeComponent(this.ObservedComponents[i], stream, info);
			}
		}
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x0001D4C4 File Offset: 0x0001B6C4
	protected internal void DeserializeComponent(Component component, PhotonStream stream, PhotonMessageInfo info)
	{
		if (component == null)
		{
			return;
		}
		if (component is UnityEngine.MonoBehaviour)
		{
			this.ExecuteComponentOnSerialize(component, stream, info);
			return;
		}
		if (component is Transform)
		{
			Transform transform = (Transform)component;
			switch (this.onSerializeTransformOption)
			{
			case OnSerializeTransform.OnlyPosition:
				transform.localPosition = (Vector3)stream.ReceiveNext();
				return;
			case OnSerializeTransform.OnlyRotation:
				transform.localRotation = (Quaternion)stream.ReceiveNext();
				return;
			case OnSerializeTransform.OnlyScale:
				transform.localScale = (Vector3)stream.ReceiveNext();
				return;
			case OnSerializeTransform.PositionAndRotation:
				transform.localPosition = (Vector3)stream.ReceiveNext();
				transform.localRotation = (Quaternion)stream.ReceiveNext();
				return;
			case OnSerializeTransform.All:
				transform.localPosition = (Vector3)stream.ReceiveNext();
				transform.localRotation = (Quaternion)stream.ReceiveNext();
				transform.localScale = (Vector3)stream.ReceiveNext();
				return;
			default:
				return;
			}
		}
		else if (component is Rigidbody)
		{
			Rigidbody rigidbody = (Rigidbody)component;
			switch (this.onSerializeRigidBodyOption)
			{
			case OnSerializeRigidBody.OnlyVelocity:
				rigidbody.velocity = (Vector3)stream.ReceiveNext();
				return;
			case OnSerializeRigidBody.OnlyAngularVelocity:
				rigidbody.angularVelocity = (Vector3)stream.ReceiveNext();
				return;
			case OnSerializeRigidBody.All:
				rigidbody.velocity = (Vector3)stream.ReceiveNext();
				rigidbody.angularVelocity = (Vector3)stream.ReceiveNext();
				return;
			default:
				return;
			}
		}
		else
		{
			if (!(component is Rigidbody2D))
			{
				Debug.LogError("Type of observed is unknown when receiving.");
				return;
			}
			Rigidbody2D rigidbody2D = (Rigidbody2D)component;
			switch (this.onSerializeRigidBodyOption)
			{
			case OnSerializeRigidBody.OnlyVelocity:
				rigidbody2D.velocity = (Vector2)stream.ReceiveNext();
				return;
			case OnSerializeRigidBody.OnlyAngularVelocity:
				rigidbody2D.angularVelocity = (float)stream.ReceiveNext();
				return;
			case OnSerializeRigidBody.All:
				rigidbody2D.velocity = (Vector2)stream.ReceiveNext();
				rigidbody2D.angularVelocity = (float)stream.ReceiveNext();
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x0001D6A0 File Offset: 0x0001B8A0
	protected internal void SerializeComponent(Component component, PhotonStream stream, PhotonMessageInfo info)
	{
		if (component == null)
		{
			return;
		}
		if (component is UnityEngine.MonoBehaviour)
		{
			this.ExecuteComponentOnSerialize(component, stream, info);
			return;
		}
		if (component is Transform)
		{
			Transform transform = (Transform)component;
			switch (this.onSerializeTransformOption)
			{
			case OnSerializeTransform.OnlyPosition:
				stream.SendNext(transform.localPosition);
				return;
			case OnSerializeTransform.OnlyRotation:
				stream.SendNext(transform.localRotation);
				return;
			case OnSerializeTransform.OnlyScale:
				stream.SendNext(transform.localScale);
				return;
			case OnSerializeTransform.PositionAndRotation:
				stream.SendNext(transform.localPosition);
				stream.SendNext(transform.localRotation);
				return;
			case OnSerializeTransform.All:
				stream.SendNext(transform.localPosition);
				stream.SendNext(transform.localRotation);
				stream.SendNext(transform.localScale);
				return;
			default:
				return;
			}
		}
		else if (component is Rigidbody)
		{
			Rigidbody rigidbody = (Rigidbody)component;
			switch (this.onSerializeRigidBodyOption)
			{
			case OnSerializeRigidBody.OnlyVelocity:
				stream.SendNext(rigidbody.velocity);
				return;
			case OnSerializeRigidBody.OnlyAngularVelocity:
				stream.SendNext(rigidbody.angularVelocity);
				return;
			case OnSerializeRigidBody.All:
				stream.SendNext(rigidbody.velocity);
				stream.SendNext(rigidbody.angularVelocity);
				return;
			default:
				return;
			}
		}
		else
		{
			if (!(component is Rigidbody2D))
			{
				Debug.LogError("Observed type is not serializable: " + component.GetType());
				return;
			}
			Rigidbody2D rigidbody2D = (Rigidbody2D)component;
			switch (this.onSerializeRigidBodyOption)
			{
			case OnSerializeRigidBody.OnlyVelocity:
				stream.SendNext(rigidbody2D.velocity);
				return;
			case OnSerializeRigidBody.OnlyAngularVelocity:
				stream.SendNext(rigidbody2D.angularVelocity);
				return;
			case OnSerializeRigidBody.All:
				stream.SendNext(rigidbody2D.velocity);
				stream.SendNext(rigidbody2D.angularVelocity);
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x0001D884 File Offset: 0x0001BA84
	protected internal void ExecuteComponentOnSerialize(Component component, PhotonStream stream, PhotonMessageInfo info)
	{
		IPunObservable punObservable = component as IPunObservable;
		if (punObservable != null)
		{
			punObservable.OnPhotonSerializeView(stream, info);
			return;
		}
		if (component != null)
		{
			MethodInfo methodInfo = null;
			if (!this.m_OnSerializeMethodInfos.TryGetValue(component, out methodInfo))
			{
				if (!NetworkingPeer.GetMethod(component as UnityEngine.MonoBehaviour, PhotonNetworkingMessage.OnPhotonSerializeView.ToString(), out methodInfo))
				{
					Debug.LogError("The observed monobehaviour (" + component.name + ") of this PhotonView does not implement OnPhotonSerializeView()!");
					methodInfo = null;
				}
				this.m_OnSerializeMethodInfos.Add(component, methodInfo);
			}
			if (methodInfo != null)
			{
				methodInfo.Invoke(component, new object[]
				{
					stream,
					info
				});
			}
		}
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x0001D92B File Offset: 0x0001BB2B
	public void RefreshRpcMonoBehaviourCache()
	{
		this.RpcMonoBehaviours = base.GetComponents<UnityEngine.MonoBehaviour>();
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x0001D939 File Offset: 0x0001BB39
	public void RPC(string methodName, PhotonTargets target, params object[] parameters)
	{
		PhotonNetwork.RPC(this, methodName, target, false, parameters);
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x0001D945 File Offset: 0x0001BB45
	public void RpcSecure(string methodName, PhotonTargets target, bool encrypt, params object[] parameters)
	{
		PhotonNetwork.RPC(this, methodName, target, encrypt, parameters);
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x0001D952 File Offset: 0x0001BB52
	public void RPC(string methodName, PhotonPlayer targetPlayer, params object[] parameters)
	{
		PhotonNetwork.RPC(this, methodName, targetPlayer, false, parameters);
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x0001D95E File Offset: 0x0001BB5E
	public void RpcSecure(string methodName, PhotonPlayer targetPlayer, bool encrypt, params object[] parameters)
	{
		PhotonNetwork.RPC(this, methodName, targetPlayer, encrypt, parameters);
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x0001D96B File Offset: 0x0001BB6B
	public static PhotonView Get(Component component)
	{
		return component.GetComponent<PhotonView>();
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x00012509 File Offset: 0x00010709
	public static PhotonView Get(GameObject gameObj)
	{
		return gameObj.GetComponent<PhotonView>();
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x0001D973 File Offset: 0x0001BB73
	public static PhotonView Find(int viewID)
	{
		return PhotonNetwork.networkingPeer.GetPhotonView(viewID);
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x0001D980 File Offset: 0x0001BB80
	public override string ToString()
	{
		return string.Format("View ({3}){0} on {1} {2}", new object[]
		{
			this.viewID,
			(base.gameObject != null) ? base.gameObject.name : "GO==null",
			this.isSceneView ? "(scene)" : string.Empty,
			this.prefix
		});
	}

	// Token: 0x0400053C RID: 1340
	public int ownerId;

	// Token: 0x0400053D RID: 1341
	public byte group;

	// Token: 0x0400053E RID: 1342
	protected internal bool mixedModeIsReliable;

	// Token: 0x0400053F RID: 1343
	public bool OwnerShipWasTransfered;

	// Token: 0x04000540 RID: 1344
	public int prefixBackup = -1;

	// Token: 0x04000541 RID: 1345
	internal object[] instantiationDataField;

	// Token: 0x04000542 RID: 1346
	protected internal object[] lastOnSerializeDataSent;

	// Token: 0x04000543 RID: 1347
	protected internal object[] lastOnSerializeDataReceived;

	// Token: 0x04000544 RID: 1348
	public ViewSynchronization synchronization;

	// Token: 0x04000545 RID: 1349
	public OnSerializeTransform onSerializeTransformOption = OnSerializeTransform.PositionAndRotation;

	// Token: 0x04000546 RID: 1350
	public OnSerializeRigidBody onSerializeRigidBodyOption = OnSerializeRigidBody.All;

	// Token: 0x04000547 RID: 1351
	public OwnershipOption ownershipTransfer;

	// Token: 0x04000548 RID: 1352
	public List<Component> ObservedComponents;

	// Token: 0x04000549 RID: 1353
	private Dictionary<Component, MethodInfo> m_OnSerializeMethodInfos = new Dictionary<Component, MethodInfo>(3);

	// Token: 0x0400054A RID: 1354
	[SerializeField]
	private int viewIdField;

	// Token: 0x0400054B RID: 1355
	public int instantiationId;

	// Token: 0x0400054C RID: 1356
	public int currentMasterID = -1;

	// Token: 0x0400054D RID: 1357
	protected internal bool didAwake;

	// Token: 0x0400054E RID: 1358
	[SerializeField]
	protected internal bool isRuntimeInstantiated;

	// Token: 0x0400054F RID: 1359
	protected internal bool removedFromLocalViewList;

	// Token: 0x04000550 RID: 1360
	internal UnityEngine.MonoBehaviour[] RpcMonoBehaviours;

	// Token: 0x04000551 RID: 1361
	private MethodInfo OnSerializeMethodInfo;

	// Token: 0x04000552 RID: 1362
	private bool failedToFindOnSerialize;
}
