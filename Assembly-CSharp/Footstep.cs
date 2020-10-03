using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
public class Footstep : MonoBehaviour
{
	// Token: 0x0600081E RID: 2078 RVA: 0x00030B08 File Offset: 0x0002ED08
	private void OnEnable()
	{
		if (LevelController.instance == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		if (LevelController.instance.currentGhost == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		if (LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType != GhostTraits.Type.Wraith)
		{
			this.rend.enabled = LevelController.instance.currentGhost.isHunting;
			this.rend.enabled = LevelController.instance.currentGhost.ghostInteraction.hasWalkedInSalt;
		}
		else
		{
			this.rend.enabled = false;
		}
		if (LevelController.instance.currentGhost.ghostIsAppeared)
		{
			if (LevelController.instance.currentGhost.agent.velocity.magnitude > 0.1f)
			{
				this.src.clip = this.huntingNoises[Random.Range(0, this.huntingNoises.Length)];
				this.src.volume = 1f;
				if (Physics.Linecast(LevelController.instance.currentGhost.raycastPoint.position, GameController.instance.myPlayer.player.headObject.transform.position, LevelController.instance.currentGhost.mask, QueryTriggerInteraction.Ignore))
				{
					this.src.volume = 0.3f;
				}
			}
		}
		else
		{
			this.src.clip = this.footstepNoises[Random.Range(0, this.footstepNoises.Length)];
			if (Physics.Linecast(LevelController.instance.currentGhost.raycastPoint.position, GameController.instance.myPlayer.player.headObject.transform.position, LevelController.instance.currentGhost.mask, QueryTriggerInteraction.Ignore))
			{
				this.src.volume = 0.05f;
			}
		}
		if (LevelController.instance.currentGhost.isHunting)
		{
			this.src.clip = this.huntingNoises[Random.Range(0, this.huntingNoises.Length)];
			this.src.volume = 1f;
			if (Physics.Linecast(LevelController.instance.currentGhost.raycastPoint.position, GameController.instance.myPlayer.player.headObject.transform.position, LevelController.instance.currentGhost.mask, QueryTriggerInteraction.Ignore))
			{
				this.src.volume = 0.3f;
			}
		}
		this.src.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(this.src.transform.position.y);
		this.src.Play();
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00030DD4 File Offset: 0x0002EFD4
	[PunRPC]
	public void Spawn(bool isRight)
	{
		this.timer = Random.Range(10f, 15f);
		this.rend.material.mainTexture = (isRight ? this.leftTexture : this.RightTexture);
		Vector3 localPosition = base.transform.localPosition;
		localPosition.x += (isRight ? 0.1f : -0.1f);
		base.transform.localPosition = localPosition;
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00030E49 File Offset: 0x0002F049
	private void Update()
	{
		this.timer -= Time.deltaTime;
		if (this.timer < 0f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000830 RID: 2096
	[SerializeField]
	private Texture leftTexture;

	// Token: 0x04000831 RID: 2097
	[SerializeField]
	private Texture RightTexture;

	// Token: 0x04000832 RID: 2098
	[SerializeField]
	private AudioSource src;

	// Token: 0x04000833 RID: 2099
	private float timer = 10f;

	// Token: 0x04000834 RID: 2100
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000835 RID: 2101
	[SerializeField]
	private AudioClip[] footstepNoises = new AudioClip[0];

	// Token: 0x04000836 RID: 2102
	[SerializeField]
	private AudioClip[] huntingNoises = new AudioClip[0];

	// Token: 0x04000837 RID: 2103
	[SerializeField]
	private Renderer rend;
}
