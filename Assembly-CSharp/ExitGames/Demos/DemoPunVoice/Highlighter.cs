using System;
using ExitGames.Client.Photon.LoadBalancing;
using UnityEngine;
using UnityEngine.UI;

namespace ExitGames.Demos.DemoPunVoice
{
	// Token: 0x02000491 RID: 1169
	[RequireComponent(typeof(Canvas))]
	public class Highlighter : MonoBehaviour
	{
		// Token: 0x06002468 RID: 9320 RVA: 0x000B1E2D File Offset: 0x000B002D
		private void OnEnable()
		{
			ChangePOV.CameraChanged += this.ChangePOV_CameraChanged;
			VoiceDemoUI.DebugToggled += this.VoiceDemoUI_DebugToggled;
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000B1E51 File Offset: 0x000B0051
		private void OnDisable()
		{
			ChangePOV.CameraChanged -= this.ChangePOV_CameraChanged;
			VoiceDemoUI.DebugToggled -= this.VoiceDemoUI_DebugToggled;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000B1E75 File Offset: 0x000B0075
		private void VoiceDemoUI_DebugToggled(bool debugMode)
		{
			this.showSpeakerLag = debugMode;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000B1E7E File Offset: 0x000B007E
		private void ChangePOV_CameraChanged(Camera camera)
		{
			this.canvas.worldCamera = camera;
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000B1E8C File Offset: 0x000B008C
		private void Awake()
		{
			this.canvas = base.GetComponent<Canvas>();
			if (this.canvas != null && this.canvas.worldCamera == null)
			{
				this.canvas.worldCamera = Camera.main;
			}
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000B1ECC File Offset: 0x000B00CC
		private void Update()
		{
			this.recorderSprite.enabled = (this.recorder != null && this.recorder.IsTransmitting && PhotonVoiceNetwork.ClientState == ExitGames.Client.Photon.LoadBalancing.ClientState.Joined);
			this.speakerSprite.enabled = (this.speaker != null && this.speaker.IsPlaying && PhotonVoiceNetwork.ClientState == ExitGames.Client.Photon.LoadBalancing.ClientState.Joined);
			this.bufferLagText.enabled = (this.showSpeakerLag && this.speaker.IsPlaying && this.speaker.IsVoiceLinked);
			this.bufferLagText.text = string.Format("{0}", this.speaker.CurrentBufferLag);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000B1F90 File Offset: 0x000B0190
		private void LateUpdate()
		{
			if (this.canvas == null || this.canvas.worldCamera == null)
			{
				return;
			}
			base.transform.rotation = Quaternion.Euler(0f, this.canvas.worldCamera.transform.eulerAngles.y, 0f);
		}

		// Token: 0x0400219E RID: 8606
		private Canvas canvas;

		// Token: 0x0400219F RID: 8607
		[SerializeField]
		private PhotonVoiceRecorder recorder;

		// Token: 0x040021A0 RID: 8608
		[SerializeField]
		private PhotonVoiceSpeaker speaker;

		// Token: 0x040021A1 RID: 8609
		[SerializeField]
		private Image recorderSprite;

		// Token: 0x040021A2 RID: 8610
		[SerializeField]
		private Image speakerSprite;

		// Token: 0x040021A3 RID: 8611
		[SerializeField]
		private Text bufferLagText;

		// Token: 0x040021A4 RID: 8612
		private bool showSpeakerLag;
	}
}
