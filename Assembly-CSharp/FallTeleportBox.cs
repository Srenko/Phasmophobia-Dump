using System;
using UnityEngine;
using UnityEngine.XR;
using VRTK;

// Token: 0x02000173 RID: 371
public class FallTeleportBox : MonoBehaviour
{
	// Token: 0x060009D6 RID: 2518 RVA: 0x0003C7FA File Offset: 0x0003A9FA
	private void OnTriggerEnter(Collider other)
	{
		this.TeleportObjects(other);
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x0003C7FA File Offset: 0x0003A9FA
	private void OnTriggerExit(Collider other)
	{
		this.TeleportObjects(other);
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0003C804 File Offset: 0x0003AA04
	private void TeleportObjects(Collider other)
	{
		if (other.isTrigger)
		{
			return;
		}
		if (other.GetComponent<PhotonView>() && !other.GetComponent<PhotonView>().isMine && PhotonNetwork.inRoom)
		{
			return;
		}
		if (other.transform.root.CompareTag("Ghost"))
		{
			return;
		}
		if (!other.transform.root.CompareTag("Player"))
		{
			if (other.GetComponent<PhotonObjectInteract>() && other.GetComponent<PhotonObjectInteract>().isProp && !other.GetComponent<PhotonObjectInteract>().isGrabbed && !other.GetComponent<ThermometerSpot>())
			{
				other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
				other.transform.position = other.transform.GetComponent<PhotonObjectInteract>().spawnPoint;
			}
			return;
		}
		if (other.gameObject.layer == LayerMask.NameToLayer("PCTriggerObject"))
		{
			return;
		}
		if (GameController.instance)
		{
			if (GameController.instance.myPlayer == null)
			{
				return;
			}
		}
		else if (MainManager.instance && MainManager.instance.localPlayer == null)
		{
			return;
		}
		if (other.GetComponent<PhotonObjectInteract>())
		{
			return;
		}
		if (other.GetComponent<ThermometerSpot>())
		{
			return;
		}
		if (other.GetComponent<Noise>())
		{
			return;
		}
		if (other.GetComponent<VRJournal>())
		{
			return;
		}
		if (other.GetComponent<VRTK_SnapDropZone>())
		{
			return;
		}
		Debug.Log("Teleported player for object: " + other.gameObject.name);
		this.TeleportPlayer();
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x0003C98C File Offset: 0x0003AB8C
	private void TeleportPlayer()
	{
		if (GameController.instance && GameController.instance.myPlayer == null)
		{
			return;
		}
		if (XRDevice.isPresent)
		{
			if (GameController.instance)
			{
				GameController.instance.myPlayer.player.basicTeleport.ForceTeleport(MultiplayerController.instance.spawns[0].position, null);
				return;
			}
			MainManager.instance.localPlayer.basicTeleport.ForceTeleport(MainManager.instance.spawns[0].position, null);
			return;
		}
		else
		{
			if (GameController.instance)
			{
				GameController.instance.myPlayer.player.transform.position = MultiplayerController.instance.spawns[0].position;
				return;
			}
			MainManager.instance.localPlayer.transform.position = MainManager.instance.spawns[0].position;
			return;
		}
	}
}
