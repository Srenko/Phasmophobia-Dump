using System;
using UnityEngine;

// Token: 0x02000164 RID: 356
public class ParabolicMicrophoneTrigger : MonoBehaviour
{
	// Token: 0x0600098C RID: 2444 RVA: 0x0003AAF4 File Offset: 0x00038CF4
	private void OnTriggerEnter(Collider other)
	{
		if (this.microphone.isOn && other.GetComponent<Noise>())
		{
			Noise component = other.GetComponent<Noise>();
			if (!this.microphone.noises.Contains(component))
			{
				this.microphone.noises.Add(component);
			}
		}
	}

	// Token: 0x0400099C RID: 2460
	[SerializeField]
	private ParabolicMicrophone microphone;
}
