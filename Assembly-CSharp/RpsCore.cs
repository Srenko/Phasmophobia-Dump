using System;
using System.Collections;
using Photon;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000065 RID: 101
public class RpsCore : PunBehaviour, IPunTurnManagerCallbacks
{
	// Token: 0x06000220 RID: 544 RVA: 0x0000EC68 File Offset: 0x0000CE68
	public void Start()
	{
		this.turnManager = base.gameObject.AddComponent<PunTurnManager>();
		this.turnManager.TurnManagerListener = this;
		this.turnManager.TurnDuration = 5f;
		this.localSelectionImage.gameObject.SetActive(false);
		this.remoteSelectionImage.gameObject.SetActive(false);
		base.StartCoroutine("CycleRemoteHandCoroutine");
		this.RefreshUIViews();
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000ECD8 File Offset: 0x0000CED8
	public void Update()
	{
		if (this.DisconnectedPanel == null)
		{
			Object.Destroy(base.gameObject);
		}
		if (Input.GetKeyUp(KeyCode.L))
		{
			PhotonNetwork.LeaveRoom(true);
		}
		if (Input.GetKeyUp(KeyCode.C))
		{
			PhotonNetwork.ConnectUsingSettings(null);
			PhotonHandler.StopFallbackSendAckThread();
		}
		if (!PhotonNetwork.inRoom)
		{
			return;
		}
		if (PhotonNetwork.connected && this.DisconnectedPanel.gameObject.GetActive())
		{
			this.DisconnectedPanel.gameObject.SetActive(false);
		}
		if (!PhotonNetwork.connected && !PhotonNetwork.connecting && !this.DisconnectedPanel.gameObject.GetActive())
		{
			this.DisconnectedPanel.gameObject.SetActive(true);
		}
		if (PhotonNetwork.room.PlayerCount > 1)
		{
			if (this.turnManager.IsOver)
			{
				return;
			}
			if (this.TurnText != null)
			{
				this.TurnText.text = this.turnManager.Turn.ToString();
			}
			if (this.turnManager.Turn > 0 && this.TimeText != null && !this.IsShowingResults)
			{
				this.TimeText.text = this.turnManager.RemainingSecondsInTurn.ToString("F1") + " SECONDS";
				this.TimerFillImage.anchorMax = new Vector2(1f - this.turnManager.RemainingSecondsInTurn / this.turnManager.TurnDuration, 1f);
			}
		}
		this.UpdatePlayerTexts();
		Sprite sprite = this.SelectionToSprite(this.localSelection);
		if (sprite != null)
		{
			this.localSelectionImage.gameObject.SetActive(true);
			this.localSelectionImage.sprite = sprite;
		}
		if (this.turnManager.IsCompletedByAll)
		{
			sprite = this.SelectionToSprite(this.remoteSelection);
			if (sprite != null)
			{
				this.remoteSelectionImage.color = new Color(1f, 1f, 1f, 1f);
				this.remoteSelectionImage.sprite = sprite;
				return;
			}
		}
		else
		{
			this.ButtonCanvasGroup.interactable = (PhotonNetwork.room.PlayerCount > 1);
			if (PhotonNetwork.room.PlayerCount < 2)
			{
				this.remoteSelectionImage.color = new Color(1f, 1f, 1f, 0f);
				return;
			}
			if (this.turnManager.Turn > 0 && !this.turnManager.IsCompletedByAll)
			{
				PhotonPlayer next = PhotonNetwork.player.GetNext();
				float a = 0.5f;
				if (this.turnManager.GetPlayerFinishedTurn(next))
				{
					a = 1f;
				}
				if (next != null && next.IsInactive)
				{
					a = 0.1f;
				}
				this.remoteSelectionImage.color = new Color(1f, 1f, 1f, a);
				this.remoteSelectionImage.sprite = this.SelectionToSprite(this.randomHand);
			}
		}
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000EFBC File Offset: 0x0000D1BC
	public void OnTurnBegins(int turn)
	{
		Debug.Log("OnTurnBegins() turn: " + turn);
		this.localSelection = RpsCore.Hand.None;
		this.remoteSelection = RpsCore.Hand.None;
		this.WinOrLossImage.gameObject.SetActive(false);
		this.localSelectionImage.gameObject.SetActive(false);
		this.remoteSelectionImage.gameObject.SetActive(true);
		this.IsShowingResults = false;
		this.ButtonCanvasGroup.interactable = true;
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000F032 File Offset: 0x0000D232
	public void OnTurnCompleted(int obj)
	{
		Debug.Log("OnTurnCompleted: " + obj);
		this.CalculateWinAndLoss();
		this.UpdateScores();
		this.OnEndTurn();
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000F05B File Offset: 0x0000D25B
	public void OnPlayerMove(PhotonPlayer photonPlayer, int turn, object move)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnPlayerMove: ",
			photonPlayer,
			" turn: ",
			turn,
			" action: ",
			move
		}));
		throw new NotImplementedException();
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000F09C File Offset: 0x0000D29C
	public void OnPlayerFinished(PhotonPlayer photonPlayer, int turn, object move)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnTurnFinished: ",
			photonPlayer,
			" turn: ",
			turn,
			" action: ",
			move
		}));
		if (photonPlayer.IsLocal)
		{
			this.localSelection = (RpsCore.Hand)((byte)move);
			return;
		}
		this.remoteSelection = (RpsCore.Hand)((byte)move);
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0000F103 File Offset: 0x0000D303
	public void OnTurnTimeEnds(int obj)
	{
		if (!this.IsShowingResults)
		{
			Debug.Log("OnTurnTimeEnds: Calling OnTurnCompleted");
			this.OnTurnCompleted(-1);
		}
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0000F11E File Offset: 0x0000D31E
	private void UpdateScores()
	{
		if (this.result == RpsCore.ResultType.LocalWin)
		{
			PhotonNetwork.player.AddScore(1);
		}
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000F134 File Offset: 0x0000D334
	public void StartTurn()
	{
		if (PhotonNetwork.isMasterClient)
		{
			this.turnManager.BeginTurn();
		}
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0000F148 File Offset: 0x0000D348
	public void MakeTurn(RpsCore.Hand selection)
	{
		this.turnManager.SendMove((byte)selection, true);
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0000F15D File Offset: 0x0000D35D
	public void OnEndTurn()
	{
		base.StartCoroutine("ShowResultsBeginNextTurnCoroutine");
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0000F16B File Offset: 0x0000D36B
	public IEnumerator ShowResultsBeginNextTurnCoroutine()
	{
		this.ButtonCanvasGroup.interactable = false;
		this.IsShowingResults = true;
		if (this.result == RpsCore.ResultType.Draw)
		{
			this.WinOrLossImage.sprite = this.SpriteDraw;
		}
		else
		{
			this.WinOrLossImage.sprite = ((this.result == RpsCore.ResultType.LocalWin) ? this.SpriteWin : this.SpriteLose);
		}
		this.WinOrLossImage.gameObject.SetActive(true);
		yield return new WaitForSeconds(2f);
		this.StartTurn();
		yield break;
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000F17A File Offset: 0x0000D37A
	public void EndGame()
	{
		Debug.Log("EndGame");
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0000F188 File Offset: 0x0000D388
	private void CalculateWinAndLoss()
	{
		this.result = RpsCore.ResultType.Draw;
		if (this.localSelection == this.remoteSelection)
		{
			return;
		}
		if (this.localSelection == RpsCore.Hand.None)
		{
			this.result = RpsCore.ResultType.LocalLoss;
			return;
		}
		if (this.remoteSelection == RpsCore.Hand.None)
		{
			this.result = RpsCore.ResultType.LocalWin;
		}
		if (this.localSelection == RpsCore.Hand.Rock)
		{
			this.result = ((this.remoteSelection == RpsCore.Hand.Scissors) ? RpsCore.ResultType.LocalWin : RpsCore.ResultType.LocalLoss);
		}
		if (this.localSelection == RpsCore.Hand.Paper)
		{
			this.result = ((this.remoteSelection == RpsCore.Hand.Rock) ? RpsCore.ResultType.LocalWin : RpsCore.ResultType.LocalLoss);
		}
		if (this.localSelection == RpsCore.Hand.Scissors)
		{
			this.result = ((this.remoteSelection == RpsCore.Hand.Paper) ? RpsCore.ResultType.LocalWin : RpsCore.ResultType.LocalLoss);
		}
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0000F21E File Offset: 0x0000D41E
	private Sprite SelectionToSprite(RpsCore.Hand hand)
	{
		switch (hand)
		{
		case RpsCore.Hand.Rock:
			return this.SelectedRock;
		case RpsCore.Hand.Paper:
			return this.SelectedPaper;
		case RpsCore.Hand.Scissors:
			return this.SelectedScissors;
		}
		return null;
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0000F250 File Offset: 0x0000D450
	private void UpdatePlayerTexts()
	{
		PhotonPlayer next = PhotonNetwork.player.GetNext();
		PhotonPlayer player = PhotonNetwork.player;
		if (next != null)
		{
			this.RemotePlayerText.text = next.NickName + "        " + next.GetScore().ToString("D2");
		}
		else
		{
			this.TimerFillImage.anchorMax = new Vector2(0f, 1f);
			this.TimeText.text = "";
			this.RemotePlayerText.text = "waiting for another player        00";
		}
		if (player != null)
		{
			this.LocalPlayerText.text = "YOU   " + player.GetScore().ToString("D2");
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0000F306 File Offset: 0x0000D506
	public IEnumerator CycleRemoteHandCoroutine()
	{
		for (;;)
		{
			this.randomHand = (RpsCore.Hand)Random.Range(1, 4);
			yield return new WaitForSeconds(0.5f);
		}
		yield break;
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0000F315 File Offset: 0x0000D515
	public void OnClickRock()
	{
		this.MakeTurn(RpsCore.Hand.Rock);
	}

	// Token: 0x06000232 RID: 562 RVA: 0x0000F31E File Offset: 0x0000D51E
	public void OnClickPaper()
	{
		this.MakeTurn(RpsCore.Hand.Paper);
	}

	// Token: 0x06000233 RID: 563 RVA: 0x0000F327 File Offset: 0x0000D527
	public void OnClickScissors()
	{
		this.MakeTurn(RpsCore.Hand.Scissors);
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0000F330 File Offset: 0x0000D530
	public void OnClickConnect()
	{
		PhotonNetwork.ConnectUsingSettings(null);
		PhotonHandler.StopFallbackSendAckThread();
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0000F33E File Offset: 0x0000D53E
	public void OnClickReConnectAndRejoin()
	{
		PhotonNetwork.ReconnectAndRejoin();
		PhotonHandler.StopFallbackSendAckThread();
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000F34C File Offset: 0x0000D54C
	private void RefreshUIViews()
	{
		this.TimerFillImage.anchorMax = new Vector2(0f, 1f);
		this.ConnectUiView.gameObject.SetActive(!PhotonNetwork.inRoom);
		this.GameUiView.gameObject.SetActive(PhotonNetwork.inRoom);
		this.ButtonCanvasGroup.interactable = (PhotonNetwork.room != null && PhotonNetwork.room.PlayerCount > 1);
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000F3C2 File Offset: 0x0000D5C2
	public override void OnLeftRoom()
	{
		Debug.Log("OnLeftRoom()");
		this.RefreshUIViews();
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
	public override void OnJoinedRoom()
	{
		this.RefreshUIViews();
		if (PhotonNetwork.room.PlayerCount == 2)
		{
			if (this.turnManager.Turn == 0)
			{
				this.StartTurn();
				return;
			}
		}
		else
		{
			Debug.Log("Waiting for another player");
		}
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000F407 File Offset: 0x0000D607
	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		Debug.Log("Other player arrived");
		if (PhotonNetwork.room.PlayerCount == 2 && this.turnManager.Turn == 0)
		{
			this.StartTurn();
		}
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000F433 File Offset: 0x0000D633
	public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{
		Debug.Log("Other player disconnected! " + otherPlayer.ToStringFull());
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000F44A File Offset: 0x0000D64A
	public override void OnConnectionFail(DisconnectCause cause)
	{
		this.DisconnectedPanel.gameObject.SetActive(true);
	}

	// Token: 0x04000261 RID: 609
	[SerializeField]
	private RectTransform ConnectUiView;

	// Token: 0x04000262 RID: 610
	[SerializeField]
	private RectTransform GameUiView;

	// Token: 0x04000263 RID: 611
	[SerializeField]
	private CanvasGroup ButtonCanvasGroup;

	// Token: 0x04000264 RID: 612
	[SerializeField]
	private RectTransform TimerFillImage;

	// Token: 0x04000265 RID: 613
	[SerializeField]
	private Text TurnText;

	// Token: 0x04000266 RID: 614
	[SerializeField]
	private Text TimeText;

	// Token: 0x04000267 RID: 615
	[SerializeField]
	private Text RemotePlayerText;

	// Token: 0x04000268 RID: 616
	[SerializeField]
	private Text LocalPlayerText;

	// Token: 0x04000269 RID: 617
	[SerializeField]
	private Image WinOrLossImage;

	// Token: 0x0400026A RID: 618
	[SerializeField]
	private Image localSelectionImage;

	// Token: 0x0400026B RID: 619
	public RpsCore.Hand localSelection;

	// Token: 0x0400026C RID: 620
	[SerializeField]
	private Image remoteSelectionImage;

	// Token: 0x0400026D RID: 621
	public RpsCore.Hand remoteSelection;

	// Token: 0x0400026E RID: 622
	[SerializeField]
	private Sprite SelectedRock;

	// Token: 0x0400026F RID: 623
	[SerializeField]
	private Sprite SelectedPaper;

	// Token: 0x04000270 RID: 624
	[SerializeField]
	private Sprite SelectedScissors;

	// Token: 0x04000271 RID: 625
	[SerializeField]
	private Sprite SpriteWin;

	// Token: 0x04000272 RID: 626
	[SerializeField]
	private Sprite SpriteLose;

	// Token: 0x04000273 RID: 627
	[SerializeField]
	private Sprite SpriteDraw;

	// Token: 0x04000274 RID: 628
	[SerializeField]
	private RectTransform DisconnectedPanel;

	// Token: 0x04000275 RID: 629
	private RpsCore.ResultType result;

	// Token: 0x04000276 RID: 630
	private PunTurnManager turnManager;

	// Token: 0x04000277 RID: 631
	public RpsCore.Hand randomHand;

	// Token: 0x04000278 RID: 632
	private bool IsShowingResults;

	// Token: 0x020004E3 RID: 1251
	public enum Hand
	{
		// Token: 0x040023A9 RID: 9129
		None,
		// Token: 0x040023AA RID: 9130
		Rock,
		// Token: 0x040023AB RID: 9131
		Paper,
		// Token: 0x040023AC RID: 9132
		Scissors
	}

	// Token: 0x020004E4 RID: 1252
	public enum ResultType
	{
		// Token: 0x040023AE RID: 9134
		None,
		// Token: 0x040023AF RID: 9135
		Draw,
		// Token: 0x040023B0 RID: 9136
		LocalWin,
		// Token: 0x040023B1 RID: 9137
		LocalLoss
	}
}
