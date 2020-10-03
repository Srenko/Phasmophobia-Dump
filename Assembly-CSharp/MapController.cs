using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200011E RID: 286
public class MapController : MonoBehaviour
{
	// Token: 0x060007A9 RID: 1961 RVA: 0x0002D6D8 File Offset: 0x0002B8D8
	private void Awake()
	{
		MapController.instance = this;
		this.players.Clear();
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0002D6EB File Offset: 0x0002B8EB
	private void Start()
	{
		GameController.instance.OnAllPlayersConnected.AddListener(new UnityAction(this.AllPlayersAreConnected));
		if (LevelController.instance.type == LevelController.levelType.large)
		{
			this.iconScale = 6f;
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0002D720 File Offset: 0x0002B920
	private void Update()
	{
		for (int i = 0; i < this.players.Count; i++)
		{
			if (this.players[i] != null)
			{
				this.players[i].mapIcon.position = this.players[i].cam.transform.position;
			}
		}
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x0002D788 File Offset: 0x0002B988
	public void ChangeFloor()
	{
		this.index++;
		if (this.index > this.maxFloorIndex)
		{
			this.index = this.minFloorIndex;
		}
		if (this.index == 0)
		{
			this.ChangeFloorMonitor(LevelRoom.Type.basement);
			return;
		}
		if (this.index == 1)
		{
			this.ChangeFloorMonitor(LevelRoom.Type.firstFloor);
			return;
		}
		this.ChangeFloorMonitor(LevelRoom.Type.secondFloor);
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0002D7E8 File Offset: 0x0002B9E8
	private void AllPlayersAreConnected()
	{
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			this.AssignPlayer(GameController.instance.playersData[i].player);
		}
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x0002D82A File Offset: 0x0002BA2A
	private void AssignPlayer(Player player)
	{
		player.mapIcon = this.playerIcons[this.players.Count].transform;
		player.mapIcon.gameObject.SetActive(true);
		this.players.Add(player);
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x0002D86C File Offset: 0x0002BA6C
	public void RemovePlayer(Player player)
	{
		for (int i = 0; i < this.players.Count; i++)
		{
			if (this.players[i] == null)
			{
				this.players[i].mapIcon.gameObject.SetActive(false);
				this.players.RemoveAt(i);
				return;
			}
			if (this.players[i] == player)
			{
				this.players[i].mapIcon.gameObject.SetActive(false);
				this.players.Remove(player);
				return;
			}
		}
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0002D910 File Offset: 0x0002BB10
	public void AssignSensor(Transform sensor, Transform icon, int floorID, MotionSensor motion)
	{
		icon.position = sensor.position;
		if (floorID == 0)
		{
			icon.SetParent(this.basementFloor);
		}
		else if (floorID == 1)
		{
			icon.SetParent(this.firstFloor);
		}
		else
		{
			icon.SetParent(this.secondFloor);
		}
		icon.transform.localScale = Vector3.one * this.iconScale;
		if (motion)
		{
			if (motion.id == 0)
			{
				MotionSensorData.instance.image1 = motion.sensorIcon;
				return;
			}
			if (motion.id == 1)
			{
				MotionSensorData.instance.image2 = motion.sensorIcon;
				return;
			}
			if (motion.id == 2)
			{
				MotionSensorData.instance.image3 = motion.sensorIcon;
				return;
			}
			if (motion.id == 3)
			{
				MotionSensorData.instance.image4 = motion.sensorIcon;
			}
		}
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0002D9EC File Offset: 0x0002BBEC
	public void AssignIcon(Transform icon, LevelRoom.Type floorType)
	{
		if (floorType == LevelRoom.Type.basement)
		{
			icon.SetParent(this.basementFloor);
		}
		else if (floorType == LevelRoom.Type.firstFloor)
		{
			icon.SetParent(this.firstFloor);
		}
		else
		{
			icon.SetParent(this.secondFloor);
		}
		icon.transform.localScale = Vector3.one * this.iconScale;
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0002DA44 File Offset: 0x0002BC44
	public void ChangeFloorMonitor(LevelRoom.Type floorType)
	{
		this.basementFloor.gameObject.SetActive(false);
		this.firstFloor.gameObject.SetActive(false);
		this.secondFloor.gameObject.SetActive(false);
		if (floorType == LevelRoom.Type.basement)
		{
			this.basementFloor.gameObject.SetActive(true);
			return;
		}
		if (floorType == LevelRoom.Type.firstFloor)
		{
			this.firstFloor.gameObject.SetActive(true);
			return;
		}
		this.secondFloor.gameObject.SetActive(true);
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0002DAC0 File Offset: 0x0002BCC0
	public void ChangePlayerFloor(Player player, LevelRoom.Type floorType)
	{
		int i = 0;
		while (i < this.players.Count)
		{
			if (this.players[i] == player)
			{
				if (floorType == LevelRoom.Type.basement)
				{
					this.players[i].mapIcon.SetParent(this.basementFloor);
					return;
				}
				if (floorType == LevelRoom.Type.firstFloor)
				{
					this.players[i].mapIcon.SetParent(this.firstFloor);
					return;
				}
				this.players[i].mapIcon.SetParent(this.secondFloor);
				return;
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x0400077A RID: 1914
	public static MapController instance;

	// Token: 0x0400077B RID: 1915
	[SerializeField]
	private List<Transform> playerIcons = new List<Transform>(4);

	// Token: 0x0400077C RID: 1916
	private List<Player> players = new List<Player>();

	// Token: 0x0400077D RID: 1917
	[SerializeField]
	private Transform basementFloor;

	// Token: 0x0400077E RID: 1918
	[SerializeField]
	private Transform firstFloor;

	// Token: 0x0400077F RID: 1919
	[SerializeField]
	private Transform secondFloor;

	// Token: 0x04000780 RID: 1920
	private int index = 1;

	// Token: 0x04000781 RID: 1921
	[SerializeField]
	private int minFloorIndex;

	// Token: 0x04000782 RID: 1922
	[SerializeField]
	private int maxFloorIndex = 2;

	// Token: 0x04000783 RID: 1923
	[SerializeField]
	private float iconScale = 1f;
}
