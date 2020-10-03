using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x02000121 RID: 289
public class PauseMenuController : MonoBehaviour
{
	// Token: 0x060007C3 RID: 1987 RVA: 0x0002DECF File Offset: 0x0002C0CF
	private void Awake()
	{
		PauseMenuController.instance = this;
		if (XRDevice.isPresent)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0002DEEC File Offset: 0x0002C0EC
	private void Start()
	{
		if (!PlayerPrefs.HasKey("player1Volume"))
		{
			PlayerPrefs.SetFloat("player1Volume", 1f);
		}
		if (!PlayerPrefs.HasKey("player2Volume"))
		{
			PlayerPrefs.SetFloat("player2Volume", 1f);
		}
		if (!PlayerPrefs.HasKey("player2Volume"))
		{
			PlayerPrefs.SetFloat("player3Volume", 1f);
		}
		if (PhotonNetwork.otherPlayers.Length != 0)
		{
			this.player2Text.text = PhotonNetwork.otherPlayers[0].NickName;
			this.player2Object.SetActive(true);
			this.player2ActorID = PhotonNetwork.otherPlayers[0].ID;
			this.player2Slider.value = this.GetSavedPlayerVolume(PhotonNetwork.otherPlayers[0].NickName);
		}
		if (PhotonNetwork.otherPlayers.Length > 1)
		{
			this.player3Text.text = PhotonNetwork.otherPlayers[1].NickName;
			this.player3Object.SetActive(true);
			this.player3ActorID = PhotonNetwork.otherPlayers[1].ID;
			this.player3Slider.value = this.GetSavedPlayerVolume(PhotonNetwork.otherPlayers[1].NickName);
		}
		if (PhotonNetwork.otherPlayers.Length > 2)
		{
			this.player4Text.text = PhotonNetwork.otherPlayers[2].NickName;
			this.player4Object.SetActive(true);
			this.player4ActorID = PhotonNetwork.otherPlayers[2].ID;
			this.player4Slider.value = this.GetSavedPlayerVolume(PhotonNetwork.otherPlayers[2].NickName);
		}
		if (PhotonNetwork.otherPlayers.Length != 0)
		{
			PlayerPrefs.SetString("player1Name", PhotonNetwork.otherPlayers[0].NickName);
		}
		if (PhotonNetwork.otherPlayers.Length > 1)
		{
			PlayerPrefs.SetString("player2Name", PhotonNetwork.otherPlayers[1].NickName);
		}
		if (PhotonNetwork.otherPlayers.Length > 2)
		{
			PlayerPrefs.SetString("player3Name", PhotonNetwork.otherPlayers[2].NickName);
		}
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x0002E0BC File Offset: 0x0002C2BC
	public void Player2SliderValueChanged()
	{
		this.player2ValueText.text = (this.player2Slider.value * 100f).ToString("0") + "%";
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i].actorID == this.player2ActorID)
			{
				GameController.instance.playersData[i].player.voiceVolume.ApplyVoiceVolume(this.player2Slider.value);
			}
		}
		this.SetPlayerVolumes();
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x0002E164 File Offset: 0x0002C364
	public void Player3SliderValueChanged()
	{
		this.player3ValueText.text = (this.player3Slider.value * 100f).ToString("0") + "%";
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i].actorID == this.player3ActorID)
			{
				GameController.instance.playersData[i].player.voiceVolume.ApplyVoiceVolume(this.player3Slider.value);
			}
		}
		this.SetPlayerVolumes();
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x0002E20C File Offset: 0x0002C40C
	public void Player4SliderValueChanged()
	{
		this.player4ValueText.text = (this.player4Slider.value * 100f).ToString("0") + "%";
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i].actorID == this.player4ActorID)
			{
				GameController.instance.playersData[i].player.voiceVolume.ApplyVoiceVolume(this.player4Slider.value);
			}
		}
		this.SetPlayerVolumes();
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x0002E2B4 File Offset: 0x0002C4B4
	private void SetPlayerVolumes()
	{
		if (this._isPaused)
		{
			PlayerPrefs.SetFloat("player1Volume", this.player2Slider.value);
			PlayerPrefs.SetFloat("player2Volume", this.player3Slider.value);
			PlayerPrefs.SetFloat("player3Volume", this.player4Slider.value);
		}
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x0002E308 File Offset: 0x0002C508
	private float GetSavedPlayerVolume(string playerName)
	{
		if (playerName == PlayerPrefs.GetString("player1Name") && PlayerPrefs.GetFloat("player1Volume") != 0f)
		{
			return PlayerPrefs.GetFloat("player1Volume");
		}
		if (playerName == PlayerPrefs.GetString("player2Name") && PlayerPrefs.GetFloat("player2Volume") != 0f)
		{
			return PlayerPrefs.GetFloat("player2Volume");
		}
		if (playerName == PlayerPrefs.GetString("player3Name") && PlayerPrefs.GetFloat("player3Volume") != 0f)
		{
			return PlayerPrefs.GetFloat("player3Volume");
		}
		return 1f;
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x0002E3A4 File Offset: 0x0002C5A4
	public void Pause(bool isPaused)
	{
		this._isPaused = isPaused;
		this.content.SetActive(isPaused);
		this.areYouSure.SetActive(false);
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
	public float GetPlayerVolume(int actorID)
	{
		if (actorID == this.player2ActorID)
		{
			return this.player2Slider.value;
		}
		if (actorID == this.player3ActorID)
		{
			return this.player3Slider.value;
		}
		if (actorID == this.player4ActorID)
		{
			return this.player4Slider.value;
		}
		return 1f;
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x0002E419 File Offset: 0x0002C619
	public void Resume()
	{
		GameController.instance.myPlayer.player.pcCanvas.Pause();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x0002E440 File Offset: 0x0002C640
	public void Leave()
	{
		if (GameController.instance)
		{
			if (GameController.instance.isTutorial)
			{
				FileBasedPrefs.SetInt("MissionStatus", 3);
				FileBasedPrefs.SetInt("setupPhase", 0);
				FileBasedPrefs.SetInt("completedTraining", 1);
				FileBasedPrefs.SetInt("StayInServerRoom", 0);
			}
			if (GameController.instance.myPlayer.player.isDead)
			{
				FileBasedPrefs.SetInt("PlayerDied", 1);
				InventoryManager.RemoveItemsFromInventory();
			}
			else
			{
				FileBasedPrefs.SetInt("PlayerDied", 0);
			}
		}
		SceneManager.LoadScene("Menu_New");
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x0002E4D0 File Offset: 0x0002C6D0
	public void Quit()
	{
		if (GameController.instance)
		{
			if (GameController.instance.isTutorial)
			{
				FileBasedPrefs.SetInt("MissionStatus", 3);
				FileBasedPrefs.SetInt("setupPhase", 0);
				FileBasedPrefs.SetInt("completedTraining", 1);
				FileBasedPrefs.SetInt("StayInServerRoom", 0);
			}
			if (GameController.instance.myPlayer.player.isDead)
			{
				FileBasedPrefs.SetInt("PlayerDied", 1);
				InventoryManager.RemoveItemsFromInventory();
			}
			else
			{
				FileBasedPrefs.SetInt("PlayerDied", 0);
			}
		}
		Application.Quit();
	}

	// Token: 0x0400078A RID: 1930
	public static PauseMenuController instance;

	// Token: 0x0400078B RID: 1931
	[SerializeField]
	private GameObject content;

	// Token: 0x0400078C RID: 1932
	[SerializeField]
	private GameObject areYouSure;

	// Token: 0x0400078D RID: 1933
	[SerializeField]
	private GameObject player2Object;

	// Token: 0x0400078E RID: 1934
	[SerializeField]
	private Text player2Text;

	// Token: 0x0400078F RID: 1935
	[SerializeField]
	private Text player2ValueText;

	// Token: 0x04000790 RID: 1936
	[SerializeField]
	private Slider player2Slider;

	// Token: 0x04000791 RID: 1937
	private int player2ActorID = 999;

	// Token: 0x04000792 RID: 1938
	[SerializeField]
	private GameObject player3Object;

	// Token: 0x04000793 RID: 1939
	[SerializeField]
	private Text player3Text;

	// Token: 0x04000794 RID: 1940
	[SerializeField]
	private Text player3ValueText;

	// Token: 0x04000795 RID: 1941
	[SerializeField]
	private Slider player3Slider;

	// Token: 0x04000796 RID: 1942
	private int player3ActorID = 999;

	// Token: 0x04000797 RID: 1943
	[SerializeField]
	private GameObject player4Object;

	// Token: 0x04000798 RID: 1944
	[SerializeField]
	private Text player4Text;

	// Token: 0x04000799 RID: 1945
	[SerializeField]
	private Text player4ValueText;

	// Token: 0x0400079A RID: 1946
	[SerializeField]
	private Slider player4Slider;

	// Token: 0x0400079B RID: 1947
	private int player4ActorID = 999;

	// Token: 0x0400079C RID: 1948
	private bool _isPaused;
}
