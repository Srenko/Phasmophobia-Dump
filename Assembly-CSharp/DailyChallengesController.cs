using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012F RID: 303
public class DailyChallengesController
{
	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x06000808 RID: 2056 RVA: 0x000303EB File Offset: 0x0002E5EB
	public static DailyChallengesController Instance
	{
		get
		{
			if (DailyChallengesController._instance == null)
			{
				DailyChallengesController._instance = new DailyChallengesController();
				DailyChallengesController._instance.Setup();
			}
			return DailyChallengesController._instance;
		}
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x00030410 File Offset: 0x0002E610
	private void Setup()
	{
		this.currentChallenges.Clear();
		this.challengeList.CreateList();
		this.todaysChallengeSeed = new Random(DateTime.UtcNow.Day);
		this.seed = this.todaysChallengeSeed.Next();
		if (PlayerPrefs.GetInt("currentChallengesSeed") == this.seed)
		{
			this.GenerateChallenge(PlayerPrefs.GetInt("challenge1"), 1);
			this.GenerateChallenge(PlayerPrefs.GetInt("challenge2"), 2);
			this.GenerateChallenge(PlayerPrefs.GetInt("challenge3"), 3);
		}
		else
		{
			this.GenerateNewDailyChallenges();
		}
		PlayerPrefs.SetInt("currentChallengesSeed", this.seed);
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x000304BC File Offset: 0x0002E6BC
	public void ForceReset()
	{
		this.currentChallenges.Clear();
		this.challengeList.CreateList();
		this.todaysChallengeSeed = new Random(DateTime.UtcNow.Day);
		this.seed = this.todaysChallengeSeed.Next();
		PlayerPrefs.SetInt("currentChallengesSeed", this.seed);
		this.GenerateNewDailyChallenges();
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00030520 File Offset: 0x0002E720
	private void GenerateNewDailyChallenges()
	{
		int num = this.todaysChallengeSeed.Next(0, 4);
		int num2 = this.todaysChallengeSeed.Next(5, 8);
		int num3 = this.todaysChallengeSeed.Next(9, 13);
		PlayerPrefs.SetInt("challenge1", num);
		PlayerPrefs.SetInt("challenge2", num2);
		PlayerPrefs.SetInt("challenge3", num3);
		PlayerPrefs.SetInt("challenge1Progression", 0);
		PlayerPrefs.SetInt("challenge2Progression", 0);
		PlayerPrefs.SetInt("challenge3Progression", 0);
		this.GenerateChallenge(num, 1);
		this.GenerateChallenge(num2, 2);
		this.GenerateChallenge(num3, 3);
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x000305B4 File Offset: 0x0002E7B4
	private void GenerateChallenge(int challengeID, int challengeNumber)
	{
		Challenge challenge = new Challenge();
		challenge.SetupChallenge(challengeNumber, challengeID, this.challengeList.listOfChallenges[challengeID].reward, this.challengeList.listOfChallenges[challengeID].challengeName, this.challengeList.listOfChallenges[challengeID].progressionMaxValue);
		this.currentChallenges.Add(challenge);
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00030620 File Offset: 0x0002E820
	public void ChangeChallengeProgression(ChallengeType type, int value)
	{
		for (int i = 0; i < this.currentChallenges.Count; i++)
		{
			if (this.currentChallenges[i].myChallenge.uniqueChallengeID == this.challengeList.listOfChallenges[(int)type].uniqueChallengeID)
			{
				this.currentChallenges[i].AddProgression(value);
			}
		}
	}

	// Token: 0x04000822 RID: 2082
	private static DailyChallengesController _instance;

	// Token: 0x04000823 RID: 2083
	private ChallengeList challengeList = new ChallengeList();

	// Token: 0x04000824 RID: 2084
	public List<Challenge> currentChallenges = new List<Challenge>();

	// Token: 0x04000825 RID: 2085
	private Random todaysChallengeSeed;

	// Token: 0x04000826 RID: 2086
	private int seed;
}
