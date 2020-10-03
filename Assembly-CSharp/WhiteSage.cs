using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200016F RID: 367
public class WhiteSage : MonoBehaviour
{
	// Token: 0x060009C7 RID: 2503 RVA: 0x0003C1E7 File Offset: 0x0003A3E7
	private void Awake()
	{
		this.smoke.SetActive(false);
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x0003C1F5 File Offset: 0x0003A3F5
	private void Start()
	{
		this.photonObjectInteract.AddPCSecondaryUseEvent(new UnityAction(this.OnSecondaryUse));
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x0003C20E File Offset: 0x0003A40E
	private IEnumerator WhiteSageUsed()
	{
		this.smoke.SetActive(true);
		this.isOn = true;
		yield return new WaitForSeconds(15f);
		this.isOn = false;
		this.smoke.SetActive(false);
		for (int i = 0; i < this.rends.Length; i++)
		{
			this.rends[i].material.color = new Color32(99, 99, 99, byte.MaxValue);
		}
		this.Check();
		yield break;
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x0003C220 File Offset: 0x0003A420
	private void OnEnable()
	{
		if (this.isOn)
		{
			this.isOn = false;
			for (int i = 0; i < this.rends.Length; i++)
			{
				this.rends[i].material.color = new Color32(99, 99, 99, byte.MaxValue);
			}
			this.Check();
		}
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x0003C27C File Offset: 0x0003A47C
	public void Use()
	{
		if (!this.hasBeenUsed)
		{
			this.hasBeenUsed = true;
			if (PhotonNetwork.inRoom)
			{
				this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
				return;
			}
			this.NetworkedUse();
		}
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0003C2B1 File Offset: 0x0003A4B1
	[PunRPC]
	private void NetworkedUse()
	{
		this.hasBeenUsed = true;
		base.StartCoroutine(this.WhiteSageUsed());
		this.Check();
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0003C2D0 File Offset: 0x0003A4D0
	private void Check()
	{
		if (!PhotonNetwork.isMasterClient)
		{
			return;
		}
		if (!this.hasMultiplied && LevelController.instance.currentGhostRoom != null && SoundController.instance.GetFloorTypeFromPosition(base.transform.position.y) == LevelController.instance.currentGhostRoom.floorType && Vector3.Distance(base.transform.position, LevelController.instance.currentGhost.transform.position) < 6f)
		{
			LevelController.instance.currentGhost.ghostInfo.activityMultiplier += (float)Random.Range(20, 30);
			this.hasMultiplied = true;
		}
		if (Vector3.Distance(base.transform.position, LevelController.instance.currentGhost.raycastPoint.transform.position) < 6f)
		{
			LevelController.instance.currentGhost.StartCoroutine(LevelController.instance.currentGhost.StopGhostFromHunting());
		}
		LevelController.instance.currentGhost.StartCoroutine(LevelController.instance.currentGhost.StopHuntingFortime());
		if (LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Yurei)
		{
			LevelController.instance.currentGhost.StartCoroutine(LevelController.instance.currentGhost.TemporarilyStopWander());
		}
		if (MissionBurnSage.instance != null && !MissionBurnSage.instance.completed && LevelController.instance.currentGhostRoom != null && SoundController.instance.GetFloorTypeFromPosition(base.transform.position.y) == LevelController.instance.currentGhostRoom.floorType && Vector3.Distance(base.transform.position, LevelController.instance.currentGhost.raycastPoint.transform.position) < 6f)
		{
			MissionBurnSage.instance.CompleteMission();
		}
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0003C4C4 File Offset: 0x0003A6C4
	private void OnSecondaryUse()
	{
		for (int i = 0; i < GameController.instance.myPlayer.player.pcPropGrab.inventoryProps.Count; i++)
		{
			if (GameController.instance.myPlayer.player.pcPropGrab.inventoryProps[i] != null && GameController.instance.myPlayer.player.pcPropGrab.inventoryProps[i].GetComponent<Lighter>())
			{
				this.Use();
			}
		}
	}

	// Token: 0x040009E0 RID: 2528
	[SerializeField]
	private GameObject smoke;

	// Token: 0x040009E1 RID: 2529
	[SerializeField]
	private PhotonView view;

	// Token: 0x040009E2 RID: 2530
	[SerializeField]
	private PhotonObjectInteract photonObjectInteract;

	// Token: 0x040009E3 RID: 2531
	[SerializeField]
	private Renderer[] rends;

	// Token: 0x040009E4 RID: 2532
	private bool hasBeenUsed;

	// Token: 0x040009E5 RID: 2533
	private bool hasMultiplied;

	// Token: 0x040009E6 RID: 2534
	private bool isOn;
}
