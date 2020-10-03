using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class HighlightSpeaker : MonoBehaviour
{
	// Token: 0x06000097 RID: 151 RVA: 0x00004E18 File Offset: 0x00003018
	private void Start()
	{
		this.speaker = base.transform.parent.GetComponent<PhotonVoiceSpeaker>();
		if (this.speaker == null)
		{
			base.enabled = false;
			return;
		}
		this.rendererComp = base.GetComponent<Renderer>();
		if (this.rendererComp == null)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00004E73 File Offset: 0x00003073
	private void Update()
	{
		if (this.speaker != null)
		{
			this.rendererComp.enabled = this.speaker.IsPlaying;
		}
	}

	// Token: 0x0400006A RID: 106
	private PhotonVoiceSpeaker speaker;

	// Token: 0x0400006B RID: 107
	private Renderer rendererComp;
}
