using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000174 RID: 372
public class LevelRoom : MonoBehaviour
{
	// Token: 0x060009DB RID: 2523 RVA: 0x0003CA94 File Offset: 0x0003AC94
	private void Awake()
	{
		foreach (BoxCollider item in base.GetComponents<BoxCollider>())
		{
			this.colliders.Add(item);
		}
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0003CAC8 File Offset: 0x0003ACC8
	private void Start()
	{
		if (this.roomName == string.Empty)
		{
			this.roomName = base.gameObject.name;
		}
		if (this == LevelController.instance.outsideRoom)
		{
			this.temperature = (float)Random.Range(15, 20);
			this.isOutsideRoom = true;
		}
		GameController.instance.OnGhostSpawned.AddListener(new UnityAction(this.SetGhostType));
		for (int i = 0; i < this.lightSwitches.Count; i++)
		{
			this.lightSwitches[i].myRoom = this;
		}
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x0003CB64 File Offset: 0x0003AD64
	private void Update()
	{
		if (!SetupPhaseController.instance.mainDoorHasUnlocked)
		{
			return;
		}
		if (!this.isOutsideRoom)
		{
			if (this.ghostInRoom)
			{
				if (this.isFreezingTemperatureGhost)
				{
					this.temperature -= Time.deltaTime * 0.12f;
				}
				else
				{
					this.temperature -= Time.deltaTime * 0.04f;
				}
			}
			else if (!this.isFreezingTemperatureGhost)
			{
				this.temperature += Time.deltaTime * 0.2f;
			}
			if (this.isFreezingTemperatureGhost)
			{
				this.temperature = Mathf.Clamp(this.temperature, -10f, this.startingTemperature);
			}
			else
			{
				this.temperature = Mathf.Clamp(this.temperature, 5f, this.startingTemperature);
			}
		}
		if (LevelController.instance.currentPlayerRoom == this)
		{
			this.currentPlayerInRoomTimer += Time.deltaTime;
		}
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0003CC54 File Offset: 0x0003AE54
	private void SetGhostType()
	{
		GhostTraits.Type ghostType = LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType;
		if (ghostType == GhostTraits.Type.Phantom || ghostType == GhostTraits.Type.Banshee || ghostType == GhostTraits.Type.Mare || ghostType == GhostTraits.Type.Wraith || ghostType == GhostTraits.Type.Demon || ghostType == GhostTraits.Type.Yurei)
		{
			this.isFreezingTemperatureGhost = true;
		}
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x0003CC9C File Offset: 0x0003AE9C
	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger)
		{
			return;
		}
		if (other.GetComponent<ThermometerSpot>())
		{
			other.GetComponent<ThermometerSpot>().myThermometer.SetTemperatureValue(this);
		}
		if (other.transform.root.CompareTag("Player"))
		{
			if (other.GetComponent<ThermometerSpot>())
			{
				return;
			}
			if (!this.playersInRoom.Contains(other.transform.root.gameObject) && !other.transform.root.GetComponent<Player>().isDead)
			{
				this.playersInRoom.Add(other.transform.root.gameObject);
			}
			other.transform.root.GetComponent<Player>().currentRoom = this;
			other.transform.root.GetComponent<Player>().voiceOcclusion.SetVoiceMixer();
			if (other.transform.root.GetComponent<PhotonView>() && other.transform.root.GetComponent<PhotonView>().isMine)
			{
				LevelController.instance.currentPlayerRoom = this;
			}
		}
		if (other.transform.root.CompareTag("Ghost") && !this.isOutsideRoom)
		{
			LevelController.instance.currentGhostRoom = this;
			this.ghostInRoom = true;
		}
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x0003CDE4 File Offset: 0x0003AFE4
	private void OnTriggerExit(Collider other)
	{
		if (other.isTrigger)
		{
			return;
		}
		if (other.transform.root.CompareTag("Player"))
		{
			if (this.playersInRoom.Contains(other.transform.root.gameObject))
			{
				this.playersInRoom.Remove(other.transform.root.gameObject);
			}
			if (other.transform.root.GetComponent<PhotonView>().isMine)
			{
				this.currentPlayerInRoomTimer = 0f;
			}
		}
		if (!this.isOutsideRoom && other.transform.root.CompareTag("Ghost"))
		{
			this.ghostInRoom = false;
		}
	}

	// Token: 0x040009FA RID: 2554
	public List<GameObject> playersInRoom = new List<GameObject>();

	// Token: 0x040009FB RID: 2555
	public List<LightSwitch> lightSwitches = new List<LightSwitch>();

	// Token: 0x040009FC RID: 2556
	public Door[] doors = new Door[0];

	// Token: 0x040009FD RID: 2557
	[HideInInspector]
	public List<BoxCollider> colliders = new List<BoxCollider>();

	// Token: 0x040009FE RID: 2558
	[SerializeField]
	private bool isFreezingTemperatureGhost;

	// Token: 0x040009FF RID: 2559
	[SerializeField]
	private bool ghostInRoom;

	// Token: 0x04000A00 RID: 2560
	private bool isOutsideRoom;

	// Token: 0x04000A01 RID: 2561
	public LevelRoom.Type floorType = LevelRoom.Type.firstFloor;

	// Token: 0x04000A02 RID: 2562
	public string roomName;

	// Token: 0x04000A03 RID: 2563
	public float temperature = 18f;

	// Token: 0x04000A04 RID: 2564
	private float startingTemperature = 18f;

	// Token: 0x04000A05 RID: 2565
	[HideInInspector]
	public float currentPlayerInRoomTimer;

	// Token: 0x04000A06 RID: 2566
	public bool isBasementOrAttic;

	// Token: 0x02000541 RID: 1345
	public enum Type
	{
		// Token: 0x0400251A RID: 9498
		basement,
		// Token: 0x0400251B RID: 9499
		firstFloor,
		// Token: 0x0400251C RID: 9500
		secondFloor
	}
}
