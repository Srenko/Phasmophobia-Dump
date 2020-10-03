using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020001C7 RID: 455
public class HeartRateData : MonoBehaviour
{
	// Token: 0x06000C71 RID: 3185 RVA: 0x0004EDED File Offset: 0x0004CFED
	private void Start()
	{
		this.SetupUI();
		GameController.instance.OnPlayerSpawned.AddListener(new UnityAction(this.SetupUI));
		GameController.instance.OnPlayerDisconnected.AddListener(new UnityAction(this.SetupUI));
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x0004EE2C File Offset: 0x0004D02C
	private void SetupUI()
	{
		this.player1HRObject.SetActive(false);
		this.player2HRObject.SetActive(false);
		this.player3HRObject.SetActive(false);
		this.player4HRObject.SetActive(false);
		if (PhotonNetwork.playerList.Length != 0)
		{
			this.player1HRObject.SetActive(true);
		}
		if (PhotonNetwork.playerList.Length > 1)
		{
			this.player2HRObject.SetActive(true);
		}
		if (PhotonNetwork.playerList.Length > 2)
		{
			this.player3HRObject.SetActive(true);
		}
		if (PhotonNetwork.playerList.Length > 3)
		{
			this.player4HRObject.SetActive(true);
		}
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x0004EEC0 File Offset: 0x0004D0C0
	private void UpdateUI()
	{
		if (GameController.instance.playersData.Count > 0)
		{
			this.player1NameText.text = GameController.instance.playersData[0].playerName;
			this.player1HeartRateText.text = (GameController.instance.playersData[0].player.isDead ? "?" : (Mathf.Clamp(100f - GameController.instance.playersData[0].player.insanity + Random.Range(-2f, 3f), 0f, 100f).ToString("0") + "%"));
		}
		if (GameController.instance.playersData.Count > 1)
		{
			this.player2NameText.text = GameController.instance.playersData[1].playerName;
			this.player2HeartRateText.text = (GameController.instance.playersData[1].player.isDead ? "?" : (Mathf.Clamp(100f - GameController.instance.playersData[1].player.insanity + Random.Range(-2f, 3f), 0f, 100f).ToString("0") + "%"));
		}
		if (GameController.instance.playersData.Count > 2)
		{
			this.player3NameText.text = GameController.instance.playersData[2].playerName;
			this.player3HeartRateText.text = (GameController.instance.playersData[2].player.isDead ? "?" : (Mathf.Clamp(100f - GameController.instance.playersData[2].player.insanity + Random.Range(-2f, 3f), 0f, 100f).ToString("0") + "%"));
		}
		if (GameController.instance.playersData.Count > 3)
		{
			this.player4NameText.text = GameController.instance.playersData[3].playerName;
			this.player4HeartRateText.text = (GameController.instance.playersData[3].player.isDead ? "?" : (Mathf.Clamp(100f - GameController.instance.playersData[3].player.insanity + Random.Range(-2f, 3f), 0f, 100f).ToString("0") + "%"));
		}
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x0004F1B1 File Offset: 0x0004D3B1
	private void Update()
	{
		this.updateTimer -= Time.deltaTime;
		if (this.updateTimer < 0f)
		{
			this.updateTimer = 2f;
			this.UpdateUI();
		}
	}

	// Token: 0x04000CF2 RID: 3314
	[SerializeField]
	private GameObject player1HRObject;

	// Token: 0x04000CF3 RID: 3315
	[SerializeField]
	private GameObject player2HRObject;

	// Token: 0x04000CF4 RID: 3316
	[SerializeField]
	private GameObject player3HRObject;

	// Token: 0x04000CF5 RID: 3317
	[SerializeField]
	private GameObject player4HRObject;

	// Token: 0x04000CF6 RID: 3318
	[SerializeField]
	private Text player1NameText;

	// Token: 0x04000CF7 RID: 3319
	[SerializeField]
	private Text player2NameText;

	// Token: 0x04000CF8 RID: 3320
	[SerializeField]
	private Text player3NameText;

	// Token: 0x04000CF9 RID: 3321
	[SerializeField]
	private Text player4NameText;

	// Token: 0x04000CFA RID: 3322
	[SerializeField]
	private Text player1HeartRateText;

	// Token: 0x04000CFB RID: 3323
	[SerializeField]
	private Text player2HeartRateText;

	// Token: 0x04000CFC RID: 3324
	[SerializeField]
	private Text player3HeartRateText;

	// Token: 0x04000CFD RID: 3325
	[SerializeField]
	private Text player4HeartRateText;

	// Token: 0x04000CFE RID: 3326
	private float updateTimer = 1f;
}
