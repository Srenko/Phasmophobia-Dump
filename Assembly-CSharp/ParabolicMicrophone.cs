using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000163 RID: 355
public class ParabolicMicrophone : MonoBehaviour
{
	// Token: 0x06000985 RID: 2437 RVA: 0x0003A913 File Offset: 0x00038B13
	private void Awake()
	{
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.view = base.GetComponent<PhotonView>();
		this.screenText.gameObject.SetActive(false);
		this.rend.material.DisableKeyword("_EMISSION");
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0003A953 File Offset: 0x00038B53
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0003A96C File Offset: 0x00038B6C
	private void Use()
	{
		this.view.RPC("UseNetworked", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0003A984 File Offset: 0x00038B84
	[PunRPC]
	private void UseNetworked()
	{
		this.isOn = !this.isOn;
		this.screenText.text = "00.0";
		this.screenText.gameObject.SetActive(this.isOn);
		if (this.isOn)
		{
			this.rend.material.EnableKeyword("_EMISSION");
			return;
		}
		this.rend.material.DisableKeyword("_EMISSION");
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0003A9FC File Offset: 0x00038BFC
	private void Update()
	{
		if (this.isOn)
		{
			this.checkTimer -= Time.deltaTime;
			if (this.checkTimer < 0f)
			{
				for (int i = 0; i < this.noises.Count; i++)
				{
					this.volume += this.noises[i].volume;
				}
				this.screenText.text = (this.volume * 10f).ToString("00.0");
				this.noises.Clear();
				this.volume = 0f;
				base.StartCoroutine(this.ResetTrigger());
				this.checkTimer = Random.Range(1f, 2f);
			}
		}
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0003AAC6 File Offset: 0x00038CC6
	private IEnumerator ResetTrigger()
	{
		this.col.enabled = false;
		yield return 0;
		this.col.enabled = true;
		yield break;
	}

	// Token: 0x04000993 RID: 2451
	[HideInInspector]
	public bool isOn;

	// Token: 0x04000994 RID: 2452
	public List<Noise> noises = new List<Noise>();

	// Token: 0x04000995 RID: 2453
	private float volume;

	// Token: 0x04000996 RID: 2454
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000997 RID: 2455
	private PhotonView view;

	// Token: 0x04000998 RID: 2456
	private float checkTimer = 2f;

	// Token: 0x04000999 RID: 2457
	[SerializeField]
	private BoxCollider col;

	// Token: 0x0400099A RID: 2458
	[SerializeField]
	private Text screenText;

	// Token: 0x0400099B RID: 2459
	[SerializeField]
	private Renderer rend;
}
