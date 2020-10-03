using System;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class SupportLogger : MonoBehaviour
{
	// Token: 0x06000630 RID: 1584 RVA: 0x000229B5 File Offset: 0x00020BB5
	public void Start()
	{
		if (GameObject.Find("PunSupportLogger") == null)
		{
			GameObject gameObject = new GameObject("PunSupportLogger");
			Object.DontDestroyOnLoad(gameObject);
			gameObject.AddComponent<SupportLogging>().LogTrafficStats = this.LogTrafficStats;
		}
	}

	// Token: 0x0400061A RID: 1562
	public bool LogTrafficStats = true;
}
