using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;

// Token: 0x020001B6 RID: 438
public class FootstepController : MonoBehaviour
{
	// Token: 0x06000BED RID: 3053 RVA: 0x0004A712 File Offset: 0x00048912
	private void Awake()
	{
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x0004A725 File Offset: 0x00048925
	private float GetCurrentSpeed()
	{
		if (this.XAxisSpeed < this.YAxisSpeed)
		{
			return this.YAxisSpeed;
		}
		return this.XAxisSpeed;
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x0004A744 File Offset: 0x00048944
	private void Update()
	{
		if (PhotonNetwork.inRoom && !this.view.isMine)
		{
			return;
		}
		if (XRDevice.isPresent)
		{
			return;
		}
		if (this.player.firstPersonController && this.player.charController)
		{
			this.isWalking = (this.player.firstPersonController.enabled && this.player.charController.velocity.magnitude > 0.5f);
		}
		else
		{
			this.isWalking = false;
		}
		if (this.isWalking)
		{
			if (this.player.pcCanvas && this.player.pcCanvas.isPaused)
			{
				return;
			}
			this.walkTimer -= Time.deltaTime;
			if (this.walkTimer < 0f)
			{
				this.PlaySound();
				this.walkTimer = 0.7f;
			}
		}
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x0004A834 File Offset: 0x00048A34
	public void PlaySound()
	{
		if (this.source.isPlaying)
		{
			return;
		}
		if (GameController.instance && GameController.instance.isLoadingBackToMenu)
		{
			return;
		}
		RaycastHit raycastHit;
		if (Physics.Linecast(base.transform.position, base.transform.position + Vector3.down * 3f, out raycastHit, this.mask, QueryTriggerInteraction.Ignore))
		{
			string tag = raycastHit.collider.tag;
			if (!(tag == "Carpet"))
			{
				if (!(tag == "Wood"))
				{
					if (!(tag == "Concrete"))
					{
						if (!(tag == "Stairs"))
						{
							if (tag == "Grass")
							{
								this.id = 4;
							}
						}
						else
						{
							this.id = 3;
						}
					}
					else
					{
						this.id = 2;
					}
				}
				else
				{
					this.id = 1;
				}
			}
			else
			{
				this.id = 0;
			}
			if (PhotonNetwork.inRoom)
			{
				this.view.RPC("NetworkedPlaySound", PhotonTargets.All, new object[]
				{
					this.id,
					PhotonNetwork.player.ID
				});
				return;
			}
			this.NetworkedPlaySound(this.id, 0);
		}
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x0004A974 File Offset: 0x00048B74
	[PunRPC]
	private void NetworkedPlaySound(int id, int actorID)
	{
		if (id == 0)
		{
			this.source.volume = 0.1f;
			this.source.clip = this.carpetAudioClips[Random.Range(0, this.carpetAudioClips.Length)];
		}
		else if (id == 1)
		{
			this.source.volume = 0.1f;
			this.source.clip = this.woodAudioClips[Random.Range(0, this.woodAudioClips.Length)];
		}
		else if (id == 2)
		{
			this.source.volume = 0.4f;
			this.source.clip = this.concreteAudioClips[Random.Range(0, this.concreteAudioClips.Length)];
		}
		else if (id == 3)
		{
			this.source.volume = 0.6f;
			this.source.clip = this.stairsAudioClips[Random.Range(0, this.stairsAudioClips.Length)];
		}
		else if (id == 4)
		{
			this.source.volume = 0.6f;
			this.source.clip = this.terrainAudioClips[Random.Range(0, this.terrainAudioClips.Length)];
		}
		this.source.outputAudioMixerGroup = SoundController.instance.GetPlayersAudioGroup(actorID);
		if (base.gameObject.activeInHierarchy)
		{
			this.source.Play();
		}
		if (base.gameObject.activeInHierarchy)
		{
			base.StartCoroutine(this.PlayNoiseObject());
		}
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x0004AADA File Offset: 0x00048CDA
	private IEnumerator PlayNoiseObject()
	{
		this.noise.gameObject.SetActive(true);
		yield return new WaitForEndOfFrame();
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x04000C2F RID: 3119
	[SerializeField]
	private AudioSource source;

	// Token: 0x04000C30 RID: 3120
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000C31 RID: 3121
	[SerializeField]
	private Player player;

	// Token: 0x04000C32 RID: 3122
	[SerializeField]
	private AudioClip[] carpetAudioClips;

	// Token: 0x04000C33 RID: 3123
	[SerializeField]
	private AudioClip[] concreteAudioClips;

	// Token: 0x04000C34 RID: 3124
	[SerializeField]
	private AudioClip[] woodAudioClips;

	// Token: 0x04000C35 RID: 3125
	[SerializeField]
	private AudioClip[] stairsAudioClips;

	// Token: 0x04000C36 RID: 3126
	[SerializeField]
	private AudioClip[] terrainAudioClips;

	// Token: 0x04000C37 RID: 3127
	[SerializeField]
	private Noise noise;

	// Token: 0x04000C38 RID: 3128
	public float XAxisSpeed;

	// Token: 0x04000C39 RID: 3129
	public float YAxisSpeed;

	// Token: 0x04000C3A RID: 3130
	public bool isWalking;

	// Token: 0x04000C3B RID: 3131
	private float walkTimer = 0.7f;

	// Token: 0x04000C3C RID: 3132
	[SerializeField]
	private LayerMask mask;

	// Token: 0x04000C3D RID: 3133
	private int id;
}
