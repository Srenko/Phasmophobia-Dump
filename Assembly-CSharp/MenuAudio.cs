using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class MenuAudio : MonoBehaviour
{
	// Token: 0x06000731 RID: 1841 RVA: 0x0002A072 File Offset: 0x00028272
	private void Awake()
	{
		MenuAudio.instance = this;
		this.source = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0002A086 File Offset: 0x00028286
	private void Start()
	{
		base.StartCoroutine(this.PlayIntroAudio());
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0002A095 File Offset: 0x00028295
	private IEnumerator PlayIntroAudio()
	{
		yield return new WaitForSeconds(2f);
		if (PlayerPrefs.GetInt("FirstSession") == 0)
		{
			this.source.clip = this.firstSessionClip;
			this.source.Play();
			PlayerPrefs.SetInt("FirstSession", 1);
		}
		else
		{
			this.source.clip = this.welcomeBackClips[Random.Range(0, this.welcomeBackClips.Length)];
			this.source.Play();
		}
		yield break;
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0002A0A4 File Offset: 0x000282A4
	public void LobbyScreen(int roomsAmount)
	{
		if (!PhotonNetwork.insideLobby)
		{
			return;
		}
		if (roomsAmount > 0)
		{
			this.source.clip = this.lobbyFoundPlayersClips[Random.Range(0, this.lobbyFoundPlayersClips.Length)];
			this.source.Play();
			return;
		}
		this.source.clip = this.lobbyNoPlayersClips[Random.Range(0, this.lobbyNoPlayersClips.Length)];
		this.source.Play();
	}

	// Token: 0x040006D9 RID: 1753
	public static MenuAudio instance;

	// Token: 0x040006DA RID: 1754
	private AudioSource source;

	// Token: 0x040006DB RID: 1755
	[SerializeField]
	private AudioClip firstSessionClip;

	// Token: 0x040006DC RID: 1756
	[SerializeField]
	private AudioClip[] welcomeBackClips;

	// Token: 0x040006DD RID: 1757
	[SerializeField]
	private AudioClip[] lobbyNoPlayersClips;

	// Token: 0x040006DE RID: 1758
	[SerializeField]
	private AudioClip[] lobbyFoundPlayersClips;
}
