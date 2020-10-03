using System;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class ColliderHit : MonoBehaviour
{
	// Token: 0x0600082A RID: 2090 RVA: 0x0003101D File Offset: 0x0002F21D
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Collision Enter: " + collision.gameObject.name);
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00031039 File Offset: 0x0002F239
	private void OnCollisionExit(Collision collision)
	{
		Debug.Log("Collision Exit: " + collision.gameObject.name);
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00031055 File Offset: 0x0002F255
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Trigger Stay: " + other.gameObject.name);
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00031055 File Offset: 0x0002F255
	private void OnTriggerExit(Collider other)
	{
		Debug.Log("Trigger Stay: " + other.gameObject.name);
	}
}
