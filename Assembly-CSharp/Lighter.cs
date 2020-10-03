using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

// Token: 0x0200015E RID: 350
public class Lighter : MonoBehaviour
{
	// Token: 0x0600094F RID: 2383 RVA: 0x00038785 File Offset: 0x00036985
	private void Awake()
	{
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x00038798 File Offset: 0x00036998
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
		this.photonInteract.AddPCSecondaryUseEvent(new UnityAction(this.SecondaryUse));
		this.photonInteract.AddPCUnGrabbedEvent(new UnityAction(this.TurnOff));
		this.photonInteract.AddUnGrabbedEvent(new UnityAction(this.TurnOff));
		this.isOn = false;
		this.flame.SetActive(false);
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x00038814 File Offset: 0x00036A14
	private void Use()
	{
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
			return;
		}
		this.NetworkedUse();
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x0003883C File Offset: 0x00036A3C
	private void SecondaryUse()
	{
		if (this.isOn && !XRDevice.isPresent)
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
				else
				{
					if (raycastHit.collider.GetComponent<WhiteSage>())
					{
						raycastHit.collider.GetComponent<WhiteSage>().Use();
						return;
					}
					if (raycastHit.collider.GetComponentInChildren<Candle>() && !raycastHit.collider.GetComponentInChildren<Candle>().isOn)
					{
						raycastHit.collider.GetComponentInChildren<Candle>().Use();
					}
				}
			}
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x0003894C File Offset: 0x00036B4C
	[PunRPC]
	private void NetworkedUse()
	{
		this.isOn = !this.isOn;
		base.StartCoroutine(this.PlayNoiseObject());
		if (this.source == null)
		{
			this.source = base.GetComponent<AudioSource>();
		}
		this.flame.SetActive(this.isOn);
		this.source.Play();
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x000389AB File Offset: 0x00036BAB
	private void TurnOff()
	{
		this.view.RPC("NetworkedTurnOff", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x000389C4 File Offset: 0x00036BC4
	[PunRPC]
	private void NetworkedTurnOff()
	{
		this.isOn = false;
		base.StartCoroutine(this.PlayNoiseObject());
		if (this.source == null)
		{
			this.source = base.GetComponent<AudioSource>();
		}
		this.flame.SetActive(false);
		this.source.Play();
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x00038A16 File Offset: 0x00036C16
	private IEnumerator PlayNoiseObject()
	{
		this.noise.gameObject.SetActive(true);
		yield return 0;
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x04000963 RID: 2403
	[SerializeField]
	private GameObject flame;

	// Token: 0x04000964 RID: 2404
	[SerializeField]
	private AudioSource source;

	// Token: 0x04000965 RID: 2405
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000966 RID: 2406
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000967 RID: 2407
	[SerializeField]
	private Noise noise;

	// Token: 0x04000968 RID: 2408
	[HideInInspector]
	public bool isOn;

	// Token: 0x04000969 RID: 2409
	[Header("PC")]
	private float grabDistance = 3f;

	// Token: 0x0400096A RID: 2410
	private Ray playerAim;

	// Token: 0x0400096B RID: 2411
	[SerializeField]
	private LayerMask mask;
}
