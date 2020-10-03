using System;
using UnityEngine;

// Token: 0x02000151 RID: 337
public class Window : MonoBehaviour
{
	// Token: 0x060008D8 RID: 2264 RVA: 0x000350E0 File Offset: 0x000332E0
	private void Awake()
	{
		this.knockingSource = base.GetComponent<AudioSource>();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x000350FA File Offset: 0x000332FA
	private void Start()
	{
		if (this.handPrintObject != null)
		{
			this.handPrintObject.SetActive(false);
		}
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00035116 File Offset: 0x00033316
	public void PlayKnockingSound()
	{
		this.view.RPC("PlayKnockingSoundSynced", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x00035130 File Offset: 0x00033330
	[PunRPC]
	private void PlayKnockingSoundSynced()
	{
		this.knockingSource.clip = this.windowAudioClips[Random.Range(0, this.windowAudioClips.Length)];
		this.knockingSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		this.knockingSource.Play();
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x0003518D File Offset: 0x0003338D
	public void SpawnHandPrintEvidence()
	{
		if (this.handPrintObject == null)
		{
			return;
		}
		if (this.handPrintObject.activeInHierarchy)
		{
			return;
		}
		this.view.RPC("NetworkedSpawnHandPrintEvidence", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x000351C2 File Offset: 0x000333C2
	[PunRPC]
	private void NetworkedSpawnHandPrintEvidence()
	{
		this.handPrintObject.GetComponent<Renderer>().material = EvidenceController.instance.handPrintMaterials[Random.Range(0, EvidenceController.instance.handPrintMaterials.Length)];
		this.handPrintObject.SetActive(true);
	}

	// Token: 0x040008EC RID: 2284
	[HideInInspector]
	public PhotonView view;

	// Token: 0x040008ED RID: 2285
	private AudioSource knockingSource;

	// Token: 0x040008EE RID: 2286
	[SerializeField]
	private AudioClip[] windowAudioClips;

	// Token: 0x040008EF RID: 2287
	[SerializeField]
	private GameObject handPrintObject;

	// Token: 0x040008F0 RID: 2288
	public Transform windowGhostStart;

	// Token: 0x040008F1 RID: 2289
	public Transform windowGhostEnd;
}
