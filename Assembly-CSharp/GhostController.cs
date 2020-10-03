using System;
using Photon;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

// Token: 0x02000116 RID: 278
[RequireComponent(typeof(PhotonView))]
public class GhostController : Photon.MonoBehaviour
{
	// Token: 0x0600076C RID: 1900 RVA: 0x0002B47F File Offset: 0x0002967F
	private void Awake()
	{
		GhostController.instance = this;
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x0002B493 File Offset: 0x00029693
	private void OnJoinedRoom()
	{
		if (PhotonNetwork.isMasterClient)
		{
			this.SpawnGhost();
		}
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x0002B493 File Offset: 0x00029693
	private void Start()
	{
		if (PhotonNetwork.isMasterClient)
		{
			this.SpawnGhost();
		}
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x0002B493 File Offset: 0x00029693
	private void OnDisconnectedFromPhoton()
	{
		if (PhotonNetwork.isMasterClient)
		{
			this.SpawnGhost();
		}
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x0002B4A2 File Offset: 0x000296A2
	private void OnMasterClientSwitched(PhotonPlayer newMasterClient)
	{
		if (newMasterClient == PhotonNetwork.player)
		{
			this.SpawnGhost();
		}
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0002B4B2 File Offset: 0x000296B2
	private void SpawnGhost()
	{
		if (GameController.instance.allPlayersAreConnected)
		{
			this.CreateGhost();
			return;
		}
		GameController.instance.OnAllPlayersConnected.AddListener(new UnityAction(this.CreateGhost));
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x0002B4E4 File Offset: 0x000296E4
	private void CreateGhost()
	{
		if (this.createdGhost)
		{
			return;
		}
		this.ghostTraits.ghostType = (GhostTraits.Type)Random.Range(1, 13);
		if (PlayerPrefs.HasKey("isDeveloperVersion") && PlayerPrefs.GetInt("isDeveloperVersion") == 1 && PlayerPrefs.GetInt("Developer_GhostType") != 0)
		{
			this.ghostTraits.ghostType = (GhostTraits.Type)PlayerPrefs.GetInt("Developer_GhostType");
		}
		this.ghostTraits.deathLength = Random.Range(50, 1000);
		this.ghostTraits.ghostAge = Random.Range(10, 90);
		this.ghostTraits.isMale = (Random.Range(0, 2) == 0);
		this.ghostTraits.favouriteRoomID = Random.Range(0, LevelController.instance.rooms.Length);
		this.ghostTraits.isShy = (Random.Range(0, 2) == 1);
		string prefabName = Constants.maleGhostNames[0];
		if (this.ghostTraits.isMale)
		{
			this.ghostTraits.ghostName = LevelController.instance.possibleMaleFirstNames[Random.Range(0, LevelController.instance.possibleMaleFirstNames.Length)] + " " + LevelController.instance.possibleLastNames[Random.Range(0, LevelController.instance.possibleLastNames.Length)];
			prefabName = Constants.maleGhostNames[Random.Range(0, Constants.maleGhostNames.Length)];
		}
		else
		{
			this.ghostTraits.ghostName = LevelController.instance.possibleFemaleFirstNames[Random.Range(0, LevelController.instance.possibleFemaleFirstNames.Length)] + " " + LevelController.instance.possibleLastNames[Random.Range(0, LevelController.instance.possibleLastNames.Length)];
			prefabName = Constants.femaleGhostNames[Random.Range(0, Constants.femaleGhostNames.Length)];
		}
		GhostAI component = PhotonNetwork.InstantiateSceneObject(prefabName, this.spawn.position, this.spawn.rotation, 0, null).GetComponent<GhostAI>();
		component.ghostInfo.ghostTraits = this.ghostTraits;
		this.view.RPC("GhostHasBeenCreated", PhotonTargets.AllBuffered, Array.Empty<object>());
		Collider collider = LevelController.instance.rooms[this.ghostTraits.favouriteRoomID].colliders[Random.Range(0, LevelController.instance.rooms[this.ghostTraits.favouriteRoomID].colliders.Count)];
		Vector3 pos = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x), collider.bounds.min.y, Random.Range(collider.bounds.min.z, collider.bounds.max.z));
		Vector3 zero = Vector3.zero;
		if (this.GetPositionOnNavMesh(pos, out zero))
		{
			component.agent.Warp(zero);
		}
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0002B7BD File Offset: 0x000299BD
	[PunRPC]
	private void GhostHasBeenCreated()
	{
		this.createdGhost = true;
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x0002B7C8 File Offset: 0x000299C8
	public void UpdatePlayerSanity()
	{
		if (!GameController.instance.isLoadingBackToMenu)
		{
			this.view.RPC("NetworkedUpdatePlayerSanity", PhotonTargets.Others, new object[]
			{
				GameController.instance.myPlayer.player.insanity,
				GameController.instance.myPlayer.actorID
			});
		}
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x0002B82C File Offset: 0x00029A2C
	[PunRPC]
	public void NetworkedUpdatePlayerSanity(float insanity, int actorID)
	{
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i].actorID == actorID)
			{
				GameController.instance.playersData[i].player.insanity = insanity;
			}
		}
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0002B888 File Offset: 0x00029A88
	private bool GetPositionOnNavMesh(Vector3 pos, out Vector3 hitPos)
	{
		NavMeshHit navMeshHit;
		if (NavMesh.SamplePosition(pos, out navMeshHit, 2f, 1))
		{
			hitPos = navMeshHit.position;
			return true;
		}
		hitPos = Vector3.zero;
		return false;
	}

	// Token: 0x04000719 RID: 1817
	private PhotonView view;

	// Token: 0x0400071A RID: 1818
	public static GhostController instance;

	// Token: 0x0400071B RID: 1819
	[SerializeField]
	private Transform spawn;

	// Token: 0x0400071C RID: 1820
	private GhostTraits ghostTraits;

	// Token: 0x0400071D RID: 1821
	private bool createdGhost;

	// Token: 0x0400071E RID: 1822
	[SerializeField]
	public GhostEventPlayer ghostEventPlayer;
}
