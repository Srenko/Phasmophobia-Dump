using System;
using UnityEngine;

// Token: 0x02000133 RID: 307
public class Noise : MonoBehaviour
{
	// Token: 0x06000822 RID: 2082 RVA: 0x00030EA1 File Offset: 0x0002F0A1
	public void OnEnable()
	{
		if (this.source)
		{
			this.source.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		}
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00030ED8 File Offset: 0x0002F0D8
	public void PlaySound(AudioClip clip, float volume)
	{
		this.source.volume = volume;
		this.source.clip = clip;
		this.source.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		this.source.Play();
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x00030F2D File Offset: 0x0002F12D
	private void Update()
	{
		if (!this.source)
		{
			this.timer -= Time.deltaTime;
			if (this.timer < 0f)
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x04000838 RID: 2104
	public float volume;

	// Token: 0x04000839 RID: 2105
	private float timer = 3f;

	// Token: 0x0400083A RID: 2106
	public AudioSource source;
}
