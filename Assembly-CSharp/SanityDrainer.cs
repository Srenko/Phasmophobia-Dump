using System;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public class SanityDrainer : MonoBehaviour
{
	// Token: 0x060006AC RID: 1708 RVA: 0x00025390 File Offset: 0x00023590
	private void Awake()
	{
		this.ghostAI = base.GetComponent<GhostAI>();
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x000253A0 File Offset: 0x000235A0
	private void OnEnable()
	{
		if (GameController.instance == null)
		{
			base.enabled = false;
			return;
		}
		if (GameController.instance.myPlayer == null)
		{
			base.enabled = false;
			return;
		}
		if (GameController.instance.myPlayer.player == null)
		{
			base.enabled = false;
			return;
		}
		this.ghostIsVisible = false;
		if (!(this.source == null))
		{
			return;
		}
		if (GameController.instance == null)
		{
			return;
		}
		if (GameController.instance.myPlayer != null)
		{
			this.source = GameController.instance.myPlayer.player.heartBeatAudioSource;
			return;
		}
		base.enabled = false;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x00025447 File Offset: 0x00023647
	private void OnDisable()
	{
		this.ghostIsVisible = false;
		if (this.source != null)
		{
			this.source.Stop();
		}
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x00025469 File Offset: 0x00023669
	private void Start()
	{
		if (this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Phantom)
		{
			this.strength = 0.4f;
		}
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x00025490 File Offset: 0x00023690
	private void Update()
	{
		this.ghostIsVisible = false;
		this.viewPos = GameController.instance.myPlayer.player.cam.WorldToViewportPoint(base.transform.position);
		if ((this.ghostAI.myRends[0].isVisible || this.ghostAI.isHunting) && (this.ghostAI.ghostIsAppeared || this.ghostAI.isHunting) && this.viewPos.x > 0f && this.viewPos.x < 1f && this.viewPos.y > 0f && this.viewPos.y < 1f && !Physics.Linecast(base.transform.position, GameController.instance.myPlayer.player.headObject.transform.position, this.mask) && Vector3.Distance(base.transform.position, GameController.instance.myPlayer.player.headObject.transform.position) < 10f)
		{
			GameController.instance.myPlayer.player.insanity += Time.deltaTime * this.strength;
			this.ghostIsVisible = true;
			if (!this.source.isPlaying)
			{
				this.source.Play();
			}
		}
		if (!this.ghostIsVisible && this.source.isPlaying)
		{
			this.source.Stop();
		}
	}

	// Token: 0x0400066E RID: 1646
	private Vector3 viewPos;

	// Token: 0x0400066F RID: 1647
	private GhostAI ghostAI;

	// Token: 0x04000670 RID: 1648
	[SerializeField]
	private LayerMask mask;

	// Token: 0x04000671 RID: 1649
	private AudioSource source;

	// Token: 0x04000672 RID: 1650
	private bool ghostIsVisible;

	// Token: 0x04000673 RID: 1651
	private float strength = 0.2f;
}
