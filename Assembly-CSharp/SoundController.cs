using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000126 RID: 294
[RequireComponent(typeof(PhotonView))]
public class SoundController : MonoBehaviour
{
	// Token: 0x060007E8 RID: 2024 RVA: 0x0002F822 File Offset: 0x0002DA22
	private void Awake()
	{
		SoundController.instance = this;
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x0002F836 File Offset: 0x0002DA36
	[PunRPC]
	private void PlayDoorKnockingSound()
	{
		if (!this.doorAudioSource.isPlaying)
		{
			this.doorAudioSource.Play();
		}
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x0002F850 File Offset: 0x0002DA50
	public AudioMixerGroup GetFloorAudioSnapshot(float yPos)
	{
		if (yPos < this.BasementFloorStartYPos)
		{
			return this.basementGroup;
		}
		if (yPos < this.FirstFloorStartYPos)
		{
			return this.firstFloorGroup;
		}
		return this.secondFloorGroup;
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x0002F878 File Offset: 0x0002DA78
	public LevelRoom.Type GetFloorTypeFromAudioGroup(AudioMixerGroup group)
	{
		if (group == this.basementGroup)
		{
			return LevelRoom.Type.basement;
		}
		if (group == this.firstFloorGroup)
		{
			return LevelRoom.Type.firstFloor;
		}
		return LevelRoom.Type.secondFloor;
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0002F89B File Offset: 0x0002DA9B
	public LevelRoom.Type GetFloorTypeFromPosition(float yPos)
	{
		if (yPos < this.BasementFloorStartYPos)
		{
			return LevelRoom.Type.basement;
		}
		if (yPos < this.FirstFloorStartYPos)
		{
			return LevelRoom.Type.firstFloor;
		}
		return LevelRoom.Type.secondFloor;
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x0002F8B4 File Offset: 0x0002DAB4
	public AudioMixerGroup GetPlayersAudioGroup(int actorID)
	{
		AudioMixerSnapshot currentPlayerSnapshot = this.firstFloorSnapshot;
		if (MainManager.instance)
		{
			currentPlayerSnapshot = MainManager.instance.localPlayer.currentPlayerSnapshot;
		}
		else
		{
			for (int i = 0; i < GameController.instance.playersData.Count; i++)
			{
				if (GameController.instance.playersData[i].actorID == actorID)
				{
					currentPlayerSnapshot = GameController.instance.playersData[i].player.currentPlayerSnapshot;
				}
			}
		}
		if (currentPlayerSnapshot == this.firstFloorSnapshot)
		{
			return this.firstFloorGroup;
		}
		if (currentPlayerSnapshot == this.secondFloorSnapshot)
		{
			return this.secondFloorGroup;
		}
		if (currentPlayerSnapshot == this.basementFloorSnapshot)
		{
			return this.basementGroup;
		}
		if (currentPlayerSnapshot == this.exteriorSnapshot)
		{
			return this.exteriorGroup;
		}
		return this.TruckGroup;
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x0002F990 File Offset: 0x0002DB90
	public AudioMixerGroup GetOurPlayersAudioGroup()
	{
		AudioMixerSnapshot currentPlayerSnapshot = this.firstFloorSnapshot;
		if (MainManager.instance)
		{
			currentPlayerSnapshot = MainManager.instance.localPlayer.currentPlayerSnapshot;
		}
		else
		{
			currentPlayerSnapshot = GameController.instance.myPlayer.player.currentPlayerSnapshot;
		}
		if (currentPlayerSnapshot == this.firstFloorSnapshot)
		{
			return this.firstFloorGroup;
		}
		if (currentPlayerSnapshot == this.secondFloorSnapshot)
		{
			return this.secondFloorGroup;
		}
		if (currentPlayerSnapshot == this.basementFloorSnapshot)
		{
			return this.basementGroup;
		}
		if (currentPlayerSnapshot == this.exteriorSnapshot)
		{
			return this.exteriorGroup;
		}
		return this.TruckGroup;
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x0002FA34 File Offset: 0x0002DC34
	public AudioMixerGroup GetAudioGroupFromSnapshot(AudioMixerSnapshot snapshot)
	{
		if (snapshot == this.firstFloorSnapshot)
		{
			return this.firstFloorGroup;
		}
		if (snapshot == this.secondFloorSnapshot)
		{
			return this.secondFloorGroup;
		}
		if (snapshot == this.basementFloorSnapshot)
		{
			return this.basementGroup;
		}
		if (snapshot == this.exteriorSnapshot)
		{
			return this.exteriorGroup;
		}
		return this.TruckGroup;
	}

	// Token: 0x040007B2 RID: 1970
	public static SoundController instance;

	// Token: 0x040007B3 RID: 1971
	[HideInInspector]
	public PhotonView view;

	// Token: 0x040007B4 RID: 1972
	[Header("Door Knocking")]
	public AudioSource doorAudioSource;

	// Token: 0x040007B5 RID: 1973
	[Header("Audio Mixer")]
	[SerializeField]
	private float BasementFloorStartYPos;

	// Token: 0x040007B6 RID: 1974
	[SerializeField]
	private float FirstFloorStartYPos = 3f;

	// Token: 0x040007B7 RID: 1975
	[SerializeField]
	private AudioMixerGroup basementGroup;

	// Token: 0x040007B8 RID: 1976
	[SerializeField]
	private AudioMixerGroup firstFloorGroup;

	// Token: 0x040007B9 RID: 1977
	[SerializeField]
	private AudioMixerGroup secondFloorGroup;

	// Token: 0x040007BA RID: 1978
	public AudioMixerGroup exteriorGroup;

	// Token: 0x040007BB RID: 1979
	public AudioMixerGroup TruckGroup;

	// Token: 0x040007BC RID: 1980
	public AudioMixerSnapshot exteriorSnapshot;

	// Token: 0x040007BD RID: 1981
	public AudioMixerSnapshot truckSnapshot;

	// Token: 0x040007BE RID: 1982
	public AudioMixerSnapshot firstFloorSnapshot;

	// Token: 0x040007BF RID: 1983
	public AudioMixerSnapshot secondFloorSnapshot;

	// Token: 0x040007C0 RID: 1984
	public AudioMixerSnapshot basementFloorSnapshot;

	// Token: 0x040007C1 RID: 1985
	public AudioClip[] genericImpactClips;
}
