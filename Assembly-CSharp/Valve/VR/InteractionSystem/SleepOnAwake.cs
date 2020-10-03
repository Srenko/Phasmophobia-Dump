using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000433 RID: 1075
	public class SleepOnAwake : MonoBehaviour
	{
		// Token: 0x060020F5 RID: 8437 RVA: 0x000A2B48 File Offset: 0x000A0D48
		private void Awake()
		{
			Rigidbody component = base.GetComponent<Rigidbody>();
			if (component)
			{
				component.Sleep();
			}
		}
	}
}
