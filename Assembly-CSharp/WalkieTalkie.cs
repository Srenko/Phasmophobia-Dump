using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.XR;

// Token: 0x020001BE RID: 446
[RequireComponent(typeof(PhotonView))]
public class WalkieTalkie : MonoBehaviour
{
	// Token: 0x06000C3D RID: 3133 RVA: 0x0004CECA File Offset: 0x0004B0CA
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x0004CEEC File Offset: 0x0004B0EC
	private void Start()
	{
		if (MainManager.instance != null)
		{
			base.enabled = false;
			return;
		}
		if (this.photonInteract && this.view.isMine)
		{
			this.photonInteract.AddUseEvent(new UnityAction(this.Use));
			this.photonInteract.AddStopEvent(new UnityAction(this.Stop));
			this.photonInteract.AddUnGrabbedEvent(new UnityAction(this.Stop));
		}
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x0004CF6D File Offset: 0x0004B16D
	public void Use()
	{
		this.view.RPC("TurnOn", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x0004CF85 File Offset: 0x0004B185
	public void Stop()
	{
		this.view.RPC("TurnOff", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x0004CFA0 File Offset: 0x0004B1A0
	[PunRPC]
	private void TurnOn()
	{
		if (LevelController.instance == null)
		{
			return;
		}
		this.isOn = true;
		if (base.gameObject.activeInHierarchy)
		{
			this.source.Play();
		}
		if (LevelController.instance.currentGhost && LevelController.instance.currentGhost.isHunting)
		{
			if (base.gameObject.activeInHierarchy)
			{
				this.staticSource.Play();
			}
			return;
		}
		this.noise.gameObject.SetActive(true);
		this.noise.volume = 0.8f;
		this.photonVoiceSource.outputAudioMixerGroup = this.staticEffect;
		this.photonVoiceSource.volume = 0.04f * (XRDevice.isPresent ? PauseMenuManager.instance.GetPlayerVolume(this.view.ownerId) : PauseMenuController.instance.GetPlayerVolume(this.view.ownerId));
		this.photonVoiceSource.spatialBlend = 0f;
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x0004D09B File Offset: 0x0004B29B
	private void Update()
	{
		if (this.isOn && LevelController.instance.currentGhost.isHunting)
		{
			this.Stop();
		}
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x0004D0BC File Offset: 0x0004B2BC
	[PunRPC]
	private void TurnOff()
	{
		this.staticSource.Stop();
		this.isOn = false;
		if (base.gameObject.activeInHierarchy)
		{
			this.source.Play();
		}
		this.noise.gameObject.SetActive(false);
		this.noise.volume = 0f;
		this.photonVoiceSource.outputAudioMixerGroup = SoundController.instance.GetAudioGroupFromSnapshot(this.player.currentPlayerSnapshot);
		this.photonVoiceSource.volume = 0.8f * (XRDevice.isPresent ? PauseMenuManager.instance.GetPlayerVolume(this.view.ownerId) : PauseMenuController.instance.GetPlayerVolume(this.view.ownerId));
		this.photonVoiceSource.spatialBlend = 1f;
	}

	// Token: 0x04000CCA RID: 3274
	[SerializeField]
	private AudioSource source;

	// Token: 0x04000CCB RID: 3275
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000CCC RID: 3276
	[SerializeField]
	private AudioSource photonVoiceSource;

	// Token: 0x04000CCD RID: 3277
	[SerializeField]
	private Noise noise;

	// Token: 0x04000CCE RID: 3278
	[HideInInspector]
	public bool isOn;

	// Token: 0x04000CCF RID: 3279
	[SerializeField]
	private Player player;

	// Token: 0x04000CD0 RID: 3280
	[SerializeField]
	private AudioSource staticSource;

	// Token: 0x04000CD1 RID: 3281
	[SerializeField]
	private AudioMixerGroup staticEffect;

	// Token: 0x04000CD2 RID: 3282
	private PhotonView view;
}
