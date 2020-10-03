using System;
using UnityEngine;

// Token: 0x02000160 RID: 352
public class MotionSensorTrigger : MonoBehaviour
{
	// Token: 0x06000967 RID: 2407 RVA: 0x00039178 File Offset: 0x00037378
	private void OnTriggerEnter(Collider other)
	{
		if (!this.motionSensor.isPlaced)
		{
			return;
		}
		if (!other.isTrigger)
		{
			if (PhotonNetwork.isMasterClient && other.CompareTag("Ghost"))
			{
				this.motionSensor.Detection(true);
			}
			if (other.transform.root.CompareTag("Player"))
			{
				this.motionSensor.Detection(false);
			}
		}
	}

	// Token: 0x04000981 RID: 2433
	[SerializeField]
	private MotionSensor motionSensor;
}
