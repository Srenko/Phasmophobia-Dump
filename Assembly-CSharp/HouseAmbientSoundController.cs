using System;
using System.Collections.Generic;
using Photon;
using UnityEngine;

// Token: 0x02000118 RID: 280
[RequireComponent(typeof(PhotonView))]
public class HouseAmbientSoundController : Photon.MonoBehaviour
{
	// Token: 0x06000778 RID: 1912 RVA: 0x0002B8C0 File Offset: 0x00029AC0
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.col = base.GetComponent<BoxCollider>();
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0002B8DC File Offset: 0x00029ADC
	private void Update()
	{
		if (!PhotonNetwork.inRoom)
		{
			return;
		}
		if (!PhotonNetwork.isMasterClient)
		{
			return;
		}
		this.timer -= Time.deltaTime;
		if (this.timer < 0f)
		{
			if (!this.src)
			{
				Vector3 vector = new Vector3(Random.Range(this.col.bounds.min.x, this.col.bounds.max.x), base.transform.position.y, Random.Range(this.col.bounds.min.z, this.col.bounds.max.z));
				this.view.RPC("PlaySound", PhotonTargets.All, new object[]
				{
					vector,
					Random.Range(0, this.clips.Count)
				});
			}
			this.timer = Random.Range(5f, 20f);
		}
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x0002B9FC File Offset: 0x00029BFC
	[PunRPC]
	private void PlaySound(Vector3 pos, int clipID)
	{
		if (GameController.instance.myPlayer != null && GameController.instance.myPlayer.player.isDead)
		{
			return;
		}
		if (this.roomSpecific)
		{
			if (LevelController.instance.currentPlayerRoom == this.specificRoom)
			{
				ObjectPooler.instance.SpawnFromPool("Noise", pos, Quaternion.identity).GetComponent<Noise>().PlaySound(this.clips[clipID], 0.6f);
				return;
			}
		}
		else
		{
			ObjectPooler.instance.SpawnFromPool("Noise", pos, Quaternion.identity).GetComponent<Noise>().PlaySound(this.clips[clipID], 0.15f);
		}
	}

	// Token: 0x04000726 RID: 1830
	private BoxCollider col;

	// Token: 0x04000727 RID: 1831
	private float timer;

	// Token: 0x04000728 RID: 1832
	public List<AudioClip> clips = new List<AudioClip>();

	// Token: 0x04000729 RID: 1833
	private AudioSource src;

	// Token: 0x0400072A RID: 1834
	private PhotonView view;

	// Token: 0x0400072B RID: 1835
	[SerializeField]
	private LevelRoom specificRoom;

	// Token: 0x0400072C RID: 1836
	[SerializeField]
	private bool roomSpecific;
}
