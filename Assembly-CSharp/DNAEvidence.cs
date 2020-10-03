using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000130 RID: 304
public class DNAEvidence : MonoBehaviour
{
	// Token: 0x0600080F RID: 2063 RVA: 0x000306A1 File Offset: 0x0002E8A1
	private void Start()
	{
		this.photonInteract.AddPCGrabbedEvent(new UnityAction(this.Grabbed));
		this.photonInteract.AddGrabbedEvent(new UnityAction(this.Grabbed));
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x000306D1 File Offset: 0x0002E8D1
	private IEnumerator PhysicsDelay()
	{
		yield return new WaitForSeconds(3f);
		this.rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
		this.rigid.isKinematic = true;
		this.rigid.useGravity = false;
		yield break;
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x000306E0 File Offset: 0x0002E8E0
	private void Grabbed()
	{
		this.view.RPC("GrabbedNetworked", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x000306F8 File Offset: 0x0002E8F8
	[PunRPC]
	private void GrabbedNetworked()
	{
		EvidenceController.instance.foundGhostDNA = true;
		GameController.instance.myPlayer.player.evidenceAudioSource.Play();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000827 RID: 2087
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000828 RID: 2088
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000829 RID: 2089
	[SerializeField]
	private Rigidbody rigid;
}
