using System;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class GhostAudio : MonoBehaviour
{
	// Token: 0x06000686 RID: 1670 RVA: 0x0002487F File Offset: 0x00022A7F
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.ghostAI = base.GetComponent<GhostAI>();
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0002489C File Offset: 0x00022A9C
	public void PlaySound(int id, bool loopSource, bool bansheePower)
	{
		if (!bansheePower)
		{
			this.view.RPC("PlaySoundNetworked", PhotonTargets.All, new object[]
			{
				id,
				loopSource
			});
			return;
		}
		this.view.RPC("PlaySoundNetworked", this.ghostAI.bansheeTarget.view.owner, new object[]
		{
			id,
			loopSource
		});
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x00024913 File Offset: 0x00022B13
	public void StopSound()
	{
		this.view.RPC("StopSoundNetworked", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0002492B File Offset: 0x00022B2B
	[PunRPC]
	private void StopSoundNetworked()
	{
		this.soundFXSource.Stop();
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x00024938 File Offset: 0x00022B38
	[PunRPC]
	private void PlaySoundNetworked(int id, bool loopSource)
	{
		if (id == 0)
		{
			this.soundFXSource.clip = this.hummingSound;
			this.soundFXSource.volume = 0.6f;
		}
		else if (id == 1)
		{
			this.soundFXSource.clip = this.screamSoundClips[Random.Range(0, this.screamSoundClips.Length)];
			this.soundFXSource.volume = 0.3f;
		}
		this.soundFXSource.loop = loopSource;
		this.soundFXSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(this.ghostAI.transform.position.y);
		this.soundFXSource.Play();
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x000249E0 File Offset: 0x00022BE0
	public void TurnOnOrOffAppearSource(bool on)
	{
		this.view.RPC("TurnOnOrOffAppearSourceSync", PhotonTargets.All, new object[]
		{
			on
		});
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x00024A02 File Offset: 0x00022C02
	[PunRPC]
	private void TurnOnOrOffAppearSourceSync(bool on)
	{
		if (on)
		{
			this.ghostAppearSource.Play();
			return;
		}
		this.ghostAppearSource.Stop();
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x00024A1E File Offset: 0x00022C1E
	public void PlayOrStopAppearSource(bool play)
	{
		this.view.RPC("PlayOrStopAppearSourceSync", PhotonTargets.All, new object[]
		{
			play
		});
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x00024A40 File Offset: 0x00022C40
	[PunRPC]
	private void PlayOrStopAppearSourceSync(bool play)
	{
		this.ghostAppearSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		if (!play)
		{
			this.ghostAppearSource.volume = 0f;
			return;
		}
		if (!Physics.Linecast(this.ghostAI.raycastPoint.position, GameController.instance.myPlayer.player.headObject.transform.position, this.ghostAI.mask, QueryTriggerInteraction.Ignore))
		{
			this.ghostAppearSource.volume = 0.1f;
			return;
		}
		this.ghostAppearSource.volume = 0f;
	}

	// Token: 0x04000651 RID: 1617
	private PhotonView view;

	// Token: 0x04000652 RID: 1618
	private GhostAI ghostAI;

	// Token: 0x04000653 RID: 1619
	[SerializeField]
	private AudioSource soundFXSource;

	// Token: 0x04000654 RID: 1620
	[SerializeField]
	private AudioClip hummingSound;

	// Token: 0x04000655 RID: 1621
	[SerializeField]
	private AudioClip[] screamSoundClips;

	// Token: 0x04000656 RID: 1622
	[SerializeField]
	private AudioSource ghostAppearSource;
}
