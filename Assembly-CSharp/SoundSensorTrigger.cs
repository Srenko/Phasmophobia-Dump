using System;
using UnityEngine;

// Token: 0x02000169 RID: 361
public class SoundSensorTrigger : MonoBehaviour
{
	// Token: 0x060009A7 RID: 2471 RVA: 0x0003B82C File Offset: 0x00039A2C
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Noise>())
		{
			Noise component = other.GetComponent<Noise>();
			if (component.volume > this.soundSensor.highestVolume)
			{
				this.soundSensor.highestVolume = component.volume;
			}
		}
	}

	// Token: 0x040009BF RID: 2495
	[SerializeField]
	private SoundSensor soundSensor;
}
