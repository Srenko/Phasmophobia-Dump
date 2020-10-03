using System;
using ExitGames.Client.Photon.LoadBalancing;
using UnityEngine;
using UnityEngine.UI;

namespace ExitGames.Demos.DemoPunVoice
{
	// Token: 0x02000493 RID: 1171
	[RequireComponent(typeof(Button))]
	[DisallowMultipleComponent]
	[SelectionBase]
	public class PushToTalkPrivateButton : MonoBehaviour
	{
		// Token: 0x06002475 RID: 9333 RVA: 0x000B209F File Offset: 0x000B029F
		private void Start()
		{
			this.pttScript = Object.FindObjectOfType<PushToTalkScript>();
			UnityVoiceFrontend client = PhotonVoiceNetwork.Client;
			client.OnStateChangeAction = (Action<ExitGames.Client.Photon.LoadBalancing.ClientState>)Delegate.Combine(client.OnStateChangeAction, new Action<ExitGames.Client.Photon.LoadBalancing.ClientState>(this.OnVoiceClientStateChanged));
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000B20D4 File Offset: 0x000B02D4
		private void OnVoiceClientStateChanged(ExitGames.Client.Photon.LoadBalancing.ClientState state)
		{
			if (this.pushToTalkPrivateButton != null)
			{
				if (state == ExitGames.Client.Photon.LoadBalancing.ClientState.Joined)
				{
					this.pushToTalkPrivateButton.gameObject.SetActive(true);
					this.Subscribed = (this.Subscribed || this.Subscribe());
					return;
				}
				this.pushToTalkPrivateButton.gameObject.SetActive(false);
			}
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000B2130 File Offset: 0x000B0330
		public void SetAudioGroup(PhotonPlayer player)
		{
			if (!this.Subscribed)
			{
				this.TargetActorNr = player.ID;
				this.buttonText.text = string.Format("Talk-To-Player{0}", this.TargetActorNr);
				this.keyCode = KeyCode.Keypad0 + this.TargetActorNr;
				this.keyCode2 = KeyCode.Alpha0 + this.TargetActorNr;
				int num;
				if (PhotonNetwork.player.ID < this.TargetActorNr)
				{
					num = this.TargetActorNr + PhotonNetwork.player.ID * 10;
				}
				else
				{
					if (PhotonNetwork.player.ID <= this.TargetActorNr)
					{
						return;
					}
					num = PhotonNetwork.player.ID + this.TargetActorNr * 10;
				}
				if (num > 255)
				{
					Debug.LogErrorFormat("Unsupprted AudioGroup value: {0}. Will not be able to talk to player number {1}", new object[]
					{
						num,
						player.ID
					});
					Object.Destroy(this);
					return;
				}
				this.AudioGroup = (byte)num;
				if (PhotonVoiceNetwork.ClientState == ExitGames.Client.Photon.LoadBalancing.ClientState.Joined)
				{
					this.Subscribed = this.Subscribe();
				}
			}
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000B223C File Offset: 0x000B043C
		public void PushToTalkOn()
		{
			if (this.pttScript.CurrentTargetGroup != -1)
			{
				Debug.LogWarningFormat("Cannot talk to Player {0} as already talking to another player", new object[]
				{
					this.TargetActorNr
				});
				return;
			}
			if (this.Subscribed)
			{
				this.buttonText.text = string.Format("Talking-To-Player{0}", this.TargetActorNr);
				this.pttScript.PushToTalkOn((int)this.AudioGroup);
				return;
			}
			Debug.LogWarningFormat("Cannot talk to Player {0} as Voice client not subscribed to AudioGroup {1}", new object[]
			{
				this.TargetActorNr,
				this.AudioGroup
			});
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000B22DC File Offset: 0x000B04DC
		public void Update()
		{
			if (Input.GetKeyDown(this.keyCode) || Input.GetKeyDown(this.keyCode2))
			{
				this.PushToTalkOn();
				return;
			}
			if (Input.GetKeyUp(this.keyCode) || Input.GetKeyUp(this.keyCode2))
			{
				this.PushToTalkOff();
			}
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000B232A File Offset: 0x000B052A
		public void PushToTalkOff()
		{
			this.buttonText.text = string.Format("Talk-To-Player{0}", this.TargetActorNr);
			this.pttScript.PushToTalkOff((int)this.AudioGroup);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000B235D File Offset: 0x000B055D
		public bool Subscribe()
		{
			return !this.Subscribed && PhotonVoiceNetwork.Client.ChangeAudioGroups(null, new byte[]
			{
				this.AudioGroup
			});
		}

		// Token: 0x040021A7 RID: 8615
		[SerializeField]
		private Button pushToTalkPrivateButton;

		// Token: 0x040021A8 RID: 8616
		[SerializeField]
		private Text buttonText;

		// Token: 0x040021A9 RID: 8617
		private PushToTalkScript pttScript;

		// Token: 0x040021AA RID: 8618
		public int TargetActorNr;

		// Token: 0x040021AB RID: 8619
		public byte AudioGroup;

		// Token: 0x040021AC RID: 8620
		public bool Subscribed;

		// Token: 0x040021AD RID: 8621
		private KeyCode keyCode;

		// Token: 0x040021AE RID: 8622
		private KeyCode keyCode2;
	}
}
