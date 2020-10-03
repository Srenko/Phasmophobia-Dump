using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x020001B3 RID: 435
public class PCPushToTalk : MonoBehaviour
{
	// Token: 0x06000BDE RID: 3038 RVA: 0x0004A31C File Offset: 0x0004851C
	private void Awake()
	{
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x0004A330 File Offset: 0x00048530
	private void Start()
	{
		this.player.myVoiceRecorder.Transmit = false;
		if (this.view.isMine)
		{
			if (PlayerPrefs.GetInt("localPushToTalkValue") == 1)
			{
				this.isPushToTalk = false;
				PhotonVoiceSettings.Instance.VoiceDetection = true;
				PhotonVoiceSettings.Instance.AutoTransmit = true;
				return;
			}
			this.isPushToTalk = true;
			PhotonVoiceSettings.Instance.VoiceDetection = false;
			PhotonVoiceSettings.Instance.AutoTransmit = false;
		}
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x0004A3A4 File Offset: 0x000485A4
	private void Update()
	{
		if (this.player.myVoiceRecorder.IsTransmitting)
		{
			if (!this.noise.gameObject.activeInHierarchy)
			{
				this.noise.gameObject.SetActive(true);
				return;
			}
		}
		else if (this.noise.gameObject.activeInHierarchy)
		{
			this.noise.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x0004A40C File Offset: 0x0004860C
	public void OnLocalPushToTalk(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			if (this.player.view.isMine && this.isPushToTalk)
			{
				this.player.myVoiceRecorder.Transmit = true;
			}
			this.isPressingPushToTalk = true;
		}
		if (context.phase == InputActionPhase.Canceled)
		{
			if (this.player.view.isMine && this.isPushToTalk)
			{
				this.player.myVoiceRecorder.Transmit = false;
			}
			this.isPressingPushToTalk = false;
		}
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x0004A494 File Offset: 0x00048694
	public void OnGlobalPushToTalk(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			if (this.walkieTalkie.isActiveAndEnabled && this.player.view.isMine)
			{
				if (this.isPushToTalk)
				{
					this.player.myVoiceRecorder.Transmit = true;
				}
				this.walkieTalkie.Use();
			}
			this.isPressingPushToTalk = true;
		}
		if (context.phase == InputActionPhase.Canceled)
		{
			if (this.walkieTalkie.isActiveAndEnabled && this.player.view.isMine)
			{
				if (this.isPushToTalk)
				{
					this.player.myVoiceRecorder.Transmit = false;
				}
				this.walkieTalkie.Stop();
			}
			this.isPressingPushToTalk = true;
		}
	}

	// Token: 0x04000C21 RID: 3105
	[SerializeField]
	private Player player;

	// Token: 0x04000C22 RID: 3106
	[SerializeField]
	private WalkieTalkie walkieTalkie;

	// Token: 0x04000C23 RID: 3107
	[SerializeField]
	private Noise noise;

	// Token: 0x04000C24 RID: 3108
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000C25 RID: 3109
	public bool isPushToTalk = true;

	// Token: 0x04000C26 RID: 3110
	public bool isPressingPushToTalk;
}
