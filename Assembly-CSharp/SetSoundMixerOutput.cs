using System;
using UnityEngine;

// Token: 0x0200013D RID: 317
[RequireComponent(typeof(AudioSource))]
public class SetSoundMixerOutput : MonoBehaviour
{
	// Token: 0x06000849 RID: 2121 RVA: 0x000317BD File Offset: 0x0002F9BD
	private void Start()
	{
		if (SoundController.instance)
		{
			base.GetComponent<AudioSource>().outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		}
	}
}
