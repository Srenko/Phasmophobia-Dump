using System;
using System.Collections;
using System.Collections.Generic;
using Photon;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Rendering;

// Token: 0x020000EB RID: 235
public class GhostAI : Photon.MonoBehaviour
{
	// Token: 0x0600065C RID: 1628 RVA: 0x00023448 File Offset: 0x00021648
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.ghostInfo = base.GetComponent<GhostInfo>();
		this.ghostAudio = base.GetComponent<GhostAudio>();
		this.agent = base.GetComponent<NavMeshAgent>();
		this.ghostInteraction = base.GetComponent<GhostInteraction>();
		this.sanityDrainer = base.GetComponent<SanityDrainer>();
		this.canAttack = true;
		this.defaultSpeed = this.agent.speed;
		this.sanityDrainer.enabled = false;
		foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>())
		{
			this.myRends.Add(renderer);
			renderer.enabled = false;
		}
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x000234F0 File Offset: 0x000216F0
	private void Start()
	{
		LevelController.instance.currentGhost = this;
		if (!this.view.isMine)
		{
			this.agent.enabled = false;
		}
		if (PhotonNetwork.isMasterClient)
		{
			this.ghostInfo.SyncValues(this.ghostInfo.ghostTraits);
		}
		if (this.view.isMine)
		{
			this.ChangeState(GhostAI.States.idle, null, null);
		}
		base.StartCoroutine(this.SpawnDelay());
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x00023561 File Offset: 0x00021761
	private void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		if (PhotonNetwork.isMasterClient)
		{
			this.agent.enabled = true;
			this.ChangeState(GhostAI.States.favouriteRoom, null, null);
		}
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0002357F File Offset: 0x0002177F
	private IEnumerator SpawnDelay()
	{
		yield return new WaitForSeconds(2f);
		if (PhotonNetwork.isMasterClient)
		{
			this.view.RPC("SyncGhostType", PhotonTargets.AllBufferedViaServer, new object[]
			{
				this.ghostInfo.ghostTraits.ghostType.ToString()
			});
		}
		yield break;
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0002358E File Offset: 0x0002178E
	public IEnumerator StartHuntingTimer()
	{
		this.canEnterHuntingMode = false;
		yield return new WaitForSeconds(25f);
		this.canEnterHuntingMode = true;
		yield break;
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x000235A0 File Offset: 0x000217A0
	[PunRPC]
	private void SyncGhostType(string name)
	{
		if (this.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Banshee)
		{
			if (GameController.instance.playersData.Count == PhotonNetwork.room.PlayerCount)
			{
				this.SetInitialBansheeTarget();
				return;
			}
			GameController.instance.OnAllPlayersConnected.AddListener(new UnityAction(this.SetInitialBansheeTarget));
		}
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x00023600 File Offset: 0x00021800
	private void Update()
	{
		if (this.view.isMine && this.stateMachine != null && !GameController.instance.isLoadingBackToMenu)
		{
			this.stateMachine.ExecuteStateUpdate();
		}
		if (this.isHunting)
		{
			this.appearTimer -= Time.deltaTime;
			if (this.appearTimer < 0f)
			{
				this.FlashAppear(Random.Range(0.08f, 0.3f));
				this.appearTimer = ((this.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Phantom) ? Random.Range(1f, 2f) : Random.Range(0.3f, 1f));
			}
		}
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x000236B0 File Offset: 0x000218B0
	public void ChangeState(GhostAI.States state, PhotonObjectInteract obj = null, PhotonObjectInteract[] objects = null)
	{
		if (!this.view.isMine)
		{
			return;
		}
		this.state = state;
		switch (state)
		{
		case GhostAI.States.idle:
			this.stateMachine.ChangeState(new IdleState(this));
			return;
		case GhostAI.States.wander:
			this.stateMachine.ChangeState(new WanderState(this, this.agent));
			return;
		case GhostAI.States.hunting:
			this.stateMachine.ChangeState(new HuntingState(this, this.agent, this.view));
			return;
		case GhostAI.States.favouriteRoom:
			this.stateMachine.ChangeState(new FavouriteRoomState(this, this.agent));
			return;
		case GhostAI.States.light:
			this.stateMachine.ChangeState(new LightState(this, this.ghostInteraction, obj));
			return;
		case GhostAI.States.door:
			this.stateMachine.ChangeState(new DoorState(this, this.ghostInteraction, this.ghostInfo, obj));
			return;
		case GhostAI.States.throwing:
			this.stateMachine.ChangeState(new ThrowingState(this, this.ghostInteraction, obj));
			return;
		case GhostAI.States.fusebox:
			this.stateMachine.ChangeState(new FuseBoxState(this, this.ghostInteraction));
			return;
		case GhostAI.States.appear:
			this.stateMachine.ChangeState(new AppearState(this, this.agent));
			return;
		case GhostAI.States.doorKnock:
			this.stateMachine.ChangeState(new DoorKnockState(this, this.ghostInteraction));
			return;
		case GhostAI.States.windowKnock:
			this.stateMachine.ChangeState(new WindowKnockState(this, this.ghostInteraction, obj));
			return;
		case GhostAI.States.carAlarm:
			this.stateMachine.ChangeState(new CarAlarmState(this, this.ghostInteraction));
			return;
		case GhostAI.States.radio:
			this.stateMachine.ChangeState(new RadioState(this, this.ghostInteraction));
			return;
		case GhostAI.States.flicker:
			this.stateMachine.ChangeState(new LightFlickerState(this, this.ghostInteraction));
			return;
		case GhostAI.States.lockDoor:
			this.stateMachine.ChangeState(new DoorLockState(this, this.ghostInteraction, obj));
			return;
		case GhostAI.States.cctv:
			this.stateMachine.ChangeState(new CCTVState(this, this.ghostInteraction, obj));
			return;
		case GhostAI.States.randomEvent:
			this.RandomEvent();
			return;
		case GhostAI.States.GhostAbility:
			this.PerformGhostAbility(obj, objects);
			return;
		case GhostAI.States.killPlayer:
			this.stateMachine.ChangeState(new KillPlayerState(this, this.agent, this.view));
			return;
		case GhostAI.States.sink:
			this.stateMachine.ChangeState(new SinkState(this, this.ghostInteraction, obj));
			return;
		case GhostAI.States.sound:
			this.stateMachine.ChangeState(new SoundState(this, this.ghostInteraction, obj));
			return;
		case GhostAI.States.painting:
			this.stateMachine.ChangeState(new PaintingState(this, this.ghostInteraction, obj));
			return;
		case GhostAI.States.mannequin:
			this.stateMachine.ChangeState(new MannequinState(this, this.ghostInteraction, obj));
			return;
		case GhostAI.States.teleportObject:
			this.stateMachine.ChangeState(new TeleportObjectState(this, this.ghostInteraction, obj));
			return;
		case GhostAI.States.animationObject:
			this.stateMachine.ChangeState(new AnimationObjectState(this, this.ghostInteraction, obj));
			return;
		default:
			Debug.LogError("There is no AI switch statement for state: " + state);
			return;
		}
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x000239A8 File Offset: 0x00021BA8
	public void RandomEvent()
	{
		switch (Random.Range(0, 4))
		{
		case 0:
			this.stateMachine.ChangeState(new GhostEvent_1(this, this.ghostInteraction));
			return;
		case 1:
			this.stateMachine.ChangeState(new GhostEvent_2(this, this.ghostInteraction));
			return;
		case 2:
			this.stateMachine.ChangeState(new GhostEvent_3(this, this.ghostInteraction));
			return;
		case 3:
			this.stateMachine.ChangeState(new GhostEvent_4(this, this.ghostInteraction));
			return;
		default:
			Debug.LogError("Failed: trying to start a random Ghost event that has not been setup.");
			return;
		}
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x00023A40 File Offset: 0x00021C40
	private void PerformGhostAbility(PhotonObjectInteract obj = null, PhotonObjectInteract[] objects = null)
	{
		switch (this.ghostInfo.ghostTraits.ghostType)
		{
		case GhostTraits.Type.Spirit:
		case GhostTraits.Type.Mare:
		case GhostTraits.Type.Revenant:
			break;
		case GhostTraits.Type.Wraith:
			this.stateMachine.ChangeState(new WraithPower(this, this.ghostInteraction, this.agent));
			return;
		case GhostTraits.Type.Phantom:
			this.stateMachine.ChangeState(new PhantomPower(this, this.ghostInteraction, this.agent));
			return;
		case GhostTraits.Type.Poltergeist:
			this.stateMachine.ChangeState(new PoltergeistPower(this, this.ghostInteraction, this.mask, objects));
			return;
		case GhostTraits.Type.Banshee:
			this.stateMachine.ChangeState(new BansheePower(this, this.ghostInteraction, this.ghostAudio, this.agent, this.mask));
			return;
		case GhostTraits.Type.Jinn:
			this.stateMachine.ChangeState(new JinnPower(this, this.ghostInteraction));
			break;
		default:
			return;
		}
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x00023B23 File Offset: 0x00021D23
	public IEnumerator ResetRigidbody(Rigidbody rigid, Door door = null)
	{
		yield return new WaitForSeconds(3f);
		rigid.isKinematic = true;
		if (door)
		{
			door.UnGrabbedDoor();
		}
		yield break;
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x00023B39 File Offset: 0x00021D39
	public IEnumerator StopGhostFromHunting()
	{
		this.canAttack = false;
		yield return new WaitForSeconds((float)((this.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Spirit) ? 180 : 90));
		this.canAttack = true;
		yield break;
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x00023B48 File Offset: 0x00021D48
	public IEnumerator StopHuntingFortime()
	{
		if (!this.isHunting)
		{
			yield return null;
		}
		this.smudgeSticksUsed = true;
		yield return new WaitForSeconds(1f);
		this.agent.SetDestination(base.transform.position);
		yield return new WaitForSeconds(5f);
		this.smudgeSticksUsed = false;
		yield break;
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x00023B57 File Offset: 0x00021D57
	public IEnumerator TemporarilyStopWander()
	{
		this.canWander = false;
		yield return new WaitForSeconds(90f);
		this.canWander = true;
		yield break;
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x00023B68 File Offset: 0x00021D68
	[PunRPC]
	private void Hunting(bool isHunting)
	{
		this.isHunting = isHunting;
		if (isHunting)
		{
			for (int i = 0; i < LevelController.instance.fuseBox.switches.Count; i++)
			{
				LevelController.instance.fuseBox.switches[i].StartBlinking();
			}
			for (int j = 0; j < GameController.instance.playersData.Count; j++)
			{
				if (GameController.instance.playersData[j].player.pcFlashlight != null)
				{
					GameController.instance.playersData[j].player.pcFlashlight.TurnBlinkOnOrOff(true);
				}
			}
			this.canEnterHuntingMode = false;
			return;
		}
		for (int k = 0; k < LevelController.instance.fuseBox.switches.Count; k++)
		{
			LevelController.instance.fuseBox.switches[k].StopBlinking();
		}
		for (int l = 0; l < LevelController.instance.torches.Count; l++)
		{
			if (LevelController.instance.torches[l] != null)
			{
				LevelController.instance.torches[l].TurnBlinkOff();
			}
		}
		for (int m = 0; m < GameController.instance.playersData.Count; m++)
		{
			if (GameController.instance.playersData[m].player.pcFlashlight != null)
			{
				GameController.instance.playersData[m].player.pcFlashlight.TurnBlinkOnOrOff(false);
			}
		}
		this.ghostAudio.TurnOnOrOffAppearSource(false);
		this.ghostAudio.PlayOrStopAppearSource(false);
		this.UnAppear(false);
		base.StartCoroutine(this.StartHuntingTimer());
		if (!GameController.instance.myPlayer.player.isDead)
		{
			DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.SurviveHunting, 1);
		}
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x00023D50 File Offset: 0x00021F50
	[PunRPC]
	private void MakeGhostAppear(bool appear, int randNum)
	{
		if (!appear && !this.ghostIsAppeared)
		{
			return;
		}
		this.ghostIsAppeared = appear;
		foreach (Renderer renderer in this.myRends)
		{
			renderer.enabled = appear;
		}
		if (!GameController.instance.myPlayer.player.isDead)
		{
			this.sanityDrainer.enabled = appear;
		}
		if (appear)
		{
			if (!GameController.instance.myPlayer.player.isDead && randNum == 1)
			{
				for (int i = 0; i < this.myRends.Count; i++)
				{
					this.myRends[i].shadowCastingMode = ShadowCastingMode.ShadowsOnly;
				}
				return;
			}
		}
		else
		{
			for (int j = 0; j < this.myRends.Count; j++)
			{
				this.myRends[j].shadowCastingMode = ShadowCastingMode.On;
			}
		}
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x00023E4C File Offset: 0x0002204C
	public void UnAppear(bool isEvent)
	{
		this.view.RPC("MakeGhostAppear", PhotonTargets.All, new object[]
		{
			false,
			Random.Range(0, isEvent ? 2 : 3)
		});
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x00023E83 File Offset: 0x00022083
	public void Appear(bool isEvent)
	{
		this.view.RPC("MakeGhostAppear", PhotonTargets.All, new object[]
		{
			true,
			Random.Range(0, isEvent ? 2 : 3)
		});
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x00023EBA File Offset: 0x000220BA
	public void FlashAppear(float timer)
	{
		if (!GameController.instance.myPlayer.player.isDead)
		{
			base.StartCoroutine(this.Flash(timer));
		}
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x00023EE0 File Offset: 0x000220E0
	private IEnumerator Flash(float timer)
	{
		this.ghostIsAppeared = true;
		this.ghostAudio.PlayOrStopAppearSource(true);
		foreach (Renderer renderer in this.myRends)
		{
			renderer.enabled = true;
		}
		yield return new WaitForSeconds(timer);
		this.ghostIsAppeared = false;
		this.ghostAudio.PlayOrStopAppearSource(false);
		using (List<Renderer>.Enumerator enumerator = this.myRends.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Renderer renderer2 = enumerator.Current;
				renderer2.enabled = false;
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x00023EF6 File Offset: 0x000220F6
	public void JinnPowerDistanceCheck()
	{
		this.view.RPC("JinnPowerDistanceCheckNetworked", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00023F10 File Offset: 0x00022110
	[PunRPC]
	private void JinnPowerDistanceCheckNetworked()
	{
		if (Vector3.Distance(base.transform.position, GameController.instance.myPlayer.player.transform.position) < 3f)
		{
			GameController.instance.myPlayer.player.insanity += 25f;
		}
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x00023F70 File Offset: 0x00022170
	public void SetInitialBansheeTarget()
	{
		this.view.RPC("SetBansheeTargetNetworked", PhotonTargets.AllBufferedViaServer, new object[]
		{
			GameController.instance.playersData[Random.Range(0, GameController.instance.playersData.Count)].actorID
		});
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x00023FC8 File Offset: 0x000221C8
	public void SetNewBansheeTarget()
	{
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (!GameController.instance.playersData[i].player.isDead)
			{
				this.view.RPC("SetBansheeTargetNetworked", PhotonTargets.AllBufferedViaServer, new object[]
				{
					GameController.instance.playersData[i].actorID
				});
				return;
			}
		}
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00024040 File Offset: 0x00022240
	[PunRPC]
	private void SetBansheeTargetNetworked(int actorID)
	{
		for (int i = 0; i < GameController.instance.playersData.Count; i++)
		{
			if (GameController.instance.playersData[i].actorID == actorID)
			{
				this.bansheeTarget = GameController.instance.playersData[i].player;
				return;
			}
		}
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0002409B File Offset: 0x0002229B
	public void ActivateGhostParticleEvidence()
	{
		this.ghostParticle.GetComponent<ParticleSystemRenderer>().enabled = true;
		this.ghostParticle.Play();
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x000240B9 File Offset: 0x000222B9
	public void ChasingPlayer(bool isChasing)
	{
		this.view.RPC("SyncChasingPlayer", PhotonTargets.All, new object[]
		{
			isChasing
		});
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x000240DB File Offset: 0x000222DB
	[PunRPC]
	private void SyncChasingPlayer(bool isChasing)
	{
		this.isChasingPlayer = isChasing;
	}

	// Token: 0x04000634 RID: 1588
	private StateMachine stateMachine = new StateMachine();

	// Token: 0x04000635 RID: 1589
	public GhostAI.States state;

	// Token: 0x04000636 RID: 1590
	[HideInInspector]
	public PhotonView view;

	// Token: 0x04000637 RID: 1591
	public Animator anim;

	// Token: 0x04000638 RID: 1592
	[HideInInspector]
	public GhostInfo ghostInfo;

	// Token: 0x04000639 RID: 1593
	[HideInInspector]
	public NavMeshAgent agent;

	// Token: 0x0400063A RID: 1594
	[HideInInspector]
	public GhostAudio ghostAudio;

	// Token: 0x0400063B RID: 1595
	[HideInInspector]
	public GhostInteraction ghostInteraction;

	// Token: 0x0400063C RID: 1596
	public GhostActivity ghostActivity;

	// Token: 0x0400063D RID: 1597
	[Header("Appear State")]
	[HideInInspector]
	public List<Renderer> myRends = new List<Renderer>();

	// Token: 0x0400063E RID: 1598
	[HideInInspector]
	public bool ghostIsAppeared;

	// Token: 0x0400063F RID: 1599
	[HideInInspector]
	public SanityDrainer sanityDrainer;

	// Token: 0x04000640 RID: 1600
	[Header("Hunting State")]
	public LayerMask mask;

	// Token: 0x04000641 RID: 1601
	public Transform raycastPoint;

	// Token: 0x04000642 RID: 1602
	[HideInInspector]
	public bool isChasingPlayer;

	// Token: 0x04000643 RID: 1603
	[HideInInspector]
	public float defaultSpeed;

	// Token: 0x04000644 RID: 1604
	public bool canEnterHuntingMode = true;

	// Token: 0x04000645 RID: 1605
	private float appearTimer = 1f;

	// Token: 0x04000646 RID: 1606
	[HideInInspector]
	public bool isHunting;

	// Token: 0x04000647 RID: 1607
	[HideInInspector]
	public bool canAttack = true;

	// Token: 0x04000648 RID: 1608
	[HideInInspector]
	public bool smudgeSticksUsed;

	// Token: 0x04000649 RID: 1609
	[HideInInspector]
	public bool canWander;

	// Token: 0x0400064A RID: 1610
	[Header("Banshee")]
	[HideInInspector]
	public Player bansheeTarget;

	// Token: 0x0400064B RID: 1611
	[SerializeField]
	private ParticleSystem ghostParticle;

	// Token: 0x0400064C RID: 1612
	[HideInInspector]
	public Player playerToKill;

	// Token: 0x02000504 RID: 1284
	public enum States
	{
		// Token: 0x04002419 RID: 9241
		idle,
		// Token: 0x0400241A RID: 9242
		wander,
		// Token: 0x0400241B RID: 9243
		hunting,
		// Token: 0x0400241C RID: 9244
		favouriteRoom,
		// Token: 0x0400241D RID: 9245
		light,
		// Token: 0x0400241E RID: 9246
		door,
		// Token: 0x0400241F RID: 9247
		throwing,
		// Token: 0x04002420 RID: 9248
		fusebox,
		// Token: 0x04002421 RID: 9249
		appear,
		// Token: 0x04002422 RID: 9250
		doorKnock,
		// Token: 0x04002423 RID: 9251
		windowKnock,
		// Token: 0x04002424 RID: 9252
		carAlarm,
		// Token: 0x04002425 RID: 9253
		radio,
		// Token: 0x04002426 RID: 9254
		flicker,
		// Token: 0x04002427 RID: 9255
		lockDoor,
		// Token: 0x04002428 RID: 9256
		cctv,
		// Token: 0x04002429 RID: 9257
		randomEvent,
		// Token: 0x0400242A RID: 9258
		GhostAbility,
		// Token: 0x0400242B RID: 9259
		killPlayer,
		// Token: 0x0400242C RID: 9260
		sink,
		// Token: 0x0400242D RID: 9261
		sound,
		// Token: 0x0400242E RID: 9262
		painting,
		// Token: 0x0400242F RID: 9263
		mannequin,
		// Token: 0x04002430 RID: 9264
		teleportObject,
		// Token: 0x04002431 RID: 9265
		animationObject
	}
}
