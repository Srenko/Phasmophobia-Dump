using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class HighlightRecorder : MonoBehaviour
{
	// Token: 0x06000094 RID: 148 RVA: 0x00004D94 File Offset: 0x00002F94
	private void Start()
	{
		this.recorder = base.transform.parent.GetComponent<PhotonVoiceRecorder>();
		if (this.recorder == null)
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

	// Token: 0x06000095 RID: 149 RVA: 0x00004DEF File Offset: 0x00002FEF
	private void Update()
	{
		if (this.recorder != null)
		{
			this.rendererComp.enabled = this.recorder.IsTransmitting;
		}
	}

	// Token: 0x04000068 RID: 104
	private PhotonVoiceRecorder recorder;

	// Token: 0x04000069 RID: 105
	private Renderer rendererComp;
}
