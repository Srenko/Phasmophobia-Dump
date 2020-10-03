using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class BugRegionSpawner : MonoBehaviour
{
	// Token: 0x06000109 RID: 265 RVA: 0x00008A00 File Offset: 0x00006C00
	private void Start()
	{
		for (int i = 0; i < this.amountToSpawn; i++)
		{
			BugsAI component = PhotonNetwork.Instantiate(this.bugToSpawn.name, base.transform.position, this.bugToSpawn.transform.rotation, 0, null).GetComponent<BugsAI>();
			component.col = this.regionCollider;
			component.transform.SetParent(base.transform);
		}
	}

	// Token: 0x04000138 RID: 312
	[SerializeField]
	private BoxCollider regionCollider;

	// Token: 0x04000139 RID: 313
	[SerializeField]
	private int amountToSpawn;

	// Token: 0x0400013A RID: 314
	[SerializeField]
	private GameObject bugToSpawn;
}
