using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EC RID: 236
public class GhostActivity : MonoBehaviour
{
	// Token: 0x06000679 RID: 1657 RVA: 0x0002411B File Offset: 0x0002231B
	private void Awake()
	{
		this.ghostAI = base.GetComponentInParent<GhostAI>();
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0002412C File Offset: 0x0002232C
	private void Start()
	{
		if (LevelController.instance.type != LevelController.levelType.small)
		{
			base.GetComponent<BoxCollider>().size = new Vector3(6f, 2f, 6f);
		}
		if (!PhotonNetwork.isMasterClient)
		{
			return;
		}
		if (GameController.instance)
		{
			if (GameController.instance.levelDifficulty != Contract.LevelDifficulty.Amateur)
			{
				this.ghostAbilityRandMax = 14;
				return;
			}
		}
		else if (Object.FindObjectOfType<GameController>().levelDifficulty != Contract.LevelDifficulty.Amateur)
		{
			this.ghostAbilityRandMax = 14;
		}
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x000241A4 File Offset: 0x000223A4
	public void Interact()
	{
		if (this.objectsToInteractWith.Count == 0 || !SetupPhaseController.instance.mainDoorHasUnlocked)
		{
			this.ghostAI.ChangeState(GhostAI.States.favouriteRoom, null, null);
			return;
		}
		if (Random.Range(0, 3) == 1)
		{
			this.InteractWithARandomDoor();
			return;
		}
		if (Random.Range(0, 3) < 2)
		{
			this.GhostWriting();
			return;
		}
		this.InteractWithARandomProp();
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x00024204 File Offset: 0x00022404
	public void InteractWithARandomProp()
	{
		if (this.objectsToInteractWith.Count == 0)
		{
			this.ghostAI.ChangeState(GhostAI.States.favouriteRoom, null, null);
			return;
		}
		if (this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Shade && LevelController.instance.currentGhostRoom.playersInRoom.Count > 1)
		{
			this.ghostAI.ChangeState(GhostAI.States.favouriteRoom, null, null);
			return;
		}
		PhotonObjectInteract photonObjectInteract = this.objectsToInteractWith[Random.Range(0, this.objectsToInteractWith.Count)];
		if (photonObjectInteract.GetComponent<LightSwitch>())
		{
			if (Random.Range(0, 4) < 3)
			{
				this.ghostAI.ChangeState(GhostAI.States.light, photonObjectInteract, null);
				return;
			}
			this.ghostAI.ChangeState(GhostAI.States.flicker, photonObjectInteract, null);
			return;
		}
		else
		{
			if (photonObjectInteract.GetComponent<Car>())
			{
				this.ghostAI.ChangeState(GhostAI.States.carAlarm, null, null);
				return;
			}
			if (photonObjectInteract.GetComponent<Window>())
			{
				this.ghostAI.ChangeState(GhostAI.States.windowKnock, photonObjectInteract, null);
				return;
			}
			if (photonObjectInteract.GetComponent<Radio>())
			{
				this.ghostAI.ChangeState(GhostAI.States.radio, null, null);
				return;
			}
			if (photonObjectInteract.GetComponent<CCTV>())
			{
				this.ghostAI.ChangeState(GhostAI.States.cctv, photonObjectInteract, null);
				return;
			}
			if (photonObjectInteract.GetComponent<Sink>())
			{
				this.ghostAI.ChangeState(GhostAI.States.sink, photonObjectInteract, null);
				return;
			}
			if (photonObjectInteract.GetComponent<Sound>())
			{
				this.ghostAI.ChangeState(GhostAI.States.sound, photonObjectInteract, null);
				return;
			}
			if (photonObjectInteract.GetComponent<Painting>())
			{
				this.ghostAI.ChangeState(GhostAI.States.painting, photonObjectInteract, null);
				return;
			}
			if (photonObjectInteract.GetComponent<Mannequin>())
			{
				this.ghostAI.ChangeState(GhostAI.States.mannequin, photonObjectInteract, null);
				return;
			}
			if (photonObjectInteract.GetComponent<TeleportableObject>())
			{
				this.ghostAI.ChangeState(GhostAI.States.teleportObject, photonObjectInteract, null);
				return;
			}
			if (photonObjectInteract.GetComponent<AnimationObject>())
			{
				this.ghostAI.ChangeState(GhostAI.States.animationObject, photonObjectInteract, null);
				return;
			}
			if (photonObjectInteract.GetComponent<GhostWriting>())
			{
				photonObjectInteract.GetComponent<GhostWriting>().Use();
				this.ghostAI.ChangeState(GhostAI.States.throwing, photonObjectInteract, null);
				return;
			}
			this.ghostAI.ChangeState(GhostAI.States.throwing, this.GetPropToThrow(), null);
			return;
		}
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0002441C File Offset: 0x0002261C
	public void GhostWriting()
	{
		PhotonObjectInteract photonObjectInteract = null;
		for (int i = 0; i < this.objectsToInteractWith.Count; i++)
		{
			if (this.objectsToInteractWith[i].GetComponent<GhostWriting>())
			{
				photonObjectInteract = this.objectsToInteractWith[i];
			}
		}
		if (photonObjectInteract == null)
		{
			this.InteractWithARandomProp();
			return;
		}
		photonObjectInteract.GetComponent<GhostWriting>().Use();
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x00024484 File Offset: 0x00022684
	public void InteractWithARandomDoor()
	{
		if (this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Shade && LevelController.instance.currentGhostRoom.playersInRoom.Count > 1)
		{
			this.ghostAI.ChangeState(GhostAI.States.favouriteRoom, null, null);
			return;
		}
		PhotonObjectInteract doorToOpen = this.GetDoorToOpen();
		if (doorToOpen == null)
		{
			this.InteractWithARandomProp();
			return;
		}
		if (doorToOpen.GetComponent<Door>())
		{
			if (doorToOpen.GetComponent<Door>().type == Key.KeyType.main)
			{
				if (Random.Range(0, 5) < 3)
				{
					this.ghostAI.ChangeState(GhostAI.States.door, doorToOpen, null);
					return;
				}
				this.ghostAI.ChangeState(GhostAI.States.doorKnock, null, null);
				return;
			}
			else
			{
				if (Random.Range(0, 3) < 2)
				{
					this.ghostAI.ChangeState(GhostAI.States.door, doorToOpen, null);
					return;
				}
				this.ghostAI.ChangeState(GhostAI.States.lockDoor, doorToOpen, null);
			}
		}
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x00024554 File Offset: 0x00022754
	public void GhostAbility()
	{
		if (this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Shade && LevelController.instance.currentGhostRoom.playersInRoom.Count > 1)
		{
			this.ghostAI.ChangeState(GhostAI.States.favouriteRoom, null, null);
			return;
		}
		int num = Random.Range(0, this.ghostAbilityRandMax);
		if (num == 0 || num == 1)
		{
			this.ghostAI.ChangeState(GhostAI.States.appear, null, null);
			return;
		}
		if (num == 2)
		{
			this.ghostAI.ChangeState(GhostAI.States.fusebox, null, null);
			return;
		}
		if (num == 3 || num == 4)
		{
			this.GetPropsToThrow();
			if (this.propsToThrow.Count > 0)
			{
				this.ghostAI.ChangeState(GhostAI.States.GhostAbility, null, this.propsToThrow.ToArray());
				return;
			}
			this.ghostAI.ChangeState(GhostAI.States.idle, null, null);
			return;
		}
		else
		{
			if (GameController.instance.isTutorial)
			{
				this.ghostAI.ChangeState(GhostAI.States.favouriteRoom, null, null);
				return;
			}
			this.ghostAI.ChangeState(GhostAI.States.randomEvent, null, null);
			return;
		}
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x00024648 File Offset: 0x00022848
	private void GetPropsToThrow()
	{
		this.propsToThrow.Clear();
		for (int i = 0; i < this.objectsToInteractWith.Count; i++)
		{
			if (this.objectsToInteractWith[i].isProp && !this.objectsToInteractWith[i].GetComponent<Joint>())
			{
				this.propsToThrow.Add(this.objectsToInteractWith[i]);
			}
		}
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x000246B8 File Offset: 0x000228B8
	public PhotonObjectInteract GetPropToThrow()
	{
		PhotonObjectInteract result = null;
		for (int i = 0; i < this.objectsToInteractWith.Count; i++)
		{
			if (this.objectsToInteractWith[i].isProp && !this.objectsToInteractWith[i].GetComponent<Joint>())
			{
				result = this.objectsToInteractWith[i];
			}
		}
		return result;
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x00024718 File Offset: 0x00022918
	public PhotonObjectInteract GetDoorToOpen()
	{
		for (int i = 0; i < this.objectsToInteractWith.Count; i++)
		{
			if (this.objectsToInteractWith[i].GetComponent<Door>() && this.objectsToInteractWith[i].GetComponent<Door>().type != Key.KeyType.main)
			{
				return this.objectsToInteractWith[i];
			}
		}
		return null;
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0002477C File Offset: 0x0002297C
	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger)
		{
			return;
		}
		if (other.GetComponent<PhotonObjectInteract>())
		{
			if (!other.CompareTag("Item") && !other.CompareTag("DSLR") && !other.CompareTag("EMFReader"))
			{
				if (!this.objectsToInteractWith.Contains(other.GetComponent<PhotonObjectInteract>()))
				{
					this.objectsToInteractWith.Add(other.GetComponent<PhotonObjectInteract>());
					return;
				}
			}
			else if (other.GetComponent<GhostWriting>() && !this.objectsToInteractWith.Contains(other.GetComponent<PhotonObjectInteract>()))
			{
				this.objectsToInteractWith.Add(other.GetComponent<PhotonObjectInteract>());
			}
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0002481C File Offset: 0x00022A1C
	private void OnTriggerExit(Collider other)
	{
		if (other.isTrigger)
		{
			return;
		}
		if (other.GetComponent<PhotonObjectInteract>() && this.objectsToInteractWith.Contains(other.GetComponent<PhotonObjectInteract>()))
		{
			this.objectsToInteractWith.Remove(other.GetComponent<PhotonObjectInteract>());
		}
	}

	// Token: 0x0400064D RID: 1613
	[SerializeField]
	private List<PhotonObjectInteract> objectsToInteractWith = new List<PhotonObjectInteract>();

	// Token: 0x0400064E RID: 1614
	private List<PhotonObjectInteract> propsToThrow = new List<PhotonObjectInteract>();

	// Token: 0x0400064F RID: 1615
	private GhostAI ghostAI;

	// Token: 0x04000650 RID: 1616
	private int ghostAbilityRandMax = 12;
}
