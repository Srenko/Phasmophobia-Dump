using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000434 RID: 1076
	public class SoundDeparent : MonoBehaviour
	{
		// Token: 0x060020F7 RID: 8439 RVA: 0x000A2B6A File Offset: 0x000A0D6A
		private void Awake()
		{
			this.thisAudioSource = base.GetComponent<AudioSource>();
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000A2B78 File Offset: 0x000A0D78
		private void Start()
		{
			base.gameObject.transform.parent = null;
			if (this.destroyAfterPlayOnce)
			{
				Object.Destroy(base.gameObject, this.thisAudioSource.clip.length);
			}
		}

		// Token: 0x04001E80 RID: 7808
		public bool destroyAfterPlayOnce = true;

		// Token: 0x04001E81 RID: 7809
		private AudioSource thisAudioSource;
	}
}
