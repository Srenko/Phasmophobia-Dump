using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000123 RID: 291
[RequireComponent(typeof(PhotonView))]
public class SanityEffectsController : MonoBehaviour
{
	// Token: 0x060007D8 RID: 2008 RVA: 0x0002ED04 File Offset: 0x0002CF04
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x0002ED14 File Offset: 0x0002CF14
	private void Update()
	{
		this.timer -= Time.deltaTime;
		if (this.timer < 0f)
		{
			if (PhotonNetwork.isMasterClient)
			{
				this.AttemptToSpawnWindowGhost();
			}
			this.timer = Random.Range(10f, (GameController.instance.myPlayer.player.insanity > 50f) ? 30f : 20f);
		}
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x0002ED84 File Offset: 0x0002CF84
	private void AttemptToSpawnWindowGhost()
	{
		if (this.windows.Length == 0 || this.windowGhostObj == null)
		{
			return;
		}
		if (Random.Range(0, 4) != 1)
		{
			return;
		}
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (!GameController.instance.playersData[i].player.isDead && GameController.instance.playersData[i].player.currentRoom == LevelController.instance.outsideRoom)
			{
				return;
			}
		}
		this.view.RPC("SpawnGhostNetworked", PhotonTargets.All, new object[]
		{
			this.windows[Random.Range(0, this.windows.Length)].view.viewID
		});
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x0002EE54 File Offset: 0x0002D054
	[PunRPC]
	private void SpawnGhostNetworked(int windowViewId)
	{
		if (GameController.instance.myPlayer.player.isDead)
		{
			return;
		}
		base.StartCoroutine(this.SpawnGhostAtWindow(PhotonView.Find(windowViewId).GetComponent<Window>()));
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x0002EE85 File Offset: 0x0002D085
	private IEnumerator SpawnGhostAtWindow(Window window)
	{
		if (window.windowGhostStart == null)
		{
			yield return null;
		}
		this.windowGhostObj.transform.position = window.windowGhostStart.position;
		this.windowGhostObj.transform.rotation = window.windowGhostStart.rotation;
		this.windowGhostObj.SetActive(true);
		while (Vector3.Distance(this.windowGhostObj.transform.position, window.windowGhostEnd.position) > 0.2f)
		{
			this.windowGhostObj.transform.Translate(Vector3.forward * Time.deltaTime * 4f);
			yield return null;
		}
		this.windowGhostObj.SetActive(false);
		yield break;
	}

	// Token: 0x040007A0 RID: 1952
	private float timer = 30f;

	// Token: 0x040007A1 RID: 1953
	private Vector3 pos;

	// Token: 0x040007A2 RID: 1954
	private PhotonView view;

	// Token: 0x040007A3 RID: 1955
	[SerializeField]
	private Window[] windows;

	// Token: 0x040007A4 RID: 1956
	[SerializeField]
	private GameObject windowGhostObj;

	// Token: 0x040007A5 RID: 1957
	private GhostTraits.Type ghostType;
}
