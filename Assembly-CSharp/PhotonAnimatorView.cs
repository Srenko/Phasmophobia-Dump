using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BE RID: 190
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PhotonView))]
[AddComponentMenu("Photon Networking/Photon Animator View")]
public class PhotonAnimatorView : MonoBehaviour, IPunObservable
{
	// Token: 0x06000564 RID: 1380 RVA: 0x0001E9A9 File Offset: 0x0001CBA9
	private void Awake()
	{
		this.m_PhotonView = base.GetComponent<PhotonView>();
		this.m_StreamQueue = new PhotonStreamQueue(120);
		this.m_Animator = base.GetComponent<Animator>();
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0001E9D0 File Offset: 0x0001CBD0
	private void Update()
	{
		if (this.m_Animator.applyRootMotion && !this.m_PhotonView.isMine && PhotonNetwork.connected)
		{
			this.m_Animator.applyRootMotion = false;
		}
		if (!PhotonNetwork.inRoom || PhotonNetwork.room.PlayerCount <= 1)
		{
			this.m_StreamQueue.Reset();
			return;
		}
		if (this.m_PhotonView.isMine)
		{
			this.SerializeDataContinuously();
			this.CacheDiscreteTriggers();
			return;
		}
		this.DeserializeDataContinuously();
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0001EA4C File Offset: 0x0001CC4C
	public void CacheDiscreteTriggers()
	{
		for (int i = 0; i < this.m_SynchronizeParameters.Count; i++)
		{
			PhotonAnimatorView.SynchronizedParameter synchronizedParameter = this.m_SynchronizeParameters[i];
			if (synchronizedParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete && synchronizedParameter.Type == PhotonAnimatorView.ParameterType.Trigger && this.m_Animator.GetBool(synchronizedParameter.Name) && synchronizedParameter.Type == PhotonAnimatorView.ParameterType.Trigger)
			{
				this.m_raisedDiscreteTriggersCache.Add(synchronizedParameter.Name);
				return;
			}
		}
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
	public bool DoesLayerSynchronizeTypeExist(int layerIndex)
	{
		return this.m_SynchronizeLayers.FindIndex((PhotonAnimatorView.SynchronizedLayer item) => item.LayerIndex == layerIndex) != -1;
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0001EAF8 File Offset: 0x0001CCF8
	public bool DoesParameterSynchronizeTypeExist(string name)
	{
		return this.m_SynchronizeParameters.FindIndex((PhotonAnimatorView.SynchronizedParameter item) => item.Name == name) != -1;
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0001EB2F File Offset: 0x0001CD2F
	public List<PhotonAnimatorView.SynchronizedLayer> GetSynchronizedLayers()
	{
		return this.m_SynchronizeLayers;
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0001EB37 File Offset: 0x0001CD37
	public List<PhotonAnimatorView.SynchronizedParameter> GetSynchronizedParameters()
	{
		return this.m_SynchronizeParameters;
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0001EB40 File Offset: 0x0001CD40
	public PhotonAnimatorView.SynchronizeType GetLayerSynchronizeType(int layerIndex)
	{
		int num = this.m_SynchronizeLayers.FindIndex((PhotonAnimatorView.SynchronizedLayer item) => item.LayerIndex == layerIndex);
		if (num == -1)
		{
			return PhotonAnimatorView.SynchronizeType.Disabled;
		}
		return this.m_SynchronizeLayers[num].SynchronizeType;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0001EB8C File Offset: 0x0001CD8C
	public PhotonAnimatorView.SynchronizeType GetParameterSynchronizeType(string name)
	{
		int num = this.m_SynchronizeParameters.FindIndex((PhotonAnimatorView.SynchronizedParameter item) => item.Name == name);
		if (num == -1)
		{
			return PhotonAnimatorView.SynchronizeType.Disabled;
		}
		return this.m_SynchronizeParameters[num].SynchronizeType;
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0001EBD8 File Offset: 0x0001CDD8
	public void SetLayerSynchronized(int layerIndex, PhotonAnimatorView.SynchronizeType synchronizeType)
	{
		if (Application.isPlaying)
		{
			this.m_WasSynchronizeTypeChanged = true;
		}
		int num = this.m_SynchronizeLayers.FindIndex((PhotonAnimatorView.SynchronizedLayer item) => item.LayerIndex == layerIndex);
		if (num == -1)
		{
			this.m_SynchronizeLayers.Add(new PhotonAnimatorView.SynchronizedLayer
			{
				LayerIndex = layerIndex,
				SynchronizeType = synchronizeType
			});
			return;
		}
		this.m_SynchronizeLayers[num].SynchronizeType = synchronizeType;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x0001EC54 File Offset: 0x0001CE54
	public void SetParameterSynchronized(string name, PhotonAnimatorView.ParameterType type, PhotonAnimatorView.SynchronizeType synchronizeType)
	{
		if (Application.isPlaying)
		{
			this.m_WasSynchronizeTypeChanged = true;
		}
		int num = this.m_SynchronizeParameters.FindIndex((PhotonAnimatorView.SynchronizedParameter item) => item.Name == name);
		if (num == -1)
		{
			this.m_SynchronizeParameters.Add(new PhotonAnimatorView.SynchronizedParameter
			{
				Name = name,
				Type = type,
				SynchronizeType = synchronizeType
			});
			return;
		}
		this.m_SynchronizeParameters[num].SynchronizeType = synchronizeType;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0001ECD8 File Offset: 0x0001CED8
	private void SerializeDataContinuously()
	{
		if (this.m_Animator == null)
		{
			return;
		}
		for (int i = 0; i < this.m_SynchronizeLayers.Count; i++)
		{
			if (this.m_SynchronizeLayers[i].SynchronizeType == PhotonAnimatorView.SynchronizeType.Continuous)
			{
				this.m_StreamQueue.SendNext(this.m_Animator.GetLayerWeight(this.m_SynchronizeLayers[i].LayerIndex));
			}
		}
		for (int j = 0; j < this.m_SynchronizeParameters.Count; j++)
		{
			PhotonAnimatorView.SynchronizedParameter synchronizedParameter = this.m_SynchronizeParameters[j];
			if (synchronizedParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Continuous)
			{
				PhotonAnimatorView.ParameterType type = synchronizedParameter.Type;
				switch (type)
				{
				case PhotonAnimatorView.ParameterType.Float:
					this.m_StreamQueue.SendNext(this.m_Animator.GetFloat(synchronizedParameter.Name));
					break;
				case (PhotonAnimatorView.ParameterType)2:
					break;
				case PhotonAnimatorView.ParameterType.Int:
					this.m_StreamQueue.SendNext(this.m_Animator.GetInteger(synchronizedParameter.Name));
					break;
				case PhotonAnimatorView.ParameterType.Bool:
					this.m_StreamQueue.SendNext(this.m_Animator.GetBool(synchronizedParameter.Name));
					break;
				default:
					if (type == PhotonAnimatorView.ParameterType.Trigger)
					{
						this.m_StreamQueue.SendNext(this.m_Animator.GetBool(synchronizedParameter.Name));
					}
					break;
				}
			}
		}
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0001EE34 File Offset: 0x0001D034
	private void DeserializeDataContinuously()
	{
		if (!this.m_StreamQueue.HasQueuedObjects())
		{
			return;
		}
		for (int i = 0; i < this.m_SynchronizeLayers.Count; i++)
		{
			if (this.m_SynchronizeLayers[i].SynchronizeType == PhotonAnimatorView.SynchronizeType.Continuous)
			{
				this.m_Animator.SetLayerWeight(this.m_SynchronizeLayers[i].LayerIndex, (float)this.m_StreamQueue.ReceiveNext());
			}
		}
		for (int j = 0; j < this.m_SynchronizeParameters.Count; j++)
		{
			PhotonAnimatorView.SynchronizedParameter synchronizedParameter = this.m_SynchronizeParameters[j];
			if (synchronizedParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Continuous)
			{
				PhotonAnimatorView.ParameterType type = synchronizedParameter.Type;
				switch (type)
				{
				case PhotonAnimatorView.ParameterType.Float:
					this.m_Animator.SetFloat(synchronizedParameter.Name, (float)this.m_StreamQueue.ReceiveNext());
					break;
				case (PhotonAnimatorView.ParameterType)2:
					break;
				case PhotonAnimatorView.ParameterType.Int:
					this.m_Animator.SetInteger(synchronizedParameter.Name, (int)this.m_StreamQueue.ReceiveNext());
					break;
				case PhotonAnimatorView.ParameterType.Bool:
					this.m_Animator.SetBool(synchronizedParameter.Name, (bool)this.m_StreamQueue.ReceiveNext());
					break;
				default:
					if (type == PhotonAnimatorView.ParameterType.Trigger)
					{
						this.m_Animator.SetBool(synchronizedParameter.Name, (bool)this.m_StreamQueue.ReceiveNext());
					}
					break;
				}
			}
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x0001EF90 File Offset: 0x0001D190
	private void SerializeDataDiscretly(PhotonStream stream)
	{
		for (int i = 0; i < this.m_SynchronizeLayers.Count; i++)
		{
			if (this.m_SynchronizeLayers[i].SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete)
			{
				stream.SendNext(this.m_Animator.GetLayerWeight(this.m_SynchronizeLayers[i].LayerIndex));
			}
		}
		for (int j = 0; j < this.m_SynchronizeParameters.Count; j++)
		{
			PhotonAnimatorView.SynchronizedParameter synchronizedParameter = this.m_SynchronizeParameters[j];
			if (synchronizedParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete)
			{
				PhotonAnimatorView.ParameterType type = synchronizedParameter.Type;
				switch (type)
				{
				case PhotonAnimatorView.ParameterType.Float:
					stream.SendNext(this.m_Animator.GetFloat(synchronizedParameter.Name));
					break;
				case (PhotonAnimatorView.ParameterType)2:
					break;
				case PhotonAnimatorView.ParameterType.Int:
					stream.SendNext(this.m_Animator.GetInteger(synchronizedParameter.Name));
					break;
				case PhotonAnimatorView.ParameterType.Bool:
					stream.SendNext(this.m_Animator.GetBool(synchronizedParameter.Name));
					break;
				default:
					if (type == PhotonAnimatorView.ParameterType.Trigger)
					{
						stream.SendNext(this.m_raisedDiscreteTriggersCache.Contains(synchronizedParameter.Name));
					}
					break;
				}
			}
		}
		this.m_raisedDiscreteTriggersCache.Clear();
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0001F0CC File Offset: 0x0001D2CC
	private void DeserializeDataDiscretly(PhotonStream stream)
	{
		for (int i = 0; i < this.m_SynchronizeLayers.Count; i++)
		{
			if (this.m_SynchronizeLayers[i].SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete)
			{
				this.m_Animator.SetLayerWeight(this.m_SynchronizeLayers[i].LayerIndex, (float)stream.ReceiveNext());
			}
		}
		for (int j = 0; j < this.m_SynchronizeParameters.Count; j++)
		{
			PhotonAnimatorView.SynchronizedParameter synchronizedParameter = this.m_SynchronizeParameters[j];
			if (synchronizedParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete)
			{
				PhotonAnimatorView.ParameterType type = synchronizedParameter.Type;
				switch (type)
				{
				case PhotonAnimatorView.ParameterType.Float:
					if (!(stream.PeekNext() is float))
					{
						return;
					}
					this.m_Animator.SetFloat(synchronizedParameter.Name, (float)stream.ReceiveNext());
					break;
				case (PhotonAnimatorView.ParameterType)2:
					break;
				case PhotonAnimatorView.ParameterType.Int:
					if (!(stream.PeekNext() is int))
					{
						return;
					}
					this.m_Animator.SetInteger(synchronizedParameter.Name, (int)stream.ReceiveNext());
					break;
				case PhotonAnimatorView.ParameterType.Bool:
					if (!(stream.PeekNext() is bool))
					{
						return;
					}
					this.m_Animator.SetBool(synchronizedParameter.Name, (bool)stream.ReceiveNext());
					break;
				default:
					if (type == PhotonAnimatorView.ParameterType.Trigger)
					{
						if (!(stream.PeekNext() is bool))
						{
							return;
						}
						if ((bool)stream.ReceiveNext())
						{
							this.m_Animator.SetTrigger(synchronizedParameter.Name);
						}
					}
					break;
				}
			}
		}
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x0001F240 File Offset: 0x0001D440
	private void SerializeSynchronizationTypeState(PhotonStream stream)
	{
		byte[] array = new byte[this.m_SynchronizeLayers.Count + this.m_SynchronizeParameters.Count];
		for (int i = 0; i < this.m_SynchronizeLayers.Count; i++)
		{
			array[i] = (byte)this.m_SynchronizeLayers[i].SynchronizeType;
		}
		for (int j = 0; j < this.m_SynchronizeParameters.Count; j++)
		{
			array[this.m_SynchronizeLayers.Count + j] = (byte)this.m_SynchronizeParameters[j].SynchronizeType;
		}
		stream.SendNext(array);
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x0001F2D4 File Offset: 0x0001D4D4
	private void DeserializeSynchronizationTypeState(PhotonStream stream)
	{
		byte[] array = (byte[])stream.ReceiveNext();
		for (int i = 0; i < this.m_SynchronizeLayers.Count; i++)
		{
			this.m_SynchronizeLayers[i].SynchronizeType = (PhotonAnimatorView.SynchronizeType)array[i];
		}
		for (int j = 0; j < this.m_SynchronizeParameters.Count; j++)
		{
			this.m_SynchronizeParameters[j].SynchronizeType = (PhotonAnimatorView.SynchronizeType)array[this.m_SynchronizeLayers.Count + j];
		}
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x0001F350 File Offset: 0x0001D550
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (this.m_Animator == null)
		{
			return;
		}
		if (stream.isWriting)
		{
			if (this.m_WasSynchronizeTypeChanged)
			{
				this.m_StreamQueue.Reset();
				this.SerializeSynchronizationTypeState(stream);
				this.m_WasSynchronizeTypeChanged = false;
			}
			this.m_StreamQueue.Serialize(stream);
			this.SerializeDataDiscretly(stream);
			return;
		}
		if (stream.PeekNext() is byte[])
		{
			this.DeserializeSynchronizationTypeState(stream);
		}
		this.m_StreamQueue.Deserialize(stream);
		this.DeserializeDataDiscretly(stream);
	}

	// Token: 0x0400057E RID: 1406
	private Animator m_Animator;

	// Token: 0x0400057F RID: 1407
	private PhotonStreamQueue m_StreamQueue;

	// Token: 0x04000580 RID: 1408
	[HideInInspector]
	[SerializeField]
	private bool ShowLayerWeightsInspector = true;

	// Token: 0x04000581 RID: 1409
	[HideInInspector]
	[SerializeField]
	private bool ShowParameterInspector = true;

	// Token: 0x04000582 RID: 1410
	[HideInInspector]
	[SerializeField]
	private List<PhotonAnimatorView.SynchronizedParameter> m_SynchronizeParameters = new List<PhotonAnimatorView.SynchronizedParameter>();

	// Token: 0x04000583 RID: 1411
	[HideInInspector]
	[SerializeField]
	private List<PhotonAnimatorView.SynchronizedLayer> m_SynchronizeLayers = new List<PhotonAnimatorView.SynchronizedLayer>();

	// Token: 0x04000584 RID: 1412
	private Vector3 m_ReceiverPosition;

	// Token: 0x04000585 RID: 1413
	private float m_LastDeserializeTime;

	// Token: 0x04000586 RID: 1414
	private bool m_WasSynchronizeTypeChanged = true;

	// Token: 0x04000587 RID: 1415
	private PhotonView m_PhotonView;

	// Token: 0x04000588 RID: 1416
	private List<string> m_raisedDiscreteTriggersCache = new List<string>();

	// Token: 0x020004F2 RID: 1266
	public enum ParameterType
	{
		// Token: 0x040023E2 RID: 9186
		Float = 1,
		// Token: 0x040023E3 RID: 9187
		Int = 3,
		// Token: 0x040023E4 RID: 9188
		Bool,
		// Token: 0x040023E5 RID: 9189
		Trigger = 9
	}

	// Token: 0x020004F3 RID: 1267
	public enum SynchronizeType
	{
		// Token: 0x040023E7 RID: 9191
		Disabled,
		// Token: 0x040023E8 RID: 9192
		Discrete,
		// Token: 0x040023E9 RID: 9193
		Continuous
	}

	// Token: 0x020004F4 RID: 1268
	[Serializable]
	public class SynchronizedParameter
	{
		// Token: 0x040023EA RID: 9194
		public PhotonAnimatorView.ParameterType Type;

		// Token: 0x040023EB RID: 9195
		public PhotonAnimatorView.SynchronizeType SynchronizeType;

		// Token: 0x040023EC RID: 9196
		public string Name;
	}

	// Token: 0x020004F5 RID: 1269
	[Serializable]
	public class SynchronizedLayer
	{
		// Token: 0x040023ED RID: 9197
		public PhotonAnimatorView.SynchronizeType SynchronizeType;

		// Token: 0x040023EE RID: 9198
		public int LayerIndex;
	}
}
