using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000125 RID: 293
[RequireComponent(typeof(PhotonView))]
public class SetupPhaseController : MonoBehaviour
{
	// Token: 0x060007E1 RID: 2017 RVA: 0x0002F64D File Offset: 0x0002D84D
	private void Awake()
	{
		SetupPhaseController.instance = this;
		this.view = base.GetComponent<PhotonView>();
		this.isSetupPhase = true;
		this.clockText.text = "5 : 00";
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x0002F678 File Offset: 0x0002D878
	private void Start()
	{
		if (GameController.instance.levelDifficulty == Contract.LevelDifficulty.Intermediate)
		{
			this.setupPhaseTimerMinutes = 1;
			this.clockText.text = "2 : 00";
			return;
		}
		if (GameController.instance.levelDifficulty == Contract.LevelDifficulty.Professional)
		{
			this.setupPhaseTimerSeconds = 0f;
			this.setupPhaseTimerMinutes = 0;
			this.clockText.text = "0 : 00";
		}
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x0002F6DC File Offset: 0x0002D8DC
	private void Update()
	{
		if (this.isSetupPhase && this.mainDoorHasUnlocked && GameController.instance.allPlayersAreConnected)
		{
			this.setupPhaseTimerSeconds -= Time.deltaTime;
			if (this.setupPhaseTimerMinutes < 0)
			{
				this.BeginHuntingPhase();
				return;
			}
			if (!this.clockAudio.isPlaying)
			{
				this.clockAudio.Play();
			}
			if (this.setupPhaseTimerSeconds < 0f)
			{
				this.setupPhaseTimerSeconds = 59f;
				this.setupPhaseTimerMinutes--;
			}
			this.clockText.text = "0" + this.setupPhaseTimerMinutes.ToString() + " : " + this.setupPhaseTimerSeconds.ToString("00");
		}
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x0002F7A3 File Offset: 0x0002D9A3
	public void ForceEnterHuntingPhase()
	{
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("ForceEnterHuntingPhaseNetworked", PhotonTargets.All, Array.Empty<object>());
			return;
		}
		this.ForceEnterHuntingPhaseNetworked();
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x0002F7C9 File Offset: 0x0002D9C9
	[PunRPC]
	private void ForceEnterHuntingPhaseNetworked()
	{
		this.setupPhaseTimerMinutes = -1;
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x0002F7D2 File Offset: 0x0002D9D2
	public void BeginHuntingPhase()
	{
		this.isSetupPhase = false;
		this.clockText.text = "00 : 00";
		this.clockAudio.Stop();
		FileBasedPrefs.SetInt("setupPhase", 1);
	}

	// Token: 0x040007A9 RID: 1961
	public static SetupPhaseController instance;

	// Token: 0x040007AA RID: 1962
	private PhotonView view;

	// Token: 0x040007AB RID: 1963
	[HideInInspector]
	public bool isSetupPhase = true;

	// Token: 0x040007AC RID: 1964
	[SerializeField]
	private Text clockText;

	// Token: 0x040007AD RID: 1965
	[SerializeField]
	private AudioSource clockAudio;

	// Token: 0x040007AE RID: 1966
	[SerializeField]
	private float setupPhaseTimerSeconds = 59f;

	// Token: 0x040007AF RID: 1967
	[SerializeField]
	private int setupPhaseTimerMinutes = 4;

	// Token: 0x040007B0 RID: 1968
	[HideInInspector]
	public bool mainDoorHasUnlocked;

	// Token: 0x040007B1 RID: 1969
	[HideInInspector]
	public bool allPlayersAreLoadedIn;
}
