using System;
using System.Collections;
using Photon;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001CD RID: 461
[RequireComponent(typeof(PhotonView))]
public class TruckRadioController : Photon.MonoBehaviour
{
	// Token: 0x06000C9B RID: 3227 RVA: 0x00050608 File Offset: 0x0004E808
	private void Awake()
	{
		TruckRadioController.instance = this;
		this.view = base.GetComponent<PhotonView>();
		this.source = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x00050628 File Offset: 0x0004E828
	private void Start()
	{
		if (GameController.instance)
		{
			GameController.instance.OnAllPlayersConnected.AddListener(new UnityAction(this.PlayIntroductionAudio));
		}
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x00050654 File Offset: 0x0004E854
	public void PlayIntroductionAudio()
	{
		if (this.source == null)
		{
			return;
		}
		if (!this.view.isMine)
		{
			return;
		}
		int num = Random.Range(0, this.introductionClips.Length);
		this.view.RPC("PlayAudioClip", PhotonTargets.All, new object[]
		{
			0,
			num
		});
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x000506B8 File Offset: 0x0004E8B8
	public void PlayKeyWarningAudio()
	{
		if (!this.view.isMine)
		{
			return;
		}
		if (this.source.isPlaying)
		{
			return;
		}
		int num = Random.Range(0, this.keyWarningClips.Length);
		this.view.RPC("PlayAudioClip", PhotonTargets.All, new object[]
		{
			2,
			num
		});
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x0005071C File Offset: 0x0004E91C
	public void PlayHintAudio()
	{
		if (!PhotonNetwork.isMasterClient)
		{
			return;
		}
		switch (Random.Range(0, 4))
		{
		case 0:
			this.view.RPC("PlayAudioClip", PhotonTargets.All, new object[]
			{
				5,
				Random.Range(0, this.nonFriendlyHintClips.Length)
			});
			return;
		case 1:
			this.view.RPC("PlayAudioClip", PhotonTargets.All, new object[]
			{
				4,
				Random.Range(0, this.friendlyHintClips.Length)
			});
			return;
		case 2:
			this.view.RPC("PlayAudioClip", PhotonTargets.All, new object[]
			{
				3,
				Random.Range(0, this.aggressiveHintClips.Length)
			});
			return;
		case 3:
			this.view.RPC("PlayAudioClip", PhotonTargets.All, new object[]
			{
				1,
				Random.Range(0, this.noHintClips.Length)
			});
			return;
		default:
			return;
		}
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x0005082B File Offset: 0x0004EA2B
	[PunRPC]
	private IEnumerator PlayAudioClip(int id, int rand)
	{
		if (!this.view.isMine)
		{
			yield return null;
		}
		switch (id)
		{
		case 0:
			yield return new WaitForSeconds(2f);
			this.source.clip = this.introductionClips[rand];
			this.source.Play();
			yield return new WaitForSeconds(this.introductionClips[rand].length + 2f);
			this.PlayHintAudio();
			break;
		case 1:
			this.source.clip = this.noHintClips[rand];
			break;
		case 2:
			if (!this.playedKeyAudio)
			{
				this.source.clip = this.keyWarningClips[rand];
				this.playedKeyAudio = true;
			}
			break;
		case 3:
			this.source.clip = this.aggressiveHintClips[rand];
			break;
		case 4:
			this.source.clip = this.friendlyHintClips[rand];
			break;
		case 5:
			this.source.clip = this.nonFriendlyHintClips[rand];
			break;
		}
		if (id != 0)
		{
			this.source.Play();
		}
		yield break;
	}

	// Token: 0x04000D42 RID: 3394
	public static TruckRadioController instance;

	// Token: 0x04000D43 RID: 3395
	private PhotonView view;

	// Token: 0x04000D44 RID: 3396
	private AudioSource source;

	// Token: 0x04000D45 RID: 3397
	[HideInInspector]
	public bool playedKeyAudio;

	// Token: 0x04000D46 RID: 3398
	[SerializeField]
	private AudioClip[] introductionClips;

	// Token: 0x04000D47 RID: 3399
	[SerializeField]
	private AudioClip[] keyWarningClips;

	// Token: 0x04000D48 RID: 3400
	[SerializeField]
	private AudioClip[] noHintClips;

	// Token: 0x04000D49 RID: 3401
	[SerializeField]
	private AudioClip[] aggressiveHintClips;

	// Token: 0x04000D4A RID: 3402
	[SerializeField]
	private AudioClip[] friendlyHintClips;

	// Token: 0x04000D4B RID: 3403
	[SerializeField]
	private AudioClip[] nonFriendlyHintClips;
}
