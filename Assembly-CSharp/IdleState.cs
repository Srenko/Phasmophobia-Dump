using System;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class IdleState : IState
{
	// Token: 0x060006F1 RID: 1777 RVA: 0x000291C7 File Offset: 0x000273C7
	public IdleState(GhostAI ghostAI)
	{
		this.ghostAI = ghostAI;
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x000291E0 File Offset: 0x000273E0
	public void Enter()
	{
		this.idleTimer = Random.Range(2f, 6f);
		this.ghostAI.anim.SetInteger("IdleNumber", Random.Range(0, 2));
		this.ghostAI.anim.SetBool("isIdle", true);
		this.ghostAI.UnAppear(false);
		this.ghostAI.ghostAudio.TurnOnOrOffAppearSource(false);
		this.ghostAI.ghostAudio.PlayOrStopAppearSource(false);
		if (GameController.instance.levelDifficulty == Contract.LevelDifficulty.Amateur)
		{
			this.maxRandomAbilityValue = 100;
		}
		else if (GameController.instance.levelDifficulty == Contract.LevelDifficulty.Intermediate)
		{
			this.maxRandomAbilityValue = 115;
		}
		else if (GameController.instance.levelDifficulty == Contract.LevelDifficulty.Professional)
		{
			this.maxRandomAbilityValue = 130;
		}
		if (this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Oni && LevelController.instance.currentGhostRoom.playersInRoom.Count > 0)
		{
			this.OniMultiplier = 30;
		}
		if (this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Mare)
		{
			if (LevelController.instance.currentGhostRoom.lightSwitches.Count > 0)
			{
				if (LevelController.instance.currentGhostRoom.lightSwitches[0].isOn)
				{
					this.huntingMultiplier -= 10;
				}
				else
				{
					this.huntingMultiplier += 10;
				}
			}
			else
			{
				this.huntingMultiplier += 10;
			}
		}
		if (this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Wraith && LevelController.instance.currentGhost.ghostInteraction.hasWalkedInSalt)
		{
			this.WraithMultiplier = 50;
		}
		if (!SetupPhaseController.instance.mainDoorHasUnlocked)
		{
			this.ghostAI.ChangeState(GhostAI.States.favouriteRoom, null, null);
			return;
		}
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x000293AC File Offset: 0x000275AC
	public void Execute()
	{
		this.idleTimer -= Time.deltaTime;
		if (this.idleTimer < 0f)
		{
			this.ghostAI.anim.SetBool("isIdle", false);
			int num = (int)GameController.instance.GetAveragePlayerInsanity();
			this.huntingMultiplier += ((this.ghostAI.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Demon) ? 15 : 0);
			if (Random.Range(0, 2) == 1 && !GameController.instance.isTutorial && this.ghostAI.canEnterHuntingMode && this.ghostAI.canAttack)
			{
				if (num + this.huntingMultiplier >= 50 && num + this.huntingMultiplier < 75)
				{
					if (Random.Range(0, 5) == 1)
					{
						this.ghostAI.ChangeState(GhostAI.States.hunting, null, null);
						return;
					}
				}
				else if (num + this.huntingMultiplier >= 75 && Random.Range(0, 3) == 1)
				{
					this.ghostAI.ChangeState(GhostAI.States.hunting, null, null);
					return;
				}
			}
			float num2 = Mathf.Clamp((float)num + this.ghostAI.ghostInfo.activityMultiplier + (float)this.WraithMultiplier + (float)this.OniMultiplier + (float)((PhotonNetwork.playerList.Length == 1) ? 15 : 0), 0f, 100f);
			if ((float)Random.Range(0, this.maxRandomAbilityValue) <= num2 && Random.Range(0, 2) == 1)
			{
				int num3 = Random.Range(0, 11);
				if (num3 == 0 || num3 == 1 || num3 == 2 || num3 == 3 || num3 == 4)
				{
					this.ghostAI.ghostActivity.Interact();
					return;
				}
				if (num3 == 5 || num3 == 6 || num3 == 7 || num3 == 8)
				{
					this.ghostAI.ghostActivity.GhostAbility();
					return;
				}
				if (Random.Range(0, 3) == 1)
				{
					this.ghostAI.ChangeState(GhostAI.States.wander, null, null);
					return;
				}
				this.ghostAI.ChangeState(GhostAI.States.favouriteRoom, null, null);
				return;
			}
			else
			{
				if (Random.Range(0, 5) == 1)
				{
					this.ghostAI.ChangeState(GhostAI.States.wander, null, null);
					return;
				}
				if (Random.Range(0, 4) == 1)
				{
					this.ghostAI.ghostActivity.Interact();
					return;
				}
				this.ghostAI.ChangeState(GhostAI.States.favouriteRoom, null, null);
			}
		}
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x00003F60 File Offset: 0x00002160
	public void Exit()
	{
	}

	// Token: 0x040006AD RID: 1709
	private float idleTimer;

	// Token: 0x040006AE RID: 1710
	private GhostAI ghostAI;

	// Token: 0x040006AF RID: 1711
	private int maxRandomAbilityValue = 100;

	// Token: 0x040006B0 RID: 1712
	private int OniMultiplier;

	// Token: 0x040006B1 RID: 1713
	private int WraithMultiplier;

	// Token: 0x040006B2 RID: 1714
	private int huntingMultiplier;
}
