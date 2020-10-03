using System;
using UnityEngine;

// Token: 0x020001C3 RID: 451
public class CCTVTruckTrigger : MonoBehaviour
{
	// Token: 0x06000C5A RID: 3162 RVA: 0x0004D5B0 File Offset: 0x0004B7B0
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.CompareTag("Player"))
		{
			if (other.isTrigger)
			{
				return;
			}
			if (other.GetComponent<PhotonObjectInteract>() && !other.GetComponent<WalkieTalkie>())
			{
				return;
			}
			if (other.GetComponent<ThermometerSpot>())
			{
				return;
			}
			if (other.transform.root.GetComponent<PhotonView>().isMine)
			{
				CCTVController.instance.StartRendering();
			}
		}
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x0004D628 File Offset: 0x0004B828
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.root.CompareTag("Player"))
		{
			if (other.isTrigger)
			{
				return;
			}
			if (other.GetComponent<PhotonObjectInteract>() && !other.GetComponent<WalkieTalkie>())
			{
				return;
			}
			if (other.GetComponent<ThermometerSpot>())
			{
				return;
			}
			if (other.transform.root.GetComponent<PhotonView>().isMine)
			{
				CCTVController.instance.StopRendering();
			}
		}
	}
}
