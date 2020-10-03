using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000435 RID: 1077
	public class SoundPlayOneshot : MonoBehaviour
	{
		// Token: 0x060020FA RID: 8442 RVA: 0x000A2BBD File Offset: 0x000A0DBD
		private void Awake()
		{
			this.thisAudioSource = base.GetComponent<AudioSource>();
			if (this.playOnAwake)
			{
				this.Play();
			}
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x000A2BDC File Offset: 0x000A0DDC
		public void Play()
		{
			if (this.thisAudioSource != null && this.thisAudioSource.isActiveAndEnabled && !Util.IsNullOrEmpty<AudioClip>(this.waveFiles))
			{
				this.thisAudioSource.volume = Random.Range(this.volMin, this.volMax);
				this.thisAudioSource.pitch = Random.Range(this.pitchMin, this.pitchMax);
				this.thisAudioSource.PlayOneShot(this.waveFiles[Random.Range(0, this.waveFiles.Length)]);
			}
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000A2C69 File Offset: 0x000A0E69
		public void Pause()
		{
			if (this.thisAudioSource != null)
			{
				this.thisAudioSource.Pause();
			}
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000A2C84 File Offset: 0x000A0E84
		public void UnPause()
		{
			if (this.thisAudioSource != null)
			{
				this.thisAudioSource.UnPause();
			}
		}

		// Token: 0x04001E82 RID: 7810
		public AudioClip[] waveFiles;

		// Token: 0x04001E83 RID: 7811
		private AudioSource thisAudioSource;

		// Token: 0x04001E84 RID: 7812
		public float volMin;

		// Token: 0x04001E85 RID: 7813
		public float volMax;

		// Token: 0x04001E86 RID: 7814
		public float pitchMin;

		// Token: 0x04001E87 RID: 7815
		public float pitchMax;

		// Token: 0x04001E88 RID: 7816
		public bool playOnAwake;
	}
}
