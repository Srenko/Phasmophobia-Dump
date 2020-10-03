using System;
using UnityEngine;

// Token: 0x02000156 RID: 342
public class EMFReaderTrigger : MonoBehaviour
{
	// Token: 0x0600090A RID: 2314 RVA: 0x0003664A File Offset: 0x0003484A
	private void Awake()
	{
		this.emfReader = base.GetComponentInParent<EMFReader>();
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x00036658 File Offset: 0x00034858
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("EMF") && other.GetComponent<EMF>())
		{
			if (SoundController.instance.GetFloorTypeFromPosition(other.transform.position.y) != GameController.instance.myPlayer.player.currentRoom.floorType)
			{
				return;
			}
			this.emfReader.AddEMFZone(other.GetComponent<EMF>());
		}
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x000366C6 File Offset: 0x000348C6
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("EMF") && other.GetComponent<EMF>())
		{
			this.emfReader.RemoveEMFZone(other.GetComponent<EMF>());
		}
	}

	// Token: 0x0400091E RID: 2334
	private EMFReader emfReader;
}
