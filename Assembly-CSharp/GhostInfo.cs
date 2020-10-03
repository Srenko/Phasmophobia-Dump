using System;
using Photon;
using UnityEngine;

// Token: 0x020000EF RID: 239
public class GhostInfo : Photon.MonoBehaviour
{
	// Token: 0x06000698 RID: 1688 RVA: 0x00024D04 File Offset: 0x00022F04
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x00024D12 File Offset: 0x00022F12
	private void Update()
	{
		if (this.activityMultiplier > 0f)
		{
			this.activityMultiplier -= Time.deltaTime / 2f;
		}
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x00024D3C File Offset: 0x00022F3C
	public void SyncValues(GhostTraits traits)
	{
		this.view.RPC("SyncValuesNetworked", PhotonTargets.AllBuffered, new object[]
		{
			(int)traits.ghostType,
			this.ghostTraits.ghostAge,
			this.ghostTraits.isMale,
			this.ghostTraits.ghostName,
			this.ghostTraits.isShy,
			this.ghostTraits.deathLength,
			this.ghostTraits.favouriteRoomID
		});
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x00024DDC File Offset: 0x00022FDC
	[PunRPC]
	private void SyncValuesNetworked(int ghostType, int ghostAge, bool isMale, string ghostName, bool isShy, int deathLength, int favouriteRoomID)
	{
		GhostTraits ghostTraits = new GhostTraits
		{
			ghostType = (GhostTraits.Type)ghostType,
			ghostAge = ghostAge,
			isMale = isMale,
			ghostName = ghostName,
			isShy = isShy,
			deathLength = deathLength,
			favouriteRoomID = favouriteRoomID
		};
		FileBasedPrefs.SetString("GhostType", LocalisationSystem.GetLocalisedValue("Journal_" + (GhostTraits.Type)ghostType + "Title"));
		this.ghostTraits = ghostTraits;
		this.favouriteRoom = LevelController.instance.rooms[ghostTraits.favouriteRoomID];
		LevelController.instance.currentGhost = this.ghost;
		GameController.instance.OnGhostSpawned.Invoke();
		if (PhotonNetwork.isMasterClient)
		{
			EvidenceController.instance.SpawnAllGhostTypeEvidence(ghostTraits.ghostType);
		}
	}

	// Token: 0x04000660 RID: 1632
	[HideInInspector]
	public PhotonView view;

	// Token: 0x04000661 RID: 1633
	[HideInInspector]
	public GhostTraits ghostTraits;

	// Token: 0x04000662 RID: 1634
	[SerializeField]
	private GhostAI ghost;

	// Token: 0x04000663 RID: 1635
	[HideInInspector]
	public LevelRoom favouriteRoom;

	// Token: 0x04000664 RID: 1636
	[HideInInspector]
	public float activityMultiplier;
}
