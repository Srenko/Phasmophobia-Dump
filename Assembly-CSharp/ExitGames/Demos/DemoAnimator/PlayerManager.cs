using System;
using Photon;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace ExitGames.Demos.DemoAnimator
{
	// Token: 0x02000489 RID: 1161
	public class PlayerManager : PunBehaviour, IPunObservable
	{
		// Token: 0x0600242B RID: 9259 RVA: 0x000B10FC File Offset: 0x000AF2FC
		public void Awake()
		{
			if (this.Beams == null)
			{
				Debug.LogError("<Color=Red><b>Missing</b></Color> Beams Reference.", this);
			}
			else
			{
				this.Beams.SetActive(false);
			}
			if (base.photonView.isMine)
			{
				PlayerManager.LocalPlayerInstance = base.gameObject;
			}
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000B1154 File Offset: 0x000AF354
		public void Start()
		{
			CameraWork component = base.gameObject.GetComponent<CameraWork>();
			if (component != null)
			{
				if (base.photonView.isMine)
				{
					component.OnStartFollowing();
				}
			}
			else
			{
				Debug.LogError("<Color=Red><b>Missing</b></Color> CameraWork Component on player Prefab.", this);
			}
			if (this.PlayerUiPrefab != null)
			{
				Object.Instantiate<GameObject>(this.PlayerUiPrefab).SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
			}
			else
			{
				Debug.LogWarning("<Color=Red><b>Missing</b></Color> PlayerUiPrefab reference on player Prefab.", this);
			}
			SceneManager.sceneLoaded += this.OnSceneLoaded;
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000B11D9 File Offset: 0x000AF3D9
		public void OnDisable()
		{
			SceneManager.sceneLoaded -= this.OnSceneLoaded;
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000B11EC File Offset: 0x000AF3EC
		public void Update()
		{
			if (base.photonView.isMine)
			{
				this.ProcessInputs();
				if (this.Health <= 0f)
				{
					GameManager.Instance.LeaveRoom();
				}
			}
			if (this.Beams != null && this.IsFiring != this.Beams.GetActive())
			{
				this.Beams.SetActive(this.IsFiring);
			}
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000B1255 File Offset: 0x000AF455
		public void OnTriggerEnter(Collider other)
		{
			if (!base.photonView.isMine)
			{
				return;
			}
			if (!other.name.Contains("Beam"))
			{
				return;
			}
			this.Health -= 0.1f;
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000B128A File Offset: 0x000AF48A
		public void OnTriggerStay(Collider other)
		{
			if (!base.photonView.isMine)
			{
				return;
			}
			if (!other.name.Contains("Beam"))
			{
				return;
			}
			this.Health -= 0.1f * Time.deltaTime;
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000B12C8 File Offset: 0x000AF4C8
		private void CalledOnLevelWasLoaded(int level)
		{
			if (!Physics.Raycast(base.transform.position, -Vector3.up, 5f))
			{
				base.transform.position = new Vector3(0f, 5f, 0f);
			}
			Object.Instantiate<GameObject>(this.PlayerUiPrefab).SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000B132C File Offset: 0x000AF52C
		private void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode)
		{
			this.CalledOnLevelWasLoaded(scene.buildIndex);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000B133C File Offset: 0x000AF53C
		private void ProcessInputs()
		{
			if (Input.GetButtonDown("Fire1"))
			{
				EventSystem.current.IsPointerOverGameObject();
				if (!this.IsFiring)
				{
					this.IsFiring = true;
				}
			}
			if (Input.GetButtonUp("Fire1") && this.IsFiring)
			{
				this.IsFiring = false;
			}
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000B138C File Offset: 0x000AF58C
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			if (stream.isWriting)
			{
				stream.SendNext(this.IsFiring);
				stream.SendNext(this.Health);
				return;
			}
			this.IsFiring = (bool)stream.ReceiveNext();
			this.Health = (float)stream.ReceiveNext();
		}

		// Token: 0x04002177 RID: 8567
		[Tooltip("The Player's UI GameObject Prefab")]
		public GameObject PlayerUiPrefab;

		// Token: 0x04002178 RID: 8568
		[Tooltip("The Beams GameObject to control")]
		public GameObject Beams;

		// Token: 0x04002179 RID: 8569
		[Tooltip("The current Health of our player")]
		public float Health = 1f;

		// Token: 0x0400217A RID: 8570
		[Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
		public static GameObject LocalPlayerInstance;

		// Token: 0x0400217B RID: 8571
		private bool IsFiring;
	}
}
