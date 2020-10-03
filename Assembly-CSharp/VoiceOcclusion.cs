using System;
using UnityEngine;

// Token: 0x020001BC RID: 444
public class VoiceOcclusion : MonoBehaviour
{
	// Token: 0x06000C37 RID: 3127 RVA: 0x0004CDBA File Offset: 0x0004AFBA
	public void SetVoiceMixer()
	{
		if (!this.walkieTalkie.isOn)
		{
			this.source.outputAudioMixerGroup = SoundController.instance.GetAudioGroupFromSnapshot(this.player.currentPlayerSnapshot);
		}
	}

	// Token: 0x04000CC4 RID: 3268
	public AudioSource source;

	// Token: 0x04000CC5 RID: 3269
	[SerializeField]
	private Player player;

	// Token: 0x04000CC6 RID: 3270
	[SerializeField]
	private WalkieTalkie walkieTalkie;
}
