using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class GhostInteraction : MonoBehaviour
{
	// Token: 0x0600069D RID: 1693 RVA: 0x00024EA7 File Offset: 0x000230A7
	private void Awake()
	{
		this.listener = base.GetComponent<AudioListener>();
		this.ghostAI = base.GetComponent<GhostAI>();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x00024ED0 File Offset: 0x000230D0
	private void Update()
	{
		if (this.view.isMine)
		{
			this.StepTimer -= Time.deltaTime;
			if (this.StepTimer < 0f)
			{
				if (this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Spirit)
				{
					this.StepTimer = ((this.ghostAI.ghostIsAppeared || this.ghostAI.isHunting || this.hasWalkedInSalt) ? Random.Range(0.3f, 1f) : Random.Range(2f, 15f));
				}
				else
				{
					this.StepTimer = ((this.ghostAI.ghostIsAppeared || this.ghostAI.isHunting || this.hasWalkedInSalt) ? Random.Range(0.3f, 1f) : Random.Range(15f, 40f));
				}
				this.GhostStep();
			}
		}
		if (this.hasWalkedInSalt)
		{
			this.walkedInSaltTimer -= Time.deltaTime;
			if (this.walkedInSaltTimer < 0f)
			{
				this.view.RPC("SyncSaltFalse", PhotonTargets.All, Array.Empty<object>());
				this.walkedInSaltTimer = 10f;
			}
		}
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x00025007 File Offset: 0x00023207
	[PunRPC]
	private void SyncSaltFalse()
	{
		this.hasWalkedInSalt = false;
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x00025010 File Offset: 0x00023210
	private bool IsEMFEvidence()
	{
		return this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Phantom || this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Banshee || this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Jinn || this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Revenant || this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Shade || this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Oni;
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x000250B4 File Offset: 0x000232B4
	public void CreateInteractionEMF(Vector3 pos)
	{
		if (GameController.instance.myPlayer.player.isDead)
		{
			return;
		}
		if (!this.IsEMFEvidence())
		{
			this.SpawnEMF(pos, EMF.Type.GhostInteraction);
			return;
		}
		if (Random.Range(0, 3) == 1)
		{
			this.SpawnEMF(pos, EMF.Type.GhostEvidence);
			return;
		}
		this.SpawnEMF(pos, EMF.Type.GhostInteraction);
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x00025104 File Offset: 0x00023304
	public void CreateAppearedEMF(Vector3 pos)
	{
		if (GameController.instance.myPlayer.player.isDead)
		{
			return;
		}
		this.SpawnEMF(pos, EMF.Type.GhostAppeared);
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x00025128 File Offset: 0x00023328
	public void CreateThrowingEMF(Vector3 pos)
	{
		if (this.IsEMFEvidence())
		{
			if (Random.Range(0, 3) == 1)
			{
				this.SpawnEMF(pos, EMF.Type.GhostEvidence);
			}
			else
			{
				this.SpawnEMF(pos, EMF.Type.GhostThrowing);
			}
		}
		else
		{
			this.SpawnEMF(pos, EMF.Type.GhostThrowing);
		}
		this.view.RPC("PlayThrowingNoise", PhotonTargets.All, new object[]
		{
			pos
		});
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x00025183 File Offset: 0x00023383
	public void CreateDoorNoise(Vector3 pos)
	{
		if (GameController.instance.myPlayer.player.isDead)
		{
			return;
		}
		this.view.RPC("PlayDoorNoise", PhotonTargets.All, new object[]
		{
			pos
		});
		this.CreateInteractionEMF(pos);
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x000251C4 File Offset: 0x000233C4
	private void GhostStep()
	{
		if (SetupPhaseController.instance && SetupPhaseController.instance.mainDoorHasUnlocked)
		{
			this.view.RPC("SpawnFootstepNetworked", PhotonTargets.All, new object[]
			{
				this.footstepSpawnPoint.position,
				this.footstepSpawnPoint.rotation,
				Random.Range(0, 2)
			});
		}
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x00025238 File Offset: 0x00023438
	[PunRPC]
	private void PlayDoorNoise(Vector3 pos)
	{
		Noise component = ObjectPooler.instance.SpawnFromPool("Noise", pos, Quaternion.identity).GetComponent<Noise>();
		component.source.volume = 0.6f;
		component.PlaySound(this.doorNoises[Random.Range(0, this.doorNoises.Count)], 0.15f);
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x00025298 File Offset: 0x00023498
	[PunRPC]
	private void PlayThrowingNoise(Vector3 pos)
	{
		Noise component = ObjectPooler.instance.SpawnFromPool("Noise", pos, Quaternion.identity).GetComponent<Noise>();
		component.source.volume = 0.6f;
		component.PlaySound(this.throwingNoises[Random.Range(0, this.throwingNoises.Count)], 0.15f);
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x000252F5 File Offset: 0x000234F5
	private void SpawnEMF(Vector3 pos, EMF.Type type)
	{
		this.view.RPC("SpawnEMFNetworked", PhotonTargets.All, new object[]
		{
			pos,
			(int)type
		});
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x00025320 File Offset: 0x00023520
	[PunRPC]
	private void SpawnEMFNetworked(Vector3 pos, int typeID)
	{
		ObjectPooler.instance.SpawnFromPool("EMF", pos, Quaternion.identity).GetComponent<EMF>().SetType((EMF.Type)typeID);
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x00025342 File Offset: 0x00023542
	[PunRPC]
	private void SpawnFootstepNetworked(Vector3 pos, Quaternion rotation, int randSpawn)
	{
		ObjectPooler.instance.SpawnFromPool("Footstep", pos, rotation).GetComponent<Footstep>().Spawn(randSpawn == 1);
	}

	// Token: 0x04000665 RID: 1637
	private GhostAI ghostAI;

	// Token: 0x04000666 RID: 1638
	private AudioListener listener;

	// Token: 0x04000667 RID: 1639
	private PhotonView view;

	// Token: 0x04000668 RID: 1640
	[SerializeField]
	public List<AudioClip> throwingNoises = new List<AudioClip>();

	// Token: 0x04000669 RID: 1641
	public List<AudioClip> doorNoises = new List<AudioClip>();

	// Token: 0x0400066A RID: 1642
	[HideInInspector]
	public float StepTimer;

	// Token: 0x0400066B RID: 1643
	[SerializeField]
	private Transform footstepSpawnPoint;

	// Token: 0x0400066C RID: 1644
	[HideInInspector]
	public bool hasWalkedInSalt;

	// Token: 0x0400066D RID: 1645
	private float walkedInSaltTimer = 10f;
}
