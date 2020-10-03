using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class PickupTriggerForward : MonoBehaviour
{
	// Token: 0x06000208 RID: 520 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
	public void OnTriggerEnter(Collider other)
	{
		PickupItem component = base.transform.parent.GetComponent<PickupItem>();
		if (component != null)
		{
			component.OnTriggerEnter(other);
		}
	}
}
