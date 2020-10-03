using System;
using System.Collections.Generic;
using ExitGames.Client.Photon.LoadBalancing;
using UnityEngine;
using UnityEngine.UI;

namespace ExitGames.Demos.DemoPunVoice
{
	// Token: 0x02000494 RID: 1172
	public class PushToTalkScript : MonoBehaviour
	{
		// Token: 0x0600247D RID: 9341 RVA: 0x000B2383 File Offset: 0x000B0583
		public void OnEnable()
		{
			CharacterInstantiation.CharacterInstantiated += this.CharacterInstantiation_CharacterInstantiated;
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000B2396 File Offset: 0x000B0596
		public void OnDisable()
		{
			CharacterInstantiation.CharacterInstantiated -= this.CharacterInstantiation_CharacterInstantiated;
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000B23AC File Offset: 0x000B05AC
		public void Start()
		{
			if (this.pushToTalkButton != null)
			{
				this.pushToTalkText = this.pushToTalkButton.GetComponentInChildren<Text>();
				if (this.pushToTalkText != null)
				{
					this.pushToTalkText.text = "Talk-To-All";
				}
				else
				{
					Debug.LogWarning("Could not find the button's text component.");
				}
				this.pushToTalkButton.gameObject.SetActive(false);
				this.parent = this.pushToTalkButton.transform.parent;
			}
			this.buttons = new Dictionary<int, PushToTalkPrivateButton>(4);
			UnityVoiceFrontend client = PhotonVoiceNetwork.Client;
			client.OnStateChangeAction = (Action<ExitGames.Client.Photon.LoadBalancing.ClientState>)Delegate.Combine(client.OnStateChangeAction, new Action<ExitGames.Client.Photon.LoadBalancing.ClientState>(this.OnVoiceClientStateChanged));
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000B245C File Offset: 0x000B065C
		public void OnToggleValueChanged(bool v)
		{
			this.MuteOthersWhileTalking = v;
			if (this.MuteOthersWhileTalking && this.CurrentTargetGroup != -1)
			{
				this.KeepOnlyOneGroup((byte)this.CurrentTargetGroup);
				return;
			}
			if (!this.MuteOthersWhileTalking)
			{
				if (this.CurrentTargetGroup != -1 && this.rec != null)
				{
					this.rec.AudioGroup = (byte)this.CurrentTargetGroup;
				}
				this.SubscribeToAllPrivateGroups();
			}
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000B24C6 File Offset: 0x000B06C6
		private void CharacterInstantiation_CharacterInstantiated(GameObject character)
		{
			this.rec = character.GetComponent<PhotonVoiceRecorder>();
			if (this.rec != null)
			{
				this.rec.enabled = true;
			}
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000B24F0 File Offset: 0x000B06F0
		private void OnVoiceClientStateChanged(ExitGames.Client.Photon.LoadBalancing.ClientState state)
		{
			if (this.pushToTalkButton != null)
			{
				if (state == ExitGames.Client.Photon.LoadBalancing.ClientState.Joined)
				{
					this.pushToTalkButton.gameObject.SetActive(true);
					for (int i = 0; i < PhotonNetwork.otherPlayers.Length; i++)
					{
						this.OnPhotonPlayerConnected(PhotonNetwork.otherPlayers[i]);
					}
					return;
				}
				this.pushToTalkButton.gameObject.SetActive(false);
			}
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000B2554 File Offset: 0x000B0754
		public void PushToTalkOn(int group)
		{
			if (this.CurrentTargetGroup != -1)
			{
				Debug.LogWarningFormat("Cannot start AudioGroup {0}, player already talking to AudioGroup {1}", new object[]
				{
					group,
					this.CurrentTargetGroup
				});
				return;
			}
			if (this.MuteOthersWhileTalking)
			{
				this.KeepOnlyOneGroup((byte)group);
			}
			else if (this.rec != null)
			{
				this.SubscribeToAllPrivateGroups();
				this.rec.AudioGroup = (byte)group;
			}
			this.PushToTalk(true);
			this.CurrentTargetGroup = group;
			if (group == 0 && this.pushToTalkText != null)
			{
				this.pushToTalkText.text = "Talk now!";
			}
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000B25F4 File Offset: 0x000B07F4
		public void PushToTalkOff(int group)
		{
			if (this.CurrentTargetGroup == group)
			{
				this.PushToTalk(false);
				this.SubscribeToAllPrivateGroups();
			}
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000B260C File Offset: 0x000B080C
		public void PushToTalkOff()
		{
			this.PushToTalkOff(0);
			if (this.pushToTalkText != null)
			{
				this.pushToTalkText.text = "Talk-To-All";
			}
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x000B2633 File Offset: 0x000B0833
		public void PushToTalkOn()
		{
			this.PushToTalkOn(0);
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000B263C File Offset: 0x000B083C
		public void PushToTalk(bool toggle)
		{
			this.CurrentTargetGroup = -1;
			if (this.rec != null)
			{
				this.rec.Transmit = toggle;
			}
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000B265F File Offset: 0x000B085F
		public void OnLeftRoom()
		{
			if (this.rec != null)
			{
				this.rec.enabled = false;
				this.rec = null;
			}
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000B2682 File Offset: 0x000B0882
		public void Update()
		{
			if (Input.GetKeyDown("v"))
			{
				this.PushToTalkOn();
				return;
			}
			if (Input.GetKeyUp("v"))
			{
				this.PushToTalkOff();
			}
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000B26AC File Offset: 0x000B08AC
		public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
		{
			if (this.buttons.ContainsKey(newPlayer.ID))
			{
				Debug.LogWarningFormat(this.buttons[newPlayer.ID].gameObject, "PTT Button already added for player number {0}", new object[]
				{
					newPlayer.ID
				});
				return;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.buttonPrefab);
			gameObject.transform.SetParent(this.parent, false);
			gameObject.transform.SetSiblingIndex(newPlayer.ID + 1);
			PushToTalkPrivateButton componentInChildren = gameObject.GetComponentInChildren<PushToTalkPrivateButton>();
			componentInChildren.SetAudioGroup(newPlayer);
			this.buttons.Add(newPlayer.ID, componentInChildren);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000B2750 File Offset: 0x000B0950
		public void OnPhotonPlayerDisconnected(PhotonPlayer leavingPlayer)
		{
			PhotonVoiceNetwork.Client.ChangeAudioGroups(new byte[]
			{
				this.buttons[leavingPlayer.ID].AudioGroup
			}, null);
			if (this.buttons.ContainsKey(leavingPlayer.ID))
			{
				Object.Destroy(this.buttons[leavingPlayer.ID].gameObject);
				this.buttons.Remove(leavingPlayer.ID);
				return;
			}
			Debug.LogWarningFormat("PTT Button not found for leaving player number {0}", new object[]
			{
				leavingPlayer.ID
			});
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000B27E8 File Offset: 0x000B09E8
		private void SubscribeToAllPrivateGroups()
		{
			PhotonVoiceNetwork.Client.ChangeAudioGroups(null, new byte[0]);
			for (int i = 0; i < PhotonNetwork.otherPlayers.Length; i++)
			{
				this.buttons[PhotonNetwork.otherPlayers[i].ID].Subscribed = true;
			}
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000B2838 File Offset: 0x000B0A38
		private void KeepOnlyOneGroup(byte group)
		{
			for (int i = 0; i < PhotonNetwork.otherPlayers.Length; i++)
			{
				if (this.buttons[PhotonNetwork.otherPlayers[i].ID].AudioGroup != group)
				{
					this.buttons[PhotonNetwork.otherPlayers[i].ID].Subscribed = false;
				}
				else
				{
					PhotonVoiceNetwork.Client.GlobalAudioGroup = group;
					this.buttons[PhotonNetwork.otherPlayers[i].ID].Subscribed = true;
				}
			}
		}

		// Token: 0x040021AF RID: 8623
		[SerializeField]
		private Button pushToTalkButton;

		// Token: 0x040021B0 RID: 8624
		private Text pushToTalkText;

		// Token: 0x040021B1 RID: 8625
		private PhotonVoiceRecorder rec;

		// Token: 0x040021B2 RID: 8626
		private Transform parent;

		// Token: 0x040021B3 RID: 8627
		[SerializeField]
		private GameObject buttonPrefab;

		// Token: 0x040021B4 RID: 8628
		private Dictionary<int, PushToTalkPrivateButton> buttons;

		// Token: 0x040021B5 RID: 8629
		public bool MuteOthersWhileTalking;

		// Token: 0x040021B6 RID: 8630
		public int CurrentTargetGroup = -1;
	}
}
