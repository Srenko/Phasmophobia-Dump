using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Chat;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200004E RID: 78
public class ChatGui : MonoBehaviour, IChatClientListener
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000191 RID: 401 RVA: 0x0000AF41 File Offset: 0x00009141
	// (set) Token: 0x06000192 RID: 402 RVA: 0x0000AF49 File Offset: 0x00009149
	public string UserName { get; set; }

	// Token: 0x06000193 RID: 403 RVA: 0x0000AF54 File Offset: 0x00009154
	public void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		this.UserIdText.text = "";
		this.StateText.text = "";
		this.StateText.gameObject.SetActive(true);
		this.UserIdText.gameObject.SetActive(true);
		this.Title.SetActive(true);
		this.ChatPanel.gameObject.SetActive(false);
		this.ConnectingLabel.SetActive(false);
		if (string.IsNullOrEmpty(this.UserName))
		{
			this.UserName = "user" + Environment.TickCount % 99;
		}
		bool flag = string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.ChatAppID);
		this.missingAppIdErrorPanel.SetActive(flag);
		this.UserIdFormPanel.gameObject.SetActive(!flag);
		if (string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.ChatAppID))
		{
			Debug.LogError("You need to set the chat app ID in the PhotonServerSettings file in order to continue.");
			return;
		}
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000B050 File Offset: 0x00009250
	public void Connect()
	{
		this.UserIdFormPanel.gameObject.SetActive(false);
		this.chatClient = new ChatClient(this, ConnectionProtocol.Udp);
		this.chatClient.UseBackgroundWorkerForSending = true;
		this.chatClient.Connect(PhotonNetwork.PhotonServerSettings.ChatAppID, "1.0", new Photon.Chat.AuthenticationValues(this.UserName));
		this.ChannelToggleToInstantiate.gameObject.SetActive(false);
		Debug.Log("Connecting as: " + this.UserName);
		this.ConnectingLabel.SetActive(true);
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000B0DF File Offset: 0x000092DF
	public void OnDestroy()
	{
		if (this.chatClient != null)
		{
			this.chatClient.Disconnect();
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000B0DF File Offset: 0x000092DF
	public void OnApplicationQuit()
	{
		if (this.chatClient != null)
		{
			this.chatClient.Disconnect();
		}
	}

	// Token: 0x06000197 RID: 407 RVA: 0x0000B0F4 File Offset: 0x000092F4
	public void Update()
	{
		if (this.chatClient != null)
		{
			this.chatClient.Service();
		}
		if (this.StateText == null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		this.StateText.gameObject.SetActive(this.ShowState);
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0000B144 File Offset: 0x00009344
	public void OnEnterSend()
	{
		if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
		{
			this.SendChatMessage(this.InputFieldChat.text);
			this.InputFieldChat.text = "";
		}
	}

	// Token: 0x06000199 RID: 409 RVA: 0x0000B17C File Offset: 0x0000937C
	public void OnClickSend()
	{
		if (this.InputFieldChat != null)
		{
			this.SendChatMessage(this.InputFieldChat.text);
			this.InputFieldChat.text = "";
		}
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000B1B0 File Offset: 0x000093B0
	private void SendChatMessage(string inputLine)
	{
		if (string.IsNullOrEmpty(inputLine))
		{
			return;
		}
		if ("test".Equals(inputLine))
		{
			if (this.TestLength != this.testBytes.Length)
			{
				this.testBytes = new byte[this.TestLength];
			}
			this.chatClient.SendPrivateMessage(this.chatClient.AuthValues.UserId, this.testBytes, true);
		}
		bool flag = this.chatClient.PrivateChannels.ContainsKey(this.selectedChannelName);
		string target = string.Empty;
		if (flag)
		{
			target = this.selectedChannelName.Split(new char[]
			{
				':'
			})[1];
		}
		if (inputLine[0].Equals('\\'))
		{
			string[] array = inputLine.Split(new char[]
			{
				' '
			}, 2);
			if (array[0].Equals("\\help"))
			{
				this.PostHelpToCurrentChannel();
			}
			if (array[0].Equals("\\state"))
			{
				int num = 0;
				List<string> list = new List<string>();
				list.Add("i am state " + num);
				string[] array2 = array[1].Split(new char[]
				{
					' ',
					','
				});
				if (array2.Length != 0)
				{
					num = int.Parse(array2[0]);
				}
				if (array2.Length > 1)
				{
					list.Add(array2[1]);
				}
				this.chatClient.SetOnlineStatus(num, list.ToArray());
				return;
			}
			if ((array[0].Equals("\\subscribe") || array[0].Equals("\\s")) && !string.IsNullOrEmpty(array[1]))
			{
				this.chatClient.Subscribe(array[1].Split(new char[]
				{
					' ',
					','
				}));
				return;
			}
			if ((array[0].Equals("\\unsubscribe") || array[0].Equals("\\u")) && !string.IsNullOrEmpty(array[1]))
			{
				this.chatClient.Unsubscribe(array[1].Split(new char[]
				{
					' ',
					','
				}));
				return;
			}
			if (array[0].Equals("\\clear"))
			{
				if (flag)
				{
					this.chatClient.PrivateChannels.Remove(this.selectedChannelName);
					return;
				}
				ChatChannel chatChannel;
				if (this.chatClient.TryGetChannel(this.selectedChannelName, flag, out chatChannel))
				{
					chatChannel.ClearMessages();
					return;
				}
			}
			else if (array[0].Equals("\\msg") && !string.IsNullOrEmpty(array[1]))
			{
				string[] array3 = array[1].Split(new char[]
				{
					' ',
					','
				}, 2);
				if (array3.Length < 2)
				{
					return;
				}
				string target2 = array3[0];
				string message = array3[1];
				this.chatClient.SendPrivateMessage(target2, message, false);
				return;
			}
			else
			{
				if ((!array[0].Equals("\\join") && !array[0].Equals("\\j")) || string.IsNullOrEmpty(array[1]))
				{
					Debug.Log("The command '" + array[0] + "' is invalid.");
					return;
				}
				string[] array4 = array[1].Split(new char[]
				{
					' ',
					','
				}, 2);
				if (this.channelToggles.ContainsKey(array4[0]))
				{
					this.ShowChannel(array4[0]);
					return;
				}
				this.chatClient.Subscribe(new string[]
				{
					array4[0]
				});
				return;
			}
		}
		else
		{
			if (flag)
			{
				this.chatClient.SendPrivateMessage(target, inputLine, false);
				return;
			}
			this.chatClient.PublishMessage(this.selectedChannelName, inputLine, false);
		}
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000B508 File Offset: 0x00009708
	public void PostHelpToCurrentChannel()
	{
		Text currentChannelText = this.CurrentChannelText;
		currentChannelText.text += ChatGui.HelpText;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000B525 File Offset: 0x00009725
	public void DebugReturn(DebugLevel level, string message)
	{
		if (level == DebugLevel.ERROR)
		{
			Debug.LogError(message);
			return;
		}
		if (level == DebugLevel.WARNING)
		{
			Debug.LogWarning(message);
			return;
		}
		Debug.Log(message);
	}

	// Token: 0x0600019D RID: 413 RVA: 0x0000B544 File Offset: 0x00009744
	public void OnConnected()
	{
		if (this.ChannelsToJoinOnConnect != null && this.ChannelsToJoinOnConnect.Length != 0)
		{
			this.chatClient.Subscribe(this.ChannelsToJoinOnConnect, this.HistoryLengthToFetch);
		}
		this.ConnectingLabel.SetActive(false);
		this.UserIdText.text = "Connected as " + this.UserName;
		this.ChatPanel.gameObject.SetActive(true);
		if (this.FriendsList != null && this.FriendsList.Length != 0)
		{
			this.chatClient.AddFriends(this.FriendsList);
			foreach (string text in this.FriendsList)
			{
				if (this.FriendListUiItemtoInstantiate != null && text != this.UserName)
				{
					this.InstantiateFriendButton(text);
				}
			}
		}
		if (this.FriendListUiItemtoInstantiate != null)
		{
			this.FriendListUiItemtoInstantiate.SetActive(false);
		}
		this.chatClient.SetOnlineStatus(2);
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0000B638 File Offset: 0x00009838
	public void OnDisconnected()
	{
		this.ConnectingLabel.SetActive(false);
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0000B646 File Offset: 0x00009846
	public void OnChatStateChange(ChatState state)
	{
		this.StateText.text = state.ToString();
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000B660 File Offset: 0x00009860
	public void OnSubscribed(string[] channels, bool[] results)
	{
		foreach (string channelName in channels)
		{
			this.chatClient.PublishMessage(channelName, "says 'hi'.", false);
			if (this.ChannelToggleToInstantiate != null)
			{
				this.InstantiateChannelButton(channelName);
			}
		}
		Debug.Log("OnSubscribed: " + string.Join(", ", channels));
		this.ShowChannel(channels[0]);
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000B6CC File Offset: 0x000098CC
	private void InstantiateChannelButton(string channelName)
	{
		if (this.channelToggles.ContainsKey(channelName))
		{
			Debug.Log("Skipping creation for an existing channel toggle.");
			return;
		}
		Toggle toggle = Object.Instantiate<Toggle>(this.ChannelToggleToInstantiate);
		toggle.gameObject.SetActive(true);
		toggle.GetComponentInChildren<ChannelSelector>().SetChannel(channelName);
		toggle.transform.SetParent(this.ChannelToggleToInstantiate.transform.parent, false);
		this.channelToggles.Add(channelName, toggle);
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000B740 File Offset: 0x00009940
	private void InstantiateFriendButton(string friendId)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.FriendListUiItemtoInstantiate);
		gameObject.gameObject.SetActive(true);
		FriendItem component = gameObject.GetComponent<FriendItem>();
		component.FriendId = friendId;
		gameObject.transform.SetParent(this.FriendListUiItemtoInstantiate.transform.parent, false);
		this.friendListItemLUT[friendId] = component;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x0000B79C File Offset: 0x0000999C
	public void OnUnsubscribed(string[] channels)
	{
		foreach (string text in channels)
		{
			if (this.channelToggles.ContainsKey(text))
			{
				Object.Destroy(this.channelToggles[text].gameObject);
				this.channelToggles.Remove(text);
				Debug.Log("Unsubscribed from channel '" + text + "'.");
				if (text == this.selectedChannelName && this.channelToggles.Count > 0)
				{
					IEnumerator<KeyValuePair<string, Toggle>> enumerator = this.channelToggles.GetEnumerator();
					enumerator.MoveNext();
					KeyValuePair<string, Toggle> keyValuePair = enumerator.Current;
					this.ShowChannel(keyValuePair.Key);
					keyValuePair = enumerator.Current;
					keyValuePair.Value.isOn = true;
				}
			}
			else
			{
				Debug.Log("Can't unsubscribe from channel '" + text + "' because you are currently not subscribed to it.");
			}
		}
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x0000B881 File Offset: 0x00009A81
	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		if (channelName.Equals(this.selectedChannelName))
		{
			this.ShowChannel(this.selectedChannelName);
		}
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0000B8A0 File Offset: 0x00009AA0
	public void OnPrivateMessage(string sender, object message, string channelName)
	{
		this.InstantiateChannelButton(channelName);
		byte[] array = message as byte[];
		if (array != null)
		{
			Debug.Log("Message with byte[].Length: " + array.Length);
		}
		if (this.selectedChannelName.Equals(channelName))
		{
			this.ShowChannel(channelName);
		}
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x0000B8EC File Offset: 0x00009AEC
	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		Debug.LogWarning("status: " + string.Format("{0} is {1}. Msg:{2}", user, status, message));
		if (this.friendListItemLUT.ContainsKey(user))
		{
			FriendItem friendItem = this.friendListItemLUT[user];
			if (friendItem != null)
			{
				friendItem.OnFriendStatusUpdate(status, gotMessage, message);
			}
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000B949 File Offset: 0x00009B49
	public void OnUserSubscribed(string channel, string user)
	{
		Debug.LogFormat("OnUserSubscribed: channel=\"{0}\" userId=\"{1}\"", new object[]
		{
			channel,
			user
		});
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x0000B963 File Offset: 0x00009B63
	public void OnUserUnsubscribed(string channel, string user)
	{
		Debug.LogFormat("OnUserUnsubscribed: channel=\"{0}\" userId=\"{1}\"", new object[]
		{
			channel,
			user
		});
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000B980 File Offset: 0x00009B80
	public void AddMessageToSelectedChannel(string msg)
	{
		ChatChannel chatChannel = null;
		if (!this.chatClient.TryGetChannel(this.selectedChannelName, out chatChannel))
		{
			Debug.Log("AddMessageToSelectedChannel failed to find channel: " + this.selectedChannelName);
			return;
		}
		if (chatChannel != null)
		{
			chatChannel.Add("Bot", msg, chatChannel.LastMsgId);
		}
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000B9D0 File Offset: 0x00009BD0
	public void ShowChannel(string channelName)
	{
		if (string.IsNullOrEmpty(channelName))
		{
			return;
		}
		ChatChannel chatChannel = null;
		if (!this.chatClient.TryGetChannel(channelName, out chatChannel))
		{
			Debug.Log("ShowChannel failed to find channel: " + channelName);
			return;
		}
		this.selectedChannelName = channelName;
		this.CurrentChannelText.text = chatChannel.ToStringMessages();
		Debug.Log("ShowChannel: " + this.selectedChannelName);
		foreach (KeyValuePair<string, Toggle> keyValuePair in this.channelToggles)
		{
			keyValuePair.Value.isOn = (keyValuePair.Key == channelName);
		}
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0000BA94 File Offset: 0x00009C94
	public void OpenDashboard()
	{
		Application.OpenURL("https://www.photonengine.com/en/Dashboard/Chat");
	}

	// Token: 0x040001C3 RID: 451
	public string[] ChannelsToJoinOnConnect;

	// Token: 0x040001C4 RID: 452
	public string[] FriendsList;

	// Token: 0x040001C5 RID: 453
	public int HistoryLengthToFetch;

	// Token: 0x040001C7 RID: 455
	private string selectedChannelName;

	// Token: 0x040001C8 RID: 456
	public ChatClient chatClient;

	// Token: 0x040001C9 RID: 457
	public GameObject missingAppIdErrorPanel;

	// Token: 0x040001CA RID: 458
	public GameObject ConnectingLabel;

	// Token: 0x040001CB RID: 459
	public RectTransform ChatPanel;

	// Token: 0x040001CC RID: 460
	public GameObject UserIdFormPanel;

	// Token: 0x040001CD RID: 461
	public InputField InputFieldChat;

	// Token: 0x040001CE RID: 462
	public Text CurrentChannelText;

	// Token: 0x040001CF RID: 463
	public Toggle ChannelToggleToInstantiate;

	// Token: 0x040001D0 RID: 464
	public GameObject FriendListUiItemtoInstantiate;

	// Token: 0x040001D1 RID: 465
	private readonly Dictionary<string, Toggle> channelToggles = new Dictionary<string, Toggle>();

	// Token: 0x040001D2 RID: 466
	private readonly Dictionary<string, FriendItem> friendListItemLUT = new Dictionary<string, FriendItem>();

	// Token: 0x040001D3 RID: 467
	public bool ShowState = true;

	// Token: 0x040001D4 RID: 468
	public GameObject Title;

	// Token: 0x040001D5 RID: 469
	public Text StateText;

	// Token: 0x040001D6 RID: 470
	public Text UserIdText;

	// Token: 0x040001D7 RID: 471
	private static string HelpText = "\n    -- HELP --\nTo subscribe to channel(s) (channelNames are case sensitive):\n\t<color=#E07B00>\\subscribe</color> <color=green><list of channelnames></color>\n\tor\n\t<color=#E07B00>\\s</color> <color=green><list of channelnames></color>\n\nTo leave channel(s):\n\t<color=#E07B00>\\unsubscribe</color> <color=green><list of channelnames></color>\n\tor\n\t<color=#E07B00>\\u</color> <color=green><list of channelnames></color>\n\nTo switch the active channel\n\t<color=#E07B00>\\join</color> <color=green><channelname></color>\n\tor\n\t<color=#E07B00>\\j</color> <color=green><channelname></color>\n\nTo send a private message (username are case sensitive):\n\t\\<color=#E07B00>msg</color> <color=green><username></color> <color=green><message></color>\n\nTo change status:\n\t\\<color=#E07B00>state</color> <color=green><stateIndex></color> <color=green><message></color>\n<color=green>0</color> = Offline <color=green>1</color> = Invisible <color=green>2</color> = Online <color=green>3</color> = Away \n<color=green>4</color> = Do not disturb <color=green>5</color> = Looking For Group <color=green>6</color> = Playing\n\nTo clear the current chat tab (private chats get closed):\n\t<color=#E07B00>\\clear</color>";

	// Token: 0x040001D8 RID: 472
	public int TestLength = 2048;

	// Token: 0x040001D9 RID: 473
	private byte[] testBytes = new byte[2048];
}
