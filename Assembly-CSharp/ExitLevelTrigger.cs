using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C6 RID: 454
[RequireComponent(typeof(Rigidbody))]
public class ExitLevelTrigger : MonoBehaviour
{
	// Token: 0x06000C6E RID: 3182 RVA: 0x0004EC7C File Offset: 0x0004CE7C
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
			if (other.GetComponent<VRJournal>())
			{
				return;
			}
			if (other.GetComponent<Noise>())
			{
				return;
			}
			if (!this.playersInTruck.Contains(other.transform.root.GetComponent<Player>()))
			{
				this.playersInTruck.Add(other.transform.root.GetComponent<Player>());
			}
		}
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x0004ED2C File Offset: 0x0004CF2C
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
			if (other.GetComponent<VRJournal>())
			{
				return;
			}
			if (other.GetComponent<Noise>())
			{
				return;
			}
			if (this.playersInTruck.Contains(other.transform.root.GetComponent<Player>()))
			{
				this.playersInTruck.Remove(other.transform.root.GetComponent<Player>());
			}
		}
	}

	// Token: 0x04000CF1 RID: 3313
	public List<Player> playersInTruck = new List<Player>();
}
