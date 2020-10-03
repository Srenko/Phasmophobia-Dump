using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001B5 RID: 437
public class DeadPlayer : MonoBehaviour
{
	// Token: 0x06000BE8 RID: 3048 RVA: 0x0004A620 File Offset: 0x00048820
	public void Spawn(int modelID, int actorID)
	{
		this.view.RPC("SpawnBody", PhotonTargets.All, new object[]
		{
			modelID
		});
		this.view.RPC("PlaySound", PhotonTargets.All, new object[]
		{
			actorID
		});
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x0004A66D File Offset: 0x0004886D
	[PunRPC]
	private void SpawnBody(int modelID)
	{
		this.characterModels[modelID].SetActive(true);
		base.StartCoroutine(this.EnableRagdoll());
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x0004A68A File Offset: 0x0004888A
	private IEnumerator EnableRagdoll()
	{
		yield return new WaitForSeconds(4f);
		for (int i = 0; i < this.ragdollAnims.Length; i++)
		{
			this.ragdollAnims[i].enabled = false;
		}
		for (int j = 0; j < this.ragdollColliders.Length; j++)
		{
			if (this.ragdollColliders[j].gameObject.activeInHierarchy)
			{
				this.ragdollColliders[j].attachedRigidbody.velocity = Vector3.zero;
			}
		}
		for (int k = 0; k < this.ragdollColliders.Length; k++)
		{
			if (this.ragdollColliders[k].gameObject.activeInHierarchy)
			{
				this.ragdollColliders[k].attachedRigidbody.isKinematic = false;
			}
		}
		yield return new WaitForSeconds(6f);
		for (int l = 0; l < this.ragdollColliders.Length; l++)
		{
			if (this.ragdollColliders[l].gameObject.activeInHierarchy)
			{
				this.ragdollColliders[l].attachedRigidbody.velocity = Vector3.zero;
				this.ragdollColliders[l].attachedRigidbody.useGravity = false;
				this.ragdollColliders[l].attachedRigidbody.isKinematic = true;
				this.ragdollColliders[l].enabled = false;
			}
		}
		yield break;
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x0004A69C File Offset: 0x0004889C
	[PunRPC]
	private void PlaySound(int actorID)
	{
		bool flag = false;
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i].actorID == actorID)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			this.source.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		}
		this.source.Play();
	}

	// Token: 0x04000C2A RID: 3114
	[SerializeField]
	private GameObject[] characterModels;

	// Token: 0x04000C2B RID: 3115
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000C2C RID: 3116
	[SerializeField]
	private AudioSource source;

	// Token: 0x04000C2D RID: 3117
	[SerializeField]
	private Collider[] ragdollColliders;

	// Token: 0x04000C2E RID: 3118
	[SerializeField]
	private Animator[] ragdollAnims;
}
