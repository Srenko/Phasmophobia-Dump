using System;
using UnityEngine;

// Token: 0x0200013F RID: 319
public class TriggerBox : MonoBehaviour
{
	// Token: 0x0600084C RID: 2124 RVA: 0x00031955 File Offset: 0x0002FB55
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.gameObject.name);
	}
}
