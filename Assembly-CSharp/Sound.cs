using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200014E RID: 334
[RequireComponent(typeof(PhotonView))]
public class Sound : MonoBehaviour
{
	// Token: 0x060008C9 RID: 2249 RVA: 0x00034E34 File Offset: 0x00033034
	private void Awake()
	{
		if (this.view == null)
		{
			this.view = base.GetComponent<PhotonView>();
		}
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00034E61 File Offset: 0x00033061
	public void Use()
	{
		this.view.RPC("NetworkedUse", PhotonTargets.All, new object[]
		{
			Random.Range(0, this.clips.Length)
		});
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00034E90 File Offset: 0x00033090
	[PunRPC]
	private void NetworkedUse(int id)
	{
		base.StartCoroutine(this.PlayNoiseObject());
		this.source.clip = this.clips[id];
		this.source.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		this.source.Play();
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x00034EED File Offset: 0x000330ED
	private IEnumerator PlayNoiseObject()
	{
		this.noise.gameObject.SetActive(true);
		yield return 0;
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x040008E1 RID: 2273
	[SerializeField]
	private PhotonView view;

	// Token: 0x040008E2 RID: 2274
	[SerializeField]
	private Noise noise;

	// Token: 0x040008E3 RID: 2275
	[SerializeField]
	private AudioClip[] clips;

	// Token: 0x040008E4 RID: 2276
	[SerializeField]
	private AudioSource source;
}
