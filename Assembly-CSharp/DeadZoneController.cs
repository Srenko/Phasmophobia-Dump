using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;

// Token: 0x02000112 RID: 274
public class DeadZoneController : MonoBehaviour
{
	// Token: 0x06000748 RID: 1864 RVA: 0x0002A99C File Offset: 0x00028B9C
	private void Awake()
	{
		DeadZoneController.instance = this;
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x0002A9B0 File Offset: 0x00028BB0
	private void Start()
	{
		this.EnableOrDisableDeadZone(false);
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x0002A9BC File Offset: 0x00028BBC
	public void SpawnDeathRoom()
	{
		this.playerToAttack = LevelController.instance.currentGhost.playerToKill;
		this.oldGhostPos = LevelController.instance.currentGhost.transform.position;
		this.view.RPC("SpawnDeathRoomNetworked", LevelController.instance.currentGhost.playerToKill.view.owner, Array.Empty<object>());
		base.StartCoroutine(this.TeleportGhostDelay());
		base.StartCoroutine(this.KillPlayerAfterDelay());
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x0002AA40 File Offset: 0x00028C40
	private IEnumerator TeleportGhostDelay()
	{
		yield return new WaitForSeconds(3f);
		LevelController.instance.currentGhost.agent.Warp(this.ghostSpawn.position);
		LevelController.instance.currentGhost.anim.SetBool("isIdle", true);
		yield break;
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x0002AA4F File Offset: 0x00028C4F
	private IEnumerator KillPlayerAfterDelay()
	{
		yield return new WaitForSeconds(5f);
		this.DespawnDeathRoom();
		this.playerToAttack = null;
		yield break;
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x0002AA60 File Offset: 0x00028C60
	[PunRPC]
	private void SpawnDeathRoomNetworked()
	{
		GameController.instance.myPlayer.player.StopAllMovement();
		GameController.instance.myPlayer.player.ghostDeathHands.SetActive(false);
		this.deadZoneLightObj.SetActive(true);
		if (XRDevice.isPresent)
		{
			this.oldSteamVRPos = GameController.instance.myPlayer.player.steamVRObj.position;
			this.oldVRIKPos = GameController.instance.myPlayer.player.VRIKObj.position;
		}
		else
		{
			this.oldPCPlayerPos = GameController.instance.myPlayer.player.transform.position;
		}
		for (int i = 0; i < this.deathRoomObjets.Length; i++)
		{
			this.deathRoomObjets[i].SetActive(true);
		}
		if (XRDevice.isPresent)
		{
			GameController.instance.myPlayer.player.steamVRObj.position = Vector3.zero;
			GameController.instance.myPlayer.player.VRIKObj.position = this.playerSpawn.position;
			return;
		}
		GameController.instance.myPlayer.player.transform.position = this.playerSpawn.position + Vector3.up;
		GameController.instance.myPlayer.player.charController.velocity.Set(0f, 0f, 0f);
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0002ABD8 File Offset: 0x00028DD8
	private void DespawnDeathRoom()
	{
		LevelController.instance.currentGhost.agent.Warp(this.oldGhostPos);
		LevelController.instance.currentGhost.ChangeState(GhostAI.States.favouriteRoom, null, null);
		this.view.RPC("DespawnDeathRoomNetworked", this.playerToAttack.view.owner, Array.Empty<object>());
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0002AC37 File Offset: 0x00028E37
	[PunRPC]
	private void DespawnDeathRoomNetworked()
	{
		base.StartCoroutine(this.DespawnDeathRoomEvent());
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x0002AC46 File Offset: 0x00028E46
	private IEnumerator DespawnDeathRoomEvent()
	{
		this.deadZoneLightObj.SetActive(false);
		this.deadZoneLightSmashAudio.Play();
		yield return new WaitForSeconds(2f);
		GameController.instance.myPlayer.player.chokingAudioSource.Play();
		yield return new WaitForSeconds(1.7f);
		GameController.instance.myPlayer.player.KillPlayer();
		if (XRDevice.isPresent)
		{
			GameController.instance.myPlayer.player.steamVRObj.position = this.oldSteamVRPos;
			GameController.instance.myPlayer.player.VRIKObj.position = this.oldVRIKPos;
		}
		else
		{
			GameController.instance.myPlayer.player.transform.position = this.oldPCPlayerPos;
		}
		for (int i = 0; i < this.deathRoomObjets.Length; i++)
		{
			this.deathRoomObjets[i].SetActive(false);
		}
		yield break;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x0002AC55 File Offset: 0x00028E55
	public void EnableOrDisableDeadZone(bool active)
	{
		this.zoneObjects.SetActive(active);
	}

	// Token: 0x040006ED RID: 1773
	public static DeadZoneController instance;

	// Token: 0x040006EE RID: 1774
	public GameObject zoneObjects;

	// Token: 0x040006EF RID: 1775
	private PhotonView view;

	// Token: 0x040006F0 RID: 1776
	[SerializeField]
	private GameObject[] deathRoomObjets;

	// Token: 0x040006F1 RID: 1777
	[SerializeField]
	private Transform ghostSpawn;

	// Token: 0x040006F2 RID: 1778
	[SerializeField]
	private Transform playerSpawn;

	// Token: 0x040006F3 RID: 1779
	private Vector3 oldGhostPos;

	// Token: 0x040006F4 RID: 1780
	private Vector3 oldPCPlayerPos;

	// Token: 0x040006F5 RID: 1781
	private Vector3 oldSteamVRPos;

	// Token: 0x040006F6 RID: 1782
	private Vector3 oldVRIKPos;

	// Token: 0x040006F7 RID: 1783
	private Player playerToAttack;

	// Token: 0x040006F8 RID: 1784
	[SerializeField]
	private GameObject deadZoneLightObj;

	// Token: 0x040006F9 RID: 1785
	[SerializeField]
	private AudioSource deadZoneLightSmashAudio;
}
