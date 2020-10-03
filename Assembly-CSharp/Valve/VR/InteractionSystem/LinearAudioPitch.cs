using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200042B RID: 1067
	public class LinearAudioPitch : MonoBehaviour
	{
		// Token: 0x060020C0 RID: 8384 RVA: 0x000A19DA File Offset: 0x0009FBDA
		private void Awake()
		{
			if (this.audioSource == null)
			{
				this.audioSource = base.GetComponent<AudioSource>();
			}
			if (this.linearMapping == null)
			{
				this.linearMapping = base.GetComponent<LinearMapping>();
			}
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000A1A10 File Offset: 0x0009FC10
		private void Update()
		{
			if (this.applyContinuously)
			{
				this.Apply();
			}
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000A1A20 File Offset: 0x0009FC20
		private void Apply()
		{
			float t = this.pitchCurve.Evaluate(this.linearMapping.value);
			this.audioSource.pitch = Mathf.Lerp(this.minPitch, this.maxPitch, t);
		}

		// Token: 0x04001E44 RID: 7748
		public LinearMapping linearMapping;

		// Token: 0x04001E45 RID: 7749
		public AnimationCurve pitchCurve;

		// Token: 0x04001E46 RID: 7750
		public float minPitch;

		// Token: 0x04001E47 RID: 7751
		public float maxPitch;

		// Token: 0x04001E48 RID: 7752
		public bool applyContinuously = true;

		// Token: 0x04001E49 RID: 7753
		private AudioSource audioSource;
	}
}
