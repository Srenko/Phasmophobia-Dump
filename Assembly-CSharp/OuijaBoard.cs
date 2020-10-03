using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

// Token: 0x02000161 RID: 353
public class OuijaBoard : MonoBehaviour
{
	// Token: 0x06000969 RID: 2409 RVA: 0x000391E0 File Offset: 0x000373E0
	private void Awake()
	{
		this.noise = base.GetComponentInChildren<Noise>();
		this.startPosition = base.transform.localPosition;
		this.markerDestination = this.startPosition;
		this.source = this.marker.GetComponent<AudioSource>();
		this.view = base.GetComponent<PhotonView>();
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.rend = base.GetComponent<Renderer>();
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0003925C File Offset: 0x0003745C
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
		this.SetupKeywords();
		SpeechRecognitionController.instance.AddOuijaBoard(this);
		if (PhotonNetwork.isMasterClient && Random.Range(0, 2) == 1 && PlayerPrefs.GetInt("isYoutuberVersion") == 0)
		{
			base.Invoke("DisableDelay", 10f);
		}
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x000392BE File Offset: 0x000374BE
	private void DisableDelay()
	{
		this.view.RPC("DisableOuijaBoard", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0000AC1C File Offset: 0x00008E1C
	[PunRPC]
	private void DisableOuijaBoard()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x000392D8 File Offset: 0x000374D8
	public void SetupKeywords()
	{
		Question item = new Question
		{
			questions = new List<string>
			{
				LocalisationSystem.GetLocalisedValue("Ouija_Victim1"),
				LocalisationSystem.GetLocalisedValue("Ouija_Victim2"),
				LocalisationSystem.GetLocalisedValue("Ouija_Victim3"),
				LocalisationSystem.GetLocalisedValue("Ouija_Victim4"),
				LocalisationSystem.GetLocalisedValue("Ouija_Victim5"),
				LocalisationSystem.GetLocalisedValue("Ouija_Victim6"),
				LocalisationSystem.GetLocalisedValue("Ouija_Victim7"),
				LocalisationSystem.GetLocalisedValue("Ouija_Victim8")
			},
			answerType = Question.AnswerType.victim
		};
		this.questions.Add(item);
		item = new Question
		{
			questions = new List<string>
			{
				LocalisationSystem.GetLocalisedValue("Ouija_Age1"),
				LocalisationSystem.GetLocalisedValue("Ouija_Age2"),
				LocalisationSystem.GetLocalisedValue("Ouija_Age3"),
				LocalisationSystem.GetLocalisedValue("Ouija_Age4")
			},
			answerType = Question.AnswerType.age
		};
		this.questions.Add(item);
		item = new Question
		{
			questions = new List<string>
			{
				LocalisationSystem.GetLocalisedValue("Ouija_Dead1"),
				LocalisationSystem.GetLocalisedValue("Ouija_Dead2"),
				LocalisationSystem.GetLocalisedValue("Ouija_Dead3"),
				LocalisationSystem.GetLocalisedValue("Ouija_Dead4"),
				LocalisationSystem.GetLocalisedValue("Ouija_Dead5")
			},
			answerType = Question.AnswerType.dead
		};
		this.questions.Add(item);
		item = new Question
		{
			questions = new List<string>
			{
				LocalisationSystem.GetLocalisedValue("Ouija_RoomAmount1"),
				LocalisationSystem.GetLocalisedValue("Ouija_RoomAmount2"),
				LocalisationSystem.GetLocalisedValue("Ouija_RoomAmount3"),
				LocalisationSystem.GetLocalisedValue("Ouija_RoomAmount4"),
				LocalisationSystem.GetLocalisedValue("Ouija_RoomAmount5"),
				LocalisationSystem.GetLocalisedValue("Ouija_RoomAmount6"),
				LocalisationSystem.GetLocalisedValue("Ouija_RoomAmount7"),
				LocalisationSystem.GetLocalisedValue("Ouija_RoomAmount8"),
				LocalisationSystem.GetLocalisedValue("Ouija_RoomAmount9")
			},
			answerType = Question.AnswerType.roomAmount
		};
		this.questions.Add(item);
		item = new Question
		{
			questions = new List<string>
			{
				LocalisationSystem.GetLocalisedValue("Ouija_Location1"),
				LocalisationSystem.GetLocalisedValue("Ouija_Location2"),
				LocalisationSystem.GetLocalisedValue("Ouija_Location3"),
				LocalisationSystem.GetLocalisedValue("Ouija_Location4"),
				LocalisationSystem.GetLocalisedValue("Ouija_Location5"),
				LocalisationSystem.GetLocalisedValue("Ouija_Location6"),
				LocalisationSystem.GetLocalisedValue("Ouija_Location7"),
				LocalisationSystem.GetLocalisedValue("Ouija_Location8"),
				LocalisationSystem.GetLocalisedValue("Ouija_Location9")
			},
			answerType = Question.AnswerType.location
		};
		this.questions.Add(item);
		for (int i = 0; i < this.questions.Count; i++)
		{
			for (int j = 0; j < this.questions[i].questions.Count; j++)
			{
				SpeechRecognitionController.instance.AddKeyword(this.questions[i].questions[j]);
			}
		}
		for (int k = 0; k < this.yesOrNoQuestions.Count; k++)
		{
			for (int l = 0; l < this.yesOrNoQuestions[k].questions.Count; l++)
			{
				SpeechRecognitionController.instance.AddKeyword(this.yesOrNoQuestions[k].questions[l]);
			}
		}
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x000396A4 File Offset: 0x000378A4
	private void Update()
	{
		if (!PhotonNetwork.isMasterClient)
		{
			return;
		}
		if (this.lettersList.Count > 0)
		{
			if (this.markerDestination == this.startPosition)
			{
				this.markerDestination = this.GetLetterPosition(this.lettersList[0]);
			}
			if (Vector3.Distance(this.marker.localPosition, this.markerDestination) < 0.1f && !this.reachedDestination)
			{
				this.reachedDestination = true;
				base.StartCoroutine(this.GetNewLetter());
			}
			this.marker.localPosition = Vector3.MoveTowards(this.marker.localPosition, this.markerDestination, 4f * Time.deltaTime * Random.Range(0f, 2f));
			return;
		}
		if (this.source.isPlaying)
		{
			this.view.RPC("Sound", PhotonTargets.All, new object[]
			{
				false
			});
		}
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x00039798 File Offset: 0x00037998
	private IEnumerator GetNewLetter()
	{
		this.view.RPC("Sound", PhotonTargets.All, new object[]
		{
			false
		});
		yield return new WaitForSeconds(1f);
		this.view.RPC("Sound", PhotonTargets.All, new object[]
		{
			true
		});
		this.lettersList.RemoveAt(0);
		if (this.lettersList.Count > 0)
		{
			this.markerDestination = this.GetLetterPosition(this.lettersList[0]);
		}
		else
		{
			this.markerDestination = this.startPosition;
		}
		this.reachedDestination = false;
		yield break;
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x000397A7 File Offset: 0x000379A7
	private void Use()
	{
		this.view.RPC("OuijaBoardNetworkedUse", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x000397C0 File Offset: 0x000379C0
	[PunRPC]
	private void OuijaBoardNetworkedUse()
	{
		this.inUse = !this.inUse;
		if (this.inUse)
		{
			this.rend.material.EnableKeyword("_EMISSION");
			return;
		}
		this.rend.material.DisableKeyword("_EMISSION");
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0003980F File Offset: 0x00037A0F
	[PunRPC]
	private void Sound(bool on)
	{
		if (on)
		{
			this.source.Play();
			this.noise.gameObject.SetActive(true);
			return;
		}
		this.source.Stop();
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x00039850 File Offset: 0x00037A50
	public void OnPhraseRecognized(string args)
	{
		if (!this.inUse)
		{
			return;
		}
		if (LevelController.instance.currentPlayerRoom == null || LevelController.instance.currentGhostRoom == null)
		{
			return;
		}
		if (LevelController.instance.currentPlayerRoom == LevelController.instance.outsideRoom)
		{
			return;
		}
		if (Vector3.Distance(base.transform.position, GameController.instance.myPlayer.player.headObject.transform.position) > 5f)
		{
			return;
		}
		if (!XRDevice.isPresent && PlayerPrefs.GetInt("localPushToTalkValue") == 0 && PhotonNetwork.room.PlayerCount > 1 && !GameController.instance.myPlayer.player.pcPushToTalk.isPressingPushToTalk)
		{
			return;
		}
		if (Random.Range(0, 3) == 1 && PlayerPrefs.GetInt("isYoutuberVersion") == 0)
		{
			for (int i = 0; i < LevelController.instance.currentPlayerRoom.lightSwitches.Count; i++)
			{
				LevelController.instance.currentPlayerRoom.lightSwitches[i].view.RPC("FlickerNetworked", PhotonTargets.All, Array.Empty<object>());
			}
			if (SetupPhaseController.instance.isSetupPhase)
			{
				SetupPhaseController.instance.ForceEnterHuntingPhase();
			}
			GameController.instance.myPlayer.player.insanity += 40f;
			this.Use();
			return;
		}
		for (int j = 0; j < this.questions.Count; j++)
		{
			for (int k = 0; k < this.questions[j].questions.Count; k++)
			{
				if (args == this.questions[j].questions[k])
				{
					switch (this.questions[j].answerType)
					{
					case Question.AnswerType.dead:
						this.view.RPC("DeadAnswer", PhotonNetwork.masterClient, Array.Empty<object>());
						break;
					case Question.AnswerType.roomAmount:
						this.view.RPC("RoomAnswer", PhotonNetwork.masterClient, Array.Empty<object>());
						break;
					case Question.AnswerType.location:
						this.view.RPC("LocationAnswer", PhotonNetwork.masterClient, Array.Empty<object>());
						break;
					case Question.AnswerType.age:
						this.view.RPC("AgeAnswer", PhotonNetwork.masterClient, Array.Empty<object>());
						break;
					}
					if (LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType != GhostTraits.Type.Demon)
					{
						GameController.instance.myPlayer.player.insanity += Random.Range(5f, 10f);
					}
					return;
				}
			}
		}
		for (int l = 0; l < this.yesOrNoQuestions.Count; l++)
		{
			for (int m = 0; m < this.yesOrNoQuestions[l].questions.Count; m++)
			{
				if (args == this.yesOrNoQuestions[l].questions[m])
				{
					if (Random.Range(0, 3) == 2)
					{
						this.view.RPC("MaybeAnswer", PhotonNetwork.masterClient, Array.Empty<object>());
					}
					else
					{
						GhostInfo ghostInfo = LevelController.instance.currentGhost.ghostInfo;
						if (this.yesOrNoQuestions[l].questionType == YesNoMaybeQuestion.QuestionType.location)
						{
							if (Vector3.Distance(ghostInfo.transform.position, base.transform.position) < 3f)
							{
								this.view.RPC("NoAnswer", PhotonNetwork.masterClient, Array.Empty<object>());
							}
							else
							{
								this.view.RPC("YesAnswer", PhotonNetwork.masterClient, Array.Empty<object>());
							}
						}
						else
						{
							this.view.RPC("MaybeAnswer", PhotonNetwork.masterClient, Array.Empty<object>());
						}
					}
					if (LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType != GhostTraits.Type.Demon)
					{
						GameController.instance.myPlayer.player.insanity += Random.Range(5f, 10f);
					}
					return;
				}
			}
		}
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00039C81 File Offset: 0x00037E81
	[PunRPC]
	private void LocationAnswer()
	{
		this.Answer(LevelController.instance.currentGhostRoom.roomName.ToString());
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x00039CA0 File Offset: 0x00037EA0
	[PunRPC]
	private void RoomAnswer()
	{
		this.Answer((this.GetCurrentRoom().playersInRoom.Count + ((this.GetCurrentRoom() == LevelController.instance.currentGhostRoom) ? 1 : 0)).ToString());
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00039CE7 File Offset: 0x00037EE7
	[PunRPC]
	private void YesAnswer()
	{
		this.Answer("yes");
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x00039CF4 File Offset: 0x00037EF4
	[PunRPC]
	private void NoAnswer()
	{
		this.Answer("no");
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x00039D01 File Offset: 0x00037F01
	[PunRPC]
	private void MaybeAnswer()
	{
		this.Answer("maybe");
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x00039D0E File Offset: 0x00037F0E
	[PunRPC]
	private void AgeAnswer()
	{
		this.Answer(LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostAge.ToString());
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x00039D34 File Offset: 0x00037F34
	[PunRPC]
	private void DeadAnswer()
	{
		this.Answer(LevelController.instance.currentGhost.ghostInfo.ghostTraits.deathLength.ToString());
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00039D5C File Offset: 0x00037F5C
	public void Answer(string msg)
	{
		DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.OuijaBoardResponse, 1);
		if (!PhotonNetwork.isMasterClient)
		{
			return;
		}
		LevelController.instance.currentGhost.ghostInteraction.CreateInteractionEMF(base.transform.position);
		this.lettersList.Clear();
		this.view.RPC("Sound", PhotonTargets.All, new object[]
		{
			true
		});
		msg = msg.ToLower();
		msg = msg.Replace(" ", "");
		if (msg == "yes" || msg == "no" || msg == "maybe")
		{
			this.lettersList.Add(msg);
			return;
		}
		foreach (char c in msg.ToCharArray())
		{
			this.lettersList.Add(c.ToString());
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x00039E44 File Offset: 0x00038044
	private Vector3 GetLetterPosition(string letter)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(letter);
		if (num <= 3859557458U)
		{
			if (num <= 1007465396U)
			{
				if (num <= 856466825U)
				{
					if (num <= 822911587U)
					{
						if (num != 806133968U)
						{
							if (num == 822911587U)
							{
								if (letter == "4")
								{
									return this.letterPositions[29].localPosition;
								}
							}
						}
						else if (letter == "5")
						{
							return this.letterPositions[30].localPosition;
						}
					}
					else if (num != 839689206U)
					{
						if (num == 856466825U)
						{
							if (letter == "6")
							{
								return this.letterPositions[31].localPosition;
							}
						}
					}
					else if (letter == "7")
					{
						return this.letterPositions[32].localPosition;
					}
				}
				else if (num <= 890022063U)
				{
					if (num != 873244444U)
					{
						if (num == 890022063U)
						{
							if (letter == "0")
							{
								return this.letterPositions[35].localPosition;
							}
						}
					}
					else if (letter == "1")
					{
						return this.letterPositions[26].localPosition;
					}
				}
				else if (num != 906799682U)
				{
					if (num != 923577301U)
					{
						if (num == 1007465396U)
						{
							if (letter == "9")
							{
								return this.letterPositions[34].localPosition;
							}
						}
					}
					else if (letter == "2")
					{
						return this.letterPositions[27].localPosition;
					}
				}
				else if (letter == "3")
				{
					return this.letterPositions[28].localPosition;
				}
			}
			else if (num <= 3775669363U)
			{
				if (num <= 1319056784U)
				{
					if (num != 1024243015U)
					{
						if (num == 1319056784U)
						{
							if (letter == "yes")
							{
								return this.letterPositions[36].localPosition;
							}
						}
					}
					else if (letter == "8")
					{
						return this.letterPositions[33].localPosition;
					}
				}
				else if (num != 1647734778U)
				{
					if (num != 3758891744U)
					{
						if (num == 3775669363U)
						{
							if (letter == "d")
							{
								return this.letterPositions[3].localPosition;
							}
						}
					}
					else if (letter == "e")
					{
						return this.letterPositions[4].localPosition;
					}
				}
				else if (letter == "no")
				{
					return this.letterPositions[37].localPosition;
				}
			}
			else if (num <= 3792446982U)
			{
				if (num != 3778851499U)
				{
					if (num == 3792446982U)
					{
						if (letter == "g")
						{
							return this.letterPositions[6].localPosition;
						}
					}
				}
				else if (letter == "maybe")
				{
					return this.letterPositions[38].localPosition;
				}
			}
			else if (num != 3809224601U)
			{
				if (num != 3826002220U)
				{
					if (num == 3859557458U)
					{
						if (letter == "c")
						{
							return this.letterPositions[2].localPosition;
						}
					}
				}
				else if (letter == "a")
				{
					return this.letterPositions[0].localPosition;
				}
			}
			else if (letter == "f")
			{
				return this.letterPositions[5].localPosition;
			}
		}
		else if (num <= 4027333648U)
		{
			if (num <= 3943445553U)
			{
				if (num <= 3893112696U)
				{
					if (num != 3876335077U)
					{
						if (num == 3893112696U)
						{
							if (letter == "m")
							{
								return this.letterPositions[12].localPosition;
							}
						}
					}
					else if (letter == "b")
					{
						return this.letterPositions[1].localPosition;
					}
				}
				else if (num != 3909890315U)
				{
					if (num != 3926667934U)
					{
						if (num == 3943445553U)
						{
							if (letter == "n")
							{
								return this.letterPositions[13].localPosition;
							}
						}
					}
					else if (letter == "o")
					{
						return this.letterPositions[14].localPosition;
					}
				}
				else if (letter == "l")
				{
					return this.letterPositions[11].localPosition;
				}
			}
			else if (num <= 3977000791U)
			{
				if (num != 3960223172U)
				{
					if (num == 3977000791U)
					{
						if (letter == "h")
						{
							return this.letterPositions[7].localPosition;
						}
					}
				}
				else if (letter == "i")
				{
					return this.letterPositions[8].localPosition;
				}
			}
			else if (num != 3993778410U)
			{
				if (num != 4010556029U)
				{
					if (num == 4027333648U)
					{
						if (letter == "u")
						{
							return this.letterPositions[20].localPosition;
						}
					}
				}
				else if (letter == "j")
				{
					return this.letterPositions[9].localPosition;
				}
			}
			else if (letter == "k")
			{
				return this.letterPositions[10].localPosition;
			}
		}
		else if (num <= 4111221743U)
		{
			if (num <= 4060888886U)
			{
				if (num != 4044111267U)
				{
					if (num == 4060888886U)
					{
						if (letter == "w")
						{
							return this.letterPositions[22].localPosition;
						}
					}
				}
				else if (letter == "t")
				{
					return this.letterPositions[19].localPosition;
				}
			}
			else if (num != 4077666505U)
			{
				if (num != 4094444124U)
				{
					if (num == 4111221743U)
					{
						if (letter == "p")
						{
							return this.letterPositions[15].localPosition;
						}
					}
				}
				else if (letter == "q")
				{
					return this.letterPositions[16].localPosition;
				}
			}
			else if (letter == "v")
			{
				return this.letterPositions[21].localPosition;
			}
		}
		else if (num <= 4144776981U)
		{
			if (num != 4127999362U)
			{
				if (num == 4144776981U)
				{
					if (letter == "r")
					{
						return this.letterPositions[17].localPosition;
					}
				}
			}
			else if (letter == "s")
			{
				return this.letterPositions[18].localPosition;
			}
		}
		else if (num != 4228665076U)
		{
			if (num != 4245442695U)
			{
				if (num == 4278997933U)
				{
					if (letter == "z")
					{
						return this.letterPositions[25].localPosition;
					}
				}
			}
			else if (letter == "x")
			{
				return this.letterPositions[23].localPosition;
			}
		}
		else if (letter == "y")
		{
			return this.letterPositions[24].localPosition;
		}
		Debug.LogError(string.Concat(new object[]
		{
			"Letter ",
			letter,
			" could not be found on ",
			this
		}));
		return this.letterPositions[38].localPosition;
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0003A720 File Offset: 0x00038920
	private LevelRoom GetCurrentRoom()
	{
		LevelRoom[] array = Object.FindObjectsOfType<LevelRoom>();
		LevelRoom levelRoom = array[0];
		for (int i = 0; i < array.Length; i++)
		{
			if (Vector3.Distance(base.transform.position, array[i].transform.position) < Vector3.Distance(base.transform.position, levelRoom.transform.position))
			{
				levelRoom = array[i];
			}
		}
		return levelRoom;
	}

	// Token: 0x04000982 RID: 2434
	[SerializeField]
	private List<Question> questions = new List<Question>();

	// Token: 0x04000983 RID: 2435
	private List<YesNoMaybeQuestion> yesOrNoQuestions = new List<YesNoMaybeQuestion>();

	// Token: 0x04000984 RID: 2436
	private bool inUse;

	// Token: 0x04000985 RID: 2437
	public Transform marker;

	// Token: 0x04000986 RID: 2438
	public List<Transform> letterPositions = new List<Transform>();

	// Token: 0x04000987 RID: 2439
	private List<string> lettersList = new List<string>();

	// Token: 0x04000988 RID: 2440
	private Vector3 markerDestination;

	// Token: 0x04000989 RID: 2441
	private Vector3 startPosition;

	// Token: 0x0400098A RID: 2442
	private bool reachedDestination;

	// Token: 0x0400098B RID: 2443
	private AudioSource source;

	// Token: 0x0400098C RID: 2444
	private PhotonView view;

	// Token: 0x0400098D RID: 2445
	private PhotonObjectInteract photonInteract;

	// Token: 0x0400098E RID: 2446
	private Noise noise;

	// Token: 0x0400098F RID: 2447
	private Renderer rend;
}
