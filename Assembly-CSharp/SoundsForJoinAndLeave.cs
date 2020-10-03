using System;
using Photon;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class SoundsForJoinAndLeave : PunBehaviour
{
	// Token: 0x06000020 RID: 32 RVA: 0x00002969 File Offset: 0x00000B69
	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		if (this.JoinClip != null)
		{
			if (this.source == null)
			{
				this.source = Object.FindObjectOfType<AudioSource>();
			}
			this.source.PlayOneShot(this.JoinClip);
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000029A3 File Offset: 0x00000BA3
	public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{
		if (this.LeaveClip != null)
		{
			if (this.source == null)
			{
				this.source = Object.FindObjectOfType<AudioSource>();
			}
			this.source.PlayOneShot(this.LeaveClip);
		}
	}

	// Token: 0x0400002F RID: 47
	public AudioClip JoinClip;

	// Token: 0x04000030 RID: 48
	public AudioClip LeaveClip;

	// Token: 0x04000031 RID: 49
	private AudioSource source;
}
