using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x02000157 RID: 343
public class EVPRecorder : MonoBehaviour
{
	// Token: 0x0600090E RID: 2318 RVA: 0x000366F3 File Offset: 0x000348F3
	private void Awake()
	{
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x00036706 File Offset: 0x00034906
	private void Start()
	{
		if (MainManager.instance)
		{
			base.gameObject.SetActive(false);
			return;
		}
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
		SpeechRecognitionController.instance.AddEVPRecorder(this);
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x00036744 File Offset: 0x00034944
	private void Update()
	{
		if (this.isOn)
		{
			this.scanTimer -= Time.deltaTime;
			if (this.scanTimer < 0f)
			{
				if (this.currentFMChannel >= 110f)
				{
					this.isAddingFM = false;
				}
				else if (this.currentFMChannel <= 85f)
				{
					this.isAddingFM = true;
				}
				if (this.isAddingFM)
				{
					this.currentFMChannel += 0.1f;
				}
				else
				{
					this.currentFMChannel -= 0.1f;
				}
				if (this.hasAnswered)
				{
					this.fmText.text = this.currentFMChannel.ToString("0.0") + "fm";
				}
				this.scanTimer = 0.1f;
			}
			if ((GameController.instance.myPlayer.player.myVoiceRecorder.IsTransmitting || PhotonNetwork.offlineMode) && Vector3.Distance(base.transform.position, GameController.instance.myPlayer.player.headObject.transform.position) < 5f)
			{
				this.responseTimer += Time.deltaTime;
				if (this.responseTimer > 10f)
				{
					this.ResponseCheck();
					this.responseTimer = 0f;
				}
			}
		}
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00036895 File Offset: 0x00034A95
	private IEnumerator FailCheck()
	{
		if (this.hasAnswered)
		{
			yield return null;
		}
		yield return new WaitForSeconds(1f);
		if (!this.hasAnswered)
		{
			this.fmText.text = LocalisationSystem.GetLocalisedValue("SpiritBox_Error");
		}
		yield return new WaitForSeconds(1f);
		this.hasAnswered = true;
		yield break;
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x000368A4 File Offset: 0x00034AA4
	private void ResponseCheck()
	{
		if (!XRDevice.isPresent && PlayerPrefs.GetInt("localPushToTalkValue") == 0 && PhotonNetwork.room.PlayerCount > 1 && !GameController.instance.myPlayer.player.pcPushToTalk.isPressingPushToTalk)
		{
			return;
		}
		if (GameController.instance.myPlayer.player.isDead)
		{
			return;
		}
		if (this.soundSource.isPlaying)
		{
			return;
		}
		this.hasAnswered = false;
		base.StartCoroutine(this.FailCheck());
		if (LevelController.instance.currentPlayerRoom == null || LevelController.instance.currentGhostRoom == null)
		{
			return;
		}
		if (LevelController.instance.currentPlayerRoom == LevelController.instance.outsideRoom)
		{
			return;
		}
		if (LevelController.instance.currentPlayerRoom.floorType != LevelController.instance.currentGhostRoom.floorType)
		{
			return;
		}
		if (LevelController.instance.currentPlayerRoom != LevelController.instance.currentGhostRoom && Vector3.Distance(base.transform.position, LevelController.instance.currentGhost.raycastPoint.transform.position) > 3f)
		{
			return;
		}
		if (LevelController.instance.currentGhostRoom.playersInRoom.Count > 1 && LevelController.instance.currentGhost.ghostInfo.ghostTraits.isShy)
		{
			return;
		}
		if (!this.IsCorrectGhostType())
		{
			return;
		}
		if (LevelController.instance.fuseBox.isOn)
		{
			for (int i = 0; i < LevelController.instance.currentPlayerRoom.lightSwitches.Count; i++)
			{
				if (LevelController.instance.currentPlayerRoom.lightSwitches[i].isOn)
				{
					return;
				}
			}
		}
		if (!SpeechRecognitionController.instance.hasSaidGhostsName && !GameController.instance.isTutorial && Random.Range(0, 3) == 1 && PlayerPrefs.GetInt("isYoutuberVersion") == 0)
		{
			return;
		}
		this.hasAnswered = true;
		int num = Random.Range(0, 3);
		if (num == 0)
		{
			this.LocationAnswer();
		}
		if (num == 1)
		{
			this.DifficultyAnswer();
			return;
		}
		this.AgeAnswer();
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x00036AB3 File Offset: 0x00034CB3
	private void Use()
	{
		this.view.RPC("NetworkUse", PhotonTargets.All, new object[]
		{
			PhotonNetwork.player.ID
		});
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x00036ADE File Offset: 0x00034CDE
	public void TurnOff()
	{
		if (this.isOn)
		{
			this.view.RPC("NetworkUse", PhotonTargets.All, new object[]
			{
				PhotonNetwork.player.ID
			});
		}
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x00036B14 File Offset: 0x00034D14
	[PunRPC]
	private void NetworkUse(int actorID)
	{
		this.isOn = !this.isOn;
		if (this.isOn)
		{
			this.loopSource.outputAudioMixerGroup = SoundController.instance.GetPlayersAudioGroup(actorID);
			this.loopSource.Play();
			this.fmText.text = this.currentFMChannel.ToString("0.0");
			this.noise.gameObject.SetActive(true);
			return;
		}
		this.noise.gameObject.SetActive(false);
		this.loopSource.Stop();
		this.fmText.text = "";
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x00036BB4 File Offset: 0x00034DB4
	private bool IsCorrectGhostType()
	{
		return LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Spirit || LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Poltergeist || LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Jinn || LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Wraith || LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Mare || LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Demon || LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Oni;
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x00036C90 File Offset: 0x00034E90
	public void SetupKeywords()
	{
		Question item = new Question
		{
			questions = new List<string>
			{
				LocalisationSystem.GetLocalisedValue("Q_What do you want"),
				LocalisationSystem.GetLocalisedValue("Q_Why are you here"),
				LocalisationSystem.GetLocalisedValue("Q_Do you want to hurt us"),
				LocalisationSystem.GetLocalisedValue("Q_Are you angry"),
				LocalisationSystem.GetLocalisedValue("Q_Do you want us here"),
				LocalisationSystem.GetLocalisedValue("Q_Shall we leave"),
				LocalisationSystem.GetLocalisedValue("Q_Should we leave"),
				LocalisationSystem.GetLocalisedValue("Q_Do you want us to leave"),
				LocalisationSystem.GetLocalisedValue("Q_What should we do"),
				LocalisationSystem.GetLocalisedValue("Q_Can we help"),
				LocalisationSystem.GetLocalisedValue("Q_Are you friendly"),
				LocalisationSystem.GetLocalisedValue("Q_What are you")
			},
			questionType = Question.QuestionType.difficulty
		};
		this.questions.Add(item);
		item = new Question
		{
			questions = new List<string>
			{
				LocalisationSystem.GetLocalisedValue("Q_Where are you"),
				LocalisationSystem.GetLocalisedValue("Q_Are you close"),
				LocalisationSystem.GetLocalisedValue("Q_Can you show yourself"),
				LocalisationSystem.GetLocalisedValue("Q_Give us a sign"),
				LocalisationSystem.GetLocalisedValue("Q_Let us know you are here"),
				LocalisationSystem.GetLocalisedValue("Q_Show yourself"),
				LocalisationSystem.GetLocalisedValue("Q_Can you talk"),
				LocalisationSystem.GetLocalisedValue("Q_Speak to us"),
				LocalisationSystem.GetLocalisedValue("Q_Are you here"),
				LocalisationSystem.GetLocalisedValue("Q_Are you with us"),
				LocalisationSystem.GetLocalisedValue("Q_Anybody with us"),
				LocalisationSystem.GetLocalisedValue("Q_Is anyone here"),
				LocalisationSystem.GetLocalisedValue("Q_Anybody in the room"),
				LocalisationSystem.GetLocalisedValue("Q_Anybody here"),
				LocalisationSystem.GetLocalisedValue("Q_Is there a spirit here"),
				LocalisationSystem.GetLocalisedValue("Q_Is there a Ghost here"),
				LocalisationSystem.GetLocalisedValue("Q_What is your location")
			},
			questionType = Question.QuestionType.location
		};
		this.questions.Add(item);
		item = new Question
		{
			questions = new List<string>
			{
				LocalisationSystem.GetLocalisedValue("Q_Are you a girl"),
				LocalisationSystem.GetLocalisedValue("Q_Are you a boy"),
				LocalisationSystem.GetLocalisedValue("Q_Are you male"),
				LocalisationSystem.GetLocalisedValue("Q_Are you female"),
				LocalisationSystem.GetLocalisedValue("Q_Who are you"),
				LocalisationSystem.GetLocalisedValue("Q_What are you"),
				LocalisationSystem.GetLocalisedValue("Q_Who is this"),
				LocalisationSystem.GetLocalisedValue("Q_Who are we talking to"),
				LocalisationSystem.GetLocalisedValue("Q_Who am I talking to"),
				LocalisationSystem.GetLocalisedValue("Q_Hello"),
				LocalisationSystem.GetLocalisedValue("Q_What is your name"),
				LocalisationSystem.GetLocalisedValue("Q_Can you give me your name"),
				LocalisationSystem.GetLocalisedValue("Q_What is your gender"),
				LocalisationSystem.GetLocalisedValue("Q_What gender"),
				LocalisationSystem.GetLocalisedValue("Q_Are you male or female"),
				LocalisationSystem.GetLocalisedValue("Q_Are you a man"),
				LocalisationSystem.GetLocalisedValue("Q_Are you a woman")
			},
			questionType = Question.QuestionType.gender
		};
		this.questions.Add(item);
		item = new Question
		{
			questions = new List<string>
			{
				LocalisationSystem.GetLocalisedValue("Q_How old are you"),
				LocalisationSystem.GetLocalisedValue("Q_How young are you"),
				LocalisationSystem.GetLocalisedValue("Q_What is your age"),
				LocalisationSystem.GetLocalisedValue("Q_When were you born"),
				LocalisationSystem.GetLocalisedValue("Q_Are you a child"),
				LocalisationSystem.GetLocalisedValue("Q_Are you old"),
				LocalisationSystem.GetLocalisedValue("Q_Are you young")
			},
			questionType = Question.QuestionType.age
		};
		this.questions.Add(item);
		for (int i = 0; i < this.questions.Count; i++)
		{
			for (int j = 0; j < this.questions[i].questions.Count; j++)
			{
				SpeechRecognitionController.instance.AddKeyword(this.questions[i].questions[j]);
			}
		}
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x000370F0 File Offset: 0x000352F0
	public void OnPhraseRecognized(string args)
	{
		if (!this.isOn)
		{
			return;
		}
		if (this.soundSource.isPlaying)
		{
			return;
		}
		if (!XRDevice.isPresent && PlayerPrefs.GetInt("localPushToTalkValue") == 0 && PhotonNetwork.room.PlayerCount > 1 && !GameController.instance.myPlayer.player.pcPushToTalk.isPressingPushToTalk)
		{
			return;
		}
		if (GameController.instance.myPlayer.player.isDead)
		{
			return;
		}
		this.hasAnswered = false;
		base.StartCoroutine(this.FailCheck());
		if (LevelController.instance.currentPlayerRoom == null || LevelController.instance.currentGhostRoom == null)
		{
			return;
		}
		if (LevelController.instance.currentPlayerRoom == LevelController.instance.outsideRoom)
		{
			return;
		}
		if (LevelController.instance.currentPlayerRoom.floorType != LevelController.instance.currentGhostRoom.floorType)
		{
			return;
		}
		if (LevelController.instance.currentPlayerRoom != LevelController.instance.currentGhostRoom && Vector3.Distance(base.transform.position, LevelController.instance.currentGhost.transform.position) > 3f)
		{
			return;
		}
		if (LevelController.instance.currentGhostRoom.playersInRoom.Count > 1 && LevelController.instance.currentGhost.ghostInfo.ghostTraits.isShy)
		{
			return;
		}
		if (!this.IsCorrectGhostType())
		{
			return;
		}
		if (LevelController.instance.fuseBox.isOn)
		{
			for (int i = 0; i < LevelController.instance.currentPlayerRoom.lightSwitches.Count; i++)
			{
				if (LevelController.instance.currentPlayerRoom.lightSwitches[i].isOn)
				{
					return;
				}
			}
		}
		if (!SpeechRecognitionController.instance.hasSaidGhostsName && !GameController.instance.isTutorial && Random.Range(0, 3) == 1 && PlayerPrefs.GetInt("isYoutuberVersion") == 0)
		{
			return;
		}
		this.hasAnswered = true;
		for (int j = 0; j < this.questions.Count; j++)
		{
			for (int k = 0; k < this.questions[j].questions.Count; k++)
			{
				if (args == this.questions[j].questions[k])
				{
					if (this.questions[j].questionType == Question.QuestionType.difficulty)
					{
						this.DifficultyAnswer();
						return;
					}
					if (this.questions[j].questionType == Question.QuestionType.age)
					{
						this.AgeAnswer();
						return;
					}
					if (this.questions[j].questionType == Question.QuestionType.location)
					{
						this.LocationAnswer();
						return;
					}
				}
			}
		}
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00037392 File Offset: 0x00035592
	private void DifficultyAnswer()
	{
		this.view.RPC("PlayDifficultySound", PhotonTargets.All, new object[]
		{
			Random.Range(0, this.difficultyAnswerClips.Length)
		});
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x000373C4 File Offset: 0x000355C4
	[PunRPC]
	private void PlayDifficultySound(int index)
	{
		this.soundSource.clip = this.difficultyAnswerClips[index];
		this.soundSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		this.soundSource.Play();
		DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.SpiritBoxResponse, 1);
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00037424 File Offset: 0x00035624
	[PunRPC]
	private void PlayLocationSound(int index)
	{
		this.soundSource.clip = this.locationAnswerClips[index];
		this.soundSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		this.soundSource.Play();
		DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.SpiritBoxResponse, 1);
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00037484 File Offset: 0x00035684
	[PunRPC]
	private void PlayAboutSound(int index)
	{
		this.soundSource.clip = this.aboutAnswerClips[index];
		this.soundSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		this.soundSource.Play();
		DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.SpiritBoxResponse, 1);
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x000374E1 File Offset: 0x000356E1
	public void PlayTrailerSound()
	{
		this.soundSource.clip = this.trailerSoundClip;
		this.soundSource.Play();
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x00037500 File Offset: 0x00035700
	private void LocationAnswer()
	{
		if (Vector3.Distance(GameController.instance.myPlayer.player.headObject.transform.position, LevelController.instance.currentGhost.transform.position) < 4f)
		{
			this.view.RPC("PlayLocationSound", PhotonTargets.All, new object[]
			{
				Random.Range(0, 3)
			});
			return;
		}
		this.view.RPC("PlayLocationSound", PhotonTargets.All, new object[]
		{
			Random.Range(3, 6)
		});
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x00037598 File Offset: 0x00035798
	private void AgeAnswer()
	{
		if (Random.Range(0, 2) == 0)
		{
			if (LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostAge < 5)
			{
				this.view.RPC("PlayAboutSound", PhotonTargets.All, new object[]
				{
					1
				});
				return;
			}
			if (LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostAge < 21)
			{
				this.view.RPC("PlayAboutSound", PhotonTargets.All, new object[]
				{
					5
				});
				return;
			}
			this.view.RPC("PlayAboutSound", PhotonTargets.All, new object[]
			{
				0
			});
			return;
		}
		else
		{
			if (LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostAge >= 21)
			{
				this.view.RPC("PlayAboutSound", PhotonTargets.All, new object[]
				{
					7
				});
				return;
			}
			if (Random.Range(0, 1) == 1)
			{
				this.view.RPC("PlayAboutSound", PhotonTargets.All, new object[]
				{
					9
				});
				return;
			}
			this.view.RPC("PlayAboutSound", PhotonTargets.All, new object[]
			{
				2
			});
			return;
		}
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x000376D8 File Offset: 0x000358D8
	private void GenderAnswer()
	{
		if (LevelController.instance.currentGhost.ghostInfo.ghostTraits.isMale)
		{
			if (LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostAge < 21)
			{
				this.view.RPC("PlayAboutSound", PhotonTargets.All, new object[]
				{
					8
				});
				return;
			}
			this.view.RPC("PlayAboutSound", PhotonTargets.All, new object[]
			{
				3
			});
			return;
		}
		else
		{
			if (LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostAge < 21)
			{
				this.view.RPC("PlayAboutSound", PhotonTargets.All, new object[]
				{
					4
				});
				return;
			}
			this.view.RPC("PlayAboutSound", PhotonTargets.All, new object[]
			{
				6
			});
			return;
		}
	}

	// Token: 0x0400091F RID: 2335
	[SerializeField]
	private Text fmText;

	// Token: 0x04000920 RID: 2336
	public AudioSource loopSource;

	// Token: 0x04000921 RID: 2337
	public AudioSource soundSource;

	// Token: 0x04000922 RID: 2338
	[SerializeField]
	private AudioClip[] difficultyAnswerClips = new AudioClip[0];

	// Token: 0x04000923 RID: 2339
	[SerializeField]
	private AudioClip[] locationAnswerClips = new AudioClip[0];

	// Token: 0x04000924 RID: 2340
	[SerializeField]
	private AudioClip[] aboutAnswerClips = new AudioClip[0];

	// Token: 0x04000925 RID: 2341
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000926 RID: 2342
	[SerializeField]
	private Noise noise;

	// Token: 0x04000927 RID: 2343
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000928 RID: 2344
	private bool isOn;

	// Token: 0x04000929 RID: 2345
	private float scanTimer = 0.1f;

	// Token: 0x0400092A RID: 2346
	private float currentFMChannel = 100f;

	// Token: 0x0400092B RID: 2347
	private bool isAddingFM;

	// Token: 0x0400092C RID: 2348
	private List<Question> questions = new List<Question>();

	// Token: 0x0400092D RID: 2349
	private List<string> yesQuestions = new List<string>();

	// Token: 0x0400092E RID: 2350
	private List<string> noQuestions = new List<string>();

	// Token: 0x0400092F RID: 2351
	private float responseTimer;

	// Token: 0x04000930 RID: 2352
	[SerializeField]
	private AudioClip trailerSoundClip;

	// Token: 0x04000931 RID: 2353
	private bool hasAnswered = true;
}
