using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000024 RID: 36
public class ElevatorManager : MonoBehaviour
{
	// Token: 0x060000FA RID: 250 RVA: 0x00008337 File Offset: 0x00006537
	private void Awake()
	{
		if (this.RandomStartFloor)
		{
			this.elevatorsCount = base.transform.childCount;
			this.InitialFloor = Random.Range(1, this.elevatorsCount + 1);
			this.WasStarted();
		}
	}

	// Token: 0x04000111 RID: 273
	private int elevatorsCount;

	// Token: 0x04000112 RID: 274
	public bool RandomStartFloor = true;

	// Token: 0x04000113 RID: 275
	public int InitialFloor = 1;

	// Token: 0x04000114 RID: 276
	public UnityAction WasStarted;

	// Token: 0x04000115 RID: 277
	[HideInInspector]
	public int _floor;

	// Token: 0x04000116 RID: 278
	private Transform[] elevators;
}
