using System;
using UnityEngine;
using UnityEngine.XR;

// Token: 0x020001BD RID: 445
public class VoiceVolume : MonoBehaviour
{
	// Token: 0x06000C39 RID: 3129 RVA: 0x0004CDE9 File Offset: 0x0004AFE9
	private void Awake()
	{
		this.source = base.GetComponent<AudioSource>();
		this.startVolume = this.source.volume;
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x0004CE08 File Offset: 0x0004B008
	private void Start()
	{
		if (MainManager.instance)
		{
			this.source.outputAudioMixerGroup = SoundController.instance.GetAudioGroupFromSnapshot(SoundController.instance.firstFloorSnapshot);
			this.source.volume = this.startVolume;
			return;
		}
		this.source.volume = this.startVolume * (XRDevice.isPresent ? PauseMenuManager.instance.GetPlayerVolume(this.view.ownerId) : PauseMenuController.instance.GetPlayerVolume(this.view.ownerId));
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x0004CE97 File Offset: 0x0004B097
	public void ApplyVoiceVolume(float volume)
	{
		if (MainManager.instance)
		{
			this.source.volume = this.startVolume;
			return;
		}
		this.source.volume = this.startVolume * volume;
	}

	// Token: 0x04000CC7 RID: 3271
	private AudioSource source;

	// Token: 0x04000CC8 RID: 3272
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000CC9 RID: 3273
	private float startVolume;
}
