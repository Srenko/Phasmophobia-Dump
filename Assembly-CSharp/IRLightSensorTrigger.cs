using System;
using UnityEngine;

// Token: 0x0200015C RID: 348
public class IRLightSensorTrigger : MonoBehaviour
{
	// Token: 0x06000948 RID: 2376 RVA: 0x000385DC File Offset: 0x000367DC
	private void OnTriggerEnter(Collider other)
	{
		if (!this.irLightSensor.isPlaced)
		{
			return;
		}
		if (other.isTrigger)
		{
			return;
		}
		if (PhotonNetwork.isMasterClient && other.CompareTag("Ghost"))
		{
			this.irLightSensor.Detection();
		}
		if (other.transform.root.CompareTag("Player"))
		{
			this.irLightSensor.Detection();
		}
	}

	// Token: 0x0400095B RID: 2395
	[SerializeField]
	private IRLightSensor irLightSensor;
}
