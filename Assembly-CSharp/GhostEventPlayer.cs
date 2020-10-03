using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000EE RID: 238
public class GhostEventPlayer : MonoBehaviour
{
	// Token: 0x06000690 RID: 1680 RVA: 0x00024AED File Offset: 0x00022CED
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.agent = base.GetComponent<NavMeshAgent>();
		this.evidence.enabled = false;
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x00024B14 File Offset: 0x00022D14
	public void SpawnPlayer(Player target, Vector3 pos)
	{
		this.agent.Warp(pos);
		this.targetPlayer = target;
		this.agent.isStopped = false;
		if (PhotonNetwork.playerList.Length > 1)
		{
			this.view.RPC("SpawnRandomPlayerModel", PhotonTargets.All, new object[]
			{
				GameController.instance.playersData[Random.Range(0, GameController.instance.playersData.Count)].player.modelID
			});
			return;
		}
		this.view.RPC("SpawnRandomPlayerModel", PhotonTargets.All, new object[]
		{
			Random.Range(0, this.models.Length)
		});
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x00024BC6 File Offset: 0x00022DC6
	public void Stop()
	{
		this.targetPlayer = null;
		this.walkTimer = 0.7f;
		this.view.RPC("StopNetworked", PhotonTargets.All, Array.Empty<object>());
		this.agent.isStopped = true;
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x00024BFC File Offset: 0x00022DFC
	[PunRPC]
	public void StopNetworked()
	{
		this.evidence.enabled = false;
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x00024C0A File Offset: 0x00022E0A
	[PunRPC]
	private void SpawnRandomPlayerModel(int id)
	{
		this.evidence.enabled = true;
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x00024C18 File Offset: 0x00022E18
	private void Update()
	{
		if (this.targetPlayer != null)
		{
			this.agent.SetDestination(this.targetPlayer.headObject.transform.position);
			this.walkTimer -= Time.deltaTime;
			if (this.walkTimer < 0f)
			{
				this.view.RPC("NetworkedPlaySound", PhotonTargets.All, Array.Empty<object>());
				this.walkTimer = 0.7f;
			}
		}
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x00024C94 File Offset: 0x00022E94
	[PunRPC]
	private void NetworkedPlaySound()
	{
		this.source.clip = this.footstepClips[Random.Range(0, this.footstepClips.Length)];
		this.source.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		this.source.Play();
	}

	// Token: 0x04000657 RID: 1623
	private PhotonView view;

	// Token: 0x04000658 RID: 1624
	private NavMeshAgent agent;

	// Token: 0x04000659 RID: 1625
	private Player targetPlayer;

	// Token: 0x0400065A RID: 1626
	[SerializeField]
	private GameObject[] models;

	// Token: 0x0400065B RID: 1627
	public AudioSource screamSource;

	// Token: 0x0400065C RID: 1628
	[SerializeField]
	private AudioSource source;

	// Token: 0x0400065D RID: 1629
	[SerializeField]
	private AudioClip[] footstepClips;

	// Token: 0x0400065E RID: 1630
	private float walkTimer = 0.7f;

	// Token: 0x0400065F RID: 1631
	[SerializeField]
	private Evidence evidence;
}
