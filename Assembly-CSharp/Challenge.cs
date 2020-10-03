using System;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class Challenge
{
	// Token: 0x06000801 RID: 2049 RVA: 0x0002FE4C File Offset: 0x0002E04C
	public void SetupChallenge(int _dailyChallengeID, int _uniqueID, int _reward, string _challengeName, int _progressionMaxValue)
	{
		this.dailyChallengeID = _dailyChallengeID;
		this.myChallenge.uniqueChallengeID = _uniqueID;
		this.myChallenge.reward = _reward;
		this.myChallenge.challengeName = LocalisationSystem.GetLocalisedValue(_challengeName);
		this.myChallenge.progressionMaxValue = _progressionMaxValue;
		this.completed = (PlayerPrefs.GetInt("challenge" + this.dailyChallengeID + "Completed") == 1);
		this.progressionValue = PlayerPrefs.GetInt("challenge" + this.dailyChallengeID + "Progression");
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x0002FEE4 File Offset: 0x0002E0E4
	public string GetLocalisedName()
	{
		return LocalisationSystem.GetLocalisedValue(this.myChallenge.challengeName);
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x0002FEF8 File Offset: 0x0002E0F8
	public void AddProgression(int value)
	{
		this.progressionValue += value;
		PlayerPrefs.SetInt("challenge" + this.dailyChallengeID + "Progression", this.progressionValue);
		if (this.progressionValue >= this.myChallenge.progressionMaxValue)
		{
			this.Completed();
		}
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x0002FF54 File Offset: 0x0002E154
	private void Completed()
	{
		if (!this.completed)
		{
			this.completed = true;
			PlayerPrefs.SetInt("challenge" + this.dailyChallengeID + "Completed", 1);
			FileBasedPrefs.SetInt("PlayersMoney", FileBasedPrefs.GetInt("PlayersMoney", 0) + this.myChallenge.reward);
			this.progressionValue = this.myChallenge.progressionMaxValue;
			PlayerPrefs.SetInt("challenge" + this.dailyChallengeID + "Progression", this.progressionValue);
		}
	}

	// Token: 0x0400080E RID: 2062
	[HideInInspector]
	public PhotonView view;

	// Token: 0x0400080F RID: 2063
	public bool completed;

	// Token: 0x04000810 RID: 2064
	private int dailyChallengeID;

	// Token: 0x04000811 RID: 2065
	public int progressionValue;

	// Token: 0x04000812 RID: 2066
	public Challenge.ChallengeValues myChallenge;

	// Token: 0x0200051D RID: 1309
	public struct ChallengeValues
	{
		// Token: 0x04002499 RID: 9369
		public int uniqueChallengeID;

		// Token: 0x0400249A RID: 9370
		public int reward;

		// Token: 0x0400249B RID: 9371
		public string challengeName;

		// Token: 0x0400249C RID: 9372
		public int progressionMaxValue;
	}
}
