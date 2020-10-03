using System;
using UnityEngine;
using VRTK;

// Token: 0x02000171 RID: 369
public class WiccanAltar : MonoBehaviour
{
	// Token: 0x040009E8 RID: 2536
	[SerializeField]
	private Transform[] markers;

	// Token: 0x040009E9 RID: 2537
	[SerializeField]
	private Candle candle_1;

	// Token: 0x040009EA RID: 2538
	[SerializeField]
	private Candle candle_2;

	// Token: 0x040009EB RID: 2539
	[SerializeField]
	private Light myLight;

	// Token: 0x040009EC RID: 2540
	[SerializeField]
	private AudioSource source;

	// Token: 0x040009ED RID: 2541
	[SerializeField]
	private ParticleSystem particles;

	// Token: 0x040009EE RID: 2542
	public VRTK_SnapDropZone[] zones;

	// Token: 0x040009EF RID: 2543
	private PhotonObjectInteract photonInteract;

	// Token: 0x040009F0 RID: 2544
	private Renderer rend;

	// Token: 0x040009F1 RID: 2545
	[HideInInspector]
	public PhotonView view;

	// Token: 0x040009F2 RID: 2546
	public static WiccanAltar instance;

	// Token: 0x040009F3 RID: 2547
	private bool inUse;
}
