using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200014D RID: 333
public class Sink : MonoBehaviour
{
	// Token: 0x060008C1 RID: 2241 RVA: 0x00034C77 File Offset: 0x00032E77
	private void Awake()
	{
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.view = base.GetComponent<PhotonView>();
		this.noise.gameObject.SetActive(false);
		this.evidence.enabled = false;
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x00034CAE File Offset: 0x00032EAE
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x00034CC7 File Offset: 0x00032EC7
	public void Use()
	{
		this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x00034CDF File Offset: 0x00032EDF
	public void SpawnDirtyWater()
	{
		this.view.RPC("SpawnDirtyWaterSync", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x00034CF8 File Offset: 0x00032EF8
	[PunRPC]
	private void SpawnDirtyWaterSync()
	{
		for (int i = 0; i < this.waterRends.Length; i++)
		{
			this.waterRends[i].material.color = new Color32(115, 80, 60, 180);
		}
		this.evidence.enabled = true;
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x00034D4C File Offset: 0x00032F4C
	[PunRPC]
	private void NetworkedUse()
	{
		this.waterIsOn = !this.waterIsOn;
		this.tapWater.SetActive(this.waterIsOn);
		if (this.waterIsOn)
		{
			this.source.Play();
			this.noise.gameObject.SetActive(true);
			return;
		}
		this.source.Stop();
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x00034DBC File Offset: 0x00032FBC
	private void Update()
	{
		if (this.waterIsOn && this.timer > 0f)
		{
			this.timer -= Time.deltaTime;
			this.water.localPosition = Vector3.MoveTowards(this.water.localPosition, this.target.localPosition, 0.01f * Time.deltaTime);
		}
	}

	// Token: 0x040008D6 RID: 2262
	private PhotonObjectInteract photonInteract;

	// Token: 0x040008D7 RID: 2263
	private PhotonView view;

	// Token: 0x040008D8 RID: 2264
	[SerializeField]
	private Transform water;

	// Token: 0x040008D9 RID: 2265
	[SerializeField]
	private Transform target;

	// Token: 0x040008DA RID: 2266
	private float timer = 20f;

	// Token: 0x040008DB RID: 2267
	private bool waterIsOn;

	// Token: 0x040008DC RID: 2268
	[SerializeField]
	private AudioSource source;

	// Token: 0x040008DD RID: 2269
	[SerializeField]
	private GameObject tapWater;

	// Token: 0x040008DE RID: 2270
	[SerializeField]
	private Noise noise;

	// Token: 0x040008DF RID: 2271
	[SerializeField]
	private MeshRenderer[] waterRends;

	// Token: 0x040008E0 RID: 2272
	[SerializeField]
	private Evidence evidence;
}
