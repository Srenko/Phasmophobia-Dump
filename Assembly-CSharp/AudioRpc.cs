using System;
using Photon;
using UnityEngine;

// Token: 0x02000073 RID: 115
public class AudioRpc : Photon.MonoBehaviour
{
	// Token: 0x06000295 RID: 661 RVA: 0x00011776 File Offset: 0x0000F976
	private void Awake()
	{
		this.m_Source = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000296 RID: 662 RVA: 0x00011784 File Offset: 0x0000F984
	[PunRPC]
	private void Marco()
	{
		if (!base.enabled)
		{
			return;
		}
		Debug.Log("Marco");
		this.m_Source.clip = this.marco;
		this.m_Source.Play();
	}

	// Token: 0x06000297 RID: 663 RVA: 0x000117B5 File Offset: 0x0000F9B5
	[PunRPC]
	private void Polo()
	{
		if (!base.enabled)
		{
			return;
		}
		Debug.Log("Polo");
		this.m_Source.clip = this.polo;
		this.m_Source.Play();
	}

	// Token: 0x06000298 RID: 664 RVA: 0x000117E6 File Offset: 0x0000F9E6
	private void OnApplicationFocus(bool focus)
	{
		base.enabled = focus;
	}

	// Token: 0x040002E5 RID: 741
	public AudioClip marco;

	// Token: 0x040002E6 RID: 742
	public AudioClip polo;

	// Token: 0x040002E7 RID: 743
	private AudioSource m_Source;
}
