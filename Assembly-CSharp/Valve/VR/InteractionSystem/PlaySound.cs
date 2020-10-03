using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000430 RID: 1072
	[RequireComponent(typeof(AudioSource))]
	public class PlaySound : MonoBehaviour
	{
		// Token: 0x060020D3 RID: 8403 RVA: 0x000A1EA4 File Offset: 0x000A00A4
		private void Awake()
		{
			this.audioSource = base.GetComponent<AudioSource>();
			this.clip = this.audioSource.clip;
			if (!this.audioSource.playOnAwake)
			{
				if (!this.audioSource.playOnAwake && this.playOnAwakeWithDelay)
				{
					this.PlayWithDelay(this.delayOffsetTime);
					if (this.useRetriggerTime)
					{
						base.InvokeRepeating("Play", this.timeInitial, Random.Range(this.timeMin, this.timeMax));
						return;
					}
				}
				else if (this.audioSource.playOnAwake && this.playOnAwakeWithDelay)
				{
					this.PlayWithDelay(this.delayOffsetTime);
					if (this.useRetriggerTime)
					{
						base.InvokeRepeating("Play", this.timeInitial, Random.Range(this.timeMin, this.timeMax));
					}
				}
				return;
			}
			if (this.useRetriggerTime)
			{
				base.InvokeRepeating("Play", this.timeInitial, Random.Range(this.timeMin, this.timeMax));
				return;
			}
			this.Play();
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000A1FA4 File Offset: 0x000A01A4
		public void Play()
		{
			if (this.looping)
			{
				this.PlayLooping();
				return;
			}
			this.PlayOneShotSound();
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000A1FBD File Offset: 0x000A01BD
		public void PlayWithDelay(float delayTime)
		{
			if (this.looping)
			{
				base.Invoke("PlayLooping", delayTime);
				return;
			}
			base.Invoke("PlayOneShotSound", delayTime);
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000A1FE0 File Offset: 0x000A01E0
		public AudioClip PlayOneShotSound()
		{
			if (!this.audioSource.isActiveAndEnabled)
			{
				return null;
			}
			this.SetAudioSource();
			if (this.stopOnPlay)
			{
				this.audioSource.Stop();
			}
			if (this.disableOnEnd)
			{
				base.Invoke("Disable", this.clip.length);
			}
			this.audioSource.PlayOneShot(this.clip);
			return this.clip;
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000A204C File Offset: 0x000A024C
		public AudioClip PlayLooping()
		{
			this.SetAudioSource();
			if (!this.audioSource.loop)
			{
				this.audioSource.loop = true;
			}
			this.audioSource.Play();
			if (this.stopOnEnd)
			{
				base.Invoke("Stop", this.audioSource.clip.length);
			}
			return this.clip;
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x0000AC1C File Offset: 0x00008E1C
		public void Disable()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000A20AC File Offset: 0x000A02AC
		public void Stop()
		{
			this.audioSource.Stop();
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000A20BC File Offset: 0x000A02BC
		private void SetAudioSource()
		{
			if (this.useRandomVolume)
			{
				this.audioSource.volume = Random.Range(this.volMin, this.volMax);
				if (this.useRandomSilence && (float)Random.Range(0, 1) < this.percentToNotPlay)
				{
					this.audioSource.volume = 0f;
				}
			}
			if (this.useRandomPitch)
			{
				this.audioSource.pitch = Random.Range(this.pitchMin, this.pitchMax);
			}
			if (this.waveFile.Length != 0)
			{
				this.audioSource.clip = this.waveFile[Random.Range(0, this.waveFile.Length)];
				this.clip = this.audioSource.clip;
			}
		}

		// Token: 0x04001E5D RID: 7773
		[Tooltip("List of audio clips to play.")]
		public AudioClip[] waveFile;

		// Token: 0x04001E5E RID: 7774
		[Tooltip("Stops the currently playing clip in the audioSource. Otherwise clips will overlap/mix.")]
		public bool stopOnPlay;

		// Token: 0x04001E5F RID: 7775
		[Tooltip("After the audio clip finishes playing, disable the game object the sound is on.")]
		public bool disableOnEnd;

		// Token: 0x04001E60 RID: 7776
		[Tooltip("Loop the sound after the wave file variation has been chosen.")]
		public bool looping;

		// Token: 0x04001E61 RID: 7777
		[Tooltip("If the sound is looping and updating it's position every frame, stop the sound at the end of the wav/clip length. ")]
		public bool stopOnEnd;

		// Token: 0x04001E62 RID: 7778
		[Tooltip("Start a wave file playing on awake, but after a delay.")]
		public bool playOnAwakeWithDelay;

		// Token: 0x04001E63 RID: 7779
		[Header("Random Volume")]
		public bool useRandomVolume = true;

		// Token: 0x04001E64 RID: 7780
		[Tooltip("Minimum volume that will be used when randomly set.")]
		[Range(0f, 1f)]
		public float volMin = 1f;

		// Token: 0x04001E65 RID: 7781
		[Tooltip("Maximum volume that will be used when randomly set.")]
		[Range(0f, 1f)]
		public float volMax = 1f;

		// Token: 0x04001E66 RID: 7782
		[Header("Random Pitch")]
		[Tooltip("Use min and max random pitch levels when playing sounds.")]
		public bool useRandomPitch = true;

		// Token: 0x04001E67 RID: 7783
		[Tooltip("Minimum pitch that will be used when randomly set.")]
		[Range(-3f, 3f)]
		public float pitchMin = 1f;

		// Token: 0x04001E68 RID: 7784
		[Tooltip("Maximum pitch that will be used when randomly set.")]
		[Range(-3f, 3f)]
		public float pitchMax = 1f;

		// Token: 0x04001E69 RID: 7785
		[Header("Random Time")]
		[Tooltip("Use Retrigger Time to repeat the sound within a time range")]
		public bool useRetriggerTime;

		// Token: 0x04001E6A RID: 7786
		[Tooltip("Inital time before the first repetion starts")]
		[Range(0f, 360f)]
		public float timeInitial;

		// Token: 0x04001E6B RID: 7787
		[Tooltip("Minimum time that will pass before the sound is retriggered")]
		[Range(0f, 360f)]
		public float timeMin;

		// Token: 0x04001E6C RID: 7788
		[Tooltip("Maximum pitch that will be used when randomly set.")]
		[Range(0f, 360f)]
		public float timeMax;

		// Token: 0x04001E6D RID: 7789
		[Header("Random Silence")]
		[Tooltip("Use Retrigger Time to repeat the sound within a time range")]
		public bool useRandomSilence;

		// Token: 0x04001E6E RID: 7790
		[Tooltip("Percent chance that the wave file will not play")]
		[Range(0f, 1f)]
		public float percentToNotPlay;

		// Token: 0x04001E6F RID: 7791
		[Header("Delay Time")]
		[Tooltip("Time to offset playback of sound")]
		public float delayOffsetTime;

		// Token: 0x04001E70 RID: 7792
		private AudioSource audioSource;

		// Token: 0x04001E71 RID: 7793
		private AudioClip clip;
	}
}
