using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000141 RID: 321
[RequireComponent(typeof(PhotonView))]
public class AnimationObject : MonoBehaviour
{
	// Token: 0x06000851 RID: 2129 RVA: 0x00031AAB File Offset: 0x0002FCAB
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.anim.enabled = false;
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x00031AC5 File Offset: 0x0002FCC5
	public void Use()
	{
		if (!this.anim.isActiveAndEnabled)
		{
			this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
		}
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x00031AEA File Offset: 0x0002FCEA
	private IEnumerator PlayAnimation()
	{
		if (this.source != null)
		{
			this.source.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
			this.source.Play();
		}
		this.anim.enabled = true;
		yield return new WaitForSeconds(this.animTimer);
		this.anim.enabled = false;
		yield break;
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x00031AF9 File Offset: 0x0002FCF9
	[PunRPC]
	private void NetworkedUse()
	{
		base.StartCoroutine(this.PlayAnimation());
	}

	// Token: 0x04000865 RID: 2149
	private PhotonView view;

	// Token: 0x04000866 RID: 2150
	[SerializeField]
	private Animator anim;

	// Token: 0x04000867 RID: 2151
	[SerializeField]
	private AudioSource source;

	// Token: 0x04000868 RID: 2152
	[SerializeField]
	private float animTimer;
}
