using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000153 RID: 339
public class Candle : MonoBehaviour
{
	// Token: 0x060008F6 RID: 2294 RVA: 0x00035C84 File Offset: 0x00033E84
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00035C94 File Offset: 0x00033E94
	private void Start()
	{
		this.isOn = false;
		this.flame.SetActive(false);
		if (this.photonInteract == null)
		{
			base.GetComponent<PhotonObjectInteract>().AddPCSecondaryUseEvent(new UnityAction(this.SecondaryUse));
			return;
		}
		this.photonInteract.AddPCSecondaryUseEvent(new UnityAction(this.SecondaryUse));
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00035CF4 File Offset: 0x00033EF4
	public void Use()
	{
		this.isOn = !this.isOn;
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("NetworkedUse", PhotonTargets.All, new object[]
			{
				this.isOn
			});
			return;
		}
		this.NetworkedUse(this.isOn);
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00035D49 File Offset: 0x00033F49
	[PunRPC]
	private void NetworkedUse(bool _isOn)
	{
		base.StopCoroutine(this.CandleOffTimer());
		this.isOn = _isOn;
		this.flame.SetActive(this.isOn);
		if (this.view.isMine)
		{
			base.StartCoroutine(this.CandleOffTimer());
		}
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00035D89 File Offset: 0x00033F89
	private IEnumerator CandleOffTimer()
	{
		yield return new WaitForSeconds(Random.Range(150f, 300f));
		if (!this.stayOn && this.isOn)
		{
			this.Use();
		}
		yield break;
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00035D98 File Offset: 0x00033F98
	private void SecondaryUse()
	{
		if (this.isOn)
		{
			this.playerAim = GameController.instance.myPlayer.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(this.playerAim, out raycastHit, this.grabDistance, this.mask, QueryTriggerInteraction.Ignore))
			{
				if (raycastHit.collider.GetComponent<Candle>())
				{
					if (!raycastHit.collider.GetComponent<Candle>().isOn)
					{
						raycastHit.collider.GetComponent<Candle>().Use();
						return;
					}
				}
				else if (raycastHit.collider.GetComponent<WhiteSage>())
				{
					raycastHit.collider.GetComponent<WhiteSage>().Use();
					return;
				}
			}
		}
		else
		{
			for (int i = 0; i < GameController.instance.myPlayer.player.pcPropGrab.inventoryProps.Count; i++)
			{
				if (GameController.instance.myPlayer.player.pcPropGrab.inventoryProps[i] != null && GameController.instance.myPlayer.player.pcPropGrab.inventoryProps[i].GetComponent<Lighter>())
				{
					this.Use();
				}
			}
		}
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x00035EEC File Offset: 0x000340EC
	private void OnTriggerEnter(Collider other)
	{
		if (this.isOn)
		{
			return;
		}
		if (other.GetComponent<Lighter>())
		{
			if (other.GetComponent<Lighter>().isOn)
			{
				this.Use();
				return;
			}
		}
		else if (other.GetComponent<Candle>() && other.GetComponent<Candle>().isOn)
		{
			this.Use();
		}
	}

	// Token: 0x04000908 RID: 2312
	[SerializeField]
	private GameObject flame;

	// Token: 0x04000909 RID: 2313
	[HideInInspector]
	public PhotonView view;

	// Token: 0x0400090A RID: 2314
	[HideInInspector]
	public bool isOn;

	// Token: 0x0400090B RID: 2315
	public bool stayOn;

	// Token: 0x0400090C RID: 2316
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x0400090D RID: 2317
	[Header("PC")]
	private float grabDistance = 3f;

	// Token: 0x0400090E RID: 2318
	private Ray playerAim;

	// Token: 0x0400090F RID: 2319
	[SerializeField]
	private LayerMask mask;
}
