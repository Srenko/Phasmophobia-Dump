using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200015D RID: 349
public class Key : MonoBehaviour
{
	// Token: 0x0600094A RID: 2378 RVA: 0x00038641 File Offset: 0x00036841
	private void Awake()
	{
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x0003865C File Offset: 0x0003685C
	private void Start()
	{
		this.photonInteract.AddGrabbedEvent(new UnityAction(this.Grabbed));
		this.photonInteract.AddPCGrabbedEvent(new UnityAction(this.Grabbed));
		switch (this.type)
		{
		case Key.KeyType.main:
			this.KeyName = "Key_Main";
			return;
		case Key.KeyType.basement:
			this.KeyName = "Key_Basement";
			return;
		case Key.KeyType.garage:
			this.KeyName = "Key_Garage";
			return;
		case Key.KeyType.Car:
			this.KeyName = "Key_Car";
			return;
		default:
			return;
		}
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x000386E4 File Offset: 0x000368E4
	private void Grabbed()
	{
		this.view.RPC("GrabbedKey", PhotonTargets.AllBuffered, new object[]
		{
			PhotonNetwork.player
		});
		GameController.instance.myPlayer.player.keysAudioSource.Play();
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x00038720 File Offset: 0x00036920
	[PunRPC]
	private void GrabbedKey(PhotonPlayer player)
	{
		GameController.instance.myPlayer.player.keys.Add(this.type);
		LevelController.instance.journalController.AddKey(LocalisationSystem.GetLocalisedValue(this.KeyName));
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400095C RID: 2396
	private PhotonView view;

	// Token: 0x0400095D RID: 2397
	public Key.KeyType type;

	// Token: 0x0400095E RID: 2398
	private Door door;

	// Token: 0x0400095F RID: 2399
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000960 RID: 2400
	private Ray playerAim;

	// Token: 0x04000961 RID: 2401
	[SerializeField]
	private LayerMask mask;

	// Token: 0x04000962 RID: 2402
	private string KeyName = "Key";

	// Token: 0x02000531 RID: 1329
	public enum KeyType
	{
		// Token: 0x040024E3 RID: 9443
		main,
		// Token: 0x040024E4 RID: 9444
		basement,
		// Token: 0x040024E5 RID: 9445
		garage,
		// Token: 0x040024E6 RID: 9446
		Car,
		// Token: 0x040024E7 RID: 9447
		none
	}
}
