using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200044B RID: 1099
	public class SoundBowClick : MonoBehaviour
	{
		// Token: 0x060021CF RID: 8655 RVA: 0x000A77A1 File Offset: 0x000A59A1
		private void Awake()
		{
			this.thisAudioSource = base.GetComponent<AudioSource>();
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000A77B0 File Offset: 0x000A59B0
		public void PlayBowTensionClicks(float normalizedTension)
		{
			float num = this.pitchTensionCurve.Evaluate(normalizedTension);
			this.thisAudioSource.pitch = (this.maxPitch - this.minPitch) * num + this.minPitch;
			this.thisAudioSource.PlayOneShot(this.bowClick);
		}

		// Token: 0x04001F55 RID: 8021
		public AudioClip bowClick;

		// Token: 0x04001F56 RID: 8022
		public AnimationCurve pitchTensionCurve;

		// Token: 0x04001F57 RID: 8023
		public float minPitch;

		// Token: 0x04001F58 RID: 8024
		public float maxPitch;

		// Token: 0x04001F59 RID: 8025
		private AudioSource thisAudioSource;
	}
}
