using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

// Token: 0x02000162 RID: 354
public class PainKillers : MonoBehaviour
{
	// Token: 0x0600097F RID: 2431 RVA: 0x0003A7B8 File Offset: 0x000389B8
	private void Awake()
	{
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0003A7CB File Offset: 0x000389CB
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x0003A7E4 File Offset: 0x000389E4
	private void Use()
	{
		GameController.instance.myPlayer.player.insanity -= 40f;
		this.view.RPC("NetworkedUse", PhotonTargets.AllBuffered, new object[]
		{
			GameController.instance.myPlayer.photonPlayer
		});
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0003A83C File Offset: 0x00038A3C
	[PunRPC]
	private void NetworkedUse(PhotonPlayer player)
	{
		base.StartCoroutine(this.PlayNoiseObject());
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i].photonPlayer == player)
			{
				GameController.instance.playersData[i].player.sanityPillsAudioSource.Play();
			}
		}
		if (this.view.isMine)
		{
			if (XRDevice.isPresent)
			{
				this.photonInteract.ActivateHands();
			}
			else
			{
				GameController.instance.myPlayer.player.pcPropGrab.Drop(false);
			}
		}
		if (PhotonNetwork.isMasterClient)
		{
			LevelController.instance.itemSpawner.RemovePainKillers();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x0003A904 File Offset: 0x00038B04
	private IEnumerator PlayNoiseObject()
	{
		this.noise.gameObject.SetActive(true);
		yield return 0;
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x04000990 RID: 2448
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000991 RID: 2449
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000992 RID: 2450
	[SerializeField]
	private Noise noise;
}
