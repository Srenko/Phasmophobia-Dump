using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;

// Token: 0x02000144 RID: 324
[RequireComponent(typeof(PhotonView))]
public class Door : MonoBehaviour
{
	// Token: 0x06000863 RID: 2147 RVA: 0x00031F60 File Offset: 0x00030160
	private void Awake()
	{
		if (!this.photonInteract)
		{
			this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		}
		if (!this.view)
		{
			this.view = base.GetComponent<PhotonView>();
		}
		if (!this.col)
		{
			this.col = base.GetComponent<BoxCollider>();
		}
		if (!this.body)
		{
			this.body = base.GetComponent<Rigidbody>();
		}
		if (this.noise != null)
		{
			this.noise.gameObject.SetActive(false);
		}
		this.closed = true;
		this.unlockTimer = Random.Range(10f, 20f);
		if (this.locked)
		{
			this.body.constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00032024 File Offset: 0x00030224
	private void Start()
	{
		if (this.handPrintObject != null)
		{
			this.handPrintObject.SetActive(false);
		}
		if (this.type != Key.KeyType.main)
		{
			if (this.lockSource)
			{
				this.lockSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
			}
			if (this.loopSource)
			{
				this.loopSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
			}
		}
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x000320B8 File Offset: 0x000302B8
	private void Update()
	{
		if (this.locked)
		{
			if (this.hasBeenUnlocked && PhotonNetwork.isMasterClient && !LevelController.instance.currentGhost.isHunting)
			{
				this.unlockTimer -= Time.deltaTime;
				if (this.unlockTimer < 0f)
				{
					this.UnlockDoor();
					this.unlockTimer = Random.Range(10f, 20f);
					return;
				}
			}
		}
		else if (Time.frameCount % 3 == 0)
		{
			if (!this.closed)
			{
				if (!this.loopSource.isPlaying)
				{
					this.loopSource.Play();
				}
				this.loopSource.volume = this.body.velocity.magnitude / this.loopVolumeDivide;
				return;
			}
			if (this.loopSource.isPlaying)
			{
				this.loopSource.Stop();
			}
		}
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0003219B File Offset: 0x0003039B
	public void DisableOrEnableCollider(bool active)
	{
		this.view.RPC("DisableOrEnableColliderNetworked", PhotonTargets.All, new object[]
		{
			active
		});
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x000321BD File Offset: 0x000303BD
	[PunRPC]
	private void DisableOrEnableColliderNetworked(bool active)
	{
		this.col.enabled = active;
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x000321CB File Offset: 0x000303CB
	[PunRPC]
	private void NetworkedGrabbedDoor()
	{
		this.closed = false;
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x000321D4 File Offset: 0x000303D4
	public void AttemptToUnlockDoor()
	{
		bool flag = false;
		if (this.locked)
		{
			if (!LevelController.instance.currentGhost.isHunting)
			{
				for (int i = 0; i < GameController.instance.myPlayer.player.keys.Count; i++)
				{
					if (GameController.instance.myPlayer.player.keys[i] == this.type)
					{
						this.UnlockDoor();
						flag = true;
					}
				}
			}
			if (!flag)
			{
				this.view.RPC("NetworkedPlayLockSound", PhotonTargets.All, Array.Empty<object>());
			}
		}
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00032264 File Offset: 0x00030464
	public void GrabbedDoor()
	{
		bool flag = false;
		if (!XRDevice.isPresent)
		{
			this.body.isKinematic = false;
			this.body.useGravity = true;
		}
		if (this.locked)
		{
			if (!LevelController.instance.currentGhost.isHunting)
			{
				for (int i = 0; i < GameController.instance.myPlayer.player.keys.Count; i++)
				{
					if (GameController.instance.myPlayer.player.keys[i] == this.type)
					{
						this.UnlockDoor();
						flag = true;
					}
				}
				if (!flag)
				{
					this.view.RPC("NetworkedPlayLockSound", PhotonTargets.All, Array.Empty<object>());
				}
			}
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			this.view.RPC("NetworkedGrabbedDoor", PhotonTargets.All, Array.Empty<object>());
		}
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x00032330 File Offset: 0x00030530
	public void UnGrabbedDoor()
	{
		this.body.mass = 100f;
		this.body.isKinematic = true;
		this.body.useGravity = false;
		Quaternion localRotation = base.transform.localRotation;
		Vector3 eulerAngles = localRotation.eulerAngles;
		if ((eulerAngles.y < this.closedYRot + 7f && !this.isReversed) || (eulerAngles.y > this.closedYRot - 7f && this.isReversed))
		{
			eulerAngles.y = this.closedYRot;
			this.view.RPC("NetworkedPlayClosedSound", PhotonTargets.All, Array.Empty<object>());
			localRotation.eulerAngles = eulerAngles;
			base.transform.localRotation = localRotation;
		}
		if (!PhotonNetwork.isMasterClient)
		{
			this.view.RPC("GiveControlToMasterClient", PhotonTargets.MasterClient, Array.Empty<object>());
		}
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x00032405 File Offset: 0x00030605
	[PunRPC]
	private void GiveControlToMasterClient()
	{
		this.view.RequestOwnership();
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x00032412 File Offset: 0x00030612
	public void TrailerCloseDoor()
	{
		base.transform.localRotation = new Quaternion(0f, this.closedYRot, 0f, 0f);
		this.view.RPC("NetworkedPlayClosedSound", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00032450 File Offset: 0x00030650
	public void UnlockDoor()
	{
		if (!this.locked)
		{
			return;
		}
		this.body.constraints = RigidbodyConstraints.None;
		this.locked = false;
		if (!this.hasBeenUnlocked)
		{
			this.view.RPC("SyncHasBeenLocked", PhotonTargets.AllBuffered, Array.Empty<object>());
		}
		this.view.RPC("SyncLockState", PhotonTargets.AllBuffered, new object[]
		{
			this.locked
		});
		this.view.RPC("NetworkedPlayUnlockSound", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x000324D4 File Offset: 0x000306D4
	public void LockDoor()
	{
		if (this.locked)
		{
			return;
		}
		this.body.constraints = RigidbodyConstraints.FreezeAll;
		if ((base.transform.localEulerAngles.y < this.closedYRot + 12f && !this.isReversed) || (base.transform.localEulerAngles.y > this.closedYRot - 12f && this.isReversed))
		{
			base.transform.localEulerAngles = new Vector3(0f, this.closedYRot, 0f);
			this.view.RPC("NetworkedPlayClosedSound", PhotonTargets.All, Array.Empty<object>());
		}
		if (!this.closed)
		{
			return;
		}
		this.locked = true;
		this.view.RPC("SyncLockState", PhotonTargets.AllBuffered, new object[]
		{
			this.locked
		});
		this.view.RPC("NetworkedPlayLockSound", PhotonTargets.All, Array.Empty<object>());
		if (this.type == Key.KeyType.main)
		{
			if (!LevelController.instance.currentGhost.isHunting)
			{
				this.view.RPC("UnlockDoorTimer", PhotonTargets.AllBuffered, new object[]
				{
					Random.Range(5f, 20f)
				});
				return;
			}
		}
		else
		{
			this.view.RPC("UnlockDoorTimer", PhotonTargets.AllBuffered, new object[]
			{
				Random.Range(5f, 20f)
			});
		}
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x0003263C File Offset: 0x0003083C
	[PunRPC]
	private void SyncLockState(bool isLocked)
	{
		this.locked = isLocked;
		this.body.constraints = (isLocked ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None);
		this.unlockTimer = Random.Range(10f, 20f);
		if (this.type == Key.KeyType.main)
		{
			SetupPhaseController.instance.mainDoorHasUnlocked = true;
		}
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0003268B File Offset: 0x0003088B
	[PunRPC]
	private void SyncHasBeenLocked()
	{
		this.hasBeenUnlocked = true;
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00032694 File Offset: 0x00030894
	public void PlayLockedSound()
	{
		this.view.RPC("NetworkedPlayLockSound", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x000326AC File Offset: 0x000308AC
	[PunRPC]
	private void NetworkedPlayLockSound()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.lockSource.volume = 0.4f;
		this.lockSource.clip = this.doorLockClips[Random.Range(0, this.doorLockClips.Length)];
		this.lockSource.Play();
		if (this.noise != null)
		{
			base.StartCoroutine(this.PlayNoiseObject(0.12f));
		}
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x00032724 File Offset: 0x00030924
	[PunRPC]
	private void NetworkedPlayUnlockSound()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.lockSource.volume = 0.4f;
		this.lockSource.clip = this.doorUnlockClips[Random.Range(0, this.doorUnlockClips.Length)];
		this.lockSource.Play();
		if (this.noise != null)
		{
			base.StartCoroutine(this.PlayNoiseObject(0.11f));
		}
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x0003279C File Offset: 0x0003099C
	[PunRPC]
	private void NetworkedPlayClosedSound()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.closed = true;
		if (!this.lockSource.isPlaying)
		{
			this.lockSource.volume = this.closedVolume;
			this.lockSource.clip = this.doorClosedClips[Random.Range(0, this.doorClosedClips.Length)];
			this.lockSource.Play();
			if (this.noise != null)
			{
				base.StartCoroutine(this.PlayNoiseObject(0.4f));
			}
		}
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x00032827 File Offset: 0x00030A27
	[PunRPC]
	private IEnumerator UnlockDoorTimer(float timer)
	{
		yield return new WaitForSeconds(timer);
		if (this.locked)
		{
			this.UnlockDoor();
		}
		yield break;
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0003283D File Offset: 0x00030A3D
	public void SpawnHandPrintEvidence()
	{
		if (this.handPrintObject == null)
		{
			return;
		}
		this.view.RPC("NetworkedSpawnHandPrintEvidence", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x00032864 File Offset: 0x00030A64
	[PunRPC]
	private void NetworkedSpawnHandPrintEvidence()
	{
		this.handPrintObject.GetComponent<Renderer>().material = this.handPrintMaterials[Random.Range(0, this.handPrintMaterials.Length)];
		this.handPrintObject.SetActive(true);
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x00032897 File Offset: 0x00030A97
	public void DisableOrEnableDoor(bool activate)
	{
		if (activate)
		{
			this.view.RPC("EnableDoorNetworked", PhotonTargets.All, Array.Empty<object>());
			return;
		}
		this.view.RPC("ForceDropDoorNetworked", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x000328CC File Offset: 0x00030ACC
	[PunRPC]
	private void ForceDropDoorNetworked()
	{
		this.photonInteract.isGrabbable = false;
		this.canBeGrabbed = false;
		if (!XRDevice.isPresent && GameController.instance.myPlayer.player.dragRigidBodyUse.objectHeld == base.gameObject)
		{
			GameController.instance.myPlayer.player.dragRigidBodyUse.DropObject();
		}
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x00032932 File Offset: 0x00030B32
	[PunRPC]
	private void EnableDoorNetworked()
	{
		this.photonInteract.isGrabbable = true;
		this.canBeGrabbed = true;
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x00032947 File Offset: 0x00030B47
	private IEnumerator PlayNoiseObject(float volume)
	{
		this.noise.volume = volume;
		this.noise.gameObject.SetActive(true);
		yield return 0;
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0400087E RID: 2174
	public bool locked;

	// Token: 0x0400087F RID: 2175
	public bool closed;

	// Token: 0x04000880 RID: 2176
	[SerializeField]
	private AudioClip[] doorLockClips;

	// Token: 0x04000881 RID: 2177
	[SerializeField]
	private AudioClip[] doorUnlockClips;

	// Token: 0x04000882 RID: 2178
	[SerializeField]
	private AudioClip[] doorClosedClips;

	// Token: 0x04000883 RID: 2179
	[SerializeField]
	private float closedVolume = 0.1f;

	// Token: 0x04000884 RID: 2180
	[SerializeField]
	private AudioSource lockSource;

	// Token: 0x04000885 RID: 2181
	[SerializeField]
	private AudioSource loopSource;

	// Token: 0x04000886 RID: 2182
	[SerializeField]
	private float loopVolumeDivide = 5f;

	// Token: 0x04000887 RID: 2183
	private bool hasBeenUnlocked;

	// Token: 0x04000888 RID: 2184
	private float unlockTimer = 15f;

	// Token: 0x04000889 RID: 2185
	[HideInInspector]
	public bool canBeGrabbed = true;

	// Token: 0x0400088A RID: 2186
	public PhotonView view;

	// Token: 0x0400088B RID: 2187
	public Rigidbody body;

	// Token: 0x0400088C RID: 2188
	public PhotonObjectInteract photonInteract;

	// Token: 0x0400088D RID: 2189
	[SerializeField]
	private GameObject handPrintObject;

	// Token: 0x0400088E RID: 2190
	[SerializeField]
	private Material[] handPrintMaterials;

	// Token: 0x0400088F RID: 2191
	public BoxCollider col;

	// Token: 0x04000890 RID: 2192
	[SerializeField]
	private bool isReversed;

	// Token: 0x04000891 RID: 2193
	public float closedYRot;

	// Token: 0x04000892 RID: 2194
	[SerializeField]
	private Noise noise;

	// Token: 0x04000893 RID: 2195
	public Renderer rend;

	// Token: 0x04000894 RID: 2196
	public Key.KeyType type;
}
