using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200011B RID: 283
[RequireComponent(typeof(PhotonView))]
public class LevelController : MonoBehaviour
{
	// Token: 0x06000796 RID: 1942 RVA: 0x0002CC0C File Offset: 0x0002AE0C
	private void Awake()
	{
		LevelController.instance = this;
		this.view = base.GetComponent<PhotonView>();
		this.currentPlayerRoom = this.outsideRoom;
		this.currentGhostRoom = this.rooms[0];
		this.SetGhostName();
		this.gameController.OnAllPlayersConnected.AddListener(new UnityAction(this.SpawnMainDoorKey));
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x0002CC68 File Offset: 0x0002AE68
	private void Start()
	{
		if (PhotonNetwork.inRoom)
		{
			if (PhotonNetwork.isMasterClient)
			{
				this.view.RPC("SyncFuseBoxLocation", PhotonTargets.AllBuffered, new object[]
				{
					Random.Range(0, this.fuseboxSpawnLocations.Length)
				});
				return;
			}
		}
		else
		{
			this.SyncFuseBoxLocation(Random.Range(0, this.fuseboxSpawnLocations.Length));
		}
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x0002CCC8 File Offset: 0x0002AEC8
	[PunRPC]
	private void SyncFuseBoxLocation(int index)
	{
		this.fuseBox.parentObject.position = this.fuseboxSpawnLocations[index].position;
		this.fuseBox.parentObject.rotation = this.fuseboxSpawnLocations[index].rotation;
		this.fuseBox.SetupAudioGroup();
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x0002CD1A File Offset: 0x0002AF1A
	private void SpawnMainDoorKey()
	{
		this.mainDoorKeySpawner.gameObject.SetActive(true);
		this.mainDoorKeySpawner.Spawn();
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x0002CD38 File Offset: 0x0002AF38
	private void SetGhostName()
	{
		for (int i = 0; i < this.adultMalesInLevelCount; i++)
		{
			LevelController.Person item = new LevelController.Person
			{
				isMale = true,
				firstName = this.possibleMaleFirstNames[Random.Range(0, this.possibleMaleFirstNames.Length)],
				lastName = " " + this.possibleLastNames[Random.Range(0, this.possibleLastNames.Length)],
				age = Random.Range(21, 80)
			};
			this.peopleInHouse.Add(item);
			this.adultsInHouse.Add(item);
		}
		for (int j = 0; j < this.adultFemalesInLevelCount; j++)
		{
			LevelController.Person item2 = new LevelController.Person
			{
				isMale = false,
				firstName = this.possibleFemaleFirstNames[Random.Range(0, this.possibleFemaleFirstNames.Length)],
				lastName = " " + this.possibleLastNames[Random.Range(0, this.possibleLastNames.Length)],
				age = Random.Range(21, 80)
			};
			this.peopleInHouse.Add(item2);
			this.adultsInHouse.Add(item2);
		}
		for (int k = 0; k < this.kidMalesInLevelCount; k++)
		{
			LevelController.Person item3 = new LevelController.Person
			{
				isMale = true,
				firstName = this.possibleMaleFirstNames[Random.Range(0, this.possibleMaleFirstNames.Length)],
				lastName = " " + this.possibleLastNames[Random.Range(0, this.possibleLastNames.Length)],
				age = Random.Range(5, 21)
			};
			this.peopleInHouse.Add(item3);
		}
		for (int l = 0; l < this.kidFemalesInLevelCount; l++)
		{
			LevelController.Person item4 = new LevelController.Person
			{
				isMale = true,
				firstName = this.possibleFemaleFirstNames[Random.Range(0, this.possibleFemaleFirstNames.Length)],
				lastName = " " + this.possibleLastNames[Random.Range(0, this.possibleLastNames.Length)],
				age = Random.Range(5, 21)
			};
			this.peopleInHouse.Add(item4);
		}
		for (int m = 0; m < this.childrenMalesInLevelCount; m++)
		{
			LevelController.Person item5 = new LevelController.Person
			{
				isMale = true,
				firstName = this.possibleMaleFirstNames[Random.Range(0, this.possibleMaleFirstNames.Length)],
				lastName = " " + this.possibleLastNames[Random.Range(0, this.possibleLastNames.Length)],
				age = Random.Range(1, 5)
			};
			this.peopleInHouse.Add(item5);
		}
		for (int n = 0; n < this.childrenFemalesInLevelCount; n++)
		{
			LevelController.Person item6 = new LevelController.Person
			{
				isMale = false,
				firstName = this.possibleFemaleFirstNames[Random.Range(0, this.possibleFemaleFirstNames.Length)],
				lastName = " " + this.possibleLastNames[Random.Range(0, this.possibleLastNames.Length)],
				age = Random.Range(1, 5)
			};
			this.peopleInHouse.Add(item6);
		}
	}

	// Token: 0x0400074D RID: 1869
	public static LevelController instance;

	// Token: 0x0400074E RID: 1870
	[HideInInspector]
	public PhotonView view;

	// Token: 0x0400074F RID: 1871
	[HideInInspector]
	public LevelRoom currentPlayerRoom;

	// Token: 0x04000750 RID: 1872
	[HideInInspector]
	public LevelRoom currentGhostRoom;

	// Token: 0x04000751 RID: 1873
	[HideInInspector]
	public GhostAI currentGhost;

	// Token: 0x04000752 RID: 1874
	[HideInInspector]
	public List<LevelController.Person> peopleInHouse = new List<LevelController.Person>();

	// Token: 0x04000753 RID: 1875
	[HideInInspector]
	public List<LevelController.Person> adultsInHouse = new List<LevelController.Person>();

	// Token: 0x04000754 RID: 1876
	public List<GameObject> doors = new List<GameObject>();

	// Token: 0x04000755 RID: 1877
	public LevelRoom[] rooms = new LevelRoom[0];

	// Token: 0x04000756 RID: 1878
	public Transform[] fuseboxSpawnLocations;

	// Token: 0x04000757 RID: 1879
	public Transform[] MannequinTeleportSpots;

	// Token: 0x04000758 RID: 1880
	public LevelRoom outsideRoom;

	// Token: 0x04000759 RID: 1881
	public FuseBox fuseBox;

	// Token: 0x0400075A RID: 1882
	public JournalController journalController;

	// Token: 0x0400075B RID: 1883
	[HideInInspector]
	public List<JournalController> journals = new List<JournalController>();

	// Token: 0x0400075C RID: 1884
	public GameController gameController;

	// Token: 0x0400075D RID: 1885
	public ItemSpawner itemSpawner;

	// Token: 0x0400075E RID: 1886
	public float nightVisionPower = 1000f;

	// Token: 0x0400075F RID: 1887
	public Door[] exitDoors = new Door[0];

	// Token: 0x04000760 RID: 1888
	public Radio[] radiosInLevel;

	// Token: 0x04000761 RID: 1889
	[HideInInspector]
	public List<Crucifix> crucifix = new List<Crucifix>();

	// Token: 0x04000762 RID: 1890
	[HideInInspector]
	public List<Torch> torches = new List<Torch>();

	// Token: 0x04000763 RID: 1891
	public string streetName = "41 Tanglewood Street \n New Britain, CT 06051";

	// Token: 0x04000764 RID: 1892
	public LevelController.levelType type;

	// Token: 0x04000765 RID: 1893
	[HideInInspector]
	public string[] possibleMaleFirstNames = new string[]
	{
		"James",
		"John",
		"Robert",
		"Michael",
		"William",
		"David",
		"Richard",
		"Charles",
		"Joseph",
		"Thomas",
		"Christopher",
		"Daniel",
		"Paul",
		"Mark",
		"Donald",
		"George",
		"Kenneth",
		"Steven",
		"Edward",
		"Brian",
		"Ronald",
		"Anthony",
		"Kevin",
		"Jason",
		"Gary",
		"Larry",
		"Eric",
		"Raymond",
		"Jerry",
		"Harold",
		"Pater",
		"Justin",
		"Billy",
		"Carlos",
		"Russell",
		"Walter"
	};

	// Token: 0x04000766 RID: 1894
	[HideInInspector]
	public string[] possibleFemaleFirstNames = new string[]
	{
		"Mary",
		"Patricia",
		"Linda",
		"Barbara",
		"Elizabeth",
		"Jennifer",
		"Maria",
		"Susan",
		"Margaret",
		"Dorothy",
		"Lisa",
		"Nancy",
		"Karen",
		"Betty",
		"Helen",
		"Sandra",
		"Donna",
		"Carol",
		"Ruth",
		"Ann",
		"Julie",
		"Doris",
		"Gloria",
		"Judy",
		"Lori",
		"Jane",
		"Ellen",
		"April",
		"Megan",
		"Robin",
		"Holly",
		"Carla",
		"Ella",
		"Stacey",
		"Marcia",
		"Nellie",
		"Shelly"
	};

	// Token: 0x04000767 RID: 1895
	[HideInInspector]
	public string[] possibleLastNames = new string[]
	{
		"Smith",
		"Johnson",
		"Williams",
		"Jones",
		"Brown",
		"Miller",
		"Wilson",
		"Moore",
		"Taylor",
		"Anderson",
		"Jackson",
		"White",
		"Harris",
		"Martin",
		"Thompson",
		"Garcia",
		"Martinez",
		"Robinson",
		"Clark",
		"Lewis",
		"Walker",
		"Young",
		"Hill",
		"Hall",
		"Wright",
		"Roberts",
		"Carter",
		"Baker",
		"Wilson",
		"Anderson"
	};

	// Token: 0x04000768 RID: 1896
	public int adultMalesInLevelCount;

	// Token: 0x04000769 RID: 1897
	public int adultFemalesInLevelCount;

	// Token: 0x0400076A RID: 1898
	public int kidMalesInLevelCount;

	// Token: 0x0400076B RID: 1899
	public int kidFemalesInLevelCount;

	// Token: 0x0400076C RID: 1900
	public int childrenMalesInLevelCount;

	// Token: 0x0400076D RID: 1901
	public int childrenFemalesInLevelCount;

	// Token: 0x0400076E RID: 1902
	public Car car;

	// Token: 0x0400076F RID: 1903
	public KeySpawner mainDoorKeySpawner;

	// Token: 0x02000515 RID: 1301
	public struct Person
	{
		// Token: 0x04002476 RID: 9334
		public string firstName;

		// Token: 0x04002477 RID: 9335
		public int age;

		// Token: 0x04002478 RID: 9336
		[HideInInspector]
		public string lastName;

		// Token: 0x04002479 RID: 9337
		public bool isMale;
	}

	// Token: 0x02000516 RID: 1302
	public enum levelType
	{
		// Token: 0x0400247B RID: 9339
		small,
		// Token: 0x0400247C RID: 9340
		medium,
		// Token: 0x0400247D RID: 9341
		large
	}
}
