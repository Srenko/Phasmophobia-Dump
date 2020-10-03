using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000113 RID: 275
[RequireComponent(typeof(PhotonView))]
public class EvidenceController : MonoBehaviour
{
	// Token: 0x06000753 RID: 1875 RVA: 0x0002AC63 File Offset: 0x00028E63
	private void Awake()
	{
		EvidenceController.instance = this;
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0002AC77 File Offset: 0x00028E77
	private void Start()
	{
		GameController.instance.OnAllPlayersConnected.AddListener(new UnityAction(this.StartRecogniserDelay));
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x0002AC94 File Offset: 0x00028E94
	private void StartRecogniserDelay()
	{
		base.Invoke("SpawnOuijaBoard", 10f);
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0002ACA8 File Offset: 0x00028EA8
	private void SpawnOuijaBoard()
	{
		if (this.hasRun)
		{
			return;
		}
		if (PhotonNetwork.isMasterClient)
		{
			int index = Random.Range(0, this.ouijaBoardSpawnSpots.Count);
			this.ouijaBoard = PhotonNetwork.InstantiateSceneObject("Ouija board", this.ouijaBoardSpawnSpots[index].position, Quaternion.identity, 0, null);
			Quaternion rotation = this.ouijaBoard.transform.rotation;
			Vector3 eulerAngles = rotation.eulerAngles;
			eulerAngles = new Vector3(-90f, 0f, 0f);
			rotation.eulerAngles = eulerAngles;
			this.ouijaBoard.transform.rotation = rotation;
		}
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x0002AD4C File Offset: 0x00028F4C
	public void SpawnAllGhostTypeEvidence(GhostTraits.Type ghostType)
	{
		if (ghostType != GhostTraits.Type.Spirit && ghostType != GhostTraits.Type.Wraith)
		{
			if (ghostType == GhostTraits.Type.Phantom)
			{
				if (PhotonNetwork.isMasterClient)
				{
					this.SpawnGhostOrb();
				}
			}
			else if (ghostType == GhostTraits.Type.Poltergeist)
			{
				if (PhotonNetwork.isMasterClient)
				{
					this.SpawnGhostOrb();
				}
			}
			else if (ghostType != GhostTraits.Type.Banshee)
			{
				if (ghostType == GhostTraits.Type.Jinn)
				{
					if (PhotonNetwork.isMasterClient)
					{
						this.SpawnGhostOrb();
					}
				}
				else if (ghostType == GhostTraits.Type.Mare)
				{
					if (PhotonNetwork.isMasterClient)
					{
						this.SpawnGhostOrb();
					}
				}
				else if (ghostType != GhostTraits.Type.Revenant)
				{
					if (ghostType == GhostTraits.Type.Shade)
					{
						if (PhotonNetwork.isMasterClient)
						{
							this.SpawnGhostOrb();
						}
					}
					else if (ghostType != GhostTraits.Type.Demon)
					{
						if (ghostType == GhostTraits.Type.Yurei)
						{
							if (PhotonNetwork.isMasterClient)
							{
								this.SpawnGhostOrb();
							}
						}
					}
				}
			}
		}
		if (PhotonNetwork.isMasterClient)
		{
			this.SpawnBoneDNAEvidence();
		}
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x0002ADFC File Offset: 0x00028FFC
	private void SpawnBoneDNAEvidence()
	{
		int num = Random.Range(0, this.roomsToSpawnDNAEvidenceInside.Length);
		int index = Random.Range(0, this.roomsToSpawnDNAEvidenceInside[num].colliders.Count);
		Bounds bounds = this.roomsToSpawnDNAEvidenceInside[num].colliders[index].bounds;
		Vector3 position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), Random.Range(bounds.min.z, bounds.max.z));
		this.bone = PhotonNetwork.InstantiateSceneObject("Bone", position, Quaternion.identity, 0, null);
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x0002AEC0 File Offset: 0x000290C0
	private void SpawnGhostOrb()
	{
		Bounds bounds = LevelController.instance.rooms[LevelController.instance.currentGhost.ghostInfo.ghostTraits.favouriteRoomID].colliders[0].bounds;
		Vector3 position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), Random.Range(bounds.min.z, bounds.max.z));
		PhotonNetwork.InstantiateSceneObject("GhostOrb", position, Quaternion.identity, 0, null);
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x0002AF74 File Offset: 0x00029174
	public bool IsFingerPrintEvidence()
	{
		GhostInfo ghostInfo = LevelController.instance.currentGhost.ghostInfo;
		return ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Wraith || ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Poltergeist || ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Spirit || ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Banshee || ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Revenant;
	}

	// Token: 0x040006FA RID: 1786
	public static EvidenceController instance;

	// Token: 0x040006FB RID: 1787
	[SerializeField]
	private List<Transform> ouijaBoardSpawnSpots = new List<Transform>(0);

	// Token: 0x040006FC RID: 1788
	[HideInInspector]
	public List<Evidence> evidenceInLevel = new List<Evidence>();

	// Token: 0x040006FD RID: 1789
	[HideInInspector]
	public PhotonView view;

	// Token: 0x040006FE RID: 1790
	[HideInInspector]
	public int totalEvidenceFoundInPhotos;

	// Token: 0x040006FF RID: 1791
	[HideInInspector]
	public bool foundGhostDNA;

	// Token: 0x04000700 RID: 1792
	public LevelRoom[] roomsToSpawnDNAEvidenceInside;

	// Token: 0x04000701 RID: 1793
	[SerializeField]
	private Sink[] sinks;

	// Token: 0x04000702 RID: 1794
	public Material[] handPrintMaterials;

	// Token: 0x04000703 RID: 1795
	private bool hasRun;

	// Token: 0x04000704 RID: 1796
	private GameObject bone;

	// Token: 0x04000705 RID: 1797
	private GameObject ouijaBoard;
}
